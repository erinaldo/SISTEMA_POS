namespace Palatium.ComandaNueva
{
    partial class frmDivisionCuentas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.colIdOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.pnlGrids = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSalir.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(1155, 617);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(165, 71);
            this.btnSalir.TabIndex = 52;
            this.btnSalir.Text = "SALIR";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAgregar.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Location = new System.Drawing.Point(330, 617);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(165, 71);
            this.btnAgregar.TabIndex = 51;
            this.btnAgregar.Text = "AGREGAR";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.OrangeRed;
            this.btnAnterior.Enabled = false;
            this.btnAnterior.Font = new System.Drawing.Font("Maiandra GD", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.Image = global::Palatium.Properties.Resources.izquierda;
            this.btnAnterior.Location = new System.Drawing.Point(0, 617);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(165, 71);
            this.btnAnterior.TabIndex = 50;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.Yellow;
            this.btnSiguiente.Enabled = false;
            this.btnSiguiente.Font = new System.Drawing.Font("Maiandra GD", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente.Image = global::Palatium.Properties.Resources.derecha;
            this.btnSiguiente.Location = new System.Drawing.Point(165, 617);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(165, 71);
            this.btnSiguiente.TabIndex = 49;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // colIdOrden
            // 
            this.colIdOrden.HeaderText = "IdOrden";
            this.colIdOrden.Name = "colIdOrden";
            this.colIdOrden.ReadOnly = true;
            this.colIdOrden.Visible = false;
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
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle25;
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
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.valor.DefaultCellStyle = dataGridViewCellStyle26;
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
            this.idProducto.HeaderText = "Id del Producto";
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
            this.cancelar.HeaderText = "Cancelar Producto";
            this.cancelar.Name = "cancelar";
            this.cancelar.ReadOnly = true;
            this.cancelar.Visible = false;
            // 
            // motivoCancelacion
            // 
            this.motivoCancelacion.HeaderText = "Motivo Cancelacion";
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
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnAceptar.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(990, 617);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(165, 71);
            this.btnAceptar.TabIndex = 53;
            this.btnAceptar.Text = "GUARDAR";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // pnlGrids
            // 
            this.pnlGrids.BackColor = System.Drawing.Color.SpringGreen;
            this.pnlGrids.Location = new System.Drawing.Point(0, 0);
            this.pnlGrids.Name = "pnlGrids";
            this.pnlGrids.Size = new System.Drawing.Size(330, 592);
            this.pnlGrids.TabIndex = 48;
            // 
            // frmDivisionCuentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1362, 700);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.pnlGrids);
            this.Name = "frmDivisionCuentas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dividir la Comanda";
            this.Load += new System.EventHandler(this.frmDivisionCuentas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdOrden;
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
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel pnlGrids;
    }
}