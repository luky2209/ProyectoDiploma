using BE;
using BLL;
using Services;
using Services.Modelos.Idioma;
using Services_13M;
using System;
using System.Collections.Generic;
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

        public RegistrarAsistencia19M()
        {
            InitializeComponent();
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

        private void btnCargar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

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

        public void actualizarIdioma()
        {
            var t = ServiceSessionManager13M.getIntancia().Idioma;

            this.Text = t.Translate("RegistrarAsistencia19M.formTitle");
            lblClase.Text = t.Translate("RegistrarAsistencia19M.lblClase");
            lblFecha.Text = t.Translate("RegistrarAsistencia19M.lblFecha");
            btnCargar.Text = t.Translate("RegistrarAsistencia19M.btnCargar");
            btnPresente.Text = t.Translate("RegistrarAsistencia19M.btnPresente");
            btnAusente.Text = t.Translate("RegistrarAsistencia19M.btnAusente");

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
