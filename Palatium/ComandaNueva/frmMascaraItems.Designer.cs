namespace Palatium.ComandaNueva
{
    partial class frmMascaraItems
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPedido = new System.Windows.Forms.DataGridView();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.chkTodos = new System.Windows.Forms.CheckBox();
            this.cmbMascaras = new System.Windows.Forms.ComboBox();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secuencia_impresion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bandera_cortesia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivo_cortesia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bandera_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivo_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_mascara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_ordenamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordenamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porcentaje_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bandera_comentario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor_descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre_original = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga_servicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.White;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::Palatium.Properties.Resources.salir_mesas;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(434, 415);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(184, 76);
            this.btnSalir.TabIndex = 100;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.White;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::Palatium.Properties.Resources.aceptar_digitos;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(434, 338);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(184, 76);
            this.btnAceptar.TabIndex = 99;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(299, 20);
            this.label1.TabIndex = 98;
            this.label1.Text = "ÍTEMS DEL PEDIDO REGISTRADO\r\n";
            // 
            // dgvPedido
            // 
            this.dgvPedido.AllowUserToAddRows = false;
            this.dgvPedido.AllowUserToDeleteRows = false;
            this.dgvPedido.AllowUserToResizeColumns = false;
            this.dgvPedido.AllowUserToResizeRows = false;
            this.dgvPedido.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvPedido.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvPedido.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPedido.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPedido.ColumnHeadersHeight = 30;
            this.dgvPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cantidad,
            this.nombre_producto,
            this.valor_unitario,
            this.valor_total,
            this.id_producto,
            this.paga_iva,
            this.codigo_producto,
            this.secuencia_impresion,
            this.bandera_cortesia,
            this.motivo_cortesia,
            this.bandera_descuento,
            this.motivo_descuento,
            this.id_mascara,
            this.id_ordenamiento,
            this.ordenamiento,
            this.porcentaje_descuento,
            this.bandera_comentario,
            this.valor_descuento,
            this.nombre_original,
            this.paga_servicio});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPedido.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvPedido.EnableHeadersVisualStyles = false;
            this.dgvPedido.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvPedido.Location = new System.Drawing.Point(12, 52);
            this.dgvPedido.MultiSelect = false;
            this.dgvPedido.Name = "dgvPedido";
            this.dgvPedido.ReadOnly = true;
            this.dgvPedido.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPedido.RowHeadersVisible = false;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPedido.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedido.Size = new System.Drawing.Size(393, 439);
            this.dgvPedido.TabIndex = 97;
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.White;
            this.btnRemover.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemover.Image = global::Palatium.Properties.Resources.eliminar_pago_png;
            this.btnRemover.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRemover.Location = new System.Drawing.Point(434, 214);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(184, 76);
            this.btnRemover.TabIndex = 102;
            this.btnRemover.Text = "Remover\r\nMáscara";
            this.btnRemover.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.White;
            this.btnAplicar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.Image = global::Palatium.Properties.Resources.ok2_png;
            this.btnAplicar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAplicar.Location = new System.Drawing.Point(434, 132);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(184, 76);
            this.btnAplicar.TabIndex = 101;
            this.btnAplicar.Text = "Aplicar\r\nMáscara";
            this.btnAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // chkTodos
            // 
            this.chkTodos.AutoSize = true;
            this.chkTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTodos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkTodos.Location = new System.Drawing.Point(434, 52);
            this.chkTodos.Name = "chkTodos";
            this.chkTodos.Size = new System.Drawing.Size(169, 22);
            this.chkTodos.TabIndex = 103;
            this.chkTodos.Text = "Seleccionar Todos";
            this.chkTodos.UseVisualStyleBackColor = true;
            // 
            // cmbMascaras
            // 
            this.cmbMascaras.BackColor = System.Drawing.Color.White;
            this.cmbMascaras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMascaras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMascaras.FormattingEnabled = true;
            this.cmbMascaras.Location = new System.Drawing.Point(415, 80);
            this.cmbMascaras.Name = "cmbMascaras";
            this.cmbMascaras.Size = new System.Drawing.Size(219, 28);
            this.cmbMascaras.TabIndex = 104;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.FillWeight = 60.9137F;
            this.cantidad.HeaderText = "CANT.";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 53;
            // 
            // nombre_producto
            // 
            this.nombre_producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombre_producto.DefaultCellStyle = dataGridViewCellStyle2;
            this.nombre_producto.FillWeight = 168.8291F;
            this.nombre_producto.HeaderText = "PRODUCTO";
            this.nombre_producto.Name = "nombre_producto";
            this.nombre_producto.ReadOnly = true;
            // 
            // valor_unitario
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valor_unitario.DefaultCellStyle = dataGridViewCellStyle3;
            this.valor_unitario.HeaderText = "V. UNITARIO";
            this.valor_unitario.Name = "valor_unitario";
            this.valor_unitario.ReadOnly = true;
            this.valor_unitario.Visible = false;
            // 
            // valor_total
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valor_total.DefaultCellStyle = dataGridViewCellStyle4;
            this.valor_total.FillWeight = 70.25717F;
            this.valor_total.HeaderText = "VALOR";
            this.valor_total.Name = "valor_total";
            this.valor_total.ReadOnly = true;
            this.valor_total.Width = 62;
            // 
            // id_producto
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_producto.DefaultCellStyle = dataGridViewCellStyle5;
            this.id_producto.HeaderText = "ID_PRODUCTO";
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            this.id_producto.Visible = false;
            // 
            // paga_iva
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paga_iva.DefaultCellStyle = dataGridViewCellStyle6;
            this.paga_iva.HeaderText = "PAGA IVA";
            this.paga_iva.Name = "paga_iva";
            this.paga_iva.ReadOnly = true;
            this.paga_iva.Visible = false;
            // 
            // codigo_producto
            // 
            this.codigo_producto.HeaderText = "CODIGO_PRODUCTO";
            this.codigo_producto.Name = "codigo_producto";
            this.codigo_producto.ReadOnly = true;
            this.codigo_producto.Visible = false;
            // 
            // secuencia_impresion
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secuencia_impresion.DefaultCellStyle = dataGridViewCellStyle7;
            this.secuencia_impresion.HeaderText = "SECUENCIA IMPRESION";
            this.secuencia_impresion.Name = "secuencia_impresion";
            this.secuencia_impresion.ReadOnly = true;
            this.secuencia_impresion.Visible = false;
            // 
            // bandera_cortesia
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandera_cortesia.DefaultCellStyle = dataGridViewCellStyle8;
            this.bandera_cortesia.HeaderText = "BANDERA_CORTESIA";
            this.bandera_cortesia.Name = "bandera_cortesia";
            this.bandera_cortesia.ReadOnly = true;
            this.bandera_cortesia.Visible = false;
            // 
            // motivo_cortesia
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motivo_cortesia.DefaultCellStyle = dataGridViewCellStyle9;
            this.motivo_cortesia.HeaderText = "MOTIVO_CORTESIA";
            this.motivo_cortesia.Name = "motivo_cortesia";
            this.motivo_cortesia.ReadOnly = true;
            this.motivo_cortesia.Visible = false;
            // 
            // bandera_descuento
            // 
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bandera_descuento.DefaultCellStyle = dataGridViewCellStyle10;
            this.bandera_descuento.HeaderText = "BANDERA DESCUENTO";
            this.bandera_descuento.Name = "bandera_descuento";
            this.bandera_descuento.ReadOnly = true;
            this.bandera_descuento.Visible = false;
            // 
            // motivo_descuento
            // 
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motivo_descuento.DefaultCellStyle = dataGridViewCellStyle11;
            this.motivo_descuento.HeaderText = "MOTIVO_DESCUENTO";
            this.motivo_descuento.Name = "motivo_descuento";
            this.motivo_descuento.ReadOnly = true;
            this.motivo_descuento.Visible = false;
            // 
            // id_mascara
            // 
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_mascara.DefaultCellStyle = dataGridViewCellStyle12;
            this.id_mascara.HeaderText = "ID_MASCARA";
            this.id_mascara.Name = "id_mascara";
            this.id_mascara.ReadOnly = true;
            this.id_mascara.Visible = false;
            // 
            // id_ordenamiento
            // 
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.id_ordenamiento.DefaultCellStyle = dataGridViewCellStyle13;
            this.id_ordenamiento.HeaderText = "ID_ORDENAMIENTO";
            this.id_ordenamiento.Name = "id_ordenamiento";
            this.id_ordenamiento.ReadOnly = true;
            this.id_ordenamiento.Visible = false;
            // 
            // ordenamiento
            // 
            this.ordenamiento.HeaderText = "ORDENAMIENTO";
            this.ordenamiento.Name = "ordenamiento";
            this.ordenamiento.ReadOnly = true;
            this.ordenamiento.Visible = false;
            // 
            // porcentaje_descuento
            // 
            this.porcentaje_descuento.HeaderText = "PORCENTAJE DESCUENTO";
            this.porcentaje_descuento.Name = "porcentaje_descuento";
            this.porcentaje_descuento.ReadOnly = true;
            this.porcentaje_descuento.Visible = false;
            // 
            // bandera_comentario
            // 
            this.bandera_comentario.HeaderText = "BANDERA_COMENTARIO";
            this.bandera_comentario.Name = "bandera_comentario";
            this.bandera_comentario.ReadOnly = true;
            this.bandera_comentario.Visible = false;
            // 
            // valor_descuento
            // 
            this.valor_descuento.HeaderText = "VALOR_DESCUENTO";
            this.valor_descuento.Name = "valor_descuento";
            this.valor_descuento.ReadOnly = true;
            this.valor_descuento.Visible = false;
            // 
            // nombre_original
            // 
            this.nombre_original.HeaderText = "NOMBRE_ORIGINAL";
            this.nombre_original.Name = "nombre_original";
            this.nombre_original.ReadOnly = true;
            this.nombre_original.Visible = false;
            // 
            // paga_servicio
            // 
            this.paga_servicio.HeaderText = "PAGA_SERVICIO";
            this.paga_servicio.Name = "paga_servicio";
            this.paga_servicio.ReadOnly = true;
            this.paga_servicio.Visible = false;
            // 
            // frmMascaraItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(646, 513);
            this.Controls.Add(this.cmbMascaras);
            this.Controls.Add(this.chkTodos);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPedido);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMascaraItems";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMascaraItems";
            this.Load += new System.EventHandler(this.frmMascaraItems_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMascaraItems_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dgvPedido;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.CheckBox chkTodos;
        private System.Windows.Forms.ComboBox cmbMascaras;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn paga_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn secuencia_impresion;
        private System.Windows.Forms.DataGridViewTextBoxColumn bandera_cortesia;
        private System.Windows.Forms.DataGridViewTextBoxColumn motivo_cortesia;
        private System.Windows.Forms.DataGridViewTextBoxColumn bandera_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn motivo_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_mascara;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_ordenamiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn ordenamiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn porcentaje_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn bandera_comentario;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor_descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre_original;
        private System.Windows.Forms.DataGridViewTextBoxColumn paga_servicio;
    }
}