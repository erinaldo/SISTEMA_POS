namespace Palatium.Formularios
{
    partial class FInformacionReserva
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
            this.tabCon_Reserva = new System.Windows.Forms.TabControl();
            this.tabPag_Reserva = new System.Windows.Forms.TabPage();
            this.Grb_listReReserva = new System.Windows.Forms.GroupBox();
            this.btnBuscarReserva = new System.Windows.Forms.Button();
            this.txtBuscarReserva = new System.Windows.Forms.TextBox();
            this.dgvReserva = new System.Windows.Forms.DataGridView();
            this.Grb_opcioReserva = new System.Windows.Forms.GroupBox();
            this.btnCerrarReserva = new System.Windows.Forms.Button();
            this.btnLimpiarReserva = new System.Windows.Forms.Button();
            this.btnAnularReserva = new System.Windows.Forms.Button();
            this.btnNuevoReserva = new System.Windows.Forms.Button();
            this.Grb_DatoReserva = new System.Windows.Forms.GroupBox();
            this.Txt_Informacion = new System.Windows.Forms.TextBox();
            this.txtFechaReserva = new System.Windows.Forms.TextBox();
            this.Btn_Abrir_Grid = new System.Windows.Forms.Button();
            this.txtNumPersReserva = new System.Windows.Forms.TextBox();
            this.Txt_Buscar = new System.Windows.Forms.TextBox();
            this.cmbLocaliReserva = new System.Windows.Forms.ComboBox();
            this.cmbJornaReserva = new System.Windows.Forms.ComboBox();
            this.lblNumPersonas = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblJornada = new System.Windows.Forms.Label();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.txtHoraReserva = new System.Windows.Forms.TextBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.cmbEstadoReserva = new System.Windows.Forms.ComboBox();
            this.lblEstaReserva = new System.Windows.Forms.Label();
            this.lblDescrReserva = new System.Windows.Forms.Label();
            this.txtDescripReserva = new System.Windows.Forms.TextBox();
            this.tabCon_Reserva.SuspendLayout();
            this.tabPag_Reserva.SuspendLayout();
            this.Grb_listReReserva.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReserva)).BeginInit();
            this.Grb_opcioReserva.SuspendLayout();
            this.Grb_DatoReserva.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_Reserva
            // 
            this.tabCon_Reserva.Controls.Add(this.tabPag_Reserva);
            this.tabCon_Reserva.Location = new System.Drawing.Point(-4, -1);
            this.tabCon_Reserva.Name = "tabCon_Reserva";
            this.tabCon_Reserva.SelectedIndex = 0;
            this.tabCon_Reserva.Size = new System.Drawing.Size(1017, 533);
            this.tabCon_Reserva.TabIndex = 2;
            // 
            // tabPag_Reserva
            // 
            this.tabPag_Reserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_Reserva.Controls.Add(this.Grb_listReReserva);
            this.tabPag_Reserva.Controls.Add(this.Grb_opcioReserva);
            this.tabPag_Reserva.Controls.Add(this.Grb_DatoReserva);
            this.tabPag_Reserva.Location = new System.Drawing.Point(4, 22);
            this.tabPag_Reserva.Name = "tabPag_Reserva";
            this.tabPag_Reserva.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_Reserva.Size = new System.Drawing.Size(1009, 507);
            this.tabPag_Reserva.TabIndex = 0;
            this.tabPag_Reserva.Text = "Módulo de Reserva";
            // 
            // Grb_listReReserva
            // 
            this.Grb_listReReserva.Controls.Add(this.btnBuscarReserva);
            this.Grb_listReReserva.Controls.Add(this.txtBuscarReserva);
            this.Grb_listReReserva.Controls.Add(this.dgvReserva);
            this.Grb_listReReserva.Location = new System.Drawing.Point(464, 19);
            this.Grb_listReReserva.Name = "Grb_listReReserva";
            this.Grb_listReReserva.Size = new System.Drawing.Size(536, 477);
            this.Grb_listReReserva.TabIndex = 5;
            this.Grb_listReReserva.TabStop = false;
            this.Grb_listReReserva.Text = "Lista de Registros";
            // 
            // btnBuscarReserva
            // 
            this.btnBuscarReserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarReserva.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscarReserva.Location = new System.Drawing.Point(235, 25);
            this.btnBuscarReserva.Name = "btnBuscarReserva";
            this.btnBuscarReserva.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarReserva.TabIndex = 4;
            this.btnBuscarReserva.Text = "Buscar";
            this.btnBuscarReserva.UseVisualStyleBackColor = false;
            this.btnBuscarReserva.Click += new System.EventHandler(this.Btn_BuscarReserva_Click);
            // 
            // txtBuscarReserva
            // 
            this.txtBuscarReserva.Location = new System.Drawing.Point(13, 29);
            this.txtBuscarReserva.MaxLength = 20;
            this.txtBuscarReserva.Name = "txtBuscarReserva";
            this.txtBuscarReserva.Size = new System.Drawing.Size(216, 20);
            this.txtBuscarReserva.TabIndex = 3;
            // 
            // dgvReserva
            // 
            this.dgvReserva.AllowUserToAddRows = false;
            this.dgvReserva.AllowUserToDeleteRows = false;
            this.dgvReserva.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReserva.Location = new System.Drawing.Point(13, 61);
            this.dgvReserva.Name = "dgvReserva";
            this.dgvReserva.ReadOnly = true;
            this.dgvReserva.Size = new System.Drawing.Size(510, 403);
            this.dgvReserva.TabIndex = 0;
            this.dgvReserva.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Reserva_CellClick);
            // 
            // Grb_opcioReserva
            // 
            this.Grb_opcioReserva.Controls.Add(this.btnCerrarReserva);
            this.Grb_opcioReserva.Controls.Add(this.btnLimpiarReserva);
            this.Grb_opcioReserva.Controls.Add(this.btnAnularReserva);
            this.Grb_opcioReserva.Controls.Add(this.btnNuevoReserva);
            this.Grb_opcioReserva.Location = new System.Drawing.Point(17, 366);
            this.Grb_opcioReserva.Name = "Grb_opcioReserva";
            this.Grb_opcioReserva.Size = new System.Drawing.Size(432, 130);
            this.Grb_opcioReserva.TabIndex = 4;
            this.Grb_opcioReserva.TabStop = false;
            this.Grb_opcioReserva.Text = "Opciones";
            // 
            // btnCerrarReserva
            // 
            this.btnCerrarReserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarReserva.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrarReserva.Location = new System.Drawing.Point(313, 44);
            this.btnCerrarReserva.Name = "btnCerrarReserva";
            this.btnCerrarReserva.Size = new System.Drawing.Size(88, 39);
            this.btnCerrarReserva.TabIndex = 3;
            this.btnCerrarReserva.Text = "Cerrar";
            this.btnCerrarReserva.UseVisualStyleBackColor = false;
            this.btnCerrarReserva.Click += new System.EventHandler(this.Btn_CerrarReserva_Click);
            // 
            // btnLimpiarReserva
            // 
            this.btnLimpiarReserva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarReserva.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiarReserva.Location = new System.Drawing.Point(219, 44);
            this.btnLimpiarReserva.Name = "btnLimpiarReserva";
            this.btnLimpiarReserva.Size = new System.Drawing.Size(88, 39);
            this.btnLimpiarReserva.TabIndex = 2;
            this.btnLimpiarReserva.Text = "Limpiar";
            this.btnLimpiarReserva.UseVisualStyleBackColor = false;
            this.btnLimpiarReserva.Click += new System.EventHandler(this.Btn_LimpiarReserva_Click);
            // 
            // btnAnularReserva
            // 
            this.btnAnularReserva.BackColor = System.Drawing.Color.Red;
            this.btnAnularReserva.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnularReserva.Location = new System.Drawing.Point(125, 44);
            this.btnAnularReserva.Name = "btnAnularReserva";
            this.btnAnularReserva.Size = new System.Drawing.Size(88, 39);
            this.btnAnularReserva.TabIndex = 1;
            this.btnAnularReserva.Text = "Anular";
            this.btnAnularReserva.UseVisualStyleBackColor = false;
            this.btnAnularReserva.Click += new System.EventHandler(this.Btn_AnularReserva_Click);
            // 
            // btnNuevoReserva
            // 
            this.btnNuevoReserva.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoReserva.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevoReserva.Location = new System.Drawing.Point(31, 44);
            this.btnNuevoReserva.Name = "btnNuevoReserva";
            this.btnNuevoReserva.Size = new System.Drawing.Size(88, 39);
            this.btnNuevoReserva.TabIndex = 0;
            this.btnNuevoReserva.Text = "Nuevo";
            this.btnNuevoReserva.UseVisualStyleBackColor = false;
            this.btnNuevoReserva.Click += new System.EventHandler(this.BtnNuevoReserva_Click);
            // 
            // Grb_DatoReserva
            // 
            this.Grb_DatoReserva.Controls.Add(this.Txt_Informacion);
            this.Grb_DatoReserva.Controls.Add(this.txtFechaReserva);
            this.Grb_DatoReserva.Controls.Add(this.Btn_Abrir_Grid);
            this.Grb_DatoReserva.Controls.Add(this.txtNumPersReserva);
            this.Grb_DatoReserva.Controls.Add(this.Txt_Buscar);
            this.Grb_DatoReserva.Controls.Add(this.cmbLocaliReserva);
            this.Grb_DatoReserva.Controls.Add(this.cmbJornaReserva);
            this.Grb_DatoReserva.Controls.Add(this.lblNumPersonas);
            this.Grb_DatoReserva.Controls.Add(this.lblCliente);
            this.Grb_DatoReserva.Controls.Add(this.lblJornada);
            this.Grb_DatoReserva.Controls.Add(this.lblLocalidad);
            this.Grb_DatoReserva.Controls.Add(this.txtHoraReserva);
            this.Grb_DatoReserva.Controls.Add(this.lblHora);
            this.Grb_DatoReserva.Controls.Add(this.lblFecha);
            this.Grb_DatoReserva.Controls.Add(this.cmbEstadoReserva);
            this.Grb_DatoReserva.Controls.Add(this.lblEstaReserva);
            this.Grb_DatoReserva.Controls.Add(this.lblDescrReserva);
            this.Grb_DatoReserva.Controls.Add(this.txtDescripReserva);
            this.Grb_DatoReserva.Enabled = false;
            this.Grb_DatoReserva.Location = new System.Drawing.Point(17, 19);
            this.Grb_DatoReserva.Name = "Grb_DatoReserva";
            this.Grb_DatoReserva.Size = new System.Drawing.Size(432, 341);
            this.Grb_DatoReserva.TabIndex = 3;
            this.Grb_DatoReserva.TabStop = false;
            this.Grb_DatoReserva.Text = "Datos del Registro";
            // 
            // Txt_Informacion
            // 
            this.Txt_Informacion.Enabled = false;
            this.Txt_Informacion.Location = new System.Drawing.Point(251, 149);
            this.Txt_Informacion.Name = "Txt_Informacion";
            this.Txt_Informacion.Size = new System.Drawing.Size(171, 20);
            this.Txt_Informacion.TabIndex = 8;
            // 
            // txtFechaReserva
            // 
            this.txtFechaReserva.Location = new System.Drawing.Point(77, 28);
            this.txtFechaReserva.MaxLength = 20;
            this.txtFechaReserva.Name = "txtFechaReserva";
            this.txtFechaReserva.Size = new System.Drawing.Size(152, 20);
            this.txtFechaReserva.TabIndex = 22;
            // 
            // Btn_Abrir_Grid
            // 
            this.Btn_Abrir_Grid.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Abrir_Grid.Location = new System.Drawing.Point(229, 149);
            this.Btn_Abrir_Grid.Name = "Btn_Abrir_Grid";
            this.Btn_Abrir_Grid.Size = new System.Drawing.Size(20, 20);
            this.Btn_Abrir_Grid.TabIndex = 7;
            this.Btn_Abrir_Grid.Text = "?";
            this.Btn_Abrir_Grid.UseVisualStyleBackColor = true;
            this.Btn_Abrir_Grid.Click += new System.EventHandler(this.Btn_Abrir_Grid_Click);
            // 
            // txtNumPersReserva
            // 
            this.txtNumPersReserva.Location = new System.Drawing.Point(125, 195);
            this.txtNumPersReserva.MaxLength = 3;
            this.txtNumPersReserva.Name = "txtNumPersReserva";
            this.txtNumPersReserva.Size = new System.Drawing.Size(51, 20);
            this.txtNumPersReserva.TabIndex = 6;
            this.txtNumPersReserva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumPersReserva_KeyPress);
            // 
            // Txt_Buscar
            // 
            this.Txt_Buscar.Location = new System.Drawing.Point(77, 149);
            this.Txt_Buscar.MaxLength = 13;
            this.Txt_Buscar.Name = "Txt_Buscar";
            this.Txt_Buscar.Size = new System.Drawing.Size(148, 20);
            this.Txt_Buscar.TabIndex = 6;
            // 
            // cmbLocaliReserva
            // 
            this.cmbLocaliReserva.FormattingEnabled = true;
            this.cmbLocaliReserva.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbLocaliReserva.Location = new System.Drawing.Point(77, 97);
            this.cmbLocaliReserva.Name = "cmbLocaliReserva";
            this.cmbLocaliReserva.Size = new System.Drawing.Size(152, 21);
            this.cmbLocaliReserva.TabIndex = 20;
            // 
            // cmbJornaReserva
            // 
            this.cmbJornaReserva.FormattingEnabled = true;
            this.cmbJornaReserva.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbJornaReserva.Location = new System.Drawing.Point(77, 63);
            this.cmbJornaReserva.Name = "cmbJornaReserva";
            this.cmbJornaReserva.Size = new System.Drawing.Size(152, 21);
            this.cmbJornaReserva.TabIndex = 19;
            // 
            // lblNumPersonas
            // 
            this.lblNumPersonas.AutoSize = true;
            this.lblNumPersonas.BackColor = System.Drawing.Color.Transparent;
            this.lblNumPersonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumPersonas.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNumPersonas.Location = new System.Drawing.Point(6, 200);
            this.lblNumPersonas.Name = "lblNumPersonas";
            this.lblNumPersonas.Size = new System.Drawing.Size(113, 15);
            this.lblNumPersonas.TabIndex = 18;
            this.lblNumPersonas.Text = "Número_personas:";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.BackColor = System.Drawing.Color.Transparent;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCliente.Location = new System.Drawing.Point(10, 154);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 15);
            this.lblCliente.TabIndex = 17;
            this.lblCliente.Text = "Cliente:";
            // 
            // lblJornada
            // 
            this.lblJornada.AutoSize = true;
            this.lblJornada.BackColor = System.Drawing.Color.Transparent;
            this.lblJornada.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJornada.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblJornada.Location = new System.Drawing.Point(10, 69);
            this.lblJornada.Name = "lblJornada";
            this.lblJornada.Size = new System.Drawing.Size(55, 15);
            this.lblJornada.TabIndex = 16;
            this.lblJornada.Text = "Jornada:";
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLocalidad.Location = new System.Drawing.Point(10, 103);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(61, 15);
            this.lblLocalidad.TabIndex = 15;
            this.lblLocalidad.Text = "Localidad";
            // 
            // txtHoraReserva
            // 
            this.txtHoraReserva.Location = new System.Drawing.Point(295, 28);
            this.txtHoraReserva.MaxLength = 20;
            this.txtHoraReserva.Name = "txtHoraReserva";
            this.txtHoraReserva.Size = new System.Drawing.Size(70, 20);
            this.txtHoraReserva.TabIndex = 14;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.BackColor = System.Drawing.Color.Transparent;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblHora.Location = new System.Drawing.Point(252, 33);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(37, 15);
            this.lblHora.TabIndex = 13;
            this.lblHora.Text = "Hora:";
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.Transparent;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecha.Location = new System.Drawing.Point(10, 33);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(44, 15);
            this.lblFecha.TabIndex = 12;
            this.lblFecha.Text = "Fecha:";
            // 
            // cmbEstadoReserva
            // 
            this.cmbEstadoReserva.Enabled = false;
            this.cmbEstadoReserva.FormattingEnabled = true;
            this.cmbEstadoReserva.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoReserva.Location = new System.Drawing.Point(93, 294);
            this.cmbEstadoReserva.Name = "cmbEstadoReserva";
            this.cmbEstadoReserva.Size = new System.Drawing.Size(152, 21);
            this.cmbEstadoReserva.TabIndex = 10;
            this.cmbEstadoReserva.SelectedIndexChanged += new System.EventHandler(this.CmbEstadoReserva_SelectedIndexChanged);
            // 
            // lblEstaReserva
            // 
            this.lblEstaReserva.AutoSize = true;
            this.lblEstaReserva.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaReserva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaReserva.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaReserva.Location = new System.Drawing.Point(13, 300);
            this.lblEstaReserva.Name = "lblEstaReserva";
            this.lblEstaReserva.Size = new System.Drawing.Size(45, 15);
            this.lblEstaReserva.TabIndex = 7;
            this.lblEstaReserva.Text = "Estado";
            // 
            // lblDescrReserva
            // 
            this.lblDescrReserva.AutoSize = true;
            this.lblDescrReserva.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrReserva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrReserva.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrReserva.Location = new System.Drawing.Point(10, 238);
            this.lblDescrReserva.Name = "lblDescrReserva";
            this.lblDescrReserva.Size = new System.Drawing.Size(75, 15);
            this.lblDescrReserva.TabIndex = 5;
            this.lblDescrReserva.Text = "Descripción:";
            // 
            // txtDescripReserva
            // 
            this.txtDescripReserva.Location = new System.Drawing.Point(93, 238);
            this.txtDescripReserva.MaxLength = 20;
            this.txtDescripReserva.Multiline = true;
            this.txtDescripReserva.Name = "txtDescripReserva";
            this.txtDescripReserva.Size = new System.Drawing.Size(329, 44);
            this.txtDescripReserva.TabIndex = 4;
            this.txtDescripReserva.Leave += new System.EventHandler(this.Txt_DescripReserva_Leave);
            // 
            // FInformacionReserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1010, 529);
            this.Controls.Add(this.tabCon_Reserva);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionReserva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Registro de Reservas";
            this.Load += new System.EventHandler(this.FInformacionReserva_Load);
            this.tabCon_Reserva.ResumeLayout(false);
            this.tabPag_Reserva.ResumeLayout(false);
            this.Grb_listReReserva.ResumeLayout(false);
            this.Grb_listReReserva.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReserva)).EndInit();
            this.Grb_opcioReserva.ResumeLayout(false);
            this.Grb_DatoReserva.ResumeLayout(false);
            this.Grb_DatoReserva.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_Reserva;
        private System.Windows.Forms.TabPage tabPag_Reserva;
        private System.Windows.Forms.GroupBox Grb_listReReserva;
        private System.Windows.Forms.Button btnBuscarReserva;
        private System.Windows.Forms.TextBox txtBuscarReserva;
        private System.Windows.Forms.DataGridView dgvReserva;
        private System.Windows.Forms.GroupBox Grb_opcioReserva;
        private System.Windows.Forms.Button btnCerrarReserva;
        private System.Windows.Forms.Button btnLimpiarReserva;
        private System.Windows.Forms.Button btnAnularReserva;
        private System.Windows.Forms.Button btnNuevoReserva;
        private System.Windows.Forms.GroupBox Grb_DatoReserva;
        private System.Windows.Forms.ComboBox cmbEstadoReserva;
        private System.Windows.Forms.Label lblEstaReserva;
        private System.Windows.Forms.Label lblDescrReserva;
        private System.Windows.Forms.TextBox txtDescripReserva;
        private System.Windows.Forms.TextBox txtNumPersReserva;
        private System.Windows.Forms.ComboBox cmbLocaliReserva;
        private System.Windows.Forms.ComboBox cmbJornaReserva;
        private System.Windows.Forms.Label lblNumPersonas;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblJornada;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.TextBox txtHoraReserva;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtFechaReserva;
        private System.Windows.Forms.TextBox Txt_Informacion;
        private System.Windows.Forms.Button Btn_Abrir_Grid;
        private System.Windows.Forms.TextBox Txt_Buscar;

    }
}