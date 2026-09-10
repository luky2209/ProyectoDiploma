namespace Servicios
{
    partial class Configuracion_Inicial
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbInstancias = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ProbarConexion = new System.Windows.Forms.Button();
            this.btnGuardarYContinuar = new System.Windows.Forms.Button();
            this.txtBaseDatos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radWindowsAuth = new System.Windows.Forms.RadioButton();
            this.radSqlAuth = new System.Windows.Forms.RadioButton();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 13F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(24, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(843, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configuración conexión a la base de datos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(24, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(755, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Elegí la instancia de SQL Server donde queres instalar / conectarte a la base.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(24, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Instancia:";
            // 
            // cmbInstancias
            // 
            this.cmbInstancias.FormattingEnabled = true;
            this.cmbInstancias.Location = new System.Drawing.Point(24, 175);
            this.cmbInstancias.Margin = new System.Windows.Forms.Padding(6);
            this.cmbInstancias.Name = "cmbInstancias";
            this.cmbInstancias.Size = new System.Drawing.Size(804, 33);
            this.cmbInstancias.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(844, 171);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "Detectar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProbarConexion
            // 
            this.ProbarConexion.BackColor = System.Drawing.Color.SteelBlue;
            this.ProbarConexion.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProbarConexion.Location = new System.Drawing.Point(24, 561);
            this.ProbarConexion.Margin = new System.Windows.Forms.Padding(6);
            this.ProbarConexion.Name = "ProbarConexion";
            this.ProbarConexion.Size = new System.Drawing.Size(241, 44);
            this.ProbarConexion.TabIndex = 5;
            this.ProbarConexion.Text = "Probar conexion";
            this.ProbarConexion.UseVisualStyleBackColor = false;
            this.ProbarConexion.Click += new System.EventHandler(this.ProbarConexion_Click);
            // 
            // btnGuardarYContinuar
            // 
            this.btnGuardarYContinuar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGuardarYContinuar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnGuardarYContinuar.Location = new System.Drawing.Point(277, 561);
            this.btnGuardarYContinuar.Margin = new System.Windows.Forms.Padding(6);
            this.btnGuardarYContinuar.Name = "btnGuardarYContinuar";
            this.btnGuardarYContinuar.Size = new System.Drawing.Size(231, 44);
            this.btnGuardarYContinuar.TabIndex = 6;
            this.btnGuardarYContinuar.Text = "Guardar y continuar";
            this.btnGuardarYContinuar.UseVisualStyleBackColor = false;
            this.btnGuardarYContinuar.Click += new System.EventHandler(this.btnGuardarYContinuar_Click);
            // 
            // txtBaseDatos
            // 
            this.txtBaseDatos.Location = new System.Drawing.Point(24, 277);
            this.txtBaseDatos.Name = "txtBaseDatos";
            this.txtBaseDatos.Size = new System.Drawing.Size(804, 31);
            this.txtBaseDatos.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(26, 240);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Base de datos:";
            // 
            // radWindowsAuth
            // 
            this.radWindowsAuth.AutoSize = true;
            this.radWindowsAuth.Location = new System.Drawing.Point(29, 334);
            this.radWindowsAuth.Name = "radWindowsAuth";
            this.radWindowsAuth.Size = new System.Drawing.Size(180, 29);
            this.radWindowsAuth.TabIndex = 9;
            this.radWindowsAuth.TabStop = true;
            this.radWindowsAuth.Text = "Windows Auth";
            this.radWindowsAuth.UseVisualStyleBackColor = true;
            // 
            // radSqlAuth
            // 
            this.radSqlAuth.AutoSize = true;
            this.radSqlAuth.Location = new System.Drawing.Point(229, 334);
            this.radSqlAuth.Name = "radSqlAuth";
            this.radSqlAuth.Size = new System.Drawing.Size(135, 29);
            this.radSqlAuth.TabIndex = 10;
            this.radSqlAuth.TabStop = true;
            this.radSqlAuth.Text = "SQL Auth";
            this.radSqlAuth.UseVisualStyleBackColor = true;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblEstado.ForeColor = System.Drawing.SystemColors.Control;
            this.lblEstado.Location = new System.Drawing.Point(839, 280);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(70, 25);
            this.lblEstado.TabIndex = 11;
            this.lblEstado.Text = "label5";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(24, 385);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(302, 31);
            this.txtUsuario.TabIndex = 12;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(24, 455);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(302, 31);
            this.txtPassword.TabIndex = 13;
            // 
            // Configuracion_Inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(1099, 620);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.radSqlAuth);
            this.Controls.Add(this.radWindowsAuth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBaseDatos);
            this.Controls.Add(this.btnGuardarYContinuar);
            this.Controls.Add(this.ProbarConexion);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbInstancias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Configuracion_Inicial";
            this.Text = "Configuracion_Inicial";
            this.Load += new System.EventHandler(this.Configuracion_Inicial_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbInstancias;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ProbarConexion;
        private System.Windows.Forms.Button btnGuardarYContinuar;
        private System.Windows.Forms.TextBox txtBaseDatos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radWindowsAuth;
        private System.Windows.Forms.RadioButton radSqlAuth;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
    }
}