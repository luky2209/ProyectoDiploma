using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Inscripcion13M
    {
        public int NumeroInscripcion { get; set; }
        public string DNINadador { get; set; }
        public int CodigoTorneo { get; set; }
        public int IdPrueba { get; set; }
        public string Categoria { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public string Estado { get; set; }
        public long DVH { get; set; }

        // estos dos son solo para mostrar en la grilla, no estan guardados asi en la tabla
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DescripcionPrueba { get; set; }
    }
}
