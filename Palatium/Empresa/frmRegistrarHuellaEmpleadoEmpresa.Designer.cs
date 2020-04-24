namespace Palatium.Empresa
{
    partial class frmRegistrarHuellaEmpleadoEmpresa
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
            this.label16 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEmpresas = new MisControles.ComboDatos();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.grupoRegistros = new System.Windows.Forms.GroupBox();
            this.grupoHuella = new System.Windows.Forms.GroupBox();
            this.imagenHuellas = new System.Windows.Forms.PictureBox();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.deviceSerial = new System.Windows.Forms.Label();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtBase64_1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoDatos.SuspendLayout();
            this.grupoRegistros.SuspendLayout();
            this.grupoHuella.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagenHuellas)).BeginInit();
            this.SuspendLayout();
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 449);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "N° de Registros:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_ico;
            this.btnBuscar.Location = new System.Drawing.Point(406, 49);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(34, 27);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Location = new System.Drawing.Point(135, 53);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(265, 20);
            this.txtBuscar.TabIndex = 4;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(10, 88);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(435, 344);
            this.dgvDatos.TabIndex = 21;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Empresa:";
            // 
            // cmbEmpresas
            // 
            this.cmbEmpresas.FormattingEnabled = true;
            this.cmbEmpresas.Location = new System.Drawing.Point(135, 19);
            this.cmbEmpresas.Name = "cmbEmpresas";
            this.cmbEmpresas.Size = new System.Drawing.Size(265, 21);
            this.cmbEmpresas.TabIndex = 27;
            this.cmbEmpresas.SelectedIndexChanged += new System.EventHandler(this.cmbEmpresas_SelectedIndexChanged);
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.Location = new System.Drawing.Point(121, 449);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(155, 16);
            this.lblRegistros.TabIndex = 0;
            this.lblRegistros.Text = "0 Registros Encontrados";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(6, 24);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(192, 82);
            this.txtDescripcion.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Búsqueda:";
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grupoDatos.Location = new System.Drawing.Point(480, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(212, 117);
            this.grupoDatos.TabIndex = 40;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Empleado";
            // 
            // grupoRegistros
            // 
            this.grupoRegistros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoRegistros.Controls.Add(this.label1);
            this.grupoRegistros.Controls.Add(this.cmbEmpresas);
            this.grupoRegistros.Controls.Add(this.lblRegistros);
            this.grupoRegistros.Controls.Add(this.label16);
            this.grupoRegistros.Controls.Add(this.btnBuscar);
            this.grupoRegistros.Controls.Add(this.txtBuscar);
            this.grupoRegistros.Controls.Add(this.label2);
            this.grupoRegistros.Controls.Add(this.dgvDatos);
            this.grupoRegistros.Location = new System.Drawing.Point(12, 12);
            this.grupoRegistros.Name = "grupoRegistros";
            this.grupoRegistros.Size = new System.Drawing.Size(458, 477);
            this.grupoRegistros.TabIndex = 39;
            this.grupoRegistros.TabStop = false;
            // 
            // grupoHuella
            // 
            this.grupoHuella.Controls.Add(this.imagenHuellas);
            this.grupoHuella.Enabled = false;
            this.grupoHuella.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grupoHuella.Location = new System.Drawing.Point(480, 135);
            this.grupoHuella.Name = "grupoHuella";
            this.grupoHuella.Size = new System.Drawing.Size(212, 241);
            this.grupoHuella.TabIndex = 44;
            this.grupoHuella.TabStop = false;
            this.grupoHuella.Text = "Huella Dactilar";
            // 
            // imagenHuellas
            // 
            this.imagenHuellas.BackColor = System.Drawing.SystemColors.Window;
            this.imagenHuellas.Location = new System.Drawing.Point(15, 23);
            this.imagenHuellas.Name = "imagenHuellas";
            this.imagenHuellas.Size = new System.Drawing.Size(183, 207);
            this.imagenHuellas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imagenHuellas.TabIndex = 45;
            this.imagenHuellas.TabStop = false;
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.Red;
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.Location = new System.Drawing.Point(599, 382);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(93, 39);
            this.btnRemover.TabIndex = 49;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Visible = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Gold;
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(599, 427);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(93, 39);
            this.btnLimpiar.TabIndex = 50;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnVerificar
            // 
            this.btnVerificar.BackColor = System.Drawing.Color.Blue;
            this.btnVerificar.Enabled = false;
            this.btnVerificar.ForeColor = System.Drawing.Color.White;
            this.btnVerificar.Location = new System.Drawing.Point(480, 382);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(93, 39);
            this.btnVerificar.TabIndex = 48;
            this.btnVerificar.Text = "Verificar";
            this.btnVerificar.UseVisualStyleBackColor = false;
            this.btnVerificar.Visible = false;
            this.btnVerificar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // deviceSerial
            // 
            this.deviceSerial.Location = new System.Drawing.Point(705, 447);
            this.deviceSerial.Name = "deviceSerial";
            this.deviceSerial.Size = new System.Drawing.Size(257, 30);
            this.deviceSerial.TabIndex = 52;
            this.deviceSerial.Text = "Device Serial: ";
            // 
            // txtMensajes
            // 
            this.txtMensajes.Location = new System.Drawing.Point(700, 17);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensajes.Size = new System.Drawing.Size(254, 407);
            this.txtMensajes.TabIndex = 53;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.Enabled = false;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(480, 427);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(93, 39);
            this.btnGuardar.TabIndex = 54;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Visible = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtBase64_1
            // 
            this.txtBase64_1.Location = new System.Drawing.Point(12, 495);
            this.txtBase64_1.Multiline = true;
            this.txtBase64_1.Name = "txtBase64_1";
            this.txtBase64_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBase64_1.Size = new System.Drawing.Size(950, 48);
            this.txtBase64_1.TabIndex = 51;
            // 
            // frmRegistrarHuellaEmpleadoEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(962, 492);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtMensajes);
            this.Controls.Add(this.deviceSerial);
            this.Controls.Add(this.btnVerificar);
            this.Controls.Add(this.txtBase64_1);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.grupoHuella);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.grupoRegistros);
            this.MaximizeBox = false;
            this.Name = "frmRegistrarHuellaEmpleadoEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Huellas Dactilares - Empleado de Cliente Empresarial";
            this.Load += new System.EventHandler(this.frmRegistrarHuellaEmpleadoEmpresa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoRegistros.ResumeLayout(false);
            this.grupoRegistros.PerformLayout();
            this.grupoHuella.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imagenHuellas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Label label1;
        private MisControles.ComboDatos cmbEmpresas;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.GroupBox grupoRegistros;
        private System.Windows.Forms.GroupBox grupoHuella;
        private System.Windows.Forms.PictureBox imagenHuellas;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.Label deviceSerial;
        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox txtBase64_1;
    }
}