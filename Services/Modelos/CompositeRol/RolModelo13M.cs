using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public class RolModelo13M
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Componente13M> Permisos { get; set; } = new List<Componente13M>();

        public override string ToString()
        {
            return this.Nombre;
        }
        public List<Componente13M> ObtenerPermisos()
        {
            List<Componente13M> permisos = new List<Componente13M >();

            foreach (Componente13M hijo in this.Permisos)
            {
                permisos.AddRange(hijo.obtenerPermisos());
            }

            return permisos.GroupBy(p => p.Id).Select(grupo => grupo.First()).ToList(); // esto permite que no se le asignen permisos duplicados
        }
    }
}
