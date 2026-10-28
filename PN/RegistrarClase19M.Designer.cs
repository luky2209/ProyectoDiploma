namespace Servicios
{
    partial class RegistrarClase19M
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblLogoApp = new System.Windows.Forms.Label();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbClases = new System.Windows.Forms.GroupBox();
            this.dgvClases = new System.Windows.Forms.DataGridView();
            this.gbDatos = new System.Windows.Forms.GroupBox();
            this.labelTipoClase = new System.Windows.Forms.Label();
            this.cmbTipoClase = new System.Windows.Forms.ComboBox();
            this.labelNivel = new System.Windows.Forms.Label();
            this.cmbNivel = new System.Windows.Forms.ComboBox();
            this.labelEdadMinima = new System.Windows.Forms.Label();
            this.numEdadMinima = new System.Windows.Forms.NumericUpDown();
            this.labelEdadMaxima = new System.Windows.Forms.Label();
            this.numEdadMaxima = new System.Windows.Forms.NumericUpDown();
            this.labelCupo = new System.Windows.Forms.Label();
            this.numCupo = new System.Windows.Forms.NumericUpDown();
            this.labelHorarioInicio = new System.Windows.Forms.Label();
            this.dtpHorarioInicio = new System.Windows.Forms.DateTimePicker();
            this.labelHorarioFin = new System.Windows.Forms.Label();
            this.dtpHorarioFin = new System.Windows.Forms.DateTimePicker();
            this.labelDias = new System.Windows.Forms.Label();
            this.clbDias = new System.Windows.Forms.CheckedListBox();
            this.pnlHeader.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.gbClases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClases)).BeginInit();
            this.gbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEdadMinima)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdadMaxima)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCupo)).BeginInit();
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
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 33);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(175, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Registrar Clase";
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
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.btnAgregar);
            this.pnlBotones.Controls.Add(this.btnModificar);
            this.pnlBotones.Controls.Add(this.btnEliminar);
            this.pnlBotones.Controls.Add(this.btnGuardar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Location = new System.Drawing.Point(20, 308);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(827, 56);
            this.pnlBotones.TabIndex = 1;
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
            // gbClases
            // 
            this.gbClases.Controls.Add(this.dgvClases);
            this.gbClases.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbClases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbClases.Location = new System.Drawing.Point(20, 84);
            this.gbClases.Name = "gbClases";
            this.gbClases.Size = new System.Drawing.Size(960, 210);
            this.gbClases.TabIndex = 2;
            this.gbClases.TabStop = false;
            this.gbClases.Text = "Clases";
            // 
            // dgvClases
            // 
            this.dgvClases.AllowUserToAddRows = false;
            this.dgvClases.AllowUserToDeleteRows = false;
            this.dgvClases.AllowUserToResizeRows = false;
            this.dgvClases.BackgroundColor = System.Drawing.Color.White;
            this.dgvClases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.dgvClases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClases.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvClases.Location = new System.Drawing.Point(12, 27);
            this.dgvClases.MultiSelect = false;
            this.dgvClases.Name = "dgvClases";
            this.dgvClases.ReadOnly = true;
            this.dgvClases.RowHeadersVisible = false;
            this.dgvClases.RowTemplate.Height = 26;
            this.dgvClases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClases.Size = new System.Drawing.Size(936, 170);
            this.dgvClases.TabIndex = 0;
            this.dgvClases.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClases_CellClick);
            // 
            // gbDatos
            // 
            this.gbDatos.Controls.Add(this.labelTipoClase);
            this.gbDatos.Controls.Add(this.cmbTipoClase);
            this.gbDatos.Controls.Add(this.labelNivel);
            this.gbDatos.Controls.Add(this.cmbNivel);
            this.gbDatos.Controls.Add(this.labelEdadMinima);
            this.gbDatos.Controls.Add(this.numEdadMinima);
            this.gbDatos.Controls.Add(this.labelEdadMaxima);
            this.gbDatos.Controls.Add(this.numEdadMaxima);
            this.gbDatos.Controls.Add(this.labelCupo);
            this.gbDatos.Controls.Add(this.numCupo);
            this.gbDatos.Controls.Add(this.labelHorarioInicio);
            this.gbDatos.Controls.Add(this.dtpHorarioInicio);
            this.gbDatos.Controls.Add(this.labelHorarioFin);
            this.gbDatos.Controls.Add(this.dtpHorarioFin);
            this.gbDatos.Controls.Add(this.labelDias);
            this.gbDatos.Controls.Add(this.clbDias);
            this.gbDatos.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbDatos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbDatos.Location = new System.Drawing.Point(20, 378);
            this.gbDatos.Name = "gbDatos";
            this.gbDatos.Size = new System.Drawing.Size(960, 250);
            this.gbDatos.TabIndex = 3;
            this.gbDatos.TabStop = false;
            this.gbDatos.Text = "Datos de la clase";
            // 
            // labelTipoClase
            // 
            this.labelTipoClase.AutoSize = true;
            this.labelTipoClase.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelTipoClase.Location = new System.Drawing.Point(40, 40);
            this.labelTipoClase.Name = "labelTipoClase";
            this.labelTipoClase.Size = new System.Drawing.Size(86, 17);
            this.labelTipoClase.TabIndex = 0;
            this.labelTipoClase.Text = "Tipo de clase";
            // 
            // cmbTipoClase
            // 
            this.cmbTipoClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoClase.FormattingEnabled = true;
            this.cmbTipoClase.Location = new System.Drawing.Point(170, 36);
            this.cmbTipoClase.Name = "cmbTipoClase";
            this.cmbTipoClase.Size = new System.Drawing.Size(235, 25);
            this.cmbTipoClase.TabIndex = 1;
            // 
            // labelNivel
            // 
            this.labelNivel.AutoSize = true;
            this.labelNivel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelNivel.Location = new System.Drawing.Point(40, 80);
            this.labelNivel.Name = "labelNivel";
            this.labelNivel.Size = new System.Drawing.Size(113, 17);
            this.labelNivel.TabIndex = 2;
            this.labelNivel.Text = "Nivel de dificultad";
            // 
            // cmbNivel
            // 
            this.cmbNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNivel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNivel.FormattingEnabled = true;
            this.cmbNivel.Location = new System.Drawing.Point(170, 76);
            this.cmbNivel.Name = "cmbNivel";
            this.cmbNivel.Size = new System.Drawing.Size(235, 25);
            this.cmbNivel.TabIndex = 3;
            // 
            // labelEdadMinima
            // 
            this.labelEdadMinima.AutoSize = true;
            this.labelEdadMinima.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelEdadMinima.Location = new System.Drawing.Point(40, 120);
            this.labelEdadMinima.Name = "labelEdadMinima";
            this.labelEdadMinima.Size = new System.Drawing.Size(84, 17);
            this.labelEdadMinima.TabIndex = 4;
            this.labelEdadMinima.Text = "Edad minima";
            // 
            // numEdadMinima
            // 
            this.numEdadMinima.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numEdadMinima.Location = new System.Drawing.Point(170, 116);
            this.numEdadMinima.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numEdadMinima.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEdadMinima.Name = "numEdadMinima";
            this.numEdadMinima.Size = new System.Drawing.Size(80, 25);
            this.numEdadMinima.TabIndex = 5;
            this.numEdadMinima.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // labelEdadMaxima
            // 
            this.labelEdadMaxima.AutoSize = true;
            this.labelEdadMaxima.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelEdadMaxima.Location = new System.Drawing.Point(40, 160);
            this.labelEdadMaxima.Name = "labelEdadMaxima";
            this.labelEdadMaxima.Size = new System.Drawing.Size(87, 17);
            this.labelEdadMaxima.TabIndex = 6;
            this.labelEdadMaxima.Text = "Edad maxima";
            // 
            // numEdadMaxima
            // 
            this.numEdadMaxima.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numEdadMaxima.Location = new System.Drawing.Point(170, 156);
            this.numEdadMaxima.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numEdadMaxima.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numEdadMaxima.Name = "numEdadMaxima";
            this.numEdadMaxima.Size = new System.Drawing.Size(80, 25);
            this.numEdadMaxima.TabIndex = 7;
            this.numEdadMaxima.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // labelCupo
            // 
            this.labelCupo.AutoSize = true;
            this.labelCupo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelCupo.Location = new System.Drawing.Point(300, 120);
            this.labelCupo.Name = "labelCupo";
            this.labelCupo.Size = new System.Drawing.Size(89, 17);
            this.labelCupo.TabIndex = 8;
            this.labelCupo.Text = "Cupo maximo";
            // 
            // numCupo
            // 
            this.numCupo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numCupo.Location = new System.Drawing.Point(430, 116);
            this.numCupo.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCupo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCupo.Name = "numCupo";
            this.numCupo.Size = new System.Drawing.Size(80, 25);
            this.numCupo.TabIndex = 9;
            this.numCupo.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelHorarioInicio
            // 
            this.labelHorarioInicio.AutoSize = true;
            this.labelHorarioInicio.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelHorarioInicio.Location = new System.Drawing.Point(545, 40);
            this.labelHorarioInicio.Name = "labelHorarioInicio";
            this.labelHorarioInicio.Size = new System.Drawing.Size(106, 17);
            this.labelHorarioInicio.TabIndex = 10;
            this.labelHorarioInicio.Text = "Horario de inicio";
            // 
            // dtpHorarioInicio
            // 
            this.dtpHorarioInicio.CustomFormat = "HH:mm";
            this.dtpHorarioInicio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHorarioInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHorarioInicio.Location = new System.Drawing.Point(660, 36);
            this.dtpHorarioInicio.Name = "dtpHorarioInicio";
            this.dtpHorarioInicio.ShowUpDown = true;
            this.dtpHorarioInicio.Size = new System.Drawing.Size(90, 25);
            this.dtpHorarioInicio.TabIndex = 11;
            // 
            // labelHorarioFin
            // 
            this.labelHorarioFin.AutoSize = true;
            this.labelHorarioFin.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelHorarioFin.Location = new System.Drawing.Point(545, 80);
            this.labelHorarioFin.Name = "labelHorarioFin";
            this.labelHorarioFin.Size = new System.Drawing.Size(90, 17);
            this.labelHorarioFin.TabIndex = 12;
            this.labelHorarioFin.Text = "Horario de fin";
            // 
            // dtpHorarioFin
            // 
            this.dtpHorarioFin.CustomFormat = "HH:mm";
            this.dtpHorarioFin.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpHorarioFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHorarioFin.Location = new System.Drawing.Point(660, 76);
            this.dtpHorarioFin.Name = "dtpHorarioFin";
            this.dtpHorarioFin.ShowUpDown = true;
            this.dtpHorarioFin.Size = new System.Drawing.Size(90, 25);
            this.dtpHorarioFin.TabIndex = 13;
            // 
            // labelDias
            // 
            this.labelDias.AutoSize = true;
            this.labelDias.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelDias.Location = new System.Drawing.Point(770, 40);
            this.labelDias.Name = "labelDias";
            this.labelDias.Size = new System.Drawing.Size(33, 17);
            this.labelDias.TabIndex = 14;
            this.labelDias.Text = "Dias";
            // 
            // clbDias
            // 
            this.clbDias.CheckOnClick = true;
            this.clbDias.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.clbDias.FormattingEnabled = true;
            this.clbDias.Location = new System.Drawing.Point(770, 64);
            this.clbDias.Name = "clbDias";
            this.clbDias.Size = new System.Drawing.Size(160, 164);
            this.clbDias.TabIndex = 15;
            // 
            // RegistrarClase19M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.Controls.Add(this.gbDatos);
            this.Controls.Add(this.gbClases);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1016, 699);
            this.Name = "RegistrarClase19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Clase";
            this.Load += new System.EventHandler(this.RegistrarClase19M_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.gbClases.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClases)).EndInit();
            this.gbDatos.ResumeLayout(false);
            this.gbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEdadMinima)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEdadMaxima)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCupo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbClases;
        private System.Windows.Forms.DataGridView dgvClases;
        private System.Windows.Forms.GroupBox gbDatos;
        private System.Windows.Forms.Label labelTipoClase;
        private System.Windows.Forms.ComboBox cmbTipoClase;
        private System.Windows.Forms.Label labelNivel;
        private System.Windows.Forms.ComboBox cmbNivel;
        private System.Windows.Forms.Label labelEdadMinima;
        private System.Windows.Forms.NumericUpDown numEdadMinima;
        private System.Windows.Forms.Label labelEdadMaxima;
        private System.Windows.Forms.NumericUpDown numEdadMaxima;
        private System.Windows.Forms.Label labelCupo;
        private System.Windows.Forms.NumericUpDown numCupo;
        private System.Windows.Forms.Label labelHorarioInicio;
        private System.Windows.Forms.DateTimePicker dtpHorarioInicio;
        private System.Windows.Forms.Label labelHorarioFin;
        private System.Windows.Forms.DateTimePicker dtpHorarioFin;
        private System.Windows.Forms.Label labelDias;
        private System.Windows.Forms.CheckedListBox clbDias;
    }
}
