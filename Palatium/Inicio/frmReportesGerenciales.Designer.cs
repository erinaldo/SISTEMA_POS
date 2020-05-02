namespace Palatium.Inicio
{
    partial class frmReportesGerenciales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportesGerenciales));
            this.lblNombreEquipo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblVersionDemo = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.btnEstadosCuentasRepartidoresExternos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNombreEquipo
            // 
            this.lblNombreEquipo.AutoSize = true;
            this.lblNombreEquipo.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEquipo.Location = new System.Drawing.Point(197, 591);
            this.lblNombreEquipo.Name = "lblNombreEquipo";
            this.lblNombreEquipo.Size = new System.Drawing.Size(209, 22);
            this.lblNombreEquipo.TabIndex = 107;
            this.lblNombreEquipo.Text = "NOMBRE DEL EQUIPO";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Maiandra GD", 14.25F);
            this.label7.Location = new System.Drawing.Point(42, 591);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 22);
            this.label7.TabIndex = 106;
            this.label7.Text = "Nombre Equipo:";
            // 
            // lblVersionDemo
            // 
            this.lblVersionDemo.AutoSize = true;
            this.lblVersionDemo.Font = new System.Drawing.Font("Maiandra GD", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionDemo.ForeColor = System.Drawing.Color.Red;
            this.lblVersionDemo.Location = new System.Drawing.Point(39, 30);
            this.lblVersionDemo.Name = "lblVersionDemo";
            this.lblVersionDemo.Size = new System.Drawing.Size(195, 32);
            this.lblVersionDemo.TabIndex = 105;
            this.lblVersionDemo.Text = "Versión Demo";
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
            this.logo.TabIndex = 104;
            this.logo.TabStop = false;
            // 
            // btnEstadosCuentasRepartidoresExternos
            // 
            this.btnEstadosCuentasRepartidoresExternos.BackColor = System.Drawing.Color.Navy;
            this.btnEstadosCuentasRepartidoresExternos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEstadosCuentasRepartidoresExternos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstadosCuentasRepartidoresExternos.FlatAppearance.BorderSize = 2;
            this.btnEstadosCuentasRepartidoresExternos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEstadosCuentasRepartidoresExternos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEstadosCuentasRepartidoresExternos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadosCuentasRepartidoresExternos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstadosCuentasRepartidoresExternos.ForeColor = System.Drawing.Color.White;
            this.btnEstadosCuentasRepartidoresExternos.Image = ((System.Drawing.Image)(resources.GetObject("btnEstadosCuentasRepartidoresExternos.Image")));
            this.btnEstadosCuentasRepartidoresExternos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEstadosCuentasRepartidoresExternos.Location = new System.Drawing.Point(532, 76);
            this.btnEstadosCuentasRepartidoresExternos.Name = "btnEstadosCuentasRepartidoresExternos";
            this.btnEstadosCuentasRepartidoresExternos.Size = new System.Drawing.Size(140, 115);
            this.btnEstadosCuentasRepartidoresExternos.TabIndex = 108;
            this.btnEstadosCuentasRepartidoresExternos.Text = "Estados de Cuenta\r\nRep. Externos";
            this.btnEstadosCuentasRepartidoresExternos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEstadosCuentasRepartidoresExternos.UseVisualStyleBackColor = false;
            this.btnEstadosCuentasRepartidoresExternos.Click += new System.EventHandler(this.btnEstadosCuentasRepartidoresExternos_Click);
            // 
            // frmReportesGerenciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1360, 694);
            this.Controls.Add(this.btnEstadosCuentasRepartidoresExternos);
            this.Controls.Add(this.lblNombreEquipo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblVersionDemo);
            this.Controls.Add(this.logo);
            this.Name = "frmReportesGerenciales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes Gerenciales";
            this.Load += new System.EventHandler(this.frmReportesGerenciales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNombreEquipo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblVersionDemo;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button btnEstadosCuentasRepartidoresExternos;
    }
}