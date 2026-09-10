using BE;
using BLL;
using Services;
using Services.Modelos;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Servicios
{
    public partial class MenuPrincipal : Form, IIdiomaObserver
    {
        UsuarioModelo13M usuarioActual = Services_13M.ServiceSessionManager13M.getIntancia().usuarioActivo;
        BLLIdioma13M _idiomaService = new BLLIdioma13M();
        UsuarioService _userService = new UsuarioService();

        public MenuPrincipal()
        {
            InitializeComponent();
            configurarAcceso();
            AplicarPermisosSidebar();
            CargarSubItemsIdioma();

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();

        }

        // Abre el formulario para registrar un nadador nuevo en el padrón
        private void RegistrarNadador_Click(object sender, EventArgs e)
        {
            RegistrarNadador19M form = new RegistrarNadador19M();
            form.Show();
        }

        // Copia los permisos de los menús a los botones de la barra lateral
        // para que cada opción quede habilitada o deshabilitada igual que en el menú.
        private void AplicarPermisosSidebar()
        {
            btnGestionUsuarios.Enabled = gestionUsuariosToolStripMenuItem.Enabled;
            btnGestionRoles.Enabled = gestionRolToolStripMenuItem.Enabled;
            btnGestionFamilias.Enabled = gestionFamiliaToolStripMenuItem.Enabled;
            btnAuditoria.Enabled = bitacoraEventosToolStripMenuItem.Enabled;
            btnGestionRespaldo.Enabled = gestionRespaldoToolStripMenuItem.Enabled;
            btnCambiarClave.Enabled = cambiarClaveToolStripMenuItem.Enabled;
            btnCerrarSesion.Enabled = cerrarSesionToolStripMenuItem.Enabled;
        }
        private void CargarSubItemsIdioma()
        {
            idiomaToolStripMenuItem.DropDownItems.Clear();

            var idiomas = _idiomaService.obtenerTodos();

            foreach (var idioma in idiomas)
            {
                var item = new ToolStripMenuItem(idioma.Nombre);
                item.Tag = idioma;

                // Marcar el idioma actual del usuario
                int idiomaActual = ServiceSessionManager13M.getIntancia().usuarioActivo.IdIdioma;
                item.Checked = idioma.Id == idiomaActual;

                item.Click += IdiomaItem_Click;
                idiomaToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void IdiomaItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var idiomaSeleccionado = (Idioma13M)item.Tag;

            // Guardar en BD y sesión
            _userService.GuardarIdioma(idiomaSeleccionado.Id);

            // Aplicar idioma globalmente
            string cod = idiomaSeleccionado.Id == 1 ? "es" : "en";
            ServiceSessionManager13M.getIntancia().Idioma.CargarIdioma(cod);

            // Actualizar checks del submenú
            foreach (ToolStripMenuItem subItem in idiomaToolStripMenuItem.DropDownItems)
            {
                subItem.Checked = subItem.Tag == item.Tag;
            }
        }

        private void configurarAcceso()
        {
            cambiarClaveToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Cambiar Clave");
            cerrarSesionToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Cerrar Sesion");
            gestionUsuariosToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Gestion Usuario");
            iniciarSesionToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Iniciar Sesion");
            bitacoraEventosToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Auditoria Eventos");
            gestionRolToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Gestion Roles");
            gestionFamiliaToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Gestion Familia");
            cambiarClaveToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Cambiar Clave");
            cerrarSesionToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Cerrar Sesion");
            iniciarSesionToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Iniciar Sesion");
            idiomaToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Cambiar Idioma");
            gestionRespaldoToolStripMenuItem.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Gestion Respaldo");
        }

        private void cambiarClaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarContraseña form = new CambiarContraseña();
            form.Show();
        }

        private void gestionUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionUsuario form = new GestionUsuario();
            form.Show();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            DialogResult resultado = MessageBox.Show(
                t.Translate("MenuPrincipal.msgConfirmarCierreSesion"),
                t.Translate("MenuPrincipal.msgTituloCierreSesion"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Services_13M.ServiceSessionManager13M.getIntancia().Logout();

            base.OnFormClosing(e);
        }

        private void iniciarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
        }

        private void bitacoraEventosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuditoriaBitacora form = new AuditoriaBitacora();
            form.Show();
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("MenuPrincipal.formTitle");
            label1.Text = string.Format(t.Translate("MenuPrincipal.labelBienvenido"), usuarioActual.Nombre, usuarioActual.Apellido);
            usuarioToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuUsuario");
            cambiarClaveToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuCambiarClave");
            cerrarSesionToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuCerrarSesion");
            iniciarSesionToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuIniciarSesion");
            administradorToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuAdministrador");
            gestionUsuariosToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuGestionUsuarios");
            bitacoraEventosToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuBitacoraEventos");
            gestionFamiliaToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuGestionFamilia");
            gestionRolToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuGestionRol");
            ayudaToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuAyuda");
            idiomaToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuIdioma");
            gestionRespaldoToolStripMenuItem.Text = t.Translate("MenuPrincipal.menuRespaldo");

            // textos de la barra lateral del nuevo diseño
            lblTituloMain.Text = t.Translate("MenuPrincipal.dashboardTitle");
            lblUsuarioSidebar.Text = string.Format(t.Translate("MenuPrincipal.labelBienvenido"), usuarioActual.Nombre, usuarioActual.Apellido);
            lblSeccionNadadores.Text = t.Translate("MenuPrincipal.seccionNadadores");
            lblSeccionTorneos.Text = t.Translate("MenuPrincipal.seccionTorneos");
            lblSeccionClases.Text = t.Translate("MenuPrincipal.seccionClases");
            lblSeccionAdmin.Text = t.Translate("MenuPrincipal.seccionAdmin");
            btnRegistrarNadador.Text = t.Translate("MenuPrincipal.btnRegistrarNadador");
            btnTorneos.Text = t.Translate("MenuPrincipal.btnTorneos");
            btnClases.Text = t.Translate("MenuPrincipal.btnClases");
            btnGestionUsuarios.Text = t.Translate("MenuPrincipal.menuGestionUsuarios");
            btnGestionRoles.Text = t.Translate("MenuPrincipal.menuGestionRol");
            btnGestionFamilias.Text = t.Translate("MenuPrincipal.menuGestionFamilia");
            btnAuditoria.Text = t.Translate("MenuPrincipal.menuBitacoraEventos");
            btnGestionRespaldo.Text = t.Translate("MenuPrincipal.menuRespaldo");
            btnCambiarClave.Text = t.Translate("MenuPrincipal.menuCambiarClave");
            btnCerrarSesion.Text = t.Translate("MenuPrincipal.menuCerrarSesion");
        }

        private void gestionFamiliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionFamilia form = new GestionFamilia();
            form.Show();
        }

        private void gestionRolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionRol form = new GestionRol();
            form.Show();
        }

        private void usuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void administradorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionRespaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GestionRespaldo form = new GestionRespaldo();
            form.Show();
        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
