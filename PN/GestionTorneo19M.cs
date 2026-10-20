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
    public partial class GestionTorneo19M : Form, IIdiomaObserver
    {
        BLLTorneo13M _bllTorneo = new BLLTorneo13M();
        List<Torneo13M> _listTorneos = new List<Torneo13M>();
        List<Prueba13M> _listPruebas = new List<Prueba13M>();
        List<string> _categorias = new List<string>();

        // este enum es para que el boton guardar sepa si esta creando o modificando.
        // segun el boton que apretemos (agregar o modificar) se pone un modo. 
        private enum ModoOperacion
        {
            Ninguno,
            Crear,
            Modificar
        }

        private ModoOperacion modoActual = ModoOperacion.Ninguno;

        public GestionTorneo19M()
        {
            InitializeComponent();
            CargarCategorias();
            CargarPruebas();
            CargarGrilla();

            gbDatos.Visible = false;
            btnCancelar.Enabled = false;

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // arma la lista de categorias de natacion que se pueden habilitar en un torneo
        private void CargarCategorias()
        {
            _categorias.AddRange(new string[] { "Infantil", "Menor", "Cadete", "Juvenil", "Junior", "Senior" });

            clbCategorias.Items.AddRange(_categorias.Cast<object>().ToArray());
        }

        // arma la lista de las 20 pruebas del catalogo
        private void CargarPruebas()
        {
            _listPruebas = _bllTorneo.obtenerTodasLasPruebas();

            clbPruebas.Items.Clear();

            foreach (Prueba13M prueba in _listPruebas)
            {
                clbPruebas.Items.Add(prueba);
            }
        }

        // llena la grilla con todos los torneos
        private void CargarGrilla()
        {
            dgvTorneos.AutoGenerateColumns = false;

            dgvTorneos.Columns.Clear();

            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoTorneo", DataPropertyName = "CodigoTorneo", HeaderText = "Codigo" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sede", DataPropertyName = "Sede", HeaderText = "Sede" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Arancel", DataPropertyName = "Arancel", HeaderText = "Arancel" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categorias", DataPropertyName = "Categorias", HeaderText = "Categorias" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado" });

            dgvTorneos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvTorneos.Columns["Arancel"].DefaultCellStyle.Format = "C2";

            _listTorneos = _bllTorneo.obtenerTodos();

            dgvTorneos.DataSource = _listTorneos;

            actualizarIdioma();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtSede.Clear();

            dtpFecha.Value = DateTime.Today.AddMonths(1);
            nudArancel.Value = 0;

            DesmarcarTodo(clbCategorias);
            DesmarcarTodo(clbPruebas);
        }

        private void DesmarcarTodo(CheckedListBox lista)
        {
            for (int i = 0; i < lista.Items.Count; i++)
            {
                lista.SetItemChecked(i, false);
            }
        }

        // este es para cuando se aprete el boton modificar se seleccionen todos los chekcbox
        private void MarcarPorTexto(CheckedListBox lista, string valores)
        {
            DesmarcarTodo(lista);

            string[] separado = valores.Split(',');

            for (int i = 0; i < lista.Items.Count; i++)
            {
                string texto = lista.Items[i].ToString();

                if (separado.Any(v => v.Trim().Equals(texto, StringComparison.OrdinalIgnoreCase)))
                {
                    lista.SetItemChecked(i, true);
                }
            }
        }

        // pone en los campos los datos del torneo seleccionado en la grilla
        private void CargarTorneoEnCampos(Torneo13M torneo)
        {
            txtNombre.Text = torneo.Nombre;
            dtpFecha.Value = torneo.Fecha;
            txtSede.Text = torneo.Sede;
            nudArancel.Value = torneo.Arancel;

            MarcarPorTexto(clbCategorias, torneo.Categorias);
            MarcarPruebasDelTorneo(torneo.CodigoTorneo);
        }

        // marca en la lista de pruebas solo las que tiene habilitadas el torneo
        private void MarcarPruebasDelTorneo(int codigoTorneo)
        {
            DesmarcarTodo(clbPruebas);

            List<int> pruebasHabilitadas = _bllTorneo.obtenerIdsPruebasDelTorneo(codigoTorneo);

            for (int i = 0; i < clbPruebas.Items.Count; i++)
            {
                Prueba13M prueba = (Prueba13M)clbPruebas.Items[i];

                if (pruebasHabilitadas.Contains(prueba.IdPrueba))
                {
                    clbPruebas.SetItemChecked(i, true);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacion.Crear;
            gbDatos.Visible = true;

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;

            LimpiarCampos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvTorneos.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("GestionTorneo19M.msgSeleccionarTorneo"));
                return;
            }

            Torneo13M torneo = (Torneo13M)dgvTorneos.CurrentRow.DataBoundItem;

            modoActual = ModoOperacion.Modificar;
            gbDatos.Visible = true;

            CargarTorneoEnCampos(torneo);

            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnCancelar.Enabled = true;
        }

        private void dgvTorneos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Torneo13M torneo = (Torneo13M)dgvTorneos.Rows[e.RowIndex].DataBoundItem;

            CargarTorneoEnCampos(torneo);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (dgvTorneos.CurrentRow == null)
            {
                MessageBox.Show(t.Translate("GestionTorneo19M.msgSeleccionarTorneo"));
                return;
            }

            Torneo13M torneo = (Torneo13M)dgvTorneos.CurrentRow.DataBoundItem;

            DialogResult r = MessageBox.Show(
                t.Translate("GestionTorneo19M.msgConfirmarEliminar"),
                t.Translate("GestionTorneo19M.msgConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _bllTorneo.EliminarTorneo(torneo.CodigoTorneo);

                MessageBox.Show(t.Translate("GestionTorneo19M.msgTorneoEliminado"));

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("GestionTorneo19M.msgError") + ex.Message);
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

                string nombre = txtNombre.Text.Trim();
                string sede = txtSede.Text.Trim();
                DateTime fecha = dtpFecha.Value.Date;
                decimal arancel = nudArancel.Value;

                List<string> categoriasElegidas = ObtenerMarcados(clbCategorias);
                List<int> pruebasElegidas = ObtenerIdsPruebasMarcadas();

                // validaciones antes de guardar
                if (nombre.Length <= 0 || sede.Length <= 0)
                {
                    MessageBox.Show(t.Translate("GestionTorneo19M.msgCamposObligatorios"));
                    return;
                }
                if (categoriasElegidas.Count <= 0)
                {
                    MessageBox.Show(t.Translate("GestionTorneo19M.msgFaltaCategoria"));
                    return;
                }
                if (pruebasElegidas.Count <= 0)
                {
                    MessageBox.Show(t.Translate("GestionTorneo19M.msgFaltaPrueba"));
                    return;
                }

                string categorias = string.Join(",", categoriasElegidas);

                if (modoActual == ModoOperacion.Crear)
                {
                    _bllTorneo.CrearTorneo(nombre, fecha, sede, arancel, categorias, pruebasElegidas);

                    MessageBox.Show(t.Translate("GestionTorneo19M.msgTorneoCreado"));
                }
                else if (modoActual == ModoOperacion.Modificar)
                {
                    Torneo13M torneo = (Torneo13M)dgvTorneos.CurrentRow.DataBoundItem;

                    _bllTorneo.ModificarTorneo(torneo.CodigoTorneo, nombre, fecha, sede, arancel, categorias, pruebasElegidas);

                    MessageBox.Show(t.Translate("GestionTorneo19M.msgTorneoModificado"));
                }

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("GestionTorneo19M.msgError") + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearFormulario();
        }

        private List<string> ObtenerMarcados(CheckedListBox lista)
        {
            List<string> marcados = new List<string>();

            for (int i = 0; i < lista.Items.Count; i++)
            {
                //este if agarra los Checkbox que estan tildados y los anade a la lista
                if (lista.GetItemChecked(i)) 
                {
                    marcados.Add(lista.Items[i].ToString());
                }
            }
            return marcados;
        }

        // este devuelve los codigos de las pruebas que estan tildadas en la lista
        private List<int> ObtenerIdsPruebasMarcadas()
        {
            List<int> marcados = new List<int>();

            for (int i = 0; i < clbPruebas.Items.Count; i++)
            {
                if (clbPruebas.GetItemChecked(i))
                {
                    marcados.Add(((Prueba13M)clbPruebas.Items[i]).IdPrueba);
                }
            }
            return marcados;
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

            this.Text = t.Translate("GestionTorneo19M.formTitle");
            lblTitulo.Text = t.Translate("GestionTorneo19M.lblTitulo");
            lblLogoApp.Text = t.Translate("GestionTorneo19M.lblLogoApp");
            gbTorneos.Text = t.Translate("GestionTorneo19M.gbTorneos");
            gbDatos.Text = t.Translate("GestionTorneo19M.gbDatos");
            labelNombre.Text = t.Translate("GestionTorneo19M.labelNombre");
            labelFecha.Text = t.Translate("GestionTorneo19M.labelFecha");
            labelSede.Text = t.Translate("GestionTorneo19M.labelSede");
            labelArancel.Text = t.Translate("GestionTorneo19M.labelArancel");
            labelCategorias.Text = t.Translate("GestionTorneo19M.labelCategorias");
            labelPruebas.Text = t.Translate("GestionTorneo19M.labelPruebas");
            btnAgregar.Text = t.Translate("GestionTorneo19M.btnAgregar");
            btnModificar.Text = t.Translate("GestionTorneo19M.btnModificar");
            btnEliminar.Text = t.Translate("GestionTorneo19M.btnEliminar");
            btnGuardar.Text = t.Translate("GestionTorneo19M.btnGuardar");
            btnCancelar.Text = t.Translate("GestionTorneo19M.btnCancelar");

            if (dgvTorneos.Columns.Count > 0)
            {
                dgvTorneos.Columns["CodigoTorneo"].HeaderText = t.Translate("GestionTorneo19M.colCodigo");
                dgvTorneos.Columns["Nombre"].HeaderText = t.Translate("GestionTorneo19M.colNombre");
                dgvTorneos.Columns["Fecha"].HeaderText = t.Translate("GestionTorneo19M.colFecha");
                dgvTorneos.Columns["Sede"].HeaderText = t.Translate("GestionTorneo19M.colSede");
                dgvTorneos.Columns["Arancel"].HeaderText = t.Translate("GestionTorneo19M.colArancel");
                dgvTorneos.Columns["Categorias"].HeaderText = t.Translate("GestionTorneo19M.colCategorias");
                dgvTorneos.Columns["Estado"].HeaderText = t.Translate("GestionTorneo19M.colEstado");
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
