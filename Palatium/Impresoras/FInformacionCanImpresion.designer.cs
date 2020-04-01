namespace Palatium.Impresoras
{
    partial class FInformacionCanImpresion
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
            this.tabCon_CanImpre = new System.Windows.Forms.TabControl();
            this.tabPag_CanImpre = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTerminales = new MisControles.ComboDatos();
            this.label7 = new System.Windows.Forms.Label();
            this.Grb_listReCanImpre = new System.Windows.Forms.GroupBox();
            this.btnBuscarCanImpre = new System.Windows.Forms.Button();
            this.txtBuscarCanImpre = new System.Windows.Forms.TextBox();
            this.dgvCanImpre = new System.Windows.Forms.DataGridView();
            this.Grb_opcioCanImpre = new System.Windows.Forms.GroupBox();
            this.btnCerrarCanImpre = new System.Windows.Forms.Button();
            this.btnLimpiarCanImpre = new System.Windows.Forms.Button();
            this.btnAnularCanImpre = new System.Windows.Forms.Button();
            this.btnNuevoCanImpre = new System.Windows.Forms.Button();
            this.Grb_DatoCanImpre = new System.Windows.Forms.GroupBox();
            this.chkAbrirCajon = new System.Windows.Forms.CheckBox();
            this.chkCortarPapel = new System.Windows.Forms.CheckBox();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbImpresoras = new System.Windows.Forms.ComboBox();
            this.lblId = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtIPAsignada = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtPuertoImpresora = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCantidadImpresiones = new System.Windows.Forms.TextBox();
            this.cmbEstadoCanImpre = new System.Windows.Forms.ComboBox();
            this.lblEstaCajero = new System.Windows.Forms.Label();
            this.lbldescrCajero = new System.Windows.Forms.Label();
            this.txtDescripCanImpre = new System.Windows.Forms.TextBox();
            this.lblcodigoCajero = new System.Windows.Forms.Label();
            this.txtCodigoCanImpre = new System.Windows.Forms.TextBox();
            this.tabCon_CanImpre.SuspendLayout();
            this.tabPag_CanImpre.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Grb_listReCanImpre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanImpre)).BeginInit();
            this.Grb_opcioCanImpre.SuspendLayout();
            this.Grb_DatoCanImpre.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_CanImpre
            // 
            this.tabCon_CanImpre.Controls.Add(this.tabPag_CanImpre);
            this.tabCon_CanImpre.Location = new System.Drawing.Point(0, 2);
            this.tabCon_CanImpre.Name = "tabCon_CanImpre";
            this.tabCon_CanImpre.SelectedIndex = 0;
            this.tabCon_CanImpre.Size = new System.Drawing.Size(977, 532);
            this.tabCon_CanImpre.TabIndex = 2;
            // 
            // tabPag_CanImpre
            // 
            this.tabPag_CanImpre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_CanImpre.Controls.Add(this.groupBox1);
            this.tabPag_CanImpre.Controls.Add(this.Grb_listReCanImpre);
            this.tabPag_CanImpre.Controls.Add(this.Grb_opcioCanImpre);
            this.tabPag_CanImpre.Controls.Add(this.Grb_DatoCanImpre);
            this.tabPag_CanImpre.Location = new System.Drawing.Point(4, 22);
            this.tabPag_CanImpre.Name = "tabPag_CanImpre";
            this.tabPag_CanImpre.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_CanImpre.Size = new System.Drawing.Size(969, 506);
            this.tabPag_CanImpre.TabIndex = 0;
            this.tabPag_CanImpre.Text = "Módulo de Canal_Impresión";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTerminales);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(17, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 66);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Terminales Registradas";
            // 
            // cmbTerminales
            // 
            this.cmbTerminales.FormattingEnabled = true;
            this.cmbTerminales.Location = new System.Drawing.Point(92, 27);
            this.cmbTerminales.Name = "cmbTerminales";
            this.cmbTerminales.Size = new System.Drawing.Size(236, 21);
            this.cmbTerminales.TabIndex = 23;
            this.cmbTerminales.SelectedIndexChanged += new System.EventHandler(this.cmbTerminales_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(14, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "Terminales:";
            // 
            // Grb_listReCanImpre
            // 
            this.Grb_listReCanImpre.Controls.Add(this.btnBuscarCanImpre);
            this.Grb_listReCanImpre.Controls.Add(this.txtBuscarCanImpre);
            this.Grb_listReCanImpre.Controls.Add(this.dgvCanImpre);
            this.Grb_listReCanImpre.Location = new System.Drawing.Point(379, 19);
            this.Grb_listReCanImpre.Name = "Grb_listReCanImpre";
            this.Grb_listReCanImpre.Size = new System.Drawing.Size(572, 479);
            this.Grb_listReCanImpre.TabIndex = 5;
            this.Grb_listReCanImpre.TabStop = false;
            this.Grb_listReCanImpre.Text = "Lista de Registros";
            // 
            // btnBuscarCanImpre
            // 
            this.btnBuscarCanImpre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarCanImpre.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCanImpre.Location = new System.Drawing.Point(235, 25);
            this.btnBuscarCanImpre.Name = "btnBuscarCanImpre";
            this.btnBuscarCanImpre.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarCanImpre.TabIndex = 2;
            this.btnBuscarCanImpre.Text = "Buscar";
            this.btnBuscarCanImpre.UseVisualStyleBackColor = false;
            this.btnBuscarCanImpre.Click += new System.EventHandler(this.Btn_BuscarCanImpre_Click);
            // 
            // txtBuscarCanImpre
            // 
            this.txtBuscarCanImpre.Location = new System.Drawing.Point(13, 28);
            this.txtBuscarCanImpre.MaxLength = 20;
            this.txtBuscarCanImpre.Name = "txtBuscarCanImpre";
            this.txtBuscarCanImpre.Size = new System.Drawing.Size(216, 20);
            this.txtBuscarCanImpre.TabIndex = 1;
            // 
            // dgvCanImpre
            // 
            this.dgvCanImpre.AllowUserToAddRows = false;
            this.dgvCanImpre.AllowUserToDeleteRows = false;
            this.dgvCanImpre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanImpre.Location = new System.Drawing.Point(13, 61);
            this.dgvCanImpre.Name = "dgvCanImpre";
            this.dgvCanImpre.ReadOnly = true;
            this.dgvCanImpre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCanImpre.Size = new System.Drawing.Size(535, 403);
            this.dgvCanImpre.TabIndex = 0;
            this.dgvCanImpre.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_CanImpre_CellClick);
            // 
            // Grb_opcioCanImpre
            // 
            this.Grb_opcioCanImpre.Controls.Add(this.btnCerrarCanImpre);
            this.Grb_opcioCanImpre.Controls.Add(this.btnLimpiarCanImpre);
            this.Grb_opcioCanImpre.Controls.Add(this.btnAnularCanImpre);
            this.Grb_opcioCanImpre.Controls.Add(this.btnNuevoCanImpre);
            this.Grb_opcioCanImpre.Location = new System.Drawing.Point(17, 422);
            this.Grb_opcioCanImpre.Name = "Grb_opcioCanImpre";
            this.Grb_opcioCanImpre.Size = new System.Drawing.Size(342, 76);
            this.Grb_opcioCanImpre.TabIndex = 4;
            this.Grb_opcioCanImpre.TabStop = false;
            this.Grb_opcioCanImpre.Text = "Opciones";
            // 
            // btnCerrarCanImpre
            // 
            this.btnCerrarCanImpre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarCanImpre.ForeColor = System.Drawing.Color.White;
            this.btnCerrarCanImpre.Location = new System.Drawing.Point(238, 26);
            this.btnCerrarCanImpre.Name = "btnCerrarCanImpre";
            this.btnCerrarCanImpre.Size = new System.Drawing.Size(63, 39);
            this.btnCerrarCanImpre.TabIndex = 14;
            this.btnCerrarCanImpre.Text = "Cerrar";
            this.btnCerrarCanImpre.UseVisualStyleBackColor = false;
            this.btnCerrarCanImpre.Click += new System.EventHandler(this.Btn_CerrarCanImpre_Click);
            // 
            // btnLimpiarCanImpre
            // 
            this.btnLimpiarCanImpre.BackColor = System.Drawing.Color.Lime;
            this.btnLimpiarCanImpre.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarCanImpre.Location = new System.Drawing.Point(169, 26);
            this.btnLimpiarCanImpre.Name = "btnLimpiarCanImpre";
            this.btnLimpiarCanImpre.Size = new System.Drawing.Size(63, 39);
            this.btnLimpiarCanImpre.TabIndex = 13;
            this.btnLimpiarCanImpre.Text = "Limpiar";
            this.btnLimpiarCanImpre.UseVisualStyleBackColor = false;
            this.btnLimpiarCanImpre.Click += new System.EventHandler(this.Btn_LimpiarCanImpre_Click);
            // 
            // btnAnularCanImpre
            // 
            this.btnAnularCanImpre.BackColor = System.Drawing.Color.Red;
            this.btnAnularCanImpre.ForeColor = System.Drawing.Color.White;
            this.btnAnularCanImpre.Location = new System.Drawing.Point(100, 26);
            this.btnAnularCanImpre.Name = "btnAnularCanImpre";
            this.btnAnularCanImpre.Size = new System.Drawing.Size(63, 39);
            this.btnAnularCanImpre.TabIndex = 12;
            this.btnAnularCanImpre.Text = "Anular";
            this.btnAnularCanImpre.UseVisualStyleBackColor = false;
            this.btnAnularCanImpre.Click += new System.EventHandler(this.Btn_AnularCanImpre_Click);
            // 
            // btnNuevoCanImpre
            // 
            this.btnNuevoCanImpre.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoCanImpre.ForeColor = System.Drawing.Color.White;
            this.btnNuevoCanImpre.Location = new System.Drawing.Point(31, 26);
            this.btnNuevoCanImpre.Name = "btnNuevoCanImpre";
            this.btnNuevoCanImpre.Size = new System.Drawing.Size(63, 39);
            this.btnNuevoCanImpre.TabIndex = 11;
            this.btnNuevoCanImpre.Text = "Nuevo";
            this.btnNuevoCanImpre.UseVisualStyleBackColor = false;
            this.btnNuevoCanImpre.Click += new System.EventHandler(this.BtnNuevoCanImpre_Click);
            // 
            // Grb_DatoCanImpre
            // 
            this.Grb_DatoCanImpre.Controls.Add(this.chkAbrirCajon);
            this.Grb_DatoCanImpre.Controls.Add(this.chkCortarPapel);
            this.Grb_DatoCanImpre.Controls.Add(this.cmbLocalidad);
            this.Grb_DatoCanImpre.Controls.Add(this.label6);
            this.Grb_DatoCanImpre.Controls.Add(this.cmbImpresoras);
            this.Grb_DatoCanImpre.Controls.Add(this.lblId);
            this.Grb_DatoCanImpre.Controls.Add(this.label5);
            this.Grb_DatoCanImpre.Controls.Add(this.label4);
            this.Grb_DatoCanImpre.Controls.Add(this.TxtIPAsignada);
            this.Grb_DatoCanImpre.Controls.Add(this.label3);
            this.Grb_DatoCanImpre.Controls.Add(this.TxtPuertoImpresora);
            this.Grb_DatoCanImpre.Controls.Add(this.label2);
            this.Grb_DatoCanImpre.Controls.Add(this.label1);
            this.Grb_DatoCanImpre.Controls.Add(this.TxtCantidadImpresiones);
            this.Grb_DatoCanImpre.Controls.Add(this.cmbEstadoCanImpre);
            this.Grb_DatoCanImpre.Controls.Add(this.lblEstaCajero);
            this.Grb_DatoCanImpre.Controls.Add(this.lbldescrCajero);
            this.Grb_DatoCanImpre.Controls.Add(this.txtDescripCanImpre);
            this.Grb_DatoCanImpre.Controls.Add(this.lblcodigoCajero);
            this.Grb_DatoCanImpre.Controls.Add(this.txtCodigoCanImpre);
            this.Grb_DatoCanImpre.Enabled = false;
            this.Grb_DatoCanImpre.Location = new System.Drawing.Point(17, 91);
            this.Grb_DatoCanImpre.Name = "Grb_DatoCanImpre";
            this.Grb_DatoCanImpre.Size = new System.Drawing.Size(342, 325);
            this.Grb_DatoCanImpre.TabIndex = 3;
            this.Grb_DatoCanImpre.TabStop = false;
            this.Grb_DatoCanImpre.Text = "Datos del Registro";
            // 
            // chkAbrirCajon
            // 
            this.chkAbrirCajon.AutoSize = true;
            this.chkAbrirCajon.Location = new System.Drawing.Point(184, 296);
            this.chkAbrirCajon.Name = "chkAbrirCajon";
            this.chkAbrirCajon.Size = new System.Drawing.Size(126, 17);
            this.chkAbrirCajon.TabIndex = 24;
            this.chkAbrirCajon.Text = "Abrir Cajón de Dinero";
            this.chkAbrirCajon.UseVisualStyleBackColor = true;
            // 
            // chkCortarPapel
            // 
            this.chkCortarPapel.AutoSize = true;
            this.chkCortarPapel.Location = new System.Drawing.Point(38, 296);
            this.chkCortarPapel.Name = "chkCortarPapel";
            this.chkCortarPapel.Size = new System.Drawing.Size(84, 17);
            this.chkCortarPapel.TabIndex = 23;
            this.chkCortarPapel.Text = "Cortar Papel";
            this.chkCortarPapel.UseVisualStyleBackColor = true;
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(119, 26);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(193, 21);
            this.cmbLocalidad.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(12, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "Localidad:";
            // 
            // cmbImpresoras
            // 
            this.cmbImpresoras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImpresoras.FormattingEnabled = true;
            this.cmbImpresoras.Location = new System.Drawing.Point(119, 149);
            this.cmbImpresoras.Name = "cmbImpresoras";
            this.cmbImpresoras.Size = new System.Drawing.Size(193, 21);
            this.cmbImpresoras.TabIndex = 7;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(262, 308);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 20;
            this.lblId.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(201, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Max. 99";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(12, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "IP Asignada:";
            // 
            // TxtIPAsignada
            // 
            this.TxtIPAsignada.Location = new System.Drawing.Point(119, 226);
            this.TxtIPAsignada.MaxLength = 20;
            this.TxtIPAsignada.Multiline = true;
            this.TxtIPAsignada.Name = "TxtIPAsignada";
            this.TxtIPAsignada.Size = new System.Drawing.Size(195, 20);
            this.TxtIPAsignada.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(12, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 39);
            this.label3.TabIndex = 16;
            this.label3.Text = "Puerto de la Impresora";
            // 
            // TxtPuertoImpresora
            // 
            this.TxtPuertoImpresora.Location = new System.Drawing.Point(119, 180);
            this.TxtPuertoImpresora.MaxLength = 50;
            this.TxtPuertoImpresora.Multiline = true;
            this.TxtPuertoImpresora.Name = "TxtPuertoImpresora";
            this.TxtPuertoImpresora.Size = new System.Drawing.Size(195, 41);
            this.TxtPuertoImpresora.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 51);
            this.label2.TabIndex = 14;
            this.label2.Text = "Nombre de la Impresora";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(12, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "No. Impresiones:";
            // 
            // TxtCantidadImpresiones
            // 
            this.TxtCantidadImpresiones.Location = new System.Drawing.Point(119, 123);
            this.TxtCantidadImpresiones.MaxLength = 20;
            this.TxtCantidadImpresiones.Name = "TxtCantidadImpresiones";
            this.TxtCantidadImpresiones.Size = new System.Drawing.Size(76, 20);
            this.TxtCantidadImpresiones.TabIndex = 6;
            // 
            // cmbEstadoCanImpre
            // 
            this.cmbEstadoCanImpre.Enabled = false;
            this.cmbEstadoCanImpre.FormattingEnabled = true;
            this.cmbEstadoCanImpre.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoCanImpre.Location = new System.Drawing.Point(119, 251);
            this.cmbEstadoCanImpre.Name = "cmbEstadoCanImpre";
            this.cmbEstadoCanImpre.Size = new System.Drawing.Size(107, 21);
            this.cmbEstadoCanImpre.TabIndex = 10;
            this.cmbEstadoCanImpre.SelectedIndexChanged += new System.EventHandler(this.CmbEstadoCanImpre_SelectedIndexChanged);
            // 
            // lblEstaCajero
            // 
            this.lblEstaCajero.AutoSize = true;
            this.lblEstaCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaCajero.Location = new System.Drawing.Point(12, 251);
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
            this.lbldescrCajero.Location = new System.Drawing.Point(12, 77);
            this.lbldescrCajero.Name = "lbldescrCajero";
            this.lbldescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lbldescrCajero.TabIndex = 5;
            this.lbldescrCajero.Text = "Descripción:";
            // 
            // txtDescripCanImpre
            // 
            this.txtDescripCanImpre.Location = new System.Drawing.Point(119, 75);
            this.txtDescripCanImpre.MaxLength = 20;
            this.txtDescripCanImpre.Multiline = true;
            this.txtDescripCanImpre.Name = "txtDescripCanImpre";
            this.txtDescripCanImpre.Size = new System.Drawing.Size(195, 44);
            this.txtDescripCanImpre.TabIndex = 5;
            // 
            // lblcodigoCajero
            // 
            this.lblcodigoCajero.AutoSize = true;
            this.lblcodigoCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblcodigoCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcodigoCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblcodigoCajero.Location = new System.Drawing.Point(12, 53);
            this.lblcodigoCajero.Name = "lblcodigoCajero";
            this.lblcodigoCajero.Size = new System.Drawing.Size(49, 15);
            this.lblcodigoCajero.TabIndex = 3;
            this.lblcodigoCajero.Text = "Código:";
            // 
            // txtCodigoCanImpre
            // 
            this.txtCodigoCanImpre.Location = new System.Drawing.Point(119, 51);
            this.txtCodigoCanImpre.MaxLength = 20;
            this.txtCodigoCanImpre.Name = "txtCodigoCanImpre";
            this.txtCodigoCanImpre.Size = new System.Drawing.Size(195, 20);
            this.txtCodigoCanImpre.TabIndex = 4;
            this.txtCodigoCanImpre.Leave += new System.EventHandler(this.Txt_CodigoCanImpre_Leave);
            // 
            // FInformacionCanImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(964, 534);
            this.Controls.Add(this.tabCon_CanImpre);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionCanImpresion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Canales de Impresión";
            this.Load += new System.EventHandler(this.FInformacionCanImpresion_Load);
            this.tabCon_CanImpre.ResumeLayout(false);
            this.tabPag_CanImpre.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Grb_listReCanImpre.ResumeLayout(false);
            this.Grb_listReCanImpre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanImpre)).EndInit();
            this.Grb_opcioCanImpre.ResumeLayout(false);
            this.Grb_DatoCanImpre.ResumeLayout(false);
            this.Grb_DatoCanImpre.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_CanImpre;
        private System.Windows.Forms.TabPage tabPag_CanImpre;
        private System.Windows.Forms.GroupBox Grb_listReCanImpre;
        private System.Windows.Forms.Button btnBuscarCanImpre;
        private System.Windows.Forms.TextBox txtBuscarCanImpre;
        private System.Windows.Forms.DataGridView dgvCanImpre;
        private System.Windows.Forms.GroupBox Grb_opcioCanImpre;
        private System.Windows.Forms.Button btnCerrarCanImpre;
        private System.Windows.Forms.Button btnLimpiarCanImpre;
        private System.Windows.Forms.Button btnAnularCanImpre;
        private System.Windows.Forms.Button btnNuevoCanImpre;
        private System.Windows.Forms.GroupBox Grb_DatoCanImpre;
        private System.Windows.Forms.ComboBox cmbEstadoCanImpre;
        private System.Windows.Forms.Label lblEstaCajero;
        private System.Windows.Forms.Label lbldescrCajero;
        private System.Windows.Forms.TextBox txtDescripCanImpre;
        private System.Windows.Forms.Label lblcodigoCajero;
        private System.Windows.Forms.TextBox txtCodigoCanImpre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtIPAsignada;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtPuertoImpresora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCantidadImpresiones;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.ComboBox cmbImpresoras;
        private System.Windows.Forms.Label label6;
        private MisControles.ComboDatos cmbLocalidad;
        private System.Windows.Forms.GroupBox groupBox1;
        private MisControles.ComboDatos cmbTerminales;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkAbrirCajon;
        private System.Windows.Forms.CheckBox chkCortarPapel;
    }
}