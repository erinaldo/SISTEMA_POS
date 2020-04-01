namespace Palatium.Tarjeta_Almuerzo
{
    partial class frmCreacionTarjetaAlmuerzo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbControl = new System.Windows.Forms.TabControl();
            this.tabCrear = new System.Windows.Forms.TabPage();
            this.btnTarjetas = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPrecioFinal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidadReal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidadNominal = new System.Windows.Forms.TextBox();
            this.cmbListarTarjetas = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCorreoElectronicoDefault = new System.Windows.Forms.Button();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkPasaporte = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnConsumidorFinal = new System.Windows.Forms.Button();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.btnEditar = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.tabLista = new System.Windows.Forms.TabPage();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.rdbTodos = new System.Windows.Forms.RadioButton();
            this.rdbVigentes = new System.Windows.Forms.RadioButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_creacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.disponibles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbControl.SuspendLayout();
            this.tabCrear.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // tbControl
            // 
            this.tbControl.Controls.Add(this.tabCrear);
            this.tbControl.Controls.Add(this.tabLista);
            this.tbControl.Location = new System.Drawing.Point(0, 0);
            this.tbControl.Name = "tbControl";
            this.tbControl.SelectedIndex = 0;
            this.tbControl.Size = new System.Drawing.Size(779, 339);
            this.tbControl.TabIndex = 2;
            this.tbControl.SelectedIndexChanged += new System.EventHandler(this.tbControl_SelectedIndexChanged);
            // 
            // tabCrear
            // 
            this.tabCrear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabCrear.Controls.Add(this.btnTarjetas);
            this.tabCrear.Controls.Add(this.btnAceptar);
            this.tabCrear.Controls.Add(this.btnLimpiar);
            this.tabCrear.Controls.Add(this.groupBox2);
            this.tabCrear.Controls.Add(this.groupBox1);
            this.tabCrear.Location = new System.Drawing.Point(4, 22);
            this.tabCrear.Name = "tabCrear";
            this.tabCrear.Padding = new System.Windows.Forms.Padding(3);
            this.tabCrear.Size = new System.Drawing.Size(771, 313);
            this.tabCrear.TabIndex = 0;
            this.tabCrear.Text = "Crear Tarjetas de Almuerzo";
            // 
            // btnTarjetas
            // 
            this.btnTarjetas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnTarjetas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTarjetas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnTarjetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTarjetas.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTarjetas.Location = new System.Drawing.Point(540, 207);
            this.btnTarjetas.Name = "btnTarjetas";
            this.btnTarjetas.Size = new System.Drawing.Size(127, 82);
            this.btnTarjetas.TabIndex = 206;
            this.btnTarjetas.Text = "Pagar con Tarjetas";
            this.btnTarjetas.UseVisualStyleBackColor = false;
            this.btnTarjetas.Click += new System.EventHandler(this.btnTarjetas_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Lime;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(407, 207);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(127, 82);
            this.btnAceptar.TabIndex = 205;
            this.btnAceptar.Text = "Pagar en Efectivo";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(671, 207);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(93, 82);
            this.btnLimpiar.TabIndex = 204;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtPrecioFinal);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtCantidadReal);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCantidadNominal);
            this.groupBox2.Controls.Add(this.cmbListarTarjetas);
            this.groupBox2.Location = new System.Drawing.Point(407, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 154);
            this.groupBox2.TabIndex = 198;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tarjetas Configuradas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(76, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 202;
            this.label3.Text = "Precio Final:";
            // 
            // txtPrecioFinal
            // 
            this.txtPrecioFinal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPrecioFinal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecioFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioFinal.Location = new System.Drawing.Point(200, 121);
            this.txtPrecioFinal.MaxLength = 13;
            this.txtPrecioFinal.Name = "txtPrecioFinal";
            this.txtPrecioFinal.ReadOnly = true;
            this.txtPrecioFinal.Size = new System.Drawing.Size(80, 22);
            this.txtPrecioFinal.TabIndex = 201;
            this.txtPrecioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(76, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 16);
            this.label2.TabIndex = 200;
            this.label2.Text = "Cantidad Real:";
            // 
            // txtCantidadReal
            // 
            this.txtCantidadReal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCantidadReal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCantidadReal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadReal.Location = new System.Drawing.Point(200, 96);
            this.txtCantidadReal.MaxLength = 13;
            this.txtCantidadReal.Name = "txtCantidadReal";
            this.txtCantidadReal.ReadOnly = true;
            this.txtCantidadReal.Size = new System.Drawing.Size(80, 22);
            this.txtCantidadReal.TabIndex = 199;
            this.txtCantidadReal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(76, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 198;
            this.label1.Text = "Cantidad Nominal:";
            // 
            // txtCantidadNominal
            // 
            this.txtCantidadNominal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCantidadNominal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCantidadNominal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadNominal.Location = new System.Drawing.Point(200, 71);
            this.txtCantidadNominal.MaxLength = 13;
            this.txtCantidadNominal.Name = "txtCantidadNominal";
            this.txtCantidadNominal.ReadOnly = true;
            this.txtCantidadNominal.Size = new System.Drawing.Size(80, 22);
            this.txtCantidadNominal.TabIndex = 197;
            this.txtCantidadNominal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbListarTarjetas
            // 
            this.cmbListarTarjetas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListarTarjetas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbListarTarjetas.FormattingEnabled = true;
            this.cmbListarTarjetas.Location = new System.Drawing.Point(16, 29);
            this.cmbListarTarjetas.Name = "cmbListarTarjetas";
            this.cmbListarTarjetas.Size = new System.Drawing.Size(319, 23);
            this.cmbListarTarjetas.TabIndex = 99;
            this.cmbListarTarjetas.SelectedIndexChanged += new System.EventHandler(this.cmbListarTarjetas_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.btnCorreoElectronicoDefault);
            this.groupBox1.Controls.Add(this.txtTelefono);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDireccion);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMail);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.chkPasaporte);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnConsumidorFinal);
            this.groupBox1.Controls.Add(this.txtIdentificacion);
            this.groupBox1.Controls.Add(this.txtApellidos);
            this.groupBox1.Controls.Add(this.btnEditar);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 286);
            this.groupBox1.TabIndex = 197;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Cliente";
            // 
            // btnCorreoElectronicoDefault
            // 
            this.btnCorreoElectronicoDefault.AccessibleName = "0";
            this.btnCorreoElectronicoDefault.AutoSize = true;
            this.btnCorreoElectronicoDefault.FlatAppearance.BorderSize = 0;
            this.btnCorreoElectronicoDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorreoElectronicoDefault.ForeColor = System.Drawing.Color.Transparent;
            this.btnCorreoElectronicoDefault.Image = global::Palatium.Properties.Resources.mail_default;
            this.btnCorreoElectronicoDefault.Location = new System.Drawing.Point(328, 210);
            this.btnCorreoElectronicoDefault.Name = "btnCorreoElectronicoDefault";
            this.btnCorreoElectronicoDefault.Size = new System.Drawing.Size(36, 33);
            this.btnCorreoElectronicoDefault.TabIndex = 203;
            this.btnCorreoElectronicoDefault.UseVisualStyleBackColor = true;
            this.btnCorreoElectronicoDefault.Click += new System.EventHandler(this.btnCorreoElectronicoDefault_Click);
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtTelefono.Location = new System.Drawing.Point(93, 251);
            this.txtTelefono.MaxLength = 10;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Size = new System.Drawing.Size(235, 22);
            this.txtTelefono.TabIndex = 199;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(15, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.TabIndex = 202;
            this.label9.Text = "Teléfono:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtDireccion.Location = new System.Drawing.Point(15, 164);
            this.txtDireccion.MaxLength = 100;
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(350, 45);
            this.txtDireccion.TabIndex = 197;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(15, 219);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 16);
            this.label10.TabIndex = 201;
            this.label10.Text = "Email:";
            // 
            // txtMail
            // 
            this.txtMail.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtMail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtMail.Location = new System.Drawing.Point(93, 216);
            this.txtMail.MaxLength = 50;
            this.txtMail.Name = "txtMail";
            this.txtMail.ReadOnly = true;
            this.txtMail.Size = new System.Drawing.Size(235, 22);
            this.txtMail.TabIndex = 198;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(15, 141);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 16);
            this.label11.TabIndex = 200;
            this.label11.Text = "Dirección:";
            // 
            // chkPasaporte
            // 
            this.chkPasaporte.AutoSize = true;
            this.chkPasaporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPasaporte.Location = new System.Drawing.Point(18, 23);
            this.chkPasaporte.Name = "chkPasaporte";
            this.chkPasaporte.Size = new System.Drawing.Size(149, 20);
            this.chkPasaporte.TabIndex = 194;
            this.chkPasaporte.Text = "Consultar Pasaporte";
            this.chkPasaporte.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_ico;
            this.btnBuscar.Location = new System.Drawing.Point(284, 44);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(29, 26);
            this.btnBuscar.TabIndex = 195;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(15, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 16);
            this.label7.TabIndex = 187;
            this.label7.Text = "Identificación:";
            // 
            // btnConsumidorFinal
            // 
            this.btnConsumidorFinal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnConsumidorFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsumidorFinal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConsumidorFinal.Location = new System.Drawing.Point(313, 44);
            this.btnConsumidorFinal.Name = "btnConsumidorFinal";
            this.btnConsumidorFinal.Size = new System.Drawing.Size(52, 26);
            this.btnConsumidorFinal.TabIndex = 196;
            this.btnConsumidorFinal.Text = "C.F.";
            this.btnConsumidorFinal.UseVisualStyleBackColor = false;
            this.btnConsumidorFinal.Click += new System.EventHandler(this.btnConsumidorFinal_Click);
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtIdentificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificacion.Location = new System.Drawing.Point(125, 46);
            this.txtIdentificacion.MaxLength = 13;
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(149, 22);
            this.txtIdentificacion.TabIndex = 182;
            this.txtIdentificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdentificacion_KeyPress);
            // 
            // txtApellidos
            // 
            this.txtApellidos.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtApellidos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApellidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidos.Location = new System.Drawing.Point(17, 93);
            this.txtApellidos.MaxLength = 100;
            this.txtApellidos.Multiline = true;
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.ReadOnly = true;
            this.txtApellidos.Size = new System.Drawing.Size(348, 45);
            this.txtApellidos.TabIndex = 183;
            // 
            // btnEditar
            // 
            this.btnEditar.AutoSize = true;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.LinkColor = System.Drawing.Color.Red;
            this.btnEditar.Location = new System.Drawing.Point(271, 24);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(94, 16);
            this.btnEditar.TabIndex = 193;
            this.btnEditar.TabStop = true;
            this.btnEditar.Text = "Editar Datos";
            this.btnEditar.Visible = false;
            this.btnEditar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnEditar_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(16, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 16);
            this.label8.TabIndex = 191;
            this.label8.Text = "Razón Social / Nombres:";
            // 
            // tabLista
            // 
            this.tabLista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabLista.Controls.Add(this.btnFiltrar);
            this.tabLista.Controls.Add(this.rdbTodos);
            this.tabLista.Controls.Add(this.rdbVigentes);
            this.tabLista.Controls.Add(this.txtBuscar);
            this.tabLista.Controls.Add(this.label4);
            this.tabLista.Controls.Add(this.dgvDatos);
            this.tabLista.Location = new System.Drawing.Point(4, 22);
            this.tabLista.Name = "tabLista";
            this.tabLista.Padding = new System.Windows.Forms.Padding(3);
            this.tabLista.Size = new System.Drawing.Size(771, 313);
            this.tabLista.TabIndex = 1;
            this.tabLista.Text = "Lista de tarjetas del Sistema";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Image = global::Palatium.Properties.Resources.buscar_ico;
            this.btnFiltrar.Location = new System.Drawing.Point(443, 8);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(29, 26);
            this.btnFiltrar.TabIndex = 196;
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // rdbTodos
            // 
            this.rdbTodos.AutoSize = true;
            this.rdbTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbTodos.Location = new System.Drawing.Point(695, 12);
            this.rdbTodos.Name = "rdbTodos";
            this.rdbTodos.Size = new System.Drawing.Size(66, 20);
            this.rdbTodos.TabIndex = 135;
            this.rdbTodos.Text = "Todos";
            this.rdbTodos.UseVisualStyleBackColor = true;
            this.rdbTodos.CheckedChanged += new System.EventHandler(this.rdbTodos_CheckedChanged);
            // 
            // rdbVigentes
            // 
            this.rdbVigentes.AutoSize = true;
            this.rdbVigentes.Checked = true;
            this.rdbVigentes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbVigentes.Location = new System.Drawing.Point(600, 12);
            this.rdbVigentes.Name = "rdbVigentes";
            this.rdbVigentes.Size = new System.Drawing.Size(79, 20);
            this.rdbVigentes.TabIndex = 134;
            this.rdbVigentes.TabStop = true;
            this.rdbVigentes.Text = "Vigentes";
            this.rdbVigentes.UseVisualStyleBackColor = true;
            this.rdbVigentes.CheckedChanged += new System.EventHandler(this.rdbVigentes_CheckedChanged);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(76, 10);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(351, 22);
            this.txtBuscar.TabIndex = 133;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 132;
            this.label4.Text = "Buscar:";
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
            this.identificacion,
            this.cliente,
            this.fecha_creacion,
            this.numero_tarjeta,
            this.estado_P,
            this.disponibles});
            this.dgvDatos.Location = new System.Drawing.Point(8, 49);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(757, 261);
            this.dgvDatos.TabIndex = 131;
            // 
            // identificacion
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identificacion.DefaultCellStyle = dataGridViewCellStyle7;
            this.identificacion.HeaderText = "IDENTIFICACIÓN";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            this.identificacion.Width = 120;
            // 
            // cliente
            // 
            this.cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cliente.DefaultCellStyle = dataGridViewCellStyle8;
            this.cliente.HeaderText = "CLIENTE";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // fecha_creacion
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha_creacion.DefaultCellStyle = dataGridViewCellStyle9;
            this.fecha_creacion.HeaderText = "FECHA EMISIÓN";
            this.fecha_creacion.Name = "fecha_creacion";
            this.fecha_creacion.ReadOnly = true;
            this.fecha_creacion.Width = 120;
            // 
            // numero_tarjeta
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numero_tarjeta.DefaultCellStyle = dataGridViewCellStyle10;
            this.numero_tarjeta.HeaderText = "No. TARJETA";
            this.numero_tarjeta.Name = "numero_tarjeta";
            this.numero_tarjeta.ReadOnly = true;
            // 
            // estado_P
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado_P.DefaultCellStyle = dataGridViewCellStyle11;
            this.estado_P.HeaderText = "ESTADO";
            this.estado_P.Name = "estado_P";
            this.estado_P.ReadOnly = true;
            // 
            // disponibles
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.disponibles.DefaultCellStyle = dataGridViewCellStyle12;
            this.disponibles.HeaderText = "DISPONIBLE";
            this.disponibles.Name = "disponibles";
            this.disponibles.ReadOnly = true;
            // 
            // frmCreacionTarjetaAlmuerzo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(777, 335);
            this.Controls.Add(this.tbControl);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCreacionTarjetaAlmuerzo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo para crear Tarjetas de Almuerzo";
            this.Load += new System.EventHandler(this.frmCreacionTarjetaAlmuerzo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCreacionTarjetaAlmuerzo_KeyDown);
            this.tbControl.ResumeLayout(false);
            this.tabCrear.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabLista.ResumeLayout(false);
            this.tabLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbControl;
        private System.Windows.Forms.TabPage tabCrear;
        private System.Windows.Forms.TabPage tabLista;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkPasaporte;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnConsumidorFinal;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.LinkLabel btnEditar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPrecioFinal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidadReal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidadNominal;
        private System.Windows.Forms.ComboBox cmbListarTarjetas;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnCorreoElectronicoDefault;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnTarjetas;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.RadioButton rdbTodos;
        private System.Windows.Forms.RadioButton rdbVigentes;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_creacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero_tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn disponibles;
    }
}