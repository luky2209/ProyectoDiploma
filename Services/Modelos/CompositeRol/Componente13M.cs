using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public abstract class Componente13M
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public virtual void agregarHijos(Componente13M c)
        {
            throw new NotImplementedException();
        }
        public virtual void eliminarHijo(Componente13M c)
        {
            throw new NotImplementedException();
        }
        public virtual List<Componente13M> obtenerPermisos()
        {
            throw new NotImplementedException();
        }
    }
}
