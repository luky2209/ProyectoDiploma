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
    public class BLLInscripcionClase13M
    {
        DALInscripcionClase13M _dal = new DALInscripcionClase13M();
        BLLNadador13M _bllNadador = new BLLNadador13M();
        BLLClase13M _bllClase = new BLLClase13M();
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<InscripcionClase13M> obtenerTodas()
        {
            List<InscripcionClase13M> lista = new List<InscripcionClase13M>();

            DataTable dt = _dal.obtenerTodas();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearInscripcionClase(row));
            }

            return lista;
        }

        public List<InscripcionClase13M> obtenerPorClase(int codigoClase)
        {
            return obtenerTodas().Where(i => i.CodigoClase == codigoClase).ToList();
        }

        public int ContarInscripcionesDeLaClase(int codigoClase)
        {
            return _dal.ContarInscripcionesDeLaClase(codigoClase);
        }

        // este metodo inscribe a un nadador del padron en una clase
        public void CrearInscripcionClase(string dni, int codigoClase)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            Nadador13M nadador = _bllNadador.obtenerPorDNI(dni);

            if (nadador == null)
            {
                throw new Exception(idioma.Translate("RegistrarNadadorClase19M.msgNadadorNoEncontrado"));
            }

            Clase13M clase = _bllClase.obtenerPorCodigo(codigoClase);

            if (clase == null)
            {
                throw new Exception(idioma.Translate("RegistrarNadadorClase19M.msgClaseNoEncontrada"));
            }

            // se recalcula la edad a partir de la fecha de nacimiento para no depender
            // de un valor almacenado que puede estar desactualizado
            int edad = CalcularEdad(nadador.FechaNacimiento);

            if (edad < clase.EdadMinima || edad > clase.EdadMaxima)
            {
                throw new Exception(idioma.Translate("RegistrarNadadorClase19M.msgFueraDeRangoEtario"));
            }

            if (ContarInscripcionesDeLaClase(codigoClase) >= clase.CupoMaximo)
            {
                throw new Exception(idioma.Translate("RegistrarNadadorClase19M.msgClaseCompleta"));
            }

            if (_dal.ExisteInscripcionClase(dni, codigoClase))
            {
                throw new Exception(idioma.Translate("RegistrarNadadorClase19M.msgYaInscripto"));
            }

            long dvh = Services.DigitoVerificador13M.CalcularDVH(
                dni + codigoClase + DateTime.Today.ToString("yyyyMMdd") + "Activo");

            _dal.InsertarInscripcionClase(dni, codigoClase, DateTime.Today, "Activo", dvh);

            Services.DigitoVerificador13M.ActualizarDVVInscripcionClase();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se inscribio al nadador DNI {dni} en la clase Nro. {codigoClase}.", Criticidad13M.Medio, Modulos13M.Clase);
        }

        private static int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }

        // este metodo pasa una fila de la base a un objeto de inscripcion a clase
        private InscripcionClase13M MapearInscripcionClase(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            return new InscripcionClase13M
            {
                NumeroInscripcionClase = Convert.ToInt32(row["NumeroInscripcionClase"]),
                DNINadador = row["DNINadador"].ToString(),
                CodigoClase = Convert.ToInt32(row["CodigoClase"]),
                FechaInscripcion = Convert.ToDateTime(row["FechaInscripcion"]),
                Estado = row["Estado"].ToString(),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                TipoClase = row["TipoClase"].ToString()
            };
        }
    }
}