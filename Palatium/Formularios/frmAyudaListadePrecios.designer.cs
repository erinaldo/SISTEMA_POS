namespace InicioAplicacion.Formularios
{
    partial class frmAyudaListadePrecios
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
            this.lblBuscar = new System.Windows.Forms.Label();
            this.btnBuscarProductos = new System.Windows.Forms.Button();
            this.txtBuscaProductos = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnSalirProductos = new System.Windows.Forms.Button();
            this.btnAceptarProductos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 20);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 7;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // btnBuscarProductos
            // 
            this.btnBuscarProductos.Location = new System.Drawing.Point(304, 42);
            this.btnBuscarProductos.Name = "btnBuscarProductos";
            this.btnBuscarProductos.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarProductos.TabIndex = 11;
            this.btnBuscarProductos.Text = "Buscar";
            this.btnBuscarProductos.UseVisualStyleBackColor = true;
            // 
            // txtBuscaProductos
            // 
            this.txtBuscaProductos.Location = new System.Drawing.Point(15, 45);
            this.txtBuscaProductos.Name = "txtBuscaProductos";
            this.txtBuscaProductos.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaProductos.TabIndex = 10;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(15, 81);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(636, 310);
            this.dgvProductos.TabIndex = 12;
            // 
            // btnSalirProductos
            // 
            this.btnSalirProductos.Location = new System.Drawing.Point(573, 415);
            this.btnSalirProductos.Name = "btnSalirProductos";
            this.btnSalirProductos.Size = new System.Drawing.Size(75, 23);
            this.btnSalirProductos.TabIndex = 14;
            this.btnSalirProductos.Text = "Salir";
            this.btnSalirProductos.UseVisualStyleBackColor = true;
            // 
            // btnAceptarProductos
            // 
            this.btnAceptarProductos.Location = new System.Drawing.Point(481, 417);
            this.btnAceptarProductos.Name = "btnAceptarProductos";
            this.btnAceptarProductos.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarProductos.TabIndex = 13;
            this.btnAceptarProductos.Text = "Aceptar";
            this.btnAceptarProductos.UseVisualStyleBackColor = true;
            // 
            // frmAyudaListadePrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 452);
            this.Controls.Add(this.btnSalirProductos);
            this.Controls.Add(this.btnAceptarProductos);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.btnBuscarProductos);
            this.Controls.Add(this.txtBuscaProductos);
            this.Controls.Add(this.lblBuscar);
            this.Name = "frmAyudaListadePrecios";
            this.Text = "Categorías";
            this.Load += new System.EventHandler(this.frmAyudaListadePrecios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Button btnBuscarProductos;
        private System.Windows.Forms.TextBox txtBuscaProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnSalirProductos;
        private System.Windows.Forms.Button btnAceptarProductos;
    }
}