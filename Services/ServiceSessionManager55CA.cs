using BE;
using Services;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_55CA
{
    public sealed class ServiceSessionManager55CA
    {
        private ServiceSessionManager55CA() 
        {
            Idioma = new IdiomaManager();
        }

        private static ServiceSessionManager55CA _instancia;

        public UsuarioModelo55CA usuarioActivo { get; private set; }

        public static ServiceSessionManager55CA getIntancia()
        {
            if( _instancia == null)
            {
                _instancia = new ServiceSessionManager55CA();
            }

            return _instancia;
        }

        public void Login(UsuarioModelo55CA usuario)
        {
            usuarioActivo = usuario;
        }

        public void Logout()
        {
            usuarioActivo = null;
        }

        public bool estaLogueado()
        {
            return usuarioActivo != null;
        }


        public bool TienePermiso(string nombrePermiso)
        {
            List<Componente55CA> todosLosPermisos = usuarioActivo.Rol.ObtenerPermisos();

            //recorremos la lista buscando coincidencia por el nombre de la patente
            foreach (Componente55CA componente in todosLosPermisos)
            {
                if (string.Equals(componente.Nombre, nombrePermiso, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }


        public IdiomaManager Idioma { get; private set; }

    }
}
