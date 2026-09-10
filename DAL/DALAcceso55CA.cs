using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALAcceso55CA
    {
        private string _stringConnection => ConexionConfigDAL.ObtenerConnectionString();
        public DataTable executeDataTable(string query, Dictionary<string, object> parametros = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    if(parametros != null)
                    {
                        foreach (var p in parametros)
                        {
                            cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                        } 
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    try
                    {
                        da.Fill(dt);
                    }
                    catch (SqlException ex)
                    {

                        throw new Exception("Error de lectura en la base de datos", ex);
                    }
                }
            }
            return dt;
        }

        public int executeNonQuery(string consulta, Dictionary<string, object> parametros = null)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    if (parametros != null)
                    {
                        foreach (var p in parametros)
                        {
                            cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                        }
                    }
                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error al escribir en la base de datos", ex);
                    }
                }
            }
        }

        public object executeScalar(string consulta, Dictionary<string, object> parametros = null)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                using (SqlCommand cmd = new SqlCommand(consulta, conn))
                {
                    if (parametros != null)
                    {
                        foreach (var p in parametros)
                        {
                            cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
                        }
                    }

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error al leer valor escalar en la base de datos", ex);
                    }
                }
            }
        }
    }
}
