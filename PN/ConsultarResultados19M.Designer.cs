namespace Servicios
{
    partial class ConsultarResultados19M
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
            this.gbRanking = new System.Windows.Forms.GroupBox();
            this.dgvRanking = new System.Windows.Forms.DataGridView();
            this.gbCobro = new System.Windows.Forms.GroupBox();
            this.lblArancel = new System.Windows.Forms.Label();
            this.txtArancel = new System.Windows.Forms.TextBox();
            this.lblEstadoPago = new System.Windows.Forms.Label();
            this.txtEstadoPago = new System.Windows.Forms.TextBox();
            this.btnCobrar = new System.Windows.Forms.Button();
            this.btnImprimirCertificado = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.gbTorneos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorneos)).BeginInit();
            this.gbRanking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).BeginInit();
            this.gbCobro.SuspendLayout();
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
            this.lblTitulo.Size = new System.Drawing.Size(266, 32);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "Ranking de los Torneos";
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
            this.gbTorneos.Size = new System.Drawing.Size(960, 170);
            this.gbTorneos.TabIndex = 1;
            this.gbTorneos.TabStop = false;
            this.gbTorneos.Text = "Torneos finalizados";
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
            this.dgvTorneos.Location = new System.Drawing.Point(12, 26);
            this.dgvTorneos.MultiSelect = false;
            this.dgvTorneos.Name = "dgvTorneos";
            this.dgvTorneos.ReadOnly = true;
            this.dgvTorneos.RowHeadersVisible = false;
            this.dgvTorneos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTorneos.Size = new System.Drawing.Size(936, 132);
            this.dgvTorneos.TabIndex = 0;
            this.dgvTorneos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTorneos_CellClick);
            // 
            // gbRanking
            // 
            this.gbRanking.Controls.Add(this.dgvRanking);
            this.gbRanking.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbRanking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbRanking.Location = new System.Drawing.Point(20, 266);
            this.gbRanking.Name = "gbRanking";
            this.gbRanking.Size = new System.Drawing.Size(632, 330);
            this.gbRanking.TabIndex = 2;
            this.gbRanking.TabStop = false;
            this.gbRanking.Text = "Ranking oficial del torneo";
            // 
            // dgvRanking
            // 
            this.dgvRanking.AllowUserToAddRows = false;
            this.dgvRanking.AllowUserToDeleteRows = false;
            this.dgvRanking.AllowUserToResizeRows = false;
            this.dgvRanking.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRanking.BackgroundColor = System.Drawing.Color.White;
            this.dgvRanking.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRanking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRanking.Location = new System.Drawing.Point(12, 26);
            this.dgvRanking.MultiSelect = false;
            this.dgvRanking.Name = "dgvRanking";
            this.dgvRanking.ReadOnly = true;
            this.dgvRanking.RowHeadersVisible = false;
            this.dgvRanking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRanking.Size = new System.Drawing.Size(608, 292);
            this.dgvRanking.TabIndex = 0;
            this.dgvRanking.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRanking_CellClick);
            // 
            // gbCobro
            // 
            this.gbCobro.Controls.Add(this.btnImprimirCertificado);
            this.gbCobro.Controls.Add(this.btnCobrar);
            this.gbCobro.Controls.Add(this.txtEstadoPago);
            this.gbCobro.Controls.Add(this.lblEstadoPago);
            this.gbCobro.Controls.Add(this.txtArancel);
            this.gbCobro.Controls.Add(this.lblArancel);
            this.gbCobro.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.gbCobro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.gbCobro.Location = new System.Drawing.Point(664, 266);
            this.gbCobro.Name = "gbCobro";
            this.gbCobro.Size = new System.Drawing.Size(316, 330);
            this.gbCobro.TabIndex = 3;
            this.gbCobro.TabStop = false;
            this.gbCobro.Text = "Cobro del arancel";
            // 
            // lblArancel
            // 
            this.lblArancel.AutoSize = true;
            this.lblArancel.Location = new System.Drawing.Point(20, 40);
            this.lblArancel.Name = "lblArancel";
            this.lblArancel.Size = new System.Drawing.Size(59, 19);
            this.lblArancel.TabIndex = 0;
            this.lblArancel.Text = "Arancel";
            // 
            // txtArancel
            // 
            this.txtArancel.BackColor = System.Drawing.Color.White;
            this.txtArancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtArancel.Location = new System.Drawing.Point(20, 62);
            this.txtArancel.Name = "txtArancel";
            this.txtArancel.ReadOnly = true;
            this.txtArancel.Size = new System.Drawing.Size(276, 26);
            this.txtArancel.TabIndex = 1;
            // 
            // lblEstadoPago
            // 
            this.lblEstadoPago.AutoSize = true;
            this.lblEstadoPago.Location = new System.Drawing.Point(20, 110);
            this.lblEstadoPago.Name = "lblEstadoPago";
            this.lblEstadoPago.Size = new System.Drawing.Size(100, 19);
            this.lblEstadoPago.TabIndex = 2;
            this.lblEstadoPago.Text = "Estado de pago";
            // 
            // txtEstadoPago
            // 
            this.txtEstadoPago.BackColor = System.Drawing.Color.White;
            this.txtEstadoPago.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEstadoPago.Location = new System.Drawing.Point(20, 132);
            this.txtEstadoPago.Name = "txtEstadoPago";
            this.txtEstadoPago.ReadOnly = true;
            this.txtEstadoPago.Size = new System.Drawing.Size(276, 26);
            this.txtEstadoPago.TabIndex = 3;
            // 
            // btnCobrar
            // 
            this.btnCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.btnCobrar.FlatAppearance.BorderSize = 0;
            this.btnCobrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobrar.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCobrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.btnCobrar.Location = new System.Drawing.Point(20, 200);
            this.btnCobrar.Name = "btnCobrar";
            this.btnCobrar.Size = new System.Drawing.Size(276, 40);
            this.btnCobrar.TabIndex = 4;
            this.btnCobrar.Text = "Cobrar";
            this.btnCobrar.UseVisualStyleBackColor = false;
            this.btnCobrar.Click += new System.EventHandler(this.btnCobrar_Click);
            // 
            // btnImprimirCertificado
            // 
            this.btnImprimirCertificado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.btnImprimirCertificado.FlatAppearance.BorderSize = 0;
            this.btnImprimirCertificado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirCertificado.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnImprimirCertificado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.btnImprimirCertificado.Location = new System.Drawing.Point(20, 255);
            this.btnImprimirCertificado.Name = "btnImprimirCertificado";
            this.btnImprimirCertificado.Size = new System.Drawing.Size(276, 40);
            this.btnImprimirCertificado.TabIndex = 5;
            this.btnImprimirCertificado.Text = "Imprimir certificado";
            this.btnImprimirCertificado.UseVisualStyleBackColor = false;
            this.btnImprimirCertificado.Click += new System.EventHandler(this.btnImprimirCertificado_Click);
            // 
            // ConsultarResultados19M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 660);
            this.Controls.Add(this.gbCobro);
            this.Controls.Add(this.gbRanking);
            this.Controls.Add(this.gbTorneos);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ConsultarResultados19M";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ranking de los Torneos";
            this.Load += new System.EventHandler(this.ConsultarResultados19M_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.gbTorneos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTorneos)).EndInit();
            this.gbRanking.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRanking)).EndInit();
            this.gbCobro.ResumeLayout(false);
            this.gbCobro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.GroupBox gbTorneos;
        private System.Windows.Forms.DataGridView dgvTorneos;
        private System.Windows.Forms.GroupBox gbRanking;
        private System.Windows.Forms.DataGridView dgvRanking;
        private System.Windows.Forms.GroupBox gbCobro;
        private System.Windows.Forms.Label lblArancel;
        private System.Windows.Forms.TextBox txtArancel;
        private System.Windows.Forms.Label lblEstadoPago;
        private System.Windows.Forms.TextBox txtEstadoPago;
        private System.Windows.Forms.Button btnCobrar;
        private System.Windows.Forms.Button btnImprimirCertificado;
    }
}
