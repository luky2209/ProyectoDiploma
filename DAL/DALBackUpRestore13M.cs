using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBackUpRestore13M
    {
        private DALAcceso13M _dal = new DALAcceso13M();
        private string _nombreDB = "is--servicios";

        public void realizarBackUp(string ruta)
        {
            string query = $@"BACKUP DATABASE [{_nombreDB}] 
                              TO DISK = @ruta 
                              WITH FORMAT, NAME = 'Copia de Seguridad - is--servicios';";

            var parametros = new Dictionary<string, object>
            {
                { "@ruta", ruta }
            };

            _dal.executeNonQuery(query, parametros);
        }

        public void realizarRestore(string ruta)
        {
            string connectionConfigurada = ConexionConfigDAL.ObtenerConnectionString();
            if (string.IsNullOrEmpty(connectionConfigurada))
            {
                throw new Exception("No hay configuracion de conexion guardada. Revise el archivo de configuracion de la aplicacion.");
            }

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionConfigurada);
            builder.InitialCatalog = "master"; // si no apuntamos a la master falla
            string stringConnectionMaster = builder.ConnectionString;

            string query = $@"ALTER DATABASE [{_nombreDB}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                      RESTORE DATABASE [{_nombreDB}] FROM DISK = @ruta WITH REPLACE;
                      ALTER DATABASE [{_nombreDB}] SET MULTI_USER;";

            using (SqlConnection conn = new SqlConnection(stringConnectionMaster)) // lo hacemos aca para utilizar la string connection buildeada.
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ruta", ruta);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
