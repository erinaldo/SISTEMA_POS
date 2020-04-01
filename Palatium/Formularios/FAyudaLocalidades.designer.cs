namespace Palatium.Formularios
{
    partial class FAyudaLocalidades
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
            this.btnSalirLocalidades = new System.Windows.Forms.Button();
            this.btnAceptarLocalidades = new System.Windows.Forms.Button();
            this.btnBuscarLocalidades = new System.Windows.Forms.Button();
            this.dgvLocalidades = new System.Windows.Forms.DataGridView();
            this.txtBuscaLocalidades = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalidades)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirLocalidades
            // 
            this.btnSalirLocalidades.Location = new System.Drawing.Point(484, 390);
            this.btnSalirLocalidades.Name = "btnSalirLocalidades";
            this.btnSalirLocalidades.Size = new System.Drawing.Size(75, 23);
            this.btnSalirLocalidades.TabIndex = 3;
            this.btnSalirLocalidades.Text = "Salir";
            this.btnSalirLocalidades.UseVisualStyleBackColor = true;
            this.btnSalirLocalidades.Click += new System.EventHandler(this.btnSalirLocalidades_Click);
            // 
            // btnAceptarLocalidades
            // 
            this.btnAceptarLocalidades.Location = new System.Drawing.Point(403, 390);
            this.btnAceptarLocalidades.Name = "btnAceptarLocalidades";
            this.btnAceptarLocalidades.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarLocalidades.TabIndex = 2;
            this.btnAceptarLocalidades.Text = "Aceptar";
            this.btnAceptarLocalidades.UseVisualStyleBackColor = true;
            this.btnAceptarLocalidades.Click += new System.EventHandler(this.btnAceptarLocalidades_Click);
            // 
            // btnBuscarLocalidades
            // 
            this.btnBuscarLocalidades.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarLocalidades.Name = "btnBuscarLocalidades";
            this.btnBuscarLocalidades.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarLocalidades.TabIndex = 1;
            this.btnBuscarLocalidades.Text = "Buscar";
            this.btnBuscarLocalidades.UseVisualStyleBackColor = true;
            this.btnBuscarLocalidades.Click += new System.EventHandler(this.btnBuscarLocalidades_Click);
            // 
            // dgvLocalidades
            // 
            this.dgvLocalidades.AllowUserToAddRows = false;
            this.dgvLocalidades.AllowUserToDeleteRows = false;
            this.dgvLocalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalidades.Location = new System.Drawing.Point(15, 62);
            this.dgvLocalidades.Name = "dgvLocalidades";
            this.dgvLocalidades.ReadOnly = true;
            this.dgvLocalidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalidades.Size = new System.Drawing.Size(544, 310);
            this.dgvLocalidades.TabIndex = 8;
            this.dgvLocalidades.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocalidades_CellClick);
            this.dgvLocalidades.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLocalidades_CellDoubleClick);
            // 
            // txtBuscaLocalidades
            // 
            this.txtBuscaLocalidades.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaLocalidades.Name = "txtBuscaLocalidades";
            this.txtBuscaLocalidades.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaLocalidades.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 9);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 6;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaLocalidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 425);
            this.Controls.Add(this.btnSalirLocalidades);
            this.Controls.Add(this.btnAceptarLocalidades);
            this.Controls.Add(this.btnBuscarLocalidades);
            this.Controls.Add(this.dgvLocalidades);
            this.Controls.Add(this.txtBuscaLocalidades);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaLocalidades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Búsqueda";
            this.Load += new System.EventHandler(this.FAyudaLocalidades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalidades)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirLocalidades;
        private System.Windows.Forms.Button btnAceptarLocalidades;
        private System.Windows.Forms.Button btnBuscarLocalidades;
        private System.Windows.Forms.DataGridView dgvLocalidades;
        private System.Windows.Forms.TextBox txtBuscaLocalidades;
        private System.Windows.Forms.Label lblBuscar;
    }
}