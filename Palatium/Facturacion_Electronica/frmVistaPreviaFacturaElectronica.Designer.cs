namespace Palatium.Facturacion_Electronica
{
    partial class frmVistaPreviaFacturaElectronica
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
            this.rptVisor = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rptVisor
            // 
            this.rptVisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptVisor.Location = new System.Drawing.Point(0, 0);
            this.rptVisor.Name = "rptVisor";
            this.rptVisor.Size = new System.Drawing.Size(724, 607);
            this.rptVisor.TabIndex = 0;
            this.rptVisor.ZoomPercent = 150;
            // 
            // frmVistaPreviaFacturaElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 607);
            this.Controls.Add(this.rptVisor);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVistaPreviaFacturaElectronica";
            this.Text = "Vista Previa de Factura Electrónica";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVistaPreviaFacturaElectronica_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptVisor;

    }
}