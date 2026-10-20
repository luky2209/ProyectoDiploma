namespace Servicios
{
    partial class AsignarProfesorClase19M
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblLogoApp = new System.Windows.Forms.Label();
            this.gbAsignacion = new System.Windows.Forms.GroupBox();
            this.labelClase = new System.Windows.Forms.Label();
            this.cmbClase = new System.Windows.Forms.ComboBox();
            this.labelProfesor = new System.Windows.Forms.Label();
            this.cmbProfesor = new System.Windows.Forms.ComboBox();
            this.labelFranja = new System.Windows.Forms.Label();
            this.txtFranja = new System.Windows.Forms.TextBox();
            this.labelFechaAsignacion = new System.Windows.Forms.Label();
            this.txtFechaAsignacion = new System.Windows.Forms.TextBox();
            this.labelEspecialidad = new System.Windows.Forms.Label();
            this.txtEspecialidad = new System.Windows.Forms.TextBox();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbClases = new System.Windows.Forms.GroupBox();
            this.dgvClases = new System.Windows.Forms.DataGridView();
            this.gbAsignados = new System.Windows.Forms.GroupBox();
            this.dgvAsignados = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.gbAsignacion.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            this.gbClases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClases)).BeginInit();
            this.gbAsignados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignados)).BeginInit();
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
            this.lblTitulo.Size = new System.Drawing.Size(260, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Asignar Profesor a Clase";
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
            // gbAsignacion
            // 
            this.gbAsignacion.Controls.Add(this.labelClase);
            this.gbAsignacion.Controls.Add(this.cmbClase);
            this.gbAsignacion.Controls.Add(this.labelProfesor);
            this.gbAsignacion.Controls.Add(this.cmbProfesor);
            this.gbAsignacion.Controls.Add(this.labelFranja);
            this.gbAsignacion.Controls.Add(this.txtFranja);
            this.gbAsignacion.Controls.Add(this.labelFechaAsignacion);
            this.gbAsignacion.Controls.Add(this.txtFechaAsignacion);
            this.gbAsignacion.Controls.Add(this.labelEspecialidad);
            this.gbAsignacion.Controls.Add(this.txtEspecialidad);
            this.gbAsignacion.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbAsignacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbAsignacion.Location = new System.Drawing.Point(20, 84);
            this.gbAsignacion.Name = "gbAsignacion";
            this.gbAsignacion.Size = new System.Drawing.Size(960, 140);
            this.gbAsignacion.TabIndex = 1;
            this.gbAsignacion.TabStop = false;
            this.gbAsignacion.Text = "Asignacion";
            // 
            // labelClase
            // 
            this.labelClase.AutoSize = true;
            this.labelClase.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelClase.Location = new System.Drawing.Point(30, 43);
            this.labelClase.Name = "labelClase";
            this.labelClase.Size = new System.Drawing.Size(38, 17);
            this.labelClase.TabIndex = 0;
            this.labelClase.Text = "Clase";
            // 
            // cmbClase
            // 
            this.cmbClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbClase.FormattingEnabled = true;
            this.cmbClase.Location = new System.Drawing.Point(150, 39);
            this.cmbClase.Name = "cmbClase";
            this.cmbClase.Size = new System.Drawing.Size(300, 25);
            this.cmbClase.TabIndex = 1;
            this.cmbClase.SelectedIndexChanged += new System.EventHandler(this.cmbClase_SelectedIndexChanged);
            // 
            // labelProfesor
            // 
            this.labelProfesor.AutoSize = true;
            this.labelProfesor.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelProfesor.Location = new System.Drawing.Point(30, 76);
            this.labelProfesor.Name = "labelProfesor";
            this.labelProfesor.Size = new System.Drawing.Size(57, 17);
            this.labelProfesor.TabIndex = 2;
            this.labelProfesor.Text = "Profesor";
            // 
            // cmbProfesor
            // 
            this.cmbProfesor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfesor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbProfesor.FormattingEnabled = true;
            this.cmbProfesor.Location = new System.Drawing.Point(150, 72);
            this.cmbProfesor.Name = "cmbProfesor";
            this.cmbProfesor.Size = new System.Drawing.Size(300, 25);
            this.cmbProfesor.TabIndex = 3;
            this.cmbProfesor.SelectedIndexChanged += new System.EventHandler(this.cmbProfesor_SelectedIndexChanged);
            // 
            // labelFranja
            // 
            this.labelFranja.AutoSize = true;
            this.labelFranja.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelFranja.Location = new System.Drawing.Point(30, 109);
            this.labelFranja.Name = "labelFranja";
            this.labelFranja.Size = new System.Drawing.Size(91, 17);
            this.labelFranja.TabIndex = 4;
            this.labelFranja.Text = "Franja horaria";
            // 
            // txtFranja
            // 
            this.txtFranja.BackColor = System.Drawing.Color.White;
            this.txtFranja.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFranja.Location = new System.Drawing.Point(150, 105);
            this.txtFranja.Name = "txtFranja";
            this.txtFranja.ReadOnly = true;
            this.txtFranja.Size = new System.Drawing.Size(300, 25);
            this.txtFranja.TabIndex = 5;
            // 
            // labelFechaAsignacion
            // 
            this.labelFechaAsignacion.AutoSize = true;
            this.labelFechaAsignacion.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelFechaAsignacion.Location = new System.Drawing.Point(490, 43);
            this.labelFechaAsignacion.Name = "labelFechaAsignacion";
            this.labelFechaAsignacion.Size = new System.Drawing.Size(123, 17);
            this.labelFechaAsignacion.TabIndex = 6;
            this.labelFechaAsignacion.Text = "Fecha de asignacion";
            // 
            // txtFechaAsignacion
            // 
            this.txtFechaAsignacion.BackColor = System.Drawing.Color.White;
            this.txtFechaAsignacion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFechaAsignacion.Location = new System.Drawing.Point(640, 39);
            this.txtFechaAsignacion.Name = "txtFechaAsignacion";
            this.txtFechaAsignacion.ReadOnly = true;
            this.txtFechaAsignacion.Size = new System.Drawing.Size(280, 25);
            this.txtFechaAsignacion.TabIndex = 7;
            // 
            // labelEspecialidad
            // 
            this.labelEspecialidad.AutoSize = true;
            this.labelEspecialidad.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.labelEspecialidad.Location = new System.Drawing.Point(490, 76);
            this.labelEspecialidad.Name = "labelEspecialidad";
            this.labelEspecialidad.Size = new System.Drawing.Size(78, 17);
            this.labelEspecialidad.TabIndex = 8;
            this.labelEspecialidad.Text = "Especialidad";
            // 
            // txtEspecialidad
            // 
            this.txtEspecialidad.BackColor = System.Drawing.Color.White;
            this.txtEspecialidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEspecialidad.Location = new System.Drawing.Point(640, 72);
            this.txtEspecialidad.Name = "txtEspecialidad";
            this.txtEspecialidad.ReadOnly = true;
            this.txtEspecialidad.Size = new System.Drawing.Size(280, 25);
            this.txtEspecialidad.TabIndex = 9;
            // 
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.btnAsignar);
            this.pnlBotones.Controls.Add(this.btnCancelar);
            this.pnlBotones.Location = new System.Drawing.Point(20, 234);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(460, 50);
            this.pnlBotones.TabIndex = 2;
            // 
            // btnAsignar
            // 
            this.btnAsignar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnAsignar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAsignar.FlatAppearance.BorderSize = 0;
            this.btnAsignar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAsignar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAsignar.ForeColor = System.Drawing.Color.White;
            this.btnAsignar.Location = new System.Drawing.Point(14, 8);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(180, 34);
            this.btnAsignar.TabIndex = 0;
            this.btnAsignar.Text = "Asignar";
            this.btnAsignar.UseVisualStyleBackColor = false;
            this.btnAsignar.Click += new System.EventHandler(this.btnAsignar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.btnCancelar.Location = new System.Drawing.Point(206, 8);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(180, 34);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbClases
            // 
            this.gbClases.Controls.Add(this.dgvClases);
            this.gbClases.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbClases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbClases.Location = new System.Drawing.Point(20, 294);
            this.gbClases.Name = "gbClases";
            this.gbClases.Size = new System.Drawing.Size(960, 180);
            this.gbClases.TabIndex = 3;
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
            this.dgvClases.Location = new System.Drawing.Point(12, 25);
            this.dgvClases.MultiSelect = false;
            this.dgvClases.Name = "dgvClases";
            this.dgvClases.ReadOnly = true;
            this.dgvClases.RowHeadersVisible = false;
            this.dgvClases.RowTemplate.Height = 26;
            this.dgvClases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClases.Size = new System.Drawing.Size(936, 142);
            this.dgvClases.TabIndex = 0;
            this.dgvClases.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClases_CellClick);
            // 
            // gbAsignados
            // 
            this.gbAsignados.Controls.Add(this.dgvAsignados);
            this.gbAsignados.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbAsignados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbAsignados.Location = new System.Drawing.Point(20, 484);
            this.gbAsignados.Name = "gbAsignados";
            this.gbAsignados.Size = new System.Drawing.Size(960, 180);
            this.gbAsignados.TabIndex = 4;
            this.gbAsignados.TabStop = false;
            this.gbAsignados.Text = "Profesores asignados";
            // 
            // dgvAsignados
            // 
            this.dgvAsignados.AllowUserToAddRows = false;
            this.dgvAsignados.AllowUserToDeleteRows = false;
            this.dgvAsignados.AllowUserToResizeRows = false;
            this.dgvAsignados.BackgroundColor = System.Drawing.Color.White;
            this.dgvAsignados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.dgvAsignados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAsignados.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvAsignados.Location = new System.Drawing.Point(12, 25);
            this.dgvAsignados.MultiSelect = false;
            this.dgvAsignados.Name = "dgvAsignados";
            this.dgvAsignados.ReadOnly = true;
            this.dgvAsignados.RowHeadersVisible = false;
            this.dgvAsignados.RowTemplate.Height = 26;
            this.dgvAsignados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAsignados.Size = new System.Drawing.Size(936, 142);
            this.dgvAsignados.TabIndex = 0;
            this.dgvAsignados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAsignados_CellClick);
            // 
            // AsignarProfesorClase19M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.gbAsignados);
            this.Controls.Add(this.gbClases);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.gbAsignacion);
            this.Controls.Add(this.pnlHeader);
            this.MinimumSize = new System.Drawing.Size(1016, 719);
            this.Name = "AsignarProfesorClase19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar Profesor a Clase";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.gbAsignacion.ResumeLayout(false);
            this.gbAsignacion.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.gbClases.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClases)).EndInit();
            this.gbAsignados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAsignados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.GroupBox gbAsignacion;
        private System.Windows.Forms.Label labelClase;
        private System.Windows.Forms.ComboBox cmbClase;
        private System.Windows.Forms.Label labelProfesor;
        private System.Windows.Forms.ComboBox cmbProfesor;
        private System.Windows.Forms.Label labelFranja;
        private System.Windows.Forms.TextBox txtFranja;
        private System.Windows.Forms.Label labelFechaAsignacion;
        private System.Windows.Forms.TextBox txtFechaAsignacion;
        private System.Windows.Forms.Label labelEspecialidad;
        private System.Windows.Forms.TextBox txtEspecialidad;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbClases;
        private System.Windows.Forms.DataGridView dgvClases;
        private System.Windows.Forms.GroupBox gbAsignados;
        private System.Windows.Forms.DataGridView dgvAsignados;
    }
}
