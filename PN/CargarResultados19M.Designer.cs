namespace Servicios
{
    partial class CargarResultados19M
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
            this.gbResultados = new System.Windows.Forms.GroupBox();
            this.dgvResultados = new System.Windows.Forms.DataGridView();
            this.gbCargaTiempo = new System.Windows.Forms.GroupBox();
            this.lblMinutos = new System.Windows.Forms.Label();
            this.txtMinutos = new System.Windows.Forms.TextBox();
            this.lblSegundos = new System.Windows.Forms.Label();
            this.txtSegundos = new System.Windows.Forms.TextBox();
            this.lblCentesimas = new System.Windows.Forms.Label();
            this.txtCentesimas = new System.Windows.Forms.TextBox();
            this.chkDescalificado = new System.Windows.Forms.CheckBox();
            this.lblTiempoACargar = new System.Windows.Forms.Label();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnFinalizar = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.gbTorneos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorneos)).BeginInit();
            this.gbResultados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).BeginInit();
            this.gbCargaTiempo.SuspendLayout();
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
            this.lblTitulo.Size = new System.Drawing.Size(255, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Cargar Resultados del Torneo";
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
            this.gbTorneos.Size = new System.Drawing.Size(960, 210);
            this.gbTorneos.TabIndex = 1;
            this.gbTorneos.TabStop = false;
            this.gbTorneos.Text = "Torneos para finalizar";
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
            this.dgvTorneos.Size = new System.Drawing.Size(936, 172);
            this.dgvTorneos.TabIndex = 0;
            this.dgvTorneos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTorneos_CellClick);
            //
            // gbResultados
            //
            this.gbResultados.Controls.Add(this.dgvResultados);
            this.gbResultados.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbResultados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbResultados.Location = new System.Drawing.Point(20, 306);
            this.gbResultados.Name = "gbResultados";
            this.gbResultados.Size = new System.Drawing.Size(656, 330);
            this.gbResultados.TabIndex = 2;
            this.gbResultados.TabStop = false;
            this.gbResultados.Text = "Tiempos de los nadadores inscriptos";
            //
            // dgvResultados
            //
            this.dgvResultados.AllowUserToAddRows = false;
            this.dgvResultados.AllowUserToDeleteRows = false;
            this.dgvResultados.AllowUserToResizeRows = false;
            this.dgvResultados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultados.BackgroundColor = System.Drawing.Color.White;
            this.dgvResultados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultados.Columns.Clear();
            this.dgvResultados.Location = new System.Drawing.Point(12, 26);
            this.dgvResultados.MultiSelect = false;
            this.dgvResultados.Name = "dgvResultados";
            this.dgvResultados.ReadOnly = true;
            this.dgvResultados.RowHeadersVisible = false;
            this.dgvResultados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultados.Size = new System.Drawing.Size(632, 292);
            this.dgvResultados.TabIndex = 0;
            this.dgvResultados.SelectionChanged += new System.EventHandler(this.dgvResultados_SelectionChanged);
            this.dgvResultados.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResultados_CellFormatting);
            //
            // gbCargaTiempo
            //
            this.gbCargaTiempo.Controls.Add(this.lblMinutos);
            this.gbCargaTiempo.Controls.Add(this.txtMinutos);
            this.gbCargaTiempo.Controls.Add(this.lblSegundos);
            this.gbCargaTiempo.Controls.Add(this.txtSegundos);
            this.gbCargaTiempo.Controls.Add(this.lblCentesimas);
            this.gbCargaTiempo.Controls.Add(this.txtCentesimas);
            this.gbCargaTiempo.Controls.Add(this.chkDescalificado);
            this.gbCargaTiempo.Controls.Add(this.lblTiempoACargar);
            this.gbCargaTiempo.Controls.Add(this.lblTiempo);
            this.gbCargaTiempo.Controls.Add(this.btnAplicar);
            this.gbCargaTiempo.Enabled = false;
            this.gbCargaTiempo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbCargaTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbCargaTiempo.Location = new System.Drawing.Point(692, 306);
            this.gbCargaTiempo.Name = "gbCargaTiempo";
            this.gbCargaTiempo.Size = new System.Drawing.Size(288, 330);
            this.gbCargaTiempo.TabIndex = 3;
            this.gbCargaTiempo.TabStop = false;
            this.gbCargaTiempo.Text = "Cargar tiempo";
            //
            // lblMinutos
            //
            this.lblMinutos.AutoSize = true;
            this.lblMinutos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMinutos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.lblMinutos.Location = new System.Drawing.Point(14, 32);
            this.lblMinutos.Name = "lblMinutos";
            this.lblMinutos.Size = new System.Drawing.Size(52, 15);
            this.lblMinutos.TabIndex = 0;
            this.lblMinutos.Text = "Minutos";
            //
            // txtMinutos
            //
            this.txtMinutos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMinutos.Location = new System.Drawing.Point(96, 29);
            this.txtMinutos.MaxLength = 3;
            this.txtMinutos.Name = "txtMinutos";
            this.txtMinutos.Size = new System.Drawing.Size(80, 23);
            this.txtMinutos.TabIndex = 1;
            this.txtMinutos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMinutos.TextChanged += new System.EventHandler(this.txtTiempo_TextChanged);
            //
            // lblSegundos
            //
            this.lblSegundos.AutoSize = true;
            this.lblSegundos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSegundos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.lblSegundos.Location = new System.Drawing.Point(14, 64);
            this.lblSegundos.Name = "lblSegundos";
            this.lblSegundos.Size = new System.Drawing.Size(59, 15);
            this.lblSegundos.TabIndex = 2;
            this.lblSegundos.Text = "Segundos";
            //
            // txtSegundos
            //
            this.txtSegundos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSegundos.Location = new System.Drawing.Point(96, 61);
            this.txtSegundos.MaxLength = 2;
            this.txtSegundos.Name = "txtSegundos";
            this.txtSegundos.Size = new System.Drawing.Size(80, 23);
            this.txtSegundos.TabIndex = 3;
            this.txtSegundos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSegundos.TextChanged += new System.EventHandler(this.txtTiempo_TextChanged);
            //
            // lblCentesimas
            //
            this.lblCentesimas.AutoSize = true;
            this.lblCentesimas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCentesimas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.lblCentesimas.Location = new System.Drawing.Point(14, 96);
            this.lblCentesimas.Name = "lblCentesimas";
            this.lblCentesimas.Size = new System.Drawing.Size(70, 15);
            this.lblCentesimas.TabIndex = 4;
            this.lblCentesimas.Text = "Centesimas";
            //
            // txtCentesimas
            //
            this.txtCentesimas.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCentesimas.Location = new System.Drawing.Point(96, 93);
            this.txtCentesimas.MaxLength = 2;
            this.txtCentesimas.Name = "txtCentesimas";
            this.txtCentesimas.Size = new System.Drawing.Size(80, 23);
            this.txtCentesimas.TabIndex = 5;
            this.txtCentesimas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCentesimas.TextChanged += new System.EventHandler(this.txtTiempo_TextChanged);
            //
            // chkDescalificado
            //
            this.chkDescalificado.AutoSize = true;
            this.chkDescalificado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkDescalificado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.chkDescalificado.Location = new System.Drawing.Point(14, 128);
            this.chkDescalificado.Name = "chkDescalificado";
            this.chkDescalificado.Size = new System.Drawing.Size(93, 20);
            this.chkDescalificado.TabIndex = 6;
            this.chkDescalificado.Text = "Descalificado";
            this.chkDescalificado.UseVisualStyleBackColor = true;
            this.chkDescalificado.CheckedChanged += new System.EventHandler(this.chkDescalificado_CheckedChanged);
            //
            // lblTiempoACargar
            //
            this.lblTiempoACargar.AutoSize = true;
            this.lblTiempoACargar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblTiempoACargar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.lblTiempoACargar.Location = new System.Drawing.Point(14, 166);
            this.lblTiempoACargar.Name = "lblTiempoACargar";
            this.lblTiempoACargar.Size = new System.Drawing.Size(97, 15);
            this.lblTiempoACargar.TabIndex = 7;
            this.lblTiempoACargar.Text = "Tiempo a cargar:";
            //
            // lblTiempo
            //
            this.lblTiempo.AutoSize = false;
            this.lblTiempo.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.lblTiempo.Location = new System.Drawing.Point(14, 186);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(260, 40);
            this.lblTiempo.TabIndex = 8;
            this.lblTiempo.Text = "0'00\"00";
            this.lblTiempo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnAplicar
            //
            this.btnAplicar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(14, 246);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(260, 36);
            this.btnAplicar.TabIndex = 9;
            this.btnAplicar.Text = "Aplicar al nadador";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            //
            // pnlBotones
            //
            this.pnlBotones.Controls.Add(this.btnFinalizar);
            this.pnlBotones.Location = new System.Drawing.Point(20, 648);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(960, 56);
            this.pnlBotones.TabIndex = 4;
            //
            // btnFinalizar
            //
            this.btnFinalizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(180)))), ((int)(((byte)(216)))));
            this.btnFinalizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalizar.Enabled = false;
            this.btnFinalizar.FlatAppearance.BorderSize = 0;
            this.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnFinalizar.ForeColor = System.Drawing.Color.White;
            this.btnFinalizar.Location = new System.Drawing.Point(14, 11);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(220, 34);
            this.btnFinalizar.TabIndex = 0;
            this.btnFinalizar.Text = "Finalizar torneo y guardar resultados";
            this.btnFinalizar.UseVisualStyleBackColor = false;
            this.btnFinalizar.Click += new System.EventHandler(this.btnFinalizar_Click);
            //
            // CargarResultados19M
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.gbCargaTiempo);
            this.Controls.Add(this.gbResultados);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.gbTorneos);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 720);
            this.Name = "CargarResultados19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cargar Resultados del Torneo";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.gbTorneos.ResumeLayout(false);
            this.gbTorneos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorneos)).EndInit();
            this.gbResultados.ResumeLayout(false);
            this.gbResultados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultados)).EndInit();
            this.gbCargaTiempo.ResumeLayout(false);
            this.gbCargaTiempo.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.GroupBox gbTorneos;
        private System.Windows.Forms.DataGridView dgvTorneos;
        private System.Windows.Forms.GroupBox gbResultados;
        private System.Windows.Forms.DataGridView dgvResultados;
        private System.Windows.Forms.GroupBox gbCargaTiempo;
        private System.Windows.Forms.Label lblMinutos;
        private System.Windows.Forms.TextBox txtMinutos;
        private System.Windows.Forms.Label lblSegundos;
        private System.Windows.Forms.TextBox txtSegundos;
        private System.Windows.Forms.Label lblCentesimas;
        private System.Windows.Forms.TextBox txtCentesimas;
        private System.Windows.Forms.CheckBox chkDescalificado;
        private System.Windows.Forms.Label lblTiempoACargar;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Panel pnlBotones;
        private System.Windows.Forms.Button btnFinalizar;
    }
}
