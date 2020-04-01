namespace Palatium.Productos
{
    partial class frmMateriaPrima
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiarTodo = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dBAyudaCategorias = new Controles.Auxiliares.DB_Ayuda();
            this.grupoPrecio = new System.Windows.Forms.GroupBox();
            this.cmbConsumo = new System.Windows.Forms.ComboBox();
            this.cmbCompra = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPrecioMinorista = new System.Windows.Forms.TextBox();
            this.lblUnidadCompra = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUniConsumo = new System.Windows.Forms.Label();
            this.txtRendimiento = new System.Windows.Forms.TextBox();
            this.txtPrecioUnitario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.txtPresentacion = new System.Windows.Forms.TextBox();
            this.lblPreCompra = new System.Windows.Forms.Label();
            this.lblPrecioMinorista = new System.Windows.Forms.Label();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDecripCategoria = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCodiCategori = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.grupoStock = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblStocMin = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.lblSecuencia = new System.Windows.Forms.Label();
            this.txtSecuencia = new System.Windows.Forms.TextBox();
            this.txtStockMaximo = new System.Windows.Forms.TextBox();
            this.lblStoMax = new System.Windows.Forms.Label();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.chkPagaIva = new System.Windows.Forms.CheckBox();
            this.chkPreModifProductos = new System.Windows.Forms.CheckBox();
            this.chkExpiraProductos = new System.Windows.Forms.CheckBox();
            this.rdbReferenciaInsumos = new System.Windows.Forms.RadioButton();
            this.rdbReceta = new System.Windows.Forms.RadioButton();
            this.grupoReceta = new System.Windows.Forms.GroupBox();
            this.BtnLimpiarDbAyuda = new System.Windows.Forms.Button();
            this.dbAyudaReceta = new Controles.Auxiliares.DB_Ayuda();
            this.grupoBotones = new System.Windows.Forms.GroupBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.grupoGrid = new System.Windows.Forms.GroupBox();
            this.lblNombreCategoria = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.cmbTipoProducto = new System.Windows.Forms.ComboBox();
            this.cmbClaseProducto = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.grupoPrecio.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.grupoStock.SuspendLayout();
            this.grupoOpciones.SuspendLayout();
            this.grupoReceta.SuspendLayout();
            this.grupoBotones.SuspendLayout();
            this.grupoGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiarTodo);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.dBAyudaCategorias);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(703, 59);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione la categoría:";
            // 
            // btnLimpiarTodo
            // 
            this.btnLimpiarTodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarTodo.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarTodo.Location = new System.Drawing.Point(574, 19);
            this.btnLimpiarTodo.Name = "btnLimpiarTodo";
            this.btnLimpiarTodo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnLimpiarTodo.Size = new System.Drawing.Size(116, 30);
            this.btnLimpiarTodo.TabIndex = 3;
            this.btnLimpiarTodo.Text = "Limpiar Todo";
            this.btnLimpiarTodo.UseVisualStyleBackColor = false;
            this.btnLimpiarTodo.Click += new System.EventHandler(this.btnLimpiarTodo_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Blue;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(492, 18);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 31);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dBAyudaCategorias
            // 
            this.dBAyudaCategorias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dBAyudaCategorias.iId = 21;
            this.dBAyudaCategorias.Location = new System.Drawing.Point(9, 22);
            this.dBAyudaCategorias.Margin = new System.Windows.Forms.Padding(4);
            this.dBAyudaCategorias.Name = "dBAyudaCategorias";
            this.dBAyudaCategorias.sCodigo = null;
            this.dBAyudaCategorias.Size = new System.Drawing.Size(467, 22);
            this.dBAyudaCategorias.sNombre = null;
            this.dBAyudaCategorias.TabIndex = 1;
            // 
            // grupoPrecio
            // 
            this.grupoPrecio.Controls.Add(this.cmbConsumo);
            this.grupoPrecio.Controls.Add(this.cmbCompra);
            this.grupoPrecio.Controls.Add(this.label10);
            this.grupoPrecio.Controls.Add(this.label19);
            this.grupoPrecio.Controls.Add(this.label20);
            this.grupoPrecio.Controls.Add(this.label11);
            this.grupoPrecio.Controls.Add(this.label13);
            this.grupoPrecio.Controls.Add(this.label14);
            this.grupoPrecio.Controls.Add(this.txtPrecioMinorista);
            this.grupoPrecio.Controls.Add(this.lblUnidadCompra);
            this.grupoPrecio.Controls.Add(this.label6);
            this.grupoPrecio.Controls.Add(this.lblUniConsumo);
            this.grupoPrecio.Controls.Add(this.txtRendimiento);
            this.grupoPrecio.Controls.Add(this.txtPrecioUnitario);
            this.grupoPrecio.Controls.Add(this.label2);
            this.grupoPrecio.Controls.Add(this.label5);
            this.grupoPrecio.Controls.Add(this.txtPrecioCompra);
            this.grupoPrecio.Controls.Add(this.txtPresentacion);
            this.grupoPrecio.Controls.Add(this.lblPreCompra);
            this.grupoPrecio.Controls.Add(this.lblPrecioMinorista);
            this.grupoPrecio.Enabled = false;
            this.grupoPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoPrecio.Location = new System.Drawing.Point(449, 68);
            this.grupoPrecio.Name = "grupoPrecio";
            this.grupoPrecio.Size = new System.Drawing.Size(266, 244);
            this.grupoPrecio.TabIndex = 33;
            this.grupoPrecio.TabStop = false;
            this.grupoPrecio.Text = "Control de Precio y Unidades";
            // 
            // cmbConsumo
            // 
            this.cmbConsumo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConsumo.FormattingEnabled = true;
            this.cmbConsumo.Location = new System.Drawing.Point(133, 204);
            this.cmbConsumo.Name = "cmbConsumo";
            this.cmbConsumo.Size = new System.Drawing.Size(111, 21);
            this.cmbConsumo.TabIndex = 78;
            // 
            // cmbCompra
            // 
            this.cmbCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCompra.FormattingEnabled = true;
            this.cmbCompra.Location = new System.Drawing.Point(133, 178);
            this.cmbCompra.Name = "cmbCompra";
            this.cmbCompra.Size = new System.Drawing.Size(111, 21);
            this.cmbCompra.TabIndex = 73;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(117, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 18);
            this.label10.TabIndex = 76;
            this.label10.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(119, 204);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 18);
            this.label19.TabIndex = 77;
            this.label19.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(120, 182);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 18);
            this.label20.TabIndex = 76;
            this.label20.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(117, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 18);
            this.label11.TabIndex = 75;
            this.label11.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(117, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 18);
            this.label13.TabIndex = 74;
            this.label13.Text = "*";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(117, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 18);
            this.label14.TabIndex = 73;
            this.label14.Text = "*";
            // 
            // txtPrecioMinorista
            // 
            this.txtPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioMinorista.Location = new System.Drawing.Point(133, 135);
            this.txtPrecioMinorista.MaxLength = 20;
            this.txtPrecioMinorista.Name = "txtPrecioMinorista";
            this.txtPrecioMinorista.Size = new System.Drawing.Size(111, 20);
            this.txtPrecioMinorista.TabIndex = 41;
            this.txtPrecioMinorista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioMinorista_KeyPress);
            // 
            // lblUnidadCompra
            // 
            this.lblUnidadCompra.AutoSize = true;
            this.lblUnidadCompra.BackColor = System.Drawing.Color.Transparent;
            this.lblUnidadCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadCompra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUnidadCompra.Location = new System.Drawing.Point(17, 181);
            this.lblUnidadCompra.Name = "lblUnidadCompra";
            this.lblUnidadCompra.Size = new System.Drawing.Size(100, 15);
            this.lblUnidadCompra.TabIndex = 46;
            this.lblUnidadCompra.Text = "Unidad Compra: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(13, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 15);
            this.label6.TabIndex = 42;
            this.label6.Text = "Precio Minorista: ";
            // 
            // lblUniConsumo
            // 
            this.lblUniConsumo.AutoSize = true;
            this.lblUniConsumo.BackColor = System.Drawing.Color.Transparent;
            this.lblUniConsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUniConsumo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblUniConsumo.Location = new System.Drawing.Point(17, 206);
            this.lblUniConsumo.Name = "lblUniConsumo";
            this.lblUniConsumo.Size = new System.Drawing.Size(109, 15);
            this.lblUniConsumo.TabIndex = 47;
            this.lblUniConsumo.Text = "Unidad Consumo: ";
            // 
            // txtRendimiento
            // 
            this.txtRendimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtRendimiento.Location = new System.Drawing.Point(133, 69);
            this.txtRendimiento.MaxLength = 20;
            this.txtRendimiento.Name = "txtRendimiento";
            this.txtRendimiento.Size = new System.Drawing.Size(111, 20);
            this.txtRendimiento.TabIndex = 37;
            this.txtRendimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRendimiento_KeyPress);
            // 
            // txtPrecioUnitario
            // 
            this.txtPrecioUnitario.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPrecioUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioUnitario.Location = new System.Drawing.Point(133, 90);
            this.txtPrecioUnitario.MaxLength = 20;
            this.txtPrecioUnitario.Name = "txtPrecioUnitario";
            this.txtPrecioUnitario.ReadOnly = true;
            this.txtPrecioUnitario.Size = new System.Drawing.Size(111, 20);
            this.txtPrecioUnitario.TabIndex = 38;
            this.txtPrecioUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioUnitario_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 39;
            this.label2.Text = "Rendimiento: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(13, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 40;
            this.label5.Text = "Precio Unitario: ";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioCompra.Location = new System.Drawing.Point(133, 27);
            this.txtPrecioCompra.MaxLength = 20;
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(111, 20);
            this.txtPrecioCompra.TabIndex = 11;
            this.txtPrecioCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCompra_KeyPress);
            this.txtPrecioCompra.Leave += new System.EventHandler(this.txtPrecioCompra_Leave);
            // 
            // txtPresentacion
            // 
            this.txtPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPresentacion.Location = new System.Drawing.Point(133, 48);
            this.txtPresentacion.MaxLength = 20;
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Size = new System.Drawing.Size(111, 20);
            this.txtPresentacion.TabIndex = 12;
            this.txtPresentacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPresentacion_KeyPress);
            this.txtPresentacion.Leave += new System.EventHandler(this.txtPresentacion_Leave);
            // 
            // lblPreCompra
            // 
            this.lblPreCompra.AutoSize = true;
            this.lblPreCompra.BackColor = System.Drawing.Color.Transparent;
            this.lblPreCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreCompra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPreCompra.Location = new System.Drawing.Point(13, 29);
            this.lblPreCompra.Name = "lblPreCompra";
            this.lblPreCompra.Size = new System.Drawing.Size(95, 15);
            this.lblPreCompra.TabIndex = 34;
            this.lblPreCompra.Text = "Precio Compra: ";
            // 
            // lblPrecioMinorista
            // 
            this.lblPrecioMinorista.AutoSize = true;
            this.lblPrecioMinorista.BackColor = System.Drawing.Color.Transparent;
            this.lblPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioMinorista.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrecioMinorista.Location = new System.Drawing.Point(13, 51);
            this.lblPrecioMinorista.Name = "lblPrecioMinorista";
            this.lblPrecioMinorista.Size = new System.Drawing.Size(85, 15);
            this.lblPrecioMinorista.TabIndex = 36;
            this.lblPrecioMinorista.Text = "Presentación: ";
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.cmbClaseProducto);
            this.grupoDatos.Controls.Add(this.cmbTipoProducto);
            this.grupoDatos.Controls.Add(this.label8);
            this.grupoDatos.Controls.Add(this.label9);
            this.grupoDatos.Controls.Add(this.label7);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.lblDecripCategoria);
            this.grupoDatos.Controls.Add(this.txtNombre);
            this.grupoDatos.Controls.Add(this.lblCodiCategori);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 68);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(431, 129);
            this.grupoDatos.TabIndex = 32;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(138, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 18);
            this.label8.TabIndex = 72;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(138, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 18);
            this.label9.TabIndex = 71;
            this.label9.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(138, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 18);
            this.label7.TabIndex = 70;
            this.label7.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(138, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 18);
            this.label1.TabIndex = 69;
            this.label1.Text = "*";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 66;
            this.label3.Text = "Tipo de Producto:";
            // 
            // lblDecripCategoria
            // 
            this.lblDecripCategoria.AutoSize = true;
            this.lblDecripCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblDecripCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecripCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDecripCategoria.Location = new System.Drawing.Point(7, 52);
            this.lblDecripCategoria.Name = "lblDecripCategoria";
            this.lblDecripCategoria.Size = new System.Drawing.Size(129, 15);
            this.lblDecripCategoria.TabIndex = 32;
            this.lblDecripCategoria.Text = "Nombre del producto: ";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(154, 51);
            this.txtNombre.MaxLength = 30;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(270, 20);
            this.txtNombre.TabIndex = 8;
            // 
            // lblCodiCategori
            // 
            this.lblCodiCategori.AutoSize = true;
            this.lblCodiCategori.BackColor = System.Drawing.Color.Transparent;
            this.lblCodiCategori.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodiCategori.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodiCategori.Location = new System.Drawing.Point(7, 31);
            this.lblCodiCategori.Name = "lblCodiCategori";
            this.lblCodiCategori.Size = new System.Drawing.Size(123, 15);
            this.lblCodiCategori.TabIndex = 30;
            this.lblCodiCategori.Text = "Código del producto: ";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(154, 30);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(93, 20);
            this.txtCodigo.TabIndex = 7;
            // 
            // grupoStock
            // 
            this.grupoStock.Controls.Add(this.label15);
            this.grupoStock.Controls.Add(this.lblStocMin);
            this.grupoStock.Controls.Add(this.label16);
            this.grupoStock.Controls.Add(this.label17);
            this.grupoStock.Controls.Add(this.txtStockMinimo);
            this.grupoStock.Controls.Add(this.lblSecuencia);
            this.grupoStock.Controls.Add(this.txtSecuencia);
            this.grupoStock.Controls.Add(this.txtStockMaximo);
            this.grupoStock.Controls.Add(this.lblStoMax);
            this.grupoStock.Enabled = false;
            this.grupoStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoStock.Location = new System.Drawing.Point(207, 204);
            this.grupoStock.Name = "grupoStock";
            this.grupoStock.Size = new System.Drawing.Size(236, 109);
            this.grupoStock.TabIndex = 35;
            this.grupoStock.TabStop = false;
            this.grupoStock.Text = "Control Stock";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(103, 71);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 18);
            this.label15.TabIndex = 79;
            this.label15.Text = "*";
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
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(103, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 18);
            this.label16.TabIndex = 78;
            this.label16.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(103, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 18);
            this.label17.TabIndex = 77;
            this.label17.Text = "*";
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtStockMinimo.Location = new System.Drawing.Point(119, 28);
            this.txtStockMinimo.MaxLength = 20;
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(110, 20);
            this.txtStockMinimo.TabIndex = 18;
            this.txtStockMinimo.Text = "0";
            this.txtStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStockMinimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockMin_KeyPress);
            // 
            // lblSecuencia
            // 
            this.lblSecuencia.AutoSize = true;
            this.lblSecuencia.BackColor = System.Drawing.Color.Transparent;
            this.lblSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecuencia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSecuencia.Location = new System.Drawing.Point(16, 73);
            this.lblSecuencia.Name = "lblSecuencia";
            this.lblSecuencia.Size = new System.Drawing.Size(71, 15);
            this.lblSecuencia.TabIndex = 50;
            this.lblSecuencia.Text = "Secuencia: ";
            // 
            // txtSecuencia
            // 
            this.txtSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSecuencia.Location = new System.Drawing.Point(119, 72);
            this.txtSecuencia.MaxLength = 20;
            this.txtSecuencia.Name = "txtSecuencia";
            this.txtSecuencia.Size = new System.Drawing.Size(110, 20);
            this.txtSecuencia.TabIndex = 20;
            this.txtSecuencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecuencia_KeyPress);
            // 
            // txtStockMaximo
            // 
            this.txtStockMaximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtStockMaximo.Location = new System.Drawing.Point(119, 50);
            this.txtStockMaximo.MaxLength = 20;
            this.txtStockMaximo.Name = "txtStockMaximo";
            this.txtStockMaximo.Size = new System.Drawing.Size(110, 20);
            this.txtStockMaximo.TabIndex = 19;
            this.txtStockMaximo.Text = "0";
            this.txtStockMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtStockMaximo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStockMax_KeyPress);
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
            // grupoOpciones
            // 
            this.grupoOpciones.Controls.Add(this.chkPagaIva);
            this.grupoOpciones.Controls.Add(this.chkPreModifProductos);
            this.grupoOpciones.Controls.Add(this.chkExpiraProductos);
            this.grupoOpciones.Enabled = false;
            this.grupoOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoOpciones.Location = new System.Drawing.Point(12, 203);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(189, 109);
            this.grupoOpciones.TabIndex = 34;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
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
            // grupoReceta
            // 
            this.grupoReceta.Controls.Add(this.rdbReferenciaInsumos);
            this.grupoReceta.Controls.Add(this.rdbReceta);
            this.grupoReceta.Controls.Add(this.BtnLimpiarDbAyuda);
            this.grupoReceta.Controls.Add(this.dbAyudaReceta);
            this.grupoReceta.Enabled = false;
            this.grupoReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoReceta.Location = new System.Drawing.Point(12, 328);
            this.grupoReceta.Name = "grupoReceta";
            this.grupoReceta.Size = new System.Drawing.Size(467, 72);
            this.grupoReceta.TabIndex = 37;
            this.grupoReceta.TabStop = false;
            // 
            // BtnLimpiarDbAyuda
            // 
            this.BtnLimpiarDbAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLimpiarDbAyuda.ForeColor = System.Drawing.Color.Red;
            this.BtnLimpiarDbAyuda.Location = new System.Drawing.Point(432, 25);
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
            this.dbAyudaReceta.Size = new System.Drawing.Size(416, 22);
            this.dbAyudaReceta.sNombre = null;
            this.dbAyudaReceta.TabIndex = 22;
            // 
            // grupoBotones
            // 
            this.grupoBotones.Controls.Add(this.btnEliminar);
            this.grupoBotones.Controls.Add(this.btnLimpiar);
            this.grupoBotones.Controls.Add(this.btnAgregar);
            this.grupoBotones.Enabled = false;
            this.grupoBotones.Location = new System.Drawing.Point(485, 328);
            this.grupoBotones.Name = "grupoBotones";
            this.grupoBotones.Size = new System.Drawing.Size(230, 72);
            this.grupoBotones.TabIndex = 38;
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
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
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
            // grupoGrid
            // 
            this.grupoGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoGrid.Controls.Add(this.lblNombreCategoria);
            this.grupoGrid.Controls.Add(this.label12);
            this.grupoGrid.Controls.Add(this.btnBuscar);
            this.grupoGrid.Controls.Add(this.txtBuscar);
            this.grupoGrid.Controls.Add(this.dgvProductos);
            this.grupoGrid.Enabled = false;
            this.grupoGrid.Location = new System.Drawing.Point(721, 12);
            this.grupoGrid.Name = "grupoGrid";
            this.grupoGrid.Size = new System.Drawing.Size(461, 388);
            this.grupoGrid.TabIndex = 39;
            this.grupoGrid.TabStop = false;
            this.grupoGrid.Text = "Lista de Registros para Búsqueda por Nombres";
            // 
            // lblNombreCategoria
            // 
            this.lblNombreCategoria.AutoSize = true;
            this.lblNombreCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCategoria.Location = new System.Drawing.Point(118, 28);
            this.lblNombreCategoria.Name = "lblNombreCategoria";
            this.lblNombreCategoria.Size = new System.Drawing.Size(77, 16);
            this.lblNombreCategoria.TabIndex = 30;
            this.lblNombreCategoria.Text = "NINGUNA";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 16);
            this.label12.TabIndex = 29;
            this.label12.Text = "CATEGORÍA:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(230, 52);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 27;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(16, 56);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(208, 20);
            this.txtBuscar.TabIndex = 26;
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(16, 85);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersWidth = 25;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(435, 293);
            this.dgvProductos.TabIndex = 28;
            this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellDoubleClick);
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Location = new System.Drawing.Point(154, 73);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(194, 21);
            this.cmbTipoProducto.TabIndex = 79;
            // 
            // cmbClaseProducto
            // 
            this.cmbClaseProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClaseProducto.FormattingEnabled = true;
            this.cmbClaseProducto.Location = new System.Drawing.Point(154, 95);
            this.cmbClaseProducto.Name = "cmbClaseProducto";
            this.cmbClaseProducto.Size = new System.Drawing.Size(194, 21);
            this.cmbClaseProducto.TabIndex = 80;
            // 
            // frmMateriaPrima
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1190, 405);
            this.Controls.Add(this.grupoGrid);
            this.Controls.Add(this.grupoReceta);
            this.Controls.Add(this.grupoBotones);
            this.Controls.Add(this.grupoStock);
            this.Controls.Add(this.grupoOpciones);
            this.Controls.Add(this.grupoPrecio);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmMateriaPrima";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Ingreso de Materia Prima";
            this.Load += new System.EventHandler(this.frmMateriaPrima_Load);
            this.groupBox1.ResumeLayout(false);
            this.grupoPrecio.ResumeLayout(false);
            this.grupoPrecio.PerformLayout();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoStock.ResumeLayout(false);
            this.grupoStock.PerformLayout();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoOpciones.PerformLayout();
            this.grupoReceta.ResumeLayout(false);
            this.grupoReceta.PerformLayout();
            this.grupoBotones.ResumeLayout(false);
            this.grupoGrid.ResumeLayout(false);
            this.grupoGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLimpiarTodo;
        private System.Windows.Forms.Button btnOK;
        private Controles.Auxiliares.DB_Ayuda dBAyudaCategorias;
        private System.Windows.Forms.GroupBox grupoPrecio;
        private System.Windows.Forms.TextBox txtRendimiento;
        private System.Windows.Forms.TextBox txtPrecioUnitario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.TextBox txtPresentacion;
        private System.Windows.Forms.Label lblPreCompra;
        private System.Windows.Forms.Label lblPrecioMinorista;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblDecripCategoria;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCodiCategori;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.GroupBox grupoStock;
        private System.Windows.Forms.Label lblStocMin;
        private System.Windows.Forms.TextBox txtStockMinimo;
        private System.Windows.Forms.Label lblSecuencia;
        private System.Windows.Forms.TextBox txtSecuencia;
        private System.Windows.Forms.TextBox txtStockMaximo;
        private System.Windows.Forms.Label lblStoMax;
        private System.Windows.Forms.GroupBox grupoOpciones;
        private System.Windows.Forms.CheckBox chkPagaIva;
        private System.Windows.Forms.CheckBox chkPreModifProductos;
        private System.Windows.Forms.CheckBox chkExpiraProductos;
        private System.Windows.Forms.TextBox txtPrecioMinorista;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblUnidadCompra;
        private System.Windows.Forms.Label lblUniConsumo;
        private System.Windows.Forms.RadioButton rdbReferenciaInsumos;
        private System.Windows.Forms.RadioButton rdbReceta;
        private System.Windows.Forms.GroupBox grupoReceta;
        private System.Windows.Forms.Button BtnLimpiarDbAyuda;
        private Controles.Auxiliares.DB_Ayuda dbAyudaReceta;
        private System.Windows.Forms.GroupBox grupoBotones;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox grupoGrid;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblNombreCategoria;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbCompra;
        private System.Windows.Forms.ComboBox cmbConsumo;
        private System.Windows.Forms.ComboBox cmbClaseProducto;
        private System.Windows.Forms.ComboBox cmbTipoProducto;
    }
}