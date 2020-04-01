namespace Palatium.Formularios
{
    partial class FInformeDiarioVentas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExcel = new System.Windows.Forms.Button();
            this.Grb_listReInformeVentas = new System.Windows.Forms.GroupBox();
            this.txtServicio = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.txtValorIva = new System.Windows.Forms.TextBox();
            this.lblValorIva = new System.Windows.Forms.Label();
            this.txtValorNeto = new System.Windows.Forms.TextBox();
            this.lblValorNeto = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtValorBruto = new System.Windows.Forms.TextBox();
            this.lblValorBruto = new System.Windows.Forms.Label();
            this.dgvInformeVentas = new System.Windows.Forms.DataGridView();
            this.btnLimpiarInformeVentas = new System.Windows.Forms.Button();
            this.btnCerrarInformeVentas = new System.Windows.Forms.Button();
            this.Grb_DatoInformeVentas = new System.Windows.Forms.GroupBox();
            this.dbAyudaPersonas = new Controles.Auxiliares.DB_Ayuda();
            this.cmbVendedor = new MisControles.ComboDatos();
            this.cmbComprobante = new MisControles.ComboDatos();
            this.chkCodigoVendedor = new System.Windows.Forms.CheckBox();
            this.lblComprovante = new System.Windows.Forms.Label();
            this.chkCedulaRuc = new System.Windows.Forms.CheckBox();
            this.btnOKInformeVentas = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbMoneda = new MisControles.ComboDatos();
            this.chkAnuladas = new System.Windows.Forms.CheckBox();
            this.txtFechaFinal = new System.Windows.Forms.TextBox();
            this.txtFechaInicio = new System.Windows.Forms.TextBox();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.idFactura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseIVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseCero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valorNeto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valorTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idTipoComprovante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grb_listReInformeVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInformeVentas)).BeginInit();
            this.Grb_DatoInformeVentas.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(12, 490);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(70, 39);
            this.btnExcel.TabIndex = 27;
            this.btnExcel.Text = "Enviar a Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // Grb_listReInformeVentas
            // 
            this.Grb_listReInformeVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listReInformeVentas.Controls.Add(this.txtServicio);
            this.Grb_listReInformeVentas.Controls.Add(this.label1);
            this.Grb_listReInformeVentas.Controls.Add(this.txtValorTotal);
            this.Grb_listReInformeVentas.Controls.Add(this.lblValorTotal);
            this.Grb_listReInformeVentas.Controls.Add(this.txtValorIva);
            this.Grb_listReInformeVentas.Controls.Add(this.lblValorIva);
            this.Grb_listReInformeVentas.Controls.Add(this.txtValorNeto);
            this.Grb_listReInformeVentas.Controls.Add(this.lblValorNeto);
            this.Grb_listReInformeVentas.Controls.Add(this.txtDescuento);
            this.Grb_listReInformeVentas.Controls.Add(this.lblDescuento);
            this.Grb_listReInformeVentas.Controls.Add(this.txtValorBruto);
            this.Grb_listReInformeVentas.Controls.Add(this.lblValorBruto);
            this.Grb_listReInformeVentas.Controls.Add(this.dgvInformeVentas);
            this.Grb_listReInformeVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Grb_listReInformeVentas.Location = new System.Drawing.Point(12, 160);
            this.Grb_listReInformeVentas.Name = "Grb_listReInformeVentas";
            this.Grb_listReInformeVentas.Size = new System.Drawing.Size(1020, 324);
            this.Grb_listReInformeVentas.TabIndex = 5;
            this.Grb_listReInformeVentas.TabStop = false;
            this.Grb_listReInformeVentas.Text = "Lista de Registros";
            // 
            // txtServicio
            // 
            this.txtServicio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtServicio.Location = new System.Drawing.Point(824, 290);
            this.txtServicio.Name = "txtServicio";
            this.txtServicio.ReadOnly = true;
            this.txtServicio.Size = new System.Drawing.Size(75, 20);
            this.txtServicio.TabIndex = 49;
            this.txtServicio.Text = "0.00";
            this.txtServicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(821, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 48;
            this.label1.Text = "V. Servicio";
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtValorTotal.Location = new System.Drawing.Point(920, 290);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.ReadOnly = true;
            this.txtValorTotal.Size = new System.Drawing.Size(75, 20);
            this.txtValorTotal.TabIndex = 47;
            this.txtValorTotal.Text = "0.00";
            this.txtValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotal.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblValorTotal.Location = new System.Drawing.Point(917, 272);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(65, 15);
            this.lblValorTotal.TabIndex = 46;
            this.lblValorTotal.Text = "Valor Total";
            // 
            // txtValorIva
            // 
            this.txtValorIva.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtValorIva.Location = new System.Drawing.Point(728, 290);
            this.txtValorIva.Name = "txtValorIva";
            this.txtValorIva.ReadOnly = true;
            this.txtValorIva.Size = new System.Drawing.Size(75, 20);
            this.txtValorIva.TabIndex = 45;
            this.txtValorIva.Text = "0.00";
            this.txtValorIva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorIva
            // 
            this.lblValorIva.AutoSize = true;
            this.lblValorIva.BackColor = System.Drawing.Color.Transparent;
            this.lblValorIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorIva.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblValorIva.Location = new System.Drawing.Point(725, 272);
            this.lblValorIva.Name = "lblValorIva";
            this.lblValorIva.Size = new System.Drawing.Size(56, 15);
            this.lblValorIva.TabIndex = 44;
            this.lblValorIva.Text = "Valor Iva:";
            // 
            // txtValorNeto
            // 
            this.txtValorNeto.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtValorNeto.Location = new System.Drawing.Point(632, 290);
            this.txtValorNeto.Name = "txtValorNeto";
            this.txtValorNeto.ReadOnly = true;
            this.txtValorNeto.Size = new System.Drawing.Size(75, 20);
            this.txtValorNeto.TabIndex = 43;
            this.txtValorNeto.Text = "0.00";
            this.txtValorNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorNeto
            // 
            this.lblValorNeto.AutoSize = true;
            this.lblValorNeto.BackColor = System.Drawing.Color.Transparent;
            this.lblValorNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorNeto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblValorNeto.Location = new System.Drawing.Point(629, 272);
            this.lblValorNeto.Name = "lblValorNeto";
            this.lblValorNeto.Size = new System.Drawing.Size(67, 15);
            this.lblValorNeto.TabIndex = 42;
            this.lblValorNeto.Text = "Valor Neto:";
            // 
            // txtDescuento
            // 
            this.txtDescuento.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescuento.Location = new System.Drawing.Point(536, 290);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(75, 20);
            this.txtDescuento.TabIndex = 41;
            this.txtDescuento.Text = "0.00";
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.BackColor = System.Drawing.Color.Transparent;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescuento.Location = new System.Drawing.Point(533, 272);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(69, 15);
            this.lblDescuento.TabIndex = 40;
            this.lblDescuento.Text = "Descuento:";
            // 
            // txtValorBruto
            // 
            this.txtValorBruto.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtValorBruto.Location = new System.Drawing.Point(440, 290);
            this.txtValorBruto.Name = "txtValorBruto";
            this.txtValorBruto.ReadOnly = true;
            this.txtValorBruto.Size = new System.Drawing.Size(75, 20);
            this.txtValorBruto.TabIndex = 39;
            this.txtValorBruto.Text = "0.00";
            this.txtValorBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblValorBruto
            // 
            this.lblValorBruto.AutoSize = true;
            this.lblValorBruto.BackColor = System.Drawing.Color.Transparent;
            this.lblValorBruto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorBruto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblValorBruto.Location = new System.Drawing.Point(437, 272);
            this.lblValorBruto.Name = "lblValorBruto";
            this.lblValorBruto.Size = new System.Drawing.Size(70, 15);
            this.lblValorBruto.TabIndex = 38;
            this.lblValorBruto.Text = "Valor Bruto:";
            // 
            // dgvInformeVentas
            // 
            this.dgvInformeVentas.AllowUserToAddRows = false;
            this.dgvInformeVentas.AllowUserToDeleteRows = false;
            this.dgvInformeVentas.AllowUserToResizeRows = false;
            this.dgvInformeVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInformeVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idFactura,
            this.localidad,
            this.fecha,
            this.factura,
            this.cliente,
            this.baseIVA,
            this.baseCero,
            this.descuento,
            this.valorNeto,
            this.iva,
            this.servicio,
            this.valorTotal,
            this.idTipoComprovante});
            this.dgvInformeVentas.Location = new System.Drawing.Point(10, 19);
            this.dgvInformeVentas.Name = "dgvInformeVentas";
            this.dgvInformeVentas.ReadOnly = true;
            this.dgvInformeVentas.RowHeadersVisible = false;
            this.dgvInformeVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInformeVentas.Size = new System.Drawing.Size(1000, 245);
            this.dgvInformeVentas.TabIndex = 0;
            // 
            // btnLimpiarInformeVentas
            // 
            this.btnLimpiarInformeVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarInformeVentas.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarInformeVentas.Location = new System.Drawing.Point(885, 490);
            this.btnLimpiarInformeVentas.Name = "btnLimpiarInformeVentas";
            this.btnLimpiarInformeVentas.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiarInformeVentas.TabIndex = 2;
            this.btnLimpiarInformeVentas.Text = "Limpiar";
            this.btnLimpiarInformeVentas.UseVisualStyleBackColor = false;
            this.btnLimpiarInformeVentas.Click += new System.EventHandler(this.btnLimpiarInformeVentas_Click);
            // 
            // btnCerrarInformeVentas
            // 
            this.btnCerrarInformeVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarInformeVentas.ForeColor = System.Drawing.Color.White;
            this.btnCerrarInformeVentas.Location = new System.Drawing.Point(961, 490);
            this.btnCerrarInformeVentas.Name = "btnCerrarInformeVentas";
            this.btnCerrarInformeVentas.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarInformeVentas.TabIndex = 3;
            this.btnCerrarInformeVentas.Text = "Cerrar";
            this.btnCerrarInformeVentas.UseVisualStyleBackColor = false;
            this.btnCerrarInformeVentas.Click += new System.EventHandler(this.btnCerrarInformeVentas_Click);
            // 
            // Grb_DatoInformeVentas
            // 
            this.Grb_DatoInformeVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_DatoInformeVentas.Controls.Add(this.dbAyudaPersonas);
            this.Grb_DatoInformeVentas.Controls.Add(this.cmbVendedor);
            this.Grb_DatoInformeVentas.Controls.Add(this.cmbComprobante);
            this.Grb_DatoInformeVentas.Controls.Add(this.chkCodigoVendedor);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblComprovante);
            this.Grb_DatoInformeVentas.Controls.Add(this.chkCedulaRuc);
            this.Grb_DatoInformeVentas.Controls.Add(this.btnOKInformeVentas);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblCliente);
            this.Grb_DatoInformeVentas.Controls.Add(this.cmbMoneda);
            this.Grb_DatoInformeVentas.Controls.Add(this.chkAnuladas);
            this.Grb_DatoInformeVentas.Controls.Add(this.txtFechaFinal);
            this.Grb_DatoInformeVentas.Controls.Add(this.txtFechaInicio);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblFechaInicio);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblFechaFin);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblMoneda);
            this.Grb_DatoInformeVentas.Controls.Add(this.cmbLocalidad);
            this.Grb_DatoInformeVentas.Controls.Add(this.cmbEmpresa);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblLocalidad);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblEmpresa);
            this.Grb_DatoInformeVentas.Controls.Add(this.lblVendedor);
            this.Grb_DatoInformeVentas.Location = new System.Drawing.Point(12, 12);
            this.Grb_DatoInformeVentas.Name = "Grb_DatoInformeVentas";
            this.Grb_DatoInformeVentas.Size = new System.Drawing.Size(1020, 142);
            this.Grb_DatoInformeVentas.TabIndex = 3;
            this.Grb_DatoInformeVentas.TabStop = false;
            this.Grb_DatoInformeVentas.Text = "Datos del Registro";
            // 
            // dbAyudaPersonas
            // 
            this.dbAyudaPersonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbAyudaPersonas.iId = 0;
            this.dbAyudaPersonas.Location = new System.Drawing.Point(6, 106);
            this.dbAyudaPersonas.Name = "dbAyudaPersonas";
            this.dbAyudaPersonas.sCodigo = null;
            this.dbAyudaPersonas.Size = new System.Drawing.Size(459, 26);
            this.dbAyudaPersonas.sNombre = null;
            this.dbAyudaPersonas.TabIndex = 58;
            // 
            // cmbVendedor
            // 
            this.cmbVendedor.FormattingEnabled = true;
            this.cmbVendedor.Location = new System.Drawing.Point(430, 43);
            this.cmbVendedor.Name = "cmbVendedor";
            this.cmbVendedor.Size = new System.Drawing.Size(103, 21);
            this.cmbVendedor.TabIndex = 43;
            // 
            // cmbComprobante
            // 
            this.cmbComprobante.FormattingEnabled = true;
            this.cmbComprobante.Location = new System.Drawing.Point(509, 106);
            this.cmbComprobante.Name = "cmbComprobante";
            this.cmbComprobante.Size = new System.Drawing.Size(143, 21);
            this.cmbComprobante.TabIndex = 25;
            // 
            // chkCodigoVendedor
            // 
            this.chkCodigoVendedor.AutoSize = true;
            this.chkCodigoVendedor.Location = new System.Drawing.Point(815, 110);
            this.chkCodigoVendedor.Name = "chkCodigoVendedor";
            this.chkCodigoVendedor.Size = new System.Drawing.Size(144, 17);
            this.chkCodigoVendedor.TabIndex = 42;
            this.chkCodigoVendedor.Text = "Incluye código Vendedor";
            this.chkCodigoVendedor.UseVisualStyleBackColor = true;
            this.chkCodigoVendedor.Visible = false;
            // 
            // lblComprovante
            // 
            this.lblComprovante.AutoSize = true;
            this.lblComprovante.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComprovante.Location = new System.Drawing.Point(506, 81);
            this.lblComprovante.Name = "lblComprovante";
            this.lblComprovante.Size = new System.Drawing.Size(129, 15);
            this.lblComprovante.TabIndex = 24;
            this.lblComprovante.Text = "Tipo de Comprobante:";
            // 
            // chkCedulaRuc
            // 
            this.chkCedulaRuc.AutoSize = true;
            this.chkCedulaRuc.Location = new System.Drawing.Point(688, 110);
            this.chkCedulaRuc.Name = "chkCedulaRuc";
            this.chkCedulaRuc.Size = new System.Drawing.Size(124, 17);
            this.chkCedulaRuc.TabIndex = 41;
            this.chkCedulaRuc.Text = "Incluye Cedula/RUC";
            this.chkCedulaRuc.UseVisualStyleBackColor = true;
            this.chkCedulaRuc.Visible = false;
            // 
            // btnOKInformeVentas
            // 
            this.btnOKInformeVentas.BackColor = System.Drawing.Color.Blue;
            this.btnOKInformeVentas.ForeColor = System.Drawing.Color.White;
            this.btnOKInformeVentas.Location = new System.Drawing.Point(927, 25);
            this.btnOKInformeVentas.Name = "btnOKInformeVentas";
            this.btnOKInformeVentas.Size = new System.Drawing.Size(70, 39);
            this.btnOKInformeVentas.TabIndex = 0;
            this.btnOKInformeVentas.Text = "OK";
            this.btnOKInformeVentas.UseVisualStyleBackColor = false;
            this.btnOKInformeVentas.Click += new System.EventHandler(this.btnOKInformeVentas_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(2, 81);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 15);
            this.lblCliente.TabIndex = 20;
            this.lblCliente.Text = "Cliente:";
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(561, 43);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(103, 21);
            this.cmbMoneda.TabIndex = 40;
            // 
            // chkAnuladas
            // 
            this.chkAnuladas.AutoSize = true;
            this.chkAnuladas.Location = new System.Drawing.Point(688, 81);
            this.chkAnuladas.Name = "chkAnuladas";
            this.chkAnuladas.Size = new System.Drawing.Size(107, 17);
            this.chkAnuladas.TabIndex = 39;
            this.chkAnuladas.Text = "Incluye Anuladas";
            this.chkAnuladas.UseVisualStyleBackColor = true;
            // 
            // txtFechaFinal
            // 
            this.txtFechaFinal.Location = new System.Drawing.Point(795, 43);
            this.txtFechaFinal.Multiline = true;
            this.txtFechaFinal.Name = "txtFechaFinal";
            this.txtFechaFinal.Size = new System.Drawing.Size(84, 21);
            this.txtFechaFinal.TabIndex = 38;
            // 
            // txtFechaInicio
            // 
            this.txtFechaInicio.Location = new System.Drawing.Point(690, 43);
            this.txtFechaInicio.Multiline = true;
            this.txtFechaInicio.Name = "txtFechaInicio";
            this.txtFechaInicio.Size = new System.Drawing.Size(84, 21);
            this.txtFechaInicio.TabIndex = 37;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaInicio.Location = new System.Drawing.Point(687, 21);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(44, 15);
            this.lblFechaInicio.TabIndex = 36;
            this.lblFechaInicio.Text = "Fecha:";
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFechaFin.Location = new System.Drawing.Point(792, 21);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(44, 15);
            this.lblFechaFin.TabIndex = 35;
            this.lblFechaFin.Text = "Fecha:";
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.BackColor = System.Drawing.Color.Transparent;
            this.lblMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneda.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMoneda.Location = new System.Drawing.Point(558, 21);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(56, 15);
            this.lblMoneda.TabIndex = 33;
            this.lblMoneda.Text = "Moneda:";
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(226, 43);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(178, 21);
            this.cmbLocalidad.TabIndex = 14;
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Enabled = false;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(10, 43);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(174, 21);
            this.cmbEmpresa.TabIndex = 13;
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad.Location = new System.Drawing.Point(223, 22);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(61, 15);
            this.lblLocalidad.TabIndex = 12;
            this.lblLocalidad.Text = "Localidad";
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(7, 25);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(57, 15);
            this.lblEmpresa.TabIndex = 11;
            this.lblEmpresa.Text = "Empresa";
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.BackColor = System.Drawing.Color.Transparent;
            this.lblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendedor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblVendedor.Location = new System.Drawing.Point(427, 22);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(63, 15);
            this.lblVendedor.TabIndex = 7;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // idFactura
            // 
            this.idFactura.HeaderText = "Id Factura";
            this.idFactura.Name = "idFactura";
            this.idFactura.ReadOnly = true;
            this.idFactura.Visible = false;
            // 
            // localidad
            // 
            this.localidad.HeaderText = "Localidad";
            this.localidad.Name = "localidad";
            this.localidad.ReadOnly = true;
            // 
            // fecha
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // factura
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.factura.DefaultCellStyle = dataGridViewCellStyle2;
            this.factura.HeaderText = "No. Factura";
            this.factura.Name = "factura";
            this.factura.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // baseIVA
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.baseIVA.DefaultCellStyle = dataGridViewCellStyle3;
            this.baseIVA.HeaderText = "Base IVA";
            this.baseIVA.Name = "baseIVA";
            this.baseIVA.ReadOnly = true;
            this.baseIVA.Width = 80;
            // 
            // baseCero
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.baseCero.DefaultCellStyle = dataGridViewCellStyle4;
            this.baseCero.HeaderText = "Base 0";
            this.baseCero.Name = "baseCero";
            this.baseCero.ReadOnly = true;
            this.baseCero.Width = 80;
            // 
            // descuento
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.descuento.DefaultCellStyle = dataGridViewCellStyle5;
            this.descuento.HeaderText = "Descuento";
            this.descuento.Name = "descuento";
            this.descuento.ReadOnly = true;
            this.descuento.Width = 80;
            // 
            // valorNeto
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valorNeto.DefaultCellStyle = dataGridViewCellStyle6;
            this.valorNeto.HeaderText = "V. Neto";
            this.valorNeto.Name = "valorNeto";
            this.valorNeto.ReadOnly = true;
            this.valorNeto.Width = 80;
            // 
            // iva
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.iva.DefaultCellStyle = dataGridViewCellStyle7;
            this.iva.HeaderText = "I.V.A.";
            this.iva.Name = "iva";
            this.iva.ReadOnly = true;
            this.iva.Width = 80;
            // 
            // servicio
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.servicio.DefaultCellStyle = dataGridViewCellStyle8;
            this.servicio.HeaderText = "Servicio";
            this.servicio.Name = "servicio";
            this.servicio.ReadOnly = true;
            this.servicio.Width = 80;
            // 
            // valorTotal
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valorTotal.DefaultCellStyle = dataGridViewCellStyle9;
            this.valorTotal.HeaderText = "TOTAL";
            this.valorTotal.Name = "valorTotal";
            this.valorTotal.ReadOnly = true;
            this.valorTotal.Width = 80;
            // 
            // idTipoComprovante
            // 
            this.idTipoComprovante.HeaderText = "IdTipoComprobante";
            this.idTipoComprovante.Name = "idTipoComprovante";
            this.idTipoComprovante.ReadOnly = true;
            this.idTipoComprovante.Visible = false;
            // 
            // FInformeDiarioVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1043, 535);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.Grb_listReInformeVentas);
            this.Controls.Add(this.Grb_DatoInformeVentas);
            this.Controls.Add(this.btnLimpiarInformeVentas);
            this.Controls.Add(this.btnCerrarInformeVentas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformeDiarioVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Informe Diario de Ventas";
            this.Load += new System.EventHandler(this.FInformeDiarioVentas_Load);
            this.Grb_listReInformeVentas.ResumeLayout(false);
            this.Grb_listReInformeVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInformeVentas)).EndInit();
            this.Grb_DatoInformeVentas.ResumeLayout(false);
            this.Grb_DatoInformeVentas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReInformeVentas;
        private System.Windows.Forms.DataGridView dgvInformeVentas;
        private System.Windows.Forms.Button btnCerrarInformeVentas;
        private System.Windows.Forms.Button btnLimpiarInformeVentas;
        private System.Windows.Forms.Button btnOKInformeVentas;
        private System.Windows.Forms.GroupBox Grb_DatoInformeVentas;
        private MisControles.ComboDatos cmbComprobante;
        private System.Windows.Forms.Label lblComprovante;
        private System.Windows.Forms.Label lblCliente;
        private MisControles.ComboDatos cmbLocalidad;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.TextBox txtFechaFinal;
        private System.Windows.Forms.TextBox txtFechaInicio;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.TextBox txtValorIva;
        private System.Windows.Forms.Label lblValorIva;
        private System.Windows.Forms.TextBox txtValorNeto;
        private System.Windows.Forms.Label lblValorNeto;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.TextBox txtValorBruto;
        private System.Windows.Forms.Label lblValorBruto;
        private MisControles.ComboDatos cmbMoneda;
        private System.Windows.Forms.CheckBox chkCodigoVendedor;
        private System.Windows.Forms.CheckBox chkCedulaRuc;
        private System.Windows.Forms.CheckBox chkAnuladas;
        private MisControles.ComboDatos cmbVendedor;
        private System.Windows.Forms.Button btnExcel;
        private Controles.Auxiliares.DB_Ayuda dbAyudaPersonas;
        private System.Windows.Forms.TextBox txtServicio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idFactura;
        private System.Windows.Forms.DataGridViewTextBoxColumn localidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseIVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseCero;
        private System.Windows.Forms.DataGridViewTextBoxColumn descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorNeto;
        private System.Windows.Forms.DataGridViewTextBoxColumn iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn idTipoComprovante;
    }
}