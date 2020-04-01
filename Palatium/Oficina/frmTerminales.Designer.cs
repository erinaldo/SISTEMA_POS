namespace Palatium.Oficina
{
    partial class frmTerminales
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
            this.components = new System.ComponentModel.Container();
            this.Grb_listReCanImpre = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.Grb_opcioCanImpre = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.btnExtraerIpAsignada = new System.Windows.Forms.Button();
            this.btnExtraerNombreEquipo = new System.Windows.Forms.Button();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.label6 = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtIPAsignada = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNombreEquipo = new System.Windows.Forms.TextBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaCajero = new System.Windows.Forms.Label();
            this.lbldescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblcodigoCajero = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.Grb_listReCanImpre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.Grb_opcioCanImpre.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_listReCanImpre
            // 
            this.Grb_listReCanImpre.BackColor = System.Drawing.Color.Transparent;
            this.Grb_listReCanImpre.Controls.Add(this.btnBuscar);
            this.Grb_listReCanImpre.Controls.Add(this.txtBusqueda);
            this.Grb_listReCanImpre.Controls.Add(this.dgvDatos);
            this.Grb_listReCanImpre.Location = new System.Drawing.Point(387, 77);
            this.Grb_listReCanImpre.Name = "Grb_listReCanImpre";
            this.Grb_listReCanImpre.Size = new System.Drawing.Size(627, 286);
            this.Grb_listReCanImpre.TabIndex = 8;
            this.Grb_listReCanImpre.TabStop = false;
            this.Grb_listReCanImpre.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(235, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(13, 28);
            this.txtBusqueda.MaxLength = 20;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(216, 20);
            this.txtBusqueda.TabIndex = 1;
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(598, 210);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellClick);
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // Grb_opcioCanImpre
            // 
            this.Grb_opcioCanImpre.BackColor = System.Drawing.Color.Transparent;
            this.Grb_opcioCanImpre.Controls.Add(this.btnLimpiar);
            this.Grb_opcioCanImpre.Controls.Add(this.btnEliminar);
            this.Grb_opcioCanImpre.Controls.Add(this.btnNuevo);
            this.Grb_opcioCanImpre.Location = new System.Drawing.Point(12, 288);
            this.Grb_opcioCanImpre.Name = "Grb_opcioCanImpre";
            this.Grb_opcioCanImpre.Size = new System.Drawing.Size(360, 75);
            this.Grb_opcioCanImpre.TabIndex = 7;
            this.Grb_opcioCanImpre.TabStop = false;
            this.Grb_opcioCanImpre.Text = "Opciones";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Lime;
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(211, 21);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(63, 39);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(142, 21);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(63, 39);
            this.btnEliminar.TabIndex = 12;
            this.btnEliminar.Text = "Anular";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(73, 21);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(63, 39);
            this.btnNuevo.TabIndex = 11;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevoCanImpre_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.BackColor = System.Drawing.Color.Transparent;
            this.grupoDatos.Controls.Add(this.btnExtraerIpAsignada);
            this.grupoDatos.Controls.Add(this.btnExtraerNombreEquipo);
            this.grupoDatos.Controls.Add(this.cmbLocalidad);
            this.grupoDatos.Controls.Add(this.label6);
            this.grupoDatos.Controls.Add(this.lblId);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.TxtIPAsignada);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.txtNombreEquipo);
            this.grupoDatos.Controls.Add(this.cmbEstado);
            this.grupoDatos.Controls.Add(this.lblEstaCajero);
            this.grupoDatos.Controls.Add(this.lbldescrCajero);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblcodigoCajero);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 77);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(360, 205);
            this.grupoDatos.TabIndex = 6;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // btnExtraerIpAsignada
            // 
            this.btnExtraerIpAsignada.BackColor = System.Drawing.Color.DeepPink;
            this.btnExtraerIpAsignada.Location = new System.Drawing.Point(317, 142);
            this.btnExtraerIpAsignada.Name = "btnExtraerIpAsignada";
            this.btnExtraerIpAsignada.Size = new System.Drawing.Size(25, 20);
            this.btnExtraerIpAsignada.TabIndex = 25;
            this.btnExtraerIpAsignada.Text = "...";
            this.ttMensaje.SetToolTip(this.btnExtraerIpAsignada, "Clic aquí para extraer la dirección IP del equipo");
            this.btnExtraerIpAsignada.UseVisualStyleBackColor = false;
            this.btnExtraerIpAsignada.Click += new System.EventHandler(this.btnExtraerIpAsignada_Click);
            // 
            // btnExtraerNombreEquipo
            // 
            this.btnExtraerNombreEquipo.BackColor = System.Drawing.Color.DeepPink;
            this.btnExtraerNombreEquipo.Location = new System.Drawing.Point(317, 115);
            this.btnExtraerNombreEquipo.Name = "btnExtraerNombreEquipo";
            this.btnExtraerNombreEquipo.Size = new System.Drawing.Size(25, 20);
            this.btnExtraerNombreEquipo.TabIndex = 9;
            this.btnExtraerNombreEquipo.Text = "...";
            this.ttMensaje.SetToolTip(this.btnExtraerNombreEquipo, "Clic aquí para extrer el nombre del equipo");
            this.btnExtraerNombreEquipo.UseVisualStyleBackColor = false;
            this.btnExtraerNombreEquipo.Click += new System.EventHandler(this.btnExtraerNombreEquipo_Click);
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.Enabled = false;
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(118, 36);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(195, 21);
            this.cmbLocalidad.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(11, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "Localidad:";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(262, 305);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 20;
            this.lblId.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(11, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "IP Asignada:";
            // 
            // TxtIPAsignada
            // 
            this.TxtIPAsignada.BackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtIPAsignada.Location = new System.Drawing.Point(118, 141);
            this.TxtIPAsignada.MaxLength = 20;
            this.TxtIPAsignada.Multiline = true;
            this.TxtIPAsignada.Name = "TxtIPAsignada";
            this.TxtIPAsignada.Size = new System.Drawing.Size(195, 20);
            this.TxtIPAsignada.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(11, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nombre Equipo:";
            // 
            // txtNombreEquipo
            // 
            this.txtNombreEquipo.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtNombreEquipo.Location = new System.Drawing.Point(118, 115);
            this.txtNombreEquipo.MaxLength = 20;
            this.txtNombreEquipo.Name = "txtNombreEquipo";
            this.txtNombreEquipo.Size = new System.Drawing.Size(195, 20);
            this.txtNombreEquipo.TabIndex = 6;
            // 
            // cmbEstado
            // 
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstado.Location = new System.Drawing.Point(118, 167);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(107, 21);
            this.cmbEstado.TabIndex = 10;
            this.cmbEstado.SelectedIndexChanged += new System.EventHandler(this.cmbEstado_SelectedIndexChanged);
            // 
            // lblEstaCajero
            // 
            this.lblEstaCajero.AutoSize = true;
            this.lblEstaCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaCajero.Location = new System.Drawing.Point(11, 168);
            this.lblEstaCajero.Name = "lblEstaCajero";
            this.lblEstaCajero.Size = new System.Drawing.Size(48, 15);
            this.lblEstaCajero.TabIndex = 7;
            this.lblEstaCajero.Text = "Estado:";
            // 
            // lbldescrCajero
            // 
            this.lbldescrCajero.AutoSize = true;
            this.lbldescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lbldescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbldescrCajero.Location = new System.Drawing.Point(11, 90);
            this.lbldescrCajero.Name = "lbldescrCajero";
            this.lbldescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lbldescrCajero.TabIndex = 5;
            this.lbldescrCajero.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(118, 89);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(195, 20);
            this.txtDescripcion.TabIndex = 5;
            // 
            // lblcodigoCajero
            // 
            this.lblcodigoCajero.AutoSize = true;
            this.lblcodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoCajero.Location = new System.Drawing.Point(11, 64);
            this.lblcodigoCajero.Name = "lblcodigoCajero";
            this.lblcodigoCajero.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoCajero.TabIndex = 3;
            this.lblcodigoCajero.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(118, 63);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(195, 20);
            this.txtCodigo.TabIndex = 4;
            // 
            // frmTerminales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1023, 380);
            this.Controls.Add(this.Grb_listReCanImpre);
            this.Controls.Add(this.Grb_opcioCanImpre);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTerminales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Terminales";
            this.Load += new System.EventHandler(this.frmTerminales_Load);
            this.Grb_listReCanImpre.ResumeLayout(false);
            this.Grb_listReCanImpre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.Grb_opcioCanImpre.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReCanImpre;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox Grb_opcioCanImpre;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grupoDatos;
        private MisControles.ComboDatos cmbLocalidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtIPAsignada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombreEquipo;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaCajero;
        private System.Windows.Forms.Label lbldescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblcodigoCajero;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnExtraerIpAsignada;
        private System.Windows.Forms.Button btnExtraerNombreEquipo;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}