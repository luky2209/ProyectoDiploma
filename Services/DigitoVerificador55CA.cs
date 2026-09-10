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
    public static class DigitoVerificador55CA
    {
        static DALDigitoVerificador55CA dalControl = new DALDigitoVerificador55CA();
        static DALBackUpRestore55CA dalBackup = new DALBackUpRestore55CA();
        static DALBitacora55CA dalBitacora = new DALBitacora55CA();

        static DALUsuario55CA dalUsuario = new DALUsuario55CA();
        static DALRol dalRol = new DALRol();
        static DALFamilia dalFamilia = new DALFamilia();
        static DALPatente dalPatente = new DALPatente();

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

        public static void RepararUsuario() { RepararTodoUsuario(); GuardarOActualizarDVV("Usuario", ObtenerSumaDVHUsuario()); Registrar("Usuario"); }
        public static void RepararRol() { RepararTodoRol(); GuardarOActualizarDVV("Rol", ObtenerSumaDVHRol()); Registrar("Rol"); }
        public static void RepararFamilia() { RepararTodoFamilia(); GuardarOActualizarDVV("Familia", ObtenerSumaDVHFamilia()); Registrar("Familia"); }
        public static void RepararPatente() { RepararTodoPatente(); GuardarOActualizarDVV("Patente", ObtenerSumaDVHPatente()); Registrar("Patente"); }

        public static void RealizarRestore(string ruta) => dalBackup.realizarRestore(ruta);

        private static void RegistrarDeteccion(string tabla)
        {
            string dni = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo?.DNI ?? "SISTEMA";
            dalBitacora.insertarLog(dni, $"Se detectó una inconsistencia en la tabla {tabla}.", (int)Criticidad55CA.Alto, (int)Modulos55CA.Seguridad, DateTime.Now);
        }

        private static void Registrar(string tabla)
        {
            string dni = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo?.DNI ?? "SISTEMA";
            dalBitacora.insertarLog(dni, $"Se reparó la tabla {tabla}.", (int)Criticidad55CA.Alto, (int)Modulos55CA.Seguridad, DateTime.Now);
        }

        public static void ActualizarDVVUsuario() => GuardarOActualizarDVV("Usuario", ObtenerSumaDVHUsuario());
        public static void ActualizarDVVRol() => GuardarOActualizarDVV("Rol", ObtenerSumaDVHRol());
        public static void ActualizarDVVFamilia() => GuardarOActualizarDVV("Familia", ObtenerSumaDVHFamilia());
        public static void ActualizarDVVPatente() => GuardarOActualizarDVV("Patente", ObtenerSumaDVHPatente());
    }
}
