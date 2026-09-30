using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALPrueba13M
    {
        DALAcceso13M acceso = new DALAcceso13M();

        // este metodo trae todas las pruebas del catalogo (los 20 estilos con distancia)
        public DataTable obtenerTodas()
        {
            string query = @"SELECT IdPrueba, Estilo, Distancia, DVH
                             FROM Prueba ORDER BY Estilo, Distancia";

            DataTable dt = acceso.executeDataTable(query);

            return dt;
        }

        // este metodo trae las pruebas que tiene habilitadas un torneo
        public DataTable obtenerPruebasDelTorneo(int codigoTorneo)
        {
            string query = @"SELECT TP.CodigoTorneo, TP.IdPrueba, P.Estilo, P.Distancia
                             FROM TorneoPrueba TP
                             INNER JOIN Prueba P ON P.IdPrueba = TP.IdPrueba
                             WHERE TP.CodigoTorneo = @codigoTorneo
                             ORDER BY P.Estilo, P.Distancia";

            var parametros = new Dictionary<string, object>
            {
                { "@codigoTorneo", codigoTorneo }
            };

            DataTable dt = acceso.executeDataTable(query, parametros);

            return dt;
        }
        public void ActualizarDVH(int idPrueba, long dvh)
        {
            string query = @"UPDATE Prueba
                             SET DVH = @dvh
                             WHERE IdPrueba = @idPrueba";

            var parametros = new Dictionary<string, object>
            {
                { "@idPrueba", idPrueba },
                { "@dvh", dvh }
            };

            acceso.executeNonQuery(query, parametros);
        }
    }
}
