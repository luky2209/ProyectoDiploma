using BE;
using BE.Enum;
using DAL;
using Services;
using Services.Modelos;
//using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UsuarioService
    {
        DALUsuario13M dal = new DALUsuario13M();
        BitacoraEventosService bit = new BitacoraEventosService();

        public List<UsuarioModelo13M> obtenerTodos()
        {
            var dt = dal.obtenerTodos();
            List<UsuarioModelo13M> lista = new List<UsuarioModelo13M>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearUsuario(row));
            }

            return lista;
        }

        public bool login(string user, string password)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            var usuario = MapearUsuario(dal.obtenerPorUser(user));

            //validaciones
            if (Services_13M.ServiceSessionManager13M.getIntancia().estaLogueado())
            {
                throw new Exception(idioma.Translate("ExcSesionActiva"));
            }

            if (usuario == null)
            {
                throw new Exception(idioma.Translate("ExcUsuarioNoExiste"));
            }

            if (usuario.Bloqueo == true)
            {
                throw new Exception(idioma.Translate("ExcUsuarioBloqueado"));
            }

            if (usuario.Activo == false)
            {
                throw new Exception(idioma.Translate("ExcUsuarioInactivo"));
            }

            if (usuario.UltimoIntentoFallido.HasValue)
            {
                TimeSpan tiempo = DateTime.Now - usuario.UltimoIntentoFallido.Value;

                // si pasaron más de 30 minutos se reinicia
                if (tiempo.TotalMinutes >= 30)
                {
                    dal.reiniciarIntentos(usuario.DNI);

                    usuario.Intentos = 0;
                }
            }

            string passwordHash = Services_13M.ServiceSeguridad13M.Hashear(password);

            if (usuario.Password != passwordHash)
            {
                dal.aumentarIntento(usuario.DNI);

                usuario.Intentos++;

                if (usuario.Intentos >= 4)
                {
                    dal.bloquearUsuario(usuario.DNI);
                    bit.registrarEvento(usuario.DNI, $"Usuario {usuario.User} bloqueado.", Criticidad13M.Alto, Modulos13M.Seguridad);

                    throw new Exception(idioma.Translate("ExcCuentaBloqueada"));
                }

                throw new Exception(string.Format(idioma.Translate("ExcPasswordIncorrecta"), usuario.Intentos));
            }
            BLLRol gestorRol = new BLLRol();

            List<RolModelo13M> roles = gestorRol.ObtenerRolesConJerarquia();

            var rolConPermisos = roles.FirstOrDefault(r => r.Id == usuario.Rol.Id);

            if(rolConPermisos != null)
            {
                usuario.Rol = rolConPermisos;
            }

            //login ok
            Services_13M.ServiceSessionManager13M.getIntancia().Login(usuario);

            bit.registrarEvento(usuario.DNI, $"Realizo login exitoso.", Criticidad13M.Medio, Modulos13M.Usuario);

            dal.reiniciarIntentos(usuario.DNI);

            // verificamos si sigue usando password por defecto
            string passwordDefault = GenerarPassword(usuario.Apellido, usuario.DNI);

            string passwordDefaultHash = Services_13M.ServiceSeguridad13M.Hashear(passwordDefault);

            bool usaPasswordDefault =
                usuario.Password == passwordDefaultHash;

            return usaPasswordDefault;
        }

        public void CrearUsuario(string dni, string nombre, string apellido, string email, RolModelo13M rol)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;


            if (dal.obtenerPorDNI(dni) != null)
            {
                throw new Exception(idioma.Translate("ExcUsuarioDniExistente"));
            }

            string user = GenerarUsuario(nombre, dni);
            string password = GenerarPassword(apellido, dni);
            string passwordHash = Services_13M.ServiceSeguridad13M.Hashear(password);

            long dvh = CalcularDVHUsuario(
                dni,
                nombre,
                apellido,
                email,
                rol.Id,
                user,
                passwordHash
            );
            Services.DigitoVerificador13M.ActualizarDVVUsuario();

            dal.InsertarUsuario(dni, nombre, apellido, email, rol.Id, user, passwordHash, dvh);

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(dniAutor, "Se creo un usuario nuevo", Criticidad13M.Medio, Modulos13M.Usuario);
        }

        public void activarDesactivar(string dni)
        {
            List<UsuarioModelo13M> todosLosUsuarios = obtenerTodos();

            UsuarioModelo13M usuario = todosLosUsuarios.FirstOrDefault(u => u.DNI == dni);

            string evento = "";

            if (usuario.Activo == true)
            {
                dal.DesactivarUsuario(dni);
                RecalcularDVHUsuario(dni);
                evento = $"Se desactivó la cuenta del usuario: {usuario.User}";
            }
            else
            {
                dal.ActivarUsuario(dni);
                RecalcularDVHUsuario(dni);
                evento = $"Se activó la cuenta del usuario: {usuario.User}";
            }

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(dniAutor, evento, Criticidad13M.Alto, Modulos13M.Usuario);
        }

        public void ModificarUsuario(string dni, string email, RolModelo13M rol)
        {
            dal.ModificarUsuario(dni, email, rol.Id);
            RecalcularDVHUsuario(dni);

            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(dniAutor, $"Se modificó usuario DNI {dni}", Criticidad13M.Medio, Modulos13M.Usuario);
        }

        public bool cambiarPassword(string passwordActual, string passwordNueva)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            string passwordActualHash = Services_13M.ServiceSeguridad13M.Hashear(passwordActual);

            UsuarioModelo13M usuarioActivo = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo;

            if (passwordActualHash != usuarioActivo.Password)
            {
                throw new Exception(idioma.Translate("ExcPasswordActualIncorrecta"));
            }

            string passwordNuevaHash = Services_13M.ServiceSeguridad13M.Hashear(passwordNueva);

            if (passwordActualHash == passwordNuevaHash)
            {
                throw new Exception(idioma.Translate("ExcPasswordIgualAnterior"));
            }

            dal.CambiarPassword(passwordNuevaHash, usuarioActivo.DNI);
            RecalcularDVHUsuario(usuarioActivo.DNI);
            

            return true;

        }

        private UsuarioModelo13M MapearUsuario(DataRow row)
        {
            if (row == null)
            {
                return null;
            }
            return new UsuarioModelo13M
            {
                DNI = row["DNI"].ToString(),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Email = row["Email"].ToString(),
                Rol = new RolModelo13M { Id = Convert.ToInt32(row["IdRol"]), Nombre = row["NombreRol"].ToString() },
                User = row["Username"].ToString(),
                Password = row["PasswordHash"].ToString(),
                Intentos = Convert.ToInt32(row["Intentos"]),
                Bloqueo = Convert.ToBoolean(row["Bloqueo"]),
                Activo = Convert.ToBoolean(row["Activo"]),
                UltimoIntentoFallido = row["UltimoIntentoFallido"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["UltimoIntentoFallido"]),
                IdIdioma = Convert.ToInt32(row["IdIdioma"]),
                //DVH = row["DVH"] == DBNull.Value ? 0 : Convert.ToInt64(row["DVH"]),

            };
        }

        public void DesbloquearUsuario(string dni)
        {
            var idioma = Services_13M.ServiceSessionManager13M.getIntancia().Idioma;

            var row = dal.obtenerPorDNI(dni);
            var usuario = MapearUsuario(row);

            if (usuario == null)
                throw new Exception(idioma.Translate("ExcUsuarioNoEncontrado"));

            if (!usuario.Bloqueo)
                throw new Exception(idioma.Translate("ExcUsuarioNoBloqueado"));

            // password default
            string nuevaPass = GenerarPassword(usuario.Apellido, usuario.DNI);
            string nuevaPassHash = Services_13M.ServiceSeguridad13M.Hashear(nuevaPass);

            // desbloqueo
            dal.desbloquearUsuario(dni, nuevaPassHash);
            RecalcularDVHUsuario(dni);

            // bitácora
            string dniAutor = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(
                dniAutor,
                $"Se desbloqueó el usuario: {usuario.User}",
                Criticidad13M.Alto,
                Modulos13M.Usuario
            );
        }

        public void GuardarIdioma(int idIdioma)
        {
            string dni = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.DNI;
            dal.GuardarIdioma(dni, idIdioma);
            RecalcularDVHUsuario(dni);

            Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo.IdIdioma = idIdioma;
        }

        private long CalcularDVHUsuario(


            string dni,
            string nombre,
            string apellido,
            string email,
            int idRol,
            string user,
            string passwordHash)
            
        {
                    string cadena =
                    dni +
                    nombre +
                    apellido +
                    email +
                    idRol +
                    user +
                    passwordHash;

            return Services.DigitoVerificador13M.CalcularDVH(cadena);
        }

        private void RecalcularDVHUsuario(string dni)
        {
            var row = dal.obtenerPorDNI(dni);

            UsuarioModelo13M usuario = MapearUsuario(row);

            long nuevoDVH = CalcularDVHUsuario(
                usuario.DNI,
                usuario.Nombre,
                usuario.Apellido,
                usuario.Email,
                usuario.Rol.Id,
                usuario.User,
                usuario.Password
            );

            dal.ActualizarDVH(dni, nuevoDVH);
            Services.DigitoVerificador13M.ActualizarDVVUsuario();

        }


        public void RepararDVH(string dni)
        {
            RecalcularDVHUsuario(dni);
        }

        #region Credenciales
        public string GenerarUsuario(string nombre, string dni)
        {
            return nombre.Trim().ToLower() + dni;
        }

        public string GenerarPassword(string apellido, string dni)
        {
            return apellido.Trim().ToLower() + dni;
        }
        #endregion Credenciales
    }
}
