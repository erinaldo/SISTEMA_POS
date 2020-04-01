namespace Palatium.Soporte
{
    partial class frmReparacionMesas
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSubirFecha = new System.Windows.Forms.Button();
            this.btnFecha = new System.Windows.Forms.Button();
            this.btnBajarFecha = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.colIdPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumeroPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdMesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFechaPedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCambio = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colExito = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubirFecha
            // 
            this.btnSubirFecha.BackColor = System.Drawing.Color.Navy;
            this.btnSubirFecha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSubirFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubirFecha.FlatAppearance.BorderSize = 2;
            this.btnSubirFecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubirFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirFecha.ForeColor = System.Drawing.Color.White;
            this.btnSubirFecha.Image = global::Palatium.Properties.Resources.arriba_2;
            this.btnSubirFecha.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSubirFecha.Location = new System.Drawing.Point(15, 12);
            this.btnSubirFecha.Name = "btnSubirFecha";
            this.btnSubirFecha.Size = new System.Drawing.Size(57, 49);
            this.btnSubirFecha.TabIndex = 58;
            this.ttMensaje.SetToolTip(this.btnSubirFecha, "Clic aquí para aumentar la fecha");
            this.btnSubirFecha.UseVisualStyleBackColor = false;
            this.btnSubirFecha.Click += new System.EventHandler(this.btnSubirFecha_Click);
            this.btnSubirFecha.MouseEnter += new System.EventHandler(this.btnSubirFecha_MouseEnter);
            this.btnSubirFecha.MouseLeave += new System.EventHandler(this.btnSubirFecha_MouseLeave);
            // 
            // btnFecha
            // 
            this.btnFecha.BackColor = System.Drawing.Color.Navy;
            this.btnFecha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFecha.FlatAppearance.BorderSize = 2;
            this.btnFecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecha.ForeColor = System.Drawing.Color.White;
            this.btnFecha.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFecha.Location = new System.Drawing.Point(78, 12);
            this.btnFecha.Name = "btnFecha";
            this.btnFecha.Size = new System.Drawing.Size(141, 49);
            this.btnFecha.TabIndex = 59;
            this.ttMensaje.SetToolTip(this.btnFecha, "Clic aquí para mostrar el calendario");
            this.btnFecha.UseVisualStyleBackColor = false;
            this.btnFecha.Click += new System.EventHandler(this.btnFecha_Click);
            this.btnFecha.MouseEnter += new System.EventHandler(this.btnFecha_MouseEnter);
            this.btnFecha.MouseLeave += new System.EventHandler(this.btnFecha_MouseLeave);
            // 
            // btnBajarFecha
            // 
            this.btnBajarFecha.BackColor = System.Drawing.Color.Navy;
            this.btnBajarFecha.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBajarFecha.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBajarFecha.FlatAppearance.BorderSize = 2;
            this.btnBajarFecha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBajarFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajarFecha.ForeColor = System.Drawing.Color.White;
            this.btnBajarFecha.Image = global::Palatium.Properties.Resources.abajo_2;
            this.btnBajarFecha.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBajarFecha.Location = new System.Drawing.Point(225, 12);
            this.btnBajarFecha.Name = "btnBajarFecha";
            this.btnBajarFecha.Size = new System.Drawing.Size(57, 49);
            this.btnBajarFecha.TabIndex = 60;
            this.ttMensaje.SetToolTip(this.btnBajarFecha, "Clic aquí para regresar la fecha");
            this.btnBajarFecha.UseVisualStyleBackColor = false;
            this.btnBajarFecha.Click += new System.EventHandler(this.btnBajarFecha_Click);
            this.btnBajarFecha.MouseEnter += new System.EventHandler(this.btnBajarFecha_MouseEnter);
            this.btnBajarFecha.MouseLeave += new System.EventHandler(this.btnBajarFecha_MouseLeave);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Navy;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnBuscar.FlatAppearance.BorderSize = 2;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBuscar.Location = new System.Drawing.Point(350, 9);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(61, 57);
            this.btnBuscar.TabIndex = 62;
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnBuscar, "Clic aquí para buscar los registros");
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.btnBuscar.MouseEnter += new System.EventHandler(this.btnBuscar_MouseEnter);
            this.btnBuscar.MouseLeave += new System.EventHandler(this.btnBuscar_MouseLeave);
            // 
            // btnProcesar
            // 
            this.btnProcesar.BackColor = System.Drawing.Color.Navy;
            this.btnProcesar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnProcesar.FlatAppearance.BorderSize = 2;
            this.btnProcesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcesar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcesar.Image = global::Palatium.Properties.Resources.ok2_png;
            this.btnProcesar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProcesar.Location = new System.Drawing.Point(417, 9);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(61, 57);
            this.btnProcesar.TabIndex = 63;
            this.btnProcesar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnProcesar, "Clic aquí para procesar la información");
            this.btnProcesar.UseVisualStyleBackColor = false;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdPedido,
            this.colNumeroPedido,
            this.colIdMesa,
            this.colOrden,
            this.colFechaPedido,
            this.colCambio,
            this.colExito});
            this.dgvDatos.Location = new System.Drawing.Point(12, 109);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(877, 391);
            this.dgvDatos.TabIndex = 61;
            this.dgvDatos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellEndEdit);
            this.dgvDatos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvDatos_EditingControlShowing);
            // 
            // colIdPedido
            // 
            this.colIdPedido.HeaderText = "IdPedido";
            this.colIdPedido.Name = "colIdPedido";
            this.colIdPedido.ReadOnly = true;
            this.colIdPedido.Visible = false;
            // 
            // colNumeroPedido
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colNumeroPedido.DefaultCellStyle = dataGridViewCellStyle1;
            this.colNumeroPedido.HeaderText = "N° PEDIDO";
            this.colNumeroPedido.Name = "colNumeroPedido";
            this.colNumeroPedido.ReadOnly = true;
            // 
            // colIdMesa
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colIdMesa.DefaultCellStyle = dataGridViewCellStyle2;
            this.colIdMesa.HeaderText = "N° MESA";
            this.colIdMesa.Name = "colIdMesa";
            this.colIdMesa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colOrden
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colOrden.DefaultCellStyle = dataGridViewCellStyle3;
            this.colOrden.HeaderText = "TIPO DE ORDEN";
            this.colOrden.Name = "colOrden";
            this.colOrden.ReadOnly = true;
            this.colOrden.Width = 200;
            // 
            // colFechaPedido
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colFechaPedido.DefaultCellStyle = dataGridViewCellStyle4;
            this.colFechaPedido.HeaderText = "FECHA DEL PEDIDO";
            this.colFechaPedido.Name = "colFechaPedido";
            this.colFechaPedido.ReadOnly = true;
            this.colFechaPedido.Width = 200;
            // 
            // colCambio
            // 
            this.colCambio.HeaderText = "EDITADO";
            this.colCambio.Name = "colCambio";
            this.colCambio.ReadOnly = true;
            // 
            // colExito
            // 
            this.colExito.HeaderText = "PROCESADO";
            this.colExito.Name = "colExito";
            this.colExito.ReadOnly = true;
            // 
            // frmReparacionMesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(901, 512);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.btnBajarFecha);
            this.Controls.Add(this.btnFecha);
            this.Controls.Add(this.btnSubirFecha);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReparacionMesas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soporte de Mesas Sin Identificador";
            this.Load += new System.EventHandler(this.frmReparacionMesas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubirFecha;
        private System.Windows.Forms.Button btnFecha;
        private System.Windows.Forms.Button btnBajarFecha;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.DataGridView dgvDatos;
        internal System.Windows.Forms.Button btnBuscar;
        internal System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumeroPedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdMesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrden;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFechaPedido;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCambio;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colExito;
    }
}