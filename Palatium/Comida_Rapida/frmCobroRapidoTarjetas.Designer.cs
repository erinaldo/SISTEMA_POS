namespace Palatium.Comida_Rapida
{
    partial class frmCobroRapidoTarjetas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.pnlFormasCobros = new System.Windows.Forms.Panel();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoverPago = new System.Windows.Forms.Button();
            this.dgvPagos = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conciliacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_operador_tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_tipo_tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bandera_insertar_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbDebito = new System.Windows.Forms.RadioButton();
            this.rdbCredito = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdbMedianet = new System.Windows.Forms.RadioButton();
            this.rdbDatafast = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPropina = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumeroLote = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSiguiente.Image = global::Palatium.Properties.Resources.derecha;
            this.btnSiguiente.Location = new System.Drawing.Point(100, 234);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(83, 71);
            this.btnSiguiente.TabIndex = 136;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Image = global::Palatium.Properties.Resources.izquierda;
            this.btnAnterior.Location = new System.Drawing.Point(13, 232);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(85, 71);
            this.btnAnterior.TabIndex = 135;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // pnlFormasCobros
            // 
            this.pnlFormasCobros.Location = new System.Drawing.Point(12, 12);
            this.pnlFormasCobros.Name = "pnlFormasCobros";
            this.pnlFormasCobros.Size = new System.Drawing.Size(307, 214);
            this.pnlFormasCobros.TabIndex = 134;
            // 
            // btnFacturar
            // 
            this.btnFacturar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnFacturar.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturar.Location = new System.Drawing.Point(325, 232);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(147, 71);
            this.btnFacturar.TabIndex = 139;
            this.btnFacturar.Text = "COBRAR";
            this.btnFacturar.UseVisualStyleBackColor = false;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.OrangeRed;
            this.btnSalir.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Location = new System.Drawing.Point(472, 232);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(147, 71);
            this.btnSalir.TabIndex = 140;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(325, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 71);
            this.panel1.TabIndex = 142;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Lime;
            this.lblTotal.Location = new System.Drawing.Point(175, 18);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(107, 31);
            this.lblTotal.TabIndex = 25;
            this.lblTotal.Text = "$ 0.00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "TOTAL:";
            // 
            // btnRemoverPago
            // 
            this.btnRemoverPago.AccessibleDescription = "";
            this.btnRemoverPago.BackColor = System.Drawing.Color.Yellow;
            this.btnRemoverPago.Font = new System.Drawing.Font("Maiandra GD", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoverPago.Location = new System.Drawing.Point(189, 232);
            this.btnRemoverPago.Name = "btnRemoverPago";
            this.btnRemoverPago.Size = new System.Drawing.Size(130, 71);
            this.btnRemoverPago.TabIndex = 141;
            this.btnRemoverPago.Text = "REMOVER EL PAGO";
            this.btnRemoverPago.UseVisualStyleBackColor = false;
            this.btnRemoverPago.Click += new System.EventHandler(this.btnRemoverPago_Click);
            // 
            // dgvPagos
            // 
            this.dgvPagos.AllowUserToAddRows = false;
            this.dgvPagos.AllowUserToDeleteRows = false;
            this.dgvPagos.AllowUserToResizeColumns = false;
            this.dgvPagos.AllowUserToResizeRows = false;
            this.dgvPagos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPagos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPagos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPagos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPagos.ColumnHeadersHeight = 30;
            this.dgvPagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPagos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.conciliacion,
            this.id_operador_tarjeta,
            this.id_tipo_tarjeta,
            this.numero_lote,
            this.bandera_insertar_lote});
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagos.DefaultCellStyle = dataGridViewCellStyle29;
            this.dgvPagos.EnableHeadersVisualStyles = false;
            this.dgvPagos.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvPagos.Location = new System.Drawing.Point(325, 109);
            this.dgvPagos.MultiSelect = false;
            this.dgvPagos.Name = "dgvPagos";
            this.dgvPagos.ReadOnly = true;
            this.dgvPagos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPagos.RowHeadersVisible = false;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPagos.RowsDefaultCellStyle = dataGridViewCellStyle30;
            this.dgvPagos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPagos.Size = new System.Drawing.Size(294, 117);
            this.dgvPagos.TabIndex = 143;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle26;
            this.dataGridViewTextBoxColumn1.FillWeight = 60.9137F;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 53;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle27;
            this.dataGridViewTextBoxColumn2.FillWeight = 168.8291F;
            this.dataGridViewTextBoxColumn2.HeaderText = "FORMA DE PAGO";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle28;
            this.dataGridViewTextBoxColumn3.FillWeight = 70.25717F;
            this.dataGridViewTextBoxColumn3.HeaderText = "VALOR";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 62;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ID_SRI";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // conciliacion
            // 
            this.conciliacion.HeaderText = "CONCILIACION";
            this.conciliacion.Name = "conciliacion";
            this.conciliacion.ReadOnly = true;
            this.conciliacion.Visible = false;
            // 
            // id_operador_tarjeta
            // 
            this.id_operador_tarjeta.HeaderText = "ID_OPERADOR_TARJETA";
            this.id_operador_tarjeta.Name = "id_operador_tarjeta";
            this.id_operador_tarjeta.ReadOnly = true;
            this.id_operador_tarjeta.Visible = false;
            // 
            // id_tipo_tarjeta
            // 
            this.id_tipo_tarjeta.HeaderText = "ID_TIPO_TARJETA";
            this.id_tipo_tarjeta.Name = "id_tipo_tarjeta";
            this.id_tipo_tarjeta.ReadOnly = true;
            this.id_tipo_tarjeta.Visible = false;
            // 
            // numero_lote
            // 
            this.numero_lote.HeaderText = "NUMERO_LOTE";
            this.numero_lote.Name = "numero_lote";
            this.numero_lote.ReadOnly = true;
            this.numero_lote.Visible = false;
            // 
            // bandera_insertar_lote
            // 
            this.bandera_insertar_lote.HeaderText = "BANDERA INSERTAR LOTE";
            this.bandera_insertar_lote.Name = "bandera_insertar_lote";
            this.bandera_insertar_lote.ReadOnly = true;
            this.bandera_insertar_lote.Visible = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Teal;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.rdbDebito);
            this.panel4.Controls.Add(this.rdbCredito);
            this.panel4.Location = new System.Drawing.Point(13, 353);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(606, 30);
            this.panel4.TabIndex = 149;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(11, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 18);
            this.label2.TabIndex = 86;
            this.label2.Text = "TIPO DE TARJETA";
            // 
            // rdbDebito
            // 
            this.rdbDebito.AutoSize = true;
            this.rdbDebito.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDebito.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbDebito.Location = new System.Drawing.Point(492, 4);
            this.rdbDebito.Name = "rdbDebito";
            this.rdbDebito.Size = new System.Drawing.Size(86, 22);
            this.rdbDebito.TabIndex = 85;
            this.rdbDebito.Text = "DÉBITO";
            this.rdbDebito.UseVisualStyleBackColor = true;
            this.rdbDebito.CheckedChanged += new System.EventHandler(this.rdbDebito_CheckedChanged);
            // 
            // rdbCredito
            // 
            this.rdbCredito.AutoSize = true;
            this.rdbCredito.Checked = true;
            this.rdbCredito.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbCredito.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbCredito.Location = new System.Drawing.Point(367, 4);
            this.rdbCredito.Name = "rdbCredito";
            this.rdbCredito.Size = new System.Drawing.Size(98, 22);
            this.rdbCredito.TabIndex = 84;
            this.rdbCredito.TabStop = true;
            this.rdbCredito.Text = "CRÉDITO";
            this.rdbCredito.UseVisualStyleBackColor = true;
            this.rdbCredito.CheckedChanged += new System.EventHandler(this.rdbCredito_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Teal;
            this.panel3.Controls.Add(this.rdbMedianet);
            this.panel3.Controls.Add(this.rdbDatafast);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(13, 320);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(606, 30);
            this.panel3.TabIndex = 148;
            // 
            // rdbMedianet
            // 
            this.rdbMedianet.AutoSize = true;
            this.rdbMedianet.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbMedianet.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbMedianet.Location = new System.Drawing.Point(492, 5);
            this.rdbMedianet.Name = "rdbMedianet";
            this.rdbMedianet.Size = new System.Drawing.Size(110, 22);
            this.rdbMedianet.TabIndex = 82;
            this.rdbMedianet.Text = "MEDIANET";
            this.rdbMedianet.UseVisualStyleBackColor = true;
            this.rdbMedianet.CheckedChanged += new System.EventHandler(this.rdbMedianet_CheckedChanged);
            // 
            // rdbDatafast
            // 
            this.rdbDatafast.AutoSize = true;
            this.rdbDatafast.Checked = true;
            this.rdbDatafast.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDatafast.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rdbDatafast.Location = new System.Drawing.Point(367, 5);
            this.rdbDatafast.Name = "rdbDatafast";
            this.rdbDatafast.Size = new System.Drawing.Size(107, 22);
            this.rdbDatafast.TabIndex = 81;
            this.rdbDatafast.TabStop = true;
            this.rdbDatafast.Text = "DATAFAST";
            this.rdbDatafast.UseVisualStyleBackColor = true;
            this.rdbDatafast.CheckedChanged += new System.EventHandler(this.rdbDatafast_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 18);
            this.label1.TabIndex = 80;
            this.label1.Text = "OPERADOR DE TARJETA";
            // 
            // txtPropina
            // 
            this.txtPropina.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtPropina.Location = new System.Drawing.Point(539, 386);
            this.txtPropina.Name = "txtPropina";
            this.txtPropina.Size = new System.Drawing.Size(79, 25);
            this.txtPropina.TabIndex = 147;
            this.txtPropina.Text = "0";
            this.txtPropina.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(447, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 18);
            this.label4.TabIndex = 146;
            this.label4.Text = "PROPINA:";
            // 
            // txtNumeroLote
            // 
            this.txtNumeroLote.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.txtNumeroLote.Location = new System.Drawing.Point(112, 389);
            this.txtNumeroLote.Name = "txtNumeroLote";
            this.txtNumeroLote.Size = new System.Drawing.Size(139, 25);
            this.txtNumeroLote.TabIndex = 145;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(24, 392);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 18);
            this.label5.TabIndex = 144;
            this.label5.Text = "No. LOTE:";
            // 
            // frmCobroRapidoTarjetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(632, 420);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.txtPropina);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNumeroLote);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvPagos);
            this.Controls.Add(this.btnRemoverPago);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnFacturar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.pnlFormasCobros);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCobroRapidoTarjetas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobro con Tarjetas";
            this.Load += new System.EventHandler(this.frmCobroRapidoTarjetas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCobroRapidoTarjetas_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagos)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Panel pnlFormasCobros;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRemoverPago;
        public System.Windows.Forms.DataGridView dgvPagos;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn conciliacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_operador_tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_tipo_tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero_lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn bandera_insertar_lote;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbDebito;
        private System.Windows.Forms.RadioButton rdbCredito;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdbMedianet;
        private System.Windows.Forms.RadioButton rdbDatafast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPropina;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumeroLote;
        private System.Windows.Forms.Label label5;
    }
}