namespace Palatium.Cancelar_Orden
{
    partial class frmEliminarComanda
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
            this.tRevisar = new System.Windows.Forms.Timer(this.components);
            this.btnCancelar = new DevComponents.DotNetBar.ButtonX();
            this.btnOrdenes = new DevComponents.DotNetBar.ButtonX();
            this.btnBajar = new DevComponents.DotNetBar.ButtonX();
            this.btnFecha = new DevComponents.DotNetBar.ButtonX();
            this.btnSubir = new DevComponents.DotNetBar.ButtonX();
            this.btnBusqueda = new DevComponents.DotNetBar.ButtonX();
            this.btn0 = new DevComponents.DotNetBar.ButtonX();
            this.btnBusquedaOrdenCuenta = new DevComponents.DotNetBar.ButtonX();
            this.txtBusqueda = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnRetroceder = new DevComponents.DotNetBar.ButtonX();
            this.btnCanceladas = new DevComponents.DotNetBar.ButtonX();
            this.btnLlevar = new DevComponents.DotNetBar.ButtonX();
            this.btnMesa = new DevComponents.DotNetBar.ButtonX();
            this.btnDomicilio = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBajarComandas = new DevComponents.DotNetBar.ButtonX();
            this.btnSubirComandas = new DevComponents.DotNetBar.ButtonX();
            this.btnTotalOrdenes = new DevComponents.DotNetBar.ButtonX();
            this.btn3 = new DevComponents.DotNetBar.ButtonX();
            this.btn2 = new DevComponents.DotNetBar.ButtonX();
            this.btn1 = new DevComponents.DotNetBar.ButtonX();
            this.btn6 = new DevComponents.DotNetBar.ButtonX();
            this.btn5 = new DevComponents.DotNetBar.ButtonX();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.btn4 = new DevComponents.DotNetBar.ButtonX();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btn9 = new DevComponents.DotNetBar.ButtonX();
            this.btn8 = new DevComponents.DotNetBar.ButtonX();
            this.btn7 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlOrdenes = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tRevisar
            // 
            this.tRevisar.Enabled = true;
            this.tRevisar.Interval = 60000000;
            // 
            // btnCancelar
            // 
            this.btnCancelar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancelar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(159, 413);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(122, 59);
            this.btnCancelar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancelar.TabIndex = 56;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnOrdenes
            // 
            this.btnOrdenes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOrdenes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOrdenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrdenes.Location = new System.Drawing.Point(14, 413);
            this.btnOrdenes.Name = "btnOrdenes";
            this.btnOrdenes.Size = new System.Drawing.Size(122, 59);
            this.btnOrdenes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOrdenes.TabIndex = 54;
            this.btnOrdenes.Text = "Todas las Órdenes";
            this.btnOrdenes.Click += new System.EventHandler(this.btnOrdenes_Click);
            // 
            // btnBajar
            // 
            this.btnBajar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBajar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBajar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajar.Image = global::Palatium.Properties.Resources.abajo_2;
            this.btnBajar.Location = new System.Drawing.Point(224, 358);
            this.btnBajar.Name = "btnBajar";
            this.btnBajar.Size = new System.Drawing.Size(57, 49);
            this.btnBajar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBajar.TabIndex = 53;
            this.btnBajar.Click += new System.EventHandler(this.btnBajar_Click);
            // 
            // btnFecha
            // 
            this.btnFecha.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFecha.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnFecha.Location = new System.Drawing.Point(77, 358);
            this.btnFecha.Name = "btnFecha";
            this.btnFecha.Size = new System.Drawing.Size(141, 49);
            this.btnFecha.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFecha.TabIndex = 52;
            this.btnFecha.Click += new System.EventHandler(this.btnFecha_Click);
            // 
            // btnSubir
            // 
            this.btnSubir.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubir.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubir.Image = global::Palatium.Properties.Resources.arriba_2;
            this.btnSubir.Location = new System.Drawing.Point(14, 358);
            this.btnSubir.Name = "btnSubir";
            this.btnSubir.Size = new System.Drawing.Size(57, 49);
            this.btnSubir.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubir.TabIndex = 50;
            this.btnSubir.Click += new System.EventHandler(this.btnSubir_Click);
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBusqueda.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBusqueda.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBusqueda.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnBusqueda.Location = new System.Drawing.Point(196, 286);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(85, 70);
            this.btnBusqueda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBusqueda.TabIndex = 49;
            this.btnBusqueda.Text = "Búsqueda";
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // btn0
            // 
            this.btn0.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn0.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn0.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn0.Location = new System.Drawing.Point(105, 286);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(85, 70);
            this.btn0.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn0.TabIndex = 48;
            this.btn0.Text = "0";
            this.btn0.Click += new System.EventHandler(this.btn0_Click);
            // 
            // btnBusquedaOrdenCuenta
            // 
            this.btnBusquedaOrdenCuenta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBusquedaOrdenCuenta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBusquedaOrdenCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBusquedaOrdenCuenta.Image = global::Palatium.Properties.Resources.hash_tag;
            this.btnBusquedaOrdenCuenta.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnBusquedaOrdenCuenta.Location = new System.Drawing.Point(14, 286);
            this.btnBusquedaOrdenCuenta.Name = "btnBusquedaOrdenCuenta";
            this.btnBusquedaOrdenCuenta.Size = new System.Drawing.Size(85, 70);
            this.btnBusquedaOrdenCuenta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBusquedaOrdenCuenta.TabIndex = 47;
            this.btnBusquedaOrdenCuenta.Text = "Por número de orden";
            this.btnBusquedaOrdenCuenta.Click += new System.EventHandler(this.btnBusquedaOrdenCuenta_Click);
            // 
            // txtBusqueda
            // 
            // 
            // 
            // 
            this.txtBusqueda.Border.Class = "TextBoxBorder";
            this.txtBusqueda.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(14, 24);
            this.txtBusqueda.MaxLength = 10;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(176, 38);
            this.txtBusqueda.TabIndex = 46;
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // btnRetroceder
            // 
            this.btnRetroceder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRetroceder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRetroceder.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btnRetroceder.Image = global::Palatium.Properties.Resources.borrar_caracteres;
            this.btnRetroceder.Location = new System.Drawing.Point(196, 22);
            this.btnRetroceder.Name = "btnRetroceder";
            this.btnRetroceder.Size = new System.Drawing.Size(85, 42);
            this.btnRetroceder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRetroceder.TabIndex = 45;
            this.btnRetroceder.Click += new System.EventHandler(this.btnRetroceder_Click);
            // 
            // btnCanceladas
            // 
            this.btnCanceladas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCanceladas.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCanceladas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanceladas.Location = new System.Drawing.Point(577, 79);
            this.btnCanceladas.Name = "btnCanceladas";
            this.btnCanceladas.Size = new System.Drawing.Size(153, 57);
            this.btnCanceladas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCanceladas.TabIndex = 61;
            this.btnCanceladas.Text = "Cliente\r\nEmpresarial";
            this.btnCanceladas.Click += new System.EventHandler(this.btnCanceladas_Click);
            // 
            // btnLlevar
            // 
            this.btnLlevar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLlevar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLlevar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLlevar.Location = new System.Drawing.Point(233, 79);
            this.btnLlevar.Name = "btnLlevar";
            this.btnLlevar.Size = new System.Drawing.Size(153, 57);
            this.btnLlevar.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLlevar.TabIndex = 60;
            this.btnLlevar.Text = "Llevar";
            this.btnLlevar.Click += new System.EventHandler(this.btnLlevar_Click);
            // 
            // btnMesa
            // 
            this.btnMesa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMesa.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesa.Location = new System.Drawing.Point(61, 79);
            this.btnMesa.Name = "btnMesa";
            this.btnMesa.Size = new System.Drawing.Size(153, 57);
            this.btnMesa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMesa.TabIndex = 59;
            this.btnMesa.Text = "Mesa";
            this.btnMesa.Click += new System.EventHandler(this.btnMesa_Click);
            // 
            // btnDomicilio
            // 
            this.btnDomicilio.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDomicilio.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDomicilio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDomicilio.Location = new System.Drawing.Point(405, 79);
            this.btnDomicilio.Name = "btnDomicilio";
            this.btnDomicilio.Size = new System.Drawing.Size(153, 57);
            this.btnDomicilio.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDomicilio.TabIndex = 58;
            this.btnDomicilio.Text = "Venta Express";
            this.btnDomicilio.Click += new System.EventHandler(this.btnDomicilio_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.btnBajarComandas);
            this.groupBox1.Controls.Add(this.btnDomicilio);
            this.groupBox1.Controls.Add(this.btnSubirComandas);
            this.groupBox1.Controls.Add(this.btnCanceladas);
            this.groupBox1.Controls.Add(this.btnLlevar);
            this.groupBox1.Controls.Add(this.btnMesa);
            this.groupBox1.Controls.Add(this.btnTotalOrdenes);
            this.groupBox1.Location = new System.Drawing.Point(11, 442);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(812, 146);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // btnBajarComandas
            // 
            this.btnBajarComandas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBajarComandas.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBajarComandas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajarComandas.Image = global::Palatium.Properties.Resources.abajo_2;
            this.btnBajarComandas.Location = new System.Drawing.Point(529, 16);
            this.btnBajarComandas.Name = "btnBajarComandas";
            this.btnBajarComandas.Size = new System.Drawing.Size(57, 57);
            this.btnBajarComandas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBajarComandas.TabIndex = 55;
            this.btnBajarComandas.Click += new System.EventHandler(this.btnBajarComandas_Click);
            // 
            // btnSubirComandas
            // 
            this.btnSubirComandas.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubirComandas.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSubirComandas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirComandas.Image = global::Palatium.Properties.Resources.arriba_2;
            this.btnSubirComandas.Location = new System.Drawing.Point(206, 16);
            this.btnSubirComandas.Name = "btnSubirComandas";
            this.btnSubirComandas.Size = new System.Drawing.Size(57, 57);
            this.btnSubirComandas.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubirComandas.TabIndex = 54;
            this.btnSubirComandas.Click += new System.EventHandler(this.btnSubirComandas_Click);
            // 
            // btnTotalOrdenes
            // 
            this.btnTotalOrdenes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTotalOrdenes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTotalOrdenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTotalOrdenes.Location = new System.Drawing.Point(269, 16);
            this.btnTotalOrdenes.Name = "btnTotalOrdenes";
            this.btnTotalOrdenes.Size = new System.Drawing.Size(254, 57);
            this.btnTotalOrdenes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTotalOrdenes.TabIndex = 57;
            this.btnTotalOrdenes.Click += new System.EventHandler(this.btnTotalOrdenes_Click);
            // 
            // btn3
            // 
            this.btn3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn3.Location = new System.Drawing.Point(196, 214);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(85, 70);
            this.btn3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn3.TabIndex = 44;
            this.btn3.Text = "3";
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // btn2
            // 
            this.btn2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn2.Location = new System.Drawing.Point(105, 214);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(85, 70);
            this.btn2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn2.TabIndex = 43;
            this.btn2.Text = "2";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn1
            // 
            this.btn1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn1.Location = new System.Drawing.Point(14, 214);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(85, 70);
            this.btn1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn1.TabIndex = 42;
            this.btn1.Text = "1";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn6
            // 
            this.btn6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn6.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn6.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn6.Location = new System.Drawing.Point(196, 142);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(85, 70);
            this.btn6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn6.TabIndex = 41;
            this.btn6.Text = "6";
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btn5
            // 
            this.btn5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn5.Location = new System.Drawing.Point(105, 142);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(85, 70);
            this.btn5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn5.TabIndex = 40;
            this.btn5.Text = "5";
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(19, 34);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(257, 28);
            this.cmbLocalidades.TabIndex = 0;
            this.cmbLocalidades.SelectedIndexChanged += new System.EventHandler(this.cmbLocalidades_SelectedIndexChanged);
            // 
            // btn4
            // 
            this.btn4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn4.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn4.Location = new System.Drawing.Point(14, 142);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(85, 70);
            this.btn4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn4.TabIndex = 39;
            this.btn4.Text = "4";
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn9
            // 
            this.btn9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn9.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn9.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn9.Location = new System.Drawing.Point(196, 70);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(85, 70);
            this.btn9.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn9.TabIndex = 38;
            this.btn9.Text = "9";
            this.btn9.Click += new System.EventHandler(this.btn9_Click);
            // 
            // btn8
            // 
            this.btn8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn8.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn8.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn8.Location = new System.Drawing.Point(105, 70);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(85, 70);
            this.btn8.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn8.TabIndex = 37;
            this.btn8.Text = "8";
            this.btn8.Click += new System.EventHandler(this.btn8_Click);
            // 
            // btn7
            // 
            this.btn7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn7.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn7.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.875F, System.Drawing.FontStyle.Bold);
            this.btn7.Location = new System.Drawing.Point(14, 70);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(85, 70);
            this.btn7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn7.TabIndex = 36;
            this.btn7.Text = "7";
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox3.Controls.Add(this.cmbLocalidades);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(832, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(297, 84);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Localidades";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox2.Controls.Add(this.btnCancelar);
            this.groupBox2.Controls.Add(this.btnOrdenes);
            this.groupBox2.Controls.Add(this.btnBajar);
            this.groupBox2.Controls.Add(this.btnFecha);
            this.groupBox2.Controls.Add(this.btnSubir);
            this.groupBox2.Controls.Add(this.btnBusqueda);
            this.groupBox2.Controls.Add(this.btn0);
            this.groupBox2.Controls.Add(this.btnBusquedaOrdenCuenta);
            this.groupBox2.Controls.Add(this.txtBusqueda);
            this.groupBox2.Controls.Add(this.btnRetroceder);
            this.groupBox2.Controls.Add(this.btn3);
            this.groupBox2.Controls.Add(this.btn2);
            this.groupBox2.Controls.Add(this.btn1);
            this.groupBox2.Controls.Add(this.btn6);
            this.groupBox2.Controls.Add(this.btn5);
            this.groupBox2.Controls.Add(this.btn4);
            this.groupBox2.Controls.Add(this.btn9);
            this.groupBox2.Controls.Add(this.btn8);
            this.groupBox2.Controls.Add(this.btn7);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(832, 101);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(297, 488);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Búsqueda rápida";
            // 
            // pnlOrdenes
            // 
            this.pnlOrdenes.AutoScroll = true;
            this.pnlOrdenes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pnlOrdenes.Location = new System.Drawing.Point(23, 16);
            this.pnlOrdenes.Margin = new System.Windows.Forms.Padding(2);
            this.pnlOrdenes.Name = "pnlOrdenes";
            this.pnlOrdenes.Size = new System.Drawing.Size(800, 421);
            this.pnlOrdenes.TabIndex = 65;
            // 
            // frmEliminarComanda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1140, 598);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pnlOrdenes);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEliminarComanda";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eliminar Comanda";
            this.Load += new System.EventHandler(this.frmEliminarComanda_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEliminarComanda_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tRevisar;
        private DevComponents.DotNetBar.ButtonX btnCancelar;
        private DevComponents.DotNetBar.ButtonX btnOrdenes;
        private DevComponents.DotNetBar.ButtonX btnBajar;
        private DevComponents.DotNetBar.ButtonX btnFecha;
        private DevComponents.DotNetBar.ButtonX btnSubir;
        private DevComponents.DotNetBar.ButtonX btnBusqueda;
        private DevComponents.DotNetBar.ButtonX btn0;
        private DevComponents.DotNetBar.ButtonX btnBusquedaOrdenCuenta;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBusqueda;
        private DevComponents.DotNetBar.ButtonX btnRetroceder;
        private DevComponents.DotNetBar.ButtonX btnCanceladas;
        private DevComponents.DotNetBar.ButtonX btnLlevar;
        private DevComponents.DotNetBar.ButtonX btnMesa;
        private DevComponents.DotNetBar.ButtonX btnDomicilio;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnTotalOrdenes;
        private DevComponents.DotNetBar.ButtonX btn3;
        private DevComponents.DotNetBar.ButtonX btn2;
        private DevComponents.DotNetBar.ButtonX btn1;
        private DevComponents.DotNetBar.ButtonX btn6;
        private DevComponents.DotNetBar.ButtonX btn5;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private DevComponents.DotNetBar.ButtonX btn4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevComponents.DotNetBar.ButtonX btn9;
        private DevComponents.DotNetBar.ButtonX btn8;
        private DevComponents.DotNetBar.ButtonX btn7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlOrdenes;
        private DevComponents.DotNetBar.ButtonX btnBajarComandas;
        private DevComponents.DotNetBar.ButtonX btnSubirComandas;
    }
}