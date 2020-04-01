namespace Palatium.Correo
{
    partial class frmCorreo
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
            this.grbCorreoRemitente = new System.Windows.Forms.GroupBox();
            this.txtContraseñaRemitente = new System.Windows.Forms.TextBox();
            this.lblContraseña = new System.Windows.Forms.Label();
            this.txtCorreoRemitente = new System.Windows.Forms.TextBox();
            this.lblDireccionCorreoRemitente = new System.Windows.Forms.Label();
            this.txtNombreRemitente = new System.Windows.Forms.TextBox();
            this.lblNombreRemitente = new System.Windows.Forms.Label();
            this.chkEnableSSL = new System.Windows.Forms.CheckBox();
            this.txtPuerto = new System.Windows.Forms.TextBox();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.grbCorreoDestinatario = new System.Windows.Forms.GroupBox();
            this.txtBcc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCuerpo = new System.Windows.Forms.TextBox();
            this.lblCuerpo = new System.Windows.Forms.Label();
            this.btnAdjuntar = new System.Windows.Forms.Button();
            this.txtAdjuntos = new System.Windows.Forms.TextBox();
            this.lblAdjunto = new System.Windows.Forms.Label();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.lblCC = new System.Windows.Forms.Label();
            this.txtAsunto = new System.Windows.Forms.TextBox();
            this.lblAsunto = new System.Windows.Forms.Label();
            this.txtCorreoDestinatario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNombreDestinatario = new System.Windows.Forms.TextBox();
            this.lblNombreDestinatario = new System.Windows.Forms.Label();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.grbCorreoRemitente.SuspendLayout();
            this.grbCorreoDestinatario.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbCorreoRemitente
            // 
            this.grbCorreoRemitente.Controls.Add(this.txtContraseñaRemitente);
            this.grbCorreoRemitente.Controls.Add(this.lblContraseña);
            this.grbCorreoRemitente.Controls.Add(this.txtCorreoRemitente);
            this.grbCorreoRemitente.Controls.Add(this.lblDireccionCorreoRemitente);
            this.grbCorreoRemitente.Controls.Add(this.txtNombreRemitente);
            this.grbCorreoRemitente.Controls.Add(this.lblNombreRemitente);
            this.grbCorreoRemitente.Controls.Add(this.chkEnableSSL);
            this.grbCorreoRemitente.Controls.Add(this.txtPuerto);
            this.grbCorreoRemitente.Controls.Add(this.lblPuerto);
            this.grbCorreoRemitente.Controls.Add(this.txtHost);
            this.grbCorreoRemitente.Controls.Add(this.lblHost);
            this.grbCorreoRemitente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbCorreoRemitente.ForeColor = System.Drawing.Color.Teal;
            this.grbCorreoRemitente.Location = new System.Drawing.Point(12, 12);
            this.grbCorreoRemitente.Name = "grbCorreoRemitente";
            this.grbCorreoRemitente.Size = new System.Drawing.Size(722, 216);
            this.grbCorreoRemitente.TabIndex = 0;
            this.grbCorreoRemitente.TabStop = false;
            this.grbCorreoRemitente.Text = "Correo Remitente";
            // 
            // txtContraseñaRemitente
            // 
            this.txtContraseñaRemitente.Location = new System.Drawing.Point(173, 175);
            this.txtContraseñaRemitente.Name = "txtContraseñaRemitente";
            this.txtContraseñaRemitente.PasswordChar = '*';
            this.txtContraseñaRemitente.Size = new System.Drawing.Size(487, 24);
            this.txtContraseñaRemitente.TabIndex = 11;
            // 
            // lblContraseña
            // 
            this.lblContraseña.AutoSize = true;
            this.lblContraseña.ForeColor = System.Drawing.Color.Black;
            this.lblContraseña.Location = new System.Drawing.Point(25, 178);
            this.lblContraseña.Name = "lblContraseña";
            this.lblContraseña.Size = new System.Drawing.Size(136, 18);
            this.lblContraseña.TabIndex = 10;
            this.lblContraseña.Text = "Contraseña Correo";
            // 
            // txtCorreoRemitente
            // 
            this.txtCorreoRemitente.Location = new System.Drawing.Point(173, 145);
            this.txtCorreoRemitente.Name = "txtCorreoRemitente";
            this.txtCorreoRemitente.Size = new System.Drawing.Size(487, 24);
            this.txtCorreoRemitente.TabIndex = 9;
            // 
            // lblDireccionCorreoRemitente
            // 
            this.lblDireccionCorreoRemitente.AutoSize = true;
            this.lblDireccionCorreoRemitente.ForeColor = System.Drawing.Color.Black;
            this.lblDireccionCorreoRemitente.Location = new System.Drawing.Point(25, 148);
            this.lblDireccionCorreoRemitente.Name = "lblDireccionCorreoRemitente";
            this.lblDireccionCorreoRemitente.Size = new System.Drawing.Size(142, 18);
            this.lblDireccionCorreoRemitente.TabIndex = 8;
            this.lblDireccionCorreoRemitente.Text = "Dirección de Correo";
            // 
            // txtNombreRemitente
            // 
            this.txtNombreRemitente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreRemitente.Location = new System.Drawing.Point(173, 111);
            this.txtNombreRemitente.Name = "txtNombreRemitente";
            this.txtNombreRemitente.Size = new System.Drawing.Size(487, 24);
            this.txtNombreRemitente.TabIndex = 7;
            // 
            // lblNombreRemitente
            // 
            this.lblNombreRemitente.AutoSize = true;
            this.lblNombreRemitente.ForeColor = System.Drawing.Color.Black;
            this.lblNombreRemitente.Location = new System.Drawing.Point(25, 114);
            this.lblNombreRemitente.Name = "lblNombreRemitente";
            this.lblNombreRemitente.Size = new System.Drawing.Size(133, 18);
            this.lblNombreRemitente.TabIndex = 6;
            this.lblNombreRemitente.Text = "Nombre Remitente";
            // 
            // chkEnableSSL
            // 
            this.chkEnableSSL.AutoSize = true;
            this.chkEnableSSL.ForeColor = System.Drawing.Color.Black;
            this.chkEnableSSL.Location = new System.Drawing.Point(552, 83);
            this.chkEnableSSL.Name = "chkEnableSSL";
            this.chkEnableSSL.Size = new System.Drawing.Size(108, 22);
            this.chkEnableSSL.TabIndex = 5;
            this.chkEnableSSL.Text = "Enable SSL.";
            this.chkEnableSSL.UseVisualStyleBackColor = true;
            // 
            // txtPuerto
            // 
            this.txtPuerto.Location = new System.Drawing.Point(173, 53);
            this.txtPuerto.Name = "txtPuerto";
            this.txtPuerto.Size = new System.Drawing.Size(487, 24);
            this.txtPuerto.TabIndex = 3;
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.ForeColor = System.Drawing.Color.Black;
            this.lblPuerto.Location = new System.Drawing.Point(25, 56);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(52, 18);
            this.lblPuerto.TabIndex = 2;
            this.lblPuerto.Text = "Puerto";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(173, 23);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(487, 24);
            this.txtHost.TabIndex = 1;
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.ForeColor = System.Drawing.Color.Black;
            this.lblHost.Location = new System.Drawing.Point(25, 26);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(40, 18);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "Host";
            // 
            // grbCorreoDestinatario
            // 
            this.grbCorreoDestinatario.Controls.Add(this.txtBcc);
            this.grbCorreoDestinatario.Controls.Add(this.label1);
            this.grbCorreoDestinatario.Controls.Add(this.txtCuerpo);
            this.grbCorreoDestinatario.Controls.Add(this.lblCuerpo);
            this.grbCorreoDestinatario.Controls.Add(this.btnAdjuntar);
            this.grbCorreoDestinatario.Controls.Add(this.txtAdjuntos);
            this.grbCorreoDestinatario.Controls.Add(this.lblAdjunto);
            this.grbCorreoDestinatario.Controls.Add(this.txtCC);
            this.grbCorreoDestinatario.Controls.Add(this.lblCC);
            this.grbCorreoDestinatario.Controls.Add(this.txtAsunto);
            this.grbCorreoDestinatario.Controls.Add(this.lblAsunto);
            this.grbCorreoDestinatario.Controls.Add(this.txtCorreoDestinatario);
            this.grbCorreoDestinatario.Controls.Add(this.label2);
            this.grbCorreoDestinatario.Controls.Add(this.txtNombreDestinatario);
            this.grbCorreoDestinatario.Controls.Add(this.lblNombreDestinatario);
            this.grbCorreoDestinatario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbCorreoDestinatario.ForeColor = System.Drawing.Color.Teal;
            this.grbCorreoDestinatario.Location = new System.Drawing.Point(12, 234);
            this.grbCorreoDestinatario.Name = "grbCorreoDestinatario";
            this.grbCorreoDestinatario.Size = new System.Drawing.Size(722, 407);
            this.grbCorreoDestinatario.TabIndex = 1;
            this.grbCorreoDestinatario.TabStop = false;
            this.grbCorreoDestinatario.Text = "Correo Destinatario";
            // 
            // txtBcc
            // 
            this.txtBcc.Location = new System.Drawing.Point(173, 152);
            this.txtBcc.Name = "txtBcc";
            this.txtBcc.Size = new System.Drawing.Size(487, 24);
            this.txtBcc.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(25, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 25;
            this.label1.Text = "BCC.";
            // 
            // txtCuerpo
            // 
            this.txtCuerpo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCuerpo.Location = new System.Drawing.Point(173, 276);
            this.txtCuerpo.Multiline = true;
            this.txtCuerpo.Name = "txtCuerpo";
            this.txtCuerpo.Size = new System.Drawing.Size(487, 113);
            this.txtCuerpo.TabIndex = 24;
            // 
            // lblCuerpo
            // 
            this.lblCuerpo.AutoSize = true;
            this.lblCuerpo.ForeColor = System.Drawing.Color.Black;
            this.lblCuerpo.Location = new System.Drawing.Point(25, 279);
            this.lblCuerpo.Name = "lblCuerpo";
            this.lblCuerpo.Size = new System.Drawing.Size(57, 18);
            this.lblCuerpo.TabIndex = 23;
            this.lblCuerpo.Text = "Cuerpo";
            // 
            // btnAdjuntar
            // 
            this.btnAdjuntar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdjuntar.ForeColor = System.Drawing.Color.Black;
            this.btnAdjuntar.Location = new System.Drawing.Point(603, 227);
            this.btnAdjuntar.Name = "btnAdjuntar";
            this.btnAdjuntar.Size = new System.Drawing.Size(57, 26);
            this.btnAdjuntar.TabIndex = 22;
            this.btnAdjuntar.Text = "...";
            this.btnAdjuntar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAdjuntar.UseVisualStyleBackColor = true;
            this.btnAdjuntar.Click += new System.EventHandler(this.btnAdjuntar_Click);
            // 
            // txtAdjuntos
            // 
            this.txtAdjuntos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAdjuntos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjuntos.Location = new System.Drawing.Point(173, 229);
            this.txtAdjuntos.Multiline = true;
            this.txtAdjuntos.Name = "txtAdjuntos";
            this.txtAdjuntos.ReadOnly = true;
            this.txtAdjuntos.Size = new System.Drawing.Size(424, 24);
            this.txtAdjuntos.TabIndex = 21;
            // 
            // lblAdjunto
            // 
            this.lblAdjunto.AutoSize = true;
            this.lblAdjunto.ForeColor = System.Drawing.Color.Black;
            this.lblAdjunto.Location = new System.Drawing.Point(25, 229);
            this.lblAdjunto.Name = "lblAdjunto";
            this.lblAdjunto.Size = new System.Drawing.Size(127, 36);
            this.lblAdjunto.TabIndex = 20;
            this.lblAdjunto.Text = "Adjuntar Archivos\r\n(Separado por \"|\")";
            // 
            // txtCC
            // 
            this.txtCC.Location = new System.Drawing.Point(173, 116);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(487, 24);
            this.txtCC.TabIndex = 19;
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.ForeColor = System.Drawing.Color.Black;
            this.lblCC.Location = new System.Drawing.Point(25, 119);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(34, 18);
            this.lblCC.TabIndex = 18;
            this.lblCC.Text = "CC.";
            // 
            // txtAsunto
            // 
            this.txtAsunto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAsunto.Location = new System.Drawing.Point(173, 188);
            this.txtAsunto.Name = "txtAsunto";
            this.txtAsunto.Size = new System.Drawing.Size(487, 24);
            this.txtAsunto.TabIndex = 17;
            // 
            // lblAsunto
            // 
            this.lblAsunto.AutoSize = true;
            this.lblAsunto.ForeColor = System.Drawing.Color.Black;
            this.lblAsunto.Location = new System.Drawing.Point(25, 191);
            this.lblAsunto.Name = "lblAsunto";
            this.lblAsunto.Size = new System.Drawing.Size(54, 18);
            this.lblAsunto.TabIndex = 16;
            this.lblAsunto.Text = "Asunto";
            // 
            // txtCorreoDestinatario
            // 
            this.txtCorreoDestinatario.Location = new System.Drawing.Point(173, 75);
            this.txtCorreoDestinatario.Multiline = true;
            this.txtCorreoDestinatario.Name = "txtCorreoDestinatario";
            this.txtCorreoDestinatario.Size = new System.Drawing.Size(487, 24);
            this.txtCorreoDestinatario.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(25, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 36);
            this.label2.TabIndex = 14;
            this.label2.Text = "Dirreción de Correo\r\n(Separado por \',\')";
            // 
            // txtNombreDestinatario
            // 
            this.txtNombreDestinatario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreDestinatario.Location = new System.Drawing.Point(173, 26);
            this.txtNombreDestinatario.Name = "txtNombreDestinatario";
            this.txtNombreDestinatario.Size = new System.Drawing.Size(487, 24);
            this.txtNombreDestinatario.TabIndex = 13;
            // 
            // lblNombreDestinatario
            // 
            this.lblNombreDestinatario.AutoSize = true;
            this.lblNombreDestinatario.ForeColor = System.Drawing.Color.Black;
            this.lblNombreDestinatario.Location = new System.Drawing.Point(25, 29);
            this.lblNombreDestinatario.Name = "lblNombreDestinatario";
            this.lblNombreDestinatario.Size = new System.Drawing.Size(145, 18);
            this.lblNombreDestinatario.TabIndex = 12;
            this.lblNombreDestinatario.Text = "Nombre Destinatario";
            // 
            // btnEnviar
            // 
            this.btnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.ForeColor = System.Drawing.Color.Black;
            this.btnEnviar.Location = new System.Drawing.Point(239, 647);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(115, 26);
            this.btnEnviar.TabIndex = 25;
            this.btnEnviar.Text = "Enviar Correo";
            this.btnEnviar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Black;
            this.btnSalir.Location = new System.Drawing.Point(402, 647);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(115, 26);
            this.btnSalir.TabIndex = 26;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // frmCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(761, 677);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.grbCorreoDestinatario);
            this.Controls.Add(this.grbCorreoRemitente);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCorreo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar Corrreo";
            this.Load += new System.EventHandler(this.frmCorreo_Load);
            this.grbCorreoRemitente.ResumeLayout(false);
            this.grbCorreoRemitente.PerformLayout();
            this.grbCorreoDestinatario.ResumeLayout(false);
            this.grbCorreoDestinatario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbCorreoRemitente;
        private System.Windows.Forms.TextBox txtContraseñaRemitente;
        private System.Windows.Forms.Label lblContraseña;
        private System.Windows.Forms.TextBox txtCorreoRemitente;
        private System.Windows.Forms.Label lblDireccionCorreoRemitente;
        private System.Windows.Forms.TextBox txtNombreRemitente;
        private System.Windows.Forms.Label lblNombreRemitente;
        private System.Windows.Forms.CheckBox chkEnableSSL;
        private System.Windows.Forms.TextBox txtPuerto;
        private System.Windows.Forms.Label lblPuerto;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.GroupBox grbCorreoDestinatario;
        private System.Windows.Forms.TextBox txtCuerpo;
        private System.Windows.Forms.Label lblCuerpo;
        private System.Windows.Forms.Button btnAdjuntar;
        private System.Windows.Forms.TextBox txtAdjuntos;
        private System.Windows.Forms.Label lblAdjunto;
        private System.Windows.Forms.TextBox txtCC;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.TextBox txtAsunto;
        private System.Windows.Forms.Label lblAsunto;
        private System.Windows.Forms.TextBox txtCorreoDestinatario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNombreDestinatario;
        private System.Windows.Forms.Label lblNombreDestinatario;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.TextBox txtBcc;
        private System.Windows.Forms.Label label1;
    }
}