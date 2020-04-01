namespace Palatium.Formularios
{
    partial class FReciboDeCobros
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
            this.tabReciboCobros = new System.Windows.Forms.TabControl();
            this.tabPagReciboCobros = new System.Windows.Forms.TabPage();
            this.grpDocumentosPago = new System.Windows.Forms.GroupBox();
            this.dgvDocumentosPago = new System.Windows.Forms.DataGridView();
            this.clmTipoDocument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFachaVcto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCuentaNtarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmTitular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmAutorizacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalDocumento = new System.Windows.Forms.TextBox();
            this.lblTotalDocumento = new System.Windows.Forms.Label();
            this.grpCliente = new System.Windows.Forms.GroupBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.txtFechaPago = new System.Windows.Forms.TextBox();
            this.lblFechaPago = new System.Windows.Forms.Label();
            this.txtObserv = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblObserv = new System.Windows.Forms.Label();
            this.btnLimpiarReciboCobros = new System.Windows.Forms.Button();
            this.grpDocumentosPagados = new System.Windows.Forms.GroupBox();
            this.txtTotal3 = new System.Windows.Forms.TextBox();
            this.txtTotal2 = new System.Windows.Forms.TextBox();
            this.txtTotal1 = new System.Windows.Forms.TextBox();
            this.lblTotales = new System.Windows.Forms.Label();
            this.dgvDocumentosPagados = new System.Windows.Forms.DataGridView();
            this.clmTipoDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValorFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmSaldoPagar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValorPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImprimirReciboCobros = new System.Windows.Forms.Button();
            this.btnCerrarReciboCobros = new System.Windows.Forms.Button();
            this.Grb_DatoReciboCobros = new System.Windows.Forms.GroupBox();
            this.btnPregunta = new System.Windows.Forms.Button();
            this.txtNombreRecibo = new System.Windows.Forms.TextBox();
            this.btnOKReciboCobros = new System.Windows.Forms.Button();
            this.lblRecibo = new System.Windows.Forms.Label();
            this.txtNumRecibo = new System.Windows.Forms.TextBox();
            this.cmbFormato = new MisControles.ComboDatos();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblFormato = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.txtFaltante = new System.Windows.Forms.TextBox();
            this.lblFaltante = new System.Windows.Forms.Label();
            this.tabReciboCobros.SuspendLayout();
            this.tabPagReciboCobros.SuspendLayout();
            this.grpDocumentosPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosPago)).BeginInit();
            this.grpCliente.SuspendLayout();
            this.grpDocumentosPagados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosPagados)).BeginInit();
            this.Grb_DatoReciboCobros.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabReciboCobros
            // 
            this.tabReciboCobros.Controls.Add(this.tabPagReciboCobros);
            this.tabReciboCobros.Location = new System.Drawing.Point(-4, -1);
            this.tabReciboCobros.Name = "tabReciboCobros";
            this.tabReciboCobros.SelectedIndex = 0;
            this.tabReciboCobros.Size = new System.Drawing.Size(1035, 689);
            this.tabReciboCobros.TabIndex = 4;
            // 
            // tabPagReciboCobros
            // 
            this.tabPagReciboCobros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPagReciboCobros.Controls.Add(this.grpDocumentosPago);
            this.tabPagReciboCobros.Controls.Add(this.grpCliente);
            this.tabPagReciboCobros.Controls.Add(this.btnLimpiarReciboCobros);
            this.tabPagReciboCobros.Controls.Add(this.grpDocumentosPagados);
            this.tabPagReciboCobros.Controls.Add(this.btnImprimirReciboCobros);
            this.tabPagReciboCobros.Controls.Add(this.btnCerrarReciboCobros);
            this.tabPagReciboCobros.Controls.Add(this.Grb_DatoReciboCobros);
            this.tabPagReciboCobros.Controls.Add(this.txtFaltante);
            this.tabPagReciboCobros.Controls.Add(this.lblFaltante);
            this.tabPagReciboCobros.Location = new System.Drawing.Point(4, 22);
            this.tabPagReciboCobros.Name = "tabPagReciboCobros";
            this.tabPagReciboCobros.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagReciboCobros.Size = new System.Drawing.Size(1027, 663);
            this.tabPagReciboCobros.TabIndex = 0;
            this.tabPagReciboCobros.Text = "Recibo de cobro";
            // 
            // grpDocumentosPago
            // 
            this.grpDocumentosPago.Controls.Add(this.dgvDocumentosPago);
            this.grpDocumentosPago.Controls.Add(this.txtTotalDocumento);
            this.grpDocumentosPago.Controls.Add(this.lblTotalDocumento);
            this.grpDocumentosPago.Location = new System.Drawing.Point(17, 392);
            this.grpDocumentosPago.Name = "grpDocumentosPago";
            this.grpDocumentosPago.Size = new System.Drawing.Size(987, 188);
            this.grpDocumentosPago.TabIndex = 48;
            this.grpDocumentosPago.TabStop = false;
            this.grpDocumentosPago.Text = "Documentos de Pago";
            // 
            // dgvDocumentosPago
            // 
            this.dgvDocumentosPago.AllowUserToAddRows = false;
            this.dgvDocumentosPago.AllowUserToDeleteRows = false;
            this.dgvDocumentosPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocumentosPago.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTipoDocument,
            this.clmFachaVcto,
            this.clmNume,
            this.clmCuentaNtarjeta,
            this.clmValor,
            this.clmBanco,
            this.clmTitular,
            this.clmAutorizacion});
            this.dgvDocumentosPago.Location = new System.Drawing.Point(70, 29);
            this.dgvDocumentosPago.Name = "dgvDocumentosPago";
            this.dgvDocumentosPago.ReadOnly = true;
            this.dgvDocumentosPago.Size = new System.Drawing.Size(845, 122);
            this.dgvDocumentosPago.TabIndex = 0;
            // 
            // clmTipoDocument
            // 
            this.clmTipoDocument.DataPropertyName = "codigo";
            this.clmTipoDocument.HeaderText = "Tipo Documento";
            this.clmTipoDocument.Name = "clmTipoDocument";
            this.clmTipoDocument.ReadOnly = true;
            // 
            // clmFachaVcto
            // 
            this.clmFachaVcto.DataPropertyName = "fecha_vcto";
            this.clmFachaVcto.HeaderText = "Facha Vcto.";
            this.clmFachaVcto.Name = "clmFachaVcto";
            this.clmFachaVcto.ReadOnly = true;
            // 
            // clmNume
            // 
            this.clmNume.DataPropertyName = "numero_documento";
            this.clmNume.HeaderText = "Número";
            this.clmNume.Name = "clmNume";
            this.clmNume.ReadOnly = true;
            // 
            // clmCuentaNtarjeta
            // 
            this.clmCuentaNtarjeta.DataPropertyName = "Numero_Cta";
            this.clmCuentaNtarjeta.HeaderText = "Cuenta/N° Tajerta";
            this.clmCuentaNtarjeta.Name = "clmCuentaNtarjeta";
            this.clmCuentaNtarjeta.ReadOnly = true;
            // 
            // clmValor
            // 
            this.clmValor.DataPropertyName = "valor";
            this.clmValor.HeaderText = "Valor";
            this.clmValor.Name = "clmValor";
            this.clmValor.ReadOnly = true;
            // 
            // clmBanco
            // 
            this.clmBanco.DataPropertyName = "Banco";
            this.clmBanco.HeaderText = "Banco";
            this.clmBanco.Name = "clmBanco";
            this.clmBanco.ReadOnly = true;
            // 
            // clmTitular
            // 
            this.clmTitular.DataPropertyName = "Titular";
            this.clmTitular.HeaderText = "Titular";
            this.clmTitular.Name = "clmTitular";
            this.clmTitular.ReadOnly = true;
            // 
            // clmAutorizacion
            // 
            this.clmAutorizacion.DataPropertyName = "autorizacion";
            this.clmAutorizacion.HeaderText = "Autorización";
            this.clmAutorizacion.Name = "clmAutorizacion";
            this.clmAutorizacion.ReadOnly = true;
            // 
            // txtTotalDocumento
            // 
            this.txtTotalDocumento.Location = new System.Drawing.Point(740, 161);
            this.txtTotalDocumento.Multiline = true;
            this.txtTotalDocumento.Name = "txtTotalDocumento";
            this.txtTotalDocumento.ReadOnly = true;
            this.txtTotalDocumento.Size = new System.Drawing.Size(75, 21);
            this.txtTotalDocumento.TabIndex = 47;
            // 
            // lblTotalDocumento
            // 
            this.lblTotalDocumento.AutoSize = true;
            this.lblTotalDocumento.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDocumento.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalDocumento.Location = new System.Drawing.Point(630, 162);
            this.lblTotalDocumento.Name = "lblTotalDocumento";
            this.lblTotalDocumento.Size = new System.Drawing.Size(110, 15);
            this.lblTotalDocumento.TabIndex = 46;
            this.lblTotalDocumento.Text = "Total Documentos:";
            // 
            // grpCliente
            // 
            this.grpCliente.Controls.Add(this.txtSerie);
            this.grpCliente.Controls.Add(this.lblSerie);
            this.grpCliente.Controls.Add(this.txtLocalidad);
            this.grpCliente.Controls.Add(this.lblLocalidad);
            this.grpCliente.Controls.Add(this.txtFechaPago);
            this.grpCliente.Controls.Add(this.lblFechaPago);
            this.grpCliente.Controls.Add(this.txtObserv);
            this.grpCliente.Controls.Add(this.txtCliente);
            this.grpCliente.Controls.Add(this.lblCliente);
            this.grpCliente.Controls.Add(this.lblObserv);
            this.grpCliente.Location = new System.Drawing.Point(17, 90);
            this.grpCliente.Name = "grpCliente";
            this.grpCliente.Size = new System.Drawing.Size(987, 95);
            this.grpCliente.TabIndex = 28;
            this.grpCliente.TabStop = false;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(890, 19);
            this.txtSerie.Multiline = true;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(85, 21);
            this.txtSerie.TabIndex = 44;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.BackColor = System.Drawing.Color.Transparent;
            this.lblSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerie.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSerie.Location = new System.Drawing.Point(840, 24);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(39, 15);
            this.lblSerie.TabIndex = 43;
            this.lblSerie.Text = "Serie:";
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.Location = new System.Drawing.Point(686, 46);
            this.txtLocalidad.Multiline = true;
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.Size = new System.Drawing.Size(167, 21);
            this.txtLocalidad.TabIndex = 40;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLocalidad.Location = new System.Drawing.Point(607, 51);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(64, 15);
            this.lblLocalidad.TabIndex = 39;
            this.lblLocalidad.Text = "Localidad:";
            // 
            // txtFechaPago
            // 
            this.txtFechaPago.Location = new System.Drawing.Point(686, 19);
            this.txtFechaPago.Multiline = true;
            this.txtFechaPago.Name = "txtFechaPago";
            this.txtFechaPago.Size = new System.Drawing.Size(85, 21);
            this.txtFechaPago.TabIndex = 42;
            // 
            // lblFechaPago
            // 
            this.lblFechaPago.AutoSize = true;
            this.lblFechaPago.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaPago.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaPago.Location = new System.Drawing.Point(607, 24);
            this.lblFechaPago.Name = "lblFechaPago";
            this.lblFechaPago.Size = new System.Drawing.Size(76, 15);
            this.lblFechaPago.TabIndex = 41;
            this.lblFechaPago.Text = "Fecha Pago:";
            // 
            // txtObserv
            // 
            this.txtObserv.Location = new System.Drawing.Point(57, 46);
            this.txtObserv.Multiline = true;
            this.txtObserv.Name = "txtObserv";
            this.txtObserv.Size = new System.Drawing.Size(514, 21);
            this.txtObserv.TabIndex = 38;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(57, 19);
            this.txtCliente.Multiline = true;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(514, 21);
            this.txtCliente.TabIndex = 37;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.BackColor = System.Drawing.Color.Transparent;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCliente.Location = new System.Drawing.Point(7, 24);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 15);
            this.lblCliente.TabIndex = 36;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblObserv
            // 
            this.lblObserv.AutoSize = true;
            this.lblObserv.BackColor = System.Drawing.Color.Transparent;
            this.lblObserv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObserv.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblObserv.Location = new System.Drawing.Point(7, 52);
            this.lblObserv.Name = "lblObserv";
            this.lblObserv.Size = new System.Drawing.Size(48, 15);
            this.lblObserv.TabIndex = 35;
            this.lblObserv.Text = "Observ.";
            // 
            // btnLimpiarReciboCobros
            // 
            this.btnLimpiarReciboCobros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLimpiarReciboCobros.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarReciboCobros.Location = new System.Drawing.Point(790, 609);
            this.btnLimpiarReciboCobros.Name = "btnLimpiarReciboCobros";
            this.btnLimpiarReciboCobros.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiarReciboCobros.TabIndex = 27;
            this.btnLimpiarReciboCobros.Text = "Enviar a Excel";
            this.btnLimpiarReciboCobros.UseVisualStyleBackColor = false;
            // 
            // grpDocumentosPagados
            // 
            this.grpDocumentosPagados.Controls.Add(this.txtTotal3);
            this.grpDocumentosPagados.Controls.Add(this.txtTotal2);
            this.grpDocumentosPagados.Controls.Add(this.txtTotal1);
            this.grpDocumentosPagados.Controls.Add(this.lblTotales);
            this.grpDocumentosPagados.Controls.Add(this.dgvDocumentosPagados);
            this.grpDocumentosPagados.Location = new System.Drawing.Point(17, 191);
            this.grpDocumentosPagados.Name = "grpDocumentosPagados";
            this.grpDocumentosPagados.Size = new System.Drawing.Size(987, 188);
            this.grpDocumentosPagados.TabIndex = 5;
            this.grpDocumentosPagados.TabStop = false;
            this.grpDocumentosPagados.Text = "Documentos Pagados";
            // 
            // txtTotal3
            // 
            this.txtTotal3.Location = new System.Drawing.Point(741, 164);
            this.txtTotal3.Multiline = true;
            this.txtTotal3.Name = "txtTotal3";
            this.txtTotal3.ReadOnly = true;
            this.txtTotal3.Size = new System.Drawing.Size(75, 21);
            this.txtTotal3.TabIndex = 51;
            // 
            // txtTotal2
            // 
            this.txtTotal2.Location = new System.Drawing.Point(657, 164);
            this.txtTotal2.Multiline = true;
            this.txtTotal2.Name = "txtTotal2";
            this.txtTotal2.ReadOnly = true;
            this.txtTotal2.Size = new System.Drawing.Size(75, 21);
            this.txtTotal2.TabIndex = 50;
            // 
            // txtTotal1
            // 
            this.txtTotal1.Location = new System.Drawing.Point(576, 164);
            this.txtTotal1.Multiline = true;
            this.txtTotal1.Name = "txtTotal1";
            this.txtTotal1.ReadOnly = true;
            this.txtTotal1.Size = new System.Drawing.Size(75, 21);
            this.txtTotal1.TabIndex = 49;
            // 
            // lblTotales
            // 
            this.lblTotales.AutoSize = true;
            this.lblTotales.BackColor = System.Drawing.Color.Transparent;
            this.lblTotales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotales.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotales.Location = new System.Drawing.Point(503, 165);
            this.lblTotales.Name = "lblTotales";
            this.lblTotales.Size = new System.Drawing.Size(67, 15);
            this.lblTotales.TabIndex = 48;
            this.lblTotales.Text = "Valor Neto:";
            // 
            // dgvDocumentosPagados
            // 
            this.dgvDocumentosPagados.AllowUserToAddRows = false;
            this.dgvDocumentosPagados.AllowUserToDeleteRows = false;
            this.dgvDocumentosPagados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDocumentosPagados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmTipoDocumento,
            this.clmNumero,
            this.clmFecha,
            this.clmValorFactura,
            this.clmSaldoPagar,
            this.clmValorPago});
            this.dgvDocumentosPagados.Location = new System.Drawing.Point(173, 29);
            this.dgvDocumentosPagados.Name = "dgvDocumentosPagados";
            this.dgvDocumentosPagados.ReadOnly = true;
            this.dgvDocumentosPagados.Size = new System.Drawing.Size(642, 122);
            this.dgvDocumentosPagados.TabIndex = 0;
            // 
            // clmTipoDocumento
            // 
            this.clmTipoDocumento.DataPropertyName = "codigo";
            this.clmTipoDocumento.HeaderText = "Tipo Documento";
            this.clmTipoDocumento.Name = "clmTipoDocumento";
            this.clmTipoDocumento.ReadOnly = true;
            // 
            // clmNumero
            // 
            this.clmNumero.DataPropertyName = "numero_documento";
            this.clmNumero.HeaderText = "Número";
            this.clmNumero.Name = "clmNumero";
            this.clmNumero.ReadOnly = true;
            // 
            // clmFecha
            // 
            this.clmFecha.DataPropertyName = "fecha_vcto";
            this.clmFecha.HeaderText = "Fecha";
            this.clmFecha.Name = "clmFecha";
            this.clmFecha.ReadOnly = true;
            // 
            // clmValorFactura
            // 
            this.clmValorFactura.DataPropertyName = "valor";
            this.clmValorFactura.HeaderText = "Valor Factura";
            this.clmValorFactura.Name = "clmValorFactura";
            this.clmValorFactura.ReadOnly = true;
            // 
            // clmSaldoPagar
            // 
            this.clmSaldoPagar.DataPropertyName = "Saldo_Anterior";
            this.clmSaldoPagar.HeaderText = "Saldo a Pagar";
            this.clmSaldoPagar.Name = "clmSaldoPagar";
            this.clmSaldoPagar.ReadOnly = true;
            // 
            // clmValorPago
            // 
            this.clmValorPago.DataPropertyName = "Total_Pagado";
            this.clmValorPago.HeaderText = "Valor Pago";
            this.clmValorPago.Name = "clmValorPago";
            this.clmValorPago.ReadOnly = true;
            // 
            // btnImprimirReciboCobros
            // 
            this.btnImprimirReciboCobros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnImprimirReciboCobros.ForeColor = System.Drawing.Color.White;
            this.btnImprimirReciboCobros.Location = new System.Drawing.Point(862, 609);
            this.btnImprimirReciboCobros.Name = "btnImprimirReciboCobros";
            this.btnImprimirReciboCobros.Size = new System.Drawing.Size(70, 39);
            this.btnImprimirReciboCobros.TabIndex = 2;
            this.btnImprimirReciboCobros.Text = "Limpiar";
            this.btnImprimirReciboCobros.UseVisualStyleBackColor = false;
            this.btnImprimirReciboCobros.Click += new System.EventHandler(this.btnImprimirReciboCobros_Click);
            // 
            // btnCerrarReciboCobros
            // 
            this.btnCerrarReciboCobros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarReciboCobros.ForeColor = System.Drawing.Color.White;
            this.btnCerrarReciboCobros.Location = new System.Drawing.Point(934, 609);
            this.btnCerrarReciboCobros.Name = "btnCerrarReciboCobros";
            this.btnCerrarReciboCobros.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarReciboCobros.TabIndex = 3;
            this.btnCerrarReciboCobros.Text = "Cerrar";
            this.btnCerrarReciboCobros.UseVisualStyleBackColor = false;
            this.btnCerrarReciboCobros.Click += new System.EventHandler(this.btnCerrarReciboCobros_Click);
            // 
            // Grb_DatoReciboCobros
            // 
            this.Grb_DatoReciboCobros.Controls.Add(this.btnPregunta);
            this.Grb_DatoReciboCobros.Controls.Add(this.txtNombreRecibo);
            this.Grb_DatoReciboCobros.Controls.Add(this.btnOKReciboCobros);
            this.Grb_DatoReciboCobros.Controls.Add(this.lblRecibo);
            this.Grb_DatoReciboCobros.Controls.Add(this.txtNumRecibo);
            this.Grb_DatoReciboCobros.Controls.Add(this.cmbFormato);
            this.Grb_DatoReciboCobros.Controls.Add(this.cmbEmpresa);
            this.Grb_DatoReciboCobros.Controls.Add(this.lblFormato);
            this.Grb_DatoReciboCobros.Controls.Add(this.lblEmpresa);
            this.Grb_DatoReciboCobros.Location = new System.Drawing.Point(17, 6);
            this.Grb_DatoReciboCobros.Name = "Grb_DatoReciboCobros";
            this.Grb_DatoReciboCobros.Size = new System.Drawing.Size(987, 78);
            this.Grb_DatoReciboCobros.TabIndex = 3;
            this.Grb_DatoReciboCobros.TabStop = false;
            // 
            // btnPregunta
            // 
            this.btnPregunta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPregunta.Location = new System.Drawing.Point(421, 32);
            this.btnPregunta.Name = "btnPregunta";
            this.btnPregunta.Size = new System.Drawing.Size(33, 23);
            this.btnPregunta.TabIndex = 22;
            this.btnPregunta.Text = "?";
            this.btnPregunta.UseVisualStyleBackColor = true;
            this.btnPregunta.Click += new System.EventHandler(this.btnPregunta_Click);
            // 
            // txtNombreRecibo
            // 
            this.txtNombreRecibo.Location = new System.Drawing.Point(460, 32);
            this.txtNombreRecibo.Multiline = true;
            this.txtNombreRecibo.Name = "txtNombreRecibo";
            this.txtNombreRecibo.ReadOnly = true;
            this.txtNombreRecibo.Size = new System.Drawing.Size(233, 23);
            this.txtNombreRecibo.TabIndex = 23;
            // 
            // btnOKReciboCobros
            // 
            this.btnOKReciboCobros.BackColor = System.Drawing.Color.Blue;
            this.btnOKReciboCobros.ForeColor = System.Drawing.Color.White;
            this.btnOKReciboCobros.Location = new System.Drawing.Point(890, 14);
            this.btnOKReciboCobros.Name = "btnOKReciboCobros";
            this.btnOKReciboCobros.Size = new System.Drawing.Size(70, 39);
            this.btnOKReciboCobros.TabIndex = 0;
            this.btnOKReciboCobros.Text = "OK";
            this.btnOKReciboCobros.UseVisualStyleBackColor = false;
            this.btnOKReciboCobros.Click += new System.EventHandler(this.btnOKReciboCobros_Click);
            // 
            // lblRecibo
            // 
            this.lblRecibo.AutoSize = true;
            this.lblRecibo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecibo.Location = new System.Drawing.Point(271, 9);
            this.lblRecibo.Name = "lblRecibo";
            this.lblRecibo.Size = new System.Drawing.Size(49, 15);
            this.lblRecibo.TabIndex = 20;
            this.lblRecibo.Text = "Recibo:";
            // 
            // txtNumRecibo
            // 
            this.txtNumRecibo.Location = new System.Drawing.Point(272, 34);
            this.txtNumRecibo.Multiline = true;
            this.txtNumRecibo.Name = "txtNumRecibo";
            this.txtNumRecibo.ReadOnly = true;
            this.txtNumRecibo.Size = new System.Drawing.Size(143, 21);
            this.txtNumRecibo.TabIndex = 21;
            this.txtNumRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbFormato
            // 
            this.cmbFormato.FormattingEnabled = true;
            this.cmbFormato.Location = new System.Drawing.Point(718, 32);
            this.cmbFormato.Name = "cmbFormato";
            this.cmbFormato.Size = new System.Drawing.Size(135, 21);
            this.cmbFormato.TabIndex = 14;
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(70, 32);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(174, 21);
            this.cmbEmpresa.TabIndex = 13;
            // 
            // lblFormato
            // 
            this.lblFormato.AutoSize = true;
            this.lblFormato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormato.Location = new System.Drawing.Point(715, 14);
            this.lblFormato.Name = "lblFormato";
            this.lblFormato.Size = new System.Drawing.Size(56, 15);
            this.lblFormato.TabIndex = 12;
            this.lblFormato.Text = "Formato:";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(7, 34);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(60, 15);
            this.lblEmpresa.TabIndex = 11;
            this.lblEmpresa.Text = "Empresa:";
            // 
            // txtFaltante
            // 
            this.txtFaltante.Location = new System.Drawing.Point(100, 619);
            this.txtFaltante.Multiline = true;
            this.txtFaltante.Name = "txtFaltante";
            this.txtFaltante.ReadOnly = true;
            this.txtFaltante.Size = new System.Drawing.Size(101, 21);
            this.txtFaltante.TabIndex = 39;
            // 
            // lblFaltante
            // 
            this.lblFaltante.AutoSize = true;
            this.lblFaltante.BackColor = System.Drawing.Color.Transparent;
            this.lblFaltante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaltante.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFaltante.Location = new System.Drawing.Point(24, 620);
            this.lblFaltante.Name = "lblFaltante";
            this.lblFaltante.Size = new System.Drawing.Size(54, 15);
            this.lblFaltante.TabIndex = 38;
            this.lblFaltante.Text = "Faltante:";
            // 
            // FReciboDeCobros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1027, 684);
            this.Controls.Add(this.tabReciboCobros);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FReciboDeCobros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Recibo de Cobros";
            this.Load += new System.EventHandler(this.FReciboDeCobros_Load);
            this.tabReciboCobros.ResumeLayout(false);
            this.tabPagReciboCobros.ResumeLayout(false);
            this.tabPagReciboCobros.PerformLayout();
            this.grpDocumentosPago.ResumeLayout(false);
            this.grpDocumentosPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosPago)).EndInit();
            this.grpCliente.ResumeLayout(false);
            this.grpCliente.PerformLayout();
            this.grpDocumentosPagados.ResumeLayout(false);
            this.grpDocumentosPagados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocumentosPagados)).EndInit();
            this.Grb_DatoReciboCobros.ResumeLayout(false);
            this.Grb_DatoReciboCobros.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabReciboCobros;
        private System.Windows.Forms.TabPage tabPagReciboCobros;
        private System.Windows.Forms.GroupBox grpDocumentosPago;
        private System.Windows.Forms.DataGridView dgvDocumentosPago;
        private System.Windows.Forms.TextBox txtTotalDocumento;
        private System.Windows.Forms.Label lblTotalDocumento;
        private System.Windows.Forms.GroupBox grpCliente;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.TextBox txtFechaPago;
        private System.Windows.Forms.Label lblFechaPago;
        private System.Windows.Forms.TextBox txtObserv;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblObserv;
        private System.Windows.Forms.Button btnLimpiarReciboCobros;
        private System.Windows.Forms.GroupBox grpDocumentosPagados;
        private System.Windows.Forms.TextBox txtTotal3;
        private System.Windows.Forms.TextBox txtTotal2;
        private System.Windows.Forms.TextBox txtTotal1;
        private System.Windows.Forms.Label lblTotales;
        private System.Windows.Forms.DataGridView dgvDocumentosPagados;
        private System.Windows.Forms.Button btnImprimirReciboCobros;
        private System.Windows.Forms.Button btnCerrarReciboCobros;
        private System.Windows.Forms.GroupBox Grb_DatoReciboCobros;
        private System.Windows.Forms.Button btnPregunta;
        private System.Windows.Forms.TextBox txtNombreRecibo;
        private System.Windows.Forms.Button btnOKReciboCobros;
        private System.Windows.Forms.Label lblRecibo;
        private System.Windows.Forms.TextBox txtNumRecibo;
        private MisControles.ComboDatos cmbFormato;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblFormato;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox txtFaltante;
        private System.Windows.Forms.Label lblFaltante;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTipoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValorFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmSaldoPagar;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValorPago;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTipoDocument;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFachaVcto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNume;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCuentaNtarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBanco;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmTitular;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmAutorizacion;
    }
}