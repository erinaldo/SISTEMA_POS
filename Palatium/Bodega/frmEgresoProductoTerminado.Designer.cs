namespace Palatium.Bodega
{
    partial class frmEgresoProductoTerminado
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtFechaAplicacion = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblIngresoNumeros = new System.Windows.Forms.Label();
            this.dBAyudaIngresoNumeros = new Controles.Auxiliares.DB_Ayuda();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbBodega = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNotaEntrega = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOrdenDisenio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOrdenFabricacion = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dBAyudaPersona = new Controles.Auxiliares.DB_Ayuda();
            this.dBAyudaProveedor = new Controles.Auxiliares.DB_Ayuda();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbMotivo = new MisControles.ComboDatos();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.btnX = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnA = new System.Windows.Forms.Button();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.punto = new System.Windows.Forms.DataGridViewButtonColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadAnterior = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox2.Controls.Add(this.txtFechaAplicacion);
            this.groupBox2.Controls.Add(this.lblFecha);
            this.groupBox2.Controls.Add(this.lblIngresoNumeros);
            this.groupBox2.Controls.Add(this.dBAyudaIngresoNumeros);
            this.groupBox2.Controls.Add(this.lblBodega);
            this.groupBox2.Controls.Add(this.cmbBodega);
            this.groupBox2.Controls.Add(this.lblOficina);
            this.groupBox2.Controls.Add(this.cmbOficina);
            this.groupBox2.Location = new System.Drawing.Point(14, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(939, 70);
            this.groupBox2.TabIndex = 102;
            this.groupBox2.TabStop = false;
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
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtNotaEntrega);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtOrdenDisenio);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtOrdenFabricacion);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.txtComentarios);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.dBAyudaPersona);
            this.groupBox3.Controls.Add(this.dBAyudaProveedor);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblProveedor);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cmbMotivo);
            this.groupBox3.Location = new System.Drawing.Point(14, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(938, 163);
            this.groupBox3.TabIndex = 98;
            this.groupBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(598, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Nota de Entrega";
            // 
            // txtNotaEntrega
            // 
            this.txtNotaEntrega.BackColor = System.Drawing.SystemColors.Window;
            this.txtNotaEntrega.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNotaEntrega.Location = new System.Drawing.Point(601, 33);
            this.txtNotaEntrega.Name = "txtNotaEntrega";
            this.txtNotaEntrega.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtNotaEntrega.Size = new System.Drawing.Size(125, 20);
            this.txtNotaEntrega.TabIndex = 91;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(467, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "Orden Diseño";
            // 
            // txtOrdenDisenio
            // 
            this.txtOrdenDisenio.BackColor = System.Drawing.SystemColors.Window;
            this.txtOrdenDisenio.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOrdenDisenio.Location = new System.Drawing.Point(470, 33);
            this.txtOrdenDisenio.Name = "txtOrdenDisenio";
            this.txtOrdenDisenio.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOrdenDisenio.Size = new System.Drawing.Size(125, 20);
            this.txtOrdenDisenio.TabIndex = 90;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(336, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 92;
            this.label8.Text = "Ord. Fabricación";
            // 
            // txtOrdenFabricacion
            // 
            this.txtOrdenFabricacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtOrdenFabricacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtOrdenFabricacion.Location = new System.Drawing.Point(339, 33);
            this.txtOrdenFabricacion.Name = "txtOrdenFabricacion";
            this.txtOrdenFabricacion.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOrdenFabricacion.Size = new System.Drawing.Size(125, 20);
            this.txtOrdenFabricacion.TabIndex = 89;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button2.Location = new System.Drawing.Point(845, 114);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 39);
            this.button2.TabIndex = 88;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtComentarios
            // 
            this.txtComentarios.Location = new System.Drawing.Point(21, 126);
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(805, 20);
            this.txtComentarios.TabIndex = 78;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Comentarios";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(475, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Persona";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(18, 61);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(68, 13);
            this.lblProveedor.TabIndex = 74;
            this.lblProveedor.Text = "Entregado a:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Motivo";
            // 
            // cmbMotivo
            // 
            this.cmbMotivo.FormattingEnabled = true;
            this.cmbMotivo.Location = new System.Drawing.Point(15, 32);
            this.cmbMotivo.Name = "cmbMotivo";
            this.cmbMotivo.Size = new System.Drawing.Size(296, 21);
            this.cmbMotivo.TabIndex = 0;
            // 
            // btnX
            // 
            this.btnX.Enabled = false;
            this.btnX.Image = global::Palatium.Properties.Resources.menos;
            this.btnX.Location = new System.Drawing.Point(6, 174);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(23, 23);
            this.btnX.TabIndex = 94;
            this.ttMensaje.SetToolTip(this.btnX, "Clic aquí para remover la línea seleccionada");
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Palatium.Properties.Resources.impresora;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(615, 450);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(66, 23);
            this.btnImprimir.TabIndex = 97;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(687, 450);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 96;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.Image = global::Palatium.Properties.Resources.borrar;
            this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnular.Location = new System.Drawing.Point(753, 450);
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
            this.btnGrabar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(819, 450);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(67, 23);
            this.btnGrabar.TabIndex = 95;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(892, 450);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 23);
            this.btnSalir.TabIndex = 100;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.btnA);
            this.groupBox1.Controls.Add(this.btnX);
            this.groupBox1.Controls.Add(this.txtValorTotal);
            this.groupBox1.Controls.Add(this.lblValorTotal);
            this.groupBox1.Controls.Add(this.dgvDetalleVenta);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(939, 201);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            // 
            // btnA
            // 
            this.btnA.Enabled = false;
            this.btnA.Image = global::Palatium.Properties.Resources.plus_png;
            this.btnA.Location = new System.Drawing.Point(31, 174);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(23, 23);
            this.btnA.TabIndex = 93;
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.SystemColors.Window;
            this.txtValorTotal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValorTotal.Location = new System.Drawing.Point(850, 174);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.Size = new System.Drawing.Size(73, 20);
            this.txtValorTotal.TabIndex = 91;
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Location = new System.Drawing.Point(786, 179);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(58, 13);
            this.lblValorTotal.TabIndex = 90;
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
            this.unidad,
            this.cantidad,
            this.precioUnitario,
            this.subtotal,
            this.stock,
            this.cantidadAnterior,
            this.idProducto});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(7, 20);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(916, 150);
            this.dgvDetalleVenta.TabIndex = 52;
            this.dgvDetalleVenta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellContentClick);
            this.dgvDetalleVenta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellEndEdit);
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Código Producto";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 135;
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
            this.descripcionProducto.Width = 200;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Unidad";
            this.unidad.Name = "unidad";
            this.unidad.ReadOnly = true;
            this.unidad.Width = 60;
            // 
            // cantidad
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle6;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle7;
            this.precioUnitario.HeaderText = "Precio Uni.";
            this.precioUnitario.Name = "precioUnitario";
            this.precioUnitario.Width = 90;
            // 
            // subtotal
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.subtotal.DefaultCellStyle = dataGridViewCellStyle8;
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            this.subtotal.ReadOnly = true;
            this.subtotal.Width = 70;
            // 
            // stock
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.stock.DefaultCellStyle = dataGridViewCellStyle9;
            this.stock.HeaderText = "Stock";
            this.stock.Name = "stock";
            this.stock.ReadOnly = true;
            this.stock.Width = 70;
            // 
            // cantidadAnterior
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidadAnterior.DefaultCellStyle = dataGridViewCellStyle10;
            this.cantidadAnterior.HeaderText = "Cantidad Anterior";
            this.cantidadAnterior.Name = "cantidadAnterior";
            this.cantidadAnterior.ReadOnly = true;
            this.cantidadAnterior.Width = 120;
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
            // frmEgresoProductoTerminado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(966, 481);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmEgresoProductoTerminado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Egreso de Producto Terminado";
            this.Load += new System.EventHandler(this.frmEgresoProductoTerminado_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtFechaAplicacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblIngresoNumeros;
        private Controles.Auxiliares.DB_Ayuda dBAyudaIngresoNumeros;
        private System.Windows.Forms.Label lblBodega;
        private MisControles.ComboDatos cmbBodega;
        private System.Windows.Forms.Label lblOficina;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNotaEntrega;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOrdenDisenio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOrdenFabricacion;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label label3;
        private Controles.Auxiliares.DB_Ayuda dBAyudaPersona;
        private Controles.Auxiliares.DB_Ayuda dBAyudaProveedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Label label6;
        private MisControles.ComboDatos cmbMotivo;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewButtonColumn punto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadAnterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}