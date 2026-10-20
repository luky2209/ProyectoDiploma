using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALResultado13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        // este metodo trae el ranking de un torneo con los datos del nadador y de la prueba
        // los descalificados van al final porque no tienen puesto
        public DataTable obtenerPorTorneo(int codigoTorneo)
        {
            string query = @"SELECT R.IdResultado, R.NumeroInscripcion, R.DNINadador, R.IdPrueba,
                                    R.Minutos, R.Segundos, R.Centesimas, R.Descalificado,
                                    R.Posicion, R.Premio, R.DVH,
                                    N.Nombre, N.Apellido, I.Categoria, I.Estado,
                                    P.Estilo, P.Distancia
                             FROM Resultado R
                             INNER JOIN Inscripcion I ON I.NumeroInscripcion = R.NumeroInscripcion
                             INNER JOIN Nadador N ON N.DNI = R.DNINadador
                             INNER JOIN Prueba P ON P.IdPrueba = R.IdPrueba
                             WHERE I.CodigoTorneo = @codigoTorneo
                             ORDER BY P.Estilo, P.Distancia, R.Descalificado, R.Posicion";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt;
        }

        // este metodo inserta el resultado de un nadador en una prueba de un torneo
        public void InsertarResultado(int numeroInscripcion, string dni, int idPrueba, int minutos,
            int segundos, int centesimas, bool descalificado, int? posicion, string premio, long dvh)
        {
            string query = @"INSERT INTO Resultado (NumeroInscripcion, DNINadador, IdPrueba, Minutos,
                                             Segundos, Centesimas, Descalificado, Posicion, Premio, DVH)
                             VALUES (@numeroInscripcion, @dni, @idPrueba, @minutos,
                                     @segundos, @centesimas, @descalificado, @posicion, @premio, @dvh)";

            var parametros = new Dictionary<string, object>
            {
                { "@numeroInscripcion", numeroInscripcion },
                { "@dni", dni },
                { "@idPrueba", idPrueba },
                { "@minutos", minutos },
                { "@segundos", segundos },
                { "@centesimas", centesimas },
                { "@descalificado", descalificado },
                { "@posicion", posicion == null ? (object)DBNull.Value : posicion },
                { "@premio", premio },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo trae todas las filas de la tabla Resultado, se usa para el digito verificador
        public DataTable obtenerTodas()
        {
            string query = @"SELECT IdResultado, NumeroInscripcion, DNINadador, IdPrueba,
                                    Minutos, Segundos, Centesimas, Descalificado,
                                    Posicion, Premio, DVH
                             FROM Resultado ORDER BY IdResultado";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        // este metodo solo actualiza el digito verificador de un resultado
        public void ActualizarDVH(int idResultado, long dvh)
        {
            string query = @"UPDATE Resultado
                             SET DVH = @dvh
                             WHERE IdResultado = @idResultado";

            var parametros = new Dictionary<string, object>
            {
                { "@idResultado", idResultado },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo borra todos los resultados que tenia un torneo
        public void EliminarResultadosDelTorneo(int codigoTorneo)
        {
            string query = @"DELETE R FROM Resultado R
                             INNER JOIN Inscripcion I ON I.NumeroInscripcion = R.NumeroInscripcion
                             WHERE I.CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}
