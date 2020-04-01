namespace Palatium.Bodega
{
    partial class frmReporteDeIngresosRangoDeFechas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grupoSeleccion = new System.Windows.Forms.GroupBox();
            this.txtHasta = new System.Windows.Forms.MaskedTextBox();
            this.txtDesde = new System.Windows.Forms.MaskedTextBox();
            this.btnHasta = new System.Windows.Forms.Button();
            this.btnDesde = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvInforme = new System.Windows.Forms.DataGridView();
            this.idMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.medida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vUnitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExportarAExcel = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grupoSeleccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).BeginInit();
            this.SuspendLayout();
            // 
            // grupoSeleccion
            // 
            this.grupoSeleccion.Controls.Add(this.btnExportarAExcel);
            this.grupoSeleccion.Controls.Add(this.btnLimpiar);
            this.grupoSeleccion.Controls.Add(this.btnAceptar);
            this.grupoSeleccion.Controls.Add(this.txtHasta);
            this.grupoSeleccion.Controls.Add(this.txtDesde);
            this.grupoSeleccion.Controls.Add(this.btnHasta);
            this.grupoSeleccion.Controls.Add(this.btnDesde);
            this.grupoSeleccion.Controls.Add(this.label2);
            this.grupoSeleccion.Controls.Add(this.label1);
            this.grupoSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoSeleccion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grupoSeleccion.Location = new System.Drawing.Point(12, 12);
            this.grupoSeleccion.Name = "grupoSeleccion";
            this.grupoSeleccion.Size = new System.Drawing.Size(900, 85);
            this.grupoSeleccion.TabIndex = 4;
            this.grupoSeleccion.TabStop = false;
            // 
            // txtHasta
            // 
            this.txtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHasta.Location = new System.Drawing.Point(267, 43);
            this.txtHasta.Mask = "00/00/0000";
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(182, 21);
            this.txtHasta.TabIndex = 19;
            this.txtHasta.ValidatingType = typeof(System.DateTime);
            // 
            // txtDesde
            // 
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(22, 43);
            this.txtDesde.Mask = "00/00/0000";
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(182, 21);
            this.txtDesde.TabIndex = 18;
            this.txtDesde.ValidatingType = typeof(System.DateTime);
            // 
            // btnHasta
            // 
            this.btnHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHasta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHasta.Location = new System.Drawing.Point(455, 39);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(38, 29);
            this.btnHasta.TabIndex = 5;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Visible = false;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDesde.Location = new System.Drawing.Point(223, 39);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(38, 29);
            this.btnDesde.TabIndex = 4;
            this.btnDesde.Text = "...";
            this.btnDesde.UseVisualStyleBackColor = true;
            this.btnDesde.Visible = false;
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(267, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Desde:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "A partir de:";
            // 
            // dgvInforme
            // 
            this.dgvInforme.AllowUserToAddRows = false;
            this.dgvInforme.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvInforme.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvInforme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInforme.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idMovimiento,
            this.fechaPedido,
            this.numeroPedido,
            this.valor,
            this.tipoPedido,
            this.cod,
            this.medida,
            this.producto,
            this.cantidad,
            this.vUnitario,
            this.vTotal,
            this.iva,
            this.desc});
            this.dgvInforme.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvInforme.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvInforme.Location = new System.Drawing.Point(12, 103);
            this.dgvInforme.Name = "dgvInforme";
            this.dgvInforme.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInforme.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvInforme.RowHeadersVisible = false;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInforme.RowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvInforme.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInforme.Size = new System.Drawing.Size(900, 261);
            this.dgvInforme.TabIndex = 20;
            // 
            // idMovimiento
            // 
            this.idMovimiento.HeaderText = "IDMOVIMIENTO";
            this.idMovimiento.Name = "idMovimiento";
            this.idMovimiento.ReadOnly = true;
            this.idMovimiento.Visible = false;
            // 
            // fechaPedido
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.fechaPedido.DefaultCellStyle = dataGridViewCellStyle1;
            this.fechaPedido.FillWeight = 284.7716F;
            this.fechaPedido.HeaderText = "LOC";
            this.fechaPedido.Name = "fechaPedido";
            this.fechaPedido.ReadOnly = true;
            this.fechaPedido.Width = 50;
            // 
            // numeroPedido
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numeroPedido.DefaultCellStyle = dataGridViewCellStyle2;
            this.numeroPedido.FillWeight = 7.156028F;
            this.numeroPedido.HeaderText = "FECHA";
            this.numeroPedido.Name = "numeroPedido";
            this.numeroPedido.ReadOnly = true;
            this.numeroPedido.Width = 80;
            // 
            // valor
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.valor.DefaultCellStyle = dataGridViewCellStyle3;
            this.valor.FillWeight = 8.072395F;
            this.valor.HeaderText = "PROVEEDOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 220;
            // 
            // tipoPedido
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.tipoPedido.DefaultCellStyle = dataGridViewCellStyle4;
            this.tipoPedido.HeaderText = "FACTURA";
            this.tipoPedido.Name = "tipoPedido";
            this.tipoPedido.ReadOnly = true;
            this.tipoPedido.Width = 80;
            // 
            // cod
            // 
            this.cod.HeaderText = "COD";
            this.cod.Name = "cod";
            this.cod.ReadOnly = true;
            this.cod.Width = 70;
            // 
            // medida
            // 
            this.medida.HeaderText = "MEDIDA";
            this.medida.Name = "medida";
            this.medida.ReadOnly = true;
            // 
            // producto
            // 
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            this.producto.Width = 220;
            // 
            // cantidad
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // vUnitario
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.vUnitario.DefaultCellStyle = dataGridViewCellStyle6;
            this.vUnitario.HeaderText = "V. UNIT.";
            this.vUnitario.Name = "vUnitario";
            this.vUnitario.ReadOnly = true;
            // 
            // vTotal
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.vTotal.DefaultCellStyle = dataGridViewCellStyle7;
            this.vTotal.HeaderText = "V. TOTAL";
            this.vTotal.Name = "vTotal";
            this.vTotal.ReadOnly = true;
            // 
            // iva
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.iva.DefaultCellStyle = dataGridViewCellStyle8;
            this.iva.HeaderText = "IVA";
            this.iva.Name = "iva";
            this.iva.ReadOnly = true;
            // 
            // desc
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.desc.DefaultCellStyle = dataGridViewCellStyle9;
            this.desc.HeaderText = "DESC";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            // 
            // btnExportarAExcel
            // 
            this.btnExportarAExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarAExcel.Image = global::Palatium.Properties.Resources.excel_png1;
            this.btnExportarAExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarAExcel.Location = new System.Drawing.Point(753, 25);
            this.btnExportarAExcel.Name = "btnExportarAExcel";
            this.btnExportarAExcel.Size = new System.Drawing.Size(89, 46);
            this.btnExportarAExcel.TabIndex = 91;
            this.btnExportarAExcel.Text = "Excel";
            this.btnExportarAExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarAExcel.UseVisualStyleBackColor = true;
            this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.limpiar_ico;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(658, 25);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(89, 46);
            this.btnLimpiar.TabIndex = 90;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::Palatium.Properties.Resources.buscar;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(563, 25);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(89, 46);
            this.btnAceptar.TabIndex = 89;
            this.btnAceptar.Text = "Buscar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // frmReporteDeIngresosRangoDeFechas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(918, 375);
            this.Controls.Add(this.dgvInforme);
            this.Controls.Add(this.grupoSeleccion);
            this.MaximizeBox = false;
            this.Name = "frmReporteDeIngresosRangoDeFechas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Ingresos en un Rango de fechas";
            this.Load += new System.EventHandler(this.frmReporteDeIngresosRangoDeFechas_Load);
            this.grupoSeleccion.ResumeLayout(false);
            this.grupoSeleccion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoSeleccion;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvInforme;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod;
        private System.Windows.Forms.DataGridViewTextBoxColumn medida;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn vUnitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn vTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.MaskedTextBox txtHasta;
        private System.Windows.Forms.MaskedTextBox txtDesde;
        private System.Windows.Forms.Button btnExportarAExcel;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAceptar;
    }
}