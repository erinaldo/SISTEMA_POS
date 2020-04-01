namespace Palatium.Domicilios
{
    partial class frmNumeroTelefono
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
            this.btnPorTelefono = new System.Windows.Forms.Button();
            this.btnPorIdentificacion = new System.Windows.Forms.Button();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroTelefono = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtReferencia = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCalleSecundaria = new System.Windows.Forms.TextBox();
            this.txtNumeracionVivienda = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtCallePrincipal = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSector = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtFechaUltimaOrden = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtClienteDesde = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtNumeroOrdenes = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnEditarDatos = new System.Windows.Forms.Button();
            this.btnTerminar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPorTelefono
            // 
            this.btnPorTelefono.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnPorTelefono.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnPorTelefono.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnPorTelefono.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPorTelefono.Location = new System.Drawing.Point(18, 19);
            this.btnPorTelefono.Name = "btnPorTelefono";
            this.btnPorTelefono.Size = new System.Drawing.Size(215, 40);
            this.btnPorTelefono.TabIndex = 0;
            this.btnPorTelefono.Text = "Por Número Telefónico";
            this.ttMensaje.SetToolTip(this.btnPorTelefono, "Clic aquí para buscar por Número Telefónico");
            this.btnPorTelefono.UseVisualStyleBackColor = false;
            this.btnPorTelefono.Click += new System.EventHandler(this.btnPorTelefono_Click);
            // 
            // btnPorIdentificacion
            // 
            this.btnPorIdentificacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPorIdentificacion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnPorIdentificacion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnPorIdentificacion.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPorIdentificacion.Location = new System.Drawing.Point(233, 19);
            this.btnPorIdentificacion.Name = "btnPorIdentificacion";
            this.btnPorIdentificacion.Size = new System.Drawing.Size(215, 40);
            this.btnPorIdentificacion.TabIndex = 1;
            this.btnPorIdentificacion.Text = "Por Identificación";
            this.ttMensaje.SetToolTip(this.btnPorIdentificacion, "Clic aquí para buscar por Número de Identificación");
            this.btnPorIdentificacion.UseVisualStyleBackColor = false;
            this.btnPorIdentificacion.Click += new System.EventHandler(this.btnPorIdentificacion_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Maiandra GD", 18.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(453, 20);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(231, 37);
            this.txtBusqueda.TabIndex = 2;
            this.txtBusqueda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnBuscar.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(690, 19);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(116, 40);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPorTelefono);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnPorIdentificacion);
            this.groupBox1.Controls.Add(this.txtBusqueda);
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(836, 78);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMail);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNumeroTelefono);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.txtRazonSocial);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtIdentificacion);
            this.groupBox2.Location = new System.Drawing.Point(12, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(836, 132);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // txtMail
            // 
            this.txtMail.BackColor = System.Drawing.SystemColors.Info;
            this.txtMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMail.Location = new System.Drawing.Point(169, 83);
            this.txtMail.Margin = new System.Windows.Forms.Padding(2);
            this.txtMail.Name = "txtMail";
            this.txtMail.ReadOnly = true;
            this.txtMail.Size = new System.Drawing.Size(637, 31);
            this.txtMail.TabIndex = 31;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(18, 86);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 26);
            this.label20.TabIndex = 30;
            this.label20.Text = "E - Mail:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(465, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 26);
            this.label1.TabIndex = 28;
            this.label1.Text = "# de Teléfono:";
            // 
            // txtNumeroTelefono
            // 
            this.txtNumeroTelefono.BackColor = System.Drawing.SystemColors.Info;
            this.txtNumeroTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroTelefono.Location = new System.Drawing.Point(618, 48);
            this.txtNumeroTelefono.Margin = new System.Windows.Forms.Padding(2);
            this.txtNumeroTelefono.Name = "txtNumeroTelefono";
            this.txtNumeroTelefono.ReadOnly = true;
            this.txtNumeroTelefono.Size = new System.Drawing.Size(188, 31);
            this.txtNumeroTelefono.TabIndex = 29;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(18, 18);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(147, 26);
            this.label23.TabIndex = 27;
            this.label23.Text = "Razón Social:";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.BackColor = System.Drawing.SystemColors.Info;
            this.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazonSocial.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(169, 13);
            this.txtRazonSocial.Margin = new System.Windows.Forms.Padding(2);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.ReadOnly = true;
            this.txtRazonSocial.Size = new System.Drawing.Size(637, 31);
            this.txtRazonSocial.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(18, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "Identificación:";
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.BackColor = System.Drawing.SystemColors.Info;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificacion.Location = new System.Drawing.Point(169, 48);
            this.txtIdentificacion.Margin = new System.Windows.Forms.Padding(2);
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.ReadOnly = true;
            this.txtIdentificacion.Size = new System.Drawing.Size(256, 31);
            this.txtIdentificacion.TabIndex = 10;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtReferencia);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtCalleSecundaria);
            this.groupBox3.Controls.Add(this.txtNumeracionVivienda);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtCallePrincipal);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtSector);
            this.groupBox3.Location = new System.Drawing.Point(12, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(836, 202);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // txtReferencia
            // 
            this.txtReferencia.BackColor = System.Drawing.SystemColors.Info;
            this.txtReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.txtReferencia.Location = new System.Drawing.Point(169, 122);
            this.txtReferencia.Margin = new System.Windows.Forms.Padding(2);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.ReadOnly = true;
            this.txtReferencia.Size = new System.Drawing.Size(637, 62);
            this.txtReferencia.TabIndex = 31;
            this.txtReferencia.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(18, 125);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(128, 52);
            this.label12.TabIndex = 30;
            this.label12.Text = "Referencias\r\nde entrega:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(331, 90);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 26);
            this.label11.TabIndex = 28;
            this.label11.Text = "Intersección:";
            // 
            // txtCalleSecundaria
            // 
            this.txtCalleSecundaria.BackColor = System.Drawing.SystemColors.Info;
            this.txtCalleSecundaria.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCalleSecundaria.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalleSecundaria.Location = new System.Drawing.Point(470, 87);
            this.txtCalleSecundaria.Margin = new System.Windows.Forms.Padding(2);
            this.txtCalleSecundaria.Name = "txtCalleSecundaria";
            this.txtCalleSecundaria.ReadOnly = true;
            this.txtCalleSecundaria.Size = new System.Drawing.Size(336, 31);
            this.txtCalleSecundaria.TabIndex = 29;
            // 
            // txtNumeracionVivienda
            // 
            this.txtNumeracionVivienda.BackColor = System.Drawing.SystemColors.Info;
            this.txtNumeracionVivienda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeracionVivienda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeracionVivienda.Location = new System.Drawing.Point(169, 87);
            this.txtNumeracionVivienda.Margin = new System.Windows.Forms.Padding(2);
            this.txtNumeracionVivienda.Name = "txtNumeracionVivienda";
            this.txtNumeracionVivienda.ReadOnly = true;
            this.txtNumeracionVivienda.Size = new System.Drawing.Size(140, 31);
            this.txtNumeracionVivienda.TabIndex = 27;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(18, 90);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(136, 26);
            this.label22.TabIndex = 26;
            this.label22.Text = "Numeración:";
            // 
            // txtCallePrincipal
            // 
            this.txtCallePrincipal.BackColor = System.Drawing.SystemColors.Info;
            this.txtCallePrincipal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCallePrincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCallePrincipal.Location = new System.Drawing.Point(169, 51);
            this.txtCallePrincipal.Margin = new System.Windows.Forms.Padding(2);
            this.txtCallePrincipal.Name = "txtCallePrincipal";
            this.txtCallePrincipal.ReadOnly = true;
            this.txtCallePrincipal.Size = new System.Drawing.Size(637, 31);
            this.txtCallePrincipal.TabIndex = 25;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label21.Location = new System.Drawing.Point(18, 54);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(158, 26);
            this.label21.TabIndex = 24;
            this.label21.Text = "Calle Principal:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(18, 19);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 26);
            this.label13.TabIndex = 10;
            this.label13.Text = "Sector:";
            // 
            // txtSector
            // 
            this.txtSector.BackColor = System.Drawing.SystemColors.Info;
            this.txtSector.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSector.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSector.Location = new System.Drawing.Point(169, 16);
            this.txtSector.Margin = new System.Windows.Forms.Padding(2);
            this.txtSector.Name = "txtSector";
            this.txtSector.ReadOnly = true;
            this.txtSector.Size = new System.Drawing.Size(637, 31);
            this.txtSector.TabIndex = 9;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox4.Controls.Add(this.txtFechaUltimaOrden);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.txtClienteDesde);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtNumeroOrdenes);
            this.groupBox4.Location = new System.Drawing.Point(12, 433);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(836, 100);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // txtFechaUltimaOrden
            // 
            this.txtFechaUltimaOrden.BackColor = System.Drawing.SystemColors.Info;
            this.txtFechaUltimaOrden.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFechaUltimaOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFechaUltimaOrden.Location = new System.Drawing.Point(636, 19);
            this.txtFechaUltimaOrden.Margin = new System.Windows.Forms.Padding(2);
            this.txtFechaUltimaOrden.Name = "txtFechaUltimaOrden";
            this.txtFechaUltimaOrden.ReadOnly = true;
            this.txtFechaUltimaOrden.Size = new System.Drawing.Size(170, 31);
            this.txtFechaUltimaOrden.TabIndex = 27;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(428, 21);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(204, 26);
            this.label18.TabIndex = 26;
            this.label18.Text = "Fecha última orden:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(15, 21);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(151, 26);
            this.label14.TabIndex = 20;
            this.label14.Text = "Cliente desde:";
            // 
            // txtClienteDesde
            // 
            this.txtClienteDesde.BackColor = System.Drawing.SystemColors.Info;
            this.txtClienteDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClienteDesde.Location = new System.Drawing.Point(186, 18);
            this.txtClienteDesde.Margin = new System.Windows.Forms.Padding(2);
            this.txtClienteDesde.Name = "txtClienteDesde";
            this.txtClienteDesde.ReadOnly = true;
            this.txtClienteDesde.Size = new System.Drawing.Size(223, 31);
            this.txtClienteDesde.TabIndex = 21;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(15, 56);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(166, 26);
            this.label15.TabIndex = 22;
            this.label15.Text = "No. de órdenes:";
            // 
            // txtNumeroOrdenes
            // 
            this.txtNumeroOrdenes.BackColor = System.Drawing.SystemColors.Info;
            this.txtNumeroOrdenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroOrdenes.Location = new System.Drawing.Point(185, 53);
            this.txtNumeroOrdenes.Margin = new System.Windows.Forms.Padding(2);
            this.txtNumeroOrdenes.Name = "txtNumeroOrdenes";
            this.txtNumeroOrdenes.ReadOnly = true;
            this.txtNumeroOrdenes.Size = new System.Drawing.Size(223, 31);
            this.txtNumeroOrdenes.TabIndex = 23;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnEditarDatos);
            this.groupBox5.Controls.Add(this.btnTerminar);
            this.groupBox5.Controls.Add(this.btnCancelar);
            this.groupBox5.Controls.Add(this.btnHistorial);
            this.groupBox5.Location = new System.Drawing.Point(860, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(159, 530);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            // 
            // btnEditarDatos
            // 
            this.btnEditarDatos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnEditarDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarDatos.Image = global::Palatium.Properties.Resources.editar_png;
            this.btnEditarDatos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditarDatos.Location = new System.Drawing.Point(5, 102);
            this.btnEditarDatos.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditarDatos.Name = "btnEditarDatos";
            this.btnEditarDatos.Size = new System.Drawing.Size(149, 82);
            this.btnEditarDatos.TabIndex = 26;
            this.btnEditarDatos.Text = "Editar Datos";
            this.btnEditarDatos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEditarDatos.UseVisualStyleBackColor = false;
            this.btnEditarDatos.Click += new System.EventHandler(this.btnEditarDatos_Click);
            // 
            // btnTerminar
            // 
            this.btnTerminar.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnTerminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminar.Image = global::Palatium.Properties.Resources.ok2_png;
            this.btnTerminar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTerminar.Location = new System.Drawing.Point(5, 18);
            this.btnTerminar.Margin = new System.Windows.Forms.Padding(2);
            this.btnTerminar.Name = "btnTerminar";
            this.btnTerminar.Size = new System.Drawing.Size(149, 82);
            this.btnTerminar.TabIndex = 25;
            this.btnTerminar.Text = "Terminar";
            this.btnTerminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTerminar.UseVisualStyleBackColor = false;
            this.btnTerminar.Click += new System.EventHandler(this.btnTerminar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::Palatium.Properties.Resources.cancelar_digitos;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelar.Location = new System.Drawing.Point(5, 433);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(141, 82);
            this.btnCancelar.TabIndex = 24;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnHistorial
            // 
            this.btnHistorial.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.85714F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorial.Image = global::Palatium.Properties.Resources.editar_comanda_revisar;
            this.btnHistorial.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHistorial.Location = new System.Drawing.Point(5, 186);
            this.btnHistorial.Margin = new System.Windows.Forms.Padding(2);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(149, 82);
            this.btnHistorial.TabIndex = 23;
            this.btnHistorial.Text = "Historial";
            this.btnHistorial.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHistorial.UseVisualStyleBackColor = false;
            // 
            // frmNumeroTelefono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1031, 538);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNumeroTelefono";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Numero de Teléfono";
            this.Load += new System.EventHandler(this.frmNumeroTelefono_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmNumeroTelefono_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPorTelefono;
        private System.Windows.Forms.Button btnPorIdentificacion;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtIdentificacion;
        private System.Windows.Forms.Label label23;
        public System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtNumeroTelefono;
        public System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox txtSector;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtCallePrincipal;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.TextBox txtNumeracionVivienda;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtCalleSecundaria;
        public System.Windows.Forms.RichTextBox txtReferencia;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtClienteDesde;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNumeroOrdenes;
        private System.Windows.Forms.TextBox txtFechaUltimaOrden;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnTerminar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnHistorial;
        private System.Windows.Forms.Button btnEditarDatos;
    }
}