using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALDigitoVerificador55CA
    {
        DALAcceso55CA acceso = new DALAcceso55CA();

        

        public void GuardarDVV(string tabla, long dvv, long dvh)
        {
            string query = "INSERT INTO DigitoVerificador (NombreTabla, DVV, DVH) VALUES (@tabla, @dvv, @dvh)";
            var parametros = new Dictionary<string, object>
            {
                { "@tabla", tabla },
                
                { "@dvv", dvv },
            
                { "@dvh", dvh }
            };
            acceso.executeNonQuery(query, parametros);
        }

        public void ActualizarDVV(string tabla, long dvv, long dvh)
        {
            string query = "UPDATE DigitoVerificador SET DVV = @dvv, DVH = @dvh WHERE NombreTabla = @tabla";
            var parametros = new Dictionary<string, object>
                {
                    { "@tabla", tabla },
                    { "@dvv", dvv },
                    { "@dvh", dvh }
                };
            acceso.executeNonQuery(query, parametros);
        }

        public bool ExisteTabla(string tabla)
        {
            string query = "SELECT 1 FROM DigitoVerificador WHERE NombreTabla = @tabla";
            var parametros = new Dictionary<string, object> 
            { 
                { 
                    "@tabla", tabla 
                } 
            };
            DataTable dt = acceso.executeDataTable(query, parametros);
            return dt.Rows.Count > 0;
        }

        public DataRow ObtenerFila(string tabla)
        {
            string query = "SELECT NombreTabla, DVH, DVV FROM DigitoVerificador WHERE NombreTabla = @tabla";
            var parametros = new Dictionary<string, object> 
            { 
                { 
                    "@tabla", tabla 
                } 
            };
            DataTable dt = acceso.executeDataTable(query, parametros);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
    }
}
