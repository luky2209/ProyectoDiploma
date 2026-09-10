using BLL;
using Services.Modelos;
using Services.Modelos.Idioma;
using Services_55CA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servicios
{
    public partial class GestionFamilia : Form, IIdiomaObserver
    {
        private BLLFamilia bllFamilia = new BLLFamilia();
        private BLLPatente bllPermiso = new BLLPatente();
        private List<FamiliaModelo55CA> listaFamilias;

        private enum ModoOperacionFamilia
        {
            Ninguno,
            Crear,
            Asignar,
            Eliminar
        }

        private ModoOperacionFamilia modoActual = ModoOperacionFamilia.Ninguno;

        public GestionFamilia()
        {
            InitializeComponent();
            cargarDatos();

            ServiceSessionManager55CA.getIntancia().Idioma.Suscribir(this);
            actualizarIdioma();
        }

        public void cargarDatos()
        {
            tvPermisosAsignados.Nodes.Clear();
            listaFamilias = bllFamilia.ObtenerTodos();
            dgvFamilias.DataSource = null;
            dgvFamilias.DataSource = listaFamilias;

            var todosLosComponentes = new List<Componente55CA>();
            todosLosComponentes.AddRange(listaFamilias);
            todosLosComponentes.AddRange(bllPermiso.obtenerTodos());

            checkListPermisosFamilias.DataSource = null;
            checkListPermisosFamilias.DataSource = todosLosComponentes;

            btAplicar.Enabled = false;
            btCancelar.Enabled = false;
        }
        public void actualizarIdioma()
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;

            this.Text = t.Translate("GestionFamilia.formTitle");
            label1.Text = t.Translate("GestionFamilia.labelFamilias");
            label2.Text = t.Translate("GestionFamilia.labelPermisosFamilias");
            label3.Text = t.Translate("GestionFamilia.labelAsignados");
            groupBox1.Text = t.Translate("GestionFamilia.groupBoxDatos");
            label4.Text = t.Translate("GestionFamilia.labelNombre");
            btCrear.Text = t.Translate("GestionFamilia.btnCrear");
            btAsignar.Text = t.Translate("GestionFamilia.btnAsignar");
            btAplicar.Text = t.Translate("GestionFamilia.btnAplicar");
            btEliminar.Text = t.Translate("GestionFamilia.btnEliminar");
            btCancelar.Text = t.Translate("GestionFamilia.btnCancelar");
        }


        private void dgvFamilias_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFamilias.CurrentRow != null)
            {
                FamiliaModelo55CA familiaSeleccionada = (FamiliaModelo55CA)dgvFamilias.CurrentRow.DataBoundItem;
                MostrarArbol(familiaSeleccionada);
            }
        }

        private void MostrarArbol(FamiliaModelo55CA familia)
        {
            tvPermisosAsignados.Nodes.Clear(); 

            TreeNode nodoRaiz = new TreeNode(familia.Nombre);
            tvPermisosAsignados.Nodes.Add(nodoRaiz);
            ConstruirRamas(nodoRaiz, familia);

            tvPermisosAsignados.ExpandAll();
        }

        private void ConstruirRamas(TreeNode nodoPadre, FamiliaModelo55CA familia)
        {
            foreach (Componente55CA hijo in familia.obtenerPermisos())
            {
                TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                nodoPadre.Nodes.Add(nodoHijo);

                if (hijo is FamiliaModelo55CA subFamilia)
                {
                    nodoHijo.NodeFont = new Font(tvPermisosAsignados.Font, FontStyle.Bold);
                    ConstruirRamas(nodoHijo, subFamilia);
                }
            }
        }

        private void btCrear_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacionFamilia.Crear;

            groupBox1.Visible = true;
            txtNombre.Focus();

            btAplicar.Enabled = true;
            btCancelar.Enabled = true;
            btCrear.Enabled = false;
            btEliminar.Enabled = false;
            btAsignar.Enabled = false;
        }

        private void btAsignar_Click(object sender, EventArgs e)
        {
            if (dgvFamilias.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una Familia de la lista de Familias.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FamiliaModelo55CA familiaDestino = (FamiliaModelo55CA)dgvFamilias.CurrentRow.DataBoundItem;

            var todosLosComponentes = new List<Componente55CA>();
            todosLosComponentes.AddRange(bllFamilia.ObtenerTodos());
            todosLosComponentes.AddRange(bllPermiso.obtenerTodos());

            var listaFiltrada = todosLosComponentes.Where(componente => !(componente is FamiliaModelo55CA && componente.Id == familiaDestino.Id)).ToList();

            checkListPermisosFamilias.DataSource = null;
            checkListPermisosFamilias.DataSource = listaFiltrada;

            modoActual = ModoOperacionFamilia.Asignar;

            btAplicar.Enabled = true;
            btCancelar.Enabled = true;
            btEliminar.Enabled = false;
            btCrear.Enabled = false;
            btAsignar.Enabled = false;
        }

        private void btEliminar_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacionFamilia.Eliminar;

            btCancelar.Enabled = true;
            btAplicar.Enabled = true;
            btAsignar.Enabled = false;
            btCrear.Enabled = false;
        }

        private void btAplicar_Click(object sender, EventArgs e)
        {
            var t = ServiceSessionManager55CA.getIntancia().Idioma;

            try
            {
                if (modoActual == ModoOperacionFamilia.Crear)
                {
                    string nombre = txtNombre.Text;

                    if (nombre.Length <= 0)
                    {
                        MessageBox.Show(t.Translate("GestionFamilia.msgNombreRequerido"));
                        return;
                    }

                    if (checkListPermisosFamilias.CheckedItems.Count == 0)
                    {
                        MessageBox.Show(t.Translate("GestionFamilia.msgSeleccionarComponente"), t.Translate("GestionFamilia.msgValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    List<Componente55CA> componentesSeleccionados = new List<Componente55CA>();

                    foreach (var item in checkListPermisosFamilias.CheckedItems)
                    {
                        componentesSeleccionados.Add((Componente55CA)item);
                    }

                    bllFamilia.CrearFamilia(nombre, componentesSeleccionados);

                    MessageBox.Show(t.Translate("GestionFamilia.msgFamiliaCreada"));
                }

                else if (modoActual == ModoOperacionFamilia.Asignar)
                {
                    if (dgvFamilias.CurrentRow == null)
                    {
                        MessageBox.Show(t.Translate("GestionFamilia.msgSeleccionarFamilia"), t.Translate("GestionFamilia.msgValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (checkListPermisosFamilias.CheckedItems.Count == 0)
                    {
                        MessageBox.Show(t.Translate("GestionFamilia.msgSeleccionarComponente"), t.Translate("GestionFamilia.msgValidacion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    FamiliaModelo55CA familiaDestino = (FamiliaModelo55CA)dgvFamilias.CurrentRow.DataBoundItem;

                    foreach (Componente55CA componenteMarcado in checkListPermisosFamilias.CheckedItems)
                    {
                        if (componenteMarcado is PermisoModelo55CA patente)
                        {
                            bllFamilia.AsignarPatente(familiaDestino, patente);
                        }
                        else if (componenteMarcado is FamiliaModelo55CA familiaHija)
                        {
                            bllFamilia.AsignarFamilia(familiaDestino, familiaHija);
                        }
                    }

                    MessageBox.Show(t.Translate("GestionFamilia.msgComponenteAsignado"));
                }

                else if (modoActual == ModoOperacionFamilia.Eliminar)
                {
                    if (dgvFamilias.CurrentRow == null)
                    {
                        MessageBox.Show(t.Translate("GestionFamilia.msgSeleccionarFamiliaComponente"));
                        return;
                    }

                    FamiliaModelo55CA familiaSeleccionada = (FamiliaModelo55CA)dgvFamilias.CurrentRow.DataBoundItem;

                    try
                    {
                        bllFamilia.EliminarFamilia(familiaSeleccionada.Id);
                        MessageBox.Show(t.Translate("GestionFamilia.msgFamiliaEliminada"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }

                }

                modoActual = ModoOperacionFamilia.Ninguno;

                txtNombre.Clear();
                groupBox1.Visible = false;

                btCrear.Enabled = true;
                btAsignar.Enabled = true;
                btEliminar.Enabled = true;
                btAplicar.Enabled = false;
                btCancelar.Enabled = false;

                cargarDatos();
                DesmarcarCheckList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, t.Translate("GestionFamilia.msgAtencion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            modoActual = ModoOperacionFamilia.Ninguno;

            groupBox1.Visible = false;
            btCancelar.Enabled = false;
            btAplicar.Enabled = false;
            btCrear.Enabled = true;
            btAsignar.Enabled = true;
            btEliminar.Enabled = true;

            DesmarcarCheckList();
        }

        private void DesmarcarCheckList()
        {
            for (int i = 0; i < checkListPermisosFamilias.Items.Count; i++)
            {
                checkListPermisosFamilias.SetItemChecked(i, false);
            }
        }
    }
}
