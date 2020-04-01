namespace Palatium.ReportesTextbox
{
    partial class frmVentasMesero
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
            this.lblRecibir = new System.Windows.Forms.Label();
            this.menuImprimir = new System.Windows.Forms.ToolStripMenuItem();
            this.txtReporte = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRecibir
            // 
            this.lblRecibir.AutoSize = true;
            this.lblRecibir.Location = new System.Drawing.Point(483, 245);
            this.lblRecibir.Name = "lblRecibir";
            this.lblRecibir.Size = new System.Drawing.Size(35, 13);
            this.lblRecibir.TabIndex = 23;
            this.lblRecibir.Text = "label1";
            // 
            // menuImprimir
            // 
            this.menuImprimir.Name = "menuImprimir";
            this.menuImprimir.Size = new System.Drawing.Size(65, 20);
            this.menuImprimir.Text = "Imprimir";
            this.menuImprimir.Click += new System.EventHandler(this.menuImprimir_Click);
            // 
            // txtReporte
            // 
            this.txtReporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReporte.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.Location = new System.Drawing.Point(3, 22);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ReadOnly = true;
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReporte.Size = new System.Drawing.Size(348, 564);
            this.txtReporte.TabIndex = 21;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuImprimir});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(351, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // frmVentasMesero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(351, 591);
            this.Controls.Add(this.lblRecibir);
            this.Controls.Add(this.txtReporte);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVentasMesero";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventas por Mesero";
            this.Load += new System.EventHandler(this.frmVentasMesero_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVentasMesero_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecibir;
        private System.Windows.Forms.ToolStripMenuItem menuImprimir;
        private System.Windows.Forms.TextBox txtReporte;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}