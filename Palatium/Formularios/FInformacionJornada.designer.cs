namespace Palatium.Formularios
{
    partial class FInformacionJornada
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
            this.tabPag_jornada = new System.Windows.Forms.TabPage();
            this.Grb_listRejornada = new System.Windows.Forms.GroupBox();
            this.btnBuscarjornada = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Grb_opciojornada = new System.Windows.Forms.GroupBox();
            this.btnCerrarjornada = new System.Windows.Forms.Button();
            this.btnLimpiarjornada = new System.Windows.Forms.Button();
            this.btnAnularjornada = new System.Windows.Forms.Button();
            this.btnNuevojornada = new System.Windows.Forms.Button();
            this.Grb_Datojornada = new System.Windows.Forms.GroupBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaJornada = new System.Windows.Forms.Label();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigoCajero = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.tabCon_jornada = new System.Windows.Forms.TabControl();
            this.tabPag_jornada.SuspendLayout();
            this.Grb_listRejornada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.Grb_opciojornada.SuspendLayout();
            this.Grb_Datojornada.SuspendLayout();
            this.tabCon_jornada.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPag_jornada
            // 
            this.tabPag_jornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPag_jornada.Controls.Add(this.Grb_listRejornada);
            this.tabPag_jornada.Controls.Add(this.Grb_opciojornada);
            this.tabPag_jornada.Controls.Add(this.Grb_Datojornada);
            this.tabPag_jornada.Location = new System.Drawing.Point(4, 22);
            this.tabPag_jornada.Name = "tabPag_jornada";
            this.tabPag_jornada.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_jornada.Size = new System.Drawing.Size(835, 268);
            this.tabPag_jornada.TabIndex = 0;
            this.tabPag_jornada.Text = "Módulo jornada";
            // 
            // Grb_listRejornada
            // 
            this.Grb_listRejornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listRejornada.Controls.Add(this.btnBuscarjornada);
            this.Grb_listRejornada.Controls.Add(this.txtBuscar);
            this.Grb_listRejornada.Controls.Add(this.dgvDatos);
            this.Grb_listRejornada.Location = new System.Drawing.Point(371, 19);
            this.Grb_listRejornada.Name = "Grb_listRejornada";
            this.Grb_listRejornada.Size = new System.Drawing.Size(457, 238);
            this.Grb_listRejornada.TabIndex = 5;
            this.Grb_listRejornada.TabStop = false;
            this.Grb_listRejornada.Text = "Lista de Registros";
            // 
            // btnBuscarjornada
            // 
            this.btnBuscarjornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarjornada.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscarjornada.Location = new System.Drawing.Point(235, 25);
            this.btnBuscarjornada.Name = "btnBuscarjornada";
            this.btnBuscarjornada.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarjornada.TabIndex = 4;
            this.btnBuscarjornada.Text = "Buscar";
            this.btnBuscarjornada.UseVisualStyleBackColor = false;
            this.btnBuscarjornada.Click += new System.EventHandler(this.Btn_Buscarjornada_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 29);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(427, 162);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // Grb_opciojornada
            // 
            this.Grb_opciojornada.Controls.Add(this.btnCerrarjornada);
            this.Grb_opciojornada.Controls.Add(this.btnLimpiarjornada);
            this.Grb_opciojornada.Controls.Add(this.btnAnularjornada);
            this.Grb_opciojornada.Controls.Add(this.btnNuevojornada);
            this.Grb_opciojornada.Location = new System.Drawing.Point(17, 176);
            this.Grb_opciojornada.Name = "Grb_opciojornada";
            this.Grb_opciojornada.Size = new System.Drawing.Size(342, 81);
            this.Grb_opciojornada.TabIndex = 4;
            this.Grb_opciojornada.TabStop = false;
            this.Grb_opciojornada.Text = "Opciones";
            // 
            // btnCerrarjornada
            // 
            this.btnCerrarjornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarjornada.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrarjornada.Location = new System.Drawing.Point(240, 27);
            this.btnCerrarjornada.Name = "btnCerrarjornada";
            this.btnCerrarjornada.Size = new System.Drawing.Size(64, 39);
            this.btnCerrarjornada.TabIndex = 3;
            this.btnCerrarjornada.Text = "Cerrar";
            this.btnCerrarjornada.UseVisualStyleBackColor = false;
            this.btnCerrarjornada.Click += new System.EventHandler(this.Btn_Cerrarjornada_Click);
            // 
            // btnLimpiarjornada
            // 
            this.btnLimpiarjornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarjornada.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiarjornada.Location = new System.Drawing.Point(170, 27);
            this.btnLimpiarjornada.Name = "btnLimpiarjornada";
            this.btnLimpiarjornada.Size = new System.Drawing.Size(64, 39);
            this.btnLimpiarjornada.TabIndex = 2;
            this.btnLimpiarjornada.Text = "Limpiar";
            this.btnLimpiarjornada.UseVisualStyleBackColor = false;
            this.btnLimpiarjornada.Click += new System.EventHandler(this.Btn_Limpiarjornada_Click);
            // 
            // btnAnularjornada
            // 
            this.btnAnularjornada.BackColor = System.Drawing.Color.Red;
            this.btnAnularjornada.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnularjornada.Location = new System.Drawing.Point(100, 27);
            this.btnAnularjornada.Name = "btnAnularjornada";
            this.btnAnularjornada.Size = new System.Drawing.Size(64, 39);
            this.btnAnularjornada.TabIndex = 1;
            this.btnAnularjornada.Text = "Anular";
            this.btnAnularjornada.UseVisualStyleBackColor = false;
            this.btnAnularjornada.Click += new System.EventHandler(this.Btn_Anularjornada_Click);
            // 
            // btnNuevojornada
            // 
            this.btnNuevojornada.BackColor = System.Drawing.Color.Blue;
            this.btnNuevojornada.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevojornada.Location = new System.Drawing.Point(30, 27);
            this.btnNuevojornada.Name = "btnNuevojornada";
            this.btnNuevojornada.Size = new System.Drawing.Size(64, 39);
            this.btnNuevojornada.TabIndex = 0;
            this.btnNuevojornada.Text = "Nuevo";
            this.btnNuevojornada.UseVisualStyleBackColor = false;
            this.btnNuevojornada.Click += new System.EventHandler(this.BtnNuevojornada_Click);
            // 
            // Grb_Datojornada
            // 
            this.Grb_Datojornada.Controls.Add(this.cmbEstado);
            this.Grb_Datojornada.Controls.Add(this.lblEstaJornada);
            this.Grb_Datojornada.Controls.Add(this.lblDescrCajero);
            this.Grb_Datojornada.Controls.Add(this.txtDescripcion);
            this.Grb_Datojornada.Controls.Add(this.lblCodigoCajero);
            this.Grb_Datojornada.Controls.Add(this.txtCodigo);
            this.Grb_Datojornada.Enabled = false;
            this.Grb_Datojornada.Location = new System.Drawing.Point(17, 19);
            this.Grb_Datojornada.Name = "Grb_Datojornada";
            this.Grb_Datojornada.Size = new System.Drawing.Size(342, 151);
            this.Grb_Datojornada.TabIndex = 3;
            this.Grb_Datojornada.TabStop = false;
            this.Grb_Datojornada.Text = "Datos del Registro";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(100, 106);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(134, 21);
            this.cmbEstado.TabIndex = 10;
            // 
            // lblEstaJornada
            // 
            this.lblEstaJornada.AutoSize = true;
            this.lblEstaJornada.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaJornada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaJornada.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaJornada.Location = new System.Drawing.Point(15, 106);
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
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(216, 44);
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
            this.txtCodigo.Leave += new System.EventHandler(this.txtCodigo_Leave);
            // 
            // tabCon_jornada
            // 
            this.tabCon_jornada.Controls.Add(this.tabPag_jornada);
            this.tabCon_jornada.Location = new System.Drawing.Point(-4, 1);
            this.tabCon_jornada.Name = "tabCon_jornada";
            this.tabCon_jornada.SelectedIndex = 0;
            this.tabCon_jornada.Size = new System.Drawing.Size(843, 294);
            this.tabCon_jornada.TabIndex = 2;
            // 
            // FInformacionJornada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(840, 293);
            this.Controls.Add(this.tabCon_jornada);
            this.MaximizeBox = false;
            this.Name = "FInformacionJornada";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Jornadas";
            this.Load += new System.EventHandler(this.FInformacionJornada_Load);
            this.tabPag_jornada.ResumeLayout(false);
            this.Grb_listRejornada.ResumeLayout(false);
            this.Grb_listRejornada.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.Grb_opciojornada.ResumeLayout(false);
            this.Grb_Datojornada.ResumeLayout(false);
            this.Grb_Datojornada.PerformLayout();
            this.tabCon_jornada.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPag_jornada;
        private System.Windows.Forms.GroupBox Grb_listRejornada;
        private System.Windows.Forms.Button btnBuscarjornada;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox Grb_opciojornada;
        private System.Windows.Forms.Button btnCerrarjornada;
        private System.Windows.Forms.Button btnLimpiarjornada;
        private System.Windows.Forms.Button btnAnularjornada;
        private System.Windows.Forms.Button btnNuevojornada;
        private System.Windows.Forms.GroupBox Grb_Datojornada;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaJornada;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodigoCajero;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TabControl tabCon_jornada;

    }
}