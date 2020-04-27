namespace Palatium.Personal
{
    partial class frmCajeros
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
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvCajero = new System.Windows.Forms.DataGridView();
            this.Grb_opcioCajero = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.Grb_DatoCajero = new System.Windows.Forms.GroupBox();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dbAyudaPersonal = new Controles.Auxiliares.DB_Ayuda();
            this.chkPermisos = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkContrasena = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblClaveAcceso = new System.Windows.Forms.Label();
            this.txtClaveAcceso = new System.Windows.Forms.TextBox();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigoCajero = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.idPosCajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPersona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoCajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPermiso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grb_listReCajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajero)).BeginInit();
            this.Grb_opcioCajero.SuspendLayout();
            this.Grb_DatoCajero.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.BackColor = System.Drawing.Color.Transparent;
            this.Grb_listReCajero.Controls.Add(this.btnBuscar);
            this.Grb_listReCajero.Controls.Add(this.txtBuscar);
            this.Grb_listReCajero.Controls.Add(this.dgvCajero);
            this.Grb_listReCajero.Location = new System.Drawing.Point(12, 191);
            this.Grb_listReCajero.Name = "Grb_listReCajero";
            this.Grb_listReCajero.Size = new System.Drawing.Size(461, 267);
            this.Grb_listReCajero.TabIndex = 5;
            this.Grb_listReCajero.TabStop = false;
            this.Grb_listReCajero.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(240, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(18, 28);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // dgvCajero
            // 
            this.dgvCajero.AllowUserToAddRows = false;
            this.dgvCajero.AllowUserToDeleteRows = false;
            this.dgvCajero.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCajero.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCajero.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idPosCajero,
            this.idPersona,
            this.codigo,
            this.descripcion,
            this.clave,
            this.estadoCajero,
            this.identificacion,
            this.nombres,
            this.colPermiso,
            this.is_active});
            this.dgvCajero.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCajero.Location = new System.Drawing.Point(18, 54);
            this.dgvCajero.Name = "dgvCajero";
            this.dgvCajero.ReadOnly = true;
            this.dgvCajero.RowHeadersVisible = false;
            this.dgvCajero.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCajero.Size = new System.Drawing.Size(432, 203);
            this.dgvCajero.TabIndex = 0;
            this.dgvCajero.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCajero_CellDoubleClick);
            // 
            // Grb_opcioCajero
            // 
            this.Grb_opcioCajero.BackColor = System.Drawing.Color.Transparent;
            this.Grb_opcioCajero.Controls.Add(this.btnCerrar);
            this.Grb_opcioCajero.Controls.Add(this.btnLimpiar);
            this.Grb_opcioCajero.Controls.Add(this.btnEliminar);
            this.Grb_opcioCajero.Controls.Add(this.btnNuevo);
            this.Grb_opcioCajero.Location = new System.Drawing.Point(479, 191);
            this.Grb_opcioCajero.Name = "Grb_opcioCajero";
            this.Grb_opcioCajero.Size = new System.Drawing.Size(111, 267);
            this.Grb_opcioCajero.TabIndex = 4;
            this.Grb_opcioCajero.TabStop = false;
            this.Grb_opcioCajero.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(17, 207);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(70, 50);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(17, 146);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 50);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnEliminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(17, 85);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 50);
            this.btnEliminar.TabIndex = 1;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(17, 24);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(70, 50);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // Grb_DatoCajero
            // 
            this.Grb_DatoCajero.BackColor = System.Drawing.Color.Transparent;
            this.Grb_DatoCajero.Controls.Add(this.chkHabilitado);
            this.Grb_DatoCajero.Controls.Add(this.label5);
            this.Grb_DatoCajero.Controls.Add(this.dbAyudaPersonal);
            this.Grb_DatoCajero.Controls.Add(this.chkPermisos);
            this.Grb_DatoCajero.Controls.Add(this.label4);
            this.Grb_DatoCajero.Controls.Add(this.label3);
            this.Grb_DatoCajero.Controls.Add(this.label2);
            this.Grb_DatoCajero.Controls.Add(this.chkContrasena);
            this.Grb_DatoCajero.Controls.Add(this.label1);
            this.Grb_DatoCajero.Controls.Add(this.lblClaveAcceso);
            this.Grb_DatoCajero.Controls.Add(this.txtClaveAcceso);
            this.Grb_DatoCajero.Controls.Add(this.lblDescrCajero);
            this.Grb_DatoCajero.Controls.Add(this.txtDescripcion);
            this.Grb_DatoCajero.Controls.Add(this.lblCodigoCajero);
            this.Grb_DatoCajero.Controls.Add(this.txtCodigo);
            this.Grb_DatoCajero.Enabled = false;
            this.Grb_DatoCajero.Location = new System.Drawing.Point(12, 12);
            this.Grb_DatoCajero.Name = "Grb_DatoCajero";
            this.Grb_DatoCajero.Size = new System.Drawing.Size(577, 173);
            this.Grb_DatoCajero.TabIndex = 3;
            this.Grb_DatoCajero.TabStop = false;
            this.Grb_DatoCajero.Text = "Datos del Registro";
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(107, 133);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(83, 17);
            this.chkHabilitado.TabIndex = 59;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(89, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 15);
            this.label5.TabIndex = 58;
            this.label5.Text = "*";
            // 
            // dbAyudaPersonal
            // 
            this.dbAyudaPersonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbAyudaPersonal.iId = 0;
            this.dbAyudaPersonal.Location = new System.Drawing.Point(107, 77);
            this.dbAyudaPersonal.Name = "dbAyudaPersonal";
            this.dbAyudaPersonal.sCodigo = null;
            this.dbAyudaPersonal.Size = new System.Drawing.Size(494, 26);
            this.dbAyudaPersonal.sNombre = null;
            this.dbAyudaPersonal.TabIndex = 57;
            // 
            // chkPermisos
            // 
            this.chkPermisos.AutoSize = true;
            this.chkPermisos.Location = new System.Drawing.Point(251, 122);
            this.chkPermisos.Name = "chkPermisos";
            this.chkPermisos.Size = new System.Drawing.Size(139, 17);
            this.chkPermisos.TabIndex = 56;
            this.chkPermisos.Text = "Permisos Administración";
            this.chkPermisos.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(89, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 15);
            this.label4.TabIndex = 55;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(89, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(89, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 53;
            this.label2.Text = "*";
            // 
            // chkContrasena
            // 
            this.chkContrasena.AutoSize = true;
            this.chkContrasena.Location = new System.Drawing.Point(251, 106);
            this.chkContrasena.Name = "chkContrasena";
            this.chkContrasena.Size = new System.Drawing.Size(118, 17);
            this.chkContrasena.TabIndex = 52;
            this.chkContrasena.Text = "Mostrar Contraseña";
            this.chkContrasena.UseVisualStyleBackColor = true;
            this.chkContrasena.CheckedChanged += new System.EventHandler(this.chkContrasena_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(11, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 51;
            this.label1.Text = "Personas:";
            // 
            // lblClaveAcceso
            // 
            this.lblClaveAcceso.AutoSize = true;
            this.lblClaveAcceso.BackColor = System.Drawing.Color.Transparent;
            this.lblClaveAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaveAcceso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblClaveAcceso.Location = new System.Drawing.Point(11, 108);
            this.lblClaveAcceso.Name = "lblClaveAcceso";
            this.lblClaveAcceso.Size = new System.Drawing.Size(82, 15);
            this.lblClaveAcceso.TabIndex = 12;
            this.lblClaveAcceso.Text = "Clave Acceso:";
            // 
            // txtClaveAcceso
            // 
            this.txtClaveAcceso.Location = new System.Drawing.Point(107, 107);
            this.txtClaveAcceso.MaxLength = 3;
            this.txtClaveAcceso.Name = "txtClaveAcceso";
            this.txtClaveAcceso.PasswordChar = '*';
            this.txtClaveAcceso.Size = new System.Drawing.Size(132, 20);
            this.txtClaveAcceso.TabIndex = 11;
            this.txtClaveAcceso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaveAcceso_KeyPress);
            // 
            // lblDescrCajero
            // 
            this.lblDescrCajero.AutoSize = true;
            this.lblDescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrCajero.Location = new System.Drawing.Point(11, 55);
            this.lblDescrCajero.Name = "lblDescrCajero";
            this.lblDescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lblDescrCajero.TabIndex = 5;
            this.lblDescrCajero.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(107, 54);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(326, 20);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblCodigoCajero
            // 
            this.lblCodigoCajero.AutoSize = true;
            this.lblCodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoCajero.Location = new System.Drawing.Point(11, 30);
            this.lblCodigoCajero.Name = "lblCodigoCajero";
            this.lblCodigoCajero.Size = new System.Drawing.Size(49, 15);
            this.lblCodigoCajero.TabIndex = 3;
            this.lblCodigoCajero.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(107, 28);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(107, 20);
            this.txtCodigo.TabIndex = 2;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 30F;
            this.dataGridViewImageColumn1.Frozen = true;
            this.dataGridViewImageColumn1.HeaderText = "Ediitar";
            this.dataGridViewImageColumn1.Image = global::Palatium.Properties.Resources.editar_img;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 80;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.FillWeight = 30F;
            this.dataGridViewImageColumn2.Frozen = true;
            this.dataGridViewImageColumn2.HeaderText = "Eliminar";
            this.dataGridViewImageColumn2.Image = global::Palatium.Properties.Resources.eliminar_img;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 80;
            // 
            // idPosCajero
            // 
            this.idPosCajero.HeaderText = "ID";
            this.idPosCajero.Name = "idPosCajero";
            this.idPosCajero.ReadOnly = true;
            this.idPosCajero.Visible = false;
            // 
            // idPersona
            // 
            this.idPersona.HeaderText = "idPersona";
            this.idPersona.Name = "idPersona";
            this.idPersona.ReadOnly = true;
            this.idPersona.Visible = false;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "CÓDIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // descripcion
            // 
            this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descripcion.HeaderText = "DESCRIPCIÓN";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            // 
            // clave
            // 
            this.clave.HeaderText = "CLAVE DE ACCESO";
            this.clave.Name = "clave";
            this.clave.ReadOnly = true;
            this.clave.Visible = false;
            this.clave.Width = 120;
            // 
            // estadoCajero
            // 
            this.estadoCajero.HeaderText = "ESTADO";
            this.estadoCajero.Name = "estadoCajero";
            this.estadoCajero.ReadOnly = true;
            this.estadoCajero.Width = 80;
            // 
            // identificacion
            // 
            this.identificacion.HeaderText = "IDENTIFICACION";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            this.identificacion.Visible = false;
            // 
            // nombres
            // 
            this.nombres.HeaderText = "NOMBRES";
            this.nombres.Name = "nombres";
            this.nombres.ReadOnly = true;
            this.nombres.Visible = false;
            // 
            // colPermiso
            // 
            this.colPermiso.HeaderText = "PERMISOS";
            this.colPermiso.Name = "colPermiso";
            this.colPermiso.ReadOnly = true;
            this.colPermiso.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "HABILITADO";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // frmCajeros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(598, 471);
            this.Controls.Add(this.Grb_listReCajero);
            this.Controls.Add(this.Grb_opcioCajero);
            this.Controls.Add(this.Grb_DatoCajero);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCajeros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Cajeros";
            this.Load += new System.EventHandler(this.FInformacionCajero_Load);
            this.Grb_listReCajero.ResumeLayout(false);
            this.Grb_listReCajero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajero)).EndInit();
            this.Grb_opcioCajero.ResumeLayout(false);
            this.Grb_DatoCajero.ResumeLayout(false);
            this.Grb_DatoCajero.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvCajero;
        private System.Windows.Forms.GroupBox Grb_opcioCajero;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.GroupBox Grb_DatoCajero;
        private System.Windows.Forms.CheckBox chkContrasena;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblClaveAcceso;
        private System.Windows.Forms.TextBox txtClaveAcceso;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodigoCajero;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkPermisos;
        private Controles.Auxiliares.DB_Ayuda dbAyudaPersonal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPosCajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPersona;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoCajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPermiso;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
    }
}