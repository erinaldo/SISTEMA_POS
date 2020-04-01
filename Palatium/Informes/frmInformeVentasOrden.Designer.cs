namespace Palatium.Informes
{
    partial class frmInformeVentasOrden
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
            this.Grb_listReCateraCobrar = new System.Windows.Forms.GroupBox();
            this.txtTotalPagar = new System.Windows.Forms.TextBox();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.txtIva = new System.Windows.Forms.TextBox();
            this.lblIva = new System.Windows.Forms.Label();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.lblServicio = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtValorBruto = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.dgvReimpresionFactura = new System.Windows.Forms.DataGridView();
            this.clmCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVUnidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDescto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmVTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbTipoOrden = new MisControles.ComboDatos();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHasta = new System.Windows.Forms.Button();
            this.TxtHasta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDesde = new System.Windows.Forms.Button();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.Grb_listReCateraCobrar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReimpresionFactura)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_listReCateraCobrar
            // 
            this.Grb_listReCateraCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listReCateraCobrar.Controls.Add(this.txtTotalPagar);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblTotalPagar);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtIva);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblIva);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtSubTotal);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblSubTotal);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtServicio);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblServicio);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtDescuento);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblDescuento);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtValorBruto);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblValor);
            this.Grb_listReCateraCobrar.Controls.Add(this.dgvReimpresionFactura);
            this.Grb_listReCateraCobrar.Location = new System.Drawing.Point(12, 152);
            this.Grb_listReCateraCobrar.Name = "Grb_listReCateraCobrar";
            this.Grb_listReCateraCobrar.Size = new System.Drawing.Size(1014, 357);
            this.Grb_listReCateraCobrar.TabIndex = 55;
            this.Grb_listReCateraCobrar.TabStop = false;
            this.Grb_listReCateraCobrar.Text = "Lista de Registros";
            // 
            // txtTotalPagar
            // 
            this.txtTotalPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPagar.ForeColor = System.Drawing.Color.Red;
            this.txtTotalPagar.Location = new System.Drawing.Point(906, 330);
            this.txtTotalPagar.Multiline = true;
            this.txtTotalPagar.Name = "txtTotalPagar";
            this.txtTotalPagar.ReadOnly = true;
            this.txtTotalPagar.Size = new System.Drawing.Size(83, 21);
            this.txtTotalPagar.TabIndex = 51;
            this.txtTotalPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPagar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalPagar.Location = new System.Drawing.Point(906, 312);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(83, 15);
            this.lblTotalPagar.TabIndex = 50;
            this.lblTotalPagar.Text = "Total a Pagar:";
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(688, 330);
            this.txtIva.Multiline = true;
            this.txtIva.Name = "txtIva";
            this.txtIva.ReadOnly = true;
            this.txtIva.Size = new System.Drawing.Size(75, 21);
            this.txtIva.TabIndex = 49;
            this.txtIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIva
            // 
            this.lblIva.AutoSize = true;
            this.lblIva.BackColor = System.Drawing.Color.Transparent;
            this.lblIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIva.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblIva.Location = new System.Drawing.Point(691, 312);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(39, 15);
            this.lblIva.TabIndex = 48;
            this.lblIva.Text = "I. V. A.";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Location = new System.Drawing.Point(579, 330);
            this.txtSubTotal.Multiline = true;
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.ReadOnly = true;
            this.txtSubTotal.Size = new System.Drawing.Size(75, 21);
            this.txtSubTotal.TabIndex = 47;
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSubTotal.Location = new System.Drawing.Point(576, 312);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(62, 15);
            this.lblSubTotal.TabIndex = 46;
            this.lblSubTotal.Text = "Sub Total:";
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(797, 330);
            this.txtServicio.Multiline = true;
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.ReadOnly = true;
            this.txtServicio.Size = new System.Drawing.Size(75, 21);
            this.txtServicio.TabIndex = 45;
            this.txtServicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblServicio
            // 
            this.lblServicio.AutoSize = true;
            this.lblServicio.BackColor = System.Drawing.Color.Transparent;
            this.lblServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServicio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblServicio.Location = new System.Drawing.Point(802, 312);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(53, 15);
            this.lblServicio.TabIndex = 44;
            this.lblServicio.Text = "Servicio:";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(470, 330);
            this.txtDescuento.Multiline = true;
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(75, 21);
            this.txtDescuento.TabIndex = 43;
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.BackColor = System.Drawing.Color.Transparent;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescuento.Location = new System.Drawing.Point(470, 312);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(69, 15);
            this.lblDescuento.TabIndex = 42;
            this.lblDescuento.Text = "Descuento:";
            // 
            // txtValorBruto
            // 
            this.txtValorBruto.Location = new System.Drawing.Point(361, 330);
            this.txtValorBruto.Multiline = true;
            this.txtValorBruto.Name = "txtValorBruto";
            this.txtValorBruto.ReadOnly = true;
            this.txtValorBruto.Size = new System.Drawing.Size(75, 21);
            this.txtValorBruto.TabIndex = 41;
            this.txtValorBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.BackColor = System.Drawing.Color.Transparent;
            this.lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblValor.Location = new System.Drawing.Point(358, 312);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(70, 15);
            this.lblValor.TabIndex = 40;
            this.lblValor.Text = "Valor Bruto:";
            // 
            // dgvReimpresionFactura
            // 
            this.dgvReimpresionFactura.AllowUserToAddRows = false;
            this.dgvReimpresionFactura.AllowUserToDeleteRows = false;
            this.dgvReimpresionFactura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReimpresionFactura.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCodigo,
            this.clmProducto,
            this.clmUnidad,
            this.clmCantidad,
            this.clmVUnidad,
            this.clmDescuento,
            this.clmDescto,
            this.colServicio,
            this.clmVTotal});
            this.dgvReimpresionFactura.Location = new System.Drawing.Point(44, 24);
            this.dgvReimpresionFactura.Name = "dgvReimpresionFactura";
            this.dgvReimpresionFactura.ReadOnly = true;
            this.dgvReimpresionFactura.RowHeadersWidth = 26;
            this.dgvReimpresionFactura.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReimpresionFactura.Size = new System.Drawing.Size(945, 271);
            this.dgvReimpresionFactura.TabIndex = 0;
            // 
            // clmCodigo
            // 
            this.clmCodigo.HeaderText = "Código";
            this.clmCodigo.Name = "clmCodigo";
            this.clmCodigo.ReadOnly = true;
            this.clmCodigo.Width = 80;
            // 
            // clmProducto
            // 
            this.clmProducto.HeaderText = "Producto";
            this.clmProducto.Name = "clmProducto";
            this.clmProducto.ReadOnly = true;
            this.clmProducto.Width = 220;
            // 
            // clmUnidad
            // 
            this.clmUnidad.HeaderText = "Unidad";
            this.clmUnidad.Name = "clmUnidad";
            this.clmUnidad.ReadOnly = true;
            this.clmUnidad.Width = 80;
            // 
            // clmCantidad
            // 
            this.clmCantidad.HeaderText = "Cantidad";
            this.clmCantidad.Name = "clmCantidad";
            this.clmCantidad.ReadOnly = true;
            this.clmCantidad.Width = 80;
            // 
            // clmVUnidad
            // 
            this.clmVUnidad.HeaderText = "V. Unidad";
            this.clmVUnidad.Name = "clmVUnidad";
            this.clmVUnidad.ReadOnly = true;
            this.clmVUnidad.Width = 80;
            // 
            // clmDescuento
            // 
            this.clmDescuento.HeaderText = "% Descuento";
            this.clmDescuento.Name = "clmDescuento";
            this.clmDescuento.ReadOnly = true;
            // 
            // clmDescto
            // 
            this.clmDescto.HeaderText = "Descto.";
            this.clmDescto.Name = "clmDescto";
            this.clmDescto.ReadOnly = true;
            this.clmDescto.Width = 80;
            // 
            // colServicio
            // 
            this.colServicio.HeaderText = "V. Servicio";
            this.colServicio.Name = "colServicio";
            this.colServicio.ReadOnly = true;
            this.colServicio.Width = 85;
            // 
            // clmVTotal
            // 
            this.clmVTotal.HeaderText = "V. Total";
            this.clmVTotal.Name = "clmVTotal";
            this.clmVTotal.ReadOnly = true;
            this.clmVTotal.Width = 80;
            // 
            // cmbTipoOrden
            // 
            this.cmbTipoOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoOrden.FormattingEnabled = true;
            this.cmbTipoOrden.Location = new System.Drawing.Point(339, 43);
            this.cmbTipoOrden.Name = "cmbTipoOrden";
            this.cmbTipoOrden.Size = new System.Drawing.Size(204, 24);
            this.cmbTipoOrden.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(336, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 16);
            this.label1.TabIndex = 63;
            this.label1.Text = "Seleccione el Tipo de Orden";
            // 
            // btnHasta
            // 
            this.btnHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHasta.Location = new System.Drawing.Point(241, 45);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(30, 24);
            this.btnHasta.TabIndex = 69;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // TxtHasta
            // 
            this.TxtHasta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TxtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHasta.Location = new System.Drawing.Point(100, 45);
            this.TxtHasta.Name = "TxtHasta";
            this.TxtHasta.ReadOnly = true;
            this.TxtHasta.Size = new System.Drawing.Size(135, 22);
            this.TxtHasta.TabIndex = 65;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 67;
            this.label2.Text = "Hasta:";
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.Location = new System.Drawing.Point(241, 18);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(30, 24);
            this.btnDesde.TabIndex = 68;
            this.btnDesde.Text = "...";
            this.btnDesde.UseVisualStyleBackColor = true;
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(100, 19);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.Size = new System.Drawing.Size(135, 22);
            this.txtDesde.TabIndex = 64;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(22, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 66;
            this.label3.Text = "A partir de:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.btnHasta);
            this.groupBox1.Controls.Add(this.TxtHasta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDesde);
            this.groupBox1.Controls.Add(this.txtDesde);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbTipoOrden);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(34, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(674, 100);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = global::Palatium.Properties.Resources.impresora;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(762, 515);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(66, 23);
            this.btnImprimir.TabIndex = 88;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(834, 515);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 87;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnExcel
            // 
            this.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcel.Location = new System.Drawing.Point(900, 515);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(60, 23);
            this.btnExcel.TabIndex = 90;
            this.btnExcel.Text = "Excel";
            this.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcel.UseVisualStyleBackColor = true;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(966, 515);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 23);
            this.btnSalir.TabIndex = 89;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.Control;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAceptar.Image = global::Palatium.Properties.Resources.buscar;
            this.btnAceptar.Location = new System.Drawing.Point(575, 15);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(85, 76);
            this.btnAceptar.TabIndex = 91;
            this.btnAceptar.Text = "Buscar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = false;
            // 
            // frmInformeVentasOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1034, 545);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Grb_listReCateraCobrar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInformeVentasOrden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de Ventas Por Orden";
            this.Load += new System.EventHandler(this.frmInformeVentasOrden_Load);
            this.Grb_listReCateraCobrar.ResumeLayout(false);
            this.Grb_listReCateraCobrar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReimpresionFactura)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReCateraCobrar;
        private System.Windows.Forms.TextBox txtTotalPagar;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.TextBox txtValorBruto;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.DataGridView dgvReimpresionFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVTotal;
        private MisControles.ComboDatos cmbTipoOrden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.TextBox TxtHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
    }
}