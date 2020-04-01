namespace Palatium.Facturacion_Electronica
{
    partial class frmReporteFacturaElectronica
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
            this.rptFactura = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // rptFactura
            // 
            this.rptFactura.ActiveViewIndex = -1;
            this.rptFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rptFactura.CachedPageNumberPerDoc = 10;
            this.rptFactura.Cursor = System.Windows.Forms.Cursors.Default;
            this.rptFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptFactura.Location = new System.Drawing.Point(0, 0);
            this.rptFactura.Name = "rptFactura";
            this.rptFactura.Size = new System.Drawing.Size(284, 261);
            this.rptFactura.TabIndex = 0;
            this.rptFactura.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReporteFacturaElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.rptFactura);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteFacturaElectronica";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factura Electrónica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rptFactura;
    }
}