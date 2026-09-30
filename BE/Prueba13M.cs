using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Prueba13M
    {
        public int IdPrueba { get; set; }
        public string Estilo { get; set; }
        public int Distancia { get; set; }
        public long DVH { get; set; }

        // este texto es el que se ve en el combo
        public string Descripcion
        {
            get { return Estilo + " " + Distancia + "m"; }
        }
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
