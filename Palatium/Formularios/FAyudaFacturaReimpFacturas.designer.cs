namespace Palatium.Formularios
{
    partial class FAyudaFacturaReimpFacturas
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
            this.btnSalirFactura = new System.Windows.Forms.Button();
            this.btnAceptarFactura = new System.Windows.Forms.Button();
            this.btnBuscarFactura = new System.Windows.Forms.Button();
            this.dgvFactura = new System.Windows.Forms.DataGridView();
            this.txtBuscaFactura = new System.Windows.Forms.TextBox();
            this.lblBuscarFactura = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirFactura
            // 
            this.btnSalirFactura.Location = new System.Drawing.Point(576, 389);
            this.btnSalirFactura.Name = "btnSalirFactura";
            this.btnSalirFactura.Size = new System.Drawing.Size(75, 23);
            this.btnSalirFactura.TabIndex = 17;
            this.btnSalirFactura.Text = "Salir";
            this.btnSalirFactura.UseVisualStyleBackColor = true;
            this.btnSalirFactura.Click += new System.EventHandler(this.btnSalirFactura_Click);
            // 
            // btnAceptarFactura
            // 
            this.btnAceptarFactura.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarFactura.Name = "btnAceptarFactura";
            this.btnAceptarFactura.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarFactura.TabIndex = 16;
            this.btnAceptarFactura.Text = "Aceptar";
            this.btnAceptarFactura.UseVisualStyleBackColor = true;
            this.btnAceptarFactura.Click += new System.EventHandler(this.btnAceptarFactura_Click);
            // 
            // btnBuscarFactura
            // 
            this.btnBuscarFactura.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarFactura.Name = "btnBuscarFactura";
            this.btnBuscarFactura.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarFactura.TabIndex = 15;
            this.btnBuscarFactura.Text = "Buscar";
            this.btnBuscarFactura.UseVisualStyleBackColor = true;
            this.btnBuscarFactura.Click += new System.EventHandler(this.btnBuscarFactura_Click);
            // 
            // dgvFactura
            // 
            this.dgvFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFactura.Location = new System.Drawing.Point(15, 62);
            this.dgvFactura.Name = "dgvFactura";
            this.dgvFactura.Size = new System.Drawing.Size(636, 310);
            this.dgvFactura.TabIndex = 14;
            this.dgvFactura.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFactura_CellClick);
            // 
            // txtBuscaFactura
            // 
            this.txtBuscaFactura.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaFactura.Name = "txtBuscaFactura";
            this.txtBuscaFactura.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaFactura.TabIndex = 13;
            // 
            // lblBuscarFactura
            // 
            this.lblBuscarFactura.Location = new System.Drawing.Point(12, 9);
            this.lblBuscarFactura.Name = "lblBuscarFactura";
            this.lblBuscarFactura.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarFactura.TabIndex = 12;
            this.lblBuscarFactura.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaFacturaReimpFacturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 426);
            this.Controls.Add(this.btnSalirFactura);
            this.Controls.Add(this.btnAceptarFactura);
            this.Controls.Add(this.btnBuscarFactura);
            this.Controls.Add(this.dgvFactura);
            this.Controls.Add(this.txtBuscaFactura);
            this.Controls.Add(this.lblBuscarFactura);
            this.Name = "FAyudaFacturaReimpFacturas";
            this.Text = "FAyudaFacturaReimpFacturas";
            this.Load += new System.EventHandler(this.FAyudaFacturaReimpFacturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirFactura;
        private System.Windows.Forms.Button btnAceptarFactura;
        private System.Windows.Forms.Button btnBuscarFactura;
        private System.Windows.Forms.DataGridView dgvFactura;
        private System.Windows.Forms.TextBox txtBuscaFactura;
        private System.Windows.Forms.Label lblBuscarFactura;
    }
}