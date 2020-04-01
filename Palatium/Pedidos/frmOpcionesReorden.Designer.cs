namespace Palatium.Pedidos
{
    partial class frmOpcionesReorden
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnReordenar = new System.Windows.Forms.Button();
            this.btnVistaPrevia = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(522, 83);
            this.label1.TabIndex = 5;
            this.label1.Text = "SECCIÓN\r\nOPCIONES DE REORDENAMIENTO";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnReordenar
            // 
            this.btnReordenar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReordenar.Location = new System.Drawing.Point(280, 143);
            this.btnReordenar.Name = "btnReordenar";
            this.btnReordenar.Size = new System.Drawing.Size(167, 88);
            this.btnReordenar.TabIndex = 4;
            this.btnReordenar.Text = "Ordenar Preparación de Ítems";
            this.btnReordenar.UseVisualStyleBackColor = true;
            this.btnReordenar.Click += new System.EventHandler(this.btnReordenar_Click);
            // 
            // btnVistaPrevia
            // 
            this.btnVistaPrevia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVistaPrevia.Location = new System.Drawing.Point(80, 143);
            this.btnVistaPrevia.Name = "btnVistaPrevia";
            this.btnVistaPrevia.Size = new System.Drawing.Size(167, 88);
            this.btnVistaPrevia.TabIndex = 3;
            this.btnVistaPrevia.Text = "Vista Previa\r\nde Ítems";
            this.btnVistaPrevia.UseVisualStyleBackColor = true;
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            // 
            // frmOpcionesReorden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(528, 275);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnReordenar);
            this.Controls.Add(this.btnVistaPrevia);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpcionesReorden";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de Reordenamiento de Ítems";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReordenar;
        private System.Windows.Forms.Button btnVistaPrevia;
    }
}