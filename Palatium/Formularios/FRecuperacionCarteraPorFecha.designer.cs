namespace Palatium.Formularios
{
    partial class FRecuperacionCarteraPorFecha
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
            this.tabCon_CateraCobrar = new System.Windows.Forms.TabControl();
            this.tabPag_CateraCobrar = new System.Windows.Forms.TabPage();
            this.btnExcel = new System.Windows.Forms.Button();
            this.Grb_listReCateraCobrar = new System.Windows.Forms.GroupBox();
            this.txtNCredito = new System.Windows.Forms.TextBox();
            this.lblNCredito = new System.Windows.Forms.Label();
            this.txtSobrante = new System.Windows.Forms.TextBox();
            this.lblSobrante = new System.Windows.Forms.Label();
            this.txtRIva = new System.Windows.Forms.TextBox();
            this.lblRIva = new System.Windows.Forms.Label();
            this.txtRFuente = new System.Windows.Forms.TextBox();
            this.lblRFuente = new System.Windows.Forms.Label();
            this.txtBaseComision = new System.Windows.Forms.TextBox();
            this.lblBasComision = new System.Windows.Forms.Label();
            this.txtVPagado = new System.Windows.Forms.TextBox();
            this.lblVPagad = new System.Windows.Forms.Label();
            this.dgvCateraCobrar = new System.Windows.Forms.DataGridView();
            this.clmNumfactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaVct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDocumetOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTipoDcto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNoDcto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValorPagado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNoRet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRetFuente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmRetIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmComision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSobrante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNotaCredito = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBaseCalculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCateraCobrar = new System.Windows.Forms.Button();
            this.btnCerrarCateraCobrar = new System.Windows.Forms.Button();
            this.Grb_DatoCateraCobrar = new System.Windows.Forms.GroupBox();
            this.btnPregunta = new System.Windows.Forms.Button();
            this.cmbVendedor = new MisControles.ComboDatos();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.btnOKCateraCobrar = new System.Windows.Forms.Button();
            this.cmbMoneda = new MisControles.ComboDatos();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtFechaFinal = new System.Windows.Forms.TextBox();
            this.txtIdentificacionCliente = new System.Windows.Forms.TextBox();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.tabCon_CateraCobrar.SuspendLayout();
            this.tabPag_CateraCobrar.SuspendLayout();
            this.Grb_listReCateraCobrar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCateraCobrar)).BeginInit();
            this.Grb_DatoCateraCobrar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_CateraCobrar
            // 
            this.tabCon_CateraCobrar.Controls.Add(this.tabPag_CateraCobrar);
            this.tabCon_CateraCobrar.Location = new System.Drawing.Point(-3, -1);
            this.tabCon_CateraCobrar.Name = "tabCon_CateraCobrar";
            this.tabCon_CateraCobrar.SelectedIndex = 0;
            this.tabCon_CateraCobrar.Size = new System.Drawing.Size(909, 556);
            this.tabCon_CateraCobrar.TabIndex = 5;
            // 
            // tabPag_CateraCobrar
            // 
            this.tabPag_CateraCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_CateraCobrar.Controls.Add(this.btnExcel);
            this.tabPag_CateraCobrar.Controls.Add(this.Grb_listReCateraCobrar);
            this.tabPag_CateraCobrar.Controls.Add(this.btnCateraCobrar);
            this.tabPag_CateraCobrar.Controls.Add(this.btnCerrarCateraCobrar);
            this.tabPag_CateraCobrar.Controls.Add(this.Grb_DatoCateraCobrar);
            this.tabPag_CateraCobrar.Location = new System.Drawing.Point(4, 22);
            this.tabPag_CateraCobrar.Name = "tabPag_CateraCobrar";
            this.tabPag_CateraCobrar.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_CateraCobrar.Size = new System.Drawing.Size(901, 530);
            this.tabPag_CateraCobrar.TabIndex = 0;
            this.tabPag_CateraCobrar.Text = "Cartera por Cobrar por fecha de Factura";
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(30, 485);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(70, 39);
            this.btnExcel.TabIndex = 27;
            this.btnExcel.Text = "Enviar a Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // Grb_listReCateraCobrar
            // 
            this.Grb_listReCateraCobrar.Controls.Add(this.txtNCredito);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblNCredito);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtSobrante);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblSobrante);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtRIva);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblRIva);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtRFuente);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblRFuente);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtBaseComision);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblBasComision);
            this.Grb_listReCateraCobrar.Controls.Add(this.txtVPagado);
            this.Grb_listReCateraCobrar.Controls.Add(this.lblVPagad);
            this.Grb_listReCateraCobrar.Controls.Add(this.dgvCateraCobrar);
            this.Grb_listReCateraCobrar.Location = new System.Drawing.Point(17, 141);
            this.Grb_listReCateraCobrar.Name = "Grb_listReCateraCobrar";
            this.Grb_listReCateraCobrar.Size = new System.Drawing.Size(869, 338);
            this.Grb_listReCateraCobrar.TabIndex = 5;
            this.Grb_listReCateraCobrar.TabStop = false;
            this.Grb_listReCateraCobrar.Text = "Lista de Registros";
            // 
            // txtNCredito
            // 
            this.txtNCredito.Location = new System.Drawing.Point(773, 301);
            this.txtNCredito.Multiline = true;
            this.txtNCredito.Name = "txtNCredito";
            this.txtNCredito.ReadOnly = true;
            this.txtNCredito.Size = new System.Drawing.Size(75, 21);
            this.txtNCredito.TabIndex = 51;
            // 
            // lblNCredito
            // 
            this.lblNCredito.AutoSize = true;
            this.lblNCredito.BackColor = System.Drawing.Color.Transparent;
            this.lblNCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNCredito.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNCredito.Location = new System.Drawing.Point(770, 283);
            this.lblNCredito.Name = "lblNCredito";
            this.lblNCredito.Size = new System.Drawing.Size(58, 15);
            this.lblNCredito.TabIndex = 50;
            this.lblNCredito.Text = "NCrédito:";
            // 
            // txtSobrante
            // 
            this.txtSobrante.Location = new System.Drawing.Point(682, 301);
            this.txtSobrante.Multiline = true;
            this.txtSobrante.Name = "txtSobrante";
            this.txtSobrante.ReadOnly = true;
            this.txtSobrante.Size = new System.Drawing.Size(75, 21);
            this.txtSobrante.TabIndex = 49;
            // 
            // lblSobrante
            // 
            this.lblSobrante.AutoSize = true;
            this.lblSobrante.BackColor = System.Drawing.Color.Transparent;
            this.lblSobrante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSobrante.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSobrante.Location = new System.Drawing.Point(679, 283);
            this.lblSobrante.Name = "lblSobrante";
            this.lblSobrante.Size = new System.Drawing.Size(60, 15);
            this.lblSobrante.TabIndex = 48;
            this.lblSobrante.Text = "Sobrante:";
            // 
            // txtRIva
            // 
            this.txtRIva.Location = new System.Drawing.Point(432, 301);
            this.txtRIva.Multiline = true;
            this.txtRIva.Name = "txtRIva";
            this.txtRIva.ReadOnly = true;
            this.txtRIva.Size = new System.Drawing.Size(75, 21);
            this.txtRIva.TabIndex = 47;
            // 
            // lblRIva
            // 
            this.lblRIva.AutoSize = true;
            this.lblRIva.BackColor = System.Drawing.Color.Transparent;
            this.lblRIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRIva.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRIva.Location = new System.Drawing.Point(429, 283);
            this.lblRIva.Name = "lblRIva";
            this.lblRIva.Size = new System.Drawing.Size(34, 15);
            this.lblRIva.TabIndex = 46;
            this.lblRIva.Text = "RIva:";
            // 
            // txtRFuente
            // 
            this.txtRFuente.Location = new System.Drawing.Point(341, 301);
            this.txtRFuente.Multiline = true;
            this.txtRFuente.Name = "txtRFuente";
            this.txtRFuente.ReadOnly = true;
            this.txtRFuente.Size = new System.Drawing.Size(75, 21);
            this.txtRFuente.TabIndex = 45;
            // 
            // lblRFuente
            // 
            this.lblRFuente.AutoSize = true;
            this.lblRFuente.BackColor = System.Drawing.Color.Transparent;
            this.lblRFuente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFuente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRFuente.Location = new System.Drawing.Point(338, 283);
            this.lblRFuente.Name = "lblRFuente";
            this.lblRFuente.Size = new System.Drawing.Size(57, 15);
            this.lblRFuente.TabIndex = 44;
            this.lblRFuente.Text = "RFuente:";
            // 
            // txtBaseComision
            // 
            this.txtBaseComision.Location = new System.Drawing.Point(106, 301);
            this.txtBaseComision.Multiline = true;
            this.txtBaseComision.Name = "txtBaseComision";
            this.txtBaseComision.ReadOnly = true;
            this.txtBaseComision.Size = new System.Drawing.Size(75, 21);
            this.txtBaseComision.TabIndex = 43;
            // 
            // lblBasComision
            // 
            this.lblBasComision.AutoSize = true;
            this.lblBasComision.BackColor = System.Drawing.Color.Transparent;
            this.lblBasComision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasComision.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblBasComision.Location = new System.Drawing.Point(103, 283);
            this.lblBasComision.Name = "lblBasComision";
            this.lblBasComision.Size = new System.Drawing.Size(93, 15);
            this.lblBasComision.TabIndex = 42;
            this.lblBasComision.Text = "Base Comisión:";
            // 
            // txtVPagado
            // 
            this.txtVPagado.Location = new System.Drawing.Point(13, 301);
            this.txtVPagado.Multiline = true;
            this.txtVPagado.Name = "txtVPagado";
            this.txtVPagado.ReadOnly = true;
            this.txtVPagado.Size = new System.Drawing.Size(75, 21);
            this.txtVPagado.TabIndex = 41;
            // 
            // lblVPagad
            // 
            this.lblVPagad.AutoSize = true;
            this.lblVPagad.BackColor = System.Drawing.Color.Transparent;
            this.lblVPagad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVPagad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblVPagad.Location = new System.Drawing.Point(10, 283);
            this.lblVPagad.Name = "lblVPagad";
            this.lblVPagad.Size = new System.Drawing.Size(60, 15);
            this.lblVPagad.TabIndex = 40;
            this.lblVPagad.Text = "VPagado:";
            // 
            // dgvCateraCobrar
            // 
            this.dgvCateraCobrar.AllowUserToAddRows = false;
            this.dgvCateraCobrar.AllowUserToDeleteRows = false;
            this.dgvCateraCobrar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCateraCobrar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmNumfactura,
            this.clmFechaFactura,
            this.clmFechaVct,
            this.clmDocumetOrigen,
            this.clmFechaPago,
            this.clmTipoDcto,
            this.clmNoDcto,
            this.clmValorPagado,
            this.clmNoRet,
            this.clmRetFuente,
            this.clmRetIva,
            this.clmComision,
            this.clmSobrante,
            this.clmNotaCredito,
            this.clmBaseCalculo});
            this.dgvCateraCobrar.Location = new System.Drawing.Point(13, 19);
            this.dgvCateraCobrar.Name = "dgvCateraCobrar";
            this.dgvCateraCobrar.ReadOnly = true;
            this.dgvCateraCobrar.Size = new System.Drawing.Size(838, 240);
            this.dgvCateraCobrar.TabIndex = 0;
            // 
            // clmNumfactura
            // 
            this.clmNumfactura.Frozen = true;
            this.clmNumfactura.HeaderText = "Número Factura";
            this.clmNumfactura.Name = "clmNumfactura";
            this.clmNumfactura.ReadOnly = true;
            this.clmNumfactura.Width = 60;
            // 
            // clmFechaFactura
            // 
            this.clmFechaFactura.Frozen = true;
            this.clmFechaFactura.HeaderText = "Fecha Factura";
            this.clmFechaFactura.Name = "clmFechaFactura";
            this.clmFechaFactura.ReadOnly = true;
            this.clmFechaFactura.Width = 60;
            // 
            // clmFechaVct
            // 
            this.clmFechaVct.Frozen = true;
            this.clmFechaVct.HeaderText = "Fecha Vcto. Factura";
            this.clmFechaVct.Name = "clmFechaVct";
            this.clmFechaVct.ReadOnly = true;
            this.clmFechaVct.Width = 60;
            // 
            // clmDocumetOrigen
            // 
            this.clmDocumetOrigen.HeaderText = "Documento Origen";
            this.clmDocumetOrigen.Name = "clmDocumetOrigen";
            this.clmDocumetOrigen.ReadOnly = true;
            // 
            // clmFechaPago
            // 
            this.clmFechaPago.HeaderText = "Fecha Pago";
            this.clmFechaPago.Name = "clmFechaPago";
            this.clmFechaPago.ReadOnly = true;
            // 
            // clmTipoDcto
            // 
            this.clmTipoDcto.HeaderText = "Tipo Dcto.";
            this.clmTipoDcto.Name = "clmTipoDcto";
            this.clmTipoDcto.ReadOnly = true;
            // 
            // clmNoDcto
            // 
            this.clmNoDcto.HeaderText = "No. Dcto.";
            this.clmNoDcto.Name = "clmNoDcto";
            this.clmNoDcto.ReadOnly = true;
            // 
            // clmValorPagado
            // 
            this.clmValorPagado.HeaderText = "Valor Pagado";
            this.clmValorPagado.Name = "clmValorPagado";
            this.clmValorPagado.ReadOnly = true;
            // 
            // clmNoRet
            // 
            this.clmNoRet.HeaderText = "No. Ret.";
            this.clmNoRet.Name = "clmNoRet";
            this.clmNoRet.ReadOnly = true;
            // 
            // clmRetFuente
            // 
            this.clmRetFuente.HeaderText = "Ret. Fuente";
            this.clmRetFuente.Name = "clmRetFuente";
            this.clmRetFuente.ReadOnly = true;
            // 
            // clmRetIva
            // 
            this.clmRetIva.HeaderText = "Ret. Iva";
            this.clmRetIva.Name = "clmRetIva";
            this.clmRetIva.ReadOnly = true;
            // 
            // clmComision
            // 
            this.clmComision.HeaderText = "Comisión";
            this.clmComision.Name = "clmComision";
            this.clmComision.ReadOnly = true;
            // 
            // clmSobrante
            // 
            this.clmSobrante.HeaderText = "Sobrante";
            this.clmSobrante.Name = "clmSobrante";
            this.clmSobrante.ReadOnly = true;
            // 
            // clmNotaCredito
            // 
            this.clmNotaCredito.HeaderText = "Nota de Crédito";
            this.clmNotaCredito.Name = "clmNotaCredito";
            this.clmNotaCredito.ReadOnly = true;
            // 
            // clmBaseCalculo
            // 
            this.clmBaseCalculo.HeaderText = "Base Cálculo Comisión";
            this.clmBaseCalculo.Name = "clmBaseCalculo";
            this.clmBaseCalculo.ReadOnly = true;
            // 
            // btnCateraCobrar
            // 
            this.btnCateraCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCateraCobrar.ForeColor = System.Drawing.Color.White;
            this.btnCateraCobrar.Location = new System.Drawing.Point(699, 485);
            this.btnCateraCobrar.Name = "btnCateraCobrar";
            this.btnCateraCobrar.Size = new System.Drawing.Size(70, 39);
            this.btnCateraCobrar.TabIndex = 2;
            this.btnCateraCobrar.Text = "Limpiar";
            this.btnCateraCobrar.UseVisualStyleBackColor = false;
            this.btnCateraCobrar.Click += new System.EventHandler(this.btnCateraCobrar_Click);
            // 
            // btnCerrarCateraCobrar
            // 
            this.btnCerrarCateraCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarCateraCobrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrarCateraCobrar.Location = new System.Drawing.Point(794, 485);
            this.btnCerrarCateraCobrar.Name = "btnCerrarCateraCobrar";
            this.btnCerrarCateraCobrar.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarCateraCobrar.TabIndex = 3;
            this.btnCerrarCateraCobrar.Text = "Cerrar";
            this.btnCerrarCateraCobrar.UseVisualStyleBackColor = false;
            this.btnCerrarCateraCobrar.Click += new System.EventHandler(this.btnCerrarCateraCobrar_Click);
            // 
            // Grb_DatoCateraCobrar
            // 
            this.Grb_DatoCateraCobrar.Controls.Add(this.btnPregunta);
            this.Grb_DatoCateraCobrar.Controls.Add(this.cmbVendedor);
            this.Grb_DatoCateraCobrar.Controls.Add(this.txtNombreCliente);
            this.Grb_DatoCateraCobrar.Controls.Add(this.btnOKCateraCobrar);
            this.Grb_DatoCateraCobrar.Controls.Add(this.cmbMoneda);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblCliente);
            this.Grb_DatoCateraCobrar.Controls.Add(this.txtFechaFinal);
            this.Grb_DatoCateraCobrar.Controls.Add(this.txtIdentificacionCliente);
            this.Grb_DatoCateraCobrar.Controls.Add(this.txtFechaInicio);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblFechaInicio);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblFechaFin);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblMoneda);
            this.Grb_DatoCateraCobrar.Controls.Add(this.cmbLocalidad);
            this.Grb_DatoCateraCobrar.Controls.Add(this.cmbEmpresa);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblLocalidad);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblEmpresa);
            this.Grb_DatoCateraCobrar.Controls.Add(this.lblVendedor);
            this.Grb_DatoCateraCobrar.Location = new System.Drawing.Point(17, 6);
            this.Grb_DatoCateraCobrar.Name = "Grb_DatoCateraCobrar";
            this.Grb_DatoCateraCobrar.Size = new System.Drawing.Size(869, 129);
            this.Grb_DatoCateraCobrar.TabIndex = 3;
            this.Grb_DatoCateraCobrar.TabStop = false;
            this.Grb_DatoCateraCobrar.Text = "Datos del Registro";
            // 
            // btnPregunta
            // 
            this.btnPregunta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPregunta.Location = new System.Drawing.Point(135, 91);
            this.btnPregunta.Name = "btnPregunta";
            this.btnPregunta.Size = new System.Drawing.Size(33, 23);
            this.btnPregunta.TabIndex = 22;
            this.btnPregunta.Text = "?";
            this.btnPregunta.UseVisualStyleBackColor = true;
            this.btnPregunta.Click += new System.EventHandler(this.btnPregunta_Click_1);
            // 
            // cmbVendedor
            // 
            this.cmbVendedor.FormattingEnabled = true;
            this.cmbVendedor.Location = new System.Drawing.Point(478, 32);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(156, 21);
            this.cmbVendedor.TabIndex = 43;
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.Location = new System.Drawing.Point(174, 90);
            this.txtNombreCliente.Multiline = true;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.ReadOnly = true;
            this.txtNombreCliente.Size = new System.Drawing.Size(233, 23);
            this.txtNombreCliente.TabIndex = 23;
            // 
            // btnOKCateraCobrar
            // 
            this.btnOKCateraCobrar.BackColor = System.Drawing.Color.Blue;
            this.btnOKCateraCobrar.ForeColor = System.Drawing.Color.White;
            this.btnOKCateraCobrar.Location = new System.Drawing.Point(732, 75);
            this.btnOKCateraCobrar.Name = "btnOKCateraCobrar";
            this.btnOKCateraCobrar.Size = new System.Drawing.Size(70, 39);
            this.btnOKCateraCobrar.TabIndex = 0;
            this.btnOKCateraCobrar.Text = "OK";
            this.btnOKCateraCobrar.UseVisualStyleBackColor = false;
            this.btnOKCateraCobrar.Click += new System.EventHandler(this.btnOKCateraCobrar_Click);
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(673, 32);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(128, 21);
            this.cmbMoneda.TabIndex = 40;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(4, 68);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 15);
            this.lblCliente.TabIndex = 20;
            this.lblCliente.Text = "Cliente:";
            // 
            // txtFechaFinal
            // 
            this.txtFechaFinal.Location = new System.Drawing.Point(607, 90);
            this.txtFechaFinal.Multiline = true;
            this.txtFechaFinal.Name = "txtFechaFinal";
            this.txtFechaFinal.Size = new System.Drawing.Size(107, 21);
            this.txtFechaFinal.TabIndex = 38;
            // 
            // txtIdentificacionCliente
            // 
            this.txtIdentificacionCliente.Location = new System.Drawing.Point(6, 93);
            this.txtIdentificacionCliente.Multiline = true;
            this.txtIdentificacionCliente.Name = "txtIdentificacionCliente";
            this.txtIdentificacionCliente.ReadOnly = true;
            this.txtIdentificacionCliente.Size = new System.Drawing.Size(123, 21);
            this.txtIdentificacionCliente.TabIndex = 21;
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.Location = new System.Drawing.Point(444, 90);
            this.txtFechaInicio.Multiline = true;
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.Size = new System.Drawing.Size(107, 21);
            this.txtFechaInicio.TabIndex = 37;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaInicio.Location = new System.Drawing.Point(541, 68);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(94, 15);
            this.lblFechaInicio.TabIndex = 36;
            this.lblFechaInicio.Text = "Fecha Facturas:";
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaFin.Location = new System.Drawing.Point(568, 93);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(24, 15);
            this.lblFechaFin.TabIndex = 35;
            this.lblFechaFin.Text = "Fin";
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.BackColor = System.Drawing.Color.Transparent;
            this.lblMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneda.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMoneda.Location = new System.Drawing.Point(670, 13);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(56, 15);
            this.lblMoneda.TabIndex = 33;
            this.lblMoneda.Text = "Moneda:";
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(241, 32);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(197, 21);
            this.cmbLocalidad.TabIndex = 14;
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(6, 32);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(197, 21);
            this.cmbEmpresa.TabIndex = 13;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad.Location = new System.Drawing.Point(238, 14);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(61, 15);
            this.lblLocalidad.TabIndex = 12;
            this.lblLocalidad.Text = "Localidad";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(3, 14);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(57, 15);
            this.lblEmpresa.TabIndex = 11;
            this.lblEmpresa.Text = "Empresa";
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.BackColor = System.Drawing.Color.Transparent;
            this.lblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendedor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblVendedor.Location = new System.Drawing.Point(475, 14);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(63, 15);
            this.lblVendedor.TabIndex = 7;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // FRecuperacionCarteraPorFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 545);
            this.Controls.Add(this.tabCon_CateraCobrar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FRecuperacionCarteraPorFecha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Recuperacion de Cartera por Fecha";
            this.Load += new System.EventHandler(this.FRecuperacionCarteraPorFecha_Load);
            this.tabCon_CateraCobrar.ResumeLayout(false);
            this.tabPag_CateraCobrar.ResumeLayout(false);
            this.Grb_listReCateraCobrar.ResumeLayout(false);
            this.Grb_listReCateraCobrar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCateraCobrar)).EndInit();
            this.Grb_DatoCateraCobrar.ResumeLayout(false);
            this.Grb_DatoCateraCobrar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_CateraCobrar;
        private System.Windows.Forms.TabPage tabPag_CateraCobrar;
        private System.Windows.Forms.Button btnPregunta;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtIdentificacionCliente;
        private System.Windows.Forms.GroupBox Grb_listReCateraCobrar;
        private System.Windows.Forms.TextBox txtRIva;
        private System.Windows.Forms.Label lblRIva;
        private System.Windows.Forms.TextBox txtRFuente;
        private System.Windows.Forms.Label lblRFuente;
        private System.Windows.Forms.TextBox txtBaseComision;
        private System.Windows.Forms.Label lblBasComision;
        private System.Windows.Forms.TextBox txtVPagado;
        private System.Windows.Forms.Label lblVPagad;
        private System.Windows.Forms.DataGridView dgvCateraCobrar;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnCerrarCateraCobrar;
        private System.Windows.Forms.Button btnCateraCobrar;
        private System.Windows.Forms.Button btnOKCateraCobrar;
        private System.Windows.Forms.GroupBox Grb_DatoCateraCobrar;
        private MisControles.ComboDatos cmbVendedor;
        private MisControles.ComboDatos cmbMoneda;
        private System.Windows.Forms.TextBox txtFechaFinal;
        private System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblMoneda;
        private MisControles.ComboDatos cmbLocalidad;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNumfactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaVct;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDocumetOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTipoDcto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNoDcto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValorPagado;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNoRet;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRetFuente;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmRetIva;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmComision;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSobrante;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNotaCredito;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBaseCalculo;
        private System.Windows.Forms.TextBox txtNCredito;
        private System.Windows.Forms.Label lblNCredito;
        private System.Windows.Forms.TextBox txtSobrante;
        private System.Windows.Forms.Label lblSobrante;
    }
}