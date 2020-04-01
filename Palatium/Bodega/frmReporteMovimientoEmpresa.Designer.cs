namespace Palatium.Bodega
{
    partial class frmReporteMovimientoEmpresa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnEnviarAExcel = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valorEgresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblPersona = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.dbAyudaArticuloFinal = new Controles.Auxiliares.DB_Ayuda();
            this.btnOk = new System.Windows.Forms.Button();
            this.dbAyudaArticuloInicial = new Controles.Auxiliares.DB_Ayuda();
            this.txtFechaHasta = new System.Windows.Forms.TextBox();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.lblTipoMovimiento = new System.Windows.Forms.Label();
            this.cmbTipoMovimiento = new MisControles.ComboDatos();
            this.txtFechaDesde = new System.Windows.Forms.TextBox();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.pnlEmpresa.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviarAExcel
            // 
            this.btnEnviarAExcel.Location = new System.Drawing.Point(12, 421);
            this.btnEnviarAExcel.Name = "btnEnviarAExcel";
            this.btnEnviarAExcel.Size = new System.Drawing.Size(96, 23);
            this.btnEnviarAExcel.TabIndex = 123;
            this.btnEnviarAExcel.Text = "Enviar a Excel";
            this.btnEnviarAExcel.UseVisualStyleBackColor = true;
            this.btnEnviarAExcel.Click += new System.EventHandler(this.btnEnviarAExcel_Click);
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
            this.subtotal,
            this.stock,
            this.idProducto,
            this.valorEgresos,
            this.saldo,
            this.precio,
            this.observacion});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(12, 125);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(941, 288);
            this.dgvDetalleVenta.TabIndex = 120;
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
            this.unidad.HeaderText = "Referencia/Importacion";
            this.unidad.Name = "unidad";
            this.unidad.Width = 90;
            // 
            // cantidad
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle7;
            this.cantidad.HeaderText = "Ingresos";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 90;
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle8;
            this.precioUnitario.HeaderText = "Precio Ingresos";
            this.precioUnitario.Name = "precioUnitario";
            this.precioUnitario.Width = 60;
            // 
            // subtotal
            // 
            this.subtotal.HeaderText = "Valor Ingresos";
            this.subtotal.Name = "subtotal";
            this.subtotal.Width = 70;
            // 
            // stock
            // 
            this.stock.HeaderText = "Egresos";
            this.stock.Name = "stock";
            this.stock.Width = 70;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "Precio Egresos";
            this.idProducto.Name = "idProducto";
            this.idProducto.Width = 70;
            // 
            // valorEgresos
            // 
            this.valorEgresos.HeaderText = "Valor Egresos";
            this.valorEgresos.Name = "valorEgresos";
            // 
            // saldo
            // 
            this.saldo.HeaderText = "Saldo";
            this.saldo.Name = "saldo";
            this.saldo.Width = 70;
            // 
            // precio
            // 
            this.precio.HeaderText = "Precio";
            this.precio.Name = "precio";
            this.precio.Width = 70;
            // 
            // observacion
            // 
            this.observacion.HeaderText = "Observación";
            this.observacion.Name = "observacion";
            this.observacion.Width = 150;
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Location = new System.Drawing.Point(482, 50);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(69, 13);
            this.lblPersona.TabIndex = 118;
            this.lblPersona.Text = "Artículo Final";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(27, 62);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(74, 13);
            this.lblProveedor.TabIndex = 114;
            this.lblProveedor.Text = "Artículo Inicial";
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticuloFinal);
            this.pnlEmpresa.Controls.Add(this.btnOk);
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticuloInicial);
            this.pnlEmpresa.Controls.Add(this.txtFechaHasta);
            this.pnlEmpresa.Controls.Add(this.lblPersona);
            this.pnlEmpresa.Controls.Add(this.lblFechaHasta);
            this.pnlEmpresa.Controls.Add(this.lblTipoMovimiento);
            this.pnlEmpresa.Controls.Add(this.cmbTipoMovimiento);
            this.pnlEmpresa.Controls.Add(this.txtFechaDesde);
            this.pnlEmpresa.Controls.Add(this.lblFechaDesde);
            this.pnlEmpresa.Controls.Add(this.cmbOficina);
            this.pnlEmpresa.Controls.Add(this.lblEmpresa);
            this.pnlEmpresa.Location = new System.Drawing.Point(12, 12);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(941, 97);
            this.pnlEmpresa.TabIndex = 111;
            // 
            // dbAyudaArticuloFinal
            // 
            this.dbAyudaArticuloFinal.iId = 0;
            this.dbAyudaArticuloFinal.Location = new System.Drawing.Point(481, 68);
            this.dbAyudaArticuloFinal.Name = "dbAyudaArticuloFinal";
            this.dbAyudaArticuloFinal.sCodigo = null;
            this.dbAyudaArticuloFinal.Size = new System.Drawing.Size(457, 21);
            this.dbAyudaArticuloFinal.sNombre = null;
            this.dbAyudaArticuloFinal.TabIndex = 135;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(751, 18);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(34, 23);
            this.btnOk.TabIndex = 111;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dbAyudaArticuloInicial
            // 
            this.dbAyudaArticuloInicial.iId = 0;
            this.dbAyudaArticuloInicial.Location = new System.Drawing.Point(12, 68);
            this.dbAyudaArticuloInicial.Name = "dbAyudaArticuloInicial";
            this.dbAyudaArticuloInicial.sCodigo = null;
            this.dbAyudaArticuloInicial.Size = new System.Drawing.Size(463, 21);
            this.dbAyudaArticuloInicial.sNombre = null;
            this.dbAyudaArticuloInicial.TabIndex = 134;
            // 
            // txtFechaHasta
            // 
            this.txtFechaHasta.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaHasta.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaHasta.Location = new System.Drawing.Point(608, 20);
            this.txtFechaHasta.Name = "txtFechaHasta";
            this.txtFechaHasta.Size = new System.Drawing.Size(104, 20);
            this.txtFechaHasta.TabIndex = 64;
            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Location = new System.Drawing.Point(605, 5);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new System.Drawing.Size(68, 13);
            this.lblFechaHasta.TabIndex = 63;
            this.lblFechaHasta.Text = "Fecha Hasta";
            // 
            // lblTipoMovimiento
            // 
            this.lblTipoMovimiento.AutoSize = true;
            this.lblTipoMovimiento.Location = new System.Drawing.Point(251, 3);
            this.lblTipoMovimiento.Name = "lblTipoMovimiento";
            this.lblTipoMovimiento.Size = new System.Drawing.Size(100, 13);
            this.lblTipoMovimiento.TabIndex = 62;
            this.lblTipoMovimiento.Text = "Tipo de Movimiento";
            // 
            // cmbTipoMovimiento
            // 
            this.cmbTipoMovimiento.BackColor = System.Drawing.SystemColors.Window;
            this.cmbTipoMovimiento.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbTipoMovimiento.FormattingEnabled = true;
            this.cmbTipoMovimiento.Location = new System.Drawing.Point(251, 19);
            this.cmbTipoMovimiento.Name = "cmbTipoMovimiento";
            this.cmbTipoMovimiento.Size = new System.Drawing.Size(181, 21);
            this.cmbTipoMovimiento.TabIndex = 61;
            // 
            // txtFechaDesde
            // 
            this.txtFechaDesde.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaDesde.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaDesde.Location = new System.Drawing.Point(495, 20);
            this.txtFechaDesde.Name = "txtFechaDesde";
            this.txtFechaDesde.Size = new System.Drawing.Size(104, 20);
            this.txtFechaDesde.TabIndex = 60;
            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Location = new System.Drawing.Point(492, 4);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new System.Drawing.Size(71, 13);
            this.lblFechaDesde.TabIndex = 55;
            this.lblFechaDesde.Text = "Fecha Desde";
            // 
            // cmbOficina
            // 
            this.cmbOficina.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOficina.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOficina.FormattingEnabled = true;
            this.cmbOficina.Location = new System.Drawing.Point(12, 18);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(181, 21);
            this.cmbOficina.TabIndex = 48;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(12, 4);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(48, 13);
            this.lblEmpresa.TabIndex = 55;
            this.lblEmpresa.Text = "Empresa";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(829, 421);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 122;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = global::Palatium.Properties.Resources.salida;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(895, 421);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(60, 23);
            this.btnCerrar.TabIndex = 121;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // frmReporteMovimientoEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(969, 456);
            this.Controls.Add(this.btnEnviarAExcel);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.pnlEmpresa);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReporteMovimientoEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Movimientos de Artículos por  Empresa";
            this.Load += new System.EventHandler(this.frmReporteMovimientoEmpresa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.pnlEmpresa.ResumeLayout(false);
            this.pnlEmpresa.PerformLayout();
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
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtFechaHasta;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.Label lblTipoMovimiento;
        private MisControles.ComboDatos cmbTipoMovimiento;
        private System.Windows.Forms.TextBox txtFechaDesde;
        private System.Windows.Forms.Label lblFechaDesde;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorEgresos;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacion;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloFinal;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloInicial;
    }
}