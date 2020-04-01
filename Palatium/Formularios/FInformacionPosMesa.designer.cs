namespace Palatium.Formularios
{
    partial class FInformacionPosMesa
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
            this.grupoGrid = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.idMesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idSeccionMesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroMesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.capacidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoMesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.chkEditar = new System.Windows.Forms.CheckBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumeroMesa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCapacidad = new System.Windows.Forms.TextBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaMesa = new System.Windows.Forms.Label();
            this.lblDescrMesa = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigoMesa = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.cmbSeccionMesa = new MisControles.ComboDatos();
            this.lblPosSecMesa = new System.Windows.Forms.Label();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grupoGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoOpciones.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupoGrid
            // 
            this.grupoGrid.BackColor = System.Drawing.Color.Transparent;
            this.grupoGrid.Controls.Add(this.btnBuscar);
            this.grupoGrid.Controls.Add(this.txtBuscar);
            this.grupoGrid.Controls.Add(this.dgvDatos);
            this.grupoGrid.Location = new System.Drawing.Point(363, 81);
            this.grupoGrid.Name = "grupoGrid";
            this.grupoGrid.Size = new System.Drawing.Size(658, 371);
            this.grupoGrid.TabIndex = 5;
            this.grupoGrid.TabStop = false;
            this.grupoGrid.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(235, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 13;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 29);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 12;
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
            this.idMesa,
            this.idSeccionMesa,
            this.codigo,
            this.descripcion,
            this.numeroMesa,
            this.seccion,
            this.capacidad,
            this.estadoMesa,
            this.colX,
            this.colY});
            this.dgvDatos.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(639, 291);
            this.dgvDatos.TabIndex = 14;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // idMesa
            // 
            this.idMesa.HeaderText = "ID";
            this.idMesa.Name = "idMesa";
            this.idMesa.ReadOnly = true;
            this.idMesa.Visible = false;
            // 
            // idSeccionMesa
            // 
            this.idSeccionMesa.HeaderText = "IdSeccion";
            this.idSeccionMesa.Name = "idSeccionMesa";
            this.idSeccionMesa.ReadOnly = true;
            this.idSeccionMesa.Visible = false;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "CODIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 90;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "DESCRIPCION";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 150;
            // 
            // numeroMesa
            // 
            this.numeroMesa.HeaderText = "NÚMERO DE MESA";
            this.numeroMesa.Name = "numeroMesa";
            this.numeroMesa.ReadOnly = true;
            this.numeroMesa.Width = 80;
            // 
            // seccion
            // 
            this.seccion.HeaderText = "SECCION";
            this.seccion.Name = "seccion";
            this.seccion.ReadOnly = true;
            this.seccion.Width = 120;
            // 
            // capacidad
            // 
            this.capacidad.HeaderText = "CAPACIDAD";
            this.capacidad.Name = "capacidad";
            this.capacidad.ReadOnly = true;
            this.capacidad.Width = 80;
            // 
            // estadoMesa
            // 
            this.estadoMesa.HeaderText = "ESTADO";
            this.estadoMesa.Name = "estadoMesa";
            this.estadoMesa.ReadOnly = true;
            this.estadoMesa.Width = 80;
            // 
            // colX
            // 
            this.colX.HeaderText = "colX";
            this.colX.Name = "colX";
            this.colX.ReadOnly = true;
            this.colX.Visible = false;
            // 
            // colY
            // 
            this.colY.HeaderText = "colY";
            this.colY.Name = "colY";
            this.colY.ReadOnly = true;
            this.colY.Visible = false;
            // 
            // grupoOpciones
            // 
            this.grupoOpciones.BackColor = System.Drawing.Color.Transparent;
            this.grupoOpciones.Controls.Add(this.btnCerrar);
            this.grupoOpciones.Controls.Add(this.btnLimpiar);
            this.grupoOpciones.Controls.Add(this.btnAnular);
            this.grupoOpciones.Controls.Add(this.btnNuevo);
            this.grupoOpciones.Location = new System.Drawing.Point(15, 370);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(342, 82);
            this.grupoOpciones.TabIndex = 4;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(251, 24);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(72, 39);
            this.btnCerrar.TabIndex = 11;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(173, 24);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(72, 39);
            this.btnLimpiar.TabIndex = 10;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Enabled = false;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(95, 24);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(72, 39);
            this.btnAnular.TabIndex = 9;
            this.btnAnular.Text = "Eliminar";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(17, 24);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(72, 39);
            this.btnNuevo.TabIndex = 8;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevoPosMesa_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.BackColor = System.Drawing.Color.Transparent;
            this.grupoDatos.Controls.Add(this.chkEditar);
            this.grupoDatos.Controls.Add(this.btnExaminar);
            this.grupoDatos.Controls.Add(this.label5);
            this.grupoDatos.Controls.Add(this.txtY);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.txtX);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.txtNumeroMesa);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.txtCapacidad);
            this.grupoDatos.Controls.Add(this.cmbEstado);
            this.grupoDatos.Controls.Add(this.lblEstaMesa);
            this.grupoDatos.Controls.Add(this.lblDescrMesa);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblCodigoMesa);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 162);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(342, 202);
            this.grupoDatos.TabIndex = 3;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // chkEditar
            // 
            this.chkEditar.AutoSize = true;
            this.chkEditar.Location = new System.Drawing.Point(237, 83);
            this.chkEditar.Name = "chkEditar";
            this.chkEditar.Size = new System.Drawing.Size(53, 17);
            this.chkEditar.TabIndex = 22;
            this.chkEditar.Text = "Editar";
            this.chkEditar.UseVisualStyleBackColor = true;
            this.chkEditar.Visible = false;
            this.chkEditar.CheckedChanged += new System.EventHandler(this.chkEditar_CheckedChanged);
            // 
            // btnExaminar
            // 
            this.btnExaminar.BackColor = System.Drawing.Color.Yellow;
            this.btnExaminar.Location = new System.Drawing.Point(273, 131);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(29, 21);
            this.btnExaminar.TabIndex = 6;
            this.btnExaminar.Text = "...";
            this.ttMensaje.SetToolTip(this.btnExaminar, "Clic aquí para seleccionar la ubicación de la mesa");
            this.btnExaminar.UseVisualStyleBackColor = false;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(189, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 15);
            this.label5.TabIndex = 20;
            this.label5.Text = "Y";
            // 
            // txtY
            // 
            this.txtY.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtY.Enabled = false;
            this.txtY.Location = new System.Drawing.Point(205, 132);
            this.txtY.MaxLength = 3;
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(52, 20);
            this.txtY.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(109, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "X";
            // 
            // txtX
            // 
            this.txtX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtX.Enabled = false;
            this.txtX.Location = new System.Drawing.Point(125, 132);
            this.txtX.MaxLength = 3;
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(52, 20);
            this.txtX.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(18, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Ubicación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(17, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "No. de Mesa";
            // 
            // txtNumeroMesa
            // 
            this.txtNumeroMesa.Location = new System.Drawing.Point(110, 80);
            this.txtNumeroMesa.MaxLength = 20;
            this.txtNumeroMesa.Name = "txtNumeroMesa";
            this.txtNumeroMesa.Size = new System.Drawing.Size(121, 20);
            this.txtNumeroMesa.TabIndex = 4;
            this.txtNumeroMesa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroMesa_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(16, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Capacidad:";
            // 
            // txtCapacidad
            // 
            this.txtCapacidad.Location = new System.Drawing.Point(110, 106);
            this.txtCapacidad.MaxLength = 3;
            this.txtCapacidad.Name = "txtCapacidad";
            this.txtCapacidad.Size = new System.Drawing.Size(67, 20);
            this.txtCapacidad.TabIndex = 5;
            this.txtCapacidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCapacidad_KeyPress);
            // 
            // cmbEstado
            // 
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstado.Location = new System.Drawing.Point(110, 161);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 21);
            this.cmbEstado.TabIndex = 7;
            // 
            // lblEstaMesa
            // 
            this.lblEstaMesa.AutoSize = true;
            this.lblEstaMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaMesa.Location = new System.Drawing.Point(16, 162);
            this.lblEstaMesa.Name = "lblEstaMesa";
            this.lblEstaMesa.Size = new System.Drawing.Size(48, 15);
            this.lblEstaMesa.TabIndex = 7;
            this.lblEstaMesa.Text = "Estado:";
            // 
            // lblDescrMesa
            // 
            this.lblDescrMesa.AutoSize = true;
            this.lblDescrMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrMesa.Location = new System.Drawing.Point(16, 56);
            this.lblDescrMesa.Name = "lblDescrMesa";
            this.lblDescrMesa.Size = new System.Drawing.Size(75, 15);
            this.lblDescrMesa.TabIndex = 5;
            this.lblDescrMesa.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(110, 54);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(216, 20);
            this.txtDescripcion.TabIndex = 3;
            // 
            // lblCodigoMesa
            // 
            this.lblCodigoMesa.AutoSize = true;
            this.lblCodigoMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoMesa.Location = new System.Drawing.Point(16, 30);
            this.lblCodigoMesa.Name = "lblCodigoMesa";
            this.lblCodigoMesa.Size = new System.Drawing.Size(49, 15);
            this.lblCodigoMesa.TabIndex = 3;
            this.lblCodigoMesa.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(110, 28);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(216, 20);
            this.txtCodigo.TabIndex = 2;
            // 
            // cmbSeccionMesa
            // 
            this.cmbSeccionMesa.FormattingEnabled = true;
            this.cmbSeccionMesa.Location = new System.Drawing.Point(110, 29);
            this.cmbSeccionMesa.Name = "cmbSeccionMesa";
            this.cmbSeccionMesa.Size = new System.Drawing.Size(213, 21);
            this.cmbSeccionMesa.TabIndex = 1;
            this.cmbSeccionMesa.SelectedIndexChanged += new System.EventHandler(this.cmbSeccionMesa_SelectedIndexChanged);
            // 
            // lblPosSecMesa
            // 
            this.lblPosSecMesa.AutoSize = true;
            this.lblPosSecMesa.Location = new System.Drawing.Point(16, 33);
            this.lblPosSecMesa.Name = "lblPosSecMesa";
            this.lblPosSecMesa.Size = new System.Drawing.Size(78, 13);
            this.lblPosSecMesa.TabIndex = 6;
            this.lblPosSecMesa.Text = "Sección Mesa:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbSeccionMesa);
            this.groupBox1.Controls.Add(this.lblPosSecMesa);
            this.groupBox1.Location = new System.Drawing.Point(12, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 69);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuración de Secciones";
            // 
            // FInformacionPosMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1033, 469);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grupoGrid);
            this.Controls.Add(this.grupoOpciones);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionPosMesa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Mesas";
            this.Load += new System.EventHandler(this.FInformacionPosMesa_Load);
            this.grupoGrid.ResumeLayout(false);
            this.grupoGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoGrid;
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
        private System.Windows.Forms.Label lblEstaMesa;
        private System.Windows.Forms.Label lblDescrMesa;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodigoMesa;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblPosSecMesa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCapacidad;
        private MisControles.ComboDatos cmbSeccionMesa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumeroMesa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSeccionMesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroMesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn seccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn capacidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoMesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colY;
        private System.Windows.Forms.CheckBox chkEditar;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}