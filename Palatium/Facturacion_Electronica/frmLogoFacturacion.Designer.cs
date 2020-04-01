namespace Palatium.Facturacion_Electronica
{
    partial class frmLogoFacturacion
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
            this.components = new System.ComponentModel.Container();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.btnModal = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // imgLogo
            // 
            this.imgLogo.Image = global::Palatium.Properties.Resources.tu_logo;
            this.imgLogo.Location = new System.Drawing.Point(12, 48);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(250, 250);
            this.imgLogo.TabIndex = 0;
            this.imgLogo.TabStop = false;
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(12, 300);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(29, 15);
            this.labelX8.TabIndex = 36;
            this.labelX8.Text = "Ruta:";
            // 
            // btnModal
            // 
            this.btnModal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnModal.Location = new System.Drawing.Point(238, 315);
            this.btnModal.Name = "btnModal";
            this.btnModal.Size = new System.Drawing.Size(25, 20);
            this.btnModal.TabIndex = 37;
            this.btnModal.Text = "...";
            this.ttMensaje.SetToolTip(this.btnModal, "Clic aquí para abrir el cuadro de dálogo y seleccionar la imagen");
            this.btnModal.UseVisualStyleBackColor = false;
            this.btnModal.Click += new System.EventHandler(this.btnModal_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(191, 342);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(72, 39);
            this.btnLimpiar.TabIndex = 39;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(113, 342);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(72, 39);
            this.btnNuevo.TabIndex = 38;
            this.btnNuevo.Text = "Guardar";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRuta.Location = new System.Drawing.Point(12, 316);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(217, 20);
            this.txtRuta.TabIndex = 40;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(229, 27);
            this.labelX1.TabIndex = 41;
            this.labelX1.Text = "El tamaño de la imagen debe ser de 250 x 250\r\npixeles o inferior";
            // 
            // frmLogoFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(275, 390);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnModal);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.imgLogo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogoFacturacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logo de Facturación";
            this.Load += new System.EventHandler(this.frmLogoFacturacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgLogo;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.Button btnModal;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.ToolTip ttMensaje;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}