using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLIdioma13M
    {
        DALIidioma13M dal = new DALIidioma13M();

        public List<Idioma13M> obtenerTodos()
        {
            DataTable dt = dal.obtenerTodos();
            List<Idioma13M> lista = new List<Idioma13M>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Idioma13M
                {
                    Id = System.Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return lista;
        }
    }
}
