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
    public partial class RegistrarClase19M : Form, IIdiomaObserver
    {
        BLLClase13M _bllClase = new BLLClase13M();
        List<Clase13M> _listClases = new List<Clase13M>();

        // un enum para que el boton guardar sepa si esta creando o modificando
        private enum ModoOperacion
        {
            Ninguno,
            Crear,
            Modificar
        }

        private ModoOperacion modoActual = ModoOperacion.Ninguno;

        public RegistrarClase19M()
        {
            InitializeComponent();
            CargarTiposClase();
            CargarNiveles();
            CargarDias();
            CargarGrilla();

            gbDatos.Visible = false;
            btnCancelar.Enabled = false;

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // arma la lista de tipos de clase que aparecen en el combo
        private void CargarTiposClase()
        {
            cmbTipoClase.Items.AddRange(new object[] { "Recreativa", "Competitiva" });
            cmbTipoClase.SelectedIndex = 0;
        }

        // arma la lista de niveles que aparecen en el combo
        private void CargarNiveles()
        {
            cmbNivel.Items.AddRange(new object[] { "Inicial", "Intermedio", "Avanzado" });
            cmbNivel.SelectedIndex = 0;
        }

        // arma la lista de dias de la semana para tildar
        private void CargarDias()
        {
            clbDias.Items.AddRange(new object[] { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado", "Domingo" });
        }

        // llena la grilla con todas las clases
        private void CargarGrilla()
        {
            dgvClases.AutoGenerateColumns = false;

            dgvClases.Columns.Clear();

            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoClase", DataPropertyName = "CodigoClase", HeaderText = "Codigo" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "TipoClase", DataPropertyName = "TipoClase", HeaderText = "Tipo de clase" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "NivelDificultad", DataPropertyName = "NivelDificultad", HeaderText = "Nivel de dificultad" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "Dias", DataPropertyName = "Dias", HeaderText = "Dias" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "HorarioInicio", DataPropertyName = "HorarioInicio", HeaderText = "Horario de inicio", DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" } });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "HorarioFin", DataPropertyName = "HorarioFin", HeaderText = "Horario de fin", DefaultCellStyle = new DataGridViewCellStyle { Format = "hh\\:mm" } });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "EdadMinima", DataPropertyName = "EdadMinima", HeaderText = "Edad minima" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "EdadMaxima", DataPropertyName = "EdadMaxima", HeaderText = "Edad maxima" });
            dgvClases.Columns.Add(new DataGridViewTextBoxColumn { Name = "CupoMaximo", DataPropertyName = "CupoMaximo", HeaderText = "Cupo maximo" });

            _listClases = _bllClase.obtenerTodos();

            dgvClases.DataSource = _listClases;

            actualizarIdioma();
        }

        private void LimpiarCampos()
        {
            cmbTipoClase.SelectedIndex = 0;
            cmbNivel.SelectedIndex = 0;

            for (int i = 0; i < clbDias.Items.Count; i++)
            {
                clbDias.SetItemChecked(i, false);
            }

            dtpHorarioInicio.Value = DateTime.Today.AddHours(8);
            dtpHorarioFin.Value = DateTime.Today.AddHours(9);
            numEdadMinima.Value = 6;
            numEdadMaxima.Value = 12;
            numCupo.Value = 20;
        }

        private void HabilitarCampos(bool habilitado)
        {
            cmbTipoClase.Enabled = habilitado;
            cmbNivel.Enabled = habilitado;
            clbDias.Enabled = habilitado;
            dtpHorarioInicio.Enabled = habilitado;
            dtpHorarioFin.Enabled = habilitado;
            numEdadMinima.Enabled = habilitado;
            numEdadMaxima.Enabled = habilitado;
            numCupo.Enabled = habilitado;
        }

        // arma el texto de los dias tildados separados por coma tipo asi: Lunes,Miercoles
        private string ObtenerDiasSeleccionados()
        {
            List<string> seleccionados = new List<string>();

            foreach (object item in clbDias.CheckedItems)
            {
                seleccionados.Add(item.ToString());
            }

            return string.Join(",", seleccionados);
        }

        // tilda los dias que vinieron guardados en el texto de la clase
        private void MarcarDias(string dias)
        {
            for (int i = 0; i < clbDias.Items.Count; i++)
            {
                clbDias.SetItemChecked(i, false);
            }

            if (string.IsNullOrEmpty(dias))
            {
                return;
            }

            string[] partes = dias.Split(',');

            for (int i = 0; i < clbDias.Items.Count; i++)
            {
                if (partes.Contains(clbDias.Items[i].ToString()))
                {
                    clbDias.SetItemChecked(i, true);
                }
            }
        }

        // pone en los controles los datos de la clase seleccionada en la grilla
        private void CargarClaseEnCampos(Clase13M clase)
        {
            cmbTipoClase.SelectedItem = clase.TipoClase;
            cmbNivel.SelectedItem = clase.NivelDificultad;
            MarcarDias(clase.Dias);
            dtpHorarioInicio.Value = DateTime.Today.Add(clase.HorarioInicio);
            dtpHorarioFin.Value = DateTime.Today.Add(clase.HorarioFin);
            numEdadMinima.Value = clase.EdadMinima;
            numEdadMaxima.Value = clase.EdadMaxima;
            numCupo.Value = clase.CupoMaximo;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacion.Crear;
            gbDatos.Visible = true;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;

            HabilitarCampos(true);

            LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvClases.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("RegistrarClase19M.msgSeleccionarClase"));
                return;
            }

            modoActual = ModoOperacion.Modificar;
            gbDatos.Visible = true;

            Clase13M clase = (Clase13M)dgvClases.CurrentRow.DataBoundItem;
            CargarClaseEnCampos(clase);

            HabilitarCampos(true);

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void dgvClases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Clase13M clase = (Clase13M)dgvClases.Rows[e.RowIndex].DataBoundItem;

            CargarClaseEnCampos(clase);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvClases.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("RegistrarClase19M.msgSeleccionarClase"));
                return;
            }

            int codigoClase = Convert.ToInt32(dgvClases.CurrentRow.Cells["CodigoClase"].Value);

            DialogResult r = MessageBox.Show(
                t.Translate("RegistrarClase19M.msgConfirmarEliminar"),
                t.Translate("RegistrarClase19M.msgConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (r != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _bllClase.EliminarClase(codigoClase);

                MessageBox.Show(t.Translate("RegistrarClase19M.msgClaseEliminada"));

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("RegistrarClase19M.msgError") + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            try
            {
                if (modoActual == ModoOperacion.Ninguno)
                {
                    return;
                }

                string tipoClase = cmbTipoClase.SelectedItem != null ? cmbTipoClase.SelectedItem.ToString() : "";
                string nivel = cmbNivel.SelectedItem != null ? cmbNivel.SelectedItem.ToString() : "";
                string dias = ObtenerDiasSeleccionados();
                TimeSpan horarioInicio = dtpHorarioInicio.Value.TimeOfDay;
                TimeSpan horarioFin = dtpHorarioFin.Value.TimeOfDay;
                int edadMinima = Convert.ToInt32(numEdadMinima.Value);
                int edadMaxima = Convert.ToInt32(numEdadMaxima.Value);
                int cupo = Convert.ToInt32(numCupo.Value);

                // validaciones antes de guardar por las dudas
                if (tipoClase.Length <= 0 || nivel.Length <= 0 || dias.Length <= 0)
                {
                    MessageBox.Show(t.Translate("RegistrarClase19M.msgCamposObligatorios"));
                    return;
                }

                if (modoActual == ModoOperacion.Crear)
                {
                    _bllClase.CrearClase(tipoClase, nivel, dias, horarioInicio, horarioFin, edadMinima, edadMaxima, cupo);

                    MessageBox.Show(t.Translate("RegistrarClase19M.msgClaseCreada"));
                }
                else if (modoActual == ModoOperacion.Modificar)
                {
                    int codigoClase = Convert.ToInt32(dgvClases.CurrentRow.Cells["CodigoClase"].Value);

                    _bllClase.ModificarClase(codigoClase, tipoClase, nivel, dias, horarioInicio, horarioFin, edadMinima, edadMaxima, cupo);

                    MessageBox.Show(t.Translate("RegistrarClase19M.msgClaseModificada"));
                }
                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("RegistrarClase19M.msgError") + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearFormulario();
        }

        private void ResetearFormulario()
        {
            modoActual = ModoOperacion.Ninguno;
            gbDatos.Visible = false;

            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = false;

            LimpiarCampos();
            CargarGrilla();
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("RegistrarClase19M.formTitle");
            lblTitulo.Text = t.Translate("RegistrarClase19M.lblTitulo");
            lblLogoApp.Text = t.Translate("RegistrarClase19M.lblLogoApp");
            gbClases.Text = t.Translate("RegistrarClase19M.gbClases");
            gbDatos.Text = t.Translate("RegistrarClase19M.gbDatos");
            labelTipoClase.Text = t.Translate("RegistrarClase19M.labelTipoClase");
            labelNivel.Text = t.Translate("RegistrarClase19M.labelNivel");
            labelDias.Text = t.Translate("RegistrarClase19M.labelDias");
            labelHorarioInicio.Text = t.Translate("RegistrarClase19M.labelHorarioInicio");
            labelHorarioFin.Text = t.Translate("RegistrarClase19M.labelHorarioFin");
            labelEdadMinima.Text = t.Translate("RegistrarClase19M.labelEdadMinima");
            labelEdadMaxima.Text = t.Translate("RegistrarClase19M.labelEdadMaxima");
            labelCupo.Text = t.Translate("RegistrarClase19M.labelCupo");
            btnAgregar.Text = t.Translate("RegistrarClase19M.btnAgregar");
            btnModificar.Text = t.Translate("RegistrarClase19M.btnModificar");
            btnEliminar.Text = t.Translate("RegistrarClase19M.btnEliminar");
            btnGuardar.Text = t.Translate("RegistrarClase19M.btnGuardar");
            btnCancelar.Text = t.Translate("RegistrarClase19M.btnCancelar");

            if (dgvClases.Columns.Count > 0)
            {
                dgvClases.Columns["CodigoClase"].HeaderText = t.Translate("RegistrarClase19M.colCodigo");
                dgvClases.Columns["TipoClase"].HeaderText = t.Translate("RegistrarClase19M.colTipoClase");
                dgvClases.Columns["NivelDificultad"].HeaderText = t.Translate("RegistrarClase19M.colNivel");
                dgvClases.Columns["Dias"].HeaderText = t.Translate("RegistrarClase19M.colDias");
                dgvClases.Columns["HorarioInicio"].HeaderText = t.Translate("RegistrarClase19M.colHorarioInicio");
                dgvClases.Columns["HorarioFin"].HeaderText = t.Translate("RegistrarClase19M.colHorarioFin");
                dgvClases.Columns["EdadMinima"].HeaderText = t.Translate("RegistrarClase19M.colEdadMinima");
                dgvClases.Columns["EdadMaxima"].HeaderText = t.Translate("RegistrarClase19M.colEdadMaxima");
                dgvClases.Columns["CupoMaximo"].HeaderText = t.Translate("RegistrarClase19M.colCupo");
            }
        }

        private void RegistrarClase19M_Load(object sender, EventArgs e)
        {

        }
    }
}
