using BLL;
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

namespace Servicios
{
    public partial class CambiarContraseña : Form, IIdiomaObserver
    {
        UsuarioService _usuarioService = new UsuarioService();
        ServiceSessionManager13M instancia = ServiceSessionManager13M.getIntancia();
        public CambiarContraseña()
        {
            InitializeComponent();
            txtUser.Text = instancia.usuarioActivo.User;
            instancia.Idioma.Suscribir(this);
            actualizarIdioma();
        }

        public void actualizarIdioma()
        {
            var t = instancia.Idioma;

            this.Text = t.Translate("CambiarContraseña.formTitle");
            label1.Text = t.Translate("CambiarContraseña.labelUser");
            label2.Text = t.Translate("CambiarContraseña.labelContraseñaActual");
            label4.Text = t.Translate("CambiarContraseña.labelContraseñaNueva");
            label3.Text = t.Translate("CambiarContraseña.labelConfirmar");
            btnAceptar.Text = t.Translate("CambiarContraseña.btnConfirmar");
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string passwordNueva = txtNueva.Text;
            string passwordActual = txtContraseñaActual.Text;
            string confirmacion = txtConfirmacion.Text;

            var t = ServiceSessionManager13M.getIntancia().Idioma;

            try
            {
                if (passwordNueva != confirmacion)
                {
                    MessageBox.Show(t.Translate("CambiarContraseña.msgNoCoinciden"));
                }
                else
                {
                    if(_usuarioService.cambiarPassword(passwordActual, passwordNueva))
        {
                        MessageBox.Show(t.Translate("CambiarContraseña.msgExito"));

                        // cerrar sesión
                        ServiceSessionManager13M.getIntancia().Logout();
                        this.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("CambiarContraseña.msgError") + ex.Message);
                return;
            }
        }
    }
}
