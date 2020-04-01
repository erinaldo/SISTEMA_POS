namespace Palatium.Pedidos
{
    partial class frmVersionesCocina
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
            this.btnComandaCompleta = new DevComponents.DotNetBar.ButtonX();
            this.btnImprimirComandaCompleta = new DevComponents.DotNetBar.ButtonX();
            this.cmbVersiones = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImprimirComanda = new DevComponents.DotNetBar.ButtonX();
            this.txtReporte = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnComandaCompleta
            // 
            this.btnComandaCompleta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnComandaCompleta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnComandaCompleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComandaCompleta.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnComandaCompleta.Location = new System.Drawing.Point(12, 39);
            this.btnComandaCompleta.Name = "btnComandaCompleta";
            this.btnComandaCompleta.Size = new System.Drawing.Size(192, 75);
            this.btnComandaCompleta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnComandaCompleta.TabIndex = 1;
            this.btnComandaCompleta.Text = "Ver Comanda Completa";
            this.btnComandaCompleta.Click += new System.EventHandler(this.btnComandaCompleta_Click);
            // 
            // btnImprimirComandaCompleta
            // 
            this.btnImprimirComandaCompleta.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImprimirComandaCompleta.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImprimirComandaCompleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirComandaCompleta.Image = global::Palatium.Properties.Resources.impresora_icono;
            this.btnImprimirComandaCompleta.Location = new System.Drawing.Point(222, 39);
            this.btnImprimirComandaCompleta.Name = "btnImprimirComandaCompleta";
            this.btnImprimirComandaCompleta.Size = new System.Drawing.Size(192, 75);
            this.btnImprimirComandaCompleta.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImprimirComandaCompleta.TabIndex = 2;
            this.btnImprimirComandaCompleta.Text = "Imprimir Comanda Completa";
            this.btnImprimirComandaCompleta.Click += new System.EventHandler(this.btnImprimirComandaCompleta_Click);
            // 
            // cmbVersiones
            // 
            this.cmbVersiones.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cmbVersiones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersiones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVersiones.FormattingEnabled = true;
            this.cmbVersiones.Location = new System.Drawing.Point(36, 51);
            this.cmbVersiones.Name = "cmbVersiones";
            this.cmbVersiones.Size = new System.Drawing.Size(336, 28);
            this.cmbVersiones.TabIndex = 3;
            this.cmbVersiones.SelectedIndexChanged += new System.EventHandler(this.cmbVersiones_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnImprimirComanda);
            this.groupBox1.Controls.Add(this.cmbVersiones);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 255);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Seleccione Versión de Impresión de Comanda";
            // 
            // btnImprimirComanda
            // 
            this.btnImprimirComanda.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnImprimirComanda.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnImprimirComanda.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirComanda.Image = global::Palatium.Properties.Resources.impresora_icono;
            this.btnImprimirComanda.Location = new System.Drawing.Point(125, 139);
            this.btnImprimirComanda.Name = "btnImprimirComanda";
            this.btnImprimirComanda.Size = new System.Drawing.Size(160, 75);
            this.btnImprimirComanda.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnImprimirComanda.TabIndex = 4;
            this.btnImprimirComanda.Text = "Imprimir\r\nVersión\r\nComanda";
            this.btnImprimirComanda.Click += new System.EventHandler(this.btnImprimirComanda_Click);
            // 
            // txtReporte
            // 
            this.txtReporte.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtReporte.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporte.Location = new System.Drawing.Point(426, 39);
            this.txtReporte.Multiline = true;
            this.txtReporte.Name = "txtReporte";
            this.txtReporte.ReadOnly = true;
            this.txtReporte.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReporte.Size = new System.Drawing.Size(348, 384);
            this.txtReporte.TabIndex = 5;
            // 
            // frmVersionesCocina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(786, 455);
            this.Controls.Add(this.txtReporte);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnImprimirComandaCompleta);
            this.Controls.Add(this.btnComandaCompleta);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmVersionesCocina";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresiones de comandas";
            this.Load += new System.EventHandler(this.frmVersionesCocina_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmVersionesCocina_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnComandaCompleta;
        private DevComponents.DotNetBar.ButtonX btnImprimirComandaCompleta;
        private System.Windows.Forms.ComboBox cmbVersiones;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnImprimirComanda;
        private System.Windows.Forms.TextBox txtReporte;
    }
}