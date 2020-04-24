namespace Palatium.Registros_Dactilares
{
    partial class frmClienteEmpresarial
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
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClienteEmpresarial));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.lblRegistros = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBuscar = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.btnBuscar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnLimpiar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnAgregar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnEliminar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.label6 = new System.Windows.Forms.Label();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.id_pos_cliente_empresarial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(108, 57);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(219, 41);
            this.txtDescripcion.TabIndex = 7;
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtIdentificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificacion.Location = new System.Drawing.Point(108, 21);
            this.txtIdentificacion.MaxLength = 10;
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.ReadOnly = true;
            this.txtIdentificacion.Size = new System.Drawing.Size(144, 21);
            this.txtIdentificacion.TabIndex = 6;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.lblRegistros.Location = new System.Drawing.Point(131, 376);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(155, 16);
            this.lblRegistros.TabIndex = 35;
            this.lblRegistros.Text = "0 Registros Encontrados";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.label16.Location = new System.Drawing.Point(22, 376);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 16);
            this.label16.TabIndex = 36;
            this.label16.Text = "N° de Registros:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(18, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 37;
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
            this.txtBuscar.Location = new System.Drawing.Point(106, 24);
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
            this.txtBuscar.Size = new System.Drawing.Size(304, 35);
            this.txtBuscar.Style = Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox._Style.Bunifu;
            this.txtBuscar.TabIndex = 41;
            this.txtBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBuscar.TextMarginLeft = 5;
            this.txtBuscar.TextPlaceholder = "";
            this.txtBuscar.UseSystemPasswordChar = false;
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
            this.btnBuscar.Location = new System.Drawing.Point(418, 24);
            this.btnBuscar.Name = "btnBuscar";
            stateProperties8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties8.BorderRadius = 1;
            stateProperties8.BorderThickness = 1;
            stateProperties8.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties8.IconLeftImage = null;
            stateProperties8.IconRightImage = null;
            this.btnBuscar.onHoverState = stateProperties8;
            this.btnBuscar.Size = new System.Drawing.Size(38, 35);
            this.btnBuscar.TabIndex = 42;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.btnLimpiar.Location = new System.Drawing.Point(508, 334);
            this.btnLimpiar.Name = "btnLimpiar";
            stateProperties5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties5.BorderRadius = 1;
            stateProperties5.BorderThickness = 1;
            stateProperties5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties5.IconLeftImage = null;
            stateProperties5.IconRightImage = null;
            this.btnLimpiar.onHoverState = stateProperties5;
            this.btnLimpiar.Size = new System.Drawing.Size(117, 31);
            this.btnLimpiar.TabIndex = 45;
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAgregar.BackgroundImage")));
            this.btnAgregar.ButtonText = "AGREGAR";
            this.btnAgregar.ButtonTextMarginLeft = 0;
            this.btnAgregar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnAgregar.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnAgregar.DisabledForecolor = System.Drawing.Color.White;
            this.btnAgregar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.Black;
            this.btnAgregar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnAgregar.IconPadding = 10;
            this.btnAgregar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnAgregar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAgregar.IdleBorderRadius = 32;
            this.btnAgregar.IdleBorderThickness = 0;
            this.btnAgregar.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.btnAgregar.IdleIconLeftImage = null;
            this.btnAgregar.IdleIconRightImage = null;
            this.btnAgregar.Location = new System.Drawing.Point(573, 273);
            this.btnAgregar.Name = "btnAgregar";
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties6.BorderRadius = 1;
            stateProperties6.BorderThickness = 1;
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties6.IconLeftImage = null;
            stateProperties6.IconRightImage = null;
            this.btnAgregar.onHoverState = stateProperties6;
            this.btnAgregar.Size = new System.Drawing.Size(158, 44);
            this.btnAgregar.TabIndex = 44;
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Transparent;
            this.btnEliminar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEliminar.BackgroundImage")));
            this.btnEliminar.ButtonText = "Eliminar";
            this.btnEliminar.ButtonTextMarginLeft = 0;
            this.btnEliminar.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.btnEliminar.DisabledFillColor = System.Drawing.Color.Gray;
            this.btnEliminar.DisabledForecolor = System.Drawing.Color.White;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.btnEliminar.IconPadding = 10;
            this.btnEliminar.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.btnEliminar.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEliminar.IdleBorderRadius = 32;
            this.btnEliminar.IdleBorderThickness = 0;
            this.btnEliminar.IdleFillColor = System.Drawing.Color.Red;
            this.btnEliminar.IdleIconLeftImage = null;
            this.btnEliminar.IdleIconRightImage = null;
            this.btnEliminar.Location = new System.Drawing.Point(677, 334);
            this.btnEliminar.Name = "btnEliminar";
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties7.BorderRadius = 1;
            stateProperties7.BorderThickness = 1;
            stateProperties7.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties7.IconLeftImage = null;
            stateProperties7.IconRightImage = null;
            this.btnEliminar.onHoverState = stateProperties7;
            this.btnEliminar.Size = new System.Drawing.Size(117, 31);
            this.btnEliminar.TabIndex = 43;
            this.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.label6.Location = new System.Drawing.Point(482, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 54);
            this.label6.TabIndex = 46;
            this.label6.Text = "MÓDULO PARA CREAR LOS CLIENTES EMPRESARIALES";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.chkHabilitado.Location = new System.Drawing.Point(108, 121);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(87, 20);
            this.chkHabilitado.TabIndex = 47;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
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
            this.id_pos_cliente_empresarial,
            this.id_persona,
            this.is_active,
            this.identificacion,
            this.cliente,
            this.estado});
            this.dgvDatos.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.dgvDatos.Location = new System.Drawing.Point(21, 83);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(435, 282);
            this.dgvDatos.TabIndex = 47;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.label5);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.txtIdentificacion);
            this.grupoDatos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.grupoDatos.ForeColor = System.Drawing.Color.White;
            this.grupoDatos.Location = new System.Drawing.Point(473, 83);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(336, 161);
            this.grupoDatos.TabIndex = 48;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos generales";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Razón social:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 30;
            this.label5.Text = "Identificación:";
            // 
            // id_pos_cliente_empresarial
            // 
            this.id_pos_cliente_empresarial.HeaderText = "ID";
            this.id_pos_cliente_empresarial.Name = "id_pos_cliente_empresarial";
            this.id_pos_cliente_empresarial.ReadOnly = true;
            this.id_pos_cliente_empresarial.Visible = false;
            // 
            // id_persona
            // 
            this.id_persona.HeaderText = "ID PERSONA";
            this.id_persona.Name = "id_persona";
            this.id_persona.ReadOnly = true;
            this.id_persona.Visible = false;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.identificacion.DefaultCellStyle = dataGridViewCellStyle4;
            this.identificacion.HeaderText = "IDENTIFICACIÓN";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            this.cliente.DefaultCellStyle = dataGridViewCellStyle5;
            this.cliente.HeaderText = "CLIENTE EMPRESARIAL";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // estado
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            this.estado.DefaultCellStyle = dataGridViewCellStyle6;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // frmClienteEmpresarial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(821, 410);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label2);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frmClienteEmpresarial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Clientes Empresariales";
            this.Load += new System.EventHandler(this.frmClienteEmpresarial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox txtBuscar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnBuscar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnLimpiar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnAgregar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnEliminar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_cliente_empresarial;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
    }
}