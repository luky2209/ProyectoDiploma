using BE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Instalador
{
    public static class ServicioConfiguracionConexion
    {
        private const string NOMBRE_CONEXION = "ConexionPrincipal";

        public static List<InstanciaSqlBE> ObtenerInstanciasDisponibles()
        {
            var resultado = new List<InstanciaSqlBE>();
            try
            {
                var tabla = SqlDataSourceEnumerator.Instance.GetDataSources();
                foreach (System.Data.DataRow fila in tabla.Rows)
                {
                    string servidor = fila["ServerName"].ToString();
                    string nombreInstancia = fila["InstanceName"].ToString();
                    string nombreCompleto = string.IsNullOrEmpty(nombreInstancia)
                        ? servidor : $@"{servidor}\{nombreInstancia}";
                    resultado.Add(new InstanciaSqlBE { NombreServidor = nombreCompleto });
                }
            }
            catch { }

            AgregarSiNoExiste(resultado, $@"{Environment.MachineName}\SQLEXPRESS");
            AgregarSiNoExiste(resultado, ".\\SQLEXPRESS");
            AgregarSiNoExiste(resultado, "(localdb)\\MSSQLLocalDB");
            AgregarSiNoExiste(resultado, Environment.MachineName);

            return resultado;
        }

        private static void AgregarSiNoExiste(List<InstanciaSqlBE> lista, string nombre)
        {
            if (!lista.Exists(i => i.NombreServidor.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                lista.Add(new InstanciaSqlBE { NombreServidor = nombre });
        }

        public static string ConstruirConnectionString(string servidor, string baseDatos, bool usarWindowsAuth,
            string usuario = null, string password = null)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = servidor,
                InitialCatalog = baseDatos,
                IntegratedSecurity = usarWindowsAuth,
                ConnectTimeout = 5
            };
            if (!usarWindowsAuth)
            {
                builder.UserID = usuario;
                builder.Password = password;
            }
            return builder.ConnectionString;
        }

        public static (bool exito, string mensajeError) ProbarConexion(string connectionString)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return (true, null);
                }
            }
            catch (SqlException ex)
            {
                return (false, $"No se pudo conectar: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public static string ObtenerConnectionStringGuardada()
        {
            return ConfigurationManager.ConnectionStrings[NOMBRE_CONEXION]?.ConnectionString;
        }

        public static bool ExisteConfiguracionValida()
        {
            return !string.IsNullOrWhiteSpace(ObtenerConnectionStringGuardada());
        }

        public static void GuardarConnectionString(string nuevaConnectionString)
        {
            Configuration configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var seccion = configFile.ConnectionStrings;

            if (seccion.ConnectionStrings[NOMBRE_CONEXION] != null)
                seccion.ConnectionStrings[NOMBRE_CONEXION].ConnectionString = nuevaConnectionString;
            else
                seccion.ConnectionStrings.Add(new ConnectionStringSettings(NOMBRE_CONEXION, nuevaConnectionString, "System.Data.SqlClient"));

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        public static bool ExisteBaseDatos(string connectionString, string nombreBaseDatos)
        {
            var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" };

            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM sys.databases WHERE name = @nombre", conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreBaseDatos);
                    int cantidad = (int)cmd.ExecuteScalar();
                    return cantidad > 0;
                }
            }
        }

        public static void EjecutarScriptCreacion(string connectionString, string rutaScriptSql)
        {
            var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" };
            string contenidoScript = File.ReadAllText(rutaScriptSql);

            string[] lotes = contenidoScript.Split(
                new[] { "\nGO", "\nGo", "\ngo", "\r\nGO", "\r\nGo", "\r\ngo" },
                StringSplitOptions.RemoveEmptyEntries);

            using (var conn = new SqlConnection(builder.ConnectionString))
            {
                conn.Open();
                foreach (string lote in lotes)
                {
                    if (string.IsNullOrWhiteSpace(lote)) continue;

                    using (var cmd = new SqlCommand(lote, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

    }
}
