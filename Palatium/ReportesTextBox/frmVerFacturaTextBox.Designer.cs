namespace Palatium.Pedidos
{
    partial class frmVerFacturaTextBox
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuImprimir = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRecibir = new System.Windows.Forms.Label();
            this.txtReporte = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuImprimir});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(351, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuImprimir
            // 
            this.menuImprimir.Name = "menuImprimir";
            this.menuImprimir.Size = new System.Drawing.Size(65, 20);
            this.menuImprimir.Text = "Imprimir";
            this.menuImprimir.Click += new System.EventHandler(this.menuImprimir_Click);
            // 
            // lblRecibir
            // 
            this.lblRecibir.AutoSize = true;
            this.lblRecibir.Location = new System.Drawing.Point(158, 289);
            this.lblRecibir.Name = "lblRecibir";
            this.lblRecibir.Size = new System.Drawing.Size(35, 13);
            this.lblRecibir.TabIndex = 18;
            this.lblRecibir.Text = "label1";
            // 
            // txtReporte
            // 
            this.txtReporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReporte.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.Location = new System.Drawing.Point(0, 27);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ReadOnly = true;
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReporte.Size = new System.Drawing.Size(348, 564);
            this.txtReporte.TabIndex = 19;
            // 
            // frmVerFacturaTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(351, 591);
            this.Controls.Add(this.txtReporte);
            this.Controls.Add(this.lblRecibir);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVerFacturaTextBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vista Previa de la Factura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVerFacturaTextBox_FormClosing);
            this.Load += new System.EventHandler(this.frmVerFacturaTextBox_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVerFacturaTextBox_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuImprimir;
        private System.Windows.Forms.Label lblRecibir;
        private System.Windows.Forms.TextBox txtReporte;
    }
}