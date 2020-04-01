namespace Palatium.Oficina
{
    partial class frmIngresoProductosCategoria
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
            this.grupoEncabezado = new System.Windows.Forms.GroupBox();
            this.lblEtiquetaCategoria = new System.Windows.Forms.Label();
            this.cmbCategorias = new MisControles.ComboDatos();
            this.cmbPadre = new MisControles.ComboDatos();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblListaNombre = new System.Windows.Forms.Label();
            this.txtIdCategoria = new System.Windows.Forms.TextBox();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.cmbClaseProducto = new MisControles.ComboDatos();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTipoProducto = new MisControles.ComboDatos();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDecripCategoria = new System.Windows.Forms.Label();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.lblCodiCategori = new System.Windows.Forms.Label();
            this.txtCodigoProducto = new System.Windows.Forms.TextBox();
            this.cmbDestinoImpresion = new MisControles.ComboDatos();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStoMax = new System.Windows.Forms.Label();
            this.txtStockMax = new System.Windows.Forms.TextBox();
            this.lblStocMin = new System.Windows.Forms.Label();
            this.txtSecuencia = new System.Windows.Forms.TextBox();
            this.lblSecuencia = new System.Windows.Forms.Label();
            this.txtStockMin = new System.Windows.Forms.TextBox();
            this.chkPreModifProductos = new System.Windows.Forms.CheckBox();
            this.chkExpiraProductos = new System.Windows.Forms.CheckBox();
            this.chkPagaIva = new System.Windows.Forms.CheckBox();
            this.cmbConsumo = new MisControles.ComboDatos();
            this.cmbCompra = new MisControles.ComboDatos();
            this.lblUniConsumo = new System.Windows.Forms.Label();
            this.lblUnidadCompra = new System.Windows.Forms.Label();
            this.lblPrecioMinorista = new System.Windows.Forms.Label();
            this.txtPrecioMinorista = new System.Windows.Forms.TextBox();
            this.lblPreCompra = new System.Windows.Forms.Label();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.grupoGrid = new System.Windows.Forms.GroupBox();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.grupoBotones = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.grupoReceta = new System.Windows.Forms.GroupBox();
            this.rdbReferenciaInsumos = new System.Windows.Forms.RadioButton();
            this.rdbReceta = new System.Windows.Forms.RadioButton();
            this.BtnLimpiarDbAyuda = new System.Windows.Forms.Button();
            this.dbAyudaReceta = new Controles.Auxiliares.DB_Ayuda();
            this.grupoPrecio = new System.Windows.Forms.GroupBox();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.grupoStock = new System.Windows.Forms.GroupBox();
            this.grupoImpresion = new System.Windows.Forms.GroupBox();
            this.grupoEncabezado.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.grupoGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.grupoBotones.SuspendLayout();
            this.grupoReceta.SuspendLayout();
            this.grupoPrecio.SuspendLayout();
            this.grupoOpciones.SuspendLayout();
            this.grupoStock.SuspendLayout();
            this.grupoImpresion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupoEncabezado
            // 
            this.grupoEncabezado.Controls.Add(this.lblEtiquetaCategoria);
            this.grupoEncabezado.Controls.Add(this.cmbCategorias);
            this.grupoEncabezado.Controls.Add(this.cmbPadre);
            this.grupoEncabezado.Controls.Add(this.lblCodigo);
            this.grupoEncabezado.Controls.Add(this.btnOK);
            this.grupoEncabezado.Controls.Add(this.btnAbrir);
            this.grupoEncabezado.Controls.Add(this.txtDescripcion);
            this.grupoEncabezado.Controls.Add(this.lblListaNombre);
            this.grupoEncabezado.Controls.Add(this.txtIdCategoria);
            this.grupoEncabezado.Location = new System.Drawing.Point(12, 12);
            this.grupoEncabezado.Name = "grupoEncabezado";
            this.grupoEncabezado.Size = new System.Drawing.Size(1104, 70);
            this.grupoEncabezado.TabIndex = 29;
            this.grupoEncabezado.TabStop = false;
            this.grupoEncabezado.Text = "Lista de categoría";
            // 
            // lblEtiquetaCategoria
            // 
            this.lblEtiquetaCategoria.AutoSize = true;
            this.lblEtiquetaCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblEtiquetaCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEtiquetaCategoria.Location = new System.Drawing.Point(374, 16);
            this.lblEtiquetaCategoria.Name = "lblEtiquetaCategoria";
            this.lblEtiquetaCategoria.Size = new System.Drawing.Size(129, 15);
            this.lblEtiquetaCategoria.TabIndex = 34;
            this.lblEtiquetaCategoria.Text = "Listado de Categorías:";
            this.lblEtiquetaCategoria.Visible = false;
            // 
            // cmbCategorias
            // 
            this.cmbCategorias.FormattingEnabled = true;
            this.cmbCategorias.Location = new System.Drawing.Point(375, 36);
            this.cmbCategorias.Name = "cmbCategorias";
            this.cmbCategorias.Size = new System.Drawing.Size(176, 21);
            this.cmbCategorias.TabIndex = 2;
            this.cmbCategorias.Visible = false;
            // 
            // cmbPadre
            // 
            this.cmbPadre.FormattingEnabled = true;
            this.cmbPadre.Location = new System.Drawing.Point(100, 36);
            this.cmbPadre.Name = "cmbPadre";
            this.cmbPadre.Size = new System.Drawing.Size(196, 21);
            this.cmbPadre.TabIndex = 1;
            this.cmbPadre.SelectedIndexChanged += new System.EventHandler(this.cmbPadre_SelectedIndexChanged);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigo.Location = new System.Drawing.Point(8, 36);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(81, 15);
            this.lblCodigo.TabIndex = 32;
            this.lblCodigo.Text = "Código padre";
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Blue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(1027, 30);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 31);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(745, 34);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(35, 23);
            this.btnAbrir.TabIndex = 4;
            this.btnAbrir.Text = "?";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(784, 36);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(237, 20);
            this.txtDescripcion.TabIndex = 5;
            // 
            // lblListaNombre
            // 
            this.lblListaNombre.AutoSize = true;
            this.lblListaNombre.BackColor = System.Drawing.Color.Transparent;
            this.lblListaNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListaNombre.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblListaNombre.Location = new System.Drawing.Point(641, 16);
            this.lblListaNombre.Name = "lblListaNombre";
            this.lblListaNombre.Size = new System.Drawing.Size(154, 15);
            this.lblListaNombre.TabIndex = 28;
            this.lblListaNombre.Text = "Listado de Sub Categorías:";
            this.lblListaNombre.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdCategoria
            // 
            this.txtIdCategoria.Location = new System.Drawing.Point(642, 36);
            this.txtIdCategoria.MaxLength = 0;
            this.txtIdCategoria.Name = "txtIdCategoria";
            this.txtIdCategoria.Size = new System.Drawing.Size(97, 20);
            this.txtIdCategoria.TabIndex = 3;
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.cmbClaseProducto);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.cmbTipoProducto);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.lblDecripCategoria);
            this.grupoDatos.Controls.Add(this.txtNombreProducto);
            this.grupoDatos.Controls.Add(this.lblCodiCategori);
            this.grupoDatos.Controls.Add(this.txtCodigoProducto);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 88);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(313, 129);
            this.grupoDatos.TabIndex = 30;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Lista de Registros";
            // 
            // cmbClaseProducto
            // 
            this.cmbClaseProducto.FormattingEnabled = true;
            this.cmbClaseProducto.Location = new System.Drawing.Point(154, 94);
            this.cmbClaseProducto.Name = "cmbClaseProducto";
            this.cmbClaseProducto.Size = new System.Drawing.Size(142, 21);
            this.cmbClaseProducto.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(6, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 68;
            this.label4.Text = "Clase de Producto";
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Location = new System.Drawing.Point(154, 72);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(142, 21);
            this.cmbTipoProducto.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 15);
            this.label3.TabIndex = 66;
            this.label3.Text = "Tipo de Producto";
            // 
            // lblDecripCategoria
            // 
            this.lblDecripCategoria.AutoSize = true;
            this.lblDecripCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblDecripCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecripCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDecripCategoria.Location = new System.Drawing.Point(7, 52);
            this.lblDecripCategoria.Name = "lblDecripCategoria";
            this.lblDecripCategoria.Size = new System.Drawing.Size(134, 15);
            this.lblDecripCategoria.TabIndex = 32;
            this.lblDecripCategoria.Text = "Nombre del producto: *";
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreProducto.Location = new System.Drawing.Point(154, 51);
            this.txtNombreProducto.MaxLength = 30;
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.Size = new System.Drawing.Size(142, 20);
            this.txtNombreProducto.TabIndex = 8;
            // 
            // lblCodiCategori
            // 
            this.lblCodiCategori.AutoSize = true;
            this.lblCodiCategori.BackColor = System.Drawing.Color.Transparent;
            this.lblCodiCategori.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodiCategori.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodiCategori.Location = new System.Drawing.Point(7, 31);
            this.lblCodiCategori.Name = "lblCodiCategori";
            this.lblCodiCategori.Size = new System.Drawing.Size(128, 15);
            this.lblCodiCategori.TabIndex = 30;
            this.lblCodiCategori.Text = "Código del producto: *";
            // 
            // txtCodigoProducto
            // 
            this.txtCodigoProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoProducto.Location = new System.Drawing.Point(154, 30);
            this.txtCodigoProducto.MaxLength = 20;
            this.txtCodigoProducto.Name = "txtCodigoProducto";
            this.txtCodigoProducto.Size = new System.Drawing.Size(142, 20);
            this.txtCodigoProducto.TabIndex = 7;
            // 
            // cmbDestinoImpresion
            // 
            this.cmbDestinoImpresion.FormattingEnabled = true;
            this.cmbDestinoImpresion.Location = new System.Drawing.Point(11, 59);
            this.cmbDestinoImpresion.Name = "cmbDestinoImpresion";
            this.cmbDestinoImpresion.Size = new System.Drawing.Size(203, 24);
            this.cmbDestinoImpresion.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(12, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 15);
            this.label5.TabIndex = 70;
            this.label5.Text = "Destino de Impresión: *";
            // 
            // lblStoMax
            // 
            this.lblStoMax.AutoSize = true;
            this.lblStoMax.BackColor = System.Drawing.Color.Transparent;
            this.lblStoMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoMax.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStoMax.Location = new System.Drawing.Point(15, 51);
            this.lblStoMax.Name = "lblStoMax";
            this.lblStoMax.Size = new System.Drawing.Size(88, 15);
            this.lblStoMax.TabIndex = 59;
            this.lblStoMax.Text = "Stock Máximo:";
            // 
            // txtStockMax
            // 
            this.txtStockMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtStockMax.Location = new System.Drawing.Point(109, 50);
            this.txtStockMax.MaxLength = 20;
            this.txtStockMax.Name = "txtStockMax";
            this.txtStockMax.Size = new System.Drawing.Size(106, 20);
            this.txtStockMax.TabIndex = 19;
            this.txtStockMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockMax_KeyPress);
            // 
            // lblStocMin
            // 
            this.lblStocMin.AutoSize = true;
            this.lblStocMin.BackColor = System.Drawing.Color.Transparent;
            this.lblStocMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStocMin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblStocMin.Location = new System.Drawing.Point(15, 29);
            this.lblStocMin.Name = "lblStocMin";
            this.lblStocMin.Size = new System.Drawing.Size(85, 15);
            this.lblStocMin.TabIndex = 57;
            this.lblStocMin.Text = "Stock Mínimo:";
            // 
            // txtSecuencia
            // 
            this.txtSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSecuencia.Location = new System.Drawing.Point(109, 72);
            this.txtSecuencia.MaxLength = 20;
            this.txtSecuencia.Name = "txtSecuencia";
            this.txtSecuencia.Size = new System.Drawing.Size(106, 20);
            this.txtSecuencia.TabIndex = 20;
            this.txtSecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecuencia_KeyPress);
            // 
            // lblSecuencia
            // 
            this.lblSecuencia.AutoSize = true;
            this.lblSecuencia.BackColor = System.Drawing.Color.Transparent;
            this.lblSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecuencia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSecuencia.Location = new System.Drawing.Point(16, 73);
            this.lblSecuencia.Name = "lblSecuencia";
            this.lblSecuencia.Size = new System.Drawing.Size(76, 15);
            this.lblSecuencia.TabIndex = 50;
            this.lblSecuencia.Text = "Secuencia: *";
            // 
            // txtStockMin
            // 
            this.txtStockMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtStockMin.Location = new System.Drawing.Point(109, 28);
            this.txtStockMin.MaxLength = 20;
            this.txtStockMin.Name = "txtStockMin";
            this.txtStockMin.Size = new System.Drawing.Size(106, 20);
            this.txtStockMin.TabIndex = 18;
            this.txtStockMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockMin_KeyPress);
            // 
            // chkPreModifProductos
            // 
            this.chkPreModifProductos.AutoSize = true;
            this.chkPreModifProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPreModifProductos.Location = new System.Drawing.Point(18, 48);
            this.chkPreModifProductos.Name = "chkPreModifProductos";
            this.chkPreModifProductos.Size = new System.Drawing.Size(128, 19);
            this.chkPreModifProductos.TabIndex = 16;
            this.chkPreModifProductos.Text = "Precio modificable";
            this.chkPreModifProductos.UseVisualStyleBackColor = true;
            // 
            // chkExpiraProductos
            // 
            this.chkExpiraProductos.AutoSize = true;
            this.chkExpiraProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExpiraProductos.Location = new System.Drawing.Point(18, 69);
            this.chkExpiraProductos.Name = "chkExpiraProductos";
            this.chkExpiraProductos.Size = new System.Drawing.Size(61, 19);
            this.chkExpiraProductos.TabIndex = 17;
            this.chkExpiraProductos.Text = "Expira";
            this.chkExpiraProductos.UseVisualStyleBackColor = true;
            // 
            // chkPagaIva
            // 
            this.chkPagaIva.AutoSize = true;
            this.chkPagaIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPagaIva.Location = new System.Drawing.Point(18, 28);
            this.chkPagaIva.Name = "chkPagaIva";
            this.chkPagaIva.Size = new System.Drawing.Size(73, 19);
            this.chkPagaIva.TabIndex = 15;
            this.chkPagaIva.Text = "Paga Iva";
            this.chkPagaIva.UseVisualStyleBackColor = true;
            // 
            // cmbConsumo
            // 
            this.cmbConsumo.Enabled = false;
            this.cmbConsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbConsumo.FormattingEnabled = true;
            this.cmbConsumo.Location = new System.Drawing.Point(185, 91);
            this.cmbConsumo.Name = "cmbConsumo";
            this.cmbConsumo.Size = new System.Drawing.Size(111, 21);
            this.cmbConsumo.TabIndex = 14;
            // 
            // cmbCompra
            // 
            this.cmbCompra.Enabled = false;
            this.cmbCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbCompra.FormattingEnabled = true;
            this.cmbCompra.Location = new System.Drawing.Point(185, 69);
            this.cmbCompra.Name = "cmbCompra";
            this.cmbCompra.Size = new System.Drawing.Size(111, 21);
            this.cmbCompra.TabIndex = 13;
            // 
            // lblUniConsumo
            // 
            this.lblUniConsumo.AutoSize = true;
            this.lblUniConsumo.BackColor = System.Drawing.Color.Transparent;
            this.lblUniConsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUniConsumo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUniConsumo.Location = new System.Drawing.Point(13, 94);
            this.lblUniConsumo.Name = "lblUniConsumo";
            this.lblUniConsumo.Size = new System.Drawing.Size(114, 15);
            this.lblUniConsumo.TabIndex = 43;
            this.lblUniConsumo.Text = "Unidad Consumo: *";
            // 
            // lblUnidadCompra
            // 
            this.lblUnidadCompra.AutoSize = true;
            this.lblUnidadCompra.BackColor = System.Drawing.Color.Transparent;
            this.lblUnidadCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadCompra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUnidadCompra.Location = new System.Drawing.Point(13, 72);
            this.lblUnidadCompra.Name = "lblUnidadCompra";
            this.lblUnidadCompra.Size = new System.Drawing.Size(105, 15);
            this.lblUnidadCompra.TabIndex = 42;
            this.lblUnidadCompra.Text = "Unidad Compra: *";
            // 
            // lblPrecioMinorista
            // 
            this.lblPrecioMinorista.AutoSize = true;
            this.lblPrecioMinorista.BackColor = System.Drawing.Color.Transparent;
            this.lblPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioMinorista.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrecioMinorista.Location = new System.Drawing.Point(13, 51);
            this.lblPrecioMinorista.Name = "lblPrecioMinorista";
            this.lblPrecioMinorista.Size = new System.Drawing.Size(141, 15);
            this.lblPrecioMinorista.TabIndex = 36;
            this.lblPrecioMinorista.Text = "Precio Minorista (PVP): *";
            // 
            // txtPrecioMinorista
            // 
            this.txtPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioMinorista.Location = new System.Drawing.Point(185, 48);
            this.txtPrecioMinorista.MaxLength = 20;
            this.txtPrecioMinorista.Name = "txtPrecioMinorista";
            this.txtPrecioMinorista.Size = new System.Drawing.Size(111, 20);
            this.txtPrecioMinorista.TabIndex = 12;
            this.txtPrecioMinorista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioMinorista_KeyPress);
            // 
            // lblPreCompra
            // 
            this.lblPreCompra.AutoSize = true;
            this.lblPreCompra.BackColor = System.Drawing.Color.Transparent;
            this.lblPreCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreCompra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPreCompra.Location = new System.Drawing.Point(13, 29);
            this.lblPreCompra.Name = "lblPreCompra";
            this.lblPreCompra.Size = new System.Drawing.Size(100, 15);
            this.lblPreCompra.TabIndex = 34;
            this.lblPreCompra.Text = "Precio Compra: *";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioCompra.Location = new System.Drawing.Point(185, 27);
            this.txtPrecioCompra.MaxLength = 20;
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(111, 20);
            this.txtPrecioCompra.TabIndex = 11;
            this.txtPrecioCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCompra_KeyPress);
            // 
            // grupoGrid
            // 
            this.grupoGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoGrid.Controls.Add(this.btnBuscarProducto);
            this.grupoGrid.Controls.Add(this.txtBuscarProducto);
            this.grupoGrid.Controls.Add(this.dgvProductos);
            this.grupoGrid.Enabled = false;
            this.grupoGrid.Location = new System.Drawing.Point(656, 94);
            this.grupoGrid.Name = "grupoGrid";
            this.grupoGrid.Size = new System.Drawing.Size(461, 316);
            this.grupoGrid.TabIndex = 37;
            this.grupoGrid.TabStop = false;
            this.grupoGrid.Text = "Lista de Registros para Búsqueda por Nombres";
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarProducto.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProducto.Location = new System.Drawing.Point(230, 19);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarProducto.TabIndex = 27;
            this.btnBuscarProducto.Text = "Buscar";
            this.ttMensaje.SetToolTip(this.btnBuscarProducto, "Buscarxfgdfg\r\nsdfsdfsdfsdfsdf\r\nsdfsdfsdfsdfsdfsdfsd\r\nsdffffffffffffffffffffffffff" +
        "ffff");
            this.btnBuscarProducto.UseVisualStyleBackColor = false;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Location = new System.Drawing.Point(16, 23);
            this.txtBuscarProducto.MaxLength = 20;
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(208, 20);
            this.txtBuscarProducto.TabIndex = 26;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(16, 51);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersWidth = 25;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(428, 251);
            this.dgvProductos.TabIndex = 28;
            this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellDoubleClick);
            // 
            // grupoBotones
            // 
            this.grupoBotones.Controls.Add(this.btnEliminar);
            this.grupoBotones.Controls.Add(this.btnLimpiar);
            this.grupoBotones.Controls.Add(this.btnAgregar);
            this.grupoBotones.Enabled = false;
            this.grupoBotones.Location = new System.Drawing.Point(420, 338);
            this.grupoBotones.Name = "grupoBotones";
            this.grupoBotones.Size = new System.Drawing.Size(230, 72);
            this.grupoBotones.TabIndex = 36;
            this.grupoBotones.TabStop = false;
            this.grupoBotones.Text = "Opciones";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(148, 19);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 39);
            this.btnEliminar.TabIndex = 25;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(76, 19);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 24;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiarCajero_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Blue;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(6, 19);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(70, 39);
            this.btnAgregar.TabIndex = 23;
            this.btnAgregar.Text = "Nuevo";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // grupoReceta
            // 
            this.grupoReceta.Controls.Add(this.rdbReferenciaInsumos);
            this.grupoReceta.Controls.Add(this.rdbReceta);
            this.grupoReceta.Controls.Add(this.BtnLimpiarDbAyuda);
            this.grupoReceta.Controls.Add(this.dbAyudaReceta);
            this.grupoReceta.Enabled = false;
            this.grupoReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoReceta.Location = new System.Drawing.Point(12, 338);
            this.grupoReceta.Name = "grupoReceta";
            this.grupoReceta.Size = new System.Drawing.Size(402, 72);
            this.grupoReceta.TabIndex = 35;
            this.grupoReceta.TabStop = false;
            // 
            // rdbReferenciaInsumos
            // 
            this.rdbReferenciaInsumos.AutoSize = true;
            this.rdbReferenciaInsumos.Location = new System.Drawing.Point(196, 0);
            this.rdbReferenciaInsumos.Name = "rdbReferenciaInsumos";
            this.rdbReferenciaInsumos.Size = new System.Drawing.Size(164, 20);
            this.rdbReferenciaInsumos.TabIndex = 39;
            this.rdbReferenciaInsumos.Text = "Referencia de insumos";
            this.rdbReferenciaInsumos.UseVisualStyleBackColor = true;
            this.rdbReferenciaInsumos.CheckedChanged += new System.EventHandler(this.rdbReferenciaInsumos_CheckedChanged);
            // 
            // rdbReceta
            // 
            this.rdbReceta.AutoSize = true;
            this.rdbReceta.Checked = true;
            this.rdbReceta.Location = new System.Drawing.Point(11, 0);
            this.rdbReceta.Name = "rdbReceta";
            this.rdbReceta.Size = new System.Drawing.Size(134, 20);
            this.rdbReceta.TabIndex = 38;
            this.rdbReceta.TabStop = true;
            this.rdbReceta.Text = "Control de Receta";
            this.rdbReceta.UseVisualStyleBackColor = true;
            this.rdbReceta.CheckedChanged += new System.EventHandler(this.rdbReceta_CheckedChanged);
            // 
            // BtnLimpiarDbAyuda
            // 
            this.BtnLimpiarDbAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLimpiarDbAyuda.ForeColor = System.Drawing.Color.Red;
            this.BtnLimpiarDbAyuda.Location = new System.Drawing.Point(367, 24);
            this.BtnLimpiarDbAyuda.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnLimpiarDbAyuda.Name = "BtnLimpiarDbAyuda";
            this.BtnLimpiarDbAyuda.Size = new System.Drawing.Size(28, 25);
            this.BtnLimpiarDbAyuda.TabIndex = 38;
            this.BtnLimpiarDbAyuda.Text = "X";
            this.BtnLimpiarDbAyuda.UseVisualStyleBackColor = true;
            this.BtnLimpiarDbAyuda.Click += new System.EventHandler(this.BtnLimpiarDbAyuda_Click);
            // 
            // dbAyudaReceta
            // 
            this.dbAyudaReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbAyudaReceta.iId = 21;
            this.dbAyudaReceta.Location = new System.Drawing.Point(8, 26);
            this.dbAyudaReceta.Margin = new System.Windows.Forms.Padding(4);
            this.dbAyudaReceta.Name = "dbAyudaReceta";
            this.dbAyudaReceta.sCodigo = null;
            this.dbAyudaReceta.Size = new System.Drawing.Size(352, 22);
            this.dbAyudaReceta.sNombre = null;
            this.dbAyudaReceta.TabIndex = 22;
            // 
            // grupoPrecio
            // 
            this.grupoPrecio.Controls.Add(this.txtPrecioCompra);
            this.grupoPrecio.Controls.Add(this.txtPrecioMinorista);
            this.grupoPrecio.Controls.Add(this.cmbCompra);
            this.grupoPrecio.Controls.Add(this.cmbConsumo);
            this.grupoPrecio.Controls.Add(this.lblPreCompra);
            this.grupoPrecio.Controls.Add(this.lblPrecioMinorista);
            this.grupoPrecio.Controls.Add(this.lblUnidadCompra);
            this.grupoPrecio.Controls.Add(this.lblUniConsumo);
            this.grupoPrecio.Enabled = false;
            this.grupoPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoPrecio.Location = new System.Drawing.Point(337, 87);
            this.grupoPrecio.Name = "grupoPrecio";
            this.grupoPrecio.Size = new System.Drawing.Size(313, 130);
            this.grupoPrecio.TabIndex = 31;
            this.grupoPrecio.TabStop = false;
            this.grupoPrecio.Text = "Control de Precio";
            // 
            // grupoOpciones
            // 
            this.grupoOpciones.Controls.Add(this.chkPagaIva);
            this.grupoOpciones.Controls.Add(this.chkPreModifProductos);
            this.grupoOpciones.Controls.Add(this.chkExpiraProductos);
            this.grupoOpciones.Enabled = false;
            this.grupoOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoOpciones.Location = new System.Drawing.Point(12, 223);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(161, 109);
            this.grupoOpciones.TabIndex = 32;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
            // 
            // grupoStock
            // 
            this.grupoStock.Controls.Add(this.lblStocMin);
            this.grupoStock.Controls.Add(this.txtStockMin);
            this.grupoStock.Controls.Add(this.lblSecuencia);
            this.grupoStock.Controls.Add(this.txtSecuencia);
            this.grupoStock.Controls.Add(this.txtStockMax);
            this.grupoStock.Controls.Add(this.lblStoMax);
            this.grupoStock.Enabled = false;
            this.grupoStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoStock.Location = new System.Drawing.Point(179, 223);
            this.grupoStock.Name = "grupoStock";
            this.grupoStock.Size = new System.Drawing.Size(231, 109);
            this.grupoStock.TabIndex = 33;
            this.grupoStock.TabStop = false;
            this.grupoStock.Text = "Control Stock";
            // 
            // grupoImpresion
            // 
            this.grupoImpresion.Controls.Add(this.cmbDestinoImpresion);
            this.grupoImpresion.Controls.Add(this.label5);
            this.grupoImpresion.Enabled = false;
            this.grupoImpresion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoImpresion.Location = new System.Drawing.Point(416, 223);
            this.grupoImpresion.Name = "grupoImpresion";
            this.grupoImpresion.Size = new System.Drawing.Size(234, 109);
            this.grupoImpresion.TabIndex = 34;
            this.grupoImpresion.TabStop = false;
            this.grupoImpresion.Text = "Control de Destino  de Impresión";
            // 
            // frmIngresoProductosCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1128, 425);
            this.Controls.Add(this.grupoImpresion);
            this.Controls.Add(this.grupoStock);
            this.Controls.Add(this.grupoOpciones);
            this.Controls.Add(this.grupoPrecio);
            this.Controls.Add(this.grupoReceta);
            this.Controls.Add(this.grupoBotones);
            this.Controls.Add(this.grupoGrid);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.grupoEncabezado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIngresoProductosCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso de Productos por Categoría";
            this.Load += new System.EventHandler(this.frmIngresoProductosCategoria_Load);
            this.grupoEncabezado.ResumeLayout(false);
            this.grupoEncabezado.PerformLayout();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoGrid.ResumeLayout(false);
            this.grupoGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.grupoBotones.ResumeLayout(false);
            this.grupoReceta.ResumeLayout(false);
            this.grupoReceta.PerformLayout();
            this.grupoPrecio.ResumeLayout(false);
            this.grupoPrecio.PerformLayout();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoOpciones.PerformLayout();
            this.grupoStock.ResumeLayout(false);
            this.grupoStock.PerformLayout();
            this.grupoImpresion.ResumeLayout(false);
            this.grupoImpresion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoEncabezado;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblStoMax;
        private System.Windows.Forms.TextBox txtStockMax;
        private System.Windows.Forms.Label lblStocMin;
        private System.Windows.Forms.TextBox txtStockMin;
        private System.Windows.Forms.CheckBox chkPreModifProductos;
        private System.Windows.Forms.CheckBox chkExpiraProductos;
        private System.Windows.Forms.Label lblSecuencia;
        private System.Windows.Forms.TextBox txtSecuencia;
        private System.Windows.Forms.CheckBox chkPagaIva;
        private MisControles.ComboDatos cmbConsumo;
        private MisControles.ComboDatos cmbCompra;
        private System.Windows.Forms.Label lblUniConsumo;
        private System.Windows.Forms.Label lblUnidadCompra;
        private System.Windows.Forms.Label lblPrecioMinorista;
        private System.Windows.Forms.TextBox txtPrecioMinorista;
        private System.Windows.Forms.Label lblPreCompra;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.Label lblDecripCategoria;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.Label lblCodiCategori;
        private System.Windows.Forms.TextBox txtCodigoProducto;
        private System.Windows.Forms.GroupBox grupoGrid;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox grupoBotones;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAgregar;
        public System.Windows.Forms.TextBox txtIdCategoria;
        public System.Windows.Forms.TextBox txtDescripcion;
        public System.Windows.Forms.Label lblListaNombre;
        private MisControles.ComboDatos cmbPadre;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblEtiquetaCategoria;
        private MisControles.ComboDatos cmbCategorias;
        private System.Windows.Forms.ToolTip ttMensaje;
        private MisControles.ComboDatos cmbTipoProducto;
        private System.Windows.Forms.Label label3;
        private MisControles.ComboDatos cmbClaseProducto;
        private System.Windows.Forms.Label label4;
        private MisControles.ComboDatos cmbDestinoImpresion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grupoReceta;
        private Controles.Auxiliares.DB_Ayuda dbAyudaReceta;
        private System.Windows.Forms.GroupBox grupoPrecio;
        private System.Windows.Forms.GroupBox grupoOpciones;
        private System.Windows.Forms.GroupBox grupoStock;
        private System.Windows.Forms.GroupBox grupoImpresion;
        private System.Windows.Forms.Button BtnLimpiarDbAyuda;
        private System.Windows.Forms.RadioButton rdbReferenciaInsumos;
        private System.Windows.Forms.RadioButton rdbReceta;
    }
}