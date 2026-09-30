using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALInscripcion13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        // este metodo trae todas las inscripciones con los datos del nadador y de la prueba
        public DataTable obtenerTodas()
        {
            string query = @"SELECT I.NumeroInscripcion, I.DNINadador, I.CodigoTorneo, I.IdPrueba,
                                    I.Categoria, I.FechaInscripcion, I.Estado, I.DVH,
                                    N.Nombre, N.Apellido,
                                    P.Estilo, P.Distancia
                             FROM Inscripcion I
                             INNER JOIN Nadador N ON N.DNI = I.DNINadador
                             INNER JOIN Prueba P ON P.IdPrueba = I.IdPrueba
                             ORDER BY I.NumeroInscripcion DESC";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        // este metodo trae una sola inscripcion por su numero
        public DataRow obtenerPorNumero(int numeroInscripcion)
        {
            string query = @"SELECT I.NumeroInscripcion, I.DNINadador, I.CodigoTorneo, I.IdPrueba,
                                    I.Categoria, I.FechaInscripcion, I.Estado, I.DVH,
                                    N.Nombre, N.Apellido,
                                    P.Estilo, P.Distancia
                             FROM Inscripcion I
                             INNER JOIN Nadador N ON N.DNI = I.DNINadador
                             INNER JOIN Prueba P ON P.IdPrueba = I.IdPrueba
                             WHERE I.NumeroInscripcion = @numeroInscripcion";

            var parametros = new Dictionary<string, object>
            {
                { "@numeroInscripcion", numeroInscripcion }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // este metodo revisa si el nadador ya esta inscripto en esa misma prueba de ese torneo
        // -numeroExcluido sirve para cuando se esta modificando una inscripcion y no hay que
        // contar la inscripcion que estamos por cambiar, se pasa 0 cuando es una nueva
        public bool ExisteInscripcion(string dni, int codigoTorneo, int idPrueba, int numeroExcluido)
        {
            // basicamente si esta anotado el nadador ya en el torneo va a devolver un 1. 
            string query = @"SELECT COUNT(*)
                             FROM Inscripcion
                             WHERE DNINadador = @dni
                               AND CodigoTorneo = @codigoTorneo
                               AND IdPrueba = @idPrueba
                               AND NumeroInscripcion <> @numeroExcluido";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@codigoTorneo", codigoTorneo },
                { "@idPrueba", idPrueba },
                { "@numeroExcluido", numeroExcluido }
            };

            int cantidad = Convert.ToInt32(acceso.executeScalar(query, parametros));

            return cantidad > 0;
        }

        // este metodo cuenta cuantos nadadores hay inscriptos en un torneo
        public int ContarInscripcionesDelTorneo(int codigoTorneo)
        {
            string query = "SELECT COUNT(*) FROM Inscripcion WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo inserta la inscripcion de un nadador a un torneo
        public int InsertarInscripcion(string dni, int codigoTorneo, int idPrueba, string categoria, DateTime fechaInscripcion, string estado, long dvh)
        {
            string query = @"INSERT INTO Inscripcion (DNINadador, CodigoTorneo, IdPrueba, Categoria, FechaInscripcion, Estado, DVH)
                             VALUES (@dni, @codigoTorneo, @idPrueba, @categoria, @fechaInscripcion, @estado, @dvh);
                             SELECT CAST(SCOPE_IDENTITY() AS int);";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@codigoTorneo", codigoTorneo },
                { "@idPrueba", idPrueba },
                { "@categoria", categoria },
                { "@fechaInscripcion", fechaInscripcion },
                { "@estado", estado },
                { "@dvh", dvh }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo cambia la prueba y el estado de una inscripcion
        public void ModificarInscripcion(int numeroInscripcion, int idPrueba, string categoria, string estado)
        {
            string query = @"UPDATE Inscripcion
                             SET IdPrueba = @idPrueba,
                                 Categoria = @categoria,
                                 Estado = @estado
                             WHERE NumeroInscripcion = @numeroInscripcion";

            var parametros = new Dictionary<string, object>
            {
                { "@numeroInscripcion", numeroInscripcion },
                { "@idPrueba", idPrueba },
                { "@categoria", categoria },
                { "@estado", estado }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo cambia el estado de todas las inscripciones de un nadador en un torneo
        public void ActualizarEstadoInscripciones(string dni, int codigoTorneo, string estado)
        {
            string query = @"UPDATE Inscripcion
                             SET Estado = @estado
                             WHERE DNINadador = @dni AND CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@codigoTorneo", codigoTorneo },
                { "@estado", estado }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo borra una inscripcion
        public void EliminarInscripcion(int numeroInscripcion)
        {
            string query = "DELETE FROM Inscripcion WHERE NumeroInscripcion = @numeroInscripcion";

            var parametros = new Dictionary<string, object>
            {
                { "@numeroInscripcion", numeroInscripcion }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo borra todas las inscripciones que tenia un torneo
        public void EliminarInscripcionesDelTorneo(int codigoTorneo)
        {
            string query = "DELETE FROM Inscripcion WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo solo actualiza el digito verificador de una inscripcion
        public void ActualizarDVH(int numeroInscripcion, long dvh)
        {
            string query = @"UPDATE Inscripcion
                             SET DVH = @dvh
                             WHERE NumeroInscripcion = @numeroInscripcion";

            var parametros = new Dictionary<string, object>
            {
                { "@numeroInscripcion", numeroInscripcion },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}
