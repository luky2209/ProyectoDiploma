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
        DALAcceso13M _dal = new DALAcceso13M();

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
