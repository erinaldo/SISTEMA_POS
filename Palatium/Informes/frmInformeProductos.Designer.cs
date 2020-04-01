namespace Palatium.Informes
{
    partial class frmInformeProductos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvInforme = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnExportarTexto = new System.Windows.Forms.Button();
            this.btnExportarAExcel = new System.Windows.Forms.Button();
            this.grupoSeleccion = new System.Windows.Forms.GroupBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnHasta = new System.Windows.Forms.Button();
            this.btnDesde = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtHasta = new System.Windows.Forms.TextBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtInforme = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).BeginInit();
            this.grupoSeleccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvInforme
            // 
            this.dgvInforme.AllowUserToAddRows = false;
            this.dgvInforme.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInforme.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvInforme.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInforme.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.cantidad,
            this.total});
            this.dgvInforme.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvInforme.Location = new System.Drawing.Point(78, 116);
            this.dgvInforme.Name = "dgvInforme";
            this.dgvInforme.ReadOnly = true;
            this.dgvInforme.RowHeadersVisible = false;
            this.dgvInforme.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInforme.Size = new System.Drawing.Size(755, 420);
            this.dgvInforme.TabIndex = 1;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "PRODUCTO";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // cantidad
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle5;
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // total
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total.DefaultCellStyle = dataGridViewCellStyle6;
            this.total.HeaderText = "TOTAL";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Blue;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAceptar.Image = global::Palatium.Properties.Resources.buscar;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(351, 31);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(106, 50);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Buscar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnExportarTexto
            // 
            this.btnExportarTexto.BackColor = System.Drawing.Color.OliveDrab;
            this.btnExportarTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarTexto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportarTexto.Image = global::Palatium.Properties.Resources.txt;
            this.btnExportarTexto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarTexto.Location = new System.Drawing.Point(687, 31);
            this.btnExportarTexto.Name = "btnExportarTexto";
            this.btnExportarTexto.Size = new System.Drawing.Size(107, 50);
            this.btnExportarTexto.TabIndex = 4;
            this.btnExportarTexto.Text = "Exportar a\r\nTexto";
            this.btnExportarTexto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarTexto.UseVisualStyleBackColor = false;
            this.btnExportarTexto.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnExportarAExcel
            // 
            this.btnExportarAExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExportarAExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarAExcel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportarAExcel.Image = global::Palatium.Properties.Resources.excel1;
            this.btnExportarAExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarAExcel.Location = new System.Drawing.Point(575, 31);
            this.btnExportarAExcel.Name = "btnExportarAExcel";
            this.btnExportarAExcel.Size = new System.Drawing.Size(106, 50);
            this.btnExportarAExcel.TabIndex = 5;
            this.btnExportarAExcel.Text = "Exportar\r\na Excel";
            this.btnExportarAExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarAExcel.UseVisualStyleBackColor = false;
            this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click);
            // 
            // grupoSeleccion
            // 
            this.grupoSeleccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoSeleccion.Controls.Add(this.btnImprimir);
            this.grupoSeleccion.Controls.Add(this.btnExportarTexto);
            this.grupoSeleccion.Controls.Add(this.btnHasta);
            this.grupoSeleccion.Controls.Add(this.btnDesde);
            this.grupoSeleccion.Controls.Add(this.label2);
            this.grupoSeleccion.Controls.Add(this.label1);
            this.grupoSeleccion.Controls.Add(this.btnAceptar);
            this.grupoSeleccion.Controls.Add(this.btnExportarAExcel);
            this.grupoSeleccion.Controls.Add(this.TxtHasta);
            this.grupoSeleccion.Controls.Add(this.txtDesde);
            this.grupoSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoSeleccion.Location = new System.Drawing.Point(12, 12);
            this.grupoSeleccion.Name = "grupoSeleccion";
            this.grupoSeleccion.Size = new System.Drawing.Size(870, 98);
            this.grupoSeleccion.TabIndex = 11;
            this.grupoSeleccion.TabStop = false;
            this.grupoSeleccion.Text = "Filtro de Fechas";
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Red;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Image = global::Palatium.Properties.Resources.imprimir;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImprimir.Location = new System.Drawing.Point(463, 31);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(106, 50);
            this.btnImprimir.TabIndex = 19;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnHasta
            // 
            this.btnHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHasta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHasta.Location = new System.Drawing.Point(242, 59);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(31, 22);
            this.btnHasta.TabIndex = 5;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDesde.Location = new System.Drawing.Point(242, 35);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(31, 22);
            this.btnDesde.TabIndex = 4;
            this.btnDesde.Text = "...";
            this.btnDesde.UseVisualStyleBackColor = true;
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(17, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(17, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "A partir de:";
            // 
            // TxtHasta
            // 
            this.TxtHasta.Enabled = false;
            this.TxtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHasta.Location = new System.Drawing.Point(113, 59);
            this.TxtHasta.Name = "TxtHasta";
            this.TxtHasta.Size = new System.Drawing.Size(123, 22);
            this.TxtHasta.TabIndex = 1;
            // 
            // txtDesde
            // 
            this.txtDesde.Enabled = false;
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(113, 35);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(123, 22);
            this.txtDesde.TabIndex = 0;
            // 
            // txtInforme
            // 
            this.txtInforme.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtInforme.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInforme.Location = new System.Drawing.Point(12, 116);
            this.txtInforme.Multiline = true;
            this.txtInforme.Name = "txtInforme";
            this.txtInforme.ReadOnly = true;
            this.txtInforme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInforme.Size = new System.Drawing.Size(874, 420);
            this.txtInforme.TabIndex = 12;
            this.txtInforme.Visible = false;
            // 
            // frmInformeProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(894, 543);
            this.Controls.Add(this.txtInforme);
            this.Controls.Add(this.grupoSeleccion);
            this.Controls.Add(this.dgvInforme);
            this.MaximizeBox = false;
            this.Name = "frmInformeProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.btnExportarTexto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).EndInit();
            this.grupoSeleccion.ResumeLayout(false);
            this.grupoSeleccion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInforme;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnExportarTexto;
        private System.Windows.Forms.Button btnExportarAExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.GroupBox grupoSeleccion;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtHasta;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.TextBox txtInforme;
    }
}