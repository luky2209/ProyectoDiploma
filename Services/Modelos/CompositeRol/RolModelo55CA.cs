using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class RolModelo55CA
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Componente55CA> Permisos { get; set; } = new List<Componente55CA>();

        public override string ToString()
        {
            return this.Nombre;
        }
        public List<Componente55CA> ObtenerPermisos()
        {
            List<Componente55CA> permisos = new List<Componente55CA >();

            foreach (Componente55CA hijo in this.Permisos)
            {
                permisos.AddRange(hijo.obtenerPermisos());
            }

            return permisos.GroupBy(p => p.Id).Select(grupo => grupo.First()).ToList(); // esto permite que no se le asignen permisos duplicados
        }
    }
}
