namespace Palatium
{
    partial class frmVerificadorOrigen
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
            this.lblOrden = new System.Windows.Forms.Label();
            this.btnDomicilio = new System.Windows.Forms.Button();
            this.btnLlevar = new System.Windows.Forms.Button();
            this.btnMesa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOrden
            // 
            this.lblOrden.AutoSize = true;
            this.lblOrden.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrden.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblOrden.Location = new System.Drawing.Point(59, 20);
            this.lblOrden.Name = "lblOrden";
            this.lblOrden.Size = new System.Drawing.Size(290, 29);
            this.lblOrden.TabIndex = 0;
            this.lblOrden.Text = "Seleccione El Tipo De Orden";
            // 
            // btnDomicilio
            // 
            this.btnDomicilio.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDomicilio.Location = new System.Drawing.Point(134, 194);
            this.btnDomicilio.Name = "btnDomicilio";
            this.btnDomicilio.Size = new System.Drawing.Size(125, 35);
            this.btnDomicilio.TabIndex = 6;
            this.btnDomicilio.Text = "Domicilio";
            this.btnDomicilio.UseVisualStyleBackColor = true;
            this.btnDomicilio.Visible = false;
            this.btnDomicilio.Click += new System.EventHandler(this.btnDomicilio_Click);
            // 
            // btnLlevar
            // 
            this.btnLlevar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnLlevar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLlevar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLlevar.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLlevar.Location = new System.Drawing.Point(230, 91);
            this.btnLlevar.Name = "btnLlevar";
            this.btnLlevar.Size = new System.Drawing.Size(130, 71);
            this.btnLlevar.TabIndex = 143;
            this.btnLlevar.Text = "PARA LLEVAR";
            this.btnLlevar.UseVisualStyleBackColor = false;
            this.btnLlevar.Click += new System.EventHandler(this.btnLlevar_Click);
            // 
            // btnMesa
            // 
            this.btnMesa.AccessibleDescription = "";
            this.btnMesa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnMesa.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnMesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMesa.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesa.Location = new System.Drawing.Point(32, 91);
            this.btnMesa.Name = "btnMesa";
            this.btnMesa.Size = new System.Drawing.Size(130, 71);
            this.btnMesa.TabIndex = 142;
            this.btnMesa.Text = "MESAS";
            this.btnMesa.UseVisualStyleBackColor = false;
            this.btnMesa.Click += new System.EventHandler(this.btnMesa_Click);
            // 
            // frmVerificadorOrigen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(394, 238);
            this.Controls.Add(this.btnLlevar);
            this.Controls.Add(this.btnMesa);
            this.Controls.Add(this.btnDomicilio);
            this.Controls.Add(this.lblOrden);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVerificadorOrigen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tipo de Orden";
            this.Load += new System.EventHandler(this.frmVerificadorOrigen_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVerificadorOrigen_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOrden;
        private System.Windows.Forms.Button btnDomicilio;
        private System.Windows.Forms.Button btnLlevar;
        private System.Windows.Forms.Button btnMesa;
    }
}