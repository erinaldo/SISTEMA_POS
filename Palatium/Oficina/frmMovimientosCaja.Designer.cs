namespace Palatium.Oficina
{
    partial class frmMovimientosCaja
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
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.txtHora = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grupoNuevo = new System.Windows.Forms.GroupBox();
            this.cmbCargo = new System.Windows.Forms.ComboBox();
            this.cmbEmpleados = new System.Windows.Forms.ComboBox();
            this.cmbCaja = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.TimerFecha = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbLocalidad = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.LblFecha = new System.Windows.Forms.Label();
            this.cmbFiltrar = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnImprimirLibroCaja = new System.Windows.Forms.Button();
            this.btnCalendario = new System.Windows.Forms.Button();
            this.cmbSeleccionarCaja = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnImpimir = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.rdbIngresos = new System.Windows.Forms.RadioButton();
            this.rdbEgresos = new System.Windows.Forms.RadioButton();
            this.grupoNuevo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(21, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Número:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(21, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Fecha:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(516, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Valor:";
            // 
            // txtNumero
            // 
            this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumero.Enabled = false;
            this.txtNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumero.Location = new System.Drawing.Point(105, 59);
            this.txtNumero.MaxLength = 13;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(133, 24);
            this.txtNumero.TabIndex = 10;
            // 
            // txtFecha
            // 
            this.txtFecha.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFecha.Enabled = false;
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(105, 89);
            this.txtFecha.MaxLength = 13;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(133, 24);
            this.txtFecha.TabIndex = 13;
            // 
            // txtValor
            // 
            this.txtValor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValor.Location = new System.Drawing.Point(618, 128);
            this.txtValor.MaxLength = 13;
            this.txtValor.Name = "txtValor";
            this.txtValor.Size = new System.Drawing.Size(179, 24);
            this.txtValor.TabIndex = 16;
            this.txtValor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValor_KeyPress);
            // 
            // txtConcepto
            // 
            this.txtConcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcepto.Location = new System.Drawing.Point(105, 125);
            this.txtConcepto.MaxLength = 150;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(386, 24);
            this.txtConcepto.TabIndex = 15;
            // 
            // txtHora
            // 
            this.txtHora.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtHora.Enabled = false;
            this.txtHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHora.Location = new System.Drawing.Point(319, 89);
            this.txtHora.MaxLength = 13;
            this.txtHora.Name = "txtHora";
            this.txtHora.Size = new System.Drawing.Size(110, 24);
            this.txtHora.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(511, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Cargo:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(266, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Hora:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(266, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 17;
            this.label6.Text = "Caja:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(511, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 16);
            this.label7.TabIndex = 24;
            this.label7.Text = "Dependiente:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(21, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 16);
            this.label8.TabIndex = 26;
            this.label8.Text = "Concepto:";
            // 
            // grupoNuevo
            // 
            this.grupoNuevo.Controls.Add(this.rdbEgresos);
            this.grupoNuevo.Controls.Add(this.rdbIngresos);
            this.grupoNuevo.Controls.Add(this.cmbCargo);
            this.grupoNuevo.Controls.Add(this.cmbEmpleados);
            this.grupoNuevo.Controls.Add(this.cmbCaja);
            this.grupoNuevo.Controls.Add(this.btnLimpiar);
            this.grupoNuevo.Controls.Add(this.btnGuardar);
            this.grupoNuevo.Controls.Add(this.btnSalir);
            this.grupoNuevo.Controls.Add(this.label8);
            this.grupoNuevo.Controls.Add(this.label7);
            this.grupoNuevo.Controls.Add(this.txtConcepto);
            this.grupoNuevo.Controls.Add(this.txtHora);
            this.grupoNuevo.Controls.Add(this.label4);
            this.grupoNuevo.Controls.Add(this.label5);
            this.grupoNuevo.Controls.Add(this.label6);
            this.grupoNuevo.Controls.Add(this.txtValor);
            this.grupoNuevo.Controls.Add(this.txtFecha);
            this.grupoNuevo.Controls.Add(this.txtNumero);
            this.grupoNuevo.Controls.Add(this.label2);
            this.grupoNuevo.Controls.Add(this.label1);
            this.grupoNuevo.Controls.Add(this.label3);
            this.grupoNuevo.Enabled = false;
            this.grupoNuevo.Location = new System.Drawing.Point(12, 434);
            this.grupoNuevo.Name = "grupoNuevo";
            this.grupoNuevo.Size = new System.Drawing.Size(957, 165);
            this.grupoNuevo.TabIndex = 28;
            this.grupoNuevo.TabStop = false;
            // 
            // cmbCargo
            // 
            this.cmbCargo.BackColor = System.Drawing.SystemColors.Control;
            this.cmbCargo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCargo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCargo.FormattingEnabled = true;
            this.cmbCargo.Items.AddRange(new object[] {
            "Todas",
            "Entradas",
            "Salidas"});
            this.cmbCargo.Location = new System.Drawing.Point(618, 89);
            this.cmbCargo.Name = "cmbCargo";
            this.cmbCargo.Size = new System.Drawing.Size(179, 26);
            this.cmbCargo.TabIndex = 129;
            this.ttMensaje.SetToolTip(this.cmbCargo, "Ver Entradas y Salidas Manuales");
            // 
            // cmbEmpleados
            // 
            this.cmbEmpleados.BackColor = System.Drawing.SystemColors.Control;
            this.cmbEmpleados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpleados.FormattingEnabled = true;
            this.cmbEmpleados.Items.AddRange(new object[] {
            "Todas",
            "Entradas",
            "Salidas"});
            this.cmbEmpleados.Location = new System.Drawing.Point(618, 57);
            this.cmbEmpleados.Name = "cmbEmpleados";
            this.cmbEmpleados.Size = new System.Drawing.Size(179, 26);
            this.cmbEmpleados.TabIndex = 128;
            this.ttMensaje.SetToolTip(this.cmbEmpleados, "Ver Entradas y Salidas Manuales");
            // 
            // cmbCaja
            // 
            this.cmbCaja.BackColor = System.Drawing.SystemColors.Control;
            this.cmbCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCaja.FormattingEnabled = true;
            this.cmbCaja.Items.AddRange(new object[] {
            "Todas",
            "Entradas",
            "Salidas"});
            this.cmbCaja.Location = new System.Drawing.Point(316, 57);
            this.cmbCaja.Name = "cmbCaja";
            this.cmbCaja.Size = new System.Drawing.Size(175, 26);
            this.cmbCaja.TabIndex = 127;
            this.ttMensaje.SetToolTip(this.cmbCaja, "Ver Entradas y Salidas Manuales");
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.limpiar_ico;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(837, 60);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(98, 48);
            this.btnLimpiar.TabIndex = 19;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::Palatium.Properties.Resources.guardar_ico;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(837, 13);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 48);
            this.btnGuardar.TabIndex = 18;
            this.btnGuardar.Text = "Listo";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::Palatium.Properties.Resources.salir_ico;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(837, 107);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(98, 48);
            this.btnSalir.TabIndex = 20;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(12, 100);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(957, 328);
            this.dgvDatos.TabIndex = 124;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            this.dgvDatos.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDatos_RowPostPaint);
            // 
            // TimerFecha
            // 
            this.TimerFecha.Enabled = true;
            this.TimerFecha.Interval = 1000;
            this.TimerFecha.Tick += new System.EventHandler(this.TimerFecha_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbLocalidad);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.btnNuevo);
            this.groupBox2.Controls.Add(this.LblFecha);
            this.groupBox2.Controls.Add(this.cmbFiltrar);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.btnImprimirLibroCaja);
            this.groupBox2.Controls.Add(this.btnCalendario);
            this.groupBox2.Controls.Add(this.cmbSeleccionarCaja);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnImpimir);
            this.groupBox2.Controls.Add(this.btnEliminar);
            this.groupBox2.Controls.Add(this.btnEditar);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(957, 82);
            this.groupBox2.TabIndex = 125;
            this.groupBox2.TabStop = false;
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(382, 14);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(230, 26);
            this.cmbLocalidad.TabIndex = 126;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(279, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 16);
            this.label11.TabIndex = 26;
            this.label11.Text = "Localidad:";
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Image = global::Palatium.Properties.Resources.nuevo_png2;
            this.btnNuevo.Location = new System.Drawing.Point(2, 10);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(63, 60);
            this.btnNuevo.TabIndex = 1;
            this.ttMensaje.SetToolTip(this.btnNuevo, "Nuevo (Ctrl + N)");
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // LblFecha
            // 
            this.LblFecha.AutoSize = true;
            this.LblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFecha.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblFecha.Location = new System.Drawing.Point(626, 57);
            this.LblFecha.Name = "LblFecha";
            this.LblFecha.Size = new System.Drawing.Size(46, 16);
            this.LblFecha.TabIndex = 24;
            this.LblFecha.Text = "Fecha";
            // 
            // cmbFiltrar
            // 
            this.cmbFiltrar.BackColor = System.Drawing.SystemColors.Control;
            this.cmbFiltrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFiltrar.FormattingEnabled = true;
            this.cmbFiltrar.Items.AddRange(new object[] {
            "Todas",
            "Entradas",
            "Salidas"});
            this.cmbFiltrar.Location = new System.Drawing.Point(382, 48);
            this.cmbFiltrar.Name = "cmbFiltrar";
            this.cmbFiltrar.Size = new System.Drawing.Size(230, 26);
            this.cmbFiltrar.TabIndex = 5;
            this.ttMensaje.SetToolTip(this.cmbFiltrar, "Ver Entradas y Salidas Manuales");
            this.cmbFiltrar.SelectedIndexChanged += new System.EventHandler(this.cmbFiltrar_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label10.Location = new System.Drawing.Point(279, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 16);
            this.label10.TabIndex = 22;
            this.label10.Text = "Movimiento:";
            // 
            // btnImprimirLibroCaja
            // 
            this.btnImprimirLibroCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirLibroCaja.Image = global::Palatium.Properties.Resources.imprimir;
            this.btnImprimirLibroCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimirLibroCaja.Location = new System.Drawing.Point(805, 15);
            this.btnImprimirLibroCaja.Name = "btnImprimirLibroCaja";
            this.btnImprimirLibroCaja.Size = new System.Drawing.Size(146, 50);
            this.btnImprimirLibroCaja.TabIndex = 8;
            this.btnImprimirLibroCaja.Text = "Imprimir libro\r\ncaja (tícket)";
            this.btnImprimirLibroCaja.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttMensaje.SetToolTip(this.btnImprimirLibroCaja, "Clic aquí para imprimir el libro de caja");
            this.btnImprimirLibroCaja.UseVisualStyleBackColor = true;
            this.btnImprimirLibroCaja.Click += new System.EventHandler(this.btnImprimirLibroCaja_Click);
            // 
            // btnCalendario
            // 
            this.btnCalendario.Enabled = false;
            this.btnCalendario.FlatAppearance.BorderSize = 0;
            this.btnCalendario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalendario.ForeColor = System.Drawing.Color.Transparent;
            this.btnCalendario.Image = global::Palatium.Properties.Resources.calendario_png;
            this.btnCalendario.Location = new System.Drawing.Point(734, 9);
            this.btnCalendario.Name = "btnCalendario";
            this.btnCalendario.Size = new System.Drawing.Size(63, 60);
            this.btnCalendario.TabIndex = 7;
            this.ttMensaje.SetToolTip(this.btnCalendario, "Clic aquí para visualizar el calendario");
            this.btnCalendario.UseVisualStyleBackColor = true;
            this.btnCalendario.Visible = false;
            this.btnCalendario.Click += new System.EventHandler(this.btnCalendario_Click);
            // 
            // cmbSeleccionarCaja
            // 
            this.cmbSeleccionarCaja.BackColor = System.Drawing.SystemColors.Control;
            this.cmbSeleccionarCaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeleccionarCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSeleccionarCaja.FormattingEnabled = true;
            this.cmbSeleccionarCaja.Items.AddRange(new object[] {
            "Todas",
            "Fecha"});
            this.cmbSeleccionarCaja.Location = new System.Drawing.Point(628, 27);
            this.cmbSeleccionarCaja.Name = "cmbSeleccionarCaja";
            this.cmbSeleccionarCaja.Size = new System.Drawing.Size(91, 26);
            this.cmbSeleccionarCaja.TabIndex = 6;
            this.cmbSeleccionarCaja.Visible = false;
            this.cmbSeleccionarCaja.SelectedIndexChanged += new System.EventHandler(this.cmbSeleccionarCaja_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label9.Location = new System.Drawing.Point(628, 8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Caja:";
            this.label9.Visible = false;
            // 
            // btnImpimir
            // 
            this.btnImpimir.FlatAppearance.BorderSize = 0;
            this.btnImpimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImpimir.ForeColor = System.Drawing.Color.Transparent;
            this.btnImpimir.Image = global::Palatium.Properties.Resources.impresora_icono;
            this.btnImpimir.Location = new System.Drawing.Point(201, 9);
            this.btnImpimir.Name = "btnImpimir";
            this.btnImpimir.Size = new System.Drawing.Size(63, 60);
            this.btnImpimir.TabIndex = 4;
            this.ttMensaje.SetToolTip(this.btnImpimir, "Imprimir (Ctrl + P)");
            this.btnImpimir.UseVisualStyleBackColor = true;
            this.btnImpimir.Click += new System.EventHandler(this.btnImpimir_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.Transparent;
            this.btnEliminar.Image = global::Palatium.Properties.Resources.eliminar_png;
            this.btnEliminar.Location = new System.Drawing.Point(135, 9);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(63, 60);
            this.btnEliminar.TabIndex = 3;
            this.ttMensaje.SetToolTip(this.btnEliminar, "Clic aquí para eliminar el registro seleccionado");
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.ForeColor = System.Drawing.Color.Transparent;
            this.btnEditar.Image = global::Palatium.Properties.Resources.editar_png;
            this.btnEditar.Location = new System.Drawing.Point(68, 9);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(63, 60);
            this.btnEditar.TabIndex = 2;
            this.ttMensaje.SetToolTip(this.btnEditar, "Clic aquí para editar el registro seleccionado");
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // ttMensaje
            // 
            this.ttMensaje.ToolTipTitle = "Información";
            // 
            // rdbIngresos
            // 
            this.rdbIngresos.AutoSize = true;
            this.rdbIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdbIngresos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbIngresos.Location = new System.Drawing.Point(24, 26);
            this.rdbIngresos.Name = "rdbIngresos";
            this.rdbIngresos.Size = new System.Drawing.Size(104, 20);
            this.rdbIngresos.TabIndex = 130;
            this.rdbIngresos.Text = "INGRESOS";
            this.rdbIngresos.UseVisualStyleBackColor = true;
            // 
            // rdbEgresos
            // 
            this.rdbEgresos.AutoSize = true;
            this.rdbEgresos.Checked = true;
            this.rdbEgresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.rdbEgresos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbEgresos.Location = new System.Drawing.Point(169, 26);
            this.rdbEgresos.Name = "rdbEgresos";
            this.rdbEgresos.Size = new System.Drawing.Size(99, 20);
            this.rdbEgresos.TabIndex = 131;
            this.rdbEgresos.TabStop = true;
            this.rdbEgresos.Text = "EGRESOS";
            this.rdbEgresos.UseVisualStyleBackColor = true;
            // 
            // frmMovimientosCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(981, 611);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.grupoNuevo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMovimientosCaja";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimientos de Caja";
            this.Load += new System.EventHandler(this.frmMovimientosCaja_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMovimientosCaja_KeyDown);
            this.grupoNuevo.ResumeLayout(false);
            this.grupoNuevo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.TextBox txtHora;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox grupoNuevo;
        internal System.Windows.Forms.Button btnLimpiar;
        internal System.Windows.Forms.Button btnGuardar;
        internal System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Timer TimerFecha;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnCalendario;
        private System.Windows.Forms.ComboBox cmbSeleccionarCaja;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnImpimir;
        private System.Windows.Forms.Button btnImprimirLibroCaja;
        private System.Windows.Forms.ComboBox cmbFiltrar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblFecha;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbLocalidad;
        private System.Windows.Forms.ComboBox cmbCaja;
        private System.Windows.Forms.ComboBox cmbCargo;
        private System.Windows.Forms.ComboBox cmbEmpleados;
        private System.Windows.Forms.RadioButton rdbEgresos;
        private System.Windows.Forms.RadioButton rdbIngresos;
    }
}