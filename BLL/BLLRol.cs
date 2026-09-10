using BE;
using BE.Enum;
using DAL;
using Services.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLLRol
    {
        DALRol _dal = new DALRol();
        BitacoraEventosService bllBitacora = new BitacoraEventosService();
        BLLFamilia bllFamilia = new BLLFamilia();
        BLLPatente bllPatente = new BLLPatente();
        public List<RolModelo13M> obtenerTodos()
        {
            List<RolModelo13M> lista = new List<RolModelo13M>();

            DataTable dt = _dal.obtenerTodos();

            foreach(DataRow r in dt.Rows)
            {
                lista.Add(new RolModelo13M
                {
                    Id = Convert.ToInt32(r["Id"]),
                    Nombre = r["Nombre"].ToString()
                });
            }

            return lista;
        }

        public List<RolModelo13M> ObtenerRolesConJerarquia()
        {
            List<RolModelo13M> roles = obtenerTodos();
            var dictRoles = roles.ToDictionary(r => r.Id);

            DataTable dtRelacionRolPatente = _dal.obtenerRelacionesRolPatente();
            DataTable dtRelacionRolFamilia = _dal.obtenerRelacionesRolFamilia();

            var dictFamilias = bllFamilia.ObtenerTodos().ToDictionary(f => f.Id);
            var dictPatentes = bllPatente.obtenerTodos().ToDictionary(p => p.Id);

            EnsamblarPatentesEnRoles(dictRoles, dictPatentes, dtRelacionRolPatente);
            EnsamblarFamiliasEnRoles(dictRoles, dictFamilias, dtRelacionRolFamilia);

            return roles;
        }


        public void crearRol(string nombre, List<Componente13M> componentes)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            DataTable dt = _dal.obtenerPorNombre(nombre);

            if(dt.Rows.Count > 0)
            {
                throw new Exception(string.Format(idioma.Translate("ExcNombreYaExiste"), idioma.Translate("TablaRol"), nombre));
            }

            RolModelo13M rol = new RolModelo13M { Nombre =  nombre };

            foreach (Componente13M comp in componentes)
            {
                var permisosActuales = rol.ObtenerPermisos();

                if (comp is PermisoModelo13M patente)
                {
                    if (permisosActuales.Any(p => p.Id == patente.Id))
                    {
                        throw new Exception(string.Format(idioma.Translate("ExcConflictoPatenteIndirecta"), patente.Nombre));
                    }
                }

                else if (comp is FamiliaModelo13M familia)
                {
                    var permisosHija = familia.obtenerPermisos();

                    foreach (var p in permisosHija)
                    {
                        if (permisosActuales.Any(pa => pa.Id == p.Id))
                        {
                            throw new Exception(string.Format(idioma.Translate("ExcConflictoFamiliaPermisos"), familia.Nombre));
                        }
                    }
                }

                rol.Permisos.Add(comp);
            }

            int nuevoRolId = _dal.insertarRol(nombre);

            long dvhInicial = Services.DigitoVerificador13M.CalcularDVH(nuevoRolId.ToString() + nombre);
            _dal.ActualizarDVH(nuevoRolId, dvhInicial);
            Services.DigitoVerificador13M.ActualizarDVVRol();

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;


            foreach (Componente13M comp in componentes)
            {
                if (comp is PermisoModelo13M patente)
                {
                    _dal.asignarPatenteARol(patente.Id, nuevoRolId);
                    bllBitacora.registrarEvento(dniAutor, $"Asignó la patente {patente.Nombre} a el rol {nombre}.", Criticidad13M.Alto, Modulos13M.Perfil);
                }
                else if (comp is FamiliaModelo13M familiaHija)
                {
                    _dal.asignarFamiliaARol(familiaHija.Id, nuevoRolId);
                    bllBitacora.registrarEvento(dniAutor, $"Asignó la familia {familiaHija.Nombre} a el rol {nombre}.", Criticidad13M.Alto, Modulos13M.Perfil);
                }
            }

            bllBitacora.registrarEvento(dniAutor, $"Creo un nuevo rol", Criticidad13M.Alto, Modulos13M.Perfil);

        }

        public void AsignarPatente(RolModelo13M rol, PermisoModelo13M patente)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            var permisosAplanados = rol.ObtenerPermisos();

            if (permisosAplanados.Any(p => p.Id == patente.Id))
            {
                throw new Exception(string.Format(idioma.Translate("ExcRolYaContienePermiso"), rol.Nombre, patente.Nombre));
            }

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;
            bllBitacora.registrarEvento(dniAutor, $"Asigno la patente {patente.Nombre} a el rol {rol.Nombre}.", Criticidad13M.Alto, Modulos13M.Perfil);

            _dal.asignarPatenteARol(patente.Id, rol.Id);
        }

        public void AsignarFamilia(RolModelo13M rol, FamiliaModelo13M familia)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (rol.Permisos.Any(c => c.Id == familia.Id && c is FamiliaModelo13M))
            {
                throw new Exception(string.Format(idioma.Translate("ExcRolYaTieneFamilia"), rol.Nombre, familia.Nombre));
            }

            var permisosDelRol = rol.ObtenerPermisos();
            var permisosDeLaFamilia = familia.obtenerPermisos();

            foreach (var permisoAportado in permisosDeLaFamilia)
            {
                if (permisosDelRol.Any(p => p.Id == permisoAportado.Id))
                {
                    throw new Exception(string.Format(idioma.Translate("ExcFamiliaPermisoYaPoseido"), permisoAportado.Nombre));
                }
            }

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;
            bllBitacora.registrarEvento(dniAutor, $"Asigno la familia {familia.Nombre} a el rol {rol.Nombre}.", Criticidad13M.Alto, Modulos13M.Perfil);

            _dal.asignarFamiliaARol(familia.Id, rol.Id);
        }

        public void EliminarRol(int idRol)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            if (_dal.tieneUsuariosAsignados(idRol))
            {
                throw new Exception(idioma.Translate("ExcRolConUsuariosAsignados"));
            }

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;
            bllBitacora.registrarEvento(dniAutor, $"Elimino un rol.", Criticidad13M.Alto, Modulos13M.Perfil);

            _dal.eliminarRol(idRol);
            Services.DigitoVerificador13M.ActualizarDVVRol();

        }

        public void DesasignarPatente(int idRol, int idPatente)
        {
            _dal.quitarPatenteDeRol(idRol, idPatente);
        }

        public void DesasignarFamilia(int idRol, int idFamilia)
        {
            _dal.quitarFamiliaDeRol(idRol, idFamilia);
        }

        

        

        

        #region Métodos de Ensamblaje

        private void EnsamblarPatentesEnRoles(Dictionary<int, RolModelo13M> dictRoles, Dictionary<int, PermisoModelo13M> dictPatentes, DataTable dtRelaciones)
        {
            foreach (DataRow row in dtRelaciones.Rows)
            {
                int idRol = Convert.ToInt32(row["IdRol"]);
                int idPatente = Convert.ToInt32(row["IdPatente"]);

                // si existen tanto el rol como la patente en nuestros diccionarios
                if (dictRoles.ContainsKey(idRol) && dictPatentes.ContainsKey(idPatente))
                {
                    dictRoles[idRol].Permisos.Add(dictPatentes[idPatente]);
                }
            }
        }

        private void EnsamblarFamiliasEnRoles(Dictionary<int, RolModelo13M> dictRoles, Dictionary<int, FamiliaModelo13M> dictFamilias, DataTable dtRelaciones)
        {
            foreach (DataRow row in dtRelaciones.Rows)
            {
                int idRol = Convert.ToInt32(row["IdRol"]);
                int idFamilia = Convert.ToInt32(row["IdFamilia"]);

                // si existen tanto el rol como la familia en nuestros diccionarios
                if (dictRoles.ContainsKey(idRol) && dictFamilias.ContainsKey(idFamilia))
                {
                    dictRoles[idRol].Permisos.Add(dictFamilias[idFamilia]);
                }
            }
        }

        #endregion
    }
}
