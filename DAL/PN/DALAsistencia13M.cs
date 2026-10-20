using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALAsistencia13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        // este metodo trae la asistencia de una clase en una fecha, con datos del nadador
        public DataTable obtenerPorClaseYFecha(int codigoClase, DateTime fecha)
        {
            string query = @"SELECT A.NumeroAsistencia, A.CodigoClase, A.Fecha, A.DNINadador, A.Condicion, A.DVH,
                                    N.Nombre, N.Apellido,
                                    C.TipoClase
                             FROM Asistencia A
                             INNER JOIN Nadador N ON N.DNI = A.DNINadador
                             INNER JOIN Clase C ON C.CodigoClase = A.CodigoClase
                             WHERE A.CodigoClase = @codigoClase
                               AND A.Fecha = @fecha
                             ORDER BY N.Apellido, N.Nombre";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase },
                { "@fecha", fecha.Date }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt;
        }

        // este metodo revisa si ya existe asistencia para ese nadador en esa clase y fecha
        public bool ExisteAsistencia(string dni, int codigoClase, DateTime fecha)
        {
            string query = @"SELECT COUNT(*)
                             FROM Asistencia
                             WHERE DNINadador = @dni
                               AND CodigoClase = @codigoClase
                               AND Fecha = @fecha";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@codigoClase", codigoClase },
                { "@fecha", fecha.Date }
            };

            int cantidad = Convert.ToInt32(acceso.executeScalar(query, parametros));

            return cantidad > 0;
        }

        // este metodo inserta la asistencia de un nadador
        public int InsertarAsistencia(int codigoClase, DateTime fecha, string dni, string condicion, long dvh)
        {
            string query = @"INSERT INTO Asistencia (CodigoClase, Fecha, DNINadador, Condicion, DVH)
                             VALUES (@codigoClase, @fecha, @dni, @condicion, @dvh);
                             SELECT CAST(SCOPE_IDENTITY() AS int);";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase },
                { "@fecha", fecha.Date },
                { "@dni", dni },
                { "@condicion", condicion },
                { "@dvh", dvh }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo actualiza la condicion de una asistencia que ya existe
        public void ActualizarCondicion(int codigoClase, DateTime fecha, string dni, string condicion, long dvh)
        {
            string query = @"UPDATE Asistencia
                             SET Condicion = @condicion,
                                 DVH = @dvh
                             WHERE CodigoClase = @codigoClase
                               AND Fecha = @fecha
                               AND DNINadador = @dni";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase },
                { "@fecha", fecha.Date },
                { "@dni", dni },
                { "@condicion", condicion },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}