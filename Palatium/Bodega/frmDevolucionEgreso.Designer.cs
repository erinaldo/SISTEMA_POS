namespace Palatium.Bodega
{
    partial class frmDevolucionEgreso
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
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtIva1 = new System.Windows.Forms.TextBox();
            this.lblIva1 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.btnA = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.punto = new System.Windows.Forms.DataGridViewButtonColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlFecha = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboDatos2 = new MisControles.ComboDatos();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.lblComentarios = new System.Windows.Forms.Label();
            this.comboDatos1 = new MisControles.ComboDatos();
            this.txtNombrePersona = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPersona = new System.Windows.Forms.TextBox();
            this.lblPersona = new System.Windows.Forms.Label();
            this.txtNombreProveedor = new System.Windows.Forms.TextBox();
            this.btnAyudaProveedor = new System.Windows.Forms.Button();
            this.txtProveedor = new System.Windows.Forms.TextBox();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblPorcentajeIva = new System.Windows.Forms.Label();
            this.txtProcentajeIva = new System.Windows.Forms.TextBox();
            this.lblFacturaCompra = new System.Windows.Forms.Label();
            this.txtFacturaCompra = new System.Windows.Forms.TextBox();
            this.lblNotaDeCredito = new System.Windows.Forms.Label();
            this.txtNotaCredito = new System.Windows.Forms.TextBox();
            this.cmbMotivos = new MisControles.ComboDatos();
            this.lblReferencias = new System.Windows.Forms.Label();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.txtNumeroMovimiento = new System.Windows.Forms.TextBox();
            this.txtFechaAplicacion = new System.Windows.Forms.TextBox();
            this.txtNumeros = new System.Windows.Forms.TextBox();
            this.btnAyudaIngresoNumeros = new System.Windows.Forms.Button();
            this.lblNumeroMovimiento = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.cmbBodega = new MisControles.ComboDatos();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.pnlFecha.SuspendLayout();
            this.pnlEmpresa.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(365, 402);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(66, 23);
            this.btnImprimir.TabIndex = 103;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(437, 402);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 102;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnAnular
            // 
            this.btnAnular.Location = new System.Drawing.Point(503, 402);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(60, 23);
            this.btnAnular.TabIndex = 101;
            this.btnAnular.Text = "Anular";
            this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(569, 402);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(60, 23);
            this.btnGrabar.TabIndex = 100;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(635, 402);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 23);
            this.btnSalir.TabIndex = 99;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIva1);
            this.groupBox1.Controls.Add(this.lblIva1);
            this.groupBox1.Controls.Add(this.txtSubtotal);
            this.groupBox1.Controls.Add(this.lblSubtotal);
            this.groupBox1.Controls.Add(this.txtValorTotal);
            this.groupBox1.Controls.Add(this.lblValorTotal);
            this.groupBox1.Controls.Add(this.btnA);
            this.groupBox1.Controls.Add(this.btnX);
            this.groupBox1.Controls.Add(this.dgvDetalleVenta);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 201);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            // 
            // txtIva1
            // 
            this.txtIva1.BackColor = System.Drawing.SystemColors.Window;
            this.txtIva1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIva1.Location = new System.Drawing.Point(465, 174);
            this.txtIva1.Name = "txtIva1";
            this.txtIva1.Size = new System.Drawing.Size(66, 20);
            this.txtIva1.TabIndex = 95;
            this.txtIva1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIva1
            // 
            this.lblIva1.AutoSize = true;
            this.lblIva1.Location = new System.Drawing.Point(431, 177);
            this.lblIva1.Name = "lblIva1";
            this.lblIva1.Size = new System.Drawing.Size(27, 13);
            this.lblIva1.TabIndex = 94;
            this.lblIva1.Text = "IVA:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.BackColor = System.Drawing.SystemColors.Window;
            this.txtSubtotal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSubtotal.Location = new System.Drawing.Point(370, 174);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(53, 20);
            this.txtSubtotal.TabIndex = 92;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(305, 177);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(49, 13);
            this.lblSubtotal.TabIndex = 93;
            this.lblSubtotal.Text = "Subtotal:";
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.SystemColors.Window;
            this.txtValorTotal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValorTotal.Location = new System.Drawing.Point(604, 174);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Size = new System.Drawing.Size(73, 20);
            this.txtValorTotal.TabIndex = 91;
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Location = new System.Drawing.Point(540, 177);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(58, 13);
            this.lblValorTotal.TabIndex = 90;
            this.lblValorTotal.Text = "Valor Total";
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(37, 172);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(23, 23);
            this.btnA.TabIndex = 66;
            this.btnA.Text = "A";
            this.btnA.UseVisualStyleBackColor = true;
            // 
            // btnX
            // 
            this.btnX.Location = new System.Drawing.Point(12, 172);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(23, 23);
            this.btnX.TabIndex = 65;
            this.btnX.Text = "X";
            this.btnX.UseVisualStyleBackColor = true;
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.AllowUserToAddRows = false;
            this.dgvDetalleVenta.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoProducto,
            this.punto,
            this.descripcionProducto,
            this.unidad,
            this.cantidad,
            this.precioUnitario,
            this.subtotal,
            this.stock,
            this.idProducto});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(7, 20);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(686, 150);
            this.dgvDetalleVenta.TabIndex = 52;
            this.dgvDetalleVenta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellContentClick);
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Código Producto";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 115;
            // 
            // punto
            // 
            this.punto.HeaderText = ".";
            this.punto.Name = "punto";
            this.punto.Text = "?";
            this.punto.Width = 20;
            // 
            // descripcionProducto
            // 
            this.descripcionProducto.HeaderText = "Descripción del Producto";
            this.descripcionProducto.Name = "descripcionProducto";
            this.descripcionProducto.Width = 155;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Unidad";
            this.unidad.Name = "unidad";
            this.unidad.Width = 30;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle2;
            this.precioUnitario.HeaderText = "Precio Uni.";
            this.precioUnitario.Name = "precioUnitario";
            this.precioUnitario.Width = 90;
            // 
            // subtotal
            // 
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            this.subtotal.Width = 70;
            // 
            // stock
            // 
            this.stock.HeaderText = "stock";
            this.stock.Name = "stock";
            this.stock.Width = 70;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "ID PRODUCTO";
            this.idProducto.Name = "idProducto";
            this.idProducto.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(909, 231);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(75, 21);
            this.textBox3.TabIndex = 51;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(906, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 50;
            this.label1.Text = "Total a Pagar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(123, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 40;
            this.label2.Text = "Valor:";
            // 
            // pnlFecha
            // 
            this.pnlFecha.Controls.Add(this.textBox1);
            this.pnlFecha.Controls.Add(this.comboDatos2);
            this.pnlFecha.Controls.Add(this.txtComentarios);
            this.pnlFecha.Controls.Add(this.lblComentarios);
            this.pnlFecha.Controls.Add(this.comboDatos1);
            this.pnlFecha.Controls.Add(this.txtNombrePersona);
            this.pnlFecha.Controls.Add(this.button1);
            this.pnlFecha.Controls.Add(this.txtPersona);
            this.pnlFecha.Controls.Add(this.lblPersona);
            this.pnlFecha.Controls.Add(this.txtNombreProveedor);
            this.pnlFecha.Controls.Add(this.btnAyudaProveedor);
            this.pnlFecha.Controls.Add(this.txtProveedor);
            this.pnlFecha.Controls.Add(this.lblProveedor);
            this.pnlFecha.Controls.Add(this.btnOk);
            this.pnlFecha.Controls.Add(this.lblPorcentajeIva);
            this.pnlFecha.Controls.Add(this.txtProcentajeIva);
            this.pnlFecha.Controls.Add(this.lblFacturaCompra);
            this.pnlFecha.Controls.Add(this.txtFacturaCompra);
            this.pnlFecha.Controls.Add(this.lblNotaDeCredito);
            this.pnlFecha.Controls.Add(this.txtNotaCredito);
            this.pnlFecha.Controls.Add(this.cmbMotivos);
            this.pnlFecha.Controls.Add(this.lblReferencias);
            this.pnlFecha.Controls.Add(this.lblMotivo);
            this.pnlFecha.Controls.Add(this.txtReferencia);
            this.pnlFecha.Location = new System.Drawing.Point(3, 52);
            this.pnlFecha.Name = "pnlFecha";
            this.pnlFecha.Size = new System.Drawing.Size(700, 140);
            this.pnlFecha.TabIndex = 97;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(410, -22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 20);
            this.textBox1.TabIndex = 64;
            // 
            // comboDatos2
            // 
            this.comboDatos2.Enabled = false;
            this.comboDatos2.FormattingEnabled = true;
            this.comboDatos2.Location = new System.Drawing.Point(266, -22);
            this.comboDatos2.Name = "comboDatos2";
            this.comboDatos2.Size = new System.Drawing.Size(138, 21);
            this.comboDatos2.TabIndex = 57;
            // 
            // txtComentarios
            // 
            this.txtComentarios.BackColor = System.Drawing.SystemColors.Window;
            this.txtComentarios.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtComentarios.Location = new System.Drawing.Point(6, 98);
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(409, 20);
            this.txtComentarios.TabIndex = 76;
            // 
            // lblComentarios
            // 
            this.lblComentarios.AutoSize = true;
            this.lblComentarios.Location = new System.Drawing.Point(9, 82);
            this.lblComentarios.Name = "lblComentarios";
            this.lblComentarios.Size = new System.Drawing.Size(65, 13);
            this.lblComentarios.TabIndex = 75;
            this.lblComentarios.Text = "Comentarios";
            // 
            // comboDatos1
            // 
            this.comboDatos1.FormattingEnabled = true;
            this.comboDatos1.Location = new System.Drawing.Point(131, -22);
            this.comboDatos1.Name = "comboDatos1";
            this.comboDatos1.Size = new System.Drawing.Size(129, 21);
            this.comboDatos1.TabIndex = 48;
            // 
            // txtNombrePersona
            // 
            this.txtNombrePersona.Location = new System.Drawing.Point(477, 57);
            this.txtNombrePersona.Name = "txtNombrePersona";
            this.txtNombrePersona.ReadOnly = true;
            this.txtNombrePersona.Size = new System.Drawing.Size(198, 20);
            this.txtNombrePersona.TabIndex = 72;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(451, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 71;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // txtPersona
            // 
            this.txtPersona.BackColor = System.Drawing.SystemColors.Window;
            this.txtPersona.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPersona.Location = new System.Drawing.Point(330, 57);
            this.txtPersona.Name = "txtPersona";
            this.txtPersona.Size = new System.Drawing.Size(115, 20);
            this.txtPersona.TabIndex = 74;
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Location = new System.Drawing.Point(332, 43);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(46, 13);
            this.lblPersona.TabIndex = 73;
            this.lblPersona.Text = "Persona";
            // 
            // txtNombreProveedor
            // 
            this.txtNombreProveedor.Location = new System.Drawing.Point(126, 59);
            this.txtNombreProveedor.Name = "txtNombreProveedor";
            this.txtNombreProveedor.ReadOnly = true;
            this.txtNombreProveedor.Size = new System.Drawing.Size(198, 20);
            this.txtNombreProveedor.TabIndex = 65;
            // 
            // btnAyudaProveedor
            // 
            this.btnAyudaProveedor.Location = new System.Drawing.Point(97, 57);
            this.btnAyudaProveedor.Name = "btnAyudaProveedor";
            this.btnAyudaProveedor.Size = new System.Drawing.Size(23, 23);
            this.btnAyudaProveedor.TabIndex = 65;
            this.btnAyudaProveedor.Text = "?";
            this.btnAyudaProveedor.UseVisualStyleBackColor = true;
            // 
            // txtProveedor
            // 
            this.txtProveedor.BackColor = System.Drawing.SystemColors.Window;
            this.txtProveedor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtProveedor.Location = new System.Drawing.Point(6, 59);
            this.txtProveedor.Name = "txtProveedor";
            this.txtProveedor.Size = new System.Drawing.Size(90, 20);
            this.txtProveedor.TabIndex = 70;
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(9, 43);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(56, 13);
            this.lblProveedor.TabIndex = 69;
            this.lblProveedor.Text = "Proveedor";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(620, 16);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 62;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblPorcentajeIva
            // 
            this.lblPorcentajeIva.AutoSize = true;
            this.lblPorcentajeIva.Location = new System.Drawing.Point(440, 6);
            this.lblPorcentajeIva.Name = "lblPorcentajeIva";
            this.lblPorcentajeIva.Size = new System.Drawing.Size(76, 13);
            this.lblPorcentajeIva.TabIndex = 68;
            this.lblPorcentajeIva.Text = "Porcentaje Iva";
            // 
            // txtProcentajeIva
            // 
            this.txtProcentajeIva.BackColor = System.Drawing.SystemColors.Window;
            this.txtProcentajeIva.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtProcentajeIva.Location = new System.Drawing.Point(443, 19);
            this.txtProcentajeIva.Name = "txtProcentajeIva";
            this.txtProcentajeIva.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtProcentajeIva.Size = new System.Drawing.Size(88, 20);
            this.txtProcentajeIva.TabIndex = 67;
            // 
            // lblFacturaCompra
            // 
            this.lblFacturaCompra.AutoSize = true;
            this.lblFacturaCompra.Location = new System.Drawing.Point(337, 6);
            this.lblFacturaCompra.Name = "lblFacturaCompra";
            this.lblFacturaCompra.Size = new System.Drawing.Size(82, 13);
            this.lblFacturaCompra.TabIndex = 66;
            this.lblFacturaCompra.Text = "Factura Compra";
            // 
            // txtFacturaCompra
            // 
            this.txtFacturaCompra.BackColor = System.Drawing.SystemColors.Window;
            this.txtFacturaCompra.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFacturaCompra.Location = new System.Drawing.Point(340, 19);
            this.txtFacturaCompra.Name = "txtFacturaCompra";
            this.txtFacturaCompra.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtFacturaCompra.Size = new System.Drawing.Size(88, 20);
            this.txtFacturaCompra.TabIndex = 65;
            // 
            // lblNotaDeCredito
            // 
            this.lblNotaDeCredito.AutoSize = true;
            this.lblNotaDeCredito.Location = new System.Drawing.Point(241, 6);
            this.lblNotaDeCredito.Name = "lblNotaDeCredito";
            this.lblNotaDeCredito.Size = new System.Drawing.Size(81, 13);
            this.lblNotaDeCredito.TabIndex = 64;
            this.lblNotaDeCredito.Text = "Nota de Crédito";
            // 
            // txtNotaCredito
            // 
            this.txtNotaCredito.BackColor = System.Drawing.SystemColors.Window;
            this.txtNotaCredito.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNotaCredito.Location = new System.Drawing.Point(244, 19);
            this.txtNotaCredito.Name = "txtNotaCredito";
            this.txtNotaCredito.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNotaCredito.Size = new System.Drawing.Size(88, 20);
            this.txtNotaCredito.TabIndex = 51;
            // 
            // cmbMotivos
            // 
            this.cmbMotivos.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMotivos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbMotivos.FormattingEnabled = true;
            this.cmbMotivos.Location = new System.Drawing.Point(6, 19);
            this.cmbMotivos.Name = "cmbMotivos";
            this.cmbMotivos.Size = new System.Drawing.Size(129, 21);
            this.cmbMotivos.TabIndex = 61;
            // 
            // lblReferencias
            // 
            this.lblReferencias.AutoSize = true;
            this.lblReferencias.Location = new System.Drawing.Point(143, 6);
            this.lblReferencias.Name = "lblReferencias";
            this.lblReferencias.Size = new System.Drawing.Size(96, 13);
            this.lblReferencias.TabIndex = 49;
            this.lblReferencias.Text = "Referencia/Import.";
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(9, 6);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(39, 13);
            this.lblMotivo.TabIndex = 48;
            this.lblMotivo.Text = "Motivo";
            // 
            // txtReferencia
            // 
            this.txtReferencia.BackColor = System.Drawing.SystemColors.Window;
            this.txtReferencia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtReferencia.Location = new System.Drawing.Point(146, 19);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(90, 20);
            this.txtReferencia.TabIndex = 48;
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.Controls.Add(this.txtNumeroMovimiento);
            this.pnlEmpresa.Controls.Add(this.txtFechaAplicacion);
            this.pnlEmpresa.Controls.Add(this.txtNumeros);
            this.pnlEmpresa.Controls.Add(this.btnAyudaIngresoNumeros);
            this.pnlEmpresa.Controls.Add(this.lblNumeroMovimiento);
            this.pnlEmpresa.Controls.Add(this.lblFecha);
            this.pnlEmpresa.Controls.Add(this.cmbBodega);
            this.pnlEmpresa.Controls.Add(this.lblBodega);
            this.pnlEmpresa.Controls.Add(this.cmbOficina);
            this.pnlEmpresa.Controls.Add(this.lblOficina);
            this.pnlEmpresa.Location = new System.Drawing.Point(3, 8);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(700, 46);
            this.pnlEmpresa.TabIndex = 96;
            // 
            // txtNumeroMovimiento
            // 
            this.txtNumeroMovimiento.BackColor = System.Drawing.SystemColors.Window;
            this.txtNumeroMovimiento.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNumeroMovimiento.Location = new System.Drawing.Point(285, 18);
            this.txtNumeroMovimiento.Name = "txtNumeroMovimiento";
            this.txtNumeroMovimiento.Size = new System.Drawing.Size(109, 20);
            this.txtNumeroMovimiento.TabIndex = 64;
            // 
            // txtFechaAplicacion
            // 
            this.txtFechaAplicacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaAplicacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaAplicacion.Location = new System.Drawing.Point(620, 18);
            this.txtFechaAplicacion.Name = "txtFechaAplicacion";
            this.txtFechaAplicacion.Size = new System.Drawing.Size(77, 20);
            this.txtFechaAplicacion.TabIndex = 60;
            // 
            // txtNumeros
            // 
            this.txtNumeros.Location = new System.Drawing.Point(429, 18);
            this.txtNumeros.Name = "txtNumeros";
            this.txtNumeros.ReadOnly = true;
            this.txtNumeros.Size = new System.Drawing.Size(181, 20);
            this.txtNumeros.TabIndex = 49;
            // 
            // btnAyudaIngresoNumeros
            // 
            this.btnAyudaIngresoNumeros.Location = new System.Drawing.Point(400, 16);
            this.btnAyudaIngresoNumeros.Name = "btnAyudaIngresoNumeros";
            this.btnAyudaIngresoNumeros.Size = new System.Drawing.Size(23, 23);
            this.btnAyudaIngresoNumeros.TabIndex = 49;
            this.btnAyudaIngresoNumeros.Text = "?";
            this.btnAyudaIngresoNumeros.UseVisualStyleBackColor = true;
            // 
            // lblNumeroMovimiento
            // 
            this.lblNumeroMovimiento.AutoSize = true;
            this.lblNumeroMovimiento.Location = new System.Drawing.Point(282, 2);
            this.lblNumeroMovimiento.Name = "lblNumeroMovimiento";
            this.lblNumeroMovimiento.Size = new System.Drawing.Size(81, 13);
            this.lblNumeroMovimiento.TabIndex = 59;
            this.lblNumeroMovimiento.Text = "No. Movimiento";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(646, 4);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 55;
            this.lblFecha.Text = "Fecha";
            // 
            // cmbBodega
            // 
            this.cmbBodega.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBodega.Enabled = false;
            this.cmbBodega.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(141, 18);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(138, 21);
            this.cmbBodega.TabIndex = 57;
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(138, 2);
            this.lblBodega.Name = "lblBodega";
            this.lblBodega.Size = new System.Drawing.Size(44, 13);
            this.lblBodega.TabIndex = 56;
            this.lblBodega.Text = "Bodega";
            // 
            // cmbOficina
            // 
            this.cmbOficina.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOficina.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOficina.FormattingEnabled = true;
            this.cmbOficina.Location = new System.Drawing.Point(6, 18);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(129, 21);
            this.cmbOficina.TabIndex = 48;
            this.cmbOficina.SelectedIndexChanged += new System.EventHandler(this.cmbOficina_SelectedIndexChanged);
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Location = new System.Drawing.Point(3, 4);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(71, 13);
            this.lblOficina.TabIndex = 55;
            this.lblOficina.Text = "Oficina/Local";
            // 
            // frmDevolucionEgreso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 434);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlFecha);
            this.Controls.Add(this.pnlEmpresa);
            this.Name = "frmDevolucionEgreso";
            this.Text = "frmDevolucionEgreso";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.pnlFecha.ResumeLayout(false);
            this.pnlFecha.PerformLayout();
            this.pnlEmpresa.ResumeLayout(false);
            this.pnlEmpresa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlFecha;
        private System.Windows.Forms.TextBox textBox1;
        private MisControles.ComboDatos comboDatos2;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label lblComentarios;
        private MisControles.ComboDatos comboDatos1;
        private System.Windows.Forms.TextBox txtNombrePersona;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPersona;
        private System.Windows.Forms.Label lblPersona;
        private System.Windows.Forms.TextBox txtNombreProveedor;
        private System.Windows.Forms.Button btnAyudaProveedor;
        private System.Windows.Forms.TextBox txtProveedor;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblPorcentajeIva;
        private System.Windows.Forms.TextBox txtProcentajeIva;
        private System.Windows.Forms.Label lblFacturaCompra;
        private System.Windows.Forms.TextBox txtFacturaCompra;
        private System.Windows.Forms.Label lblNotaDeCredito;
        private System.Windows.Forms.TextBox txtNotaCredito;
        private MisControles.ComboDatos cmbMotivos;
        private System.Windows.Forms.Label lblReferencias;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Panel pnlEmpresa;
        private System.Windows.Forms.TextBox txtNumeroMovimiento;
        private System.Windows.Forms.TextBox txtFechaAplicacion;
        private System.Windows.Forms.TextBox txtNumeros;
        private System.Windows.Forms.Button btnAyudaIngresoNumeros;
        private System.Windows.Forms.Label lblNumeroMovimiento;
        private System.Windows.Forms.Label lblFecha;
        private MisControles.ComboDatos cmbBodega;
        private System.Windows.Forms.Label lblBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblOficina;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewButtonColumn punto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.TextBox txtIva1;
        private System.Windows.Forms.Label lblIva1;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblSubtotal;
    }
}