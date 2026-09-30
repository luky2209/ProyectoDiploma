using BE;
using BE.Enum;
using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLResultado13M
    {
        DALResultado13M _dalResultado = new DALResultado13M();
        BLLTorneo13M _bllTorneo;
        BitacoraEventosService _bit = new BitacoraEventosService();

        // este metodo guarda los tiempos de los nadadores y cierra el torneo
        public void FinalizarTorneo(int codigoTorneo, List<Resultado13M> resultados)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (_bllTorneo == null) { _bllTorneo = new BLLTorneo13M(); }
            Torneo13M torneo = _bllTorneo.obtenerPorCodigo(codigoTorneo);

            if (torneo == null)
            {
                throw new Exception(idioma.Translate("CargarResultados19M.msgTorneoNoEncontrado"));
            }

            if (torneo.Estado != "Abierto")
            {
                throw new Exception(idioma.Translate("CargarResultados19M.msgTorneoNoAbierto"));
            }

            ValidarTiempos(resultados);

            // el puesto y el premio se calculan antes de guardar, asi el resultado
            // se inserta una sola vez y ya queda completo
            CalcularPosicionesYPremios(resultados);

            foreach (Resultado13M resultado in resultados)
            {
                long dvh = CalcularDVHResultado(resultado);

                _dalResultado.InsertarResultado(resultado.NumeroInscripcion, resultado.DNINadador,
                    resultado.IdPrueba, resultado.Minutos, resultado.Segundos, resultado.Centesimas,
                    resultado.Descalificado, resultado.Posicion, resultado.Premio, dvh);
            }

            Services.DigitoVerificador13M.ActualizarDVVResultado();

            _bllTorneo.FinalizarTorneo(codigoTorneo, DateTime.Today);

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se finalizaron los resultados del torneo {torneo.Nombre} con {resultados.Count} nadador/es.", Criticidad13M.Alto, Modulos13M.Resultado);
        }

        // este metodo trae el ranking oficial de un torneo que ya fue finalizado
        public List<Resultado13M> obtenerRankingDelTorneo(int codigoTorneo)
        {
            List<Resultado13M> lista = new List<Resultado13M>();

            DataTable dt = _dalResultado.obtenerPorTorneo(codigoTorneo);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearResultado(row));
            }

            return lista;
        }

        // este metodo borra todos los resultados de un torneo
        public void EliminarResultadosDelTorneo(int codigoTorneo)
        {
            _dalResultado.EliminarResultadosDelTorneo(codigoTorneo);
        }

        // este metodo revisa que los segundos y las centesimas esten en rango
        // un nadador descalificado puede dejar el tiempo en cero
        private void ValidarTiempos(List<Resultado13M> resultados)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            foreach (Resultado13M resultado in resultados)
            {
                if (resultado.Descalificado)
                {
                    continue;
                }

                if (resultado.Minutos < 0 || resultado.Segundos < 0 || resultado.Segundos > 59 ||
                    resultado.Centesimas < 0 || resultado.Centesimas > 99)
                {
                    throw new Exception(idioma.Translate("CargarResultados19M.msgTiempoInvalido"));
                }
            }
        }

        // este metodo reparte los puestos y las medallas de cada prueba del torneo
        // el podio se arma con los nadadores que no fueron descalificados, del tiempo mas bajo al mas alto
        private void CalcularPosicionesYPremios(List<Resultado13M> resultados)
        {
            List<int> idsPrueba = resultados.Select(r => r.IdPrueba).Distinct().ToList();

            foreach (int idPrueba in idsPrueba)
            {
                List<Resultado13M> nadadoresDeLaPrueba = resultados
                    .Where(r => r.IdPrueba == idPrueba)
                    .OrderBy(r => r.Descalificado)
                    .ThenBy(r => r.TiempoEnCentesimas)
                    .ToList();

                int posicion = 0;

                foreach (Resultado13M resultado in nadadoresDeLaPrueba)
                {
                    if (resultado.Descalificado)
                    {
                        // el descalificado no compite por el podio, se queda sin puesto y sin premio
                        resultado.Posicion = null;
                        resultado.Premio = "";
                        continue;
                    }

                    posicion++;

                    resultado.Posicion = posicion;
                    resultado.Premio = ObtenerPremio(posicion);
                }
            }
        }

        // este metodo dice que medalla le toca a cada puesto del podio
        private string ObtenerPremio(int posicion)
        {
            if (posicion == 1) { return "Medalla de Oro"; }
            if (posicion == 2) { return "Medalla de Plata"; }
            if (posicion == 3) { return "Medalla de Bronce"; }
            return "Mencion";
        }

        private long CalcularDVHResultado(Resultado13M resultado)
        {
            string posicion = resultado.Posicion.HasValue ? resultado.Posicion.Value.ToString() : "0";

            return Services.DigitoVerificador13M.CalcularDVH(
                resultado.NumeroInscripcion.ToString() + resultado.Minutos.ToString() + resultado.Segundos.ToString() +
                resultado.Centesimas.ToString() + resultado.Descalificado.ToString() + posicion + resultado.Premio);
        }

        private Resultado13M MapearResultado(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            return new Resultado13M
            {
                IdResultado = Convert.ToInt32(row["IdResultado"]),
                NumeroInscripcion = Convert.ToInt32(row["NumeroInscripcion"]),
                DNINadador = row["DNINadador"].ToString(),
                IdPrueba = Convert.ToInt32(row["IdPrueba"]),
                Minutos = Convert.ToInt32(row["Minutos"]),
                Segundos = Convert.ToInt32(row["Segundos"]),
                Centesimas = Convert.ToInt32(row["Centesimas"]),
                Descalificado = Convert.ToBoolean(row["Descalificado"]),
                Posicion = row["Posicion"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["Posicion"]),
                Premio = row["Premio"].ToString(),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Categoria = row["Categoria"].ToString(),
                Estado = row["Estado"].ToString(),
                DescripcionPrueba = row["Estilo"].ToString() + " " + row["Distancia"].ToString() + "m"
            };
        }
    }
}
