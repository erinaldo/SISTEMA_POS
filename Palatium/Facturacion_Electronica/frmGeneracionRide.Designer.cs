namespace Palatium.Facturacion_Electronica
{
    partial class frmGeneracionRide
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
            this.cmbTipoComprobante = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtInicio = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtFin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtFechaInicial = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtFechaFinal = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dgvDatos = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colMarca = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.colIdFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipoComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumeroComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaEmision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClaveAcceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmbiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConsultar = new DevComponents.DotNetBar.ButtonX();
            this.btnGenerar = new DevComponents.DotNetBar.ButtonX();
            this.btnImprimir = new DevComponents.DotNetBar.ButtonX();
            this.btnEnviar = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new DevComponents.DotNetBar.ButtonX();
            this.btnLimpiar = new DevComponents.DotNetBar.ButtonX();
            this.btnDesde = new DevComponents.DotNetBar.ButtonX();
            this.btnHasta = new DevComponents.DotNetBar.ButtonX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblCuentaRegistros = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbTipoComprobante
            // 
            this.cmbTipoComprobante.DisplayMember = "Text";
            this.cmbTipoComprobante.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTipoComprobante.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoComprobante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoComprobante.FormattingEnabled = true;
            this.cmbTipoComprobante.ItemHeight = 17;
            this.cmbTipoComprobante.Location = new System.Drawing.Point(177, 35);
            this.cmbTipoComprobante.Name = "cmbTipoComprobante";
            this.cmbTipoComprobante.Size = new System.Drawing.Size(252, 23);
            this.cmbTipoComprobante.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTipoComprobante.TabIndex = 0;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX1.Location = new System.Drawing.Point(34, 35);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(99, 17);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Tipo documento:";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX2.Location = new System.Drawing.Point(34, 66);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(101, 17);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "Secuencia Inicio:";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX3.Location = new System.Drawing.Point(407, 69);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(88, 17);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "Secuencia Fin:";
            // 
            // txtInicio
            // 
            // 
            // 
            // 
            this.txtInicio.Border.Class = "TextBoxBorder";
            this.txtInicio.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInicio.Location = new System.Drawing.Point(177, 64);
            this.txtInicio.MaxLength = 9;
            this.txtInicio.Name = "txtInicio";
            this.txtInicio.PreventEnterBeep = true;
            this.txtInicio.Size = new System.Drawing.Size(157, 22);
            this.txtInicio.TabIndex = 1;
            this.txtInicio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInicio_KeyPress);
            // 
            // txtFin
            // 
            // 
            // 
            // 
            this.txtFin.Border.Class = "TextBoxBorder";
            this.txtFin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFin.Location = new System.Drawing.Point(554, 66);
            this.txtFin.MaxLength = 9;
            this.txtFin.Name = "txtFin";
            this.txtFin.PreventEnterBeep = true;
            this.txtFin.Size = new System.Drawing.Size(157, 22);
            this.txtFin.TabIndex = 2;
            this.txtFin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFin_KeyPress);
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX4.Location = new System.Drawing.Point(34, 94);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(130, 17);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "Fecha emisión desde:";
            // 
            // txtFechaInicial
            // 
            this.txtFechaInicial.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.txtFechaInicial.Border.Class = "TextBoxBorder";
            this.txtFechaInicial.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaInicial.Location = new System.Drawing.Point(177, 92);
            this.txtFechaInicial.Name = "txtFechaInicial";
            this.txtFechaInicial.PreventEnterBeep = true;
            this.txtFechaInicial.ReadOnly = true;
            this.txtFechaInicial.Size = new System.Drawing.Size(157, 22);
            this.txtFechaInicial.TabIndex = 7;
            // 
            // txtFechaFinal
            // 
            this.txtFechaFinal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.txtFechaFinal.Border.Class = "TextBoxBorder";
            this.txtFechaFinal.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaFinal.Location = new System.Drawing.Point(554, 94);
            this.txtFechaFinal.Name = "txtFechaFinal";
            this.txtFechaFinal.PreventEnterBeep = true;
            this.txtFechaFinal.ReadOnly = true;
            this.txtFechaFinal.Size = new System.Drawing.Size(157, 22);
            this.txtFechaFinal.TabIndex = 9;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX5.Location = new System.Drawing.Point(407, 96);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(126, 17);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "Fecha emisión hasta:";
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
            this.colTipoComprobante,
            this.colNumeroComprobante,
            this.colRazonSocial,
            this.colMail,
            this.colFechaEmision,
            this.colClaveAcceso,
            this.colAmbiente});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDatos.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDatos.Location = new System.Drawing.Point(12, 145);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersWidth = 20;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(1161, 263);
            this.dgvDatos.TabIndex = 9;
            // 
            // colMarca
            // 
            this.colMarca.Checked = true;
            this.colMarca.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.colMarca.CheckValue = "N";
            this.colMarca.Frozen = true;
            this.colMarca.HeaderText = "Marca";
            this.colMarca.Name = "colMarca";
            this.colMarca.Width = 50;
            // 
            // colIdFactura
            // 
            this.colIdFactura.Frozen = true;
            this.colIdFactura.HeaderText = "IdFactura";
            this.colIdFactura.Name = "colIdFactura";
            this.colIdFactura.Visible = false;
            // 
            // colTipoComprobante
            // 
            this.colTipoComprobante.Frozen = true;
            this.colTipoComprobante.HeaderText = "Comprobante";
            this.colTipoComprobante.Name = "colTipoComprobante";
            this.colTipoComprobante.Width = 130;
            // 
            // colNumeroComprobante
            // 
            this.colNumeroComprobante.Frozen = true;
            this.colNumeroComprobante.HeaderText = "No. Comprobante";
            this.colNumeroComprobante.Name = "colNumeroComprobante";
            this.colNumeroComprobante.Width = 120;
            // 
            // colRazonSocial
            // 
            this.colRazonSocial.Frozen = true;
            this.colRazonSocial.HeaderText = "Razón Social";
            this.colRazonSocial.Name = "colRazonSocial";
            this.colRazonSocial.Width = 200;
            // 
            // colMail
            // 
            this.colMail.Frozen = true;
            this.colMail.HeaderText = "Correo Electrónico";
            this.colMail.Name = "colMail";
            this.colMail.Width = 150;
            // 
            // colFechaEmision
            // 
            this.colFechaEmision.Frozen = true;
            this.colFechaEmision.HeaderText = "Fecha de Emisión";
            this.colFechaEmision.Name = "colFechaEmision";
            this.colFechaEmision.Width = 120;
            // 
            // colClaveAcceso
            // 
            this.colClaveAcceso.Frozen = true;
            this.colClaveAcceso.HeaderText = "Clave de Acceso";
            this.colClaveAcceso.Name = "colClaveAcceso";
            this.colClaveAcceso.Width = 250;
            // 
            // colAmbiente
            // 
            this.colAmbiente.Frozen = true;
            this.colAmbiente.HeaderText = "Ambiente";
            this.colAmbiente.Name = "colAmbiente";
            // 
            // btnConsultar
            // 
            this.btnConsultar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConsultar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Location = new System.Drawing.Point(24, 19);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(111, 30);
            this.btnConsultar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConsultar.TabIndex = 5;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnGenerar
            // 
            this.btnGenerar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGenerar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.Location = new System.Drawing.Point(24, 55);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(111, 30);
            this.btnGenerar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGenerar.TabIndex = 7;
            this.btnGenerar.Text = "Generar RIDE";
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImprimir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.Location = new System.Drawing.Point(141, 55);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(111, 30);
            this.btnImprimir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImprimir.TabIndex = 8;
            this.btnImprimir.Text = "Imprimir RIDE";
            // 
            // btnEnviar
            // 
            this.btnEnviar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEnviar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviar.Location = new System.Drawing.Point(141, 19);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(111, 30);
            this.btnEnviar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEnviar.TabIndex = 6;
            this.btnEnviar.Text = "Enviar Correos";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCerrar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnImprimir);
            this.groupBox1.Controls.Add(this.btnEnviar);
            this.groupBox1.Controls.Add(this.btnGenerar);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Location = new System.Drawing.Point(781, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 96);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCerrar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(258, 55);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(111, 30);
            this.btnCerrar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLimpiar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Location = new System.Drawing.Point(258, 19);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(111, 30);
            this.btnLimpiar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnDesde
            // 
            this.btnDesde.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDesde.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDesde.Location = new System.Drawing.Point(340, 92);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(32, 22);
            this.btnDesde.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDesde.TabIndex = 3;
            this.btnDesde.Text = "...";
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // btnHasta
            // 
            this.btnHasta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHasta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHasta.Location = new System.Drawing.Point(717, 94);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(32, 22);
            this.btnHasta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHasta.TabIndex = 4;
            this.btnHasta.Text = "...";
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelX6.Location = new System.Drawing.Point(12, 418);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(97, 17);
            this.labelX6.TabIndex = 16;
            this.labelX6.Text = "Total Registros:";
            // 
            // lblCuentaRegistros
            // 
            this.lblCuentaRegistros.AutoSize = true;
            // 
            // 
            // 
            this.lblCuentaRegistros.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCuentaRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuentaRegistros.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCuentaRegistros.Location = new System.Drawing.Point(125, 418);
            this.lblCuentaRegistros.Name = "lblCuentaRegistros";
            this.lblCuentaRegistros.Size = new System.Drawing.Size(150, 17);
            this.lblCuentaRegistros.TabIndex = 17;
            this.lblCuentaRegistros.Text = "0 Registros Encontrados";
            // 
            // frmGeneracionRide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1198, 455);
            this.Controls.Add(this.lblCuentaRegistros);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.btnHasta);
            this.Controls.Add(this.btnDesde);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.txtFechaFinal);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.txtFechaInicial);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.txtFin);
            this.Controls.Add(this.txtInicio);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.cmbTipoComprobante);
            this.Name = "frmGeneracionRide";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generación Masiva de RIDE";
            this.Load += new System.EventHandler(this.frmGeneracionRide_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmGeneracionRide_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTipoComprobante;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInicio;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFin;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFechaInicial;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFechaFinal;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDatos;
        private DevComponents.DotNetBar.ButtonX btnConsultar;
        private DevComponents.DotNetBar.ButtonX btnGenerar;
        private DevComponents.DotNetBar.ButtonX btnImprimir;
        private DevComponents.DotNetBar.ButtonX btnEnviar;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnDesde;
        private DevComponents.DotNetBar.ButtonX btnHasta;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblCuentaRegistros;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn colMarca;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaEmision;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClaveAcceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmbiente;
        private DevComponents.DotNetBar.ButtonX btnCerrar;
        private DevComponents.DotNetBar.ButtonX btnLimpiar;
    }
}