namespace Palatium.Facturador
{
    partial class frmEditarDatosClienteFactura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditarDatosClienteFactura));
            this.txtReporte = new System.Windows.Forms.TextBox();
            this.rbdFactura = new System.Windows.Forms.RadioButton();
            this.rbdTicket = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.rbdNotaVenta = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grupoCliente = new System.Windows.Forms.GroupBox();
            this.btnCorreoElectronicoDefault = new System.Windows.Forms.Button();
            this.chkPasaporte = new System.Windows.Forms.CheckBox();
            this.btnEditar = new System.Windows.Forms.LinkLabel();
            this.btnBuscarCliente = new System.Windows.Forms.Button();
            this.btnConsumidorFinal = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.chkEditar = new System.Windows.Forms.CheckBox();
            this.grupoBotones = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.btnOcultar = new System.Windows.Forms.Button();
            this.btnMostrar = new System.Windows.Forms.Button();
            this.grupoDatos.SuspendLayout();
            this.grupoCliente.SuspendLayout();
            this.grupoBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReporte
            // 
            this.txtReporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReporte.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.Location = new System.Drawing.Point(675, 12);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ReadOnly = true;
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReporte.Size = new System.Drawing.Size(348, 542);
            this.txtReporte.TabIndex = 6;
            // 
            // rbdFactura
            // 
            this.rbdFactura.AutoSize = true;
            this.rbdFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.rbdFactura.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdFactura.Location = new System.Drawing.Point(209, 36);
            this.rbdFactura.Name = "rbdFactura";
            this.rbdFactura.Size = new System.Drawing.Size(121, 24);
            this.rbdFactura.TabIndex = 33;
            this.rbdFactura.Text = "Por Factura";
            this.rbdFactura.UseVisualStyleBackColor = true;
            this.rbdFactura.CheckedChanged += new System.EventHandler(this.rbdFactura_CheckedChanged);
            // 
            // rbdTicket
            // 
            this.rbdTicket.AutoSize = true;
            this.rbdTicket.Checked = true;
            this.rbdTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.rbdTicket.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdTicket.Location = new System.Drawing.Point(18, 36);
            this.rbdTicket.Name = "rbdTicket";
            this.rbdTicket.Size = new System.Drawing.Size(156, 24);
            this.rbdTicket.TabIndex = 32;
            this.rbdTicket.TabStop = true;
            this.rbdTicket.Text = "Por N° de Orden";
            this.rbdTicket.UseVisualStyleBackColor = true;
            this.rbdTicket.CheckedChanged += new System.EventHandler(this.rbdTicket_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(14, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "Ingrese Número:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(162, 79);
            this.txtBuscar.MaxLength = 13;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(203, 26);
            this.txtBuscar.TabIndex = 29;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.rbdNotaVenta);
            this.grupoDatos.Controls.Add(this.rbdFactura);
            this.grupoDatos.Controls.Add(this.rbdTicket);
            this.grupoDatos.Controls.Add(this.btnBuscar);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtBuscar);
            this.grupoDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoDatos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.grupoDatos.Location = new System.Drawing.Point(21, 12);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(556, 139);
            this.grupoDatos.TabIndex = 34;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Búsqueda por Número de Ticket";
            // 
            // rbdNotaVenta
            // 
            this.rbdNotaVenta.AutoSize = true;
            this.rbdNotaVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.rbdNotaVenta.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdNotaVenta.Location = new System.Drawing.Point(360, 36);
            this.rbdNotaVenta.Name = "rbdNotaVenta";
            this.rbdNotaVenta.Size = new System.Drawing.Size(191, 24);
            this.rbdNotaVenta.TabIndex = 34;
            this.rbdNotaVenta.Text = "Por Nota de Entrega";
            this.rbdNotaVenta.UseVisualStyleBackColor = true;
            this.rbdNotaVenta.CheckedChanged += new System.EventHandler(this.rbdNotaVenta_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBuscar.Location = new System.Drawing.Point(395, 71);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(49, 44);
            this.btnBuscar.TabIndex = 31;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // grupoCliente
            // 
            this.grupoCliente.Controls.Add(this.btnCorreoElectronicoDefault);
            this.grupoCliente.Controls.Add(this.chkPasaporte);
            this.grupoCliente.Controls.Add(this.btnEditar);
            this.grupoCliente.Controls.Add(this.btnBuscarCliente);
            this.grupoCliente.Controls.Add(this.btnConsumidorFinal);
            this.grupoCliente.Controls.Add(this.label8);
            this.grupoCliente.Controls.Add(this.txtApellidos);
            this.grupoCliente.Controls.Add(this.label6);
            this.grupoCliente.Controls.Add(this.label5);
            this.grupoCliente.Controls.Add(this.label4);
            this.grupoCliente.Controls.Add(this.label1);
            this.grupoCliente.Controls.Add(this.label2);
            this.grupoCliente.Controls.Add(this.txtMail);
            this.grupoCliente.Controls.Add(this.txtDireccion);
            this.grupoCliente.Controls.Add(this.txtTelefono);
            this.grupoCliente.Controls.Add(this.txtIdentificacion);
            this.grupoCliente.Controls.Add(this.txtNombres);
            this.grupoCliente.Enabled = false;
            this.grupoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoCliente.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.grupoCliente.Location = new System.Drawing.Point(21, 172);
            this.grupoCliente.Name = "grupoCliente";
            this.grupoCliente.Size = new System.Drawing.Size(631, 264);
            this.grupoCliente.TabIndex = 124;
            this.grupoCliente.TabStop = false;
            // 
            // btnCorreoElectronicoDefault
            // 
            this.btnCorreoElectronicoDefault.AutoSize = true;
            this.btnCorreoElectronicoDefault.FlatAppearance.BorderSize = 0;
            this.btnCorreoElectronicoDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorreoElectronicoDefault.ForeColor = System.Drawing.Color.Transparent;
            this.btnCorreoElectronicoDefault.Image = global::Palatium.Properties.Resources.mail_default;
            this.btnCorreoElectronicoDefault.Location = new System.Drawing.Point(574, 218);
            this.btnCorreoElectronicoDefault.Name = "btnCorreoElectronicoDefault";
            this.btnCorreoElectronicoDefault.Size = new System.Drawing.Size(36, 33);
            this.btnCorreoElectronicoDefault.TabIndex = 170;
            this.ttMensaje.SetToolTip(this.btnCorreoElectronicoDefault, "Clic aquí para añadir el correo electrónico de la empresa al cliente, solo para f" +
        "acturación.");
            this.btnCorreoElectronicoDefault.UseVisualStyleBackColor = true;
            this.btnCorreoElectronicoDefault.Click += new System.EventHandler(this.btnCorreoElectronicoDefault_Click);
            // 
            // chkPasaporte
            // 
            this.chkPasaporte.AutoSize = true;
            this.chkPasaporte.Location = new System.Drawing.Point(140, 32);
            this.chkPasaporte.Name = "chkPasaporte";
            this.chkPasaporte.Size = new System.Drawing.Size(192, 24);
            this.chkPasaporte.TabIndex = 159;
            this.chkPasaporte.Text = "Consultar Pasaporte";
            this.chkPasaporte.UseVisualStyleBackColor = true;
            this.chkPasaporte.CheckedChanged += new System.EventHandler(this.chkPasaporte_CheckedChanged);
            // 
            // btnEditar
            // 
            this.btnEditar.AutoSize = true;
            this.btnEditar.LinkColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(429, 64);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(110, 20);
            this.btnEditar.TabIndex = 6;
            this.btnEditar.TabStop = true;
            this.btnEditar.Text = "Editar Datos";
            this.btnEditar.Visible = false;
            this.btnEditar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnEditar_LinkClicked);
            // 
            // btnBuscarCliente
            // 
            this.btnBuscarCliente.Image = global::Palatium.Properties.Resources.buscar_ico;
            this.btnBuscarCliente.Location = new System.Drawing.Point(333, 62);
            this.btnBuscarCliente.Name = "btnBuscarCliente";
            this.btnBuscarCliente.Size = new System.Drawing.Size(29, 26);
            this.btnBuscarCliente.TabIndex = 1;
            this.btnBuscarCliente.UseVisualStyleBackColor = true;
            this.btnBuscarCliente.Click += new System.EventHandler(this.btnBuscarCliente_Click);
            // 
            // btnConsumidorFinal
            // 
            this.btnConsumidorFinal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.btnConsumidorFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsumidorFinal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConsumidorFinal.Location = new System.Drawing.Point(368, 62);
            this.btnConsumidorFinal.Name = "btnConsumidorFinal";
            this.btnConsumidorFinal.Size = new System.Drawing.Size(52, 26);
            this.btnConsumidorFinal.TabIndex = 2;
            this.btnConsumidorFinal.Text = "C.F.";
            this.btnConsumidorFinal.UseVisualStyleBackColor = false;
            this.btnConsumidorFinal.Click += new System.EventHandler(this.btnConsumidorFinal_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(14, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 40);
            this.label8.TabIndex = 16;
            this.label8.Text = "Apellidos /\r\nRazón Social:";
            // 
            // txtApellidos
            // 
            this.txtApellidos.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApellidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidos.Location = new System.Drawing.Point(140, 90);
            this.txtApellidos.MaxLength = 100;
            this.txtApellidos.Multiline = true;
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.ReadOnly = true;
            this.txtApellidos.Size = new System.Drawing.Size(467, 45);
            this.txtApellidos.TabIndex = 158;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(12, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Teléfono:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(14, 224);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Email:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(14, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Dirección:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(14, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Identificación:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(14, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nombres:";
            // 
            // txtMail
            // 
            this.txtMail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMail.Location = new System.Drawing.Point(140, 221);
            this.txtMail.MaxLength = 100;
            this.txtMail.Name = "txtMail";
            this.txtMail.ReadOnly = true;
            this.txtMail.Size = new System.Drawing.Size(426, 26);
            this.txtMail.TabIndex = 11;
            // 
            // txtDireccion
            // 
            this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(140, 193);
            this.txtDireccion.MaxLength = 100;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ReadOnly = true;
            this.txtDireccion.Size = new System.Drawing.Size(467, 26);
            this.txtDireccion.TabIndex = 10;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(140, 165);
            this.txtTelefono.MaxLength = 15;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.ReadOnly = true;
            this.txtTelefono.Size = new System.Drawing.Size(165, 26);
            this.txtTelefono.TabIndex = 9;
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificacion.Location = new System.Drawing.Point(140, 62);
            this.txtIdentificacion.MaxLength = 13;
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.Size = new System.Drawing.Size(187, 26);
            this.txtIdentificacion.TabIndex = 0;
            this.txtIdentificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIdentificacion_KeyPress);
            // 
            // txtNombres
            // 
            this.txtNombres.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombres.Location = new System.Drawing.Point(140, 137);
            this.txtNombres.MaxLength = 100;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.ReadOnly = true;
            this.txtNombres.Size = new System.Drawing.Size(467, 26);
            this.txtNombres.TabIndex = 7;
            // 
            // chkEditar
            // 
            this.chkEditar.AutoSize = true;
            this.chkEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.chkEditar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.chkEditar.Location = new System.Drawing.Point(35, 169);
            this.chkEditar.Name = "chkEditar";
            this.chkEditar.Size = new System.Drawing.Size(157, 24);
            this.chkEditar.TabIndex = 160;
            this.chkEditar.Text = "Habilitar edición";
            this.chkEditar.UseVisualStyleBackColor = true;
            this.chkEditar.CheckedChanged += new System.EventHandler(this.chkEditar_CheckedChanged);
            // 
            // grupoBotones
            // 
            this.grupoBotones.Controls.Add(this.btnSalir);
            this.grupoBotones.Controls.Add(this.btnImprimir);
            this.grupoBotones.Controls.Add(this.btnLimpiar);
            this.grupoBotones.Controls.Add(this.btnGuardar);
            this.grupoBotones.Location = new System.Drawing.Point(180, 446);
            this.grupoBotones.Name = "grupoBotones";
            this.grupoBotones.Size = new System.Drawing.Size(473, 108);
            this.grupoBotones.TabIndex = 165;
            this.grupoBotones.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Navy;
            this.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 2;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.Location = new System.Drawing.Point(352, 17);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(99, 83);
            this.btnSalir.TabIndex = 164;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnSalir_MouseEnter);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Navy;
            this.btnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.FlatAppearance.BorderSize = 2;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnImprimir.Image = global::Palatium.Properties.Resources.impresora_icono;
            this.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnImprimir.Location = new System.Drawing.Point(240, 17);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(99, 83);
            this.btnImprimir.TabIndex = 163;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            this.btnImprimir.MouseEnter += new System.EventHandler(this.btnImprimir_MouseEnter);
            this.btnImprimir.MouseLeave += new System.EventHandler(this.btnImprimir_MouseLeave);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Navy;
            this.btnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiar.FlatAppearance.BorderSize = 2;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.icono_limpiar;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLimpiar.Location = new System.Drawing.Point(128, 17);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(99, 83);
            this.btnLimpiar.TabIndex = 162;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.btnLimpiar.MouseEnter += new System.EventHandler(this.btnLimpiar_MouseEnter);
            this.btnLimpiar.MouseLeave += new System.EventHandler(this.btnLimpiar_MouseLeave);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Navy;
            this.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FlatAppearance.BorderSize = 2;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.Location = new System.Drawing.Point(16, 17);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(99, 83);
            this.btnGuardar.TabIndex = 161;
            this.btnGuardar.Text = "Aceptar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnGuardar.MouseEnter += new System.EventHandler(this.btnGuardar_MouseEnter);
            this.btnGuardar.MouseLeave += new System.EventHandler(this.btnGuardar_MouseLeave);
            // 
            // btnOcultar
            // 
            this.btnOcultar.FlatAppearance.BorderSize = 0;
            this.btnOcultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOcultar.ForeColor = System.Drawing.Color.Transparent;
            this.btnOcultar.Image = global::Palatium.Properties.Resources.icono_retroceder;
            this.btnOcultar.Location = new System.Drawing.Point(591, 107);
            this.btnOcultar.Name = "btnOcultar";
            this.btnOcultar.Size = new System.Drawing.Size(61, 59);
            this.btnOcultar.TabIndex = 167;
            this.ttMensaje.SetToolTip(this.btnOcultar, "Clic aquí para ocultar la vista previa del comprobante");
            this.btnOcultar.UseVisualStyleBackColor = true;
            this.btnOcultar.Visible = false;
            this.btnOcultar.Click += new System.EventHandler(this.btnOcultar_Click);
            // 
            // btnMostrar
            // 
            this.btnMostrar.FlatAppearance.BorderSize = 0;
            this.btnMostrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnMostrar.Image = global::Palatium.Properties.Resources.icono_adelantar;
            this.btnMostrar.Location = new System.Drawing.Point(591, 107);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(61, 59);
            this.btnMostrar.TabIndex = 166;
            this.ttMensaje.SetToolTip(this.btnMostrar, "Clic aquí para mostrar la vista previa del comprobante");
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.Visible = false;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // frmEditarDatosClienteFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(668, 573);
            this.Controls.Add(this.btnOcultar);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.grupoBotones);
            this.Controls.Add(this.chkEditar);
            this.Controls.Add(this.grupoCliente);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.txtReporte);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEditarDatosClienteFactura";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo para editar datos en la factura";
            this.Load += new System.EventHandler(this.frmEditarDatosClienteFactura_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEditarDatosClienteFactura_KeyDown);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoCliente.ResumeLayout(false);
            this.grupoCliente.PerformLayout();
            this.grupoBotones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReporte;
        private System.Windows.Forms.RadioButton rbdFactura;
        private System.Windows.Forms.RadioButton rbdTicket;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.GroupBox grupoCliente;
        private System.Windows.Forms.CheckBox chkPasaporte;
        private System.Windows.Forms.LinkLabel btnEditar;
        private System.Windows.Forms.Button btnBuscarCliente;
        private System.Windows.Forms.Button btnConsumidorFinal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.CheckBox chkEditar;
        internal System.Windows.Forms.Button btnGuardar;
        internal System.Windows.Forms.Button btnLimpiar;
        internal System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.GroupBox grupoBotones;
        internal System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.RadioButton rbdNotaVenta;
        private System.Windows.Forms.Button btnMostrar;
        private System.Windows.Forms.Button btnOcultar;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnCorreoElectronicoDefault;
    }
}