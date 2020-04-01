namespace Palatium.Receta
{
    partial class frmAdministraciónDeReceta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdministraciónDeReceta));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvReceta = new System.Windows.Forms.DataGridView();
            this.txtCostoTotal = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnA = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.ttDetalle = new System.Windows.Forms.ToolTip(this.components);
            this.txtNumeroPorciones = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.cmbOrigen = new MisControles.ComboDatos();
            this.cmbTemperaturaDeServicio = new MisControles.ComboDatos();
            this.cmbClasificacion = new MisControles.ComboDatos();
            this.cmbReceta = new MisControles.ComboDatos();
            this.txtCostoUnitario = new System.Windows.Forms.TextBox();
            this.txtPorcentajeDeCosto = new System.Windows.Forms.TextBox();
            this.txtPorcentajeDeUtilidad = new System.Windows.Forms.TextBox();
            this.txtRendimiento = new System.Windows.Forms.TextBox();
            this.txtUtilidadDeServicios = new System.Windows.Forms.TextBox();
            this.txtUtilidadDeGanancias = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.dbAyudaReceta = new Controles.Auxiliares.DB_Ayuda();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.asd = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrecioDeVenta = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ingrediente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rendimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costo_unitario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceta)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox2.Controls.Add(this.dgvReceta);
            this.groupBox2.Controls.Add(this.txtCostoTotal);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.btnA);
            this.groupBox2.Controls.Add(this.btnX);
            this.groupBox2.Location = new System.Drawing.Point(21, 257);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1014, 276);
            this.groupBox2.TabIndex = 129;
            this.groupBox2.TabStop = false;
            // 
            // dgvReceta
            // 
            this.dgvReceta.AllowUserToAddRows = false;
            this.dgvReceta.AllowUserToDeleteRows = false;
            this.dgvReceta.AllowUserToResizeColumns = false;
            this.dgvReceta.AllowUserToResizeRows = false;
            this.dgvReceta.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvReceta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReceta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ingrediente,
            this.unidad,
            this.rendimiento,
            this.cantidad,
            this.costo_unitario,
            this.importe,
            this.id_producto});
            this.dgvReceta.Location = new System.Drawing.Point(27, 12);
            this.dgvReceta.Name = "dgvReceta";
            this.dgvReceta.RowHeadersVisible = false;
            this.dgvReceta.Size = new System.Drawing.Size(962, 223);
            this.dgvReceta.TabIndex = 86;
            this.dgvReceta.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReceta_CellEndEdit);
            this.dgvReceta.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvReceta_EditingControlShowing);
            // 
            // txtCostoTotal
            // 
            this.txtCostoTotal.BackColor = System.Drawing.SystemColors.Window;
            this.txtCostoTotal.Enabled = false;
            this.txtCostoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostoTotal.Location = new System.Drawing.Point(865, 244);
            this.txtCostoTotal.Multiline = true;
            this.txtCostoTotal.Name = "txtCostoTotal";
            this.txtCostoTotal.ReadOnly = true;
            this.txtCostoTotal.Size = new System.Drawing.Size(123, 20);
            this.txtCostoTotal.TabIndex = 84;
            this.txtCostoTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtCostoTotal, "Es la suma de todos los importes.");
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(757, 245);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 19);
            this.label13.TabIndex = 85;
            this.label13.Text = "Costo Total";
            // 
            // btnA
            // 
            this.btnA.Enabled = false;
            this.btnA.Image = global::Palatium.Properties.Resources.anadir;
            this.btnA.Location = new System.Drawing.Point(56, 241);
            this.btnA.Name = "btnA";
            this.btnA.Size = new System.Drawing.Size(23, 23);
            this.btnA.TabIndex = 16;
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.btnA_Click);
            // 
            // btnX
            // 
            this.btnX.Enabled = false;
            this.btnX.Image = global::Palatium.Properties.Resources.menos;
            this.btnX.Location = new System.Drawing.Point(27, 241);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(23, 23);
            this.btnX.TabIndex = 17;
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLimpiar);
            this.groupBox3.Controls.Add(this.btnSalir);
            this.groupBox3.Controls.Add(this.btnAnular);
            this.groupBox3.Controls.Add(this.btnGrabar);
            this.groupBox3.Location = new System.Drawing.Point(740, 533);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(278, 41);
            this.groupBox3.TabIndex = 135;
            this.groupBox3.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(140, 12);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(204, 12);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 23);
            this.btnSalir.TabIndex = 21;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.Image = global::Palatium.Properties.Resources.borrar;
            this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnular.Location = new System.Drawing.Point(76, 12);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(60, 23);
            this.btnAnular.TabIndex = 19;
            this.btnAnular.Text = "Anular";
            this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(6, 12);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(66, 23);
            this.btnGrabar.TabIndex = 18;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // txtNumeroPorciones
            // 
            this.txtNumeroPorciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroPorciones.Location = new System.Drawing.Point(808, 16);
            this.txtNumeroPorciones.Multiline = true;
            this.txtNumeroPorciones.Name = "txtNumeroPorciones";
            this.txtNumeroPorciones.Size = new System.Drawing.Size(73, 21);
            this.txtNumeroPorciones.TabIndex = 4;
            this.txtNumeroPorciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtNumeroPorciones, "Es el número de veces que rinde la receta");
            this.txtNumeroPorciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(136, 17);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(222, 20);
            this.txtDescripcion.TabIndex = 2;
            this.ttDetalle.SetToolTip(this.txtDescripcion, "Señalar el nombre completo de la receta");
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(517, 16);
            this.txtCodigo.MaxLength = 10;
            this.txtCodigo.Multiline = true;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(128, 20);
            this.txtCodigo.TabIndex = 3;
            this.ttDetalle.SetToolTip(this.txtCodigo, "Ingresar un código formado por letras y número. Ejemplo: PD01");
            // 
            // cmbOrigen
            // 
            this.cmbOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOrigen.FormattingEnabled = true;
            this.cmbOrigen.Location = new System.Drawing.Point(136, 70);
            this.cmbOrigen.Name = "cmbOrigen";
            this.cmbOrigen.Size = new System.Drawing.Size(216, 21);
            this.cmbOrigen.TabIndex = 7;
            this.ttDetalle.SetToolTip(this.cmbOrigen, "Se trata del lugar de origen de la receta");
            // 
            // cmbTemperaturaDeServicio
            // 
            this.cmbTemperaturaDeServicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTemperaturaDeServicio.FormattingEnabled = true;
            this.cmbTemperaturaDeServicio.ItemHeight = 13;
            this.cmbTemperaturaDeServicio.Location = new System.Drawing.Point(136, 94);
            this.cmbTemperaturaDeServicio.Name = "cmbTemperaturaDeServicio";
            this.cmbTemperaturaDeServicio.Size = new System.Drawing.Size(216, 21);
            this.cmbTemperaturaDeServicio.TabIndex = 8;
            this.ttDetalle.SetToolTip(this.cmbTemperaturaDeServicio, "Se trata del la temperatura a la cual el platillo llegará a la mesa");
            // 
            // cmbClasificacion
            // 
            this.cmbClasificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClasificacion.FormattingEnabled = true;
            this.cmbClasificacion.Location = new System.Drawing.Point(136, 22);
            this.cmbClasificacion.Name = "cmbClasificacion";
            this.cmbClasificacion.Size = new System.Drawing.Size(216, 21);
            this.cmbClasificacion.TabIndex = 5;
            this.ttDetalle.SetToolTip(this.cmbClasificacion, "Se especifica el tipo de platillo o bebida");
            // 
            // cmbReceta
            // 
            this.cmbReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbReceta.FormattingEnabled = true;
            this.cmbReceta.Location = new System.Drawing.Point(136, 46);
            this.cmbReceta.Name = "cmbReceta";
            this.cmbReceta.Size = new System.Drawing.Size(216, 21);
            this.cmbReceta.TabIndex = 6;
            this.ttDetalle.SetToolTip(this.cmbReceta, resources.GetString("cmbReceta.ToolTip"));
            // 
            // txtCostoUnitario
            // 
            this.txtCostoUnitario.BackColor = System.Drawing.SystemColors.Window;
            this.txtCostoUnitario.Enabled = false;
            this.txtCostoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCostoUnitario.Location = new System.Drawing.Point(160, 50);
            this.txtCostoUnitario.Multiline = true;
            this.txtCostoUnitario.Name = "txtCostoUnitario";
            this.txtCostoUnitario.ReadOnly = true;
            this.txtCostoUnitario.Size = new System.Drawing.Size(123, 20);
            this.txtCostoUnitario.TabIndex = 10;
            this.txtCostoUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtCostoUnitario, "Es el costo total entre el número de porciones");
            this.txtCostoUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCostoUnitario_KeyPress);
            // 
            // txtPorcentajeDeCosto
            // 
            this.txtPorcentajeDeCosto.BackColor = System.Drawing.SystemColors.Window;
            this.txtPorcentajeDeCosto.Enabled = false;
            this.txtPorcentajeDeCosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeDeCosto.Location = new System.Drawing.Point(160, 74);
            this.txtPorcentajeDeCosto.Multiline = true;
            this.txtPorcentajeDeCosto.Name = "txtPorcentajeDeCosto";
            this.txtPorcentajeDeCosto.ReadOnly = true;
            this.txtPorcentajeDeCosto.Size = new System.Drawing.Size(123, 20);
            this.txtPorcentajeDeCosto.TabIndex = 11;
            this.txtPorcentajeDeCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtPorcentajeDeCosto, "Es el costo unitario por 100 dividio para el precio de venta.");
            this.txtPorcentajeDeCosto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentajeDeCosto_KeyPress);
            // 
            // txtPorcentajeDeUtilidad
            // 
            this.txtPorcentajeDeUtilidad.BackColor = System.Drawing.SystemColors.Window;
            this.txtPorcentajeDeUtilidad.Enabled = false;
            this.txtPorcentajeDeUtilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeDeUtilidad.Location = new System.Drawing.Point(160, 98);
            this.txtPorcentajeDeUtilidad.Multiline = true;
            this.txtPorcentajeDeUtilidad.Name = "txtPorcentajeDeUtilidad";
            this.txtPorcentajeDeUtilidad.ReadOnly = true;
            this.txtPorcentajeDeUtilidad.Size = new System.Drawing.Size(123, 20);
            this.txtPorcentajeDeUtilidad.TabIndex = 12;
            this.txtPorcentajeDeUtilidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtPorcentajeDeUtilidad, "Es la utilidad de servicio por 100 dividido para el precio de venta.");
            this.txtPorcentajeDeUtilidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentajeDeUtilidad_KeyPress);
            // 
            // txtRendimiento
            // 
            this.txtRendimiento.BackColor = System.Drawing.SystemColors.Window;
            this.txtRendimiento.Enabled = false;
            this.txtRendimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRendimiento.Location = new System.Drawing.Point(192, 74);
            this.txtRendimiento.Multiline = true;
            this.txtRendimiento.Name = "txtRendimiento";
            this.txtRendimiento.ReadOnly = true;
            this.txtRendimiento.Size = new System.Drawing.Size(123, 21);
            this.txtRendimiento.TabIndex = 15;
            this.txtRendimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtRendimiento, "Es la suma de todos los rendimientos entre \r\nel número de ingredientes.");
            this.txtRendimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRendimiento_KeyPress);
            // 
            // txtUtilidadDeServicios
            // 
            this.txtUtilidadDeServicios.BackColor = System.Drawing.SystemColors.Window;
            this.txtUtilidadDeServicios.Enabled = false;
            this.txtUtilidadDeServicios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidadDeServicios.Location = new System.Drawing.Point(192, 24);
            this.txtUtilidadDeServicios.Multiline = true;
            this.txtUtilidadDeServicios.Name = "txtUtilidadDeServicios";
            this.txtUtilidadDeServicios.ReadOnly = true;
            this.txtUtilidadDeServicios.Size = new System.Drawing.Size(123, 20);
            this.txtUtilidadDeServicios.TabIndex = 13;
            this.txtUtilidadDeServicios.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtUtilidadDeServicios, "Es el aumento del porcentaje que deseamos aumentar en el servicio");
            this.txtUtilidadDeServicios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUtilidadDeServicios_KeyPress);
            // 
            // txtUtilidadDeGanancias
            // 
            this.txtUtilidadDeGanancias.BackColor = System.Drawing.SystemColors.Window;
            this.txtUtilidadDeGanancias.Enabled = false;
            this.txtUtilidadDeGanancias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidadDeGanancias.Location = new System.Drawing.Point(192, 48);
            this.txtUtilidadDeGanancias.Multiline = true;
            this.txtUtilidadDeGanancias.Name = "txtUtilidadDeGanancias";
            this.txtUtilidadDeGanancias.ReadOnly = true;
            this.txtUtilidadDeGanancias.Size = new System.Drawing.Size(123, 20);
            this.txtUtilidadDeGanancias.TabIndex = 14;
            this.txtUtilidadDeGanancias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ttDetalle.SetToolTip(this.txtUtilidadDeGanancias, "Es el precio de venta por el porcentaje de ganancia.");
            this.txtUtilidadDeGanancias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUtilidadDeGanancias_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.dbAyudaReceta);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbEmpresa);
            this.groupBox1.Location = new System.Drawing.Point(18, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1011, 56);
            this.groupBox1.TabIndex = 140;
            this.groupBox1.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(303, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 15);
            this.label14.TabIndex = 126;
            this.label14.Text = "Receta:";
            // 
            // dbAyudaReceta
            // 
            this.dbAyudaReceta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbAyudaReceta.iId = 0;
            this.dbAyudaReceta.Location = new System.Drawing.Point(371, 17);
            this.dbAyudaReceta.Margin = new System.Windows.Forms.Padding(4);
            this.dbAyudaReceta.Name = "dbAyudaReceta";
            this.dbAyudaReceta.sCodigo = null;
            this.dbAyudaReceta.Size = new System.Drawing.Size(616, 26);
            this.dbAyudaReceta.sNombre = null;
            this.dbAyudaReceta.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 28;
            this.label5.Text = "Empresa";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(92, 16);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(166, 21);
            this.cmbEmpresa.TabIndex = 100;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox5.Controls.Add(this.txtCodigo);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.txtDescripcion);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.asd);
            this.groupBox5.Controls.Add(this.txtNumeroPorciones);
            this.groupBox5.Location = new System.Drawing.Point(18, 69);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1011, 50);
            this.groupBox5.TabIndex = 141;
            this.groupBox5.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(378, 18);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 15);
            this.label16.TabIndex = 139;
            this.label16.Text = "Código del Producto:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(8, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(106, 15);
            this.label15.TabIndex = 137;
            this.label15.Text = "Nombre del Plato:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.Image = global::Palatium.Properties.Resources.ok4;
            this.button1.Location = new System.Drawing.Point(917, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 31);
            this.button1.TabIndex = 16;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // asd
            // 
            this.asd.AutoSize = true;
            this.asd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asd.Location = new System.Drawing.Point(693, 19);
            this.asd.Name = "asd";
            this.asd.Size = new System.Drawing.Size(98, 15);
            this.asd.TabIndex = 8;
            this.asd.Text = "Nº de Porciones:";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox6.Controls.Add(this.cmbReceta);
            this.groupBox6.Controls.Add(this.cmbClasificacion);
            this.groupBox6.Controls.Add(this.label1);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.cmbTemperaturaDeServicio);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.cmbOrigen);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Location = new System.Drawing.Point(18, 119);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(371, 133);
            this.groupBox6.TabIndex = 142;
            this.groupBox6.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clasificación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Receta:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Origen:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Temp. de Servicio";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox7.Controls.Add(this.txtPorcentajeDeUtilidad);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.txtPorcentajeDeCosto);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.txtCostoUnitario);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.txtPrecioDeVenta);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Location = new System.Drawing.Point(380, 119);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(311, 133);
            this.groupBox7.TabIndex = 143;
            this.groupBox7.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(16, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 15);
            this.label10.TabIndex = 18;
            this.label10.Text = "% de Utilidad";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "% de Costo";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Costo Unitario";
            // 
            // txtPrecioDeVenta
            // 
            this.txtPrecioDeVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioDeVenta.Location = new System.Drawing.Point(160, 25);
            this.txtPrecioDeVenta.Multiline = true;
            this.txtPrecioDeVenta.Name = "txtPrecioDeVenta";
            this.txtPrecioDeVenta.Size = new System.Drawing.Size(123, 20);
            this.txtPrecioDeVenta.TabIndex = 9;
            this.txtPrecioDeVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecioDeVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioDeVenta_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Precio de Venta";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox8.Controls.Add(this.btnActualizar);
            this.groupBox8.Controls.Add(this.txtUtilidadDeGanancias);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.txtUtilidadDeServicios);
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.txtRendimiento);
            this.groupBox8.Location = new System.Drawing.Point(690, 119);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(339, 133);
            this.groupBox8.TabIndex = 144;
            this.groupBox8.TabStop = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizar.Location = new System.Drawing.Point(168, 97);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(147, 31);
            this.btnActualizar.TabIndex = 145;
            this.btnActualizar.Text = "Actualizar campos";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(21, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 15);
            this.label12.TabIndex = 22;
            this.label12.Text = "Utilidad de Ganancias";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 15);
            this.label11.TabIndex = 20;
            this.label11.Text = "Utilidad de Servicios";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Rendimiento";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBox4.Location = new System.Drawing.Point(3, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1037, 251);
            this.groupBox4.TabIndex = 129;
            this.groupBox4.TabStop = false;
            // 
            // ingrediente
            // 
            this.ingrediente.HeaderText = "INGREDIENTE";
            this.ingrediente.Name = "ingrediente";
            this.ingrediente.ReadOnly = true;
            this.ingrediente.Width = 280;
            // 
            // unidad
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.unidad.DefaultCellStyle = dataGridViewCellStyle16;
            this.unidad.HeaderText = "UNIDAD";
            this.unidad.Name = "unidad";
            this.unidad.ReadOnly = true;
            this.unidad.Width = 125;
            // 
            // rendimiento
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.rendimiento.DefaultCellStyle = dataGridViewCellStyle17;
            this.rendimiento.HeaderText = "% RENDIMIENTO";
            this.rendimiento.Name = "rendimiento";
            this.rendimiento.ReadOnly = true;
            this.rendimiento.Width = 125;
            // 
            // cantidad
            // 
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.cantidad.DefaultCellStyle = dataGridViewCellStyle18;
            this.cantidad.HeaderText = "CANTIDAD";
            this.cantidad.Name = "cantidad";
            // 
            // costo_unitario
            // 
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.costo_unitario.DefaultCellStyle = dataGridViewCellStyle19;
            this.costo_unitario.HeaderText = "COSTO UNITARIO";
            this.costo_unitario.Name = "costo_unitario";
            this.costo_unitario.ReadOnly = true;
            this.costo_unitario.Width = 150;
            // 
            // importe
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.importe.DefaultCellStyle = dataGridViewCellStyle20;
            this.importe.HeaderText = "IMPORTE";
            this.importe.Name = "importe";
            this.importe.ReadOnly = true;
            this.importe.Width = 150;
            // 
            // id_producto
            // 
            this.id_producto.HeaderText = "ID";
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            this.id_producto.Visible = false;
            // 
            // frmAdministraciónDeReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1040, 574);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdministraciónDeReceta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso de Receta";
            this.Load += new System.EventHandler(this.frmAdministraciónDeReceta_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReceta)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip ttDetalle;
        private System.Windows.Forms.TextBox txtCostoTotal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label14;
        private Controles.Auxiliares.DB_Ayuda dbAyudaReceta;
        private System.Windows.Forms.Label label5;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label asd;
        private System.Windows.Forms.TextBox txtNumeroPorciones;
        private System.Windows.Forms.GroupBox groupBox6;
        private MisControles.ComboDatos cmbReceta;
        private MisControles.ComboDatos cmbClasificacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MisControles.ComboDatos cmbTemperaturaDeServicio;
        private System.Windows.Forms.Label label3;
        private MisControles.ComboDatos cmbOrigen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtPorcentajeDeUtilidad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPorcentajeDeCosto;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCostoUnitario;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPrecioDeVenta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.TextBox txtUtilidadDeGanancias;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtUtilidadDeServicios;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRendimiento;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvReceta;
        private System.Windows.Forms.DataGridViewTextBoxColumn ingrediente;
        private System.Windows.Forms.DataGridViewTextBoxColumn unidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn rendimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn costo_unitario;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
    }
}