namespace Palatium.Pedidos
{
    partial class frmOpcionesConsumoAlimentos
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
            this.btnProducto = new System.Windows.Forms.Button();
            this.btnOrdenCompleta = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnProducto
            // 
            this.btnProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProducto.Location = new System.Drawing.Point(66, 140);
            this.btnProducto.Name = "btnProducto";
            this.btnProducto.Size = new System.Drawing.Size(167, 88);
            this.btnProducto.TabIndex = 0;
            this.btnProducto.Text = "Aplicar por producto";
            this.btnProducto.UseVisualStyleBackColor = true;
            this.btnProducto.Click += new System.EventHandler(this.btnProducto_Click);
            // 
            // btnOrdenCompleta
            // 
            this.btnOrdenCompleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdenCompleta.Location = new System.Drawing.Point(266, 140);
            this.btnOrdenCompleta.Name = "btnOrdenCompleta";
            this.btnOrdenCompleta.Size = new System.Drawing.Size(167, 88);
            this.btnOrdenCompleta.TabIndex = 1;
            this.btnOrdenCompleta.Text = "Aplicar a la Orden Completa";
            this.btnOrdenCompleta.UseVisualStyleBackColor = true;
            this.btnOrdenCompleta.Click += new System.EventHandler(this.btnOrdenCompleta_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 83);
            this.label1.TabIndex = 2;
            this.label1.Text = "SECCIÓN\r\nCONSUMO DE ALIMENTOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmOpcionesConsumoAlimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(495, 275);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOrdenCompleta);
            this.Controls.Add(this.btnProducto);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpcionesConsumoAlimentos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de Consumo de Alimentos";
            this.Load += new System.EventHandler(this.frmOpcionesConsumoAlimentos_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProducto;
        private System.Windows.Forms.Button btnOrdenCompleta;
        private System.Windows.Forms.Label label1;
    }
}