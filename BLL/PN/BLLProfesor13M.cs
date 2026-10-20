using BE;
using BE.Enum;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLProfesor13M
    {
        DALProfesor13M _dal = new DALProfesor13M();

        public List<Profesor13M> obtenerTodos()
        {
            List<Profesor13M> lista = new List<Profesor13M>();

            DataTable dt = _dal.obtenerTodos();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearProfesor(row));
            }
            return lista;
        }

        public Profesor13M obtenerPorLegajo(int legajoProfesor)
        {
            DataRow row = _dal.obtenerPorLegajo(legajoProfesor);

            if (row == null)
            {
                return null;
            }

            return MapearProfesor(row);
        }

        private Profesor13M MapearProfesor(DataRow row)
        {
            return new Profesor13M
            {
                LegajoProfesor = Convert.ToInt32(row["LegajoProfesor"]),
                Nombre = row["Nombre"].ToString(),
                Especialidad = row["Especialidad"].ToString(),
                FranjaHorariaDisponible = row["FranjaHorariaDisponible"].ToString(),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"])
            };
        }
    }
}