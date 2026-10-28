using BE;
using BE.Enum;
using DAL;
using Services;
using Services_13M;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class BLLNadadorHistorial13M
    {
        DALNadadorHistorial13M _dalHistorial = new DALNadadorHistorial13M();
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<NadadorHistorial13M> ObtenerHistorialPorDNI(string dni)
        {
            List<NadadorHistorial13M> lista = new List<NadadorHistorial13M>();
            DataTable dt = _dalHistorial.ObtenerHistorialPorDNI(dni);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(Mapear(row));
            }

            return lista;
        }

        public NadadorHistorial13M ObtenerPorId(long idHistorial)
        {
            DataRow row = _dalHistorial.ObtenerPorId(idHistorial);
            if (row == null) return null;
            return Mapear(row);
        }

        public void RestaurarVersion(long idHistorial)
        {
            string dniAutor = ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;
            NadadorHistorial13M version = ObtenerPorId(idHistorial);

            _dalHistorial.RestaurarVersion(idHistorial, dniAutor);

            if (version != null)
            {
                _bit.registrarEvento(dniAutor, $"Se resto la version {version.VersionNumero} del nadador DNI {version.DNI}.", Criticidad13M.Medio, Modulos13M.Nadador);
            }
            else
            {
                _bit.registrarEvento(dniAutor, $"Se resto una version del historial Id {idHistorial}.", Criticidad13M.Medio, Modulos13M.Nadador);
            }
        }

        private NadadorHistorial13M Mapear(DataRow row)
        {
            return new NadadorHistorial13M
            {
                IdHistorial = Convert.ToInt64(row["IdHistorial"]),
                DNI = row["DNI"].ToString(),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                Edad = Convert.ToInt32(row["Edad"]),
                Categoria = row["Categoria"].ToString(),
                CertificadoMedico = Convert.ToBoolean(row["CertificadoMedico"]),
                DVH = row["DVH"] == DBNull.Value ? (long?)null : Convert.ToInt64(row["DVH"]),
                VersionNumero = Convert.ToInt32(row["VersionNumero"]),
                FechaHora = Convert.ToDateTime(row["FechaHora"]),
                TipoOperacion = row["TipoOperacion"].ToString(),
                UsuarioDNI = row["UsuarioDNI"] == DBNull.Value ? null : row["UsuarioDNI"].ToString(),
                Activo = Convert.ToBoolean(row["Activo"]),
                OrigenVersionId = row["OrigenVersionId"] == DBNull.Value ? (long?)null : Convert.ToInt64(row["OrigenVersionId"])
            };
        }
    }
}
