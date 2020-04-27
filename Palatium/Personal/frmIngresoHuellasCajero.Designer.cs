namespace Palatium.Personal
{
    partial class frmIngresoHuellasCajero
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIngresoHuellasCajero));
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvCajero = new System.Windows.Forms.DataGridView();
            this.id_pos_cajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoCajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnVerificar = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.btnRemoverRegistro = new System.Windows.Forms.Button();
            this.txtMensajes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.imgHuellaCapturada = new System.Windows.Forms.PictureBox();
            this.lblCodigoCajero = new System.Windows.Forms.Label();
            this.txtNombreCajero = new System.Windows.Forms.TextBox();
            this.txtBase64_1 = new System.Windows.Forms.TextBox();
            this.Grb_listReCajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajero)).BeginInit();
            this.grupoDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHuellaCapturada)).BeginInit();
            this.SuspendLayout();
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.BackColor = System.Drawing.Color.Transparent;
            this.Grb_listReCajero.Controls.Add(this.btnBuscar);
            this.Grb_listReCajero.Controls.Add(this.txtBuscar);
            this.Grb_listReCajero.Controls.Add(this.dgvCajero);
            this.Grb_listReCajero.Location = new System.Drawing.Point(12, 191);
            this.Grb_listReCajero.Name = "Grb_listReCajero";
            this.Grb_listReCajero.Size = new System.Drawing.Size(574, 267);
            this.Grb_listReCajero.TabIndex = 8;
            this.Grb_listReCajero.TabStop = false;
            this.Grb_listReCajero.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(240, 24);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(18, 28);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // dgvCajero
            // 
            this.dgvCajero.AllowUserToAddRows = false;
            this.dgvCajero.AllowUserToDeleteRows = false;
            this.dgvCajero.AllowUserToResizeColumns = false;
            this.dgvCajero.AllowUserToResizeRows = false;
            this.dgvCajero.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvCajero.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCajero.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pos_cajero,
            this.is_active,
            this.codigo,
            this.descripcion,
            this.estadoCajero});
            this.dgvCajero.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCajero.Location = new System.Drawing.Point(18, 54);
            this.dgvCajero.Name = "dgvCajero";
            this.dgvCajero.ReadOnly = true;
            this.dgvCajero.RowHeadersVisible = false;
            this.dgvCajero.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCajero.Size = new System.Drawing.Size(533, 203);
            this.dgvCajero.TabIndex = 0;
            this.dgvCajero.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCajero_CellDoubleClick);
            // 
            // id_pos_cajero
            // 
            this.id_pos_cajero.HeaderText = "ID";
            this.id_pos_cajero.Name = "id_pos_cajero";
            this.id_pos_cajero.ReadOnly = true;
            this.id_pos_cajero.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "HABILITADO";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "CÓDIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            // 
            // descripcion
            // 
            this.descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descripcion.HeaderText = "DESCRIPCIÓN";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            // 
            // estadoCajero
            // 
            this.estadoCajero.HeaderText = "ESTADO";
            this.estadoCajero.Name = "estadoCajero";
            this.estadoCajero.ReadOnly = true;
            this.estadoCajero.Width = 80;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(257, 108);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 50);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(333, 108);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(70, 50);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.Red;
            this.btnRemover.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRemover.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRemover.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.Location = new System.Drawing.Point(105, 108);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(70, 50);
            this.btnRemover.TabIndex = 1;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnVerificar
            // 
            this.btnVerificar.BackColor = System.Drawing.Color.Blue;
            this.btnVerificar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnVerificar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnVerificar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnVerificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerificar.ForeColor = System.Drawing.Color.White;
            this.btnVerificar.Location = new System.Drawing.Point(181, 108);
            this.btnVerificar.Name = "btnVerificar";
            this.btnVerificar.Size = new System.Drawing.Size(70, 50);
            this.btnVerificar.TabIndex = 0;
            this.btnVerificar.Text = "Verificar";
            this.btnVerificar.UseVisualStyleBackColor = false;
            this.btnVerificar.Click += new System.EventHandler(this.btnVerificar_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.BackColor = System.Drawing.Color.Transparent;
            this.grupoDatos.Controls.Add(this.btnRemoverRegistro);
            this.grupoDatos.Controls.Add(this.txtMensajes);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.imgHuellaCapturada);
            this.grupoDatos.Controls.Add(this.btnLimpiar);
            this.grupoDatos.Controls.Add(this.lblCodigoCajero);
            this.grupoDatos.Controls.Add(this.btnGuardar);
            this.grupoDatos.Controls.Add(this.txtNombreCajero);
            this.grupoDatos.Controls.Add(this.btnRemover);
            this.grupoDatos.Controls.Add(this.btnVerificar);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(577, 173);
            this.grupoDatos.TabIndex = 6;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // btnRemoverRegistro
            // 
            this.btnRemoverRegistro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnRemoverRegistro.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnRemoverRegistro.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRemoverRegistro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRemoverRegistro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoverRegistro.ForeColor = System.Drawing.Color.White;
            this.btnRemoverRegistro.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoverRegistro.Image")));
            this.btnRemoverRegistro.Location = new System.Drawing.Point(535, 134);
            this.btnRemoverRegistro.Name = "btnRemoverRegistro";
            this.btnRemoverRegistro.Size = new System.Drawing.Size(36, 33);
            this.btnRemoverRegistro.TabIndex = 57;
            this.btnRemoverRegistro.UseVisualStyleBackColor = false;
            this.btnRemoverRegistro.Click += new System.EventHandler(this.btnRemoverRegistro_Click);
            // 
            // txtMensajes
            // 
            this.txtMensajes.Location = new System.Drawing.Point(106, 54);
            this.txtMensajes.Multiline = true;
            this.txtMensajes.Name = "txtMensajes";
            this.txtMensajes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMensajes.Size = new System.Drawing.Size(305, 46);
            this.txtMensajes.TabIndex = 56;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(15, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 55;
            this.label4.Text = "Mensajes:";
            // 
            // imgHuellaCapturada
            // 
            this.imgHuellaCapturada.BackColor = System.Drawing.SystemColors.Window;
            this.imgHuellaCapturada.Location = new System.Drawing.Point(438, 9);
            this.imgHuellaCapturada.Name = "imgHuellaCapturada";
            this.imgHuellaCapturada.Size = new System.Drawing.Size(133, 158);
            this.imgHuellaCapturada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imgHuellaCapturada.TabIndex = 47;
            this.imgHuellaCapturada.TabStop = false;
            // 
            // lblCodigoCajero
            // 
            this.lblCodigoCajero.AutoSize = true;
            this.lblCodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoCajero.Location = new System.Drawing.Point(11, 30);
            this.lblCodigoCajero.Name = "lblCodigoCajero";
            this.lblCodigoCajero.Size = new System.Drawing.Size(94, 15);
            this.lblCodigoCajero.TabIndex = 3;
            this.lblCodigoCajero.Text = "Nombre Cajero:";
            // 
            // txtNombreCajero
            // 
            this.txtNombreCajero.Location = new System.Drawing.Point(107, 28);
            this.txtNombreCajero.MaxLength = 20;
            this.txtNombreCajero.Name = "txtNombreCajero";
            this.txtNombreCajero.Size = new System.Drawing.Size(304, 20);
            this.txtNombreCajero.TabIndex = 2;
            // 
            // txtBase64_1
            // 
            this.txtBase64_1.Location = new System.Drawing.Point(620, 12);
            this.txtBase64_1.Multiline = true;
            this.txtBase64_1.Name = "txtBase64_1";
            this.txtBase64_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBase64_1.Size = new System.Drawing.Size(385, 237);
            this.txtBase64_1.TabIndex = 53;
            this.txtBase64_1.Visible = false;
            // 
            // frmIngresoHuellasCajero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(598, 471);
            this.Controls.Add(this.txtBase64_1);
            this.Controls.Add(this.Grb_listReCajero);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIngresoHuellasCajero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar Huellas de los Cajeros";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIngresoHuellasCajero_FormClosing);
            this.Load += new System.EventHandler(this.frmIngresoHuellasCajero_Load);
            this.Grb_listReCajero.ResumeLayout(false);
            this.Grb_listReCajero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajero)).EndInit();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgHuellaCapturada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvCajero;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnVerificar;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblCodigoCajero;
        private System.Windows.Forms.TextBox txtNombreCajero;
        private System.Windows.Forms.PictureBox imgHuellaCapturada;
        private System.Windows.Forms.Button btnRemoverRegistro;
        private System.Windows.Forms.TextBox txtMensajes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBase64_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_cajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoCajero;
    }
}