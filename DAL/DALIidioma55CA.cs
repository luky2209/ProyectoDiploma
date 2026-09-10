using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALIidioma55CA
    {
        DALAcceso55CA acceso = new DALAcceso55CA();

        public DataTable obtenerTodos()
        {
            string query = "SELECT Id, Nombre FROM Idioma";
            return acceso.executeDataTable(query);
        }
    }
}
