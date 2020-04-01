namespace Palatium
{
    partial class Cortesias
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPedido = new System.Windows.Forms.DataGridView();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnConsumoAlimentos = new System.Windows.Forms.Button();
            this.guardada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valuni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cortesia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivoCortesia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancelar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivoCancelacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdMascara = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSecuenciaImpresion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrdenamiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagaIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPedido
            // 
            this.dgvPedido.AllowUserToAddRows = false;
            this.dgvPedido.AllowUserToDeleteRows = false;
            this.dgvPedido.AllowUserToResizeColumns = false;
            this.dgvPedido.AllowUserToResizeRows = false;
            this.dgvPedido.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvPedido.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPedido.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvPedido.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedido.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.guardada,
            this.cantidad,
            this.producto,
            this.valuni,
            this.valor,
            this.cod,
            this.ID,
            this.idProducto,
            this.cortesia,
            this.motivoCortesia,
            this.cancelar,
            this.motivoCancelacion,
            this.colIdMascara,
            this.colSecuenciaImpresion,
            this.colOrdenamiento,
            this.colIdOrden,
            this.pagaIva});
            this.dgvPedido.EnableHeadersVisualStyles = false;
            this.dgvPedido.Location = new System.Drawing.Point(26, 25);
            this.dgvPedido.Name = "dgvPedido";
            this.dgvPedido.ReadOnly = true;
            this.dgvPedido.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPedido.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPedido.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPedido.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPedido.Size = new System.Drawing.Size(394, 354);
            this.dgvPedido.TabIndex = 11;
            // 
            // btnSalir
            // 
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::Palatium.Properties.Resources.salir_png;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(447, 275);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(192, 104);
            this.btnSalir.TabIndex = 10;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click_1);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::Palatium.Properties.Resources.ok_png;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.Location = new System.Drawing.Point(447, 135);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(192, 104);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnConsumoAlimentos
            // 
            this.btnConsumoAlimentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsumoAlimentos.Image = global::Palatium.Properties.Resources.cortesia;
            this.btnConsumoAlimentos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsumoAlimentos.Location = new System.Drawing.Point(447, 25);
            this.btnConsumoAlimentos.Name = "btnConsumoAlimentos";
            this.btnConsumoAlimentos.Size = new System.Drawing.Size(192, 104);
            this.btnConsumoAlimentos.TabIndex = 7;
            this.btnConsumoAlimentos.Text = "Cortesias";
            this.btnConsumoAlimentos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsumoAlimentos.UseVisualStyleBackColor = true;
            this.btnConsumoAlimentos.Click += new System.EventHandler(this.btnConsumoAlimentos_Click);
            // 
            // guardada
            // 
            this.guardada.HeaderText = "GUARDADA";
            this.guardada.Name = "guardada";
            this.guardada.ReadOnly = true;
            this.guardada.Visible = false;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.cantidad.FillWeight = 60.9137F;
            this.cantidad.HeaderText = "CANT.";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            this.cantidad.Width = 53;
            // 
            // producto
            // 
            this.producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.producto.FillWeight = 168.8291F;
            this.producto.HeaderText = "PRODUCTO";
            this.producto.Name = "producto";
            this.producto.ReadOnly = true;
            // 
            // valuni
            // 
            this.valuni.HeaderText = "V. UNITARIO";
            this.valuni.Name = "valuni";
            this.valuni.ReadOnly = true;
            this.valuni.Visible = false;
            // 
            // valor
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle2;
            this.valor.FillWeight = 70.25717F;
            this.valor.HeaderText = "VALOR";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 62;
            // 
            // cod
            // 
            this.cod.HeaderText = "COD";
            this.cod.Name = "cod";
            this.cod.ReadOnly = true;
            this.cod.Visible = false;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // idProducto
            // 
            this.idProducto.HeaderText = "idProducto";
            this.idProducto.Name = "idProducto";
            this.idProducto.ReadOnly = true;
            this.idProducto.Visible = false;
            // 
            // cortesia
            // 
            this.cortesia.HeaderText = "Cortesia";
            this.cortesia.Name = "cortesia";
            this.cortesia.ReadOnly = true;
            this.cortesia.Visible = false;
            // 
            // motivoCortesia
            // 
            this.motivoCortesia.HeaderText = "motivo Cortesia";
            this.motivoCortesia.Name = "motivoCortesia";
            this.motivoCortesia.ReadOnly = true;
            this.motivoCortesia.Visible = false;
            // 
            // cancelar
            // 
            this.cancelar.HeaderText = "Cancelar";
            this.cancelar.Name = "cancelar";
            this.cancelar.ReadOnly = true;
            this.cancelar.Visible = false;
            // 
            // motivoCancelacion
            // 
            this.motivoCancelacion.HeaderText = "Motivo De Cancelación";
            this.motivoCancelacion.Name = "motivoCancelacion";
            this.motivoCancelacion.ReadOnly = true;
            this.motivoCancelacion.Visible = false;
            // 
            // colIdMascara
            // 
            this.colIdMascara.HeaderText = "Mascara";
            this.colIdMascara.Name = "colIdMascara";
            this.colIdMascara.ReadOnly = true;
            this.colIdMascara.Visible = false;
            // 
            // colSecuenciaImpresion
            // 
            this.colSecuenciaImpresion.HeaderText = "Secuencia";
            this.colSecuenciaImpresion.Name = "colSecuenciaImpresion";
            this.colSecuenciaImpresion.ReadOnly = true;
            this.colSecuenciaImpresion.Visible = false;
            // 
            // colOrdenamiento
            // 
            this.colOrdenamiento.HeaderText = "Ordenamiento";
            this.colOrdenamiento.Name = "colOrdenamiento";
            this.colOrdenamiento.ReadOnly = true;
            this.colOrdenamiento.Visible = false;
            // 
            // colIdOrden
            // 
            this.colIdOrden.HeaderText = "IdOrden";
            this.colIdOrden.Name = "colIdOrden";
            this.colIdOrden.ReadOnly = true;
            this.colIdOrden.Visible = false;
            // 
            // pagaIva
            // 
            this.pagaIva.HeaderText = "PAGA IVA";
            this.pagaIva.Name = "pagaIva";
            this.pagaIva.ReadOnly = true;
            this.pagaIva.Visible = false;
            // 
            // Cortesias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(663, 412);
            this.Controls.Add(this.dgvPedido);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnConsumoAlimentos);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cortesias";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cortesias";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Cortesias_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedido)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsumoAlimentos;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnSalir;
        public System.Windows.Forms.DataGridView dgvPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn guardada;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn valuni;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn idProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cortesia;
        private System.Windows.Forms.DataGridViewTextBoxColumn motivoCortesia;
        private System.Windows.Forms.DataGridViewTextBoxColumn cancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn motivoCancelacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdMascara;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSecuenciaImpresion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrdenamiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdOrden;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagaIva;
    }
}