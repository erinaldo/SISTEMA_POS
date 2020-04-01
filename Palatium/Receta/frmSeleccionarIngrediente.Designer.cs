namespace Palatium.Receta
{
    partial class frmSeleccionarIngrediente
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtImporteTotal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.dBAyudaMateria = new Controles.Auxiliares.DB_Ayuda();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantidadNeta = new System.Windows.Forms.TextBox();
            this.lblRendimiento = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUnidadConsumo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbPorciones = new MisControles.ComboDatos();
            this.txtCostoUnitario = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCantidadBruta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(517, 288);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAceptar.Size = new System.Drawing.Size(76, 39);
            this.btnAceptar.TabIndex = 5;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtImporteTotal
            // 
            this.txtImporteTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtImporteTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporteTotal.Location = new System.Drawing.Point(17, 37);
            this.txtImporteTotal.Multiline = true;
            this.txtImporteTotal.Name = "txtImporteTotal";
            this.txtImporteTotal.Size = new System.Drawing.Size(130, 20);
            this.txtImporteTotal.TabIndex = 8;
            this.txtImporteTotal.Text = "0.0000";
            this.txtImporteTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "IMPORTE TOTAL:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.txtImporteTotal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(517, 205);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 71);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Red;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(616, 288);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(76, 39);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // dBAyudaMateria
            // 
            this.dBAyudaMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dBAyudaMateria.iId = 0;
            this.dBAyudaMateria.Location = new System.Drawing.Point(11, 28);
            this.dBAyudaMateria.Margin = new System.Windows.Forms.Padding(4);
            this.dBAyudaMateria.Name = "dBAyudaMateria";
            this.dBAyudaMateria.sCodigo = null;
            this.dBAyudaMateria.Size = new System.Drawing.Size(449, 27);
            this.dBAyudaMateria.sNombre = null;
            this.dBAyudaMateria.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCantidadNeta);
            this.groupBox2.Controls.Add(this.lblRendimiento);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblUnidadConsumo);
            this.groupBox2.Controls.Add(this.btnOk);
            this.groupBox2.Controls.Add(this.cmbPorciones);
            this.groupBox2.Controls.Add(this.dBAyudaMateria);
            this.groupBox2.Controls.Add(this.txtCostoUnitario);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCantidadBruta);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.groupBox2.Location = new System.Drawing.Point(12, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 246);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Seleccione el ingrediente (Materia Prima)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.Location = new System.Drawing.Point(35, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 50;
            this.label3.Text = "Cantidad a usar:";
            // 
            // txtCantidadNeta
            // 
            this.txtCantidadNeta.Enabled = false;
            this.txtCantidadNeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidadNeta.Location = new System.Drawing.Point(187, 208);
            this.txtCantidadNeta.Multiline = true;
            this.txtCantidadNeta.Name = "txtCantidadNeta";
            this.txtCantidadNeta.Size = new System.Drawing.Size(96, 20);
            this.txtCantidadNeta.TabIndex = 49;
            this.txtCantidadNeta.Text = "0";
            this.txtCantidadNeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRendimiento
            // 
            this.lblRendimiento.AutoSize = true;
            this.lblRendimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblRendimiento.Location = new System.Drawing.Point(184, 98);
            this.lblRendimiento.Name = "lblRendimiento";
            this.lblRendimiento.Size = new System.Drawing.Size(93, 16);
            this.lblRendimiento.TabIndex = 48;
            this.lblRendimiento.Text = "SIN ASIGNAR";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label5.Location = new System.Drawing.Point(35, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "% de Rendimiento:";
            // 
            // lblUnidadConsumo
            // 
            this.lblUnidadConsumo.AutoSize = true;
            this.lblUnidadConsumo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblUnidadConsumo.Location = new System.Drawing.Point(184, 71);
            this.lblUnidadConsumo.Name = "lblUnidadConsumo";
            this.lblUnidadConsumo.Size = new System.Drawing.Size(93, 16);
            this.lblUnidadConsumo.TabIndex = 47;
            this.lblUnidadConsumo.Text = "SIN ASIGNAR";
            // 
            // btnOk
            // 
            this.btnOk.Image = global::Palatium.Properties.Resources.ok4;
            this.btnOk.Location = new System.Drawing.Point(467, 28);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(27, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbPorciones
            // 
            this.cmbPorciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPorciones.FormattingEnabled = true;
            this.cmbPorciones.Location = new System.Drawing.Point(187, 121);
            this.cmbPorciones.Name = "cmbPorciones";
            this.cmbPorciones.Size = new System.Drawing.Size(235, 23);
            this.cmbPorciones.TabIndex = 4;
            // 
            // txtCostoUnitario
            // 
            this.txtCostoUnitario.Enabled = false;
            this.txtCostoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCostoUnitario.Location = new System.Drawing.Point(187, 156);
            this.txtCostoUnitario.Multiline = true;
            this.txtCostoUnitario.Name = "txtCostoUnitario";
            this.txtCostoUnitario.Size = new System.Drawing.Size(96, 20);
            this.txtCostoUnitario.TabIndex = 42;
            this.txtCostoUnitario.Text = "0.0000";
            this.txtCostoUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(35, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 16);
            this.label1.TabIndex = 45;
            this.label1.Text = "Unidad de Consumo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.Location = new System.Drawing.Point(35, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "Cantidad bruta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.Location = new System.Drawing.Point(36, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 43;
            this.label4.Text = "Precio Unitario:";
            // 
            // txtCantidadBruta
            // 
            this.txtCantidadBruta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidadBruta.Location = new System.Drawing.Point(187, 182);
            this.txtCantidadBruta.Multiline = true;
            this.txtCantidadBruta.Name = "txtCantidadBruta";
            this.txtCantidadBruta.Size = new System.Drawing.Size(96, 20);
            this.txtCantidadBruta.TabIndex = 3;
            this.txtCantidadBruta.Text = "1";
            this.txtCantidadBruta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidadBruta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadBruta_KeyPress_1);
            this.txtCantidadBruta.Leave += new System.EventHandler(this.txtCantidadBruta_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label6.Location = new System.Drawing.Point(36, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 44;
            this.label6.Text = "Porciones:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Palatium.Properties.Resources.ingredientes_agregar;
            this.pictureBox1.Location = new System.Drawing.Point(548, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(116, 104);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // frmSeleccionarIngrediente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(703, 351);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSalir);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSeleccionarIngrediente";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso de ingredientes";
            this.Load += new System.EventHandler(this.frmSeleccionarIngrediente_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSeleccionarIngrediente_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        public System.Windows.Forms.TextBox txtImporteTotal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSalir;
        private Controles.Auxiliares.DB_Ayuda dBAyudaMateria;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label5;
        public MisControles.ComboDatos cmbPorciones;
        public System.Windows.Forms.TextBox txtCostoUnitario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtCantidadBruta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtCantidadNeta;
        public System.Windows.Forms.Label lblUnidadConsumo;
        public System.Windows.Forms.Label lblRendimiento;
    }
}