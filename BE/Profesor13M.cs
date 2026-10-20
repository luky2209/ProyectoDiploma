using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Profesor13M
    {
        public int LegajoProfesor { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string FranjaHorariaDisponible { get; set; }
        public long DVH { get; set; }

        // este texto se ve en el combo de seleccion de profesor
        public string DescripcionLista
        {
            get { return LegajoProfesor + " - " + Nombre; }
        }

        public override string ToString()
        {
            return DescripcionLista;
        }
    }
}