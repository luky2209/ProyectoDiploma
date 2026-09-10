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
        public List<RolModelo55CA> obtenerTodos()
        {
            List<RolModelo55CA> lista = new List<RolModelo55CA>();

            DataTable dt = _dal.obtenerTodos();

            foreach(DataRow r in dt.Rows)
            {
                lista.Add(new RolModelo55CA
                {
                    Id = Convert.ToInt32(r["Id"]),
                    Nombre = r["Nombre"].ToString()
                });
            }

            return lista;
        }

        public List<RolModelo55CA> ObtenerRolesConJerarquia()
        {
            List<RolModelo55CA> roles = obtenerTodos();
            var dictRoles = roles.ToDictionary(r => r.Id);

            DataTable dtRelacionRolPatente = _dal.obtenerRelacionesRolPatente();
            DataTable dtRelacionRolFamilia = _dal.obtenerRelacionesRolFamilia();

            var dictFamilias = bllFamilia.ObtenerTodos().ToDictionary(f => f.Id);
            var dictPatentes = bllPatente.obtenerTodos().ToDictionary(p => p.Id);

            EnsamblarPatentesEnRoles(dictRoles, dictPatentes, dtRelacionRolPatente);
            EnsamblarFamiliasEnRoles(dictRoles, dictFamilias, dtRelacionRolFamilia);

            return roles;
        }


        public void crearRol(string nombre, List<Componente55CA> componentes)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            DataTable dt = _dal.obtenerPorNombre(nombre);

            if(dt.Rows.Count > 0)
            {
                throw new Exception(string.Format(idioma.Translate("ExcNombreYaExiste"), idioma.Translate("TablaRol"), nombre));
            }

            RolModelo55CA rol = new RolModelo55CA { Nombre =  nombre };

            foreach (Componente55CA comp in componentes)
            {
                var permisosActuales = rol.ObtenerPermisos();

                if (comp is PermisoModelo55CA patente)
                {
                    if (permisosActuales.Any(p => p.Id == patente.Id))
                    {
                        throw new Exception(string.Format(idioma.Translate("ExcConflictoPatenteIndirecta"), patente.Nombre));
                    }
                }

                else if (comp is FamiliaModelo55CA familia)
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

            long dvhInicial = Services.DigitoVerificador55CA.CalcularDVH(nuevoRolId.ToString() + nombre);
            _dal.ActualizarDVH(nuevoRolId, dvhInicial);
            Services.DigitoVerificador55CA.ActualizarDVVRol();

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;


            foreach (Componente55CA comp in componentes)
            {
                if (comp is PermisoModelo55CA patente)
                {
                    _dal.asignarPatenteARol(patente.Id, nuevoRolId);
                    bllBitacora.registrarEvento(dniAutor, $"Asignó la patente {patente.Nombre} a el rol {nombre}.", Criticidad55CA.Alto, Modulos55CA.Perfil);
                }
                else if (comp is FamiliaModelo55CA familiaHija)
                {
                    _dal.asignarFamiliaARol(familiaHija.Id, nuevoRolId);
                    bllBitacora.registrarEvento(dniAutor, $"Asignó la familia {familiaHija.Nombre} a el rol {nombre}.", Criticidad55CA.Alto, Modulos55CA.Perfil);
                }
            }

            bllBitacora.registrarEvento(dniAutor, $"Creo un nuevo rol", Criticidad55CA.Alto, Modulos55CA.Perfil);

        }

        public void AsignarPatente(RolModelo55CA rol, PermisoModelo55CA patente)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            var permisosAplanados = rol.ObtenerPermisos();

            if (permisosAplanados.Any(p => p.Id == patente.Id))
            {
                throw new Exception(string.Format(idioma.Translate("ExcRolYaContienePermiso"), rol.Nombre, patente.Nombre));
            }

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;
            bllBitacora.registrarEvento(dniAutor, $"Asigno la patente {patente.Nombre} a el rol {rol.Nombre}.", Criticidad55CA.Alto, Modulos55CA.Perfil);

            _dal.asignarPatenteARol(patente.Id, rol.Id);
        }

        public void AsignarFamilia(RolModelo55CA rol, FamiliaModelo55CA familia)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            if (rol.Permisos.Any(c => c.Id == familia.Id && c is FamiliaModelo55CA))
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

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;
            bllBitacora.registrarEvento(dniAutor, $"Asigno la familia {familia.Nombre} a el rol {rol.Nombre}.", Criticidad55CA.Alto, Modulos55CA.Perfil);

            _dal.asignarFamiliaARol(familia.Id, rol.Id);
        }

        public void EliminarRol(int idRol)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            if (_dal.tieneUsuariosAsignados(idRol))
            {
                throw new Exception(idioma.Translate("ExcRolConUsuariosAsignados"));
            }

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;
            bllBitacora.registrarEvento(dniAutor, $"Elimino un rol.", Criticidad55CA.Alto, Modulos55CA.Perfil);

            _dal.eliminarRol(idRol);
            Services.DigitoVerificador55CA.ActualizarDVVRol();

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

        private void EnsamblarPatentesEnRoles(Dictionary<int, RolModelo55CA> dictRoles, Dictionary<int, PermisoModelo55CA> dictPatentes, DataTable dtRelaciones)
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

        private void EnsamblarFamiliasEnRoles(Dictionary<int, RolModelo55CA> dictRoles, Dictionary<int, FamiliaModelo55CA> dictFamilias, DataTable dtRelaciones)
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
