using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Torneo13M
    {
        public int CodigoTorneo { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Sede { get; set; }
        public decimal Arancel { get; set; }
        public string Categorias { get; set; }
        public string Estado { get; set; }

        // esta queda vacia mientras el torneo esta abierto, se llena cuando el entrenador lo cierra,
        // por eso lo tengo que dejar con ? 
        public DateTime? FechaCierre { get; set; }

        public long DVH { get; set; }

        // este texto es el que se ve en el combo
        public string DescripcionLista
        {
            get { return Nombre; }
        }
        public override string ToString()
        {
            return DescripcionLista;
        }
    }
}
