using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class PermisoModelo55CA : Componente55CA
    {
        public override List<Componente55CA> obtenerPermisos()
        {
            return new List<Componente55CA> { this }; // se devuelve a si mismo
        }

        public override string ToString()
        {
            return $"Patente {this.Nombre}";
        }
    }
}
