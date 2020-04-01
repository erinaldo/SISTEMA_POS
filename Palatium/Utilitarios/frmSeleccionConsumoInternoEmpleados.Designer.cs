namespace Palatium.Utilitarios
{
    partial class frmSeleccionConsumoInternoEmpleados
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
            this.btnConsumoEmpleados = new System.Windows.Forms.Button();
            this.btnConsumoInterno = new System.Windows.Forms.Button();
            this.lblOrden = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConsumoEmpleados
            // 
            this.btnConsumoEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnConsumoEmpleados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnConsumoEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsumoEmpleados.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsumoEmpleados.Location = new System.Drawing.Point(230, 91);
            this.btnConsumoEmpleados.Name = "btnConsumoEmpleados";
            this.btnConsumoEmpleados.Size = new System.Drawing.Size(130, 71);
            this.btnConsumoEmpleados.TabIndex = 145;
            this.btnConsumoEmpleados.Text = "CONSUMO EMPLEADOS";
            this.btnConsumoEmpleados.UseVisualStyleBackColor = false;
            this.btnConsumoEmpleados.Click += new System.EventHandler(this.btnConsumoEmpleados_Click);
            // 
            // btnConsumoInterno
            // 
            this.btnConsumoInterno.AccessibleDescription = "";
            this.btnConsumoInterno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnConsumoInterno.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnConsumoInterno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsumoInterno.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsumoInterno.Location = new System.Drawing.Point(32, 91);
            this.btnConsumoInterno.Name = "btnConsumoInterno";
            this.btnConsumoInterno.Size = new System.Drawing.Size(130, 71);
            this.btnConsumoInterno.TabIndex = 144;
            this.btnConsumoInterno.Text = "CONSUMO INTERNO";
            this.btnConsumoInterno.UseVisualStyleBackColor = false;
            this.btnConsumoInterno.Click += new System.EventHandler(this.btnConsumoInterno_Click);
            // 
            // lblOrden
            // 
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblOrden.Location = new System.Drawing.Point(59, 20);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(290, 29);
            this.lblOrden.TabIndex = 146;
            this.lblOrden.Text = "Seleccione El Tipo De Orden";
            // 
            // frmSeleccionConsumoInternoEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(394, 202);
            this.Controls.Add(this.lblOrden);
            this.Controls.Add(this.btnConsumoEmpleados);
            this.Controls.Add(this.btnConsumoInterno);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeleccionConsumoInternoEmpleados";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Opciones de Consumo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConsumoEmpleados;
        private System.Windows.Forms.Button btnConsumoInterno;
        private System.Windows.Forms.Label lblOrden;
    }
}