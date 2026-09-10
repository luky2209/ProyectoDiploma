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
    public class BLLIdioma55CA
    {
        DALIidioma55CA dal = new DALIidioma55CA();

        public List<Idioma55CA> obtenerTodos()
        {
            DataTable dt = dal.obtenerTodos();
            List<Idioma55CA> lista = new List<Idioma55CA>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new Idioma55CA
                {
                    Id = System.Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return lista;
        }
    }
}
