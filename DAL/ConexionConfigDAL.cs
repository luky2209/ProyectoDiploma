using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal static class ConexionConfigDAL
    {
        private const string NOMBRE_CONEXION = "ConexionPrincipal";

        public static string ObtenerConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[NOMBRE_CONEXION]?.ConnectionString;
        }
    }
}
