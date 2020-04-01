namespace Palatium.Inicio
{
    partial class frmInicioPrograma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInicioPrograma));
            this.btnAcerca = new System.Windows.Forms.LinkLabel();
            this.lblContacto = new System.Windows.Forms.Label();
            this.lblSitioWeb = new System.Windows.Forms.LinkLabel();
            this.btnOficina = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.lblEstadoCaja = new System.Windows.Forms.Label();
            this.lblCajeroApertura = new System.Windows.Forms.Label();
            this.lblJornada = new System.Windows.Forms.Label();
            this.lblHoraApertura = new System.Windows.Forms.Label();
            this.lblFechaApertura = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblVersionDemo = new System.Windows.Forms.Label();
            this.grupoDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAcerca
            // 
            this.btnAcerca.AutoSize = true;
            this.btnAcerca.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcerca.Location = new System.Drawing.Point(421, 583);
            this.btnAcerca.Name = "btnAcerca";
            this.btnAcerca.Size = new System.Drawing.Size(81, 20);
            this.btnAcerca.TabIndex = 54;
            this.btnAcerca.TabStop = true;
            this.btnAcerca.Text = "Acerca de";
            this.btnAcerca.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAcerca_LinkClicked);
            // 
            // lblContacto
            // 
            this.lblContacto.AutoSize = true;
            this.lblContacto.BackColor = System.Drawing.Color.White;
            this.lblContacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblContacto.Location = new System.Drawing.Point(41, 579);
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.Size = new System.Drawing.Size(214, 24);
            this.lblContacto.TabIndex = 53;
            this.lblContacto.Text = "Contacto: 0995610690";
            // 
            // lblSitioWeb
            // 
            this.lblSitioWeb.AutoSize = true;
            this.lblSitioWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSitioWeb.Location = new System.Drawing.Point(288, 583);
            this.lblSitioWeb.Name = "lblSitioWeb";
            this.lblSitioWeb.Size = new System.Drawing.Size(124, 20);
            this.lblSitioWeb.TabIndex = 52;
            this.lblSitioWeb.TabStop = true;
            this.lblSitioWeb.Text = "www.aplicsis.net";
            this.lblSitioWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSitioWeb_LinkClicked);
            // 
            // btnOficina
            // 
            this.btnOficina.BackColor = System.Drawing.Color.Navy;
            this.btnOficina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOficina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOficina.FlatAppearance.BorderSize = 2;
            this.btnOficina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOficina.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOficina.ForeColor = System.Drawing.Color.White;
            this.btnOficina.Image = global::Palatium.Properties.Resources.icono_oficina;
            this.btnOficina.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOficina.Location = new System.Drawing.Point(771, 351);
            this.btnOficina.Name = "btnOficina";
            this.btnOficina.Size = new System.Drawing.Size(192, 120);
            this.btnOficina.TabIndex = 72;
            this.btnOficina.Text = "Oficina\r\nAdministración";
            this.btnOficina.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOficina.UseVisualStyleBackColor = false;
            this.btnOficina.Click += new System.EventHandler(this.btnOficina_Click);
            this.btnOficina.MouseEnter += new System.EventHandler(this.btnOficina_MouseEnter);
            this.btnOficina.MouseLeave += new System.EventHandler(this.btnOficina_MouseLeave);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.lblEstadoCaja);
            this.grupoDatos.Controls.Add(this.lblCajeroApertura);
            this.grupoDatos.Controls.Add(this.lblJornada);
            this.grupoDatos.Controls.Add(this.lblHoraApertura);
            this.grupoDatos.Controls.Add(this.lblFechaApertura);
            this.grupoDatos.Controls.Add(this.label5);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoDatos.Location = new System.Drawing.Point(517, 487);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(690, 116);
            this.grupoDatos.TabIndex = 76;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "DATOS DE LA CAJA VIGENTE";
            this.grupoDatos.Visible = false;
            // 
            // lblEstadoCaja
            // 
            this.lblEstadoCaja.AutoSize = true;
            this.lblEstadoCaja.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoCaja.Location = new System.Drawing.Point(514, 62);
            this.lblEstadoCaja.Name = "lblEstadoCaja";
            this.lblEstadoCaja.Size = new System.Drawing.Size(157, 22);
            this.lblEstadoCaja.TabIndex = 9;
            this.lblEstadoCaja.Text = "Estado de la Caja:";
            // 
            // lblCajeroApertura
            // 
            this.lblCajeroApertura.AutoSize = true;
            this.lblCajeroApertura.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCajeroApertura.Location = new System.Drawing.Point(514, 39);
            this.lblCajeroApertura.Name = "lblCajeroApertura";
            this.lblCajeroApertura.Size = new System.Drawing.Size(147, 22);
            this.lblCajeroApertura.TabIndex = 8;
            this.lblCajeroApertura.Text = "Cajero Apertura:";
            // 
            // lblJornada
            // 
            this.lblJornada.AutoSize = true;
            this.lblJornada.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJornada.Location = new System.Drawing.Point(170, 85);
            this.lblJornada.Name = "lblJornada";
            this.lblJornada.Size = new System.Drawing.Size(81, 22);
            this.lblJornada.TabIndex = 7;
            this.lblJornada.Text = "Jornada:";
            // 
            // lblHoraApertura
            // 
            this.lblHoraApertura.AutoSize = true;
            this.lblHoraApertura.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoraApertura.Location = new System.Drawing.Point(170, 62);
            this.lblHoraApertura.Name = "lblHoraApertura";
            this.lblHoraApertura.Size = new System.Drawing.Size(136, 22);
            this.lblHoraApertura.TabIndex = 6;
            this.lblHoraApertura.Text = "Hora Apertura:";
            // 
            // lblFechaApertura
            // 
            this.lblFechaApertura.AutoSize = true;
            this.lblFechaApertura.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaApertura.Location = new System.Drawing.Point(170, 39);
            this.lblFechaApertura.Name = "lblFechaApertura";
            this.lblFechaApertura.Size = new System.Drawing.Size(140, 22);
            this.lblFechaApertura.TabIndex = 5;
            this.lblFechaApertura.Text = "Fecha Apertura:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(349, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 22);
            this.label5.TabIndex = 4;
            this.label5.Text = "Estado de la Caja:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(349, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cajero Apertura:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Jornada:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hora Apertura:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Apertura:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Palatium.Properties.Resources.fondo_nombre_sistema;
            this.pictureBox2.Location = new System.Drawing.Point(571, 57);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(569, 276);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 77;
            this.pictureBox2.TabStop = false;
            // 
            // logo
            // 
            this.logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo.BackgroundImage")));
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logo.Location = new System.Drawing.Point(45, 76);
            this.logo.Margin = new System.Windows.Forms.Padding(2);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(457, 501);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 78;
            this.logo.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Navy;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::Palatium.Properties.Resources.icono_oficina;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(1098, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 120);
            this.button1.TabIndex = 79;
            this.button1.Text = "Asistente Configuración";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblVersionDemo
            // 
            this.lblVersionDemo.AutoSize = true;
            this.lblVersionDemo.Font = new System.Drawing.Font("Maiandra GD", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionDemo.ForeColor = System.Drawing.Color.Red;
            this.lblVersionDemo.Location = new System.Drawing.Point(39, 30);
            this.lblVersionDemo.Name = "lblVersionDemo";
            this.lblVersionDemo.Size = new System.Drawing.Size(195, 32);
            this.lblVersionDemo.TabIndex = 80;
            this.lblVersionDemo.Text = "Versión Demo";
            // 
            // frmInicioPrograma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1360, 694);
            this.Controls.Add(this.lblVersionDemo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.btnOficina);
            this.Controls.Add(this.btnAcerca);
            this.Controls.Add(this.lblContacto);
            this.Controls.Add(this.lblSitioWeb);
            this.Name = "frmInicioPrograma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "INICIO";
            this.Load += new System.EventHandler(this.frmInicioPrograma_Load);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel btnAcerca;
        private System.Windows.Forms.Label lblContacto;
        private System.Windows.Forms.LinkLabel lblSitioWeb;
        private System.Windows.Forms.Button btnOficina;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblEstadoCaja;
        private System.Windows.Forms.Label lblCajeroApertura;
        private System.Windows.Forms.Label lblJornada;
        private System.Windows.Forms.Label lblHoraApertura;
        private System.Windows.Forms.Label lblFechaApertura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblVersionDemo;
    }
}