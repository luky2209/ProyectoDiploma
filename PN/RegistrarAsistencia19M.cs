using BE;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Servicios
{
    public partial class RegistrarAsistencia19M : Form, IIdiomaObserver
    {
        BLLClase13M _bllClase = new BLLClase13M();
        BLLInscripcionClase13M _bllInscripcionClase = new BLLInscripcionClase13M();
        BLLAsistencia13M _bllAsistencia = new BLLAsistencia13M();

        List<Clase13M> _listClases = new List<Clase13M>();
        List<InscripcionClase13M> _listAlumnos = new List<InscripcionClase13M>();
        List<Asistencia13M> _asistenciasCargadas = new List<Asistencia13M>();

        Clase13M _claseCargada;
        DateTime _fechaCargada;

        PrintDocument _printDocListado = new PrintDocument();
        int _indiceAlumnoReporte;
        int _paginaReporte;

        public RegistrarAsistencia19M()
        {
            InitializeComponent();

            _printDocListado.PrintPage += printDocListado_PrintPage;
            cmbClase.SelectedIndexChanged += cmbClase_SelectedIndexChanged;
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;

            ConfigurarGrilla();
            CargarClases();

            ServiceSessionManager13M.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        private void ConfigurarGrilla()
        {
            dgvAlumnos.AutoGenerateColumns = false;
            dgvAlumnos.Columns.Clear();
            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn { Name = "DNI", DataPropertyName = "DNINadador", HeaderText = "DNI" });
            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nombre", DataPropertyName = "Nombre", HeaderText = "Nombre" });
            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Apellido", DataPropertyName = "Apellido", HeaderText = "Apellido" });
            dgvAlumnos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Condicion", DataPropertyName = "Condicion", HeaderText = "Condicion" });

            dgvAlumnos.DataSource = null;
        }

        private void CargarClases()
        {
            _listClases = _bllClase.obtenerTodos();

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

        private void cmbClase_SelectedIndexChanged(object sender, EventArgs e)
        {
            _claseCargada = null;
            btnDescargarPdf.Enabled = false;
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            _claseCargada = null;
            btnDescargarPdf.Enabled = false;
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            btnDescargarPdf.Enabled = false;
            _claseCargada = null;

            if (cmbClase.SelectedItem == null)
            {
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgSeleccionarClase"));
                return;
            }

            Clase13M clase = (Clase13M)cmbClase.SelectedItem;
            DateTime fecha = dtpFecha.Value.Date;

            // validar fecha antes de cargar
            if (fecha > DateTime.Today)
            {
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgFechaFutura"));
                return;
            }

            // validar dia de clase
            if (!EsDiaDeClase(clase.Dias, fecha))
            {
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgDiaNoValido"));
                return;
            }

            _listAlumnos = _bllInscripcionClase.obtenerPorClase(clase.CodigoClase);

            // armar tabla con condicion actual
            DataTable filas = new DataTable();
            filas.Columns.Add("DNINadador", typeof(string));
            filas.Columns.Add("Nombre", typeof(string));
            filas.Columns.Add("Apellido", typeof(string));
            filas.Columns.Add("Condicion", typeof(string));

            List<Asistencia13M> asistencias = _bllAsistencia.obtenerPorClaseYFecha(clase.CodigoClase, fecha);

            foreach (InscripcionClase13M insc in _listAlumnos)
            {
                Asistencia13M asis = asistencias.Find(a => a.DNINadador == insc.DNINadador);
                filas.Rows.Add(insc.DNINadador, insc.Nombre, insc.Apellido, asis != null ? asis.Condicion : "");
            }

            dgvAlumnos.DataSource = null;
            dgvAlumnos.DataSource = filas;

            _asistenciasCargadas = asistencias;
            _claseCargada = clase;
            _fechaCargada = fecha;
            btnDescargarPdf.Enabled = true;
        }

        private void dgvAlumnos_SelectionChanged(object sender, EventArgs e)
        {
            bool hay = dgvAlumnos.CurrentRow != null;
            btnPresente.Enabled = hay;
            btnAusente.Enabled = hay;
        }

        private void btnPresente_Click(object sender, EventArgs e)
        {
            RegistrarCondicion("Presente");
        }

        private void btnAusente_Click(object sender, EventArgs e)
        {
            RegistrarCondicion("Ausente");
        }

        private void RegistrarCondicion(string condicion)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            if (cmbClase.SelectedItem == null || dgvAlumnos.CurrentRow == null)
            {
                return;
            }

            Clase13M clase = (Clase13M)cmbClase.SelectedItem;
            DateTime fecha = dtpFecha.Value.Date;
            string dni = dgvAlumnos.CurrentRow.Cells["DNI"].Value.ToString();

            try
            {
                _bllAsistencia.RegistrarAsistencia(clase.CodigoClase, fecha, dni, condicion);
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgRegistroExitoso"));
                btnCargar_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgError") + ex.Message);
            }
        }

        private bool EsDiaDeClase(string dias, DateTime fecha)
        {
            if (string.IsNullOrEmpty(dias))
            {
                return false;
            }

            string[] nombres = { "Domingo", "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };
            string nombreDia = nombres[(int)fecha.DayOfWeek];

            foreach (string parte in dias.Split(','))
            {
                if (parte.Trim() == nombreDia)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnDescargarPdf_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            Clase13M claseActual = cmbClase.SelectedItem as Clase13M;

            if (_claseCargada == null || claseActual == null ||
                claseActual.CodigoClase != _claseCargada.CodigoClase ||
                dtpFecha.Value.Date != _fechaCargada.Date)
            {
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgPdfSinCargar"));
                return;
            }

            SaveFileDialog dialogo = new SaveFileDialog();
            dialogo.Title = t.Translate("RegistrarAsistencia19M.pdfGuardarTitulo");
            dialogo.Filter = t.Translate("RegistrarAsistencia19M.pdfFiltro");
            dialogo.FileName = "ListaAlumnos_Clase" + _claseCargada.CodigoClase + "_" + _fechaCargada.ToString("yyyyMMdd") + ".pdf";
            dialogo.DefaultExt = "pdf";
            dialogo.AddExtension = true;
            dialogo.OverwritePrompt = true;
            dialogo.RestoreDirectory = true;
            dialogo.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dialogo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string ruta = dialogo.FileName;

            try
            {
                if (!ExisteImpresoraPdf())
                {
                    MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgPdfImpresora"));
                    return;
                }

                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                }

                _indiceAlumnoReporte = 0;
                _paginaReporte = 1;

                _printDocListado.DocumentName = Path.GetFileName(ruta);
                _printDocListado.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                _printDocListado.PrinterSettings.PrintToFile = true;
                _printDocListado.PrinterSettings.PrintFileName = ruta;
                _printDocListado.PrintController = new StandardPrintController();
                _printDocListado.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                _printDocListado.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);

                _printDocListado.Print();

                if (!File.Exists(ruta))
                {
                    MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgPdfError") + ruta);
                    return;
                }

                MessageBox.Show(string.Format(t.Translate("RegistrarAsistencia19M.msgPdfGenerado"), ruta));
            }
            catch (Exception ex)
            {
                MessageBox.Show(t.Translate("RegistrarAsistencia19M.msgPdfError") + ex.Message);
            }
        }

        private bool ExisteImpresoraPdf()
        {
            try
            {
                foreach (string impresora in PrinterSettings.InstalledPrinters)
                {
                    if (impresora == "Microsoft Print to PDF")
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private void printDocListado_PrintPage(object sender, PrintPageEventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            Graphics g = e.Graphics;

            Font fuenteEmpresa = new Font("Arial", 18, FontStyle.Bold);
            Font fuenteTitulo = new Font("Arial", 13, FontStyle.Bold);
            Font fuenteNormal = new Font("Arial", 9);
            Font fuenteNegrita = new Font("Arial", 9, FontStyle.Bold);
            Font fuenteChica = new Font("Arial", 8);

            StringFormat formatoDerecha = new StringFormat();
            formatoDerecha.Alignment = StringAlignment.Far;

            StringFormat formatoCelda = new StringFormat();
            formatoCelda.LineAlignment = StringAlignment.Center;
            formatoCelda.Trimming = StringTrimming.EllipsisCharacter;

            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int ancho = e.MarginBounds.Width;

            g.DrawString(t.Translate("RegistrarAsistencia19M.pdfEmpresa"), fuenteEmpresa, Brushes.Black, x, y);

            g.DrawString(t.Translate("RegistrarAsistencia19M.pdfTitulo"), fuenteTitulo, Brushes.Black,
                new RectangleF(x, y, ancho, 24), formatoDerecha);
            g.DrawString(string.Format(t.Translate("RegistrarAsistencia19M.pdfClaseNro"), _claseCargada.CodigoClase),
                fuenteNormal, Brushes.Black, new RectangleF(x, y + 26, ancho, 20), formatoDerecha);

            y = e.MarginBounds.Top + 62;
            g.DrawLine(Pens.Black, x, y, x + ancho, y);
            y += 14;

            string profesor = string.IsNullOrEmpty(_claseCargada.NombreProfesor)
                ? t.Translate("RegistrarAsistencia19M.pdfSinAsignar")
                : _claseCargada.NombreProfesor;

            string legajo = _claseCargada.LegajoProfesor.HasValue
                ? _claseCargada.LegajoProfesor.Value.ToString()
                : t.Translate("RegistrarAsistencia19M.pdfSinAsignar");

            if (_indiceAlumnoReporte == 0)
            {
                int x2 = x + ancho / 2;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfDatosClase"), fuenteNegrita, Brushes.Black, x, y);
                y += 20;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfTipoClase") + ": " + _claseCargada.TipoClase,
                    fuenteNormal, Brushes.Black, x, y);
                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfNivel") + ": " + _claseCargada.NivelDificultad,
                    fuenteNormal, Brushes.Black, x2, y);
                y += 18;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfDias") + ": " + _claseCargada.Dias,
                    fuenteNormal, Brushes.Black, x, y);
                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfHorario") + ": " +
                    _claseCargada.HorarioInicio.ToString(@"hh\:mm") + " - " +
                    _claseCargada.HorarioFin.ToString(@"hh\:mm"), fuenteNormal, Brushes.Black, x2, y);
                y += 18;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfRangoEtario") + ": " +
                    _claseCargada.EdadMinima + " - " + _claseCargada.EdadMaxima,
                    fuenteNormal, Brushes.Black, x, y);
                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfCupo") + ": " + _claseCargada.CupoMaximo,
                    fuenteNormal, Brushes.Black, x2, y);
                y += 18;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfProfesor") + ": " + profesor,
                    fuenteNormal, Brushes.Black, x, y);
                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfLegajo") + ": " + legajo,
                    fuenteNormal, Brushes.Black, x2, y);
                y += 18;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfFechaListado") + ": " +
                    _fechaCargada.ToString("dd/MM/yyyy"), fuenteNormal, Brushes.Black, x, y);
                y += 26;

                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfAlumnos"), fuenteNegrita, Brushes.Black, x, y);
                y += 20;
            }
            else
            {
                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfAlumnos") + " (" +
                    t.Translate("RegistrarAsistencia19M.pdfContinuacion") + ")",
                    fuenteNegrita, Brushes.Black, x, y);
                y += 22;
            }

            int[] anchosColumnas = { 30, 70, 230, 85, 70, 100 };
            string[] titulosColumnas =
            {
                t.Translate("RegistrarAsistencia19M.pdfColNro"),
                t.Translate("RegistrarAsistencia19M.pdfColDNI"),
                t.Translate("RegistrarAsistencia19M.pdfColNombre"),
                t.Translate("RegistrarAsistencia19M.pdfColFechaInscripcion"),
                t.Translate("RegistrarAsistencia19M.pdfColEstado"),
                t.Translate("RegistrarAsistencia19M.pdfColAsistencia")
            };

            int anchoTabla = 0;

            for (int i = 0; i < anchosColumnas.Length; i++)
            {
                anchoTabla += anchosColumnas[i];
            }

            int xTabla = x + (ancho - anchoTabla) / 2;
            int yTablaInicio = y;
            int altoFila = 22;
            int altoCabecera = 24;

            Rectangle cabecera = new Rectangle(xTabla, y, anchoTabla, altoCabecera);
            g.FillRectangle(new SolidBrush(Color.FromArgb(226, 232, 240)), cabecera);
            g.DrawRectangle(Pens.Black, cabecera);

            int xColumna = xTabla;

            for (int i = 0; i < titulosColumnas.Length; i++)
            {
                g.DrawString(titulosColumnas[i], fuenteNegrita, Brushes.Black,
                    new RectangleF(xColumna, y, anchosColumnas[i], altoCabecera), formatoCelda);
                xColumna += anchosColumnas[i];

                if (i < titulosColumnas.Length - 1)
                {
                    g.DrawLine(Pens.Black, xColumna, y, xColumna, y + altoCabecera);
                }
            }

            y += altoCabecera;

            int limiteInferior = e.MarginBounds.Bottom - 190;

            while (_indiceAlumnoReporte < _listAlumnos.Count)
            {
                if (y + altoFila > limiteInferior)
                {
                    e.HasMorePages = true;
                    break;
                }

                InscripcionClase13M inscripcion = _listAlumnos[_indiceAlumnoReporte];
                Asistencia13M asistencia = _asistenciasCargadas.Find(a => a.DNINadador == inscripcion.DNINadador);

                string condicion = asistencia != null && !string.IsNullOrEmpty(asistencia.Condicion)
                    ? asistencia.Condicion
                    : t.Translate("RegistrarAsistencia19M.pdfSinRegistrar");

                string[] valores =
                {
                    (_indiceAlumnoReporte + 1).ToString(),
                    inscripcion.DNINadador ?? "",
                    inscripcion.Apellido + ", " + inscripcion.Nombre,
                    inscripcion.FechaInscripcion.ToString("dd/MM/yyyy"),
                    inscripcion.Estado ?? "",
                    condicion
                };

                xColumna = xTabla;

                for (int i = 0; i < valores.Length; i++)
                {
                    g.DrawString(valores[i], fuenteNormal, Brushes.Black,
                        new RectangleF(xColumna, y, anchosColumnas[i], altoFila), formatoCelda);
                    xColumna += anchosColumnas[i];

                    if (i < valores.Length - 1)
                    {
                        g.DrawLine(Pens.Gray, xColumna, y, xColumna, y + altoFila);
                    }
                }

                g.DrawLine(Pens.Gray, xTabla, y + altoFila, xTabla + anchoTabla, y + altoFila);

                y += altoFila;
                _indiceAlumnoReporte++;
            }

            if (_listAlumnos.Count == 0)
            {
                g.DrawString(t.Translate("RegistrarAsistencia19M.pdfSinAlumnos"), fuenteNormal, Brushes.Black,
                    new RectangleF(xTabla + 10, y, anchoTabla - 20, altoFila), formatoCelda);
                y += altoFila;
            }

            g.DrawRectangle(Pens.Black, xTabla, yTablaInicio, anchoTabla, y - yTablaInicio);

            if (e.HasMorePages)
            {
                g.DrawString(string.Format(t.Translate("RegistrarAsistencia19M.pdfPagina"), _paginaReporte),
                    fuenteChica, Brushes.Gray, x, e.MarginBounds.Bottom - 20);
                _paginaReporte++;
                return;
            }

            y += 18;

            g.DrawString(string.Format(t.Translate("RegistrarAsistencia19M.pdfTotal"), _listAlumnos.Count),
                fuenteNegrita, Brushes.Black, x, y);
            y += 18;

            g.DrawString(string.Format(t.Translate("RegistrarAsistencia19M.pdfEmitido"), DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                fuenteNormal, Brushes.Black, x, y);
            y += 36;

            g.DrawLine(Pens.Black, x, y, x + 220, y);
            g.DrawString(t.Translate("RegistrarAsistencia19M.pdfFirma"), fuenteNormal, Brushes.Black, x, y + 4);
            g.DrawString(t.Translate("RegistrarAsistencia19M.pdfAclaracion") + ": " + profesor,
                fuenteNormal, Brushes.Black, x, y + 22);
            g.DrawString(t.Translate("RegistrarAsistencia19M.pdfLegajo") + ": " + legajo,
                fuenteNormal, Brushes.Black, x, y + 40);

            g.DrawString(t.Translate("RegistrarAsistencia19M.pdfLeyenda"), fuenteChica, Brushes.Gray, x, y + 70);
            g.DrawString(string.Format(t.Translate("RegistrarAsistencia19M.pdfPagina"), _paginaReporte),
                fuenteChica, Brushes.Gray, x, e.MarginBounds.Bottom - 20);

            _paginaReporte++;
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            string[] pasos =
            {
                t.Translate("RegistrarAsistencia19M.ayudaPaso1"),
                t.Translate("RegistrarAsistencia19M.ayudaPaso2"),
                t.Translate("RegistrarAsistencia19M.ayudaPaso3"),
                t.Translate("RegistrarAsistencia19M.ayudaPaso4"),
                t.Translate("RegistrarAsistencia19M.ayudaPaso5"),
                t.Translate("RegistrarAsistencia19M.ayudaPaso6"),
                t.Translate("RegistrarAsistencia19M.ayudaPaso7")
            };

            string texto = "";

            for (int i = 0; i < pasos.Length; i++)
            {
                texto += (i + 1) + ". " + pasos[i] + Environment.NewLine + Environment.NewLine;
            }

            MessageBox.Show(texto.TrimEnd(),
                t.Translate("RegistrarAsistencia19M.ayudaTitulo"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("RegistrarAsistencia19M.formTitle");
            lblClase.Text = t.Translate("RegistrarAsistencia19M.lblClase");
            lblFecha.Text = t.Translate("RegistrarAsistencia19M.lblFecha");
            btnAyuda.Text = t.Translate("RegistrarAsistencia19M.btnAyuda");
            btnCargar.Text = t.Translate("RegistrarAsistencia19M.btnCargar");
            btnPresente.Text = t.Translate("RegistrarAsistencia19M.btnPresente");
            btnAusente.Text = t.Translate("RegistrarAsistencia19M.btnAusente");
            btnDescargarPdf.Text = t.Translate("RegistrarAsistencia19M.btnDescargarPdf");

            dgvAlumnos.Columns["DNI"].HeaderText = t.Translate("RegistrarAsistencia19M.colDNI");
            dgvAlumnos.Columns["Nombre"].HeaderText = t.Translate("RegistrarAsistencia19M.colNombre");
            dgvAlumnos.Columns["Apellido"].HeaderText = t.Translate("RegistrarAsistencia19M.colApellido");
            dgvAlumnos.Columns["Condicion"].HeaderText = t.Translate("RegistrarAsistencia19M.colCondicion");
        }

        private void RegistrarAsistencia19M_Load(object sender, EventArgs e)
        {

        }
    }
}
