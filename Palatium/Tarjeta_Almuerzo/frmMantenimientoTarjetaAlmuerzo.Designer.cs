namespace Palatium.Tarjeta_Almuerzo
{
    partial class frmMantenimientoTarjetaAlmuerzo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tbControl = new System.Windows.Forms.TabControl();
            this.tabCantidades = new System.Windows.Forms.TabPage();
            this.chkEstado = new System.Windows.Forms.CheckBox();
            this.btnLimpiarCantidades = new System.Windows.Forms.Button();
            this.btnGrabarCantidades = new System.Windows.Forms.Button();
            this.txtCantidadReal = new System.Windows.Forms.TextBox();
            this.txtCantidadNominal = new System.Windows.Forms.TextBox();
            this.txtCodigoCantidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDatosCantidades = new System.Windows.Forms.DataGridView();
            this.id_pos_tar_cantidad_almuerzo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad_nominal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad_real = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabProductos = new System.Windows.Forms.TabPage();
            this.dgvDatosParametros = new System.Windows.Forms.DataGridView();
            this.btnLimpiarParametros = new System.Windows.Forms.Button();
            this.btnGrabarParametros = new System.Windows.Forms.Button();
            this.chkHabilitadoParametros = new System.Windows.Forms.CheckBox();
            this.cmbItemTarjetaAlmuerzo = new System.Windows.Forms.ComboBox();
            this.cmbTipoTarjetaAlmuerzo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRegistroCantidades = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.id_pos_tar_cantidad_tipo_almuerzo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_tar_cantidad_almuerzo_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_producto_tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_producto_descarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad_nominal_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad_real_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbControl.SuspendLayout();
            this.tabCantidades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCantidades)).BeginInit();
            this.tabProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosParametros)).BeginInit();
            this.SuspendLayout();
            // 
            // tbControl
            // 
            this.tbControl.Controls.Add(this.tabCantidades);
            this.tbControl.Controls.Add(this.tabProductos);
            this.tbControl.Location = new System.Drawing.Point(0, 0);
            this.tbControl.Name = "tbControl";
            this.tbControl.SelectedIndex = 0;
            this.tbControl.Size = new System.Drawing.Size(588, 346);
            this.tbControl.TabIndex = 1;
            this.tbControl.SelectedIndexChanged += new System.EventHandler(this.tbControl_SelectedIndexChanged);
            // 
            // tabCantidades
            // 
            this.tabCantidades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tabCantidades.Controls.Add(this.chkEstado);
            this.tabCantidades.Controls.Add(this.btnLimpiarCantidades);
            this.tabCantidades.Controls.Add(this.btnGrabarCantidades);
            this.tabCantidades.Controls.Add(this.txtCantidadReal);
            this.tabCantidades.Controls.Add(this.txtCantidadNominal);
            this.tabCantidades.Controls.Add(this.txtCodigoCantidad);
            this.tabCantidades.Controls.Add(this.label3);
            this.tabCantidades.Controls.Add(this.label1);
            this.tabCantidades.Controls.Add(this.label2);
            this.tabCantidades.Controls.Add(this.dgvDatosCantidades);
            this.tabCantidades.Location = new System.Drawing.Point(4, 22);
            this.tabCantidades.Name = "tabCantidades";
            this.tabCantidades.Padding = new System.Windows.Forms.Padding(3);
            this.tabCantidades.Size = new System.Drawing.Size(580, 320);
            this.tabCantidades.TabIndex = 0;
            this.tabCantidades.Text = "Parametrizar Cantidades";
            // 
            // chkEstado
            // 
            this.chkEstado.AutoSize = true;
            this.chkEstado.Checked = true;
            this.chkEstado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEstado.Enabled = false;
            this.chkEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEstado.ForeColor = System.Drawing.Color.Red;
            this.chkEstado.Location = new System.Drawing.Point(20, 114);
            this.chkEstado.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkEstado.Name = "chkEstado";
            this.chkEstado.Size = new System.Drawing.Size(92, 22);
            this.chkEstado.TabIndex = 127;
            this.chkEstado.Text = "Habilitado";
            this.chkEstado.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarCantidades
            // 
            this.btnLimpiarCantidades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnLimpiarCantidades.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiarCantidades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarCantidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarCantidades.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiarCantidades.Location = new System.Drawing.Point(303, 66);
            this.btnLimpiarCantidades.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLimpiarCantidades.Name = "btnLimpiarCantidades";
            this.btnLimpiarCantidades.Size = new System.Drawing.Size(93, 40);
            this.btnLimpiarCantidades.TabIndex = 126;
            this.btnLimpiarCantidades.Text = "Limpiar";
            this.btnLimpiarCantidades.UseVisualStyleBackColor = false;
            this.btnLimpiarCantidades.Click += new System.EventHandler(this.btnLimpiarCantidades_Click);
            // 
            // btnGrabarCantidades
            // 
            this.btnGrabarCantidades.BackColor = System.Drawing.Color.Blue;
            this.btnGrabarCantidades.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGrabarCantidades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabarCantidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabarCantidades.ForeColor = System.Drawing.Color.White;
            this.btnGrabarCantidades.Location = new System.Drawing.Point(303, 20);
            this.btnGrabarCantidades.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGrabarCantidades.Name = "btnGrabarCantidades";
            this.btnGrabarCantidades.Size = new System.Drawing.Size(93, 40);
            this.btnGrabarCantidades.TabIndex = 125;
            this.btnGrabarCantidades.Text = "Nuevo";
            this.btnGrabarCantidades.UseVisualStyleBackColor = false;
            this.btnGrabarCantidades.Click += new System.EventHandler(this.btnGrabarCantidades_Click);
            // 
            // txtCantidadReal
            // 
            this.txtCantidadReal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCantidadReal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtCantidadReal.Location = new System.Drawing.Point(168, 80);
            this.txtCantidadReal.Name = "txtCantidadReal";
            this.txtCantidadReal.ReadOnly = true;
            this.txtCantidadReal.Size = new System.Drawing.Size(69, 24);
            this.txtCantidadReal.TabIndex = 97;
            this.txtCantidadReal.Text = "1";
            this.txtCantidadReal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidadReal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadReal_KeyPress);
            this.txtCantidadReal.Leave += new System.EventHandler(this.txtCantidadReal_Leave);
            // 
            // txtCantidadNominal
            // 
            this.txtCantidadNominal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCantidadNominal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtCantidadNominal.Location = new System.Drawing.Point(168, 50);
            this.txtCantidadNominal.Name = "txtCantidadNominal";
            this.txtCantidadNominal.ReadOnly = true;
            this.txtCantidadNominal.Size = new System.Drawing.Size(69, 24);
            this.txtCantidadNominal.TabIndex = 96;
            this.txtCantidadNominal.Text = "1";
            this.txtCantidadNominal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidadNominal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadNominal_KeyPress);
            this.txtCantidadNominal.Leave += new System.EventHandler(this.txtCantidadNominal_Leave);
            // 
            // txtCodigoCantidad
            // 
            this.txtCodigoCantidad.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCodigoCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtCodigoCantidad.Location = new System.Drawing.Point(168, 20);
            this.txtCodigoCantidad.Name = "txtCodigoCantidad";
            this.txtCodigoCantidad.ReadOnly = true;
            this.txtCodigoCantidad.Size = new System.Drawing.Size(69, 24);
            this.txtCodigoCantidad.TabIndex = 95;
            this.txtCodigoCantidad.Text = "0";
            this.txtCodigoCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.Location = new System.Drawing.Point(21, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 18);
            this.label3.TabIndex = 94;
            this.label3.Text = "Cantidad Real: *";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(21, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 18);
            this.label1.TabIndex = 93;
            this.label1.Text = "Cantidad Nominal: *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(21, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 92;
            this.label2.Text = "Código: *";
            // 
            // dgvDatosCantidades
            // 
            this.dgvDatosCantidades.AllowUserToAddRows = false;
            this.dgvDatosCantidades.AllowUserToDeleteRows = false;
            this.dgvDatosCantidades.AllowUserToResizeColumns = false;
            this.dgvDatosCantidades.AllowUserToResizeRows = false;
            this.dgvDatosCantidades.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatosCantidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosCantidades.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pos_tar_cantidad_almuerzo,
            this.is_active,
            this.codigo,
            this.cantidad_nominal,
            this.cantidad_real,
            this.estado});
            this.dgvDatosCantidades.Location = new System.Drawing.Point(8, 154);
            this.dgvDatosCantidades.Name = "dgvDatosCantidades";
            this.dgvDatosCantidades.ReadOnly = true;
            this.dgvDatosCantidades.RowHeadersVisible = false;
            this.dgvDatosCantidades.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatosCantidades.Size = new System.Drawing.Size(566, 156);
            this.dgvDatosCantidades.TabIndex = 0;
            this.dgvDatosCantidades.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosCantidades_CellDoubleClick);
            // 
            // id_pos_tar_cantidad_almuerzo
            // 
            this.id_pos_tar_cantidad_almuerzo.HeaderText = "ID";
            this.id_pos_tar_cantidad_almuerzo.Name = "id_pos_tar_cantidad_almuerzo";
            this.id_pos_tar_cantidad_almuerzo.ReadOnly = true;
            this.id_pos_tar_cantidad_almuerzo.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "ESTADO_VALOR";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // codigo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigo.DefaultCellStyle = dataGridViewCellStyle1;
            this.codigo.HeaderText = "CÓDIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 150;
            // 
            // cantidad_nominal
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad_nominal.DefaultCellStyle = dataGridViewCellStyle2;
            this.cantidad_nominal.HeaderText = "CANT. NOMINAL";
            this.cantidad_nominal.Name = "cantidad_nominal";
            this.cantidad_nominal.ReadOnly = true;
            this.cantidad_nominal.Width = 150;
            // 
            // cantidad_real
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad_real.DefaultCellStyle = dataGridViewCellStyle3;
            this.cantidad_real.HeaderText = "CANT. REAL";
            this.cantidad_real.Name = "cantidad_real";
            this.cantidad_real.ReadOnly = true;
            this.cantidad_real.Width = 150;
            // 
            // estado
            // 
            this.estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle4;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // tabProductos
            // 
            this.tabProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.tabProductos.Controls.Add(this.dgvDatosParametros);
            this.tabProductos.Controls.Add(this.btnLimpiarParametros);
            this.tabProductos.Controls.Add(this.btnGrabarParametros);
            this.tabProductos.Controls.Add(this.chkHabilitadoParametros);
            this.tabProductos.Controls.Add(this.cmbItemTarjetaAlmuerzo);
            this.tabProductos.Controls.Add(this.cmbTipoTarjetaAlmuerzo);
            this.tabProductos.Controls.Add(this.label6);
            this.tabProductos.Controls.Add(this.label5);
            this.tabProductos.Controls.Add(this.cmbRegistroCantidades);
            this.tabProductos.Controls.Add(this.label4);
            this.tabProductos.Location = new System.Drawing.Point(4, 22);
            this.tabProductos.Name = "tabProductos";
            this.tabProductos.Padding = new System.Windows.Forms.Padding(3);
            this.tabProductos.Size = new System.Drawing.Size(580, 320);
            this.tabProductos.TabIndex = 1;
            this.tabProductos.Text = "Parametrizar Productos de la Tarjeta de Almuerzo";
            // 
            // dgvDatosParametros
            // 
            this.dgvDatosParametros.AllowUserToAddRows = false;
            this.dgvDatosParametros.AllowUserToDeleteRows = false;
            this.dgvDatosParametros.AllowUserToResizeColumns = false;
            this.dgvDatosParametros.AllowUserToResizeRows = false;
            this.dgvDatosParametros.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatosParametros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatosParametros.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pos_tar_cantidad_tipo_almuerzo,
            this.id_pos_tar_cantidad_almuerzo_P,
            this.id_producto_tarjeta,
            this.id_producto_descarga,
            this.is_active_P,
            this.cantidad_nominal_P,
            this.cantidad_real_P,
            this.item_tarjeta,
            this.item_producto,
            this.estado_P});
            this.dgvDatosParametros.Location = new System.Drawing.Point(8, 154);
            this.dgvDatosParametros.Name = "dgvDatosParametros";
            this.dgvDatosParametros.ReadOnly = true;
            this.dgvDatosParametros.RowHeadersVisible = false;
            this.dgvDatosParametros.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatosParametros.Size = new System.Drawing.Size(566, 156);
            this.dgvDatosParametros.TabIndex = 131;
            this.dgvDatosParametros.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatosParametros_CellDoubleClick);
            // 
            // btnLimpiarParametros
            // 
            this.btnLimpiarParametros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnLimpiarParametros.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiarParametros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarParametros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarParametros.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiarParametros.Location = new System.Drawing.Point(480, 108);
            this.btnLimpiarParametros.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnLimpiarParametros.Name = "btnLimpiarParametros";
            this.btnLimpiarParametros.Size = new System.Drawing.Size(93, 40);
            this.btnLimpiarParametros.TabIndex = 130;
            this.btnLimpiarParametros.Text = "Limpiar";
            this.btnLimpiarParametros.UseVisualStyleBackColor = false;
            this.btnLimpiarParametros.Click += new System.EventHandler(this.btnLimpiarParametros_Click);
            // 
            // btnGrabarParametros
            // 
            this.btnGrabarParametros.BackColor = System.Drawing.Color.Blue;
            this.btnGrabarParametros.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGrabarParametros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGrabarParametros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabarParametros.ForeColor = System.Drawing.Color.White;
            this.btnGrabarParametros.Location = new System.Drawing.Point(379, 108);
            this.btnGrabarParametros.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGrabarParametros.Name = "btnGrabarParametros";
            this.btnGrabarParametros.Size = new System.Drawing.Size(93, 40);
            this.btnGrabarParametros.TabIndex = 129;
            this.btnGrabarParametros.Text = "Nuevo";
            this.btnGrabarParametros.UseVisualStyleBackColor = false;
            this.btnGrabarParametros.Click += new System.EventHandler(this.btnGrabarParametros_Click);
            // 
            // chkHabilitadoParametros
            // 
            this.chkHabilitadoParametros.AutoSize = true;
            this.chkHabilitadoParametros.Checked = true;
            this.chkHabilitadoParametros.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitadoParametros.Enabled = false;
            this.chkHabilitadoParametros.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitadoParametros.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitadoParametros.Location = new System.Drawing.Point(14, 108);
            this.chkHabilitadoParametros.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkHabilitadoParametros.Name = "chkHabilitadoParametros";
            this.chkHabilitadoParametros.Size = new System.Drawing.Size(92, 22);
            this.chkHabilitadoParametros.TabIndex = 128;
            this.chkHabilitadoParametros.Text = "Habilitado";
            this.chkHabilitadoParametros.UseVisualStyleBackColor = true;
            // 
            // cmbItemTarjetaAlmuerzo
            // 
            this.cmbItemTarjetaAlmuerzo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItemTarjetaAlmuerzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItemTarjetaAlmuerzo.FormattingEnabled = true;
            this.cmbItemTarjetaAlmuerzo.Location = new System.Drawing.Point(306, 36);
            this.cmbItemTarjetaAlmuerzo.Name = "cmbItemTarjetaAlmuerzo";
            this.cmbItemTarjetaAlmuerzo.Size = new System.Drawing.Size(267, 23);
            this.cmbItemTarjetaAlmuerzo.TabIndex = 98;
            // 
            // cmbTipoTarjetaAlmuerzo
            // 
            this.cmbTipoTarjetaAlmuerzo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoTarjetaAlmuerzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoTarjetaAlmuerzo.FormattingEnabled = true;
            this.cmbTipoTarjetaAlmuerzo.Location = new System.Drawing.Point(8, 36);
            this.cmbTipoTarjetaAlmuerzo.Name = "cmbTipoTarjetaAlmuerzo";
            this.cmbTipoTarjetaAlmuerzo.Size = new System.Drawing.Size(267, 23);
            this.cmbTipoTarjetaAlmuerzo.TabIndex = 97;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label6.Location = new System.Drawing.Point(303, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(172, 18);
            this.label6.TabIndex = 96;
            this.label6.Text = "Ítem de Tarjeta Almuerzo";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label5.Location = new System.Drawing.Point(11, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 18);
            this.label5.TabIndex = 95;
            this.label5.Text = "Tipo de Tarjeta Almuerzo";
            // 
            // cmbRegistroCantidades
            // 
            this.cmbRegistroCantidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegistroCantidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegistroCantidades.FormattingEnabled = true;
            this.cmbRegistroCantidades.Location = new System.Drawing.Point(192, 70);
            this.cmbRegistroCantidades.Name = "cmbRegistroCantidades";
            this.cmbRegistroCantidades.Size = new System.Drawing.Size(381, 23);
            this.cmbRegistroCantidades.TabIndex = 94;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.Location = new System.Drawing.Point(11, 71);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 18);
            this.label4.TabIndex = 93;
            this.label4.Text = "Registro de Cantidades:";
            // 
            // id_pos_tar_cantidad_tipo_almuerzo
            // 
            this.id_pos_tar_cantidad_tipo_almuerzo.HeaderText = "ID";
            this.id_pos_tar_cantidad_tipo_almuerzo.Name = "id_pos_tar_cantidad_tipo_almuerzo";
            this.id_pos_tar_cantidad_tipo_almuerzo.ReadOnly = true;
            this.id_pos_tar_cantidad_tipo_almuerzo.Visible = false;
            this.id_pos_tar_cantidad_tipo_almuerzo.Width = 80;
            // 
            // id_pos_tar_cantidad_almuerzo_P
            // 
            this.id_pos_tar_cantidad_almuerzo_P.HeaderText = "ID CANTIDAD";
            this.id_pos_tar_cantidad_almuerzo_P.Name = "id_pos_tar_cantidad_almuerzo_P";
            this.id_pos_tar_cantidad_almuerzo_P.ReadOnly = true;
            this.id_pos_tar_cantidad_almuerzo_P.Visible = false;
            // 
            // id_producto_tarjeta
            // 
            this.id_producto_tarjeta.HeaderText = "ID PRODUCTO TARJETA";
            this.id_producto_tarjeta.Name = "id_producto_tarjeta";
            this.id_producto_tarjeta.ReadOnly = true;
            this.id_producto_tarjeta.Visible = false;
            // 
            // id_producto_descarga
            // 
            this.id_producto_descarga.HeaderText = "ID PRODUCTO ITEM";
            this.id_producto_descarga.Name = "id_producto_descarga";
            this.id_producto_descarga.ReadOnly = true;
            this.id_producto_descarga.Visible = false;
            // 
            // is_active_P
            // 
            this.is_active_P.HeaderText = "IS ACTIVE";
            this.is_active_P.Name = "is_active_P";
            this.is_active_P.ReadOnly = true;
            this.is_active_P.Visible = false;
            // 
            // cantidad_nominal_P
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad_nominal_P.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad_nominal_P.HeaderText = "CANT. NOM";
            this.cantidad_nominal_P.Name = "cantidad_nominal_P";
            this.cantidad_nominal_P.ReadOnly = true;
            this.cantidad_nominal_P.Width = 80;
            // 
            // cantidad_real_P
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad_real_P.DefaultCellStyle = dataGridViewCellStyle6;
            this.cantidad_real_P.HeaderText = "CANT. REAL";
            this.cantidad_real_P.Name = "cantidad_real_P";
            this.cantidad_real_P.ReadOnly = true;
            this.cantidad_real_P.Width = 80;
            // 
            // item_tarjeta
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.item_tarjeta.DefaultCellStyle = dataGridViewCellStyle7;
            this.item_tarjeta.HeaderText = "ITEM TARJETA";
            this.item_tarjeta.Name = "item_tarjeta";
            this.item_tarjeta.ReadOnly = true;
            this.item_tarjeta.Width = 150;
            // 
            // item_producto
            // 
            this.item_producto.HeaderText = "ITEM PRODUCTO";
            this.item_producto.Name = "item_producto";
            this.item_producto.ReadOnly = true;
            this.item_producto.Width = 150;
            // 
            // estado_P
            // 
            this.estado_P.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado_P.DefaultCellStyle = dataGridViewCellStyle8;
            this.estado_P.HeaderText = "ESTADO";
            this.estado_P.Name = "estado_P";
            this.estado_P.ReadOnly = true;
            // 
            // frmMantenimientoTarjetaAlmuerzo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(590, 344);
            this.Controls.Add(this.tbControl);
            this.MaximizeBox = false;
            this.Name = "frmMantenimientoTarjetaAlmuerzo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mantenimiento de Ítems de Tarjeta de Almuerzos";
            this.Load += new System.EventHandler(this.frmMantenimientoTarjetaAlmuerzo_Load);
            this.tbControl.ResumeLayout(false);
            this.tabCantidades.ResumeLayout(false);
            this.tabCantidades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosCantidades)).EndInit();
            this.tabProductos.ResumeLayout(false);
            this.tabProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatosParametros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbControl;
        private System.Windows.Forms.TabPage tabCantidades;
        private System.Windows.Forms.DataGridView dgvDatosCantidades;
        private System.Windows.Forms.TabPage tabProductos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidadReal;
        private System.Windows.Forms.TextBox txtCantidadNominal;
        private System.Windows.Forms.TextBox txtCodigoCantidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLimpiarCantidades;
        private System.Windows.Forms.Button btnGrabarCantidades;
        private System.Windows.Forms.CheckBox chkEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_tar_cantidad_almuerzo;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad_nominal;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad_real;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.ComboBox cmbItemTarjetaAlmuerzo;
        private System.Windows.Forms.ComboBox cmbTipoTarjetaAlmuerzo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRegistroCantidades;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkHabilitadoParametros;
        private System.Windows.Forms.DataGridView dgvDatosParametros;
        private System.Windows.Forms.Button btnLimpiarParametros;
        private System.Windows.Forms.Button btnGrabarParametros;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_tar_cantidad_tipo_almuerzo;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_tar_cantidad_almuerzo_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto_tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto_descarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad_nominal_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad_real_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn item_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_P;
    }
}