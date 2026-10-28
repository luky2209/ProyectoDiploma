using BE;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Servicios
{
    public partial class AsignarProfesorClase19M : Form, IIdiomaObserver
    {
        BLLClase13M _bllClase = new BLLClase13M();
        BLLProfesor13M _bllProfesor = new BLLProfesor13M();
        List<Clase13M> _listClases = new List<Clase13M>();
        List<Profesor13M> _listProfesores = new List<Profesor13M>();

        public AsignarProfesorClase19M()
        {
            InitializeComponent();
            ConfigurarGrillas();
            CargarProfesores();
            CargarClases();

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // define una sola vez las columnas de las dos grillas
        private void ConfigurarGrillas()
        {
            dgvClases.AutoGenerateColumns = false;
            dgvClases.Columns.Clear();
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoClase", DataPropertyName = "CodigoClase" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoClase", DataPropertyName = "TipoClase" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "Dias", DataPropertyName = "Dias" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "HorarioInicio", DataPropertyName = "HorarioInicio", DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" } });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "HorarioFin", DataPropertyName = "HorarioFin", DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" } });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "LegajoProfesor", DataPropertyName = "LegajoProfesor" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreProfesor", DataPropertyName = "NombreProfesor" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaAsignacion", DataPropertyName = "FechaAsignacion", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });

            dgvAsignados.AutoGenerateColumns = false;
            dgvAsignados.Columns.Clear();
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "LegajoProfesor", DataPropertyName = "LegajoProfesor" });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "NombreProfesor", DataPropertyName = "NombreProfesor" });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoClase", DataPropertyName = "CodigoClase" });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoClase", DataPropertyName = "TipoClase" });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Dias", DataPropertyName = "Dias" });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "HorarioInicio", DataPropertyName = "HorarioInicio", DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" } });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "HorarioFin", DataPropertyName = "HorarioFin", DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" } });
            dgvAsignados.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaAsignacion", DataPropertyName = "FechaAsignacion", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
        }

        // llena el combo con todos los profesores que hay cargados
        private void CargarProfesores()
        {
            _listProfesores = _bllProfesor.obtenerTodos();

            cmbProfesor.Items.Clear();

            foreach (Profesor13M profesor in _listProfesores)
            {
                cmbProfesor.Items.Add(profesor);
            }

            if (cmbProfesor.Items.Count > 0)
            {
                cmbProfesor.SelectedIndex = 0;
            }
        }

        // carga las clases en la grilla general, en la de asignados y en el combo
        private void CargarClases()
        {
            _listClases = _bllClase.obtenerTodos();

            dgvClases.DataSource = null;
            dgvClases.DataSource = _listClases;

            dgvAsignados.DataSource = null;
            dgvAsignados.DataSource = _listClases.Where(c => c.LegajoProfesor.HasValue)
                                                 .OrderBy(c => c.NombreProfesor)
                                                 .ThenBy(c => c.CodigoClase)
                                                 .ToList();

            cmbClase.Items.Clear();

            foreach (Clase13M clase in _listClases)
            {
                cmbClase.Items.Add(clase);
            }

            if (cmbClase.Items.Count > 0)
            {
                cmbClase.SelectedIndex = 0;
            }
        }

        // muestra los datos de la clase elegida y, si ya tiene profesor, lo marca en el combo
        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clase13M clase = cmbClase.SelectedItem as Clase13M;

            if (clase == null)
            {
                return;
            }

            txtFechaAsignacion.Text = clase.FechaAsignacion.HasValue ? clase.FechaAsignacion.Value.ToString("dd/MM/yyyy HH:mm") : "";

            if (clase.LegajoProfesor.HasValue)
            {
                Profesor13M asignado = _listProfesores.Find(p => p.LegajoProfesor == clase.LegajoProfesor.Value);

                if (asignado != null)
                {
                    cmbProfesor.SelectedItem = asignado;
                }
            }
        }

        // muestra la especialidad y la franja horaria del profesor elegido
        private void cmbProfesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Profesor13M profesor = cmbProfesor.SelectedItem as Profesor13M;

            txtEspecialidad.Text = profesor != null ? profesor.Especialidad : "";
            txtFranja.Text = profesor != null ? profesor.FranjaHorariaDisponible : "";
        }

        // al tocar una fila de cualquier grilla se selecciona esa clase en el combo
        private void dgvClases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarClaseDeGrilla(dgvClases, e);
        }

        private void dgvAsignados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeleccionarClaseDeGrilla(dgvAsignados, e);
        }

        private void SeleccionarClaseDeGrilla(DataGridView grilla, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Clase13M clase = (Clase13M)grilla.Rows[e.RowIndex].DataBoundItem;

            if (clase != null)
            {
                cmbClase.SelectedItem = clase;
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (cmbClase.SelectedItem == null)
            {
                MessageBox.Show(t.Translate("AsignarProfesorClase19M.msgSeleccionarClase"));
                return;
            }

            if (cmbProfesor.SelectedItem == null)
            {
                MessageBox.Show(t.Translate("AsignarProfesorClase19M.msgSeleccionarProfesor"));
                return;
            }

            int codigoClase = ((Clase13M)cmbClase.SelectedItem).CodigoClase;
            int legajoProfesor = ((Profesor13M)cmbProfesor.SelectedItem).LegajoProfesor;

            try
            {
                if (_bllClase.AsignarProfesorAClase(codigoClase, legajoProfesor))
                {
                    MessageBox.Show(t.Translate("AsignarProfesorClase19M.msgAsignacionExitosa"));
                }
                else
                {
                    MessageBox.Show(t.Translate("AsignarProfesorClase19M.msgProfesorYaAsignado"));
                }

                CargarClases();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("AsignarProfesorClase19M.msgError") + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (cmbClase.Items.Count > 0)
            {
                cmbClase.SelectedIndex = 0;
            }

            if (cmbProfesor.Items.Count > 0)
            {
                cmbProfesor.SelectedIndex = 0;
            }

            txtFechaAsignacion.Text = "";
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("AsignarProfesorClase19M.formTitle");
            lblTitulo.Text = t.Translate("AsignarProfesorClase19M.lblTitulo");
            lblLogoApp.Text = t.Translate("AsignarProfesorClase19M.lblLogoApp");
            gbClases.Text = t.Translate("AsignarProfesorClase19M.gbClases");
            gbAsignados.Text = t.Translate("AsignarProfesorClase19M.gbAsignados");
            gbAsignacion.Text = t.Translate("AsignarProfesorClase19M.gbAsignacion");
            labelClase.Text = t.Translate("AsignarProfesorClase19M.labelClase");
            labelProfesor.Text = t.Translate("AsignarProfesorClase19M.labelProfesor");
            labelEspecialidad.Text = t.Translate("AsignarProfesorClase19M.labelEspecialidad");
            labelFranja.Text = t.Translate("AsignarProfesorClase19M.labelFranja");
            labelFechaAsignacion.Text = t.Translate("AsignarProfesorClase19M.labelFechaAsignacion");
            btnAsignar.Text = t.Translate("AsignarProfesorClase19M.btnAsignar");
            btnCancelar.Text = t.Translate("AsignarProfesorClase19M.btnCancelar");

            dgvClases.Columns["CodigoClase"].HeaderText = t.Translate("AsignarProfesorClase19M.colCodigo");
            dgvClases.Columns["TipoClase"].HeaderText = t.Translate("AsignarProfesorClase19M.colTipoClase");
            dgvClases.Columns["Dias"].HeaderText = t.Translate("AsignarProfesorClase19M.colDias");
            dgvClases.Columns["HorarioInicio"].HeaderText = t.Translate("AsignarProfesorClase19M.colHorarioInicio");
            dgvClases.Columns["HorarioFin"].HeaderText = t.Translate("AsignarProfesorClase19M.colHorarioFin");
            dgvClases.Columns["LegajoProfesor"].HeaderText = t.Translate("AsignarProfesorClase19M.colLegajoProfesor");
            dgvClases.Columns["NombreProfesor"].HeaderText = t.Translate("AsignarProfesorClase19M.colNombreProfesor");
            dgvClases.Columns["FechaAsignacion"].HeaderText = t.Translate("AsignarProfesorClase19M.colFechaAsignacion");

            dgvAsignados.Columns["LegajoProfesor"].HeaderText = t.Translate("AsignarProfesorClase19M.colLegajoProfesor");
            dgvAsignados.Columns["NombreProfesor"].HeaderText = t.Translate("AsignarProfesorClase19M.colNombreProfesor");
            dgvAsignados.Columns["CodigoClase"].HeaderText = t.Translate("AsignarProfesorClase19M.colCodigo");
            dgvAsignados.Columns["TipoClase"].HeaderText = t.Translate("AsignarProfesorClase19M.colTipoClase");
            dgvAsignados.Columns["Dias"].HeaderText = t.Translate("AsignarProfesorClase19M.colDias");
            dgvAsignados.Columns["HorarioInicio"].HeaderText = t.Translate("AsignarProfesorClase19M.colHorarioInicio");
            dgvAsignados.Columns["HorarioFin"].HeaderText = t.Translate("AsignarProfesorClase19M.colHorarioFin");
            dgvAsignados.Columns["FechaAsignacion"].HeaderText = t.Translate("AsignarProfesorClase19M.colFechaAsignacion");
        }

        private void AsignarProfesorClase19M_Load(object sender, EventArgs e)
        {

        }
    }
}
