namespace Palatium.Bodega
{
    partial class frmExistenciasRangodeFechas
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
            this.btnEnviarAExcel = new System.Windows.Forms.Button();
            this.dgvDetalleVenta = new System.Windows.Forms.DataGridView();
            this.codigoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcionProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ingresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.egresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldoActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.txtFechaHasta = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFechaDesde = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dbAyudaArticulo = new Controles.Auxiliares.DB_Ayuda();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbBodega = new MisControles.ComboDatos();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).BeginInit();
            this.pnlEmpresa.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnviarAExcel
            // 
            this.btnEnviarAExcel.Location = new System.Drawing.Point(12, 427);
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
            this.dgvDetalleVenta.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dgvDetalleVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleVenta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigoProducto,
            this.descripcionProducto,
            this.cantidad,
            this.precioUnitario,
            this.ingresos,
            this.egresos,
            this.saldoActual});
            this.dgvDetalleVenta.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvDetalleVenta.Location = new System.Drawing.Point(14, 110);
            this.dgvDetalleVenta.Name = "dgvDetalleVenta";
            this.dgvDetalleVenta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalleVenta.Size = new System.Drawing.Size(754, 309);
            this.dgvDetalleVenta.TabIndex = 127;
            // 
            // codigoProducto
            // 
            this.codigoProducto.HeaderText = "Código";
            this.codigoProducto.Name = "codigoProducto";
            this.codigoProducto.Width = 70;
            // 
            // descripcionProducto
            // 
            this.descripcionProducto.HeaderText = "Descripción";
            this.descripcionProducto.Name = "descripcionProducto";
            this.descripcionProducto.Width = 260;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.HeaderText = "Unid.";
            this.cantidad.Name = "cantidad";
            this.cantidad.Width = 50;
            // 
            // precioUnitario
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.precioUnitario.DefaultCellStyle = dataGridViewCellStyle2;
            this.precioUnitario.HeaderText = "Saldo Anterior";
            this.precioUnitario.Name = "precioUnitario";
            // 
            // ingresos
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ingresos.DefaultCellStyle = dataGridViewCellStyle3;
            this.ingresos.HeaderText = "Ingresos";
            this.ingresos.Name = "ingresos";
            this.ingresos.Width = 70;
            // 
            // egresos
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.egresos.DefaultCellStyle = dataGridViewCellStyle4;
            this.egresos.HeaderText = "Egresos";
            this.egresos.Name = "egresos";
            this.egresos.Width = 70;
            // 
            // saldoActual
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.saldoActual.DefaultCellStyle = dataGridViewCellStyle5;
            this.saldoActual.HeaderText = "Saldo Actual";
            this.saldoActual.Name = "saldoActual";
            this.saldoActual.Width = 70;
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.Controls.Add(this.txtFechaHasta);
            this.pnlEmpresa.Controls.Add(this.label1);
            this.pnlEmpresa.Controls.Add(this.txtFechaDesde);
            this.pnlEmpresa.Controls.Add(this.label2);
            this.pnlEmpresa.Controls.Add(this.dbAyudaArticulo);
            this.pnlEmpresa.Controls.Add(this.lblFamilia);
            this.pnlEmpresa.Controls.Add(this.cmbEmpresa);
            this.pnlEmpresa.Controls.Add(this.lblEmpresa);
            this.pnlEmpresa.Controls.Add(this.btnOk);
            this.pnlEmpresa.Controls.Add(this.cmbBodega);
            this.pnlEmpresa.Controls.Add(this.lblBodega);
            this.pnlEmpresa.Controls.Add(this.cmbOficina);
            this.pnlEmpresa.Controls.Add(this.lblOficina);
            this.pnlEmpresa.Location = new System.Drawing.Point(2, 12);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(766, 92);
            this.pnlEmpresa.TabIndex = 124;
            // 
            // txtFechaHasta
            // 
            this.txtFechaHasta.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaHasta.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaHasta.Location = new System.Drawing.Point(536, 19);
            this.txtFechaHasta.Name = "txtFechaHasta";
            this.txtFechaHasta.Size = new System.Drawing.Size(77, 20);
            this.txtFechaHasta.TabIndex = 137;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(533, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 136;
            this.label1.Text = "Fecha Hasta";
            // 
            // txtFechaDesde
            // 
            this.txtFechaDesde.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaDesde.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaDesde.Location = new System.Drawing.Point(453, 19);
            this.txtFechaDesde.Name = "txtFechaDesde";
            this.txtFechaDesde.Size = new System.Drawing.Size(77, 20);
            this.txtFechaDesde.TabIndex = 135;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(450, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 134;
            this.label2.Text = "Fecha Desde";
            // 
            // dbAyudaArticulo
            // 
            this.dbAyudaArticulo.iId = 0;
            this.dbAyudaArticulo.Location = new System.Drawing.Point(12, 56);
            this.dbAyudaArticulo.Name = "dbAyudaArticulo";
            this.dbAyudaArticulo.sCodigo = null;
            this.dbAyudaArticulo.Size = new System.Drawing.Size(314, 21);
            this.dbAyudaArticulo.sNombre = null;
            this.dbAyudaArticulo.TabIndex = 124;
            // 
            // lblFamilia
            // 
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(9, 41);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(44, 13);
            this.lblFamilia.TabIndex = 129;
            this.lblFamilia.Text = "Artículo";
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
            this.btnOk.Location = new System.Drawing.Point(661, 36);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 23);
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
            this.btnLimpiar.Location = new System.Drawing.Point(640, 427);
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
            this.btnCerrar.Location = new System.Drawing.Point(706, 427);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(60, 23);
            this.btnCerrar.TabIndex = 128;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // frmExistenciasRangodeFechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(779, 458);
            this.Controls.Add(this.btnEnviarAExcel);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvDetalleVenta);
            this.Controls.Add(this.pnlEmpresa);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExistenciasRangodeFechas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Existencias de Materiales en un rango de Fechas";
            this.Load += new System.EventHandler(this.frmExistenciasRangodeFechas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleVenta)).EndInit();
            this.pnlEmpresa.ResumeLayout(false);
            this.pnlEmpresa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnviarAExcel;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvDetalleVenta;
        private System.Windows.Forms.Panel pnlEmpresa;
        private Controles.Auxiliares.DB_Ayuda dbAyudaArticulo;
        private System.Windows.Forms.Label lblFamilia;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Button btnOk;
        private MisControles.ComboDatos cmbBodega;
        private System.Windows.Forms.Label lblBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblOficina;
        private System.Windows.Forms.TextBox txtFechaHasta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFechaDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigoProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcionProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn ingresos;
        private System.Windows.Forms.DataGridViewTextBoxColumn egresos;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldoActual;
    }
}