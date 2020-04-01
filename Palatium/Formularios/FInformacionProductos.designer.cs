namespace Palatium.Formularios
{
    partial class FInformacionProductos
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
            this.tabCon_Productos = new System.Windows.Forms.TabControl();
            this.tabPag_Productos = new System.Windows.Forms.TabPage();
            this.Grb_listReProductos = new System.Windows.Forms.GroupBox();
            this.btnBuscarProductos = new System.Windows.Forms.Button();
            this.txtBuscarProductos = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.Grb_opcioProductos = new System.Windows.Forms.GroupBox();
            this.btnCerrarProductos = new System.Windows.Forms.Button();
            this.btnLimpiarProductos = new System.Windows.Forms.Button();
            this.btnAnularProductos = new System.Windows.Forms.Button();
            this.btnNuevoProductos = new System.Windows.Forms.Button();
            this.Grb_DatoProductos = new System.Windows.Forms.GroupBox();
            this.lblStoMax = new System.Windows.Forms.Label();
            this.txtStockMax = new System.Windows.Forms.TextBox();
            this.lblStocMin = new System.Windows.Forms.Label();
            this.txtStockMin = new System.Windows.Forms.TextBox();
            this.lblNivProductos = new System.Windows.Forms.Label();
            this.txtNivProProductos = new System.Windows.Forms.TextBox();
            this.chkPagIceProductos = new System.Windows.Forms.CheckBox();
            this.chkPagIvaProductos = new System.Windows.Forms.CheckBox();
            this.chkPreModifProductos = new System.Windows.Forms.CheckBox();
            this.chkExpiraProductos = new System.Windows.Forms.CheckBox();
            this.chkModifiProductos = new System.Windows.Forms.CheckBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbColoProductos = new System.Windows.Forms.ComboBox();
            this.lblPosProPadre = new System.Windows.Forms.Label();
            this.cmbEstadoProductos = new System.Windows.Forms.ComboBox();
            this.cmbProPadreProductos = new System.Windows.Forms.ComboBox();
            this.lblEstaProducto = new System.Windows.Forms.Label();
            this.lblDescrProductos = new System.Windows.Forms.Label();
            this.txtDescripProductos = new System.Windows.Forms.TextBox();
            this.lblCodigoProductos = new System.Windows.Forms.Label();
            this.txtCodigoProductos = new System.Windows.Forms.TextBox();
            this.tabCon_Productos.SuspendLayout();
            this.tabPag_Productos.SuspendLayout();
            this.Grb_listReProductos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.Grb_opcioProductos.SuspendLayout();
            this.Grb_DatoProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_Productos
            // 
            this.tabCon_Productos.Controls.Add(this.tabPag_Productos);
            this.tabCon_Productos.Location = new System.Drawing.Point(-4, 0);
            this.tabCon_Productos.Name = "tabCon_Productos";
            this.tabCon_Productos.SelectedIndex = 0;
            this.tabCon_Productos.Size = new System.Drawing.Size(1039, 508);
            this.tabCon_Productos.TabIndex = 5;
            // 
            // tabPag_Productos
            // 
            this.tabPag_Productos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_Productos.Controls.Add(this.Grb_listReProductos);
            this.tabPag_Productos.Controls.Add(this.Grb_opcioProductos);
            this.tabPag_Productos.Controls.Add(this.Grb_DatoProductos);
            this.tabPag_Productos.Location = new System.Drawing.Point(4, 22);
            this.tabPag_Productos.Name = "tabPag_Productos";
            this.tabPag_Productos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_Productos.Size = new System.Drawing.Size(1031, 482);
            this.tabPag_Productos.TabIndex = 0;
            this.tabPag_Productos.Text = "Módulo de Productos";
            // 
            // Grb_listReProductos
            // 
            this.Grb_listReProductos.Controls.Add(this.btnBuscarProductos);
            this.Grb_listReProductos.Controls.Add(this.txtBuscarProductos);
            this.Grb_listReProductos.Controls.Add(this.dgvProductos);
            this.Grb_listReProductos.Location = new System.Drawing.Point(495, 19);
            this.Grb_listReProductos.Name = "Grb_listReProductos";
            this.Grb_listReProductos.Size = new System.Drawing.Size(525, 450);
            this.Grb_listReProductos.TabIndex = 5;
            this.Grb_listReProductos.TabStop = false;
            this.Grb_listReProductos.Text = "Lista de Registros";
            // 
            // btnBuscarProductos
            // 
            this.btnBuscarProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarProductos.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscarProductos.Location = new System.Drawing.Point(235, 25);
            this.btnBuscarProductos.Name = "btnBuscarProductos";
            this.btnBuscarProductos.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarProductos.TabIndex = 4;
            this.btnBuscarProductos.Text = "Buscar";
            this.btnBuscarProductos.UseVisualStyleBackColor = false;
            this.btnBuscarProductos.Click += new System.EventHandler(this.btnBuscarProductos_Click);
            // 
            // txtBuscarProductos
            // 
            this.txtBuscarProductos.Location = new System.Drawing.Point(13, 29);
            this.txtBuscarProductos.MaxLength = 20;
            this.txtBuscarProductos.Name = "txtBuscarProductos";
            this.txtBuscarProductos.Size = new System.Drawing.Size(216, 20);
            this.txtBuscarProductos.TabIndex = 3;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(13, 61);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(494, 374);
            this.dgvProductos.TabIndex = 0;
            this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_Productos_CellClick);
            // 
            // Grb_opcioProductos
            // 
            this.Grb_opcioProductos.Controls.Add(this.btnCerrarProductos);
            this.Grb_opcioProductos.Controls.Add(this.btnLimpiarProductos);
            this.Grb_opcioProductos.Controls.Add(this.btnAnularProductos);
            this.Grb_opcioProductos.Controls.Add(this.btnNuevoProductos);
            this.Grb_opcioProductos.Location = new System.Drawing.Point(17, 351);
            this.Grb_opcioProductos.Name = "Grb_opcioProductos";
            this.Grb_opcioProductos.Size = new System.Drawing.Size(472, 118);
            this.Grb_opcioProductos.TabIndex = 4;
            this.Grb_opcioProductos.TabStop = false;
            this.Grb_opcioProductos.Text = "Opciones";
            // 
            // btnCerrarProductos
            // 
            this.btnCerrarProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarProductos.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrarProductos.Location = new System.Drawing.Point(334, 38);
            this.btnCerrarProductos.Name = "btnCerrarProductos";
            this.btnCerrarProductos.Size = new System.Drawing.Size(88, 39);
            this.btnCerrarProductos.TabIndex = 3;
            this.btnCerrarProductos.Text = "Cerrar";
            this.btnCerrarProductos.UseVisualStyleBackColor = false;
            this.btnCerrarProductos.Click += new System.EventHandler(this.Btn_CerrarProductos_Click);
            // 
            // btnLimpiarProductos
            // 
            this.btnLimpiarProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarProductos.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiarProductos.Location = new System.Drawing.Point(240, 39);
            this.btnLimpiarProductos.Name = "btnLimpiarProductos";
            this.btnLimpiarProductos.Size = new System.Drawing.Size(88, 39);
            this.btnLimpiarProductos.TabIndex = 2;
            this.btnLimpiarProductos.Text = "Limpiar";
            this.btnLimpiarProductos.UseVisualStyleBackColor = false;
            this.btnLimpiarProductos.Click += new System.EventHandler(this.Btn_LimpiarProductos_Click);
            // 
            // btnAnularProductos
            // 
            this.btnAnularProductos.BackColor = System.Drawing.Color.Red;
            this.btnAnularProductos.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnularProductos.Location = new System.Drawing.Point(146, 38);
            this.btnAnularProductos.Name = "btnAnularProductos";
            this.btnAnularProductos.Size = new System.Drawing.Size(88, 39);
            this.btnAnularProductos.TabIndex = 1;
            this.btnAnularProductos.Text = "Anular";
            this.btnAnularProductos.UseVisualStyleBackColor = false;
            this.btnAnularProductos.Click += new System.EventHandler(this.Btn_AnularProductos_Click);
            // 
            // btnNuevoProductos
            // 
            this.btnNuevoProductos.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoProductos.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevoProductos.Location = new System.Drawing.Point(52, 39);
            this.btnNuevoProductos.Name = "btnNuevoProductos";
            this.btnNuevoProductos.Size = new System.Drawing.Size(88, 39);
            this.btnNuevoProductos.TabIndex = 0;
            this.btnNuevoProductos.Text = "Nuevo";
            this.btnNuevoProductos.UseVisualStyleBackColor = false;
            this.btnNuevoProductos.Click += new System.EventHandler(this.BtnNuevoProductos_Click);
            // 
            // Grb_DatoProductos
            // 
            this.Grb_DatoProductos.Controls.Add(this.lblStoMax);
            this.Grb_DatoProductos.Controls.Add(this.txtStockMax);
            this.Grb_DatoProductos.Controls.Add(this.lblStocMin);
            this.Grb_DatoProductos.Controls.Add(this.txtStockMin);
            this.Grb_DatoProductos.Controls.Add(this.lblNivProductos);
            this.Grb_DatoProductos.Controls.Add(this.txtNivProProductos);
            this.Grb_DatoProductos.Controls.Add(this.chkPagIceProductos);
            this.Grb_DatoProductos.Controls.Add(this.chkPagIvaProductos);
            this.Grb_DatoProductos.Controls.Add(this.chkPreModifProductos);
            this.Grb_DatoProductos.Controls.Add(this.chkExpiraProductos);
            this.Grb_DatoProductos.Controls.Add(this.chkModifiProductos);
            this.Grb_DatoProductos.Controls.Add(this.lblColor);
            this.Grb_DatoProductos.Controls.Add(this.cmbColoProductos);
            this.Grb_DatoProductos.Controls.Add(this.lblPosProPadre);
            this.Grb_DatoProductos.Controls.Add(this.cmbEstadoProductos);
            this.Grb_DatoProductos.Controls.Add(this.cmbProPadreProductos);
            this.Grb_DatoProductos.Controls.Add(this.lblEstaProducto);
            this.Grb_DatoProductos.Controls.Add(this.lblDescrProductos);
            this.Grb_DatoProductos.Controls.Add(this.txtDescripProductos);
            this.Grb_DatoProductos.Controls.Add(this.lblCodigoProductos);
            this.Grb_DatoProductos.Controls.Add(this.txtCodigoProductos);
            this.Grb_DatoProductos.Enabled = false;
            this.Grb_DatoProductos.Location = new System.Drawing.Point(17, 19);
            this.Grb_DatoProductos.Name = "Grb_DatoProductos";
            this.Grb_DatoProductos.Size = new System.Drawing.Size(470, 279);
            this.Grb_DatoProductos.TabIndex = 3;
            this.Grb_DatoProductos.TabStop = false;
            this.Grb_DatoProductos.Text = "Datos del Registro";
            // 
            // lblStoMax
            // 
            this.lblStoMax.AutoSize = true;
            this.lblStoMax.BackColor = System.Drawing.Color.Transparent;
            this.lblStoMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoMax.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStoMax.Location = new System.Drawing.Point(15, 213);
            this.lblStoMax.Name = "lblStoMax";
            this.lblStoMax.Size = new System.Drawing.Size(88, 15);
            this.lblStoMax.TabIndex = 23;
            this.lblStoMax.Text = "Stock Máximo:";
            // 
            // txtStockMax
            // 
            this.txtStockMax.Location = new System.Drawing.Point(113, 212);
            this.txtStockMax.MaxLength = 20;
            this.txtStockMax.Name = "txtStockMax";
            this.txtStockMax.Size = new System.Drawing.Size(66, 20);
            this.txtStockMax.TabIndex = 22;
            // 
            // lblStocMin
            // 
            this.lblStocMin.AutoSize = true;
            this.lblStocMin.BackColor = System.Drawing.Color.Transparent;
            this.lblStocMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStocMin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStocMin.Location = new System.Drawing.Point(15, 239);
            this.lblStocMin.Name = "lblStocMin";
            this.lblStocMin.Size = new System.Drawing.Size(85, 15);
            this.lblStocMin.TabIndex = 21;
            this.lblStocMin.Text = "Stock Mínimo:";
            // 
            // txtStockMin
            // 
            this.txtStockMin.Location = new System.Drawing.Point(113, 238);
            this.txtStockMin.MaxLength = 20;
            this.txtStockMin.Name = "txtStockMin";
            this.txtStockMin.Size = new System.Drawing.Size(66, 20);
            this.txtStockMin.TabIndex = 20;
            // 
            // lblNivProductos
            // 
            this.lblNivProductos.AutoSize = true;
            this.lblNivProductos.BackColor = System.Drawing.Color.Transparent;
            this.lblNivProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNivProductos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNivProductos.Location = new System.Drawing.Point(15, 187);
            this.lblNivProductos.Name = "lblNivProductos";
            this.lblNivProductos.Size = new System.Drawing.Size(92, 15);
            this.lblNivProductos.TabIndex = 19;
            this.lblNivProductos.Text = "Nivel_producto:";
            // 
            // txtNivProProductos
            // 
            this.txtNivProProductos.Location = new System.Drawing.Point(113, 185);
            this.txtNivProProductos.MaxLength = 20;
            this.txtNivProProductos.Name = "txtNivProProductos";
            this.txtNivProProductos.Size = new System.Drawing.Size(66, 20);
            this.txtNivProProductos.TabIndex = 18;
            // 
            // chkPagIceProductos
            // 
            this.chkPagIceProductos.AutoSize = true;
            this.chkPagIceProductos.Location = new System.Drawing.Point(360, 81);
            this.chkPagIceProductos.Name = "chkPagIceProductos";
            this.chkPagIceProductos.Size = new System.Drawing.Size(72, 17);
            this.chkPagIceProductos.TabIndex = 17;
            this.chkPagIceProductos.Text = "Paga_Ice";
            this.chkPagIceProductos.UseVisualStyleBackColor = true;
            // 
            // chkPagIvaProductos
            // 
            this.chkPagIvaProductos.AutoSize = true;
            this.chkPagIvaProductos.Location = new System.Drawing.Point(360, 132);
            this.chkPagIvaProductos.Name = "chkPagIvaProductos";
            this.chkPagIvaProductos.Size = new System.Drawing.Size(72, 17);
            this.chkPagIvaProductos.TabIndex = 16;
            this.chkPagIvaProductos.Text = "Paga_Iva";
            this.chkPagIvaProductos.UseVisualStyleBackColor = true;
            // 
            // chkPreModifProductos
            // 
            this.chkPreModifProductos.AutoSize = true;
            this.chkPreModifProductos.Location = new System.Drawing.Point(360, 185);
            this.chkPreModifProductos.Name = "chkPreModifProductos";
            this.chkPreModifProductos.Size = new System.Drawing.Size(112, 17);
            this.chkPreModifProductos.TabIndex = 15;
            this.chkPreModifProductos.Text = "Precio modificable";
            this.chkPreModifProductos.UseVisualStyleBackColor = true;
            // 
            // chkExpiraProductos
            // 
            this.chkExpiraProductos.AutoSize = true;
            this.chkExpiraProductos.Location = new System.Drawing.Point(360, 237);
            this.chkExpiraProductos.Name = "chkExpiraProductos";
            this.chkExpiraProductos.Size = new System.Drawing.Size(55, 17);
            this.chkExpiraProductos.TabIndex = 14;
            this.chkExpiraProductos.Text = "Expira";
            this.chkExpiraProductos.UseVisualStyleBackColor = true;
            // 
            // chkModifiProductos
            // 
            this.chkModifiProductos.AutoSize = true;
            this.chkModifiProductos.Location = new System.Drawing.Point(360, 30);
            this.chkModifiProductos.Name = "chkModifiProductos";
            this.chkModifiProductos.Size = new System.Drawing.Size(80, 17);
            this.chkModifiProductos.TabIndex = 13;
            this.chkModifiProductos.Text = "Modificable";
            this.chkModifiProductos.UseVisualStyleBackColor = true;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(16, 138);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 12;
            this.lblColor.Text = "Color:";
            // 
            // cmbColoProductos
            // 
            this.cmbColoProductos.FormattingEnabled = true;
            this.cmbColoProductos.Location = new System.Drawing.Point(113, 130);
            this.cmbColoProductos.Name = "cmbColoProductos";
            this.cmbColoProductos.Size = new System.Drawing.Size(121, 21);
            this.cmbColoProductos.TabIndex = 11;
            this.cmbColoProductos.SelectedIndexChanged += new System.EventHandler(this.Cmb_ColoProductos_SelectedIndexChanged);
            // 
            // lblPosProPadre
            // 
            this.lblPosProPadre.AutoSize = true;
            this.lblPosProPadre.Location = new System.Drawing.Point(16, 112);
            this.lblPosProPadre.Name = "lblPosProPadre";
            this.lblPosProPadre.Size = new System.Drawing.Size(83, 13);
            this.lblPosProPadre.TabIndex = 6;
            this.lblPosProPadre.Text = "Producto padre:";
            // 
            // cmbEstadoProductos
            // 
            this.cmbEstadoProductos.Enabled = false;
            this.cmbEstadoProductos.FormattingEnabled = true;
            this.cmbEstadoProductos.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoProductos.Location = new System.Drawing.Point(113, 158);
            this.cmbEstadoProductos.Name = "cmbEstadoProductos";
            this.cmbEstadoProductos.Size = new System.Drawing.Size(121, 21);
            this.cmbEstadoProductos.TabIndex = 10;
            this.cmbEstadoProductos.SelectedIndexChanged += new System.EventHandler(this.CmbEstadoProductos_SelectedIndexChanged);
            // 
            // cmbProPadreProductos
            // 
            this.cmbProPadreProductos.FormattingEnabled = true;
            this.cmbProPadreProductos.Location = new System.Drawing.Point(113, 104);
            this.cmbProPadreProductos.Name = "cmbProPadreProductos";
            this.cmbProPadreProductos.Size = new System.Drawing.Size(121, 21);
            this.cmbProPadreProductos.TabIndex = 5;
            this.cmbProPadreProductos.SelectedIndexChanged += new System.EventHandler(this.Cmb_ProPadreProductos_SelectedIndexChanged);
            // 
            // lblEstaProducto
            // 
            this.lblEstaProducto.AutoSize = true;
            this.lblEstaProducto.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaProducto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaProducto.Location = new System.Drawing.Point(16, 164);
            this.lblEstaProducto.Name = "lblEstaProducto";
            this.lblEstaProducto.Size = new System.Drawing.Size(48, 15);
            this.lblEstaProducto.TabIndex = 7;
            this.lblEstaProducto.Text = "Estado:";
            // 
            // lblDescrProductos
            // 
            this.lblDescrProductos.AutoSize = true;
            this.lblDescrProductos.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrProductos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrProductos.Location = new System.Drawing.Point(15, 56);
            this.lblDescrProductos.Name = "lblDescrProductos";
            this.lblDescrProductos.Size = new System.Drawing.Size(98, 15);
            this.lblDescrProductos.TabIndex = 5;
            this.lblDescrProductos.Text = "Descripción uso:";
            // 
            // txtDescripProductos
            // 
            this.txtDescripProductos.Location = new System.Drawing.Point(113, 54);
            this.txtDescripProductos.MaxLength = 20;
            this.txtDescripProductos.Multiline = true;
            this.txtDescripProductos.Name = "txtDescripProductos";
            this.txtDescripProductos.Size = new System.Drawing.Size(216, 44);
            this.txtDescripProductos.TabIndex = 4;
            // 
            // lblCodigoProductos
            // 
            this.lblCodigoProductos.AutoSize = true;
            this.lblCodigoProductos.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoProductos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigoProductos.Location = new System.Drawing.Point(15, 30);
            this.lblCodigoProductos.Name = "lblCodigoProductos";
            this.lblCodigoProductos.Size = new System.Drawing.Size(49, 15);
            this.lblCodigoProductos.TabIndex = 3;
            this.lblCodigoProductos.Text = "Código:";
            // 
            // txtCodigoProductos
            // 
            this.txtCodigoProductos.Location = new System.Drawing.Point(113, 28);
            this.txtCodigoProductos.MaxLength = 20;
            this.txtCodigoProductos.Name = "txtCodigoProductos";
            this.txtCodigoProductos.Size = new System.Drawing.Size(216, 20);
            this.txtCodigoProductos.TabIndex = 2;
            // 
            // FInformacionProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1029, 505);
            this.Controls.Add(this.tabCon_Productos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información de Productos";
            this.Load += new System.EventHandler(this.FInformacionProductos_Load);
            this.tabCon_Productos.ResumeLayout(false);
            this.tabPag_Productos.ResumeLayout(false);
            this.Grb_listReProductos.ResumeLayout(false);
            this.Grb_listReProductos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.Grb_opcioProductos.ResumeLayout(false);
            this.Grb_DatoProductos.ResumeLayout(false);
            this.Grb_DatoProductos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_Productos;
        private System.Windows.Forms.TabPage tabPag_Productos;
        private System.Windows.Forms.GroupBox Grb_listReProductos;
        private System.Windows.Forms.Button btnBuscarProductos;
        private System.Windows.Forms.TextBox txtBuscarProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox Grb_opcioProductos;
        private System.Windows.Forms.Button btnCerrarProductos;
        private System.Windows.Forms.Button btnLimpiarProductos;
        private System.Windows.Forms.Button btnAnularProductos;
        private System.Windows.Forms.Button btnNuevoProductos;
        private System.Windows.Forms.GroupBox Grb_DatoProductos;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cmbColoProductos;
        private System.Windows.Forms.Label lblPosProPadre;
        private System.Windows.Forms.ComboBox cmbEstadoProductos;
        private System.Windows.Forms.ComboBox cmbProPadreProductos;
        private System.Windows.Forms.Label lblEstaProducto;
        private System.Windows.Forms.Label lblDescrProductos;
        private System.Windows.Forms.TextBox txtDescripProductos;
        private System.Windows.Forms.Label lblCodigoProductos;
        private System.Windows.Forms.TextBox txtCodigoProductos;
        private System.Windows.Forms.Label lblStoMax;
        private System.Windows.Forms.TextBox txtStockMax;
        private System.Windows.Forms.Label lblStocMin;
        private System.Windows.Forms.TextBox txtStockMin;
        private System.Windows.Forms.Label lblNivProductos;
        private System.Windows.Forms.TextBox txtNivProProductos;
        private System.Windows.Forms.CheckBox chkPagIceProductos;
        private System.Windows.Forms.CheckBox chkPagIvaProductos;
        private System.Windows.Forms.CheckBox chkPreModifProductos;
        private System.Windows.Forms.CheckBox chkExpiraProductos;
        private System.Windows.Forms.CheckBox chkModifiProductos;
    }
}