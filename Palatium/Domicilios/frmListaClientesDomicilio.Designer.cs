namespace Palatium.Domicilios
{
    partial class frmListaClientesDomicilio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.id_persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.apellidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombres = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo_alterno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.correo_electronico = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calle_principal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calle_secundaria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.convencional = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.celular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
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
            this.id_persona,
            this.identificacion,
            this.apellidos,
            this.nombres,
            this.codigo_alterno,
            this.correo_electronico,
            this.direccion,
            this.calle_principal,
            this.numeracion,
            this.calle_secundaria,
            this.referencia,
            this.convencional,
            this.celular,
            this.id_direccion,
            this.id_telefono});
            this.dgvDatos.Location = new System.Drawing.Point(12, 94);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(1130, 335);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Image = global::Palatium.Properties.Resources.ok2_png;
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAceptar.Location = new System.Drawing.Point(12, 12);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(99, 76);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Image = global::Palatium.Properties.Resources.cancelar2_png;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.Location = new System.Drawing.Point(117, 12);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(99, 76);
            this.btnSalir.TabIndex = 8;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // id_persona
            // 
            this.id_persona.HeaderText = "ID_PERSONA";
            this.id_persona.Name = "id_persona";
            this.id_persona.ReadOnly = true;
            this.id_persona.Visible = false;
            // 
            // identificacion
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identificacion.DefaultCellStyle = dataGridViewCellStyle1;
            this.identificacion.HeaderText = "IDENTIFICACIÓN";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            this.identificacion.Width = 150;
            // 
            // apellidos
            // 
            this.apellidos.HeaderText = "APELLIDOS";
            this.apellidos.Name = "apellidos";
            this.apellidos.ReadOnly = true;
            this.apellidos.Width = 200;
            // 
            // nombres
            // 
            this.nombres.HeaderText = "NOMBRES";
            this.nombres.Name = "nombres";
            this.nombres.ReadOnly = true;
            this.nombres.Width = 200;
            // 
            // codigo_alterno
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigo_alterno.DefaultCellStyle = dataGridViewCellStyle2;
            this.codigo_alterno.HeaderText = "TELÉFONO";
            this.codigo_alterno.Name = "codigo_alterno";
            this.codigo_alterno.ReadOnly = true;
            // 
            // correo_electronico
            // 
            this.correo_electronico.HeaderText = "CORREO ELECTRÓNICO";
            this.correo_electronico.Name = "correo_electronico";
            this.correo_electronico.ReadOnly = true;
            this.correo_electronico.Width = 200;
            // 
            // direccion
            // 
            this.direccion.HeaderText = "DIRECCIÓN";
            this.direccion.Name = "direccion";
            this.direccion.ReadOnly = true;
            this.direccion.Width = 200;
            // 
            // calle_principal
            // 
            this.calle_principal.HeaderText = "CALLE PRINCIPAL";
            this.calle_principal.Name = "calle_principal";
            this.calle_principal.ReadOnly = true;
            this.calle_principal.Width = 200;
            // 
            // numeracion
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numeracion.DefaultCellStyle = dataGridViewCellStyle3;
            this.numeracion.HeaderText = "NUMERACIÓN";
            this.numeracion.Name = "numeracion";
            this.numeracion.ReadOnly = true;
            // 
            // calle_secundaria
            // 
            this.calle_secundaria.HeaderText = "CALLE SECUNDARIA";
            this.calle_secundaria.Name = "calle_secundaria";
            this.calle_secundaria.ReadOnly = true;
            this.calle_secundaria.Width = 200;
            // 
            // referencia
            // 
            this.referencia.HeaderText = "REFERENCIA";
            this.referencia.Name = "referencia";
            this.referencia.ReadOnly = true;
            this.referencia.Width = 200;
            // 
            // convencional
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.convencional.DefaultCellStyle = dataGridViewCellStyle4;
            this.convencional.HeaderText = "TELF. CONVENCIONAL";
            this.convencional.Name = "convencional";
            this.convencional.ReadOnly = true;
            // 
            // celular
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.celular.DefaultCellStyle = dataGridViewCellStyle5;
            this.celular.HeaderText = "TELF. CELULAR";
            this.celular.Name = "celular";
            this.celular.ReadOnly = true;
            // 
            // id_direccion
            // 
            this.id_direccion.HeaderText = "ID_DIRECCION";
            this.id_direccion.Name = "id_direccion";
            this.id_direccion.ReadOnly = true;
            this.id_direccion.Visible = false;
            // 
            // id_telefono
            // 
            this.id_telefono.HeaderText = "ID_TELEFONO";
            this.id_telefono.Name = "id_telefono";
            this.id_telefono.ReadOnly = true;
            this.id_telefono.Visible = false;
            // 
            // frmListaClientesDomicilio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(1164, 458);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.dgvDatos);
            this.KeyPreview = true;
            this.Name = "frmListaClientesDomicilio";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Clientes";
            this.Load += new System.EventHandler(this.frmListaClientesDomicilio_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmListaClientesDomicilio_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDatos;
        internal System.Windows.Forms.Button btnAceptar;
        internal System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombres;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo_alterno;
        private System.Windows.Forms.DataGridViewTextBoxColumn correo_electronico;
        private System.Windows.Forms.DataGridViewTextBoxColumn direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn calle_principal;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeracion;
        private System.Windows.Forms.DataGridViewTextBoxColumn calle_secundaria;
        private System.Windows.Forms.DataGridViewTextBoxColumn referencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn convencional;
        private System.Windows.Forms.DataGridViewTextBoxColumn celular;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_telefono;
    }
}