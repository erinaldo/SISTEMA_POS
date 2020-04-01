namespace Palatium.Pedidos
{
    partial class frmNuevaComanda
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
            this.dgvComanda = new System.Windows.Forms.DataGridView();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComanda)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvComanda
            // 
            this.dgvComanda.AllowUserToOrderColumns = true;
            this.dgvComanda.AllowUserToResizeColumns = false;
            this.dgvComanda.AllowUserToResizeRows = false;
            this.dgvComanda.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvComanda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComanda.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cantidad,
            this.descripcion,
            this.total,
            this.unitario,
            this.paga_iva});
            this.dgvComanda.Location = new System.Drawing.Point(12, 67);
            this.dgvComanda.Name = "dgvComanda";
            this.dgvComanda.RowHeadersVisible = false;
            this.dgvComanda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComanda.Size = new System.Drawing.Size(433, 150);
            this.dgvComanda.TabIndex = 0;
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "CANT.";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 50;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "DESCRIPCIÓN";
            this.descripcion.Name = "descripcion";
            this.descripcion.Width = 250;
            // 
            // total
            // 
            this.total.HeaderText = "TOTAL";
            this.total.Name = "total";
            // 
            // unitario
            // 
            this.unitario.HeaderText = "UNITARIO";
            this.unitario.Name = "unitario";
            this.unitario.Visible = false;
            // 
            // paga_iva
            // 
            this.paga_iva.HeaderText = "PAGA IVA";
            this.paga_iva.Name = "paga_iva";
            this.paga_iva.Visible = false;
            // 
            // frmNuevaComanda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.dgvComanda);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNuevaComanda";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MÓDULO  DE COMANDAS";
            ((System.ComponentModel.ISupportInitialize)(this.dgvComanda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvComanda;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn paga_iva;
    }
}