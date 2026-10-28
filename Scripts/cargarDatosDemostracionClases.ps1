# Carga de datos de demostracion para clases, inscripciones a clase y asistencia.
# Requiere conexion local a SQLEXPRESS01. ASCII only. Reejecucion NO idiomatica: aborta si ya hay datos.

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Numerics
[System.Threading.Thread]::CurrentThread.CurrentCulture = [System.Globalization.CultureInfo]::GetCultureInfo('es-AR')

$cs = 'Server=localhost\SQLEXPRESS01;Database=is--servicios;Integrated Security=True;'
$conn = New-Object System.Data.SqlClient.SqlConnection
$conn.ConnectionString = $cs
$conn.Open()

$POW64 = [System.Numerics.BigInteger]::Pow(2, 64)

function CalDVH([string]$s) {
    $sha = [System.Security.Cryptography.SHA256]::Create()
    $b = $sha.ComputeHash([System.Text.Encoding]::UTF8.GetBytes($s))
    return [long][Math]::Abs([BitConverter]::ToInt64($b, 0))
}

function Wrap64($big) {
    $m = $big % $POW64
    if ($m -lt 0) { $m += $POW64 }
    if ($m -isnot [System.Numerics.BigInteger]) { $m = [System.Numerics.BigInteger]$m }
    $bytes = $m.ToByteArray()
    if ($bytes.Length -lt 8) { $bytes = $bytes + ([byte[]](New-Object byte[] (8 - $bytes.Length))) }
    if ($bytes.Length -gt 8) { $bytes = $bytes[0..7] }
    return [BitConverter]::ToInt64($bytes, 0)
}

function GetDniDv([string]$dni) {
    $n = $dni.Length
    $ini = if ($n -eq 7) { 5 } else { 4 }
    $suma = 0
    for ($i = 0; $i -lt $n; $i++) {
        $z = [int][char]$dni[$i] - 48
        $x = (($ini + $i) * $z) % 11
        $suma += $x
    }
    $dv = 11 - ($suma % 11)
    if ($dv -eq 11) { $dv = 0 } elseif ($dv -eq 10) { $dv = 9 }
    return $dv
}

function Query($sql) {
    $cmd = $conn.CreateCommand()
    $cmd.Transaction = $script:tx
    $cmd.CommandText = $sql
    $rdr = $cmd.ExecuteReader()
    $list = New-Object System.Collections.ArrayList
    while ($rdr.Read()) {
        $h = @{}
        for ($i = 0; $i -lt $rdr.FieldCount; $i++) { $h[$rdr.GetName($i)] = $rdr.GetValue($i) }
        [void]$list.Add($h)
    }
    $rdr.Close()
    return , $list
}

function NonQuery($sql, $params) {
    $cmd = $conn.CreateCommand()
    $cmd.Transaction = $script:tx
    $cmd.CommandText = $sql
    foreach ($p in $params) {
        $sp = $cmd.CreateParameter()
        $sp.ParameterName = $p[0]
        $sp.Value = $p[1]
        [void]$cmd.Parameters.Add($sp)
    }
    $null = $cmd.ExecuteNonQuery()
}

function InsScalar($sql, $params) {
    $cmd = $conn.CreateCommand()
    $cmd.Transaction = $script:tx
    $cmd.CommandText = $sql
    foreach ($p in $params) {
        $sp = $cmd.CreateParameter()
        $sp.ParameterName = $p[0]
        $sp.Value = $p[1]
        [void]$cmd.Parameters.Add($sp)
    }
    return $cmd.ExecuteScalar()
}

