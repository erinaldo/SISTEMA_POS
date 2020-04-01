namespace Palatium.Formularios
{
    partial class FInformacionPosSecMes
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
            this.Grb_Registros = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Grb_Opciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.Grb_Dato = new System.Windows.Forms.GroupBox();
            this.cmbPaleta = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaSecMes = new System.Windows.Forms.Label();
            this.lblDescrSecMes = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblcodigoSecMesa = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.Grb_Registros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.Grb_Opciones.SuspendLayout();
            this.Grb_Dato.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_Registros
            // 
            this.Grb_Registros.BackColor = System.Drawing.Color.Transparent;
            this.Grb_Registros.Controls.Add(this.btnBuscar);
            this.Grb_Registros.Controls.Add(this.txtBuscar);
            this.Grb_Registros.Controls.Add(this.dgvDatos);
            this.Grb_Registros.Location = new System.Drawing.Point(430, 81);
            this.Grb_Registros.Name = "Grb_Registros";
            this.Grb_Registros.Size = new System.Drawing.Size(430, 272);
            this.Grb_Registros.TabIndex = 5;
            this.Grb_Registros.TabStop = false;
            this.Grb_Registros.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(235, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.Btn_BuscarPosSecMes_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 28);
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
            this.dgvDatos.Location = new System.Drawing.Point(13, 54);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(406, 202);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // Grb_Opciones
            // 
            this.Grb_Opciones.BackColor = System.Drawing.Color.Transparent;
            this.Grb_Opciones.Controls.Add(this.btnCerrar);
            this.Grb_Opciones.Controls.Add(this.btnLimpiar);
            this.Grb_Opciones.Controls.Add(this.btnAnular);
            this.Grb_Opciones.Controls.Add(this.btnNuevo);
            this.Grb_Opciones.Location = new System.Drawing.Point(12, 280);
            this.Grb_Opciones.Name = "Grb_Opciones";
            this.Grb_Opciones.Size = new System.Drawing.Size(412, 73);
            this.Grb_Opciones.TabIndex = 4;
            this.Grb_Opciones.TabStop = false;
            this.Grb_Opciones.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(288, 18);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(72, 39);
            this.btnCerrar.TabIndex = 10;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.Btn_CerrarPosSecMes_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(210, 18);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(72, 39);
            this.btnLimpiar.TabIndex = 9;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.Btn_LimpiarPosSecMes_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Enabled = false;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(132, 18);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(72, 39);
            this.btnAnular.TabIndex = 8;
            this.btnAnular.Text = "Eliminar";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.Btn_AnularPosSecMes_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(54, 18);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(72, 39);
            this.btnNuevo.TabIndex = 7;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevoPosSecMes_Click);
            // 
            // Grb_Dato
            // 
            this.Grb_Dato.BackColor = System.Drawing.Color.Transparent;
            this.Grb_Dato.Controls.Add(this.btnExaminar);
            this.Grb_Dato.Controls.Add(this.label2);
            this.Grb_Dato.Controls.Add(this.cmbPaleta);
            this.Grb_Dato.Controls.Add(this.txtRuta);
            this.Grb_Dato.Controls.Add(this.label1);
            this.Grb_Dato.Controls.Add(this.cmbEstado);
            this.Grb_Dato.Controls.Add(this.lblEstaSecMes);
            this.Grb_Dato.Controls.Add(this.lblDescrSecMes);
            this.Grb_Dato.Controls.Add(this.txtDescripcion);
            this.Grb_Dato.Controls.Add(this.lblcodigoSecMesa);
            this.Grb_Dato.Controls.Add(this.txtCodigo);
            this.Grb_Dato.Enabled = false;
            this.Grb_Dato.Location = new System.Drawing.Point(12, 81);
            this.Grb_Dato.Name = "Grb_Dato";
            this.Grb_Dato.Size = new System.Drawing.Size(412, 193);
            this.Grb_Dato.TabIndex = 3;
            this.Grb_Dato.TabStop = false;
            this.Grb_Dato.Text = "Datos del Registro";
            // 
            // cmbPaleta
            // 
            this.cmbPaleta.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPaleta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaleta.FormattingEnabled = true;
            this.cmbPaleta.Location = new System.Drawing.Point(100, 105);
            this.cmbPaleta.Name = "cmbPaleta";
            this.cmbPaleta.Size = new System.Drawing.Size(152, 21);
            this.cmbPaleta.TabIndex = 5;
            this.cmbPaleta.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbPaleta_DrawItem);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(16, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Color";
            // 
            // cmbEstado
            // 
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(100, 158);
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
            this.lblEstaSecMes.Location = new System.Drawing.Point(16, 160);
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
            this.lblDescrSecMes.Location = new System.Drawing.Point(16, 55);
            this.lblDescrSecMes.Name = "lblDescrSecMes";
            this.lblDescrSecMes.Size = new System.Drawing.Size(75, 15);
            this.lblDescrSecMes.TabIndex = 5;
            this.lblDescrSecMes.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(100, 53);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(290, 44);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblcodigoSecMesa
            // 
            this.lblcodigoSecMesa.AutoSize = true;
            this.lblcodigoSecMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoSecMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoSecMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoSecMesa.Location = new System.Drawing.Point(16, 29);
            this.lblcodigoSecMesa.Name = "lblcodigoSecMesa";
            this.lblcodigoSecMesa.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoSecMesa.TabIndex = 3;
            this.lblcodigoSecMesa.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(100, 27);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(290, 20);
            this.txtCodigo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(16, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ruta imagen:";
            // 
            // txtRuta
            // 
            this.txtRuta.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtRuta.Enabled = false;
            this.txtRuta.Location = new System.Drawing.Point(100, 132);
            this.txtRuta.MaxLength = 20;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(255, 20);
            this.txtRuta.TabIndex = 7;
            // 
            // btnExaminar
            // 
            this.btnExaminar.BackColor = System.Drawing.Color.Yellow;
            this.btnExaminar.Location = new System.Drawing.Point(361, 132);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(29, 21);
            this.btnExaminar.TabIndex = 7;
            this.btnExaminar.Text = "...";
            this.btnExaminar.UseVisualStyleBackColor = false;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // FInformacionPosSecMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(872, 368);
            this.Controls.Add(this.Grb_Registros);
            this.Controls.Add(this.Grb_Opciones);
            this.Controls.Add(this.Grb_Dato);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionPosSecMes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Secciones para Mesas";
            this.Load += new System.EventHandler(this.FInformacionPosSecMes_Load);
            this.Grb_Registros.ResumeLayout(false);
            this.Grb_Registros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.Grb_Opciones.ResumeLayout(false);
            this.Grb_Dato.ResumeLayout(false);
            this.Grb_Dato.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_Registros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox Grb_Opciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox Grb_Dato;
        private System.Windows.Forms.ComboBox cmbPaleta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaSecMes;
        private System.Windows.Forms.Label lblDescrSecMes;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblcodigoSecMesa;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnExaminar;

    }
}