using BE;
using BE.Enum;
using DAL;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLNadador13M
    {
        DALNadador13M _dal = new DALNadador13M();
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<Nadador13M> obtenerTodos()
        {
            List<Nadador13M> lista = new List<Nadador13M>();

            DataTable dt = _dal.obtenerTodos();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearNadador(row));
            }
            return lista;
        }

        public void CrearNadador(string dni, string nombre, string apellido, DateTime fechaNacimiento, int edad, string categoria, bool certificadoMedico)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (_dal.obtenerPorDNI(dni) != null)
            {
                throw new Exception(idioma.Translate("RegistrarNadador19M.msgDNIExistente"));
            }

            long dvh = Services.DigitoVerificador13M.CalcularDVH(
            dni + nombre + apellido + fechaNacimiento.ToString("yyyyMMdd") + edad + categoria + certificadoMedico);

            _dal.InsertarNadador(dni, nombre, apellido, fechaNacimiento, edad, categoria, certificadoMedico, dvh);

            Services.DigitoVerificador13M.ActualizarDVVNadador();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se registró el nadador DNI {dni}.", Criticidad13M.Medio, Modulos13M.Nadador);
        }

        public void ModificarNadador(string dni, string nombre, string apellido, DateTime fechaNacimiento, int edad, string categoria, bool certificadoMedico)
        {
            _dal.ModificarNadador(dni, nombre, apellido, fechaNacimiento, edad, categoria, certificadoMedico);

            RecalcularDVHNadador(dni);

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se modificó el nadador DNI {dni}.", Criticidad13M.Medio, Modulos13M.Nadador);
        }

        public void EliminarNadador(string dni)
        {
            _dal.EliminarNadador(dni);

            Services.DigitoVerificador13M.ActualizarDVVNadador();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se eliminó el nadador DNI {dni}.", Criticidad13M.Medio, Modulos13M.Nadador);
        }

        private Nadador13M MapearNadador(DataRow row)
        {
            return new Nadador13M
            {
                DNI = row["DNI"].ToString(),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                Edad = Convert.ToInt32(row["Edad"]),
                Categoria = row["Categoria"].ToString(),
                CertificadoMedico = Convert.ToBoolean(row["CertificadoMedico"]),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"])
            };
        }

        private void RecalcularDVHNadador(string dni)
        {
            DataRow row = _dal.obtenerPorDNI(dni);

            Nadador13M nadador = MapearNadador(row);

            long nuevoDVH = Services.DigitoVerificador13M.CalcularDVH(
                nadador.DNI + nadador.Nombre + nadador.Apellido +
                nadador.FechaNacimiento.ToString("yyyyMMdd") + nadador.Edad +
                nadador.Categoria + nadador.CertificadoMedico);

            _dal.ActualizarDVH(dni, nuevoDVH);

            Services.DigitoVerificador13M.ActualizarDVVNadador();
        }
    }
}