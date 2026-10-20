using BE;
using BE.Enum;
using DAL;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLClase13M
    {
        DALClase13M _dal = new DALClase13M();
        BitacoraEventosService _bit = new BitacoraEventosService();

        public List<Clase13M> obtenerTodos()
        {
            List<Clase13M> lista = new List<Clase13M>();

            DataTable dt = _dal.obtenerTodos();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearClase(row));
            }
            return lista;
        }

        public void CrearClase(string tipoClase, string nivelDificultad, string dias, TimeSpan horarioInicio, TimeSpan horarioFin, int edadMinima, int edadMaxima, int cupoMaximo)
        {
            ValidarClase(horarioInicio, horarioFin, edadMinima, edadMaxima, cupoMaximo);

            long dvh = CalcularDVHClase(tipoClase, nivelDificultad, dias, horarioInicio, horarioFin, edadMinima, edadMaxima, cupoMaximo, null, null);

            int codigoClase = _dal.InsertarClase(tipoClase, nivelDificultad, dias, horarioInicio, horarioFin, edadMinima, edadMaxima, cupoMaximo, dvh);

            Services.DigitoVerificador13M.ActualizarDVVClase();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se registro la clase Nro. {codigoClase}.", Criticidad13M.Medio, Modulos13M.Clase);
        }

        public void ModificarClase(int codigoClase, string tipoClase, string nivelDificultad, string dias, TimeSpan horarioInicio, TimeSpan horarioFin, int edadMinima, int edadMaxima, int cupoMaximo)
        {
            ValidarClase(horarioInicio, horarioFin, edadMinima, edadMaxima, cupoMaximo);

            _dal.ModificarClase(codigoClase, tipoClase, nivelDificultad, dias, horarioInicio, horarioFin, edadMinima, edadMaxima, cupoMaximo);

            DataRow row = _dal.obtenerPorCodigo(codigoClase);
            int? legajoProf = (row != null && row["LegajoProfesor"] != DBNull.Value) ? Convert.ToInt32(row["LegajoProfesor"]) : (int?)null;
            DateTime? fechaAsig = (row != null && row["FechaAsignacion"] != DBNull.Value) ? Convert.ToDateTime(row["FechaAsignacion"]) : (DateTime?)null;

            long dvh = CalcularDVHClase(tipoClase, nivelDificultad, dias, horarioInicio, horarioFin, edadMinima, edadMaxima, cupoMaximo, legajoProf, fechaAsig);

            _dal.ActualizarDVH(codigoClase, dvh);

            Services.DigitoVerificador13M.ActualizarDVVClase();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se modifico la clase Nro. {codigoClase}.", Criticidad13M.Medio, Modulos13M.Clase);
        }

        public void EliminarClase(int codigoClase)
        {
            _dal.EliminarClase(codigoClase);

            Services.DigitoVerificador13M.ActualizarDVVClase();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se elimino la clase Nro. {codigoClase}.", Criticidad13M.Medio, Modulos13M.Clase);
        }

        public Clase13M obtenerPorCodigo(int codigoClase)
        {
            DataRow row = _dal.obtenerPorCodigo(codigoClase);
            if (row == null)
            {
                return null;
            }
            return MapearClase(row);
        }

        private Clase13M MapearClase(DataRow row)
        {
            return new Clase13M
            {
                CodigoClase = Convert.ToInt32(row["CodigoClase"]),
                TipoClase = row["TipoClase"].ToString(),
                NivelDificultad = row["NivelDificultad"].ToString(),
                Dias = row["Dias"].ToString(),
                HorarioInicio = (TimeSpan)row["HorarioInicio"],
                HorarioFin = (TimeSpan)row["HorarioFin"],
                EdadMinima = Convert.ToInt32(row["EdadMinima"]),
                EdadMaxima = Convert.ToInt32(row["EdadMaxima"]),
                CupoMaximo = Convert.ToInt32(row["CupoMaximo"]),
                LegajoProfesor = row["LegajoProfesor"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["LegajoProfesor"]),
                FechaAsignacion = row["FechaAsignacion"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["FechaAsignacion"]),
                DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]),
                NombreProfesor = row.Table.Columns.Contains("NombreProfesor") && row["NombreProfesor"] != DBNull.Value ? row["NombreProfesor"].ToString() : ""
            };
        }

        // cheatea que el horario, el rango etario y el cupo tengan sentido antes de guardar
        private void ValidarClase(TimeSpan horarioInicio, TimeSpan horarioFin, int edadMinima, int edadMaxima, int cupoMaximo)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (horarioFin <= horarioInicio)
            {
                throw new Exception(idioma.Translate("RegistrarClase19M.msgHorarioInvalido"));
            }

            if (edadMinima > edadMaxima || edadMinima < 1 || edadMaxima > 120)
            {
                throw new Exception(idioma.Translate("RegistrarClase19M.msgRangoEtarioInvalido"));
            }

            if (cupoMaximo <= 0)
            {
                throw new Exception(idioma.Translate("RegistrarClase19M.msgCupoInvalido"));
            }
        }

        // este metodo asigna un profesor responsable a una clase
        // devuelve false cuando el profesor ya estaba asignado a esa clase
        public bool AsignarProfesorAClase(int codigoClase, int legajoProfesor)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            BLLProfesor13M bllProfesor = new BLLProfesor13M();
            Clase13M clase = obtenerPorCodigo(codigoClase);
            Profesor13M profesor = bllProfesor.obtenerPorLegajo(legajoProfesor);

            if (clase == null)
            {
                throw new Exception(idioma.Translate("AsignarProfesorClase19M.msgClaseNoEncontrada"));
            }

            if (profesor == null)
            {
                throw new Exception(idioma.Translate("AsignarProfesorClase19M.msgProfesorNoEncontrado"));
            }

            if (clase.LegajoProfesor == legajoProfesor)
            {
                return false;
            }

            ValidarDisponibilidadProfesor(profesor, clase);

            // se guarda sin segundos para que el digito calculado aca coincida con el que despues se lee de la base
            DateTime ahora = DateTime.Now;
            DateTime fechaAsignacion = new DateTime(ahora.Year, ahora.Month, ahora.Day, ahora.Hour, ahora.Minute, 0);

            _dal.AsignarProfesorClase(codigoClase, legajoProfesor, fechaAsignacion);

            long dvh = CalcularDVHClase(clase.TipoClase, clase.NivelDificultad, clase.Dias, clase.HorarioInicio, clase.HorarioFin, clase.EdadMinima, clase.EdadMaxima, clase.CupoMaximo, legajoProfesor, fechaAsignacion);

            _dal.ActualizarDVH(codigoClase, dvh);

            Services.DigitoVerificador13M.ActualizarDVVClase();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            _bit.registrarEvento(dniAutor, $"Se asigno el profesor Legajo {legajoProfesor} a la clase Nro. {codigoClase}.", Criticidad13M.Medio, Modulos13M.Clase);

            return true;
        }

        // revisa que el profesor este disponible: que su franja cubra el horario y que no se superponga con otra clase que ya tenga
        private void ValidarDisponibilidadProfesor(Profesor13M profesor, Clase13M clase)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (!LaFranjaCubreElHorario(profesor.FranjaHorariaDisponible, clase.HorarioInicio, clase.HorarioFin))
            {
                throw new Exception(idioma.Translate("AsignarProfesorClase19M.msgProfesorNoDisponible"));
            }

            foreach (Clase13M otra in obtenerTodos())
            {
                if (otra.LegajoProfesor == profesor.LegajoProfesor && otra.CodigoClase != clase.CodigoClase
                    && SeSuperponen(otra.Dias, otra.HorarioInicio, otra.HorarioFin, clase.Dias, clase.HorarioInicio, clase.HorarioFin))
                {
                    throw new Exception(idioma.Translate("AsignarProfesorClase19M.msgProfesorNoDisponible"));
                }
            }
        }

        // revisa que dos clases coincidan en algun dia y que sus horarios se pisen
        private bool SeSuperponen(string dias1, TimeSpan inicio1, TimeSpan fin1, string dias2, TimeSpan inicio2, TimeSpan fin2)
        {
            if (inicio1 >= fin2 || fin1 <= inicio2)
            {
                return false;
            }

            foreach (string dia1 in (dias1 ?? "").Split(','))
            {
                foreach (string dia2 in (dias2 ?? "").Split(','))
                {
                    if (dia1.Trim() != "" && dia1.Trim() == dia2.Trim())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // revisa que la franja del profesor (ej "08:00-10:00") contenga el horario de la clase
        private bool LaFranjaCubreElHorario(string franja, TimeSpan horarioInicio, TimeSpan horarioFin)
        {
            if (string.IsNullOrEmpty(franja))
            {
                return false;
            }

            string[] partes = franja.Replace(" ", "").Split('-');

            if (partes.Length != 2)
            {
                return false;
            }

            TimeSpan inicioFranja;
            TimeSpan finFranja;

            if (!TimeSpan.TryParse(partes[0], out inicioFranja) || !TimeSpan.TryParse(partes[1], out finFranja))
            {
                return false;
            }

            return horarioInicio >= inicioFranja && horarioFin <= finFranja;
        }

        // arma la cadena con los datos de la clase para calcular su digito verificador
        private long CalcularDVHClase(string tipoClase, string nivelDificultad, string dias, TimeSpan horarioInicio, TimeSpan horarioFin, int edadMinima, int edadMaxima, int cupoMaximo, int? legajoProfesor, DateTime? fechaAsignacion)
        {
            // cuando la clase todavia no tiene profesor asignado no se agrega nada, asi el digito de las clases viejas no cambia
            string legajo = legajoProfesor.HasValue ? legajoProfesor.Value.ToString() : "";
            string fecha = fechaAsignacion.HasValue ? fechaAsignacion.Value.ToString("yyyyMMddHHmm") : "";

            string cadena = tipoClase + nivelDificultad + dias
                + horarioInicio.ToString(@"hh\:mm") + horarioFin.ToString(@"hh\:mm")
                + edadMinima + edadMaxima + cupoMaximo + legajo + fecha;

            return Services.DigitoVerificador13M.CalcularDVH(cadena);
        }
    }
}
