namespace Palatium.Facturacion_Electronica
{
    partial class frmSincronizacionIndividual
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
            this.txtNumeroDocumento = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnValidar = new System.Windows.Forms.Button();
            this.txtEstadoProceso = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCorreoCliente = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDetalles_2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDetalles_1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFechaAutorizacion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumeroAutorizacion = new System.Windows.Forms.TextBox();
            this.txtEstadoEnvio = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtClaveAcceso = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbTipoComprobante = new MisControles.ComboDatos();
            this.btnOK = new System.Windows.Forms.Button();
            this.dB_Ayuda_Facturas = new Controles.Auxiliares.DB_Ayuda();
            this.lblTipoDocumento = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTipoAmbiente = new MisControles.ComboDatos();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTipoAmbiente_1 = new MisControles.ComboDatos();
            this.cmbTipoEmision = new MisControles.ComboDatos();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroDocumento.Location = new System.Drawing.Point(483, 32);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.ReadOnly = true;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(188, 22);
            this.txtNumeroDocumento.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(480, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Número de Documento";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.limpiar_ico;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(569, 254);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(102, 39);
            this.btnLimpiar.TabIndex = 40;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnValidar
            // 
            this.btnValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.Image = global::Palatium.Properties.Resources.enviar_png;
            this.btnValidar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValidar.Location = new System.Drawing.Point(461, 254);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(102, 39);
            this.btnValidar.TabIndex = 39;
            this.btnValidar.Text = "Procesar";
            this.btnValidar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValidar.UseVisualStyleBackColor = true;
            this.btnValidar.Click += new System.EventHandler(this.btnValidar_Click);
            // 
            // txtEstadoProceso
            // 
            this.txtEstadoProceso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEstadoProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoProceso.Location = new System.Drawing.Point(166, 271);
            this.txtEstadoProceso.Name = "txtEstadoProceso";
            this.txtEstadoProceso.ReadOnly = true;
            this.txtEstadoProceso.Size = new System.Drawing.Size(278, 22);
            this.txtEstadoProceso.TabIndex = 38;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 274);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 15);
            this.label11.TabIndex = 37;
            this.label11.Text = "Estado del Proceso";
            // 
            // txtCorreoCliente
            // 
            this.txtCorreoCliente.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCorreoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoCliente.Location = new System.Drawing.Point(166, 243);
            this.txtCorreoCliente.Name = "txtCorreoCliente";
            this.txtCorreoCliente.ReadOnly = true;
            this.txtCorreoCliente.Size = new System.Drawing.Size(278, 22);
            this.txtCorreoCliente.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(14, 246);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(152, 15);
            this.label12.TabIndex = 35;
            this.label12.Text = "Correo Electrónico Cliente:";
            // 
            // txtDetalles_2
            // 
            this.txtDetalles_2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDetalles_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetalles_2.Location = new System.Drawing.Point(18, 193);
            this.txtDetalles_2.Multiline = true;
            this.txtDetalles_2.Name = "txtDetalles_2";
            this.txtDetalles_2.ReadOnly = true;
            this.txtDetalles_2.Size = new System.Drawing.Size(653, 45);
            this.txtDetalles_2.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(15, 126);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(55, 15);
            this.label15.TabIndex = 33;
            this.label15.Text = "Detalles:";
            // 
            // txtDetalles_1
            // 
            this.txtDetalles_1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDetalles_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDetalles_1.Location = new System.Drawing.Point(18, 146);
            this.txtDetalles_1.Multiline = true;
            this.txtDetalles_1.Name = "txtDetalles_1";
            this.txtDetalles_1.ReadOnly = true;
            this.txtDetalles_1.Size = new System.Drawing.Size(653, 45);
            this.txtDetalles_1.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(14, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "Fecha de Autorización";
            // 
            // txtFechaAutorizacion
            // 
            this.txtFechaAutorizacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtFechaAutorizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaAutorizacion.Location = new System.Drawing.Point(166, 101);
            this.txtFechaAutorizacion.Name = "txtFechaAutorizacion";
            this.txtFechaAutorizacion.ReadOnly = true;
            this.txtFechaAutorizacion.Size = new System.Drawing.Size(427, 22);
            this.txtFechaAutorizacion.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Número de Autorización";
            // 
            // txtNumeroAutorizacion
            // 
            this.txtNumeroAutorizacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNumeroAutorizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroAutorizacion.Location = new System.Drawing.Point(166, 78);
            this.txtNumeroAutorizacion.Name = "txtNumeroAutorizacion";
            this.txtNumeroAutorizacion.ReadOnly = true;
            this.txtNumeroAutorizacion.Size = new System.Drawing.Size(427, 22);
            this.txtNumeroAutorizacion.TabIndex = 16;
            // 
            // txtEstadoEnvio
            // 
            this.txtEstadoEnvio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEstadoEnvio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoEnvio.Location = new System.Drawing.Point(166, 55);
            this.txtEstadoEnvio.Name = "txtEstadoEnvio";
            this.txtEstadoEnvio.ReadOnly = true;
            this.txtEstadoEnvio.Size = new System.Drawing.Size(278, 22);
            this.txtEstadoEnvio.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(14, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "Estado de Envío:";
            // 
            // txtClaveAcceso
            // 
            this.txtClaveAcceso.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtClaveAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveAcceso.Location = new System.Drawing.Point(17, 32);
            this.txtClaveAcceso.Name = "txtClaveAcceso";
            this.txtClaveAcceso.ReadOnly = true;
            this.txtClaveAcceso.Size = new System.Drawing.Size(427, 22);
            this.txtClaveAcceso.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Solicitar respuesta de envío:";
            // 
            // cmbTipoComprobante
            // 
            this.cmbTipoComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoComprobante.FormattingEnabled = true;
            this.cmbTipoComprobante.Location = new System.Drawing.Point(130, 19);
            this.cmbTipoComprobante.Name = "cmbTipoComprobante";
            this.cmbTipoComprobante.Size = new System.Drawing.Size(241, 24);
            this.cmbTipoComprobante.TabIndex = 19;
            this.cmbTipoComprobante.SelectedIndexChanged += new System.EventHandler(this.cmbTipoComprobante_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(626, 59);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(46, 28);
            this.btnOK.TabIndex = 18;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            // 
            // dB_Ayuda_Facturas
            // 
            this.dB_Ayuda_Facturas.iId = 0;
            this.dB_Ayuda_Facturas.Location = new System.Drawing.Point(216, 59);
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
            this.lblTipoDocumento.Location = new System.Drawing.Point(14, 62);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(108, 15);
            this.lblTipoDocumento.TabIndex = 4;
            this.lblTipoDocumento.Text = "lblTipoDocumento";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo Comprobante";
            // 
            // cmbTipoAmbiente
            // 
            this.cmbTipoAmbiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoAmbiente.FormattingEnabled = true;
            this.cmbTipoAmbiente.Location = new System.Drawing.Point(113, 14);
            this.cmbTipoAmbiente.Name = "cmbTipoAmbiente";
            this.cmbTipoAmbiente.Size = new System.Drawing.Size(172, 24);
            this.cmbTipoAmbiente.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 30);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tipo de Ambiente\r\nEmpresa";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmbTipoAmbiente);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(399, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(293, 45);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
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
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 91);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // cmbTipoAmbiente_1
            // 
            this.cmbTipoAmbiente_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoAmbiente_1.FormattingEnabled = true;
            this.cmbTipoAmbiente_1.Location = new System.Drawing.Point(500, 15);
            this.cmbTipoAmbiente_1.Name = "cmbTipoAmbiente_1";
            this.cmbTipoAmbiente_1.Size = new System.Drawing.Size(172, 24);
            this.cmbTipoAmbiente_1.TabIndex = 21;
            // 
            // cmbTipoEmision
            // 
            this.cmbTipoEmision.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoEmision.FormattingEnabled = true;
            this.cmbTipoEmision.Location = new System.Drawing.Point(129, 15);
            this.cmbTipoEmision.Name = "cmbTipoEmision";
            this.cmbTipoEmision.Size = new System.Drawing.Size(172, 24);
            this.cmbTipoEmision.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tipo de Ambiente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tipo de Emisión:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox3.Controls.Add(this.txtNumeroDocumento);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.btnLimpiar);
            this.groupBox3.Controls.Add(this.btnValidar);
            this.groupBox3.Controls.Add(this.txtEstadoProceso);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtCorreoCliente);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtDetalles_2);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtDetalles_1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtFechaAutorizacion);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtNumeroAutorizacion);
            this.groupBox3.Controls.Add(this.txtEstadoEnvio);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtClaveAcceso);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(13, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(703, 313);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox2.Controls.Add(this.cmbTipoAmbiente_1);
            this.groupBox2.Controls.Add(this.cmbTipoEmision);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 49);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // frmSincronizacionIndividual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(729, 468);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSincronizacionIndividual";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sincronización Individual de Facturas Electrónicas";
            this.Load += new System.EventHandler(this.frmSincronizacionIndividual_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNumeroDocumento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnValidar;
        private System.Windows.Forms.TextBox txtEstadoProceso;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCorreoCliente;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDetalles_2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDetalles_1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFechaAutorizacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumeroAutorizacion;
        private System.Windows.Forms.TextBox txtEstadoEnvio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtClaveAcceso;
        private System.Windows.Forms.Label label6;
        private MisControles.ComboDatos cmbTipoComprobante;
        private System.Windows.Forms.Button btnOK;
        private Controles.Auxiliares.DB_Ayuda dB_Ayuda_Facturas;
        private System.Windows.Forms.Label lblTipoDocumento;
        private System.Windows.Forms.Label label1;
        private MisControles.ComboDatos cmbTipoAmbiente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox1;
        private MisControles.ComboDatos cmbTipoAmbiente_1;
        private MisControles.ComboDatos cmbTipoEmision;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}