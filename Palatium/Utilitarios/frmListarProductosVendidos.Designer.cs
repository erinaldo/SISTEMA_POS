namespace Palatium.Utilitarios
{
    partial class frmListarProductosVendidos
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
            this.label9 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.dtFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.cmbCantidad = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkFiltros = new System.Windows.Forms.CheckBox();
            this.grupoFiltro = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValorMenor = new System.Windows.Forms.TextBox();
            this.txtValorMayor = new System.Windows.Forms.TextBox();
            this.rdbMenor = new System.Windows.Forms.RadioButton();
            this.rdbEntre = new System.Windows.Forms.RadioButton();
            this.rdbMayor = new System.Windows.Forms.RadioButton();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.nombre_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.grupoFiltro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "Fecha Desde:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBuscar.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Location = new System.Drawing.Point(203, 101);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(93, 54);
            this.btnBuscar.TabIndex = 139;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label22.Location = new System.Drawing.Point(12, 20);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(75, 18);
            this.label22.TabIndex = 145;
            this.label22.Text = "Localidad:";
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.BackColor = System.Drawing.Color.White;
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(118, 19);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(356, 23);
            this.cmbLocalidades.TabIndex = 144;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLimpiar.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLimpiar.Location = new System.Drawing.Point(294, 101);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(89, 54);
            this.btnLimpiar.TabIndex = 150;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(253, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 146;
            this.label1.Text = "Fecha Hasta:";
            // 
            // dtFechaDesde
            // 
            this.dtFechaDesde.Checked = false;
            this.dtFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFechaDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaDesde.Location = new System.Drawing.Point(118, 59);
            this.dtFechaDesde.Name = "dtFechaDesde";
            this.dtFechaDesde.Size = new System.Drawing.Size(115, 21);
            this.dtFechaDesde.TabIndex = 150;
            // 
            // dtFechaHasta
            // 
            this.dtFechaHasta.Checked = false;
            this.dtFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFechaHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaHasta.Location = new System.Drawing.Point(359, 59);
            this.dtFechaHasta.Name = "dtFechaHasta";
            this.dtFechaHasta.Size = new System.Drawing.Size(115, 21);
            this.dtFechaHasta.TabIndex = 151;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImprimir);
            this.groupBox1.Controls.Add(this.cmbCantidad);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkFiltros);
            this.groupBox1.Controls.Add(this.grupoFiltro);
            this.groupBox1.Controls.Add(this.dtFechaHasta);
            this.groupBox1.Controls.Add(this.dtFechaDesde);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.cmbLocalidades);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(13, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 176);
            this.groupBox1.TabIndex = 153;
            this.groupBox1.TabStop = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Lime;
            this.btnImprimir.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnImprimir.Location = new System.Drawing.Point(383, 101);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(93, 54);
            this.btnImprimir.TabIndex = 156;
            this.btnImprimir.Text = "IMPRIMIR";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // cmbCantidad
            // 
            this.cmbCantidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbCantidad.FormattingEnabled = true;
            this.cmbCantidad.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmbCantidad.Location = new System.Drawing.Point(118, 116);
            this.cmbCantidad.Name = "cmbCantidad";
            this.cmbCantidad.Size = new System.Drawing.Size(74, 26);
            this.cmbCantidad.TabIndex = 155;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 18);
            this.label2.TabIndex = 154;
            this.label2.Text = "Cantidad Filtrar:";
            // 
            // chkFiltros
            // 
            this.chkFiltros.AutoSize = true;
            this.chkFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.chkFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.chkFiltros.Location = new System.Drawing.Point(502, 11);
            this.chkFiltros.Name = "chkFiltros";
            this.chkFiltros.Size = new System.Drawing.Size(145, 22);
            this.chkFiltros.TabIndex = 153;
            this.chkFiltros.Text = "Filtrar por Montos";
            this.chkFiltros.UseVisualStyleBackColor = false;
            this.chkFiltros.CheckedChanged += new System.EventHandler(this.chkFiltros_CheckedChanged);
            // 
            // grupoFiltro
            // 
            this.grupoFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grupoFiltro.Controls.Add(this.label5);
            this.grupoFiltro.Controls.Add(this.label3);
            this.grupoFiltro.Controls.Add(this.txtValorMenor);
            this.grupoFiltro.Controls.Add(this.txtValorMayor);
            this.grupoFiltro.Controls.Add(this.rdbMenor);
            this.grupoFiltro.Controls.Add(this.rdbEntre);
            this.grupoFiltro.Controls.Add(this.rdbMayor);
            this.grupoFiltro.Enabled = false;
            this.grupoFiltro.Location = new System.Drawing.Point(489, 19);
            this.grupoFiltro.Name = "grupoFiltro";
            this.grupoFiltro.Size = new System.Drawing.Size(309, 136);
            this.grupoFiltro.TabIndex = 153;
            this.grupoFiltro.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(179, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 18);
            this.label5.TabIndex = 157;
            this.label5.Text = "Valor Menor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(180, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 156;
            this.label3.Text = "Valor Mayor";
            // 
            // txtValorMenor
            // 
            this.txtValorMenor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtValorMenor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtValorMenor.Location = new System.Drawing.Point(162, 86);
            this.txtValorMenor.Name = "txtValorMenor";
            this.txtValorMenor.ReadOnly = true;
            this.txtValorMenor.Size = new System.Drawing.Size(122, 26);
            this.txtValorMenor.TabIndex = 156;
            this.txtValorMenor.Text = "0.00";
            this.txtValorMenor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorMenor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorMenor_KeyPress);
            // 
            // txtValorMayor
            // 
            this.txtValorMayor.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtValorMayor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtValorMayor.Location = new System.Drawing.Point(162, 39);
            this.txtValorMayor.Name = "txtValorMayor";
            this.txtValorMayor.Size = new System.Drawing.Size(122, 26);
            this.txtValorMayor.TabIndex = 154;
            this.txtValorMayor.Text = "0.00";
            this.txtValorMayor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorMayor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtValorMayor_KeyPress);
            // 
            // rdbMenor
            // 
            this.rdbMenor.AutoSize = true;
            this.rdbMenor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.rdbMenor.Location = new System.Drawing.Point(18, 88);
            this.rdbMenor.Name = "rdbMenor";
            this.rdbMenor.Size = new System.Drawing.Size(119, 22);
            this.rdbMenor.TabIndex = 155;
            this.rdbMenor.Text = "Valor Menor a";
            this.rdbMenor.UseVisualStyleBackColor = true;
            this.rdbMenor.CheckedChanged += new System.EventHandler(this.rdbMenor_CheckedChanged);
            // 
            // rdbEntre
            // 
            this.rdbEntre.AutoSize = true;
            this.rdbEntre.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.rdbEntre.Location = new System.Drawing.Point(18, 60);
            this.rdbEntre.Name = "rdbEntre";
            this.rdbEntre.Size = new System.Drawing.Size(99, 22);
            this.rdbEntre.TabIndex = 154;
            this.rdbEntre.Text = "Valor Entre";
            this.rdbEntre.UseVisualStyleBackColor = true;
            this.rdbEntre.CheckedChanged += new System.EventHandler(this.rdbEntre_CheckedChanged);
            // 
            // rdbMayor
            // 
            this.rdbMayor.AutoSize = true;
            this.rdbMayor.Checked = true;
            this.rdbMayor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.rdbMayor.Location = new System.Drawing.Point(18, 32);
            this.rdbMayor.Name = "rdbMayor";
            this.rdbMayor.Size = new System.Drawing.Size(118, 22);
            this.rdbMayor.TabIndex = 152;
            this.rdbMayor.TabStop = true;
            this.rdbMayor.Text = "Valor Mayor a";
            this.rdbMayor.UseVisualStyleBackColor = true;
            this.rdbMayor.CheckedChanged += new System.EventHandler(this.rdbMayor_CheckedChanged);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeight = 30;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre_producto,
            this.cantidad,
            this.valor});
            this.dgvDatos.Location = new System.Drawing.Point(15, 25);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDatos.RowTemplate.Height = 30;
            this.dgvDatos.Size = new System.Drawing.Size(775, 280);
            this.dgvDatos.TabIndex = 0;
            // 
            // nombre_producto
            // 
            this.nombre_producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.nombre_producto.DefaultCellStyle = dataGridViewCellStyle1;
            this.nombre_producto.HeaderText = "NOMBRE DEL PRODUCTO";
            this.nombre_producto.Name = "nombre_producto";
            this.nombre_producto.ReadOnly = true;
            // 
            // cantidad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.cantidad.HeaderText = "CANTIDAD REPORTADA";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 150;
            // 
            // valor
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle3;
            this.valor.HeaderText = "VALOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 150;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 18);
            this.label4.TabIndex = 154;
            this.label4.Text = "Total Registros Encontrados:";
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.BackColor = System.Drawing.Color.Transparent;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(242, 315);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(17, 18);
            this.lblCantidad.TabIndex = 155;
            this.lblCantidad.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblCantidad);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dgvDatos);
            this.groupBox2.Location = new System.Drawing.Point(12, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(804, 343);
            this.groupBox2.TabIndex = 152;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtTotal.Location = new System.Drawing.Point(618, 311);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(172, 26);
            this.txtTotal.TabIndex = 153;
            this.txtTotal.Text = "0.00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(547, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 18);
            this.label6.TabIndex = 152;
            this.label6.Text = "TOTAL:";
            // 
            // frmListarProductosVendidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(829, 534);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListarProductosVendidos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listar Productos más Vendidos";
            this.Load += new System.EventHandler(this.frmListarProductosVendidos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grupoFiltro.ResumeLayout(false);
            this.grupoFiltro.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFechaDesde;
        private System.Windows.Forms.DateTimePicker dtFechaHasta;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbMayor;
        private System.Windows.Forms.GroupBox grupoFiltro;
        private System.Windows.Forms.RadioButton rdbMenor;
        private System.Windows.Forms.RadioButton rdbEntre;
        private System.Windows.Forms.CheckBox chkFiltros;
        private System.Windows.Forms.ComboBox cmbCantidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValorMenor;
        private System.Windows.Forms.TextBox txtValorMayor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.Button btnImprimir;


    }
}