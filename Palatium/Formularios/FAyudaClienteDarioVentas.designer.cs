namespace Palatium.Formularios
{
    partial class FAyudaClienteDarioVentas
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
            this.btnSalirPersona = new System.Windows.Forms.Button();
            this.btnAceptarPersona = new System.Windows.Forms.Button();
            this.btnBuscarPersona = new System.Windows.Forms.Button();
            this.dgvPersona = new System.Windows.Forms.DataGridView();
            this.txtBuscaPersona = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersona)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirPersona
            // 
            this.btnSalirPersona.Location = new System.Drawing.Point(576, 389);
            this.btnSalirPersona.Name = "btnSalirPersona";
            this.btnSalirPersona.Size = new System.Drawing.Size(75, 23);
            this.btnSalirPersona.TabIndex = 17;
            this.btnSalirPersona.Text = "Salir";
            this.btnSalirPersona.UseVisualStyleBackColor = true;
            this.btnSalirPersona.Click += new System.EventHandler(this.btnSalirPersona_Click);
            // 
            // btnAceptarPersona
            // 
            this.btnAceptarPersona.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarPersona.Name = "btnAceptarPersona";
            this.btnAceptarPersona.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarPersona.TabIndex = 16;
            this.btnAceptarPersona.Text = "Aceptar";
            this.btnAceptarPersona.UseVisualStyleBackColor = true;
            this.btnAceptarPersona.Click += new System.EventHandler(this.btnAceptarPersona_Click);
            // 
            // btnBuscarPersona
            // 
            this.btnBuscarPersona.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarPersona.Name = "btnBuscarPersona";
            this.btnBuscarPersona.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarPersona.TabIndex = 15;
            this.btnBuscarPersona.Text = "Buscar";
            this.btnBuscarPersona.UseVisualStyleBackColor = true;
            this.btnBuscarPersona.Click += new System.EventHandler(this.btnBuscarPersona_Click);
            // 
            // dgvPersona
            // 
            this.dgvPersona.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPersona.Location = new System.Drawing.Point(15, 62);
            this.dgvPersona.Name = "dgvPersona";
            this.dgvPersona.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPersona.Size = new System.Drawing.Size(636, 310);
            this.dgvPersona.TabIndex = 14;
            this.dgvPersona.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPersona_CellClick);
            // 
            // txtBuscaPersona
            // 
            this.txtBuscaPersona.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaPersona.Name = "txtBuscaPersona";
            this.txtBuscaPersona.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaPersona.TabIndex = 13;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 9);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 12;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaClienteDarioVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(665, 422);
            this.Controls.Add(this.btnSalirPersona);
            this.Controls.Add(this.btnAceptarPersona);
            this.Controls.Add(this.btnBuscarPersona);
            this.Controls.Add(this.dgvPersona);
            this.Controls.Add(this.txtBuscaPersona);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaClienteDarioVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaClienteDarioVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPersona)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirPersona;
        private System.Windows.Forms.Button btnAceptarPersona;
        private System.Windows.Forms.Button btnBuscarPersona;
        private System.Windows.Forms.DataGridView dgvPersona;
        private System.Windows.Forms.TextBox txtBuscaPersona;
        private System.Windows.Forms.Label lblBuscar;
    }
}