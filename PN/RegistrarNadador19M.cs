using BE;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public partial class RegistrarNadador19M : Form, IIdiomaObserver
    {
        BLLNadador13M _bllNadador = new BLLNadador13M();
        List<Nadador13M> _listNadadores = new List<Nadador13M>();

        // un enum para que el botón guardar sepa si está creando o modificando
        private enum ModoOperacion
        {
            Ninguno,
            Crear,
            Modificar
        }

        private ModoOperacion modoActual = ModoOperacion.Ninguno;

        public RegistrarNadador19M()
        {
            InitializeComponent();
            CargarCategorias();
            CargarGrilla();

            gbDatos.Visible = false;
            btnCancelar.Enabled = false;

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // Arma la lista de categorías de natación que aparecen en el combo
        private void CargarCategorias()
        {
            cmbCategoria.Items.AddRange(new object[] { "Infantil", "Menor", "Cadete", "Juvenil", "Junior", "Senior" });
            cmbCategoria.SelectedIndex = 0;
        }

        // Llena la grilla con todos los nadadores del padrón
        private void CargarGrilla()
        {
            dgvNadadores.AutoGenerateColumns = false;

            dgvNadadores.Columns.Clear();

            dgvNadadores.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNI", DataPropertyName = "DNI", HeaderText = "DNI" });
            dgvNadadores.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvNadadores.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido" });
            dgvNadadores.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaNacimiento", DataPropertyName = "FechaNacimiento", HeaderText = "Fecha de nacimiento" });
            dgvNadadores.Columns.Add(new DataGridViewTextBoxColumn { Name = "Edad", DataPropertyName = "Edad", HeaderText = "Edad" });
            dgvNadadores.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", DataPropertyName = "Categoria", HeaderText = "Categoría" });
            dgvNadadores.Columns.Add(new DataGridViewCheckBoxColumn { Name = "CertificadoMedico", DataPropertyName = "CertificadoMedico", HeaderText = "Certificado médico" });

            dgvNadadores.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";

            _listNadadores = _bllNadador.obtenerTodos();

            dgvNadadores.DataSource = _listNadadores;

            actualizarIdioma();
        }

        private void LimpiarCampos()
        {
            txtDNI.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();

            dtpFechaNacimiento.Value = DateTime.Today.AddYears(-10);
            cmbCategoria.SelectedIndex = 0;
            chkCertificado.Checked = false;

            txtDNI.Focus();
        }

        // Habilita o deshabilita los campos del formulario de datos
        private void HabilitarCampos(bool habilitado)
        {
            txtNombre.Enabled = habilitado;
            txtApellido.Enabled = habilitado;
            txtEdad.Enabled = habilitado;
            dtpFechaNacimiento.Enabled = habilitado;
            cmbCategoria.Enabled = habilitado;
            chkCertificado.Enabled = habilitado;
        }

        // Pone en los textbox los datos del nadador seleccionado en la grilla
        private void CargarNadadorEnCampos(Nadador13M nadador)
        {
            txtDNI.Text = nadador.DNI;
            txtNombre.Text = nadador.Nombre;
            txtApellido.Text = nadador.Apellido;
            dtpFechaNacimiento.Value = nadador.FechaNacimiento;
            txtEdad.Text = nadador.Edad.ToString();
            cmbCategoria.SelectedItem = nadador.Categoria;
            chkCertificado.Checked = nadador.CertificadoMedico;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacion.Crear;
            gbDatos.Visible = true;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;

            txtDNI.Enabled = true;
            HabilitarCampos(true);

            LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvNadadores.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("RegistrarNadador19M.msgSeleccionarNadador"));
                return;
            }

            modoActual = ModoOperacion.Modificar;
            gbDatos.Visible = true;

            Nadador13M nadador = (Nadador13M)dgvNadadores.CurrentRow.DataBoundItem;
            CargarNadadorEnCampos(nadador);

            // en una modificación el DNI no se puede cambiar (es la identidad del nadador)
            txtDNI.Enabled = false;
            HabilitarCampos(true);

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void dgvNadadores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            // al tocar una fila se cargan los datos del nadador en los textbox
            Nadador13M nadador = (Nadador13M)dgvNadadores.Rows[e.RowIndex].DataBoundItem;

            CargarNadadorEnCampos(nadador);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvNadadores.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("RegistrarNadador19M.msgSeleccionarNadador"));
                return;
            }

            string dni = dgvNadadores.CurrentRow.Cells["DNI"].Value.ToString();

            DialogResult r = MessageBox.Show(
                t.Translate("RegistrarNadador19M.msgConfirmarEliminar"),
                t.Translate("RegistrarNadador19M.msgConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _bllNadador.EliminarNadador(dni);

                MessageBox.Show(t.Translate("RegistrarNadador19M.msgNadadorEliminado"));

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("RegistrarNadador19M.msgError") + ex.Message);
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

                string dni = txtDNI.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string edadTxt = txtEdad.Text.Trim();
                string categoria = cmbCategoria.SelectedItem != null ? cmbCategoria.SelectedItem.ToString() : "";
                DateTime fechaNacimiento = dtpFechaNacimiento.Value.Date;
                bool certificado = chkCertificado.Checked;

                // validaciones simples antes de guardar
                if (dni.Length <= 0 || nombre.Length <= 0 || apellido.Length <= 0 || edadTxt.Length <= 0 || categoria.Length <= 0)
                {
                    MessageBox.Show(t.Translate("RegistrarNadador19M.msgCamposObligatorios"));
                    return;
                }

                if (!EsDNIValido(dni))
                {
                    MessageBox.Show(t.Translate("RegistrarNadador19M.msgDNIInvalido"));
                    return;
                }

                if (!EsEdadValida(edadTxt))
                {
                    MessageBox.Show(t.Translate("RegistrarNadador19M.msgEdadInvalida"));
                    return;
                }

                if (fechaNacimiento >= DateTime.Today)
                {
                    MessageBox.Show(t.Translate("RegistrarNadador19M.msgFechaInvalida"));
                    return;
                }

                int edad = Convert.ToInt32(edadTxt);

                if (modoActual == ModoOperacion.Crear)
                {
                    _bllNadador.CrearNadador(dni, nombre, apellido, fechaNacimiento, edad, categoria, certificado);

                    MessageBox.Show(t.Translate("RegistrarNadador19M.msgNadadorCreado"));
                }
                else if (modoActual == ModoOperacion.Modificar)
                {
                    _bllNadador.ModificarNadador(dni, nombre, apellido, fechaNacimiento, edad, categoria, certificado);

                    MessageBox.Show(t.Translate("RegistrarNadador19M.msgNadadorModificado"));
                }

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("RegistrarNadador19M.msgError") + ex.Message);
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
            gbDatos.Visible = false;

            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = false;

            LimpiarCampos();
            CargarGrilla();
        }

        #region Validaciones
        private bool EsDNIValido(string dni)
        {
            return Regex.IsMatch(dni, @"^\d{7,8}$");
        }

        private bool EsEdadValida(string edad)
        {
            int valor;
            return int.TryParse(edad, out valor) && valor >= 1 && valor <= 120;
        }
        #endregion Validaciones

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("RegistrarNadador19M.formTitle");
            lblTitulo.Text = t.Translate("RegistrarNadador19M.lblTitulo");
            lblLogoApp.Text = t.Translate("RegistrarNadador19M.lblLogoApp");
            gbPadron.Text = t.Translate("RegistrarNadador19M.gbPadron");
            gbDatos.Text = t.Translate("RegistrarNadador19M.gbDatos");
            labelDNI.Text = t.Translate("RegistrarNadador19M.labelDNI");
            labelNombre.Text = t.Translate("RegistrarNadador19M.labelNombre");
            labelApellido.Text = t.Translate("RegistrarNadador19M.labelApellido");
            labelFechaNacimiento.Text = t.Translate("RegistrarNadador19M.labelFechaNacimiento");
            labelEdad.Text = t.Translate("RegistrarNadador19M.labelEdad");
            labelCategoria.Text = t.Translate("RegistrarNadador19M.labelCategoria");
            labelCertificado.Text = t.Translate("RegistrarNadador19M.labelCertificado");
            chkCertificado.Text = t.Translate("RegistrarNadador19M.chkCertificado");
            btnAgregar.Text = t.Translate("RegistrarNadador19M.btnAgregar");
            btnModificar.Text = t.Translate("RegistrarNadador19M.btnModificar");
            btnEliminar.Text = t.Translate("RegistrarNadador19M.btnEliminar");
            btnGuardar.Text = t.Translate("RegistrarNadador19M.btnGuardar");
            btnCancelar.Text = t.Translate("RegistrarNadador19M.btnCancelar");

            if (dgvNadadores.Columns.Count > 0)
            {
                dgvNadadores.Columns["DNI"].HeaderText = t.Translate("RegistrarNadador19M.colDNI");
                dgvNadadores.Columns["Nombre"].HeaderText = t.Translate("RegistrarNadador19M.colNombre");
                dgvNadadores.Columns["Apellido"].HeaderText = t.Translate("RegistrarNadador19M.colApellido");
                dgvNadadores.Columns["FechaNacimiento"].HeaderText = t.Translate("RegistrarNadador19M.colFechaNacimiento");
                dgvNadadores.Columns["Edad"].HeaderText = t.Translate("RegistrarNadador19M.colEdad");
                dgvNadadores.Columns["Categoria"].HeaderText = t.Translate("RegistrarNadador19M.colCategoria");
                dgvNadadores.Columns["CertificadoMedico"].HeaderText = t.Translate("RegistrarNadador19M.colCertificado");
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}