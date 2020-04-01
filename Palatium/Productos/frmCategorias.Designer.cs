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
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.btnBuscarCategoria = new System.Windows.Forms.Button();
            this.txtBuscarCategoria = new System.Windows.Forms.TextBox();
            this.dgvCategoria = new System.Windows.Forms.DataGridView();
            this.Grb_opcioCategori = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
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
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblEstaCajero = new System.Windows.Forms.Label();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodCategoria = new System.Windows.Forms.Label();
            this.txtCodigoCategoria = new System.Windows.Forms.TextBox();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.cmbPadre = new MisControles.ComboDatos();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDelivery = new System.Windows.Forms.CheckBox();
            this.Grb_listReCajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategoria)).BeginInit();
            this.Grb_opcioCategori.SuspendLayout();
            this.grupoDatos.SuspendLayout();
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
            this.dgvCategoria.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategoria.Location = new System.Drawing.Point(15, 54);
            this.dgvCategoria.Name = "dgvCategoria";
            this.dgvCategoria.ReadOnly = true;
            this.dgvCategoria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategoria.Size = new System.Drawing.Size(469, 389);
            this.dgvCategoria.TabIndex = 0;
            this.dgvCategoria.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCategoria_CellDoubleClick);
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
            this.grupoDatos.Controls.Add(this.cmbEstado);
            this.grupoDatos.Controls.Add(this.lblEstaCajero);
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
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(120, 137);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(107, 21);
            this.cmbEstado.TabIndex = 9;
            // 
            // lblEstaCajero
            // 
            this.lblEstaCajero.AutoSize = true;
            this.lblEstaCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaCajero.Location = new System.Drawing.Point(13, 139);
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
            // frmCategorias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(899, 480);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaCajero;
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
    }
}