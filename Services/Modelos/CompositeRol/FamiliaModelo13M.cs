using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class FamiliaModelo13M : Componente13M
    {
        private List<Componente13M> hijos = new List<Componente13M>();

        public override void agregarHijos(Componente13M c)
        {
            hijos.Add(c);
        }

        public override void eliminarHijo(Componente13M c)
        {
            hijos.Remove(c);
        }

        public override List<Componente13M> obtenerPermisos()
        {
            List<Componente13M> permisos = new List<Componente13M>();

            foreach (Componente13M hijo in hijos)
            {
                permisos.AddRange(hijo.obtenerPermisos());
            }

            return permisos;
        }

        public override string ToString()
        {
            return $"Familia {this.Nombre}";
        }
    }
}
