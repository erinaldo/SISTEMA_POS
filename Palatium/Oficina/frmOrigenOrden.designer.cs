namespace Palatium.Oficina
{
    partial class frmOrigenOrden
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Grb_listRePosOriOrd = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.id_pos_origen_orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genera_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_modo_delivery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.presenta_opcion_delivery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repartidor_externo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_tipo_forma_cobro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maneja_servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuenta_por_cobrar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pago_anticipado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porcentaje_incremento_delivery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.grupoDelivery = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPorcentajeRecargo = new System.Windows.Forms.TextBox();
            this.dbAyudaPersona = new Controles.Auxiliares.DB_Ayuda();
            this.chkPagoAnticipado = new System.Windows.Forms.CheckBox();
            this.chkCuentaPorCobrar = new System.Windows.Forms.CheckBox();
            this.chkManejaServicio = new System.Windows.Forms.CheckBox();
            this.grupoPago = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRepartidorExterno = new System.Windows.Forms.CheckBox();
            this.chkGeneraFactura = new System.Windows.Forms.CheckBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.lblDescrOriOrd = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblcodigoOriOrd = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.cmbModoDelivery = new System.Windows.Forms.ComboBox();
            this.cmbFormasCobros = new System.Windows.Forms.ComboBox();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.Grb_listRePosOriOrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoOpciones.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.grupoDelivery.SuspendLayout();
            this.grupoPago.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_listRePosOriOrd
            // 
            this.Grb_listRePosOriOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listRePosOriOrd.Controls.Add(this.btnBuscar);
            this.Grb_listRePosOriOrd.Controls.Add(this.txtBuscar);
            this.Grb_listRePosOriOrd.Controls.Add(this.dgvDatos);
            this.Grb_listRePosOriOrd.Location = new System.Drawing.Point(409, 12);
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
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pos_origen_orden,
            this.genera_factura,
            this.id_pos_modo_delivery,
            this.presenta_opcion_delivery,
            this.repartidor_externo,
            this.id_pos_tipo_forma_cobro,
            this.id_persona,
            this.maneja_servicio,
            this.cuenta_por_cobrar,
            this.pago_anticipado,
            this.is_active,
            this.porcentaje_incremento_delivery,
            this.codigo,
            this.descripcion,
            this.estado});
            this.dgvDatos.Location = new System.Drawing.Point(14, 60);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.RowHeadersWidth = 25;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(413, 233);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // id_pos_origen_orden
            // 
            this.id_pos_origen_orden.HeaderText = "ID";
            this.id_pos_origen_orden.Name = "id_pos_origen_orden";
            this.id_pos_origen_orden.ReadOnly = true;
            this.id_pos_origen_orden.Visible = false;
            // 
            // genera_factura
            // 
            this.genera_factura.HeaderText = "GENERA FACTURA";
            this.genera_factura.Name = "genera_factura";
            this.genera_factura.ReadOnly = true;
            this.genera_factura.Visible = false;
            // 
            // id_pos_modo_delivery
            // 
            this.id_pos_modo_delivery.HeaderText = "ID MODO DELIVERY";
            this.id_pos_modo_delivery.Name = "id_pos_modo_delivery";
            this.id_pos_modo_delivery.ReadOnly = true;
            this.id_pos_modo_delivery.Visible = false;
            // 
            // presenta_opcion_delivery
            // 
            this.presenta_opcion_delivery.HeaderText = "OPCION DELIVERY";
            this.presenta_opcion_delivery.Name = "presenta_opcion_delivery";
            this.presenta_opcion_delivery.ReadOnly = true;
            this.presenta_opcion_delivery.Visible = false;
            // 
            // repartidor_externo
            // 
            this.repartidor_externo.HeaderText = "REPARTIDOR EXTERNO";
            this.repartidor_externo.Name = "repartidor_externo";
            this.repartidor_externo.ReadOnly = true;
            this.repartidor_externo.Visible = false;
            // 
            // id_pos_tipo_forma_cobro
            // 
            this.id_pos_tipo_forma_cobro.HeaderText = "ID FORMA COBRO";
            this.id_pos_tipo_forma_cobro.Name = "id_pos_tipo_forma_cobro";
            this.id_pos_tipo_forma_cobro.ReadOnly = true;
            this.id_pos_tipo_forma_cobro.Visible = false;
            // 
            // id_persona
            // 
            this.id_persona.HeaderText = "ID PERSONA";
            this.id_persona.Name = "id_persona";
            this.id_persona.ReadOnly = true;
            this.id_persona.Visible = false;
            // 
            // maneja_servicio
            // 
            this.maneja_servicio.HeaderText = "MANEJA SERVICIO";
            this.maneja_servicio.Name = "maneja_servicio";
            this.maneja_servicio.ReadOnly = true;
            this.maneja_servicio.Visible = false;
            // 
            // cuenta_por_cobrar
            // 
            this.cuenta_por_cobrar.HeaderText = "CUENTA POR COBRAR";
            this.cuenta_por_cobrar.Name = "cuenta_por_cobrar";
            this.cuenta_por_cobrar.ReadOnly = true;
            this.cuenta_por_cobrar.Visible = false;
            // 
            // pago_anticipado
            // 
            this.pago_anticipado.HeaderText = "PAGO ANTICIPADO";
            this.pago_anticipado.Name = "pago_anticipado";
            this.pago_anticipado.ReadOnly = true;
            this.pago_anticipado.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "IS ACTIVE";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // porcentaje_incremento_delivery
            // 
            this.porcentaje_incremento_delivery.HeaderText = "INCREMENTO DELIVERY";
            this.porcentaje_incremento_delivery.Name = "porcentaje_incremento_delivery";
            this.porcentaje_incremento_delivery.ReadOnly = true;
            this.porcentaje_incremento_delivery.Visible = false;
            // 
            // codigo
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigo.DefaultCellStyle = dataGridViewCellStyle11;
            this.codigo.HeaderText = "CÓDIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 70;
            // 
            // descripcion
            // 
            this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descripcion.HeaderText = "TIPO DE COMANDA";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            // 
            // estado
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle12;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // grupoOpciones
            // 
            this.grupoOpciones.Controls.Add(this.btnCerrar);
            this.grupoOpciones.Controls.Add(this.btnLimpiar);
            this.grupoOpciones.Controls.Add(this.btnAnular);
            this.grupoOpciones.Controls.Add(this.btnNuevo);
            this.grupoOpciones.Location = new System.Drawing.Point(409, 332);
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
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
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
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
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
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
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
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.cmbModoDelivery);
            this.grupoDatos.Controls.Add(this.grupoDelivery);
            this.grupoDatos.Controls.Add(this.chkPagoAnticipado);
            this.grupoDatos.Controls.Add(this.chkCuentaPorCobrar);
            this.grupoDatos.Controls.Add(this.chkManejaServicio);
            this.grupoDatos.Controls.Add(this.grupoPago);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.chkRepartidorExterno);
            this.grupoDatos.Controls.Add(this.chkGeneraFactura);
            this.grupoDatos.Controls.Add(this.chkDelivery);
            this.grupoDatos.Controls.Add(this.lblDescrOriOrd);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblcodigoOriOrd);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(391, 399);
            this.grupoDatos.TabIndex = 3;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // grupoDelivery
            // 
            this.grupoDelivery.Controls.Add(this.label3);
            this.grupoDelivery.Controls.Add(this.label5);
            this.grupoDelivery.Controls.Add(this.txtPorcentajeRecargo);
            this.grupoDelivery.Controls.Add(this.dbAyudaPersona);
            this.grupoDelivery.Enabled = false;
            this.grupoDelivery.Location = new System.Drawing.Point(11, 281);
            this.grupoDelivery.Name = "grupoDelivery";
            this.grupoDelivery.Size = new System.Drawing.Size(374, 100);
            this.grupoDelivery.TabIndex = 37;
            this.grupoDelivery.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(11, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 15);
            this.label3.TabIndex = 39;
            this.label3.Text = "Porcentaje de Recargo a Comanda:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 15);
            this.label5.TabIndex = 32;
            this.label5.Text = "Asociar Nombre:";
            // 
            // txtPorcentajeRecargo
            // 
            this.txtPorcentajeRecargo.Location = new System.Drawing.Point(269, 65);
            this.txtPorcentajeRecargo.MaxLength = 20;
            this.txtPorcentajeRecargo.Name = "txtPorcentajeRecargo";
            this.txtPorcentajeRecargo.Size = new System.Drawing.Size(96, 20);
            this.txtPorcentajeRecargo.TabIndex = 38;
            this.txtPorcentajeRecargo.Text = "0";
            this.txtPorcentajeRecargo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPorcentajeRecargo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentajeRecargo_KeyPress);
            // 
            // dbAyudaPersona
            // 
            this.dbAyudaPersona.iId = 0;
            this.dbAyudaPersona.Location = new System.Drawing.Point(9, 34);
            this.dbAyudaPersona.Name = "dbAyudaPersona";
            this.dbAyudaPersona.sCodigo = null;
            this.dbAyudaPersona.Size = new System.Drawing.Size(356, 22);
            this.dbAyudaPersona.sNombre = null;
            this.dbAyudaPersona.TabIndex = 33;
            // 
            // chkPagoAnticipado
            // 
            this.chkPagoAnticipado.AutoSize = true;
            this.chkPagoAnticipado.Location = new System.Drawing.Point(19, 141);
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
            this.chkCuentaPorCobrar.Location = new System.Drawing.Point(189, 118);
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
            this.chkManejaServicio.Location = new System.Drawing.Point(189, 141);
            this.chkManejaServicio.Name = "chkManejaServicio";
            this.chkManejaServicio.Size = new System.Drawing.Size(102, 17);
            this.chkManejaServicio.TabIndex = 33;
            this.chkManejaServicio.Text = "Maneja Servicio";
            this.chkManejaServicio.UseVisualStyleBackColor = true;
            // 
            // grupoPago
            // 
            this.grupoPago.Controls.Add(this.cmbFormasCobros);
            this.grupoPago.Controls.Add(this.label2);
            this.grupoPago.Location = new System.Drawing.Point(11, 191);
            this.grupoPago.Name = "grupoPago";
            this.grupoPago.Size = new System.Drawing.Size(374, 51);
            this.grupoPago.TabIndex = 32;
            this.grupoPago.TabStop = false;
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
            this.chkRepartidorExterno.Location = new System.Drawing.Point(11, 258);
            this.chkRepartidorExterno.Name = "chkRepartidorExterno";
            this.chkRepartidorExterno.Size = new System.Drawing.Size(129, 17);
            this.chkRepartidorExterno.TabIndex = 6;
            this.chkRepartidorExterno.Text = "Es Repartidor Externo";
            this.chkRepartidorExterno.UseVisualStyleBackColor = true;
            this.chkRepartidorExterno.CheckedChanged += new System.EventHandler(this.chkRepartidorExterno_CheckedChanged);
            // 
            // chkGeneraFactura
            // 
            this.chkGeneraFactura.AutoSize = true;
            this.chkGeneraFactura.Location = new System.Drawing.Point(19, 164);
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
            this.chkDelivery.Location = new System.Drawing.Point(20, 118);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(146, 17);
            this.chkDelivery.TabIndex = 4;
            this.chkDelivery.Text = "Presenta Opción Delivery";
            this.chkDelivery.UseVisualStyleBackColor = true;
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
            // 
            // cmbModoDelivery
            // 
            this.cmbModoDelivery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModoDelivery.FormattingEnabled = true;
            this.cmbModoDelivery.Location = new System.Drawing.Point(107, 70);
            this.cmbModoDelivery.Name = "cmbModoDelivery";
            this.cmbModoDelivery.Size = new System.Drawing.Size(209, 21);
            this.cmbModoDelivery.TabIndex = 38;
            // 
            // cmbFormasCobros
            // 
            this.cmbFormasCobros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormasCobros.FormattingEnabled = true;
            this.cmbFormasCobros.Location = new System.Drawing.Point(114, 17);
            this.cmbFormasCobros.Name = "cmbFormasCobros";
            this.cmbFormasCobros.Size = new System.Drawing.Size(209, 21);
            this.cmbFormasCobros.TabIndex = 39;
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(189, 164);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(83, 17);
            this.chkHabilitado.TabIndex = 60;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // frmOrigenOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(873, 419);
            this.Controls.Add(this.Grb_listRePosOriOrd);
            this.Controls.Add(this.grupoOpciones);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOrigenOrden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Orígenes de Orden";
            this.Load += new System.EventHandler(this.frmOrigenOrden_Load);
            this.Grb_listRePosOriOrd.ResumeLayout(false);
            this.Grb_listRePosOriOrd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoDelivery.ResumeLayout(false);
            this.grupoDelivery.PerformLayout();
            this.grupoPago.ResumeLayout(false);
            this.grupoPago.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.Label lblDescrOriOrd;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblcodigoOriOrd;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.CheckBox chkDelivery;
        private System.Windows.Forms.CheckBox chkGeneraFactura;
        private System.Windows.Forms.CheckBox chkRepartidorExterno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grupoPago;
        private System.Windows.Forms.Label label2;
        private Controles.Auxiliares.DB_Ayuda dbAyudaPersona;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkManejaServicio;
        private System.Windows.Forms.CheckBox chkCuentaPorCobrar;
        private System.Windows.Forms.CheckBox chkPagoAnticipado;
        private System.Windows.Forms.GroupBox grupoDelivery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPorcentajeRecargo;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_origen_orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn genera_factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_modo_delivery;
        private System.Windows.Forms.DataGridViewTextBoxColumn presenta_opcion_delivery;
        private System.Windows.Forms.DataGridViewTextBoxColumn repartidor_externo;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_tipo_forma_cobro;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn maneja_servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuenta_por_cobrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn pago_anticipado;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn porcentaje_incremento_delivery;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.ComboBox cmbModoDelivery;
        private System.Windows.Forms.ComboBox cmbFormasCobros;
        private System.Windows.Forms.CheckBox chkHabilitado;
    }
}