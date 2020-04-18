namespace Palatium.Formularios
{
    partial class frmFormasPagos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grupoBusqueda = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.id_pos_tipo_forma_cobro = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cg_tipo_documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_metodo_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_tipo_venta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lee_propina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrar_seccion_cobros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.texto_visualizar_boton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo_documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtVisualizarBoton = new System.Windows.Forms.TextBox();
            this.cmbTipoVenta = new System.Windows.Forms.ComboBox();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.cmbTipoDocumento = new System.Windows.Forms.ComboBox();
            this.chkMostrarSeccionCobros = new System.Windows.Forms.CheckBox();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPropina = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescrTiForPa = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblcodigoTiForPa = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.txtBase64 = new System.Windows.Forms.TextBox();
            this.grupoBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoOpciones.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // grupoBusqueda
            // 
            this.grupoBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoBusqueda.Controls.Add(this.btnBuscar);
            this.grupoBusqueda.Controls.Add(this.txtBuscar);
            this.grupoBusqueda.Controls.Add(this.dgvDatos);
            this.grupoBusqueda.Location = new System.Drawing.Point(365, 12);
            this.grupoBusqueda.Name = "grupoBusqueda";
            this.grupoBusqueda.Size = new System.Drawing.Size(714, 442);
            this.grupoBusqueda.TabIndex = 5;
            this.grupoBusqueda.TabStop = false;
            this.grupoBusqueda.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(235, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.Btn_BuscarPosTipForPag_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 29);
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
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pos_tipo_forma_cobro,
            this.cg_tipo_documento,
            this.id_pos_metodo_pago,
            this.id_pos_tipo_venta,
            this.is_active,
            this.lee_propina,
            this.mostrar_seccion_cobros,
            this.texto_visualizar_boton,
            this.codigo,
            this.descripcion,
            this.tipo_documento,
            this.propina,
            this.estado});
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(695, 364);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // id_pos_tipo_forma_cobro
            // 
            this.id_pos_tipo_forma_cobro.HeaderText = "id_pos_tipo_forma_cobro";
            this.id_pos_tipo_forma_cobro.Name = "id_pos_tipo_forma_cobro";
            this.id_pos_tipo_forma_cobro.ReadOnly = true;
            this.id_pos_tipo_forma_cobro.Visible = false;
            // 
            // cg_tipo_documento
            // 
            this.cg_tipo_documento.HeaderText = "CG TIPO DOCUMENTO";
            this.cg_tipo_documento.Name = "cg_tipo_documento";
            this.cg_tipo_documento.ReadOnly = true;
            this.cg_tipo_documento.Visible = false;
            // 
            // id_pos_metodo_pago
            // 
            this.id_pos_metodo_pago.HeaderText = "ID POS METODO PAGO";
            this.id_pos_metodo_pago.Name = "id_pos_metodo_pago";
            this.id_pos_metodo_pago.ReadOnly = true;
            this.id_pos_metodo_pago.Visible = false;
            // 
            // id_pos_tipo_venta
            // 
            this.id_pos_tipo_venta.HeaderText = "ID POS TIPO VENTA";
            this.id_pos_tipo_venta.Name = "id_pos_tipo_venta";
            this.id_pos_tipo_venta.ReadOnly = true;
            this.id_pos_tipo_venta.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "IS_ACTIVE";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // lee_propina
            // 
            this.lee_propina.HeaderText = "LEE PROPINA";
            this.lee_propina.Name = "lee_propina";
            this.lee_propina.ReadOnly = true;
            this.lee_propina.Visible = false;
            // 
            // mostrar_seccion_cobros
            // 
            this.mostrar_seccion_cobros.HeaderText = "MOSTRAR EN COBROS";
            this.mostrar_seccion_cobros.Name = "mostrar_seccion_cobros";
            this.mostrar_seccion_cobros.ReadOnly = true;
            this.mostrar_seccion_cobros.Visible = false;
            // 
            // texto_visualizar_boton
            // 
            this.texto_visualizar_boton.HeaderText = "TEXTO_VER";
            this.texto_visualizar_boton.Name = "texto_visualizar_boton";
            this.texto_visualizar_boton.ReadOnly = true;
            this.texto_visualizar_boton.Visible = false;
            // 
            // codigo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigo.DefaultCellStyle = dataGridViewCellStyle1;
            this.codigo.HeaderText = "CÓDIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 80;
            // 
            // descripcion
            // 
            this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descripcion.HeaderText = "DESCRIPCION";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            // 
            // tipo_documento
            // 
            this.tipo_documento.HeaderText = "TIPO DOCUMENTO";
            this.tipo_documento.Name = "tipo_documento";
            this.tipo_documento.ReadOnly = true;
            this.tipo_documento.Width = 150;
            // 
            // propina
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.propina.DefaultCellStyle = dataGridViewCellStyle2;
            this.propina.HeaderText = "PROPINA";
            this.propina.Name = "propina";
            this.propina.ReadOnly = true;
            // 
            // estado
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle3;
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
            this.grupoOpciones.Location = new System.Drawing.Point(12, 371);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(342, 83);
            this.grupoOpciones.TabIndex = 4;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(242, 26);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(65, 39);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.Btn_CerrarPosTipForPag_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(171, 26);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(65, 39);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.Btn_LimpiarPosTipForPag_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(100, 26);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(65, 39);
            this.btnAnular.TabIndex = 1;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.Btn_AnularPosTipForPag_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(29, 26);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(65, 39);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevoPosTipForPag_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.label6);
            this.grupoDatos.Controls.Add(this.txtVisualizarBoton);
            this.grupoDatos.Controls.Add(this.cmbTipoVenta);
            this.grupoDatos.Controls.Add(this.cmbMetodoPago);
            this.grupoDatos.Controls.Add(this.cmbTipoDocumento);
            this.grupoDatos.Controls.Add(this.chkMostrarSeccionCobros);
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.label5);
            this.grupoDatos.Controls.Add(this.btnClear);
            this.grupoDatos.Controls.Add(this.btnExaminar);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.imgLogo);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtRuta);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.chkPropina);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.lblDescrTiForPa);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblcodigoTiForPa);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(342, 353);
            this.grupoDatos.TabIndex = 3;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(15, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 30);
            this.label6.TabIndex = 67;
            this.label6.Text = "Texto en el \r\nbotón:";
            // 
            // txtVisualizarBoton
            // 
            this.txtVisualizarBoton.Location = new System.Drawing.Point(100, 76);
            this.txtVisualizarBoton.MaxLength = 20;
            this.txtVisualizarBoton.Multiline = true;
            this.txtVisualizarBoton.Name = "txtVisualizarBoton";
            this.txtVisualizarBoton.Size = new System.Drawing.Size(216, 41);
            this.txtVisualizarBoton.TabIndex = 66;
            // 
            // cmbTipoVenta
            // 
            this.cmbTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoVenta.FormattingEnabled = true;
            this.cmbTipoVenta.Location = new System.Drawing.Point(100, 208);
            this.cmbTipoVenta.Name = "cmbTipoVenta";
            this.cmbTipoVenta.Size = new System.Drawing.Size(216, 21);
            this.cmbTipoVenta.TabIndex = 65;
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Location = new System.Drawing.Point(100, 171);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(216, 21);
            this.cmbMetodoPago.TabIndex = 64;
            // 
            // cmbTipoDocumento
            // 
            this.cmbTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoDocumento.FormattingEnabled = true;
            this.cmbTipoDocumento.Location = new System.Drawing.Point(100, 134);
            this.cmbTipoDocumento.Name = "cmbTipoDocumento";
            this.cmbTipoDocumento.Size = new System.Drawing.Size(216, 21);
            this.cmbTipoDocumento.TabIndex = 63;
            // 
            // chkMostrarSeccionCobros
            // 
            this.chkMostrarSeccionCobros.AutoSize = true;
            this.chkMostrarSeccionCobros.Checked = true;
            this.chkMostrarSeccionCobros.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMostrarSeccionCobros.ForeColor = System.Drawing.Color.Red;
            this.chkMostrarSeccionCobros.Location = new System.Drawing.Point(18, 326);
            this.chkMostrarSeccionCobros.Name = "chkMostrarSeccionCobros";
            this.chkMostrarSeccionCobros.Size = new System.Drawing.Size(177, 17);
            this.chkMostrarSeccionCobros.TabIndex = 62;
            this.chkMostrarSeccionCobros.Text = "Mostrar en la sección de cobros";
            this.chkMostrarSeccionCobros.UseVisualStyleBackColor = true;
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(233, 239);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(83, 17);
            this.chkHabilitado.TabIndex = 61;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(15, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 36);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tipo de Venta:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(166, 254);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(29, 21);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "X";
            this.ttMensaje.SetToolTip(this.btnClear, "Clic aquí para remover la imagen seleccionada");
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.BackColor = System.Drawing.Color.Yellow;
            this.btnExaminar.Location = new System.Drawing.Point(307, 300);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(29, 21);
            this.btnExaminar.TabIndex = 20;
            this.btnExaminar.Text = "...";
            this.ttMensaje.SetToolTip(this.btnExaminar, "Clic aquí para buscar una imegen");
            this.btnExaminar.UseVisualStyleBackColor = false;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(15, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 30);
            this.label4.TabIndex = 19;
            this.label4.Text = "Ícono del\r\nbotón:";
            // 
            // imgLogo
            // 
            this.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgLogo.Location = new System.Drawing.Point(100, 239);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(60, 50);
            this.imgLogo.TabIndex = 18;
            this.imgLogo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(20, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Ruta Imagen:";
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRuta.Location = new System.Drawing.Point(105, 300);
            this.txtRuta.MaxLength = 20;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(202, 20);
            this.txtRuta.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(15, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 36);
            this.label2.TabIndex = 14;
            this.label2.Text = "Método de Pago:";
            // 
            // chkPropina
            // 
            this.chkPropina.AutoSize = true;
            this.chkPropina.Location = new System.Drawing.Point(233, 262);
            this.chkPropina.Name = "chkPropina";
            this.chkPropina.Size = new System.Drawing.Size(83, 17);
            this.chkPropina.TabIndex = 13;
            this.chkPropina.Text = "Lee Propina";
            this.chkPropina.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(15, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 36);
            this.label1.TabIndex = 11;
            this.label1.Text = "Tipo de documento:";
            // 
            // lblDescrTiForPa
            // 
            this.lblDescrTiForPa.AutoSize = true;
            this.lblDescrTiForPa.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrTiForPa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrTiForPa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrTiForPa.Location = new System.Drawing.Point(15, 52);
            this.lblDescrTiForPa.Name = "lblDescrTiForPa";
            this.lblDescrTiForPa.Size = new System.Drawing.Size(75, 15);
            this.lblDescrTiForPa.TabIndex = 5;
            this.lblDescrTiForPa.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(100, 50);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(216, 20);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblcodigoTiForPa
            // 
            this.lblcodigoTiForPa.AutoSize = true;
            this.lblcodigoTiForPa.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoTiForPa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoTiForPa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoTiForPa.Location = new System.Drawing.Point(15, 26);
            this.lblcodigoTiForPa.Name = "lblcodigoTiForPa";
            this.lblcodigoTiForPa.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoTiForPa.TabIndex = 3;
            this.lblcodigoTiForPa.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(100, 24);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(216, 20);
            this.txtCodigo.TabIndex = 2;
            // 
            // txtBase64
            // 
            this.txtBase64.Enabled = false;
            this.txtBase64.Location = new System.Drawing.Point(12, 463);
            this.txtBase64.MaxLength = 20;
            this.txtBase64.Multiline = true;
            this.txtBase64.Name = "txtBase64";
            this.txtBase64.Size = new System.Drawing.Size(1067, 74);
            this.txtBase64.TabIndex = 63;
            // 
            // frmFormasPagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1086, 475);
            this.Controls.Add(this.txtBase64);
            this.Controls.Add(this.grupoBusqueda);
            this.Controls.Add(this.grupoOpciones);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormasPagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Formas de Pago";
            this.Load += new System.EventHandler(this.FInformacionPosTipForPag_Load);
            this.grupoBusqueda.ResumeLayout(false);
            this.grupoBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox grupoOpciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblDescrTiForPa;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblcodigoTiForPa;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPropina;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.CheckBox chkMostrarSeccionCobros;
        private System.Windows.Forms.TextBox txtBase64;
        private System.Windows.Forms.ComboBox cmbTipoVenta;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.ComboBox cmbTipoDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_tipo_forma_cobro;
        private System.Windows.Forms.DataGridViewTextBoxColumn cg_tipo_documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_metodo_pago;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_tipo_venta;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn lee_propina;
        private System.Windows.Forms.DataGridViewTextBoxColumn mostrar_seccion_cobros;
        private System.Windows.Forms.DataGridViewTextBoxColumn texto_visualizar_boton;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo_documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn propina;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtVisualizarBoton;
    }
}