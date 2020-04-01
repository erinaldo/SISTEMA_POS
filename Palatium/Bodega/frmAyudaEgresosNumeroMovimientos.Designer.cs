namespace Palatium.Bodega
{
    partial class frmAyudaEgresosNumeroMovimientos
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
            this.Btn_Salir = new System.Windows.Forms.Button();
            this.Btn_Aceptar = new System.Windows.Forms.Button();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.numeroMovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referenciaExterna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.observacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idMovimientoBodega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idMovimientoTesoreria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Numero = new System.Windows.Forms.Label();
            this.TxtBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.Location = new System.Drawing.Point(417, 278);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(72, 31);
            this.Btn_Salir.TabIndex = 23;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = true;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Btn_Aceptar
            // 
            this.Btn_Aceptar.Location = new System.Drawing.Point(339, 278);
            this.Btn_Aceptar.Name = "Btn_Aceptar";
            this.Btn_Aceptar.Size = new System.Drawing.Size(72, 31);
            this.Btn_Aceptar.TabIndex = 22;
            this.Btn_Aceptar.Text = "Aceptar";
            this.Btn_Aceptar.UseVisualStyleBackColor = true;
            this.Btn_Aceptar.Click += new System.EventHandler(this.Btn_Aceptar_Click);
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AllowUserToAddRows = false;
            this.dgvMovimientos.AllowUserToDeleteRows = false;
            this.dgvMovimientos.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numeroMovimiento,
            this.referenciaExterna,
            this.observacion,
            this.idMovimientoBodega,
            this.fecha,
            this.idMovimientoTesoreria});
            this.dgvMovimientos.Location = new System.Drawing.Point(8, 75);
            this.dgvMovimientos.MultiSelect = false;
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.ReadOnly = true;
            this.dgvMovimientos.RowHeadersVisible = false;
            this.dgvMovimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimientos.Size = new System.Drawing.Size(480, 197);
            this.dgvMovimientos.TabIndex = 21;
            this.dgvMovimientos.DoubleClick += new System.EventHandler(this.dgvMovimientos_DoubleClick);
            // 
            // numeroMovimiento
            // 
            this.numeroMovimiento.HeaderText = "Número De Movimiento";
            this.numeroMovimiento.Name = "numeroMovimiento";
            this.numeroMovimiento.ReadOnly = true;
            // 
            // referenciaExterna
            // 
            this.referenciaExterna.HeaderText = "Referencia Externa";
            this.referenciaExterna.Name = "referenciaExterna";
            this.referenciaExterna.ReadOnly = true;
            this.referenciaExterna.Width = 264;
            // 
            // observacion
            // 
            this.observacion.HeaderText = "Observación";
            this.observacion.Name = "observacion";
            this.observacion.ReadOnly = true;
            // 
            // idMovimientoBodega
            // 
            this.idMovimientoBodega.HeaderText = "Id Movimiento Bodega";
            this.idMovimientoBodega.Name = "idMovimientoBodega";
            this.idMovimientoBodega.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            // 
            // idMovimientoTesoreria
            // 
            this.idMovimientoTesoreria.HeaderText = "Id Movimiento Tesorería";
            this.idMovimientoTesoreria.Name = "idMovimientoTesoreria";
            this.idMovimientoTesoreria.ReadOnly = true;
            // 
            // Numero
            // 
            this.Numero.AutoSize = true;
            this.Numero.Location = new System.Drawing.Point(140, 19);
            this.Numero.Name = "Numero";
            this.Numero.Size = new System.Drawing.Size(145, 13);
            this.Numero.TabIndex = 20;
            this.Numero.Text = "NÚMERO DE MOVIMIENTO";
            // 
            // TxtBusqueda
            // 
            this.TxtBusqueda.Location = new System.Drawing.Point(8, 39);
            this.TxtBusqueda.Name = "TxtBusqueda";
            this.TxtBusqueda.Size = new System.Drawing.Size(480, 20);
            this.TxtBusqueda.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Búsqueda basada en ...";
            // 
            // frmAyudaEgresosNumeroMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(509, 323);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btn_Aceptar);
            this.Controls.Add(this.dgvMovimientos);
            this.Controls.Add(this.Numero);
            this.Controls.Add(this.TxtBusqueda);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAyudaEgresosNumeroMovimientos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.Load += new System.EventHandler(this.frmAyudaEgresosNumeroMovimientos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btn_Aceptar;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.Label Numero;
        private System.Windows.Forms.TextBox TxtBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroMovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn referenciaExterna;
        private System.Windows.Forms.DataGridViewTextBoxColumn observacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMovimientoBodega;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMovimientoTesoreria;
    }
}