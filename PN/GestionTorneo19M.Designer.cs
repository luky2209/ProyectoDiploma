namespace Servicios
{
    partial class GestionTorneo19M
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
            this.gbTorneos = new System.Windows.Forms.GroupBox();
            this.dgvTorneos = new System.Windows.Forms.DataGridView();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.labelPruebas = new System.Windows.Forms.Label();
            this.clbPruebas = new System.Windows.Forms.CheckedListBox();
            this.labelCategorias = new System.Windows.Forms.Label();
            this.clbCategorias = new System.Windows.Forms.CheckedListBox();
            this.labelArancel = new System.Windows.Forms.Label();
            this.nudArancel = new System.Windows.Forms.NumericUpDown();
            this.labelSede = new System.Windows.Forms.Label();
            this.txtSede = new System.Windows.Forms.TextBox();
            this.labelFecha = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.labelNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.gbTorneos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorneos)).BeginInit();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArancel)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(170, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Gestionar Torneo";
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
            // gbTorneos
            //
            this.gbTorneos.Controls.Add(this.dgvTorneos);
            this.gbTorneos.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbTorneos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbTorneos.Location = new System.Drawing.Point(20, 84);
            this.gbTorneos.Name = "gbTorneos";
            this.gbTorneos.Size = new System.Drawing.Size(960, 250);
            this.gbTorneos.TabIndex = 1;
            this.gbTorneos.TabStop = false;
            this.gbTorneos.Text = "Torneos";
            //
            // dgvTorneos
            //
            this.dgvTorneos.AllowUserToAddRows = false;
            this.dgvTorneos.AllowUserToDeleteRows = false;
            this.dgvTorneos.AllowUserToResizeRows = false;
            this.dgvTorneos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTorneos.BackgroundColor = System.Drawing.Color.White;
            this.dgvTorneos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTorneos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTorneos.Columns.Clear();
            this.dgvTorneos.Location = new System.Drawing.Point(12, 26);
            this.dgvTorneos.MultiSelect = false;
            this.dgvTorneos.Name = "dgvTorneos";
            this.dgvTorneos.ReadOnly = true;
            this.dgvTorneos.RowHeadersVisible = false;
            this.dgvTorneos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTorneos.Size = new System.Drawing.Size(936, 212);
            this.dgvTorneos.TabIndex = 0;
            this.dgvTorneos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTorneos_CellClick);
            //
            // gbDatos
            //
            this.gbDatos.Controls.Add(this.labelPruebas);
            this.gbDatos.Controls.Add(this.clbPruebas);
            this.gbDatos.Controls.Add(this.labelCategorias);
            this.gbDatos.Controls.Add(this.clbCategorias);
            this.gbDatos.Controls.Add(this.labelArancel);
            this.gbDatos.Controls.Add(this.nudArancel);
            this.gbDatos.Controls.Add(this.labelSede);
            this.gbDatos.Controls.Add(this.txtSede);
            this.gbDatos.Controls.Add(this.labelFecha);
            this.gbDatos.Controls.Add(this.dtpFecha);
            this.gbDatos.Controls.Add(this.labelNombre);
            this.gbDatos.Controls.Add(this.txtNombre);
            this.gbDatos.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbDatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbDatos.Location = new System.Drawing.Point(20, 344);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(960, 300);
            this.gbDatos.TabIndex = 2;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos del torneo";
            this.gbDatos.Visible = false;
            //
            // labelPruebas
            //
            this.labelPruebas.AutoSize = true;
            this.labelPruebas.Location = new System.Drawing.Point(450, 105);
            this.labelPruebas.Name = "labelPruebas";
            this.labelPruebas.Size = new System.Drawing.Size(110, 19);
            this.labelPruebas.TabIndex = 9;
            this.labelPruebas.Text = "Pruebas del torneo";
            //
            // clbPruebas
            //
            this.clbPruebas.CheckOnClick = true;
            this.clbPruebas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.clbPruebas.FormattingEnabled = true;
            this.clbPruebas.ItemHeight = 24;
            this.clbPruebas.Location = new System.Drawing.Point(580, 100);
            this.clbPruebas.MultiColumn = true;
            this.clbPruebas.Name = "clbPruebas";
            this.clbPruebas.Size = new System.Drawing.Size(330, 175);
            this.clbPruebas.TabIndex = 10;
            //
            // labelCategorias
            //
            this.labelCategorias.AutoSize = true;
            this.labelCategorias.Location = new System.Drawing.Point(20, 105);
            this.labelCategorias.Name = "labelCategorias";
            this.labelCategorias.Size = new System.Drawing.Size(140, 19);
            this.labelCategorias.TabIndex = 7;
            this.labelCategorias.Text = "Categorias del torneo";
            //
            // clbCategorias
            //
            this.clbCategorias.CheckOnClick = true;
            this.clbCategorias.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.clbCategorias.FormattingEnabled = true;
            this.clbCategorias.ItemHeight = 24;
            this.clbCategorias.Location = new System.Drawing.Point(20, 130);
            this.clbCategorias.Name = "clbCategorias";
            this.clbCategorias.Size = new System.Drawing.Size(250, 145);
            this.clbCategorias.TabIndex = 8;
            //
            // labelArancel
            //
            this.labelArancel.Location = new System.Drawing.Point(480, 65);
            this.labelArancel.Name = "labelArancel";
            this.labelArancel.Size = new System.Drawing.Size(55, 19);
            this.labelArancel.TabIndex = 5;
            this.labelArancel.Text = "Arancel";
            //
            // nudArancel
            //
            this.nudArancel.DecimalPlaces = 2;
            this.nudArancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudArancel.Location = new System.Drawing.Point(600, 61);
            this.nudArancel.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.nudArancel.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.nudArancel.Name = "nudArancel";
            this.nudArancel.Size = new System.Drawing.Size(160, 26);
            this.nudArancel.TabIndex = 6;
            this.nudArancel.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // labelSede
            //
            this.labelSede.Location = new System.Drawing.Point(20, 65);
            this.labelSede.Name = "labelSede";
            this.labelSede.Size = new System.Drawing.Size(40, 19);
            this.labelSede.TabIndex = 3;
            this.labelSede.Text = "Sede";
            //
            // txtSede
            //
            this.txtSede.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSede.Location = new System.Drawing.Point(150, 61);
            this.txtSede.Name = "txtSede";
            this.txtSede.Size = new System.Drawing.Size(300, 26);
            this.txtSede.TabIndex = 4;
            //
            // labelFecha
            //
            this.labelFecha.Location = new System.Drawing.Point(480, 30);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(38, 19);
            this.labelFecha.TabIndex = 1;
            this.labelFecha.Text = "Fecha";
            //
            // dtpFecha
            //
            this.dtpFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(600, 26);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(160, 26);
            this.dtpFecha.TabIndex = 2;
            //
            // labelNombre
            //
            this.labelNombre.Location = new System.Drawing.Point(20, 30);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(48, 19);
            this.labelNombre.TabIndex = 0;
            this.labelNombre.Text = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(150, 26);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(300, 26);
            this.txtNombre.TabIndex = 0;
            //
            // pnlBotones
            //
            this.pnlBotones.Controls.Add(this.btnAgregar);
            this.pnlBotones.Controls.Add(this.btnModificar);
            this.pnlBotones.Controls.Add(this.btnEliminar);
            this.pnlBotones.Controls.Add(this.btnGuardar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Location = new System.Drawing.Point(20, 656);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(960, 56);
            this.pnlBotones.TabIndex = 3;
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
            // GestionTorneo19M
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.gbDatos);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.gbTorneos);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "GestionTorneo19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestionar Torneo";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.gbTorneos.ResumeLayout(false);
            this.gbTorneos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorneos)).EndInit();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArancel)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.GroupBox gbTorneos;
        private System.Windows.Forms.DataGridView dgvTorneos;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label labelPruebas;
        private System.Windows.Forms.CheckedListBox clbPruebas;
        private System.Windows.Forms.Label labelCategorias;
        private System.Windows.Forms.CheckedListBox clbCategorias;
        private System.Windows.Forms.Label labelArancel;
        private System.Windows.Forms.NumericUpDown nudArancel;
        private System.Windows.Forms.Label labelSede;
        private System.Windows.Forms.TextBox txtSede;
        private System.Windows.Forms.Label labelFecha;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
