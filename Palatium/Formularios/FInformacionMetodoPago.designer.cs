namespace Palatium.Formularios
{
    partial class FInformacionMetodoPago
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
            this.Grb_listReMetodoPago = new System.Windows.Forms.GroupBox();
            this.btnBuscarMetodoPago = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Grb_opcioMetodoPago = new System.Windows.Forms.GroupBox();
            this.btnCerrarMetodoPago = new System.Windows.Forms.Button();
            this.btnLimpiarMetodoPago = new System.Windows.Forms.Button();
            this.btnAnularMetodoPago = new System.Windows.Forms.Button();
            this.btnNuevoMetodoPago = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFormasPagos = new MisControles.ComboDatos();
            this.lblDescrMetodoPago = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodigoMetodoPago = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.Grb_listReMetodoPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.Grb_opcioMetodoPago.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_listReMetodoPago
            // 
            this.Grb_listReMetodoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listReMetodoPago.Controls.Add(this.btnBuscarMetodoPago);
            this.Grb_listReMetodoPago.Controls.Add(this.txtBuscar);
            this.Grb_listReMetodoPago.Controls.Add(this.dgvDatos);
            this.Grb_listReMetodoPago.Location = new System.Drawing.Point(404, 12);
            this.Grb_listReMetodoPago.Name = "Grb_listReMetodoPago";
            this.Grb_listReMetodoPago.Size = new System.Drawing.Size(560, 276);
            this.Grb_listReMetodoPago.TabIndex = 5;
            this.Grb_listReMetodoPago.TabStop = false;
            this.Grb_listReMetodoPago.Text = "Lista de Registros";
            // 
            // btnBuscarMetodoPago
            // 
            this.btnBuscarMetodoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarMetodoPago.ForeColor = System.Drawing.Color.White;
            this.btnBuscarMetodoPago.Location = new System.Drawing.Point(235, 24);
            this.btnBuscarMetodoPago.Name = "btnBuscarMetodoPago";
            this.btnBuscarMetodoPago.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarMetodoPago.TabIndex = 4;
            this.btnBuscarMetodoPago.Text = "Buscar";
            this.btnBuscarMetodoPago.UseVisualStyleBackColor = false;
            this.btnBuscarMetodoPago.Click += new System.EventHandler(this.btnBuscarMetodoPago_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Location = new System.Drawing.Point(13, 28);
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
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(541, 203);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // Grb_opcioMetodoPago
            // 
            this.Grb_opcioMetodoPago.Controls.Add(this.btnCerrarMetodoPago);
            this.Grb_opcioMetodoPago.Controls.Add(this.btnLimpiarMetodoPago);
            this.Grb_opcioMetodoPago.Controls.Add(this.btnAnularMetodoPago);
            this.Grb_opcioMetodoPago.Controls.Add(this.btnNuevoMetodoPago);
            this.Grb_opcioMetodoPago.Location = new System.Drawing.Point(12, 199);
            this.Grb_opcioMetodoPago.Name = "Grb_opcioMetodoPago";
            this.Grb_opcioMetodoPago.Size = new System.Drawing.Size(386, 89);
            this.Grb_opcioMetodoPago.TabIndex = 4;
            this.Grb_opcioMetodoPago.TabStop = false;
            this.Grb_opcioMetodoPago.Text = "Opciones";
            // 
            // btnCerrarMetodoPago
            // 
            this.btnCerrarMetodoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarMetodoPago.ForeColor = System.Drawing.Color.White;
            this.btnCerrarMetodoPago.Location = new System.Drawing.Point(275, 19);
            this.btnCerrarMetodoPago.Name = "btnCerrarMetodoPago";
            this.btnCerrarMetodoPago.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarMetodoPago.TabIndex = 3;
            this.btnCerrarMetodoPago.Text = "Cerrar";
            this.btnCerrarMetodoPago.UseVisualStyleBackColor = false;
            this.btnCerrarMetodoPago.Click += new System.EventHandler(this.btnCerrarMetodoPago_Click);
            // 
            // btnLimpiarMetodoPago
            // 
            this.btnLimpiarMetodoPago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarMetodoPago.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarMetodoPago.Location = new System.Drawing.Point(199, 19);
            this.btnLimpiarMetodoPago.Name = "btnLimpiarMetodoPago";
            this.btnLimpiarMetodoPago.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiarMetodoPago.TabIndex = 2;
            this.btnLimpiarMetodoPago.Text = "Limpiar";
            this.btnLimpiarMetodoPago.UseVisualStyleBackColor = false;
            this.btnLimpiarMetodoPago.Click += new System.EventHandler(this.btnLimpiarMetodoPago_Click);
            // 
            // btnAnularMetodoPago
            // 
            this.btnAnularMetodoPago.BackColor = System.Drawing.Color.Red;
            this.btnAnularMetodoPago.ForeColor = System.Drawing.Color.White;
            this.btnAnularMetodoPago.Location = new System.Drawing.Point(123, 19);
            this.btnAnularMetodoPago.Name = "btnAnularMetodoPago";
            this.btnAnularMetodoPago.Size = new System.Drawing.Size(70, 39);
            this.btnAnularMetodoPago.TabIndex = 1;
            this.btnAnularMetodoPago.Text = "Anular";
            this.btnAnularMetodoPago.UseVisualStyleBackColor = false;
            this.btnAnularMetodoPago.Click += new System.EventHandler(this.btnAnularMetodoPago_Click);
            // 
            // btnNuevoMetodoPago
            // 
            this.btnNuevoMetodoPago.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoMetodoPago.ForeColor = System.Drawing.Color.White;
            this.btnNuevoMetodoPago.Location = new System.Drawing.Point(47, 19);
            this.btnNuevoMetodoPago.Name = "btnNuevoMetodoPago";
            this.btnNuevoMetodoPago.Size = new System.Drawing.Size(70, 39);
            this.btnNuevoMetodoPago.TabIndex = 0;
            this.btnNuevoMetodoPago.Text = "Nuevo";
            this.btnNuevoMetodoPago.UseVisualStyleBackColor = false;
            this.btnNuevoMetodoPago.Click += new System.EventHandler(this.btnNuevoMetodoPago_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.cmbFormasPagos);
            this.grupoDatos.Controls.Add(this.lblDescrMetodoPago);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblCodigoMetodoPago);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(386, 171);
            this.grupoDatos.TabIndex = 3;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(27, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "SRI Forma Pago:";
            // 
            // cmbFormasPagos
            // 
            this.cmbFormasPagos.FormattingEnabled = true;
            this.cmbFormasPagos.Location = new System.Drawing.Point(139, 90);
            this.cmbFormasPagos.Name = "cmbFormasPagos";
            this.cmbFormasPagos.Size = new System.Drawing.Size(193, 21);
            this.cmbFormasPagos.TabIndex = 13;
            // 
            // lblDescrMetodoPago
            // 
            this.lblDescrMetodoPago.AutoSize = true;
            this.lblDescrMetodoPago.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrMetodoPago.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrMetodoPago.Location = new System.Drawing.Point(27, 64);
            this.lblDescrMetodoPago.Name = "lblDescrMetodoPago";
            this.lblDescrMetodoPago.Size = new System.Drawing.Size(75, 15);
            this.lblDescrMetodoPago.TabIndex = 5;
            this.lblDescrMetodoPago.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(139, 63);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(193, 20);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblCodigoMetodoPago
            // 
            this.lblCodigoMetodoPago.AutoSize = true;
            this.lblCodigoMetodoPago.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoMetodoPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoMetodoPago.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoMetodoPago.Location = new System.Drawing.Point(27, 38);
            this.lblCodigoMetodoPago.Name = "lblCodigoMetodoPago";
            this.lblCodigoMetodoPago.Size = new System.Drawing.Size(49, 15);
            this.lblCodigoMetodoPago.TabIndex = 3;
            this.lblCodigoMetodoPago.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(139, 37);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(146, 20);
            this.txtCodigo.TabIndex = 2;
            this.txtCodigo.Leave += new System.EventHandler(this.txtCodigoCajero_Leave);
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(139, 127);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(83, 17);
            this.chkHabilitado.TabIndex = 60;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // FInformacionMetodoPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(976, 295);
            this.Controls.Add(this.Grb_listReMetodoPago);
            this.Controls.Add(this.Grb_opcioMetodoPago);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionMetodoPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Métodos de Pago";
            this.Load += new System.EventHandler(this.FInformacionMetodoPago_Load);
            this.Grb_listReMetodoPago.ResumeLayout(false);
            this.Grb_listReMetodoPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.Grb_opcioMetodoPago.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReMetodoPago;
        private System.Windows.Forms.Button btnBuscarMetodoPago;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox Grb_opcioMetodoPago;
        private System.Windows.Forms.Button btnCerrarMetodoPago;
        private System.Windows.Forms.Button btnLimpiarMetodoPago;
        private System.Windows.Forms.Button btnAnularMetodoPago;
        private System.Windows.Forms.Button btnNuevoMetodoPago;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblDescrMetodoPago;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodigoMetodoPago;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private MisControles.ComboDatos cmbFormasPagos;
        private System.Windows.Forms.CheckBox chkHabilitado;
    }
}