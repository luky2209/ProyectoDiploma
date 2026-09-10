using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBackUpRestore55CA
    {
        private DALAcceso55CA _dal = new DALAcceso55CA();
        private string _nombreDB = "is--servicios";
        //private readonly string _stringConnectionOriginal = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=is--servicios;Integrated Security=True";
        private readonly string _stringConnectionOriginal = "Data Source=LAPTOP-8BNKG482\\SQLEXPRESS;Initial Catalog=is--servicios;Integrated Security=True";

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
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(_stringConnectionOriginal);
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
