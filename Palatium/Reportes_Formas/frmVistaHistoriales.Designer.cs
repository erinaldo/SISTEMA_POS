namespace Palatium.Reportes_Formas
{
    partial class frmVistaHistoriales
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
            this.btnHistorialProducto = new System.Windows.Forms.Button();
            this.btnHistorialComanda = new System.Windows.Forms.Button();
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlContenedor.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHistorialProducto
            // 
            this.btnHistorialProducto.AccessibleDescription = "0";
            this.btnHistorialProducto.BackColor = System.Drawing.Color.Blue;
            this.btnHistorialProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHistorialProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHistorialProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialProducto.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialProducto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnHistorialProducto.Location = new System.Drawing.Point(180, 0);
            this.btnHistorialProducto.Name = "btnHistorialProducto";
            this.btnHistorialProducto.Size = new System.Drawing.Size(205, 55);
            this.btnHistorialProducto.TabIndex = 6;
            this.btnHistorialProducto.Text = "Historial por producto seleccionado";
            this.btnHistorialProducto.UseVisualStyleBackColor = false;
            this.btnHistorialProducto.Click += new System.EventHandler(this.btnHistorialProducto_Click);
            // 
            // btnHistorialComanda
            // 
            this.btnHistorialComanda.AccessibleDescription = "0";
            this.btnHistorialComanda.BackColor = System.Drawing.Color.Blue;
            this.btnHistorialComanda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHistorialComanda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnHistorialComanda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialComanda.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialComanda.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnHistorialComanda.Location = new System.Drawing.Point(0, 0);
            this.btnHistorialComanda.Name = "btnHistorialComanda";
            this.btnHistorialComanda.Size = new System.Drawing.Size(180, 55);
            this.btnHistorialComanda.TabIndex = 5;
            this.btnHistorialComanda.Text = "Historial por tipo de comanda";
            this.btnHistorialComanda.UseVisualStyleBackColor = false;
            this.btnHistorialComanda.Click += new System.EventHandler(this.btnHistorialComanda_Click);
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlContenedor.Controls.Add(this.panel2);
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(0, 0);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(734, 586);
            this.pnlContenedor.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Navy;
            this.panel2.Controls.Add(this.btnHistorialProducto);
            this.panel2.Controls.Add(this.btnHistorialComanda);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(734, 55);
            this.panel2.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.AccessibleDescription = "0";
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(860, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 55);
            this.button1.TabIndex = 9;
            this.button1.Text = "Reportes Gerenciales";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // frmVistaHistoriales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(734, 586);
            this.Controls.Add(this.pnlContenedor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVistaHistoriales";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Historial del cliente";
            this.Load += new System.EventHandler(this.frmVistaHistoriales_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVistaHistoriales_KeyDown);
            this.pnlContenedor.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHistorialProducto;
        private System.Windows.Forms.Button btnHistorialComanda;
        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
    }
}