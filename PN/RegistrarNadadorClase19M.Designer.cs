namespace Servicios
{
    partial class RegistrarNadadorClase19M
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
            this.gbClase = new System.Windows.Forms.GroupBox();
            this.txtCupoClase = new System.Windows.Forms.TextBox();
            this.lblCupoClase = new System.Windows.Forms.Label();
            this.txtNivelClase = new System.Windows.Forms.TextBox();
            this.lblNivelClase = new System.Windows.Forms.Label();
            this.txtRangoEtarioClase = new System.Windows.Forms.TextBox();
            this.lblRangoEtarioClase = new System.Windows.Forms.Label();
            this.cmbClase = new System.Windows.Forms.ComboBox();
            this.labelClase = new System.Windows.Forms.Label();
            this.gbNadador = new System.Windows.Forms.GroupBox();
            this.txtEdadNadador = new System.Windows.Forms.TextBox();
            this.lblEdadNadador = new System.Windows.Forms.Label();
            this.txtCategoriaNadador = new System.Windows.Forms.TextBox();
            this.lblCategoriaNadador = new System.Windows.Forms.Label();
            this.cmbNadador = new System.Windows.Forms.ComboBox();
            this.labelNadador = new System.Windows.Forms.Label();
            this.gbInscripciones = new System.Windows.Forms.GroupBox();
            this.dgvInscripciones = new System.Windows.Forms.DataGridView();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.gbClase.SuspendLayout();
            this.gbNadador.SuspendLayout();
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
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(26, 33);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(297, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Registrar Nadador a Clase";
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
            // gbClase
            // 
            this.gbClase.Controls.Add(this.txtCupoClase);
            this.gbClase.Controls.Add(this.lblCupoClase);
            this.gbClase.Controls.Add(this.txtNivelClase);
            this.gbClase.Controls.Add(this.lblNivelClase);
            this.gbClase.Controls.Add(this.txtRangoEtarioClase);
            this.gbClase.Controls.Add(this.lblRangoEtarioClase);
            this.gbClase.Controls.Add(this.cmbClase);
            this.gbClase.Controls.Add(this.labelClase);
            this.gbClase.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbClase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbClase.Location = new System.Drawing.Point(20, 84);
            this.gbClase.Name = "gbClase";
            this.gbClase.Size = new System.Drawing.Size(960, 100);
            this.gbClase.TabIndex = 1;
            this.gbClase.TabStop = false;
            this.gbClase.Text = "Clase";
            // 
            // txtCupoClase
            // 
            this.txtCupoClase.BackColor = System.Drawing.Color.White;
            this.txtCupoClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCupoClase.Location = new System.Drawing.Point(660, 61);
            this.txtCupoClase.Name = "txtCupoClase";
            this.txtCupoClase.ReadOnly = true;
            this.txtCupoClase.Size = new System.Drawing.Size(150, 25);
            this.txtCupoClase.TabIndex = 7;
            // 
            // lblCupoClase
            // 
            this.lblCupoClase.AutoSize = true;
            this.lblCupoClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCupoClase.Location = new System.Drawing.Point(590, 64);
            this.lblCupoClase.Name = "lblCupoClase";
            this.lblCupoClase.Size = new System.Drawing.Size(42, 19);
            this.lblCupoClase.TabIndex = 6;
            this.lblCupoClase.Text = "Cupo";
            // 
            // txtNivelClase
            // 
            this.txtNivelClase.BackColor = System.Drawing.Color.White;
            this.txtNivelClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNivelClase.Location = new System.Drawing.Point(400, 61);
            this.txtNivelClase.Name = "txtNivelClase";
            this.txtNivelClase.ReadOnly = true;
            this.txtNivelClase.Size = new System.Drawing.Size(150, 25);
            this.txtNivelClase.TabIndex = 5;
            // 
            // lblNivelClase
            // 
            this.lblNivelClase.AutoSize = true;
            this.lblNivelClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNivelClase.Location = new System.Drawing.Point(330, 64);
            this.lblNivelClase.Name = "lblNivelClase";
            this.lblNivelClase.Size = new System.Drawing.Size(39, 19);
            this.lblNivelClase.TabIndex = 4;
            this.lblNivelClase.Text = "Nivel";
            // 
            // txtRangoEtarioClase
            // 
            this.txtRangoEtarioClase.BackColor = System.Drawing.Color.White;
            this.txtRangoEtarioClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRangoEtarioClase.Location = new System.Drawing.Point(150, 61);
            this.txtRangoEtarioClase.Name = "txtRangoEtarioClase";
            this.txtRangoEtarioClase.ReadOnly = true;
            this.txtRangoEtarioClase.Size = new System.Drawing.Size(120, 25);
            this.txtRangoEtarioClase.TabIndex = 3;
            // 
            // lblRangoEtarioClase
            // 
            this.lblRangoEtarioClase.AutoSize = true;
            this.lblRangoEtarioClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRangoEtarioClase.Location = new System.Drawing.Point(20, 64);
            this.lblRangoEtarioClase.Name = "lblRangoEtarioClase";
            this.lblRangoEtarioClase.Size = new System.Drawing.Size(87, 19);
            this.lblRangoEtarioClase.TabIndex = 2;
            this.lblRangoEtarioClase.Text = "Rango etario";
            // 
            // cmbClase
            // 
            this.cmbClase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbClase.FormattingEnabled = true;
            this.cmbClase.Location = new System.Drawing.Point(150, 25);
            this.cmbClase.Name = "cmbClase";
            this.cmbClase.Size = new System.Drawing.Size(400, 25);
            this.cmbClase.TabIndex = 1;
            // 
            // labelClase
            // 
            this.labelClase.AutoSize = true;
            this.labelClase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelClase.Location = new System.Drawing.Point(20, 28);
            this.labelClase.Name = "labelClase";
            this.labelClase.Size = new System.Drawing.Size(41, 19);
            this.labelClase.TabIndex = 0;
            this.labelClase.Text = "Clase";
            // 
            // gbNadador
            // 
            this.gbNadador.Controls.Add(this.txtEdadNadador);
            this.gbNadador.Controls.Add(this.lblEdadNadador);
            this.gbNadador.Controls.Add(this.txtCategoriaNadador);
            this.gbNadador.Controls.Add(this.lblCategoriaNadador);
            this.gbNadador.Controls.Add(this.cmbNadador);
            this.gbNadador.Controls.Add(this.labelNadador);
            this.gbNadador.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbNadador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbNadador.Location = new System.Drawing.Point(20, 190);
            this.gbNadador.Name = "gbNadador";
            this.gbNadador.Size = new System.Drawing.Size(960, 100);
            this.gbNadador.TabIndex = 2;
            this.gbNadador.TabStop = false;
            this.gbNadador.Text = "Nadador";
            // 
            // txtEdadNadador
            // 
            this.txtEdadNadador.BackColor = System.Drawing.Color.White;
            this.txtEdadNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEdadNadador.Location = new System.Drawing.Point(400, 61);
            this.txtEdadNadador.Name = "txtEdadNadador";
            this.txtEdadNadador.ReadOnly = true;
            this.txtEdadNadador.Size = new System.Drawing.Size(150, 25);
            this.txtEdadNadador.TabIndex = 5;
            // 
            // lblEdadNadador
            // 
            this.lblEdadNadador.AutoSize = true;
            this.lblEdadNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEdadNadador.Location = new System.Drawing.Point(330, 64);
            this.lblEdadNadador.Name = "lblEdadNadador";
            this.lblEdadNadador.Size = new System.Drawing.Size(39, 19);
            this.lblEdadNadador.TabIndex = 4;
            this.lblEdadNadador.Text = "Edad";
            // 
            // txtCategoriaNadador
            // 
            this.txtCategoriaNadador.BackColor = System.Drawing.Color.White;
            this.txtCategoriaNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCategoriaNadador.Location = new System.Drawing.Point(150, 61);
            this.txtCategoriaNadador.Name = "txtCategoriaNadador";
            this.txtCategoriaNadador.ReadOnly = true;
            this.txtCategoriaNadador.Size = new System.Drawing.Size(150, 25);
            this.txtCategoriaNadador.TabIndex = 3;
            // 
            // lblCategoriaNadador
            // 
            this.lblCategoriaNadador.AutoSize = true;
            this.lblCategoriaNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategoriaNadador.Location = new System.Drawing.Point(20, 64);
            this.lblCategoriaNadador.Name = "lblCategoriaNadador";
            this.lblCategoriaNadador.Size = new System.Drawing.Size(68, 19);
            this.lblCategoriaNadador.TabIndex = 2;
            this.lblCategoriaNadador.Text = "Categoria";
            // 
            // cmbNadador
            // 
            this.cmbNadador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbNadador.FormattingEnabled = true;
            this.cmbNadador.Location = new System.Drawing.Point(150, 25);
            this.cmbNadador.Name = "cmbNadador";
            this.cmbNadador.Size = new System.Drawing.Size(400, 25);
            this.cmbNadador.TabIndex = 1;
            // 
            // labelNadador
            // 
            this.labelNadador.AutoSize = true;
            this.labelNadador.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelNadador.Location = new System.Drawing.Point(20, 28);
            this.labelNadador.Name = "labelNadador";
            this.labelNadador.Size = new System.Drawing.Size(62, 19);
            this.labelNadador.TabIndex = 0;
            this.labelNadador.Text = "Nadador";
            // 
            // gbInscripciones
            // 
            this.gbInscripciones.Controls.Add(this.dgvInscripciones);
            this.gbInscripciones.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbInscripciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbInscripciones.Location = new System.Drawing.Point(20, 296);
            this.gbInscripciones.Name = "gbInscripciones";
            this.gbInscripciones.Size = new System.Drawing.Size(960, 240);
            this.gbInscripciones.TabIndex = 3;
            this.gbInscripciones.TabStop = false;
            this.gbInscripciones.Text = "Inscripciones";
            // 
            // dgvInscripciones
            // 
            this.dgvInscripciones.AllowUserToAddRows = false;
            this.dgvInscripciones.AllowUserToDeleteRows = false;
            this.dgvInscripciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInscripciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInscripciones.Location = new System.Drawing.Point(20, 30);
            this.dgvInscripciones.MultiSelect = false;
            this.dgvInscripciones.Name = "dgvInscripciones";
            this.dgvInscripciones.ReadOnly = true;
            this.dgvInscripciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInscripciones.Size = new System.Drawing.Size(920, 195);
            this.dgvInscripciones.TabIndex = 0;
            // 
            // pnlBotones
            // 
            this.pnlBotones.Controls.Add(this.btnRegistrar);
            this.pnlBotones.Location = new System.Drawing.Point(20, 542);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(960, 50);
            this.pnlBotones.TabIndex = 4;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.btnRegistrar.FlatAppearance.BorderSize = 0;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.Location = new System.Drawing.Point(0, 5);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(200, 40);
            this.btnRegistrar.TabIndex = 0;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // RegistrarNadadorClase19M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 610);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.gbInscripciones);
            this.Controls.Add(this.gbNadador);
            this.Controls.Add(this.gbClase);
            this.Controls.Add(this.pnlHeader);
            this.Name = "RegistrarNadadorClase19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Nadador a Clase";
            this.Load += new System.EventHandler(this.RegistrarNadadorClase19M_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.gbClase.ResumeLayout(false);
            this.gbClase.PerformLayout();
            this.gbNadador.ResumeLayout(false);
            this.gbNadador.PerformLayout();
            this.gbInscripciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscripciones)).EndInit();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.GroupBox gbClase;
        private System.Windows.Forms.ComboBox cmbClase;
        private System.Windows.Forms.Label labelClase;
        private System.Windows.Forms.TextBox txtRangoEtarioClase;
        private System.Windows.Forms.Label lblRangoEtarioClase;
        private System.Windows.Forms.TextBox txtNivelClase;
        private System.Windows.Forms.Label lblNivelClase;
        private System.Windows.Forms.TextBox txtCupoClase;
        private System.Windows.Forms.Label lblCupoClase;
        private System.Windows.Forms.GroupBox gbNadador;
        private System.Windows.Forms.ComboBox cmbNadador;
        private System.Windows.Forms.Label labelNadador;
        private System.Windows.Forms.TextBox txtCategoriaNadador;
        private System.Windows.Forms.Label lblCategoriaNadador;
        private System.Windows.Forms.TextBox txtEdadNadador;
        private System.Windows.Forms.Label lblEdadNadador;
        private System.Windows.Forms.GroupBox gbInscripciones;
        private System.Windows.Forms.DataGridView dgvInscripciones;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnRegistrar;
    }
}