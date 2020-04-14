namespace Palatium.Productos
{
    partial class frmIngresoProductos
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
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.grupoRegistros = new System.Windows.Forms.GroupBox();
            this.lblNombreCategoria = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblRegistros = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkExpira = new System.Windows.Forms.CheckBox();
            this.chkPrecioModificable = new System.Windows.Forms.CheckBox();
            this.chkPagaIVA = new System.Windows.Forms.CheckBox();
            this.grupoReceta = new System.Windows.Forms.GroupBox();
            this.dbAyudaReceta = new Controles.Auxiliares.DB_Ayuda();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.chkPagaServicio = new System.Windows.Forms.CheckBox();
            this.chkItemTarjetaRegalo = new System.Windows.Forms.CheckBox();
            this.chkParaTarjetaRegalo = new System.Windows.Forms.CheckBox();
            this.chkItemTarjetaAlmuerzo = new System.Windows.Forms.CheckBox();
            this.chkParaTarjetaAlmuerzo = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtRendimiento = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPresentacion = new System.Windows.Forms.TextBox();
            this.chkUsarRecets = new System.Windows.Forms.CheckBox();
            this.chkAhorroEmergencia = new System.Windows.Forms.CheckBox();
            this.cmbDestinoImpresion = new MisControles.ComboDatos();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbClaseProducto = new MisControles.ComboDatos();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipoProducto = new MisControles.ComboDatos();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSecuencia = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtPrecioMinorista = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiarTodo = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dBAyudaCategorias = new Controles.Auxiliares.DB_Ayuda();
            this.rdbGuardaConImpuestos = new System.Windows.Forms.RadioButton();
            this.rdbGuardaSinImpuestos = new System.Windows.Forms.RadioButton();
            this.chkHabilitado = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.grupoRegistros.SuspendLayout();
            this.grupoReceta.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(10, 67);
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(435, 344);
            this.dgvProductos.TabIndex = 21;
            this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductos_CellDoubleClick);
            // 
            // grupoRegistros
            // 
            this.grupoRegistros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoRegistros.Controls.Add(this.lblNombreCategoria);
            this.grupoRegistros.Controls.Add(this.label12);
            this.grupoRegistros.Controls.Add(this.lblRegistros);
            this.grupoRegistros.Controls.Add(this.label16);
            this.grupoRegistros.Controls.Add(this.btnBuscar);
            this.grupoRegistros.Controls.Add(this.txtBuscar);
            this.grupoRegistros.Controls.Add(this.label2);
            this.grupoRegistros.Controls.Add(this.dgvProductos);
            this.grupoRegistros.Enabled = false;
            this.grupoRegistros.Location = new System.Drawing.Point(12, 100);
            this.grupoRegistros.Name = "grupoRegistros";
            this.grupoRegistros.Size = new System.Drawing.Size(458, 441);
            this.grupoRegistros.TabIndex = 23;
            this.grupoRegistros.TabStop = false;
            // 
            // lblNombreCategoria
            // 
            this.lblNombreCategoria.AutoSize = true;
            this.lblNombreCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCategoria.Location = new System.Drawing.Point(137, 16);
            this.lblNombreCategoria.Name = "lblNombreCategoria";
            this.lblNombreCategoria.Size = new System.Drawing.Size(77, 16);
            this.lblNombreCategoria.TabIndex = 27;
            this.lblNombreCategoria.Text = "NINGUNA";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(99, 16);
            this.label12.TabIndex = 26;
            this.label12.Text = "CATEGORÍA:";
            // 
            // lblRegistros
            // 
            this.lblRegistros.AutoSize = true;
            this.lblRegistros.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistros.Location = new System.Drawing.Point(137, 414);
            this.lblRegistros.Name = "lblRegistros";
            this.lblRegistros.Size = new System.Drawing.Size(155, 16);
            this.lblRegistros.TabIndex = 0;
            this.lblRegistros.Text = "0 Registros Encontrados";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(28, 414);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "N° de Registros:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_ico;
            this.btnBuscar.Location = new System.Drawing.Point(411, 37);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(34, 27);
            this.btnBuscar.TabIndex = 5;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Location = new System.Drawing.Point(140, 41);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(265, 20);
            this.txtBuscar.TabIndex = 4;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarProducto_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Búsqueda:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(132, 22);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(112, 21);
            this.txtCodigo.TabIndex = 6;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(132, 45);
            this.txtDescripcion.MaxLength = 100;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(289, 21);
            this.txtDescripcion.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Descripción:";
            // 
            // chkExpira
            // 
            this.chkExpira.AutoSize = true;
            this.chkExpira.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExpira.Location = new System.Drawing.Point(226, 242);
            this.chkExpira.Name = "chkExpira";
            this.chkExpira.Size = new System.Drawing.Size(61, 19);
            this.chkExpira.TabIndex = 17;
            this.chkExpira.Text = "Expira";
            this.chkExpira.UseVisualStyleBackColor = true;
            // 
            // chkPrecioModificable
            // 
            this.chkPrecioModificable.AutoSize = true;
            this.chkPrecioModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrecioModificable.Location = new System.Drawing.Point(293, 242);
            this.chkPrecioModificable.Name = "chkPrecioModificable";
            this.chkPrecioModificable.Size = new System.Drawing.Size(128, 19);
            this.chkPrecioModificable.TabIndex = 18;
            this.chkPrecioModificable.Text = "Precio modificable";
            this.chkPrecioModificable.UseVisualStyleBackColor = true;
            // 
            // chkPagaIVA
            // 
            this.chkPagaIVA.AutoSize = true;
            this.chkPagaIVA.Checked = true;
            this.chkPagaIVA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPagaIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPagaIVA.Location = new System.Drawing.Point(12, 242);
            this.chkPagaIVA.Name = "chkPagaIVA";
            this.chkPagaIVA.Size = new System.Drawing.Size(75, 19);
            this.chkPagaIVA.TabIndex = 16;
            this.chkPagaIVA.Text = "Paga IVA";
            this.chkPagaIVA.UseVisualStyleBackColor = true;
            // 
            // grupoReceta
            // 
            this.grupoReceta.Controls.Add(this.dbAyudaReceta);
            this.grupoReceta.Enabled = false;
            this.grupoReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoReceta.Location = new System.Drawing.Point(486, 434);
            this.grupoReceta.Name = "grupoReceta";
            this.grupoReceta.Size = new System.Drawing.Size(433, 64);
            this.grupoReceta.TabIndex = 25;
            this.grupoReceta.TabStop = false;
            this.grupoReceta.Text = "Control de Receta";
            // 
            // dbAyudaReceta
            // 
            this.dbAyudaReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbAyudaReceta.iId = 21;
            this.dbAyudaReceta.Location = new System.Drawing.Point(7, 27);
            this.dbAyudaReceta.Margin = new System.Windows.Forms.Padding(4);
            this.dbAyudaReceta.Name = "dbAyudaReceta";
            this.dbAyudaReceta.sCodigo = null;
            this.dbAyudaReceta.Size = new System.Drawing.Size(414, 22);
            this.dbAyudaReceta.sNombre = null;
            this.dbAyudaReceta.TabIndex = 17;
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.chkPagaServicio);
            this.grupoDatos.Controls.Add(this.chkItemTarjetaRegalo);
            this.grupoDatos.Controls.Add(this.chkParaTarjetaRegalo);
            this.grupoDatos.Controls.Add(this.chkItemTarjetaAlmuerzo);
            this.grupoDatos.Controls.Add(this.chkParaTarjetaAlmuerzo);
            this.grupoDatos.Controls.Add(this.label21);
            this.grupoDatos.Controls.Add(this.txtRendimiento);
            this.grupoDatos.Controls.Add(this.label15);
            this.grupoDatos.Controls.Add(this.txtPresentacion);
            this.grupoDatos.Controls.Add(this.chkUsarRecets);
            this.grupoDatos.Controls.Add(this.chkAhorroEmergencia);
            this.grupoDatos.Controls.Add(this.cmbDestinoImpresion);
            this.grupoDatos.Controls.Add(this.label11);
            this.grupoDatos.Controls.Add(this.label8);
            this.grupoDatos.Controls.Add(this.label10);
            this.grupoDatos.Controls.Add(this.cmbClaseProducto);
            this.grupoDatos.Controls.Add(this.label5);
            this.grupoDatos.Controls.Add(this.cmbTipoProducto);
            this.grupoDatos.Controls.Add(this.label6);
            this.grupoDatos.Controls.Add(this.chkExpira);
            this.grupoDatos.Controls.Add(this.label9);
            this.grupoDatos.Controls.Add(this.chkPrecioModificable);
            this.grupoDatos.Controls.Add(this.chkPagaIVA);
            this.grupoDatos.Controls.Add(this.label7);
            this.grupoDatos.Controls.Add(this.txtSecuencia);
            this.grupoDatos.Controls.Add(this.label19);
            this.grupoDatos.Controls.Add(this.label20);
            this.grupoDatos.Controls.Add(this.txtPrecioMinorista);
            this.grupoDatos.Controls.Add(this.label14);
            this.grupoDatos.Controls.Add(this.txtPrecioCompra);
            this.grupoDatos.Controls.Add(this.label13);
            this.grupoDatos.Controls.Add(this.label17);
            this.grupoDatos.Controls.Add(this.label18);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grupoDatos.Location = new System.Drawing.Point(486, 100);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(433, 330);
            this.grupoDatos.TabIndex = 24;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos Generales";
            // 
            // chkPagaServicio
            // 
            this.chkPagaServicio.AutoSize = true;
            this.chkPagaServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPagaServicio.ForeColor = System.Drawing.Color.Red;
            this.chkPagaServicio.Location = new System.Drawing.Point(93, 242);
            this.chkPagaServicio.Name = "chkPagaServicio";
            this.chkPagaServicio.Size = new System.Drawing.Size(101, 19);
            this.chkPagaServicio.TabIndex = 85;
            this.chkPagaServicio.Text = "Paga Servicio";
            this.chkPagaServicio.UseVisualStyleBackColor = true;
            // 
            // chkItemTarjetaRegalo
            // 
            this.chkItemTarjetaRegalo.AutoSize = true;
            this.chkItemTarjetaRegalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkItemTarjetaRegalo.Location = new System.Drawing.Point(226, 303);
            this.chkItemTarjetaRegalo.Name = "chkItemTarjetaRegalo";
            this.chkItemTarjetaRegalo.Size = new System.Drawing.Size(183, 19);
            this.chkItemTarjetaRegalo.TabIndex = 84;
            this.chkItemTarjetaRegalo.Text = "Ítem de una tarjeta de regalo";
            this.chkItemTarjetaRegalo.UseVisualStyleBackColor = true;
            // 
            // chkParaTarjetaRegalo
            // 
            this.chkParaTarjetaRegalo.AutoSize = true;
            this.chkParaTarjetaRegalo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkParaTarjetaRegalo.Location = new System.Drawing.Point(12, 303);
            this.chkParaTarjetaRegalo.Name = "chkParaTarjetaRegalo";
            this.chkParaTarjetaRegalo.Size = new System.Drawing.Size(153, 19);
            this.chkParaTarjetaRegalo.TabIndex = 83;
            this.chkParaTarjetaRegalo.Text = "Para Tarjeta de Regalo";
            this.chkParaTarjetaRegalo.UseVisualStyleBackColor = true;
            // 
            // chkItemTarjetaAlmuerzo
            // 
            this.chkItemTarjetaAlmuerzo.AutoSize = true;
            this.chkItemTarjetaAlmuerzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkItemTarjetaAlmuerzo.Location = new System.Drawing.Point(226, 282);
            this.chkItemTarjetaAlmuerzo.Name = "chkItemTarjetaAlmuerzo";
            this.chkItemTarjetaAlmuerzo.Size = new System.Drawing.Size(200, 19);
            this.chkItemTarjetaAlmuerzo.TabIndex = 82;
            this.chkItemTarjetaAlmuerzo.Text = "Ítem de una tarjeta de almuerzo";
            this.chkItemTarjetaAlmuerzo.UseVisualStyleBackColor = true;
            // 
            // chkParaTarjetaAlmuerzo
            // 
            this.chkParaTarjetaAlmuerzo.AutoSize = true;
            this.chkParaTarjetaAlmuerzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkParaTarjetaAlmuerzo.Location = new System.Drawing.Point(12, 282);
            this.chkParaTarjetaAlmuerzo.Name = "chkParaTarjetaAlmuerzo";
            this.chkParaTarjetaAlmuerzo.Size = new System.Drawing.Size(165, 19);
            this.chkParaTarjetaAlmuerzo.TabIndex = 81;
            this.chkParaTarjetaAlmuerzo.Text = "Para Tarjeta de Almuerzo";
            this.chkParaTarjetaAlmuerzo.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(253, 91);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(81, 15);
            this.label21.TabIndex = 80;
            this.label21.Text = "Rendimiento:";
            // 
            // txtRendimiento
            // 
            this.txtRendimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRendimiento.Location = new System.Drawing.Point(345, 88);
            this.txtRendimiento.MaxLength = 3;
            this.txtRendimiento.Name = "txtRendimiento";
            this.txtRendimiento.Size = new System.Drawing.Size(76, 21);
            this.txtRendimiento.TabIndex = 11;
            this.txtRendimiento.Text = "1";
            this.txtRendimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRendimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRendimiento_KeyPress);
            this.txtRendimiento.Leave += new System.EventHandler(this.txtRendimiento_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(11, 94);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 15);
            this.label15.TabIndex = 78;
            this.label15.Text = "Presentación:";
            // 
            // txtPresentacion
            // 
            this.txtPresentacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPresentacion.Location = new System.Drawing.Point(132, 91);
            this.txtPresentacion.MaxLength = 3;
            this.txtPresentacion.Name = "txtPresentacion";
            this.txtPresentacion.Size = new System.Drawing.Size(76, 21);
            this.txtPresentacion.TabIndex = 10;
            this.txtPresentacion.Text = "1";
            this.txtPresentacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPresentacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPresentacion_KeyPress);
            this.txtPresentacion.Leave += new System.EventHandler(this.txtPresentacion_Leave);
            // 
            // chkUsarRecets
            // 
            this.chkUsarRecets.AutoSize = true;
            this.chkUsarRecets.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUsarRecets.Location = new System.Drawing.Point(226, 262);
            this.chkUsarRecets.Name = "chkUsarRecets";
            this.chkUsarRecets.Size = new System.Drawing.Size(123, 19);
            this.chkUsarRecets.TabIndex = 20;
            this.chkUsarRecets.Text = "Usar para recetas";
            this.chkUsarRecets.UseVisualStyleBackColor = true;
            // 
            // chkAhorroEmergencia
            // 
            this.chkAhorroEmergencia.AutoSize = true;
            this.chkAhorroEmergencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAhorroEmergencia.Location = new System.Drawing.Point(12, 262);
            this.chkAhorroEmergencia.Name = "chkAhorroEmergencia";
            this.chkAhorroEmergencia.Size = new System.Drawing.Size(178, 19);
            this.chkAhorroEmergencia.TabIndex = 19;
            this.chkAhorroEmergencia.Text = "Para Ahorro de Emergencia";
            this.chkAhorroEmergencia.UseVisualStyleBackColor = true;
            this.chkAhorroEmergencia.Visible = false;
            // 
            // cmbDestinoImpresion
            // 
            this.cmbDestinoImpresion.FormattingEnabled = true;
            this.cmbDestinoImpresion.Location = new System.Drawing.Point(132, 203);
            this.cmbDestinoImpresion.Name = "cmbDestinoImpresion";
            this.cmbDestinoImpresion.Size = new System.Drawing.Size(217, 24);
            this.cmbDestinoImpresion.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.Location = new System.Drawing.Point(14, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 30);
            this.label11.TabIndex = 72;
            this.label11.Text = "Destino de\r\nImpresión:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(117, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 15);
            this.label8.TabIndex = 74;
            this.label8.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(117, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 15);
            this.label10.TabIndex = 73;
            this.label10.Text = "*";
            // 
            // cmbClaseProducto
            // 
            this.cmbClaseProducto.FormattingEnabled = true;
            this.cmbClaseProducto.Location = new System.Drawing.Point(132, 171);
            this.cmbClaseProducto.Name = "cmbClaseProducto";
            this.cmbClaseProducto.Size = new System.Drawing.Size(217, 24);
            this.cmbClaseProducto.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(11, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 15);
            this.label5.TabIndex = 72;
            this.label5.Text = "Clase de Producto";
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Location = new System.Drawing.Point(132, 146);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(217, 24);
            this.cmbTipoProducto.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(10, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 15);
            this.label6.TabIndex = 71;
            this.label6.Text = "Tipo de Producto";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(117, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 15);
            this.label9.TabIndex = 27;
            this.label9.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 116);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Secuencia:";
            // 
            // txtSecuencia
            // 
            this.txtSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSecuencia.Location = new System.Drawing.Point(132, 114);
            this.txtSecuencia.MaxLength = 3;
            this.txtSecuencia.Name = "txtSecuencia";
            this.txtSecuencia.Size = new System.Drawing.Size(76, 21);
            this.txtSecuencia.TabIndex = 12;
            this.txtSecuencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecuencia_KeyPress_1);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(330, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(12, 15);
            this.label19.TabIndex = 24;
            this.label19.Text = "*";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(117, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(12, 15);
            this.label20.TabIndex = 23;
            this.label20.Text = "*";
            // 
            // txtPrecioMinorista
            // 
            this.txtPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioMinorista.Location = new System.Drawing.Point(345, 67);
            this.txtPrecioMinorista.MaxLength = 7;
            this.txtPrecioMinorista.Name = "txtPrecioMinorista";
            this.txtPrecioMinorista.Size = new System.Drawing.Size(76, 21);
            this.txtPrecioMinorista.TabIndex = 9;
            this.txtPrecioMinorista.Text = "1.00";
            this.txtPrecioMinorista.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecioMinorista.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioMinorista_KeyPress_1);
            this.txtPrecioMinorista.Leave += new System.EventHandler(this.txtPrecioMinorista_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(235, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 15);
            this.label14.TabIndex = 17;
            this.label14.Text = "Precio Minorista:";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioCompra.Location = new System.Drawing.Point(132, 68);
            this.txtPrecioCompra.MaxLength = 7;
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(76, 21);
            this.txtPrecioCompra.TabIndex = 8;
            this.txtPrecioCompra.Text = "1.00";
            this.txtPrecioCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecioCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCompra_KeyPress_1);
            this.txtPrecioCompra.Leave += new System.EventHandler(this.txtPrecioCompra_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(10, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "Precio Compra:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(117, 45);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(12, 15);
            this.label17.TabIndex = 11;
            this.label17.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(117, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(12, 15);
            this.label18.TabIndex = 10;
            this.label18.Text = "*";
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.Red;
            this.btnEliminar.Enabled = false;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(732, 502);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(93, 39);
            this.btnEliminar.TabIndex = 22;
            this.btnEliminar.Text = "Inhabilitar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(826, 502);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnLimpiar.Size = new System.Drawing.Size(93, 39);
            this.btnLimpiar.TabIndex = 23;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Blue;
            this.btnAgregar.Enabled = false;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(637, 502);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(93, 39);
            this.btnAgregar.TabIndex = 21;
            this.btnAgregar.Text = "Nuevo";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLimpiarTodo);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dBAyudaCategorias);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(907, 48);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // btnLimpiarTodo
            // 
            this.btnLimpiarTodo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarTodo.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarTodo.Location = new System.Drawing.Point(779, 12);
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
            this.btnOK.Location = new System.Drawing.Point(714, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(59, 31);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 15);
            this.label1.TabIndex = 75;
            this.label1.Text = "Seleccione la categoría de productos";
            // 
            // dBAyudaCategorias
            // 
            this.dBAyudaCategorias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dBAyudaCategorias.iId = 21;
            this.dBAyudaCategorias.Location = new System.Drawing.Point(229, 15);
            this.dBAyudaCategorias.Margin = new System.Windows.Forms.Padding(4);
            this.dBAyudaCategorias.Name = "dBAyudaCategorias";
            this.dBAyudaCategorias.sCodigo = null;
            this.dBAyudaCategorias.Size = new System.Drawing.Size(471, 22);
            this.dBAyudaCategorias.sNombre = null;
            this.dBAyudaCategorias.TabIndex = 1;
            // 
            // rdbGuardaConImpuestos
            // 
            this.rdbGuardaConImpuestos.AutoSize = true;
            this.rdbGuardaConImpuestos.Enabled = false;
            this.rdbGuardaConImpuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGuardaConImpuestos.Location = new System.Drawing.Point(318, 16);
            this.rdbGuardaConImpuestos.Name = "rdbGuardaConImpuestos";
            this.rdbGuardaConImpuestos.Size = new System.Drawing.Size(270, 20);
            this.rdbGuardaConImpuestos.TabIndex = 26;
            this.rdbGuardaConImpuestos.TabStop = true;
            this.rdbGuardaConImpuestos.Text = "Guardar información con impuestos";
            this.rdbGuardaConImpuestos.UseVisualStyleBackColor = true;
            // 
            // rdbGuardaSinImpuestos
            // 
            this.rdbGuardaSinImpuestos.AutoSize = true;
            this.rdbGuardaSinImpuestos.Enabled = false;
            this.rdbGuardaSinImpuestos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbGuardaSinImpuestos.Location = new System.Drawing.Point(12, 16);
            this.rdbGuardaSinImpuestos.Name = "rdbGuardaSinImpuestos";
            this.rdbGuardaSinImpuestos.Size = new System.Drawing.Size(265, 20);
            this.rdbGuardaSinImpuestos.TabIndex = 27;
            this.rdbGuardaSinImpuestos.TabStop = true;
            this.rdbGuardaSinImpuestos.Text = "Guardar información sin impuestos";
            this.rdbGuardaSinImpuestos.UseVisualStyleBackColor = true;
            // 
            // chkHabilitado
            // 
            this.chkHabilitado.AutoSize = true;
            this.chkHabilitado.Checked = true;
            this.chkHabilitado.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHabilitado.Enabled = false;
            this.chkHabilitado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHabilitado.ForeColor = System.Drawing.Color.Red;
            this.chkHabilitado.Location = new System.Drawing.Point(489, 514);
            this.chkHabilitado.Name = "chkHabilitado";
            this.chkHabilitado.Size = new System.Drawing.Size(138, 17);
            this.chkHabilitado.TabIndex = 60;
            this.chkHabilitado.Text = "Producto Habilitado";
            this.chkHabilitado.UseVisualStyleBackColor = true;
            // 
            // frmIngresoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(931, 549);
            this.Controls.Add(this.chkHabilitado);
            this.Controls.Add(this.rdbGuardaSinImpuestos);
            this.Controls.Add(this.rdbGuardaConImpuestos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.grupoReceta);
            this.Controls.Add(this.grupoRegistros);
            this.MaximizeBox = false;
            this.Name = "frmIngresoProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Productos";
            this.Load += new System.EventHandler(this.frmIngresoProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.grupoRegistros.ResumeLayout(false);
            this.grupoRegistros.PerformLayout();
            this.grupoReceta.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.GroupBox grupoRegistros;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkExpira;
        private System.Windows.Forms.CheckBox chkPrecioModificable;
        private System.Windows.Forms.CheckBox chkPagaIVA;
        private System.Windows.Forms.GroupBox grupoReceta;
        private Controles.Auxiliares.DB_Ayuda dbAyudaReceta;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblRegistros;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSecuencia;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtPrecioMinorista;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private MisControles.ComboDatos cmbClaseProducto;
        private System.Windows.Forms.Label label5;
        private MisControles.ComboDatos cmbTipoProducto;
        private System.Windows.Forms.Label label6;
        private MisControles.ComboDatos cmbDestinoImpresion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Controles.Auxiliares.DB_Ayuda dBAyudaCategorias;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnLimpiarTodo;
        private System.Windows.Forms.Label lblNombreCategoria;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rdbGuardaConImpuestos;
        private System.Windows.Forms.RadioButton rdbGuardaSinImpuestos;
        private System.Windows.Forms.CheckBox chkAhorroEmergencia;
        private System.Windows.Forms.CheckBox chkUsarRecets;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPresentacion;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtRendimiento;
        private System.Windows.Forms.CheckBox chkHabilitado;
        private System.Windows.Forms.CheckBox chkItemTarjetaRegalo;
        private System.Windows.Forms.CheckBox chkParaTarjetaRegalo;
        private System.Windows.Forms.CheckBox chkItemTarjetaAlmuerzo;
        private System.Windows.Forms.CheckBox chkParaTarjetaAlmuerzo;
        private System.Windows.Forms.CheckBox chkPagaServicio;
    }
}