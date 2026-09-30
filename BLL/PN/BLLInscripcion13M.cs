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
    public class BLLInscripcion13M
    {
        DALInscripcion13M _dalInscripcion = new DALInscripcion13M();
        BLLNadador13M _bllNadador = new BLLNadador13M();
        BLLTorneo13M _bllTorneo;
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<Inscripcion13M> obtenerTodas()
        {
            List<Inscripcion13M> lista = new List<Inscripcion13M>();

            DataTable dt = _dalInscripcion.obtenerTodas();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearInscripcion(row));
            }

            return lista;
        }

        // este trae solo las inscripciones de un torneo
        public List<Inscripcion13M> obtenerPorTorneo(int codigoTorneo)
        {
            return obtenerTodas().Where(i => i.CodigoTorneo == codigoTorneo).ToList();
        }

        // este metodo busca una inscripcion por su numero
        private Inscripcion13M obtenerPorNumero(int numeroInscripcion)
        {
            return MapearInscripcion(_dalInscripcion.obtenerPorNumero(numeroInscripcion));
        }

        // este metodo cuenta las inscripciones que tiene un torneo
        public int ContarInscripcionesDelTorneo(int codigoTorneo)
        {
            return _dalInscripcion.ContarInscripcionesDelTorneo(codigoTorneo);
        }

        // este metodo borra todas las inscripciones de un torneo
        public void EliminarInscripcionesDelTorneo(int codigoTorneo)
        {
            _dalInscripcion.EliminarInscripcionesDelTorneo(codigoTorneo);
        }

        // este metodo inscribe a un nadador del padron en un torneo
        public void CrearInscripcion(string dni, int codigoTorneo, int idPrueba)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            Nadador13M nadador = _bllNadador.obtenerPorDNI(dni);

            if (nadador == null)
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgNadadorNoEncontrado"));
            }

            if (_bllTorneo == null) { _bllTorneo = new BLLTorneo13M(); }
            Torneo13M torneo = _bllTorneo.obtenerPorCodigo(codigoTorneo);

            if (torneo == null)
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgTorneoNoEncontrado"));
            }

            if (torneo.Estado != "Abierto" || torneo.Fecha.Date < DateTime.Today)
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgTorneoNoAbierto"));
            }

            if (!LaPruebaEsDelTorneo(codigoTorneo, idPrueba))
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgPruebaNoHabilitada"));
            }

            if (!LaCategoriaEstaHabilitada(torneo.Categorias, nadador.Categoria))
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgCategoriaNoHabilitada"));
            }

            if (_dalInscripcion.ExisteInscripcion(dni, codigoTorneo, idPrueba, 0))
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgNadadorYaInscripto"));
            }

            long dvh = Services.DigitoVerificador13M.CalcularDVH(
                dni + codigoTorneo + idPrueba + nadador.Categoria + DateTime.Today.ToString("yyyyMMdd") + "Pendiente de Pago");

            _dalInscripcion.InsertarInscripcion(dni, codigoTorneo, idPrueba, nadador.Categoria, DateTime.Today, "Pendiente de Pago", dvh);

            Services.DigitoVerificador13M.ActualizarDVVInscripcion();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se inscribio al nadador DNI {dni} en el torneo {torneo.Nombre}.", Criticidad13M.Medio, Modulos13M.Inscripcion);
        }

        // este metodo cambia la prueba o el estado de pago de una inscripcion
        public void ModificarInscripcion(int numeroInscripcion, int idPrueba, string estado)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            Inscripcion13M inscripcion = obtenerPorNumero(numeroInscripcion);

            if (inscripcion == null)
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgInscripcionNoEncontrada"));
            }

            if (_bllTorneo == null) { _bllTorneo = new BLLTorneo13M(); }
            Torneo13M torneo = _bllTorneo.obtenerPorCodigo(inscripcion.CodigoTorneo);

            if (torneo.Estado != "Abierto" || torneo.Fecha.Date < DateTime.Today)
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgTorneoNoAbierto"));
            }

            if (!LaPruebaEsDelTorneo(inscripcion.CodigoTorneo, idPrueba))
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgPruebaNoHabilitada"));
            }

            if (_dalInscripcion.ExisteInscripcion(inscripcion.DNINadador, inscripcion.CodigoTorneo, idPrueba, numeroInscripcion))
            {
                throw new Exception(idioma.Translate("InscripcionTorneo19M.msgNadadorYaInscripto"));
            }

            _dalInscripcion.ModificarInscripcion(numeroInscripcion, idPrueba, inscripcion.Categoria, estado);

            RecalcularDVHInscripcion(numeroInscripcion);

            Services.DigitoVerificador13M.ActualizarDVVInscripcion();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se modifico la inscripcion Nro. {numeroInscripcion} del nadador DNI {inscripcion.DNINadador}.", Criticidad13M.Medio, Modulos13M.Inscripcion);
        }

        // este metodo cobra el arancel de un nadador: marca como pagadas todas sus inscripciones del torneo
        public void CobrarInscripcionesDelNadador(string dni, int codigoTorneo)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            List<Inscripcion13M> inscripciones = obtenerTodas()
                .Where(i => i.CodigoTorneo == codigoTorneo && i.DNINadador == dni)
                .ToList();

            if (inscripciones.Count == 0)
            {
                throw new Exception(idioma.Translate("ConsultarResultados19M.msgSeleccionarNadador"));
            }

            if (inscripciones.All(i => i.Estado == "Pagado"))
            {
                throw new Exception(idioma.Translate("ConsultarResultados19M.msgYaPago"));
            }

            _dalInscripcion.ActualizarEstadoInscripciones(dni, codigoTorneo, "Pagado");

            foreach (Inscripcion13M inscripcion in inscripciones)
            {
                RecalcularDVHInscripcion(inscripcion.NumeroInscripcion);
            }

            Services.DigitoVerificador13M.ActualizarDVVInscripcion();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se cobro el arancel del nadador DNI {dni} en el torneo Nro. {codigoTorneo}.", Criticidad13M.Alto, Modulos13M.Inscripcion);
        }

        // este metodo borra una inscripcion, el nadador del padron no se toca
        public void EliminarInscripcion(int numeroInscripcion)
        {
            Inscripcion13M inscripcion = obtenerPorNumero(numeroInscripcion);

            _dalInscripcion.EliminarInscripcion(numeroInscripcion);

            Services.DigitoVerificador13M.ActualizarDVVInscripcion();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se elimino la inscripcion Nro. {numeroInscripcion} del nadador DNI {inscripcion.DNINadador}.", Criticidad13M.Alto, Modulos13M.Inscripcion);
        }

        // este metodo pasa una fila de la base a un objeto de inscripcion
        private Inscripcion13M MapearInscripcion(DataRow row)
        {
            if (row == null)
            {
                return null;
            }

            return new Inscripcion13M
            {
                NumeroInscripcion = Convert.ToInt32(row["NumeroInscripcion"]),
                DNINadador = row["DNINadador"].ToString(),
                CodigoTorneo = Convert.ToInt32(row["CodigoTorneo"]),
                IdPrueba = Convert.ToInt32(row["IdPrueba"]),
                Categoria = row["Categoria"].ToString(),
                FechaInscripcion = Convert.ToDateTime(row["FechaInscripcion"]),
                Estado = row["Estado"].ToString(),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                DescripcionPrueba = row["Estilo"].ToString() + " " + row["Distancia"].ToString() + "m"
            };
        }

        // este metodo revisa si una prueba estaba habilitada en ese torneo
        private bool LaPruebaEsDelTorneo(int codigoTorneo, int idPrueba)
        {
            if (_bllTorneo == null) { _bllTorneo = new BLLTorneo13M(); }
            return _bllTorneo.obtenerPruebasDelTorneo(codigoTorneo).Exists(p => p.IdPrueba == idPrueba);
        }

        // este metodo revisa si la categoria del nadador esta entre las del torneo
        private bool LaCategoriaEstaHabilitada(string categorias, string categoriaNadador)
        {
            return categorias.Split(',').Any(c => c.Trim().Equals(categoriaNadador, StringComparison.OrdinalIgnoreCase));
        }

        // este metodo rehace el digito verificador de una inscripcion
        private void RecalcularDVHInscripcion(int numeroInscripcion)
        {
            Inscripcion13M inscripcion = obtenerPorNumero(numeroInscripcion);

            long nuevoDVH = Services.DigitoVerificador13M.CalcularDVH(
                inscripcion.DNINadador + inscripcion.CodigoTorneo + inscripcion.IdPrueba +
                inscripcion.Categoria + inscripcion.FechaInscripcion.ToString("yyyyMMdd") + inscripcion.Estado);

            _dalInscripcion.ActualizarDVH(numeroInscripcion, nuevoDVH);
        }
    }
}
