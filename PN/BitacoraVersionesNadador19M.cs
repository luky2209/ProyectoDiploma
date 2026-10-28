using BE;
using BLL;
using Services_13M;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Servicios
{
    public partial class BitacoraVersionesNadador19M : Form
    {
        BLLNadador13M _bllNadador = new BLLNadador13M();
        BLLNadadorHistorial13M _bllHistorial = new BLLNadadorHistorial13M();

        List<Nadador13M> _nadadores = new List<Nadador13M>();
        List<NadadorHistorial13M> _historial = new List<NadadorHistorial13M>();

        public BitacoraVersionesNadador19M()
        {
            InitializeComponent();
            CargarNadadores();
        }

        private void CargarNadadores()
        {
            _nadadores = _bllNadador.obtenerTodos();
            dgvNadadores.DataSource = null;
            dgvNadadores.DataSource = _nadadores;
            actualizarColumnasNadadores();
        }

        private void actualizarColumnasNadadores()
        {
            if (dgvNadadores.Columns.Count == 0) return;
            dgvNadadores.Columns["DNI"].HeaderText = "DNI";
            dgvNadadores.Columns["Nombre"].HeaderText = "Nombre";
            dgvNadadores.Columns["Apellido"].HeaderText = "Apellido";
            dgvNadadores.Columns["Categoria"].HeaderText = "Categoria";
            if (dgvNadadores.Columns.Contains("FechaNacimiento"))
                dgvNadadores.Columns["FechaNacimiento"].Visible = false;
            if (dgvNadadores.Columns.Contains("Edad"))
                dgvNadadores.Columns["Edad"].Visible = false;
            if (dgvNadadores.Columns.Contains("CertificadoMedico"))
                dgvNadadores.Columns["CertificadoMedico"].Visible = false;
            if (dgvNadadores.Columns.Contains("DVH"))
                dgvNadadores.Columns["DVH"].Visible = false;
            if (dgvNadadores.Columns.Contains("DescripcionLista"))
                dgvNadadores.Columns["DescripcionLista"].Visible = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(filtro))
            {
                CargarNadadores();
                return;
            }

            List<Nadador13M> filtrados = new List<Nadador13M>();
            foreach (Nadador13M n in _nadadores)
            {
                if (n.DNI.ToLower().Contains(filtro) ||
                    n.Nombre.ToLower().Contains(filtro) ||
                    n.Apellido.ToLower().Contains(filtro))
                {
                    filtrados.Add(n);
                }
            }

            dgvNadadores.DataSource = null;
            dgvNadadores.DataSource = filtrados;
            actualizarColumnasNadadores();
        }

        private void dgvNadadores_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNadadores.CurrentRow == null) return;
            Nadador13M seleccionado = dgvNadadores.CurrentRow.DataBoundItem as Nadador13M;
            if (seleccionado == null) return;

            CargarHistorial(seleccionado.DNI);
        }

        private void CargarHistorial(string dni)
        {
            _historial = _bllHistorial.ObtenerHistorialPorDNI(dni);
            dgvHistorial.DataSource = null;
            dgvHistorial.DataSource = _historial;
            actualizarColumnasHistorial();
            lblMensaje.Text = "";
        }

        private void actualizarColumnasHistorial()
        {
            if (dgvHistorial.Columns.Count == 0) return;
            if (dgvHistorial.Columns.Contains("IdHistorial"))
                dgvHistorial.Columns["IdHistorial"].Visible = false;
            if (dgvHistorial.Columns.Contains("DNI"))
                dgvHistorial.Columns["DNI"].Visible = false;
            if (dgvHistorial.Columns.Contains("Nombre"))
                dgvHistorial.Columns["Nombre"].Visible = false;
            if (dgvHistorial.Columns.Contains("Apellido"))
                dgvHistorial.Columns["Apellido"].Visible = false;
            if (dgvHistorial.Columns.Contains("FechaNacimiento"))
                dgvHistorial.Columns["FechaNacimiento"].Visible = false;
            if (dgvHistorial.Columns.Contains("Edad"))
                dgvHistorial.Columns["Edad"].Visible = false;
            if (dgvHistorial.Columns.Contains("Categoria"))
                dgvHistorial.Columns["Categoria"].Visible = false;
            if (dgvHistorial.Columns.Contains("CertificadoMedico"))
                dgvHistorial.Columns["CertificadoMedico"].Visible = false;
            if (dgvHistorial.Columns.Contains("DVH"))
                dgvHistorial.Columns["DVH"].Visible = false;
            if (dgvHistorial.Columns.Contains("OrigenVersionId"))
                dgvHistorial.Columns["OrigenVersionId"].Visible = false;

            if (dgvHistorial.Columns.Contains("VersionNumero"))
                dgvHistorial.Columns["VersionNumero"].HeaderText = "Version";
            if (dgvHistorial.Columns.Contains("FechaHora"))
                dgvHistorial.Columns["FechaHora"].HeaderText = "Fecha y hora";
            if (dgvHistorial.Columns.Contains("TipoOperacion"))
                dgvHistorial.Columns["TipoOperacion"].HeaderText = "Operacion";
            if (dgvHistorial.Columns.Contains("UsuarioDNI"))
                dgvHistorial.Columns["UsuarioDNI"].HeaderText = "Usuario";
            if (dgvHistorial.Columns.Contains("Activo"))
                dgvHistorial.Columns["Activo"].HeaderText = "Activa";
        }

        private void dgvHistorial_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow == null) return;
            NadadorHistorial13M version = dgvHistorial.CurrentRow.DataBoundItem as NadadorHistorial13M;
            if (version == null) return;

            MostrarDetalle(version);
        }

        private void MostrarDetalle(NadadorHistorial13M version)
        {
            lblDetalle.Text = "DNI: " + version.DNI + " | Nombre: " + version.Nombre + " " + version.Apellido +
                              " | Nacimiento: " + version.FechaNacimiento.ToString("dd/MM/yyyy") +
                              " | Edad: " + version.Edad + " | Categoria: " + version.Categoria +
                              " | Certificado: " + (version.CertificadoMedico ? "Si" : "No") +
                              " | Version: " + version.VersionNumero + " | Operacion: " + version.TipoOperacion +
                              " | Activa: " + (version.Activo ? "Si" : "No");
        }

        private void btnVerDetalle_Click(object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow == null)
            {
                lblMensaje.Text = "Seleccione una version del historial.";
                return;
            }
            NadadorHistorial13M version = dgvHistorial.CurrentRow.DataBoundItem as NadadorHistorial13M;
            if (version == null) return;
            MostrarDetalle(version);
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            if (dgvHistorial.CurrentRow == null)
            {
                lblMensaje.Text = "Seleccione una version del historial.";
                return;
            }

            NadadorHistorial13M version = dgvHistorial.CurrentRow.DataBoundItem as NadadorHistorial13M;
            if (version == null) return;

            if (version.Activo)
            {
                lblMensaje.Text = "La version seleccionada ya es la activa.";
                return;
            }

            DialogResult r = MessageBox.Show(
                "Esta seguro que desea activar la version " + version.VersionNumero + "? Esto reemplazara el estado actual del nadador.",
                "Confirmar activacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes) return;

            try
            {
                _bllHistorial.RestaurarVersion(version.IdHistorial);
                lblMensaje.Text = "Version activada correctamente.";
                CargarNadadores();
                if (dgvNadadores.CurrentRow != null)
                {
                    Nadador13M sel = dgvNadadores.CurrentRow.DataBoundItem as Nadador13M;
                    if (sel != null && sel.DNI == version.DNI)
                        CargarHistorial(version.DNI);
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}
