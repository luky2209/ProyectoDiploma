using BE;
using Services;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_13M
{
    public sealed class ServiceSessionManager13M
    {
        private ServiceSessionManager13M() 
        {
            Idioma = new IdiomaManager();
        }

        private static ServiceSessionManager13M _instancia;

        public UsuarioModelo13M usuarioActivo { get; private set; }

        public static ServiceSessionManager13M getIntancia()
        {
            if( _instancia == null)
            {
                _instancia = new ServiceSessionManager13M();
            }

            return _instancia;
        }

        public void Login(UsuarioModelo13M usuario)
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
            List<Componente13M> todosLosPermisos = usuarioActivo.Rol.ObtenerPermisos();

            //recorremos la lista buscando coincidencia por el nombre de la patente
            foreach (Componente13M componente in todosLosPermisos)
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
