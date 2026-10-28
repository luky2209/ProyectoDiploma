namespace Servicios
{
    partial class BitacoraVersionesNadador19M
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

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dgvNadadores = new System.Windows.Forms.DataGridView();
            this.dgvHistorial = new System.Windows.Forms.DataGridView();
            this.lblDetalle = new System.Windows.Forms.Label();
            this.btnVerDetalle = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNadadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(12, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(300, 20);
            this.lblTitulo.Text = "Bitacora de versiones - Nadadores";
            //
            // txtBuscar
            //
            this.txtBuscar.Location = new System.Drawing.Point(12, 40);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(200, 20);
            //
            // btnBuscar
            //
            this.btnBuscar.Location = new System.Drawing.Point(218, 38);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            //
            // dgvNadadores
            //
            this.dgvNadadores.AllowUserToAddRows = false;
            this.dgvNadadores.AllowUserToDeleteRows = false;
            this.dgvNadadores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNadadores.Location = new System.Drawing.Point(12, 70);
            this.dgvNadadores.Name = "dgvNadadores";
            this.dgvNadadores.ReadOnly = true;
            this.dgvNadadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNadadores.Size = new System.Drawing.Size(300, 200);
            this.dgvNadadores.TabIndex = 0;
            this.dgvNadadores.SelectionChanged += new System.EventHandler(this.dgvNadadores_SelectionChanged);
            //
            // dgvHistorial
            //
            this.dgvHistorial.AllowUserToAddRows = false;
            this.dgvHistorial.AllowUserToDeleteRows = false;
            this.dgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistorial.Location = new System.Drawing.Point(320, 70);
            this.dgvHistorial.Name = "dgvHistorial";
            this.dgvHistorial.ReadOnly = true;
            this.dgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistorial.Size = new System.Drawing.Size(600, 200);
            this.dgvHistorial.TabIndex = 1;
            this.dgvHistorial.SelectionChanged += new System.EventHandler(this.dgvHistorial_SelectionChanged);
            //
            // lblDetalle
            //
            this.lblDetalle.AutoSize = false;
            this.lblDetalle.Location = new System.Drawing.Point(12, 280);
            this.lblDetalle.Name = "lblDetalle";
            this.lblDetalle.Size = new System.Drawing.Size(908, 40);
            this.lblDetalle.Text = "";
            //
            // btnVerDetalle
            //
            this.btnVerDetalle.Location = new System.Drawing.Point(12, 330);
            this.btnVerDetalle.Name = "btnVerDetalle";
            this.btnVerDetalle.Size = new System.Drawing.Size(100, 23);
            this.btnVerDetalle.Text = "Ver detalle";
            this.btnVerDetalle.UseVisualStyleBackColor = true;
            this.btnVerDetalle.Click += new System.EventHandler(this.btnVerDetalle_Click);
            //
            // btnRestaurar
            //
            this.btnRestaurar.Location = new System.Drawing.Point(118, 330);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(120, 23);
            this.btnRestaurar.Text = "Activar version";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            //
            // lblMensaje
            //
            this.lblMensaje.AutoSize = false;
            this.lblMensaje.Location = new System.Drawing.Point(244, 330);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(676, 23);
            this.lblMensaje.Text = "";
            //
            // BitacoraVersionesNadador19M
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 365);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvNadadores);
            this.Controls.Add(this.dgvHistorial);
            this.Controls.Add(this.lblDetalle);
            this.Controls.Add(this.btnVerDetalle);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.lblMensaje);
            this.Name = "BitacoraVersionesNadador19M";
            this.Text = "Bitacora de versiones - Nadadores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNadadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorial)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView dgvNadadores;
        private System.Windows.Forms.DataGridView dgvHistorial;
        private System.Windows.Forms.Label lblDetalle;
        private System.Windows.Forms.Button btnVerDetalle;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Label lblMensaje;
    }
}
