using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProfesor13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        public DataTable obtenerTodos()
        {
            string query = @"SELECT LegajoProfesor, Nombre, Especialidad, FranjaHorariaDisponible, DVH
                              FROM Profesor ORDER BY LegajoProfesor";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        public DataRow obtenerPorLegajo(int legajoProfesor)
        {
            string query = @"SELECT LegajoProfesor, Nombre, Especialidad, FranjaHorariaDisponible, DVH
                              FROM Profesor WHERE LegajoProfesor = @legajoProfesor";

            var parametros = new Dictionary<string, object>
            {
                { "@legajoProfesor", legajoProfesor }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return null;
        }

        public void ActualizarDVH(int legajoProfesor, long dvh)
        {
            string query = @"UPDATE Profesor
                              SET DVH = @dvh
                              WHERE LegajoProfesor = @legajoProfesor";

            var parametros = new Dictionary<string, object>
            {
                { "@legajoProfesor", legajoProfesor },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}