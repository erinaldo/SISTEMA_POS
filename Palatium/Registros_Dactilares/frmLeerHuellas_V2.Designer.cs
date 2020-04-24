namespace Palatium.Registros_Dactilares
{
    partial class frmLeerHuellas_V2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeerHuellas_V2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuscar = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.id_pos_empleado_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_cliente_empresarial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aplica_almuerzo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnRemoverRegistro = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.imgHuellaCapturada = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnGuardar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnVerificar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnRemover = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtBase64_1 = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHuellaCapturada)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empresa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(26, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Búsqueda:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.AcceptsReturn = false;
            this.txtBuscar.AcceptsTab = false;
            this.txtBuscar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.txtBuscar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.txtBuscar.BackColor = System.Drawing.Color.Transparent;
            this.txtBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtBuscar.BackgroundImage")));
            this.txtBuscar.BorderColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(45)))), ((int)(((byte)(145)))));
            this.txtBuscar.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.txtBuscar.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(38)))), ((int)(((byte)(157)))));
            this.txtBuscar.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.txtBuscar.BorderRadius = 18;
            this.txtBuscar.BorderThickness = 1;
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.DefaultFont = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtBuscar.DefaultText = "";
            this.txtBuscar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.txtBuscar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtBuscar.HideSelection = true;
            this.txtBuscar.IconLeft = ((System.Drawing.Image)(resources.GetObject("txtBuscar.IconLeft")));
            this.txtBuscar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.txtBuscar.IconPadding = 10;
            this.txtBuscar.IconRight = null;
            this.txtBuscar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.txtBuscar.Location = new System.Drawing.Point(101, 60);
            this.txtBuscar.MaxLength = 32767;
            this.txtBuscar.MinimumSize = new System.Drawing.Size(100, 35);
            this.txtBuscar.Modified = false;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.PasswordChar = '\0';
            this.txtBuscar.ReadOnly = false;
            this.txtBuscar.SelectedText = "";
            this.txtBuscar.SelectionLength = 0;
            this.txtBuscar.SelectionStart = 0;
            this.txtBuscar.ShortcutsEnabled = true;
            this.txtBuscar.Size = new System.Drawing.Size(313, 35);
            this.txtBuscar.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBuscar.TextMarginLeft = 5;
            this.txtBuscar.TextPlaceholder = "";
            this.txtBuscar.UseSystemPasswordChar = false;
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(101, 21);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(313, 25);
            this.cmbEmpresas.TabIndex = 3;
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
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
            this.id_pos_empleado_cliente,
            this.id_persona,
            this.id_pos_cliente_empresarial,
            this.aplica_almuerzo,
            this.is_active,
            this.identificacion,
            this.nombre_empleado,
            this.estado});
            this.dgvDatos.Location = new System.Drawing.Point(29, 112);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(435, 305);
            this.dgvDatos.TabIndex = 22;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // id_pos_empleado_cliente
            // 
            this.id_pos_empleado_cliente.HeaderText = "ID EMPLEADO CLIENTE";
            this.id_pos_empleado_cliente.Name = "id_pos_empleado_cliente";
            this.id_pos_empleado_cliente.ReadOnly = true;
            this.id_pos_empleado_cliente.Visible = false;
            // 
            // id_persona
            // 
            this.id_persona.HeaderText = "ID PERSONA";
            this.id_persona.Name = "id_persona";
            this.id_persona.ReadOnly = true;
            this.id_persona.Visible = false;
            // 
            // id_pos_cliente_empresarial
            // 
            this.id_pos_cliente_empresarial.HeaderText = "ID CLIENTE EMPRESARIAL";
            this.id_pos_cliente_empresarial.Name = "id_pos_cliente_empresarial";
            this.id_pos_cliente_empresarial.ReadOnly = true;
            this.id_pos_cliente_empresarial.Visible = false;
            // 
            // aplica_almuerzo
            // 
            this.aplica_almuerzo.HeaderText = "APLICA ALMUERZO";
            this.aplica_almuerzo.Name = "aplica_almuerzo";
            this.aplica_almuerzo.ReadOnly = true;
            this.aplica_almuerzo.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "IS ACTIVE";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // identificacion
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identificacion.DefaultCellStyle = dataGridViewCellStyle1;
            this.identificacion.HeaderText = "IDENTIFICACIÓN";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            // 
            // nombre_empleado
            // 
            this.nombre_empleado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre_empleado.HeaderText = "NOMBRE EMPLEADO";
            this.nombre_empleado.Name = "nombre_empleado";
            this.nombre_empleado.ReadOnly = true;
            // 
            // estado
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle2;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.BackgroundImage")));
            this.btnBuscar.ButtonText = "OK";
            this.btnBuscar.ButtonTextMarginLeft = 0;
            this.btnBuscar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnBuscar.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnBuscar.DisabledForecolor = System.Drawing.Color.White;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnBuscar.IconPadding = 6;
            this.btnBuscar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnBuscar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnBuscar.IdleBorderRadius = 10;
            this.btnBuscar.IdleBorderThickness = 0;
            this.btnBuscar.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.btnBuscar.IdleIconLeftImage = ((System.Drawing.Image)(resources.GetObject("btnBuscar.IdleIconLeftImage")));
            this.btnBuscar.IdleIconRightImage = null;
            this.btnBuscar.Location = new System.Drawing.Point(420, 60);
            this.btnBuscar.Name = "btnBuscar";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.BorderRadius = 1;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnBuscar.onHoverState = stateProperties1;
            this.btnBuscar.Size = new System.Drawing.Size(38, 35);
            this.btnBuscar.TabIndex = 23;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.lblRegistros.Location = new System.Drawing.Point(138, 424);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(155, 16);
            this.lblRegistros.TabIndex = 24;
            this.lblRegistros.Text = "0 Registros Encontrados";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.label16.Location = new System.Drawing.Point(29, 424);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 16);
            this.label16.TabIndex = 25;
            this.label16.Text = "N° de Registros:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRemoverRegistro);
            this.groupBox2.Controls.Add(this.imgHuellaCapturada);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(477, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 194);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Huella dactilar";
            // 
            // btnRemoverRegistro
            // 
            this.btnRemoverRegistro.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoverRegistro.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoverRegistro.BackgroundImage")));
            this.btnRemoverRegistro.ButtonText = "X";
            this.btnRemoverRegistro.ButtonTextMarginLeft = 0;
            this.btnRemoverRegistro.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnRemoverRegistro.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnRemoverRegistro.DisabledForecolor = System.Drawing.Color.White;
            this.btnRemoverRegistro.ForeColor = System.Drawing.Color.White;
            this.btnRemoverRegistro.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnRemoverRegistro.IconPadding = 6;
            this.btnRemoverRegistro.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnRemoverRegistro.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(122)))), ((int)(((byte)(183)))));
            this.btnRemoverRegistro.IdleBorderRadius = 10;
            this.btnRemoverRegistro.IdleBorderThickness = 0;
            this.btnRemoverRegistro.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(49)))), ((int)(((byte)(69)))));
            this.btnRemoverRegistro.IdleIconLeftImage = ((System.Drawing.Image)(resources.GetObject("btnRemoverRegistro.IdleIconLeftImage")));
            this.btnRemoverRegistro.IdleIconRightImage = null;
            this.btnRemoverRegistro.Location = new System.Drawing.Point(106, 148);
            this.btnRemoverRegistro.Name = "btnRemoverRegistro";
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.BorderRadius = 1;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.btnRemoverRegistro.onHoverState = stateProperties2;
            this.btnRemoverRegistro.Size = new System.Drawing.Size(38, 36);
            this.btnRemoverRegistro.TabIndex = 4;
            this.btnRemoverRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttMensaje.SetToolTip(this.btnRemoverRegistro, "Clic aquí para remover la huella dactilar");
            this.btnRemoverRegistro.Click += new System.EventHandler(this.btnRemoverRegistro_Click);
            // 
            // imgHuellaCapturada
            // 
            this.imgHuellaCapturada.BackColor = System.Drawing.SystemColors.Window;
            this.imgHuellaCapturada.Location = new System.Drawing.Point(11, 26);
            this.imgHuellaCapturada.Name = "imgHuellaCapturada";
            this.imgHuellaCapturada.Size = new System.Drawing.Size(133, 158);
            this.imgHuellaCapturada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgHuellaCapturada.TabIndex = 46;
            this.imgHuellaCapturada.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(64)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Controls.Add(this.btnVerificar);
            this.panel1.Controls.Add(this.btnRemover);
            this.panel1.Location = new System.Drawing.Point(646, 228);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 189);
            this.panel1.TabIndex = 28;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.BackgroundImage")));
            this.btnLimpiar.ButtonText = "Limpiar";
            this.btnLimpiar.ButtonTextMarginLeft = 0;
            this.btnLimpiar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnLimpiar.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnLimpiar.DisabledForecolor = System.Drawing.Color.White;
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnLimpiar.IconPadding = 10;
            this.btnLimpiar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnLimpiar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLimpiar.IdleBorderRadius = 32;
            this.btnLimpiar.IdleBorderThickness = 0;
            this.btnLimpiar.IdleFillColor = System.Drawing.Color.Yellow;
            this.btnLimpiar.IdleIconLeftImage = null;
            this.btnLimpiar.IdleIconRightImage = null;
            this.btnLimpiar.Location = new System.Drawing.Point(10, 90);
            this.btnLimpiar.Name = "btnLimpiar";
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties3.BorderRadius = 1;
            stateProperties3.BorderThickness = 1;
            stateProperties3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties3.IconLeftImage = null;
            stateProperties3.IconRightImage = null;
            this.btnLimpiar.onHoverState = stateProperties3;
            this.btnLimpiar.Size = new System.Drawing.Size(158, 31);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Transparent;
            this.btnGuardar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGuardar.BackgroundImage")));
            this.btnGuardar.ButtonText = "GUARDAR";
            this.btnGuardar.ButtonTextMarginLeft = 0;
            this.btnGuardar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnGuardar.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnGuardar.DisabledForecolor = System.Drawing.Color.White;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.Black;
            this.btnGuardar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnGuardar.IconPadding = 10;
            this.btnGuardar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnGuardar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGuardar.IdleBorderRadius = 32;
            this.btnGuardar.IdleBorderThickness = 0;
            this.btnGuardar.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.btnGuardar.IdleIconLeftImage = null;
            this.btnGuardar.IdleIconRightImage = null;
            this.btnGuardar.Location = new System.Drawing.Point(10, 132);
            this.btnGuardar.Name = "btnGuardar";
            stateProperties4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties4.BorderRadius = 1;
            stateProperties4.BorderThickness = 1;
            stateProperties4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties4.IconLeftImage = null;
            stateProperties4.IconRightImage = null;
            this.btnGuardar.onHoverState = stateProperties4;
            this.btnGuardar.Size = new System.Drawing.Size(158, 44);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnVerificar
            // 
            this.btnVerificar.BackColor = System.Drawing.Color.Transparent;
            this.btnVerificar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnVerificar.BackgroundImage")));
            this.btnVerificar.ButtonText = "Verificar";
            this.btnVerificar.ButtonTextMarginLeft = 0;
            this.btnVerificar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnVerificar.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnVerificar.DisabledForecolor = System.Drawing.Color.White;
            this.btnVerificar.ForeColor = System.Drawing.Color.White;
            this.btnVerificar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnVerificar.IconPadding = 10;
            this.btnVerificar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnVerificar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnVerificar.IdleBorderRadius = 32;
            this.btnVerificar.IdleBorderThickness = 0;
            this.btnVerificar.IdleFillColor = System.Drawing.Color.Blue;
            this.btnVerificar.IdleIconLeftImage = null;
            this.btnVerificar.IdleIconRightImage = null;
            this.btnVerificar.Location = new System.Drawing.Point(10, 50);
            this.btnVerificar.Name = "btnVerificar";
            stateProperties5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties5.BorderRadius = 1;
            stateProperties5.BorderThickness = 1;
            stateProperties5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties5.IconLeftImage = null;
            stateProperties5.IconRightImage = null;
            this.btnVerificar.onHoverState = stateProperties5;
            this.btnVerificar.Size = new System.Drawing.Size(158, 31);
            this.btnVerificar.TabIndex = 1;
            this.btnVerificar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.Transparent;
            this.btnRemover.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemover.BackgroundImage")));
            this.btnRemover.ButtonText = "Remover";
            this.btnRemover.ButtonTextMarginLeft = 0;
            this.btnRemover.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnRemover.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnRemover.DisabledForecolor = System.Drawing.Color.White;
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnRemover.IconPadding = 10;
            this.btnRemover.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnRemover.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRemover.IdleBorderRadius = 32;
            this.btnRemover.IdleBorderThickness = 0;
            this.btnRemover.IdleFillColor = System.Drawing.Color.Red;
            this.btnRemover.IdleIconLeftImage = null;
            this.btnRemover.IdleIconRightImage = null;
            this.btnRemover.Location = new System.Drawing.Point(10, 9);
            this.btnRemover.Name = "btnRemover";
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties6.BorderRadius = 1;
            stateProperties6.BorderThickness = 1;
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties6.IconLeftImage = null;
            stateProperties6.IconRightImage = null;
            this.btnRemover.onHoverState = stateProperties6;
            this.btnRemover.Size = new System.Drawing.Size(158, 31);
            this.btnRemover.TabIndex = 0;
            this.btnRemover.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMensajes);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(477, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 203);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // txtMensajes
            // 
            this.txtMensajes.Location = new System.Drawing.Point(11, 128);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensajes.Size = new System.Drawing.Size(326, 60);
            this.txtMensajes.TabIndex = 54;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 31;
            this.label4.Text = "Mensajes:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(8, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "Nombre del empleado seleccionado:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(11, 45);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(326, 54);
            this.txtDescripcion.TabIndex = 30;
            // 
            // txtBase64_1
            // 
            this.txtBase64_1.Location = new System.Drawing.Point(29, 466);
            this.txtBase64_1.Multiline = true;
            this.txtBase64_1.Name = "txtBase64_1";
            this.txtBase64_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBase64_1.Size = new System.Drawing.Size(796, 48);
            this.txtBase64_1.TabIndex = 52;
            this.txtBase64_1.Visible = false;
            // 
            // frmLeerHuellas_V2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(837, 449);
            this.Controls.Add(this.txtBase64_1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.cmbEmpresas);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLeerHuellas_V2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLeerHuellas_V2";
            this.Load += new System.EventHandler(this.frmLeerHuellas_V2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgHuellaCapturada)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private System.Windows.Forms.DataGridView dgvDatos;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnBuscar;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox imgHuellaCapturada;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnRemover;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnLimpiar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnGuardar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnVerificar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.TextBox txtBase64_1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnRemoverRegistro;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_empleado_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_cliente_empresarial;
        private System.Windows.Forms.DataGridViewTextBoxColumn aplica_almuerzo;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
    }
}