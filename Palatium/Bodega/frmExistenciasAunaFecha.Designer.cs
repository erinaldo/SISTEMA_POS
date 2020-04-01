namespace Palatium.Bodega
{
    partial class frmExistenciasAunaFecha
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnEnviarAExcel = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.lblPersona = new System.Windows.Forms.Label();
            this.lblProveedor = new System.Windows.Forms.Label();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.dbAyudaArticuloFinal = new Controles.Auxiliares.DB_Ayuda();
            this.dbAyudaArticuloInicial = new Controles.Auxiliares.DB_Ayuda();
            this.dbAyudaFamiliaArticulo = new Controles.Auxiliares.DB_Ayuda();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.chkIncluirSaldo = new System.Windows.Forms.CheckBox();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.lblNombreDelProducto = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbBodega = new MisControles.ComboDatos();
            this.txtFechaCorte = new System.Windows.Forms.TextBox();
            this.lblFechaCorte = new System.Windows.Forms.Label();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMinimo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMaximo = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.saldoActual,
            this.colMinimo,
            this.colMaximo});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(12, 142);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.Size = new System.Drawing.Size(754, 271);
            this.dgvDetalleVenta.TabIndex = 120;
            // 
            // lblPersona
            // 
            this.lblPersona.AutoSize = true;
            this.lblPersona.Location = new System.Drawing.Point(344, 88);
            this.lblPersona.Name = "lblPersona";
            this.lblPersona.Size = new System.Drawing.Size(69, 13);
            this.lblPersona.TabIndex = 118;
            this.lblPersona.Text = "Artículo Final";
            // 
            // lblProveedor
            // 
            this.lblProveedor.AutoSize = true;
            this.lblProveedor.Location = new System.Drawing.Point(21, 86);
            this.lblProveedor.Name = "lblProveedor";
            this.lblProveedor.Size = new System.Drawing.Size(74, 13);
            this.lblProveedor.TabIndex = 114;
            this.lblProveedor.Text = "Artículo Inicial";
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticuloFinal);
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticuloInicial);
            this.pnlEmpresa.Controls.Add(this.dbAyudaFamiliaArticulo);
            this.pnlEmpresa.Controls.Add(this.lblFamilia);
            this.pnlEmpresa.Controls.Add(this.chkIncluirSaldo);
            this.pnlEmpresa.Controls.Add(this.txtNombreProducto);
            this.pnlEmpresa.Controls.Add(this.lblNombreDelProducto);
            this.pnlEmpresa.Controls.Add(this.cmbEmpresa);
            this.pnlEmpresa.Controls.Add(this.lblEmpresa);
            this.pnlEmpresa.Controls.Add(this.btnOk);
            this.pnlEmpresa.Controls.Add(this.cmbBodega);
            this.pnlEmpresa.Controls.Add(this.txtFechaCorte);
            this.pnlEmpresa.Controls.Add(this.lblFechaCorte);
            this.pnlEmpresa.Controls.Add(this.lblBodega);
            this.pnlEmpresa.Controls.Add(this.cmbOficina);
            this.pnlEmpresa.Controls.Add(this.lblOficina);
            this.pnlEmpresa.Location = new System.Drawing.Point(12, 6);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(754, 130);
            this.pnlEmpresa.TabIndex = 111;
            // 
            // dbAyudaArticuloFinal
            // 
            this.dbAyudaArticuloFinal.iId = 0;
            this.dbAyudaArticuloFinal.Location = new System.Drawing.Point(335, 96);
            this.dbAyudaArticuloFinal.Name = "dbAyudaArticuloFinal";
            this.dbAyudaArticuloFinal.sCodigo = null;
            this.dbAyudaArticuloFinal.Size = new System.Drawing.Size(364, 21);
            this.dbAyudaArticuloFinal.sNombre = null;
            this.dbAyudaArticuloFinal.TabIndex = 131;
            // 
            // dbAyudaArticuloInicial
            // 
            this.dbAyudaArticuloInicial.iId = 0;
            this.dbAyudaArticuloInicial.Location = new System.Drawing.Point(12, 96);
            this.dbAyudaArticuloInicial.Name = "dbAyudaArticuloInicial";
            this.dbAyudaArticuloInicial.sCodigo = null;
            this.dbAyudaArticuloInicial.Size = new System.Drawing.Size(324, 21);
            this.dbAyudaArticuloInicial.sNombre = null;
            this.dbAyudaArticuloInicial.TabIndex = 130;
            // 
            // dbAyudaFamiliaArticulo
            // 
            this.dbAyudaFamiliaArticulo.iId = 0;
            this.dbAyudaFamiliaArticulo.Location = new System.Drawing.Point(12, 56);
            this.dbAyudaFamiliaArticulo.Name = "dbAyudaFamiliaArticulo";
            this.dbAyudaFamiliaArticulo.sCodigo = null;
            this.dbAyudaFamiliaArticulo.Size = new System.Drawing.Size(379, 21);
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
            // chkIncluirSaldo
            // 
            this.chkIncluirSaldo.AutoSize = true;
            this.chkIncluirSaldo.Location = new System.Drawing.Point(597, 59);
            this.chkIncluirSaldo.Name = "chkIncluirSaldo";
            this.chkIncluirSaldo.Size = new System.Drawing.Size(102, 17);
            this.chkIncluirSaldo.TabIndex = 126;
            this.chkIncluirSaldo.Text = "Incluir Saldo = 0";
            this.chkIncluirSaldo.UseVisualStyleBackColor = true;
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombreProducto.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNombreProducto.Location = new System.Drawing.Point(397, 56);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.Size = new System.Drawing.Size(175, 20);
            this.txtNombreProducto.TabIndex = 125;
            // 
            // lblNombreDelProducto
            // 
            this.lblNombreDelProducto.AutoSize = true;
            this.lblNombreDelProducto.Location = new System.Drawing.Point(394, 42);
            this.lblNombreDelProducto.Name = "lblNombreDelProducto";
            this.lblNombreDelProducto.Size = new System.Drawing.Size(109, 13);
            this.lblNombreDelProducto.TabIndex = 124;
            this.lblNombreDelProducto.Text = "Nombre Del Producto";
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
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(704, 95);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(40, 23);
            this.btnOk.TabIndex = 111;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbBodega
            // 
            this.cmbBodega.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBodega.Enabled = false;
            this.cmbBodega.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(295, 17);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(138, 21);
            this.cmbBodega.TabIndex = 65;
            // 
            // txtFechaCorte
            // 
            this.txtFechaCorte.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaCorte.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaCorte.Location = new System.Drawing.Point(455, 19);
            this.txtFechaCorte.Name = "txtFechaCorte";
            this.txtFechaCorte.Size = new System.Drawing.Size(77, 20);
            this.txtFechaCorte.TabIndex = 60;
            // 
            // lblFechaCorte
            // 
            this.lblFechaCorte.AutoSize = true;
            this.lblFechaCorte.Location = new System.Drawing.Point(452, 5);
            this.lblFechaCorte.Name = "lblFechaCorte";
            this.lblFechaCorte.Size = new System.Drawing.Size(65, 13);
            this.lblFechaCorte.TabIndex = 55;
            this.lblFechaCorte.Text = "Fecha Corte";
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(292, 2);
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
            this.cmbOficina.Location = new System.Drawing.Point(160, 18);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(129, 21);
            this.cmbOficina.TabIndex = 48;
            this.cmbOficina.SelectedIndexChanged += new System.EventHandler(this.cmbOficina_SelectedIndexChanged);
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Location = new System.Drawing.Point(157, 4);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(71, 13);
            this.lblOficina.TabIndex = 55;
            this.lblOficina.Text = "Oficina/Local";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(650, 421);
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
            this.btnCerrar.Location = new System.Drawing.Point(716, 421);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(60, 23);
            this.btnCerrar.TabIndex = 121;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Código";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 118;
            // 
            // descripcionProducto
            // 
            this.descripcionProducto.HeaderText = "Descripción";
            this.descripcionProducto.Name = "descripcionProducto";
            this.descripcionProducto.Width = 330;
            // 
            // unidad
            // 
            this.unidad.HeaderText = "Ubic.";
            this.unidad.Name = "unidad";
            this.unidad.Width = 70;
            // 
            // cantidad
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle3;
            this.cantidad.HeaderText = "Unid.";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 70;
            // 
            // saldoActual
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.saldoActual.DefaultCellStyle = dataGridViewCellStyle4;
            this.saldoActual.HeaderText = "Saldo Actual";
            this.saldoActual.Name = "saldoActual";
            // 
            // colMinimo
            // 
            this.colMinimo.HeaderText = "StockMinimo";
            this.colMinimo.Name = "colMinimo";
            this.colMinimo.Visible = false;
            // 
            // colMaximo
            // 
            this.colMaximo.HeaderText = "StockMaximo";
            this.colMaximo.Name = "colMaximo";
            this.colMaximo.Visible = false;
            // 
            // frmExistenciasAunaFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(777, 453);
            this.Controls.Add(this.btnEnviarAExcel);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.lblPersona);
            this.Controls.Add(this.lblProveedor);
            this.Controls.Add(this.pnlEmpresa);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExistenciasAunaFecha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existencia de Materiales a una Fecha de Corte";
            this.Load += new System.EventHandler(this.frmExistenciasAunaFecha_Load);
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
        private MisControles.ComboDatos cmbBodega;
        private System.Windows.Forms.TextBox txtFechaCorte;
        private System.Windows.Forms.Label lblFechaCorte;
        private System.Windows.Forms.Label lblBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblOficina;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.CheckBox chkIncluirSaldo;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.Label lblNombreDelProducto;
        private System.Windows.Forms.Label lblFamilia;
        private Controles.Auxiliares.DB_Ayuda dbAyudaFamiliaArticulo;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloFinal;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticuloInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMinimo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaximo;
    }
}