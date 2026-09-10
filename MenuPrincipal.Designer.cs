namespace Servicios
{
    partial class MenuPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.usuarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarClaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.españolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administradorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitacoraEventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionFamiliaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionRolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionRespaldoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnCambiarClave = new System.Windows.Forms.Button();
            this.btnTorneos = new System.Windows.Forms.Button();
            this.btnRegistrarNadador = new System.Windows.Forms.Button();
            this.btnGestionRespaldo = new System.Windows.Forms.Button();
            this.btnAuditoria = new System.Windows.Forms.Button();
            this.btnGestionFamilias = new System.Windows.Forms.Button();
            this.btnGestionRoles = new System.Windows.Forms.Button();
            this.btnGestionUsuarios = new System.Windows.Forms.Button();
            this.lblSeccionAdmin = new System.Windows.Forms.Label();
            this.btnClases = new System.Windows.Forms.Button();
            this.lblSeccionClases = new System.Windows.Forms.Label();
            this.lblSeccionTorneos = new System.Windows.Forms.Label();
            this.lblSeccionNadadores = new System.Windows.Forms.Label();
            this.lblUsuarioSidebar = new System.Windows.Forms.Label();
            this.lblLogoApp = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTituloMain = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usuarioToolStripMenuItem,
            this.administradorToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(722, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // usuarioToolStripMenuItem
            // 
            this.usuarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarClaveToolStripMenuItem,
            this.cerrarSesionToolStripMenuItem,
            this.iniciarSesionToolStripMenuItem,
            this.idiomaToolStripMenuItem});
            this.usuarioToolStripMenuItem.Name = "usuarioToolStripMenuItem";
            this.usuarioToolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.usuarioToolStripMenuItem.Text = "Usuario";
            this.usuarioToolStripMenuItem.Click += new System.EventHandler(this.usuarioToolStripMenuItem_Click);
            // 
            // cambiarClaveToolStripMenuItem
            // 
            this.cambiarClaveToolStripMenuItem.Name = "cambiarClaveToolStripMenuItem";
            this.cambiarClaveToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.cambiarClaveToolStripMenuItem.Text = "Cambiar Clave";
            this.cambiarClaveToolStripMenuItem.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar Sesion";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // iniciarSesionToolStripMenuItem
            // 
            this.iniciarSesionToolStripMenuItem.Name = "iniciarSesionToolStripMenuItem";
            this.iniciarSesionToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.iniciarSesionToolStripMenuItem.Text = "Iniciar Sesion";
            this.iniciarSesionToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesionToolStripMenuItem_Click);
            // 
            // idiomaToolStripMenuItem
            // 
            this.idiomaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.españolToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.idiomaToolStripMenuItem.Name = "idiomaToolStripMenuItem";
            this.idiomaToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.idiomaToolStripMenuItem.Text = "Idioma";
            // 
            // españolToolStripMenuItem
            // 
            this.españolToolStripMenuItem.Name = "españolToolStripMenuItem";
            this.españolToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.españolToolStripMenuItem.Text = "Español";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.englishToolStripMenuItem.Text = "English";
            // 
            // administradorToolStripMenuItem
            // 
            this.administradorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionUsuariosToolStripMenuItem,
            this.bitacoraEventosToolStripMenuItem,
            this.gestionFamiliaToolStripMenuItem,
            this.gestionRolToolStripMenuItem,
            this.gestionRespaldoToolStripMenuItem});
            this.administradorToolStripMenuItem.Name = "administradorToolStripMenuItem";
            this.administradorToolStripMenuItem.Size = new System.Drawing.Size(110, 23);
            this.administradorToolStripMenuItem.Text = "Administrador";
            this.administradorToolStripMenuItem.Click += new System.EventHandler(this.administradorToolStripMenuItem_Click);
            // 
            // gestionUsuariosToolStripMenuItem
            // 
            this.gestionUsuariosToolStripMenuItem.Name = "gestionUsuariosToolStripMenuItem";
            this.gestionUsuariosToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.gestionUsuariosToolStripMenuItem.Text = "Gestion Usuarios";
            this.gestionUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gestionUsuariosToolStripMenuItem_Click);
            // 
            // bitacoraEventosToolStripMenuItem
            // 
            this.bitacoraEventosToolStripMenuItem.Name = "bitacoraEventosToolStripMenuItem";
            this.bitacoraEventosToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.bitacoraEventosToolStripMenuItem.Text = "Bitacora Eventos";
            this.bitacoraEventosToolStripMenuItem.Click += new System.EventHandler(this.bitacoraEventosToolStripMenuItem_Click);
            // 
            // gestionFamiliaToolStripMenuItem
            // 
            this.gestionFamiliaToolStripMenuItem.Name = "gestionFamiliaToolStripMenuItem";
            this.gestionFamiliaToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.gestionFamiliaToolStripMenuItem.Text = "Gestion Familia";
            this.gestionFamiliaToolStripMenuItem.Click += new System.EventHandler(this.gestionFamiliaToolStripMenuItem_Click);
            // 
            // gestionRolToolStripMenuItem
            // 
            this.gestionRolToolStripMenuItem.Name = "gestionRolToolStripMenuItem";
            this.gestionRolToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.gestionRolToolStripMenuItem.Text = "Gestion Rol";
            this.gestionRolToolStripMenuItem.Click += new System.EventHandler(this.gestionRolToolStripMenuItem_Click);
            // 
            // gestionRespaldoToolStripMenuItem
            // 
            this.gestionRespaldoToolStripMenuItem.Name = "gestionRespaldoToolStripMenuItem";
            this.gestionRespaldoToolStripMenuItem.Size = new System.Drawing.Size(187, 24);
            this.gestionRespaldoToolStripMenuItem.Text = "Gestion Respaldo";
            this.gestionRespaldoToolStripMenuItem.Click += new System.EventHandler(this.gestionRespaldoToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(60, 23);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.pnlSidebar.Controls.Add(this.btnCerrarSesion);
            this.pnlSidebar.Controls.Add(this.btnCambiarClave);
            this.pnlSidebar.Controls.Add(this.btnTorneos);
            this.pnlSidebar.Controls.Add(this.btnRegistrarNadador);
            this.pnlSidebar.Controls.Add(this.btnGestionRespaldo);
            this.pnlSidebar.Controls.Add(this.btnAuditoria);
            this.pnlSidebar.Controls.Add(this.btnGestionFamilias);
            this.pnlSidebar.Controls.Add(this.btnGestionRoles);
            this.pnlSidebar.Controls.Add(this.btnGestionUsuarios);
            this.pnlSidebar.Controls.Add(this.lblSeccionAdmin);
            this.pnlSidebar.Controls.Add(this.btnClases);
            this.pnlSidebar.Controls.Add(this.lblSeccionClases);
            this.pnlSidebar.Controls.Add(this.lblSeccionTorneos);
            this.pnlSidebar.Controls.Add(this.lblSeccionNadadores);
            this.pnlSidebar.Controls.Add(this.lblUsuarioSidebar);
            this.pnlSidebar.Controls.Add(this.lblLogoApp);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 25);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(219, 470);
            this.pnlSidebar.TabIndex = 2;
            this.pnlSidebar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSidebar_Paint);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.Transparent;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 0;
            this.btnCerrarSesion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Location = new System.Drawing.Point(21, 416);
            this.btnCerrarSesion.Margin = new System.Windows.Forms.Padding(2);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(130, 28);
            this.btnCerrarSesion.TabIndex = 21;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // btnCambiarClave
            // 
            this.btnCambiarClave.BackColor = System.Drawing.Color.Transparent;
            this.btnCambiarClave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambiarClave.FlatAppearance.BorderSize = 0;
            this.btnCambiarClave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnCambiarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambiarClave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiarClave.ForeColor = System.Drawing.Color.White;
            this.btnCambiarClave.Location = new System.Drawing.Point(19, 391);
            this.btnCambiarClave.Margin = new System.Windows.Forms.Padding(2);
            this.btnCambiarClave.Name = "btnCambiarClave";
            this.btnCambiarClave.Size = new System.Drawing.Size(130, 21);
            this.btnCambiarClave.TabIndex = 20;
            this.btnCambiarClave.Text = "Cambiar Clave";
            this.btnCambiarClave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCambiarClave.UseVisualStyleBackColor = false;
            this.btnCambiarClave.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            // 
            // btnTorneos
            // 
            this.btnTorneos.BackColor = System.Drawing.Color.Transparent;
            this.btnTorneos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTorneos.Enabled = false;
            this.btnTorneos.FlatAppearance.BorderSize = 0;
            this.btnTorneos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnTorneos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTorneos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTorneos.ForeColor = System.Drawing.Color.White;
            this.btnTorneos.Location = new System.Drawing.Point(21, 157);
            this.btnTorneos.Margin = new System.Windows.Forms.Padding(2);
            this.btnTorneos.Name = "btnTorneos";
            this.btnTorneos.Size = new System.Drawing.Size(56, 22);
            this.btnTorneos.TabIndex = 9;
            this.btnTorneos.Text = "Torneos";
            this.btnTorneos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTorneos.UseVisualStyleBackColor = false;
            // 
            // btnRegistrarNadador
            // 
            this.btnRegistrarNadador.BackColor = System.Drawing.Color.Transparent;
            this.btnRegistrarNadador.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrarNadador.FlatAppearance.BorderSize = 0;
            this.btnRegistrarNadador.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(190)))));
            this.btnRegistrarNadador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarNadador.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRegistrarNadador.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarNadador.Location = new System.Drawing.Point(21, 108);
            this.btnRegistrarNadador.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegistrarNadador.Name = "btnRegistrarNadador";
            this.btnRegistrarNadador.Size = new System.Drawing.Size(110, 28);
            this.btnRegistrarNadador.TabIndex = 7;
            this.btnRegistrarNadador.Text = "Registrar Nadador";
            this.btnRegistrarNadador.UseVisualStyleBackColor = false;
            this.btnRegistrarNadador.Click += new System.EventHandler(this.RegistrarNadador_Click);
            // 
            // btnGestionRespaldo
            // 
            this.btnGestionRespaldo.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionRespaldo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionRespaldo.FlatAppearance.BorderSize = 0;
            this.btnGestionRespaldo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnGestionRespaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionRespaldo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionRespaldo.ForeColor = System.Drawing.Color.White;
            this.btnGestionRespaldo.Location = new System.Drawing.Point(19, 364);
            this.btnGestionRespaldo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGestionRespaldo.Name = "btnGestionRespaldo";
            this.btnGestionRespaldo.Size = new System.Drawing.Size(130, 23);
            this.btnGestionRespaldo.TabIndex = 19;
            this.btnGestionRespaldo.Text = "Gestion Respaldo";
            this.btnGestionRespaldo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionRespaldo.UseVisualStyleBackColor = false;
            this.btnGestionRespaldo.Click += new System.EventHandler(this.gestionRespaldoToolStripMenuItem_Click);
            // 
            // btnAuditoria
            // 
            this.btnAuditoria.BackColor = System.Drawing.Color.Transparent;
            this.btnAuditoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAuditoria.FlatAppearance.BorderSize = 0;
            this.btnAuditoria.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnAuditoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAuditoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAuditoria.ForeColor = System.Drawing.Color.White;
            this.btnAuditoria.Location = new System.Drawing.Point(19, 337);
            this.btnAuditoria.Margin = new System.Windows.Forms.Padding(2);
            this.btnAuditoria.Name = "btnAuditoria";
            this.btnAuditoria.Size = new System.Drawing.Size(130, 23);
            this.btnAuditoria.TabIndex = 18;
            this.btnAuditoria.Text = "Bitácora Eventos";
            this.btnAuditoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAuditoria.UseVisualStyleBackColor = false;
            this.btnAuditoria.Click += new System.EventHandler(this.bitacoraEventosToolStripMenuItem_Click);
            // 
            // btnGestionFamilias
            // 
            this.btnGestionFamilias.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionFamilias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionFamilias.FlatAppearance.BorderSize = 0;
            this.btnGestionFamilias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnGestionFamilias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionFamilias.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionFamilias.ForeColor = System.Drawing.Color.White;
            this.btnGestionFamilias.Location = new System.Drawing.Point(19, 313);
            this.btnGestionFamilias.Margin = new System.Windows.Forms.Padding(2);
            this.btnGestionFamilias.Name = "btnGestionFamilias";
            this.btnGestionFamilias.Size = new System.Drawing.Size(130, 20);
            this.btnGestionFamilias.TabIndex = 17;
            this.btnGestionFamilias.Text = "Gestion Familia";
            this.btnGestionFamilias.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionFamilias.UseVisualStyleBackColor = false;
            this.btnGestionFamilias.Click += new System.EventHandler(this.gestionFamiliaToolStripMenuItem_Click);
            // 
            // btnGestionRoles
            // 
            this.btnGestionRoles.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionRoles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionRoles.FlatAppearance.BorderSize = 0;
            this.btnGestionRoles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnGestionRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionRoles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionRoles.ForeColor = System.Drawing.Color.White;
            this.btnGestionRoles.Location = new System.Drawing.Point(19, 287);
            this.btnGestionRoles.Margin = new System.Windows.Forms.Padding(2);
            this.btnGestionRoles.Name = "btnGestionRoles";
            this.btnGestionRoles.Size = new System.Drawing.Size(130, 22);
            this.btnGestionRoles.TabIndex = 16;
            this.btnGestionRoles.Text = "Gestion Rol";
            this.btnGestionRoles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionRoles.UseVisualStyleBackColor = false;
            this.btnGestionRoles.Click += new System.EventHandler(this.gestionRolToolStripMenuItem_Click);
            // 
            // btnGestionUsuarios
            // 
            this.btnGestionUsuarios.BackColor = System.Drawing.Color.Transparent;
            this.btnGestionUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionUsuarios.FlatAppearance.BorderSize = 0;
            this.btnGestionUsuarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnGestionUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionUsuarios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGestionUsuarios.ForeColor = System.Drawing.Color.White;
            this.btnGestionUsuarios.Location = new System.Drawing.Point(19, 259);
            this.btnGestionUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.btnGestionUsuarios.Name = "btnGestionUsuarios";
            this.btnGestionUsuarios.Size = new System.Drawing.Size(130, 24);
            this.btnGestionUsuarios.TabIndex = 15;
            this.btnGestionUsuarios.Text = "Gestion Usuarios";
            this.btnGestionUsuarios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGestionUsuarios.UseVisualStyleBackColor = false;
            this.btnGestionUsuarios.Click += new System.EventHandler(this.gestionUsuariosToolStripMenuItem_Click);
            // 
            // lblSeccionAdmin
            // 
            this.lblSeccionAdmin.AutoSize = true;
            this.lblSeccionAdmin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(192)))), ((int)(((byte)(222)))));
            this.lblSeccionAdmin.Location = new System.Drawing.Point(22, 240);
            this.lblSeccionAdmin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeccionAdmin.Name = "lblSeccionAdmin";
            this.lblSeccionAdmin.Size = new System.Drawing.Size(102, 17);
            this.lblSeccionAdmin.TabIndex = 14;
            this.lblSeccionAdmin.Text = "Administración";
            // 
            // btnClases
            // 
            this.btnClases.BackColor = System.Drawing.Color.Transparent;
            this.btnClases.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClases.Enabled = false;
            this.btnClases.FlatAppearance.BorderSize = 0;
            this.btnClases.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(58)))), ((int)(((byte)(107)))));
            this.btnClases.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClases.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClases.ForeColor = System.Drawing.Color.White;
            this.btnClases.Location = new System.Drawing.Point(19, 206);
            this.btnClases.Margin = new System.Windows.Forms.Padding(2);
            this.btnClases.Name = "btnClases";
            this.btnClases.Size = new System.Drawing.Size(124, 27);
            this.btnClases.TabIndex = 12;
            this.btnClases.Text = "Clases de Natación";
            this.btnClases.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClases.UseVisualStyleBackColor = false;
            // 
            // lblSeccionClases
            // 
            this.lblSeccionClases.AutoSize = true;
            this.lblSeccionClases.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionClases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(192)))), ((int)(((byte)(222)))));
            this.lblSeccionClases.Location = new System.Drawing.Point(22, 187);
            this.lblSeccionClases.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeccionClases.Name = "lblSeccionClases";
            this.lblSeccionClases.Size = new System.Drawing.Size(124, 17);
            this.lblSeccionClases.TabIndex = 11;
            this.lblSeccionClases.Text = "Clases de Natación";
            // 
            // lblSeccionTorneos
            // 
            this.lblSeccionTorneos.AutoSize = true;
            this.lblSeccionTorneos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionTorneos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(192)))), ((int)(((byte)(222)))));
            this.lblSeccionTorneos.Location = new System.Drawing.Point(22, 138);
            this.lblSeccionTorneos.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeccionTorneos.Name = "lblSeccionTorneos";
            this.lblSeccionTorneos.Size = new System.Drawing.Size(57, 17);
            this.lblSeccionTorneos.TabIndex = 8;
            this.lblSeccionTorneos.Text = "Torneos";
            // 
            // lblSeccionNadadores
            // 
            this.lblSeccionNadadores.AutoSize = true;
            this.lblSeccionNadadores.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSeccionNadadores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(192)))), ((int)(((byte)(222)))));
            this.lblSeccionNadadores.Location = new System.Drawing.Point(26, 89);
            this.lblSeccionNadadores.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSeccionNadadores.Name = "lblSeccionNadadores";
            this.lblSeccionNadadores.Size = new System.Drawing.Size(132, 17);
            this.lblSeccionNadadores.TabIndex = 6;
            this.lblSeccionNadadores.Text = "Nadadores / Padrón";
            // 
            // lblUsuarioSidebar
            // 
            this.lblUsuarioSidebar.AutoSize = true;
            this.lblUsuarioSidebar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioSidebar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(202)))), ((int)(((byte)(228)))));
            this.lblUsuarioSidebar.Location = new System.Drawing.Point(55, 47);
            this.lblUsuarioSidebar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUsuarioSidebar.Name = "lblUsuarioSidebar";
            this.lblUsuarioSidebar.Size = new System.Drawing.Size(108, 15);
            this.lblUsuarioSidebar.TabIndex = 5;
            this.lblUsuarioSidebar.Text = "Usuario Conectado";
            // 
            // lblLogoApp
            // 
            this.lblLogoApp.AutoSize = true;
            this.lblLogoApp.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogoApp.ForeColor = System.Drawing.Color.White;
            this.lblLogoApp.Location = new System.Drawing.Point(22, 10);
            this.lblLogoApp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogoApp.Name = "lblLogoApp";
            this.lblLogoApp.Size = new System.Drawing.Size(175, 37);
            this.lblLogoApp.TabIndex = 4;
            this.lblLogoApp.Text = "AquaGestión";
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.lblTituloMain);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(219, 25);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(503, 470);
            this.pnlMain.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 19.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.label1.Location = new System.Drawing.Point(45, 172);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bienvenido!";
            // 
            // lblTituloMain
            // 
            this.lblTituloMain.AutoSize = true;
            this.lblTituloMain.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(31)))), ((int)(((byte)(58)))));
            this.lblTituloMain.Location = new System.Drawing.Point(45, 104);
            this.lblTituloMain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloMain.Name = "lblTituloMain";
            this.lblTituloMain.Size = new System.Drawing.Size(132, 32);
            this.lblTituloMain.TabIndex = 0;
            this.lblTituloMain.Text = "Dashboard";
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(722, 495);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.MintCream;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem usuarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarClaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administradorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitacoraEventosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionFamiliaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionRolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem españolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionRespaldoToolStripMenuItem;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblLogoApp;
        private System.Windows.Forms.Label lblUsuarioSidebar;
        private System.Windows.Forms.Label lblSeccionNadadores;
        private System.Windows.Forms.Label lblSeccionTorneos;
        private System.Windows.Forms.Label lblSeccionClases;
        private System.Windows.Forms.Label lblSeccionAdmin;
        private System.Windows.Forms.Button btnRegistrarNadador;
        private System.Windows.Forms.Button btnTorneos;
        private System.Windows.Forms.Button btnClases;
        private System.Windows.Forms.Button btnGestionUsuarios;
        private System.Windows.Forms.Button btnGestionRoles;
        private System.Windows.Forms.Button btnGestionFamilias;
        private System.Windows.Forms.Button btnAuditoria;
        private System.Windows.Forms.Button btnGestionRespaldo;
        private System.Windows.Forms.Button btnCambiarClave;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label lblTituloMain;
        private System.Windows.Forms.Label label1;
    }
}