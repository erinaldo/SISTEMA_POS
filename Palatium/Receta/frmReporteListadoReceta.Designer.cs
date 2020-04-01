namespace Palatium.Receta
{
    partial class frmReporteListadoReceta
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
            this.CRReporte = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CRReporte
            // 
            this.CRReporte.ActiveViewIndex = -1;
            this.CRReporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRReporte.CachedPageNumberPerDoc = 10;
            this.CRReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CRReporte.Location = new System.Drawing.Point(0, 0);
            this.CRReporte.Name = "CRReporte";
            this.CRReporte.Size = new System.Drawing.Size(284, 261);
            this.CRReporte.TabIndex = 0;
            this.CRReporte.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frmReporteListadoReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.CRReporte);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteListadoReceta";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de la Receta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer CRReporte;
    }
}