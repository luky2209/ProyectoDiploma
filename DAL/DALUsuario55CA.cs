using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using Microsoft.SqlServer.Server;

namespace DAL
{
    public class DALUsuario55CA
    {
        DALAcceso55CA acceso = new DALAcceso55CA();

        public int InsertarUsuario(string dni, string nom, string ape, string mail, int idRol, string user, string passHash, long dvh)
        {
            string query = @"INSERT INTO Usuario (DNI, Nombre, Apellido, Email, IdRol, Username, PasswordHash, DVH) 
                     VALUES (@dni, @nom, @ape, @mail, @rol, @user, @pass, @dvh)";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@nom", nom },
                { "@ape", ape },
                { "@mail", mail },
                { "@rol", idRol },
                { "@user", user },
                { "@pass", passHash },
                { "@dvh", dvh }
            };

            int resultado = acceso.executeNonQuery(query, parametros);

            return resultado;
        }

        public void DesactivarUsuario(string dni)
        {
            string query = "UPDATE Usuario SET Activo = 0 WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                {
                    "@dni", dni
                }
            };
            acceso.executeNonQuery(query, parametros);
        }

        public void ActivarUsuario(string dni)
        {
            string query = "UPDATE Usuario SET Activo = 1 WHERE DNI = @dni";
            var parametros = new Dictionary<string, object> 
            {
                { 
                    "@dni", dni 
                } 
            };
            acceso.executeNonQuery(query, parametros);
        }

        public void ModificarUsuario(string dni, string email, int rol)
        {
            string query = @"UPDATE Usuario SET Email = @mail, IdRol = @rol WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@mail", email },
                { "@rol", rol },
                { "@dni", dni }
            };

            acceso.executeNonQuery(query,parametros);
        }

        public void CambiarPassword(string password, string dni)
        {
            string query = @"UPDATE Usuario SET PasswordHash = @password WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            { 
                { 
                    "@password", password 
                },
                { 
                    "@dni", dni 
                }
            };

            acceso.executeNonQuery(query, parametros);
        }

        public void GuardarIdioma(string dni, int idIdioma)
        {
            string query = "UPDATE Usuario SET IdIdioma = @idIdioma WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@idIdioma", idIdioma },
                { "@dni", dni }
            };

            acceso.executeNonQuery(query, parametros);
        }


        #region ObtenerUsuarios
        public DataTable obtenerTodos()
        {
            string query = "SELECT U.*, R.Id AS IdRol, R.Nombre AS NombreRol FROM Usuario U INNER JOIN Rol R ON U.IdRol = R.Id";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        public DataRow obtenerPorDNI(string dni)
        {
            
            string query = "SELECT U.*, R.Id AS IdRol, R.Nombre AS NombreRol FROM Usuario U INNER JOIN Rol R ON U.IdRol = R.Id WHERE U.DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                {
                    "@dni", dni
                }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public bool obtenerPorEmail(string email)
        {
            string query = "SELECT 1 FROM Usuario WHERE Email = @email";

            var parametros = new Dictionary<string, object>
            { 
                
                { 
                    "@email", email 
                } 
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt.Rows.Count > 0;
        }

        public DataRow obtenerPorUser(string user)
        {
            string query = @"SELECT U.*, R.Id AS IdRol, R.Nombre AS NombreRol FROM Usuario U INNER JOIN Rol R ON U.IdRol = R.Id WHERE U.Username = @user";

            var parametros = new Dictionary<string, object> 
            {
                {
                    "@user", user
                }
                
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null; 
        }

        #endregion ObtenerUsuarios

        #region IntentosFallidos

        public void aumentarIntento(string dni)
        {
            string query = @"
                            UPDATE Usuario
                            SET 
                                Intentos = Intentos + 1,
                                UltimoIntentoFallido = GETDATE()
                            WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                {
                    "@dni", dni
                }
            };

            acceso.executeNonQuery(query, parametros);
        }
        public int reiniciarIntentos(string dni)
        {
            string query = @"
                            UPDATE Usuario
                            SET 
                                Intentos = 0,
                                UltimoIntentoFallido = NULL
                            WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                {
                    "@dni", dni
                }
            };

            int resultado = acceso.executeNonQuery(query, parametros);

            return resultado;
        }

        public int bloquearUsuario(string dni)
        {
            string query = "UPDATE Usuario SET Bloqueo = 1, IdIdioma = 1 WHERE DNI = @dni";
            
            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni }
            };

            return acceso.executeNonQuery(query, parametros);
        }

        public int desbloquearUsuario(string dni, string password)
        {
            string query = @"UPDATE Usuario SET Intentos = 0, Bloqueo = 0, PasswordHash = @pass WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
            {
                {
                    "@pass", password
                },
                {
                    "@dni", dni
                }
            };

            return acceso.executeNonQuery(query, parametros);
        }

        #endregion IntentosFallidos

        public void ActualizarDVH(string dni, long dvh)
        {
            string query = @"UPDATE Usuario
                     SET DVH = @dvh
                     WHERE DNI = @dni";

            var parametros = new Dictionary<string, object>
                {
                    { "@dni", dni },
                    { "@dvh", dvh }
                };

            acceso.executeNonQuery(query, parametros);
        }

    }
}
