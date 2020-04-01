namespace Palatium.Informes
{
    partial class frmReporteControlFlujoDiario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grupoSeleccion = new System.Windows.Forms.GroupBox();
            this.btnExportarAExcel = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnHasta = new System.Windows.Forms.Button();
            this.btnDesde = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHasta = new System.Windows.Forms.TextBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvInforme = new System.Windows.Forms.DataGridView();
            this.fechaPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.seccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.consumoPromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grupoSeleccion.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).BeginInit();
            this.SuspendLayout();
            // 
            // grupoSeleccion
            // 
            this.grupoSeleccion.Controls.Add(this.btnExportarAExcel);
            this.grupoSeleccion.Controls.Add(this.btnAceptar);
            this.grupoSeleccion.Controls.Add(this.btnHasta);
            this.grupoSeleccion.Controls.Add(this.btnDesde);
            this.grupoSeleccion.Controls.Add(this.label2);
            this.grupoSeleccion.Controls.Add(this.label1);
            this.grupoSeleccion.Controls.Add(this.txtHasta);
            this.grupoSeleccion.Controls.Add(this.txtDesde);
            this.grupoSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoSeleccion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.grupoSeleccion.Location = new System.Drawing.Point(63, 12);
            this.grupoSeleccion.Name = "grupoSeleccion";
            this.grupoSeleccion.Size = new System.Drawing.Size(756, 107);
            this.grupoSeleccion.TabIndex = 7;
            this.grupoSeleccion.TabStop = false;
            this.grupoSeleccion.Text = "Filtro de Fechas";
            // 
            // btnExportarAExcel
            // 
            this.btnExportarAExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExportarAExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarAExcel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportarAExcel.Image = global::Palatium.Properties.Resources.excel1;
            this.btnExportarAExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarAExcel.Location = new System.Drawing.Point(560, 36);
            this.btnExportarAExcel.Name = "btnExportarAExcel";
            this.btnExportarAExcel.Size = new System.Drawing.Size(91, 49);
            this.btnExportarAExcel.TabIndex = 17;
            this.btnExportarAExcel.Text = "Exportar a Excel";
            this.btnExportarAExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarAExcel.UseVisualStyleBackColor = false;
            this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Blue;
            this.btnAceptar.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAceptar.Image = global::Palatium.Properties.Resources.buscar;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(452, 34);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(91, 51);
            this.btnAceptar.TabIndex = 11;
            this.btnAceptar.Text = "Buscar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnHasta
            // 
            this.btnHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHasta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHasta.Location = new System.Drawing.Point(300, 62);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(38, 24);
            this.btnHasta.TabIndex = 5;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDesde.Location = new System.Drawing.Point(300, 32);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(38, 24);
            this.btnDesde.TabIndex = 4;
            this.btnDesde.Text = "...";
            this.btnDesde.UseVisualStyleBackColor = true;
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(16, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(16, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "A partir de:";
            // 
            // txtHasta
            // 
            this.txtHasta.Enabled = false;
            this.txtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHasta.Location = new System.Drawing.Point(112, 62);
            this.txtHasta.Name = "txtHasta";
            this.txtHasta.Size = new System.Drawing.Size(182, 22);
            this.txtHasta.TabIndex = 1;
            // 
            // txtDesde
            // 
            this.txtDesde.Enabled = false;
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(112, 34);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(182, 22);
            this.txtDesde.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvInforme);
            this.groupBox2.Location = new System.Drawing.Point(12, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(844, 382);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // dgvInforme
            // 
            this.dgvInforme.AllowUserToAddRows = false;
            this.dgvInforme.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvInforme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInforme.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaPedido,
            this.numeroPedido,
            this.seccion,
            this.valor,
            this.tipoPedido,
            this.cuenta,
            this.consumoPromedio});
            this.dgvInforme.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dgvInforme.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvInforme.Location = new System.Drawing.Point(20, 19);
            this.dgvInforme.Name = "dgvInforme";
            this.dgvInforme.ReadOnly = true;
            this.dgvInforme.RowHeadersVisible = false;
            this.dgvInforme.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInforme.Size = new System.Drawing.Size(810, 350);
            this.dgvInforme.TabIndex = 18;
            // 
            // fechaPedido
            // 
            this.fechaPedido.FillWeight = 284.7716F;
            this.fechaPedido.HeaderText = "Mesero";
            this.fechaPedido.Name = "fechaPedido";
            this.fechaPedido.ReadOnly = true;
            this.fechaPedido.Width = 150;
            // 
            // numeroPedido
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.numeroPedido.DefaultCellStyle = dataGridViewCellStyle6;
            this.numeroPedido.FillWeight = 7.156028F;
            this.numeroPedido.HeaderText = "# Mesa";
            this.numeroPedido.Name = "numeroPedido";
            this.numeroPedido.ReadOnly = true;
            // 
            // seccion
            // 
            this.seccion.HeaderText = "Sección Mesa";
            this.seccion.Name = "seccion";
            this.seccion.ReadOnly = true;
            // 
            // valor
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle7;
            this.valor.FillWeight = 8.072395F;
            this.valor.HeaderText = "Hora";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 90;
            // 
            // tipoPedido
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tipoPedido.DefaultCellStyle = dataGridViewCellStyle8;
            this.tipoPedido.HeaderText = "#PAX";
            this.tipoPedido.Name = "tipoPedido";
            this.tipoPedido.ReadOnly = true;
            this.tipoPedido.Width = 90;
            // 
            // cuenta
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cuenta.DefaultCellStyle = dataGridViewCellStyle9;
            this.cuenta.HeaderText = "Cuenta";
            this.cuenta.Name = "cuenta";
            this.cuenta.ReadOnly = true;
            // 
            // consumoPromedio
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.consumoPromedio.DefaultCellStyle = dataGridViewCellStyle10;
            this.consumoPromedio.HeaderText = "Consumo Promedio";
            this.consumoPromedio.Name = "consumoPromedio";
            this.consumoPromedio.ReadOnly = true;
            this.consumoPromedio.Width = 140;
            // 
            // frmReporteControlFlujoDiario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(873, 522);
            this.Controls.Add(this.grupoSeleccion);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "frmReporteControlFlujoDiario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coontrol de Flujo Diario de Clientes";
            this.Load += new System.EventHandler(this.frmReporteControlFlujoDiario_Load);
            this.grupoSeleccion.ResumeLayout(false);
            this.grupoSeleccion.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoSeleccion;
        private System.Windows.Forms.Button btnExportarAExcel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHasta;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvInforme;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn seccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn consumoPromedio;
    }
}