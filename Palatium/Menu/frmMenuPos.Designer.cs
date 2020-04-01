namespace Palatium.Menú
{
    partial class frmMenuPos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuPos));
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.lblSitioWeb = new System.Windows.Forms.LinkLabel();
            this.lblContacto = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnOficina = new System.Windows.Forms.Button();
            this.btnMovimientoCaja = new System.Windows.Forms.Button();
            this.btnSalidaCajero = new System.Windows.Forms.Button();
            this.btnEntradaCajero = new System.Windows.Forms.Button();
            this.btnReabrirCaja = new System.Windows.Forms.Button();
            this.btnFacturasSri = new System.Windows.Forms.Button();
            this.btnClienteEmpresarial = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRevisar = new System.Windows.Forms.Button();
            this.btnCortesias = new System.Windows.Forms.Button();
            this.btnCanjes = new System.Windows.Forms.Button();
            this.btnConsumoEmpleados = new System.Windows.Forms.Button();
            this.btnFuncionarios = new System.Windows.Forms.Button();
            this.btnRepartidorExterno = new System.Windows.Forms.Button();
            this.btnEstadisticas = new System.Windows.Forms.Button();
            this.btnDomicilios = new System.Windows.Forms.Button();
            this.btnLlevar = new System.Windows.Forms.Button();
            this.btnMesas = new System.Windows.Forms.Button();
            this.btnAcerca = new System.Windows.Forms.LinkLabel();
            this.btnCambioOrigen = new System.Windows.Forms.Button();
            this.btnEditarFactura = new System.Windows.Forms.Button();
            this.btnDatosClientes = new System.Windows.Forms.Button();
            this.btnAbrirCajonDinero = new System.Windows.Forms.Button();
            this.btnConsultarPrecios = new System.Windows.Forms.Button();
            this.btnCambioCajero = new System.Windows.Forms.Button();
            this.btnAnularFactura = new System.Windows.Forms.Button();
            this.btnCobroAlmuerzos = new System.Windows.Forms.Button();
            this.btnVentaExpress = new System.Windows.Forms.Button();
            this.btnTarjetaAlmuerzo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logo = new System.Windows.Forms.PictureBox();
            this.grupoAccesos = new System.Windows.Forms.GroupBox();
            this.btnReimprimirFactura = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.grupoAccesos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSitioWeb
            // 
            this.lblSitioWeb.AutoSize = true;
            this.lblSitioWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSitioWeb.Location = new System.Drawing.Point(148, 628);
            this.lblSitioWeb.Name = "lblSitioWeb";
            this.lblSitioWeb.Size = new System.Drawing.Size(105, 16);
            this.lblSitioWeb.TabIndex = 13;
            this.lblSitioWeb.TabStop = true;
            this.lblSitioWeb.Text = "www.aplicsis.net";
            this.ttMensaje.SetToolTip(this.lblSitioWeb, "Clic aquí para abrir la página web del fabricante");
            this.lblSitioWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblSitioWeb_LinkClicked);
            // 
            // lblContacto
            // 
            this.lblContacto.AutoSize = true;
            this.lblContacto.BackColor = System.Drawing.Color.White;
            this.lblContacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblContacto.Location = new System.Drawing.Point(149, 612);
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.Size = new System.Drawing.Size(157, 16);
            this.lblContacto.TabIndex = 14;
            this.lblContacto.Text = "Contacto: 0995610690";
            this.ttMensaje.SetToolTip(this.lblContacto, "Clic aquí para abrir el formulario de Reimpresión de Facturas");
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Navy;
            this.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 2;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Image = global::Palatium.Properties.Resources.icono_salir_sis;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalir.Location = new System.Drawing.Point(546, 508);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(172, 118);
            this.btnSalir.TabIndex = 40;
            this.btnSalir.Text = "Salir del Programa\r\n  ";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnSalir, "Clic aquí para salir del programa");
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnSalir.MouseEnter += new System.EventHandler(this.btnSalir_MouseEnter);
            this.btnSalir.MouseLeave += new System.EventHandler(this.btnSalir_MouseLeave);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.Navy;
            this.btnCerrarSesion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrarSesion.FlatAppearance.BorderSize = 2;
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.White;
            this.btnCerrarSesion.Image = global::Palatium.Properties.Resources.icono_cerrar_sesion;
            this.btnCerrarSesion.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCerrarSesion.Location = new System.Drawing.Point(368, 508);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(172, 118);
            this.btnCerrarSesion.TabIndex = 39;
            this.btnCerrarSesion.Text = "Cerrar Sesión\r\n  ";
            this.btnCerrarSesion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCerrarSesion, "Clic aquí para cerrar sesión");
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            this.btnCerrarSesion.MouseEnter += new System.EventHandler(this.btnCerrarSesion_MouseEnter);
            this.btnCerrarSesion.MouseLeave += new System.EventHandler(this.btnCerrarSesion_MouseLeave);
            // 
            // btnOficina
            // 
            this.btnOficina.BackColor = System.Drawing.Color.Navy;
            this.btnOficina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOficina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOficina.FlatAppearance.BorderSize = 2;
            this.btnOficina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOficina.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOficina.ForeColor = System.Drawing.Color.White;
            this.btnOficina.Image = global::Palatium.Properties.Resources.icono_oficina;
            this.btnOficina.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOficina.Location = new System.Drawing.Point(190, 508);
            this.btnOficina.Name = "btnOficina";
            this.btnOficina.Size = new System.Drawing.Size(172, 118);
            this.btnOficina.TabIndex = 38;
            this.btnOficina.Text = "Oficina\r\nAdministración";
            this.btnOficina.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnOficina, "Clic aquí para abrir la administración del sistema");
            this.btnOficina.UseVisualStyleBackColor = false;
            this.btnOficina.Click += new System.EventHandler(this.btnOficina_Click);
            this.btnOficina.MouseEnter += new System.EventHandler(this.btnOficina_MouseEnter);
            this.btnOficina.MouseLeave += new System.EventHandler(this.btnOficina_MouseLeave);
            // 
            // btnMovimientoCaja
            // 
            this.btnMovimientoCaja.BackColor = System.Drawing.Color.Navy;
            this.btnMovimientoCaja.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMovimientoCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMovimientoCaja.FlatAppearance.BorderSize = 2;
            this.btnMovimientoCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovimientoCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovimientoCaja.ForeColor = System.Drawing.Color.White;
            this.btnMovimientoCaja.Image = global::Palatium.Properties.Resources.icono_caja_chica;
            this.btnMovimientoCaja.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMovimientoCaja.Location = new System.Drawing.Point(12, 508);
            this.btnMovimientoCaja.Name = "btnMovimientoCaja";
            this.btnMovimientoCaja.Size = new System.Drawing.Size(172, 118);
            this.btnMovimientoCaja.TabIndex = 37;
            this.btnMovimientoCaja.Text = "Movimientos\r\nde Caja";
            this.btnMovimientoCaja.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnMovimientoCaja, "Clic aquí para ver los movimientos de caja");
            this.btnMovimientoCaja.UseVisualStyleBackColor = false;
            this.btnMovimientoCaja.Click += new System.EventHandler(this.btnMovimientoCaja_Click);
            this.btnMovimientoCaja.MouseEnter += new System.EventHandler(this.btnMovimientoCaja_MouseEnter);
            this.btnMovimientoCaja.MouseLeave += new System.EventHandler(this.btnMovimientoCaja_MouseLeave);
            // 
            // btnSalidaCajero
            // 
            this.btnSalidaCajero.BackColor = System.Drawing.Color.Navy;
            this.btnSalidaCajero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSalidaCajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalidaCajero.FlatAppearance.BorderSize = 2;
            this.btnSalidaCajero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalidaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalidaCajero.ForeColor = System.Drawing.Color.White;
            this.btnSalidaCajero.Image = global::Palatium.Properties.Resources.icono_abrir_caja;
            this.btnSalidaCajero.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalidaCajero.Location = new System.Drawing.Point(546, 384);
            this.btnSalidaCajero.Name = "btnSalidaCajero";
            this.btnSalidaCajero.Size = new System.Drawing.Size(172, 118);
            this.btnSalidaCajero.TabIndex = 36;
            this.btnSalidaCajero.Text = "Arqueo de Caja\r\n  ";
            this.btnSalidaCajero.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnSalidaCajero, "Clic aquí para mostrar el arqueo de caja");
            this.btnSalidaCajero.UseVisualStyleBackColor = false;
            this.btnSalidaCajero.Click += new System.EventHandler(this.btnSalidaCajero_Click);
            this.btnSalidaCajero.MouseEnter += new System.EventHandler(this.btnSalidaCajero_MouseEnter);
            this.btnSalidaCajero.MouseLeave += new System.EventHandler(this.btnSalidaCajero_MouseLeave);
            // 
            // btnEntradaCajero
            // 
            this.btnEntradaCajero.BackColor = System.Drawing.Color.Navy;
            this.btnEntradaCajero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEntradaCajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntradaCajero.FlatAppearance.BorderSize = 2;
            this.btnEntradaCajero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntradaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntradaCajero.ForeColor = System.Drawing.Color.White;
            this.btnEntradaCajero.Image = global::Palatium.Properties.Resources.icono_login;
            this.btnEntradaCajero.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEntradaCajero.Location = new System.Drawing.Point(368, 384);
            this.btnEntradaCajero.Name = "btnEntradaCajero";
            this.btnEntradaCajero.Size = new System.Drawing.Size(172, 118);
            this.btnEntradaCajero.TabIndex = 35;
            this.btnEntradaCajero.Text = "Ingreso Usuario\r\n ";
            this.btnEntradaCajero.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnEntradaCajero, "Clic aquí para ingreso del usuario registrado al sistema");
            this.btnEntradaCajero.UseVisualStyleBackColor = false;
            this.btnEntradaCajero.Click += new System.EventHandler(this.btnEntradaCajero_Click);
            this.btnEntradaCajero.MouseEnter += new System.EventHandler(this.btnEntradaCajero_MouseEnter);
            this.btnEntradaCajero.MouseLeave += new System.EventHandler(this.btnEntradaCajero_MouseLeave);
            // 
            // btnReabrirCaja
            // 
            this.btnReabrirCaja.BackColor = System.Drawing.Color.Navy;
            this.btnReabrirCaja.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReabrirCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReabrirCaja.FlatAppearance.BorderSize = 2;
            this.btnReabrirCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReabrirCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReabrirCaja.ForeColor = System.Drawing.Color.White;
            this.btnReabrirCaja.Image = global::Palatium.Properties.Resources.icono_cerrar_caja;
            this.btnReabrirCaja.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReabrirCaja.Location = new System.Drawing.Point(190, 384);
            this.btnReabrirCaja.Name = "btnReabrirCaja";
            this.btnReabrirCaja.Size = new System.Drawing.Size(172, 118);
            this.btnReabrirCaja.TabIndex = 34;
            this.btnReabrirCaja.Text = "Reabrir Caja\r\n  ";
            this.btnReabrirCaja.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnReabrirCaja, "Clic aquí para reabrir caja");
            this.btnReabrirCaja.UseVisualStyleBackColor = false;
            this.btnReabrirCaja.Click += new System.EventHandler(this.btnReabrirCaja_Click);
            this.btnReabrirCaja.MouseEnter += new System.EventHandler(this.btnReabrirCaja_MouseEnter);
            this.btnReabrirCaja.MouseLeave += new System.EventHandler(this.btnReabrirCaja_MouseLeave);
            // 
            // btnFacturasSri
            // 
            this.btnFacturasSri.BackColor = System.Drawing.Color.Navy;
            this.btnFacturasSri.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFacturasSri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacturasSri.FlatAppearance.BorderSize = 2;
            this.btnFacturasSri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacturasSri.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturasSri.ForeColor = System.Drawing.Color.White;
            this.btnFacturasSri.Image = global::Palatium.Properties.Resources.icono_sri;
            this.btnFacturasSri.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFacturasSri.Location = new System.Drawing.Point(12, 384);
            this.btnFacturasSri.Name = "btnFacturasSri";
            this.btnFacturasSri.Size = new System.Drawing.Size(172, 118);
            this.btnFacturasSri.TabIndex = 33;
            this.btnFacturasSri.Text = "Enviar Facturas\r\nal SRI";
            this.btnFacturasSri.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnFacturasSri, "Clic aquí para enviar las facturas emitidas al SRI");
            this.btnFacturasSri.UseVisualStyleBackColor = false;
            this.btnFacturasSri.Click += new System.EventHandler(this.btnFacturasSri_Click);
            this.btnFacturasSri.MouseEnter += new System.EventHandler(this.btnFacturasSri_MouseEnter);
            this.btnFacturasSri.MouseLeave += new System.EventHandler(this.btnFacturasSri_MouseLeave);
            // 
            // btnClienteEmpresarial
            // 
            this.btnClienteEmpresarial.BackColor = System.Drawing.Color.Navy;
            this.btnClienteEmpresarial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClienteEmpresarial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClienteEmpresarial.FlatAppearance.BorderSize = 2;
            this.btnClienteEmpresarial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClienteEmpresarial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClienteEmpresarial.ForeColor = System.Drawing.Color.White;
            this.btnClienteEmpresarial.Image = global::Palatium.Properties.Resources.icono_cliente_empresarial;
            this.btnClienteEmpresarial.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClienteEmpresarial.Location = new System.Drawing.Point(546, 260);
            this.btnClienteEmpresarial.Name = "btnClienteEmpresarial";
            this.btnClienteEmpresarial.Size = new System.Drawing.Size(172, 118);
            this.btnClienteEmpresarial.TabIndex = 32;
            this.btnClienteEmpresarial.Text = "Cliente\r\nEmpresarial";
            this.btnClienteEmpresarial.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnClienteEmpresarial, "Clic aquí para ingresar datos completos del cliente");
            this.btnClienteEmpresarial.UseVisualStyleBackColor = false;
            this.btnClienteEmpresarial.Click += new System.EventHandler(this.btnClienteEmpresarial_Click);
            this.btnClienteEmpresarial.MouseEnter += new System.EventHandler(this.btnClienteEmpresarial_MouseEnter);
            this.btnClienteEmpresarial.MouseLeave += new System.EventHandler(this.btnClienteEmpresarial_MouseLeave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Navy;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = global::Palatium.Properties.Resources.icono_cancelar_orden;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelar.Location = new System.Drawing.Point(368, 260);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(172, 118);
            this.btnCancelar.TabIndex = 31;
            this.btnCancelar.Text = "Cancelar Orden\n  ";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCancelar, "Clic aquí para anular las comandas abiertas");
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnCancelar_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnCancelar_MouseLeave);
            // 
            // btnRevisar
            // 
            this.btnRevisar.BackColor = System.Drawing.Color.Navy;
            this.btnRevisar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRevisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevisar.FlatAppearance.BorderSize = 2;
            this.btnRevisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisar.ForeColor = System.Drawing.Color.White;
            this.btnRevisar.Image = global::Palatium.Properties.Resources.icono_revisar;
            this.btnRevisar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRevisar.Location = new System.Drawing.Point(190, 260);
            this.btnRevisar.Name = "btnRevisar";
            this.btnRevisar.Size = new System.Drawing.Size(172, 118);
            this.btnRevisar.TabIndex = 30;
            this.btnRevisar.Text = "Revisar Órdenes\r\n ";
            this.btnRevisar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnRevisar, "Clic aquí para revisar las comandas ingresadas");
            this.btnRevisar.UseVisualStyleBackColor = false;
            this.btnRevisar.Click += new System.EventHandler(this.btnRevisar_Click);
            this.btnRevisar.MouseEnter += new System.EventHandler(this.btnRevisar_MouseEnter);
            this.btnRevisar.MouseLeave += new System.EventHandler(this.btnRevisar_MouseLeave);
            // 
            // btnCortesias
            // 
            this.btnCortesias.BackColor = System.Drawing.Color.Navy;
            this.btnCortesias.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCortesias.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCortesias.FlatAppearance.BorderSize = 2;
            this.btnCortesias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCortesias.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCortesias.ForeColor = System.Drawing.Color.White;
            this.btnCortesias.Image = global::Palatium.Properties.Resources.icono_cortesia;
            this.btnCortesias.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCortesias.Location = new System.Drawing.Point(546, 12);
            this.btnCortesias.Name = "btnCortesias";
            this.btnCortesias.Size = new System.Drawing.Size(172, 118);
            this.btnCortesias.TabIndex = 29;
            this.btnCortesias.Text = "Cortesías\r\n  ";
            this.btnCortesias.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCortesias, "Clic aquí para gestión de comandas para cortesías");
            this.btnCortesias.UseVisualStyleBackColor = false;
            this.btnCortesias.Click += new System.EventHandler(this.btnCortesias_Click);
            this.btnCortesias.MouseEnter += new System.EventHandler(this.btnCortesias_MouseEnter);
            this.btnCortesias.MouseLeave += new System.EventHandler(this.btnCortesias_MouseLeave);
            // 
            // btnCanjes
            // 
            this.btnCanjes.BackColor = System.Drawing.Color.Navy;
            this.btnCanjes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCanjes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCanjes.FlatAppearance.BorderSize = 2;
            this.btnCanjes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanjes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanjes.ForeColor = System.Drawing.Color.White;
            this.btnCanjes.Image = global::Palatium.Properties.Resources.icono_canje;
            this.btnCanjes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCanjes.Location = new System.Drawing.Point(546, 136);
            this.btnCanjes.Name = "btnCanjes";
            this.btnCanjes.Size = new System.Drawing.Size(172, 118);
            this.btnCanjes.TabIndex = 28;
            this.btnCanjes.Text = "Canjes\r\n  ";
            this.btnCanjes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCanjes, "Clic aquí para gestión de comandas por canjes");
            this.btnCanjes.UseVisualStyleBackColor = false;
            this.btnCanjes.Click += new System.EventHandler(this.btnCanjes_Click);
            this.btnCanjes.MouseEnter += new System.EventHandler(this.btnCanjes_MouseEnter);
            this.btnCanjes.MouseLeave += new System.EventHandler(this.btnCanjes_MouseLeave);
            // 
            // btnConsumoEmpleados
            // 
            this.btnConsumoEmpleados.BackColor = System.Drawing.Color.Navy;
            this.btnConsumoEmpleados.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnConsumoEmpleados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsumoEmpleados.FlatAppearance.BorderSize = 2;
            this.btnConsumoEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsumoEmpleados.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsumoEmpleados.ForeColor = System.Drawing.Color.White;
            this.btnConsumoEmpleados.Image = global::Palatium.Properties.Resources.icono_consumo_empleados;
            this.btnConsumoEmpleados.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConsumoEmpleados.Location = new System.Drawing.Point(368, 136);
            this.btnConsumoEmpleados.Name = "btnConsumoEmpleados";
            this.btnConsumoEmpleados.Size = new System.Drawing.Size(172, 118);
            this.btnConsumoEmpleados.TabIndex = 27;
            this.btnConsumoEmpleados.Text = "Consumo\r\nEmpleados";
            this.btnConsumoEmpleados.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnConsumoEmpleados, "Clic aquí para gestión de comandas para consumo de empleados");
            this.btnConsumoEmpleados.UseVisualStyleBackColor = false;
            this.btnConsumoEmpleados.Click += new System.EventHandler(this.btnConsumoEmpleados_Click);
            this.btnConsumoEmpleados.MouseEnter += new System.EventHandler(this.btnConsumoEmpleados_MouseEnter);
            this.btnConsumoEmpleados.MouseLeave += new System.EventHandler(this.btnConsumoEmpleados_MouseLeave);
            // 
            // btnFuncionarios
            // 
            this.btnFuncionarios.BackColor = System.Drawing.Color.Navy;
            this.btnFuncionarios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFuncionarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFuncionarios.FlatAppearance.BorderSize = 2;
            this.btnFuncionarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFuncionarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFuncionarios.ForeColor = System.Drawing.Color.White;
            this.btnFuncionarios.Image = global::Palatium.Properties.Resources.icono_funcionarios_2;
            this.btnFuncionarios.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFuncionarios.Location = new System.Drawing.Point(448, 204);
            this.btnFuncionarios.Name = "btnFuncionarios";
            this.btnFuncionarios.Size = new System.Drawing.Size(172, 118);
            this.btnFuncionarios.TabIndex = 26;
            this.btnFuncionarios.Text = "Funcionarios\r\n  ";
            this.btnFuncionarios.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnFuncionarios, "Clic aquí para gestión de comandas para funcionarios");
            this.btnFuncionarios.UseVisualStyleBackColor = false;
            this.btnFuncionarios.Visible = false;
            this.btnFuncionarios.Click += new System.EventHandler(this.btnFuncionarios_Click);
            this.btnFuncionarios.MouseEnter += new System.EventHandler(this.btnFuncionarios_MouseEnter);
            this.btnFuncionarios.MouseLeave += new System.EventHandler(this.btnFuncionarios_MouseLeave);
            // 
            // btnRepartidorExterno
            // 
            this.btnRepartidorExterno.BackColor = System.Drawing.Color.Navy;
            this.btnRepartidorExterno.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRepartidorExterno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRepartidorExterno.FlatAppearance.BorderSize = 2;
            this.btnRepartidorExterno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRepartidorExterno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepartidorExterno.ForeColor = System.Drawing.Color.White;
            this.btnRepartidorExterno.Image = global::Palatium.Properties.Resources.icono_repartidor_externo;
            this.btnRepartidorExterno.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRepartidorExterno.Location = new System.Drawing.Point(407, 12);
            this.btnRepartidorExterno.Name = "btnRepartidorExterno";
            this.btnRepartidorExterno.Size = new System.Drawing.Size(172, 118);
            this.btnRepartidorExterno.TabIndex = 25;
            this.btnRepartidorExterno.Text = "Repartidores\r\nExternos";
            this.btnRepartidorExterno.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnRepartidorExterno, "Clic aquí para gestión de comandas por repartidores externos");
            this.btnRepartidorExterno.UseVisualStyleBackColor = false;
            this.btnRepartidorExterno.Visible = false;
            this.btnRepartidorExterno.Click += new System.EventHandler(this.btnRepartidorExterno_Click);
            this.btnRepartidorExterno.MouseEnter += new System.EventHandler(this.btnRepartidorExterno_MouseEnter);
            this.btnRepartidorExterno.MouseLeave += new System.EventHandler(this.btnRepartidorExterno_MouseLeave);
            // 
            // btnEstadisticas
            // 
            this.btnEstadisticas.BackColor = System.Drawing.Color.Navy;
            this.btnEstadisticas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEstadisticas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstadisticas.FlatAppearance.BorderSize = 2;
            this.btnEstadisticas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstadisticas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstadisticas.ForeColor = System.Drawing.Color.White;
            this.btnEstadisticas.Image = global::Palatium.Properties.Resources.estadisticas_png;
            this.btnEstadisticas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEstadisticas.Location = new System.Drawing.Point(12, 260);
            this.btnEstadisticas.Name = "btnEstadisticas";
            this.btnEstadisticas.Size = new System.Drawing.Size(172, 118);
            this.btnEstadisticas.TabIndex = 24;
            this.btnEstadisticas.Text = "Dashboard\r\n  ";
            this.btnEstadisticas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnEstadisticas, "Clic aquí para mostrar las estadísticas del sistema");
            this.btnEstadisticas.UseVisualStyleBackColor = false;
            this.btnEstadisticas.Click += new System.EventHandler(this.btnEstadisticas_Click);
            this.btnEstadisticas.MouseEnter += new System.EventHandler(this.btnEstadisticas_MouseEnter);
            this.btnEstadisticas.MouseLeave += new System.EventHandler(this.btnEstadisticas_MouseLeave);
            // 
            // btnDomicilios
            // 
            this.btnDomicilios.BackColor = System.Drawing.Color.Navy;
            this.btnDomicilios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnDomicilios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDomicilios.FlatAppearance.BorderSize = 2;
            this.btnDomicilios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDomicilios.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDomicilios.ForeColor = System.Drawing.Color.White;
            this.btnDomicilios.Image = global::Palatium.Properties.Resources.icono_domicilio;
            this.btnDomicilios.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDomicilios.Location = new System.Drawing.Point(368, 12);
            this.btnDomicilios.Name = "btnDomicilios";
            this.btnDomicilios.Size = new System.Drawing.Size(172, 118);
            this.btnDomicilios.TabIndex = 23;
            this.btnDomicilios.Text = "Domicilio\r\n  ";
            this.btnDomicilios.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnDomicilios, "Clic aquí para gestión de comandas para domicilio");
            this.btnDomicilios.UseVisualStyleBackColor = false;
            this.btnDomicilios.Click += new System.EventHandler(this.btnDomicilios_Click);
            this.btnDomicilios.MouseEnter += new System.EventHandler(this.btnDomicilios_MouseEnter);
            this.btnDomicilios.MouseLeave += new System.EventHandler(this.btnDomicilios_MouseLeave);
            // 
            // btnLlevar
            // 
            this.btnLlevar.BackColor = System.Drawing.Color.Navy;
            this.btnLlevar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLlevar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLlevar.FlatAppearance.BorderSize = 2;
            this.btnLlevar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLlevar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLlevar.ForeColor = System.Drawing.Color.White;
            this.btnLlevar.Image = global::Palatium.Properties.Resources.icono_llevar;
            this.btnLlevar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLlevar.Location = new System.Drawing.Point(190, 12);
            this.btnLlevar.Name = "btnLlevar";
            this.btnLlevar.Size = new System.Drawing.Size(172, 118);
            this.btnLlevar.TabIndex = 22;
            this.btnLlevar.Text = "Para Llevar\r\n  ";
            this.btnLlevar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnLlevar, "Clic aquí para gestión de comandas para llevar");
            this.btnLlevar.UseVisualStyleBackColor = false;
            this.btnLlevar.Click += new System.EventHandler(this.btnLlevar_Click);
            this.btnLlevar.MouseEnter += new System.EventHandler(this.btnLlevar_MouseEnter);
            this.btnLlevar.MouseLeave += new System.EventHandler(this.btnLlevar_MouseLeave);
            // 
            // btnMesas
            // 
            this.btnMesas.BackColor = System.Drawing.Color.Navy;
            this.btnMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMesas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMesas.FlatAppearance.BorderSize = 2;
            this.btnMesas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMesas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesas.ForeColor = System.Drawing.Color.White;
            this.btnMesas.Image = global::Palatium.Properties.Resources.icono_mesa_menu_2;
            this.btnMesas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMesas.Location = new System.Drawing.Point(12, 12);
            this.btnMesas.Name = "btnMesas";
            this.btnMesas.Size = new System.Drawing.Size(172, 118);
            this.btnMesas.TabIndex = 21;
            this.btnMesas.Text = "Mesas\r\n  ";
            this.btnMesas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnMesas, "Clic aquí para gestión de comandas en mesas");
            this.btnMesas.UseVisualStyleBackColor = false;
            this.btnMesas.Click += new System.EventHandler(this.btnMesas_Click);
            this.btnMesas.MouseEnter += new System.EventHandler(this.btnMesas_MouseEnter);
            this.btnMesas.MouseLeave += new System.EventHandler(this.btnMesas_MouseLeave);
            // 
            // btnAcerca
            // 
            this.btnAcerca.AutoSize = true;
            this.btnAcerca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAcerca.Location = new System.Drawing.Point(291, 628);
            this.btnAcerca.Name = "btnAcerca";
            this.btnAcerca.Size = new System.Drawing.Size(70, 16);
            this.btnAcerca.TabIndex = 41;
            this.btnAcerca.TabStop = true;
            this.btnAcerca.Text = "Acerca de";
            this.ttMensaje.SetToolTip(this.btnAcerca, "Clic aquí para abrir la página web del fabricante");
            this.btnAcerca.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAcerca_LinkClicked);
            // 
            // btnCambioOrigen
            // 
            this.btnCambioOrigen.BackColor = System.Drawing.Color.Navy;
            this.btnCambioOrigen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCambioOrigen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambioOrigen.FlatAppearance.BorderSize = 2;
            this.btnCambioOrigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambioOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambioOrigen.ForeColor = System.Drawing.Color.White;
            this.btnCambioOrigen.Image = global::Palatium.Properties.Resources.icono_cambio_orden;
            this.btnCambioOrigen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCambioOrigen.Location = new System.Drawing.Point(3, 484);
            this.btnCambioOrigen.Name = "btnCambioOrigen";
            this.btnCambioOrigen.Size = new System.Drawing.Size(94, 67);
            this.btnCambioOrigen.TabIndex = 29;
            this.btnCambioOrigen.Text = "Cambio a Cortesía";
            this.btnCambioOrigen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCambioOrigen, "Clic aquí para asignar los repartidores a los pedidos a domicilio");
            this.btnCambioOrigen.UseVisualStyleBackColor = false;
            this.btnCambioOrigen.Click += new System.EventHandler(this.btnCambioOrigen_Click);
            this.btnCambioOrigen.MouseEnter += new System.EventHandler(this.btnCambioOrigen_MouseEnter);
            this.btnCambioOrigen.MouseLeave += new System.EventHandler(this.btnCambioOrigen_MouseLeave);
            // 
            // btnEditarFactura
            // 
            this.btnEditarFactura.BackColor = System.Drawing.Color.Navy;
            this.btnEditarFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditarFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditarFactura.FlatAppearance.BorderSize = 2;
            this.btnEditarFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditarFactura.ForeColor = System.Drawing.Color.White;
            this.btnEditarFactura.Image = global::Palatium.Properties.Resources.icono_editar_factura;
            this.btnEditarFactura.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEditarFactura.Location = new System.Drawing.Point(3, 220);
            this.btnEditarFactura.Name = "btnEditarFactura";
            this.btnEditarFactura.Size = new System.Drawing.Size(94, 67);
            this.btnEditarFactura.TabIndex = 28;
            this.btnEditarFactura.Text = "Editar Datos Factura";
            this.btnEditarFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnEditarFactura, "Botón habilitado para Facturas Electrónicas");
            this.btnEditarFactura.UseVisualStyleBackColor = false;
            this.btnEditarFactura.Click += new System.EventHandler(this.btnEditarFactura_Click);
            this.btnEditarFactura.MouseEnter += new System.EventHandler(this.btnEditarFactura_MouseEnter);
            this.btnEditarFactura.MouseLeave += new System.EventHandler(this.btnEditarFactura_MouseLeave);
            // 
            // btnDatosClientes
            // 
            this.btnDatosClientes.BackColor = System.Drawing.Color.Navy;
            this.btnDatosClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDatosClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDatosClientes.FlatAppearance.BorderSize = 2;
            this.btnDatosClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatosClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatosClientes.ForeColor = System.Drawing.Color.White;
            this.btnDatosClientes.Image = global::Palatium.Properties.Resources.icono_cliemtes;
            this.btnDatosClientes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDatosClientes.Location = new System.Drawing.Point(3, 418);
            this.btnDatosClientes.Name = "btnDatosClientes";
            this.btnDatosClientes.Size = new System.Drawing.Size(94, 67);
            this.btnDatosClientes.TabIndex = 27;
            this.btnDatosClientes.Text = "Clientes";
            this.btnDatosClientes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnDatosClientes, "Clic aquí para asignar los repartidores a los pedidos a domicilio");
            this.btnDatosClientes.UseVisualStyleBackColor = false;
            this.btnDatosClientes.Click += new System.EventHandler(this.btnDatosClientes_Click);
            this.btnDatosClientes.MouseEnter += new System.EventHandler(this.btnDatosClientes_MouseEnter);
            this.btnDatosClientes.MouseLeave += new System.EventHandler(this.btnDatosClientes_MouseLeave);
            // 
            // btnAbrirCajonDinero
            // 
            this.btnAbrirCajonDinero.BackColor = System.Drawing.Color.Navy;
            this.btnAbrirCajonDinero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAbrirCajonDinero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirCajonDinero.FlatAppearance.BorderSize = 2;
            this.btnAbrirCajonDinero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCajonDinero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCajonDinero.ForeColor = System.Drawing.Color.White;
            this.btnAbrirCajonDinero.Image = global::Palatium.Properties.Resources.icono_abrir_caja_dinero;
            this.btnAbrirCajonDinero.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAbrirCajonDinero.Location = new System.Drawing.Point(3, 22);
            this.btnAbrirCajonDinero.Name = "btnAbrirCajonDinero";
            this.btnAbrirCajonDinero.Size = new System.Drawing.Size(94, 67);
            this.btnAbrirCajonDinero.TabIndex = 22;
            this.btnAbrirCajonDinero.Text = "Abrir Cajón";
            this.btnAbrirCajonDinero.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnAbrirCajonDinero, "Clic aquí para abrir el cajón de dinero");
            this.btnAbrirCajonDinero.UseVisualStyleBackColor = false;
            this.btnAbrirCajonDinero.Click += new System.EventHandler(this.btnAbrirCajonDinero_Click);
            this.btnAbrirCajonDinero.MouseEnter += new System.EventHandler(this.btnAbrirCajonDinero_MouseEnter);
            this.btnAbrirCajonDinero.MouseLeave += new System.EventHandler(this.btnAbrirCajonDinero_MouseLeave);
            // 
            // btnConsultarPrecios
            // 
            this.btnConsultarPrecios.BackColor = System.Drawing.Color.Navy;
            this.btnConsultarPrecios.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConsultarPrecios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultarPrecios.FlatAppearance.BorderSize = 2;
            this.btnConsultarPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultarPrecios.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarPrecios.ForeColor = System.Drawing.Color.White;
            this.btnConsultarPrecios.Image = global::Palatium.Properties.Resources.icono_precios;
            this.btnConsultarPrecios.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConsultarPrecios.Location = new System.Drawing.Point(3, 352);
            this.btnConsultarPrecios.Name = "btnConsultarPrecios";
            this.btnConsultarPrecios.Size = new System.Drawing.Size(94, 67);
            this.btnConsultarPrecios.TabIndex = 26;
            this.btnConsultarPrecios.Text = "Consulta\r\nde Precios";
            this.btnConsultarPrecios.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnConsultarPrecios, "Clic aquí para consultar los precios de los productos");
            this.btnConsultarPrecios.UseVisualStyleBackColor = false;
            this.btnConsultarPrecios.Click += new System.EventHandler(this.btnConsultarPrecios_Click);
            this.btnConsultarPrecios.MouseEnter += new System.EventHandler(this.btnConsultarPrecios_MouseEnter);
            this.btnConsultarPrecios.MouseLeave += new System.EventHandler(this.btnConsultarPrecios_MouseLeave);
            // 
            // btnCambioCajero
            // 
            this.btnCambioCajero.BackColor = System.Drawing.Color.Navy;
            this.btnCambioCajero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCambioCajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambioCajero.FlatAppearance.BorderSize = 2;
            this.btnCambioCajero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambioCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambioCajero.ForeColor = System.Drawing.Color.White;
            this.btnCambioCajero.Image = global::Palatium.Properties.Resources.icono_cambiar_cajero;
            this.btnCambioCajero.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCambioCajero.Location = new System.Drawing.Point(3, 286);
            this.btnCambioCajero.Name = "btnCambioCajero";
            this.btnCambioCajero.Size = new System.Drawing.Size(94, 67);
            this.btnCambioCajero.TabIndex = 25;
            this.btnCambioCajero.Text = "Cambio de\r\nCajero";
            this.btnCambioCajero.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCambioCajero, "Clic aquí para cambiar los datos de ingreso de cajero");
            this.btnCambioCajero.UseVisualStyleBackColor = false;
            this.btnCambioCajero.Click += new System.EventHandler(this.btnCambioCajero_Click);
            this.btnCambioCajero.MouseEnter += new System.EventHandler(this.btnCambioCajero_MouseEnter);
            this.btnCambioCajero.MouseLeave += new System.EventHandler(this.btnCambioCajero_MouseLeave);
            // 
            // btnAnularFactura
            // 
            this.btnAnularFactura.BackColor = System.Drawing.Color.Navy;
            this.btnAnularFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnularFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnularFactura.FlatAppearance.BorderSize = 2;
            this.btnAnularFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnularFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnularFactura.ForeColor = System.Drawing.Color.White;
            this.btnAnularFactura.Image = global::Palatium.Properties.Resources.icono_anular_factura;
            this.btnAnularFactura.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAnularFactura.Location = new System.Drawing.Point(3, 154);
            this.btnAnularFactura.Name = "btnAnularFactura";
            this.btnAnularFactura.Size = new System.Drawing.Size(94, 67);
            this.btnAnularFactura.TabIndex = 24;
            this.btnAnularFactura.Text = "Cambiar\r\nFactura";
            this.btnAnularFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnAnularFactura, "Clic aquí para anular y emitir una nueva factura");
            this.btnAnularFactura.UseVisualStyleBackColor = false;
            this.btnAnularFactura.Click += new System.EventHandler(this.btnAnularFactura_Click);
            this.btnAnularFactura.MouseEnter += new System.EventHandler(this.btnAnularFactura_MouseEnter);
            this.btnAnularFactura.MouseLeave += new System.EventHandler(this.btnAnularFactura_MouseLeave);
            // 
            // btnCobroAlmuerzos
            // 
            this.btnCobroAlmuerzos.BackColor = System.Drawing.Color.Navy;
            this.btnCobroAlmuerzos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCobroAlmuerzos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCobroAlmuerzos.FlatAppearance.BorderSize = 2;
            this.btnCobroAlmuerzos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCobroAlmuerzos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCobroAlmuerzos.ForeColor = System.Drawing.Color.White;
            this.btnCobroAlmuerzos.Image = global::Palatium.Properties.Resources.cobrar_almuerzos;
            this.btnCobroAlmuerzos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCobroAlmuerzos.Location = new System.Drawing.Point(3, 550);
            this.btnCobroAlmuerzos.Name = "btnCobroAlmuerzos";
            this.btnCobroAlmuerzos.Size = new System.Drawing.Size(94, 79);
            this.btnCobroAlmuerzos.TabIndex = 43;
            this.btnCobroAlmuerzos.Text = "Cobro de Almuerzos";
            this.btnCobroAlmuerzos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnCobroAlmuerzos, "Clic aquí para asignar los repartidores a los pedidos a domicilio");
            this.btnCobroAlmuerzos.UseVisualStyleBackColor = false;
            this.btnCobroAlmuerzos.Click += new System.EventHandler(this.btnCobroAlmuerzos_Click);
            this.btnCobroAlmuerzos.MouseEnter += new System.EventHandler(this.btnCobroAlmuerzos_MouseEnter);
            this.btnCobroAlmuerzos.MouseLeave += new System.EventHandler(this.btnCobroAlmuerzos_MouseLeave);
            // 
            // btnVentaExpress
            // 
            this.btnVentaExpress.BackColor = System.Drawing.Color.Navy;
            this.btnVentaExpress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVentaExpress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentaExpress.FlatAppearance.BorderSize = 2;
            this.btnVentaExpress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentaExpress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentaExpress.ForeColor = System.Drawing.Color.White;
            this.btnVentaExpress.Image = global::Palatium.Properties.Resources.icono_comida_rapida;
            this.btnVentaExpress.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVentaExpress.Location = new System.Drawing.Point(12, 136);
            this.btnVentaExpress.Name = "btnVentaExpress";
            this.btnVentaExpress.Size = new System.Drawing.Size(172, 118);
            this.btnVentaExpress.TabIndex = 41;
            this.btnVentaExpress.Text = "Venta\r\nExpress\r\n";
            this.btnVentaExpress.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnVentaExpress, "Clic aquí para gestión de comandas de forma express");
            this.btnVentaExpress.UseVisualStyleBackColor = false;
            this.btnVentaExpress.Click += new System.EventHandler(this.btnVentaExpress_Click);
            this.btnVentaExpress.MouseEnter += new System.EventHandler(this.btnVentaExpress_MouseEnter);
            this.btnVentaExpress.MouseLeave += new System.EventHandler(this.btnVentaExpress_MouseLeave);
            // 
            // btnTarjetaAlmuerzo
            // 
            this.btnTarjetaAlmuerzo.BackColor = System.Drawing.Color.Navy;
            this.btnTarjetaAlmuerzo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnTarjetaAlmuerzo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTarjetaAlmuerzo.FlatAppearance.BorderSize = 2;
            this.btnTarjetaAlmuerzo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTarjetaAlmuerzo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTarjetaAlmuerzo.ForeColor = System.Drawing.Color.White;
            this.btnTarjetaAlmuerzo.Image = global::Palatium.Properties.Resources.tarjeta_almuerzo_inicio;
            this.btnTarjetaAlmuerzo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTarjetaAlmuerzo.Location = new System.Drawing.Point(190, 136);
            this.btnTarjetaAlmuerzo.Name = "btnTarjetaAlmuerzo";
            this.btnTarjetaAlmuerzo.Size = new System.Drawing.Size(172, 118);
            this.btnTarjetaAlmuerzo.TabIndex = 42;
            this.btnTarjetaAlmuerzo.Text = "Tarjetas de Almuerzo";
            this.btnTarjetaAlmuerzo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMensaje.SetToolTip(this.btnTarjetaAlmuerzo, "Clic aquí para gestión de comandas para tarjetas de almuerzo");
            this.btnTarjetaAlmuerzo.UseVisualStyleBackColor = false;
            this.btnTarjetaAlmuerzo.Click += new System.EventHandler(this.btnTarjetaAlmuerzo_Click);
            this.btnTarjetaAlmuerzo.MouseEnter += new System.EventHandler(this.btnTarjetaAlmuerzo_MouseEnter);
            this.btnTarjetaAlmuerzo.MouseLeave += new System.EventHandler(this.btnTarjetaAlmuerzo_MouseLeave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTarjetaAlmuerzo);
            this.panel1.Controls.Add(this.btnVentaExpress);
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.btnCerrarSesion);
            this.panel1.Controls.Add(this.btnOficina);
            this.panel1.Controls.Add(this.btnMovimientoCaja);
            this.panel1.Controls.Add(this.btnSalidaCajero);
            this.panel1.Controls.Add(this.btnEntradaCajero);
            this.panel1.Controls.Add(this.btnReabrirCaja);
            this.panel1.Controls.Add(this.btnFacturasSri);
            this.panel1.Controls.Add(this.btnClienteEmpresarial);
            this.panel1.Controls.Add(this.btnCancelar);
            this.panel1.Controls.Add(this.btnRevisar);
            this.panel1.Controls.Add(this.btnCortesias);
            this.panel1.Controls.Add(this.btnCanjes);
            this.panel1.Controls.Add(this.btnConsumoEmpleados);
            this.panel1.Controls.Add(this.btnEstadisticas);
            this.panel1.Controls.Add(this.btnDomicilios);
            this.panel1.Controls.Add(this.btnLlevar);
            this.panel1.Controls.Add(this.btnMesas);
            this.panel1.Location = new System.Drawing.Point(614, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(729, 632);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::Palatium.Properties.Resources.logo_palatium_rest_2;
            this.pictureBox1.Location = new System.Drawing.Point(141, 528);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(457, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // logo
            // 
            this.logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo.BackgroundImage")));
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logo.Location = new System.Drawing.Point(141, 32);
            this.logo.Margin = new System.Windows.Forms.Padding(2);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(457, 501);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 9;
            this.logo.TabStop = false;
            // 
            // grupoAccesos
            // 
            this.grupoAccesos.Controls.Add(this.btnCobroAlmuerzos);
            this.grupoAccesos.Controls.Add(this.btnCambioOrigen);
            this.grupoAccesos.Controls.Add(this.btnEditarFactura);
            this.grupoAccesos.Controls.Add(this.btnDatosClientes);
            this.grupoAccesos.Controls.Add(this.btnAbrirCajonDinero);
            this.grupoAccesos.Controls.Add(this.btnConsultarPrecios);
            this.grupoAccesos.Controls.Add(this.btnReimprimirFactura);
            this.grupoAccesos.Controls.Add(this.btnCambioCajero);
            this.grupoAccesos.Controls.Add(this.btnAnularFactura);
            this.grupoAccesos.Enabled = false;
            this.grupoAccesos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoAccesos.ForeColor = System.Drawing.Color.OrangeRed;
            this.grupoAccesos.Location = new System.Drawing.Point(22, 9);
            this.grupoAccesos.Name = "grupoAccesos";
            this.grupoAccesos.Size = new System.Drawing.Size(100, 635);
            this.grupoAccesos.TabIndex = 42;
            this.grupoAccesos.TabStop = false;
            this.grupoAccesos.Text = "Accesos";
            // 
            // btnReimprimirFactura
            // 
            this.btnReimprimirFactura.BackColor = System.Drawing.Color.Navy;
            this.btnReimprimirFactura.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReimprimirFactura.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReimprimirFactura.FlatAppearance.BorderSize = 2;
            this.btnReimprimirFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReimprimirFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReimprimirFactura.ForeColor = System.Drawing.Color.White;
            this.btnReimprimirFactura.Image = global::Palatium.Properties.Resources.icono_reimprimir;
            this.btnReimprimirFactura.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReimprimirFactura.Location = new System.Drawing.Point(3, 88);
            this.btnReimprimirFactura.Name = "btnReimprimirFactura";
            this.btnReimprimirFactura.Size = new System.Drawing.Size(94, 67);
            this.btnReimprimirFactura.TabIndex = 23;
            this.btnReimprimirFactura.Text = "Reimprimir\r\nFactura";
            this.btnReimprimirFactura.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReimprimirFactura.UseVisualStyleBackColor = false;
            this.btnReimprimirFactura.Click += new System.EventHandler(this.btnReimprimirFactura_Click);
            this.btnReimprimirFactura.MouseEnter += new System.EventHandler(this.btnReimprimirFactura_MouseEnter);
            this.btnReimprimirFactura.MouseLeave += new System.EventHandler(this.btnReimprimirFactura_MouseLeave);
            // 
            // frmMenuPos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1355, 668);
            this.ControlBox = false;
            this.Controls.Add(this.grupoAccesos);
            this.Controls.Add(this.btnAcerca);
            this.Controls.Add(this.lblContacto);
            this.Controls.Add(this.lblSitioWeb);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRepartidorExterno);
            this.Controls.Add(this.btnFuncionarios);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMenuPos";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMenuPos_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMenuPos_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.grupoAccesos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnOficina;
        private System.Windows.Forms.Button btnMovimientoCaja;
        private System.Windows.Forms.Button btnSalidaCajero;
        private System.Windows.Forms.Button btnEntradaCajero;
        private System.Windows.Forms.Button btnReabrirCaja;
        private System.Windows.Forms.Button btnFacturasSri;
        private System.Windows.Forms.Button btnClienteEmpresarial;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRevisar;
        private System.Windows.Forms.Button btnCortesias;
        private System.Windows.Forms.Button btnCanjes;
        private System.Windows.Forms.Button btnConsumoEmpleados;
        private System.Windows.Forms.Button btnFuncionarios;
        private System.Windows.Forms.Button btnRepartidorExterno;
        private System.Windows.Forms.Button btnEstadisticas;
        private System.Windows.Forms.Button btnDomicilios;
        private System.Windows.Forms.Button btnLlevar;
        private System.Windows.Forms.Button btnMesas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lblSitioWeb;
        private System.Windows.Forms.Label lblContacto;
        private System.Windows.Forms.LinkLabel btnAcerca;
        private System.Windows.Forms.GroupBox grupoAccesos;
        private System.Windows.Forms.Button btnCobroAlmuerzos;
        private System.Windows.Forms.Button btnCambioOrigen;
        private System.Windows.Forms.Button btnEditarFactura;
        private System.Windows.Forms.Button btnDatosClientes;
        private System.Windows.Forms.Button btnAbrirCajonDinero;
        private System.Windows.Forms.Button btnConsultarPrecios;
        private System.Windows.Forms.Button btnReimprimirFactura;
        private System.Windows.Forms.Button btnCambioCajero;
        private System.Windows.Forms.Button btnAnularFactura;
        private System.Windows.Forms.Button btnVentaExpress;
        private System.Windows.Forms.Button btnTarjetaAlmuerzo;
    }
}