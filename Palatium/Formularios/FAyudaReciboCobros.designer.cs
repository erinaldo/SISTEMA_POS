namespace Palatium.Formularios
{
    partial class FAyudaReciboCobros
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
            this.btnSalirRecibo = new System.Windows.Forms.Button();
            this.btnAceptarRecibo = new System.Windows.Forms.Button();
            this.btnBuscarRecibo = new System.Windows.Forms.Button();
            this.dgvRecibo = new System.Windows.Forms.DataGridView();
            this.txtBuscarRecibo = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecibo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirRecibo
            // 
            this.btnSalirRecibo.Location = new System.Drawing.Point(576, 389);
            this.btnSalirRecibo.Name = "btnSalirRecibo";
            this.btnSalirRecibo.Size = new System.Drawing.Size(75, 23);
            this.btnSalirRecibo.TabIndex = 17;
            this.btnSalirRecibo.Text = "Salir";
            this.btnSalirRecibo.UseVisualStyleBackColor = true;
            this.btnSalirRecibo.Click += new System.EventHandler(this.btnSalirRecibo_Click);
            // 
            // btnAceptarRecibo
            // 
            this.btnAceptarRecibo.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarRecibo.Name = "btnAceptarRecibo";
            this.btnAceptarRecibo.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarRecibo.TabIndex = 16;
            this.btnAceptarRecibo.Text = "Aceptar";
            this.btnAceptarRecibo.UseVisualStyleBackColor = true;
            this.btnAceptarRecibo.Click += new System.EventHandler(this.btnAceptarRecibo_Click);
            // 
            // btnBuscarRecibo
            // 
            this.btnBuscarRecibo.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarRecibo.Name = "btnBuscarRecibo";
            this.btnBuscarRecibo.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarRecibo.TabIndex = 15;
            this.btnBuscarRecibo.Text = "Buscar";
            this.btnBuscarRecibo.UseVisualStyleBackColor = true;
            this.btnBuscarRecibo.Click += new System.EventHandler(this.btnBuscarRecibo_Click);
            // 
            // dgvRecibo
            // 
            this.dgvRecibo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecibo.Location = new System.Drawing.Point(15, 62);
            this.dgvRecibo.Name = "dgvRecibo";
            this.dgvRecibo.Size = new System.Drawing.Size(636, 310);
            this.dgvRecibo.TabIndex = 14;
            this.dgvRecibo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecibo_CellClick);
            // 
            // txtBuscarRecibo
            // 
            this.txtBuscarRecibo.Location = new System.Drawing.Point(15, 35);
            this.txtBuscarRecibo.Name = "txtBuscarRecibo";
            this.txtBuscarRecibo.Size = new System.Drawing.Size(274, 20);
            this.txtBuscarRecibo.TabIndex = 13;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 9);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 12;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaReciboCobros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(665, 425);
            this.Controls.Add(this.btnSalirRecibo);
            this.Controls.Add(this.btnAceptarRecibo);
            this.Controls.Add(this.btnBuscarRecibo);
            this.Controls.Add(this.dgvRecibo);
            this.Controls.Add(this.txtBuscarRecibo);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaReciboCobros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaReciboCobros_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecibo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirRecibo;
        private System.Windows.Forms.Button btnAceptarRecibo;
        private System.Windows.Forms.Button btnBuscarRecibo;
        private System.Windows.Forms.DataGridView dgvRecibo;
        private System.Windows.Forms.TextBox txtBuscarRecibo;
        private System.Windows.Forms.Label lblBuscar;
    }
}