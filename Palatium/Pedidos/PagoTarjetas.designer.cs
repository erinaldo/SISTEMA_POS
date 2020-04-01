namespace Palatium
{
    partial class PagoTarjetas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagoTarjetas));
            this.pnlFormasPagos = new System.Windows.Forms.Panel();
            this.dgv_DetallePago = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fpago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_total = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblPropina = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblCambio = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSaldo = new System.Windows.Forms.Label();
            this.btnRemoverPropina = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAbono = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRemoverIVA = new System.Windows.Forms.Button();
            this.btnCrearCliente = new System.Windows.Forms.Button();
            this.btnDividir = new System.Windows.Forms.Button();
            this.BtnListo = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnRemoverPago = new System.Windows.Forms.Button();
            this.btnConfirmarPago = new System.Windows.Forms.Button();
            this.btnPagoCompleto = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.btnRecargoTarjeta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DetallePago)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFormasPagos
            // 
            this.pnlFormasPagos.AutoScroll = true;
            this.pnlFormasPagos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFormasPagos.Location = new System.Drawing.Point(320, 21);
            this.pnlFormasPagos.Margin = new System.Windows.Forms.Padding(2);
            this.pnlFormasPagos.Name = "pnlFormasPagos";
            this.pnlFormasPagos.Size = new System.Drawing.Size(687, 401);
            this.pnlFormasPagos.TabIndex = 11;
            // 
            // dgv_DetallePago
            // 
            this.dgv_DetallePago.AllowUserToAddRows = false;
            this.dgv_DetallePago.AllowUserToDeleteRows = false;
            this.dgv_DetallePago.AllowUserToResizeRows = false;
            this.dgv_DetallePago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DetallePago.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.fpago,
            this.valor});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DetallePago.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_DetallePago.Location = new System.Drawing.Point(347, 455);
            this.dgv_DetallePago.MultiSelect = false;
            this.dgv_DetallePago.Name = "dgv_DetallePago";
            this.dgv_DetallePago.ReadOnly = true;
            this.dgv_DetallePago.RowTemplate.Height = 28;
            this.dgv_DetallePago.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_DetallePago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_DetallePago.Size = new System.Drawing.Size(397, 175);
            this.dgv_DetallePago.TabIndex = 14;
            // 
            // ID
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID.DefaultCellStyle = dataGridViewCellStyle7;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // fpago
            // 
            this.fpago.HeaderText = "FORMA DE PAGO";
            this.fpago.Name = "fpago";
            this.fpago.ReadOnly = true;
            this.fpago.Width = 250;
            // 
            // valor
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle8;
            this.valor.HeaderText = "VALOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 90;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(221, 24);
            this.label7.TabIndex = 89;
            this.label7.Text = "TOTAL DE LA ORDEN";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Teal;
            this.groupBox1.Controls.Add(this.lbl_total);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(13, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 108);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            // 
            // lbl_total
            // 
            this.lbl_total.AutoSize = true;
            this.lbl_total.BackColor = System.Drawing.Color.Transparent;
            this.lbl_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_total.ForeColor = System.Drawing.Color.White;
            this.lbl_total.Location = new System.Drawing.Point(57, 57);
            this.lbl_total.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_total.Name = "lbl_total";
            this.lbl_total.Size = new System.Drawing.Size(30, 31);
            this.lbl_total.TabIndex = 96;
            this.lbl_total.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(23, 57);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(30, 31);
            this.label21.TabIndex = 95;
            this.label21.Text = "$";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(25, 184);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 24);
            this.label9.TabIndex = 98;
            this.label9.Text = "CAMBIO";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(24, 266);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 24);
            this.label8.TabIndex = 97;
            this.label8.Text = "PROPINA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 24);
            this.label2.TabIndex = 96;
            this.label2.Text = "SALDO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(24, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 24);
            this.label6.TabIndex = 95;
            this.label6.Text = "ABONO";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Teal;
            this.groupBox2.Controls.Add(this.lblPropina);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lblCambio);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblSaldo);
            this.groupBox2.Controls.Add(this.btnRemoverPropina);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lblAbono);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(13, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 345);
            this.groupBox2.TabIndex = 99;
            this.groupBox2.TabStop = false;
            // 
            // lblPropina
            // 
            this.lblPropina.AutoSize = true;
            this.lblPropina.BackColor = System.Drawing.Color.Transparent;
            this.lblPropina.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPropina.ForeColor = System.Drawing.Color.White;
            this.lblPropina.Location = new System.Drawing.Point(56, 300);
            this.lblPropina.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPropina.Name = "lblPropina";
            this.lblPropina.Size = new System.Drawing.Size(71, 31);
            this.lblPropina.TabIndex = 104;
            this.lblPropina.Text = "0.00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(22, 300);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 31);
            this.label16.TabIndex = 103;
            this.label16.Text = "$";
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.BackColor = System.Drawing.Color.Transparent;
            this.lblCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambio.ForeColor = System.Drawing.Color.White;
            this.lblCambio.Location = new System.Drawing.Point(57, 218);
            this.lblCambio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(71, 31);
            this.lblCambio.TabIndex = 102;
            this.lblCambio.Text = "0.00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(23, 218);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(30, 31);
            this.label14.TabIndex = 101;
            this.label14.Text = "$";
            // 
            // lblSaldo
            // 
            this.lblSaldo.AutoSize = true;
            this.lblSaldo.BackColor = System.Drawing.Color.Transparent;
            this.lblSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldo.ForeColor = System.Drawing.Color.White;
            this.lblSaldo.Location = new System.Drawing.Point(56, 138);
            this.lblSaldo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSaldo.Name = "lblSaldo";
            this.lblSaldo.Size = new System.Drawing.Size(71, 31);
            this.lblSaldo.TabIndex = 100;
            this.lblSaldo.Text = "0.00";
            // 
            // btnRemoverPropina
            // 
            this.btnRemoverPropina.Image = global::Palatium.Properties.Resources.eliminar_img;
            this.btnRemoverPropina.Location = new System.Drawing.Point(140, 267);
            this.btnRemoverPropina.Name = "btnRemoverPropina";
            this.btnRemoverPropina.Size = new System.Drawing.Size(35, 26);
            this.btnRemoverPropina.TabIndex = 24;
            this.btnRemoverPropina.Text = "0.00";
            this.btnRemoverPropina.UseVisualStyleBackColor = true;
            this.btnRemoverPropina.Click += new System.EventHandler(this.btnRemoverPropina_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(22, 138);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(30, 31);
            this.label12.TabIndex = 99;
            this.label12.Text = "$";
            // 
            // lblAbono
            // 
            this.lblAbono.AutoSize = true;
            this.lblAbono.BackColor = System.Drawing.Color.Transparent;
            this.lblAbono.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAbono.ForeColor = System.Drawing.Color.White;
            this.lblAbono.Location = new System.Drawing.Point(56, 50);
            this.lblAbono.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAbono.Name = "lblAbono";
            this.lblAbono.Size = new System.Drawing.Size(71, 31);
            this.lblAbono.TabIndex = 98;
            this.lblAbono.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.14286F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(22, 50);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 31);
            this.label10.TabIndex = 97;
            this.label10.Text = "$";
            // 
            // btnRemoverIVA
            // 
            this.btnRemoverIVA.BackColor = System.Drawing.Color.SpringGreen;
            this.btnRemoverIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverIVA.ForeColor = System.Drawing.Color.Red;
            this.btnRemoverIVA.Image = global::Palatium.Properties.Resources.remover_iva;
            this.btnRemoverIVA.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverIVA.Location = new System.Drawing.Point(13, 467);
            this.btnRemoverIVA.Name = "btnRemoverIVA";
            this.btnRemoverIVA.Size = new System.Drawing.Size(241, 81);
            this.btnRemoverIVA.TabIndex = 101;
            this.btnRemoverIVA.Text = "REMOVER IVA";
            this.btnRemoverIVA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoverIVA.UseVisualStyleBackColor = false;
            this.btnRemoverIVA.Click += new System.EventHandler(this.btnRemoverIVA_Click);
            // 
            // btnCrearCliente
            // 
            this.btnCrearCliente.BackColor = System.Drawing.Color.Transparent;
            this.btnCrearCliente.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCrearCliente.BackgroundImage")));
            this.btnCrearCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCrearCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearCliente.FlatAppearance.BorderSize = 0;
            this.btnCrearCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearCliente.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCrearCliente.Image = global::Palatium.Properties.Resources.clientes_1_png;
            this.btnCrearCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCrearCliente.Location = new System.Drawing.Point(1035, 277);
            this.btnCrearCliente.Name = "btnCrearCliente";
            this.btnCrearCliente.Size = new System.Drawing.Size(172, 106);
            this.btnCrearCliente.TabIndex = 100;
            this.btnCrearCliente.Text = "Crear\r\nCliente";
            this.btnCrearCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCrearCliente.UseVisualStyleBackColor = false;
            this.btnCrearCliente.Click += new System.EventHandler(this.btnCrearCliente_Click);
            this.btnCrearCliente.MouseEnter += new System.EventHandler(this.btnCrearCliente_MouseEnter);
            this.btnCrearCliente.MouseLeave += new System.EventHandler(this.btnCrearCliente_MouseLeave);
            // 
            // btnDividir
            // 
            this.btnDividir.BackColor = System.Drawing.Color.Transparent;
            this.btnDividir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDividir.BackgroundImage")));
            this.btnDividir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDividir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDividir.FlatAppearance.BorderSize = 0;
            this.btnDividir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDividir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDividir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDividir.Image = global::Palatium.Properties.Resources.Clients_icon;
            this.btnDividir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDividir.Location = new System.Drawing.Point(776, 455);
            this.btnDividir.Name = "btnDividir";
            this.btnDividir.Size = new System.Drawing.Size(177, 85);
            this.btnDividir.TabIndex = 26;
            this.btnDividir.Text = "Dividir Precio";
            this.btnDividir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDividir.UseVisualStyleBackColor = false;
            this.btnDividir.Visible = false;
            this.btnDividir.Click += new System.EventHandler(this.btnDividir_Click);
            this.btnDividir.MouseEnter += new System.EventHandler(this.btnDividir_MouseEnter);
            this.btnDividir.MouseLeave += new System.EventHandler(this.btnDividir_MouseLeave);
            // 
            // BtnListo
            // 
            this.BtnListo.BackColor = System.Drawing.Color.Transparent;
            this.BtnListo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnListo.BackgroundImage")));
            this.BtnListo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnListo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnListo.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnListo.FlatAppearance.BorderSize = 0;
            this.BtnListo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnListo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnListo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.BtnListo.Image = global::Palatium.Properties.Resources.ok_png;
            this.BtnListo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnListo.Location = new System.Drawing.Point(1035, 399);
            this.BtnListo.Name = "BtnListo";
            this.BtnListo.Size = new System.Drawing.Size(172, 106);
            this.BtnListo.TabIndex = 25;
            this.BtnListo.Text = "Solo\r\ngrabar\r\nformas\r\nde pago\r";
            this.BtnListo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnListo.UseVisualStyleBackColor = false;
            this.BtnListo.Click += new System.EventHandler(this.BtnListo_Click);
            this.BtnListo.MouseEnter += new System.EventHandler(this.BtnListo_MouseEnter);
            this.BtnListo.MouseLeave += new System.EventHandler(this.BtnListo_MouseLeave);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Transparent;
            this.btnImprimir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImprimir.BackgroundImage")));
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnImprimir.Image = global::Palatium.Properties.Resources.imprimir_precuenta;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(1035, 33);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(172, 106);
            this.btnImprimir.TabIndex = 21;
            this.btnImprimir.Text = "Imprimir\r\nPre\r\nCuenta";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            this.btnImprimir.MouseEnter += new System.EventHandler(this.btnImprimir_MouseEnter);
            this.btnImprimir.MouseLeave += new System.EventHandler(this.btnImprimir_MouseLeave);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Transparent;
            this.btnSalir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSalir.BackgroundImage")));
            this.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Image = global::Palatium.Properties.Resources.salir_png;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(1035, 521);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(172, 106);
            this.btnSalir.TabIndex = 17;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnSalir_MouseEnter);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            // 
            // btnRemoverPago
            // 
            this.btnRemoverPago.BackColor = System.Drawing.Color.Transparent;
            this.btnRemoverPago.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemoverPago.BackgroundImage")));
            this.btnRemoverPago.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemoverPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoverPago.FlatAppearance.BorderSize = 0;
            this.btnRemoverPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverPago.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRemoverPago.Image = global::Palatium.Properties.Resources.eliminar_pago_png;
            this.btnRemoverPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemoverPago.Location = new System.Drawing.Point(776, 546);
            this.btnRemoverPago.Name = "btnRemoverPago";
            this.btnRemoverPago.Size = new System.Drawing.Size(177, 84);
            this.btnRemoverPago.TabIndex = 16;
            this.btnRemoverPago.Text = "Remover Pago";
            this.btnRemoverPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemoverPago.UseVisualStyleBackColor = false;
            this.btnRemoverPago.Click += new System.EventHandler(this.btnRemoverPago_Click);
            this.btnRemoverPago.MouseEnter += new System.EventHandler(this.btnRemoverPago_MouseEnter);
            this.btnRemoverPago.MouseLeave += new System.EventHandler(this.btnRemoverPago_MouseLeave);
            // 
            // btnConfirmarPago
            // 
            this.btnConfirmarPago.BackColor = System.Drawing.Color.Transparent;
            this.btnConfirmarPago.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirmarPago.BackgroundImage")));
            this.btnConfirmarPago.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfirmarPago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirmarPago.FlatAppearance.BorderSize = 0;
            this.btnConfirmarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarPago.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConfirmarPago.Image = global::Palatium.Properties.Resources.facturar_png1;
            this.btnConfirmarPago.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirmarPago.Location = new System.Drawing.Point(1035, 155);
            this.btnConfirmarPago.Name = "btnConfirmarPago";
            this.btnConfirmarPago.Size = new System.Drawing.Size(172, 106);
            this.btnConfirmarPago.TabIndex = 3;
            this.btnConfirmarPago.Text = "Crear\r\nFactura/\r\nCerrar\r\nComanda";
            this.btnConfirmarPago.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfirmarPago.UseVisualStyleBackColor = false;
            this.btnConfirmarPago.Click += new System.EventHandler(this.btnConfirmarPago_Click);
            this.btnConfirmarPago.MouseEnter += new System.EventHandler(this.btnConfirmarPago_MouseEnter);
            this.btnConfirmarPago.MouseLeave += new System.EventHandler(this.btnConfirmarPago_MouseLeave);
            // 
            // btnPagoCompleto
            // 
            this.btnPagoCompleto.BackColor = System.Drawing.Color.Transparent;
            this.btnPagoCompleto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPagoCompleto.BackgroundImage")));
            this.btnPagoCompleto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPagoCompleto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagoCompleto.FlatAppearance.BorderSize = 0;
            this.btnPagoCompleto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagoCompleto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPagoCompleto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPagoCompleto.Image = global::Palatium.Properties.Resources.recuperacion_cartera_menu;
            this.btnPagoCompleto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPagoCompleto.Location = new System.Drawing.Point(776, 455);
            this.btnPagoCompleto.Name = "btnPagoCompleto";
            this.btnPagoCompleto.Size = new System.Drawing.Size(177, 85);
            this.btnPagoCompleto.TabIndex = 102;
            this.btnPagoCompleto.Text = "Pago completo\r\nen efectivo";
            this.btnPagoCompleto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttMensaje.SetToolTip(this.btnPagoCompleto, "Clic aquí únicamente para cobro de almuerzos en efectivo");
            this.btnPagoCompleto.UseVisualStyleBackColor = false;
            this.btnPagoCompleto.Click += new System.EventHandler(this.btnPagoCompleto_Click);
            this.btnPagoCompleto.MouseEnter += new System.EventHandler(this.btnPagoCompleto_MouseEnter);
            this.btnPagoCompleto.MouseLeave += new System.EventHandler(this.btnPagoCompleto_MouseLeave);
            // 
            // btnRecargoTarjeta
            // 
            this.btnRecargoTarjeta.AccessibleDescription = "RECARGO TARJETAS";
            this.btnRecargoTarjeta.BackColor = System.Drawing.Color.SpringGreen;
            this.btnRecargoTarjeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecargoTarjeta.ForeColor = System.Drawing.Color.Red;
            this.btnRecargoTarjeta.Image = global::Palatium.Properties.Resources.remover_iva;
            this.btnRecargoTarjeta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecargoTarjeta.Location = new System.Drawing.Point(14, 554);
            this.btnRecargoTarjeta.Name = "btnRecargoTarjeta";
            this.btnRecargoTarjeta.Size = new System.Drawing.Size(241, 81);
            this.btnRecargoTarjeta.TabIndex = 103;
            this.btnRecargoTarjeta.Text = "RECARGO\r\nTARJETAS";
            this.btnRecargoTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRecargoTarjeta.UseVisualStyleBackColor = false;
            this.btnRecargoTarjeta.Click += new System.EventHandler(this.btnRecargoTarjeta_Click);
            // 
            // PagoTarjetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1219, 645);
            this.Controls.Add(this.btnRecargoTarjeta);
            this.Controls.Add(this.btnPagoCompleto);
            this.Controls.Add(this.btnRemoverIVA);
            this.Controls.Add(this.btnCrearCliente);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDividir);
            this.Controls.Add(this.BtnListo);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnRemoverPago);
            this.Controls.Add(this.dgv_DetallePago);
            this.Controls.Add(this.btnConfirmarPago);
            this.Controls.Add(this.pnlFormasPagos);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PagoTarjetas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formas de Cobro";
            this.Load += new System.EventHandler(this.PagoTarjetas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PagoTarjetas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DetallePago)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Panel pnlFormasPagos;
        private System.Windows.Forms.Button btnRemoverPago;
        public System.Windows.Forms.Button btnConfirmarPago;
        public System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        public System.Windows.Forms.DataGridView dgv_DetallePago;
        private System.Windows.Forms.Button btnRemoverPropina;
        public System.Windows.Forms.Button BtnListo;
        private System.Windows.Forms.Button btnDividir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label lblPropina;
        public System.Windows.Forms.Label lblCambio;
        public System.Windows.Forms.Label lblSaldo;
        public System.Windows.Forms.Label lblAbono;
        public System.Windows.Forms.Label lbl_total;
        public System.Windows.Forms.Button btnCrearCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn fpago;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.Button btnRemoverIVA;
        private System.Windows.Forms.Button btnPagoCompleto;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnRecargoTarjeta;
    }
}