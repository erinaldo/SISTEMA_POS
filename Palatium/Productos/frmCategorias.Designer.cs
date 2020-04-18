namespace Palatium.Productos
{
    partial class frmCategorias
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscarCategoria = new System.Windows.Forms.Button();
            this.txtBuscarCategoria = new System.Windows.Forms.TextBox();
            this.dgvCategoria = new System.Windows.Forms.DataGridView();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modificable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_modificable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.paga_iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subcategoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu_pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otros = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maneja_almuerzos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalle_por_origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalle_independiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoria_delivery = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.secuencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grb_opcioCategori = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            this.lblEtiquetaImagen = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.imgLogo = new System.Windows.Forms.PictureBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.chkDetalleIndependiente = new System.Windows.Forms.CheckBox();
            this.chkDetallarOrigen = new System.Windows.Forms.CheckBox();
            this.chkAlmuerzos = new System.Windows.Forms.CheckBox();
            this.chkOtros = new System.Windows.Forms.CheckBox();
            this.label17 = new System.Windows.Forms.Label();
            this.chkMenuPos = new System.Windows.Forms.CheckBox();
            this.chkTieneModifcador = new System.Windows.Forms.CheckBox();
            this.chkTieneSubCategoria = new System.Windows.Forms.CheckBox();
            this.lblSecuencia = new System.Windows.Forms.Label();
            this.txtSecuencia = new System.Windows.Forms.TextBox();
            this.chkPagaIva = new System.Windows.Forms.CheckBox();
            this.cmbConsumo = new MisControles.ComboDatos();
            this.cmbCompra = new MisControles.ComboDatos();
            this.chkPreModificable = new System.Windows.Forms.CheckBox();
            this.chkModificable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUnidadCompra = new System.Windows.Forms.Label();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodCategoria = new System.Windows.Forms.Label();
            this.txtCodigoCategoria = new System.Windows.Forms.TextBox();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cmbPadre = new MisControles.ComboDatos();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBase64 = new System.Windows.Forms.TextBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.Grb_listReCajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).BeginInit();
            this.Grb_opcioCategori.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Grb_listReCajero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listReCajero.Controls.Add(this.btnBuscarCategoria);
            this.Grb_listReCajero.Controls.Add(this.txtBuscarCategoria);
            this.Grb_listReCajero.Controls.Add(this.dgvCategoria);
            this.Grb_listReCajero.Location = new System.Drawing.Point(387, 12);
            this.Grb_listReCajero.Name = "Grb_listReCajero";
            this.Grb_listReCajero.Size = new System.Drawing.Size(500, 458);
            this.Grb_listReCajero.TabIndex = 8;
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
            this.btnBuscarCategoria.TabIndex = 2;
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
            this.txtBuscarCategoria.TabIndex = 1;
            // 
            // dgvCategoria
            // 
            this.dgvCategoria.AllowUserToAddRows = false;
            this.dgvCategoria.AllowUserToDeleteRows = false;
            this.dgvCategoria.AllowUserToResizeColumns = false;
            this.dgvCategoria.AllowUserToResizeRows = false;
            this.dgvCategoria.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategoria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_producto,
            this.modificable,
            this.precio_modificable,
            this.paga_iva,
            this.modificador,
            this.subcategoria,
            this.menu_pos,
            this.is_active,
            this.otros,
            this.maneja_almuerzos,
            this.detalle_por_origen,
            this.detalle_independiente,
            this.categoria_delivery,
            this.codigo,
            this.nombre,
            this.secuencia,
            this.estado});
            this.dgvCategoria.Location = new System.Drawing.Point(15, 54);
            this.dgvCategoria.Name = "dgvCategoria";
            this.dgvCategoria.ReadOnly = true;
            this.dgvCategoria.RowHeadersVisible = false;
            this.dgvCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategoria.Size = new System.Drawing.Size(469, 389);
            this.dgvCategoria.TabIndex = 0;
            this.dgvCategoria.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellDoubleClick);
            // 
            // id_producto
            // 
            this.id_producto.HeaderText = "ID_PRODUCTO";
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            this.id_producto.Visible = false;
            // 
            // modificable
            // 
            this.modificable.HeaderText = "MODIFICABLE";
            this.modificable.Name = "modificable";
            this.modificable.ReadOnly = true;
            this.modificable.Visible = false;
            // 
            // precio_modificable
            // 
            this.precio_modificable.HeaderText = "PRECIO_MODIFICABLE";
            this.precio_modificable.Name = "precio_modificable";
            this.precio_modificable.ReadOnly = true;
            this.precio_modificable.Visible = false;
            // 
            // paga_iva
            // 
            this.paga_iva.HeaderText = "PAGA_IVA";
            this.paga_iva.Name = "paga_iva";
            this.paga_iva.ReadOnly = true;
            this.paga_iva.Visible = false;
            // 
            // modificador
            // 
            this.modificador.HeaderText = "MODIFICADOR";
            this.modificador.Name = "modificador";
            this.modificador.ReadOnly = true;
            this.modificador.Visible = false;
            // 
            // subcategoria
            // 
            this.subcategoria.HeaderText = "SUBCATEGORIA";
            this.subcategoria.Name = "subcategoria";
            this.subcategoria.ReadOnly = true;
            this.subcategoria.Visible = false;
            // 
            // menu_pos
            // 
            this.menu_pos.HeaderText = "MENU POS";
            this.menu_pos.Name = "menu_pos";
            this.menu_pos.ReadOnly = true;
            this.menu_pos.Visible = false;
            // 
            // is_active
            // 
            this.is_active.HeaderText = "IS_ACTIVE";
            this.is_active.Name = "is_active";
            this.is_active.ReadOnly = true;
            this.is_active.Visible = false;
            // 
            // otros
            // 
            this.otros.HeaderText = "OTROS";
            this.otros.Name = "otros";
            this.otros.ReadOnly = true;
            this.otros.Visible = false;
            // 
            // maneja_almuerzos
            // 
            this.maneja_almuerzos.HeaderText = "MANEJA_ALMUERZOS";
            this.maneja_almuerzos.Name = "maneja_almuerzos";
            this.maneja_almuerzos.ReadOnly = true;
            this.maneja_almuerzos.Visible = false;
            // 
            // detalle_por_origen
            // 
            this.detalle_por_origen.HeaderText = "DETALLE_ORIGEN";
            this.detalle_por_origen.Name = "detalle_por_origen";
            this.detalle_por_origen.ReadOnly = true;
            this.detalle_por_origen.Visible = false;
            // 
            // detalle_independiente
            // 
            this.detalle_independiente.HeaderText = "DETALLE_INDEPENDIENTE";
            this.detalle_independiente.Name = "detalle_independiente";
            this.detalle_independiente.ReadOnly = true;
            this.detalle_independiente.Visible = false;
            // 
            // categoria_delivery
            // 
            this.categoria_delivery.HeaderText = "CATEGORIA_DELIVERY";
            this.categoria_delivery.Name = "categoria_delivery";
            this.categoria_delivery.ReadOnly = true;
            this.categoria_delivery.Visible = false;
            // 
            // codigo
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.codigo.DefaultCellStyle = dataGridViewCellStyle4;
            this.codigo.HeaderText = "CÓDIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 80;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nombre.HeaderText = "DESCRIPCIÓN";
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            // 
            // secuencia
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.secuencia.DefaultCellStyle = dataGridViewCellStyle5;
            this.secuencia.HeaderText = "SECUENCIA";
            this.secuencia.Name = "secuencia";
            this.secuencia.ReadOnly = true;
            // 
            // estado
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle6;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // Grb_opcioCategori
            // 
            this.Grb_opcioCategori.Controls.Add(this.btnLimpiar);
            this.Grb_opcioCategori.Controls.Add(this.btnEliminar);
            this.Grb_opcioCategori.Controls.Add(this.btnAgregar);
            this.Grb_opcioCategori.Location = new System.Drawing.Point(12, 397);
            this.Grb_opcioCategori.Name = "Grb_opcioCategori";
            this.Grb_opcioCategori.Size = new System.Drawing.Size(349, 73);
            this.Grb_opcioCategori.TabIndex = 7;
            this.Grb_opcioCategori.TabStop = false;
            this.Grb_opcioCategori.Text = "Opciones";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(218, 19);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 19;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiarCategori_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(142, 19);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 39);
            this.btnEliminar.TabIndex = 18;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnAnularCategori_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Blue;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(66, 19);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(70, 39);
            this.btnAgregar.TabIndex = 17;
            this.btnAgregar.Text = "Nuevo";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnNuevoCategori_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkHabilitado);
            this.grupoDatos.Controls.Add(this.lblEtiquetaImagen);
            this.grupoDatos.Controls.Add(this.btnClear);
            this.grupoDatos.Controls.Add(this.btnExaminar);
            this.grupoDatos.Controls.Add(this.imgLogo);
            this.grupoDatos.Controls.Add(this.chkDelivery);
            this.grupoDatos.Controls.Add(this.chkDetalleIndependiente);
            this.grupoDatos.Controls.Add(this.chkDetallarOrigen);
            this.grupoDatos.Controls.Add(this.chkAlmuerzos);
            this.grupoDatos.Controls.Add(this.chkOtros);
            this.grupoDatos.Controls.Add(this.label17);
            this.grupoDatos.Controls.Add(this.chkMenuPos);
            this.grupoDatos.Controls.Add(this.chkTieneModifcador);
            this.grupoDatos.Controls.Add(this.chkTieneSubCategoria);
            this.grupoDatos.Controls.Add(this.lblSecuencia);
            this.grupoDatos.Controls.Add(this.txtSecuencia);
            this.grupoDatos.Controls.Add(this.chkPagaIva);
            this.grupoDatos.Controls.Add(this.cmbConsumo);
            this.grupoDatos.Controls.Add(this.cmbCompra);
            this.grupoDatos.Controls.Add(this.chkPreModificable);
            this.grupoDatos.Controls.Add(this.chkModificable);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.lblUnidadCompra);
            this.grupoDatos.Controls.Add(this.lblDescrCajero);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblCodCategoria);
            this.grupoDatos.Controls.Add(this.txtCodigoCategoria);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 97);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(349, 294);
            this.grupoDatos.TabIndex = 6;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(16, 269);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(138, 17);
            this.chkHabilitado.TabIndex = 61;
            this.chkHabilitado.Text = "Producto Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // lblEtiquetaImagen
            // 
            this.lblEtiquetaImagen.BackColor = System.Drawing.Color.Transparent;
            this.lblEtiquetaImagen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEtiquetaImagen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEtiquetaImagen.Location = new System.Drawing.Point(266, 71);
            this.lblEtiquetaImagen.Name = "lblEtiquetaImagen";
            this.lblEtiquetaImagen.Size = new System.Drawing.Size(60, 18);
            this.lblEtiquetaImagen.TabIndex = 58;
            this.lblEtiquetaImagen.Text = "Imagen:";
            this.lblEtiquetaImagen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Fuchsia;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(297, 137);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(29, 21);
            this.btnClear.TabIndex = 57;
            this.btnClear.Text = "X";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.BackColor = System.Drawing.Color.Yellow;
            this.btnExaminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Fuchsia;
            this.btnExaminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExaminar.Location = new System.Drawing.Point(266, 137);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(29, 21);
            this.btnExaminar.TabIndex = 56;
            this.btnExaminar.Text = "...";
            this.btnExaminar.UseVisualStyleBackColor = false;
            this.btnExaminar.Click += new System.EventHandler(this.btnExaminar_Click);
            // 
            // imgLogo
            // 
            this.imgLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgLogo.Location = new System.Drawing.Point(266, 92);
            this.imgLogo.Name = "imgLogo";
            this.imgLogo.Size = new System.Drawing.Size(60, 44);
            this.imgLogo.TabIndex = 19;
            this.imgLogo.TabStop = false;
            // 
            // chkDelivery
            // 
            this.chkDelivery.AutoSize = true;
            this.chkDelivery.Enabled = false;
            this.chkDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDelivery.Location = new System.Drawing.Point(173, 269);
            this.chkDelivery.Name = "chkDelivery";
            this.chkDelivery.Size = new System.Drawing.Size(153, 19);
            this.chkDelivery.TabIndex = 55;
            this.chkDelivery.Text = "Categoría para Delivery";
            this.chkDelivery.UseVisualStyleBackColor = true;
            // 
            // chkDetalleIndependiente
            // 
            this.chkDetalleIndependiente.AutoSize = true;
            this.chkDetalleIndependiente.Enabled = false;
            this.chkDetalleIndependiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDetalleIndependiente.Location = new System.Drawing.Point(15, 248);
            this.chkDetalleIndependiente.Name = "chkDetalleIndependiente";
            this.chkDetalleIndependiente.Size = new System.Drawing.Size(147, 19);
            this.chkDetalleIndependiente.TabIndex = 54;
            this.chkDetalleIndependiente.Text = "Detalle Independiente";
            this.chkDetalleIndependiente.UseVisualStyleBackColor = true;
            this.chkDetalleIndependiente.CheckedChanged += new System.EventHandler(this.chkDetalleIndependiente_CheckedChanged);
            // 
            // chkDetallarOrigen
            // 
            this.chkDetallarOrigen.AutoSize = true;
            this.chkDetallarOrigen.Enabled = false;
            this.chkDetallarOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDetallarOrigen.Location = new System.Drawing.Point(173, 248);
            this.chkDetallarOrigen.Name = "chkDetallarOrigen";
            this.chkDetallarOrigen.Size = new System.Drawing.Size(130, 19);
            this.chkDetallarOrigen.TabIndex = 53;
            this.chkDetallarOrigen.Text = "Detallar por Origen";
            this.chkDetallarOrigen.UseVisualStyleBackColor = true;
            this.chkDetallarOrigen.CheckedChanged += new System.EventHandler(this.chkDetallarOrigen_CheckedChanged);
            // 
            // chkAlmuerzos
            // 
            this.chkAlmuerzos.AutoSize = true;
            this.chkAlmuerzos.Enabled = false;
            this.chkAlmuerzos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAlmuerzos.Location = new System.Drawing.Point(173, 229);
            this.chkAlmuerzos.Name = "chkAlmuerzos";
            this.chkAlmuerzos.Size = new System.Drawing.Size(129, 19);
            this.chkAlmuerzos.TabIndex = 52;
            this.chkAlmuerzos.Text = "Maneja Almuerzos";
            this.chkAlmuerzos.UseVisualStyleBackColor = true;
            // 
            // chkOtros
            // 
            this.chkOtros.AutoSize = true;
            this.chkOtros.Enabled = false;
            this.chkOtros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOtros.Location = new System.Drawing.Point(173, 210);
            this.chkOtros.Name = "chkOtros";
            this.chkOtros.Size = new System.Drawing.Size(126, 19);
            this.chkOtros.TabIndex = 16;
            this.chkOtros.Text = "Otros (Especiales)";
            this.chkOtros.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(234, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 13);
            this.label17.TabIndex = 51;
            this.label17.Text = "Máx. 2 dígitos";
            // 
            // chkMenuPos
            // 
            this.chkMenuPos.AutoSize = true;
            this.chkMenuPos.Enabled = false;
            this.chkMenuPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMenuPos.Location = new System.Drawing.Point(173, 172);
            this.chkMenuPos.Name = "chkMenuPos";
            this.chkMenuPos.Size = new System.Drawing.Size(82, 19);
            this.chkMenuPos.TabIndex = 14;
            this.chkMenuPos.Text = "Menú Pos";
            this.chkMenuPos.UseVisualStyleBackColor = true;
            // 
            // chkTieneModifcador
            // 
            this.chkTieneModifcador.AutoSize = true;
            this.chkTieneModifcador.Enabled = false;
            this.chkTieneModifcador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTieneModifcador.Location = new System.Drawing.Point(173, 191);
            this.chkTieneModifcador.Name = "chkTieneModifcador";
            this.chkTieneModifcador.Size = new System.Drawing.Size(108, 19);
            this.chkTieneModifcador.TabIndex = 15;
            this.chkTieneModifcador.Text = "Es Modificador";
            this.chkTieneModifcador.UseVisualStyleBackColor = true;
            // 
            // chkTieneSubCategoria
            // 
            this.chkTieneSubCategoria.AutoSize = true;
            this.chkTieneSubCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTieneSubCategoria.Location = new System.Drawing.Point(15, 229);
            this.chkTieneSubCategoria.Name = "chkTieneSubCategoria";
            this.chkTieneSubCategoria.Size = new System.Drawing.Size(135, 19);
            this.chkTieneSubCategoria.TabIndex = 13;
            this.chkTieneSubCategoria.Text = "Tiene SubCategoría";
            this.chkTieneSubCategoria.UseVisualStyleBackColor = true;
            // 
            // lblSecuencia
            // 
            this.lblSecuencia.AutoSize = true;
            this.lblSecuencia.BackColor = System.Drawing.Color.Transparent;
            this.lblSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecuencia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSecuencia.Location = new System.Drawing.Point(13, 71);
            this.lblSecuencia.Name = "lblSecuencia";
            this.lblSecuencia.Size = new System.Drawing.Size(68, 15);
            this.lblSecuencia.TabIndex = 49;
            this.lblSecuencia.Text = "Secuencia:";
            // 
            // txtSecuencia
            // 
            this.txtSecuencia.Location = new System.Drawing.Point(120, 70);
            this.txtSecuencia.MaxLength = 3;
            this.txtSecuencia.Name = "txtSecuencia";
            this.txtSecuencia.Size = new System.Drawing.Size(63, 20);
            this.txtSecuencia.TabIndex = 6;
            this.txtSecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecuencia_KeyPress);
            // 
            // chkPagaIva
            // 
            this.chkPagaIva.AutoSize = true;
            this.chkPagaIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPagaIva.Location = new System.Drawing.Point(15, 210);
            this.chkPagaIva.Name = "chkPagaIva";
            this.chkPagaIva.Size = new System.Drawing.Size(73, 19);
            this.chkPagaIva.TabIndex = 12;
            this.chkPagaIva.Text = "Paga Iva";
            this.chkPagaIva.UseVisualStyleBackColor = true;
            // 
            // cmbConsumo
            // 
            this.cmbConsumo.FormattingEnabled = true;
            this.cmbConsumo.Location = new System.Drawing.Point(120, 115);
            this.cmbConsumo.Name = "cmbConsumo";
            this.cmbConsumo.Size = new System.Drawing.Size(106, 21);
            this.cmbConsumo.TabIndex = 8;
            // 
            // cmbCompra
            // 
            this.cmbCompra.FormattingEnabled = true;
            this.cmbCompra.Location = new System.Drawing.Point(120, 92);
            this.cmbCompra.Name = "cmbCompra";
            this.cmbCompra.Size = new System.Drawing.Size(106, 21);
            this.cmbCompra.TabIndex = 7;
            // 
            // chkPreModificable
            // 
            this.chkPreModificable.AutoSize = true;
            this.chkPreModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPreModificable.Location = new System.Drawing.Point(15, 191);
            this.chkPreModificable.Name = "chkPreModificable";
            this.chkPreModificable.Size = new System.Drawing.Size(128, 19);
            this.chkPreModificable.TabIndex = 11;
            this.chkPreModificable.Text = "Precio modificable";
            this.chkPreModificable.UseVisualStyleBackColor = true;
            // 
            // chkModificable
            // 
            this.chkModificable.AutoSize = true;
            this.chkModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModificable.Location = new System.Drawing.Point(15, 172);
            this.chkModificable.Name = "chkModificable";
            this.chkModificable.Size = new System.Drawing.Size(90, 19);
            this.chkModificable.TabIndex = 10;
            this.chkModificable.Text = "Modificable";
            this.chkModificable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(13, 117);
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
            this.lblUnidadCompra.Location = new System.Drawing.Point(13, 94);
            this.lblUnidadCompra.Name = "lblUnidadCompra";
            this.lblUnidadCompra.Size = new System.Drawing.Size(97, 15);
            this.lblUnidadCompra.TabIndex = 13;
            this.lblUnidadCompra.Text = "Unidad Compra:";
            // 
            // lblDescrCajero
            // 
            this.lblDescrCajero.AutoSize = true;
            this.lblDescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrCajero.Location = new System.Drawing.Point(12, 47);
            this.lblDescrCajero.Name = "lblDescrCajero";
            this.lblDescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lblDescrCajero.TabIndex = 5;
            this.lblDescrCajero.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(121, 48);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(200, 20);
            this.txtDescripcion.TabIndex = 5;
            // 
            // lblCodCategoria
            // 
            this.lblCodCategoria.AutoSize = true;
            this.lblCodCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblCodCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodCategoria.Location = new System.Drawing.Point(12, 25);
            this.lblCodCategoria.Name = "lblCodCategoria";
            this.lblCodCategoria.Size = new System.Drawing.Size(102, 15);
            this.lblCodCategoria.TabIndex = 3;
            this.lblCodCategoria.Text = "Código Categoría";
            // 
            // txtCodigoCategoria
            // 
            this.txtCodigoCategoria.Location = new System.Drawing.Point(121, 26);
            this.txtCodigoCategoria.MaxLength = 2;
            this.txtCodigoCategoria.Name = "txtCodigoCategoria";
            this.txtCodigoCategoria.Size = new System.Drawing.Size(107, 20);
            this.txtCodigoCategoria.TabIndex = 4;
            this.txtCodigoCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoCategoria_KeyPress);
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Enabled = false;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(122, 19);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(197, 21);
            this.cmbEmpresa.TabIndex = 45;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(13, 20);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(60, 15);
            this.lblEmpresa.TabIndex = 44;
            this.lblEmpresa.Text = "Empresa:";
            // 
            // cmbPadre
            // 
            this.cmbPadre.FormattingEnabled = true;
            this.cmbPadre.Location = new System.Drawing.Point(122, 42);
            this.cmbPadre.Name = "cmbPadre";
            this.cmbPadre.Size = new System.Drawing.Size(196, 21);
            this.cmbPadre.TabIndex = 3;
            this.cmbPadre.SelectedIndexChanged += new System.EventHandler(this.cmbPadre_SelectedIndexChanged);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigo.Location = new System.Drawing.Point(13, 42);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(81, 15);
            this.lblCodigo.TabIndex = 11;
            this.lblCodigo.Text = "Código padre";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbEmpresa);
            this.groupBox1.Controls.Add(this.lblCodigo);
            this.groupBox1.Controls.Add(this.cmbPadre);
            this.groupBox1.Controls.Add(this.lblEmpresa);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 79);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // txtBase64
            // 
            this.txtBase64.Enabled = false;
            this.txtBase64.Location = new System.Drawing.Point(12, 502);
            this.txtBase64.MaxLength = 20;
            this.txtBase64.Multiline = true;
            this.txtBase64.Name = "txtBase64";
            this.txtBase64.Size = new System.Drawing.Size(875, 74);
            this.txtBase64.TabIndex = 64;
            // 
            // txtRuta
            // 
            this.txtRuta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRuta.Location = new System.Drawing.Point(387, 489);
            this.txtRuta.MaxLength = 20;
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.ReadOnly = true;
            this.txtRuta.Size = new System.Drawing.Size(202, 20);
            this.txtRuta.TabIndex = 65;
            // 
            // frmCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(899, 477);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.txtBase64);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Grb_listReCajero);
            this.Controls.Add(this.Grb_opcioCategori);
            this.Controls.Add(this.grupoDatos);
            this.MaximizeBox = false;
            this.Name = "frmCategorias";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso de Categorías";
            this.Load += new System.EventHandler(this.frmCategorias_Load);
            this.Grb_listReCajero.ResumeLayout(false);
            this.Grb_listReCajero.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).EndInit();
            this.Grb_opcioCategori.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgLogo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.Button btnBuscarCategoria;
        private System.Windows.Forms.TextBox txtBuscarCategoria;
        private System.Windows.Forms.DataGridView dgvCategoria;
        private System.Windows.Forms.GroupBox Grb_opcioCategori;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.CheckBox chkTieneModifcador;
        private System.Windows.Forms.CheckBox chkTieneSubCategoria;
        private System.Windows.Forms.Label lblSecuencia;
        private System.Windows.Forms.TextBox txtSecuencia;
        private System.Windows.Forms.CheckBox chkPagaIva;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private MisControles.ComboDatos cmbConsumo;
        private MisControles.ComboDatos cmbCompra;
        private MisControles.ComboDatos cmbPadre;
        private System.Windows.Forms.CheckBox chkPreModificable;
        private System.Windows.Forms.CheckBox chkModificable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUnidadCompra;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodCategoria;
        private System.Windows.Forms.TextBox txtCodigoCategoria;
        private System.Windows.Forms.CheckBox chkMenuPos;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.CheckBox chkOtros;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAlmuerzos;
        private System.Windows.Forms.CheckBox chkDetallarOrigen;
        private System.Windows.Forms.CheckBox chkDetalleIndependiente;
        private System.Windows.Forms.CheckBox chkDelivery;
        private System.Windows.Forms.PictureBox imgLogo;
        private System.Windows.Forms.Label lblEtiquetaImagen;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.TextBox txtBase64;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn modificable;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_modificable;
        private System.Windows.Forms.DataGridViewTextBoxColumn paga_iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn modificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn subcategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn menu_pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_active;
        private System.Windows.Forms.DataGridViewTextBoxColumn otros;
        private System.Windows.Forms.DataGridViewTextBoxColumn maneja_almuerzos;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalle_por_origen;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalle_independiente;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoria_delivery;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn secuencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.CheckBox chkHabilitado;
    }
}