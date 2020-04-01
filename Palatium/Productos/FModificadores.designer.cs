namespace Palatium.Productos
{
    partial class FModificadores
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
            this.tabCon_Modificadores = new System.Windows.Forms.TabControl();
            this.tabPag_Modificadores = new System.Windows.Forms.TabPage();
            this.grupoImpresion = new System.Windows.Forms.GroupBox();
            this.cmbDestinoImpresion = new MisControles.ComboDatos();
            this.label5 = new System.Windows.Forms.Label();
            this.grupoPrecio = new System.Windows.Forms.GroupBox();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.txtPrecioMinorista = new System.Windows.Forms.TextBox();
            this.lblPreCompra = new System.Windows.Forms.Label();
            this.lblPrecioMinorista = new System.Windows.Forms.Label();
            this.txtSecuencia = new System.Windows.Forms.TextBox();
            this.lblSecuencia = new System.Windows.Forms.Label();
            this.lblEstaCajero = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.chkPagaIva = new System.Windows.Forms.CheckBox();
            this.chkModificable = new System.Windows.Forms.CheckBox();
            this.chkPrecioModificable = new System.Windows.Forms.CheckBox();
            this.grupoGrid = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.grupoOpciones = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClaseProducto = new MisControles.ComboDatos();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbPadre = new MisControles.ComboDatos();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.cmbTipoProducto = new MisControles.ComboDatos();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDescrCajero = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblCodCategoria = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.tabCon_Modificadores.SuspendLayout();
            this.tabPag_Modificadores.SuspendLayout();
            this.grupoImpresion.SuspendLayout();
            this.grupoPrecio.SuspendLayout();
            this.grupoGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoOpciones.SuspendLayout();
            this.grupoDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_Modificadores
            // 
            this.tabCon_Modificadores.Controls.Add(this.tabPag_Modificadores);
            this.tabCon_Modificadores.Location = new System.Drawing.Point(-4, 0);
            this.tabCon_Modificadores.Name = "tabCon_Modificadores";
            this.tabCon_Modificadores.SelectedIndex = 0;
            this.tabCon_Modificadores.Size = new System.Drawing.Size(997, 564);
            this.tabCon_Modificadores.TabIndex = 3;
            // 
            // tabPag_Modificadores
            // 
            this.tabPag_Modificadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.tabPag_Modificadores.Controls.Add(this.grupoImpresion);
            this.tabPag_Modificadores.Controls.Add(this.grupoPrecio);
            this.tabPag_Modificadores.Controls.Add(this.grupoGrid);
            this.tabPag_Modificadores.Controls.Add(this.grupoOpciones);
            this.tabPag_Modificadores.Controls.Add(this.grupoDatos);
            this.tabPag_Modificadores.Location = new System.Drawing.Point(4, 22);
            this.tabPag_Modificadores.Name = "tabPag_Modificadores";
            this.tabPag_Modificadores.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_Modificadores.Size = new System.Drawing.Size(989, 538);
            this.tabPag_Modificadores.TabIndex = 0;
            this.tabPag_Modificadores.Text = "Mantenimiento de Modificadores";
            // 
            // grupoImpresion
            // 
            this.grupoImpresion.Controls.Add(this.cmbDestinoImpresion);
            this.grupoImpresion.Controls.Add(this.label5);
            this.grupoImpresion.Enabled = false;
            this.grupoImpresion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoImpresion.Location = new System.Drawing.Point(17, 380);
            this.grupoImpresion.Name = "grupoImpresion";
            this.grupoImpresion.Size = new System.Drawing.Size(398, 61);
            this.grupoImpresion.TabIndex = 74;
            this.grupoImpresion.TabStop = false;
            this.grupoImpresion.Text = "Control de Destino  de Impresión";
            // 
            // cmbDestinoImpresion
            // 
            this.cmbDestinoImpresion.FormattingEnabled = true;
            this.cmbDestinoImpresion.Location = new System.Drawing.Point(159, 25);
            this.cmbDestinoImpresion.Name = "cmbDestinoImpresion";
            this.cmbDestinoImpresion.Size = new System.Drawing.Size(223, 24);
            this.cmbDestinoImpresion.TabIndex = 16;
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
            // grupoPrecio
            // 
            this.grupoPrecio.Controls.Add(this.txtPrecioCompra);
            this.grupoPrecio.Controls.Add(this.txtPrecioMinorista);
            this.grupoPrecio.Controls.Add(this.lblPreCompra);
            this.grupoPrecio.Controls.Add(this.lblPrecioMinorista);
            this.grupoPrecio.Controls.Add(this.txtSecuencia);
            this.grupoPrecio.Controls.Add(this.lblSecuencia);
            this.grupoPrecio.Controls.Add(this.lblEstaCajero);
            this.grupoPrecio.Controls.Add(this.cmbEstado);
            this.grupoPrecio.Controls.Add(this.chkPagaIva);
            this.grupoPrecio.Controls.Add(this.chkModificable);
            this.grupoPrecio.Controls.Add(this.chkPrecioModificable);
            this.grupoPrecio.Enabled = false;
            this.grupoPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoPrecio.Location = new System.Drawing.Point(17, 208);
            this.grupoPrecio.Name = "grupoPrecio";
            this.grupoPrecio.Size = new System.Drawing.Size(404, 160);
            this.grupoPrecio.TabIndex = 73;
            this.grupoPrecio.TabStop = false;
            this.grupoPrecio.Text = "Control de Precio";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioCompra.Location = new System.Drawing.Point(168, 24);
            this.txtPrecioCompra.MaxLength = 20;
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(106, 20);
            this.txtPrecioCompra.TabIndex = 9;
            // 
            // txtPrecioMinorista
            // 
            this.txtPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPrecioMinorista.Location = new System.Drawing.Point(168, 46);
            this.txtPrecioMinorista.MaxLength = 20;
            this.txtPrecioMinorista.Name = "txtPrecioMinorista";
            this.txtPrecioMinorista.Size = new System.Drawing.Size(106, 20);
            this.txtPrecioMinorista.TabIndex = 10;
            // 
            // lblPreCompra
            // 
            this.lblPreCompra.AutoSize = true;
            this.lblPreCompra.BackColor = System.Drawing.Color.Transparent;
            this.lblPreCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreCompra.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPreCompra.Location = new System.Drawing.Point(17, 25);
            this.lblPreCompra.Name = "lblPreCompra";
            this.lblPreCompra.Size = new System.Drawing.Size(100, 15);
            this.lblPreCompra.TabIndex = 34;
            this.lblPreCompra.Text = "Precio Compra: *";
            // 
            // lblPrecioMinorista
            // 
            this.lblPrecioMinorista.AutoSize = true;
            this.lblPrecioMinorista.BackColor = System.Drawing.Color.Transparent;
            this.lblPrecioMinorista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioMinorista.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPrecioMinorista.Location = new System.Drawing.Point(17, 47);
            this.lblPrecioMinorista.Name = "lblPrecioMinorista";
            this.lblPrecioMinorista.Size = new System.Drawing.Size(141, 15);
            this.lblPrecioMinorista.TabIndex = 36;
            this.lblPrecioMinorista.Text = "Precio Minorista (PVP): *";
            // 
            // txtSecuencia
            // 
            this.txtSecuencia.Location = new System.Drawing.Point(168, 68);
            this.txtSecuencia.MaxLength = 3;
            this.txtSecuencia.Name = "txtSecuencia";
            this.txtSecuencia.Size = new System.Drawing.Size(63, 22);
            this.txtSecuencia.TabIndex = 11;
            this.txtSecuencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecuencia_KeyPress);
            // 
            // lblSecuencia
            // 
            this.lblSecuencia.AutoSize = true;
            this.lblSecuencia.BackColor = System.Drawing.Color.Transparent;
            this.lblSecuencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecuencia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSecuencia.Location = new System.Drawing.Point(16, 71);
            this.lblSecuencia.Name = "lblSecuencia";
            this.lblSecuencia.Size = new System.Drawing.Size(68, 15);
            this.lblSecuencia.TabIndex = 49;
            this.lblSecuencia.Text = "Secuencia:";
            // 
            // lblEstaCajero
            // 
            this.lblEstaCajero.AutoSize = true;
            this.lblEstaCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaCajero.Location = new System.Drawing.Point(17, 95);
            this.lblEstaCajero.Name = "lblEstaCajero";
            this.lblEstaCajero.Size = new System.Drawing.Size(48, 15);
            this.lblEstaCajero.TabIndex = 7;
            this.lblEstaCajero.Text = "Estado:";
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.Enabled = false;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(168, 92);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(151, 24);
            this.cmbEstado.TabIndex = 12;
            // 
            // chkPagaIva
            // 
            this.chkPagaIva.AutoSize = true;
            this.chkPagaIva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPagaIva.Location = new System.Drawing.Point(266, 134);
            this.chkPagaIva.Name = "chkPagaIva";
            this.chkPagaIva.Size = new System.Drawing.Size(73, 19);
            this.chkPagaIva.TabIndex = 15;
            this.chkPagaIva.Text = "Paga Iva";
            this.chkPagaIva.UseVisualStyleBackColor = true;
            // 
            // chkModificable
            // 
            this.chkModificable.AutoSize = true;
            this.chkModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModificable.Location = new System.Drawing.Point(12, 134);
            this.chkModificable.Name = "chkModificable";
            this.chkModificable.Size = new System.Drawing.Size(90, 19);
            this.chkModificable.TabIndex = 13;
            this.chkModificable.Text = "Modificable";
            this.chkModificable.UseVisualStyleBackColor = true;
            // 
            // chkPrecioModificable
            // 
            this.chkPrecioModificable.AutoSize = true;
            this.chkPrecioModificable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrecioModificable.Location = new System.Drawing.Point(117, 134);
            this.chkPrecioModificable.Name = "chkPrecioModificable";
            this.chkPrecioModificable.Size = new System.Drawing.Size(128, 19);
            this.chkPrecioModificable.TabIndex = 14;
            this.chkPrecioModificable.Text = "Precio modificable";
            this.chkPrecioModificable.UseVisualStyleBackColor = true;
            // 
            // grupoGrid
            // 
            this.grupoGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grupoGrid.Controls.Add(this.btnBuscar);
            this.grupoGrid.Controls.Add(this.txtBuscar);
            this.grupoGrid.Controls.Add(this.dgvDatos);
            this.grupoGrid.Location = new System.Drawing.Point(427, 19);
            this.grupoGrid.Name = "grupoGrid";
            this.grupoGrid.Size = new System.Drawing.Size(549, 501);
            this.grupoGrid.TabIndex = 24;
            this.grupoGrid.TabStop = false;
            this.grupoGrid.Text = "Lista de Registros";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(237, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 26);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(15, 20);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 1;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(15, 54);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(517, 432);
            this.dgvDatos.TabIndex = 21;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // grupoOpciones
            // 
            this.grupoOpciones.Controls.Add(this.btnCerrar);
            this.grupoOpciones.Controls.Add(this.btnLimpiar);
            this.grupoOpciones.Controls.Add(this.btnAnular);
            this.grupoOpciones.Controls.Add(this.btnNuevo);
            this.grupoOpciones.Location = new System.Drawing.Point(17, 447);
            this.grupoOpciones.Name = "grupoOpciones";
            this.grupoOpciones.Size = new System.Drawing.Size(349, 73);
            this.grupoOpciones.TabIndex = 23;
            this.grupoOpciones.TabStop = false;
            this.grupoOpciones.Text = "Opciones";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(247, 19);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(70, 39);
            this.btnCerrar.TabIndex = 20;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(171, 19);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 19;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Enabled = false;
            this.btnAnular.ForeColor = System.Drawing.Color.White;
            this.btnAnular.Location = new System.Drawing.Point(95, 19);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(70, 39);
            this.btnAnular.TabIndex = 18;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.Blue;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(19, 19);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(70, 39);
            this.btnNuevo.TabIndex = 17;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.cmbClaseProducto);
            this.grupoDatos.Controls.Add(this.cmbEmpresa);
            this.grupoDatos.Controls.Add(this.lblEmpresa);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.cmbPadre);
            this.grupoDatos.Controls.Add(this.lblCodigo);
            this.grupoDatos.Controls.Add(this.cmbTipoProducto);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.lblDescrCajero);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.lblCodCategoria);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(17, 19);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(404, 183);
            this.grupoDatos.TabIndex = 22;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Datos del Registro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(166, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 76;
            this.label2.Text = "EXTRA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(244, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 75;
            this.label1.Text = "01-99 Max.";
            // 
            // cmbClaseProducto
            // 
            this.cmbClaseProducto.FormattingEnabled = true;
            this.cmbClaseProducto.Location = new System.Drawing.Point(168, 100);
            this.cmbClaseProducto.Name = "cmbClaseProducto";
            this.cmbClaseProducto.Size = new System.Drawing.Size(210, 21);
            this.cmbClaseProducto.TabIndex = 6;
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(168, 31);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(210, 21);
            this.cmbEmpresa.TabIndex = 3;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(17, 32);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(60, 15);
            this.lblEmpresa.TabIndex = 44;
            this.lblEmpresa.Text = "Empresa:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(17, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 15);
            this.label4.TabIndex = 72;
            this.label4.Text = "Clase de Producto";
            // 
            // cmbPadre
            // 
            this.cmbPadre.FormattingEnabled = true;
            this.cmbPadre.Location = new System.Drawing.Point(168, 54);
            this.cmbPadre.Name = "cmbPadre";
            this.cmbPadre.Size = new System.Drawing.Size(209, 21);
            this.cmbPadre.TabIndex = 4;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigo.Location = new System.Drawing.Point(17, 55);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(81, 15);
            this.lblCodigo.TabIndex = 11;
            this.lblCodigo.Text = "Código padre";
            // 
            // cmbTipoProducto
            // 
            this.cmbTipoProducto.FormattingEnabled = true;
            this.cmbTipoProducto.Location = new System.Drawing.Point(168, 77);
            this.cmbTipoProducto.Name = "cmbTipoProducto";
            this.cmbTipoProducto.Size = new System.Drawing.Size(210, 21);
            this.cmbTipoProducto.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(17, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 15);
            this.label3.TabIndex = 71;
            this.label3.Text = "Tipo de Producto";
            // 
            // lblDescrCajero
            // 
            this.lblDescrCajero.AutoSize = true;
            this.lblDescrCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblDescrCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescrCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDescrCajero.Location = new System.Drawing.Point(17, 146);
            this.lblDescrCajero.Name = "lblDescrCajero";
            this.lblDescrCajero.Size = new System.Drawing.Size(75, 15);
            this.lblDescrCajero.TabIndex = 5;
            this.lblDescrCajero.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Location = new System.Drawing.Point(218, 145);
            this.txtDescripcion.MaxLength = 200;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(160, 20);
            this.txtDescripcion.TabIndex = 8;
            this.ttMensaje.SetToolTip(this.txtDescripcion, "Ingrese únicamente el modificador. \r\nE, primer caracter formará parte del código " +
        "del producto. \r\nEjm: Jamón - J01");
            // 
            // lblCodCategoria
            // 
            this.lblCodCategoria.AutoSize = true;
            this.lblCodCategoria.BackColor = System.Drawing.Color.Transparent;
            this.lblCodCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodCategoria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodCategoria.Location = new System.Drawing.Point(17, 124);
            this.lblCodCategoria.Name = "lblCodCategoria";
            this.lblCodCategoria.Size = new System.Drawing.Size(102, 15);
            this.lblCodCategoria.TabIndex = 3;
            this.lblCodCategoria.Text = "Código Categoría";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(168, 123);
            this.txtCodigo.MaxLength = 2;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(63, 20);
            this.txtCodigo.TabIndex = 7;
            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigo_KeyPress);
            // 
            // FModificadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(997, 568);
            this.Controls.Add(this.tabCon_Modificadores);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FModificadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de Modificadores";
            this.Load += new System.EventHandler(this.FModificadores_Load);
            this.tabCon_Modificadores.ResumeLayout(false);
            this.tabPag_Modificadores.ResumeLayout(false);
            this.grupoImpresion.ResumeLayout(false);
            this.grupoImpresion.PerformLayout();
            this.grupoPrecio.ResumeLayout(false);
            this.grupoPrecio.PerformLayout();
            this.grupoGrid.ResumeLayout(false);
            this.grupoGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoOpciones.ResumeLayout(false);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_Modificadores;
        private System.Windows.Forms.TabPage tabPag_Modificadores;
        private System.Windows.Forms.GroupBox grupoGrid;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox grupoOpciones;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label lblSecuencia;
        private System.Windows.Forms.TextBox txtSecuencia;
        private System.Windows.Forms.CheckBox chkPagaIva;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private MisControles.ComboDatos cmbPadre;
        private System.Windows.Forms.CheckBox chkPrecioModificable;
        private System.Windows.Forms.CheckBox chkModificable;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblEstaCajero;
        private System.Windows.Forms.Label lblDescrCajero;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblCodCategoria;
        private System.Windows.Forms.TextBox txtCodigo;
        private MisControles.ComboDatos cmbClaseProducto;
        private System.Windows.Forms.Label label4;
        private MisControles.ComboDatos cmbTipoProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grupoPrecio;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.TextBox txtPrecioMinorista;
        private System.Windows.Forms.Label lblPreCompra;
        private System.Windows.Forms.Label lblPrecioMinorista;
        private System.Windows.Forms.GroupBox grupoImpresion;
        private MisControles.ComboDatos cmbDestinoImpresion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}