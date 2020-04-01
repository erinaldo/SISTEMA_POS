namespace Palatium.Formularios
{
    partial class FInformacionPrecProduc
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
            this.tbcPreProduc = new System.Windows.Forms.TabControl();
            this.tblPreProduc = new System.Windows.Forms.TabPage();
            this.btnNuevoPosOrd = new System.Windows.Forms.Button();
            this.btnCerrarListPrecios = new System.Windows.Forms.Button();
            this.grpDatosProducto = new System.Windows.Forms.GroupBox();
            this.btnListCategoria = new System.Windows.Forms.Button();
            this.txtNomCategoria = new System.Windows.Forms.TextBox();
            this.btnProducto = new System.Windows.Forms.Button();
            this.txtDescripcionProduc = new System.Windows.Forms.TextBox();
            this.lblLisCategoria = new System.Windows.Forms.Label();
            this.txtIdCategoria = new System.Windows.Forms.TextBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.txtIdProduc = new System.Windows.Forms.TextBox();
            this.btnLimpiarListPrecios = new System.Windows.Forms.Button();
            this.btnAnularListPrecios = new System.Windows.Forms.Button();
            this.grpListPreProduc = new System.Windows.Forms.GroupBox();
            this.dgvListPreciosProductos = new System.Windows.Forms.DataGridView();
            this.clmIdPrecioProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmIdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrecioActual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmValorPorcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmPrecioNuevo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaDesde = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmFechaFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmModificado = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnNuevoListPrecios = new System.Windows.Forms.Button();
            this.grpDatoPreProduc = new System.Windows.Forms.GroupBox();
            this.txtMoneda = new System.Windows.Forms.TextBox();
            this.chkListBase = new System.Windows.Forms.CheckBox();
            this.btnListPrecio = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.chkListModi = new System.Windows.Forms.CheckBox();
            this.txtFecFinVa = new System.Windows.Forms.TextBox();
            this.txtFecIniVa = new System.Windows.Forms.TextBox();
            this.lblFecIniVaCrear = new System.Windows.Forms.Label();
            this.lblFecFinVaCrear = new System.Windows.Forms.Label();
            this.lblMonedaCrear = new System.Windows.Forms.Label();
            this.cmbEstadoListPrecios = new System.Windows.Forms.ComboBox();
            this.lblEstaListPre = new System.Windows.Forms.Label();
            this.lblLisPrec = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.tbcPreProduc.SuspendLayout();
            this.tblPreProduc.SuspendLayout();
            this.grpDatosProducto.SuspendLayout();
            this.grpListPreProduc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPreciosProductos)).BeginInit();
            this.grpDatoPreProduc.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcPreProduc
            // 
            this.tbcPreProduc.Controls.Add(this.tblPreProduc);
            this.tbcPreProduc.Location = new System.Drawing.Point(-4, 0);
            this.tbcPreProduc.Name = "tbcPreProduc";
            this.tbcPreProduc.SelectedIndex = 0;
            this.tbcPreProduc.Size = new System.Drawing.Size(900, 465);
            this.tbcPreProduc.TabIndex = 3;
            // 
            // tblPreProduc
            // 
            this.tblPreProduc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tblPreProduc.Controls.Add(this.btnCerrarListPrecios);
            this.tblPreProduc.Controls.Add(this.grpDatosProducto);
            this.tblPreProduc.Controls.Add(this.btnLimpiarListPrecios);
            this.tblPreProduc.Controls.Add(this.btnAnularListPrecios);
            this.tblPreProduc.Controls.Add(this.grpListPreProduc);
            this.tblPreProduc.Controls.Add(this.btnNuevoListPrecios);
            this.tblPreProduc.Controls.Add(this.grpDatoPreProduc);
            this.tblPreProduc.Location = new System.Drawing.Point(4, 22);
            this.tblPreProduc.Name = "tblPreProduc";
            this.tblPreProduc.Padding = new System.Windows.Forms.Padding(3);
            this.tblPreProduc.Size = new System.Drawing.Size(892, 439);
            this.tblPreProduc.TabIndex = 0;
            this.tblPreProduc.Text = "Módulo Precios Productos";
            // 
            // btnNuevoPosOrd
            // 
            this.btnNuevoPosOrd.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoPosOrd.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevoPosOrd.Location = new System.Drawing.Point(266, 137);
            this.btnNuevoPosOrd.Name = "btnNuevoPosOrd";
            this.btnNuevoPosOrd.Size = new System.Drawing.Size(71, 29);
            this.btnNuevoPosOrd.TabIndex = 8;
            this.btnNuevoPosOrd.Text = "OK";
            this.btnNuevoPosOrd.UseVisualStyleBackColor = false;
            this.btnNuevoPosOrd.Click += new System.EventHandler(this.btnNuevoPosOrd_Click);
            // 
            // btnCerrarListPrecios
            // 
            this.btnCerrarListPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnCerrarListPrecios.Location = new System.Drawing.Point(809, 396);
            this.btnCerrarListPrecios.Name = "btnCerrarListPrecios";
            this.btnCerrarListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnCerrarListPrecios.TabIndex = 3;
            this.btnCerrarListPrecios.Text = "Cerrar";
            this.btnCerrarListPrecios.UseVisualStyleBackColor = false;
            this.btnCerrarListPrecios.Click += new System.EventHandler(this.btnCerrarListPrecios_Click);
            // 
            // grpDatosProducto
            // 
            this.grpDatosProducto.Controls.Add(this.btnNuevoPosOrd);
            this.grpDatosProducto.Controls.Add(this.btnListCategoria);
            this.grpDatosProducto.Controls.Add(this.txtNomCategoria);
            this.grpDatosProducto.Controls.Add(this.btnProducto);
            this.grpDatosProducto.Controls.Add(this.txtDescripcionProduc);
            this.grpDatosProducto.Controls.Add(this.lblLisCategoria);
            this.grpDatosProducto.Controls.Add(this.txtIdCategoria);
            this.grpDatosProducto.Controls.Add(this.lblProducto);
            this.grpDatosProducto.Controls.Add(this.txtIdProduc);
            this.grpDatosProducto.Location = new System.Drawing.Point(532, 19);
            this.grpDatosProducto.Name = "grpDatosProducto";
            this.grpDatosProducto.Size = new System.Drawing.Size(350, 172);
            this.grpDatosProducto.TabIndex = 6;
            this.grpDatosProducto.TabStop = false;
            this.grpDatosProducto.Text = "Datos de Producto";
            // 
            // btnListCategoria
            // 
            this.btnListCategoria.Location = new System.Drawing.Point(115, 49);
            this.btnListCategoria.Name = "btnListCategoria";
            this.btnListCategoria.Size = new System.Drawing.Size(35, 23);
            this.btnListCategoria.TabIndex = 36;
            this.btnListCategoria.Text = "?";
            this.btnListCategoria.UseVisualStyleBackColor = true;
            this.btnListCategoria.Click += new System.EventHandler(this.btnListCategoria_Click);
            // 
            // txtNomCategoria
            // 
            this.txtNomCategoria.Location = new System.Drawing.Point(156, 49);
            this.txtNomCategoria.MaxLength = 20;
            this.txtNomCategoria.Name = "txtNomCategoria";
            this.txtNomCategoria.Size = new System.Drawing.Size(181, 20);
            this.txtNomCategoria.TabIndex = 35;
            // 
            // btnProducto
            // 
            this.btnProducto.Location = new System.Drawing.Point(115, 106);
            this.btnProducto.Name = "btnProducto";
            this.btnProducto.Size = new System.Drawing.Size(35, 23);
            this.btnProducto.TabIndex = 30;
            this.btnProducto.Text = "?";
            this.btnProducto.UseVisualStyleBackColor = true;
            this.btnProducto.Click += new System.EventHandler(this.btnProducto_Click);
            // 
            // txtDescripcionProduc
            // 
            this.txtDescripcionProduc.Location = new System.Drawing.Point(156, 109);
            this.txtDescripcionProduc.MaxLength = 20;
            this.txtDescripcionProduc.Name = "txtDescripcionProduc";
            this.txtDescripcionProduc.Size = new System.Drawing.Size(181, 20);
            this.txtDescripcionProduc.TabIndex = 29;
            // 
            // lblLisCategoria
            // 
            this.lblLisCategoria.AutoSize = true;
            this.lblLisCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblLisCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLisCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLisCategoria.Location = new System.Drawing.Point(15, 27);
            this.lblLisCategoria.Name = "lblLisCategoria";
            this.lblLisCategoria.Size = new System.Drawing.Size(69, 15);
            this.lblLisCategoria.TabIndex = 34;
            this.lblLisCategoria.Text = "Categorías:";
            // 
            // txtIdCategoria
            // 
            this.txtIdCategoria.Location = new System.Drawing.Point(18, 51);
            this.txtIdCategoria.MaxLength = 20;
            this.txtIdCategoria.Name = "txtIdCategoria";
            this.txtIdCategoria.Size = new System.Drawing.Size(91, 20);
            this.txtIdCategoria.TabIndex = 33;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.BackColor = System.Drawing.Color.Transparent;
            this.lblProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblProducto.Location = new System.Drawing.Point(15, 80);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(59, 15);
            this.lblProducto.TabIndex = 28;
            this.lblProducto.Text = "Producto:";
            // 
            // txtIdProduc
            // 
            this.txtIdProduc.Location = new System.Drawing.Point(18, 109);
            this.txtIdProduc.MaxLength = 20;
            this.txtIdProduc.Name = "txtIdProduc";
            this.txtIdProduc.Size = new System.Drawing.Size(91, 20);
            this.txtIdProduc.TabIndex = 27;
            // 
            // btnLimpiarListPrecios
            // 
            this.btnLimpiarListPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarListPrecios.Location = new System.Drawing.Point(733, 396);
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
            this.btnAnularListPrecios.Location = new System.Drawing.Point(657, 396);
            this.btnAnularListPrecios.Name = "btnAnularListPrecios";
            this.btnAnularListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnAnularListPrecios.TabIndex = 1;
            this.btnAnularListPrecios.Text = "Anular";
            this.btnAnularListPrecios.UseVisualStyleBackColor = false;
            this.btnAnularListPrecios.Click += new System.EventHandler(this.btnAnularListPrecios_Click);
            // 
            // grpListPreProduc
            // 
            this.grpListPreProduc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grpListPreProduc.Controls.Add(this.dgvListPreciosProductos);
            this.grpListPreProduc.Location = new System.Drawing.Point(17, 197);
            this.grpListPreProduc.Name = "grpListPreProduc";
            this.grpListPreProduc.Size = new System.Drawing.Size(862, 193);
            this.grpListPreProduc.TabIndex = 5;
            this.grpListPreProduc.TabStop = false;
            this.grpListPreProduc.Text = "Lista de Registros";
            // 
            // dgvListPreciosProductos
            // 
            this.dgvListPreciosProductos.AllowUserToAddRows = false;
            this.dgvListPreciosProductos.AllowUserToDeleteRows = false;
            this.dgvListPreciosProductos.AllowUserToResizeColumns = false;
            this.dgvListPreciosProductos.AllowUserToResizeRows = false;
            this.dgvListPreciosProductos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvListPreciosProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListPreciosProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmIdPrecioProducto,
            this.clmIdProducto,
            this.clmCodigo,
            this.clmNombre,
            this.clmPrecioActual,
            this.clmValorPorcentaje,
            this.clmPrecioNuevo,
            this.clmFechaDesde,
            this.clmFechaFinal,
            this.clmModificado});
            this.dgvListPreciosProductos.Location = new System.Drawing.Point(7, 20);
            this.dgvListPreciosProductos.Name = "dgvListPreciosProductos";
            this.dgvListPreciosProductos.RowHeadersWidth = 25;
            this.dgvListPreciosProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListPreciosProductos.Size = new System.Drawing.Size(845, 150);
            this.dgvListPreciosProductos.TabIndex = 10;
            this.dgvListPreciosProductos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvListPreciosProductos_CellBeginEdit);
            this.dgvListPreciosProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListPreciosProductos_CellClick);
            this.dgvListPreciosProductos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvListPreciosProductos_CellEndEdit);
            // 
            // clmIdPrecioProducto
            // 
            this.clmIdPrecioProducto.DataPropertyName = "id_precio_producto";
            this.clmIdPrecioProducto.HeaderText = "Precio Producto";
            this.clmIdPrecioProducto.Name = "clmIdPrecioProducto";
            this.clmIdPrecioProducto.ReadOnly = true;
            // 
            // clmIdProducto
            // 
            this.clmIdProducto.DataPropertyName = "id_Producto";
            this.clmIdProducto.HeaderText = "Id Producto";
            this.clmIdProducto.Name = "clmIdProducto";
            this.clmIdProducto.ReadOnly = true;
            // 
            // clmCodigo
            // 
            this.clmCodigo.DataPropertyName = "codigo";
            this.clmCodigo.HeaderText = "Codigo";
            this.clmCodigo.Name = "clmCodigo";
            this.clmCodigo.ReadOnly = true;
            // 
            // clmNombre
            // 
            this.clmNombre.DataPropertyName = "nombre";
            this.clmNombre.HeaderText = "Nombre";
            this.clmNombre.Name = "clmNombre";
            this.clmNombre.ReadOnly = true;
            // 
            // clmPrecioActual
            // 
            this.clmPrecioActual.DataPropertyName = "precioCompra";
            this.clmPrecioActual.HeaderText = "Precio Actual";
            this.clmPrecioActual.Name = "clmPrecioActual";
            this.clmPrecioActual.ReadOnly = true;
            // 
            // clmValorPorcentaje
            // 
            this.clmValorPorcentaje.DataPropertyName = "Valor_porcentaje";
            this.clmValorPorcentaje.HeaderText = "Valor Porcentaje";
            this.clmValorPorcentaje.Name = "clmValorPorcentaje";
            this.clmValorPorcentaje.ReadOnly = true;
            // 
            // clmPrecioNuevo
            // 
            this.clmPrecioNuevo.DataPropertyName = "Precio_Minorista";
            this.clmPrecioNuevo.HeaderText = "Precio Nuevo";
            this.clmPrecioNuevo.Name = "clmPrecioNuevo";
            this.clmPrecioNuevo.ReadOnly = true;
            // 
            // clmFechaDesde
            // 
            this.clmFechaDesde.DataPropertyName = "Fecha_Desde";
            this.clmFechaDesde.HeaderText = "Fecha Desde";
            this.clmFechaDesde.Name = "clmFechaDesde";
            this.clmFechaDesde.ReadOnly = true;
            // 
            // clmFechaFinal
            // 
            this.clmFechaFinal.DataPropertyName = "Fecha_Hasta";
            this.clmFechaFinal.HeaderText = "Fecha Final";
            this.clmFechaFinal.Name = "clmFechaFinal";
            this.clmFechaFinal.ReadOnly = true;
            // 
            // clmModificado
            // 
            this.clmModificado.HeaderText = "Modificado";
            this.clmModificado.Name = "clmModificado";
            this.clmModificado.ReadOnly = true;
            // 
            // btnNuevoListPrecios
            // 
            this.btnNuevoListPrecios.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoListPrecios.ForeColor = System.Drawing.Color.White;
            this.btnNuevoListPrecios.Location = new System.Drawing.Point(581, 396);
            this.btnNuevoListPrecios.Name = "btnNuevoListPrecios";
            this.btnNuevoListPrecios.Size = new System.Drawing.Size(70, 39);
            this.btnNuevoListPrecios.TabIndex = 0;
            this.btnNuevoListPrecios.Text = "Guardar";
            this.btnNuevoListPrecios.UseVisualStyleBackColor = false;
            this.btnNuevoListPrecios.Click += new System.EventHandler(this.btnNuevoListPrecios_Click);
            // 
            // grpDatoPreProduc
            // 
            this.grpDatoPreProduc.Controls.Add(this.txtMoneda);
            this.grpDatoPreProduc.Controls.Add(this.chkListBase);
            this.grpDatoPreProduc.Controls.Add(this.btnListPrecio);
            this.grpDatoPreProduc.Controls.Add(this.txtDescripcion);
            this.grpDatoPreProduc.Controls.Add(this.chkListModi);
            this.grpDatoPreProduc.Controls.Add(this.txtFecFinVa);
            this.grpDatoPreProduc.Controls.Add(this.txtFecIniVa);
            this.grpDatoPreProduc.Controls.Add(this.lblFecIniVaCrear);
            this.grpDatoPreProduc.Controls.Add(this.lblFecFinVaCrear);
            this.grpDatoPreProduc.Controls.Add(this.lblMonedaCrear);
            this.grpDatoPreProduc.Controls.Add(this.cmbEstadoListPrecios);
            this.grpDatoPreProduc.Controls.Add(this.lblEstaListPre);
            this.grpDatoPreProduc.Controls.Add(this.lblLisPrec);
            this.grpDatoPreProduc.Controls.Add(this.txtId);
            this.grpDatoPreProduc.Location = new System.Drawing.Point(17, 19);
            this.grpDatoPreProduc.Name = "grpDatoPreProduc";
            this.grpDatoPreProduc.Size = new System.Drawing.Size(509, 172);
            this.grpDatoPreProduc.TabIndex = 3;
            this.grpDatoPreProduc.TabStop = false;
            this.grpDatoPreProduc.Text = "Datos de LIsta Precios";
            // 
            // txtMoneda
            // 
            this.txtMoneda.Location = new System.Drawing.Point(132, 71);
            this.txtMoneda.MaxLength = 20;
            this.txtMoneda.Name = "txtMoneda";
            this.txtMoneda.Size = new System.Drawing.Size(107, 20);
            this.txtMoneda.TabIndex = 29;
            // 
            // chkListBase
            // 
            this.chkListBase.AutoSize = true;
            this.chkListBase.Location = new System.Drawing.Point(138, 143);
            this.chkListBase.Name = "chkListBase";
            this.chkListBase.Size = new System.Drawing.Size(75, 17);
            this.chkListBase.TabIndex = 27;
            this.chkListBase.Text = "Lista Base";
            this.chkListBase.UseVisualStyleBackColor = true;
            // 
            // btnListPrecio
            // 
            this.btnListPrecio.Location = new System.Drawing.Point(204, 25);
            this.btnListPrecio.Name = "btnListPrecio";
            this.btnListPrecio.Size = new System.Drawing.Size(35, 23);
            this.btnListPrecio.TabIndex = 26;
            this.btnListPrecio.Text = "?";
            this.btnListPrecio.UseVisualStyleBackColor = true;
            this.btnListPrecio.Click += new System.EventHandler(this.btnListPrecio_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(245, 27);
            this.txtDescripcion.MaxLength = 20;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(245, 20);
            this.txtDescripcion.TabIndex = 25;
            // 
            // chkListModi
            // 
            this.chkListModi.AutoSize = true;
            this.chkListModi.Location = new System.Drawing.Point(18, 143);
            this.chkListModi.Name = "chkListModi";
            this.chkListModi.Size = new System.Drawing.Size(105, 17);
            this.chkListModi.TabIndex = 21;
            this.chkListModi.Text = "Lista Modificable";
            this.chkListModi.UseVisualStyleBackColor = true;
            // 
            // txtFecFinVa
            // 
            this.txtFecFinVa.Location = new System.Drawing.Point(383, 97);
            this.txtFecFinVa.MaxLength = 20;
            this.txtFecFinVa.Name = "txtFecFinVa";
            this.txtFecFinVa.Size = new System.Drawing.Size(107, 20);
            this.txtFecFinVa.TabIndex = 19;
            // 
            // txtFecIniVa
            // 
            this.txtFecIniVa.Location = new System.Drawing.Point(132, 97);
            this.txtFecIniVa.MaxLength = 20;
            this.txtFecIniVa.Name = "txtFecIniVa";
            this.txtFecIniVa.Size = new System.Drawing.Size(107, 20);
            this.txtFecIniVa.TabIndex = 18;
            // 
            // lblFecIniVaCrear
            // 
            this.lblFecIniVaCrear.AutoSize = true;
            this.lblFecIniVaCrear.BackColor = System.Drawing.Color.Transparent;
            this.lblFecIniVaCrear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecIniVaCrear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecIniVaCrear.Location = new System.Drawing.Point(15, 98);
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
            this.lblFecFinVaCrear.Location = new System.Drawing.Point(265, 98);
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
            this.lblMonedaCrear.Location = new System.Drawing.Point(15, 71);
            this.lblMonedaCrear.Name = "lblMonedaCrear";
            this.lblMonedaCrear.Size = new System.Drawing.Size(56, 15);
            this.lblMonedaCrear.TabIndex = 13;
            this.lblMonedaCrear.Text = "Moneda:";
            // 
            // cmbEstadoListPrecios
            // 
            this.cmbEstadoListPrecios.Enabled = false;
            this.cmbEstadoListPrecios.FormattingEnabled = true;
            this.cmbEstadoListPrecios.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoListPrecios.Location = new System.Drawing.Point(383, 71);
            this.cmbEstadoListPrecios.Name = "cmbEstadoListPrecios";
            this.cmbEstadoListPrecios.Size = new System.Drawing.Size(107, 21);
            this.cmbEstadoListPrecios.TabIndex = 10;
            // 
            // lblEstaListPre
            // 
            this.lblEstaListPre.AutoSize = true;
            this.lblEstaListPre.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaListPre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaListPre.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaListPre.Location = new System.Drawing.Point(265, 71);
            this.lblEstaListPre.Name = "lblEstaListPre";
            this.lblEstaListPre.Size = new System.Drawing.Size(48, 15);
            this.lblEstaListPre.TabIndex = 7;
            this.lblEstaListPre.Text = "Estado:";
            // 
            // lblLisPrec
            // 
            this.lblLisPrec.AutoSize = true;
            this.lblLisPrec.BackColor = System.Drawing.Color.Transparent;
            this.lblLisPrec.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLisPrec.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLisPrec.Location = new System.Drawing.Point(14, 30);
            this.lblLisPrec.Name = "lblLisPrec";
            this.lblLisPrec.Size = new System.Drawing.Size(91, 15);
            this.lblLisPrec.TabIndex = 3;
            this.lblLisPrec.Text = "Lista de Precio:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(105, 27);
            this.txtId.MaxLength = 20;
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(93, 20);
            this.txtId.TabIndex = 2;
            // 
            // FInformacionPrecProduc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(891, 458);
            this.Controls.Add(this.tbcPreProduc);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionPrecProduc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración de Precios de Productos";
            this.Load += new System.EventHandler(this.FInformacionPrecProduc_Load);
            this.tbcPreProduc.ResumeLayout(false);
            this.tblPreProduc.ResumeLayout(false);
            this.grpDatosProducto.ResumeLayout(false);
            this.grpDatosProducto.PerformLayout();
            this.grpListPreProduc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListPreciosProductos)).EndInit();
            this.grpDatoPreProduc.ResumeLayout(false);
            this.grpDatoPreProduc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcPreProduc;
        private System.Windows.Forms.TabPage tblPreProduc;
        private System.Windows.Forms.GroupBox grpListPreProduc;
        private System.Windows.Forms.GroupBox grpDatoPreProduc;
        private System.Windows.Forms.CheckBox chkListModi;
        private System.Windows.Forms.TextBox txtFecFinVa;
        private System.Windows.Forms.TextBox txtFecIniVa;
        private System.Windows.Forms.Label lblFecIniVaCrear;
        private System.Windows.Forms.Label lblFecFinVaCrear;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnListPrecio;
        private System.Windows.Forms.Label lblLisPrec;
        private System.Windows.Forms.CheckBox chkListBase;
        private System.Windows.Forms.Label lblMonedaCrear;
        private System.Windows.Forms.ComboBox cmbEstadoListPrecios;
        private System.Windows.Forms.Label lblEstaListPre;
        private System.Windows.Forms.Button btnCerrarListPrecios;
        private System.Windows.Forms.Button btnLimpiarListPrecios;
        private System.Windows.Forms.Button btnAnularListPrecios;
        private System.Windows.Forms.Button btnNuevoListPrecios;
        private System.Windows.Forms.TextBox txtMoneda;
        private System.Windows.Forms.Button btnNuevoPosOrd;
        private System.Windows.Forms.DataGridView dgvListPreciosProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIdPrecioProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmIdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrecioActual;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmValorPorcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmPrecioNuevo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmFechaFinal;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmModificado;
        private System.Windows.Forms.GroupBox grpDatosProducto;
        private System.Windows.Forms.Button btnListCategoria;
        private System.Windows.Forms.TextBox txtNomCategoria;
        private System.Windows.Forms.Button btnProducto;
        private System.Windows.Forms.TextBox txtDescripcionProduc;
        private System.Windows.Forms.Label lblLisCategoria;
        private System.Windows.Forms.TextBox txtIdCategoria;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.TextBox txtIdProduc;
    }
}