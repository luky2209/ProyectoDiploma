using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.Instalador;
using Servicios;

namespace Servicios
{
    public partial class Configuracion_Inicial : Form
    {
        public Configuracion_Inicial()
        {
            InitializeComponent();
        }

        private void Configuracion_Inicial_Load(object sender, EventArgs e)
        {
           
        }
        private void ActualizarVisibilidadCredenciales()
        {
            bool esSqlAuth = radSqlAuth.Checked;
            txtUsuario.Enabled = esSqlAuth;
            txtPassword.Enabled = esSqlAuth;
        }
        private void CargarInstanciasDisponibles()
        {
            cmbInstancias.Items.Clear();

            var instancias = ServicioConfiguracionConexion.ObtenerInstanciasDisponibles();

            foreach (var inst in instancias)
                cmbInstancias.Items.Add(inst.NombreServidor);

            if (cmbInstancias.Items.Count > 0)
                cmbInstancias.SelectedIndex = 0;

            lblEstado.Text = $"Se encontraron {cmbInstancias.Items.Count} instancia(s).";
            Cursor = Cursors.Default;
        }

        private string ConstruirConnectionStringDesdeFormulario()
        {
            if (cmbInstancias.SelectedItem == null)
            {
                throw new InvalidOperationException("Debe seleccionar una instancia de SQL Server.");
            }
            if (string.IsNullOrWhiteSpace(txtBaseDatos.Text))
            {
                throw new InvalidOperationException("Debe indicar el nombre de la base de datos.");
            }

            string servidor = cmbInstancias.SelectedItem.ToString();
            string baseDatos = txtBaseDatos.Text.Trim();

            return ServicioConfiguracionConexion.ConstruirConnectionString(
                servidor,
                baseDatos,
                usarWindowsAuth: radWindowsAuth.Checked,
                usuario: txtUsuario.Text,
                password: txtPassword.Text);
        }

        private void ProbarConexion_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = ConstruirConnectionStringDesdeFormulario();

                Cursor = Cursors.WaitCursor;
                ProbarConexion.Enabled = false;

                // Probamos contra "master" (que siempre existe), no contra la base específica,
                // porque la base todavía puede no estar creada.
                var builderPrueba = new System.Data.SqlClient.SqlConnectionStringBuilder(cs) { InitialCatalog = "master" };
                var (exito, mensajeError) = ServicioConfiguracionConexion.ProbarConexion(builderPrueba.ConnectionString);

                if (exito)
                {
                    lblEstado.ForeColor = System.Drawing.Color.Green;
                    lblEstado.Text = "✔ Conexión exitosa.";
                    btnGuardarYContinuar.Enabled = true;
                }
                else
                {
                    lblEstado.ForeColor = System.Drawing.Color.Red;
                    lblEstado.Text = "✘ " + mensajeError;
                    btnGuardarYContinuar.Enabled = false;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
                ProbarConexion.Enabled = true;
            }
        }

        private void btnGuardarYContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                string cs = ConstruirConnectionStringDesdeFormulario();

                var builderPrueba = new System.Data.SqlClient.SqlConnectionStringBuilder(cs) { InitialCatalog = "master" };
                var (exito, mensajeError) = ServicioConfiguracionConexion.ProbarConexion(builderPrueba.ConnectionString);

                if (!exito)
                {
                    MessageBox.Show(
                        "No se puede guardar una configuración que no conecta correctamente:\n\n" + mensajeError,
                        "Conexión inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreBase = txtBaseDatos.Text.Trim();

                bool existeBase = ServicioConfiguracionConexion.ExisteBaseDatos(cs, nombreBase);
                if (!existeBase)
                {
                    var respuesta = MessageBox.Show(
                        $"La base de datos '{nombreBase}' no existe en el servidor seleccionado.\n¿Desea crearla ahora?",
                        "Base de datos no encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        string rutaScript = System.IO.Path.Combine(Application.StartupPath, "Scripts", "scriptTerceraEntrega.sql");
                        ServicioConfiguracionConexion.EjecutarScriptCreacion(cs, rutaScript);
                        MessageBox.Show("Base de datos creada correctamente.");
                    }
                }

                ServicioConfiguracionConexion.GuardarConnectionString(cs);

                MessageBox.Show("Configuración guardada correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Configuracion_Inicial_Load_1(object sender, EventArgs e)
        {
            CargarInstanciasDisponibles();

            // Si ya existe una config previa, la precargamos para que el usuario solo la ajuste
            string csActual = ServicioConfiguracionConexion.ObtenerConnectionStringGuardada();
            if (!string.IsNullOrWhiteSpace(csActual))
            {
                var builder = new System.Data.SqlClient.SqlConnectionStringBuilder(csActual);
                txtBaseDatos.Text = builder.InitialCatalog;
                if (cmbInstancias.Items.Contains(builder.DataSource))
                    cmbInstancias.SelectedItem = builder.DataSource;
                radWindowsAuth.Checked = builder.IntegratedSecurity;
                radSqlAuth.Checked = !builder.IntegratedSecurity;
            }
            else
            {
                txtBaseDatos.Text = "is--servicios"; // valor por defecto sugerido
                radWindowsAuth.Checked = true;
            }

            ActualizarVisibilidadCredenciales();
        }
    }
}
