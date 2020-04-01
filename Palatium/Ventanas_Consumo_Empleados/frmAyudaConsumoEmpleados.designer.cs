namespace Palatium.Ventanas_Consumo_Empleados
{
    partial class frmAyudaConsumoEmpleados
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
            this.dgvAyudaConsumoEmpleados = new System.Windows.Forms.DataGridView();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEmpleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lbl_Busqueda = new System.Windows.Forms.Label();
            this.TxtBusqueda = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAyudaConsumoEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Salir
            // 
            this.Btn_Salir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Btn_Salir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Btn_Salir.Location = new System.Drawing.Point(422, 258);
            this.Btn_Salir.Name = "Btn_Salir";
            this.Btn_Salir.Size = new System.Drawing.Size(70, 39);
            this.Btn_Salir.TabIndex = 11;
            this.Btn_Salir.Text = "Salir";
            this.Btn_Salir.UseVisualStyleBackColor = false;
            this.Btn_Salir.Click += new System.EventHandler(this.Btn_Salir_Click);
            // 
            // Btn_Aceptar
            // 
            this.Btn_Aceptar.BackColor = System.Drawing.Color.Blue;
            this.Btn_Aceptar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Btn_Aceptar.Location = new System.Drawing.Point(344, 258);
            this.Btn_Aceptar.Name = "Btn_Aceptar";
            this.Btn_Aceptar.Size = new System.Drawing.Size(70, 39);
            this.Btn_Aceptar.TabIndex = 10;
            this.Btn_Aceptar.Text = "Aceptar";
            this.Btn_Aceptar.UseVisualStyleBackColor = false;
            this.Btn_Aceptar.Click += new System.EventHandler(this.Btn_Aceptar_Click);
            // 
            // dgvAyudaConsumoEmpleados
            // 
            this.dgvAyudaConsumoEmpleados.AllowUserToAddRows = false;
            this.dgvAyudaConsumoEmpleados.AllowUserToDeleteRows = false;
            this.dgvAyudaConsumoEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAyudaConsumoEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nombre,
            this.identificacion,
            this.idEmpleado});
            this.dgvAyudaConsumoEmpleados.Location = new System.Drawing.Point(28, 77);
            this.dgvAyudaConsumoEmpleados.MultiSelect = false;
            this.dgvAyudaConsumoEmpleados.Name = "dgvAyudaConsumoEmpleados";
            this.dgvAyudaConsumoEmpleados.ReadOnly = true;
            this.dgvAyudaConsumoEmpleados.RowHeadersVisible = false;
            this.dgvAyudaConsumoEmpleados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAyudaConsumoEmpleados.Size = new System.Drawing.Size(464, 176);
            this.dgvAyudaConsumoEmpleados.TabIndex = 9;
            this.dgvAyudaConsumoEmpleados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAyudaConsumoEmpleados_CellDoubleClick);
            // 
            // nombre
            // 
            this.nombre.HeaderText = "NOMBRES";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 250;
            // 
            // identificacion
            // 
            this.identificacion.HeaderText = "IDENTIFICACION";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            this.identificacion.Width = 210;
            // 
            // idEmpleado
            // 
            this.idEmpleado.HeaderText = "ID";
            this.idEmpleado.Name = "idEmpleado";
            this.idEmpleado.ReadOnly = true;
            this.idEmpleado.Visible = false;
            // 
            // Lbl_Busqueda
            // 
            this.Lbl_Busqueda.AutoSize = true;
            this.Lbl_Busqueda.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Lbl_Busqueda.Location = new System.Drawing.Point(157, 15);
            this.Lbl_Busqueda.Name = "Lbl_Busqueda";
            this.Lbl_Busqueda.Size = new System.Drawing.Size(59, 13);
            this.Lbl_Busqueda.TabIndex = 8;
            this.Lbl_Busqueda.Text = "APELLIDO";
            // 
            // TxtBusqueda
            // 
            this.TxtBusqueda.Location = new System.Drawing.Point(28, 42);
            this.TxtBusqueda.Name = "TxtBusqueda";
            this.TxtBusqueda.Size = new System.Drawing.Size(377, 20);
            this.TxtBusqueda.TabIndex = 7;
            this.TxtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBusqueda_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Búsqueda basada en ...";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Blue;
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBuscar.Location = new System.Drawing.Point(422, 37);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(70, 29);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmAyudaConsumoEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(518, 309);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.Btn_Salir);
            this.Controls.Add(this.Btn_Aceptar);
            this.Controls.Add(this.dgvAyudaConsumoEmpleados);
            this.Controls.Add(this.Lbl_Busqueda);
            this.Controls.Add(this.TxtBusqueda);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAyudaConsumoEmpleados";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Personal";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAyudaConsumoEmpleados_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAyudaConsumoEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Salir;
        private System.Windows.Forms.Button Btn_Aceptar;
        private System.Windows.Forms.DataGridView dgvAyudaConsumoEmpleados;
        private System.Windows.Forms.Label Lbl_Busqueda;
        private System.Windows.Forms.TextBox TxtBusqueda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEmpleado;
        private System.Windows.Forms.Button btnBuscar;
    }
}