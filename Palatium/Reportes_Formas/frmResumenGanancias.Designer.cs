namespace Palatium.Reportes_Formas
{
    partial class frmResumenGanancias
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabDetallado = new System.Windows.Forms.TabPage();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbCobradas = new System.Windows.Forms.RadioButton();
            this.rdbTodos = new System.Windows.Forms.RadioButton();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.tabControlGanancias = new System.Windows.Forms.TabControl();
            this.tabResumido = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCantidadTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalValor = new System.Windows.Forms.TextBox();
            this.fecha_pedido_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_dscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal_neto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_otro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDetallado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.tabControlGanancias.SuspendLayout();
            this.tabResumido.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabDetallado
            // 
            this.tabDetallado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.tabDetallado.Controls.Add(this.dgvDetalle);
            this.tabDetallado.Location = new System.Drawing.Point(4, 22);
            this.tabDetallado.Name = "tabDetallado";
            this.tabDetallado.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetallado.Size = new System.Drawing.Size(726, 407);
            this.tabDetallado.TabIndex = 1;
            this.tabDetallado.Text = "Listado detallado de comandas";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeColumns = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pedido,
            this.fecha_pedido,
            this.cliente,
            this.subtotal,
            this.valor_dscto,
            this.subtotal_neto,
            this.valor_iva,
            this.valor_otro,
            this.valor});
            this.dgvDetalle.Location = new System.Drawing.Point(15, 16);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(697, 379);
            this.dgvDetalle.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.rdbCobradas);
            this.panel1.Controls.Add(this.rdbTodos);
            this.panel1.Controls.Add(this.cmbLocalidades);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnFiltrar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtFechaHasta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtFechaDesde);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 102);
            this.panel1.TabIndex = 204;
            // 
            // rdbCobradas
            // 
            this.rdbCobradas.AutoSize = true;
            this.rdbCobradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.rdbCobradas.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbCobradas.Location = new System.Drawing.Point(225, 67);
            this.rdbCobradas.Name = "rdbCobradas";
            this.rdbCobradas.Size = new System.Drawing.Size(153, 20);
            this.rdbCobradas.TabIndex = 207;
            this.rdbCobradas.Text = "Comandas cobradas";
            this.rdbCobradas.UseVisualStyleBackColor = true;
            // 
            // rdbTodos
            // 
            this.rdbTodos.AutoSize = true;
            this.rdbTodos.Checked = true;
            this.rdbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.rdbTodos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbTodos.Location = new System.Drawing.Point(225, 44);
            this.rdbTodos.Name = "rdbTodos";
            this.rdbTodos.Size = new System.Drawing.Size(154, 20);
            this.rdbTodos.TabIndex = 206;
            this.rdbTodos.TabStop = true;
            this.rdbTodos.Text = "Todas las comandas";
            this.rdbTodos.UseVisualStyleBackColor = true;
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(121, 12);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(246, 21);
            this.cmbLocalidades.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(16, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 205;
            this.label5.Text = "Localidad:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.AccessibleDescription = "";
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(567, 12);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(149, 74);
            this.btnLimpiar.TabIndex = 5;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.AccessibleDescription = "";
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnFiltrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(412, 12);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(149, 74);
            this.btnFiltrar.TabIndex = 4;
            this.btnFiltrar.Text = "REALIZAR LA BÚSQUEDA";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 197;
            this.label2.Text = "Fecha desde:";
            // 
            // dtFechaHasta
            // 
            this.dtFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaHasta.Location = new System.Drawing.Point(121, 66);
            this.dtFechaHasta.Name = "dtFechaHasta";
            this.dtFechaHasta.Size = new System.Drawing.Size(87, 20);
            this.dtFechaHasta.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(16, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 198;
            this.label3.Text = "Fecha hasta:";
            // 
            // dtFechaDesde
            // 
            this.dtFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaDesde.Location = new System.Drawing.Point(121, 40);
            this.dtFechaDesde.Name = "dtFechaDesde";
            this.dtFechaDesde.Size = new System.Drawing.Size(87, 20);
            this.dtFechaDesde.TabIndex = 2;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fecha_pedido_1,
            this.cantidad,
            this.total});
            this.dgvDatos.Location = new System.Drawing.Point(15, 16);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(697, 345);
            this.dgvDatos.TabIndex = 0;
            // 
            // tabControlGanancias
            // 
            this.tabControlGanancias.Controls.Add(this.tabResumido);
            this.tabControlGanancias.Controls.Add(this.tabDetallado);
            this.tabControlGanancias.Location = new System.Drawing.Point(0, 102);
            this.tabControlGanancias.Name = "tabControlGanancias";
            this.tabControlGanancias.SelectedIndex = 0;
            this.tabControlGanancias.Size = new System.Drawing.Size(734, 433);
            this.tabControlGanancias.TabIndex = 205;
            // 
            // tabResumido
            // 
            this.tabResumido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabResumido.Controls.Add(this.label4);
            this.tabResumido.Controls.Add(this.txtCantidadTotal);
            this.tabResumido.Controls.Add(this.label1);
            this.tabResumido.Controls.Add(this.txtTotalValor);
            this.tabResumido.Controls.Add(this.dgvDatos);
            this.tabResumido.Location = new System.Drawing.Point(4, 22);
            this.tabResumido.Name = "tabResumido";
            this.tabResumido.Padding = new System.Windows.Forms.Padding(3);
            this.tabResumido.Size = new System.Drawing.Size(726, 407);
            this.tabResumido.TabIndex = 0;
            this.tabResumido.Text = "Listado resumido de comandas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 375);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "CANTIDAD TOTAL:";
            // 
            // txtCantidadTotal
            // 
            this.txtCantidadTotal.Enabled = false;
            this.txtCantidadTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadTotal.Location = new System.Drawing.Point(188, 372);
            this.txtCantidadTotal.Name = "txtCantidadTotal";
            this.txtCantidadTotal.Size = new System.Drawing.Size(94, 26);
            this.txtCantidadTotal.TabIndex = 3;
            this.txtCantidadTotal.Text = "0";
            this.txtCantidadTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(497, 375);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "TOTAL:";
            // 
            // txtTotalValor
            // 
            this.txtTotalValor.Enabled = false;
            this.txtTotalValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalValor.Location = new System.Drawing.Point(570, 372);
            this.txtTotalValor.Name = "txtTotalValor";
            this.txtTotalValor.Size = new System.Drawing.Size(142, 26);
            this.txtTotalValor.TabIndex = 1;
            this.txtTotalValor.Text = "0.00";
            this.txtTotalValor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // fecha_pedido_1
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha_pedido_1.DefaultCellStyle = dataGridViewCellStyle8;
            this.fecha_pedido_1.HeaderText = "FECHA DE PEDIDOS";
            this.fecha_pedido_1.Name = "fecha_pedido_1";
            this.fecha_pedido_1.ReadOnly = true;
            this.fecha_pedido_1.Width = 250;
            // 
            // cantidad
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle9;
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 200;
            // 
            // total
            // 
            this.total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total.DefaultCellStyle = dataGridViewCellStyle10;
            this.total.HeaderText = "TOTAL";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // id_pedido
            // 
            this.id_pedido.HeaderText = "ID PEDIDO";
            this.id_pedido.Name = "id_pedido";
            this.id_pedido.ReadOnly = true;
            this.id_pedido.Visible = false;
            // 
            // fecha_pedido
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha_pedido.DefaultCellStyle = dataGridViewCellStyle1;
            this.fecha_pedido.HeaderText = "FECHA";
            this.fecha_pedido.Name = "fecha_pedido";
            this.fecha_pedido.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.HeaderText = "CLIENTE";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            this.cliente.Width = 250;
            // 
            // subtotal
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle2;
            this.subtotal.HeaderText = "SUBTOTAL";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            // 
            // valor_dscto
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_dscto.DefaultCellStyle = dataGridViewCellStyle3;
            this.valor_dscto.HeaderText = "DESCUENTO";
            this.valor_dscto.Name = "valor_dscto";
            this.valor_dscto.ReadOnly = true;
            // 
            // subtotal_neto
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.subtotal_neto.DefaultCellStyle = dataGridViewCellStyle4;
            this.subtotal_neto.HeaderText = "SUB. NETO";
            this.subtotal_neto.Name = "subtotal_neto";
            this.subtotal_neto.ReadOnly = true;
            // 
            // valor_iva
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_iva.DefaultCellStyle = dataGridViewCellStyle5;
            this.valor_iva.HeaderText = "IVA";
            this.valor_iva.Name = "valor_iva";
            this.valor_iva.ReadOnly = true;
            // 
            // valor_otro
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_otro.DefaultCellStyle = dataGridViewCellStyle6;
            this.valor_otro.HeaderText = "SERVICIO";
            this.valor_otro.Name = "valor_otro";
            this.valor_otro.ReadOnly = true;
            // 
            // valor
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle7;
            this.valor.HeaderText = "VALOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            // 
            // frmResumenGanancias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SpringGreen;
            this.ClientSize = new System.Drawing.Size(732, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControlGanancias);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResumenGanancias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmResumenGanancias";
            this.Load += new System.EventHandler(this.frmResumenGanancias_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmResumenGanancias_KeyDown);
            this.tabDetallado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.tabControlGanancias.ResumeLayout(false);
            this.tabResumido.ResumeLayout(false);
            this.tabResumido.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabDetallado;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFechaHasta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFechaDesde;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.TabControl tabControlGanancias;
        private System.Windows.Forms.TabPage tabResumido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCantidadTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalValor;
        private System.Windows.Forms.RadioButton rdbCobradas;
        private System.Windows.Forms.RadioButton rdbTodos;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_pedido_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_dscto;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal_neto;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_otro;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
    }
}