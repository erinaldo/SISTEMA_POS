namespace Palatium.Facturacion_Electronica
{
    partial class frmGenerarEnviarFactura
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
            this.chkSeleccionar = new System.Windows.Forms.CheckBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnReenviar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblCuentaRegistros = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.colMarca = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIdFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstablecimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPtoEmision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumeroComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.txtFechaFinal = new MetroFramework.Controls.MetroDateTime();
            this.txtFechaInicial = new MetroFramework.Controls.MetroDateTime();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkSeleccionar
            // 
            this.chkSeleccionar.AutoSize = true;
            this.chkSeleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSeleccionar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkSeleccionar.Location = new System.Drawing.Point(15, 520);
            this.chkSeleccionar.Name = "chkSeleccionar";
            this.chkSeleccionar.Size = new System.Drawing.Size(212, 20);
            this.chkSeleccionar.TabIndex = 149;
            this.chkSeleccionar.Text = "Seleccionar todos los registros";
            this.chkSeleccionar.UseVisualStyleBackColor = true;
            this.chkSeleccionar.CheckedChanged += new System.EventHandler(this.chkSeleccionar_CheckedChanged);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.limpiar_ico;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(976, 497);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(96, 43);
            this.btnLimpiar.TabIndex = 146;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnReenviar
            // 
            this.btnReenviar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnReenviar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnReenviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReenviar.Image = global::Palatium.Properties.Resources.sincronizar_png;
            this.btnReenviar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReenviar.Location = new System.Drawing.Point(383, 487);
            this.btnReenviar.Name = "btnReenviar";
            this.btnReenviar.Size = new System.Drawing.Size(313, 62);
            this.btnReenviar.TabIndex = 145;
            this.btnReenviar.Text = "ENVIAR FACTURAS AL  CLIENTE";
            this.btnReenviar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReenviar.UseVisualStyleBackColor = true;
            this.btnReenviar.Click += new System.EventHandler(this.btnReenviar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::Palatium.Properties.Resources.salir_ico;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(1078, 497);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(94, 43);
            this.btnSalir.TabIndex = 144;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblCuentaRegistros
            // 
            this.lblCuentaRegistros.AutoSize = true;
            this.lblCuentaRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuentaRegistros.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCuentaRegistros.Location = new System.Drawing.Point(140, 497);
            this.lblCuentaRegistros.Name = "lblCuentaRegistros";
            this.lblCuentaRegistros.Size = new System.Drawing.Size(178, 16);
            this.lblCuentaRegistros.TabIndex = 143;
            this.lblCuentaRegistros.Text = "0 Registros Encontrados";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(15, 497);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 16);
            this.label7.TabIndex = 142;
            this.label7.Text = "Total Registros:";
            // 
            // btnOK
            // 
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::Palatium.Properties.Resources.ok4;
            this.btnOK.Location = new System.Drawing.Point(1116, 21);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(32, 30);
            this.btnOK.TabIndex = 14;
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMarca,
            this.colIdFactura,
            this.colLocalidad,
            this.colTipo,
            this.colFecha,
            this.colEstablecimiento,
            this.colPtoEmision,
            this.colNumeroComprobante,
            this.colCliente,
            this.colMail,
            this.colEstado});
            this.dgvDatos.Location = new System.Drawing.Point(12, 87);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(1160, 390);
            this.dgvDatos.TabIndex = 140;
            // 
            // colMarca
            // 
            this.colMarca.Frozen = true;
            this.colMarca.HeaderText = "Marca";
            this.colMarca.Name = "colMarca";
            this.colMarca.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colMarca.Width = 50;
            // 
            // colIdFactura
            // 
            this.colIdFactura.Frozen = true;
            this.colIdFactura.HeaderText = "ID";
            this.colIdFactura.Name = "colIdFactura";
            this.colIdFactura.ReadOnly = true;
            this.colIdFactura.Visible = false;
            // 
            // colLocalidad
            // 
            this.colLocalidad.Frozen = true;
            this.colLocalidad.HeaderText = "Localidad";
            this.colLocalidad.Name = "colLocalidad";
            // 
            // colTipo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTipo.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTipo.Frozen = true;
            this.colTipo.HeaderText = "Doc";
            this.colTipo.Name = "colTipo";
            this.colTipo.Width = 75;
            // 
            // colFecha
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colFecha.DefaultCellStyle = dataGridViewCellStyle2;
            this.colFecha.Frozen = true;
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.Width = 75;
            // 
            // colEstablecimiento
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colEstablecimiento.DefaultCellStyle = dataGridViewCellStyle3;
            this.colEstablecimiento.Frozen = true;
            this.colEstablecimiento.HeaderText = "Est.";
            this.colEstablecimiento.Name = "colEstablecimiento";
            this.colEstablecimiento.Width = 75;
            // 
            // colPtoEmision
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colPtoEmision.DefaultCellStyle = dataGridViewCellStyle4;
            this.colPtoEmision.Frozen = true;
            this.colPtoEmision.HeaderText = "Pto. Vta.";
            this.colPtoEmision.Name = "colPtoEmision";
            this.colPtoEmision.Width = 75;
            // 
            // colNumeroComprobante
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNumeroComprobante.DefaultCellStyle = dataGridViewCellStyle5;
            this.colNumeroComprobante.Frozen = true;
            this.colNumeroComprobante.HeaderText = "Comprobante";
            this.colNumeroComprobante.Name = "colNumeroComprobante";
            // 
            // colCliente
            // 
            this.colCliente.Frozen = true;
            this.colCliente.HeaderText = "Cliente";
            this.colCliente.Name = "colCliente";
            this.colCliente.Width = 240;
            // 
            // colMail
            // 
            this.colMail.Frozen = true;
            this.colMail.HeaderText = "CorreoElectrónico";
            this.colMail.Name = "colMail";
            this.colMail.Width = 140;
            // 
            // colEstado
            // 
            this.colEstado.Frozen = true;
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.Width = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(784, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Fecha Final:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(464, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Fecha Inical:";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(76, 25);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(152, 21);
            this.cmbEmpresa.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(241, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Localidad:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empresa:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtFechaFinal);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtFechaInicial);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbLocalidad);
            this.groupBox1.Controls.Add(this.cmbEmpresa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1160, 60);
            this.groupBox1.TabIndex = 141;
            this.groupBox1.TabStop = false;
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(316, 26);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(133, 21);
            this.cmbLocalidad.TabIndex = 3;
            // 
            // txtFechaFinal
            // 
            this.txtFechaFinal.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.txtFechaFinal.Location = new System.Drawing.Point(871, 24);
            this.txtFechaFinal.MinimumSize = new System.Drawing.Size(0, 25);
            this.txtFechaFinal.Name = "txtFechaFinal";
            this.txtFechaFinal.Size = new System.Drawing.Size(219, 25);
            this.txtFechaFinal.TabIndex = 151;
            // 
            // txtFechaInicial
            // 
            this.txtFechaInicial.FontSize = MetroFramework.MetroDateTimeSize.Small;
            this.txtFechaInicial.Location = new System.Drawing.Point(551, 24);
            this.txtFechaInicial.MinimumSize = new System.Drawing.Size(0, 25);
            this.txtFechaInicial.Name = "txtFechaInicial";
            this.txtFechaInicial.Size = new System.Drawing.Size(219, 25);
            this.txtFechaInicial.TabIndex = 150;
            // 
            // frmGenerarEnviarFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.chkSeleccionar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnReenviar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblCuentaRegistros);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGenerarEnviarFactura";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reenviar Facturas ELectrónicas";
            this.Load += new System.EventHandler(this.frmGenerarEnviarFactura_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGenerarEnviarFactura_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSeleccionar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnReenviar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label lblCuentaRegistros;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private MisControles.ComboDatos cmbLocalidad;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMarca;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstablecimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPtoEmision;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private MetroFramework.Controls.MetroDateTime txtFechaFinal;
        private MetroFramework.Controls.MetroDateTime txtFechaInicial;
    }
}