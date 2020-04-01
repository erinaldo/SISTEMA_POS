namespace InicioAplicacion.Formularios
{
    partial class FAyudaModificadores
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
            this.btnSalirModificadores = new System.Windows.Forms.Button();
            this.btnAceptarModificadores = new System.Windows.Forms.Button();
            this.btnBuscarModificadores = new System.Windows.Forms.Button();
            this.dgvModificadores = new System.Windows.Forms.DataGridView();
            this.txtBuscaModificadores = new System.Windows.Forms.TextBox();
            this.lblBuscarFactOrd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModificadores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirModificadores
            // 
            this.btnSalirModificadores.Location = new System.Drawing.Point(576, 389);
            this.btnSalirModificadores.Name = "btnSalirModificadores";
            this.btnSalirModificadores.Size = new System.Drawing.Size(75, 23);
            this.btnSalirModificadores.TabIndex = 23;
            this.btnSalirModificadores.Text = "Salir";
            this.btnSalirModificadores.UseVisualStyleBackColor = true;
            this.btnSalirModificadores.Click += new System.EventHandler(this.btnSalirCategoria_Click);
            // 
            // btnAceptarModificadores
            // 
            this.btnAceptarModificadores.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarModificadores.Name = "btnAceptarModificadores";
            this.btnAceptarModificadores.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarModificadores.TabIndex = 22;
            this.btnAceptarModificadores.Text = "Aceptar";
            this.btnAceptarModificadores.UseVisualStyleBackColor = true;
            this.btnAceptarModificadores.Click += new System.EventHandler(this.btnAceptarCategoria_Click);
            // 
            // btnBuscarModificadores
            // 
            this.btnBuscarModificadores.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarModificadores.Name = "btnBuscarModificadores";
            this.btnBuscarModificadores.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarModificadores.TabIndex = 21;
            this.btnBuscarModificadores.Text = "Buscar";
            this.btnBuscarModificadores.UseVisualStyleBackColor = true;
            this.btnBuscarModificadores.Click += new System.EventHandler(this.btnBuscarCategoria_Click);
            // 
            // dgvModificadores
            // 
            this.dgvModificadores.AllowUserToAddRows = false;
            this.dgvModificadores.AllowUserToDeleteRows = false;
            this.dgvModificadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModificadores.Location = new System.Drawing.Point(15, 62);
            this.dgvModificadores.Name = "dgvModificadores";
            this.dgvModificadores.ReadOnly = true;
            this.dgvModificadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvModificadores.Size = new System.Drawing.Size(636, 310);
            this.dgvModificadores.TabIndex = 20;
            this.dgvModificadores.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellClick);
            this.dgvModificadores.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellDoubleClick);
            // 
            // txtBuscaModificadores
            // 
            this.txtBuscaModificadores.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaModificadores.Name = "txtBuscaModificadores";
            this.txtBuscaModificadores.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaModificadores.TabIndex = 19;
            // 
            // lblBuscarFactOrd
            // 
            this.lblBuscarFactOrd.Location = new System.Drawing.Point(12, 9);
            this.lblBuscarFactOrd.Name = "lblBuscarFactOrd";
            this.lblBuscarFactOrd.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarFactOrd.TabIndex = 18;
            this.lblBuscarFactOrd.Text = "Busqueda basada en la Nombre";
            // 
            // FAyudaModificadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(664, 423);
            this.Controls.Add(this.btnSalirModificadores);
            this.Controls.Add(this.btnAceptarModificadores);
            this.Controls.Add(this.btnBuscarModificadores);
            this.Controls.Add(this.dgvModificadores);
            this.Controls.Add(this.txtBuscaModificadores);
            this.Controls.Add(this.lblBuscarFactOrd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaModificadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaModificadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModificadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirModificadores;
        private System.Windows.Forms.Button btnAceptarModificadores;
        private System.Windows.Forms.Button btnBuscarModificadores;
        private System.Windows.Forms.DataGridView dgvModificadores;
        private System.Windows.Forms.TextBox txtBuscaModificadores;
        private System.Windows.Forms.Label lblBuscarFactOrd;
    }
}