namespace Palatium.Reportes_Formas
{
    partial class frmEstadoCuentaRepExterno
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbRepartidores = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.id_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.establecimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.punto_emision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clave_acceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facturaelectronica = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.autorizado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fecha_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.base_iva_cero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.base_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.cmbRepartidores);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbLocalidades);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnFiltrar);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dtFechaHasta);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dtFechaDesde);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(732, 82);
            this.panel1.TabIndex = 204;
            // 
            // cmbRepartidores
            // 
            this.cmbRepartidores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRepartidores.FormattingEnabled = true;
            this.cmbRepartidores.Location = new System.Drawing.Point(115, 43);
            this.cmbRepartidores.Name = "cmbRepartidores";
            this.cmbRepartidores.Size = new System.Drawing.Size(224, 21);
            this.cmbRepartidores.TabIndex = 206;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 207;
            this.label1.Text = "Repartidor:";
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(115, 10);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(224, 21);
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
            // btnFiltrar
            // 
            this.btnFiltrar.AccessibleDescription = "";
            this.btnFiltrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnFiltrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(571, 12);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(149, 52);
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
            this.label2.Location = new System.Drawing.Point(354, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 197;
            this.label2.Text = "Fecha desde:";
            // 
            // dtFechaHasta
            // 
            this.dtFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaHasta.Location = new System.Drawing.Point(459, 41);
            this.dtFechaHasta.Name = "dtFechaHasta";
            this.dtFechaHasta.Size = new System.Drawing.Size(87, 20);
            this.dtFechaHasta.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(354, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 198;
            this.label3.Text = "Fecha hasta:";
            // 
            // dtFechaDesde
            // 
            this.dtFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaDesde.Location = new System.Drawing.Point(459, 15);
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
            this.id_factura,
            this.establecimiento,
            this.punto_emision,
            this.numero_factura,
            this.clave_acceso,
            this.documento,
            this.facturaelectronica,
            this.autorizado,
            this.fecha_factura,
            this.numero_pedido,
            this.factura,
            this.identificacion,
            this.cliente,
            this.base_iva_cero,
            this.base_iva,
            this.valor_descuento,
            this.valor_iva,
            this.valor_servicio,
            this.total,
            this.sri,
            this.diferencia,
            this.total_orden});
            this.dgvDatos.Location = new System.Drawing.Point(9, 88);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(715, 379);
            this.dgvDatos.TabIndex = 205;
            // 
            // btnExcel
            // 
            this.btnExcel.AccessibleDescription = "";
            this.btnExcel.BackColor = System.Drawing.Color.Green;
            this.btnExcel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnExcel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExcel.Location = new System.Drawing.Point(100, 476);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(91, 46);
            this.btnExcel.TabIndex = 208;
            this.btnExcel.Text = "EXCEL";
            this.btnExcel.UseVisualStyleBackColor = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.AccessibleDescription = "";
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(9, 476);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(91, 46);
            this.btnLimpiar.TabIndex = 207;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // id_factura
            // 
            this.id_factura.HeaderText = "ID";
            this.id_factura.Name = "id_factura";
            this.id_factura.ReadOnly = true;
            this.id_factura.Visible = false;
            // 
            // establecimiento
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.establecimiento.DefaultCellStyle = dataGridViewCellStyle1;
            this.establecimiento.HeaderText = "ESTABLECIMIENTO";
            this.establecimiento.Name = "establecimiento";
            this.establecimiento.ReadOnly = true;
            this.establecimiento.Visible = false;
            // 
            // punto_emision
            // 
            this.punto_emision.HeaderText = "PUNTO DE EMISION";
            this.punto_emision.Name = "punto_emision";
            this.punto_emision.ReadOnly = true;
            this.punto_emision.Visible = false;
            this.punto_emision.Width = 120;
            // 
            // numero_factura
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numero_factura.DefaultCellStyle = dataGridViewCellStyle2;
            this.numero_factura.HeaderText = "NUMERO_FACTURA";
            this.numero_factura.Name = "numero_factura";
            this.numero_factura.ReadOnly = true;
            this.numero_factura.Visible = false;
            this.numero_factura.Width = 75;
            // 
            // clave_acceso
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clave_acceso.DefaultCellStyle = dataGridViewCellStyle3;
            this.clave_acceso.HeaderText = "CLAVE ACCESO";
            this.clave_acceso.Name = "clave_acceso";
            this.clave_acceso.ReadOnly = true;
            this.clave_acceso.Visible = false;
            // 
            // documento
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.documento.DefaultCellStyle = dataGridViewCellStyle4;
            this.documento.HeaderText = "DOC";
            this.documento.Name = "documento";
            this.documento.ReadOnly = true;
            this.documento.Visible = false;
            this.documento.Width = 75;
            // 
            // facturaelectronica
            // 
            this.facturaelectronica.HeaderText = "F.E.";
            this.facturaelectronica.Name = "facturaelectronica";
            this.facturaelectronica.ReadOnly = true;
            this.facturaelectronica.Visible = false;
            this.facturaelectronica.Width = 40;
            // 
            // autorizado
            // 
            this.autorizado.HeaderText = "AUT.";
            this.autorizado.Name = "autorizado";
            this.autorizado.ReadOnly = true;
            this.autorizado.Visible = false;
            this.autorizado.Width = 40;
            // 
            // fecha_factura
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha_factura.DefaultCellStyle = dataGridViewCellStyle5;
            this.fecha_factura.HeaderText = "FECHA FACTURA";
            this.fecha_factura.Name = "fecha_factura";
            this.fecha_factura.ReadOnly = true;
            this.fecha_factura.Width = 130;
            // 
            // numero_pedido
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numero_pedido.DefaultCellStyle = dataGridViewCellStyle6;
            this.numero_pedido.HeaderText = "N° PEDIDO";
            this.numero_pedido.Name = "numero_pedido";
            this.numero_pedido.ReadOnly = true;
            this.numero_pedido.Width = 90;
            // 
            // factura
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.factura.DefaultCellStyle = dataGridViewCellStyle7;
            this.factura.HeaderText = "N° FACTURA";
            this.factura.Name = "factura";
            this.factura.ReadOnly = true;
            this.factura.Width = 120;
            // 
            // identificacion
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identificacion.DefaultCellStyle = dataGridViewCellStyle8;
            this.identificacion.HeaderText = "IDENTIFICACIÓN";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            this.identificacion.Width = 120;
            // 
            // cliente
            // 
            this.cliente.HeaderText = "CLIENTE";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            this.cliente.Width = 200;
            // 
            // base_iva_cero
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.base_iva_cero.DefaultCellStyle = dataGridViewCellStyle9;
            this.base_iva_cero.HeaderText = "BASE CERO";
            this.base_iva_cero.Name = "base_iva_cero";
            this.base_iva_cero.ReadOnly = true;
            // 
            // base_iva
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.base_iva.DefaultCellStyle = dataGridViewCellStyle10;
            this.base_iva.HeaderText = "BASE IVA";
            this.base_iva.Name = "base_iva";
            this.base_iva.ReadOnly = true;
            // 
            // valor_descuento
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_descuento.DefaultCellStyle = dataGridViewCellStyle11;
            this.valor_descuento.HeaderText = "DESCUENTO";
            this.valor_descuento.Name = "valor_descuento";
            this.valor_descuento.ReadOnly = true;
            // 
            // valor_iva
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_iva.DefaultCellStyle = dataGridViewCellStyle12;
            this.valor_iva.HeaderText = "IVA";
            this.valor_iva.Name = "valor_iva";
            this.valor_iva.ReadOnly = true;
            // 
            // valor_servicio
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_servicio.DefaultCellStyle = dataGridViewCellStyle13;
            this.valor_servicio.HeaderText = "SERVICIO";
            this.valor_servicio.Name = "valor_servicio";
            this.valor_servicio.ReadOnly = true;
            // 
            // total
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total.DefaultCellStyle = dataGridViewCellStyle14;
            this.total.HeaderText = "TOTAL";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // sri
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.sri.DefaultCellStyle = dataGridViewCellStyle15;
            this.sri.HeaderText = "SRI";
            this.sri.Name = "sri";
            this.sri.ReadOnly = true;
            this.sri.Visible = false;
            // 
            // diferencia
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.diferencia.DefaultCellStyle = dataGridViewCellStyle16;
            this.diferencia.HeaderText = "DIFERENCIA";
            this.diferencia.Name = "diferencia";
            this.diferencia.ReadOnly = true;
            this.diferencia.Visible = false;
            // 
            // total_orden
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total_orden.DefaultCellStyle = dataGridViewCellStyle17;
            this.total_orden.HeaderText = "TOTAL ORDEN";
            this.total_orden.Name = "total_orden";
            this.total_orden.ReadOnly = true;
            this.total_orden.Visible = false;
            this.total_orden.Width = 120;
            // 
            // frmEstadoCuentaRepExterno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SpringGreen;
            this.ClientSize = new System.Drawing.Size(732, 531);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEstadoCuentaRepExterno";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Estado de Cuenta Repartidorex Externos";
            this.Load += new System.EventHandler(this.frmEstadoCuentaRepExterno_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEstadoCuentaRepExterno_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbRepartidores;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFechaHasta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFechaDesde;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn establecimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn punto_emision;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero_factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn clave_acceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn documento;
        private System.Windows.Forms.DataGridViewCheckBoxColumn facturaelectronica;
        private System.Windows.Forms.DataGridViewCheckBoxColumn autorizado;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero_pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn base_iva_cero;
        private System.Windows.Forms.DataGridViewTextBoxColumn base_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn sri;
        private System.Windows.Forms.DataGridViewTextBoxColumn diferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn total_orden;
    }
}