using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Asistencia13M
    {
        public int NumeroAsistencia { get; set; }
        public int CodigoClase { get; set; }
        public DateTime Fecha { get; set; }
        public string DNINadador { get; set; }
        public string Condicion { get; set; }
        public long DVH { get; set; }

        // solo para mostrar en grilla
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoClase { get; set; }
    }
}