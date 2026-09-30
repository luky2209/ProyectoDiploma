namespace Servicios
{
    partial class InscripcionTorneo19M
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblLogoApp = new System.Windows.Forms.Label();
            this.gbTorneo = new System.Windows.Forms.GroupBox();
            this.txtEstadoTorneo = new System.Windows.Forms.TextBox();
            this.labelEstadoTorneo = new System.Windows.Forms.Label();
            this.txtFechaTorneo = new System.Windows.Forms.TextBox();
            this.labelFechaTorneo = new System.Windows.Forms.Label();
            this.txtCategoriasTorneo = new System.Windows.Forms.TextBox();
            this.labelCategoriasTorneo = new System.Windows.Forms.Label();
            this.cmbTorneo = new System.Windows.Forms.ComboBox();
            this.labelTorneo = new System.Windows.Forms.Label();
            this.gbInscripcion = new System.Windows.Forms.GroupBox();
            this.cmbPrueba = new System.Windows.Forms.ComboBox();
            this.labelPrueba = new System.Windows.Forms.Label();
            this.txtCategoriaNadador = new System.Windows.Forms.TextBox();
            this.labelCategoriaNadador = new System.Windows.Forms.Label();
            this.cmbNadador = new System.Windows.Forms.ComboBox();
            this.labelNadador = new System.Windows.Forms.Label();
            this.gbInscripciones = new System.Windows.Forms.GroupBox();
            this.dgvInscripciones = new System.Windows.Forms.DataGridView();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.gbTorneo.SuspendLayout();
            this.gbInscripcion.SuspendLayout();
            this.gbInscripciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).BeginInit();
            this.pnlBotones.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.pnlHeader.Controls.Add(this.lblTitulo);
            this.pnlHeader.Controls.Add(this.lblLogoApp);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 72);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 33);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(296, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Inscripcion a Torneo";
            //
            // lblLogoApp
            //
            this.lblLogoApp.AutoSize = true;
            this.lblLogoApp.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogoApp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.lblLogoApp.Location = new System.Drawing.Point(27, 10);
            this.lblLogoApp.Name = "lblLogoApp";
            this.lblLogoApp.Size = new System.Drawing.Size(89, 19);
            this.lblLogoApp.TabIndex = 0;
            this.lblLogoApp.Text = "AquaGestion";
            //
            // gbTorneo
            //
            this.gbTorneo.Controls.Add(this.txtEstadoTorneo);
            this.gbTorneo.Controls.Add(this.labelEstadoTorneo);
            this.gbTorneo.Controls.Add(this.txtFechaTorneo);
            this.gbTorneo.Controls.Add(this.labelFechaTorneo);
            this.gbTorneo.Controls.Add(this.txtCategoriasTorneo);
            this.gbTorneo.Controls.Add(this.labelCategoriasTorneo);
            this.gbTorneo.Controls.Add(this.cmbTorneo);
            this.gbTorneo.Controls.Add(this.labelTorneo);
            this.gbTorneo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbTorneo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbTorneo.Location = new System.Drawing.Point(20, 84);
            this.gbTorneo.Name = "gbTorneo";
            this.gbTorneo.Size = new System.Drawing.Size(960, 100);
            this.gbTorneo.TabIndex = 1;
            this.gbTorneo.TabStop = false;
            this.gbTorneo.Text = "Torneo";
            //
            // txtCategoriasTorneo
            //
            this.txtCategoriasTorneo.BackColor = System.Drawing.Color.White;
            this.txtCategoriasTorneo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategoriasTorneo.Location = new System.Drawing.Point(150, 61);
            this.txtCategoriasTorneo.Name = "txtCategoriasTorneo";
            this.txtCategoriasTorneo.ReadOnly = true;
            this.txtCategoriasTorneo.Size = new System.Drawing.Size(400, 26);
            this.txtCategoriasTorneo.TabIndex = 7;
            //
            // labelCategoriasTorneo
            //
            this.labelCategoriasTorneo.Location = new System.Drawing.Point(20, 65);
            this.labelCategoriasTorneo.Name = "labelCategoriasTorneo";
            this.labelCategoriasTorneo.Size = new System.Drawing.Size(130, 19);
            this.labelCategoriasTorneo.TabIndex = 6;
            this.labelCategoriasTorneo.Text = "Categorias";
            //
            // txtEstadoTorneo
            //
            this.txtEstadoTorneo.BackColor = System.Drawing.Color.White;
            this.txtEstadoTorneo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEstadoTorneo.Location = new System.Drawing.Point(700, 61);
            this.txtEstadoTorneo.Name = "txtEstadoTorneo";
            this.txtEstadoTorneo.ReadOnly = true;
            this.txtEstadoTorneo.Size = new System.Drawing.Size(230, 26);
            this.txtEstadoTorneo.TabIndex = 5;
            //
            // labelEstadoTorneo
            //
            this.labelEstadoTorneo.Location = new System.Drawing.Point(580, 65);
            this.labelEstadoTorneo.Name = "labelEstadoTorneo";
            this.labelEstadoTorneo.Size = new System.Drawing.Size(100, 19);
            this.labelEstadoTorneo.TabIndex = 4;
            this.labelEstadoTorneo.Text = "Estado";
            //
            // txtFechaTorneo
            //
            this.txtFechaTorneo.BackColor = System.Drawing.Color.White;
            this.txtFechaTorneo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFechaTorneo.Location = new System.Drawing.Point(700, 26);
            this.txtFechaTorneo.Name = "txtFechaTorneo";
            this.txtFechaTorneo.ReadOnly = true;
            this.txtFechaTorneo.Size = new System.Drawing.Size(230, 26);
            this.txtFechaTorneo.TabIndex = 3;
            //
            // labelFechaTorneo
            //
            this.labelFechaTorneo.Location = new System.Drawing.Point(580, 30);
            this.labelFechaTorneo.Name = "labelFechaTorneo";
            this.labelFechaTorneo.Size = new System.Drawing.Size(100, 19);
            this.labelFechaTorneo.TabIndex = 2;
            this.labelFechaTorneo.Text = "Fecha";
            //
            // cmbTorneo
            //
            this.cmbTorneo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTorneo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTorneo.FormattingEnabled = true;
            this.cmbTorneo.Location = new System.Drawing.Point(150, 26);
            this.cmbTorneo.Name = "cmbTorneo";
            this.cmbTorneo.Size = new System.Drawing.Size(400, 26);
            this.cmbTorneo.TabIndex = 1;
            this.cmbTorneo.SelectedIndexChanged += new System.EventHandler(this.cmbTorneo_SelectedIndexChanged);
            //
            // labelTorneo
            //
            this.labelTorneo.Location = new System.Drawing.Point(20, 30);
            this.labelTorneo.Name = "labelTorneo";
            this.labelTorneo.Size = new System.Drawing.Size(110, 19);
            this.labelTorneo.TabIndex = 0;
            this.labelTorneo.Text = "Torneo";
            //
            // gbInscripcion
            //
            this.gbInscripcion.Controls.Add(this.cmbPrueba);
            this.gbInscripcion.Controls.Add(this.labelPrueba);
            this.gbInscripcion.Controls.Add(this.txtCategoriaNadador);
            this.gbInscripcion.Controls.Add(this.labelCategoriaNadador);
            this.gbInscripcion.Controls.Add(this.cmbNadador);
            this.gbInscripcion.Controls.Add(this.labelNadador);
            this.gbInscripcion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbInscripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbInscripcion.Location = new System.Drawing.Point(20, 196);
            this.gbInscripcion.Name = "gbInscripcion";
            this.gbInscripcion.Size = new System.Drawing.Size(960, 120);
            this.gbInscripcion.TabIndex = 2;
            this.gbInscripcion.TabStop = false;
            this.gbInscripcion.Text = "Datos de la inscripcion";
            //
            // cmbPrueba
            //
            this.cmbPrueba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrueba.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPrueba.FormattingEnabled = true;
            this.cmbPrueba.Location = new System.Drawing.Point(500, 61);
            this.cmbPrueba.Name = "cmbPrueba";
            this.cmbPrueba.Size = new System.Drawing.Size(430, 26);
            this.cmbPrueba.TabIndex = 7;
            //
            // labelPrueba
            //
            this.labelPrueba.Location = new System.Drawing.Point(400, 65);
            this.labelPrueba.Name = "labelPrueba";
            this.labelPrueba.Size = new System.Drawing.Size(85, 19);
            this.labelPrueba.TabIndex = 6;
            this.labelPrueba.Text = "Prueba";
            //
            // txtCategoriaNadador
            //
            this.txtCategoriaNadador.BackColor = System.Drawing.Color.White;
            this.txtCategoriaNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategoriaNadador.Location = new System.Drawing.Point(700, 26);
            this.txtCategoriaNadador.Name = "txtCategoriaNadador";
            this.txtCategoriaNadador.ReadOnly = true;
            this.txtCategoriaNadador.Size = new System.Drawing.Size(230, 26);
            this.txtCategoriaNadador.TabIndex = 3;
            //
            // labelCategoriaNadador
            //
            this.labelCategoriaNadador.Location = new System.Drawing.Point(580, 30);
            this.labelCategoriaNadador.Name = "labelCategoriaNadador";
            this.labelCategoriaNadador.Size = new System.Drawing.Size(100, 19);
            this.labelCategoriaNadador.TabIndex = 2;
            this.labelCategoriaNadador.Text = "Categoria";
            //
            // cmbNadador
            //
            this.cmbNadador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNadador.FormattingEnabled = true;
            this.cmbNadador.Location = new System.Drawing.Point(150, 26);
            this.cmbNadador.Name = "cmbNadador";
            this.cmbNadador.Size = new System.Drawing.Size(400, 26);
            this.cmbNadador.TabIndex = 1;
            this.cmbNadador.SelectedIndexChanged += new System.EventHandler(this.cmbNadador_SelectedIndexChanged);
            //
            // labelNadador
            //
            this.labelNadador.Location = new System.Drawing.Point(20, 30);
            this.labelNadador.Name = "labelNadador";
            this.labelNadador.Size = new System.Drawing.Size(110, 19);
            this.labelNadador.TabIndex = 0;
            this.labelNadador.Text = "Nadador";
            //
            // gbInscripciones
            //
            this.gbInscripciones.Controls.Add(this.dgvInscripciones);
            this.gbInscripciones.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbInscripciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbInscripciones.Location = new System.Drawing.Point(20, 328);
            this.gbInscripciones.Name = "gbInscripciones";
            this.gbInscripciones.Size = new System.Drawing.Size(960, 250);
            this.gbInscripciones.TabIndex = 3;
            this.gbInscripciones.TabStop = false;
            this.gbInscripciones.Text = "Inscripciones del torneo";
            //
            // dgvInscripciones
            //
            this.dgvInscripciones.AllowUserToAddRows = false;
            this.dgvInscripciones.AllowUserToDeleteRows = false;
            this.dgvInscripciones.AllowUserToResizeRows = false;
            this.dgvInscripciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInscripciones.BackgroundColor = System.Drawing.Color.White;
            this.dgvInscripciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInscripciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInscripciones.Columns.Clear();
            this.dgvInscripciones.Location = new System.Drawing.Point(12, 26);
            this.dgvInscripciones.MultiSelect = false;
            this.dgvInscripciones.Name = "dgvInscripciones";
            this.dgvInscripciones.ReadOnly = true;
            this.dgvInscripciones.RowHeadersVisible = false;
            this.dgvInscripciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInscripciones.Size = new System.Drawing.Size(936, 212);
            this.dgvInscripciones.TabIndex = 0;
            this.dgvInscripciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInscripciones_CellClick);
            //
            // pnlBotones
            //
            this.pnlBotones.Controls.Add(this.btnAgregar);
            this.pnlBotones.Controls.Add(this.btnModificar);
            this.pnlBotones.Controls.Add(this.btnEliminar);
            this.pnlBotones.Controls.Add(this.btnGuardar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Location = new System.Drawing.Point(20, 590);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(960, 56);
            this.pnlBotones.TabIndex = 4;
            //
            // btnAgregar
            //
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(14, 11);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(150, 34);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // btnModificar
            //
            this.btnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificar.FlatAppearance.BorderSize = 0;
            this.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnModificar.ForeColor = System.Drawing.Color.White;
            this.btnModificar.Location = new System.Drawing.Point(176, 11);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(150, 34);
            this.btnModificar.TabIndex = 1;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = false;
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            //
            // btnEliminar
            //
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(338, 11);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(150, 34);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(500, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 34);
            this.btnGuardar.TabIndex = 3;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.btnCancelar.Location = new System.Drawing.Point(662, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 34);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // InscripcionTorneo19M
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.Controls.Add(this.gbInscripciones);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.gbInscripcion);
            this.Controls.Add(this.gbTorneo);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "InscripcionTorneo19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inscripcion a Torneo";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.gbTorneo.ResumeLayout(false);
            this.gbTorneo.PerformLayout();
            this.gbInscripcion.ResumeLayout(false);
            this.gbInscripcion.PerformLayout();
            this.gbInscripciones.ResumeLayout(false);
            this.gbInscripciones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.GroupBox gbTorneo;
        private System.Windows.Forms.TextBox txtEstadoTorneo;
        private System.Windows.Forms.Label labelEstadoTorneo;
        private System.Windows.Forms.TextBox txtFechaTorneo;
        private System.Windows.Forms.Label labelFechaTorneo;
        private System.Windows.Forms.TextBox txtCategoriasTorneo;
        private System.Windows.Forms.Label labelCategoriasTorneo;
        private System.Windows.Forms.ComboBox cmbTorneo;
        private System.Windows.Forms.Label labelTorneo;
        private System.Windows.Forms.GroupBox gbInscripcion;
        private System.Windows.Forms.ComboBox cmbPrueba;
        private System.Windows.Forms.Label labelPrueba;
        private System.Windows.Forms.TextBox txtCategoriaNadador;
        private System.Windows.Forms.Label labelCategoriaNadador;
        private System.Windows.Forms.ComboBox cmbNadador;
        private System.Windows.Forms.Label labelNadador;
        private System.Windows.Forms.GroupBox gbInscripciones;
        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
