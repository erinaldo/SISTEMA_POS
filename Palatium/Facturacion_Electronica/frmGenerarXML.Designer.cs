namespace Palatium.Facturacion_Electronica
{
    partial class frmGenerarXML
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
            this.lblCodigoCajero = new System.Windows.Forms.Label();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.label1 = new System.Windows.Forms.Label();
            this.dbAyudaFacturas = new Controles.Auxiliares.DB_Ayuda();
            this.tabDatosFactura = new System.Windows.Forms.TabControl();
            this.tabDatosRegistro = new System.Windows.Forms.TabPage();
            this.dbAyudaCliente = new Controles.Auxiliares.DB_Ayuda();
            this.lblFormato = new System.Windows.Forms.Label();
            this.txtFechaVcto = new System.Windows.Forms.TextBox();
            this.lblFechaVcto = new System.Windows.Forms.Label();
            this.txtPorcientoDescuento = new System.Windows.Forms.TextBox();
            this.lblPorcientoDescuento = new System.Windows.Forms.Label();
            this.cmbTipoPago = new MisControles.ComboDatos();
            this.lblTipoPago = new System.Windows.Forms.Label();
            this.cmbTipoCliente = new MisControles.ComboDatos();
            this.lblTipoCliente = new System.Windows.Forms.Label();
            this.cmbMonedaFactura = new MisControles.ComboDatos();
            this.lblMonedaFactura = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.cmbFormato = new MisControles.ComboDatos();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.lblCiudad = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblNSerie2 = new System.Windows.Forms.Label();
            this.txtNSerie2 = new System.Windows.Forms.TextBox();
            this.cmbLocalidad2 = new MisControles.ComboDatos();
            this.lblLocalidad2 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.cmbVendedor = new MisControles.ComboDatos();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.txtNSerie1 = new System.Windows.Forms.TextBox();
            this.lbNSerie = new System.Windows.Forms.Label();
            this.tabEspecificaciones = new System.Windows.Forms.TabPage();
            this.cmbAutSri = new MisControles.ComboDatos();
            this.lblAutSri = new System.Windows.Forms.Label();
            this.txtNAut = new System.Windows.Forms.TextBox();
            this.lblNAut = new System.Windows.Forms.Label();
            this.txtMensajeFactura = new System.Windows.Forms.TextBox();
            this.lblMensajeFactura = new System.Windows.Forms.Label();
            this.txtPartidaArancelaria = new System.Windows.Forms.TextBox();
            this.lblPartidaArancelaria = new System.Windows.Forms.Label();
            this.txtNExportacion = new System.Windows.Forms.TextBox();
            this.lblNExportacion = new System.Windows.Forms.Label();
            this.txtPesoBruto = new System.Windows.Forms.TextBox();
            this.lblPesoBruto = new System.Windows.Forms.Label();
            this.txtPesoNeto = new System.Windows.Forms.TextBox();
            this.lblPesoNeto = new System.Windows.Forms.Label();
            this.txtFabricante = new System.Windows.Forms.TextBox();
            this.lblFabrMarca = new System.Windows.Forms.Label();
            this.txtRefOt = new System.Windows.Forms.TextBox();
            this.lblRefOt = new System.Windows.Forms.Label();
            this.txtObser = new System.Windows.Forms.TextBox();
            this.lblObservacion = new System.Windows.Forms.Label();
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
            this.rad4Decimales = new System.Windows.Forms.RadioButton();
            this.rad2Decimales = new System.Windows.Forms.RadioButton();
            this.btnGenerarXML = new System.Windows.Forms.Button();
            this.btnFormatoRide = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnCerrarCateraCobrar = new System.Windows.Forms.Button();
            this.btnOKFactura = new System.Windows.Forms.Button();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.tabDatosFactura.SuspendLayout();
            this.tabDatosRegistro.SuspendLayout();
            this.tabEspecificaciones.SuspendLayout();
            this.Grb_listReCateraCobrar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReimpresionFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCodigoCajero
            // 
            this.lblCodigoCajero.AutoSize = true;
            this.lblCodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoCajero.Location = new System.Drawing.Point(19, 26);
            this.lblCodigoCajero.Name = "lblCodigoCajero";
            this.lblCodigoCajero.Size = new System.Drawing.Size(64, 15);
            this.lblCodigoCajero.TabIndex = 4;
            this.lblCodigoCajero.Text = "Localidad:";
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(89, 25);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(169, 21);
            this.cmbLocalidad.TabIndex = 5;
            this.cmbLocalidad.SelectedIndexChanged += new System.EventHandler(this.cmbLocalidad_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(385, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Factura:";
            // 
            // dbAyudaFacturas
            // 
            this.dbAyudaFacturas.iId = 0;
            this.dbAyudaFacturas.Location = new System.Drawing.Point(442, 28);
            this.dbAyudaFacturas.Name = "dbAyudaFacturas";
            this.dbAyudaFacturas.sCodigo = null;
            this.dbAyudaFacturas.Size = new System.Drawing.Size(451, 20);
            this.dbAyudaFacturas.sNombre = null;
            this.dbAyudaFacturas.TabIndex = 7;
            // 
            // tabDatosFactura
            // 
            this.tabDatosFactura.Controls.Add(this.tabDatosRegistro);
            this.tabDatosFactura.Controls.Add(this.tabEspecificaciones);
            this.tabDatosFactura.Location = new System.Drawing.Point(17, 60);
            this.tabDatosFactura.Name = "tabDatosFactura";
            this.tabDatosFactura.SelectedIndex = 0;
            this.tabDatosFactura.Size = new System.Drawing.Size(1014, 197);
            this.tabDatosFactura.TabIndex = 53;
            // 
            // tabDatosRegistro
            // 
            this.tabDatosRegistro.Controls.Add(this.dbAyudaCliente);
            this.tabDatosRegistro.Controls.Add(this.lblFormato);
            this.tabDatosRegistro.Controls.Add(this.txtFechaVcto);
            this.tabDatosRegistro.Controls.Add(this.lblFechaVcto);
            this.tabDatosRegistro.Controls.Add(this.txtPorcientoDescuento);
            this.tabDatosRegistro.Controls.Add(this.lblPorcientoDescuento);
            this.tabDatosRegistro.Controls.Add(this.cmbTipoPago);
            this.tabDatosRegistro.Controls.Add(this.lblTipoPago);
            this.tabDatosRegistro.Controls.Add(this.cmbTipoCliente);
            this.tabDatosRegistro.Controls.Add(this.lblTipoCliente);
            this.tabDatosRegistro.Controls.Add(this.cmbMonedaFactura);
            this.tabDatosRegistro.Controls.Add(this.lblMonedaFactura);
            this.tabDatosRegistro.Controls.Add(this.txtTelefono);
            this.tabDatosRegistro.Controls.Add(this.lblTelefono);
            this.tabDatosRegistro.Controls.Add(this.txtDireccion);
            this.tabDatosRegistro.Controls.Add(this.lblDireccion);
            this.tabDatosRegistro.Controls.Add(this.cmbFormato);
            this.tabDatosRegistro.Controls.Add(this.txtCiudad);
            this.tabDatosRegistro.Controls.Add(this.lblCiudad);
            this.tabDatosRegistro.Controls.Add(this.lblCliente);
            this.tabDatosRegistro.Controls.Add(this.lblNSerie2);
            this.tabDatosRegistro.Controls.Add(this.txtNSerie2);
            this.tabDatosRegistro.Controls.Add(this.cmbLocalidad2);
            this.tabDatosRegistro.Controls.Add(this.lblLocalidad2);
            this.tabDatosRegistro.Controls.Add(this.txtFecha);
            this.tabDatosRegistro.Controls.Add(this.lblFecha);
            this.tabDatosRegistro.Controls.Add(this.cmbVendedor);
            this.tabDatosRegistro.Controls.Add(this.lblVendedor);
            this.tabDatosRegistro.Controls.Add(this.txtNSerie1);
            this.tabDatosRegistro.Controls.Add(this.lbNSerie);
            this.tabDatosRegistro.Location = new System.Drawing.Point(4, 22);
            this.tabDatosRegistro.Name = "tabDatosRegistro";
            this.tabDatosRegistro.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatosRegistro.Size = new System.Drawing.Size(1006, 171);
            this.tabDatosRegistro.TabIndex = 0;
            this.tabDatosRegistro.Text = "Datos de Registro";
            this.tabDatosRegistro.UseVisualStyleBackColor = true;
            // 
            // dbAyudaCliente
            // 
            this.dbAyudaCliente.iId = 0;
            this.dbAyudaCliente.Location = new System.Drawing.Point(60, 56);
            this.dbAyudaCliente.Name = "dbAyudaCliente";
            this.dbAyudaCliente.sCodigo = null;
            this.dbAyudaCliente.Size = new System.Drawing.Size(449, 32);
            this.dbAyudaCliente.sNombre = null;
            this.dbAyudaCliente.TabIndex = 54;
            // 
            // lblFormato
            // 
            this.lblFormato.AutoSize = true;
            this.lblFormato.BackColor = System.Drawing.Color.Transparent;
            this.lblFormato.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormato.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFormato.Location = new System.Drawing.Point(740, 58);
            this.lblFormato.Name = "lblFormato";
            this.lblFormato.Size = new System.Drawing.Size(56, 15);
            this.lblFormato.TabIndex = 102;
            this.lblFormato.Text = "Formato:";
            // 
            // txtFechaVcto
            // 
            this.txtFechaVcto.Location = new System.Drawing.Point(817, 131);
            this.txtFechaVcto.Multiline = true;
            this.txtFechaVcto.Name = "txtFechaVcto";
            this.txtFechaVcto.Size = new System.Drawing.Size(145, 21);
            this.txtFechaVcto.TabIndex = 101;
            // 
            // lblFechaVcto
            // 
            this.lblFechaVcto.AutoSize = true;
            this.lblFechaVcto.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaVcto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaVcto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaVcto.Location = new System.Drawing.Point(741, 133);
            this.lblFechaVcto.Name = "lblFechaVcto";
            this.lblFechaVcto.Size = new System.Drawing.Size(70, 15);
            this.lblFechaVcto.TabIndex = 100;
            this.lblFechaVcto.Text = "Fecha Vcto:";
            // 
            // txtPorcientoDescuento
            // 
            this.txtPorcientoDescuento.Location = new System.Drawing.Point(360, 131);
            this.txtPorcientoDescuento.Multiline = true;
            this.txtPorcientoDescuento.Name = "txtPorcientoDescuento";
            this.txtPorcientoDescuento.Size = new System.Drawing.Size(73, 21);
            this.txtPorcientoDescuento.TabIndex = 99;
            this.txtPorcientoDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPorcientoDescuento
            // 
            this.lblPorcientoDescuento.AutoSize = true;
            this.lblPorcientoDescuento.BackColor = System.Drawing.Color.Transparent;
            this.lblPorcientoDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPorcientoDescuento.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPorcientoDescuento.Location = new System.Drawing.Point(271, 133);
            this.lblPorcientoDescuento.Name = "lblPorcientoDescuento";
            this.lblPorcientoDescuento.Size = new System.Drawing.Size(83, 15);
            this.lblPorcientoDescuento.TabIndex = 98;
            this.lblPorcientoDescuento.Text = "% Descuento:";
            // 
            // cmbTipoPago
            // 
            this.cmbTipoPago.FormattingEnabled = true;
            this.cmbTipoPago.Location = new System.Drawing.Point(545, 132);
            this.cmbTipoPago.Name = "cmbTipoPago";
            this.cmbTipoPago.Size = new System.Drawing.Size(176, 21);
            this.cmbTipoPago.TabIndex = 97;
            // 
            // lblTipoPago
            // 
            this.lblTipoPago.AutoSize = true;
            this.lblTipoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPago.Location = new System.Drawing.Point(456, 133);
            this.lblTipoPago.Name = "lblTipoPago";
            this.lblTipoPago.Size = new System.Drawing.Size(83, 15);
            this.lblTipoPago.TabIndex = 96;
            this.lblTipoPago.Text = "Tipo de Pago:";
            // 
            // cmbTipoCliente
            // 
            this.cmbTipoCliente.FormattingEnabled = true;
            this.cmbTipoCliente.Location = new System.Drawing.Point(91, 131);
            this.cmbTipoCliente.Name = "cmbTipoCliente";
            this.cmbTipoCliente.Size = new System.Drawing.Size(154, 21);
            this.cmbTipoCliente.TabIndex = 95;
            // 
            // lblTipoCliente
            // 
            this.lblTipoCliente.AutoSize = true;
            this.lblTipoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoCliente.Location = new System.Drawing.Point(10, 137);
            this.lblTipoCliente.Name = "lblTipoCliente";
            this.lblTipoCliente.Size = new System.Drawing.Size(75, 15);
            this.lblTipoCliente.TabIndex = 94;
            this.lblTipoCliente.Text = "Tipo Cliente:";
            // 
            // cmbMonedaFactura
            // 
            this.cmbMonedaFactura.FormattingEnabled = true;
            this.cmbMonedaFactura.Location = new System.Drawing.Point(802, 95);
            this.cmbMonedaFactura.Name = "cmbMonedaFactura";
            this.cmbMonedaFactura.Size = new System.Drawing.Size(160, 21);
            this.cmbMonedaFactura.TabIndex = 93;
            // 
            // lblMonedaFactura
            // 
            this.lblMonedaFactura.AutoSize = true;
            this.lblMonedaFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonedaFactura.Location = new System.Drawing.Point(740, 96);
            this.lblMonedaFactura.Name = "lblMonedaFactura";
            this.lblMonedaFactura.Size = new System.Drawing.Size(56, 15);
            this.lblMonedaFactura.TabIndex = 92;
            this.lblMonedaFactura.Text = "Moneda:";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(603, 94);
            this.txtTelefono.Multiline = true;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(118, 21);
            this.txtTelefono.TabIndex = 91;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.BackColor = System.Drawing.Color.Transparent;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTelefono.Location = new System.Drawing.Point(539, 95);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(58, 15);
            this.lblTelefono.TabIndex = 90;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(73, 94);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(449, 21);
            this.txtDireccion.TabIndex = 89;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.BackColor = System.Drawing.Color.Transparent;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDireccion.Location = new System.Drawing.Point(10, 95);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(62, 15);
            this.lblDireccion.TabIndex = 88;
            this.lblDireccion.Text = "Dirección:";
            // 
            // cmbFormato
            // 
            this.cmbFormato.FormattingEnabled = true;
            this.cmbFormato.Location = new System.Drawing.Point(802, 55);
            this.cmbFormato.Name = "cmbFormato";
            this.cmbFormato.Size = new System.Drawing.Size(160, 21);
            this.cmbFormato.TabIndex = 87;
            // 
            // txtCiudad
            // 
            this.txtCiudad.Location = new System.Drawing.Point(582, 55);
            this.txtCiudad.Multiline = true;
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(139, 21);
            this.txtCiudad.TabIndex = 86;
            // 
            // lblCiudad
            // 
            this.lblCiudad.AutoSize = true;
            this.lblCiudad.BackColor = System.Drawing.Color.Transparent;
            this.lblCiudad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCiudad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCiudad.Location = new System.Drawing.Point(527, 58);
            this.lblCiudad.Name = "lblCiudad";
            this.lblCiudad.Size = new System.Drawing.Size(49, 15);
            this.lblCiudad.TabIndex = 85;
            this.lblCiudad.Text = "Ciudad:";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(10, 58);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 15);
            this.lblCliente.TabIndex = 81;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblNSerie2
            // 
            this.lblNSerie2.AutoSize = true;
            this.lblNSerie2.BackColor = System.Drawing.Color.Transparent;
            this.lblNSerie2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNSerie2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNSerie2.Location = new System.Drawing.Point(827, 20);
            this.lblNSerie2.Name = "lblNSerie2";
            this.lblNSerie2.Size = new System.Drawing.Size(66, 15);
            this.lblNSerie2.TabIndex = 79;
            this.lblNSerie2.Text = "N° Serie 2:";
            // 
            // txtNSerie2
            // 
            this.txtNSerie2.Location = new System.Drawing.Point(899, 18);
            this.txtNSerie2.Multiline = true;
            this.txtNSerie2.Name = "txtNSerie2";
            this.txtNSerie2.Size = new System.Drawing.Size(63, 21);
            this.txtNSerie2.TabIndex = 80;
            this.txtNSerie2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbLocalidad2
            // 
            this.cmbLocalidad2.FormattingEnabled = true;
            this.cmbLocalidad2.Location = new System.Drawing.Point(502, 17);
            this.cmbLocalidad2.Name = "cmbLocalidad2";
            this.cmbLocalidad2.Size = new System.Drawing.Size(154, 21);
            this.cmbLocalidad2.TabIndex = 78;
            // 
            // lblLocalidad2
            // 
            this.lblLocalidad2.AutoSize = true;
            this.lblLocalidad2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad2.Location = new System.Drawing.Point(435, 21);
            this.lblLocalidad2.Name = "lblLocalidad2";
            this.lblLocalidad2.Size = new System.Drawing.Size(61, 15);
            this.lblLocalidad2.TabIndex = 77;
            this.lblLocalidad2.Text = "Localidad";
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(60, 17);
            this.txtFecha.Multiline = true;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(107, 21);
            this.txtFecha.TabIndex = 75;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.Transparent;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecha.Location = new System.Drawing.Point(10, 20);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(44, 15);
            this.lblFecha.TabIndex = 72;
            this.lblFecha.Text = "Fecha:";
            // 
            // cmbVendedor
            // 
            this.cmbVendedor.FormattingEnabled = true;
            this.cmbVendedor.Location = new System.Drawing.Point(254, 17);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(156, 21);
            this.cmbVendedor.TabIndex = 76;
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.BackColor = System.Drawing.Color.Transparent;
            this.lblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendedor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblVendedor.Location = new System.Drawing.Point(182, 20);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(63, 15);
            this.lblVendedor.TabIndex = 71;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // txtNSerie1
            // 
            this.txtNSerie1.Location = new System.Drawing.Point(748, 17);
            this.txtNSerie1.Multiline = true;
            this.txtNSerie1.Name = "txtNSerie1";
            this.txtNSerie1.Size = new System.Drawing.Size(63, 21);
            this.txtNSerie1.TabIndex = 74;
            this.txtNSerie1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbNSerie
            // 
            this.lbNSerie.AutoSize = true;
            this.lbNSerie.BackColor = System.Drawing.Color.Transparent;
            this.lbNSerie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNSerie.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbNSerie.Location = new System.Drawing.Point(676, 19);
            this.lbNSerie.Name = "lbNSerie";
            this.lbNSerie.Size = new System.Drawing.Size(66, 15);
            this.lbNSerie.TabIndex = 73;
            this.lbNSerie.Text = "N° Serie 1:";
            // 
            // tabEspecificaciones
            // 
            this.tabEspecificaciones.Controls.Add(this.cmbAutSri);
            this.tabEspecificaciones.Controls.Add(this.lblAutSri);
            this.tabEspecificaciones.Controls.Add(this.txtNAut);
            this.tabEspecificaciones.Controls.Add(this.lblNAut);
            this.tabEspecificaciones.Controls.Add(this.txtMensajeFactura);
            this.tabEspecificaciones.Controls.Add(this.lblMensajeFactura);
            this.tabEspecificaciones.Controls.Add(this.txtPartidaArancelaria);
            this.tabEspecificaciones.Controls.Add(this.lblPartidaArancelaria);
            this.tabEspecificaciones.Controls.Add(this.txtNExportacion);
            this.tabEspecificaciones.Controls.Add(this.lblNExportacion);
            this.tabEspecificaciones.Controls.Add(this.txtPesoBruto);
            this.tabEspecificaciones.Controls.Add(this.lblPesoBruto);
            this.tabEspecificaciones.Controls.Add(this.txtPesoNeto);
            this.tabEspecificaciones.Controls.Add(this.lblPesoNeto);
            this.tabEspecificaciones.Controls.Add(this.txtFabricante);
            this.tabEspecificaciones.Controls.Add(this.lblFabrMarca);
            this.tabEspecificaciones.Controls.Add(this.txtRefOt);
            this.tabEspecificaciones.Controls.Add(this.lblRefOt);
            this.tabEspecificaciones.Controls.Add(this.txtObser);
            this.tabEspecificaciones.Controls.Add(this.lblObservacion);
            this.tabEspecificaciones.Location = new System.Drawing.Point(4, 22);
            this.tabEspecificaciones.Name = "tabEspecificaciones";
            this.tabEspecificaciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabEspecificaciones.Size = new System.Drawing.Size(1006, 171);
            this.tabEspecificaciones.TabIndex = 1;
            this.tabEspecificaciones.Text = "Especificaciones";
            this.tabEspecificaciones.UseVisualStyleBackColor = true;
            // 
            // cmbAutSri
            // 
            this.cmbAutSri.FormattingEnabled = true;
            this.cmbAutSri.Location = new System.Drawing.Point(612, 112);
            this.cmbAutSri.Name = "cmbAutSri";
            this.cmbAutSri.Size = new System.Drawing.Size(154, 21);
            this.cmbAutSri.TabIndex = 99;
            // 
            // lblAutSri
            // 
            this.lblAutSri.AutoSize = true;
            this.lblAutSri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAutSri.Location = new System.Drawing.Point(545, 113);
            this.lblAutSri.Name = "lblAutSri";
            this.lblAutSri.Size = new System.Drawing.Size(53, 15);
            this.lblAutSri.TabIndex = 98;
            this.lblAutSri.Text = "Aut. SRI:";
            // 
            // txtNAut
            // 
            this.txtNAut.Location = new System.Drawing.Point(877, 112);
            this.txtNAut.Multiline = true;
            this.txtNAut.Name = "txtNAut";
            this.txtNAut.Size = new System.Drawing.Size(118, 21);
            this.txtNAut.TabIndex = 97;
            // 
            // lblNAut
            // 
            this.lblNAut.AutoSize = true;
            this.lblNAut.BackColor = System.Drawing.Color.Transparent;
            this.lblNAut.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNAut.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNAut.Location = new System.Drawing.Point(827, 113);
            this.lblNAut.Name = "lblNAut";
            this.lblNAut.Size = new System.Drawing.Size(44, 15);
            this.lblNAut.TabIndex = 96;
            this.lblNAut.Text = "N° Aut.";
            // 
            // txtMensajeFactura
            // 
            this.txtMensajeFactura.Location = new System.Drawing.Point(156, 111);
            this.txtMensajeFactura.Multiline = true;
            this.txtMensajeFactura.Name = "txtMensajeFactura";
            this.txtMensajeFactura.Size = new System.Drawing.Size(302, 21);
            this.txtMensajeFactura.TabIndex = 95;
            // 
            // lblMensajeFactura
            // 
            this.lblMensajeFactura.AutoSize = true;
            this.lblMensajeFactura.BackColor = System.Drawing.Color.Transparent;
            this.lblMensajeFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeFactura.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMensajeFactura.Location = new System.Drawing.Point(29, 112);
            this.lblMensajeFactura.Name = "lblMensajeFactura";
            this.lblMensajeFactura.Size = new System.Drawing.Size(121, 15);
            this.lblMensajeFactura.TabIndex = 94;
            this.lblMensajeFactura.Text = "Mensaje fijo Factura:";
            // 
            // txtPartidaArancelaria
            // 
            this.txtPartidaArancelaria.Location = new System.Drawing.Point(877, 75);
            this.txtPartidaArancelaria.Multiline = true;
            this.txtPartidaArancelaria.Name = "txtPartidaArancelaria";
            this.txtPartidaArancelaria.Size = new System.Drawing.Size(118, 21);
            this.txtPartidaArancelaria.TabIndex = 93;
            // 
            // lblPartidaArancelaria
            // 
            this.lblPartidaArancelaria.AutoSize = true;
            this.lblPartidaArancelaria.BackColor = System.Drawing.Color.Transparent;
            this.lblPartidaArancelaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartidaArancelaria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPartidaArancelaria.Location = new System.Drawing.Point(757, 78);
            this.lblPartidaArancelaria.Name = "lblPartidaArancelaria";
            this.lblPartidaArancelaria.Size = new System.Drawing.Size(114, 15);
            this.lblPartidaArancelaria.TabIndex = 92;
            this.lblPartidaArancelaria.Text = "Partida Arancelaria:";
            // 
            // txtNExportacion
            // 
            this.txtNExportacion.Location = new System.Drawing.Point(616, 75);
            this.txtNExportacion.Multiline = true;
            this.txtNExportacion.Name = "txtNExportacion";
            this.txtNExportacion.Size = new System.Drawing.Size(118, 21);
            this.txtNExportacion.TabIndex = 91;
            // 
            // lblNExportacion
            // 
            this.lblNExportacion.AutoSize = true;
            this.lblNExportacion.BackColor = System.Drawing.Color.Transparent;
            this.lblNExportacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNExportacion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNExportacion.Location = new System.Drawing.Point(459, 78);
            this.lblNExportacion.Name = "lblNExportacion";
            this.lblNExportacion.Size = new System.Drawing.Size(109, 15);
            this.lblNExportacion.TabIndex = 90;
            this.lblNExportacion.Text = "N° de Exportación:";
            // 
            // txtPesoBruto
            // 
            this.txtPesoBruto.Location = new System.Drawing.Point(319, 76);
            this.txtPesoBruto.Multiline = true;
            this.txtPesoBruto.Name = "txtPesoBruto";
            this.txtPesoBruto.Size = new System.Drawing.Size(118, 21);
            this.txtPesoBruto.TabIndex = 89;
            // 
            // lblPesoBruto
            // 
            this.lblPesoBruto.AutoSize = true;
            this.lblPesoBruto.BackColor = System.Drawing.Color.Transparent;
            this.lblPesoBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPesoBruto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPesoBruto.Location = new System.Drawing.Point(245, 77);
            this.lblPesoBruto.Name = "lblPesoBruto";
            this.lblPesoBruto.Size = new System.Drawing.Size(70, 15);
            this.lblPesoBruto.TabIndex = 88;
            this.lblPesoBruto.Text = "Peso Bruto:";
            // 
            // txtPesoNeto
            // 
            this.txtPesoNeto.Location = new System.Drawing.Point(107, 75);
            this.txtPesoNeto.Multiline = true;
            this.txtPesoNeto.Name = "txtPesoNeto";
            this.txtPesoNeto.Size = new System.Drawing.Size(118, 21);
            this.txtPesoNeto.TabIndex = 87;
            // 
            // lblPesoNeto
            // 
            this.lblPesoNeto.AutoSize = true;
            this.lblPesoNeto.BackColor = System.Drawing.Color.Transparent;
            this.lblPesoNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPesoNeto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPesoNeto.Location = new System.Drawing.Point(29, 76);
            this.lblPesoNeto.Name = "lblPesoNeto";
            this.lblPesoNeto.Size = new System.Drawing.Size(67, 15);
            this.lblPesoNeto.TabIndex = 86;
            this.lblPesoNeto.Text = "Peso Neto:";
            // 
            // txtFabricante
            // 
            this.txtFabricante.Location = new System.Drawing.Point(616, 38);
            this.txtFabricante.Multiline = true;
            this.txtFabricante.Name = "txtFabricante";
            this.txtFabricante.Size = new System.Drawing.Size(118, 21);
            this.txtFabricante.TabIndex = 85;
            // 
            // lblFabrMarca
            // 
            this.lblFabrMarca.AutoSize = true;
            this.lblFabrMarca.BackColor = System.Drawing.Color.Transparent;
            this.lblFabrMarca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFabrMarca.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFabrMarca.Location = new System.Drawing.Point(459, 39);
            this.lblFabrMarca.Name = "lblFabrMarca";
            this.lblFabrMarca.Size = new System.Drawing.Size(154, 15);
            this.lblFabrMarca.TabIndex = 84;
            this.lblFabrMarca.Text = "Fabr./N. de Entrega Marca:";
            // 
            // txtRefOt
            // 
            this.txtRefOt.Location = new System.Drawing.Point(319, 38);
            this.txtRefOt.Multiline = true;
            this.txtRefOt.Name = "txtRefOt";
            this.txtRefOt.Size = new System.Drawing.Size(118, 21);
            this.txtRefOt.TabIndex = 83;
            // 
            // lblRefOt
            // 
            this.lblRefOt.AutoSize = true;
            this.lblRefOt.BackColor = System.Drawing.Color.Transparent;
            this.lblRefOt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefOt.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRefOt.Location = new System.Drawing.Point(245, 39);
            this.lblRefOt.Name = "lblRefOt";
            this.lblRefOt.Size = new System.Drawing.Size(54, 15);
            this.lblRefOt.TabIndex = 82;
            this.lblRefOt.Text = "Ref. O.T.";
            // 
            // txtObser
            // 
            this.txtObser.Location = new System.Drawing.Point(107, 38);
            this.txtObser.Multiline = true;
            this.txtObser.Name = "txtObser";
            this.txtObser.Size = new System.Drawing.Size(118, 21);
            this.txtObser.TabIndex = 81;
            // 
            // lblObservacion
            // 
            this.lblObservacion.AutoSize = true;
            this.lblObservacion.BackColor = System.Drawing.Color.Transparent;
            this.lblObservacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObservacion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblObservacion.Location = new System.Drawing.Point(29, 39);
            this.lblObservacion.Name = "lblObservacion";
            this.lblObservacion.Size = new System.Drawing.Size(43, 15);
            this.lblObservacion.TabIndex = 80;
            this.lblObservacion.Text = "Obser.";
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
            this.Grb_listReCateraCobrar.Location = new System.Drawing.Point(17, 262);
            this.Grb_listReCateraCobrar.Name = "Grb_listReCateraCobrar";
            this.Grb_listReCateraCobrar.Size = new System.Drawing.Size(1014, 239);
            this.Grb_listReCateraCobrar.TabIndex = 54;
            this.Grb_listReCateraCobrar.TabStop = false;
            this.Grb_listReCateraCobrar.Text = "Lista de Registros";
            // 
            // txtTotalPagar
            // 
            this.txtTotalPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPagar.ForeColor = System.Drawing.Color.Red;
            this.txtTotalPagar.Location = new System.Drawing.Point(901, 202);
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
            this.lblTotalPagar.Location = new System.Drawing.Point(901, 184);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(83, 15);
            this.lblTotalPagar.TabIndex = 50;
            this.lblTotalPagar.Text = "Total a Pagar:";
            // 
            // txtIva
            // 
            this.txtIva.Location = new System.Drawing.Point(683, 202);
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
            this.lblIva.Location = new System.Drawing.Point(686, 184);
            this.lblIva.Name = "lblIva";
            this.lblIva.Size = new System.Drawing.Size(39, 15);
            this.lblIva.TabIndex = 48;
            this.lblIva.Text = "I. V. A.";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Location = new System.Drawing.Point(574, 202);
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
            this.lblSubTotal.Location = new System.Drawing.Point(571, 184);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(62, 15);
            this.lblSubTotal.TabIndex = 46;
            this.lblSubTotal.Text = "Sub Total:";
            // 
            // txtServicio
            // 
            this.txtServicio.Location = new System.Drawing.Point(792, 202);
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
            this.lblServicio.Location = new System.Drawing.Point(797, 184);
            this.lblServicio.Name = "lblServicio";
            this.lblServicio.Size = new System.Drawing.Size(53, 15);
            this.lblServicio.TabIndex = 44;
            this.lblServicio.Text = "Servicio:";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Location = new System.Drawing.Point(465, 202);
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
            this.lblDescuento.Location = new System.Drawing.Point(465, 184);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(69, 15);
            this.lblDescuento.TabIndex = 42;
            this.lblDescuento.Text = "Descuento:";
            // 
            // txtValorBruto
            // 
            this.txtValorBruto.Location = new System.Drawing.Point(356, 202);
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
            this.lblValor.Location = new System.Drawing.Point(353, 184);
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
            this.dgvReimpresionFactura.Size = new System.Drawing.Size(945, 157);
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
            // rad4Decimales
            // 
            this.rad4Decimales.AutoSize = true;
            this.rad4Decimales.Location = new System.Drawing.Point(506, 525);
            this.rad4Decimales.Name = "rad4Decimales";
            this.rad4Decimales.Size = new System.Drawing.Size(83, 17);
            this.rad4Decimales.TabIndex = 61;
            this.rad4Decimales.Text = "4 Decimales";
            this.rad4Decimales.UseVisualStyleBackColor = true;
            // 
            // rad2Decimales
            // 
            this.rad2Decimales.AutoSize = true;
            this.rad2Decimales.Checked = true;
            this.rad2Decimales.Location = new System.Drawing.Point(506, 507);
            this.rad2Decimales.Name = "rad2Decimales";
            this.rad2Decimales.Size = new System.Drawing.Size(83, 17);
            this.rad2Decimales.TabIndex = 60;
            this.rad2Decimales.TabStop = true;
            this.rad2Decimales.Text = "2 Decimales";
            this.rad2Decimales.UseVisualStyleBackColor = true;
            // 
            // btnGenerarXML
            // 
            this.btnGenerarXML.BackColor = System.Drawing.Color.Maroon;
            this.btnGenerarXML.ForeColor = System.Drawing.Color.White;
            this.btnGenerarXML.Location = new System.Drawing.Point(653, 507);
            this.btnGenerarXML.Name = "btnGenerarXML";
            this.btnGenerarXML.Size = new System.Drawing.Size(70, 39);
            this.btnGenerarXML.TabIndex = 59;
            this.btnGenerarXML.Text = "Generar XML";
            this.ttMensaje.SetToolTip(this.btnGenerarXML, "Clic aquí para generar el formato XML de la factura");
            this.btnGenerarXML.UseVisualStyleBackColor = false;
            this.btnGenerarXML.Click += new System.EventHandler(this.btnGenerarXML_Click);
            // 
            // btnFormatoRide
            // 
            this.btnFormatoRide.BackColor = System.Drawing.Color.Olive;
            this.btnFormatoRide.ForeColor = System.Drawing.Color.White;
            this.btnFormatoRide.Location = new System.Drawing.Point(729, 507);
            this.btnFormatoRide.Name = "btnFormatoRide";
            this.btnFormatoRide.Size = new System.Drawing.Size(70, 39);
            this.btnFormatoRide.TabIndex = 58;
            this.btnFormatoRide.Text = "Formato RIDE";
            this.ttMensaje.SetToolTip(this.btnFormatoRide, "Clic aquí para visualizar el RIDE de la factura");
            this.btnFormatoRide.UseVisualStyleBackColor = false;
            this.btnFormatoRide.Click += new System.EventHandler(this.btnFormatoRide_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(805, 507);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 57;
            this.btnLimpiar.Text = "Limpiar";
            this.ttMensaje.SetToolTip(this.btnLimpiar, "Clic aquí para limpiar el formulario");
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(881, 507);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(70, 39);
            this.btnImprimir.TabIndex = 55;
            this.btnImprimir.Text = "Imprimir";
            this.ttMensaje.SetToolTip(this.btnImprimir, "Clic aquí para imprimir la factura");
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCerrarCateraCobrar
            // 
            this.btnCerrarCateraCobrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarCateraCobrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrarCateraCobrar.Location = new System.Drawing.Point(957, 507);
            this.btnCerrarCateraCobrar.Name = "btnCerrarCateraCobrar";
            this.btnCerrarCateraCobrar.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarCateraCobrar.TabIndex = 56;
            this.btnCerrarCateraCobrar.Text = "Cerrar";
            this.ttMensaje.SetToolTip(this.btnCerrarCateraCobrar, "Clic aquí para cerrar");
            this.btnCerrarCateraCobrar.UseVisualStyleBackColor = false;
            this.btnCerrarCateraCobrar.Click += new System.EventHandler(this.btnCerrarCateraCobrar_Click);
            // 
            // btnOKFactura
            // 
            this.btnOKFactura.BackColor = System.Drawing.Color.Blue;
            this.btnOKFactura.ForeColor = System.Drawing.Color.White;
            this.btnOKFactura.Location = new System.Drawing.Point(899, 22);
            this.btnOKFactura.Name = "btnOKFactura";
            this.btnOKFactura.Size = new System.Drawing.Size(56, 29);
            this.btnOKFactura.TabIndex = 63;
            this.btnOKFactura.Text = "OK";
            this.btnOKFactura.UseVisualStyleBackColor = false;
            this.btnOKFactura.Click += new System.EventHandler(this.btnOKFactura_Click);
            // 
            // txtMail
            // 
            this.txtMail.BackColor = System.Drawing.Color.White;
            this.txtMail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtMail.Location = new System.Drawing.Point(72, 517);
            this.txtMail.Multiline = true;
            this.txtMail.Name = "txtMail";
            this.txtMail.ReadOnly = true;
            this.txtMail.Size = new System.Drawing.Size(247, 21);
            this.txtMail.TabIndex = 91;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(21, 518);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 90;
            this.label2.Text = "E:Mail:";
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Transparent;
            this.btnEditar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.Image = global::Palatium.Properties.Resources.editar_img;
            this.btnEditar.Location = new System.Drawing.Point(325, 513);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(33, 29);
            this.btnEditar.TabIndex = 92;
            this.ttMensaje.SetToolTip(this.btnEditar, "Clic aquí para editar el correo electrónico del cliente");
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGuardar.Location = new System.Drawing.Point(364, 513);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(33, 29);
            this.btnGuardar.TabIndex = 93;
            this.ttMensaje.SetToolTip(this.btnGuardar, "Clic aquí para actualizar el correo electrónico del  cliente");
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmGenerarXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1048, 555);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.txtMail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOKFactura);
            this.Controls.Add(this.rad4Decimales);
            this.Controls.Add(this.rad2Decimales);
            this.Controls.Add(this.btnGenerarXML);
            this.Controls.Add(this.btnFormatoRide);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnCerrarCateraCobrar);
            this.Controls.Add(this.Grb_listReCateraCobrar);
            this.Controls.Add(this.tabDatosFactura);
            this.Controls.Add(this.dbAyudaFacturas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLocalidad);
            this.Controls.Add(this.lblCodigoCajero);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGenerarXML";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar XML - Reimpresión de Facturas";
            this.Load += new System.EventHandler(this.frmGenerarXML_Load);
            this.tabDatosFactura.ResumeLayout(false);
            this.tabDatosRegistro.ResumeLayout(false);
            this.tabDatosRegistro.PerformLayout();
            this.tabEspecificaciones.ResumeLayout(false);
            this.tabEspecificaciones.PerformLayout();
            this.Grb_listReCateraCobrar.ResumeLayout(false);
            this.Grb_listReCateraCobrar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReimpresionFactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCodigoCajero;
        private MisControles.ComboDatos cmbLocalidad;
        private System.Windows.Forms.Label label1;
        private Controles.Auxiliares.DB_Ayuda dbAyudaFacturas;
        private System.Windows.Forms.TabControl tabDatosFactura;
        private System.Windows.Forms.TabPage tabDatosRegistro;
        private System.Windows.Forms.Label lblFormato;
        private System.Windows.Forms.TextBox txtFechaVcto;
        private System.Windows.Forms.Label lblFechaVcto;
        private System.Windows.Forms.TextBox txtPorcientoDescuento;
        private System.Windows.Forms.Label lblPorcientoDescuento;
        private MisControles.ComboDatos cmbTipoPago;
        private System.Windows.Forms.Label lblTipoPago;
        private MisControles.ComboDatos cmbTipoCliente;
        private System.Windows.Forms.Label lblTipoCliente;
        private MisControles.ComboDatos cmbMonedaFactura;
        private System.Windows.Forms.Label lblMonedaFactura;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblDireccion;
        private MisControles.ComboDatos cmbFormato;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label lblCiudad;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblNSerie2;
        private System.Windows.Forms.TextBox txtNSerie2;
        private MisControles.ComboDatos cmbLocalidad2;
        private System.Windows.Forms.Label lblLocalidad2;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private MisControles.ComboDatos cmbVendedor;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.TextBox txtNSerie1;
        private System.Windows.Forms.Label lbNSerie;
        private System.Windows.Forms.TabPage tabEspecificaciones;
        private MisControles.ComboDatos cmbAutSri;
        private System.Windows.Forms.Label lblAutSri;
        private System.Windows.Forms.TextBox txtNAut;
        private System.Windows.Forms.Label lblNAut;
        private System.Windows.Forms.TextBox txtMensajeFactura;
        private System.Windows.Forms.Label lblMensajeFactura;
        private System.Windows.Forms.TextBox txtPartidaArancelaria;
        private System.Windows.Forms.Label lblPartidaArancelaria;
        private System.Windows.Forms.TextBox txtNExportacion;
        private System.Windows.Forms.Label lblNExportacion;
        private System.Windows.Forms.TextBox txtPesoBruto;
        private System.Windows.Forms.Label lblPesoBruto;
        private System.Windows.Forms.TextBox txtPesoNeto;
        private System.Windows.Forms.Label lblPesoNeto;
        private System.Windows.Forms.TextBox txtFabricante;
        private System.Windows.Forms.Label lblFabrMarca;
        private System.Windows.Forms.TextBox txtRefOt;
        private System.Windows.Forms.Label lblRefOt;
        private System.Windows.Forms.TextBox txtObser;
        private System.Windows.Forms.Label lblObservacion;
        private Controles.Auxiliares.DB_Ayuda dbAyudaCliente;
        private System.Windows.Forms.GroupBox Grb_listReCateraCobrar;
        private System.Windows.Forms.TextBox txtTotalPagar;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.TextBox txtIva;
        private System.Windows.Forms.Label lblIva;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.TextBox txtValorBruto;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.DataGridView dgvReimpresionFactura;
        private System.Windows.Forms.RadioButton rad4Decimales;
        private System.Windows.Forms.RadioButton rad2Decimales;
        private System.Windows.Forms.Button btnGenerarXML;
        private System.Windows.Forms.Button btnFormatoRide;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnCerrarCateraCobrar;
        private System.Windows.Forms.Button btnOKFactura;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.Label lblServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVUnidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmVTotal;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
    }
}