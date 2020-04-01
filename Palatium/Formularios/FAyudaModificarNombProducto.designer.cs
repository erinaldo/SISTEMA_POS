namespace Palatium.Formularios
{
    partial class FAyudaModificarNombProducto
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
            this.btnSalirNombre = new System.Windows.Forms.Button();
            this.btnAceptarNombre = new System.Windows.Forms.Button();
            this.btnBuscarNombre = new System.Windows.Forms.Button();
            this.dgvNombre = new System.Windows.Forms.DataGridView();
            this.txtBuscaNombre = new System.Windows.Forms.TextBox();
            this.lblBuscarNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNombre)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirNombre
            // 
            this.btnSalirNombre.Location = new System.Drawing.Point(576, 389);
            this.btnSalirNombre.Name = "btnSalirNombre";
            this.btnSalirNombre.Size = new System.Drawing.Size(75, 23);
            this.btnSalirNombre.TabIndex = 23;
            this.btnSalirNombre.Text = "Salir";
            this.btnSalirNombre.UseVisualStyleBackColor = true;
            this.btnSalirNombre.Click += new System.EventHandler(this.btnSalirNombre_Click);
            // 
            // btnAceptarNombre
            // 
            this.btnAceptarNombre.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarNombre.Name = "btnAceptarNombre";
            this.btnAceptarNombre.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarNombre.TabIndex = 22;
            this.btnAceptarNombre.Text = "Aceptar";
            this.btnAceptarNombre.UseVisualStyleBackColor = true;
            this.btnAceptarNombre.Click += new System.EventHandler(this.btnAceptarNombre_Click);
            // 
            // btnBuscarNombre
            // 
            this.btnBuscarNombre.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarNombre.Name = "btnBuscarNombre";
            this.btnBuscarNombre.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarNombre.TabIndex = 21;
            this.btnBuscarNombre.Text = "Buscar";
            this.btnBuscarNombre.UseVisualStyleBackColor = true;
            this.btnBuscarNombre.Click += new System.EventHandler(this.btnBuscarNombre_Click);
            // 
            // dgvNombre
            // 
            this.dgvNombre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNombre.Location = new System.Drawing.Point(15, 62);
            this.dgvNombre.Name = "dgvNombre";
            this.dgvNombre.Size = new System.Drawing.Size(636, 310);
            this.dgvNombre.TabIndex = 20;
            this.dgvNombre.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNombre_CellClick);
            // 
            // txtBuscaNombre
            // 
            this.txtBuscaNombre.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaNombre.Name = "txtBuscaNombre";
            this.txtBuscaNombre.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaNombre.TabIndex = 19;
            // 
            // lblBuscarNombre
            // 
            this.lblBuscarNombre.Location = new System.Drawing.Point(12, 9);
            this.lblBuscarNombre.Name = "lblBuscarNombre";
            this.lblBuscarNombre.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarNombre.TabIndex = 18;
            this.lblBuscarNombre.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaModificarNombProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(672, 425);
            this.Controls.Add(this.btnSalirNombre);
            this.Controls.Add(this.btnAceptarNombre);
            this.Controls.Add(this.btnBuscarNombre);
            this.Controls.Add(this.dgvNombre);
            this.Controls.Add(this.txtBuscaNombre);
            this.Controls.Add(this.lblBuscarNombre);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaModificarNombProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaModificarNombProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNombre)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirNombre;
        private System.Windows.Forms.Button btnAceptarNombre;
        private System.Windows.Forms.Button btnBuscarNombre;
        private System.Windows.Forms.DataGridView dgvNombre;
        private System.Windows.Forms.TextBox txtBuscaNombre;
        private System.Windows.Forms.Label lblBuscarNombre;
    }
}