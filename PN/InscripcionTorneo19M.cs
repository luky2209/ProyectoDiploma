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
    public partial class InscripcionTorneo19M : Form, IIdiomaObserver
    {
        BLLInscripcion13M _bllInscripcion = new BLLInscripcion13M();
        BLLTorneo13M _bllTorneo = new BLLTorneo13M();
        BLLNadador13M _bllNadador = new BLLNadador13M();
        List<Inscripcion13M> _listInscripciones = new List<Inscripcion13M>();
        List<Nadador13M> _listNadadores = new List<Nadador13M>();
        List<Torneo13M> _listTorneos = new List<Torneo13M>();

        // un enum para que el boton guardar sepa si esta creando o modificando
        private enum ModoOperacion
        {
            Ninguno,
            Crear,
            Modificar
        }

        private ModoOperacion modoActual = ModoOperacion.Ninguno;

        public InscripcionTorneo19M()
        {
            InitializeComponent();
            CargarNadadores();
            CargarTorneos();
            CargarGrilla();

            btnCancelar.Enabled = false;

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // llena el combo con todos los nadadores
        private void CargarNadadores()
        {
            _listNadadores = _bllNadador.obtenerTodos();

            cmbNadador.Items.Clear();

            foreach (Nadador13M nadador in _listNadadores)
            {
                cmbNadador.Items.Add(nadador);
            }
        }

        // llena el combo con los torneos que todavia se pueden usar para inscribir
        private void CargarTorneos()
        {
            _listTorneos = _bllTorneo.obtenerAbiertos();

            cmbTorneo.Items.Clear();

            foreach (Torneo13M torneo in _listTorneos)
            {
                cmbTorneo.Items.Add(torneo);
            }

            if (cmbTorneo.Items.Count > 0)
            {
                cmbTorneo.SelectedIndex = 0;
            }
        }

        // cuando se elige un torneo se muestran su fecha, su estado y sus pruebas
        private void cmbTorneo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Torneo13M torneo = cmbTorneo.SelectedItem as Torneo13M;

            if (torneo == null)
            {
                return;
            }

            txtFechaTorneo.Text = torneo.Fecha.ToString("dd/MM/yyyy");
            txtEstadoTorneo.Text = torneo.Estado;
            txtCategoriasTorneo.Text = torneo.Categorias.Replace(",", ", ");

            CargarPruebasDelTorneo(torneo.CodigoTorneo);
            CargarGrilla();
        }

        // llena el combo de pruebas con las que tiene habilitadas el torneo elegido
        private void CargarPruebasDelTorneo(int codigoTorneo)
        {
            cmbPrueba.Items.Clear();

            foreach (Prueba13M prueba in _bllTorneo.obtenerPruebasDelTorneo(codigoTorneo))
            {
                cmbPrueba.Items.Add(prueba);
            }

            if (cmbPrueba.Items.Count > 0)
            {
                cmbPrueba.SelectedIndex = 0;
            }
        }

        // cuando elegis un nadador se muestra su categoria
        private void cmbNadador_SelectedIndexChanged(object sender, EventArgs e)
        {
            Nadador13M nadador = cmbNadador.SelectedItem as Nadador13M;

            txtCategoriaNadador.Text = nadador != null ? nadador.Categoria : "";
        }

        // llena la grilla con las inscripciones del torneo elegido
        private void CargarGrilla()
        {
            dgvInscripciones.AutoGenerateColumns = false;

            dgvInscripciones.Columns.Clear();

            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeroInscripcion", DataPropertyName = "NumeroInscripcion", HeaderText = "Nro." });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNINadador", DataPropertyName = "DNINadador", HeaderText = "DNI" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "DescripcionPrueba", DataPropertyName = "DescripcionPrueba", HeaderText = "Prueba" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", DataPropertyName = "Categoria", HeaderText = "Categoria" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaInscripcion", DataPropertyName = "FechaInscripcion", HeaderText = "Fecha de inscripcion" });
            dgvInscripciones.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado" });

            dgvInscripciones.Columns["FechaInscripcion"].DefaultCellStyle.Format = "dd/MM/yyyy";

            Torneo13M torneo = cmbTorneo.SelectedItem as Torneo13M;

            _listInscripciones = torneo == null
                ? new List<Inscripcion13M>()
                : _bllInscripcion.obtenerPorTorneo(torneo.CodigoTorneo);

            dgvInscripciones.DataSource = _listInscripciones;

            actualizarIdioma();
        }

        // pone en los campos los datos de la inscripcion seleccionada
        private void CargarInscripcionEnCampos(Inscripcion13M inscripcion)
        {
            cmbNadador.SelectedItem = _listNadadores.Find(n => n.DNI == inscripcion.DNINadador);
            cmbPrueba.SelectedItem = _bllTorneo.obtenerPruebasDelTorneo(inscripcion.CodigoTorneo)
                .Find(p => p.IdPrueba == inscripcion.IdPrueba);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacion.Crear;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;

            cmbNadador.SelectedIndex = -1;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvInscripciones.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("InscripcionTorneo19M.msgSeleccionarInscripcion"));
                return;
            }

            modoActual = ModoOperacion.Modificar;

            Inscripcion13M inscripcion = (Inscripcion13M)dgvInscripciones.CurrentRow.DataBoundItem;
            CargarInscripcionEnCampos(inscripcion);

            // en una modificacion el nadador y el torneo no se pueden cambiar
            cmbNadador.Enabled = false;
            cmbTorneo.Enabled = false;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void dgvInscripciones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // toco la fila y se cargan los datos de la inscripcion en los campos
            Inscripcion13M inscripcion = (Inscripcion13M)dgvInscripciones.Rows[e.RowIndex].DataBoundItem;

            CargarInscripcionEnCampos(inscripcion);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvInscripciones.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("InscripcionTorneo19M.msgSeleccionarInscripcion"));
                return;
            }

            Inscripcion13M inscripcion = (Inscripcion13M)dgvInscripciones.CurrentRow.DataBoundItem;

            DialogResult r = MessageBox.Show(
                t.Translate("InscripcionTorneo19M.msgConfirmarEliminar"),
                t.Translate("InscripcionTorneo19M.msgConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _bllInscripcion.EliminarInscripcion(inscripcion.NumeroInscripcion);

                MessageBox.Show(t.Translate("InscripcionTorneo19M.msgInscripcionEliminada"));

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("InscripcionTorneo19M.msgError") + ex.Message);
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

                Nadador13M nadador = cmbNadador.SelectedItem as Nadador13M;
                Torneo13M torneo = cmbTorneo.SelectedItem as Torneo13M;
                Prueba13M prueba = cmbPrueba.SelectedItem as Prueba13M;

                // validaciones antes de guardar
                if (nadador == null || torneo == null || prueba == null)
                {
                    MessageBox.Show(t.Translate("InscripcionTorneo19M.msgCamposObligatorios"));
                    return;
                }

                if (modoActual == ModoOperacion.Crear)
                {
                    _bllInscripcion.CrearInscripcion(nadador.DNI, torneo.CodigoTorneo, prueba.IdPrueba);

                    MessageBox.Show(t.Translate("InscripcionTorneo19M.msgInscripcionCreada"));
                }
                else if (modoActual == ModoOperacion.Modificar)
                {
                    Inscripcion13M inscripcion = (Inscripcion13M)dgvInscripciones.CurrentRow.DataBoundItem;

                    _bllInscripcion.ModificarInscripcion(inscripcion.NumeroInscripcion, prueba.IdPrueba, inscripcion.Estado);

                    MessageBox.Show(t.Translate("InscripcionTorneo19M.msgInscripcionModificada"));
                }

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("InscripcionTorneo19M.msgError") + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearFormulario();
        }

        // Vuelve el formulario al estado inicial: sin modo activo y con la grilla renovada
        private void ResetearFormulario()
        {
            modoActual = ModoOperacion.Ninguno;

            cmbNadador.Enabled = true;
            cmbTorneo.Enabled = true;

            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = false;

            cmbNadador.SelectedIndex = -1;

            CargarGrilla();
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("InscripcionTorneo19M.formTitle");
            lblTitulo.Text = t.Translate("InscripcionTorneo19M.lblTitulo");
            lblLogoApp.Text = t.Translate("InscripcionTorneo19M.lblLogoApp");
            gbTorneo.Text = t.Translate("InscripcionTorneo19M.gbTorneo");
            labelTorneo.Text = t.Translate("InscripcionTorneo19M.labelTorneo");
            labelFechaTorneo.Text = t.Translate("InscripcionTorneo19M.labelFecha");
            labelEstadoTorneo.Text = t.Translate("InscripcionTorneo19M.labelEstadoTorneo");
            labelCategoriasTorneo.Text = t.Translate("InscripcionTorneo19M.labelCategoriasTorneo");
            gbInscripcion.Text = t.Translate("InscripcionTorneo19M.gbInscripcion");
            labelNadador.Text = t.Translate("InscripcionTorneo19M.labelNadador");
            labelCategoriaNadador.Text = t.Translate("InscripcionTorneo19M.labelCategoria");
            labelPrueba.Text = t.Translate("InscripcionTorneo19M.labelPrueba");
            gbInscripciones.Text = t.Translate("InscripcionTorneo19M.gbInscripciones");
            btnAgregar.Text = t.Translate("InscripcionTorneo19M.btnAgregar");
            btnModificar.Text = t.Translate("InscripcionTorneo19M.btnModificar");
            btnEliminar.Text = t.Translate("InscripcionTorneo19M.btnEliminar");
            btnGuardar.Text = t.Translate("InscripcionTorneo19M.btnGuardar");
            btnCancelar.Text = t.Translate("InscripcionTorneo19M.btnCancelar");

            if (dgvInscripciones.Columns.Count > 0)
            {
                dgvInscripciones.Columns["NumeroInscripcion"].HeaderText = t.Translate("InscripcionTorneo19M.colNumero");
                dgvInscripciones.Columns["DNINadador"].HeaderText = t.Translate("InscripcionTorneo19M.colDNI");
                dgvInscripciones.Columns["Nombre"].HeaderText = t.Translate("InscripcionTorneo19M.colNombre");
                dgvInscripciones.Columns["Apellido"].HeaderText = t.Translate("InscripcionTorneo19M.colApellido");
                dgvInscripciones.Columns["DescripcionPrueba"].HeaderText = t.Translate("InscripcionTorneo19M.colPrueba");
                dgvInscripciones.Columns["Categoria"].HeaderText = t.Translate("InscripcionTorneo19M.colCategoria");
                dgvInscripciones.Columns["FechaInscripcion"].HeaderText = t.Translate("InscripcionTorneo19M.colFecha");
                dgvInscripciones.Columns["Estado"].HeaderText = t.Translate("InscripcionTorneo19M.colEstado");
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
