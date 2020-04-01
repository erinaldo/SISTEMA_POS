namespace Palatium.Formularios
{
    partial class FInformacionPosOriOrd
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
            this.tabCon_PosOriOrd = new System.Windows.Forms.TabControl();
            this.tabPag_PosOriOrd = new System.Windows.Forms.TabPage();
            this.Grb_listRePosOriOrd = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.chkPagoAnticipado = new System.Windows.Forms.CheckBox();
            this.chkCuentaPorCobrar = new System.Windows.Forms.CheckBox();
            this.chkManejaServicio = new System.Windows.Forms.CheckBox();
            this.grupoPago = new System.Windows.Forms.GroupBox();
            this.dbAyudaPersona = new Controles.Auxiliares.DB_Ayuda();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFormasCobros = new MisControles.ComboDatos();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbModoDelivery = new MisControles.ComboDatos();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRepartidorExterno = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.chkGeneraFactura = new System.Windows.Forms.CheckBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaOriOrd = new System.Windows.Forms.Label();
            this.lblDescrOriOrd = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblcodigoOriOrd = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.tabCon_PosOriOrd.SuspendLayout();
            this.tabPag_PosOriOrd.SuspendLayout();
            this.Grb_listRePosOriOrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoOpciones.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.grupoPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCon_PosOriOrd
            // 
            this.tabCon_PosOriOrd.Controls.Add(this.tabPag_PosOriOrd);
            this.tabCon_PosOriOrd.Location = new System.Drawing.Point(-4, -1);
            this.tabCon_PosOriOrd.Name = "tabCon_PosOriOrd";
            this.tabCon_PosOriOrd.SelectedIndex = 0;
            this.tabCon_PosOriOrd.Size = new System.Drawing.Size(879, 453);
            this.tabCon_PosOriOrd.TabIndex = 3;
            // 
            // tabPag_PosOriOrd
            // 
            this.tabPag_PosOriOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPag_PosOriOrd.Controls.Add(this.Grb_listRePosOriOrd);
            this.tabPag_PosOriOrd.Controls.Add(this.grupoOpciones);
            this.tabPag_PosOriOrd.Controls.Add(this.grupoDatos);
            this.tabPag_PosOriOrd.Location = new System.Drawing.Point(4, 22);
            this.tabPag_PosOriOrd.Name = "tabPag_PosOriOrd";
            this.tabPag_PosOriOrd.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_PosOriOrd.Size = new System.Drawing.Size(871, 427);
            this.tabPag_PosOriOrd.TabIndex = 0;
            this.tabPag_PosOriOrd.Text = "Módulo de Tipo de Órdenes";
            // 
            // Grb_listRePosOriOrd
            // 
            this.Grb_listRePosOriOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listRePosOriOrd.Controls.Add(this.btnBuscar);
            this.Grb_listRePosOriOrd.Controls.Add(this.txtBuscar);
            this.Grb_listRePosOriOrd.Controls.Add(this.dgvDatos);
            this.Grb_listRePosOriOrd.Location = new System.Drawing.Point(409, 19);
            this.Grb_listRePosOriOrd.Name = "Grb_listRePosOriOrd";
            this.Grb_listRePosOriOrd.Size = new System.Drawing.Size(440, 314);
            this.Grb_listRePosOriOrd.TabIndex = 5;
            this.Grb_listRePosOriOrd.TabStop = false;
            this.Grb_listRePosOriOrd.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(236, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.Btn_BuscarPosOriOrd_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(14, 28);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(14, 60);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersWidth = 25;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(413, 233);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // grupoOpciones
            // 
            this.grupoOpciones.Controls.Add(this.btnCerrar);
            this.grupoOpciones.Controls.Add(this.btnLimpiar);
            this.grupoOpciones.Controls.Add(this.btnAnular);
            this.grupoOpciones.Controls.Add(this.btnNuevo);
            this.grupoOpciones.Location = new System.Drawing.Point(409, 339);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(440, 79);
            this.grupoOpciones.TabIndex = 4;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(300, 26);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(71, 39);
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.Btn_CerrarPosOriOrd_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(223, 26);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(71, 39);
            this.btnLimpiar.TabIndex = 11;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.Btn_LimpiarPosOriOrd_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Enabled = false;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(146, 26);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(71, 39);
            this.btnAnular.TabIndex = 10;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.Btn_AnularPosOriOrd_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(69, 26);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(71, 39);
            this.btnNuevo.TabIndex = 9;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevoPosOriOrd_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkPagoAnticipado);
            this.grupoDatos.Controls.Add(this.chkCuentaPorCobrar);
            this.grupoDatos.Controls.Add(this.chkManejaServicio);
            this.grupoDatos.Controls.Add(this.grupoPago);
            this.grupoDatos.Controls.Add(this.cmbModoDelivery);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.chkRepartidorExterno);
            this.grupoDatos.Controls.Add(this.btnClear);
            this.grupoDatos.Controls.Add(this.btnExaminar);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.imgLogo);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtRuta);
            this.grupoDatos.Controls.Add(this.chkGeneraFactura);
            this.grupoDatos.Controls.Add(this.chkDelivery);
            this.grupoDatos.Controls.Add(this.cmbEstado);
            this.grupoDatos.Controls.Add(this.lblEstaOriOrd);
            this.grupoDatos.Controls.Add(this.lblDescrOriOrd);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblcodigoOriOrd);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 19);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(391, 399);
            this.grupoDatos.TabIndex = 3;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // chkPagoAnticipado
            // 
            this.chkPagoAnticipado.AutoSize = true;
            this.chkPagoAnticipado.Location = new System.Drawing.Point(188, 174);
            this.chkPagoAnticipado.Name = "chkPagoAnticipado";
            this.chkPagoAnticipado.Size = new System.Drawing.Size(104, 17);
            this.chkPagoAnticipado.TabIndex = 36;
            this.chkPagoAnticipado.Text = "Pago Anticipado";
            this.chkPagoAnticipado.UseVisualStyleBackColor = true;
            this.chkPagoAnticipado.CheckedChanged += new System.EventHandler(this.chkPagoAnticpado_CheckedChanged);
            // 
            // chkCuentaPorCobrar
            // 
            this.chkCuentaPorCobrar.AutoSize = true;
            this.chkCuentaPorCobrar.Location = new System.Drawing.Point(188, 151);
            this.chkCuentaPorCobrar.Name = "chkCuentaPorCobrar";
            this.chkCuentaPorCobrar.Size = new System.Drawing.Size(112, 17);
            this.chkCuentaPorCobrar.TabIndex = 35;
            this.chkCuentaPorCobrar.Text = "Cuenta por Cobrar";
            this.chkCuentaPorCobrar.UseVisualStyleBackColor = true;
            this.chkCuentaPorCobrar.CheckedChanged += new System.EventHandler(this.chkCuentaPorCobrar_CheckedChanged);
            // 
            // chkManejaServicio
            // 
            this.chkManejaServicio.AutoSize = true;
            this.chkManejaServicio.Location = new System.Drawing.Point(18, 174);
            this.chkManejaServicio.Name = "chkManejaServicio";
            this.chkManejaServicio.Size = new System.Drawing.Size(102, 17);
            this.chkManejaServicio.TabIndex = 33;
            this.chkManejaServicio.Text = "Maneja Servicio";
            this.chkManejaServicio.UseVisualStyleBackColor = true;
            // 
            // grupoPago
            // 
            this.grupoPago.Controls.Add(this.dbAyudaPersona);
            this.grupoPago.Controls.Add(this.label5);
            this.grupoPago.Controls.Add(this.cmbFormasCobros);
            this.grupoPago.Controls.Add(this.label2);
            this.grupoPago.Location = new System.Drawing.Point(11, 191);
            this.grupoPago.Name = "grupoPago";
            this.grupoPago.Size = new System.Drawing.Size(374, 102);
            this.grupoPago.TabIndex = 32;
            this.grupoPago.TabStop = false;
            // 
            // dbAyudaPersona
            // 
            this.dbAyudaPersona.iId = 0;
            this.dbAyudaPersona.Location = new System.Drawing.Point(12, 64);
            this.dbAyudaPersona.Name = "dbAyudaPersona";
            this.dbAyudaPersona.sCodigo = null;
            this.dbAyudaPersona.Size = new System.Drawing.Size(356, 22);
            this.dbAyudaPersona.sNombre = null;
            this.dbAyudaPersona.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(9, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "Asociar Nombre:";
            // 
            // cmbFormasCobros
            // 
            this.cmbFormasCobros.FormattingEnabled = true;
            this.cmbFormasCobros.Location = new System.Drawing.Point(122, 17);
            this.cmbFormasCobros.Name = "cmbFormasCobros";
            this.cmbFormasCobros.Size = new System.Drawing.Size(183, 21);
            this.cmbFormasCobros.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(9, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Forma de Cobro:";
            // 
            // cmbModoDelivery
            // 
            this.cmbModoDelivery.FormattingEnabled = true;
            this.cmbModoDelivery.Location = new System.Drawing.Point(107, 70);
            this.cmbModoDelivery.Name = "cmbModoDelivery";
            this.cmbModoDelivery.Size = new System.Drawing.Size(209, 21);
            this.cmbModoDelivery.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(13, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "Modo Delivery:";
            // 
            // chkRepartidorExterno
            // 
            this.chkRepartidorExterno.AutoSize = true;
            this.chkRepartidorExterno.Location = new System.Drawing.Point(188, 128);
            this.chkRepartidorExterno.Name = "chkRepartidorExterno";
            this.chkRepartidorExterno.Size = new System.Drawing.Size(129, 17);
            this.chkRepartidorExterno.TabIndex = 6;
            this.chkRepartidorExterno.Text = "Es Repartidor Externo";
            this.chkRepartidorExterno.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(285, 325);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(29, 21);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "X";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.BackColor = System.Drawing.Color.Yellow;
            this.btnExaminar.Location = new System.Drawing.Point(249, 325);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(29, 21);
            this.btnExaminar.TabIndex = 7;
            this.btnExaminar.Text = "...";
            this.btnExaminar.UseVisualStyleBackColor = false;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(15, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "Vista Previa:";
            // 
            // imgLogo
            // 
            this.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgLogo.Location = new System.Drawing.Point(107, 325);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(107, 64);
            this.imgLogo.TabIndex = 24;
            this.imgLogo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(15, 300);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Ruta Imagen:";
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRuta.Location = new System.Drawing.Point(107, 299);
            this.txtRuta.MaxLength = 20;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(207, 20);
            this.txtRuta.TabIndex = 22;
            // 
            // chkGeneraFactura
            // 
            this.chkGeneraFactura.AutoSize = true;
            this.chkGeneraFactura.Location = new System.Drawing.Point(19, 151);
            this.chkGeneraFactura.Name = "chkGeneraFactura";
            this.chkGeneraFactura.Size = new System.Drawing.Size(100, 17);
            this.chkGeneraFactura.TabIndex = 5;
            this.chkGeneraFactura.Text = "Genera Factura";
            this.chkGeneraFactura.UseVisualStyleBackColor = true;
            this.chkGeneraFactura.CheckedChanged += new System.EventHandler(this.chkGerneraFactura_CheckedChanged);
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Location = new System.Drawing.Point(19, 128);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(146, 17);
            this.chkDelivery.TabIndex = 4;
            this.chkDelivery.Text = "Presenta Opción Delivery";
            this.chkDelivery.UseVisualStyleBackColor = true;
            // 
            // cmbEstado
            // 
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstado.Location = new System.Drawing.Point(107, 92);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(100, 21);
            this.cmbEstado.TabIndex = 10;
            // 
            // lblEstaOriOrd
            // 
            this.lblEstaOriOrd.AutoSize = true;
            this.lblEstaOriOrd.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaOriOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaOriOrd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaOriOrd.Location = new System.Drawing.Point(13, 92);
            this.lblEstaOriOrd.Name = "lblEstaOriOrd";
            this.lblEstaOriOrd.Size = new System.Drawing.Size(48, 15);
            this.lblEstaOriOrd.TabIndex = 7;
            this.lblEstaOriOrd.Text = "Estado:";
            // 
            // lblDescrOriOrd
            // 
            this.lblDescrOriOrd.AutoSize = true;
            this.lblDescrOriOrd.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrOriOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrOriOrd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrOriOrd.Location = new System.Drawing.Point(13, 51);
            this.lblDescrOriOrd.Name = "lblDescrOriOrd";
            this.lblDescrOriOrd.Size = new System.Drawing.Size(75, 15);
            this.lblDescrOriOrd.TabIndex = 5;
            this.lblDescrOriOrd.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(107, 49);
            this.txtDescripcion.MaxLength = 50;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(209, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // lblcodigoOriOrd
            // 
            this.lblcodigoOriOrd.AutoSize = true;
            this.lblcodigoOriOrd.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoOriOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoOriOrd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoOriOrd.Location = new System.Drawing.Point(13, 30);
            this.lblcodigoOriOrd.Name = "lblcodigoOriOrd";
            this.lblcodigoOriOrd.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoOriOrd.TabIndex = 3;
            this.lblcodigoOriOrd.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(107, 28);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(209, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.Leave += new System.EventHandler(this.Txt_CodigoPosOriOrd_Leave);
            // 
            // FInformacionPosOriOrd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(873, 447);
            this.Controls.Add(this.tabCon_PosOriOrd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionPosOriOrd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Orígenes de Orden";
            this.Load += new System.EventHandler(this.FInformacionPosOriOrd_Load);
            this.tabCon_PosOriOrd.ResumeLayout(false);
            this.tabPag_PosOriOrd.ResumeLayout(false);
            this.Grb_listRePosOriOrd.ResumeLayout(false);
            this.Grb_listRePosOriOrd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoPago.ResumeLayout(false);
            this.grupoPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_PosOriOrd;
        private System.Windows.Forms.TabPage tabPag_PosOriOrd;
        private System.Windows.Forms.GroupBox Grb_listRePosOriOrd;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox grupoOpciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaOriOrd;
        private System.Windows.Forms.Label lblDescrOriOrd;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblcodigoOriOrd;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.CheckBox chkDelivery;
        private System.Windows.Forms.CheckBox chkGeneraFactura;
        private System.Windows.Forms.CheckBox chkRepartidorExterno;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRuta;
        private MisControles.ComboDatos cmbModoDelivery;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grupoPago;
        private MisControles.ComboDatos cmbFormasCobros;
        private System.Windows.Forms.Label label2;
        private Controles.Auxiliares.DB_Ayuda dbAyudaPersona;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkManejaServicio;
        private System.Windows.Forms.CheckBox chkCuentaPorCobrar;
        private System.Windows.Forms.CheckBox chkPagoAnticipado;
    }
}