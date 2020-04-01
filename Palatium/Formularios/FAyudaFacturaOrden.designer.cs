namespace InicioAplicacion.Formularios
{
    partial class FAyudaFacturaOrden
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
            this.btnSalirFactOrd = new System.Windows.Forms.Button();
            this.btnAceptarFactOrd = new System.Windows.Forms.Button();
            this.btnBuscarFactOrd = new System.Windows.Forms.Button();
            this.dgvFactOrd = new System.Windows.Forms.DataGridView();
            this.txtBuscaFactOrd = new System.Windows.Forms.TextBox();
            this.lblBuscarFactOrd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactOrd)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirFactOrd
            // 
            this.btnSalirFactOrd.Location = new System.Drawing.Point(576, 389);
            this.btnSalirFactOrd.Name = "btnSalirFactOrd";
            this.btnSalirFactOrd.Size = new System.Drawing.Size(75, 23);
            this.btnSalirFactOrd.TabIndex = 11;
            this.btnSalirFactOrd.Text = "Salir";
            this.btnSalirFactOrd.UseVisualStyleBackColor = true;
            this.btnSalirFactOrd.Click += new System.EventHandler(this.btnSalirFactOrd_Click);
            // 
            // btnAceptarFactOrd
            // 
            this.btnAceptarFactOrd.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarFactOrd.Name = "btnAceptarFactOrd";
            this.btnAceptarFactOrd.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarFactOrd.TabIndex = 10;
            this.btnAceptarFactOrd.Text = "Aceptar";
            this.btnAceptarFactOrd.UseVisualStyleBackColor = true;
            this.btnAceptarFactOrd.Click += new System.EventHandler(this.btnAceptarFactOrd_Click);
            // 
            // btnBuscarFactOrd
            // 
            this.btnBuscarFactOrd.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarFactOrd.Name = "btnBuscarFactOrd";
            this.btnBuscarFactOrd.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarFactOrd.TabIndex = 9;
            this.btnBuscarFactOrd.Text = "Buscar";
            this.btnBuscarFactOrd.UseVisualStyleBackColor = true;
            this.btnBuscarFactOrd.Click += new System.EventHandler(this.btnBuscarFactOrd_Click);
            // 
            // dgvFactOrd
            // 
            this.dgvFactOrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFactOrd.Location = new System.Drawing.Point(15, 62);
            this.dgvFactOrd.Name = "dgvFactOrd";
            this.dgvFactOrd.Size = new System.Drawing.Size(636, 310);
            this.dgvFactOrd.TabIndex = 8;
            this.dgvFactOrd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFactOrd_CellClick);
            // 
            // txtBuscaFactOrd
            // 
            this.txtBuscaFactOrd.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaFactOrd.Name = "txtBuscaFactOrd";
            this.txtBuscaFactOrd.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaFactOrd.TabIndex = 7;
            // 
            // lblBuscarFactOrd
            // 
            this.lblBuscarFactOrd.Location = new System.Drawing.Point(12, 9);
            this.lblBuscarFactOrd.Name = "lblBuscarFactOrd";
            this.lblBuscarFactOrd.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarFactOrd.TabIndex = 6;
            this.lblBuscarFactOrd.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaFacturaOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(667, 419);
            this.Controls.Add(this.btnSalirFactOrd);
            this.Controls.Add(this.btnAceptarFactOrd);
            this.Controls.Add(this.btnBuscarFactOrd);
            this.Controls.Add(this.dgvFactOrd);
            this.Controls.Add(this.txtBuscaFactOrd);
            this.Controls.Add(this.lblBuscarFactOrd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaFacturaOrden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaFacturaOrden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFactOrd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirFactOrd;
        private System.Windows.Forms.Button btnAceptarFactOrd;
        private System.Windows.Forms.Button btnBuscarFactOrd;
        private System.Windows.Forms.DataGridView dgvFactOrd;
        private System.Windows.Forms.TextBox txtBuscaFactOrd;
        private System.Windows.Forms.Label lblBuscarFactOrd;
    }
}