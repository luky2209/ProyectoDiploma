using BE.Enum;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_55CA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Servicios
{
    public partial class AuditoriaBitacora : Form, IIdiomaObserver
    {
        BitacoraEventosService bitService = new BitacoraEventosService();
        PrintDocument printDoc = new PrintDocument();
        public AuditoriaBitacora()
        {
            InitializeComponent();
            printDoc.PrintPage += printDoc_PrintPage;

            this.Load += new System.EventHandler(this.AuditoriaBitacora_Load_1);

            this.dgvBitacora.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvBitacora_CellFormatting);
            ServiceSessionManager55CA.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }


        private void CargarGrillaInicial()
        {
            dgvBitacora.DataSource = bitService.obtenerUltimos3Dias();

            if (dgvBitacora.Columns["Id"] != null)
            {
                dgvBitacora.Columns["Id"].Visible = false;
            }

            dgvBitacora.Columns["Nombre"].Visible = false;
            dgvBitacora.Columns["Apellido"].Visible = false;

            actualizarIdioma();
        }

        private void CargarComboModulo()
        {
            var listaModulos = new List<object>();
            listaModulos.Add(new { Id = 0, Nombre = "Todos" });

            foreach (Modulos55CA mod in Enum.GetValues(typeof(Modulos55CA)))
            {
                listaModulos.Add(new { Id = (int)mod, Nombre = mod.ToString() });
            }

            cbModulos.DataSource = listaModulos;
            cbModulos.DisplayMember = "Nombre";
            cbModulos.ValueMember = "Id";
            cbModulos.SelectedIndex = 0;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date.AddDays(1).AddSeconds(-1);


            if (desde > hasta)
            {
                MessageBox.Show(
                    t.Translate("AuditoriaBitacora.errorFechasMsg"),
                    t.Translate("AuditoriaBitacora.errorFechasTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            int? moduloSeleccionado = null;
            if (cbModulos.SelectedValue != null && (int)cbModulos.SelectedValue != 0)
            {
                moduloSeleccionado = (int)cbModulos.SelectedValue;
            }

            dgvBitacora.DataSource = bitService.obtenerBitacora(desde, hasta, moduloSeleccionado);
            dgvBitacora.Columns["Nombre"].Visible = false;
            dgvBitacora.Columns["Apellido"].Visible = false;
        }


        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            int x = 20;
            int y = 50;
            int rowHeight = 25;

            Font font = new Font("Arial", 8);
            Font fontHeader = new Font("Arial", 8, FontStyle.Bold);

            
            e.Graphics.DrawString("Bitácora de Eventos", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x, y);
            y += 40;

           
            Dictionary<string, int> columnWidths = new Dictionary<string, int>()
            {
            { "DNI", 70 },
            { "Nombre", 80 },
            { "Apellido", 80 },
            { "Evento", 250 }, 
            { "Criticidad", 70 },
            { "Modulo", 80 },
            { "FechaHora", 130 }
            };
            int defaultWidth = 100; 
            StringFormat trimFormat = new StringFormat();
            trimFormat.Trimming = StringTrimming.EllipsisCharacter;
            trimFormat.LineAlignment = StringAlignment.Center; 

            int currentX = x; 
            foreach (DataGridViewColumn col in dgvBitacora.Columns)
            {
                if (col.Visible)
                {
                    int width = columnWidths.ContainsKey(col.Name) ? columnWidths[col.Name] : defaultWidth;

                    Rectangle cellRect = new Rectangle(currentX, y, width, rowHeight);

                    e.Graphics.DrawString(col.HeaderText, fontHeader, Brushes.Black, cellRect, trimFormat);

                    currentX += width;
                }
            }

            y += rowHeight;
            currentX = x; 

            
            foreach (DataGridViewRow row in dgvBitacora.Rows)
            {
                if (!row.IsNewRow)
                {
                    currentX = x; 

                    for (int i = 0; i < dgvBitacora.Columns.Count; i++)
                    {
                        DataGridViewColumn col = dgvBitacora.Columns[i];

                        if (col.Visible)
                        {
                            string cellText = row.Cells[i].FormattedValue?.ToString() ?? "";

                            int width = columnWidths.ContainsKey(col.Name) ? columnWidths[col.Name] : defaultWidth;

                            Rectangle cellRect = new Rectangle(currentX, y, width, rowHeight);

                            
                            e.Graphics.DrawString(cellText, font, Brushes.Black, cellRect, trimFormat);

                            currentX += width;
                        }
                    }

                    y += rowHeight;
                }
            }
        }

        private void btnLimpiat_Click_1(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-3).Date; 
            dtpHasta.Value = DateTime.Now;

            txtNombre.Clear();
            txtApellido.Clear();

            if (cbModulos.Items.Count > 0)
            {
                cbModulos.SelectedIndex = 0;
            }
                

            CargarGrillaInicial();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.Document = printDoc;

            if (pd.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        

        private void dgvBitacora_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBitacora.CurrentRow != null)
            {
                txtNombre.Text = dgvBitacora.CurrentRow.Cells["Nombre"].Value?.ToString() ?? "";
                txtApellido.Text = dgvBitacora.CurrentRow.Cells["Apellido"].Value?.ToString() ?? "";
            }
        }

        private void AuditoriaBitacora_Load_1(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Now.AddDays(-3);
            dtpHasta.Value = DateTime.Now;
            CargarComboModulo();
            CargarGrillaInicial();
        }

        private void dgvBitacora_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null || e.RowIndex < 0) return;

            if (dgvBitacora.Columns[e.ColumnIndex].Name == "Criticidad")
            {
                if (Int32.TryParse(e.Value.ToString(), out int criticidad))
                {
                    e.Value = ((Criticidad55CA)criticidad).ToString();
                    e.FormattingApplied = true;
                }
            }

            
            if (dgvBitacora.Columns[e.ColumnIndex].Name == "Modulo")
            {
                if (Int32.TryParse(e.Value.ToString(), out int modulo))
                {
                    e.Value = ((Modulos55CA)modulo).ToString();
                    e.FormattingApplied = true;
                }
            }
        }

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;

            this.Text = t.Translate("AuditoriaBitacora.formTitle");
            label2.Text = t.Translate("AuditoriaBitacora.labelDesde");
            label1.Text = t.Translate("AuditoriaBitacora.labelHasta");
            label3.Text = t.Translate("AuditoriaBitacora.labelNombre");
            label4.Text = t.Translate("AuditoriaBitacora.labelApellido");
            btnFiltrar.Text = t.Translate("AuditoriaBitacora.btnFiltrar");
            btnLimpiat.Text = t.Translate("AuditoriaBitacora.btnLimpiar");
            btnPDF.Text = t.Translate("AuditoriaBitacora.btnPDF");

            if(dgvBitacora.Columns.Count > 0)
            {
                dgvBitacora.Columns["Evento"].HeaderText = t.Translate("AuditoriaBitacora.colEvento");
                dgvBitacora.Columns["Criticidad"].HeaderText = t.Translate("AuditoriaBitacora.colCriticidad");
                dgvBitacora.Columns["Modulo"].HeaderText = t.Translate("AuditoriaBitacora.colModulo");
                dgvBitacora.Columns["FechaHora"].HeaderText = t.Translate("AuditoriaBitacora.colFechaHora");
            }
            

        }
    }
}
