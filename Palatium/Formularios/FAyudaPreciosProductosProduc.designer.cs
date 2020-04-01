namespace Palatium.Formularios
{
    partial class FAyudaPreciosProductosProduc
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
            this.btnSalirProductos = new System.Windows.Forms.Button();
            this.btnAceptarProductos = new System.Windows.Forms.Button();
            this.btnBuscarProductos = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.txtBuscaProductos = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirProductos
            // 
            this.btnSalirProductos.Location = new System.Drawing.Point(576, 389);
            this.btnSalirProductos.Name = "btnSalirProductos";
            this.btnSalirProductos.Size = new System.Drawing.Size(75, 23);
            this.btnSalirProductos.TabIndex = 11;
            this.btnSalirProductos.Text = "Salir";
            this.btnSalirProductos.UseVisualStyleBackColor = true;
            this.btnSalirProductos.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptarProductos
            // 
            this.btnAceptarProductos.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarProductos.Name = "btnAceptarProductos";
            this.btnAceptarProductos.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarProductos.TabIndex = 10;
            this.btnAceptarProductos.Text = "Aceptar";
            this.btnAceptarProductos.UseVisualStyleBackColor = true;
            this.btnAceptarProductos.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnBuscarProductos
            // 
            this.btnBuscarProductos.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarProductos.Name = "btnBuscarProductos";
            this.btnBuscarProductos.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarProductos.TabIndex = 9;
            this.btnBuscarProductos.Text = "Buscar";
            this.btnBuscarProductos.UseVisualStyleBackColor = true;
            this.btnBuscarProductos.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(15, 62);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(636, 310);
            this.dgvProductos.TabIndex = 8;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLisPreci_CellClick);
            this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellDoubleClick);
            this.dgvProductos.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLisPreci_CellMouseDoubleClick);
            // 
            // txtBuscaProductos
            // 
            this.txtBuscaProductos.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaProductos.Name = "txtBuscaProductos";
            this.txtBuscaProductos.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaProductos.TabIndex = 7;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 9);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 6;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaPreciosProductosProduc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(662, 420);
            this.Controls.Add(this.btnSalirProductos);
            this.Controls.Add(this.btnAceptarProductos);
            this.Controls.Add(this.btnBuscarProductos);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.txtBuscaProductos);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaPreciosProductosProduc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaPreciosProductosProduc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirProductos;
        private System.Windows.Forms.Button btnAceptarProductos;
        private System.Windows.Forms.Button btnBuscarProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.TextBox txtBuscaProductos;
        private System.Windows.Forms.Label lblBuscar;
    }
}