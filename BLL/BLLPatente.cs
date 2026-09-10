using DAL;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLPatente
    {
        DALPatente dal = new DALPatente();
        public List<PermisoModelo13M> obtenerTodos()
        {
            DataTable dt = dal.obtenerTodos();

            List<PermisoModelo13M> lista = new List<PermisoModelo13M>();

            foreach (DataRow row in dt.Rows)
            {
                var patente = new PermisoModelo13M
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                };

                lista.Add(patente);
            }

            return lista;
        }

        

        
    }
}
