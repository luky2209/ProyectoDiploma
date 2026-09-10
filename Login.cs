using BE;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Services;

namespace Servicios
{
    public partial class Login : Form, IIdiomaObserver
    {
        UsuarioService _userService = new UsuarioService();
        BLLIdioma13M _idiomaService = new BLLIdioma13M();
        

        public Login()
        {
            InitializeComponent();
            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);

        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            string username = txtUser.Text;
            string password = txtPassword.Text;
            try
            {
                bool usaPasswordDefault = _userService.login(username, password);

                int idiomaUsuario = ServiceSessionManager13M.getIntancia().usuarioActivo.IdIdioma;
                string codIdiomaUsuario = idiomaUsuario == 1 ? "es" : "en";
                ServiceSessionManager13M.getIntancia().Idioma.CargarIdioma(codIdiomaUsuario);

                bool usuarioOk = DigitoVerificador13M.VerificarUsuario();
                bool rolOk = DigitoVerificador13M.VerificarRol();
                bool familiaOk = DigitoVerificador13M.VerificarFamilia();
                bool patenteOk = DigitoVerificador13M.VerificarPatente();
                bool nadadorOk = DigitoVerificador13M.VerificarNadador();


                
                if (!usuarioOk || !rolOk || !familiaOk || !patenteOk || !nadadorOk)
                {
                    if (ServiceSessionManager13M.getIntancia().usuarioActivo.Rol.Id != 1)
                    {
                        MessageBox.Show("Se encontraron inconsistencias en la base de datos, contactese con un administrador");
                        txtUser.Text = "";
                        txtPassword.Text = "";
                        ServiceSessionManager13M.getIntancia().Logout();
                        return;

                    }
                    this.Hide();
                    RepararInconsistencias pantalla = new RepararInconsistencias(usuarioOk, rolOk, familiaOk, patenteOk, nadadorOk);
                    pantalla.FormClosed += (s, args) => RestaurarIdiomaLogin();
                    pantalla.Show();
                    return;
                }


                
                
                txtUser.Text = "";
                txtPassword.Text = "";
                this.Hide();

                if (usaPasswordDefault)
                {
                    CambiarContraseña form = new CambiarContraseña();
                    form.FormClosed += (s, args) => RestaurarIdiomaLogin();
                    form.Show();
                }
                else
                {
                    MenuPrincipal menu = new MenuPrincipal();
                    menu.FormClosed += (s, args) => RestaurarIdiomaLogin();
                    menu.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RestaurarIdiomaLogin()
        {

            this.Show();
        }

        public void actualizarIdioma()
        {

        }

        
    }
}
