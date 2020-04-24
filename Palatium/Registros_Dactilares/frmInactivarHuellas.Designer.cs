namespace Palatium.Registros_Dactilares
{
    partial class frmInactivarHuellas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInactivarHuellas));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.StateProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.btnGuardar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnBuscar = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.cmbEmpresas = new System.Windows.Forms.ComboBox();
            this.txtBuscar = new Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkHabilitarHuella = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEstadoHuella = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.id_pos_empleado_cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.huella_dactilar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active_huella = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
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
            this.txtDescripcion.Size = new System.Drawing.Size(326, 71);
            this.txtDescripcion.TabIndex = 30;
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
            this.btnLimpiar.Location = new System.Drawing.Point(15, 15);
            this.btnLimpiar.Name = "btnLimpiar";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.BorderRadius = 1;
            stateProperties1.BorderThickness = 1;
            stateProperties1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties1.IconLeftImage = null;
            stateProperties1.IconRightImage = null;
            this.btnLimpiar.onHoverState = stateProperties1;
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
            this.btnGuardar.Location = new System.Drawing.Point(179, 7);
            this.btnGuardar.Name = "btnGuardar";
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.BorderRadius = 1;
            stateProperties2.BorderThickness = 1;
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties2.IconLeftImage = null;
            stateProperties2.IconRightImage = null;
            this.btnGuardar.onHoverState = stateProperties2;
            this.btnGuardar.Size = new System.Drawing.Size(158, 44);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(64)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.btnLimpiar);
            this.panel1.Controls.Add(this.btnGuardar);
            this.panel1.Location = new System.Drawing.Point(477, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 64);
            this.panel1.TabIndex = 62;
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.lblRegistros.Location = new System.Drawing.Point(138, 424);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(155, 16);
            this.lblRegistros.TabIndex = 59;
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
            this.label16.TabIndex = 60;
            this.label16.Text = "N° de Registros:";
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
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties3.BorderRadius = 1;
            stateProperties3.BorderThickness = 1;
            stateProperties3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            stateProperties3.IconLeftImage = null;
            stateProperties3.IconRightImage = null;
            this.btnBuscar.onHoverState = stateProperties3;
            this.btnBuscar.Size = new System.Drawing.Size(38, 35);
            this.btnBuscar.TabIndex = 58;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
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
            this.huella_dactilar,
            this.is_active_huella,
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
            this.dgvDatos.TabIndex = 57;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpresas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(101, 21);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(313, 25);
            this.cmbEmpresas.TabIndex = 56;
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
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
            this.txtBuscar.TabIndex = 55;
            this.txtBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtBuscar.TextMarginLeft = 5;
            this.txtBuscar.TextPlaceholder = "";
            this.txtBuscar.UseSystemPasswordChar = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(26, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 54;
            this.label2.Text = "Búsqueda:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Empresa:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkHabilitarHuella);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtEstadoHuella);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIdentificacion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDescripcion);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(477, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 260);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información";
            // 
            // chkHabilitarHuella
            // 
            this.chkHabilitarHuella.AutoSize = true;
            this.chkHabilitarHuella.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.chkHabilitarHuella.Location = new System.Drawing.Point(223, 212);
            this.chkHabilitarHuella.Name = "chkHabilitarHuella";
            this.chkHabilitarHuella.Size = new System.Drawing.Size(114, 20);
            this.chkHabilitarHuella.TabIndex = 35;
            this.chkHabilitarHuella.Text = "Habilitar huella";
            this.chkHabilitarHuella.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "Estado actual de la huella";
            // 
            // txtEstadoHuella
            // 
            this.txtEstadoHuella.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEstadoHuella.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstadoHuella.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstadoHuella.Location = new System.Drawing.Point(11, 212);
            this.txtEstadoHuella.MaxLength = 100;
            this.txtEstadoHuella.Name = "txtEstadoHuella";
            this.txtEstadoHuella.ReadOnly = true;
            this.txtEstadoHuella.Size = new System.Drawing.Size(163, 21);
            this.txtEstadoHuella.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "Identificación";
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtIdentificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificacion.Location = new System.Drawing.Point(11, 156);
            this.txtIdentificacion.MaxLength = 100;
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.ReadOnly = true;
            this.txtIdentificacion.Size = new System.Drawing.Size(326, 21);
            this.txtIdentificacion.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(232)))), ((int)(((byte)(166)))));
            this.label6.Location = new System.Drawing.Point(502, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(312, 54);
            this.label6.TabIndex = 35;
            this.label6.Text = "ACTIVAR / INACTIVAR LAS HUELLAS DE LOS EMPLEADOS";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // id_pos_empleado_cliente
            // 
            this.id_pos_empleado_cliente.HeaderText = "ID EMPLEADO CLIENTE";
            this.id_pos_empleado_cliente.Name = "id_pos_empleado_cliente";
            this.id_pos_empleado_cliente.ReadOnly = true;
            this.id_pos_empleado_cliente.Visible = false;
            // 
            // huella_dactilar
            // 
            this.huella_dactilar.HeaderText = "HUELLA DACTILAR";
            this.huella_dactilar.Name = "huella_dactilar";
            this.huella_dactilar.ReadOnly = true;
            this.huella_dactilar.Visible = false;
            // 
            // is_active_huella
            // 
            this.is_active_huella.HeaderText = "IS ACTIVE";
            this.is_active_huella.Name = "is_active_huella";
            this.is_active_huella.ReadOnly = true;
            this.is_active_huella.Visible = false;
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
            this.estado.HeaderText = "ESTADO HUELLA";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Width = 120;
            // 
            // frmInactivarHuellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(837, 449);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblRegistros);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.cmbEmpresas);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmInactivarHuellas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmInactivarHuellas";
            this.Load += new System.EventHandler(this.frmInactivarHuellas_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnLimpiar;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnGuardar;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.Label label16;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton btnBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.ComboBox cmbEmpresas;
        private Bunifu.UI.WinForms.BunifuTextbox.BunifuTextBox txtBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEstadoHuella;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.CheckBox chkHabilitarHuella;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_empleado_cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn huella_dactilar;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active_huella;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
    }
}