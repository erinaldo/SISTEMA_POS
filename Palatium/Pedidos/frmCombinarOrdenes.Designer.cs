namespace Palatium.Pedidos
{
    partial class frmCombinarOrdenes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCombinar = new System.Windows.Forms.Button();
            this.pnlCombinar = new System.Windows.Forms.Panel();
            this.txtMesero = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.dgvPedido = new System.Windows.Forms.DataGridView();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valuni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCantidadDebida = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_numeromesa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Navy;
            this.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSalir.FlatAppearance.BorderSize = 2;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Location = new System.Drawing.Point(854, 524);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(140, 61);
            this.btnSalir.TabIndex = 76;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnSalir_MouseEnter);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            // 
            // btnCombinar
            // 
            this.btnCombinar.BackColor = System.Drawing.Color.Navy;
            this.btnCombinar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCombinar.FlatAppearance.BorderSize = 2;
            this.btnCombinar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCombinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCombinar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCombinar.Location = new System.Drawing.Point(1016, 524);
            this.btnCombinar.Name = "btnCombinar";
            this.btnCombinar.Size = new System.Drawing.Size(140, 61);
            this.btnCombinar.TabIndex = 77;
            this.btnCombinar.Text = "Combinar";
            this.btnCombinar.UseVisualStyleBackColor = false;
            this.btnCombinar.Click += new System.EventHandler(this.btnCombinar_Click);
            this.btnCombinar.MouseEnter += new System.EventHandler(this.btnCombinar_MouseEnter);
            this.btnCombinar.MouseLeave += new System.EventHandler(this.btnCombinar_MouseLeave);
            // 
            // pnlCombinar
            // 
            this.pnlCombinar.AutoScroll = true;
            this.pnlCombinar.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCombinar.Location = new System.Drawing.Point(24, 15);
            this.pnlCombinar.Name = "pnlCombinar";
            this.pnlCombinar.Size = new System.Drawing.Size(760, 478);
            this.pnlCombinar.TabIndex = 99;
            // 
            // txtMesero
            // 
            this.txtMesero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMesero.Location = new System.Drawing.Point(1016, 23);
            this.txtMesero.Name = "txtMesero";
            this.txtMesero.ReadOnly = true;
            this.txtMesero.Size = new System.Drawing.Size(109, 21);
            this.txtMesero.TabIndex = 110;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(1016, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 107;
            this.label3.Text = "MESERO:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(874, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 106;
            this.label2.Text = "# DE ORDEN:";
            // 
            // txtOrden
            // 
            this.txtOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrden.Location = new System.Drawing.Point(874, 23);
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.ReadOnly = true;
            this.txtOrden.Size = new System.Drawing.Size(109, 21);
            this.txtOrden.TabIndex = 109;
            // 
            // dgvPedido
            // 
            this.dgvPedido.AllowUserToAddRows = false;
            this.dgvPedido.AllowUserToDeleteRows = false;
            this.dgvPedido.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPedido.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPedido.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPedido.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cantidad,
            this.producto,
            this.valuni,
            this.valor});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPedido.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPedido.EnableHeadersVisualStyles = false;
            this.dgvPedido.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvPedido.Location = new System.Drawing.Point(854, 99);
            this.dgvPedido.Name = "dgvPedido";
            this.dgvPedido.ReadOnly = true;
            this.dgvPedido.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPedido.RowHeadersVisible = false;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPedido.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedido.Size = new System.Drawing.Size(303, 331);
            this.dgvPedido.TabIndex = 108;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.FillWeight = 60.9137F;
            this.cantidad.HeaderText = "CANT.";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 53;
            // 
            // producto
            // 
            this.producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.producto.FillWeight = 168.8291F;
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // valuni
            // 
            this.valuni.HeaderText = "V. UNITARIO";
            this.valuni.Name = "valuni";
            this.valuni.ReadOnly = true;
            this.valuni.Visible = false;
            // 
            // valor
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle2;
            this.valor.FillWeight = 70.25717F;
            this.valor.HeaderText = "VALOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 62;
            // 
            // lblCantidadDebida
            // 
            this.lblCantidadDebida.AutoSize = true;
            this.lblCantidadDebida.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadDebida.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblCantidadDebida.Location = new System.Drawing.Point(1016, 456);
            this.lblCantidadDebida.Name = "lblCantidadDebida";
            this.lblCantidadDebida.Size = new System.Drawing.Size(95, 31);
            this.lblCantidadDebida.TabIndex = 112;
            this.lblCantidadDebida.Text = "$ 0.00";
            this.lblCantidadDebida.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label10.Location = new System.Drawing.Point(898, 452);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 40);
            this.label10.TabIndex = 111;
            this.label10.Text = "CANTIDAD DEBIDA:";
            // 
            // txt_numeromesa
            // 
            this.txt_numeromesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_numeromesa.Location = new System.Drawing.Point(874, 65);
            this.txt_numeromesa.Name = "txt_numeromesa";
            this.txt_numeromesa.ReadOnly = true;
            this.txt_numeromesa.Size = new System.Drawing.Size(109, 21);
            this.txt_numeromesa.TabIndex = 114;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(874, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 113;
            this.label7.Text = "# DE MESA:";
            // 
            // frmCombinarOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1169, 597);
            this.Controls.Add(this.txt_numeromesa);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblCantidadDebida);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMesero);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrden);
            this.Controls.Add(this.dgvPedido);
            this.Controls.Add(this.pnlCombinar);
            this.Controls.Add(this.btnCombinar);
            this.Controls.Add(this.btnSalir);
            this.Name = "frmCombinarOrdenes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Combinar Orden";
            this.Load += new System.EventHandler(this.frmCombinarOrdenes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCombinar;
        private System.Windows.Forms.Panel pnlCombinar;
        public System.Windows.Forms.TextBox txtMesero;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtOrden;
        public System.Windows.Forms.DataGridView dgvPedido;
        private System.Windows.Forms.Label lblCantidadDebida;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn valuni;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        public System.Windows.Forms.TextBox txt_numeromesa;
        private System.Windows.Forms.Label label7;
    }
}