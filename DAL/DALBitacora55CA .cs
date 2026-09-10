using BE;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBitacora55CA
    {
        DALAcceso55CA acceso = new DALAcceso55CA();

        public int insertarLog(string dni, string evento, int criticidad, int modulo, DateTime fecha)
        {
            string query = @"INSERT INTO BitacoraEventos
            (DNI, Evento, Criticidad, Modulo, FechaHora) 
            VALUES (@dni, @evento, @criticidad, @modulo, @fecha)";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@dni", dni },
                { "@evento", evento },
                { "@criticidad", criticidad },
                { "@modulo", modulo },
                { "@fecha", fecha }
            };

            int resultado = acceso.executeNonQuery(query, parametros);

            return resultado;
        }

        public DataTable obtenerBitacora(DateTime desde, DateTime hasta, int? moduloId = null)
        {
            string query = @"SELECT B.Id, B.DNI, U.Username, U.Nombre, U.Apellido, B.Evento,
            B.Criticidad, B.Modulo, B.FechaHora FROM BitacoraEventos B LEFT JOIN Usuario U ON B.DNI = U.DNI
            WHERE B.FechaHora BETWEEN @desde AND @hasta";


            var parametros = new Dictionary<string, object>
            {
                {
                    "@desde", desde
                },
                {
                    "@hasta", hasta
                }
            };

            if (moduloId.HasValue)
            {
                query += " AND B.Modulo = @modulo";
                parametros.Add("@modulo", moduloId.Value);
            }

            return acceso.executeDataTable(query, parametros);
        }
    }
}
