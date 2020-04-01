namespace Palatium.Receta
{
    partial class frmEgresoDeReceta
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
            this.dbAyudaReceta = new Controles.Auxiliares.DB_Ayuda();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFechaAplicacion = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.cmbBodega = new MisControles.ComboDatos();
            this.lblBodega = new System.Windows.Forms.Label();
            this.cmbOficina = new MisControles.ComboDatos();
            this.lblOficina = new System.Windows.Forms.Label();
            this.lblNumeroMovimiento = new System.Windows.Forms.Label();
            this.dB_Ayuda3 = new Controles.Auxiliares.DB_Ayuda();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtNumeroItems = new System.Windows.Forms.TextBox();
            this.txtRefenciaExterna = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dbAyudaReceta
            // 
            this.dbAyudaReceta.iId = 0;
            this.dbAyudaReceta.Location = new System.Drawing.Point(5, 46);
            this.dbAyudaReceta.Name = "dbAyudaReceta";
            this.dbAyudaReceta.sCodigo = null;
            this.dbAyudaReceta.Size = new System.Drawing.Size(351, 21);
            this.dbAyudaReceta.sNombre = null;
            this.dbAyudaReceta.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Seleccionar Receta";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRefenciaExterna);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtFechaAplicacion);
            this.groupBox1.Controls.Add(this.lblFecha);
            this.groupBox1.Controls.Add(this.TxtNumeroItems);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dbAyudaReceta);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 161);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // txtFechaAplicacion
            // 
            this.txtFechaAplicacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtFechaAplicacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFechaAplicacion.Location = new System.Drawing.Point(393, 46);
            this.txtFechaAplicacion.Name = "txtFechaAplicacion";
            this.txtFechaAplicacion.Size = new System.Drawing.Size(77, 20);
            this.txtFechaAplicacion.TabIndex = 6;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(390, 30);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 64;
            this.lblFecha.Text = "Fecha";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGrabar);
            this.groupBox2.Controls.Add(this.btnSalir);
            this.groupBox2.Location = new System.Drawing.Point(426, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 48);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(10, 13);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(107, 23);
            this.btnGrabar.TabIndex = 10;
            this.btnGrabar.Text = "Generar Egreso";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click_1);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.Location = new System.Drawing.Point(123, 13);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(79, 23);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            // 
            // cmbBodega
            // 
            this.cmbBodega.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBodega.Enabled = false;
            this.cmbBodega.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbBodega.FormattingEnabled = true;
            this.cmbBodega.Location = new System.Drawing.Point(152, 27);
            this.cmbBodega.Name = "cmbBodega";
            this.cmbBodega.Size = new System.Drawing.Size(138, 21);
            this.cmbBodega.TabIndex = 2;
            // 
            // lblBodega
            // 
            this.lblBodega.AutoSize = true;
            this.lblBodega.Location = new System.Drawing.Point(149, 11);
            this.lblBodega.Name = "lblBodega";
            this.lblBodega.Size = new System.Drawing.Size(44, 13);
            this.lblBodega.TabIndex = 60;
            this.lblBodega.Text = "Bodega";
            // 
            // cmbOficina
            // 
            this.cmbOficina.BackColor = System.Drawing.SystemColors.Window;
            this.cmbOficina.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbOficina.FormattingEnabled = true;
            this.cmbOficina.Location = new System.Drawing.Point(17, 27);
            this.cmbOficina.Name = "cmbOficina";
            this.cmbOficina.Size = new System.Drawing.Size(129, 21);
            this.cmbOficina.TabIndex = 1;
            this.cmbOficina.SelectedIndexChanged += new System.EventHandler(this.cmbOficina_SelectedIndexChanged);
            // 
            // lblOficina
            // 
            this.lblOficina.AutoSize = true;
            this.lblOficina.Location = new System.Drawing.Point(14, 13);
            this.lblOficina.Name = "lblOficina";
            this.lblOficina.Size = new System.Drawing.Size(71, 13);
            this.lblOficina.TabIndex = 59;
            this.lblOficina.Text = "Oficina/Local";
            // 
            // lblNumeroMovimiento
            // 
            this.lblNumeroMovimiento.AutoSize = true;
            this.lblNumeroMovimiento.Location = new System.Drawing.Point(292, 7);
            this.lblNumeroMovimiento.Name = "lblNumeroMovimiento";
            this.lblNumeroMovimiento.Size = new System.Drawing.Size(81, 13);
            this.lblNumeroMovimiento.TabIndex = 62;
            this.lblNumeroMovimiento.Text = "No. Movimiento";
            // 
            // dB_Ayuda3
            // 
            this.dB_Ayuda3.iId = 0;
            this.dB_Ayuda3.Location = new System.Drawing.Point(293, 25);
            this.dB_Ayuda3.Name = "dB_Ayuda3";
            this.dB_Ayuda3.sCodigo = null;
            this.dB_Ayuda3.Size = new System.Drawing.Size(313, 21);
            this.dB_Ayuda3.sNombre = null;
            this.dB_Ayuda3.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblNumeroMovimiento);
            this.groupBox3.Controls.Add(this.dB_Ayuda3);
            this.groupBox3.Location = new System.Drawing.Point(12, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(626, 60);
            this.groupBox3.TabIndex = 65;
            this.groupBox3.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ingrese el número de Items";
            // 
            // TxtNumeroItems
            // 
            this.TxtNumeroItems.Location = new System.Drawing.Point(5, 109);
            this.TxtNumeroItems.Name = "TxtNumeroItems";
            this.TxtNumeroItems.Size = new System.Drawing.Size(85, 20);
            this.TxtNumeroItems.TabIndex = 7;
            this.TxtNumeroItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNumeroItems.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNumeroItems_KeyPress);
            // 
            // txtRefenciaExterna
            // 
            this.txtRefenciaExterna.Location = new System.Drawing.Point(171, 109);
            this.txtRefenciaExterna.Name = "txtRefenciaExterna";
            this.txtRefenciaExterna.Size = new System.Drawing.Size(85, 20);
            this.txtRefenciaExterna.TabIndex = 66;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(168, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 65;
            this.label4.Text = "Referencia Extena";
            // 
            // frmEgresoDeReceta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 302);
            this.Controls.Add(this.cmbBodega);
            this.Controls.Add(this.lblBodega);
            this.Controls.Add(this.cmbOficina);
            this.Controls.Add(this.lblOficina);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmEgresoDeReceta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Egreso de Receta";
            this.Load += new System.EventHandler(this.frmEgresoDeReceta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controles.Auxiliares.DB_Ayuda dbAyudaReceta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private MisControles.ComboDatos cmbBodega;
        private System.Windows.Forms.Label lblBodega;
        private MisControles.ComboDatos cmbOficina;
        private System.Windows.Forms.Label lblOficina;
        private System.Windows.Forms.Label lblNumeroMovimiento;
        private Controles.Auxiliares.DB_Ayuda dB_Ayuda3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtFechaAplicacion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtRefenciaExterna;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtNumeroItems;
        private System.Windows.Forms.Label label2;
    }
}