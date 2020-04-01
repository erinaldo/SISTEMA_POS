namespace Palatium.Bodega
{
    partial class frmTransferenciaDeProductos
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBodega = new System.Windows.Forms.Label();
            this.lblOficina = new System.Windows.Forms.Label();
            this.cmbBodega = new MisControles.ComboDatos();
            this.cmbOficina = new MisControles.ComboDatos();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBodega2 = new MisControles.ComboDatos();
            this.cmbOficina2 = new MisControles.ComboDatos();
            this.txtNumeroTraslado = new System.Windows.Forms.TextBox();
            this.lblIngresoNumeros = new System.Windows.Forms.Label();
            this.txtTraslado = new System.Windows.Forms.TextBox();
            this.btnAyudaNumeroTraslado = new System.Windows.Forms.Button();
            this.cmbMotivos = new MisControles.ComboDatos();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.lblReferencias = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.txtFechaAplicacion = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.lblComentarios = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroRecepcion = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.especificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idUnidadDeCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cgUnidadCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnA = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblBodega);
            this.groupBox1.Controls.Add(this.lblOficina);
            this.groupBox1.Controls.Add(this.cmbBodega);
            this.groupBox1.Controls.Add(this.cmbOficina);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Despachar desde";
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(158, 21);
            this.lblBodega.Name = "lblBodega";
            this.lblBodega.Size = new System.Drawing.Size(44, 13);
            this.lblBodega.TabIndex = 61;
            this.lblBodega.Text = "Bodega";
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Location = new System.Drawing.Point(24, 21);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(71, 13);
            this.lblOficina.TabIndex = 60;
            this.lblOficina.Text = "Oficina/Local";
            // 
            // cmbBodega
            // 
            this.cmbBodega.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBodega.Enabled = false;
            this.cmbBodega.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(160, 36);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(138, 21);
            this.cmbBodega.TabIndex = 2;
            // 
            // cmbOficina
            // 
            this.cmbOficina.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOficina.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOficina.FormattingEnabled = true;
            this.cmbOficina.Location = new System.Drawing.Point(25, 36);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(129, 21);
            this.cmbOficina.TabIndex = 1;
            this.cmbOficina.SelectedIndexChanged += new System.EventHandler(this.cmbOficina_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbBodega2);
            this.groupBox2.Controls.Add(this.cmbOficina2);
            this.groupBox2.Location = new System.Drawing.Point(21, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 69);
            this.groupBox2.TabIndex = 62;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hacia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Bodega";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Oficina/Local";
            // 
            // cmbBodega2
            // 
            this.cmbBodega2.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBodega2.Enabled = false;
            this.cmbBodega2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBodega2.FormattingEnabled = true;
            this.cmbBodega2.Location = new System.Drawing.Point(160, 34);
            this.cmbBodega2.Name = "cmbBodega2";
            this.cmbBodega2.Size = new System.Drawing.Size(138, 21);
            this.cmbBodega2.TabIndex = 6;
            // 
            // cmbOficina2
            // 
            this.cmbOficina2.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOficina2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOficina2.FormattingEnabled = true;
            this.cmbOficina2.Location = new System.Drawing.Point(25, 34);
            this.cmbOficina2.Name = "cmbOficina2";
            this.cmbOficina2.Size = new System.Drawing.Size(129, 21);
            this.cmbOficina2.TabIndex = 5;
            this.cmbOficina2.SelectedIndexChanged += new System.EventHandler(this.cmbOficina2_SelectedIndexChanged);
            // 
            // txtNumeroTraslado
            // 
            this.txtNumeroTraslado.BackColor = System.Drawing.Color.White;
            this.txtNumeroTraslado.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNumeroTraslado.Location = new System.Drawing.Point(349, 52);
            this.txtNumeroTraslado.Name = "txtNumeroTraslado";
            this.txtNumeroTraslado.Size = new System.Drawing.Size(145, 20);
            this.txtNumeroTraslado.TabIndex = 3;
            // 
            // lblIngresoNumeros
            // 
            this.lblIngresoNumeros.AutoSize = true;
            this.lblIngresoNumeros.Location = new System.Drawing.Point(346, 37);
            this.lblIngresoNumeros.Name = "lblIngresoNumeros";
            this.lblIngresoNumeros.Size = new System.Drawing.Size(99, 13);
            this.lblIngresoNumeros.TabIndex = 65;
            this.lblIngresoNumeros.Text = "Número de traslado";
            // 
            // txtTraslado
            // 
            this.txtTraslado.BackColor = System.Drawing.Color.White;
            this.txtTraslado.Location = new System.Drawing.Point(525, 49);
            this.txtTraslado.Name = "txtTraslado";
            this.txtTraslado.ReadOnly = true;
            this.txtTraslado.Size = new System.Drawing.Size(250, 20);
            this.txtTraslado.TabIndex = 68;
            // 
            // btnAyudaNumeroTraslado
            // 
            this.btnAyudaNumeroTraslado.Location = new System.Drawing.Point(496, 47);
            this.btnAyudaNumeroTraslado.Name = "btnAyudaNumeroTraslado";
            this.btnAyudaNumeroTraslado.Size = new System.Drawing.Size(23, 23);
            this.btnAyudaNumeroTraslado.TabIndex = 4;
            this.btnAyudaNumeroTraslado.Text = "?";
            this.btnAyudaNumeroTraslado.UseVisualStyleBackColor = true;
            this.btnAyudaNumeroTraslado.Click += new System.EventHandler(this.btnAyudaNumeroTraslado_Click);
            // 
            // cmbMotivos
            // 
            this.cmbMotivos.BackColor = System.Drawing.SystemColors.Window;
            this.cmbMotivos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbMotivos.FormattingEnabled = true;
            this.cmbMotivos.Location = new System.Drawing.Point(349, 129);
            this.cmbMotivos.Name = "cmbMotivos";
            this.cmbMotivos.Size = new System.Drawing.Size(163, 21);
            this.cmbMotivos.TabIndex = 7;
            // 
            // lblMotivo
            // 
            this.lblMotivo.AutoSize = true;
            this.lblMotivo.Location = new System.Drawing.Point(349, 114);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(39, 13);
            this.lblMotivo.TabIndex = 69;
            this.lblMotivo.Text = "Motivo";
            // 
            // lblReferencias
            // 
            this.lblReferencias.AutoSize = true;
            this.lblReferencias.Location = new System.Drawing.Point(519, 115);
            this.lblReferencias.Name = "lblReferencias";
            this.lblReferencias.Size = new System.Drawing.Size(98, 13);
            this.lblReferencias.TabIndex = 72;
            this.lblReferencias.Text = "Referencia Externa";
            // 
            // txtReferencia
            // 
            this.txtReferencia.BackColor = System.Drawing.Color.White;
            this.txtReferencia.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtReferencia.Location = new System.Drawing.Point(521, 130);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(126, 20);
            this.txtReferencia.TabIndex = 8;
            // 
            // txtFechaAplicacion
            // 
            this.txtFechaAplicacion.BackColor = System.Drawing.Color.White;
            this.txtFechaAplicacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaAplicacion.Location = new System.Drawing.Point(667, 129);
            this.txtFechaAplicacion.Name = "txtFechaAplicacion";
            this.txtFechaAplicacion.Size = new System.Drawing.Size(112, 20);
            this.txtFechaAplicacion.TabIndex = 8;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(666, 114);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 73;
            this.lblFecha.Text = "Fecha";
            // 
            // txtComentarios
            // 
            this.txtComentarios.BackColor = System.Drawing.Color.White;
            this.txtComentarios.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtComentarios.Location = new System.Drawing.Point(40, 185);
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(434, 20);
            this.txtComentarios.TabIndex = 9;
            // 
            // lblComentarios
            // 
            this.lblComentarios.AutoSize = true;
            this.lblComentarios.Location = new System.Drawing.Point(43, 170);
            this.lblComentarios.Name = "lblComentarios";
            this.lblComentarios.Size = new System.Drawing.Size(65, 13);
            this.lblComentarios.TabIndex = 77;
            this.lblComentarios.Text = "Comentarios";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "Num. de Recepción";
            // 
            // txtNumeroRecepcion
            // 
            this.txtNumeroRecepcion.BackColor = System.Drawing.Color.White;
            this.txtNumeroRecepcion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNumeroRecepcion.Location = new System.Drawing.Point(485, 185);
            this.txtNumeroRecepcion.Name = "txtNumeroRecepcion";
            this.txtNumeroRecepcion.ReadOnly = true;
            this.txtNumeroRecepcion.Size = new System.Drawing.Size(165, 20);
            this.txtNumeroRecepcion.TabIndex = 10;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnOk.Location = new System.Drawing.Point(698, 179);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnX
            // 
            this.btnX.Location = new System.Drawing.Point(19, 172);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(23, 23);
            this.btnX.TabIndex = 13;
            this.btnX.Text = "X";
            this.ttMensaje.SetToolTip(this.btnX, "Clic aquí para remover la fila seleccionada");
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
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
            this.boton,
            this.descripcionProducto,
            this.especificacion,
            this.unidad,
            this.cantidad,
            this.stock,
            this.idProducto,
            this.idUnidadDeCompra,
            this.cgUnidadCompra});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(3, 13);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(787, 158);
            this.dgvDetalleVenta.TabIndex = 82;
            this.dgvDetalleVenta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellContentClick);
            this.dgvDetalleVenta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleVenta_CellEndEdit);
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Código Producto";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 115;
            // 
            // boton
            // 
            this.boton.HeaderText = "?";
            this.boton.Name = "boton";
            this.boton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.boton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.boton.Width = 25;
            // 
            // descripcionProducto
            // 
            this.descripcionProducto.HeaderText = "Descripción del Producto";
            this.descripcionProducto.Name = "descripcionProducto";
            this.descripcionProducto.ReadOnly = true;
            this.descripcionProducto.Width = 230;
            // 
            // especificacion
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.especificacion.DefaultCellStyle = dataGridViewCellStyle1;
            this.especificacion.HeaderText = "Especificación";
            this.especificacion.Name = "especificacion";
            this.especificacion.ReadOnly = true;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Unidad";
            this.unidad.Name = "unidad";
            this.unidad.ReadOnly = true;
            this.unidad.Width = 70;
            // 
            // cantidad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // stock
            // 
            this.stock.HeaderText = "Stock";
            this.stock.Name = "stock";
            this.stock.ReadOnly = true;
            this.stock.Width = 70;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "ID PRODUCTO";
            this.idProducto.Name = "idProducto";
            this.idProducto.Visible = false;
            // 
            // idUnidadDeCompra
            // 
            this.idUnidadDeCompra.HeaderText = "Id Unidad de Compra";
            this.idUnidadDeCompra.Name = "idUnidadDeCompra";
            this.idUnidadDeCompra.Visible = false;
            // 
            // cgUnidadCompra
            // 
            this.cgUnidadCompra.HeaderText = "Unidad Compra";
            this.cgUnidadCompra.Name = "cgUnidadCompra";
            this.cgUnidadCompra.Visible = false;
            // 
            // btnA
            // 
            this.btnA.Location = new System.Drawing.Point(44, 172);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(23, 23);
            this.btnA.TabIndex = 12;
            this.btnA.Text = "A";
            this.ttMensaje.SetToolTip(this.btnA, "Clic aquí para agregar una fila");
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.btnA);
            this.panel1.Controls.Add(this.dgvDetalleVenta);
            this.panel1.Controls.Add(this.btnX);
            this.panel1.Location = new System.Drawing.Point(4, 222);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 196);
            this.panel1.TabIndex = 85;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.txtTraslado);
            this.panel2.Controls.Add(this.btnAyudaNumeroTraslado);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(796, 213);
            this.panel2.TabIndex = 86;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(526, 424);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(67, 23);
            this.btnLimpiar.TabIndex = 16;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.Image = global::Palatium.Properties.Resources.borrar;
            this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnular.Location = new System.Drawing.Point(599, 424);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(67, 23);
            this.btnAnular.TabIndex = 17;
            this.btnAnular.Text = "Anular";
            this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(667, 424);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(67, 23);
            this.btnGrabar.TabIndex = 14;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(740, 424);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(63, 23);
            this.btnSalir.TabIndex = 18;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Palatium.Properties.Resources.impresora;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(450, 424);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(70, 23);
            this.btnImprimir.TabIndex = 16;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // frmTransferenciaDeProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(807, 456);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNumeroRecepcion);
            this.Controls.Add(this.txtComentarios);
            this.Controls.Add(this.lblComentarios);
            this.Controls.Add(this.txtFechaAplicacion);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.lblReferencias);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.cmbMotivos);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.txtNumeroTraslado);
            this.Controls.Add(this.lblIngresoNumeros);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTransferenciaDeProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transferencia o Despacho de Productos de una bodega o otra";
            this.Load += new System.EventHandler(this.frmTransferenciaDeProductos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MisControles.ComboDatos cmbBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblBodega;
        private System.Windows.Forms.Label lblOficina;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MisControles.ComboDatos cmbBodega2;
        private MisControles.ComboDatos cmbOficina2;
        private System.Windows.Forms.TextBox txtNumeroTraslado;
        private System.Windows.Forms.Label lblIngresoNumeros;
        private System.Windows.Forms.TextBox txtTraslado;
        private System.Windows.Forms.Button btnAyudaNumeroTraslado;
        private MisControles.ComboDatos cmbMotivos;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.Label lblReferencias;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.TextBox txtFechaAplicacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label lblComentarios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumeroRecepcion;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewButtonColumn boton;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn especificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn idUnidadDeCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn cgUnidadCompra;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}