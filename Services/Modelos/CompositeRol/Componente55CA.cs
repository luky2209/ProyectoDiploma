using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Modelos
{
    public abstract class Componente55CA
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        
        public virtual void agregarHijos(Componente55CA c)
        {
            throw new NotImplementedException();
        }
        public virtual void eliminarHijo(Componente55CA c)
        {
            throw new NotImplementedException();
        }
        public virtual List<Componente55CA> obtenerPermisos()
        {
            throw new NotImplementedException();
        }
    }
}
