using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class FamiliaModelo55CA : Componente55CA
    {
        private List<Componente55CA> hijos = new List<Componente55CA>();

        public override void agregarHijos(Componente55CA c)
        {
            hijos.Add(c);
        }

        public override void eliminarHijo(Componente55CA c)
        {
            hijos.Remove(c);
        }

        public override List<Componente55CA> obtenerPermisos()
        {
            List<Componente55CA> permisos = new List<Componente55CA>();

            foreach (Componente55CA hijo in hijos)
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
