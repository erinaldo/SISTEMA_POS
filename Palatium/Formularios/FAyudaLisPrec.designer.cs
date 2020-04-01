namespace Palatium.Formularios
{
    partial class FAyudaLisPrec
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
            this.txtBuscaLisPrec = new System.Windows.Forms.TextBox();
            this.dgvLisPreci = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLisPreci)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(13, 13);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // txtBuscaLisPrec
            // 
            this.txtBuscaLisPrec.Location = new System.Drawing.Point(16, 39);
            this.txtBuscaLisPrec.Name = "txtBuscaLisPrec";
            this.txtBuscaLisPrec.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaLisPrec.TabIndex = 1;
            // 
            // dgvLisPreci
            // 
            this.dgvLisPreci.AllowUserToAddRows = false;
            this.dgvLisPreci.AllowUserToDeleteRows = false;
            this.dgvLisPreci.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLisPreci.Location = new System.Drawing.Point(16, 66);
            this.dgvLisPreci.Name = "dgvLisPreci";
            this.dgvLisPreci.ReadOnly = true;
            this.dgvLisPreci.Size = new System.Drawing.Size(636, 310);
            this.dgvLisPreci.TabIndex = 2;
            this.dgvLisPreci.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLisPreci_CellClick);
            this.dgvLisPreci.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLisPreci_CellDoubleClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(305, 36);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(485, 395);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(577, 393);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // FAyudaLisPrec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(674, 430);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvLisPreci);
            this.Controls.Add(this.txtBuscaLisPrec);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaLisPrec";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaLisPrec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLisPreci)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscaLisPrec;
        private System.Windows.Forms.DataGridView dgvLisPreci;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnSalir;
    }
}