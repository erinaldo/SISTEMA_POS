namespace Palatium.Formularios
{
    partial class frmPreciosProductos
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
            this.grpDatosProducto = new System.Windows.Forms.GroupBox();
            this.btnListCategoria = new System.Windows.Forms.Button();
            this.txtNomCategoria = new System.Windows.Forms.TextBox();
            this.lblLisCategoria = new System.Windows.Forms.Label();
            this.txtIdCategoria = new System.Windows.Forms.TextBox();
            this.grpListPreProduc = new System.Windows.Forms.GroupBox();
            this.dgvListPreciosProductos = new System.Windows.Forms.DataGridView();
            this.clmIdPrecioProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrecioActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValorPorcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrecioNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmModificado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.statusStrip_cajero = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpDatosProducto.SuspendLayout();
            this.grpListPreProduc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPreciosProductos)).BeginInit();
            this.statusStrip_cajero.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatosProducto
            // 
            this.grpDatosProducto.Controls.Add(this.btnListCategoria);
            this.grpDatosProducto.Controls.Add(this.txtNomCategoria);
            this.grpDatosProducto.Controls.Add(this.lblLisCategoria);
            this.grpDatosProducto.Controls.Add(this.txtIdCategoria);
            this.grpDatosProducto.Location = new System.Drawing.Point(265, 26);
            this.grpDatosProducto.Name = "grpDatosProducto";
            this.grpDatosProducto.Size = new System.Drawing.Size(350, 117);
            this.grpDatosProducto.TabIndex = 7;
            this.grpDatosProducto.TabStop = false;
            this.grpDatosProducto.Text = "Datos de Producto";
            // 
            // btnListCategoria
            // 
            this.btnListCategoria.Location = new System.Drawing.Point(115, 49);
            this.btnListCategoria.Name = "btnListCategoria";
            this.btnListCategoria.Size = new System.Drawing.Size(35, 23);
            this.btnListCategoria.TabIndex = 36;
            this.btnListCategoria.Text = "?";
            this.btnListCategoria.UseVisualStyleBackColor = true;
            this.btnListCategoria.Click += new System.EventHandler(this.btnListCategoria_Click);
            // 
            // txtNomCategoria
            // 
            this.txtNomCategoria.Location = new System.Drawing.Point(156, 49);
            this.txtNomCategoria.MaxLength = 20;
            this.txtNomCategoria.Name = "txtNomCategoria";
            this.txtNomCategoria.Size = new System.Drawing.Size(181, 20);
            this.txtNomCategoria.TabIndex = 35;
            // 
            // lblLisCategoria
            // 
            this.lblLisCategoria.AutoSize = true;
            this.lblLisCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblLisCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLisCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLisCategoria.Location = new System.Drawing.Point(15, 27);
            this.lblLisCategoria.Name = "lblLisCategoria";
            this.lblLisCategoria.Size = new System.Drawing.Size(69, 15);
            this.lblLisCategoria.TabIndex = 34;
            this.lblLisCategoria.Text = "Categorías:";
            // 
            // txtIdCategoria
            // 
            this.txtIdCategoria.Location = new System.Drawing.Point(18, 51);
            this.txtIdCategoria.MaxLength = 20;
            this.txtIdCategoria.Name = "txtIdCategoria";
            this.txtIdCategoria.Size = new System.Drawing.Size(91, 20);
            this.txtIdCategoria.TabIndex = 33;
            // 
            // grpListPreProduc
            // 
            this.grpListPreProduc.Controls.Add(this.dgvListPreciosProductos);
            this.grpListPreProduc.Controls.Add(this.statusStrip_cajero);
            this.grpListPreProduc.Location = new System.Drawing.Point(61, 161);
            this.grpListPreProduc.Name = "grpListPreProduc";
            this.grpListPreProduc.Size = new System.Drawing.Size(773, 308);
            this.grpListPreProduc.TabIndex = 8;
            this.grpListPreProduc.TabStop = false;
            this.grpListPreProduc.Text = "Lista de Registros";
            // 
            // dgvListPreciosProductos
            // 
            this.dgvListPreciosProductos.AllowUserToAddRows = false;
            this.dgvListPreciosProductos.AllowUserToDeleteRows = false;
            this.dgvListPreciosProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPreciosProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmIdPrecioProducto,
            this.clmIdProducto,
            this.clmCodigo,
            this.clmNombre,
            this.clmPrecioActual,
            this.clmValorPorcentaje,
            this.clmPrecioNuevo,
            this.clmFechaDesde,
            this.clmFechaFinal,
            this.clmModificado});
            this.dgvListPreciosProductos.Location = new System.Drawing.Point(16, 19);
            this.dgvListPreciosProductos.Name = "dgvListPreciosProductos";
            this.dgvListPreciosProductos.Size = new System.Drawing.Size(735, 269);
            this.dgvListPreciosProductos.TabIndex = 10;
            // 
            // clmIdPrecioProducto
            // 
            this.clmIdPrecioProducto.DataPropertyName = "id_precio_producto";
            this.clmIdPrecioProducto.HeaderText = "Precio Producto";
            this.clmIdPrecioProducto.Name = "clmIdPrecioProducto";
            this.clmIdPrecioProducto.ReadOnly = true;
            // 
            // clmIdProducto
            // 
            this.clmIdProducto.DataPropertyName = "id_Producto";
            this.clmIdProducto.HeaderText = "Id Producto";
            this.clmIdProducto.Name = "clmIdProducto";
            this.clmIdProducto.ReadOnly = true;
            // 
            // clmCodigo
            // 
            this.clmCodigo.DataPropertyName = "codigo";
            this.clmCodigo.HeaderText = "Codigo";
            this.clmCodigo.Name = "clmCodigo";
            this.clmCodigo.ReadOnly = true;
            // 
            // clmNombre
            // 
            this.clmNombre.DataPropertyName = "nombre";
            this.clmNombre.HeaderText = "Nombre";
            this.clmNombre.Name = "clmNombre";
            this.clmNombre.ReadOnly = true;
            // 
            // clmPrecioActual
            // 
            this.clmPrecioActual.DataPropertyName = "precioCompra";
            this.clmPrecioActual.HeaderText = "Precio Actual";
            this.clmPrecioActual.Name = "clmPrecioActual";
            this.clmPrecioActual.ReadOnly = true;
            // 
            // clmValorPorcentaje
            // 
            this.clmValorPorcentaje.DataPropertyName = "Valor_porcentaje";
            this.clmValorPorcentaje.HeaderText = "Valor Porcentaje";
            this.clmValorPorcentaje.Name = "clmValorPorcentaje";
            this.clmValorPorcentaje.ReadOnly = true;
            // 
            // clmPrecioNuevo
            // 
            this.clmPrecioNuevo.DataPropertyName = "Precio_Minorista";
            this.clmPrecioNuevo.HeaderText = "Precio Nuevo";
            this.clmPrecioNuevo.Name = "clmPrecioNuevo";
            this.clmPrecioNuevo.ReadOnly = true;
            // 
            // clmFechaDesde
            // 
            this.clmFechaDesde.DataPropertyName = "Fecha_Desde";
            this.clmFechaDesde.HeaderText = "Fecha Desde";
            this.clmFechaDesde.Name = "clmFechaDesde";
            this.clmFechaDesde.ReadOnly = true;
            // 
            // clmFechaFinal
            // 
            this.clmFechaFinal.DataPropertyName = "Fecha_Hasta";
            this.clmFechaFinal.HeaderText = "Fecha Final";
            this.clmFechaFinal.Name = "clmFechaFinal";
            this.clmFechaFinal.ReadOnly = true;
            // 
            // clmModificado
            // 
            this.clmModificado.HeaderText = "Modificado";
            this.clmModificado.Name = "clmModificado";
            this.clmModificado.ReadOnly = true;
            // 
            // statusStrip_cajero
            // 
            this.statusStrip_cajero.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip_cajero.Location = new System.Drawing.Point(3, 168);
            this.statusStrip_cajero.Name = "statusStrip_cajero";
            this.statusStrip_cajero.Size = new System.Drawing.Size(716, 22);
            this.statusStrip_cajero.TabIndex = 9;
            this.statusStrip_cajero.Text = "statusStrip1";
            this.statusStrip_cajero.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // frmPreciosProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(915, 514);
            this.Controls.Add(this.grpListPreProduc);
            this.Controls.Add(this.grpDatosProducto);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPreciosProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Precios de Productos";
            this.grpDatosProducto.ResumeLayout(false);
            this.grpDatosProducto.PerformLayout();
            this.grpListPreProduc.ResumeLayout(false);
            this.grpListPreProduc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPreciosProductos)).EndInit();
            this.statusStrip_cajero.ResumeLayout(false);
            this.statusStrip_cajero.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatosProducto;
        private System.Windows.Forms.Button btnListCategoria;
        private System.Windows.Forms.TextBox txtNomCategoria;
        private System.Windows.Forms.Label lblLisCategoria;
        private System.Windows.Forms.TextBox txtIdCategoria;
        private System.Windows.Forms.GroupBox grpListPreProduc;
        private System.Windows.Forms.DataGridView dgvListPreciosProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIdPrecioProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrecioActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValorPorcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrecioNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaFinal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmModificado;
        private System.Windows.Forms.StatusStrip statusStrip_cajero;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}