$script:tx = $conn.BeginTransaction()
try {
    # --- Nadadores (solo los nombres pedidos) ---
    $nombres   = @('Maximo','Jeremias','Pablo','Benjamin','Nahuel','Lautaro')
    $apellidos = @('Gomez','Fernandez','Rodriguez','Lopez','Martinez','Diaz')
    $dnisBase  = @('40123450','40123451','40123452','40123453','40123454','40123455')
    $fechasNac = @('2018-03-10','2016-07-22','2014-11-05','2012-01-18','2010-09-30','2008-05-14')
    $edades    = @(8,10,12,14,16,18)
    $categorias = @('Infantil','Menor','Cadete','Juvenil','Junior','Senior')

    $nadadores = New-Object System.Collections.ArrayList
    for ($i = 0; $i -lt 6; $i++) {
        $dni = $dnisBase[$i] + (GetDniDv $dnisBase[$i]).ToString()
        $cert = ($i % 2 -eq 0)
        [void]$nadadores.Add([PSCustomObject]@{
            DNI = $dni; Nombre = $nombres[$i]; Apellido = $apellidos[$i]
            FechaNacimiento = $fechasNac[$i]; Edad = $edades[$i]; Categoria = $categorias[$i]; CertificadoMedico = $cert
        })
    }

    # --- Precondicion de idempotencia ---
    $dnisBuscados = ($nadadores | ForEach-Object { "'" + $_.DNI + "'" }) -join ','
    $rep = Query ("SELECT COUNT(*) AS C FROM Nadador WHERE DNI IN (" + $dnisBuscados + ")")
    if ([int]$rep[0]['C'] -gt 0) { throw 'Ya existen nadadores con esos DNI. Carga abortada.' }

    $tiposClase = @('Libre','Escuela','Competencia')
    $rep = Query ("SELECT COUNT(*) AS C FROM Clase WHERE TipoClase IN ('Libre','Escuela','Competencia')")
    if ([int]$rep[0]['C'] -gt 0) { throw 'Ya existen clases de demostracion. Carga abortada.' }

    Write-Host 'Insertando nadadores...'
    foreach ($n in $nadadores) {
        $cad = $n.DNI + $n.Nombre + $n.Apellido + ([datetime]$n.FechaNacimiento).ToString('yyyyMMdd') + $n.Edad + $n.Categoria + $n.CertificadoMedico.ToString()
        $dvh = CalDVH $cad
        NonQuery 'INSERT INTO Nadador (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH) VALUES (@dni, @nom, @ape, @nac, @edad, @cat, @cert, @dvh)' @(
            @('@dni', $n.DNI), @('@nom', $n.Nombre), @('@ape', $n.Apellido), @('@nac', [datetime]$n.FechaNacimiento), @('@edad', $n.Edad), @('@cat', $n.Categoria), @('@cert', $n.CertificadoMedico), @('@dvh', $dvh)
        )
    }

    # --- Clases ---
    $clases = @(
        [PSCustomObject]@{ Tipo='Libre'; Nivel='Basico'; Dias='Lunes,Miercoles'; Ini='17:00'; Fin='18:30'; Emin=6; Emax=10; Cupo=12 },
        [PSCustomObject]@{ Tipo='Escuela'; Nivel='Intermedio'; Dias='Martes,Jueves'; Ini='18:00'; Fin='19:30'; Emin=10; Emax=14; Cupo=12 },
        [PSCustomObject]@{ Tipo='Competencia'; Nivel='Avanzado'; Dias='Lunes,Miercoles'; Ini='19:00'; Fin='20:30'; Emin=14; Emax=18; Cupo=10 },
        [PSCustomObject]@{ Tipo='Libre'; Nivel='Basico'; Dias='Sabado'; Ini='09:00'; Fin='10:30'; Emin=8; Emax=16; Cupo=15 }
    )

    Write-Host 'Insertando clases...'
    $codigoClases = New-Object System.Collections.ArrayList
    foreach ($c in $clases) {
        $iniTs = [TimeSpan]::Parse($c.Ini)
        $finTs = [TimeSpan]::Parse($c.Fin)
        $cad = $c.Tipo + $c.Nivel + $c.Dias + $iniTs.ToString('hh\:mm') + $finTs.ToString('hh\:mm') + $c.Emin + $c.Emax + $c.Cupo + '' + ''
        $dvh = CalDVH $cad
        $codigo = [int](InsScalar 'INSERT INTO Clase (TipoClase, NivelDificultad, Dias, HorarioInicio, HorarioFin, EdadMinima, EdadMaxima, CupoMaximo, DVH) VALUES (@tipo, @nivel, @dias, @ini, @fin, @emin, @emax, @cupo, @dvh); SELECT CAST(SCOPE_IDENTITY() AS int);' @(
            @('@tipo', $c.Tipo), @('@nivel', $c.Nivel), @('@dias', $c.Dias), @('@ini', $iniTs), @('@fin', $finTs), @('@emin', $c.Emin), @('@emax', $c.Emax), @('@cupo', $c.Cupo), @('@dvh', $dvh)
        ))
        [void]$codigoClases.Add($codigo)
        $c | Add-Member -NotePropertyName CodigoClase -NotePropertyValue $codigo -Force
    }

    # --- Inscripciones a clase ---
    # mapeo manual por edad
    $mapa = @{
        'Maximo'   = @(0,3)
        'Jeremias' = @(0,1,3)
        'Pablo'    = @(1,3)
        'Benjamin' = @(1,2,3)
        'Nahuel'   = @(2,3)
        'Lautaro'  = @(2)
    }

    Write-Host 'Insertando inscripciones a clase...'
    $inscripcionesClase = New-Object System.Collections.ArrayList
    $hoy = [datetime]::Today
    foreach ($n in $nadadores) {
        foreach ($idx in $mapa[$n.Nombre]) {
            $clase = $clases[$idx]
            $codigoClase = $clase.CodigoClase
            $cad = $n.DNI + $codigoClase + $hoy.ToString('yyyyMMdd') + 'Activo'
            $dvh = CalDVH $cad
            $num = [int](InsScalar 'INSERT INTO InscripcionClase (DNINadador, CodigoClase, FechaInscripcion, Estado, DVH) VALUES (@dni, @cc, @fec, @est, @dvh); SELECT CAST(SCOPE_IDENTITY() AS int);' @(
                @('@dni', $n.DNI), @('@cc', $codigoClase), @('@fec', $hoy), @('@est', 'Activo'), @('@dvh', $dvh)
            ))
            [void]$inscripcionesClase.Add([PSCustomObject]@{ Numero=$num; DNI=$n.DNI; CodigoClase=$codigoClase; Nombre=$n.Nombre })
        }
    }

    # --- Asistencias ---
    # fechas pasadas que coincidan con dias de clase
    $fechasPorClase = @{
        0 = @('2026-10-05','2026-10-07')   # Lunes,Miercoles
        1 = @('2026-10-06','2026-10-08')   # Martes,Jueves
        2 = @('2026-10-05','2026-10-07')   # Lunes,Miercoles
        3 = @('2026-10-03','2026-10-10')   # Sabado
    }

    Write-Host 'Insertando asistencias...'
    $rng = New-Object System.Random
    for ($i = 0; $i -lt $clases.Count; $i++) {
        $clase = $clases[$i]
        $codigoClase = $clase.CodigoClase
        $inscDeClase = @($inscripcionesClase | Where-Object { $_.CodigoClase -eq $codigoClase })
        foreach ($fechaStr in $fechasPorClase[$i]) {
            $fecha = [datetime]$fechaStr
            foreach ($insc in $inscDeClase) {
                $condicion = if ($rng.Next(0,2) -eq 0) { 'Presente' } else { 'Ausente' }
                $cad = $insc.DNI + $codigoClase + $fecha.ToString('yyyyMMdd') + $condicion
                $dvh = CalDVH $cad
                NonQuery 'INSERT INTO Asistencia (CodigoClase, Fecha, DNINadador, Condicion, DVH) VALUES (@cc, @fec, @dni, @cond, @dvh)' @(
                    @('@cc', $codigoClase), @('@fec', $fecha), @('@dni', $insc.DNI), @('@cond', $condicion), @('@dvh', $dvh)
                )
            }
        }
    }

    # --- Recalcular DVV/DVH control para Nadador, Clase, InscripcionClase ---
    Write-Host 'Recalculando DVV...'
    function RecalcularControl($nombreTabla, $rows, $chain) {
        $acc = [System.Numerics.BigInteger]0
        foreach ($r in $rows) { $acc += (CalDVH (& $chain $r)) }
        $dvv = Wrap64 $acc
        $ctl = CalDVH ($nombreTabla + $dvv.ToString())
        NonQuery 'UPDATE DigitoVerificador SET DVV = @dvv, DVH = @ctl WHERE NombreTabla = @nt' @(
            @('@nt', $nombreTabla), @('@dvv', $dvv), @('@ctl', $ctl)
        )
    }

    $chainNad = { param($r) $r['DNI'].ToString() + $r['Nombre'].ToString() + $r['Apellido'].ToString() + ([datetime]$r['FechaNacimiento']).ToString('yyyyMMdd') + $r['Edad'].ToString() + $r['Categoria'].ToString() + $r['CertificadoMedico'].ToString() }
    RecalcularControl 'Nadador' (Query 'SELECT DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico FROM Nadador') $chainNad

    $chainClase = { param($r) $r['TipoClase'].ToString() + $r['NivelDificultad'].ToString() + $r['Dias'].ToString() + ([TimeSpan]$r['HorarioInicio']).ToString('hh\:mm') + ([TimeSpan]$r['HorarioFin']).ToString('hh\:mm') + $r['EdadMinima'].ToString() + $r['EdadMaxima'].ToString() + $r['CupoMaximo'].ToString() + '' + '' }
    RecalcularControl 'Clase' (Query 'SELECT TipoClase, NivelDificultad, Dias, HorarioInicio, HorarioFin, EdadMinima, EdadMaxima, CupoMaximo FROM Clase') $chainClase

    $chainInsClase = { param($r) $r['DNINadador'].ToString() + $r['CodigoClase'].ToString() + ([datetime]$r['FechaInscripcion']).ToString('yyyyMMdd') + $r['Estado'].ToString() }
    RecalcularControl 'InscripcionClase' (Query 'SELECT DNINadador, CodigoClase, FechaInscripcion, Estado FROM InscripcionClase') $chainInsClase

    $script:tx.Commit()
    Write-Host 'OK. Transaccion confirmada.'
}
catch {
    Write-Host ("ERROR: " + $_.Exception.Message)
    $script:tx.Rollback()
    throw
}
finally {
    $conn.Close()
}