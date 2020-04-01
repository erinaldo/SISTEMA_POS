namespace Palatium.Formularios
{
    partial class FSubCategoria
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
            this.tabCon_categori = new System.Windows.Forms.TabControl();
            this.tabPag_Categori = new System.Windows.Forms.TabPage();
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscarCategoria = new System.Windows.Forms.Button();
            this.txtBuscarCategoria = new System.Windows.Forms.TextBox();
            this.statusStrip_cajero = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.Grb_opcioCategori = new System.Windows.Forms.GroupBox();
            this.btnCerrarCategori = new System.Windows.Forms.Button();
            this.btnLimpiarCategori = new System.Windows.Forms.Button();
            this.btnAnularCategori = new System.Windows.Forms.Button();
            this.btnNuevoCategori = new System.Windows.Forms.Button();
            this.Grb_DatoCategori = new System.Windows.Forms.GroupBox();
            this.lblSecuencia = new System.Windows.Forms.Label();
            this.txtSecuencia = new System.Windows.Forms.TextBox();
            this.chkPagaIva = new System.Windows.Forms.CheckBox();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.chkMayusculas = new System.Windows.Forms.CheckBox();
            this.cmbConsumo = new MisControles.ComboDatos();
            this.cmbCompra = new MisControles.ComboDatos();
            this.cmbPadre = new MisControles.ComboDatos();
            this.chkPreModificable = new System.Windows.Forms.CheckBox();
            this.chkModificable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUnidadCompra = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.cmbEstadoCatego = new System.Windows.Forms.ComboBox();
            this.lblEstaCajero = new System.Windows.Forms.Label();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripCateg = new System.Windows.Forms.TextBox();
            this.lblCodCategoria = new System.Windows.Forms.Label();
            this.txtCodigoCatego = new System.Windows.Forms.TextBox();
            this.tabCon_categori.SuspendLayout();
            this.tabPag_Categori.SuspendLayout();
            this.Grb_listReCajero.SuspendLayout();
            this.statusStrip_cajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.Grb_opcioCategori.SuspendLayout();
            this.Grb_DatoCategori.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_categori
            // 
            this.tabCon_categori.Controls.Add(this.tabPag_Categori);
            this.tabCon_categori.Location = new System.Drawing.Point(12, 12);
            this.tabCon_categori.Name = "tabCon_categori";
            this.tabCon_categori.SelectedIndex = 0;
            this.tabCon_categori.Size = new System.Drawing.Size(958, 579);
            this.tabCon_categori.TabIndex = 3;
            // 
            // tabPag_Categori
            // 
            this.tabPag_Categori.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_Categori.Controls.Add(this.Grb_listReCajero);
            this.tabPag_Categori.Controls.Add(this.Grb_opcioCategori);
            this.tabPag_Categori.Controls.Add(this.Grb_DatoCategori);
            this.tabPag_Categori.Location = new System.Drawing.Point(4, 22);
            this.tabPag_Categori.Name = "tabPag_Categori";
            this.tabPag_Categori.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_Categori.Size = new System.Drawing.Size(950, 553);
            this.tabPag_Categori.TabIndex = 0;
            this.tabPag_Categori.Text = "Mantenimiento de Categorías";
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.Controls.Add(this.btnBuscarCategoria);
            this.Grb_listReCajero.Controls.Add(this.txtBuscarCategoria);
            this.Grb_listReCajero.Controls.Add(this.statusStrip_cajero);
            this.Grb_listReCajero.Controls.Add(this.dgvProductos);
            this.Grb_listReCajero.Location = new System.Drawing.Point(383, 19);
            this.Grb_listReCajero.Name = "Grb_listReCajero";
            this.Grb_listReCajero.Size = new System.Drawing.Size(549, 511);
            this.Grb_listReCajero.TabIndex = 5;
            this.Grb_listReCajero.TabStop = false;
            this.Grb_listReCajero.Text = "Lista de Registros";
            // 
            // btnBuscarCategoria
            // 
            this.btnBuscarCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarCategoria.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCategoria.Location = new System.Drawing.Point(237, 16);
            this.btnBuscarCategoria.Name = "btnBuscarCategoria";
            this.btnBuscarCategoria.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarCategoria.TabIndex = 11;
            this.btnBuscarCategoria.Text = "Buscar";
            this.btnBuscarCategoria.UseVisualStyleBackColor = false;
            this.btnBuscarCategoria.Click += new System.EventHandler(this.btnBuscarCategoria_Click);
            // 
            // txtBuscarCategoria
            // 
            this.txtBuscarCategoria.Location = new System.Drawing.Point(15, 20);
            this.txtBuscarCategoria.MaxLength = 20;
            this.txtBuscarCategoria.Name = "txtBuscarCategoria";
            this.txtBuscarCategoria.Size = new System.Drawing.Size(216, 20);
            this.txtBuscarCategoria.TabIndex = 10;
            // 
            // statusStrip_cajero
            // 
            this.statusStrip_cajero.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip_cajero.Location = new System.Drawing.Point(3, 203);
            this.statusStrip_cajero.Name = "statusStrip_cajero";
            this.statusStrip_cajero.Size = new System.Drawing.Size(830, 22);
            this.statusStrip_cajero.TabIndex = 9;
            this.statusStrip_cajero.Text = "statusStrip1";
            this.statusStrip_cajero.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(15, 54);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.Size = new System.Drawing.Size(517, 451);
            this.dgvProductos.TabIndex = 0;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellClick);
            // 
            // Grb_opcioCategori
            // 
            this.Grb_opcioCategori.Controls.Add(this.btnCerrarCategori);
            this.Grb_opcioCategori.Controls.Add(this.btnLimpiarCategori);
            this.Grb_opcioCategori.Controls.Add(this.btnAnularCategori);
            this.Grb_opcioCategori.Controls.Add(this.btnNuevoCategori);
            this.Grb_opcioCategori.Location = new System.Drawing.Point(17, 457);
            this.Grb_opcioCategori.Name = "Grb_opcioCategori";
            this.Grb_opcioCategori.Size = new System.Drawing.Size(349, 73);
            this.Grb_opcioCategori.TabIndex = 4;
            this.Grb_opcioCategori.TabStop = false;
            this.Grb_opcioCategori.Text = "Opciones";
            // 
            // btnCerrarCategori
            // 
            this.btnCerrarCategori.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarCategori.ForeColor = System.Drawing.Color.White;
            this.btnCerrarCategori.Location = new System.Drawing.Point(247, 19);
            this.btnCerrarCategori.Name = "btnCerrarCategori";
            this.btnCerrarCategori.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarCategori.TabIndex = 3;
            this.btnCerrarCategori.Text = "Cerrar";
            this.btnCerrarCategori.UseVisualStyleBackColor = false;
            this.btnCerrarCategori.Click += new System.EventHandler(this.btnCerrarCategori_Click);
            // 
            // btnLimpiarCategori
            // 
            this.btnLimpiarCategori.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarCategori.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarCategori.Location = new System.Drawing.Point(171, 19);
            this.btnLimpiarCategori.Name = "btnLimpiarCategori";
            this.btnLimpiarCategori.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiarCategori.TabIndex = 2;
            this.btnLimpiarCategori.Text = "Limpiar";
            this.btnLimpiarCategori.UseVisualStyleBackColor = false;
            this.btnLimpiarCategori.Click += new System.EventHandler(this.btnLimpiarCategori_Click);
            // 
            // btnAnularCategori
            // 
            this.btnAnularCategori.BackColor = System.Drawing.Color.Red;
            this.btnAnularCategori.ForeColor = System.Drawing.Color.White;
            this.btnAnularCategori.Location = new System.Drawing.Point(95, 19);
            this.btnAnularCategori.Name = "btnAnularCategori";
            this.btnAnularCategori.Size = new System.Drawing.Size(70, 39);
            this.btnAnularCategori.TabIndex = 1;
            this.btnAnularCategori.Text = "Anular";
            this.btnAnularCategori.UseVisualStyleBackColor = false;
            this.btnAnularCategori.Click += new System.EventHandler(this.btnAnularCategori_Click);
            // 
            // btnNuevoCategori
            // 
            this.btnNuevoCategori.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoCategori.ForeColor = System.Drawing.Color.White;
            this.btnNuevoCategori.Location = new System.Drawing.Point(19, 19);
            this.btnNuevoCategori.Name = "btnNuevoCategori";
            this.btnNuevoCategori.Size = new System.Drawing.Size(70, 39);
            this.btnNuevoCategori.TabIndex = 0;
            this.btnNuevoCategori.Text = "Nuevo";
            this.btnNuevoCategori.UseVisualStyleBackColor = false;
            this.btnNuevoCategori.Click += new System.EventHandler(this.btnNuevoCategori_Click);
            // 
            // Grb_DatoCategori
            // 
            this.Grb_DatoCategori.Controls.Add(this.lblSecuencia);
            this.Grb_DatoCategori.Controls.Add(this.txtSecuencia);
            this.Grb_DatoCategori.Controls.Add(this.chkPagaIva);
            this.Grb_DatoCategori.Controls.Add(this.cmbEmpresa);
            this.Grb_DatoCategori.Controls.Add(this.lblEmpresa);
            this.Grb_DatoCategori.Controls.Add(this.chkMayusculas);
            this.Grb_DatoCategori.Controls.Add(this.cmbConsumo);
            this.Grb_DatoCategori.Controls.Add(this.cmbCompra);
            this.Grb_DatoCategori.Controls.Add(this.cmbPadre);
            this.Grb_DatoCategori.Controls.Add(this.chkPreModificable);
            this.Grb_DatoCategori.Controls.Add(this.chkModificable);
            this.Grb_DatoCategori.Controls.Add(this.label1);
            this.Grb_DatoCategori.Controls.Add(this.lblUnidadCompra);
            this.Grb_DatoCategori.Controls.Add(this.lblCodigo);
            this.Grb_DatoCategori.Controls.Add(this.cmbEstadoCatego);
            this.Grb_DatoCategori.Controls.Add(this.lblEstaCajero);
            this.Grb_DatoCategori.Controls.Add(this.lblDescrCajero);
            this.Grb_DatoCategori.Controls.Add(this.txtDescripCateg);
            this.Grb_DatoCategori.Controls.Add(this.lblCodCategoria);
            this.Grb_DatoCategori.Controls.Add(this.txtCodigoCatego);
            this.Grb_DatoCategori.Enabled = false;
            this.Grb_DatoCategori.Location = new System.Drawing.Point(17, 19);
            this.Grb_DatoCategori.Name = "Grb_DatoCategori";
            this.Grb_DatoCategori.Size = new System.Drawing.Size(349, 432);
            this.Grb_DatoCategori.TabIndex = 3;
            this.Grb_DatoCategori.TabStop = false;
            this.Grb_DatoCategori.Text = "Datos del Registro";
            // 
            // lblSecuencia
            // 
            this.lblSecuencia.AutoSize = true;
            this.lblSecuencia.BackColor = System.Drawing.Color.Transparent;
            this.lblSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecuencia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSecuencia.Location = new System.Drawing.Point(12, 209);
            this.lblSecuencia.Name = "lblSecuencia";
            this.lblSecuencia.Size = new System.Drawing.Size(68, 15);
            this.lblSecuencia.TabIndex = 49;
            this.lblSecuencia.Text = "Secuencia:";
            // 
            // txtSecuencia
            // 
            this.txtSecuencia.Location = new System.Drawing.Point(120, 204);
            this.txtSecuencia.MaxLength = 3;
            this.txtSecuencia.Name = "txtSecuencia";
            this.txtSecuencia.Size = new System.Drawing.Size(63, 20);
            this.txtSecuencia.TabIndex = 48;
            this.txtSecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecuencia_KeyPress);
            // 
            // chkPagaIva
            // 
            this.chkPagaIva.AutoSize = true;
            this.chkPagaIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPagaIva.Location = new System.Drawing.Point(15, 394);
            this.chkPagaIva.Name = "chkPagaIva";
            this.chkPagaIva.Size = new System.Drawing.Size(73, 19);
            this.chkPagaIva.TabIndex = 47;
            this.chkPagaIva.Text = "Paga Iva";
            this.chkPagaIva.UseVisualStyleBackColor = true;
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(121, 30);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(197, 21);
            this.cmbEmpresa.TabIndex = 45;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(13, 35);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(60, 15);
            this.lblEmpresa.TabIndex = 44;
            this.lblEmpresa.Text = "Empresa:";
            // 
            // chkMayusculas
            // 
            this.chkMayusculas.AutoSize = true;
            this.chkMayusculas.Location = new System.Drawing.Point(211, 181);
            this.chkMayusculas.Name = "chkMayusculas";
            this.chkMayusculas.Size = new System.Drawing.Size(110, 17);
            this.chkMayusculas.TabIndex = 46;
            this.chkMayusculas.Text = "Todo Mayúsculas";
            this.chkMayusculas.UseVisualStyleBackColor = true;
            this.chkMayusculas.CheckedChanged += new System.EventHandler(this.chkMayusculas_CheckedChanged);
            // 
            // cmbConsumo
            // 
            this.cmbConsumo.FormattingEnabled = true;
            this.cmbConsumo.Location = new System.Drawing.Point(122, 275);
            this.cmbConsumo.Name = "cmbConsumo";
            this.cmbConsumo.Size = new System.Drawing.Size(106, 21);
            this.cmbConsumo.TabIndex = 41;
            // 
            // cmbCompra
            // 
            this.cmbCompra.FormattingEnabled = true;
            this.cmbCompra.Location = new System.Drawing.Point(121, 243);
            this.cmbCompra.Name = "cmbCompra";
            this.cmbCompra.Size = new System.Drawing.Size(106, 21);
            this.cmbCompra.TabIndex = 40;
            // 
            // cmbPadre
            // 
            this.cmbPadre.FormattingEnabled = true;
            this.cmbPadre.Location = new System.Drawing.Point(121, 67);
            this.cmbPadre.Name = "cmbPadre";
            this.cmbPadre.Size = new System.Drawing.Size(106, 21);
            this.cmbPadre.TabIndex = 39;
            this.cmbPadre.SelectedIndexChanged += new System.EventHandler(this.cmbPadre_SelectedIndexChanged);
            // 
            // chkPreModificable
            // 
            this.chkPreModificable.AutoSize = true;
            this.chkPreModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPreModificable.Location = new System.Drawing.Point(15, 369);
            this.chkPreModificable.Name = "chkPreModificable";
            this.chkPreModificable.Size = new System.Drawing.Size(128, 19);
            this.chkPreModificable.TabIndex = 30;
            this.chkPreModificable.Text = "Precio modificable";
            this.chkPreModificable.UseVisualStyleBackColor = true;
            // 
            // chkModificable
            // 
            this.chkModificable.AutoSize = true;
            this.chkModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModificable.Location = new System.Drawing.Point(15, 344);
            this.chkModificable.Name = "chkModificable";
            this.chkModificable.Size = new System.Drawing.Size(90, 19);
            this.chkModificable.TabIndex = 28;
            this.chkModificable.Text = "Modificable";
            this.chkModificable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(13, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Unidad Consumo:";
            // 
            // lblUnidadCompra
            // 
            this.lblUnidadCompra.AutoSize = true;
            this.lblUnidadCompra.BackColor = System.Drawing.Color.Transparent;
            this.lblUnidadCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadCompra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUnidadCompra.Location = new System.Drawing.Point(13, 249);
            this.lblUnidadCompra.Name = "lblUnidadCompra";
            this.lblUnidadCompra.Size = new System.Drawing.Size(97, 15);
            this.lblUnidadCompra.TabIndex = 13;
            this.lblUnidadCompra.Text = "Unidad Compra:";
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigo.Location = new System.Drawing.Point(13, 74);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(60, 15);
            this.lblCodigo.TabIndex = 11;
            this.lblCodigo.Text = "Categoría";
            // 
            // cmbEstadoCatego
            // 
            this.cmbEstadoCatego.Enabled = false;
            this.cmbEstadoCatego.FormattingEnabled = true;
            this.cmbEstadoCatego.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoCatego.Location = new System.Drawing.Point(121, 308);
            this.cmbEstadoCatego.Name = "cmbEstadoCatego";
            this.cmbEstadoCatego.Size = new System.Drawing.Size(107, 21);
            this.cmbEstadoCatego.TabIndex = 10;
            this.cmbEstadoCatego.SelectedIndexChanged += new System.EventHandler(this.cmbEstadoCatego_SelectedIndexChanged);
            // 
            // lblEstaCajero
            // 
            this.lblEstaCajero.AutoSize = true;
            this.lblEstaCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaCajero.Location = new System.Drawing.Point(13, 314);
            this.lblEstaCajero.Name = "lblEstaCajero";
            this.lblEstaCajero.Size = new System.Drawing.Size(48, 15);
            this.lblEstaCajero.TabIndex = 7;
            this.lblEstaCajero.Text = "Estado:";
            // 
            // lblDescrCajero
            // 
            this.lblDescrCajero.AutoSize = true;
            this.lblDescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrCajero.Location = new System.Drawing.Point(13, 133);
            this.lblDescrCajero.Name = "lblDescrCajero";
            this.lblDescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lblDescrCajero.TabIndex = 5;
            this.lblDescrCajero.Text = "Descripción:";
            // 
            // txtDescripCateg
            // 
            this.txtDescripCateg.Location = new System.Drawing.Point(121, 131);
            this.txtDescripCateg.MaxLength = 200;
            this.txtDescripCateg.Multiline = true;
            this.txtDescripCateg.Name = "txtDescripCateg";
            this.txtDescripCateg.Size = new System.Drawing.Size(200, 44);
            this.txtDescripCateg.TabIndex = 4;
            // 
            // lblCodCategoria
            // 
            this.lblCodCategoria.AutoSize = true;
            this.lblCodCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblCodCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodCategoria.Location = new System.Drawing.Point(13, 107);
            this.lblCodCategoria.Name = "lblCodCategoria";
            this.lblCodCategoria.Size = new System.Drawing.Size(49, 15);
            this.lblCodCategoria.TabIndex = 3;
            this.lblCodCategoria.Text = "Código ";
            // 
            // txtCodigoCatego
            // 
            this.txtCodigoCatego.Location = new System.Drawing.Point(121, 102);
            this.txtCodigoCatego.MaxLength = 20;
            this.txtCodigoCatego.Name = "txtCodigoCatego";
            this.txtCodigoCatego.Size = new System.Drawing.Size(107, 20);
            this.txtCodigoCatego.TabIndex = 2;
            // 
            // FSubCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(975, 595);
            this.Controls.Add(this.tabCon_categori);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FSubCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Sub Categorias";
            this.Load += new System.EventHandler(this.FSubCategorias_Load);
            this.tabCon_categori.ResumeLayout(false);
            this.tabPag_Categori.ResumeLayout(false);
            this.Grb_listReCajero.ResumeLayout(false);
            this.Grb_listReCajero.PerformLayout();
            this.statusStrip_cajero.ResumeLayout(false);
            this.statusStrip_cajero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.Grb_opcioCategori.ResumeLayout(false);
            this.Grb_DatoCategori.ResumeLayout(false);
            this.Grb_DatoCategori.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_categori;
        private System.Windows.Forms.TabPage tabPag_Categori;
        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.Button btnBuscarCategoria;
        private System.Windows.Forms.TextBox txtBuscarCategoria;
        private System.Windows.Forms.StatusStrip statusStrip_cajero;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox Grb_opcioCategori;
        private System.Windows.Forms.Button btnCerrarCategori;
        private System.Windows.Forms.Button btnLimpiarCategori;
        private System.Windows.Forms.Button btnAnularCategori;
        private System.Windows.Forms.Button btnNuevoCategori;
        private System.Windows.Forms.GroupBox Grb_DatoCategori;
        private System.Windows.Forms.Label lblSecuencia;
        private System.Windows.Forms.CheckBox chkPagaIva;
        private MisControles.ComboDatos cmbConsumo;
        private MisControles.ComboDatos cmbCompra;
        private System.Windows.Forms.CheckBox chkPreModificable;
        private System.Windows.Forms.CheckBox chkModificable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUnidadCompra;
        private System.Windows.Forms.ComboBox cmbEstadoCatego;
        private System.Windows.Forms.Label lblEstaCajero;
        private System.Windows.Forms.TextBox txtSecuencia;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.CheckBox chkMayusculas;
        private MisControles.ComboDatos cmbPadre;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripCateg;
        private System.Windows.Forms.Label lblCodCategoria;
        private System.Windows.Forms.TextBox txtCodigoCatego;


    }
}