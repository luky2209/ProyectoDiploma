using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class PermisoModelo13M : Componente13M
    {
        public override List<Componente13M> obtenerPermisos()
        {
            return new List<Componente13M> { this }; // se devuelve a si mismo
        }

        public override string ToString()
        {
            return $"Patente {this.Nombre}";
        }
    }
}
