namespace Palatium.Facturacion_Electronica
{
    partial class frmEnviarComprobanteElectronico
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbTipoAmbiente = new MisControles.ComboDatos();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipoComprobante = new MisControles.ComboDatos();
            this.btnOK = new System.Windows.Forms.Button();
            this.dB_Ayuda_Facturas = new Controles.Auxiliares.DB_Ayuda();
            this.lblTipoDocumento = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtArchivoEnviar = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtRutaArchivosFirmados = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNumeroDocumento = new System.Windows.Forms.TextBox();
            this.txtClaveAcceso = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTipoAmbiente_1 = new MisControles.ComboDatos();
            this.cmbTipoEmision = new MisControles.ComboDatos();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDetallesEnvio = new System.Windows.Forms.TextBox();
            this.txtInformacionAdicionalEnvio = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTipoEnvio = new System.Windows.Forms.TextBox();
            this.txtCodigoEnvio = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMensajeErrorEnvio = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEstadoEnvio = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.cmbTipoComprobante);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.dB_Ayuda_Facturas);
            this.groupBox1.Controls.Add(this.lblTipoDocumento);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 97);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmbTipoAmbiente);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(399, 9);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(293, 45);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            // 
            // cmbTipoAmbiente
            // 
            this.cmbTipoAmbiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoAmbiente.FormattingEnabled = true;
            this.cmbTipoAmbiente.Location = new System.Drawing.Point(113, 11);
            this.cmbTipoAmbiente.Name = "cmbTipoAmbiente";
            this.cmbTipoAmbiente.Size = new System.Drawing.Size(172, 24);
            this.cmbTipoAmbiente.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 30);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tipo de Ambiente\r\nEmpresa";
            // 
            // cmbTipoComprobante
            // 
            this.cmbTipoComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoComprobante.FormattingEnabled = true;
            this.cmbTipoComprobante.Location = new System.Drawing.Point(130, 22);
            this.cmbTipoComprobante.Name = "cmbTipoComprobante";
            this.cmbTipoComprobante.Size = new System.Drawing.Size(241, 24);
            this.cmbTipoComprobante.TabIndex = 19;
            this.cmbTipoComprobante.SelectedIndexChanged += new System.EventHandler(this.cmbTipoComprobante_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(626, 62);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(46, 28);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dB_Ayuda_Facturas
            // 
            this.dB_Ayuda_Facturas.iId = 0;
            this.dB_Ayuda_Facturas.Location = new System.Drawing.Point(216, 62);
            this.dB_Ayuda_Facturas.Name = "dB_Ayuda_Facturas";
            this.dB_Ayuda_Facturas.sCodigo = null;
            this.dB_Ayuda_Facturas.Size = new System.Drawing.Size(393, 22);
            this.dB_Ayuda_Facturas.sNombre = null;
            this.dB_Ayuda_Facturas.TabIndex = 5;
            // 
            // lblTipoDocumento
            // 
            this.lblTipoDocumento.AutoSize = true;
            this.lblTipoDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoDocumento.Location = new System.Drawing.Point(14, 65);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(108, 15);
            this.lblTipoDocumento.TabIndex = 4;
            this.lblTipoDocumento.Text = "lblTipoDocumento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo Comprobante";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox3.Controls.Add(this.txtArchivoEnviar);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtRutaArchivosFirmados);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtNumeroDocumento);
            this.groupBox3.Controls.Add(this.txtClaveAcceso);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(13, 152);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(703, 105);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            // 
            // txtArchivoEnviar
            // 
            this.txtArchivoEnviar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtArchivoEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArchivoEnviar.Location = new System.Drawing.Point(166, 73);
            this.txtArchivoEnviar.Name = "txtArchivoEnviar";
            this.txtArchivoEnviar.ReadOnly = true;
            this.txtArchivoEnviar.Size = new System.Drawing.Size(278, 22);
            this.txtArchivoEnviar.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(14, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "Archivo a enviar:";
            // 
            // txtRutaArchivosFirmados
            // 
            this.txtRutaArchivosFirmados.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRutaArchivosFirmados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRutaArchivosFirmados.Location = new System.Drawing.Point(166, 50);
            this.txtRutaArchivosFirmados.Name = "txtRutaArchivosFirmados";
            this.txtRutaArchivosFirmados.ReadOnly = true;
            this.txtRutaArchivosFirmados.Size = new System.Drawing.Size(278, 22);
            this.txtRutaArchivosFirmados.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "Ruta archivos firmados";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Location = new System.Drawing.Point(483, 27);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.ReadOnly = true;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(188, 22);
            this.txtNumeroDocumento.TabIndex = 9;
            // 
            // txtClaveAcceso
            // 
            this.txtClaveAcceso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtClaveAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveAcceso.Location = new System.Drawing.Point(17, 27);
            this.txtClaveAcceso.Name = "txtClaveAcceso";
            this.txtClaveAcceso.ReadOnly = true;
            this.txtClaveAcceso.Size = new System.Drawing.Size(427, 22);
            this.txtClaveAcceso.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(480, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "Número de Documento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Clave de Acceso del Registro";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox2.Controls.Add(this.cmbTipoAmbiente_1);
            this.groupBox2.Controls.Add(this.cmbTipoEmision);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 50);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // cmbTipoAmbiente_1
            // 
            this.cmbTipoAmbiente_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoAmbiente_1.FormattingEnabled = true;
            this.cmbTipoAmbiente_1.Location = new System.Drawing.Point(500, 14);
            this.cmbTipoAmbiente_1.Name = "cmbTipoAmbiente_1";
            this.cmbTipoAmbiente_1.Size = new System.Drawing.Size(172, 24);
            this.cmbTipoAmbiente_1.TabIndex = 21;
            // 
            // cmbTipoEmision
            // 
            this.cmbTipoEmision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoEmision.FormattingEnabled = true;
            this.cmbTipoEmision.Location = new System.Drawing.Point(129, 14);
            this.cmbTipoEmision.Name = "cmbTipoEmision";
            this.cmbTipoEmision.Size = new System.Drawing.Size(172, 24);
            this.cmbTipoEmision.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tipo de Ambiente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo de Emisión:";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox4.Controls.Add(this.btnLimpiar);
            this.groupBox4.Controls.Add(this.btnEnviar);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtDetallesEnvio);
            this.groupBox4.Controls.Add(this.txtInformacionAdicionalEnvio);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txtTipoEnvio);
            this.groupBox4.Controls.Add(this.txtCodigoEnvio);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtMensajeErrorEnvio);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtEstadoEnvio);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtMensaje);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(11, 258);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(705, 230);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.limpiar_ico;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(358, 182);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(102, 39);
            this.btnLimpiar.TabIndex = 17;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Image = global::Palatium.Properties.Resources.enviar_png;
            this.btnEnviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnviar.Location = new System.Drawing.Point(250, 182);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(102, 39);
            this.btnEnviar.TabIndex = 16;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(14, 164);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 15);
            this.label15.TabIndex = 31;
            this.label15.Text = "Detalles:";
            // 
            // txtDetallesEnvio
            // 
            this.txtDetallesEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDetallesEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetallesEnvio.Location = new System.Drawing.Point(117, 157);
            this.txtDetallesEnvio.Name = "txtDetallesEnvio";
            this.txtDetallesEnvio.ReadOnly = true;
            this.txtDetallesEnvio.Size = new System.Drawing.Size(553, 22);
            this.txtDetallesEnvio.TabIndex = 30;
            // 
            // txtInformacionAdicionalEnvio
            // 
            this.txtInformacionAdicionalEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtInformacionAdicionalEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInformacionAdicionalEnvio.Location = new System.Drawing.Point(118, 93);
            this.txtInformacionAdicionalEnvio.Multiline = true;
            this.txtInformacionAdicionalEnvio.Name = "txtInformacionAdicionalEnvio";
            this.txtInformacionAdicionalEnvio.ReadOnly = true;
            this.txtInformacionAdicionalEnvio.Size = new System.Drawing.Size(552, 62);
            this.txtInformacionAdicionalEnvio.TabIndex = 29;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(13, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 30);
            this.label14.TabIndex = 28;
            this.label14.Text = "Información\r\nAdicional:\r\n";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(229, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 15);
            this.label13.TabIndex = 27;
            this.label13.Text = "Tipo:";
            // 
            // txtTipoEnvio
            // 
            this.txtTipoEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTipoEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTipoEnvio.Location = new System.Drawing.Point(276, 43);
            this.txtTipoEnvio.Name = "txtTipoEnvio";
            this.txtTipoEnvio.ReadOnly = true;
            this.txtTipoEnvio.Size = new System.Drawing.Size(75, 22);
            this.txtTipoEnvio.TabIndex = 26;
            // 
            // txtCodigoEnvio
            // 
            this.txtCodigoEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCodigoEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoEnvio.Location = new System.Drawing.Point(117, 43);
            this.txtCodigoEnvio.Name = "txtCodigoEnvio";
            this.txtCodigoEnvio.ReadOnly = true;
            this.txtCodigoEnvio.Size = new System.Drawing.Size(75, 22);
            this.txtCodigoEnvio.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 46);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "Codigo de error:";
            // 
            // txtMensajeErrorEnvio
            // 
            this.txtMensajeErrorEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMensajeErrorEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensajeErrorEnvio.Location = new System.Drawing.Point(449, 43);
            this.txtMensajeErrorEnvio.Multiline = true;
            this.txtMensajeErrorEnvio.Name = "txtMensajeErrorEnvio";
            this.txtMensajeErrorEnvio.ReadOnly = true;
            this.txtMensajeErrorEnvio.Size = new System.Drawing.Size(221, 46);
            this.txtMensajeErrorEnvio.TabIndex = 23;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(385, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 30);
            this.label11.TabIndex = 22;
            this.label11.Text = "Mensaje\r\nde error:";
            // 
            // txtEstadoEnvio
            // 
            this.txtEstadoEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEstadoEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoEnvio.Location = new System.Drawing.Point(449, 19);
            this.txtEstadoEnvio.Name = "txtEstadoEnvio";
            this.txtEstadoEnvio.ReadOnly = true;
            this.txtEstadoEnvio.Size = new System.Drawing.Size(221, 22);
            this.txtEstadoEnvio.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(385, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 15);
            this.label8.TabIndex = 20;
            this.label8.Text = "Estado:";
            // 
            // txtMensaje
            // 
            this.txtMensaje.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensaje.Location = new System.Drawing.Point(117, 16);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.ReadOnly = true;
            this.txtMensaje.Size = new System.Drawing.Size(234, 22);
            this.txtMensaje.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Mensaje:";
            // 
            // frmEnviarComprobanteElectronico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(729, 490);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmEnviarComprobanteElectronico";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enviar Comprobante Electrónico Firmado al SRI OFFLINE";
            this.Load += new System.EventHandler(this.frmEnviarComprobanteElectronico_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private MisControles.ComboDatos cmbTipoComprobante;
        private System.Windows.Forms.Button btnOK;
        private Controles.Auxiliares.DB_Ayuda dB_Ayuda_Facturas;
        private System.Windows.Forms.Label lblTipoDocumento;
        private System.Windows.Forms.Label label1;
        private MisControles.ComboDatos cmbTipoAmbiente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtArchivoEnviar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRutaArchivosFirmados;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNumeroDocumento;
        private System.Windows.Forms.TextBox txtClaveAcceso;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private MisControles.ComboDatos cmbTipoAmbiente_1;
        private MisControles.ComboDatos cmbTipoEmision;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDetallesEnvio;
        private System.Windows.Forms.TextBox txtInformacionAdicionalEnvio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTipoEnvio;
        private System.Windows.Forms.TextBox txtCodigoEnvio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMensajeErrorEnvio;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEstadoEnvio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}