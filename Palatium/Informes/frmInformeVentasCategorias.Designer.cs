namespace Palatium.Informes
{
    partial class frmInformeVentasCategorias
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtInforme = new System.Windows.Forms.TextBox();
            this.dgvInforme = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExportarAExcel = new System.Windows.Forms.Button();
            this.btnExportarTexto = new System.Windows.Forms.Button();
            this.grupoSeleccion = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnHasta = new System.Windows.Forms.Button();
            this.btnDesde = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtHasta = new System.Windows.Forms.TextBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).BeginInit();
            this.grupoSeleccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtInforme);
            this.groupBox2.Controls.Add(this.dgvInforme);
            this.groupBox2.Location = new System.Drawing.Point(12, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(919, 380);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ventas ";
            // 
            // txtInforme
            // 
            this.txtInforme.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtInforme.Location = new System.Drawing.Point(6, 19);
            this.txtInforme.Multiline = true;
            this.txtInforme.Name = "txtInforme";
            this.txtInforme.ReadOnly = true;
            this.txtInforme.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtInforme.Size = new System.Drawing.Size(907, 352);
            this.txtInforme.TabIndex = 17;
            this.txtInforme.Visible = false;
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
            this.dgvInforme.Location = new System.Drawing.Point(96, 19);
            this.dgvInforme.Name = "dgvInforme";
            this.dgvInforme.ReadOnly = true;
            this.dgvInforme.RowHeadersVisible = false;
            this.dgvInforme.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInforme.Size = new System.Drawing.Size(755, 352);
            this.dgvInforme.TabIndex = 16;
            // 
            // nombre
            // 
            this.nombre.HeaderText = "CATEGORIA";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // total
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.total.DefaultCellStyle = dataGridViewCellStyle2;
            this.total.HeaderText = "TOTAL";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // btnExportarAExcel
            // 
            this.btnExportarAExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExportarAExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarAExcel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportarAExcel.Image = global::Palatium.Properties.Resources.excel1;
            this.btnExportarAExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarAExcel.Location = new System.Drawing.Point(596, 29);
            this.btnExportarAExcel.Name = "btnExportarAExcel";
            this.btnExportarAExcel.Size = new System.Drawing.Size(91, 50);
            this.btnExportarAExcel.TabIndex = 18;
            this.btnExportarAExcel.Text = "Exportar a Excel";
            this.btnExportarAExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarAExcel.UseVisualStyleBackColor = false;
            this.btnExportarAExcel.Click += new System.EventHandler(this.btnExportarAExcel_Click);
            // 
            // btnExportarTexto
            // 
            this.btnExportarTexto.BackColor = System.Drawing.Color.Green;
            this.btnExportarTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarTexto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportarTexto.Image = global::Palatium.Properties.Resources.txt;
            this.btnExportarTexto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarTexto.Location = new System.Drawing.Point(704, 29);
            this.btnExportarTexto.Name = "btnExportarTexto";
            this.btnExportarTexto.Size = new System.Drawing.Size(91, 50);
            this.btnExportarTexto.TabIndex = 17;
            this.btnExportarTexto.Text = "Exportar a Texto";
            this.btnExportarTexto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportarTexto.UseVisualStyleBackColor = false;
            this.btnExportarTexto.Click += new System.EventHandler(this.button2_Click);
            // 
            // grupoSeleccion
            // 
            this.grupoSeleccion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoSeleccion.Controls.Add(this.button1);
            this.grupoSeleccion.Controls.Add(this.btnExportarTexto);
            this.grupoSeleccion.Controls.Add(this.btnExportarAExcel);
            this.grupoSeleccion.Controls.Add(this.btnAceptar);
            this.grupoSeleccion.Controls.Add(this.btnHasta);
            this.grupoSeleccion.Controls.Add(this.btnDesde);
            this.grupoSeleccion.Controls.Add(this.label2);
            this.grupoSeleccion.Controls.Add(this.label1);
            this.grupoSeleccion.Controls.Add(this.TxtHasta);
            this.grupoSeleccion.Controls.Add(this.txtDesde);
            this.grupoSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoSeleccion.Location = new System.Drawing.Point(12, 12);
            this.grupoSeleccion.Name = "grupoSeleccion";
            this.grupoSeleccion.Size = new System.Drawing.Size(913, 101);
            this.grupoSeleccion.TabIndex = 12;
            this.grupoSeleccion.TabStop = false;
            this.grupoSeleccion.Text = "Filtro de Fechas";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = global::Palatium.Properties.Resources.imprimir;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(488, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 52);
            this.button1.TabIndex = 20;
            this.button1.Text = "Imprimir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Blue;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAceptar.Image = global::Palatium.Properties.Resources.buscar;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(380, 30);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(91, 50);
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
            this.btnHasta.Location = new System.Drawing.Point(242, 61);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(32, 22);
            this.btnHasta.TabIndex = 5;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDesde.Location = new System.Drawing.Point(242, 33);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(32, 22);
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
            this.label2.Location = new System.Drawing.Point(19, 64);
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
            this.label1.Location = new System.Drawing.Point(19, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "A partir de:";
            // 
            // TxtHasta
            // 
            this.TxtHasta.Enabled = false;
            this.TxtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHasta.Location = new System.Drawing.Point(115, 61);
            this.TxtHasta.Name = "TxtHasta";
            this.TxtHasta.Size = new System.Drawing.Size(121, 22);
            this.TxtHasta.TabIndex = 1;
            // 
            // txtDesde
            // 
            this.txtDesde.Enabled = false;
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(115, 33);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(121, 22);
            this.txtDesde.TabIndex = 0;
            // 
            // frmInformeVentasCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(944, 506);
            this.Controls.Add(this.grupoSeleccion);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.Name = "frmInformeVentasCategorias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informe de Ventas por Categorías";
            this.Load += new System.EventHandler(this.frmInformeVentasCategorias_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInforme)).EndInit();
            this.grupoSeleccion.ResumeLayout(false);
            this.grupoSeleccion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExportarAExcel;
        private System.Windows.Forms.DataGridView dgvInforme;
        private System.Windows.Forms.Button btnExportarTexto;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.GroupBox grupoSeleccion;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtHasta;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtInforme;
        private System.Windows.Forms.Button btnAceptar;
    }
}