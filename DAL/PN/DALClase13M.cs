using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALClase13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        public DataTable obtenerTodos()
        {
            string query = @"SELECT c.CodigoClase, c.TipoClase, c.NivelDificultad, c.Dias, c.HorarioInicio, c.HorarioFin,
                                    c.EdadMinima, c.EdadMaxima, c.CupoMaximo, c.LegajoProfesor, c.FechaAsignacion, c.DVH,
                                    p.Nombre as NombreProfesor
                             FROM Clase c
                              LEFT JOIN Profesor p ON p.LegajoProfesor = c.LegajoProfesor
                              ORDER BY c.CodigoClase";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        public DataRow obtenerPorCodigo(int codigoClase)
        {
            string query = @"SELECT c.CodigoClase, c.TipoClase, c.NivelDificultad, c.Dias, c.HorarioInicio, c.HorarioFin,
                                    c.EdadMinima, c.EdadMaxima, c.CupoMaximo, c.LegajoProfesor, c.FechaAsignacion, c.DVH,
                                    p.Nombre as NombreProfesor
                             FROM Clase c
                              LEFT JOIN Profesor p ON p.LegajoProfesor = c.LegajoProfesor
                              WHERE c.CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            return null;
        }

        // este metodo inserta una clase nueva y devuelve el codigo que se le genero
        public int InsertarClase(string tipoClase, string nivelDificultad, string dias, TimeSpan horarioInicio, TimeSpan horarioFin, int edadMinima, int edadMaxima, int cupoMaximo, long dvh)
        {
            string query = @"INSERT INTO Clase (TipoClase, NivelDificultad, Dias, HorarioInicio, HorarioFin, EdadMinima, EdadMaxima, CupoMaximo, DVH)
                             VALUES (@tipoClase, @nivel, @dias, @horarioInicio, @horarioFin, @edadMinima, @edadMaxima, @cupo, @dvh);
                             SELECT CAST(SCOPE_IDENTITY() AS int);";

            var parametros = new Dictionary<string, object>
            {
                { "@tipoClase", tipoClase },
                { "@nivel", nivelDificultad },
                { "@dias", dias },
                { "@horarioInicio", horarioInicio },
                { "@horarioFin", horarioFin },
                { "@edadMinima", edadMinima },
                { "@edadMaxima", edadMaxima },
                { "@cupo", cupoMaximo },
                { "@dvh", dvh }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo cambia los datos de una clase que ya existe
        public void ModificarClase(int codigoClase, string tipoClase, string nivelDificultad, string dias, TimeSpan horarioInicio, TimeSpan horarioFin, int edadMinima, int edadMaxima, int cupoMaximo)
        {
            string query = @"UPDATE Clase
                             SET TipoClase = @tipoClase,
                                 NivelDificultad = @nivel,
                                 Dias = @dias,
                                 HorarioInicio = @horarioInicio,
                                 HorarioFin = @horarioFin,
                                 EdadMinima = @edadMinima,
                                 EdadMaxima = @edadMaxima,
                                 CupoMaximo = @cupo
                             WHERE CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase },
                { "@tipoClase", tipoClase },
                { "@nivel", nivelDificultad },
                { "@dias", dias },
                { "@horarioInicio", horarioInicio },
                { "@horarioFin", horarioFin },
                { "@edadMinima", edadMinima },
                { "@edadMaxima", edadMaxima },
                { "@cupo", cupoMaximo }
            };

            acceso.executeNonQuery(query, parametros);
        }

        public void AsignarProfesorClase(int codigoClase, int? legajoProfesor, DateTime? fechaAsignacion)
        {
            string query = @"UPDATE Clase
                              SET LegajoProfesor = @legajoProfesor,
                                  FechaAsignacion = @fechaAsignacion
                              WHERE CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase },
                { "@legajoProfesor", legajoProfesor },
                { "@fechaAsignacion", fechaAsignacion }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo borra una clase de la base
        public void EliminarClase(int codigoClase)
        {
            string query = "DELETE FROM Clase WHERE CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo solo actualiza el digito verificador de una clase
        public void ActualizarDVH(int codigoClase, long dvh)
        {
            string query = @"UPDATE Clase
                             SET DVH = @dvh
                             WHERE CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}
