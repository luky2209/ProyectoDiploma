using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPatente
    {
        DALAcceso55CA _dal = new DALAcceso55CA();

        public DataTable obtenerTodos()
        {
            string query = "SELECT * FROM Patente";

            return _dal.executeDataTable(query);
        }

        public void ActualizarDVH(int id, long dvh)
        {
            string query = "UPDATE Patente SET DVH = @dvh WHERE Id = @id";
            var parametros = new Dictionary<string, object>
            {
                { "@dvh", dvh },
                { "@id", id }
            };
            _dal.executeNonQuery(query, parametros);
        }
    }
}
