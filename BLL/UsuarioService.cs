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
        DALUsuario55CA dal = new DALUsuario55CA();
        BitacoraEventosService bit = new BitacoraEventosService();

        public List<UsuarioModelo55CA> obtenerTodos()
        {
            var dt = dal.obtenerTodos();
            List<UsuarioModelo55CA> lista = new List<UsuarioModelo55CA>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(MapearUsuario(row));
            }

            return lista;
        }

        public bool login(string user, string password)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            var usuario = MapearUsuario(dal.obtenerPorUser(user));

            //validaciones
            if (Services_55CA.ServiceSessionManager55CA.getIntancia().estaLogueado())
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

            string passwordHash = Services_55CA.ServiceSeguridad55CA.Hashear(password);

            if (usuario.Password != passwordHash)
            {
                dal.aumentarIntento(usuario.DNI);

                usuario.Intentos++;

                if (usuario.Intentos >= 4)
                {
                    dal.bloquearUsuario(usuario.DNI);
                    bit.registrarEvento(usuario.DNI, $"Usuario {usuario.User} bloqueado.", Criticidad55CA.Alto, Modulos55CA.Seguridad);

                    throw new Exception(idioma.Translate("ExcCuentaBloqueada"));
                }

                throw new Exception(string.Format(idioma.Translate("ExcPasswordIncorrecta"), usuario.Intentos));
            }
            BLLRol gestorRol = new BLLRol();

            List<RolModelo55CA> roles = gestorRol.ObtenerRolesConJerarquia();

            var rolConPermisos = roles.FirstOrDefault(r => r.Id == usuario.Rol.Id);

            if(rolConPermisos != null)
            {
                usuario.Rol = rolConPermisos;
            }

            //login ok
            Services_55CA.ServiceSessionManager55CA.getIntancia().Login(usuario);

            bit.registrarEvento(usuario.DNI, $"Realizo login exitoso.", Criticidad55CA.Medio, Modulos55CA.Usuario);

            dal.reiniciarIntentos(usuario.DNI);

            // verificamos si sigue usando password por defecto
            string passwordDefault = GenerarPassword(usuario.Apellido, usuario.DNI);

            string passwordDefaultHash = Services_55CA.ServiceSeguridad55CA.Hashear(passwordDefault);

            bool usaPasswordDefault =
                usuario.Password == passwordDefaultHash;

            return usaPasswordDefault;
        }

        public void CrearUsuario(string dni, string nombre, string apellido, string email, RolModelo55CA rol)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;


            if (dal.obtenerPorDNI(dni) != null)
            {
                throw new Exception(idioma.Translate("ExcUsuarioDniExistente"));
            }

            string user = GenerarUsuario(nombre, dni);
            string password = GenerarPassword(apellido, dni);
            string passwordHash = Services_55CA.ServiceSeguridad55CA.Hashear(password);

            long dvh = CalcularDVHUsuario(
                dni,
                nombre,
                apellido,
                email,
                rol.Id,
                user,
                passwordHash
            );
            Services.DigitoVerificador55CA.ActualizarDVVUsuario();

            dal.InsertarUsuario(dni, nombre, apellido, email, rol.Id, user, passwordHash, dvh);

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(dniAutor, "Se creo un usuario nuevo", Criticidad55CA.Medio, Modulos55CA.Usuario);
        }

        public void activarDesactivar(string dni)
        {
            List<UsuarioModelo55CA> todosLosUsuarios = obtenerTodos();

            UsuarioModelo55CA usuario = todosLosUsuarios.FirstOrDefault(u => u.DNI == dni);

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

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(dniAutor, evento, Criticidad55CA.Alto, Modulos55CA.Usuario);
        }

        public void ModificarUsuario(string dni, string email, RolModelo55CA rol)
        {
            dal.ModificarUsuario(dni, email, rol.Id);
            RecalcularDVHUsuario(dni);

            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(dniAutor, $"Se modificó usuario DNI {dni}", Criticidad55CA.Medio, Modulos55CA.Usuario);
        }

        public bool cambiarPassword(string passwordActual, string passwordNueva)
        {
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            string passwordActualHash = Services_55CA.ServiceSeguridad55CA.Hashear(passwordActual);

            UsuarioModelo55CA usuarioActivo = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo;

            if (passwordActualHash != usuarioActivo.Password)
            {
                throw new Exception(idioma.Translate("ExcPasswordActualIncorrecta"));
            }

            string passwordNuevaHash = Services_55CA.ServiceSeguridad55CA.Hashear(passwordNueva);

            if (passwordActualHash == passwordNuevaHash)
            {
                throw new Exception(idioma.Translate("ExcPasswordIgualAnterior"));
            }

            dal.CambiarPassword(passwordNuevaHash, usuarioActivo.DNI);
            RecalcularDVHUsuario(usuarioActivo.DNI);
            

            return true;

        }

        private UsuarioModelo55CA MapearUsuario(DataRow row)
        {
            if (row == null)
            {
                return null;
            }
            return new UsuarioModelo55CA
            {
                DNI = row["DNI"].ToString(),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Email = row["Email"].ToString(),
                Rol = new RolModelo55CA { Id = Convert.ToInt32(row["IdRol"]), Nombre = row["NombreRol"].ToString() },
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
            var idioma = Services_55CA.ServiceSessionManager55CA.getIntancia().Idioma;

            var row = dal.obtenerPorDNI(dni);
            var usuario = MapearUsuario(row);

            if (usuario == null)
                throw new Exception(idioma.Translate("ExcUsuarioNoEncontrado"));

            if (!usuario.Bloqueo)
                throw new Exception(idioma.Translate("ExcUsuarioNoBloqueado"));

            // password default
            string nuevaPass = GenerarPassword(usuario.Apellido, usuario.DNI);
            string nuevaPassHash = Services_55CA.ServiceSeguridad55CA.Hashear(nuevaPass);

            // desbloqueo
            dal.desbloquearUsuario(dni, nuevaPassHash);
            RecalcularDVHUsuario(dni);

            // bitácora
            string dniAutor = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;

            bit.registrarEvento(
                dniAutor,
                $"Se desbloqueó el usuario: {usuario.User}",
                Criticidad55CA.Alto,
                Modulos55CA.Usuario
            );
        }

        public void GuardarIdioma(int idIdioma)
        {
            string dni = Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.DNI;
            dal.GuardarIdioma(dni, idIdioma);
            RecalcularDVHUsuario(dni);

            Services_55CA.ServiceSessionManager55CA.getIntancia().usuarioActivo.IdIdioma = idIdioma;
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

            return Services.DigitoVerificador55CA.CalcularDVH(cadena);
        }

        private void RecalcularDVHUsuario(string dni)
        {
            var row = dal.obtenerPorDNI(dni);

            UsuarioModelo55CA usuario = MapearUsuario(row);

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
            Services.DigitoVerificador55CA.ActualizarDVVUsuario();

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
