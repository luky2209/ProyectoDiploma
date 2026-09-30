# Carga de datos de demostracion para CU04 - Cobrar Venta.
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

function D([string]$s) {
    return [decimal]::Parse($s, [System.Globalization.CultureInfo]::InvariantCulture)
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
    # --- Datos de nadadores (10, 21 anios, categoria Senior, 7 con certificado medico) ---
    $nombres   = @('Jeremias','Milagros','Melina','Yapura','Maximo','Nahuel','Lautaro','Julian','Pablito','Bautista')
    $apellidos = @('Gomez','Calella','Rodriguez','Fernandez','Sosa','Alvarez','Benitez','Cabrera','Herrera','Pereyra')
    $dnisBase  = @('35123456','47307577','35123457','35123458','35123459','35123460','35123461','35123462','35123463','35123464')
    $fechasNac = @('2005-01-08','2005-02-14','2005-03-21','2005-04-02','2005-05-11','2005-06-23','2005-01-15','2005-02-20','2005-03-05','2005-04-18')

    $nadadores = New-Object System.Collections.ArrayList
    for ($i = 0; $i -lt 10; $i++) {
        $dni = $dnisBase[$i] + (GetDniDv $dnisBase[$i]).ToString()
        $cert = $i -lt 7
        [void]$nadadores.Add([PSCustomObject]@{
            DNI = $dni; Nombre = $nombres[$i]; Apellido = $apellidos[$i]
            FechaNacimiento = $fechasNac[$i]; Edad = 21; Categoria = 'Senior'; CertificadoMedico = $cert
        })
    }

    # --- Datos de torneos (4 Finalizado + 1 Abierto) ---
    $tNombres = @('Torneo Club Atletico Sur','Torneo Escuela del Norte','Torneo Open Serie Primavera','Torneo Municipal Villa Urquiza','Torneo Abierto de Octubre')
    $tFechas  = @('2026-03-14','2026-04-11','2026-05-09','2026-06-13','2026-10-10')
    $tSedes   = @('Glew','San Isidro','Mar del Plata','CABA','Lomas de Zamora')
    $tArancel = @('18000.00','16500.00','24000.00','21000.00','19500.00')
    $tCierre  = @('2026-03-15','2026-04-12','2026-05-10','2026-06-14',$null)
    $tEstado  = @('Finalizado','Finalizado','Finalizado','Finalizado','Abierto')
    $tCategorias = 'Infantil,Menor,Cadete,Juvenil,Junior,Senior'

    # --- Precondiciones de idempotencia ---
    $dnisBuscados = ($nadadores | ForEach-Object { "'" + $_.DNI + "'" }) -join ','
    $nombresBuscados = ($tNombres | ForEach-Object { "'" + $_ + "'" }) -join ','
    $rep = Query ("SELECT COUNT(*) AS C FROM Nadador WHERE DNI IN (" + $dnisBuscados + ")")
    if ([int]$rep[0]['C'] -gt 0) { throw 'Ya existen nadadores con esos DNI. Carga abortada.' }
    $rep = Query ("SELECT COUNT(*) AS C FROM Torneo WHERE Nombre IN (" + $nombresBuscados + ")")
    if ([int]$rep[0]['C'] -gt 0) { throw 'Ya existen torneos con esos nombres. Carga abortada.' }

    # --- Distribucion de nadadores en las 4 pruebas por torneo (3-3-2-2, rotando) ---
    $dist = @(
        @(1, 1, 1, 6, 6, 6, 11, 11, 16, 16),
        @(11, 16, 16, 1, 1, 1, 6, 6, 6, 11),
        @(6, 6, 11, 11, 16, 16, 1, 1, 1, 6),
        @(1, 1, 6, 6, 6, 11, 11, 16, 16, 1),
        @(1, 1, 1, 6, 6, 6, 11, 11, 16, 16)
    )
    $fechasInsc = @('2026-02-25','2026-03-20','2026-04-18','2026-05-20','2026-09-20')
    $insEstado  = @('Pagado','Pagado','Pagado','Pendiente de Pago','Pendiente de Pago')

    Write-Host 'Insertando nadadores...'
    foreach ($n in $nadadores) {
        $cad = $n.DNI + $n.Nombre + $n.Apellido + ([datetime]$n.FechaNacimiento).ToString('yyyyMMdd') + $n.Edad + $n.Categoria + $n.CertificadoMedico.ToString()
        $dvh = CalDVH $cad
        NonQuery 'INSERT INTO Nadador (DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH) VALUES (@dni, @nom, @ape, @nac, @edad, @cat, @cert, @dvh)' @(
            @('@dni', $n.DNI), @('@nom', $n.Nombre), @('@ape', $n.Apellido), @('@nac', [datetime]$n.FechaNacimiento), @('@edad', $n.Edad), @('@cat', $n.Categoria), @('@cert', $n.CertificadoMedico), @('@dvh', $dvh)
        )
    }

    Write-Host 'Insertando torneos y TorneoPrueba...'
    $codigoTorneos = New-Object System.Collections.ArrayList
    for ($t = 0; $t -lt 5; $t++) {
        $arancel = D $tArancel[$t]
        $fecha = [datetime]$tFechas[$t]
        $cad = $tNombres[$t] + $fecha.ToString('yyyyMMdd') + $tSedes[$t] + $arancel.ToString() + $tCategorias
        $dvh = CalDVH $cad
        $paramCierre = if ($null -eq $tCierre[$t]) { [DBNull]::Value } else { [datetime]$tCierre[$t] }
        $codigoTorneo = [int](InsScalar 'INSERT INTO Torneo (Nombre, Fecha, Sede, Arancel, Categorias, Estado, FechaCierre, DVH) VALUES (@nom, @fec, @sed, @ara, @cat, @est, @cie, @dvh); SELECT CAST(SCOPE_IDENTITY() AS int);' @(
            @('@nom', $tNombres[$t]), @('@fec', $fecha), @('@sed', $tSedes[$t]), @('@ara', $arancel), @('@cat', $tCategorias), @('@est', $tEstado[$t]), @('@cie', $paramCierre), @('@dvh', $dvh)
        ))
        [void]$codigoTorneos.Add($codigoTorneo)
        foreach ($idPrueba in ($dist[$t] | Select-Object -Unique)) {
            $dvhP = CalDVH ($codigoTorneo.ToString() + $idPrueba.ToString())
            NonQuery 'INSERT INTO TorneoPrueba (CodigoTorneo, IdPrueba, DVH) VALUES (@ct, @ip, @dvh)' @(
                @('@ct', $codigoTorneo), @('@ip', [int]$idPrueba), @('@dvh', $dvhP)
            )
        }
    }

    Write-Host 'Insertando inscripciones (10 por torneo)...'
    $inscripciones = New-Object System.Collections.ArrayList
    for ($t = 0; $t -lt 5; $t++) {
        $codigoTorneo = $codigoTorneos[$t]
        for ($ni = 0; $ni -lt 10; $ni++) {
            $n = $nadadores[$ni]
            $idPrueba = [int]$dist[$t][$ni]
            $fechaIn = [datetime]$fechasInsc[$t]
            $estado  = $insEstado[$t]
            $cad = $n.DNI + $codigoTorneo + $idPrueba + $n.Categoria + $fechaIn.ToString('yyyyMMdd') + $estado
            $dvh = CalDVH $cad
            $numInsc = [int](InsScalar 'INSERT INTO Inscripcion (DNINadador, CodigoTorneo, IdPrueba, Categoria, FechaInscripcion, Estado, DVH) VALUES (@dni, @ct, @ip, @cat, @fec, @est, @dvh); SELECT CAST(SCOPE_IDENTITY() AS int);' @(
                @('@dni', $n.DNI), @('@ct', $codigoTorneo), @('@ip', $idPrueba), @('@cat', $n.Categoria), @('@fec', $fechaIn), @('@est', $estado), @('@dvh', $dvh)
            ))
            [void]$inscripciones.Add([PSCustomObject]@{
                NumeroInscripcion = $numInsc; CodigoTorneo = $codigoTorneo; IdPrueba = $idPrueba; DNINadador = $n.DNI; TorneoIndex = $t; NadadorIndex = $ni
            })
        }
    }

    Write-Host 'Calculando resultados (40) en los torneos finalizados...'
    $baseHund = @{ 1 = 2350; 6 = 2680; 11 = 2790; 16 = 3250 }

    for ($t = 0; $t -lt 4; $t++) {
        $codigoTorneo = $codigoTorneos[$t]
        $insT = @($inscripciones | Where-Object { $_.CodigoTorneo -eq $codigoTorneo })
        foreach ($idPrueba in ($insT | Select-Object -ExpandProperty IdPrueba -Unique)) {
            $grupo = @($insT | Where-Object { $_.IdPrueba -eq $idPrueba } | Sort-Object NadadorIndex)
            $posicion = 1
            foreach ($ins in $grupo) {
                $descalificadoBool = $false
                if ($t -eq 2 -and $ins.NadadorIndex -eq 9) { $descalificadoBool = $true }
                if ($t -eq 3 -and $ins.NadadorIndex -eq 5) { $descalificadoBool = $true }
                if ($descalificadoBool) {
                    $min = 0; $seg = 0; $cen = 0
                    $cad = $ins.NumeroInscripcion.ToString() + $min.ToString() + $seg.ToString() + $cen.ToString() + 'True' + '0' + ''
                    $dvh = CalDVH $cad
                    $sql = 'INSERT INTO Resultado (NumeroInscripcion, DNINadador, IdPrueba, Minutos, Segundos, Centesimas, Descalificado, Posicion, Premio, DVH) VALUES (@ni, @dni, @ip, @min, @seg, @cen, @desc, NULL, NULL, @dvh)'
                    NonQuery $sql @(
                        @('@ni', $ins.NumeroInscripcion), @('@dni', $ins.DNINadador), @('@ip', $ins.IdPrueba), @('@min', $min), @('@seg', $seg), @('@cen', $cen), @('@desc', $true), @('@dvh', $dvh)
                    )
                } else {
                    $hund = $baseHund[[int]$idPrueba] + ($grupo.IndexOf($ins)) * 55
                    $min = [int][math]::Floor($hund / 6000)
                    $rest = $hund % 6000
                    $seg = [int][math]::Floor($rest / 100)
                    $cen = [int]($rest % 100)
                    $precio = if ($posicion -eq 1) { 'Medalla de Oro' } elseif ($posicion -eq 2) { 'Medalla de Plata' } elseif ($posicion -eq 3) { 'Medalla de Bronce' } else { $null }
                    $cad = $ins.NumeroInscripcion.ToString() + $min.ToString() + $seg.ToString() + $cen.ToString() + 'False' + $posicion.ToString() + $precio
                    $dvh = CalDVH $cad
                    $paramPremio = if ($null -eq $precio) { [DBNull]::Value } else { $precio }
                    $sql = 'INSERT INTO Resultado (NumeroInscripcion, DNINadador, IdPrueba, Minutos, Segundos, Centesimas, Descalificado, Posicion, Premio, DVH) VALUES (@ni, @dni, @ip, @min, @seg, @cen, @desc, @pos, @pre, @dvh)'
                    NonQuery $sql @(
                        @('@ni', $ins.NumeroInscripcion), @('@dni', $ins.DNINadador), @('@ip', $ins.IdPrueba), @('@min', $min), @('@seg', $seg), @('@cen', $cen), @('@desc', $false), @('@pos', $posicion), @('@pre', $paramPremio), @('@dvh', $dvh)
                    )
                    $posicion++
                }
            }
        }
    }

    Write-Host 'Actualizando DVH/DVV/control de DigitoVerificador...'
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

    $chainTor = { param($r) $r['Nombre'].ToString() + ([datetime]$r['Fecha']).ToString('yyyyMMdd') + $r['Sede'].ToString() + $r['Arancel'].ToString() + $r['Categorias'].ToString() }
    RecalcularControl 'Torneo' (Query 'SELECT Nombre, Fecha, Sede, Arancel, Categorias FROM Torneo') $chainTor

    $chainTP = { param($r) $r['CodigoTorneo'].ToString() + $r['IdPrueba'].ToString() }
    RecalcularControl 'TorneoPrueba' (Query 'SELECT CodigoTorneo, IdPrueba FROM TorneoPrueba') $chainTP

    $chainIns = { param($r) $r['DNINadador'].ToString() + $r['CodigoTorneo'].ToString() + $r['IdPrueba'].ToString() + $r['Categoria'].ToString() + ([datetime]$r['FechaInscripcion']).ToString('yyyyMMdd') + $r['Estado'].ToString() }
    RecalcularControl 'Inscripcion' (Query 'SELECT DNINadador, CodigoTorneo, IdPrueba, Categoria, FechaInscripcion, Estado FROM Inscripcion') $chainIns

    $chainRes = { param($r) $pr = if ($r['Premio'] -is [DBNull]) { '' } else { $r['Premio'].ToString() }; $ps = if ($r['Posicion'] -is [DBNull]) { '0' } else { $r['Posicion'].ToString() }; $r['NumeroInscripcion'].ToString() + $r['Minutos'].ToString() + $r['Segundos'].ToString() + $r['Centesimas'].ToString() + $r['Descalificado'].ToString() + $ps + $pr }
    RecalcularControl 'Resultado' (Query 'SELECT NumeroInscripcion, Minutos, Segundos, Centesimas, Descalificado, Posicion, Premio FROM Resultado') $chainRes

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