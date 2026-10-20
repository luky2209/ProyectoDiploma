using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALInscripcionClase13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        // este metodo trae todas las inscripciones a clase con datos del nadador y de la clase
        public DataTable obtenerTodas()
        {
            string query = @"SELECT IC.NumeroInscripcionClase, IC.DNINadador, IC.CodigoClase,
                                    IC.FechaInscripcion, IC.Estado, IC.DVH,
                                    N.Nombre, N.Apellido,
                                    C.TipoClase
                             FROM InscripcionClase IC
                             INNER JOIN Nadador N ON N.DNI = IC.DNINadador
                             INNER JOIN Clase C ON C.CodigoClase = IC.CodigoClase
                             ORDER BY IC.NumeroInscripcionClase DESC";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        // este metodo revisa si el nadador ya esta inscripto en esa clase
        public bool ExisteInscripcionClase(string dni, int codigoClase)
        {
            string query = @"SELECT COUNT(*)
                             FROM InscripcionClase
                             WHERE DNINadador = @dni
                               AND CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@codigoClase", codigoClase }
            };

            int cantidad = Convert.ToInt32(acceso.executeScalar(query, parametros));

            return cantidad > 0;
        }

        // este metodo cuenta cuantos nadadores hay inscriptos en una clase
        public int ContarInscripcionesDeLaClase(int codigoClase)
        {
            string query = "SELECT COUNT(*) FROM InscripcionClase WHERE CodigoClase = @codigoClase";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoClase", codigoClase }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo inserta la inscripcion de un nadador a una clase
        public int InsertarInscripcionClase(string dni, int codigoClase, DateTime fechaInscripcion, string estado, long dvh)
        {
            string query = @"INSERT INTO InscripcionClase (DNINadador, CodigoClase, FechaInscripcion, Estado, DVH)
                             VALUES (@dni, @codigoClase, @fechaInscripcion, @estado, @dvh);
                             SELECT CAST(SCOPE_IDENTITY() AS int);";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@codigoClase", codigoClase },
                { "@fechaInscripcion", fechaInscripcion },
                { "@estado", estado },
                { "@dvh", dvh }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo solo actualiza el digito verificador de una inscripcion a clase
        public void ActualizarDVH(int numeroInscripcionClase, long dvh)
        {
            string query = @"UPDATE InscripcionClase
                             SET DVH = @dvh
                             WHERE NumeroInscripcionClase = @numeroInscripcionClase";

            var parametros = new Dictionary<string, object>
            {
                { "@numeroInscripcionClase", numeroInscripcionClase },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}