using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Clase13M
    {
        public int CodigoClase { get; set; }
        public string TipoClase { get; set; }
        public string NivelDificultad { get; set; }
        public string Dias { get; set; }
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFin { get; set; }
        public int EdadMinima { get; set; }
        public int EdadMaxima { get; set; }
        public int CupoMaximo { get; set; }
        public int? LegajoProfesor { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public long DVH { get; set; }

        // solo para mostrar en grilla
        public string NombreProfesor { get; set; }

        // este texto es el que se ve en el combo de asignacion
        public string DescripcionLista
        {
            get { return "Nro. " + CodigoClase + " - " + TipoClase + " - " + Dias; }
        }

        public override string ToString()
        {
            return DescripcionLista;
        }
    }
}
