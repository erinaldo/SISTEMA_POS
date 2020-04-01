namespace Palatium.Receta
{
    partial class frmIngresarIngrediente
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
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbEquivalencias = new System.Windows.Forms.RadioButton();
            this.rdbUnidades = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbEquivalencias = new MisControles.ComboDatos();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbUnidades = new MisControles.ComboDatos();
            this.cmbPorciones = new MisControles.ComboDatos();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCostoUnitario = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCantidadNeta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCantidadBruta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dBAyudaMateria = new Controles.Auxiliares.DB_Ayuda();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtImporteTotal = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRendimiento = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.grupoDatos.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.groupBox2);
            this.grupoDatos.Controls.Add(this.cmbPorciones);
            this.grupoDatos.Controls.Add(this.label6);
            this.grupoDatos.Controls.Add(this.txtCostoUnitario);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.txtCantidadNeta);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtCantidadBruta);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.dBAyudaMateria);
            this.grupoDatos.Location = new System.Drawing.Point(12, 6);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(554, 256);
            this.grupoDatos.TabIndex = 1;
            this.grupoDatos.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbEquivalencias);
            this.groupBox2.Controls.Add(this.rdbUnidades);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cmbEquivalencias);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cmbUnidades);
            this.groupBox2.Location = new System.Drawing.Point(24, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(509, 107);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            // 
            // rdbEquivalencias
            // 
            this.rdbEquivalencias.AutoSize = true;
            this.rdbEquivalencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbEquivalencias.Location = new System.Drawing.Point(274, 20);
            this.rdbEquivalencias.Name = "rdbEquivalencias";
            this.rdbEquivalencias.Size = new System.Drawing.Size(176, 20);
            this.rdbEquivalencias.TabIndex = 16;
            this.rdbEquivalencias.Text = "Utilizar Equivalencias";
            this.rdbEquivalencias.UseVisualStyleBackColor = true;
            this.rdbEquivalencias.CheckedChanged += new System.EventHandler(this.rdbEquivalencias_CheckedChanged);
            // 
            // rdbUnidades
            // 
            this.rdbUnidades.AutoSize = true;
            this.rdbUnidades.Checked = true;
            this.rdbUnidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbUnidades.Location = new System.Drawing.Point(18, 20);
            this.rdbUnidades.Name = "rdbUnidades";
            this.rdbUnidades.Size = new System.Drawing.Size(145, 20);
            this.rdbUnidades.TabIndex = 15;
            this.rdbUnidades.TabStop = true;
            this.rdbUnidades.Text = "Utilizar Unidades";
            this.rdbUnidades.UseVisualStyleBackColor = true;
            this.rdbUnidades.CheckedChanged += new System.EventHandler(this.rdbUnidades_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label10.Location = new System.Drawing.Point(274, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "Equivalencias";
            // 
            // cmbEquivalencias
            // 
            this.cmbEquivalencias.Enabled = false;
            this.cmbEquivalencias.FormattingEnabled = true;
            this.cmbEquivalencias.Location = new System.Drawing.Point(274, 71);
            this.cmbEquivalencias.Name = "cmbEquivalencias";
            this.cmbEquivalencias.Size = new System.Drawing.Size(208, 21);
            this.cmbEquivalencias.TabIndex = 13;
            this.cmbEquivalencias.SelectedIndexChanged += new System.EventHandler(this.cmbEquivalencias_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label9.Location = new System.Drawing.Point(18, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "Unidades";
            // 
            // cmbUnidades
            // 
            this.cmbUnidades.FormattingEnabled = true;
            this.cmbUnidades.Location = new System.Drawing.Point(18, 71);
            this.cmbUnidades.Name = "cmbUnidades";
            this.cmbUnidades.Size = new System.Drawing.Size(208, 21);
            this.cmbUnidades.TabIndex = 11;
            this.cmbUnidades.SelectedIndexChanged += new System.EventHandler(this.cmbUnidades_SelectedIndexChanged);
            // 
            // cmbPorciones
            // 
            this.cmbPorciones.FormattingEnabled = true;
            this.cmbPorciones.Location = new System.Drawing.Point(365, 218);
            this.cmbPorciones.Name = "cmbPorciones";
            this.cmbPorciones.Size = new System.Drawing.Size(168, 21);
            this.cmbPorciones.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label6.Location = new System.Drawing.Point(362, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Porciones:";
            // 
            // txtCostoUnitario
            // 
            this.txtCostoUnitario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCostoUnitario.Location = new System.Drawing.Point(248, 218);
            this.txtCostoUnitario.Multiline = true;
            this.txtCostoUnitario.Name = "txtCostoUnitario";
            this.txtCostoUnitario.Size = new System.Drawing.Size(96, 20);
            this.txtCostoUnitario.TabIndex = 8;
            this.txtCostoUnitario.Text = "0.00";
            this.txtCostoUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCostoUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCostoUnitario_KeyPress);
            this.txtCostoUnitario.Leave += new System.EventHandler(this.txtCostoUnitario_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.Location = new System.Drawing.Point(245, 199);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Costo Unitario:";
            // 
            // txtCantidadNeta
            // 
            this.txtCantidadNeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidadNeta.Location = new System.Drawing.Point(135, 218);
            this.txtCantidadNeta.Multiline = true;
            this.txtCantidadNeta.Name = "txtCantidadNeta";
            this.txtCantidadNeta.Size = new System.Drawing.Size(96, 20);
            this.txtCantidadNeta.TabIndex = 6;
            this.txtCantidadNeta.Text = "1";
            this.txtCantidadNeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidadNeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadNeta_KeyPress);
            this.txtCantidadNeta.Leave += new System.EventHandler(this.txtCantidadNeta_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.Location = new System.Drawing.Point(132, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cantidad Neta:";
            // 
            // txtCantidadBruta
            // 
            this.txtCantidadBruta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtCantidadBruta.Location = new System.Drawing.Point(21, 218);
            this.txtCantidadBruta.Multiline = true;
            this.txtCantidadBruta.Name = "txtCantidadBruta";
            this.txtCantidadBruta.Size = new System.Drawing.Size(96, 20);
            this.txtCantidadBruta.TabIndex = 4;
            this.txtCantidadBruta.Text = "1";
            this.txtCantidadBruta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidadBruta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadBruta_KeyPress);
            this.txtCantidadBruta.Leave += new System.EventHandler(this.txtCantidadBruta_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label2.Location = new System.Drawing.Point(18, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cantidad Bruta:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(21, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Seleccione el ingrediente (Materia Prima)";
            // 
            // dBAyudaMateria
            // 
            this.dBAyudaMateria.iId = 0;
            this.dBAyudaMateria.Location = new System.Drawing.Point(21, 43);
            this.dBAyudaMateria.Name = "dBAyudaMateria";
            this.dBAyudaMateria.sCodigo = null;
            this.dBAyudaMateria.Size = new System.Drawing.Size(478, 22);
            this.dBAyudaMateria.sNombre = null;
            this.dBAyudaMateria.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtImporteTotal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtRendimiento);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(572, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(175, 141);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            // 
            // txtImporteTotal
            // 
            this.txtImporteTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtImporteTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImporteTotal.Location = new System.Drawing.Point(20, 101);
            this.txtImporteTotal.Multiline = true;
            this.txtImporteTotal.Name = "txtImporteTotal";
            this.txtImporteTotal.Size = new System.Drawing.Size(130, 20);
            this.txtImporteTotal.TabIndex = 30;
            this.txtImporteTotal.Text = "0.00";
            this.txtImporteTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtImporteTotal.Leave += new System.EventHandler(this.txtImporteTotal_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 16);
            this.label7.TabIndex = 29;
            this.label7.Text = "IMPORTE TOTAL:";
            // 
            // txtRendimiento
            // 
            this.txtRendimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtRendimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRendimiento.Location = new System.Drawing.Point(20, 37);
            this.txtRendimiento.Multiline = true;
            this.txtRendimiento.Name = "txtRendimiento";
            this.txtRendimiento.ReadOnly = true;
            this.txtRendimiento.Size = new System.Drawing.Size(130, 20);
            this.txtRendimiento.TabIndex = 28;
            this.txtRendimiento.Text = "0.00";
            this.txtRendimiento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 16);
            this.label8.TabIndex = 27;
            this.label8.Text = "RENDIMIENTO:";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.Red;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(671, 205);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(76, 39);
            this.btnSalir.TabIndex = 26;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(572, 205);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnAceptar.Size = new System.Drawing.Size(76, 39);
            this.btnAceptar.TabIndex = 25;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // frmIngresarIngrediente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(761, 283);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnSalir);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIngresarIngrediente";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingresar Ingrediente";
            this.Load += new System.EventHandler(this.frmIngresarIngrediente_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmIngresarIngrediente_KeyDown);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtCantidadNeta;
        public System.Windows.Forms.TextBox txtCantidadBruta;
        public System.Windows.Forms.TextBox txtCostoUnitario;
        public MisControles.ComboDatos cmbPorciones;
        public System.Windows.Forms.TextBox txtImporteTotal;
        public System.Windows.Forms.TextBox txtRendimiento;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbEquivalencias;
        private System.Windows.Forms.RadioButton rdbUnidades;
        private System.Windows.Forms.Label label10;
        public MisControles.ComboDatos cmbEquivalencias;
        private System.Windows.Forms.Label label9;
        public MisControles.ComboDatos cmbUnidades;
        private Controles.Auxiliares.DB_Ayuda dBAyudaMateria;
    }
}