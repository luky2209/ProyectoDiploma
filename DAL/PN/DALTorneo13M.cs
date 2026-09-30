using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALTorneo13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        public DataTable obtenerTodos()
        {
            string query = @"SELECT CodigoTorneo, Nombre, Fecha, Sede, Arancel, Categorias, Estado, FechaCierre, DVH
                             FROM Torneo ORDER BY Fecha DESC";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        public DataRow obtenerPorCodigo(int codigoTorneo)
        {
            string query = @"SELECT CodigoTorneo, Nombre, Fecha, Sede, Arancel, Categorias, Estado, FechaCierre, DVH
                             FROM Torneo WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            // Si encontro al menos una fila, devuelve la primera
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }

            // si la tabla vino vacia, devuelve null
            return null;
        }

        // aca agarra torneos que todavia se pueden usar para inscribir
        // o sea los que estan abiertos y la la fecha no paso
        public DataTable obtenerAbiertos()
        {
            string query = @"SELECT CodigoTorneo, Nombre, Fecha, Sede, Arancel, Categorias, Estado, FechaCierre, DVH
                             FROM Torneo
                             WHERE Estado = 'Abierto' AND Fecha >= CAST(GETDATE() AS date)
                             ORDER BY Fecha";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        // aca agarra los torneos que ya se cerraron y tienen el ranking cargado
        public DataTable obtenerFinalizados()
        {
            string query = @"SELECT CodigoTorneo, Nombre, Fecha, Sede, Arancel, Categorias, Estado, FechaCierre, DVH
                             FROM Torneo
                             WHERE Estado = 'Finalizado'
                             ORDER BY FechaCierre DESC";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        // este metodo inserta un torneo nuevo y devuelve el codigo que se le genero
        public int InsertarTorneo(string nombre, DateTime fecha, string sede, decimal arancel, string categorias, string estado, long dvh)
        {
            string query = @"INSERT INTO Torneo (Nombre, Fecha, Sede, Arancel, Categorias, Estado, DVH)
                             VALUES (@nombre, @fecha, @sede, @arancel, @categorias, @estado, @dvh);
                             SELECT CAST(SCOPE_IDENTITY() AS int);";

            var parametros = new Dictionary<string, object>
            {
                { "@nombre", nombre },
                { "@fecha", fecha },
                { "@sede", sede },
                { "@arancel", arancel },
                { "@categorias", categorias },
                { "@estado", estado },
                { "@dvh", dvh }
            };

            return Convert.ToInt32(acceso.executeScalar(query, parametros));
        }

        // este metodo cambia los datos de un torneo que ya existe
        public void ModificarTorneo(int codigoTorneo, string nombre, DateTime fecha, string sede, decimal arancel, string categorias)
        {
            string query = @"UPDATE Torneo
                             SET Nombre = @nombre,
                                 Fecha = @fecha,
                                 Sede = @sede,
                                 Arancel = @arancel,
                                 Categorias = @categorias
                             WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo },
                { "@nombre", nombre },
                { "@fecha", fecha },
                { "@sede", sede },
                { "@arancel", arancel },
                { "@categorias", categorias }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo cierra un torneo, le pone la fecha de cierre y lo pasa a finalizado
        public void FinalizarTorneo(int codigoTorneo, DateTime fechaCierre)
        {
            string query = @"UPDATE Torneo
                             SET Estado = 'Finalizado',
                                 FechaCierre = @fechaCierre
                             WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo },
                { "@fechaCierre", fechaCierre }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo borra un torneo de la base
        public void EliminarTorneo(int codigoTorneo)
        {
            string query = "DELETE FROM Torneo WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo solo actualiza el digito verificador de un torneo
        public void ActualizarDVH(int codigoTorneo, long dvh)
        {
            string query = @"UPDATE Torneo
                             SET DVH = @dvh
                             WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo agrega una prueba del catalogo a las habilitadas de un torneo
        public void InsertarTorneoPrueba(int codigoTorneo, int idPrueba, long dvh)
        {
            string query = @"INSERT INTO TorneoPrueba (CodigoTorneo, IdPrueba, DVH)
                             VALUES (@codigoTorneo, @idPrueba, @dvh)";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo },
                { "@idPrueba", idPrueba },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo solo actualiza el digito verificador de una prueba de un torneo
        public void ActualizarDVHTorneoPrueba(int codigoTorneo, int idPrueba, long dvh)
        {
            string query = @"UPDATE TorneoPrueba
                             SET DVH = @dvh
                             WHERE CodigoTorneo = @codigoTorneo AND IdPrueba = @idPrueba";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo },
                { "@idPrueba", idPrueba },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo borra todas las pruebas que tenia habilitadas un torneo
        public void EliminarTorneoPruebasDe(int codigoTorneo)
        {
            string query = "DELETE FROM TorneoPrueba WHERE CodigoTorneo = @codigoTorneo";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            acceso.executeNonQuery(query, parametros);
        }

        // este metodo trae todas las filas de la tabla TorneoPrueba, se usa para el digito verificador
        public DataTable obtenerTodasTorneoPrueba()
        {
            string query = @"SELECT CodigoTorneo, IdPrueba, DVH
                             FROM TorneoPrueba ORDER BY CodigoTorneo, IdPrueba";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }
    }
}
