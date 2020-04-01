namespace Palatium.Bodega
{
    partial class frmKardexPorBodega
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
            this.btnEnviarAExcel = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPersona = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.txtFechaHasta = new MetroFramework.Controls.MetroDateTime();
            this.txtFechaDesde = new MetroFramework.Controls.MetroDateTime();
            this.lblTipoMovimiento = new System.Windows.Forms.Label();
            this.cmbTipoMovimiento = new MisControles.ComboDatos();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cmbBodega = new MisControles.ComboDatos();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbFechasProductos = new System.Windows.Forms.RadioButton();
            this.rbRangoFechas = new System.Windows.Forms.RadioButton();
            this.dbAyudaArticuloFinal = new Controles.Auxiliares.DB_Ayuda();
            this.dbAyudaArticuloInicial = new Controles.Auxiliares.DB_Ayuda();
            this.dbAyudaFamiliaArticulo = new Controles.Auxiliares.DB_Ayuda();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.pnlEmpresa.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviarAExcel
            // 
            this.btnEnviarAExcel.Location = new System.Drawing.Point(3, 485);
            this.btnEnviarAExcel.Name = "btnEnviarAExcel";
            this.btnEnviarAExcel.Size = new System.Drawing.Size(96, 23);
            this.btnEnviarAExcel.TabIndex = 130;
            this.btnEnviarAExcel.Text = "Enviar a Excel";
            this.btnEnviarAExcel.UseVisualStyleBackColor = true;
            this.btnEnviarAExcel.Click += new System.EventHandler(this.btnEnviarAExcel_Click);
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.AllowUserToAddRows = false;
            this.dgvDetalleVenta.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoProducto,
            this.descripcionProducto,
            this.unidad,
            this.cantidad,
            this.precioUnitario,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(3, 198);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(754, 281);
            this.dgvDetalleVenta.TabIndex = 127;
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Fecha";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 118;
            // 
            // descripcionProducto
            // 
            this.descripcionProducto.HeaderText = "Movimiento";
            this.descripcionProducto.Name = "descripcionProducto";
            this.descripcionProducto.Width = 347;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Bd";
            this.unidad.Name = "unidad";
            this.unidad.Width = 70;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.HeaderText = "Referencia";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 70;
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle2;
            this.precioUnitario.HeaderText = "Código";
            this.precioUnitario.Name = "precioUnitario";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Unidad de";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Ingresos";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Costo Unitario";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Ingresos / Total";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Egresos";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Costo Unitario";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Egresos / Total";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Saldo";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Precio Promedio";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Total";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Código";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Cliente/Proveedor";
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Observación";
            this.Column13.Name = "Column13";
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Location = new System.Drawing.Point(332, 54);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(69, 13);
            this.lblPersona.TabIndex = 126;
            this.lblPersona.Text = "Artículo Final";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(9, 52);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(74, 13);
            this.lblProveedor.TabIndex = 125;
            this.lblProveedor.Text = "Artículo Inicial";
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlEmpresa.Controls.Add(this.txtFechaHasta);
            this.pnlEmpresa.Controls.Add(this.txtFechaDesde);
            this.pnlEmpresa.Controls.Add(this.lblTipoMovimiento);
            this.pnlEmpresa.Controls.Add(this.cmbTipoMovimiento);
            this.pnlEmpresa.Controls.Add(this.label1);
            this.pnlEmpresa.Controls.Add(this.label2);
            this.pnlEmpresa.Controls.Add(this.cmbEmpresa);
            this.pnlEmpresa.Controls.Add(this.lblEmpresa);
            this.pnlEmpresa.Controls.Add(this.cmbBodega);
            this.pnlEmpresa.Controls.Add(this.lblBodega);
            this.pnlEmpresa.Controls.Add(this.cmbOficina);
            this.pnlEmpresa.Controls.Add(this.lblOficina);
            this.pnlEmpresa.Location = new System.Drawing.Point(2, 3);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(754, 87);
            this.pnlEmpresa.TabIndex = 124;
            // 
            // txtFechaHasta
            // 
            this.txtFechaHasta.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.txtFechaHasta.Location = new System.Drawing.Point(468, 50);
            this.txtFechaHasta.MinimumSize = new System.Drawing.Size(0, 25);
            this.txtFechaHasta.Name = "txtFechaHasta";
            this.txtFechaHasta.Size = new System.Drawing.Size(270, 25);
            this.txtFechaHasta.TabIndex = 146;
            // 
            // txtFechaDesde
            // 
            this.txtFechaDesde.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.txtFechaDesde.Location = new System.Drawing.Point(93, 48);
            this.txtFechaDesde.MinimumSize = new System.Drawing.Size(0, 25);
            this.txtFechaDesde.Name = "txtFechaDesde";
            this.txtFechaDesde.Size = new System.Drawing.Size(270, 25);
            this.txtFechaDesde.TabIndex = 145;
            // 
            // lblTipoMovimiento
            // 
            this.lblTipoMovimiento.AutoSize = true;
            this.lblTipoMovimiento.Location = new System.Drawing.Point(560, 3);
            this.lblTipoMovimiento.Name = "lblTipoMovimiento";
            this.lblTipoMovimiento.Size = new System.Drawing.Size(100, 13);
            this.lblTipoMovimiento.TabIndex = 143;
            this.lblTipoMovimiento.Text = "Tipo de Movimiento";
            // 
            // cmbTipoMovimiento
            // 
            this.cmbTipoMovimiento.BackColor = System.Drawing.SystemColors.Window;
            this.cmbTipoMovimiento.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbTipoMovimiento.FormattingEnabled = true;
            this.cmbTipoMovimiento.Location = new System.Drawing.Point(563, 17);
            this.cmbTipoMovimiento.Name = "cmbTipoMovimiento";
            this.cmbTipoMovimiento.Size = new System.Drawing.Size(175, 21);
            this.cmbTipoMovimiento.TabIndex = 142;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(388, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 140;
            this.label1.Text = "Fecha Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 138;
            this.label2.Text = "Fecha Desde:";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEmpresa.Enabled = false;
            this.cmbEmpresa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(12, 17);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(129, 21);
            this.cmbEmpresa.TabIndex = 112;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(9, 3);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(48, 13);
            this.lblEmpresa.TabIndex = 113;
            this.lblEmpresa.Text = "Empresa";
            // 
            // cmbBodega
            // 
            this.cmbBodega.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBodega.Enabled = false;
            this.cmbBodega.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(328, 17);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(175, 21);
            this.cmbBodega.TabIndex = 65;
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(329, 3);
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
            this.cmbOficina.Location = new System.Drawing.Point(147, 17);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(175, 21);
            this.cmbOficina.TabIndex = 48;
            this.cmbOficina.SelectedIndexChanged += new System.EventHandler(this.cmbOficina_SelectedIndexChanged);
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Location = new System.Drawing.Point(144, 3);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(71, 13);
            this.lblOficina.TabIndex = 55;
            this.lblOficina.Text = "Oficina/Local";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbFechasProductos);
            this.panel1.Controls.Add(this.rbRangoFechas);
            this.panel1.Location = new System.Drawing.Point(335, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 35);
            this.panel1.TabIndex = 144;
            // 
            // rbFechasProductos
            // 
            this.rbFechasProductos.AutoSize = true;
            this.rbFechasProductos.Location = new System.Drawing.Point(139, 10);
            this.rbFechasProductos.Name = "rbFechasProductos";
            this.rbFechasProductos.Size = new System.Drawing.Size(184, 17);
            this.rbFechasProductos.TabIndex = 1;
            this.rbFechasProductos.Text = "Rango de Fechas y de Productos";
            this.rbFechasProductos.UseVisualStyleBackColor = true;
            // 
            // rbRangoFechas
            // 
            this.rbRangoFechas.AutoSize = true;
            this.rbRangoFechas.Checked = true;
            this.rbRangoFechas.Location = new System.Drawing.Point(19, 10);
            this.rbRangoFechas.Name = "rbRangoFechas";
            this.rbRangoFechas.Size = new System.Drawing.Size(119, 17);
            this.rbRangoFechas.TabIndex = 0;
            this.rbRangoFechas.TabStop = true;
            this.rbRangoFechas.Text = "Solo Rango Fechas";
            this.rbRangoFechas.UseVisualStyleBackColor = true;
            // 
            // dbAyudaArticuloFinal
            // 
            this.dbAyudaArticuloFinal.iId = 0;
            this.dbAyudaArticuloFinal.Location = new System.Drawing.Point(331, 68);
            this.dbAyudaArticuloFinal.Name = "dbAyudaArticuloFinal";
            this.dbAyudaArticuloFinal.sCodigo = null;
            this.dbAyudaArticuloFinal.Size = new System.Drawing.Size(413, 21);
            this.dbAyudaArticuloFinal.sNombre = null;
            this.dbAyudaArticuloFinal.TabIndex = 131;
            // 
            // dbAyudaArticuloInicial
            // 
            this.dbAyudaArticuloInicial.iId = 0;
            this.dbAyudaArticuloInicial.Location = new System.Drawing.Point(12, 68);
            this.dbAyudaArticuloInicial.Name = "dbAyudaArticuloInicial";
            this.dbAyudaArticuloInicial.sCodigo = null;
            this.dbAyudaArticuloInicial.Size = new System.Drawing.Size(324, 21);
            this.dbAyudaArticuloInicial.sNombre = null;
            this.dbAyudaArticuloInicial.TabIndex = 130;
            // 
            // dbAyudaFamiliaArticulo
            // 
            this.dbAyudaFamiliaArticulo.iId = 0;
            this.dbAyudaFamiliaArticulo.Location = new System.Drawing.Point(11, 28);
            this.dbAyudaFamiliaArticulo.Name = "dbAyudaFamiliaArticulo";
            this.dbAyudaFamiliaArticulo.sCodigo = null;
            this.dbAyudaFamiliaArticulo.Size = new System.Drawing.Size(314, 21);
            this.dbAyudaFamiliaArticulo.sNombre = null;
            this.dbAyudaFamiliaArticulo.TabIndex = 124;
            // 
            // lblFamilia
            // 
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(8, 13);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(94, 13);
            this.lblFamilia.TabIndex = 129;
            this.lblFamilia.Text = "Familia de Artículo";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOk.Location = new System.Drawing.Point(679, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(65, 42);
            this.btnOk.TabIndex = 111;
            this.btnOk.Text = "OK";
            this.ttMensaje.SetToolTip(this.btnOk, "Clic aquí para consultar");
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(630, 485);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 129;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = global::Palatium.Properties.Resources.salida;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(696, 485);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(60, 23);
            this.btnCerrar.TabIndex = 128;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.lblFamilia);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.dbAyudaFamiliaArticulo);
            this.panel2.Controls.Add(this.dbAyudaArticuloInicial);
            this.panel2.Controls.Add(this.dbAyudaArticuloFinal);
            this.panel2.Controls.Add(this.lblPersona);
            this.panel2.Controls.Add(this.lblProveedor);
            this.panel2.Location = new System.Drawing.Point(2, 90);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(754, 102);
            this.panel2.TabIndex = 147;
            // 
            // frmKardexPorBodega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(762, 520);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnEnviarAExcel);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.pnlEmpresa);
            this.MaximizeBox = false;
            this.Name = "frmKardexPorBodega";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kardex por Bodega";
            this.Load += new System.EventHandler(this.frmKardexPorBodega_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.pnlEmpresa.ResumeLayout(false);
            this.pnlEmpresa.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnviarAExcel;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Label lblPersona;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Panel pnlEmpresa;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloFinal;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloInicial;
        private Controles.Auxiliares.DB_Ayuda dbAyudaFamiliaArticulo;
        private System.Windows.Forms.Label lblFamilia;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Button btnOk;
        private MisControles.ComboDatos cmbBodega;
        private System.Windows.Forms.Label lblBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblOficina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbFechasProductos;
        private System.Windows.Forms.RadioButton rbRangoFechas;
        private System.Windows.Forms.Label lblTipoMovimiento;
        private MisControles.ComboDatos cmbTipoMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private MetroFramework.Controls.MetroDateTime txtFechaDesde;
        private MetroFramework.Controls.MetroDateTime txtFechaHasta;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}