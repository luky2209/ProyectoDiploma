using BE;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Servicios
{
    public partial class RegistrarNadadorClase19M : Form, IIdiomaObserver
    {
        BLLInscripcionClase13M _bllInscripcionClase = new BLLInscripcionClase13M();
        BLLClase13M _bllClase = new BLLClase13M();
        BLLNadador13M _bllNadador = new BLLNadador13M();

        public RegistrarNadadorClase19M()
        {
            InitializeComponent();

            cmbClase.SelectedIndexChanged += cmbClase_SelectedIndexChanged;
            cmbNadador.SelectedIndexChanged += cmbNadador_SelectedIndexChanged;

            CargarClases();
            CargarNadadores();
            CargarGrilla();

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        private void CargarClases()
        {
            cmbClase.Items.Clear();
            foreach (Clase13M clase in _bllClase.obtenerTodos())
            {
                cmbClase.Items.Add(clase);
            }
            if (cmbClase.Items.Count > 0)
            {
                cmbClase.SelectedIndex = 0;
            }
        }

        private void CargarNadadores()
        {
            cmbNadador.Items.Clear();
            foreach (Nadador13M nadador in _bllNadador.obtenerTodos())
            {
                cmbNadador.Items.Add(nadador);
            }
            if (cmbNadador.Items.Count > 0)
            {
                cmbNadador.SelectedIndex = 0;
            }
        }

        // cuando se elige una clase se muestran su rango etario, su nivel y su cupo
        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clase13M clase = cmbClase.SelectedItem as Clase13M;

            if (clase == null)
            {
                return;
            }

            txtRangoEtarioClase.Text = clase.EdadMinima + " - " + clase.EdadMaxima;
            txtNivelClase.Text = clase.NivelDificultad;
            txtCupoClase.Text = _bllInscripcionClase.ContarInscripcionesDeLaClase(clase.CodigoClase) + " / " + clase.CupoMaximo;

            CargarGrilla();
        }

        // cuando se elige un nadador se muestran su categoria y su edad
        private void cmbNadador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nadador13M nadador = cmbNadador.SelectedItem as Nadador13M;

            txtCategoriaNadador.Text = nadador != null ? nadador.Categoria : "";
            txtEdadNadador.Text = nadador != null ? nadador.Edad.ToString() : "";
        }

        private void CargarGrilla()
        {
            dgvInscripciones.Columns.Clear();
            dgvInscripciones.AutoGenerateColumns = false;

            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroInscripcionClase", DataPropertyName = "NumeroInscripcionClase", HeaderText = "Nro." });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNINadador", DataPropertyName = "DNINadador", HeaderText = "DNI" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoClase", DataPropertyName = "TipoClase", HeaderText = "Clase" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaInscripcion", DataPropertyName = "FechaInscripcion", HeaderText = "Fecha" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado" });

            Clase13M clase = cmbClase.SelectedItem as Clase13M;
            if (clase == null)
            {
                dgvInscripciones.DataSource = null;
                return;
            }

            dgvInscripciones.DataSource = _bllInscripcionClase.obtenerPorClase(clase.CodigoClase);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            try
            {
                Nadador13M nadador = cmbNadador.SelectedItem as Nadador13M;
                Clase13M clase = cmbClase.SelectedItem as Clase13M;

                if (nadador == null || clase == null)
                {
                    MessageBox.Show(t.Translate("RegistrarNadadorClase19M.msgCamposObligatorios"));
                    return;
                }

                _bllInscripcionClase.CrearInscripcionClase(nadador.DNI, clase.CodigoClase);

                MessageBox.Show(t.Translate("RegistrarNadadorClase19M.msgRegistroExitoso"));

                cmbClase_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("RegistrarNadadorClase19M.formTitle");
            lblTitulo.Text = t.Translate("RegistrarNadadorClase19M.lblTitulo");
            gbClase.Text = t.Translate("RegistrarNadadorClase19M.gbClase");
            labelClase.Text = t.Translate("RegistrarNadadorClase19M.labelClase");
            lblRangoEtarioClase.Text = t.Translate("RegistrarNadadorClase19M.lblRangoEtarioClase");
            lblNivelClase.Text = t.Translate("RegistrarNadadorClase19M.lblNivelClase");
            lblCupoClase.Text = t.Translate("RegistrarNadadorClase19M.lblCupoClase");
            gbNadador.Text = t.Translate("RegistrarNadadorClase19M.gbNadador");
            labelNadador.Text = t.Translate("RegistrarNadadorClase19M.labelNadador");
            lblCategoriaNadador.Text = t.Translate("RegistrarNadadorClase19M.lblCategoriaNadador");
            lblEdadNadador.Text = t.Translate("RegistrarNadadorClase19M.lblEdadNadador");
            gbInscripciones.Text = t.Translate("RegistrarNadadorClase19M.gbInscripciones");
            btnRegistrar.Text = t.Translate("RegistrarNadadorClase19M.btnRegistrar");
        }

        private void RegistrarNadadorClase19M_Load(object sender, EventArgs e)
        {

        }
    }
}