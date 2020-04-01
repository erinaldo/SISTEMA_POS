namespace Palatium.Inicio
{
    partial class frmInicio
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
            this.btnConfiguracion = new System.Windows.Forms.Button();
            this.btnPuntoVenta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.BackColor = System.Drawing.Color.Transparent;
            this.btnConfiguracion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfiguracion.FlatAppearance.BorderSize = 0;
            this.btnConfiguracion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnConfiguracion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnConfiguracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracion.Font = new System.Drawing.Font("Agency FB", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnConfiguracion.Image = global::Palatium.Properties.Resources.inicio_configuracion;
            this.btnConfiguracion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConfiguracion.Location = new System.Drawing.Point(237, 146);
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(195, 173);
            this.btnConfiguracion.TabIndex = 2;
            this.btnConfiguracion.Text = "Configuración";
            this.btnConfiguracion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfiguracion.UseVisualStyleBackColor = false;
            this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // btnPuntoVenta
            // 
            this.btnPuntoVenta.BackColor = System.Drawing.Color.Transparent;
            this.btnPuntoVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPuntoVenta.FlatAppearance.BorderSize = 0;
            this.btnPuntoVenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPuntoVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPuntoVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPuntoVenta.Font = new System.Drawing.Font("Agency FB", 21.75F);
            this.btnPuntoVenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnPuntoVenta.Image = global::Palatium.Properties.Resources.inicio_punto_venta_2;
            this.btnPuntoVenta.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPuntoVenta.Location = new System.Drawing.Point(36, 146);
            this.btnPuntoVenta.Name = "btnPuntoVenta";
            this.btnPuntoVenta.Size = new System.Drawing.Size(195, 173);
            this.btnPuntoVenta.TabIndex = 3;
            this.btnPuntoVenta.Text = "Punto de Venta";
            this.btnPuntoVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPuntoVenta.UseVisualStyleBackColor = false;
            this.btnPuntoVenta.Click += new System.EventHandler(this.btnPuntoVenta_Click);
            // 
            // frmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Palatium.Properties.Resources.fondo_inicio;
            this.ClientSize = new System.Drawing.Size(464, 331);
            this.Controls.Add(this.btnPuntoVenta);
            this.Controls.Add(this.btnConfiguracion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PALATIUM REST - INICIO";
            this.Load += new System.EventHandler(this.frmInicio_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Button btnPuntoVenta;



    }
}