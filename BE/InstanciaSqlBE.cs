using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class InstanciaSqlBE
    {
        public string NombreServidor { get; set; }
        public bool EsLocal { get; set; }

        public override string ToString()
        {
            return NombreServidor;
        }
    }
}
