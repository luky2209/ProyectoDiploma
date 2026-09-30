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
    public class BLLTorneo13M
    {
        DALTorneo13M _dalTorneo = new DALTorneo13M();
        DALPrueba13M _dalPrueba = new DALPrueba13M();
        BLLInscripcion13M _bllInscripcion;
        BLLResultado13M _bllResultado;
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<Torneo13M> obtenerTodos()
        {
            List<Torneo13M> lista = new List<Torneo13M>();

            DataTable dt = _dalTorneo.obtenerTodos();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearTorneo(row));
            }
            return lista;
        }

        // este trae solo los torneos que tienen el estado abierto
        public List<Torneo13M> obtenerAbiertos()
        {
            List<Torneo13M> lista = new List<Torneo13M>();

            DataTable dt = _dalTorneo.obtenerAbiertos();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearTorneo(row));
            }

            return lista;
        }

        // este trae solo los torneo que ya se cerraron y tienen el ranking cargado
        public List<Torneo13M> obtenerFinalizados()
        {
            List<Torneo13M> lista = new List<Torneo13M>();

            DataTable dt = _dalTorneo.obtenerFinalizados();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearTorneo(row));
            }

            return lista;
        }

        // este metodo trae un solo torneo por id
        public Torneo13M obtenerPorCodigo(int codigoTorneo)
        {
            return MapearTorneo(_dalTorneo.obtenerPorCodigo(codigoTorneo));
        }

        public List<Prueba13M> obtenerTodasLasPruebas()
        {
            List<Prueba13M> lista = new List<Prueba13M>();

            DataTable dt = _dalPrueba.obtenerTodas();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearPrueba(row));
            }

            return lista;
        }

        // este metodo devuelve solo los codigos de las pruebas que tiene habilitadas un torneo
        // nada mas hace un join 
        public List<int> obtenerIdsPruebasDelTorneo(int codigoTorneo)
        {
            return obtenerPruebasDelTorneo(codigoTorneo).Select(p => p.IdPrueba).ToList();
        }

        // este metodo trae las pruebas que tiene habilitadas un torneo
        // por este metodo tuve que agregar la tabla extra: TorneoPrueba
        public List<Prueba13M> obtenerPruebasDelTorneo(int codigoTorneo)
        {
            List<Prueba13M> lista = new List<Prueba13M>();

            DataTable dt = _dalPrueba.obtenerPruebasDelTorneo(codigoTorneo);

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearPrueba(row));
            }

            return lista;
        }

        public void CrearTorneo(string nombre, DateTime fecha, string sede, decimal arancel, string categorias, List<int> idsPrueba)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (idsPrueba.Count == 0)
            {
                throw new Exception(idioma.Translate("GestionTorneo19M.msgSinPruebas"));
            }

            long dvh = Services.DigitoVerificador13M.CalcularDVH(
                nombre + fecha.ToString("yyyyMMdd") + sede + arancel + categorias);

            int codigoTorneo = _dalTorneo.InsertarTorneo(nombre, fecha, sede, arancel, categorias, "Abierto", dvh);

            foreach (int idPrueba in idsPrueba)
            {
                long dvhPrueba = Services.DigitoVerificador13M.CalcularDVH(codigoTorneo.ToString() + idPrueba.ToString());

                _dalTorneo.InsertarTorneoPrueba(codigoTorneo, idPrueba, dvhPrueba);
            }

            Services.DigitoVerificador13M.ActualizarDVVTorneo();
            Services.DigitoVerificador13M.ActualizarDVVTorneoPrueba();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se creo el torneo {nombre}.", Criticidad13M.Medio, Modulos13M.Torneo);
        }

        // este metodo cambia los datos de un torneo y deja las pruebas que se tildaron
        public void ModificarTorneo(int codigoTorneo, string nombre, DateTime fecha, string sede, decimal arancel, string categorias, List<int> idsPrueba)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (idsPrueba.Count == 0)
            {
                throw new Exception(idioma.Translate("GestionTorneo19M.msgSinPruebas"));
            }

            _dalTorneo.ModificarTorneo(codigoTorneo, nombre, fecha, sede, arancel, categorias);

            _dalTorneo.EliminarTorneoPruebasDe(codigoTorneo);

            foreach (int idPrueba in idsPrueba)
            {
                long dvhPrueba = Services.DigitoVerificador13M.CalcularDVH(codigoTorneo.ToString() + idPrueba.ToString());

                _dalTorneo.InsertarTorneoPrueba(codigoTorneo, idPrueba, dvhPrueba);
            }

            RecalcularDVHTorneo(codigoTorneo);

            Services.DigitoVerificador13M.ActualizarDVVTorneo();
            Services.DigitoVerificador13M.ActualizarDVVTorneoPrueba();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se modifico el torneo {nombre}.", Criticidad13M.Medio, Modulos13M.Torneo);
        }

        // este metodo cierra un torneo, le pone la fecha de cierre y lo pasa a finalizado
        // el digito verificador del torneo no se recalcula porque su cadena no usa el estado
        public void FinalizarTorneo(int codigoTorneo, DateTime fechaCierre)
        {
            _dalTorneo.FinalizarTorneo(codigoTorneo, fechaCierre);
        }

        // este metodo borra un torneo con todas sus inscripciones y todas sus pruebas
        public void EliminarTorneo(int codigoTorneo)
        {
            Torneo13M torneo = obtenerPorCodigo(codigoTorneo);

            if (_bllInscripcion == null) { _bllInscripcion = new BLLInscripcion13M(); }
            if (_bllResultado == null) { _bllResultado = new BLLResultado13M(); }
            int cantidadInscripciones = _bllInscripcion.ContarInscripcionesDelTorneo(codigoTorneo);

            _bllResultado.EliminarResultadosDelTorneo(codigoTorneo);

            _bllInscripcion.EliminarInscripcionesDelTorneo(codigoTorneo);

            _dalTorneo.EliminarTorneoPruebasDe(codigoTorneo);

            _dalTorneo.EliminarTorneo(codigoTorneo);

            Services.DigitoVerificador13M.ActualizarDVVTorneo();
            Services.DigitoVerificador13M.ActualizarDVVTorneoPrueba();
            Services.DigitoVerificador13M.ActualizarDVVInscripcion();
            Services.DigitoVerificador13M.ActualizarDVVResultado();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se elimino el torneo {torneo.Nombre} con {cantidadInscripciones} inscripcion/es.", Criticidad13M.Alto, Modulos13M.Torneo);
        }

        // este metodo pasa una fila de la base a un objeto de torneo
        private Torneo13M MapearTorneo(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            return new Torneo13M
            {
                CodigoTorneo = Convert.ToInt32(row["CodigoTorneo"]),
                Nombre = row["Nombre"].ToString(),
                Fecha = Convert.ToDateTime(row["Fecha"]),
                Sede = row["Sede"].ToString(),
                Arancel = Convert.ToDecimal(row["Arancel"]),
                Categorias = row["Categorias"].ToString(),
                Estado = row["Estado"].ToString(),
                FechaCierre = row["FechaCierre"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["FechaCierre"]),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"])
            };
        }

        // este metodo pasa una fila de la base a un objeto de prueba
        private Prueba13M MapearPrueba(DataRow row)
        {
            return new Prueba13M
            {
                IdPrueba = Convert.ToInt32(row["IdPrueba"]),
                Estilo = row["Estilo"].ToString(),
                Distancia = Convert.ToInt32(row["Distancia"])
            };
        }

        private void RecalcularDVHTorneo(int codigoTorneo)
        {
            Torneo13M torneo = obtenerPorCodigo(codigoTorneo);

            long nuevoDVH = Services.DigitoVerificador13M.CalcularDVH(
                torneo.Nombre + torneo.Fecha.ToString("yyyyMMdd") + torneo.Sede +
                torneo.Arancel + torneo.Categorias);

            _dalTorneo.ActualizarDVH(codigoTorneo, nuevoDVH);
        }
    }
}
