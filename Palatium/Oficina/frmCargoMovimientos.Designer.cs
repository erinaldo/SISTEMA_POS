namespace Palatium.Oficina
{
    partial class frmCargoMovimientos
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
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaSecMes = new System.Windows.Forms.Label();
            this.lblDescrSecMes = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.lblcodigoSecMesa = new System.Windows.Forms.Label();
            this.Grb_Opciones = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Grb_Registros = new System.Windows.Forms.GroupBox();
            this.Grb_Dato = new System.Windows.Forms.GroupBox();
            this.Grb_Opciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.Grb_Registros.SuspendLayout();
            this.Grb_Dato.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbEstado
            // 
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(100, 116);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(152, 21);
            this.cmbEstado.TabIndex = 6;
            // 
            // lblEstaSecMes
            // 
            this.lblEstaSecMes.AutoSize = true;
            this.lblEstaSecMes.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaSecMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaSecMes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaSecMes.Location = new System.Drawing.Point(16, 118);
            this.lblEstaSecMes.Name = "lblEstaSecMes";
            this.lblEstaSecMes.Size = new System.Drawing.Size(48, 15);
            this.lblEstaSecMes.TabIndex = 7;
            this.lblEstaSecMes.Text = "Estado:";
            // 
            // lblDescrSecMes
            // 
            this.lblDescrSecMes.AutoSize = true;
            this.lblDescrSecMes.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrSecMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrSecMes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrSecMes.Location = new System.Drawing.Point(16, 68);
            this.lblDescrSecMes.Name = "lblDescrSecMes";
            this.lblDescrSecMes.Size = new System.Drawing.Size(75, 15);
            this.lblDescrSecMes.TabIndex = 5;
            this.lblDescrSecMes.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(100, 66);
            this.txtDescripcion.MaxLength = 150;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(216, 44);
            this.txtDescripcion.TabIndex = 4;
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(100, 40);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(216, 20);
            this.txtCodigo.TabIndex = 3;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(244, 19);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(72, 39);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(166, 19);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(72, 39);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Enabled = false;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(88, 19);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(72, 39);
            this.btnAnular.TabIndex = 8;
            this.btnAnular.Text = "Eliminar";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(10, 19);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(72, 39);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // lblcodigoSecMesa
            // 
            this.lblcodigoSecMesa.AutoSize = true;
            this.lblcodigoSecMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoSecMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoSecMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoSecMesa.Location = new System.Drawing.Point(16, 42);
            this.lblcodigoSecMesa.Name = "lblcodigoSecMesa";
            this.lblcodigoSecMesa.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoSecMesa.TabIndex = 3;
            this.lblcodigoSecMesa.Text = "Código:";
            // 
            // Grb_Opciones
            // 
            this.Grb_Opciones.BackColor = System.Drawing.Color.Transparent;
            this.Grb_Opciones.Controls.Add(this.btnCerrar);
            this.Grb_Opciones.Controls.Add(this.btnLimpiar);
            this.Grb_Opciones.Controls.Add(this.btnAnular);
            this.Grb_Opciones.Controls.Add(this.btnNuevo);
            this.Grb_Opciones.Location = new System.Drawing.Point(12, 258);
            this.Grb_Opciones.Name = "Grb_Opciones";
            this.Grb_Opciones.Size = new System.Drawing.Size(342, 73);
            this.Grb_Opciones.TabIndex = 7;
            this.Grb_Opciones.TabStop = false;
            this.Grb_Opciones.Text = "Opciones";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(235, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 29);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 1;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDatos.Location = new System.Drawing.Point(13, 55);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(350, 180);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // Grb_Registros
            // 
            this.Grb_Registros.BackColor = System.Drawing.Color.Transparent;
            this.Grb_Registros.Controls.Add(this.btnBuscar);
            this.Grb_Registros.Controls.Add(this.txtBuscar);
            this.Grb_Registros.Controls.Add(this.dgvDatos);
            this.Grb_Registros.Location = new System.Drawing.Point(372, 81);
            this.Grb_Registros.Name = "Grb_Registros";
            this.Grb_Registros.Size = new System.Drawing.Size(384, 250);
            this.Grb_Registros.TabIndex = 8;
            this.Grb_Registros.TabStop = false;
            this.Grb_Registros.Text = "Lista de Registros";
            // 
            // Grb_Dato
            // 
            this.Grb_Dato.BackColor = System.Drawing.Color.Transparent;
            this.Grb_Dato.Controls.Add(this.cmbEstado);
            this.Grb_Dato.Controls.Add(this.lblEstaSecMes);
            this.Grb_Dato.Controls.Add(this.lblDescrSecMes);
            this.Grb_Dato.Controls.Add(this.txtDescripcion);
            this.Grb_Dato.Controls.Add(this.lblcodigoSecMesa);
            this.Grb_Dato.Controls.Add(this.txtCodigo);
            this.Grb_Dato.Enabled = false;
            this.Grb_Dato.Location = new System.Drawing.Point(12, 81);
            this.Grb_Dato.Name = "Grb_Dato";
            this.Grb_Dato.Size = new System.Drawing.Size(342, 171);
            this.Grb_Dato.TabIndex = 6;
            this.Grb_Dato.TabStop = false;
            this.Grb_Dato.Text = "Datos del Registro";
            // 
            // frmCargoMovimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 345);
            this.Controls.Add(this.Grb_Opciones);
            this.Controls.Add(this.Grb_Registros);
            this.Controls.Add(this.Grb_Dato);
            this.MaximizeBox = false;
            this.Name = "frmCargoMovimientos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Cargos para Movimientos de Caja";
            this.Load += new System.EventHandler(this.frmCargoMovimientos_Load);
            this.Grb_Opciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.Grb_Registros.ResumeLayout(false);
            this.Grb_Registros.PerformLayout();
            this.Grb_Dato.ResumeLayout(false);
            this.Grb_Dato.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaSecMes;
        private System.Windows.Forms.Label lblDescrSecMes;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label lblcodigoSecMesa;
        private System.Windows.Forms.GroupBox Grb_Opciones;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox Grb_Registros;
        private System.Windows.Forms.GroupBox Grb_Dato;
    }
}