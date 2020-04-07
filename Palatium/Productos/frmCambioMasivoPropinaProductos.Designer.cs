namespace Palatium.Productos
{
    partial class frmCambioMasivoPropinaProductos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCategorias = new System.Windows.Forms.ComboBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.actualizar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_recuperado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga_iva = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.paga_servicio = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.valor_nuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rdbGuardaSinImpuestos = new System.Windows.Forms.RadioButton();
            this.rdbGuardaConImpuestos = new System.Windows.Forms.RadioButton();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCategorias);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(709, 48);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Blue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(635, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 31);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 15);
            this.label1.TabIndex = 75;
            this.label1.Text = "Seleccione la categoría de productos:";
            // 
            // cmbCategorias
            // 
            this.cmbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategorias.FormattingEnabled = true;
            this.cmbCategorias.Location = new System.Drawing.Point(228, 15);
            this.cmbCategorias.Name = "cmbCategorias";
            this.cmbCategorias.Size = new System.Drawing.Size(401, 24);
            this.cmbCategorias.TabIndex = 76;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.actualizar,
            this.id_producto,
            this.valor_recuperado,
            this.nombre,
            this.valor,
            this.paga_iva,
            this.paga_servicio,
            this.valor_nuevo});
            this.dgvDatos.Location = new System.Drawing.Point(12, 92);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.RowHeadersWidth = 25;
            this.dgvDatos.Size = new System.Drawing.Size(709, 319);
            this.dgvDatos.TabIndex = 24;
            this.dgvDatos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellEndEdit);
            this.dgvDatos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDatos_EditingControlShowing);
            // 
            // actualizar
            // 
            this.actualizar.HeaderText = "";
            this.actualizar.Name = "actualizar";
            this.actualizar.ReadOnly = true;
            this.actualizar.Width = 30;
            // 
            // id_producto
            // 
            this.id_producto.HeaderText = "ID_PRODUCTO";
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            this.id_producto.Visible = false;
            // 
            // valor_recuperado
            // 
            this.valor_recuperado.HeaderText = "VALOR_RECUPERADO";
            this.valor_recuperado.Name = "valor_recuperado";
            this.valor_recuperado.ReadOnly = true;
            this.valor_recuperado.Visible = false;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre.HeaderText = "NOMBRE PRODUCTO";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // valor
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.valor.DefaultCellStyle = dataGridViewCellStyle7;
            this.valor.HeaderText = "VALOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            // 
            // paga_iva
            // 
            this.paga_iva.HeaderText = "PAGA IVA";
            this.paga_iva.Name = "paga_iva";
            this.paga_iva.ReadOnly = true;
            this.paga_iva.Width = 80;
            // 
            // paga_servicio
            // 
            this.paga_servicio.HeaderText = "PAGA SERVICIO";
            this.paga_servicio.Name = "paga_servicio";
            // 
            // valor_nuevo
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor_nuevo.DefaultCellStyle = dataGridViewCellStyle8;
            this.valor_nuevo.HeaderText = "VALOR NUEVO";
            this.valor_nuevo.Name = "valor_nuevo";
            this.valor_nuevo.Width = 110;
            // 
            // rdbGuardaSinImpuestos
            // 
            this.rdbGuardaSinImpuestos.AutoSize = true;
            this.rdbGuardaSinImpuestos.Enabled = false;
            this.rdbGuardaSinImpuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGuardaSinImpuestos.Location = new System.Drawing.Point(12, 12);
            this.rdbGuardaSinImpuestos.Name = "rdbGuardaSinImpuestos";
            this.rdbGuardaSinImpuestos.Size = new System.Drawing.Size(265, 20);
            this.rdbGuardaSinImpuestos.TabIndex = 29;
            this.rdbGuardaSinImpuestos.TabStop = true;
            this.rdbGuardaSinImpuestos.Text = "Guardar información sin impuestos";
            this.rdbGuardaSinImpuestos.UseVisualStyleBackColor = true;
            // 
            // rdbGuardaConImpuestos
            // 
            this.rdbGuardaConImpuestos.AutoSize = true;
            this.rdbGuardaConImpuestos.Enabled = false;
            this.rdbGuardaConImpuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGuardaConImpuestos.Location = new System.Drawing.Point(318, 12);
            this.rdbGuardaConImpuestos.Name = "rdbGuardaConImpuestos";
            this.rdbGuardaConImpuestos.Size = new System.Drawing.Size(270, 20);
            this.rdbGuardaConImpuestos.TabIndex = 28;
            this.rdbGuardaConImpuestos.TabStop = true;
            this.rdbGuardaConImpuestos.Text = "Guardar información con impuestos";
            this.rdbGuardaConImpuestos.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(628, 417);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnLimpiar.Size = new System.Drawing.Size(93, 39);
            this.btnLimpiar.TabIndex = 31;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Blue;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(529, 417);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(93, 39);
            this.btnGuardar.TabIndex = 30;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmCambioMasivoPropinaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.rdbGuardaSinImpuestos);
            this.Controls.Add(this.rdbGuardaConImpuestos);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmCambioMasivoPropinaProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Actualización del 10% de propina";
            this.Load += new System.EventHandler(this.frmCambioMasivoPropinaProductos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbCategorias;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn actualizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_recuperado;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn paga_iva;
        private System.Windows.Forms.DataGridViewCheckBoxColumn paga_servicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_nuevo;
        private System.Windows.Forms.RadioButton rdbGuardaSinImpuestos;
        private System.Windows.Forms.RadioButton rdbGuardaConImpuestos;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuardar;
    }
}