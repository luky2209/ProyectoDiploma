using BE;
using BE.Enum;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLAsistencia13M
    {
        DALAsistencia13M _dal = new DALAsistencia13M();
        BLLClase13M _bllClase = new BLLClase13M();
        BLLInscripcionClase13M _bllInscripcionClase = new BLLInscripcionClase13M();
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<Asistencia13M> obtenerPorClaseYFecha(int codigoClase, DateTime fecha)
        {
            List<Asistencia13M> lista = new List<Asistencia13M>();

            DataTable dt = _dal.obtenerPorClaseYFecha(codigoClase, fecha);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearAsistencia(row));
            }

            return lista;
        }

        // este metodo registra o actualiza la asistencia de un nadador en una clase y fecha
        public void RegistrarAsistencia(int codigoClase, DateTime fecha, string dni, string condicion)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            Clase13M clase = _bllClase.obtenerPorCodigo(codigoClase);

            if (clase == null)
            {
                throw new Exception(idioma.Translate("RegistrarAsistencia19M.msgClaseNoEncontrada"));
            }

            // valida que la fecha no sea futura
            if (fecha.Date > DateTime.Today)
            {
                throw new Exception(idioma.Translate("RegistrarAsistencia19M.msgFechaFutura"));
            }

            // valida que el dia de la fecha coincida con un dia de la clase
            if (!EsDiaDeClase(clase.Dias, fecha))
            {
                throw new Exception(idioma.Translate("RegistrarAsistencia19M.msgDiaNoValido"));
            }

            // valida que el nadador este inscripto en la clase
            if (!_bllInscripcionClase.obtenerPorClase(codigoClase).Any(i => i.DNINadador == dni))
            {
                throw new Exception(idioma.Translate("RegistrarAsistencia19M.msgNadadorNoInscripto"));
            }

            long dvh = Services.DigitoVerificador13M.CalcularDVH(
                dni + codigoClase + fecha.ToString("yyyyMMdd") + condicion);

            if (_dal.ExisteAsistencia(dni, codigoClase, fecha))
            {
                _dal.ActualizarCondicion(codigoClase, fecha, dni, condicion, dvh);
            }
            else
            {
                _dal.InsertarAsistencia(codigoClase, fecha, dni, condicion, dvh);
            }

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se registro asistencia {condicion} del nadador DNI {dni} en la clase Nro. {codigoClase} para la fecha {fecha:dd/MM/yyyy}.", Criticidad13M.Medio, Modulos13M.Clase);
        }

        // este metodo pasa una fila de la base a un objeto de asistencia
        private Asistencia13M MapearAsistencia(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            return new Asistencia13M
            {
                NumeroAsistencia = Convert.ToInt32(row["NumeroAsistencia"]),
                CodigoClase = Convert.ToInt32(row["CodigoClase"]),
                Fecha = Convert.ToDateTime(row["Fecha"]),
                DNINadador = row["DNINadador"].ToString(),
                Condicion = row["Condicion"].ToString(),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]),
                Nombre = row.Table.Columns.Contains("Nombre") && row["Nombre"] != DBNull.Value ? row["Nombre"].ToString() : "",
                Apellido = row.Table.Columns.Contains("Apellido") && row["Apellido"] != DBNull.Value ? row["Apellido"].ToString() : "",
                TipoClase = row.Table.Columns.Contains("TipoClase") && row["TipoClase"] != DBNull.Value ? row["TipoClase"].ToString() : ""
            };
        }

        // este metodo verifica que el dia de la fecha este entre los dias de la clase
        private bool EsDiaDeClase(string dias, DateTime fecha)
        {
            if (string.IsNullOrEmpty(dias))
            {
                return false;
            }

            string nombreDia = ObtenerNombreDia(fecha.DayOfWeek);

            foreach (string parte in dias.Split(','))
            {
                if (parte.Trim() == nombreDia)
                {
                    return true;
                }
            }

            return false;
        }

        // este metodo traduce el DayOfWeek a los nombres que usa el sistema
        private string ObtenerNombreDia(DayOfWeek dia)
        {
            switch (dia)
            {
                case DayOfWeek.Monday: return "Lunes";
                case DayOfWeek.Tuesday: return "Martes";
                case DayOfWeek.Wednesday: return "Miercoles";
                case DayOfWeek.Thursday: return "Jueves";
                case DayOfWeek.Friday: return "Viernes";
                case DayOfWeek.Saturday: return "Sabado";
                case DayOfWeek.Sunday: return "Domingo";
                default: return "";
            }
        }
    }
}