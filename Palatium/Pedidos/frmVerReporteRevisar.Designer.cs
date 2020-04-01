namespace Palatium.Pedidos
{
    partial class frmVerReporteRevisar
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
            this.rbdVerFactura = new System.Windows.Forms.RadioButton();
            this.rbdVerPrecuenta = new System.Windows.Forms.RadioButton();
            this.txtReporte = new System.Windows.Forms.TextBox();
            this.btnReabrir = new DevComponents.DotNetBar.ButtonX();
            this.btnEditar = new DevComponents.DotNetBar.ButtonX();
            this.btnImprimir = new DevComponents.DotNetBar.ButtonX();
            this.btnListo = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // rbdVerFactura
            // 
            this.rbdVerFactura.AutoSize = true;
            this.rbdVerFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbdVerFactura.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdVerFactura.Location = new System.Drawing.Point(44, 120);
            this.rbdVerFactura.Name = "rbdVerFactura";
            this.rbdVerFactura.Size = new System.Drawing.Size(153, 29);
            this.rbdVerFactura.TabIndex = 12;
            this.rbdVerFactura.Text = "Ver Factura";
            this.rbdVerFactura.UseVisualStyleBackColor = true;
            this.rbdVerFactura.CheckedChanged += new System.EventHandler(this.rbdVerFactura_CheckedChanged);
            // 
            // rbdVerPrecuenta
            // 
            this.rbdVerPrecuenta.AutoSize = true;
            this.rbdVerPrecuenta.Checked = true;
            this.rbdVerPrecuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbdVerPrecuenta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdVerPrecuenta.Location = new System.Drawing.Point(44, 59);
            this.rbdVerPrecuenta.Name = "rbdVerPrecuenta";
            this.rbdVerPrecuenta.Size = new System.Drawing.Size(191, 29);
            this.rbdVerPrecuenta.TabIndex = 13;
            this.rbdVerPrecuenta.TabStop = true;
            this.rbdVerPrecuenta.Text = "Ver Pre Cuenta";
            this.rbdVerPrecuenta.UseVisualStyleBackColor = true;
            this.rbdVerPrecuenta.CheckedChanged += new System.EventHandler(this.rbdVerPrecuenta_CheckedChanged);
            // 
            // txtReporte
            // 
            this.txtReporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReporte.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.Location = new System.Drawing.Point(295, 49);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ReadOnly = true;
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReporte.Size = new System.Drawing.Size(348, 564);
            this.txtReporte.TabIndex = 14;
            // 
            // btnReabrir
            // 
            this.btnReabrir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReabrir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReabrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReabrir.Image = global::Palatium.Properties.Resources.editar_png1;
            this.btnReabrir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnReabrir.Location = new System.Drawing.Point(44, 181);
            this.btnReabrir.Name = "btnReabrir";
            this.btnReabrir.Size = new System.Drawing.Size(157, 122);
            this.btnReabrir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReabrir.TabIndex = 15;
            this.btnReabrir.Text = "Reabrir la Orden";
            this.btnReabrir.Visible = false;
            this.btnReabrir.Click += new System.EventHandler(this.btnReabrir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEditar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.Image = global::Palatium.Properties.Resources.editar_png1;
            this.btnEditar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnEditar.Location = new System.Drawing.Point(44, 181);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(157, 122);
            this.btnEditar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEditar.TabIndex = 16;
            this.btnEditar.Text = "Editar la Orden";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Image = global::Palatium.Properties.Resources.imprimir_precuenta;
            this.btnImprimir.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnImprimir.Location = new System.Drawing.Point(44, 330);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(157, 122);
            this.btnImprimir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImprimir.TabIndex = 17;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnListo
            // 
            this.btnListo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnListo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnListo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListo.Image = global::Palatium.Properties.Resources.ok_png;
            this.btnListo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnListo.Location = new System.Drawing.Point(44, 491);
            this.btnListo.Name = "btnListo";
            this.btnListo.Size = new System.Drawing.Size(157, 122);
            this.btnListo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnListo.TabIndex = 18;
            this.btnListo.Text = "Listo";
            this.btnListo.Click += new System.EventHandler(this.btnListo_Click);
            // 
            // frmVerReporteRevisar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.btnListo);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnReabrir);
            this.Controls.Add(this.txtReporte);
            this.Controls.Add(this.rbdVerPrecuenta);
            this.Controls.Add(this.rbdVerFactura);
            this.Controls.Add(this.btnEditar);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVerReporteRevisar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de la Orden";
            this.Load += new System.EventHandler(this.frmVerReporteRevisar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVerReporteRevisar_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbdVerFactura;
        private System.Windows.Forms.RadioButton rbdVerPrecuenta;
        private System.Windows.Forms.TextBox txtReporte;
        private DevComponents.DotNetBar.ButtonX btnReabrir;
        private DevComponents.DotNetBar.ButtonX btnEditar;
        private DevComponents.DotNetBar.ButtonX btnImprimir;
        private DevComponents.DotNetBar.ButtonX btnListo;
    }
}