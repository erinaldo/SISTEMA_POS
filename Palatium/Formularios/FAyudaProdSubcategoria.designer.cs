namespace Palatium.Formularios
{
    partial class FAyudaProdSubcategoria
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
            this.btnSalirSubCategoria = new System.Windows.Forms.Button();
            this.btnAceptarSubCategoria = new System.Windows.Forms.Button();
            this.btnBuscarSubCategoria = new System.Windows.Forms.Button();
            this.dgvSubCategoria = new System.Windows.Forms.DataGridView();
            this.txtBuscaSubCategoria = new System.Windows.Forms.TextBox();
            this.lblBuscarFactOrd = new System.Windows.Forms.Label();
            this.cmbCategorias = new MisControles.ComboDatos();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategoria)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirSubCategoria
            // 
            this.btnSalirSubCategoria.Location = new System.Drawing.Point(567, 389);
            this.btnSalirSubCategoria.Name = "btnSalirSubCategoria";
            this.btnSalirSubCategoria.Size = new System.Drawing.Size(75, 23);
            this.btnSalirSubCategoria.TabIndex = 23;
            this.btnSalirSubCategoria.Text = "Salir";
            this.btnSalirSubCategoria.UseVisualStyleBackColor = true;
            this.btnSalirSubCategoria.Click += new System.EventHandler(this.btnSalirSubCategoria_Click);
            // 
            // btnAceptarSubCategoria
            // 
            this.btnAceptarSubCategoria.Location = new System.Drawing.Point(475, 391);
            this.btnAceptarSubCategoria.Name = "btnAceptarSubCategoria";
            this.btnAceptarSubCategoria.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarSubCategoria.TabIndex = 22;
            this.btnAceptarSubCategoria.Text = "Aceptar";
            this.btnAceptarSubCategoria.UseVisualStyleBackColor = true;
            this.btnAceptarSubCategoria.Click += new System.EventHandler(this.btnAceptarSubCategoria_Click);
            // 
            // btnBuscarSubCategoria
            // 
            this.btnBuscarSubCategoria.Location = new System.Drawing.Point(295, 32);
            this.btnBuscarSubCategoria.Name = "btnBuscarSubCategoria";
            this.btnBuscarSubCategoria.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarSubCategoria.TabIndex = 21;
            this.btnBuscarSubCategoria.Text = "Buscar";
            this.btnBuscarSubCategoria.UseVisualStyleBackColor = true;
            this.btnBuscarSubCategoria.Click += new System.EventHandler(this.btnBuscarSubCategoria_Click);
            // 
            // dgvSubCategoria
            // 
            this.dgvSubCategoria.AllowUserToAddRows = false;
            this.dgvSubCategoria.AllowUserToDeleteRows = false;
            this.dgvSubCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubCategoria.Location = new System.Drawing.Point(6, 62);
            this.dgvSubCategoria.Name = "dgvSubCategoria";
            this.dgvSubCategoria.ReadOnly = true;
            this.dgvSubCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubCategoria.Size = new System.Drawing.Size(636, 310);
            this.dgvSubCategoria.TabIndex = 20;
            this.dgvSubCategoria.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubCategoria_CellClick);
            this.dgvSubCategoria.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubCategoria_CellDoubleClick);
            // 
            // txtBuscaSubCategoria
            // 
            this.txtBuscaSubCategoria.Location = new System.Drawing.Point(6, 35);
            this.txtBuscaSubCategoria.Name = "txtBuscaSubCategoria";
            this.txtBuscaSubCategoria.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaSubCategoria.TabIndex = 19;
            // 
            // lblBuscarFactOrd
            // 
            this.lblBuscarFactOrd.Location = new System.Drawing.Point(3, 9);
            this.lblBuscarFactOrd.Name = "lblBuscarFactOrd";
            this.lblBuscarFactOrd.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarFactOrd.TabIndex = 18;
            this.lblBuscarFactOrd.Text = "Busqueda basada en la Nombre";
            // 
            // cmbCategorias
            // 
            this.cmbCategorias.FormattingEnabled = true;
            this.cmbCategorias.Location = new System.Drawing.Point(404, 32);
            this.cmbCategorias.Name = "cmbCategorias";
            this.cmbCategorias.Size = new System.Drawing.Size(176, 21);
            this.cmbCategorias.TabIndex = 24;
            this.cmbCategorias.SelectedIndexChanged += new System.EventHandler(this.cmbCategorias_SelectedIndexChanged);
            // 
            // FAyudaProdSubcategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(652, 420);
            this.Controls.Add(this.cmbCategorias);
            this.Controls.Add(this.btnSalirSubCategoria);
            this.Controls.Add(this.btnAceptarSubCategoria);
            this.Controls.Add(this.btnBuscarSubCategoria);
            this.Controls.Add(this.dgvSubCategoria);
            this.Controls.Add(this.txtBuscaSubCategoria);
            this.Controls.Add(this.lblBuscarFactOrd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaProdSubcategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaProdSubcategoria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubCategoria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirSubCategoria;
        private System.Windows.Forms.Button btnAceptarSubCategoria;
        private System.Windows.Forms.Button btnBuscarSubCategoria;
        private System.Windows.Forms.DataGridView dgvSubCategoria;
        private System.Windows.Forms.TextBox txtBuscaSubCategoria;
        private System.Windows.Forms.Label lblBuscarFactOrd;
        private MisControles.ComboDatos cmbCategorias;
    }
}