namespace Palatium.Bodega
{
    partial class frmKardexPorEmpresa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnEnviarAExcel = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
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
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPersona = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbFechasProductos = new System.Windows.Forms.RadioButton();
            this.rbRangoFechas = new System.Windows.Forms.RadioButton();
            this.lblTipoMovimiento = new System.Windows.Forms.Label();
            this.cmbTipoMovimiento = new MisControles.ComboDatos();
            this.txtFechaHasta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaDesde = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dbAyudaArticuloFinal = new Controles.Auxiliares.DB_Ayuda();
            this.dbAyudaArticuloInicial = new Controles.Auxiliares.DB_Ayuda();
            this.dbAyudaFamiliaArticulo = new Controles.Auxiliares.DB_Ayuda();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.pnlEmpresa.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviarAExcel
            // 
            this.btnEnviarAExcel.Location = new System.Drawing.Point(3, 465);
            this.btnEnviarAExcel.Name = "btnEnviarAExcel";
            this.btnEnviarAExcel.Size = new System.Drawing.Size(96, 23);
            this.btnEnviarAExcel.TabIndex = 137;
            this.btnEnviarAExcel.Text = "Enviar a Excel";
            this.btnEnviarAExcel.UseVisualStyleBackColor = true;
            this.btnEnviarAExcel.Click += new System.EventHandler(this.btnEnviarAExcel_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(888, 465);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 136;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = global::Palatium.Properties.Resources.salida;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(954, 465);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(60, 23);
            this.btnCerrar.TabIndex = 135;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // dgvDetalleVenta
            // 
            this.dgvDetalleVenta.AllowUserToAddRows = false;
            this.dgvDetalleVenta.BackgroundColor = System.Drawing.SystemColors.ControlDark;
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
            this.Column13,
            this.Column14,
            this.Column15});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(3, 139);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(1011, 316);
            this.dgvDetalleVenta.TabIndex = 134;
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Fecha";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 80;
            // 
            // descripcionProducto
            // 
            this.descripcionProducto.HeaderText = "Movimiento";
            this.descripcionProducto.Name = "descripcionProducto";
            this.descripcionProducto.Width = 90;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Bd";
            this.unidad.Name = "unidad";
            this.unidad.Width = 30;
            // 
            // cantidad
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad.HeaderText = "Referencia";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 90;
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle6;
            this.precioUnitario.HeaderText = "Código";
            this.precioUnitario.Name = "precioUnitario";
            this.precioUnitario.Width = 90;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Descripción";
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Ingresos";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Costo Unitario";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Ingresos / Total";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Egresos";
            this.Column5.Name = "Column5";
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Costo Unitario";
            this.Column6.Name = "Column6";
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Egresos / Total";
            this.Column7.Name = "Column7";
            this.Column7.Width = 60;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Saldo";
            this.Column8.Name = "Column8";
            this.Column8.Width = 60;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Precio Promedio";
            this.Column9.Name = "Column9";
            this.Column9.Width = 60;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Total";
            this.Column10.Name = "Column10";
            this.Column10.Width = 60;
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
            this.Column12.Width = 150;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Observación";
            this.Column13.Name = "Column13";
            this.Column13.Width = 150;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Mov. Asociado";
            this.Column14.Name = "Column14";
            this.Column14.Width = 80;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Núm. Interno";
            this.Column15.Name = "Column15";
            this.Column15.Width = 90;
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Location = new System.Drawing.Point(479, 82);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(69, 13);
            this.lblPersona.TabIndex = 133;
            this.lblPersona.Text = "Artículo Final";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(12, 83);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(74, 13);
            this.lblProveedor.TabIndex = 132;
            this.lblProveedor.Text = "Artículo Inicial";
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.Controls.Add(this.panel1);
            this.pnlEmpresa.Controls.Add(this.lblTipoMovimiento);
            this.pnlEmpresa.Controls.Add(this.cmbTipoMovimiento);
            this.pnlEmpresa.Controls.Add(this.txtFechaHasta);
            this.pnlEmpresa.Controls.Add(this.lblPersona);
            this.pnlEmpresa.Controls.Add(this.label1);
            this.pnlEmpresa.Controls.Add(this.txtFechaDesde);
            this.pnlEmpresa.Controls.Add(this.label2);
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticuloFinal);
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticuloInicial);
            this.pnlEmpresa.Controls.Add(this.dbAyudaFamiliaArticulo);
            this.pnlEmpresa.Controls.Add(this.lblFamilia);
            this.pnlEmpresa.Controls.Add(this.cmbEmpresa);
            this.pnlEmpresa.Controls.Add(this.lblEmpresa);
            this.pnlEmpresa.Controls.Add(this.btnOk);
            this.pnlEmpresa.Location = new System.Drawing.Point(3, 3);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(1011, 130);
            this.pnlEmpresa.TabIndex = 131;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbFechasProductos);
            this.panel1.Controls.Add(this.rbRangoFechas);
            this.panel1.Location = new System.Drawing.Point(638, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 34);
            this.panel1.TabIndex = 144;
            // 
            // rbFechasProductos
            // 
            this.rbFechasProductos.Location = new System.Drawing.Point(146, 7);
            this.rbFechasProductos.Name = "rbFechasProductos";
            this.rbFechasProductos.Size = new System.Drawing.Size(193, 19);
            this.rbFechasProductos.TabIndex = 1;
            this.rbFechasProductos.Text = "Rango de Fechas y de Productos";
            this.rbFechasProductos.UseVisualStyleBackColor = true;
            // 
            // rbRangoFechas
            // 
            this.rbRangoFechas.Checked = true;
            this.rbRangoFechas.Location = new System.Drawing.Point(10, 7);
            this.rbRangoFechas.Name = "rbRangoFechas";
            this.rbRangoFechas.Size = new System.Drawing.Size(130, 19);
            this.rbRangoFechas.TabIndex = 0;
            this.rbRangoFechas.TabStop = true;
            this.rbRangoFechas.Text = "Solo Rango  Fechas";
            this.rbRangoFechas.UseVisualStyleBackColor = true;
            // 
            // lblTipoMovimiento
            // 
            this.lblTipoMovimiento.AutoSize = true;
            this.lblTipoMovimiento.Location = new System.Drawing.Point(201, 2);
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
            this.cmbTipoMovimiento.Location = new System.Drawing.Point(204, 17);
            this.cmbTipoMovimiento.Name = "cmbTipoMovimiento";
            this.cmbTipoMovimiento.Size = new System.Drawing.Size(202, 21);
            this.cmbTipoMovimiento.TabIndex = 142;
            // 
            // txtFechaHasta
            // 
            this.txtFechaHasta.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaHasta.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaHasta.Location = new System.Drawing.Point(504, 17);
            this.txtFechaHasta.Name = "txtFechaHasta";
            this.txtFechaHasta.Size = new System.Drawing.Size(77, 20);
            this.txtFechaHasta.TabIndex = 141;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(501, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 140;
            this.label1.Text = "Fecha Hasta";
            // 
            // txtFechaDesde
            // 
            this.txtFechaDesde.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaDesde.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaDesde.Location = new System.Drawing.Point(421, 17);
            this.txtFechaDesde.Name = "txtFechaDesde";
            this.txtFechaDesde.Size = new System.Drawing.Size(77, 20);
            this.txtFechaDesde.TabIndex = 139;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(418, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 138;
            this.label2.Text = "Fecha Desde";
            // 
            // dbAyudaArticuloFinal
            // 
            this.dbAyudaArticuloFinal.iId = 0;
            this.dbAyudaArticuloFinal.Location = new System.Drawing.Point(479, 96);
            this.dbAyudaArticuloFinal.Name = "dbAyudaArticuloFinal";
            this.dbAyudaArticuloFinal.sCodigo = null;
            this.dbAyudaArticuloFinal.Size = new System.Drawing.Size(461, 21);
            this.dbAyudaArticuloFinal.sNombre = null;
            this.dbAyudaArticuloFinal.TabIndex = 131;
            // 
            // dbAyudaArticuloInicial
            // 
            this.dbAyudaArticuloInicial.iId = 0;
            this.dbAyudaArticuloInicial.Location = new System.Drawing.Point(12, 96);
            this.dbAyudaArticuloInicial.Name = "dbAyudaArticuloInicial";
            this.dbAyudaArticuloInicial.sCodigo = null;
            this.dbAyudaArticuloInicial.Size = new System.Drawing.Size(464, 21);
            this.dbAyudaArticuloInicial.sNombre = null;
            this.dbAyudaArticuloInicial.TabIndex = 130;
            // 
            // dbAyudaFamiliaArticulo
            // 
            this.dbAyudaFamiliaArticulo.iId = 0;
            this.dbAyudaFamiliaArticulo.Location = new System.Drawing.Point(12, 56);
            this.dbAyudaFamiliaArticulo.Name = "dbAyudaFamiliaArticulo";
            this.dbAyudaFamiliaArticulo.sCodigo = null;
            this.dbAyudaFamiliaArticulo.Size = new System.Drawing.Size(464, 21);
            this.dbAyudaFamiliaArticulo.sNombre = null;
            this.dbAyudaFamiliaArticulo.TabIndex = 124;
            // 
            // lblFamilia
            // 
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(9, 41);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(94, 13);
            this.lblFamilia.TabIndex = 129;
            this.lblFamilia.Text = "Familia de Artículo";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEmpresa.Enabled = false;
            this.cmbEmpresa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(12, 17);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(173, 21);
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
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(949, 94);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(44, 23);
            this.btnOk.TabIndex = 111;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmKardexPorEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 497);
            this.Controls.Add(this.btnEnviarAExcel);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.pnlEmpresa);
            this.Name = "frmKardexPorEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kardex por Empresa";
            this.Load += new System.EventHandler(this.frmKardexPorEmpresa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.pnlEmpresa.ResumeLayout(false);
            this.pnlEmpresa.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnviarAExcel;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Label lblPersona;
        private System.Windows.Forms.Label lblProveedor;
        private System.Windows.Forms.Panel pnlEmpresa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbFechasProductos;
        private System.Windows.Forms.RadioButton rbRangoFechas;
        private System.Windows.Forms.Label lblTipoMovimiento;
        private MisControles.ComboDatos cmbTipoMovimiento;
        private System.Windows.Forms.TextBox txtFechaHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFechaDesde;
        private System.Windows.Forms.Label label2;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloFinal;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloInicial;
        private Controles.Auxiliares.DB_Ayuda dbAyudaFamiliaArticulo;
        private System.Windows.Forms.Label lblFamilia;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Button btnOk;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;

    }
}