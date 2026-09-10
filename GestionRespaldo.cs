using Services;
using Services.Modelos.Idioma;
using Services_55CA;
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
    public partial class GestionRespaldo : Form, IIdiomaObserver
    {
        BackUpRestore55CA serviceBackUpRestore = new BackUpRestore55CA();

        public GestionRespaldo()
        {
            InitializeComponent();
            actualizarIdioma();
        }

        private void buscarCarpetaBackUp_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                var t = ServiceSessionManager55CA.getIntancia().Idioma;
                fbd.Description = t.Translate("GestionRespaldo.descSeleccionarCarpetaBackup");

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaBackUp.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnRealizarBackUp_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutaBackUp.Text))
                {
                    MessageBox.Show(t.Translate("GestionRespaldo.msgSeleccionarRuta"));
                    return;
                }

                serviceBackUpRestore.realizarBackUp(txtRutaBackUp.Text);
                MessageBox.Show(t.Translate("GestionRespaldo.msgBackupExitoso"), t.Translate("GestionRespaldo.titleExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtRutaBackUp.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("GestionRespaldo.msgErrorBackup") + ex.Message, t.Translate("GestionRespaldo.titleError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarCarpetaRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                var t = ServiceSessionManager55CA.getIntancia().Idioma;
                ofd.Title = t.Translate("GestionRespaldo.titleSeleccionarBackup");
                ofd.Filter = "Archivos de Backup SQL (*.bak)|*.bak|Todos los archivos (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtRutaRestore.Text = ofd.FileName;
                }
            }
        }

        private void btnRealizarRestore_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;
            try
            {
                if (string.IsNullOrWhiteSpace(txtRutaRestore.Text))
                {
                    MessageBox.Show(t.Translate("GestionRespaldo.msgSeleccionarArchivoBackup"));
                    return;
                }

                DialogResult r = MessageBox.Show(t.Translate("GestionRespaldo.msgConfirmarRestore"), t.Translate("GestionRespaldo.titleAdvertenciaCritica"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (r == DialogResult.Yes)
                {
                    serviceBackUpRestore.realizarRestore(txtRutaRestore.Text);
                    MessageBox.Show(t.Translate("GestionRespaldo.msgRestoreExitoso"), t.Translate("GestionRespaldo.titleExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("GestionRespaldo.msgErrorRestore") + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;

            this.Text = t.Translate("GestionRespaldo.titulo");
            btnRealizarBackUp.Text = t.Translate("GestionRespaldo.btnRealizarBackup");
            btnRealizarRestore.Text = t.Translate("GestionRespaldo.btnRealizarRestore");

        }
    }
}
