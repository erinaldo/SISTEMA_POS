namespace Palatium.Bodega
{
    partial class frmIngresoMateriaPrima
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFechaAplicacion = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblIngresoNumeros = new System.Windows.Forms.Label();
            this.dBAyudaIngresoNumeros = new Controles.Auxiliares.DB_Ayuda();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbBodega = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lblIva = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.lblComentarios = new System.Windows.Forms.Label();
            this.dBAyudaPersona = new Controles.Auxiliares.DB_Ayuda();
            this.dBAyudaProveedor = new Controles.Auxiliares.DB_Ayuda();
            this.lblPersona = new System.Windows.Forms.Label();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.lblNotaEntrega = new System.Windows.Forms.Label();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtNotaEntrega = new System.Windows.Forms.TextBox();
            this.lblFacturaCompra = new System.Windows.Forms.Label();
            this.txtFacturaCompra = new System.Windows.Forms.TextBox();
            this.lblNotaPedido = new System.Windows.Forms.Label();
            this.txtNotaPedido = new System.Windows.Forms.TextBox();
            this.lblReferencias = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.cmbMoneda = new MisControles.ComboDatos();
            this.cmbMotivo = new MisControles.ComboDatos();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnA = new System.Windows.Forms.Button();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.btnX = new System.Windows.Forms.Button();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.punto = new System.Windows.Forms.DataGridViewButtonColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.especificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.procentajeDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtIva1 = new System.Windows.Forms.TextBox();
            this.lblValorBruto = new System.Windows.Forms.Label();
            this.lblIva1 = new System.Windows.Forms.Label();
            this.txtValorBruto = new System.Windows.Forms.TextBox();
            this.txtValorNeto = new System.Windows.Forms.TextBox();
            this.lblDescuento1 = new System.Windows.Forms.Label();
            this.lblValorNeto = new System.Windows.Forms.Label();
            this.txtDescuento1 = new System.Windows.Forms.TextBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.dsCombos1 = new Palatium.dsCombos();
            this.dsCombos2 = new Palatium.dsCombos();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCombos1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCombos2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFechaAplicacion);
            this.groupBox1.Controls.Add(this.lblFecha);
            this.groupBox1.Controls.Add(this.lblIngresoNumeros);
            this.groupBox1.Controls.Add(this.dBAyudaIngresoNumeros);
            this.groupBox1.Controls.Add(this.lblBodega);
            this.groupBox1.Controls.Add(this.cmbBodega);
            this.groupBox1.Controls.Add(this.lblOficina);
            this.groupBox1.Controls.Add(this.cmbOficina);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(939, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtFechaAplicacion
            // 
            this.txtFechaAplicacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFechaAplicacion.Location = new System.Drawing.Point(833, 30);
            this.txtFechaAplicacion.Name = "txtFechaAplicacion";
            this.txtFechaAplicacion.ReadOnly = true;
            this.txtFechaAplicacion.Size = new System.Drawing.Size(90, 20);
            this.txtFechaAplicacion.TabIndex = 89;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(833, 14);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 61;
            this.lblFecha.Text = "Fecha";
            // 
            // lblIngresoNumeros
            // 
            this.lblIngresoNumeros.AutoSize = true;
            this.lblIngresoNumeros.Location = new System.Drawing.Point(373, 14);
            this.lblIngresoNumeros.Name = "lblIngresoNumeros";
            this.lblIngresoNumeros.Size = new System.Drawing.Size(87, 13);
            this.lblIngresoNumeros.TabIndex = 60;
            this.lblIngresoNumeros.Text = "Ingreso Números";
            // 
            // dBAyudaIngresoNumeros
            // 
            this.dBAyudaIngresoNumeros.Enabled = false;
            this.dBAyudaIngresoNumeros.iId = 0;
            this.dBAyudaIngresoNumeros.Location = new System.Drawing.Point(373, 29);
            this.dBAyudaIngresoNumeros.Name = "dBAyudaIngresoNumeros";
            this.dBAyudaIngresoNumeros.sCodigo = null;
            this.dBAyudaIngresoNumeros.Size = new System.Drawing.Size(454, 22);
            this.dBAyudaIngresoNumeros.sNombre = null;
            this.dBAyudaIngresoNumeros.TabIndex = 59;
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(193, 14);
            this.lblBodega.Name = "lblBodega";
            this.lblBodega.Size = new System.Drawing.Size(44, 13);
            this.lblBodega.TabIndex = 58;
            this.lblBodega.Text = "Bodega";
            // 
            // cmbBodega
            // 
            this.cmbBodega.Enabled = false;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(193, 30);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(167, 21);
            this.cmbBodega.TabIndex = 1;
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Location = new System.Drawing.Point(16, 14);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(71, 13);
            this.lblOficina.TabIndex = 57;
            this.lblOficina.Text = "Oficina/Local";
            // 
            // cmbOficina
            // 
            this.cmbOficina.FormattingEnabled = true;
            this.cmbOficina.Location = new System.Drawing.Point(16, 30);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(167, 21);
            this.cmbOficina.TabIndex = 0;
            this.cmbOficina.SelectedIndexChanged += new System.EventHandler(this.cmbOficina_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.cmbEstado);
            this.groupBox2.Controls.Add(this.txtDescuento);
            this.groupBox2.Controls.Add(this.txtIva);
            this.groupBox2.Controls.Add(this.lblEstado);
            this.groupBox2.Controls.Add(this.lblDescuento);
            this.groupBox2.Controls.Add(this.lblIva);
            this.groupBox2.Controls.Add(this.txtComentarios);
            this.groupBox2.Controls.Add(this.lblComentarios);
            this.groupBox2.Controls.Add(this.dBAyudaPersona);
            this.groupBox2.Controls.Add(this.dBAyudaProveedor);
            this.groupBox2.Controls.Add(this.lblPersona);
            this.groupBox2.Controls.Add(this.lblMoneda);
            this.groupBox2.Controls.Add(this.lblProveedor);
            this.groupBox2.Controls.Add(this.lblNotaEntrega);
            this.groupBox2.Controls.Add(this.lblMotivo);
            this.groupBox2.Controls.Add(this.txtNotaEntrega);
            this.groupBox2.Controls.Add(this.lblFacturaCompra);
            this.groupBox2.Controls.Add(this.txtFacturaCompra);
            this.groupBox2.Controls.Add(this.lblNotaPedido);
            this.groupBox2.Controls.Add(this.txtNotaPedido);
            this.groupBox2.Controls.Add(this.lblReferencias);
            this.groupBox2.Controls.Add(this.txtReferencia);
            this.groupBox2.Controls.Add(this.cmbMoneda);
            this.groupBox2.Controls.Add(this.cmbMotivo);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(938, 163);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnOk.Location = new System.Drawing.Point(845, 114);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 39);
            this.btnOk.TabIndex = 88;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(660, 124);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 21);
            this.cmbEstado.TabIndex = 87;
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(562, 126);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.Size = new System.Drawing.Size(69, 20);
            this.txtDescuento.TabIndex = 86;
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(468, 126);
            this.txtIva.Name = "txtIva";
            this.txtIva.ReadOnly = true;
            this.txtIva.Size = new System.Drawing.Size(69, 20);
            this.txtIva.TabIndex = 85;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(660, 106);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(40, 13);
            this.lblEstado.TabIndex = 84;
            this.lblEstado.Text = "Estado";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(561, 106);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(70, 13);
            this.lblDescuento.TabIndex = 83;
            this.lblDescuento.Text = "% Descuento";
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.Location = new System.Drawing.Point(465, 106);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(35, 13);
            this.lblIva.TabIndex = 82;
            this.lblIva.Text = "% IVA";
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(21, 126);
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(423, 20);
            this.txtComentarios.TabIndex = 78;
            // 
            // lblComentarios
            // 
            this.lblComentarios.AutoSize = true;
            this.lblComentarios.Location = new System.Drawing.Point(18, 106);
            this.lblComentarios.Name = "lblComentarios";
            this.lblComentarios.Size = new System.Drawing.Size(65, 13);
            this.lblComentarios.TabIndex = 77;
            this.lblComentarios.Text = "Comentarios";
            // 
            // dBAyudaPersona
            // 
            this.dBAyudaPersona.iId = 0;
            this.dBAyudaPersona.Location = new System.Drawing.Point(475, 77);
            this.dBAyudaPersona.Name = "dBAyudaPersona";
            this.dBAyudaPersona.sCodigo = null;
            this.dBAyudaPersona.Size = new System.Drawing.Size(454, 22);
            this.dBAyudaPersona.sNombre = null;
            this.dBAyudaPersona.TabIndex = 76;
            // 
            // dBAyudaProveedor
            // 
            this.dBAyudaProveedor.iId = 0;
            this.dBAyudaProveedor.Location = new System.Drawing.Point(18, 77);
            this.dBAyudaProveedor.Name = "dBAyudaProveedor";
            this.dBAyudaProveedor.sCodigo = null;
            this.dBAyudaProveedor.Size = new System.Drawing.Size(454, 22);
            this.dBAyudaProveedor.sNombre = null;
            this.dBAyudaProveedor.TabIndex = 60;
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Location = new System.Drawing.Point(475, 61);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(46, 13);
            this.lblPersona.TabIndex = 75;
            this.lblPersona.Text = "Persona";
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.Location = new System.Drawing.Point(173, 16);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(46, 13);
            this.lblMoneda.TabIndex = 65;
            this.lblMoneda.Text = "Moneda";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(18, 61);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(56, 13);
            this.lblProveedor.TabIndex = 74;
            this.lblProveedor.Text = "Proveedor";
            // 
            // lblNotaEntrega
            // 
            this.lblNotaEntrega.AutoSize = true;
            this.lblNotaEntrega.Location = new System.Drawing.Point(726, 16);
            this.lblNotaEntrega.Name = "lblNotaEntrega";
            this.lblNotaEntrega.Size = new System.Drawing.Size(85, 13);
            this.lblNotaEntrega.TabIndex = 72;
            this.lblNotaEntrega.Text = "Nota de Entrega";
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(15, 16);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(39, 13);
            this.lblMotivo.TabIndex = 64;
            this.lblMotivo.Text = "Motivo";
            // 
            // txtNotaEntrega
            // 
            this.txtNotaEntrega.Location = new System.Drawing.Point(726, 33);
            this.txtNotaEntrega.Name = "txtNotaEntrega";
            this.txtNotaEntrega.Size = new System.Drawing.Size(118, 20);
            this.txtNotaEntrega.TabIndex = 5;
            // 
            // lblFacturaCompra
            // 
            this.lblFacturaCompra.AutoSize = true;
            this.lblFacturaCompra.Location = new System.Drawing.Point(597, 16);
            this.lblFacturaCompra.Name = "lblFacturaCompra";
            this.lblFacturaCompra.Size = new System.Drawing.Size(97, 13);
            this.lblFacturaCompra.TabIndex = 71;
            this.lblFacturaCompra.Text = "Factura de Compra";
            // 
            // txtFacturaCompra
            // 
            this.txtFacturaCompra.Location = new System.Drawing.Point(597, 33);
            this.txtFacturaCompra.Name = "txtFacturaCompra";
            this.txtFacturaCompra.Size = new System.Drawing.Size(118, 20);
            this.txtFacturaCompra.TabIndex = 4;
            // 
            // lblNotaPedido
            // 
            this.lblNotaPedido.AutoSize = true;
            this.lblNotaPedido.Location = new System.Drawing.Point(468, 16);
            this.lblNotaPedido.Name = "lblNotaPedido";
            this.lblNotaPedido.Size = new System.Drawing.Size(81, 13);
            this.lblNotaPedido.TabIndex = 70;
            this.lblNotaPedido.Text = "Nota de Pedido";
            // 
            // txtNotaPedido
            // 
            this.txtNotaPedido.Location = new System.Drawing.Point(468, 33);
            this.txtNotaPedido.Name = "txtNotaPedido";
            this.txtNotaPedido.Size = new System.Drawing.Size(118, 20);
            this.txtNotaPedido.TabIndex = 3;
            // 
            // lblReferencias
            // 
            this.lblReferencias.AutoSize = true;
            this.lblReferencias.Location = new System.Drawing.Point(339, 16);
            this.lblReferencias.Name = "lblReferencias";
            this.lblReferencias.Size = new System.Drawing.Size(96, 13);
            this.lblReferencias.TabIndex = 69;
            this.lblReferencias.Text = "Referencia/Import.";
            // 
            // txtReferencia
            // 
            this.txtReferencia.Location = new System.Drawing.Point(339, 33);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(118, 20);
            this.txtReferencia.TabIndex = 2;
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(173, 32);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(145, 21);
            this.cmbMoneda.TabIndex = 1;
            // 
            // cmbMotivo
            // 
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Location = new System.Drawing.Point(15, 32);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new System.Drawing.Size(145, 21);
            this.cmbMotivo.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnA);
            this.groupBox3.Controls.Add(this.txtValorTotal);
            this.groupBox3.Controls.Add(this.btnX);
            this.groupBox3.Controls.Add(this.lblValorTotal);
            this.groupBox3.Controls.Add(this.dgvDetalleVenta);
            this.groupBox3.Controls.Add(this.txtIva1);
            this.groupBox3.Controls.Add(this.lblValorBruto);
            this.groupBox3.Controls.Add(this.lblIva1);
            this.groupBox3.Controls.Add(this.txtValorBruto);
            this.groupBox3.Controls.Add(this.txtValorNeto);
            this.groupBox3.Controls.Add(this.lblDescuento1);
            this.groupBox3.Controls.Add(this.lblValorNeto);
            this.groupBox3.Controls.Add(this.txtDescuento1);
            this.groupBox3.Location = new System.Drawing.Point(12, 248);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(937, 199);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnA
            // 
            this.btnA.Enabled = false;
            this.btnA.Image = global::Palatium.Properties.Resources.plus_png;
            this.btnA.Location = new System.Drawing.Point(31, 171);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(23, 23);
            this.btnA.TabIndex = 91;
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.SystemColors.Window;
            this.txtValorTotal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValorTotal.Location = new System.Drawing.Point(844, 173);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Size = new System.Drawing.Size(73, 20);
            this.txtValorTotal.TabIndex = 101;
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnX
            // 
            this.btnX.Enabled = false;
            this.btnX.Image = global::Palatium.Properties.Resources.menos;
            this.btnX.Location = new System.Drawing.Point(6, 171);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(23, 23);
            this.btnX.TabIndex = 92;
            this.ttMensaje.SetToolTip(this.btnX, "Clic aquí para remover la línea seleccionada");
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Location = new System.Drawing.Point(780, 176);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(58, 13);
            this.lblValorTotal.TabIndex = 100;
            this.lblValorTotal.Text = "Valor Total";
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.AllowUserToAddRows = false;
            this.dgvDetalleVenta.AllowUserToResizeColumns = false;
            this.dgvDetalleVenta.AllowUserToResizeRows = false;
            this.dgvDetalleVenta.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoProducto,
            this.punto,
            this.descripcionProducto,
            this.especificacion,
            this.unidad,
            this.cantidad,
            this.precioUnitario,
            this.procentajeDescuento,
            this.descuento,
            this.subtotal,
            this.stock,
            this.idProducto,
            this.paga_iva,
            this.valor_unidad});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(6, 19);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.RowHeadersWidth = 25;
            this.dgvDetalleVenta.Size = new System.Drawing.Size(915, 150);
            this.dgvDetalleVenta.TabIndex = 91;
            this.dgvDetalleVenta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellContentClick);
            this.dgvDetalleVenta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellEndEdit);
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Código Producto";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 110;
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
            this.descripcionProducto.ReadOnly = true;
            this.descripcionProducto.Width = 150;
            // 
            // especificacion
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.especificacion.DefaultCellStyle = dataGridViewCellStyle1;
            this.especificacion.HeaderText = "Especificación";
            this.especificacion.Name = "especificacion";
            this.especificacion.ReadOnly = true;
            this.especificacion.Width = 80;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Unidad";
            this.unidad.Name = "unidad";
            this.unidad.ReadOnly = true;
            this.unidad.Width = 80;
            // 
            // cantidad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 60;
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle3;
            this.precioUnitario.HeaderText = "Precio Uni.";
            this.precioUnitario.Name = "precioUnitario";
            this.precioUnitario.Width = 90;
            // 
            // procentajeDescuento
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.procentajeDescuento.DefaultCellStyle = dataGridViewCellStyle4;
            this.procentajeDescuento.HeaderText = "% desc.";
            this.procentajeDescuento.Name = "procentajeDescuento";
            this.procentajeDescuento.Width = 80;
            // 
            // descuento
            // 
            this.descuento.HeaderText = "Descuento";
            this.descuento.Name = "descuento";
            this.descuento.Width = 60;
            // 
            // subtotal
            // 
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            this.subtotal.Width = 70;
            // 
            // stock
            // 
            this.stock.HeaderText = "Stock";
            this.stock.Name = "stock";
            this.stock.ReadOnly = true;
            this.stock.Width = 60;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "ID PRODUCTO";
            this.idProducto.Name = "idProducto";
            this.idProducto.Visible = false;
            // 
            // paga_iva
            // 
            this.paga_iva.HeaderText = "paga_iva";
            this.paga_iva.Name = "paga_iva";
            this.paga_iva.Visible = false;
            // 
            // valor_unidad
            // 
            this.valor_unidad.HeaderText = "valor_unidad";
            this.valor_unidad.Name = "valor_unidad";
            this.valor_unidad.Visible = false;
            // 
            // txtIva1
            // 
            this.txtIva1.BackColor = System.Drawing.SystemColors.Window;
            this.txtIva1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIva1.Location = new System.Drawing.Point(713, 173);
            this.txtIva1.Name = "txtIva1";
            this.txtIva1.Size = new System.Drawing.Size(66, 20);
            this.txtIva1.TabIndex = 99;
            this.txtIva1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorBruto
            // 
            this.lblValorBruto.AutoSize = true;
            this.lblValorBruto.Location = new System.Drawing.Point(306, 176);
            this.lblValorBruto.Name = "lblValorBruto";
            this.lblValorBruto.Size = new System.Drawing.Size(62, 13);
            this.lblValorBruto.TabIndex = 93;
            this.lblValorBruto.Text = "Valor Bruto:";
            // 
            // lblIva1
            // 
            this.lblIva1.AutoSize = true;
            this.lblIva1.Location = new System.Drawing.Point(679, 176);
            this.lblIva1.Name = "lblIva1";
            this.lblIva1.Size = new System.Drawing.Size(27, 13);
            this.lblIva1.TabIndex = 98;
            this.lblIva1.Text = "IVA:";
            // 
            // txtValorBruto
            // 
            this.txtValorBruto.BackColor = System.Drawing.SystemColors.Window;
            this.txtValorBruto.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValorBruto.Location = new System.Drawing.Point(371, 173);
            this.txtValorBruto.Name = "txtValorBruto";
            this.txtValorBruto.Size = new System.Drawing.Size(53, 20);
            this.txtValorBruto.TabIndex = 92;
            this.txtValorBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtValorNeto
            // 
            this.txtValorNeto.BackColor = System.Drawing.SystemColors.Window;
            this.txtValorNeto.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValorNeto.Location = new System.Drawing.Point(614, 173);
            this.txtValorNeto.Name = "txtValorNeto";
            this.txtValorNeto.Size = new System.Drawing.Size(62, 20);
            this.txtValorNeto.TabIndex = 97;
            this.txtValorNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuento1
            // 
            this.lblDescuento1.AutoSize = true;
            this.lblDescuento1.Location = new System.Drawing.Point(425, 176);
            this.lblDescuento1.Name = "lblDescuento1";
            this.lblDescuento1.Size = new System.Drawing.Size(62, 13);
            this.lblDescuento1.TabIndex = 94;
            this.lblDescuento1.Text = "Descuento:";
            // 
            // lblValorNeto
            // 
            this.lblValorNeto.AutoSize = true;
            this.lblValorNeto.Location = new System.Drawing.Point(555, 176);
            this.lblValorNeto.Name = "lblValorNeto";
            this.lblValorNeto.Size = new System.Drawing.Size(57, 13);
            this.lblValorNeto.TabIndex = 96;
            this.lblValorNeto.Text = "Valor Neto";
            // 
            // txtDescuento1
            // 
            this.txtDescuento1.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescuento1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDescuento1.Location = new System.Drawing.Point(488, 173);
            this.txtDescuento1.Name = "txtDescuento1";
            this.txtDescuento1.Size = new System.Drawing.Size(66, 20);
            this.txtDescuento1.TabIndex = 95;
            this.txtDescuento1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Palatium.Properties.Resources.impresora;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(621, 453);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(66, 23);
            this.btnImprimir.TabIndex = 88;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(693, 453);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 87;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.Image = global::Palatium.Properties.Resources.borrar;
            this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnular.Location = new System.Drawing.Point(759, 453);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(60, 23);
            this.btnAnular.TabIndex = 90;
            this.btnAnular.Text = "Anular";
            this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(821, 453);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(66, 23);
            this.btnGrabar.TabIndex = 86;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(891, 453);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 23);
            this.btnSalir.TabIndex = 89;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ttMensaje
            // 
            this.ttMensaje.ToolTipTitle = "Clic aquí para agregar una nueva línea";
            // 
            // dsCombos1
            // 
            this.dsCombos1.DataSetName = "dsCombos";
            this.dsCombos1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsCombos2
            // 
            this.dsCombos2.DataSetName = "dsCombos";
            this.dsCombos2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmIngresoMateriaPrima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(966, 481);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmIngresoMateriaPrima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de ingreso de Materia Prima";
            this.Load += new System.EventHandler(this.frmIngresoBodega_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCombos1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCombos2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private dsCombos dsCombos1;
        private dsCombos dsCombos2;
        private MisControles.ComboDatos cmbBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblBodega;
        private System.Windows.Forms.Label lblOficina;
        private Controles.Auxiliares.DB_Ayuda dBAyudaIngresoNumeros;
        private System.Windows.Forms.Label lblIngresoNumeros;
        private System.Windows.Forms.GroupBox groupBox2;
        private MisControles.ComboDatos cmbMotivo;
        private MisControles.ComboDatos cmbMoneda;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.TextBox txtNotaEntrega;
        private System.Windows.Forms.TextBox txtFacturaCompra;
        private System.Windows.Forms.TextBox txtNotaPedido;
        private System.Windows.Forms.Label lblNotaEntrega;
        private System.Windows.Forms.Label lblFacturaCompra;
        private System.Windows.Forms.Label lblNotaPedido;
        private System.Windows.Forms.Label lblReferencias;
        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.Label lblMotivo;
        private Controles.Auxiliares.DB_Ayuda dBAyudaPersona;
        private Controles.Auxiliares.DB_Ayuda dBAyudaProveedor;
        private System.Windows.Forms.Label lblPersona;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label lblComentarios;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.TextBox txtIva1;
        private System.Windows.Forms.Label lblValorBruto;
        private System.Windows.Forms.Label lblIva1;
        private System.Windows.Forms.TextBox txtValorBruto;
        private System.Windows.Forms.TextBox txtValorNeto;
        private System.Windows.Forms.Label lblDescuento1;
        private System.Windows.Forms.Label lblValorNeto;
        private System.Windows.Forms.TextBox txtDescuento1;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.TextBox txtFechaAplicacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewButtonColumn punto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn especificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn procentajeDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn paga_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_unidad;
    }
}