namespace Palatium.Informes
{
    partial class frmFacturaMenuExpress
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
            this.grupoSeleccion = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnHasta = new System.Windows.Forms.Button();
            this.btnDesde = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtHasta = new System.Windows.Forms.TextBox();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.txtDatos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRepartidoresExternos = new MisControles.ComboDatos();
            this.grupoSeleccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupoSeleccion
            // 
            this.grupoSeleccion.Controls.Add(this.cmbRepartidoresExternos);
            this.grupoSeleccion.Controls.Add(this.label3);
            this.grupoSeleccion.Controls.Add(this.btnLimpiar);
            this.grupoSeleccion.Controls.Add(this.btnFacturar);
            this.grupoSeleccion.Controls.Add(this.btnBuscar);
            this.grupoSeleccion.Controls.Add(this.btnHasta);
            this.grupoSeleccion.Controls.Add(this.btnDesde);
            this.grupoSeleccion.Controls.Add(this.label2);
            this.grupoSeleccion.Controls.Add(this.label1);
            this.grupoSeleccion.Controls.Add(this.TxtHasta);
            this.grupoSeleccion.Controls.Add(this.txtDesde);
            this.grupoSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grupoSeleccion.Location = new System.Drawing.Point(31, 21);
            this.grupoSeleccion.Name = "grupoSeleccion";
            this.grupoSeleccion.Size = new System.Drawing.Size(383, 255);
            this.grupoSeleccion.TabIndex = 1;
            this.grupoSeleccion.TabStop = false;
            this.grupoSeleccion.Text = "Filtro de Fechas";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(241, 185);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnFacturar
            // 
            this.btnFacturar.BackColor = System.Drawing.Color.Red;
            this.btnFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturar.ForeColor = System.Drawing.Color.White;
            this.btnFacturar.Location = new System.Drawing.Point(165, 185);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(70, 39);
            this.btnFacturar.TabIndex = 19;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.UseVisualStyleBackColor = false;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Blue;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(89, 185);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(70, 39);
            this.btnBuscar.TabIndex = 18;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnHasta
            // 
            this.btnHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHasta.Location = new System.Drawing.Point(324, 119);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(38, 24);
            this.btnHasta.TabIndex = 5;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.Location = new System.Drawing.Point(324, 81);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(38, 24);
            this.btnDesde.TabIndex = 4;
            this.btnDesde.Text = "...";
            this.btnDesde.UseVisualStyleBackColor = true;
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hasta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "A partir de:";
            // 
            // TxtHasta
            // 
            this.TxtHasta.Enabled = false;
            this.TxtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHasta.Location = new System.Drawing.Point(136, 120);
            this.TxtHasta.Name = "TxtHasta";
            this.TxtHasta.Size = new System.Drawing.Size(182, 22);
            this.TxtHasta.TabIndex = 1;
            // 
            // txtDesde
            // 
            this.txtDesde.Enabled = false;
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(136, 82);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(182, 22);
            this.txtDesde.TabIndex = 0;
            // 
            // txtDatos
            // 
            this.txtDatos.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatos.Location = new System.Drawing.Point(440, 21);
            this.txtDatos.Multiline = true;
            this.txtDatos.Name = "txtDatos";
            this.txtDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDatos.Size = new System.Drawing.Size(324, 438);
            this.txtDatos.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 32);
            this.label3.TabIndex = 21;
            this.label3.Text = "Repartidores\r\nExternos:";
            // 
            // cmbRepartidoresExternos
            // 
            this.cmbRepartidoresExternos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRepartidoresExternos.FormattingEnabled = true;
            this.cmbRepartidoresExternos.Location = new System.Drawing.Point(136, 42);
            this.cmbRepartidoresExternos.Name = "cmbRepartidoresExternos";
            this.cmbRepartidoresExternos.Size = new System.Drawing.Size(226, 24);
            this.cmbRepartidoresExternos.TabIndex = 22;
            // 
            // frmFacturaMenuExpress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(776, 471);
            this.Controls.Add(this.txtDatos);
            this.Controls.Add(this.grupoSeleccion);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFacturaMenuExpress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Factura Menú Express";
            this.Load += new System.EventHandler(this.frmFacturaMenuExpress_Load);
            this.grupoSeleccion.ResumeLayout(false);
            this.grupoSeleccion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoSeleccion;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtHasta;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtDatos;
        private MisControles.ComboDatos cmbRepartidoresExternos;
        private System.Windows.Forms.Label label3;
    }
}