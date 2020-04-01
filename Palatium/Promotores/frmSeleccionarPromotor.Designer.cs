namespace Palatium.Promotores
{
    partial class frmSeleccionarPromotor
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
            this.lblPromotor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlPromotores = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblPromotor
            // 
            this.lblPromotor.AutoSize = true;
            this.lblPromotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPromotor.ForeColor = System.Drawing.Color.Blue;
            this.lblPromotor.Location = new System.Drawing.Point(149, 8);
            this.lblPromotor.Name = "lblPromotor";
            this.lblPromotor.Size = new System.Drawing.Size(129, 24);
            this.lblPromotor.TabIndex = 37;
            this.lblPromotor.Text = "PROMOTOR";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 24);
            this.label2.TabIndex = 36;
            this.label2.Text = "PROMOTOR:";
            // 
            // pnlPromotores
            // 
            this.pnlPromotores.BackColor = System.Drawing.Color.Transparent;
            this.pnlPromotores.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPromotores.Location = new System.Drawing.Point(12, 35);
            this.pnlPromotores.Name = "pnlPromotores";
            this.pnlPromotores.Size = new System.Drawing.Size(510, 310);
            this.pnlPromotores.TabIndex = 35;
            // 
            // frmSeleccionarPromotor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(527, 352);
            this.Controls.Add(this.lblPromotor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlPromotores);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeleccionarPromotor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccionar Promotor";
            this.Load += new System.EventHandler(this.frmSeleccionarPromotor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPromotor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlPromotores;
    }
}