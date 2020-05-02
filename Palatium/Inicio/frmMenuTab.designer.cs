namespace Palatium.Inicio
{
    partial class frmMenuTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenuTab));
            this.pnlContenedor = new System.Windows.Forms.Panel();
            this.stEstado = new System.Windows.Forms.StatusStrip();
            this.lblEtiqueta = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnActivarProducto = new System.Windows.Forms.Button();
            this.btnTarjetasAlmuerzo = new System.Windows.Forms.Button();
            this.btnConfiguracion = new System.Windows.Forms.Button();
            this.btnSincronizarSRI = new System.Windows.Forms.Button();
            this.btnUtilitarios = new System.Windows.Forms.Button();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnComedor = new System.Windows.Forms.Button();
            this.btnRestaurante = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.btnReportesGerenciales = new System.Windows.Forms.Button();
            this.pnlContenedor.SuspendLayout();
            this.stEstado.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContenedor
            // 
            this.pnlContenedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlContenedor.Controls.Add(this.stEstado);
            this.pnlContenedor.Controls.Add(this.panel1);
            this.pnlContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenedor.Location = new System.Drawing.Point(0, 0);
            this.pnlContenedor.Name = "pnlContenedor";
            this.pnlContenedor.Size = new System.Drawing.Size(1360, 749);
            this.pnlContenedor.TabIndex = 1;
            // 
            // stEstado
            // 
            this.stEstado.Dock = System.Windows.Forms.DockStyle.Top;
            this.stEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEtiqueta});
            this.stEstado.Location = new System.Drawing.Point(0, 55);
            this.stEstado.Name = "stEstado";
            this.stEstado.Size = new System.Drawing.Size(1360, 22);
            this.stEstado.TabIndex = 1;
            // 
            // lblEtiqueta
            // 
            this.lblEtiqueta.Name = "lblEtiqueta";
            this.lblEtiqueta.Size = new System.Drawing.Size(97, 17);
            this.lblEtiqueta.Text = "DESCONECTADO";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.btnReportesGerenciales);
            this.panel1.Controls.Add(this.btnActivarProducto);
            this.panel1.Controls.Add(this.btnTarjetasAlmuerzo);
            this.panel1.Controls.Add(this.btnConfiguracion);
            this.panel1.Controls.Add(this.btnSincronizarSRI);
            this.panel1.Controls.Add(this.btnUtilitarios);
            this.panel1.Controls.Add(this.btnCerrarSesion);
            this.panel1.Controls.Add(this.btnCerrar);
            this.panel1.Controls.Add(this.btnComedor);
            this.panel1.Controls.Add(this.btnRestaurante);
            this.panel1.Controls.Add(this.btnInicio);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1360, 55);
            this.panel1.TabIndex = 0;
            // 
            // btnActivarProducto
            // 
            this.btnActivarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnActivarProducto.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnActivarProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnActivarProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnActivarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivarProducto.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivarProducto.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActivarProducto.Image = global::Palatium.Properties.Resources.icono_boton_activar_producto;
            this.btnActivarProducto.Location = new System.Drawing.Point(1084, 0);
            this.btnActivarProducto.Name = "btnActivarProducto";
            this.btnActivarProducto.Size = new System.Drawing.Size(69, 55);
            this.btnActivarProducto.TabIndex = 14;
            this.ttMensaje.SetToolTip(this.btnActivarProducto, "Clic aquí para activar el producto");
            this.btnActivarProducto.UseVisualStyleBackColor = false;
            this.btnActivarProducto.Click += new System.EventHandler(this.btnActivarProducto_Click);
            // 
            // btnTarjetasAlmuerzo
            // 
            this.btnTarjetasAlmuerzo.AccessibleDescription = "0";
            this.btnTarjetasAlmuerzo.BackColor = System.Drawing.Color.Blue;
            this.btnTarjetasAlmuerzo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnTarjetasAlmuerzo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnTarjetasAlmuerzo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTarjetasAlmuerzo.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTarjetasAlmuerzo.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnTarjetasAlmuerzo.Location = new System.Drawing.Point(360, 0);
            this.btnTarjetasAlmuerzo.Name = "btnTarjetasAlmuerzo";
            this.btnTarjetasAlmuerzo.Size = new System.Drawing.Size(195, 55);
            this.btnTarjetasAlmuerzo.TabIndex = 13;
            this.btnTarjetasAlmuerzo.Text = "Tarjetas de Almuerzo";
            this.btnTarjetasAlmuerzo.UseVisualStyleBackColor = false;
            this.btnTarjetasAlmuerzo.Visible = false;
            this.btnTarjetasAlmuerzo.Click += new System.EventHandler(this.btnTarjetasAlmuerzo_Click);
            // 
            // btnConfiguracion
            // 
            this.btnConfiguracion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnConfiguracion.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnConfiguracion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfiguracion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConfiguracion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracion.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfiguracion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConfiguracion.Image = global::Palatium.Properties.Resources.icono_boton_ajustes;
            this.btnConfiguracion.Location = new System.Drawing.Point(1153, 0);
            this.btnConfiguracion.Name = "btnConfiguracion";
            this.btnConfiguracion.Size = new System.Drawing.Size(69, 55);
            this.btnConfiguracion.TabIndex = 12;
            this.ttMensaje.SetToolTip(this.btnConfiguracion, "Clic aquí para abrir la configuración del sistema");
            this.btnConfiguracion.UseVisualStyleBackColor = false;
            this.btnConfiguracion.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // btnSincronizarSRI
            // 
            this.btnSincronizarSRI.AccessibleDescription = "0";
            this.btnSincronizarSRI.BackColor = System.Drawing.Color.Blue;
            this.btnSincronizarSRI.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSincronizarSRI.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSincronizarSRI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSincronizarSRI.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSincronizarSRI.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSincronizarSRI.Location = new System.Drawing.Point(680, 0);
            this.btnSincronizarSRI.Name = "btnSincronizarSRI";
            this.btnSincronizarSRI.Size = new System.Drawing.Size(180, 55);
            this.btnSincronizarSRI.TabIndex = 7;
            this.btnSincronizarSRI.Text = "Sincronización SRI";
            this.btnSincronizarSRI.UseVisualStyleBackColor = false;
            this.btnSincronizarSRI.Visible = false;
            this.btnSincronizarSRI.Click += new System.EventHandler(this.btnSincronizarSRI_Click);
            // 
            // btnUtilitarios
            // 
            this.btnUtilitarios.AccessibleDescription = "0";
            this.btnUtilitarios.BackColor = System.Drawing.Color.Blue;
            this.btnUtilitarios.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnUtilitarios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnUtilitarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUtilitarios.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUtilitarios.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUtilitarios.Location = new System.Drawing.Point(555, 0);
            this.btnUtilitarios.Name = "btnUtilitarios";
            this.btnUtilitarios.Size = new System.Drawing.Size(125, 55);
            this.btnUtilitarios.TabIndex = 8;
            this.btnUtilitarios.Text = "Utilitarios";
            this.btnUtilitarios.UseVisualStyleBackColor = false;
            this.btnUtilitarios.Visible = false;
            this.btnUtilitarios.Click += new System.EventHandler(this.btnUtilitarios_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.Color.Blue;
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrarSesion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCerrarSesion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrarSesion.Image = global::Palatium.Properties.Resources.icono_cerrar_sesion_menu;
            this.btnCerrarSesion.Location = new System.Drawing.Point(1222, 0);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(69, 55);
            this.btnCerrarSesion.TabIndex = 10;
            this.ttMensaje.SetToolTip(this.btnCerrarSesion, "Clic aquí para cerrar sesión");
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Visible = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Blue;
            this.btnCerrar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCerrar.Image = global::Palatium.Properties.Resources.icono_salir_menu;
            this.btnCerrar.Location = new System.Drawing.Point(1291, 0);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(69, 55);
            this.btnCerrar.TabIndex = 11;
            this.ttMensaje.SetToolTip(this.btnCerrar, "Clic aquí para cerrar el sistema");
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnComedor
            // 
            this.btnComedor.AccessibleDescription = "0";
            this.btnComedor.BackColor = System.Drawing.Color.Blue;
            this.btnComedor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnComedor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnComedor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComedor.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComedor.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnComedor.Location = new System.Drawing.Point(235, 0);
            this.btnComedor.Name = "btnComedor";
            this.btnComedor.Size = new System.Drawing.Size(125, 55);
            this.btnComedor.TabIndex = 6;
            this.btnComedor.Text = "Comedores";
            this.btnComedor.UseVisualStyleBackColor = false;
            this.btnComedor.Visible = false;
            this.btnComedor.Click += new System.EventHandler(this.btnComedor_Click);
            // 
            // btnRestaurante
            // 
            this.btnRestaurante.AccessibleDescription = "0";
            this.btnRestaurante.BackColor = System.Drawing.Color.Blue;
            this.btnRestaurante.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRestaurante.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRestaurante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurante.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurante.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnRestaurante.Location = new System.Drawing.Point(110, 0);
            this.btnRestaurante.Name = "btnRestaurante";
            this.btnRestaurante.Size = new System.Drawing.Size(125, 55);
            this.btnRestaurante.TabIndex = 5;
            this.btnRestaurante.Text = "Restaurantes";
            this.btnRestaurante.UseVisualStyleBackColor = false;
            this.btnRestaurante.Visible = false;
            this.btnRestaurante.Click += new System.EventHandler(this.btnRestaurante_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.AccessibleDescription = "1";
            this.btnInicio.BackColor = System.Drawing.Color.Red;
            this.btnInicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnInicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicio.Font = new System.Drawing.Font("Maiandra GD", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnInicio.Location = new System.Drawing.Point(0, 0);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(110, 55);
            this.btnInicio.TabIndex = 2;
            this.btnInicio.Text = "INICIO";
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnReportesGerenciales
            // 
            this.btnReportesGerenciales.BackColor = System.Drawing.Color.Yellow;
            this.btnReportesGerenciales.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnReportesGerenciales.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnReportesGerenciales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnReportesGerenciales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportesGerenciales.Font = new System.Drawing.Font("Maiandra GD", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportesGerenciales.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReportesGerenciales.Image = ((System.Drawing.Image)(resources.GetObject("btnReportesGerenciales.Image")));
            this.btnReportesGerenciales.Location = new System.Drawing.Point(1015, 0);
            this.btnReportesGerenciales.Name = "btnReportesGerenciales";
            this.btnReportesGerenciales.Size = new System.Drawing.Size(69, 55);
            this.btnReportesGerenciales.TabIndex = 15;
            this.ttMensaje.SetToolTip(this.btnReportesGerenciales, "Clic aquí para visualizar reportes");
            this.btnReportesGerenciales.UseVisualStyleBackColor = false;
            this.btnReportesGerenciales.Click += new System.EventHandler(this.btnReportesGerenciales_Click);
            // 
            // frmMenuTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1360, 749);
            this.Controls.Add(this.pnlContenedor);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmMenuTab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MENU PRINCIPAL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMenuTab_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMenuTab_KeyDown);
            this.pnlContenedor.ResumeLayout(false);
            this.pnlContenedor.PerformLayout();
            this.stEstado.ResumeLayout(false);
            this.stEstado.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContenedor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSincronizarSRI;
        private System.Windows.Forms.Button btnUtilitarios;
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnComedor;
        private System.Windows.Forms.Button btnRestaurante;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.StatusStrip stEstado;
        private System.Windows.Forms.ToolStripStatusLabel lblEtiqueta;
        private System.Windows.Forms.Button btnConfiguracion;
        private System.Windows.Forms.Button btnTarjetasAlmuerzo;
        private System.Windows.Forms.Button btnActivarProducto;
        private System.Windows.Forms.Button btnReportesGerenciales;


    }
}