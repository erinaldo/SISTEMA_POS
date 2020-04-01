namespace Palatium.Formularios
{
    partial class FAyudaRespoLocalida
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
            this.btnSalirRespon = new System.Windows.Forms.Button();
            this.btnAceptarRespon = new System.Windows.Forms.Button();
            this.btnBuscarRespon = new System.Windows.Forms.Button();
            this.dgvRespon = new System.Windows.Forms.DataGridView();
            this.txtBuscaRespon = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRespon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalirRespon
            // 
            this.btnSalirRespon.Location = new System.Drawing.Point(576, 389);
            this.btnSalirRespon.Name = "btnSalirRespon";
            this.btnSalirRespon.Size = new System.Drawing.Size(75, 23);
            this.btnSalirRespon.TabIndex = 17;
            this.btnSalirRespon.Text = "Salir";
            this.btnSalirRespon.UseVisualStyleBackColor = true;
            this.btnSalirRespon.Click += new System.EventHandler(this.btnSalirRespon_Click);
            // 
            // btnAceptarRespon
            // 
            this.btnAceptarRespon.Location = new System.Drawing.Point(484, 391);
            this.btnAceptarRespon.Name = "btnAceptarRespon";
            this.btnAceptarRespon.Size = new System.Drawing.Size(75, 23);
            this.btnAceptarRespon.TabIndex = 16;
            this.btnAceptarRespon.Text = "Aceptar";
            this.btnAceptarRespon.UseVisualStyleBackColor = true;
            this.btnAceptarRespon.Click += new System.EventHandler(this.btnAceptarRespon_Click);
            // 
            // btnBuscarRespon
            // 
            this.btnBuscarRespon.Location = new System.Drawing.Point(304, 32);
            this.btnBuscarRespon.Name = "btnBuscarRespon";
            this.btnBuscarRespon.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarRespon.TabIndex = 15;
            this.btnBuscarRespon.Text = "Buscar";
            this.btnBuscarRespon.UseVisualStyleBackColor = true;
            this.btnBuscarRespon.Click += new System.EventHandler(this.btnBuscarRespon_Click);
            // 
            // dgvRespon
            // 
            this.dgvRespon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRespon.Location = new System.Drawing.Point(15, 62);
            this.dgvRespon.Name = "dgvRespon";
            this.dgvRespon.Size = new System.Drawing.Size(636, 310);
            this.dgvRespon.TabIndex = 14;
            this.dgvRespon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRespon_CellClick);
            this.dgvRespon.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRespon_CellDoubleClick);
            // 
            // txtBuscaRespon
            // 
            this.txtBuscaRespon.Location = new System.Drawing.Point(15, 35);
            this.txtBuscaRespon.Name = "txtBuscaRespon";
            this.txtBuscaRespon.Size = new System.Drawing.Size(274, 20);
            this.txtBuscaRespon.TabIndex = 13;
            // 
            // lblBuscar
            // 
            this.lblBuscar.Location = new System.Drawing.Point(12, 9);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(277, 22);
            this.lblBuscar.TabIndex = 12;
            this.lblBuscar.Text = "Busqueda basada en la Descripción";
            // 
            // FAyudaRespoLocalida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(672, 423);
            this.Controls.Add(this.btnSalirRespon);
            this.Controls.Add(this.btnAceptarRespon);
            this.Controls.Add(this.btnBuscarRespon);
            this.Controls.Add(this.dgvRespon);
            this.Controls.Add(this.txtBuscaRespon);
            this.Controls.Add(this.lblBuscar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAyudaRespoLocalida";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.FAyudaRespoLocalida_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRespon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalirRespon;
        private System.Windows.Forms.Button btnAceptarRespon;
        private System.Windows.Forms.Button btnBuscarRespon;
        private System.Windows.Forms.DataGridView dgvRespon;
        private System.Windows.Forms.TextBox txtBuscaRespon;
        private System.Windows.Forms.Label lblBuscar;
    }
}