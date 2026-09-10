namespace Servicios
{
    partial class GestionRespaldo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRutaBackUp = new System.Windows.Forms.TextBox();
            this.buscarCarpetaBackUp = new System.Windows.Forms.Button();
            this.btnRealizarBackUp = new System.Windows.Forms.Button();
            this.btnRealizarRestore = new System.Windows.Forms.Button();
            this.buscarCarpetaRestore = new System.Windows.Forms.Button();
            this.txtRutaRestore = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtRutaBackUp
            // 
            this.txtRutaBackUp.Location = new System.Drawing.Point(42, 124);
            this.txtRutaBackUp.Name = "txtRutaBackUp";
            this.txtRutaBackUp.Size = new System.Drawing.Size(494, 31);
            this.txtRutaBackUp.TabIndex = 0;
            // 
            // buscarCarpetaBackUp
            // 
            this.buscarCarpetaBackUp.BackColor = System.Drawing.Color.SteelBlue;
            this.buscarCarpetaBackUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarCarpetaBackUp.ForeColor = System.Drawing.SystemColors.Control;
            this.buscarCarpetaBackUp.Location = new System.Drawing.Point(555, 104);
            this.buscarCarpetaBackUp.Name = "buscarCarpetaBackUp";
            this.buscarCarpetaBackUp.Size = new System.Drawing.Size(75, 58);
            this.buscarCarpetaBackUp.TabIndex = 1;
            this.buscarCarpetaBackUp.Text = "🗂️";
            this.buscarCarpetaBackUp.UseVisualStyleBackColor = false;
            this.buscarCarpetaBackUp.Click += new System.EventHandler(this.buscarCarpetaBackUp_Click);
            // 
            // btnRealizarBackUp
            // 
            this.btnRealizarBackUp.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRealizarBackUp.Font = new System.Drawing.Font("Verdana", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarBackUp.ForeColor = System.Drawing.Color.MintCream;
            this.btnRealizarBackUp.Location = new System.Drawing.Point(42, 162);
            this.btnRealizarBackUp.Margin = new System.Windows.Forms.Padding(4);
            this.btnRealizarBackUp.Name = "btnRealizarBackUp";
            this.btnRealizarBackUp.Size = new System.Drawing.Size(312, 63);
            this.btnRealizarBackUp.TabIndex = 9;
            this.btnRealizarBackUp.Text = "Realizar BackUp";
            this.btnRealizarBackUp.UseVisualStyleBackColor = false;
            this.btnRealizarBackUp.Click += new System.EventHandler(this.btnRealizarBackUp_Click);
            // 
            // btnRealizarRestore
            // 
            this.btnRealizarRestore.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRealizarRestore.Font = new System.Drawing.Font("Verdana", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRealizarRestore.ForeColor = System.Drawing.Color.MintCream;
            this.btnRealizarRestore.Location = new System.Drawing.Point(42, 321);
            this.btnRealizarRestore.Margin = new System.Windows.Forms.Padding(4);
            this.btnRealizarRestore.Name = "btnRealizarRestore";
            this.btnRealizarRestore.Size = new System.Drawing.Size(312, 63);
            this.btnRealizarRestore.TabIndex = 12;
            this.btnRealizarRestore.Text = "Realizar Restore";
            this.btnRealizarRestore.UseVisualStyleBackColor = false;
            this.btnRealizarRestore.Click += new System.EventHandler(this.btnRealizarRestore_Click);
            // 
            // buscarCarpetaRestore
            // 
            this.buscarCarpetaRestore.BackColor = System.Drawing.Color.SteelBlue;
            this.buscarCarpetaRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buscarCarpetaRestore.ForeColor = System.Drawing.SystemColors.Control;
            this.buscarCarpetaRestore.Location = new System.Drawing.Point(555, 263);
            this.buscarCarpetaRestore.Name = "buscarCarpetaRestore";
            this.buscarCarpetaRestore.Size = new System.Drawing.Size(75, 58);
            this.buscarCarpetaRestore.TabIndex = 11;
            this.buscarCarpetaRestore.Text = "🗂️";
            this.buscarCarpetaRestore.UseVisualStyleBackColor = false;
            this.buscarCarpetaRestore.Click += new System.EventHandler(this.buscarCarpetaRestore_Click);
            // 
            // txtRutaRestore
            // 
            this.txtRutaRestore.Location = new System.Drawing.Point(42, 283);
            this.txtRutaRestore.Name = "txtRutaRestore";
            this.txtRutaRestore.Size = new System.Drawing.Size(494, 31);
            this.txtRutaRestore.TabIndex = 10;
            // 
            // GestionRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(762, 485);
            this.Controls.Add(this.btnRealizarRestore);
            this.Controls.Add(this.buscarCarpetaRestore);
            this.Controls.Add(this.txtRutaRestore);
            this.Controls.Add(this.btnRealizarBackUp);
            this.Controls.Add(this.buscarCarpetaBackUp);
            this.Controls.Add(this.txtRutaBackUp);
            this.Name = "GestionRespaldo";
            this.Text = "GestionRespaldo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRutaBackUp;
        private System.Windows.Forms.Button buscarCarpetaBackUp;
        private System.Windows.Forms.Button btnRealizarBackUp;
        private System.Windows.Forms.Button btnRealizarRestore;
        private System.Windows.Forms.Button buscarCarpetaRestore;
        private System.Windows.Forms.TextBox txtRutaRestore;
    }
}