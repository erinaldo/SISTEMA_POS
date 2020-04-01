namespace Palatium.Formularios
{
    partial class FAyudaIngresProduCategori
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
            this.btnSalirCategoria = new System.Windows.Forms.Button();
            this.btnAceptarCategoria = new System.Windows.Forms.Button();
            this.btnBuscarCategoria = new System.Windows.Forms.Button();
            this.dgvCategoria = new System.Windows.Forms.DataGridView();
            this.txtBuscaCategoria = new System.Windows.Forms.TextBox();
            this.lblBuscarFactOrd = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirCategoria
            // 
            this.btnSalirCategoria.Location = new System.Drawing.Point(576, 389);
            this.btnSalirCategoria.Name = "btnSalirCategoria";
            this.btnSalirCategoria.Size = new System.Drawing.Size(75, 23);
            this.btnSalirCategoria.TabIndex = 17;
            this.btnSalirCategoria.Text = "Salir";
            this.btnSalirCategoria.UseVisualStyleBackColor = true;
            this.btnSalirCategoria.Click += new System.EventHandler(this.btnSalirCategoria_Click);
            // 
            // btnAceptarCategoria
            // 
            this.btnAceptarCategoria.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarCategoria.Name = "btnAceptarCategoria";
            this.btnAceptarCategoria.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarCategoria.TabIndex = 16;
            this.btnAceptarCategoria.Text = "Aceptar";
            this.btnAceptarCategoria.UseVisualStyleBackColor = true;
            this.btnAceptarCategoria.Click += new System.EventHandler(this.btnAceptarCategoria_Click);
            // 
            // btnBuscarCategoria
            // 
            this.btnBuscarCategoria.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarCategoria.Name = "btnBuscarCategoria";
            this.btnBuscarCategoria.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarCategoria.TabIndex = 15;
            this.btnBuscarCategoria.Text = "Buscar";
            this.btnBuscarCategoria.UseVisualStyleBackColor = true;
            this.btnBuscarCategoria.Click += new System.EventHandler(this.btnBuscarCategoria_Click);
            // 
            // dgvCategoria
            // 
            this.dgvCategoria.AllowUserToAddRows = false;
            this.dgvCategoria.AllowUserToDeleteRows = false;
            this.dgvCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategoria.Location = new System.Drawing.Point(15, 62);
            this.dgvCategoria.Name = "dgvCategoria";
            this.dgvCategoria.ReadOnly = true;
            this.dgvCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategoria.Size = new System.Drawing.Size(636, 310);
            this.dgvCategoria.TabIndex = 14;
            this.dgvCategoria.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellClick);
            this.dgvCategoria.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellContentClick);
            this.dgvCategoria.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellDoubleClick);
            // 
            // txtBuscaCategoria
            // 
            this.txtBuscaCategoria.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaCategoria.Name = "txtBuscaCategoria";
            this.txtBuscaCategoria.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaCategoria.TabIndex = 13;
            this.txtBuscaCategoria.TextChanged += new System.EventHandler(this.txtBuscaCategoria_TextChanged);
            // 
            // lblBuscarFactOrd
            // 
            this.lblBuscarFactOrd.Location = new System.Drawing.Point(12, 9);
            this.lblBuscarFactOrd.Name = "lblBuscarFactOrd";
            this.lblBuscarFactOrd.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarFactOrd.TabIndex = 12;
            this.lblBuscarFactOrd.Text = "Busqueda basada en la Nombre";
            this.lblBuscarFactOrd.Click += new System.EventHandler(this.lblBuscarFactOrd_Click);
            // 
            // FAyudaIngresProduCategori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(672, 428);
            this.Controls.Add(this.btnSalirCategoria);
            this.Controls.Add(this.btnAceptarCategoria);
            this.Controls.Add(this.btnBuscarCategoria);
            this.Controls.Add(this.dgvCategoria);
            this.Controls.Add(this.txtBuscaCategoria);
            this.Controls.Add(this.lblBuscarFactOrd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaIngresProduCategori";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaIngresProduCategori_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirCategoria;
        private System.Windows.Forms.Button btnAceptarCategoria;
        private System.Windows.Forms.Button btnBuscarCategoria;
        private System.Windows.Forms.DataGridView dgvCategoria;
        private System.Windows.Forms.TextBox txtBuscaCategoria;
        private System.Windows.Forms.Label lblBuscarFactOrd;
    }
}