namespace Palatium.Personal
{
    partial class frmInactivarHuellasCajeros
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
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigoCajero = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblClaveAcceso = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.dgvCajero = new System.Windows.Forms.DataGridView();
            this.id_pos_cajero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active_huella = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grb_listReCajero.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajero)).BeginInit();
            this.SuspendLayout();
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(107, 133);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(157, 17);
            this.chkHabilitado.TabIndex = 59;
            this.chkHabilitado.Text = "Habilitar huella dactilar";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEstado.Location = new System.Drawing.Point(107, 92);
            this.txtEstado.MaxLength = 3;
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(132, 20);
            this.txtEstado.TabIndex = 11;
            // 
            // lblDescrCajero
            // 
            this.lblDescrCajero.AutoSize = true;
            this.lblDescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrCajero.Location = new System.Drawing.Point(11, 61);
            this.lblDescrCajero.Name = "lblDescrCajero";
            this.lblDescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lblDescrCajero.TabIndex = 5;
            this.lblDescrCajero.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(107, 60);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(447, 20);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblCodigoCajero
            // 
            this.lblCodigoCajero.AutoSize = true;
            this.lblCodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoCajero.Location = new System.Drawing.Point(11, 29);
            this.lblCodigoCajero.Name = "lblCodigoCajero";
            this.lblCodigoCajero.Size = new System.Drawing.Size(49, 15);
            this.lblCodigoCajero.TabIndex = 3;
            this.lblCodigoCajero.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtCodigo.Location = new System.Drawing.Point(107, 28);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(107, 20);
            this.txtCodigo.TabIndex = 2;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.FillWeight = 30F;
            this.dataGridViewImageColumn2.Frozen = true;
            this.dataGridViewImageColumn2.HeaderText = "Eliminar";
            this.dataGridViewImageColumn2.Image = global::Palatium.Properties.Resources.eliminar_img;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 80;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Blue;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(408, 100);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(70, 50);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Nuevo";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.BackColor = System.Drawing.Color.Transparent;
            this.Grb_listReCajero.Controls.Add(this.dgvCajero);
            this.Grb_listReCajero.Controls.Add(this.btnBuscar);
            this.Grb_listReCajero.Controls.Add(this.txtBuscar);
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
            this.btnBuscar.Location = new System.Drawing.Point(245, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(23, 32);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // grupoDatos
            // 
            this.grupoDatos.BackColor = System.Drawing.Color.Transparent;
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.btnLimpiar);
            this.grupoDatos.Controls.Add(this.lblClaveAcceso);
            this.grupoDatos.Controls.Add(this.btnGuardar);
            this.grupoDatos.Controls.Add(this.txtEstado);
            this.grupoDatos.Controls.Add(this.lblDescrCajero);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblCodigoCajero);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(577, 173);
            this.grupoDatos.TabIndex = 6;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 30F;
            this.dataGridViewImageColumn1.Frozen = true;
            this.dataGridViewImageColumn1.HeaderText = "Ediitar";
            this.dataGridViewImageColumn1.Image = global::Palatium.Properties.Resources.editar_img;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 80;
            // 
            // lblClaveAcceso
            // 
            this.lblClaveAcceso.AutoSize = true;
            this.lblClaveAcceso.BackColor = System.Drawing.Color.Transparent;
            this.lblClaveAcceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClaveAcceso.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblClaveAcceso.Location = new System.Drawing.Point(11, 93);
            this.lblClaveAcceso.Name = "lblClaveAcceso";
            this.lblClaveAcceso.Size = new System.Drawing.Size(87, 15);
            this.lblClaveAcceso.TabIndex = 12;
            this.lblClaveAcceso.Text = "Estado Huella:";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Yellow;
            this.btnLimpiar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.ForeColor = System.Drawing.Color.Black;
            this.btnLimpiar.Location = new System.Drawing.Point(484, 100);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 50);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
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
            this.is_active_huella,
            this.codigo,
            this.descripcion,
            this.estado});
            this.dgvCajero.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCajero.Location = new System.Drawing.Point(21, 58);
            this.dgvCajero.Name = "dgvCajero";
            this.dgvCajero.ReadOnly = true;
            this.dgvCajero.RowHeadersVisible = false;
            this.dgvCajero.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCajero.Size = new System.Drawing.Size(533, 203);
            this.dgvCajero.TabIndex = 5;
            this.dgvCajero.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCajero_CellDoubleClick);
            // 
            // id_pos_cajero
            // 
            this.id_pos_cajero.HeaderText = "ID";
            this.id_pos_cajero.Name = "id_pos_cajero";
            this.id_pos_cajero.ReadOnly = true;
            this.id_pos_cajero.Visible = false;
            // 
            // is_active_huella
            // 
            this.is_active_huella.HeaderText = "HABILITADO";
            this.is_active_huella.Name = "is_active_huella";
            this.is_active_huella.ReadOnly = true;
            this.is_active_huella.Visible = false;
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
            // estado
            // 
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Width = 80;
            // 
            // frmInactivarHuellasCajeros
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(598, 471);
            this.Controls.Add(this.Grb_listReCajero);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInactivarHuellasCajeros";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inactivar huellas de los cajeros";
            this.Load += new System.EventHandler(this.frmInactivarHuellasCajeros_Load);
            this.Grb_listReCajero.ResumeLayout(false);
            this.Grb_listReCajero.PerformLayout();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCajero)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodigoCajero;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label lblClaveAcceso;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvCajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_cajero;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active_huella;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;

    }
}