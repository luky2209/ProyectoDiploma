using BE;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Servicios
{
    public partial class ConsultarResultados19M : Form, IIdiomaObserver
    {
        BLLTorneo13M _bllTorneo = new BLLTorneo13M();
        BLLResultado13M _bllResultado = new BLLResultado13M();
        BLLInscripcion13M _bllInscripcion = new BLLInscripcion13M();
        List<Torneo13M> _listTorneos = new List<Torneo13M>();
        List<Resultado13M> _listRanking = new List<Resultado13M>();

        // guarda el torneo y el nadador elegidos para el cobro y el certificado
        Torneo13M _torneoSeleccionado;
        Resultado13M _resultadoCertificado;

        PrintDocument _printDoc = new PrintDocument();

        public ConsultarResultados19M()
        {
            InitializeComponent();

            _printDoc.PrintPage += printDoc_PrintPage;

            // solo quien tiene el permiso de cobro puede usar el boton
            btnCobrar.Enabled = ServiceSessionManager13M.getIntancia().TienePermiso("Cobrar Inscripcion");

            CargarGrillaTorneos();
            CargarGrillaRanking();

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        // solo se ven los torneos que el entrenador ya finalized
        private void CargarGrillaTorneos()
        {
            dgvTorneos.AutoGenerateColumns = false;

            dgvTorneos.Columns.Clear();

            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "CodigoTorneo", DataPropertyName = "CodigoTorneo", HeaderText = "Codigo" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", DataPropertyName = "Fecha", HeaderText = "Fecha" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Sede", DataPropertyName = "Sede", HeaderText = "Sede" });
            dgvTorneos.Columns.Add(new DataGridViewTextBoxColumn { Name = "FechaCierre", DataPropertyName = "FechaCierre", HeaderText = "Fecha de cierre" });

            dgvTorneos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvTorneos.Columns["FechaCierre"].DefaultCellStyle.Format = "dd/MM/yyyy";

            _listTorneos = _bllTorneo.obtenerFinalizados();

            dgvTorneos.DataSource = _listTorneos;

            actualizarIdioma();
        }

        // al elegir un torneo ya finalizado se muestra su ranking oficial
        private void dgvTorneos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Torneo13M torneo = (Torneo13M)dgvTorneos.Rows[e.RowIndex].DataBoundItem;

            _torneoSeleccionado = torneo;

            txtArancel.Text = torneo.Arancel.ToString("N2");
            txtEstadoPago.Text = "";

            _listRanking = _bllResultado.obtenerRankingDelTorneo(torneo.CodigoTorneo);

            CargarGrillaRanking();
        }

        // al tocar una fila del ranking se muestra el estado de pago de esa inscripcion
        private void dgvRanking_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Resultado13M resultado = (Resultado13M)dgvRanking.Rows[e.RowIndex].DataBoundItem;

            txtEstadoPago.Text = resultado.Estado;
        }

        // el ranking viene ordenado desde la base, por prueba y de menor a mayor tiempo
        private void CargarGrillaRanking()
        {
            dgvRanking.AutoGenerateColumns = false;

            dgvRanking.Columns.Clear();

            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "DescripcionPrueba", DataPropertyName = "DescripcionPrueba", HeaderText = "Prueba" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "Posicion", DataPropertyName = "Posicion", HeaderText = "Posicion" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNINadador", DataPropertyName = "DNINadador", HeaderText = "DNI" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "Categoria", DataPropertyName = "Categoria", HeaderText = "Categoria" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "TiempoOficial", DataPropertyName = "TiempoOficial", HeaderText = "Tiempo oficial" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "Premio", DataPropertyName = "Premio", HeaderText = "Premio" });
            dgvRanking.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", DataPropertyName = "Estado", HeaderText = "Estado de pago" });

            dgvRanking.DataSource = _listRanking;

            actualizarIdioma();
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("ConsultarResultados19M.formTitle");
            lblTitulo.Text = t.Translate("ConsultarResultados19M.lblTitulo");
            lblLogoApp.Text = t.Translate("ConsultarResultados19M.lblLogoApp");
            gbTorneos.Text = t.Translate("ConsultarResultados19M.gbTorneos");
            gbRanking.Text = t.Translate("ConsultarResultados19M.gbRanking");
            gbCobro.Text = t.Translate("ConsultarResultados19M.gbCobro");
            lblArancel.Text = t.Translate("ConsultarResultados19M.lblArancel");
            lblEstadoPago.Text = t.Translate("ConsultarResultados19M.lblEstadoPago");
            btnCobrar.Text = t.Translate("ConsultarResultados19M.btnCobrar");
            btnImprimirCertificado.Text = t.Translate("ConsultarResultados19M.btnImprimirCertificado");

            if (dgvTorneos.Columns.Count > 0)
            {
                dgvTorneos.Columns["CodigoTorneo"].HeaderText = t.Translate("ConsultarResultados19M.colCodigo");
                dgvTorneos.Columns["Nombre"].HeaderText = t.Translate("ConsultarResultados19M.colNombre");
                dgvTorneos.Columns["Fecha"].HeaderText = t.Translate("ConsultarResultados19M.colFecha");
                dgvTorneos.Columns["Sede"].HeaderText = t.Translate("ConsultarResultados19M.colSede");
                dgvTorneos.Columns["FechaCierre"].HeaderText = t.Translate("ConsultarResultados19M.colFechaCierre");
            }

            if (dgvRanking.Columns.Count > 0)
            {
                dgvRanking.Columns["DescripcionPrueba"].HeaderText = t.Translate("ConsultarResultados19M.colPrueba");
                dgvRanking.Columns["Posicion"].HeaderText = t.Translate("ConsultarResultados19M.colPosicion");
                dgvRanking.Columns["DNINadador"].HeaderText = t.Translate("ConsultarResultados19M.colDNI");
                dgvRanking.Columns["Nombre"].HeaderText = t.Translate("ConsultarResultados19M.colNombre");
                dgvRanking.Columns["Apellido"].HeaderText = t.Translate("ConsultarResultados19M.colApellido");
                dgvRanking.Columns["Categoria"].HeaderText = t.Translate("ConsultarResultados19M.colCategoria");
                dgvRanking.Columns["TiempoOficial"].HeaderText = t.Translate("ConsultarResultados19M.colTiempo");
                dgvRanking.Columns["Premio"].HeaderText = t.Translate("ConsultarResultados19M.colPremio");
                dgvRanking.Columns["Estado"].HeaderText = t.Translate("ConsultarResultados19M.colEstado");
            }
        }

        private void btnCobrar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            Resultado13M resultado = ObtenerResultadoSeleccionado();

            if (resultado == null || _torneoSeleccionado == null)
            {
                MessageBox.Show(t.Translate("ConsultarResultados19M.msgSeleccionarNadador"));
                return;
            }

            try
            {
                _bllInscripcion.CobrarInscripcionesDelNadador(resultado.DNINadador, _torneoSeleccionado.CodigoTorneo);

                // el cobro alcanza a todas las inscripciones del nadador en el torneo
                foreach (Resultado13M fila in _listRanking.Where(r => r.DNINadador == resultado.DNINadador))
                {
                    fila.Estado = "Pagado";
                }

                CargarGrillaRanking();

                txtEstadoPago.Text = "Pagado";

                MessageBox.Show(t.Translate("ConsultarResultados19M.msgCobroRealizado"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("ConsultarResultados19M.msgError") + ex.Message);
            }
        }

        private void btnImprimirCertificado_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            Resultado13M resultado = ObtenerResultadoSeleccionado();

            if (resultado == null || _torneoSeleccionado == null)
            {
                MessageBox.Show(t.Translate("ConsultarResultados19M.msgSeleccionarNadador"));
                return;
            }

            _resultadoCertificado = resultado;

            PrintDialog dialogo = new PrintDialog();
            dialogo.Document = _printDoc;

            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                _printDoc.Print();
            }
        }

        // devuelve la fila del ranking que esta seleccionada
        private Resultado13M ObtenerResultadoSeleccionado()
        {
            if (dgvRanking.CurrentRow == null)
            {
                return null;
            }

            return (Resultado13M)dgvRanking.CurrentRow.DataBoundItem;
        }

        // dibuja el certificado de participacion del nadador elegido
        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_resultadoCertificado == null || _torneoSeleccionado == null)
            {
                return;
            }

            var t = ServiceSessionManager13M.getIntancia().Idioma;
            Graphics g = e.Graphics;

            Font fuenteTitulo = new Font("Segoe UI", 20, FontStyle.Bold);
            Font fuenteNormal = new Font("Segoe UI", 11);
            SolidBrush pincel = new SolidBrush(Color.Black);

            int x = 60;
            int y = 80;
            int ancho = e.MarginBounds.Width;

            g.DrawString(t.Translate("ConsultarResultados19M.certTitulo"), fuenteTitulo, pincel, x, y);
            y += 50;

            string texto = string.Format(
                t.Translate("ConsultarResultados19M.certTexto"),
                _resultadoCertificado.Nombre,
                _resultadoCertificado.Apellido,
                _resultadoCertificado.DNINadador,
                _torneoSeleccionado.Nombre,
                _torneoSeleccionado.Fecha.ToString("dd/MM/yyyy"),
                _torneoSeleccionado.Sede);

            g.DrawString(texto, fuenteNormal, pincel, new RectangleF(x, y, ancho, 60));
            y += 70;

            // se listan todas las pruebas del nadador en el torneo
            foreach (Resultado13M fila in _listRanking.Where(r => r.DNINadador == _resultadoCertificado.DNINadador))
            {
                string linea = fila.DescripcionPrueba + ": ";

                if (fila.Descalificado)
                {
                    linea += t.Translate("ConsultarResultados19M.certDescalificado");
                }
                else
                {
                    linea += fila.TiempoOficial;

                    if (fila.Posicion.HasValue)
                    {
                        linea += " - " + string.Format(t.Translate("ConsultarResultados19M.certPosicion"), fila.Posicion.Value);
                    }
                }

                if (!string.IsNullOrEmpty(fila.Premio))
                {
                    linea += " - " + fila.Premio;
                }

                g.DrawString("- " + linea, fuenteNormal, pincel, x, y);
                y += 25;
            }

            y += 20;

            g.DrawString(string.Format(t.Translate("ConsultarResultados19M.certArancel"), _torneoSeleccionado.Arancel.ToString("N2")), fuenteNormal, pincel, x, y);
            y += 30;

            g.DrawString(string.Format(t.Translate("ConsultarResultados19M.certFecha"), DateTime.Today.ToString("dd/MM/yyyy")), fuenteNormal, pincel, x, y);
            y += 60;

            g.DrawLine(Pens.Black, x, y, x + 220, y);
            g.DrawString(t.Translate("ConsultarResultados19M.certFirma"), fuenteNormal, pincel, x, y + 5);
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ConsultarResultados19M_Load(object sender, EventArgs e)
        {

        }
    }
}
