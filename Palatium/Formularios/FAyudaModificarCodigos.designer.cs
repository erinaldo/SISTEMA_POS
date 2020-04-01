namespace Palatium.Formularios
{
    partial class FAyudaModificarCodigos
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
            this.btnSalirCodigo = new System.Windows.Forms.Button();
            this.btnAceptarCodigo = new System.Windows.Forms.Button();
            this.btnBuscarCodigo = new System.Windows.Forms.Button();
            this.dgvCodigo = new System.Windows.Forms.DataGridView();
            this.txtBuscaCodigo = new System.Windows.Forms.TextBox();
            this.lblBuscarCodigo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodigo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirCodigo
            // 
            this.btnSalirCodigo.Location = new System.Drawing.Point(576, 389);
            this.btnSalirCodigo.Name = "btnSalirCodigo";
            this.btnSalirCodigo.Size = new System.Drawing.Size(75, 23);
            this.btnSalirCodigo.TabIndex = 17;
            this.btnSalirCodigo.Text = "Salir";
            this.btnSalirCodigo.UseVisualStyleBackColor = true;
            this.btnSalirCodigo.Click += new System.EventHandler(this.btnSalirCodigo_Click);
            // 
            // btnAceptarCodigo
            // 
            this.btnAceptarCodigo.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarCodigo.Name = "btnAceptarCodigo";
            this.btnAceptarCodigo.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarCodigo.TabIndex = 16;
            this.btnAceptarCodigo.Text = "Aceptar";
            this.btnAceptarCodigo.UseVisualStyleBackColor = true;
            this.btnAceptarCodigo.Click += new System.EventHandler(this.btnAceptarCodigo_Click);
            // 
            // btnBuscarCodigo
            // 
            this.btnBuscarCodigo.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarCodigo.Name = "btnBuscarCodigo";
            this.btnBuscarCodigo.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarCodigo.TabIndex = 15;
            this.btnBuscarCodigo.Text = "Buscar";
            this.btnBuscarCodigo.UseVisualStyleBackColor = true;
            this.btnBuscarCodigo.Click += new System.EventHandler(this.btnBuscarCodigo_Click);
            // 
            // dgvCodigo
            // 
            this.dgvCodigo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCodigo.Location = new System.Drawing.Point(15, 62);
            this.dgvCodigo.Name = "dgvCodigo";
            this.dgvCodigo.Size = new System.Drawing.Size(636, 310);
            this.dgvCodigo.TabIndex = 14;
            this.dgvCodigo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCodigo_CellClick);
            // 
            // txtBuscaCodigo
            // 
            this.txtBuscaCodigo.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaCodigo.Name = "txtBuscaCodigo";
            this.txtBuscaCodigo.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaCodigo.TabIndex = 13;
            // 
            // lblBuscarCodigo
            // 
            this.lblBuscarCodigo.Location = new System.Drawing.Point(12, 9);
            this.lblBuscarCodigo.Name = "lblBuscarCodigo";
            this.lblBuscarCodigo.Size = new System.Drawing.Size(277, 22);
            this.lblBuscarCodigo.TabIndex = 12;
            this.lblBuscarCodigo.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaModificarCodigos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(664, 425);
            this.Controls.Add(this.btnSalirCodigo);
            this.Controls.Add(this.btnAceptarCodigo);
            this.Controls.Add(this.btnBuscarCodigo);
            this.Controls.Add(this.dgvCodigo);
            this.Controls.Add(this.txtBuscaCodigo);
            this.Controls.Add(this.lblBuscarCodigo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaModificarCodigos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaModificarCodigos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCodigo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirCodigo;
        private System.Windows.Forms.Button btnAceptarCodigo;
        private System.Windows.Forms.Button btnBuscarCodigo;
        private System.Windows.Forms.DataGridView dgvCodigo;
        private System.Windows.Forms.TextBox txtBuscaCodigo;
        private System.Windows.Forms.Label lblBuscarCodigo;
    }
}