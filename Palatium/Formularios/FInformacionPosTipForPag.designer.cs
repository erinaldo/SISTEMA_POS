namespace Palatium.Formularios
{
    partial class FInformacionPosTipForPag
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
            this.tabCon_PosTipForPag = new System.Windows.Forms.TabControl();
            this.tabPag_PosTipForPag = new System.Windows.Forms.TabPage();
            this.grupoBusqueda = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.cmbTipoVenta = new MisControles.ComboDatos();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.cmbMetodoPago = new MisControles.ComboDatos();
            this.label2 = new System.Windows.Forms.Label();
            this.chkPropina = new System.Windows.Forms.CheckBox();
            this.cmbTipoDocumento = new MisControles.ComboDatos();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDescrTiForPa = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblcodigoTiForPa = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.tabCon_PosTipForPag.SuspendLayout();
            this.tabPag_PosTipForPag.SuspendLayout();
            this.grupoBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoOpciones.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCon_PosTipForPag
            // 
            this.tabCon_PosTipForPag.Controls.Add(this.tabPag_PosTipForPag);
            this.tabCon_PosTipForPag.Location = new System.Drawing.Point(-4, 0);
            this.tabCon_PosTipForPag.Name = "tabCon_PosTipForPag";
            this.tabCon_PosTipForPag.SelectedIndex = 0;
            this.tabCon_PosTipForPag.Size = new System.Drawing.Size(1098, 493);
            this.tabCon_PosTipForPag.TabIndex = 2;
            // 
            // tabPag_PosTipForPag
            // 
            this.tabPag_PosTipForPag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPag_PosTipForPag.Controls.Add(this.grupoBusqueda);
            this.tabPag_PosTipForPag.Controls.Add(this.grupoOpciones);
            this.tabPag_PosTipForPag.Controls.Add(this.grupoDatos);
            this.tabPag_PosTipForPag.Location = new System.Drawing.Point(4, 22);
            this.tabPag_PosTipForPag.Name = "tabPag_PosTipForPag";
            this.tabPag_PosTipForPag.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_PosTipForPag.Size = new System.Drawing.Size(1090, 467);
            this.tabPag_PosTipForPag.TabIndex = 0;
            this.tabPag_PosTipForPag.Text = "Módulo Tipo-Forma_cobro";
            // 
            // grupoBusqueda
            // 
            this.grupoBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoBusqueda.Controls.Add(this.btnBuscar);
            this.grupoBusqueda.Controls.Add(this.txtBuscar);
            this.grupoBusqueda.Controls.Add(this.dgvDatos);
            this.grupoBusqueda.Location = new System.Drawing.Point(370, 19);
            this.grupoBusqueda.Name = "grupoBusqueda";
            this.grupoBusqueda.Size = new System.Drawing.Size(714, 442);
            this.grupoBusqueda.TabIndex = 5;
            this.grupoBusqueda.TabStop = false;
            this.grupoBusqueda.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(235, 25);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.Btn_BuscarPosTipForPag_Click);
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
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(695, 364);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // grupoOpciones
            // 
            this.grupoOpciones.Controls.Add(this.btnCerrar);
            this.grupoOpciones.Controls.Add(this.btnLimpiar);
            this.grupoOpciones.Controls.Add(this.btnAnular);
            this.grupoOpciones.Controls.Add(this.btnNuevo);
            this.grupoOpciones.Location = new System.Drawing.Point(17, 378);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(342, 83);
            this.grupoOpciones.TabIndex = 4;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(242, 26);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(65, 39);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.Btn_CerrarPosTipForPag_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(171, 26);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(65, 39);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.Btn_LimpiarPosTipForPag_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnular.Location = new System.Drawing.Point(100, 26);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(65, 39);
            this.btnAnular.TabIndex = 1;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.Btn_AnularPosTipForPag_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevo.Location = new System.Drawing.Point(29, 26);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(65, 39);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevoPosTipForPag_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.cmbTipoVenta);
            this.grupoDatos.Controls.Add(this.label5);
            this.grupoDatos.Controls.Add(this.btnClear);
            this.grupoDatos.Controls.Add(this.btnExaminar);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.imgLogo);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtRuta);
            this.grupoDatos.Controls.Add(this.cmbMetodoPago);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.chkPropina);
            this.grupoDatos.Controls.Add(this.cmbTipoDocumento);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.lblDescrTiForPa);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblcodigoTiForPa);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(17, 19);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(342, 353);
            this.grupoDatos.TabIndex = 3;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // cmbTipoVenta
            // 
            this.cmbTipoVenta.FormattingEnabled = true;
            this.cmbTipoVenta.Location = new System.Drawing.Point(100, 209);
            this.cmbTipoVenta.Name = "cmbTipoVenta";
            this.cmbTipoVenta.Size = new System.Drawing.Size(216, 21);
            this.cmbTipoVenta.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(15, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 36);
            this.label5.TabIndex = 22;
            this.label5.Text = "Tipo de Venta:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(278, 275);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(29, 21);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "X";
            this.ttMensaje.SetToolTip(this.btnClear, "Clic aquí para remover la imagen seleccionada");
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.BackColor = System.Drawing.Color.Yellow;
            this.btnExaminar.Location = new System.Drawing.Point(242, 275);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(29, 21);
            this.btnExaminar.TabIndex = 20;
            this.btnExaminar.Text = "...";
            this.ttMensaje.SetToolTip(this.btnExaminar, "Clic aquí para buscar una imegen");
            this.btnExaminar.UseVisualStyleBackColor = false;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(15, 302);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Vista Previa:";
            // 
            // imgLogo
            // 
            this.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgLogo.Location = new System.Drawing.Point(100, 281);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(107, 64);
            this.imgLogo.TabIndex = 18;
            this.imgLogo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(15, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Ruta Imagen:";
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRuta.Location = new System.Drawing.Point(100, 246);
            this.txtRuta.MaxLength = 20;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(207, 20);
            this.txtRuta.TabIndex = 16;
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Location = new System.Drawing.Point(100, 171);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(216, 21);
            this.cmbMetodoPago.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(15, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 36);
            this.label2.TabIndex = 14;
            this.label2.Text = "Método de Pago:";
            // 
            // chkPropina
            // 
            this.chkPropina.AutoSize = true;
            this.chkPropina.Location = new System.Drawing.Point(233, 104);
            this.chkPropina.Name = "chkPropina";
            this.chkPropina.Size = new System.Drawing.Size(83, 17);
            this.chkPropina.TabIndex = 13;
            this.chkPropina.Text = "Lee Propina";
            this.chkPropina.UseVisualStyleBackColor = true;
            // 
            // cmbTipoDocumento
            // 
            this.cmbTipoDocumento.FormattingEnabled = true;
            this.cmbTipoDocumento.Location = new System.Drawing.Point(100, 132);
            this.cmbTipoDocumento.Name = "cmbTipoDocumento";
            this.cmbTipoDocumento.Size = new System.Drawing.Size(216, 21);
            this.cmbTipoDocumento.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(15, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 36);
            this.label1.TabIndex = 11;
            this.label1.Text = "Tipo de documento:";
            // 
            // lblDescrTiForPa
            // 
            this.lblDescrTiForPa.AutoSize = true;
            this.lblDescrTiForPa.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrTiForPa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrTiForPa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrTiForPa.Location = new System.Drawing.Point(15, 52);
            this.lblDescrTiForPa.Name = "lblDescrTiForPa";
            this.lblDescrTiForPa.Size = new System.Drawing.Size(75, 15);
            this.lblDescrTiForPa.TabIndex = 5;
            this.lblDescrTiForPa.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(100, 50);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(216, 44);
            this.txtDescripcion.TabIndex = 4;
            // 
            // lblcodigoTiForPa
            // 
            this.lblcodigoTiForPa.AutoSize = true;
            this.lblcodigoTiForPa.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoTiForPa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoTiForPa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoTiForPa.Location = new System.Drawing.Point(15, 26);
            this.lblcodigoTiForPa.Name = "lblcodigoTiForPa";
            this.lblcodigoTiForPa.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoTiForPa.TabIndex = 3;
            this.lblcodigoTiForPa.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(100, 24);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(216, 20);
            this.txtCodigo.TabIndex = 2;
            this.txtCodigo.Leave += new System.EventHandler(this.Txt_CodigoPosTipForPag_Leave);
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(100, 104);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(83, 17);
            this.chkHabilitado.TabIndex = 61;
            this.chkHabilitado.Text = "Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // FInformacionPosTipForPag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1106, 505);
            this.Controls.Add(this.tabCon_PosTipForPag);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionPosTipForPag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Formas de Pago";
            this.Load += new System.EventHandler(this.FInformacionPosTipForPag_Load);
            this.tabCon_PosTipForPag.ResumeLayout(false);
            this.tabPag_PosTipForPag.ResumeLayout(false);
            this.grupoBusqueda.ResumeLayout(false);
            this.grupoBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_PosTipForPag;
        private System.Windows.Forms.TabPage tabPag_PosTipForPag;
        private System.Windows.Forms.GroupBox grupoBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox grupoOpciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblDescrTiForPa;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblcodigoTiForPa;
        private System.Windows.Forms.TextBox txtCodigo;
        private MisControles.ComboDatos cmbTipoDocumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPropina;
        private MisControles.ComboDatos cmbMetodoPago;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip ttMensaje;
        private MisControles.ComboDatos cmbTipoVenta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkHabilitado;
    }
}