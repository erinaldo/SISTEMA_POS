namespace Palatium.Productos
{
    partial class frmAdministracionClaseProducto
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
            this.Grb_opciojornada = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.Grb_listRejornada = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvRegistro = new System.Windows.Forms.DataGridView();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaJornada = new System.Windows.Forms.Label();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigoCajero = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.chkMateriaPrima = new System.Windows.Forms.CheckBox();
            this.chkProductoTerminado = new System.Windows.Forms.CheckBox();
            this.idJornada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materia_prima = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto_terminado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grb_opciojornada.SuspendLayout();
            this.Grb_listRejornada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro)).BeginInit();
            this.grupoDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_opciojornada
            // 
            this.Grb_opciojornada.BackColor = System.Drawing.Color.Transparent;
            this.Grb_opciojornada.Controls.Add(this.btnCerrar);
            this.Grb_opciojornada.Controls.Add(this.btnLimpiar);
            this.Grb_opciojornada.Controls.Add(this.btnAnular);
            this.Grb_opciojornada.Controls.Add(this.btnNuevo);
            this.Grb_opciojornada.Location = new System.Drawing.Point(12, 246);
            this.Grb_opciojornada.Name = "Grb_opciojornada";
            this.Grb_opciojornada.Size = new System.Drawing.Size(342, 75);
            this.Grb_opciojornada.TabIndex = 19;
            this.Grb_opciojornada.TabStop = false;
            this.Grb_opciojornada.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(245, 22);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(64, 39);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(175, 22);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(64, 39);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Enabled = false;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(105, 22);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(64, 39);
            this.btnAnular.TabIndex = 1;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(35, 22);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(64, 39);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // Grb_listRejornada
            // 
            this.Grb_listRejornada.BackColor = System.Drawing.Color.Transparent;
            this.Grb_listRejornada.Controls.Add(this.btnBuscar);
            this.Grb_listRejornada.Controls.Add(this.txtBuscar);
            this.Grb_listRejornada.Controls.Add(this.dgvRegistro);
            this.Grb_listRejornada.Location = new System.Drawing.Point(362, 79);
            this.Grb_listRejornada.Name = "Grb_listRejornada";
            this.Grb_listRejornada.Size = new System.Drawing.Size(469, 242);
            this.Grb_listRejornada.TabIndex = 18;
            this.Grb_listRejornada.TabStop = false;
            this.Grb_listRejornada.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(238, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(16, 29);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // dgvRegistro
            // 
            this.dgvRegistro.AllowUserToAddRows = false;
            this.dgvRegistro.AllowUserToDeleteRows = false;
            this.dgvRegistro.AllowUserToResizeColumns = false;
            this.dgvRegistro.AllowUserToResizeRows = false;
            this.dgvRegistro.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvRegistro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistro.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idJornada,
            this.codigo,
            this.descripcion,
            this.estado1,
            this.materia_prima,
            this.producto_terminado});
            this.dgvRegistro.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvRegistro.Location = new System.Drawing.Point(16, 61);
            this.dgvRegistro.MultiSelect = false;
            this.dgvRegistro.Name = "dgvRegistro";
            this.dgvRegistro.ReadOnly = true;
            this.dgvRegistro.RowHeadersVisible = false;
            this.dgvRegistro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRegistro.Size = new System.Drawing.Size(434, 167);
            this.dgvRegistro.TabIndex = 0;
            this.dgvRegistro.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRegistro_CellDoubleClick);
            // 
            // grupoDatos
            // 
            this.grupoDatos.BackColor = System.Drawing.Color.Transparent;
            this.grupoDatos.Controls.Add(this.chkProductoTerminado);
            this.grupoDatos.Controls.Add(this.chkMateriaPrima);
            this.grupoDatos.Controls.Add(this.cmbEstado);
            this.grupoDatos.Controls.Add(this.lblEstaJornada);
            this.grupoDatos.Controls.Add(this.lblDescrCajero);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblCodigoCajero);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 79);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(342, 161);
            this.grupoDatos.TabIndex = 17;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(100, 124);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(107, 21);
            this.cmbEstado.TabIndex = 10;
            // 
            // lblEstaJornada
            // 
            this.lblEstaJornada.AutoSize = true;
            this.lblEstaJornada.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaJornada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaJornada.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaJornada.Location = new System.Drawing.Point(15, 124);
            this.lblEstaJornada.Name = "lblEstaJornada";
            this.lblEstaJornada.Size = new System.Drawing.Size(48, 15);
            this.lblEstaJornada.TabIndex = 7;
            this.lblEstaJornada.Text = "Estado:";
            // 
            // lblDescrCajero
            // 
            this.lblDescrCajero.AutoSize = true;
            this.lblDescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrCajero.Location = new System.Drawing.Point(15, 56);
            this.lblDescrCajero.Name = "lblDescrCajero";
            this.lblDescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lblDescrCajero.TabIndex = 5;
            this.lblDescrCajero.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(100, 54);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(216, 20);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblCodigoCajero
            // 
            this.lblCodigoCajero.AutoSize = true;
            this.lblCodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoCajero.Location = new System.Drawing.Point(15, 30);
            this.lblCodigoCajero.Name = "lblCodigoCajero";
            this.lblCodigoCajero.Size = new System.Drawing.Size(49, 15);
            this.lblCodigoCajero.TabIndex = 3;
            this.lblCodigoCajero.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(100, 28);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(216, 20);
            this.txtCodigo.TabIndex = 2;
            // 
            // chkMateriaPrima
            // 
            this.chkMateriaPrima.AutoSize = true;
            this.chkMateriaPrima.Location = new System.Drawing.Point(18, 91);
            this.chkMateriaPrima.Name = "chkMateriaPrima";
            this.chkMateriaPrima.Size = new System.Drawing.Size(122, 17);
            this.chkMateriaPrima.TabIndex = 11;
            this.chkMateriaPrima.Text = "Aplica Materia Prima";
            this.chkMateriaPrima.UseVisualStyleBackColor = true;
            // 
            // chkProductoTerminado
            // 
            this.chkProductoTerminado.AutoSize = true;
            this.chkProductoTerminado.Location = new System.Drawing.Point(161, 91);
            this.chkProductoTerminado.Name = "chkProductoTerminado";
            this.chkProductoTerminado.Size = new System.Drawing.Size(154, 17);
            this.chkProductoTerminado.TabIndex = 20;
            this.chkProductoTerminado.Text = "Aplica Producto Terminado";
            this.chkProductoTerminado.UseVisualStyleBackColor = true;
            // 
            // idJornada
            // 
            this.idJornada.HeaderText = "IDJORNADA";
            this.idJornada.Name = "idJornada";
            this.idJornada.ReadOnly = true;
            this.idJornada.Visible = false;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "CODIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "DESCRIPCION";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 200;
            // 
            // estado1
            // 
            this.estado1.HeaderText = "ESTADO";
            this.estado1.Name = "estado1";
            this.estado1.ReadOnly = true;
            // 
            // materia_prima
            // 
            this.materia_prima.HeaderText = "MATERIA PRIMA";
            this.materia_prima.Name = "materia_prima";
            this.materia_prima.ReadOnly = true;
            this.materia_prima.Visible = false;
            // 
            // producto_terminado
            // 
            this.producto_terminado.HeaderText = "PRODUCTO TERMINADO";
            this.producto_terminado.Name = "producto_terminado";
            this.producto_terminado.ReadOnly = true;
            this.producto_terminado.Visible = false;
            // 
            // frmAdministracionClaseProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(842, 337);
            this.Controls.Add(this.Grb_opciojornada);
            this.Controls.Add(this.Grb_listRejornada);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdministracionClaseProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración de Clases de Producto";
            this.Load += new System.EventHandler(this.frmAdministracionClaseProducto_Load);
            this.Grb_opciojornada.ResumeLayout(false);
            this.Grb_listRejornada.ResumeLayout(false);
            this.Grb_listRejornada.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro)).EndInit();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_opciojornada;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox Grb_listRejornada;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvRegistro;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaJornada;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodigoCajero;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.CheckBox chkProductoTerminado;
        private System.Windows.Forms.CheckBox chkMateriaPrima;
        private System.Windows.Forms.DataGridViewTextBoxColumn idJornada;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado1;
        private System.Windows.Forms.DataGridViewTextBoxColumn materia_prima;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto_terminado;
    }
}