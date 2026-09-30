using BE.Enum;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DigitoVerificador13M
    {
        static DALDigitoVerificador13M dalControl = new DALDigitoVerificador13M();
        static DALBackUpRestore13M dalBackup = new DALBackUpRestore13M();
        static DALBitacora13M dalBitacora = new DALBitacora13M();

        static DALUsuario13M dalUsuario = new DALUsuario13M();
        static DALRol dalRol = new DALRol();
        static DALFamilia dalFamilia = new DALFamilia();
        static DALPatente dalPatente = new DALPatente();
        static DALNadador13M dalNadador = new DALNadador13M();
        static DALPrueba13M dalPrueba = new DALPrueba13M();
        static DALTorneo13M dalTorneo = new DALTorneo13M();
        static DALInscripcion13M dalInscripcion = new DALInscripcion13M();
        static DALResultado13M dalResultado = new DALResultado13M();

        public static long CalcularDVH(string cadena)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(cadena));

                long resultado = BitConverter.ToInt64(hashBytes, 0);

                return Math.Abs(resultado);
            }
        }

        public static long CalcularDVV(IEnumerable<long> dvhs)
        {
            long suma = 0;
            foreach (long dvh in dvhs)
            {
                suma += dvh;
            }
            return suma;
        }

        private static long CalcularDVHControl(string nombreTabla, long dvv)
        {
            return CalcularDVH(nombreTabla + dvv);
        }

        private static void GuardarOActualizarDVV(string nombreTabla, long dvv)
        {
            long dvh = CalcularDVHControl(nombreTabla, dvv);
            if (dalControl.ExisteTabla(nombreTabla))
                dalControl.ActualizarDVV(nombreTabla, dvv, dvh);
            else
                dalControl.GuardarDVV(nombreTabla, dvv, dvh);
        }

        private static long CalcularDVHUsuario(DataRow row)
        {
            string cadena = row["DNI"].ToString() + row["Nombre"].ToString() + row["Apellido"].ToString()
                + row["Email"].ToString() + row["IdRol"].ToString() + row["Username"].ToString() + row["PasswordHash"].ToString();
            return CalcularDVH(cadena);
        }

        private static long ObtenerSumaDVHUsuario()
        {
            long suma = 0;
            foreach (DataRow row in dalUsuario.obtenerTodos().Rows) suma += CalcularDVHUsuario(row);
            return suma;
        }

        private static void RepararTodoUsuario()
        {
            foreach (DataRow row in dalUsuario.obtenerTodos().Rows)
            {
                long guardado = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long calculado = CalcularDVHUsuario(row);
                if (guardado != calculado) dalUsuario.ActualizarDVH(row["DNI"].ToString(), calculado);
            }
        }

        private static long CalcularDVHFilaGenerica(DataRow row)
        {
            return CalcularDVH(row["Id"].ToString() + row["Nombre"].ToString());
        }

        private static long ObtenerSumaDVHRol() { long s = 0; foreach (DataRow r in dalRol.obtenerTodos().Rows) s += CalcularDVHFilaGenerica(r); return s; }
        private static void RepararTodoRol()
        {
            foreach (DataRow row in dalRol.obtenerTodos().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHFilaGenerica(row);
                if (g != c) dalRol.ActualizarDVH(Convert.ToInt32(row["Id"]), c);
            }
        }

        private static long ObtenerSumaDVHFamilia() { long s = 0; foreach (DataRow r in dalFamilia.obtenerTodos().Rows) s += CalcularDVHFilaGenerica(r); return s; }
        private static void RepararTodoFamilia()
        {
            foreach (DataRow row in dalFamilia.obtenerTodos().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHFilaGenerica(row);
                if (g != c) dalFamilia.ActualizarDVH(Convert.ToInt32(row["Id"]), c);
            }
        }

        private static long ObtenerSumaDVHPatente() { long s = 0; foreach (DataRow r in dalPatente.obtenerTodos().Rows) s += CalcularDVHFilaGenerica(r); return s; }
        private static void RepararTodoPatente()
        {
            foreach (DataRow row in dalPatente.obtenerTodos().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHFilaGenerica(row);
                if (g != c) dalPatente.ActualizarDVH(Convert.ToInt32(row["Id"]), c);
            }
        }

        private static long CalcularDVHNadador(DataRow row)
        {
            string cadena = row["DNI"].ToString() + row["Nombre"].ToString() + row["Apellido"].ToString()
                + Convert.ToDateTime(row["FechaNacimiento"]).ToString("yyyyMMdd") + row["Edad"].ToString()
                + row["Categoria"].ToString() + row["CertificadoMedico"].ToString();
            return CalcularDVH(cadena);
        }

        private static long ObtenerSumaDVHNadador() { long s = 0; foreach (DataRow r in dalNadador.obtenerTodos().Rows) s += CalcularDVHNadador(r); return s; }
        private static void RepararTodoNadador()
        {
            foreach (DataRow row in dalNadador.obtenerTodos().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHNadador(row);
                if (g != c) dalNadador.ActualizarDVH(row["DNI"].ToString(), c);
            }
        }

        private static long CalcularDVHPrueba(DataRow row)
        {
            return CalcularDVH(row["IdPrueba"].ToString() + row["Estilo"].ToString() + row["Distancia"].ToString());
        }

        private static long ObtenerSumaDVHPrueba() { long s = 0; foreach (DataRow r in dalPrueba.obtenerTodas().Rows) s += CalcularDVHPrueba(r); return s; }
        private static void RepararTodoPrueba()
        {
            foreach (DataRow row in dalPrueba.obtenerTodas().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHPrueba(row);
                if (g != c) dalPrueba.ActualizarDVH(Convert.ToInt32(row["IdPrueba"]), c);
            }
        }

        private static long CalcularDVHTorneo(DataRow row)
        {
            string cadena = row["Nombre"].ToString() + Convert.ToDateTime(row["Fecha"]).ToString("yyyyMMdd")
                + row["Sede"].ToString() + row["Arancel"].ToString() + row["Categorias"].ToString();
            return CalcularDVH(cadena);
        }

        private static long ObtenerSumaDVHTorneo() { long s = 0; foreach (DataRow r in dalTorneo.obtenerTodos().Rows) s += CalcularDVHTorneo(r); return s; }
        private static void RepararTodoTorneo()
        {
            foreach (DataRow row in dalTorneo.obtenerTodos().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHTorneo(row);
                if (g != c) dalTorneo.ActualizarDVH(Convert.ToInt32(row["CodigoTorneo"]), c);
            }
        }

        private static long CalcularDVHTorneoPrueba(DataRow row)
        {
            return CalcularDVH(row["CodigoTorneo"].ToString() + row["IdPrueba"].ToString());
        }

        private static long ObtenerSumaDVHTorneoPrueba() { long s = 0; foreach (DataRow r in dalTorneo.obtenerTodasTorneoPrueba().Rows) s += CalcularDVHTorneoPrueba(r); return s; }
        private static void RepararTodoTorneoPrueba()
        {
            foreach (DataRow row in dalTorneo.obtenerTodasTorneoPrueba().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHTorneoPrueba(row);
                if (g != c) dalTorneo.ActualizarDVHTorneoPrueba(Convert.ToInt32(row["CodigoTorneo"]), Convert.ToInt32(row["IdPrueba"]), c);
            }
        }

        private static long CalcularDVHInscripcion(DataRow row)
        {
            string cadena = row["DNINadador"].ToString() + row["CodigoTorneo"].ToString() + row["IdPrueba"].ToString()
                + row["Categoria"].ToString() + Convert.ToDateTime(row["FechaInscripcion"]).ToString("yyyyMMdd") + row["Estado"].ToString();
            return CalcularDVH(cadena);
        }

        private static long ObtenerSumaDVHInscripcion() { long s = 0; foreach (DataRow r in dalInscripcion.obtenerTodas().Rows) s += CalcularDVHInscripcion(r); return s; }
        private static void RepararTodoInscripcion()
        {
            foreach (DataRow row in dalInscripcion.obtenerTodas().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHInscripcion(row);
                if (g != c) dalInscripcion.ActualizarDVH(Convert.ToInt32(row["NumeroInscripcion"]), c);
            }
        }

        private static long CalcularDVHResultado(DataRow row)
        {
            // un resultado descalificado tiene la posicion en 0, por eso se usa ese valor
            string posicion = row["Posicion"] == DBNull.Value ? "0" : Convert.ToInt32(row["Posicion"]).ToString();

            string cadena = row["NumeroInscripcion"].ToString() + row["Minutos"].ToString()
                + row["Segundos"].ToString() + row["Centesimas"].ToString()
                + row["Descalificado"].ToString() + posicion + row["Premio"].ToString();
            return CalcularDVH(cadena);
        }

        private static long ObtenerSumaDVHResultado() { long s = 0; foreach (DataRow r in dalResultado.obtenerTodas().Rows) s += CalcularDVHResultado(r); return s; }
        private static void RepararTodoResultado()
        {
            foreach (DataRow row in dalResultado.obtenerTodas().Rows)
            {
                long g = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]);
                long c = CalcularDVHResultado(row);
                if (g != c) dalResultado.ActualizarDVH(Convert.ToInt32(row["IdResultado"]), c);
            }
        }

        private static bool VerificarTabla(string nombreTabla, Func<long> obtenerSuma, Action reparar)
        {
            DataRow filaControl = dalControl.ObtenerFila(nombreTabla);
            if (filaControl == null)
            {
                reparar();
                GuardarOActualizarDVV(nombreTabla, obtenerSuma());
                return true;
            }

            long dvvGuardado = Convert.ToInt64(filaControl["DVV"]);
            long dvhGuardado = Convert.ToInt64(filaControl["DVH"]);

            if (dvhGuardado != CalcularDVHControl(nombreTabla, dvvGuardado))
            {
                RegistrarDeteccion(nombreTabla);
                return false;
            }

            bool consistente = dvvGuardado == obtenerSuma();
            if (!consistente) RegistrarDeteccion(nombreTabla);
            return consistente;
        }

        public static bool VerificarUsuario() => VerificarTabla("Usuario", ObtenerSumaDVHUsuario, RepararTodoUsuario);
        public static bool VerificarRol() => VerificarTabla("Rol", ObtenerSumaDVHRol, RepararTodoRol);
        public static bool VerificarFamilia() => VerificarTabla("Familia", ObtenerSumaDVHFamilia, RepararTodoFamilia);
        public static bool VerificarPatente() => VerificarTabla("Patente", ObtenerSumaDVHPatente, RepararTodoPatente);
        public static bool VerificarNadador() => VerificarTabla("Nadador", ObtenerSumaDVHNadador, RepararTodoNadador);
        public static bool VerificarPrueba() => VerificarTabla("Prueba", ObtenerSumaDVHPrueba, RepararTodoPrueba);
        public static bool VerificarTorneo() => VerificarTabla("Torneo", ObtenerSumaDVHTorneo, RepararTodoTorneo);
        public static bool VerificarTorneoPrueba() => VerificarTabla("TorneoPrueba", ObtenerSumaDVHTorneoPrueba, RepararTodoTorneoPrueba);
        public static bool VerificarInscripcion() => VerificarTabla("Inscripcion", ObtenerSumaDVHInscripcion, RepararTodoInscripcion);
        public static bool VerificarResultado() => VerificarTabla("Resultado", ObtenerSumaDVHResultado, RepararTodoResultado);

        public static void RepararUsuario() { RepararTodoUsuario(); GuardarOActualizarDVV("Usuario", ObtenerSumaDVHUsuario()); Registrar("Usuario"); }
        public static void RepararRol() { RepararTodoRol(); GuardarOActualizarDVV("Rol", ObtenerSumaDVHRol()); Registrar("Rol"); }
        public static void RepararFamilia() { RepararTodoFamilia(); GuardarOActualizarDVV("Familia", ObtenerSumaDVHFamilia()); Registrar("Familia"); }
        public static void RepararPatente() { RepararTodoPatente(); GuardarOActualizarDVV("Patente", ObtenerSumaDVHPatente()); Registrar("Patente"); }
        public static void RepararNadador() { RepararTodoNadador(); GuardarOActualizarDVV("Nadador", ObtenerSumaDVHNadador()); Registrar("Nadador"); }
        public static void RepararPrueba() { RepararTodoPrueba(); GuardarOActualizarDVV("Prueba", ObtenerSumaDVHPrueba()); Registrar("Prueba"); }
        public static void RepararTorneo() { RepararTodoTorneo(); GuardarOActualizarDVV("Torneo", ObtenerSumaDVHTorneo()); Registrar("Torneo"); }
        public static void RepararTorneoPrueba() { RepararTodoTorneoPrueba(); GuardarOActualizarDVV("TorneoPrueba", ObtenerSumaDVHTorneoPrueba()); Registrar("TorneoPrueba"); }
        public static void RepararInscripcion() { RepararTodoInscripcion(); GuardarOActualizarDVV("Inscripcion", ObtenerSumaDVHInscripcion()); Registrar("Inscripcion"); }
        public static void RepararResultado() { RepararTodoResultado(); GuardarOActualizarDVV("Resultado", ObtenerSumaDVHResultado()); Registrar("Resultado"); }

        public static void RealizarRestore(string ruta) => dalBackup.realizarRestore(ruta);

        private static void RegistrarDeteccion(string tabla)
        {
            string dni = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo?.DNI ?? "SISTEMA";
            dalBitacora.insertarLog(dni, $"Se detecto una inconsistencia en la tabla {tabla}.", (int)Criticidad13M.Alto, (int)Modulos13M.Seguridad, DateTime.Now);
        }

        private static void Registrar(string tabla)
        {
            string dni = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo?.DNI ?? "SISTEMA";
            dalBitacora.insertarLog(dni, $"Se reparo la tabla {tabla}.", (int)Criticidad13M.Alto, (int)Modulos13M.Seguridad, DateTime.Now);
        }

        public static void ActualizarDVVUsuario() => GuardarOActualizarDVV("Usuario", ObtenerSumaDVHUsuario());
        public static void ActualizarDVVRol() => GuardarOActualizarDVV("Rol", ObtenerSumaDVHRol());
        public static void ActualizarDVVFamilia() => GuardarOActualizarDVV("Familia", ObtenerSumaDVHFamilia());
        public static void ActualizarDVVPatente() => GuardarOActualizarDVV("Patente", ObtenerSumaDVHPatente());
        public static void ActualizarDVVNadador() => GuardarOActualizarDVV("Nadador", ObtenerSumaDVHNadador());
        public static void ActualizarDVVPrueba() => GuardarOActualizarDVV("Prueba", ObtenerSumaDVHPrueba());
        public static void ActualizarDVVTorneo() => GuardarOActualizarDVV("Torneo", ObtenerSumaDVHTorneo());
        public static void ActualizarDVVTorneoPrueba() => GuardarOActualizarDVV("TorneoPrueba", ObtenerSumaDVHTorneoPrueba());
        public static void ActualizarDVVInscripcion() => GuardarOActualizarDVV("Inscripcion", ObtenerSumaDVHInscripcion());
        public static void ActualizarDVVResultado() => GuardarOActualizarDVV("Resultado", ObtenerSumaDVHResultado());
    }
}
