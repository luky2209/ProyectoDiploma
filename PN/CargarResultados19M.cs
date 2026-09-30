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
    public partial class CargarResultados19M : Form, IIdiomaObserver
    {
        BLLTorneo13M _bllTorneo = new BLLTorneo13M();
        BLLInscripcion13M _bllInscripcion = new BLLInscripcion13M();
        BLLResultado13M _bllResultado = new BLLResultado13M();
        List<Torneo13M> _listTorneos = new List<Torneo13M>();
        List<Resultado13M> _listResultados = new List<Resultado13M>();

        // mientras se llenan los campos del panel no se tiene que recalcular la vista previa
        bool _cargandoPanel = false;

        public CargarResultados19M()
        {
            InitializeComponent();
            CargarGrillaTorneos();

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // esto es para que aparezcan solo los torneos que siguen abiertos
        private void CargarGrillaTorneos()
        {
            dgvTorneos.AutoGenerateColumns = false;

            dgvTorneos.Columns.Clear();

            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoTorneo", DataPropertyName = "CodigoTorneo", HeaderText = "Codigo" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sede", DataPropertyName = "Sede", HeaderText = "Sede" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categorias", DataPropertyName = "Categorias", HeaderText = "Categorias" });

            dgvTorneos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

            _listTorneos = _bllTorneo.obtenerAbiertos();

            dgvTorneos.DataSource = _listTorneos;

            actualizarIdioma();
        }

        // este metodo es para agarrar todos los nadadores que estan en un torneo cuando le hago click
        private void dgvTorneos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Torneo13M torneo = (Torneo13M)dgvTorneos.Rows[e.RowIndex].DataBoundItem;

            CargarNadadoresInscriptos(torneo.CodigoTorneo);
        }
        // este lo mismo
        private void CargarNadadoresInscriptos(int codigoTorneo)
        {
            _listResultados = new List<Resultado13M>();

            foreach (Inscripcion13M inscripcion in _bllInscripcion.obtenerPorTorneo(codigoTorneo))
            {
                _listResultados.Add(new Resultado13M
                {
                    NumeroInscripcion = inscripcion.NumeroInscripcion,
                    DNINadador = inscripcion.DNINadador,
                    IdPrueba = inscripcion.IdPrueba,
                    Nombre = inscripcion.Nombre,
                    Apellido = inscripcion.Apellido,
                    Categoria = inscripcion.Categoria,
                    DescripcionPrueba = inscripcion.DescripcionPrueba,
                    Premio = ""
                });
            }
            CargarGrillaResultados();
        }

        private void CargarGrillaResultados()
        {
            dgvResultados.AutoGenerateColumns = false;

            dgvResultados.Columns.Clear();

            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNINadador", DataPropertyName = "DNINadador", HeaderText = "DNI", FillWeight = 65 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre", FillWeight = 100 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido", FillWeight = 100 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", DataPropertyName = "Categoria", HeaderText = "Categoria", FillWeight = 70 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "DescripcionPrueba", DataPropertyName = "DescripcionPrueba", HeaderText = "Prueba", FillWeight = 85 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tiempo", DataPropertyName = "TiempoOficial", HeaderText = "Tiempo", FillWeight = 90 });

            dgvResultados.DataSource = _listResultados;

            btnFinalizar.Enabled = _listResultados.Count > 0;

            if (_listResultados.Count == 0)
            {
                gbCargaTiempo.Enabled = false;
            }
            actualizarIdioma();
        }

        // cuando elegis un nadador se muestran sus tiempos en los text box
        private void dgvResultados_SelectionChanged(object sender, EventArgs e)
        {
            Resultado13M resultado = ObtenerResultadoSeleccionado();

            gbCargaTiempo.Enabled = true;

            // se congela la vista previa mientras se llenan los campos, para que no se recalcule a medio camino
            _cargandoPanel = true;

            chkDescalificado.Checked = resultado.Descalificado;

            // un descalificado deja el tiempo en cero, asi que no se muestra nada en los campos
            txtMinutos.Text = resultado.Descalificado ? "" : resultado.Minutos.ToString();
            txtSegundos.Text = resultado.Descalificado ? "" : resultado.Segundos.ToString("00");
            txtCentesimas.Text = resultado.Descalificado ? "" : resultado.Centesimas.ToString("00");

            _cargandoPanel = false;

            ActualizarVistaPrevia();
        }

        private Resultado13M ObtenerResultadoSeleccionado()
        {
            if (dgvResultados.CurrentRow == null || dgvResultados.CurrentRow.DataBoundItem == null)
            {
                return null;
            }
            return (Resultado13M)dgvResultados.CurrentRow.DataBoundItem;
        }

        // la vista previa arma el mismo texto que despues se ve en la grilla
        private void ActualizarVistaPrevia()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (chkDescalificado.Checked)
            {
                txtMinutos.Enabled = false;
                txtSegundos.Enabled = false;
                txtCentesimas.Enabled = false;
                lblTiempo.ForeColor = Color.FromArgb(192, 0, 0);
                lblTiempo.Text = t.Translate("CargarResultados19M.colDescalificado");
                return;
            }

            txtMinutos.Enabled = true;
            txtSegundos.Enabled = true;
            txtCentesimas.Enabled = true;
            lblTiempo.ForeColor = Color.FromArgb(0, 180, 216);

            int minutos, segundos, centesimas;

            if (int.TryParse(txtMinutos.Text, out minutos) &&
                int.TryParse(txtSegundos.Text, out segundos) &&
                int.TryParse(txtCentesimas.Text, out centesimas))
            {
                lblTiempo.Text = minutos + "'" + segundos.ToString("00") + "\"" + centesimas.ToString("00");
            }
            else
            {
                lblTiempo.Text = "--";
            }
        }

        private void txtTiempo_TextChanged(object sender, EventArgs e)
        {
            if (!_cargandoPanel)
            {
                ActualizarVistaPrevia();
            }
        }

        private void chkDescalificado_CheckedChanged(object sender, EventArgs e)
        {
            if (!_cargandoPanel)
            {
                ActualizarVistaPrevia();
            }
        }

        // el tiempo del panel se guarda en la fila elegida de la grilla
        private void btnAplicar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            Resultado13M resultado = ObtenerResultadoSeleccionado();

            if (resultado == null)
            {
                MessageBox.Show(t.Translate("CargarResultados19M.msgFaltaNadador"));
                return;
            }

            if (chkDescalificado.Checked)
            {
                resultado.Descalificado = true;
                resultado.Minutos = 0;
                resultado.Segundos = 0;
                resultado.Centesimas = 0;
            }
            else
            {
                int minutos, segundos, centesimas;

                // para que avise si el campo esta vacio o tiene algo que no es numero
                try
                {
                    minutos = Convert.ToInt32(txtMinutos.Text);
                    segundos = Convert.ToInt32(txtSegundos.Text);
                    centesimas = Convert.ToInt32(txtCentesimas.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(t.Translate("CargarResultados19M.msgTiempoIncompleto"));
                    return;
                }

                // para que no se pueda poner cualquier numero en el tiempo
                if (minutos < 0 || segundos < 0 || segundos > 59 || centesimas < 0 || centesimas > 99)
                {
                    MessageBox.Show(t.Translate("CargarResultados19M.msgTiempoInvalido"));
                    return;
                }

                resultado.Descalificado = false;
                resultado.Minutos = minutos;
                resultado.Segundos = segundos;
                resultado.Centesimas = centesimas;
            }

            dgvResultados.Refresh();

            ActualizarVistaPrevia();
        }

        // el descalificado se muestra como texto y no como un tiempo en cero
        private void dgvResultados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != dgvResultados.Columns["Tiempo"].Index)
            {
                return;
            }

            Resultado13M resultado = dgvResultados.Rows[e.RowIndex].DataBoundItem as Resultado13M;

            if (resultado == null)
            {
                return;
            }

            if (resultado.Descalificado)
            {
                e.Value = ServiceSessionManager13M.getIntancia().Idioma.Translate("CargarResultados19M.colDescalificado");
            }
            else
            {
                e.Value = resultado.TiempoOficial;
            }
        }

        // el boton guarda los tiempos, reparte las medallas y cierra el torneo
        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            Torneo13M torneo = (Torneo13M)dgvTorneos.CurrentRow.DataBoundItem;

            DialogResult r = MessageBox.Show(
                t.Translate("CargarResultados19M.msgConfirmar"),
                t.Translate("CargarResultados19M.msgConfirmarTitulo"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _bllResultado.FinalizarTorneo(torneo.CodigoTorneo, _listResultados);

                MessageBox.Show(t.Translate("CargarResultados19M.msgTorneoFinalizado"));

                ResetearFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("CargarResultados19M.msgError") + ex.Message);
            }
        }

        // el torneo cerrado desaparece de la grilla de arriba, asi que se recarga
        private void ResetearFormulario()
        {
            btnFinalizar.Enabled = false;

            _listResultados = new List<Resultado13M>();
            dgvResultados.DataSource = _listResultados;

            gbCargaTiempo.Enabled = false;

            CargarGrillaTorneos();
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("CargarResultados19M.formTitle");
            lblTitulo.Text = t.Translate("CargarResultados19M.lblTitulo");
            lblLogoApp.Text = t.Translate("CargarResultados19M.lblLogoApp");
            gbTorneos.Text = t.Translate("CargarResultados19M.gbTorneos");
            gbResultados.Text = t.Translate("CargarResultados19M.gbResultados");
            gbCargaTiempo.Text = t.Translate("CargarResultados19M.gbCargaTiempo");
            btnFinalizar.Text = t.Translate("CargarResultados19M.btnFinalizar");
            btnAplicar.Text = t.Translate("CargarResultados19M.btnAplicar");
            lblMinutos.Text = t.Translate("CargarResultados19M.colMinutos");
            lblSegundos.Text = t.Translate("CargarResultados19M.colSegundos");
            lblCentesimas.Text = t.Translate("CargarResultados19M.colCentesimas");
            chkDescalificado.Text = t.Translate("CargarResultados19M.colDescalificado");
            lblTiempoACargar.Text = t.Translate("CargarResultados19M.lblTiempoACargar");

            if (dgvTorneos.Columns.Count > 0)
            {
                dgvTorneos.Columns["CodigoTorneo"].HeaderText = t.Translate("CargarResultados19M.colCodigo");
                dgvTorneos.Columns["Nombre"].HeaderText = t.Translate("CargarResultados19M.colNombre");
                dgvTorneos.Columns["Fecha"].HeaderText = t.Translate("CargarResultados19M.colFecha");
                dgvTorneos.Columns["Sede"].HeaderText = t.Translate("CargarResultados19M.colSede");
                dgvTorneos.Columns["Categorias"].HeaderText = t.Translate("CargarResultados19M.colCategorias");
            }

            if (dgvResultados.Columns.Count > 0)
            {
                dgvResultados.Columns["DNINadador"].HeaderText = t.Translate("CargarResultados19M.colDNI");
                dgvResultados.Columns["Nombre"].HeaderText = t.Translate("CargarResultados19M.colNombre");
                dgvResultados.Columns["Apellido"].HeaderText = t.Translate("CargarResultados19M.colApellido");
                dgvResultados.Columns["Categoria"].HeaderText = t.Translate("CargarResultados19M.colCategoria");
                dgvResultados.Columns["DescripcionPrueba"].HeaderText = t.Translate("CargarResultados19M.colPrueba");
                dgvResultados.Columns["Tiempo"].HeaderText = t.Translate("CargarResultados19M.colTiempo");
            }

            ActualizarVistaPrevia();
        }
    }
}
