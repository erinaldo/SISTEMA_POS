namespace InicioAplicacion.Formularios
{
    partial class FAyudaProductos
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
            this.btnSalirProduc = new System.Windows.Forms.Button();
            this.btnAceptarProduc = new System.Windows.Forms.Button();
            this.btnBuscarProduc = new System.Windows.Forms.Button();
            this.dgvProduc = new System.Windows.Forms.DataGridView();
            this.txtBuscaProduc = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduc)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirProduc
            // 
            this.btnSalirProduc.Location = new System.Drawing.Point(576, 389);
            this.btnSalirProduc.Name = "btnSalirProduc";
            this.btnSalirProduc.Size = new System.Drawing.Size(75, 23);
            this.btnSalirProduc.TabIndex = 11;
            this.btnSalirProduc.Text = "Salir";
            this.btnSalirProduc.UseVisualStyleBackColor = true;
            this.btnSalirProduc.Click += new System.EventHandler(this.btnSalirProduc_Click);
            // 
            // btnAceptarProduc
            // 
            this.btnAceptarProduc.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarProduc.Name = "btnAceptarProduc";
            this.btnAceptarProduc.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarProduc.TabIndex = 10;
            this.btnAceptarProduc.Text = "Aceptar";
            this.btnAceptarProduc.UseVisualStyleBackColor = true;
            this.btnAceptarProduc.Click += new System.EventHandler(this.btnAceptarProduc_Click);
            // 
            // btnBuscarProduc
            // 
            this.btnBuscarProduc.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarProduc.Name = "btnBuscarProduc";
            this.btnBuscarProduc.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarProduc.TabIndex = 9;
            this.btnBuscarProduc.Text = "Buscar";
            this.btnBuscarProduc.UseVisualStyleBackColor = true;
            this.btnBuscarProduc.Click += new System.EventHandler(this.btnBuscarProduc_Click);
            // 
            // dgvProduc
            // 
            this.dgvProduc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduc.Location = new System.Drawing.Point(15, 62);
            this.dgvProduc.Name = "dgvProduc";
            this.dgvProduc.Size = new System.Drawing.Size(636, 310);
            this.dgvProduc.TabIndex = 8;
            this.dgvProduc.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduc_CellClick);
            this.dgvProduc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduc_CellDoubleClick);
            // 
            // txtBuscaProduc
            // 
            this.txtBuscaProduc.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaProduc.Name = "txtBuscaProduc";
            this.txtBuscaProduc.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaProduc.TabIndex = 7;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 9);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 6;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(670, 421);
            this.Controls.Add(this.btnSalirProduc);
            this.Controls.Add(this.btnAceptarProduc);
            this.Controls.Add(this.btnBuscarProduc);
            this.Controls.Add(this.dgvProduc);
            this.Controls.Add(this.txtBuscaProduc);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirProduc;
        private System.Windows.Forms.Button btnAceptarProduc;
        private System.Windows.Forms.Button btnBuscarProduc;
        private System.Windows.Forms.DataGridView dgvProduc;
        private System.Windows.Forms.TextBox txtBuscaProduc;
        private System.Windows.Forms.Label lblBuscar;
    }
}