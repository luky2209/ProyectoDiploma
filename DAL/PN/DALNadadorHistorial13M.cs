using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class DALNadadorHistorial13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        public DataTable ObtenerHistorialPorDNI(string dni)
        {
            string query = @"SELECT IdHistorial, DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH,
                                    VersionNumero, FechaHora, TipoOperacion, UsuarioDNI, Activo, OrigenVersionId
                             FROM NadadorHistorial 
                             WHERE DNI = @dni 
                             ORDER BY IdHistorial DESC";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni }
            };

            return acceso.executeDataTable(query, parametros);
        }

        public DataRow ObtenerPorId(long idHistorial)
        {
            string query = @"SELECT IdHistorial, DNI, Nombre, Apellido, FechaNacimiento, Edad, Categoria, CertificadoMedico, DVH,
                                    VersionNumero, FechaHora, TipoOperacion, UsuarioDNI, Activo, OrigenVersionId
                             FROM NadadorHistorial 
                             WHERE IdHistorial = @idHistorial";

            var parametros = new Dictionary<string, object>
            {
                { "@idHistorial", idHistorial }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public void RestaurarVersion(long idHistorial, string usuarioDNI)
        {
            string query = "EXEC sp_RestaurarVersionNadador @IdHistorial, @UsuarioDNI";

            var parametros = new Dictionary<string, object>
            {
                { "@IdHistorial", idHistorial },
                { "@UsuarioDNI", usuarioDNI }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}
