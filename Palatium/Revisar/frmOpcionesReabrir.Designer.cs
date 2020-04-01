namespace Palatium
{
    partial class frmOpcionesReabrir
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
            this.btnCambiar = new DevComponents.DotNetBar.ButtonX();
            this.btnReversar = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // btnCambiar
            // 
            this.btnCambiar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCambiar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCambiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambiar.Image = global::Palatium.Properties.Resources.dolar_png;
            this.btnCambiar.Location = new System.Drawing.Point(79, 206);
            this.btnCambiar.Name = "btnCambiar";
            this.btnCambiar.Size = new System.Drawing.Size(240, 114);
            this.btnCambiar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCambiar.TabIndex = 0;
            this.btnCambiar.Text = "Cambiar \r\nFormas \r\nde Pago";
            this.btnCambiar.Click += new System.EventHandler(this.btnCambiar_Click);
            // 
            // btnReversar
            // 
            this.btnReversar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReversar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReversar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReversar.Image = global::Palatium.Properties.Resources.facturar_png1;
            this.btnReversar.Location = new System.Drawing.Point(354, 206);
            this.btnReversar.Name = "btnReversar";
            this.btnReversar.Size = new System.Drawing.Size(240, 114);
            this.btnReversar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReversar.TabIndex = 1;
            this.btnReversar.Text = "Eliminar Cobros\r\ncon Anulación \r\nde Factura";
            this.btnReversar.Click += new System.EventHandler(this.btnReversar_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX1.Location = new System.Drawing.Point(23, 49);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(626, 84);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "ORDEN ASOCIADA A UNA FACTURA\r\nSeleccione la acción a realizar.";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // frmOpcionesReabrir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(672, 386);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.btnReversar);
            this.Controls.Add(this.btnCambiar);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpcionesReabrir";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de Reapertura";
            this.Load += new System.EventHandler(this.frmOpcionesReabrir_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOpcionesReabrir_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCambiar;
        private DevComponents.DotNetBar.ButtonX btnReversar;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}