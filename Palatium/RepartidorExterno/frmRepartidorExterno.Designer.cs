namespace Palatium.RepartidorExterno
{
    partial class frmRepartidorExterno
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnHasta = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtHasta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDesde = new System.Windows.Forms.Button();
            this.cmbRepartidoresExternos = new MisControles.ComboDatos();
            this.txtDesde = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDatos = new System.Windows.Forms.TextBox();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(46)))), ((int)(((byte)(59)))));
            this.Panel1.Controls.Add(this.btnLimpiar);
            this.Panel1.Controls.Add(this.btnFacturar);
            this.Panel1.Controls.Add(this.btnBuscar);
            this.Panel1.Controls.Add(this.btnHasta);
            this.Panel1.Controls.Add(this.label3);
            this.Panel1.Controls.Add(this.TxtHasta);
            this.Panel1.Controls.Add(this.label2);
            this.Panel1.Controls.Add(this.btnDesde);
            this.Panel1.Controls.Add(this.cmbRepartidoresExternos);
            this.Panel1.Controls.Add(this.txtDesde);
            this.Panel1.Controls.Add(this.label1);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(270, 532);
            this.Panel1.TabIndex = 2;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(167, 228);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 27;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnFacturar
            // 
            this.btnFacturar.BackColor = System.Drawing.Color.Red;
            this.btnFacturar.Enabled = false;
            this.btnFacturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturar.ForeColor = System.Drawing.Color.White;
            this.btnFacturar.Location = new System.Drawing.Point(89, 228);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(70, 39);
            this.btnFacturar.TabIndex = 26;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.UseVisualStyleBackColor = false;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Blue;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(11, 228);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(70, 39);
            this.btnBuscar.TabIndex = 25;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnHasta
            // 
            this.btnHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHasta.Location = new System.Drawing.Point(203, 180);
            this.btnHasta.Name = "btnHasta";
            this.btnHasta.Size = new System.Drawing.Size(38, 24);
            this.btnHasta.TabIndex = 11;
            this.btnHasta.Text = "...";
            this.btnHasta.UseVisualStyleBackColor = true;
            this.btnHasta.Click += new System.EventHandler(this.btnHasta_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Repartidores Externos:";
            // 
            // TxtHasta
            // 
            this.TxtHasta.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.TxtHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHasta.Location = new System.Drawing.Point(12, 180);
            this.TxtHasta.Name = "TxtHasta";
            this.TxtHasta.ReadOnly = true;
            this.TxtHasta.Size = new System.Drawing.Size(182, 22);
            this.TxtHasta.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(12, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hasta:";
            // 
            // btnDesde
            // 
            this.btnDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesde.Location = new System.Drawing.Point(203, 128);
            this.btnDesde.Name = "btnDesde";
            this.btnDesde.Size = new System.Drawing.Size(38, 24);
            this.btnDesde.TabIndex = 10;
            this.btnDesde.Text = "...";
            this.btnDesde.UseVisualStyleBackColor = true;
            this.btnDesde.Click += new System.EventHandler(this.btnDesde_Click);
            // 
            // cmbRepartidoresExternos
            // 
            this.cmbRepartidoresExternos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRepartidoresExternos.FormattingEnabled = true;
            this.cmbRepartidoresExternos.Location = new System.Drawing.Point(12, 50);
            this.cmbRepartidoresExternos.Name = "cmbRepartidoresExternos";
            this.cmbRepartidoresExternos.Size = new System.Drawing.Size(226, 24);
            this.cmbRepartidoresExternos.TabIndex = 24;
            this.cmbRepartidoresExternos.SelectedIndexChanged += new System.EventHandler(this.cmbRepartidoresExternos_SelectedIndexChanged);
            // 
            // txtDesde
            // 
            this.txtDesde.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesde.Location = new System.Drawing.Point(12, 129);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.ReadOnly = true;
            this.txtDesde.Size = new System.Drawing.Size(182, 22);
            this.txtDesde.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "A partir de:";
            // 
            // txtDatos
            // 
            this.txtDatos.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtDatos.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtDatos.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDatos.Location = new System.Drawing.Point(276, 0);
            this.txtDatos.Multiline = true;
            this.txtDatos.Name = "txtDatos";
            this.txtDatos.ReadOnly = true;
            this.txtDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDatos.Size = new System.Drawing.Size(366, 532);
            this.txtDatos.TabIndex = 5;
            // 
            // frmRepartidorExterno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 532);
            this.Controls.Add(this.txtDatos);
            this.Controls.Add(this.Panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRepartidorExterno";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generar Facturas de Repartidores Externos";
            this.Load += new System.EventHandler(this.frmRepartidorExterno_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Label label3;
        private MisControles.ComboDatos cmbRepartidoresExternos;
        private System.Windows.Forms.Button btnHasta;
        private System.Windows.Forms.TextBox TxtHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDesde;
        private System.Windows.Forms.TextBox txtDesde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtDatos;
    }
}