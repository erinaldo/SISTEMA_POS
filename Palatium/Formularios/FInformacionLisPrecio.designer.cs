namespace Palatium.Formularios
{
    partial class FInformacionLisPrecio
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
            this.tbcListPrecio = new System.Windows.Forms.TabControl();
            this.tblListPrecioCrear = new System.Windows.Forms.TabPage();
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscarListPrecios = new System.Windows.Forms.Button();
            this.txtBuscarListPre = new System.Windows.Forms.TextBox();
            this.dgvListPrecios = new System.Windows.Forms.DataGridView();
            this.Grb_opcioCajero = new System.Windows.Forms.GroupBox();
            this.btnCerrarListPrecios = new System.Windows.Forms.Button();
            this.btnLimpiarListPrecios = new System.Windows.Forms.Button();
            this.btnAnularListPrecios = new System.Windows.Forms.Button();
            this.btnNuevoListPrecios = new System.Windows.Forms.Button();
            this.grpDatoListPrecios = new System.Windows.Forms.GroupBox();
            this.cmbCliente = new MisControles.ComboDatos();
            this.cmbMoneda = new MisControles.ComboDatos();
            this.chkRestrLocalidad = new System.Windows.Forms.CheckBox();
            this.rdbLisComCrear = new System.Windows.Forms.RadioButton();
            this.rdbLisEspCrear = new System.Windows.Forms.RadioButton();
            this.chkListModiCrear = new System.Windows.Forms.CheckBox();
            this.rdbLisBasCrear = new System.Windows.Forms.RadioButton();
            this.txtFecFinVaCrear = new System.Windows.Forms.TextBox();
            this.txtFecIniVaCrear = new System.Windows.Forms.TextBox();
            this.lblFecIniVaCrear = new System.Windows.Forms.Label();
            this.lblFecFinVaCrear = new System.Windows.Forms.Label();
            this.lblMonedaCrear = new System.Windows.Forms.Label();
            this.lblTipClieCrear = new System.Windows.Forms.Label();
            this.cmbEstadoListPrecios = new System.Windows.Forms.ComboBox();
            this.lblEstaListPre = new System.Windows.Forms.Label();
            this.lblNombreCrear = new System.Windows.Forms.Label();
            this.txtNombreCrear = new System.Windows.Forms.TextBox();
            this.tbcListPrecio.SuspendLayout();
            this.tblListPrecioCrear.SuspendLayout();
            this.Grb_listReCajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPrecios)).BeginInit();
            this.Grb_opcioCajero.SuspendLayout();
            this.grpDatoListPrecios.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcListPrecio
            // 
            this.tbcListPrecio.Controls.Add(this.tblListPrecioCrear);
            this.tbcListPrecio.Location = new System.Drawing.Point(-4, -1);
            this.tbcListPrecio.Name = "tbcListPrecio";
            this.tbcListPrecio.SelectedIndex = 0;
            this.tbcListPrecio.Size = new System.Drawing.Size(944, 552);
            this.tbcListPrecio.TabIndex = 2;
            // 
            // tblListPrecioCrear
            // 
            this.tblListPrecioCrear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tblListPrecioCrear.Controls.Add(this.Grb_listReCajero);
            this.tblListPrecioCrear.Controls.Add(this.Grb_opcioCajero);
            this.tblListPrecioCrear.Controls.Add(this.grpDatoListPrecios);
            this.tblListPrecioCrear.Location = new System.Drawing.Point(4, 22);
            this.tblListPrecioCrear.Name = "tblListPrecioCrear";
            this.tblListPrecioCrear.Padding = new System.Windows.Forms.Padding(3);
            this.tblListPrecioCrear.Size = new System.Drawing.Size(936, 526);
            this.tblListPrecioCrear.TabIndex = 0;
            this.tblListPrecioCrear.Text = "Módulo Lista_Precios";
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listReCajero.Controls.Add(this.btnBuscarListPrecios);
            this.Grb_listReCajero.Controls.Add(this.txtBuscarListPre);
            this.Grb_listReCajero.Controls.Add(this.dgvListPrecios);
            this.Grb_listReCajero.Location = new System.Drawing.Point(377, 19);
            this.Grb_listReCajero.Name = "Grb_listReCajero";
            this.Grb_listReCajero.Size = new System.Drawing.Size(553, 405);
            this.Grb_listReCajero.TabIndex = 5;
            this.Grb_listReCajero.TabStop = false;
            this.Grb_listReCajero.Text = "Lista de Registros";
            // 
            // btnBuscarListPrecios
            // 
            this.btnBuscarListPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnBuscarListPrecios.Location = new System.Drawing.Point(235, 25);
            this.btnBuscarListPrecios.Name = "btnBuscarListPrecios";
            this.btnBuscarListPrecios.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarListPrecios.TabIndex = 4;
            this.btnBuscarListPrecios.Text = "Buscar";
            this.btnBuscarListPrecios.UseVisualStyleBackColor = false;
            this.btnBuscarListPrecios.Click += new System.EventHandler(this.btnBuscarListPrecios_Click);
            // 
            // txtBuscarListPre
            // 
            this.txtBuscarListPre.Location = new System.Drawing.Point(13, 29);
            this.txtBuscarListPre.MaxLength = 20;
            this.txtBuscarListPre.Name = "txtBuscarListPre";
            this.txtBuscarListPre.Size = new System.Drawing.Size(216, 20);
            this.txtBuscarListPre.TabIndex = 3;
            // 
            // dgvListPrecios
            // 
            this.dgvListPrecios.AllowUserToAddRows = false;
            this.dgvListPrecios.AllowUserToDeleteRows = false;
            this.dgvListPrecios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPrecios.Location = new System.Drawing.Point(13, 61);
            this.dgvListPrecios.Name = "dgvListPrecios";
            this.dgvListPrecios.ReadOnly = true;
            this.dgvListPrecios.Size = new System.Drawing.Size(534, 329);
            this.dgvListPrecios.TabIndex = 0;
            this.dgvListPrecios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListPrecios_CellClick);
            // 
            // Grb_opcioCajero
            // 
            this.Grb_opcioCajero.Controls.Add(this.btnCerrarListPrecios);
            this.Grb_opcioCajero.Controls.Add(this.btnLimpiarListPrecios);
            this.Grb_opcioCajero.Controls.Add(this.btnAnularListPrecios);
            this.Grb_opcioCajero.Controls.Add(this.btnNuevoListPrecios);
            this.Grb_opcioCajero.Location = new System.Drawing.Point(17, 337);
            this.Grb_opcioCajero.Name = "Grb_opcioCajero";
            this.Grb_opcioCajero.Size = new System.Drawing.Size(342, 87);
            this.Grb_opcioCajero.TabIndex = 4;
            this.Grb_opcioCajero.TabStop = false;
            this.Grb_opcioCajero.Text = "Opciones";
            // 
            // btnCerrarListPrecios
            // 
            this.btnCerrarListPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnCerrarListPrecios.Location = new System.Drawing.Point(245, 33);
            this.btnCerrarListPrecios.Name = "btnCerrarListPrecios";
            this.btnCerrarListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarListPrecios.TabIndex = 3;
            this.btnCerrarListPrecios.Text = "Cerrar";
            this.btnCerrarListPrecios.UseVisualStyleBackColor = false;
            this.btnCerrarListPrecios.Click += new System.EventHandler(this.btnCerrarListPrecios_Click);
            // 
            // btnLimpiarListPrecios
            // 
            this.btnLimpiarListPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarListPrecios.Location = new System.Drawing.Point(169, 33);
            this.btnLimpiarListPrecios.Name = "btnLimpiarListPrecios";
            this.btnLimpiarListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiarListPrecios.TabIndex = 2;
            this.btnLimpiarListPrecios.Text = "Limpiar";
            this.btnLimpiarListPrecios.UseVisualStyleBackColor = false;
            this.btnLimpiarListPrecios.Click += new System.EventHandler(this.btnLimpiarListPrecios_Click);
            // 
            // btnAnularListPrecios
            // 
            this.btnAnularListPrecios.BackColor = System.Drawing.Color.Red;
            this.btnAnularListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnAnularListPrecios.Location = new System.Drawing.Point(93, 33);
            this.btnAnularListPrecios.Name = "btnAnularListPrecios";
            this.btnAnularListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnAnularListPrecios.TabIndex = 1;
            this.btnAnularListPrecios.Text = "Anular";
            this.btnAnularListPrecios.UseVisualStyleBackColor = false;
            this.btnAnularListPrecios.Click += new System.EventHandler(this.btnAnularListPrecios_Click);
            // 
            // btnNuevoListPrecios
            // 
            this.btnNuevoListPrecios.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnNuevoListPrecios.Location = new System.Drawing.Point(17, 33);
            this.btnNuevoListPrecios.Name = "btnNuevoListPrecios";
            this.btnNuevoListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnNuevoListPrecios.TabIndex = 0;
            this.btnNuevoListPrecios.Text = "Nuevo";
            this.btnNuevoListPrecios.UseVisualStyleBackColor = false;
            this.btnNuevoListPrecios.Click += new System.EventHandler(this.btnNuevoListPrecios_Click);
            // 
            // grpDatoListPrecios
            // 
            this.grpDatoListPrecios.Controls.Add(this.cmbCliente);
            this.grpDatoListPrecios.Controls.Add(this.cmbMoneda);
            this.grpDatoListPrecios.Controls.Add(this.chkRestrLocalidad);
            this.grpDatoListPrecios.Controls.Add(this.rdbLisComCrear);
            this.grpDatoListPrecios.Controls.Add(this.rdbLisEspCrear);
            this.grpDatoListPrecios.Controls.Add(this.chkListModiCrear);
            this.grpDatoListPrecios.Controls.Add(this.rdbLisBasCrear);
            this.grpDatoListPrecios.Controls.Add(this.txtFecFinVaCrear);
            this.grpDatoListPrecios.Controls.Add(this.txtFecIniVaCrear);
            this.grpDatoListPrecios.Controls.Add(this.lblFecIniVaCrear);
            this.grpDatoListPrecios.Controls.Add(this.lblFecFinVaCrear);
            this.grpDatoListPrecios.Controls.Add(this.lblMonedaCrear);
            this.grpDatoListPrecios.Controls.Add(this.lblTipClieCrear);
            this.grpDatoListPrecios.Controls.Add(this.cmbEstadoListPrecios);
            this.grpDatoListPrecios.Controls.Add(this.lblEstaListPre);
            this.grpDatoListPrecios.Controls.Add(this.lblNombreCrear);
            this.grpDatoListPrecios.Controls.Add(this.txtNombreCrear);
            this.grpDatoListPrecios.Enabled = false;
            this.grpDatoListPrecios.Location = new System.Drawing.Point(17, 19);
            this.grpDatoListPrecios.Name = "grpDatoListPrecios";
            this.grpDatoListPrecios.Size = new System.Drawing.Size(342, 312);
            this.grpDatoListPrecios.TabIndex = 3;
            this.grpDatoListPrecios.TabStop = false;
            this.grpDatoListPrecios.Text = "Datos del Registro";
            // 
            // cmbCliente
            // 
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(132, 80);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(184, 21);
            this.cmbCliente.TabIndex = 27;
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(132, 53);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(107, 21);
            this.cmbMoneda.TabIndex = 26;
            // 
            // chkRestrLocalidad
            // 
            this.chkRestrLocalidad.AutoSize = true;
            this.chkRestrLocalidad.Location = new System.Drawing.Point(17, 229);
            this.chkRestrLocalidad.Name = "chkRestrLocalidad";
            this.chkRestrLocalidad.Size = new System.Drawing.Size(128, 17);
            this.chkRestrLocalidad.TabIndex = 24;
            this.chkRestrLocalidad.Text = "Restricción Localidad";
            this.chkRestrLocalidad.UseVisualStyleBackColor = true;
            // 
            // rdbLisComCrear
            // 
            this.rdbLisComCrear.AutoSize = true;
            this.rdbLisComCrear.Location = new System.Drawing.Point(118, 276);
            this.rdbLisComCrear.Name = "rdbLisComCrear";
            this.rdbLisComCrear.Size = new System.Drawing.Size(83, 17);
            this.rdbLisComCrear.TabIndex = 23;
            this.rdbLisComCrear.TabStop = true;
            this.rdbLisComCrear.Text = "Lista Combo";
            this.rdbLisComCrear.UseVisualStyleBackColor = true;
            // 
            // rdbLisEspCrear
            // 
            this.rdbLisEspCrear.AutoSize = true;
            this.rdbLisEspCrear.Location = new System.Drawing.Point(226, 276);
            this.rdbLisEspCrear.Name = "rdbLisEspCrear";
            this.rdbLisEspCrear.Size = new System.Drawing.Size(90, 17);
            this.rdbLisEspCrear.TabIndex = 22;
            this.rdbLisEspCrear.TabStop = true;
            this.rdbLisEspCrear.Text = "Lista Especial";
            this.rdbLisEspCrear.UseVisualStyleBackColor = true;
            // 
            // chkListModiCrear
            // 
            this.chkListModiCrear.AutoSize = true;
            this.chkListModiCrear.Location = new System.Drawing.Point(18, 195);
            this.chkListModiCrear.Name = "chkListModiCrear";
            this.chkListModiCrear.Size = new System.Drawing.Size(105, 17);
            this.chkListModiCrear.TabIndex = 21;
            this.chkListModiCrear.Text = "Lista Modificable";
            this.chkListModiCrear.UseVisualStyleBackColor = true;
            // 
            // rdbLisBasCrear
            // 
            this.rdbLisBasCrear.AutoSize = true;
            this.rdbLisBasCrear.Enabled = false;
            this.rdbLisBasCrear.Location = new System.Drawing.Point(18, 276);
            this.rdbLisBasCrear.Name = "rdbLisBasCrear";
            this.rdbLisBasCrear.Size = new System.Drawing.Size(74, 17);
            this.rdbLisBasCrear.TabIndex = 20;
            this.rdbLisBasCrear.TabStop = true;
            this.rdbLisBasCrear.Text = "Lista Base";
            this.rdbLisBasCrear.UseVisualStyleBackColor = true;
            // 
            // txtFecFinVaCrear
            // 
            this.txtFecFinVaCrear.Location = new System.Drawing.Point(132, 135);
            this.txtFecFinVaCrear.MaxLength = 20;
            this.txtFecFinVaCrear.Name = "txtFecFinVaCrear";
            this.txtFecFinVaCrear.Size = new System.Drawing.Size(107, 20);
            this.txtFecFinVaCrear.TabIndex = 19;
            // 
            // txtFecIniVaCrear
            // 
            this.txtFecIniVaCrear.Location = new System.Drawing.Point(132, 108);
            this.txtFecIniVaCrear.MaxLength = 20;
            this.txtFecIniVaCrear.Name = "txtFecIniVaCrear";
            this.txtFecIniVaCrear.Size = new System.Drawing.Size(107, 20);
            this.txtFecIniVaCrear.TabIndex = 18;
            // 
            // lblFecIniVaCrear
            // 
            this.lblFecIniVaCrear.AutoSize = true;
            this.lblFecIniVaCrear.BackColor = System.Drawing.Color.Transparent;
            this.lblFecIniVaCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecIniVaCrear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecIniVaCrear.Location = new System.Drawing.Point(15, 113);
            this.lblFecIniVaCrear.Name = "lblFecIniVaCrear";
            this.lblFecIniVaCrear.Size = new System.Drawing.Size(114, 15);
            this.lblFecIniVaCrear.TabIndex = 17;
            this.lblFecIniVaCrear.Text = "Fecha válida inicial:";
            // 
            // lblFecFinVaCrear
            // 
            this.lblFecFinVaCrear.AutoSize = true;
            this.lblFecFinVaCrear.BackColor = System.Drawing.Color.Transparent;
            this.lblFecFinVaCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecFinVaCrear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecFinVaCrear.Location = new System.Drawing.Point(14, 140);
            this.lblFecFinVaCrear.Name = "lblFecFinVaCrear";
            this.lblFecFinVaCrear.Size = new System.Drawing.Size(105, 15);
            this.lblFecFinVaCrear.TabIndex = 15;
            this.lblFecFinVaCrear.Text = "Fecha válida final:";
            // 
            // lblMonedaCrear
            // 
            this.lblMonedaCrear.AutoSize = true;
            this.lblMonedaCrear.BackColor = System.Drawing.Color.Transparent;
            this.lblMonedaCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonedaCrear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMonedaCrear.Location = new System.Drawing.Point(14, 59);
            this.lblMonedaCrear.Name = "lblMonedaCrear";
            this.lblMonedaCrear.Size = new System.Drawing.Size(56, 15);
            this.lblMonedaCrear.TabIndex = 13;
            this.lblMonedaCrear.Text = "Moneda:";
            // 
            // lblTipClieCrear
            // 
            this.lblTipClieCrear.AutoSize = true;
            this.lblTipClieCrear.BackColor = System.Drawing.Color.Transparent;
            this.lblTipClieCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipClieCrear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTipClieCrear.Location = new System.Drawing.Point(15, 86);
            this.lblTipClieCrear.Name = "lblTipClieCrear";
            this.lblTipClieCrear.Size = new System.Drawing.Size(73, 15);
            this.lblTipClieCrear.TabIndex = 11;
            this.lblTipClieCrear.Text = "Tipo cliente:";
            // 
            // cmbEstadoListPrecios
            // 
            this.cmbEstadoListPrecios.Enabled = false;
            this.cmbEstadoListPrecios.FormattingEnabled = true;
            this.cmbEstadoListPrecios.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoListPrecios.Location = new System.Drawing.Point(132, 161);
            this.cmbEstadoListPrecios.Name = "cmbEstadoListPrecios";
            this.cmbEstadoListPrecios.Size = new System.Drawing.Size(107, 21);
            this.cmbEstadoListPrecios.TabIndex = 10;
            this.cmbEstadoListPrecios.SelectedIndexChanged += new System.EventHandler(this.cmbEstadoCrear_SelectedIndexChanged);
            // 
            // lblEstaListPre
            // 
            this.lblEstaListPre.AutoSize = true;
            this.lblEstaListPre.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaListPre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaListPre.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaListPre.Location = new System.Drawing.Point(14, 167);
            this.lblEstaListPre.Name = "lblEstaListPre";
            this.lblEstaListPre.Size = new System.Drawing.Size(48, 15);
            this.lblEstaListPre.TabIndex = 7;
            this.lblEstaListPre.Text = "Estado:";
            // 
            // lblNombreCrear
            // 
            this.lblNombreCrear.AutoSize = true;
            this.lblNombreCrear.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCrear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNombreCrear.Location = new System.Drawing.Point(15, 32);
            this.lblNombreCrear.Name = "lblNombreCrear";
            this.lblNombreCrear.Size = new System.Drawing.Size(55, 15);
            this.lblNombreCrear.TabIndex = 3;
            this.lblNombreCrear.Text = "Nombre:";
            // 
            // txtNombreCrear
            // 
            this.txtNombreCrear.Location = new System.Drawing.Point(132, 27);
            this.txtNombreCrear.MaxLength = 20;
            this.txtNombreCrear.Name = "txtNombreCrear";
            this.txtNombreCrear.Size = new System.Drawing.Size(183, 20);
            this.txtNombreCrear.TabIndex = 2;
            // 
            // FInformacionLisPrecio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(937, 459);
            this.Controls.Add(this.tbcListPrecio);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionLisPrecio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Infromación de Lista de Precios";
            this.Load += new System.EventHandler(this.FInformacionLisPrecio_Load);
            this.tbcListPrecio.ResumeLayout(false);
            this.tblListPrecioCrear.ResumeLayout(false);
            this.Grb_listReCajero.ResumeLayout(false);
            this.Grb_listReCajero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPrecios)).EndInit();
            this.Grb_opcioCajero.ResumeLayout(false);
            this.grpDatoListPrecios.ResumeLayout(false);
            this.grpDatoListPrecios.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcListPrecio;
        private System.Windows.Forms.TabPage tblListPrecioCrear;
        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.DataGridView dgvListPrecios;
        private System.Windows.Forms.GroupBox Grb_opcioCajero;
        private System.Windows.Forms.Button btnCerrarListPrecios;
        private System.Windows.Forms.Button btnLimpiarListPrecios;
        private System.Windows.Forms.Button btnAnularListPrecios;
        private System.Windows.Forms.Button btnNuevoListPrecios;
        private System.Windows.Forms.GroupBox grpDatoListPrecios;
        private System.Windows.Forms.ComboBox cmbEstadoListPrecios;
        private System.Windows.Forms.Label lblEstaListPre;
        private System.Windows.Forms.Label lblNombreCrear;
        private System.Windows.Forms.TextBox txtNombreCrear;
        private System.Windows.Forms.Label lblMonedaCrear;
        private System.Windows.Forms.Label lblTipClieCrear;
        private System.Windows.Forms.TextBox txtFecFinVaCrear;
        private System.Windows.Forms.TextBox txtFecIniVaCrear;
        private System.Windows.Forms.Label lblFecIniVaCrear;
        private System.Windows.Forms.Label lblFecFinVaCrear;
        private System.Windows.Forms.RadioButton rdbLisComCrear;
        private System.Windows.Forms.RadioButton rdbLisEspCrear;
        private System.Windows.Forms.CheckBox chkListModiCrear;
        private System.Windows.Forms.RadioButton rdbLisBasCrear;
        private System.Windows.Forms.Button btnBuscarListPrecios;
        private System.Windows.Forms.TextBox txtBuscarListPre;
        private System.Windows.Forms.CheckBox chkRestrLocalidad;
        private MisControles.ComboDatos cmbCliente;
        private MisControles.ComboDatos cmbMoneda;

    }
}