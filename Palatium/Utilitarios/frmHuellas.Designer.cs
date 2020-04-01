namespace Palatium.Utilitarios
{
    partial class frmHuellas
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
            this.label1 = new System.Windows.Forms.Label();
            this.rdbV9 = new System.Windows.Forms.RadioButton();
            this.rdbV10 = new System.Windows.Forms.RadioButton();
            this.btnConectar = new System.Windows.Forms.Button();
            this.btnDesconectar = new System.Windows.Forms.Button();
            this.huellas_1 = new System.Windows.Forms.PictureBox();
            this.huellas_2 = new System.Windows.Forms.PictureBox();
            this.btnTomar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnComparar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.huellas_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huellas_2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Versión del Sensor de reconocimiento";
            // 
            // rdbV9
            // 
            this.rdbV9.AutoSize = true;
            this.rdbV9.Checked = true;
            this.rdbV9.Location = new System.Drawing.Point(75, 49);
            this.rdbV9.Name = "rdbV9";
            this.rdbV9.Size = new System.Drawing.Size(46, 17);
            this.rdbV9.TabIndex = 1;
            this.rdbV9.TabStop = true;
            this.rdbV9.Text = "v9.0";
            this.rdbV9.UseVisualStyleBackColor = true;
            // 
            // rdbV10
            // 
            this.rdbV10.AutoSize = true;
            this.rdbV10.Location = new System.Drawing.Point(148, 49);
            this.rdbV10.Name = "rdbV10";
            this.rdbV10.Size = new System.Drawing.Size(52, 17);
            this.rdbV10.TabIndex = 2;
            this.rdbV10.Text = "v10.0";
            this.rdbV10.UseVisualStyleBackColor = true;
            // 
            // btnConectar
            // 
            this.btnConectar.Location = new System.Drawing.Point(20, 72);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(122, 37);
            this.btnConectar.TabIndex = 3;
            this.btnConectar.Text = "Conectar sensor";
            this.btnConectar.UseVisualStyleBackColor = true;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // btnDesconectar
            // 
            this.btnDesconectar.Location = new System.Drawing.Point(148, 72);
            this.btnDesconectar.Name = "btnDesconectar";
            this.btnDesconectar.Size = new System.Drawing.Size(122, 37);
            this.btnDesconectar.TabIndex = 4;
            this.btnDesconectar.Text = "Desconectar sensor";
            this.btnDesconectar.UseVisualStyleBackColor = true;
            this.btnDesconectar.Click += new System.EventHandler(this.btnDesconectar_Click);
            // 
            // huellas_1
            // 
            this.huellas_1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.huellas_1.Location = new System.Drawing.Point(23, 117);
            this.huellas_1.Name = "huellas_1";
            this.huellas_1.Size = new System.Drawing.Size(118, 165);
            this.huellas_1.TabIndex = 5;
            this.huellas_1.TabStop = false;
            // 
            // huellas_2
            // 
            this.huellas_2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.huellas_2.Location = new System.Drawing.Point(148, 117);
            this.huellas_2.Name = "huellas_2";
            this.huellas_2.Size = new System.Drawing.Size(118, 165);
            this.huellas_2.TabIndex = 6;
            this.huellas_2.TabStop = false;
            // 
            // btnTomar
            // 
            this.btnTomar.Location = new System.Drawing.Point(148, 288);
            this.btnTomar.Name = "btnTomar";
            this.btnTomar.Size = new System.Drawing.Size(122, 37);
            this.btnTomar.TabIndex = 8;
            this.btnTomar.Text = "Tomar";
            this.btnTomar.UseVisualStyleBackColor = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(20, 288);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(122, 37);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            // 
            // btnComparar
            // 
            this.btnComparar.Location = new System.Drawing.Point(23, 331);
            this.btnComparar.Name = "btnComparar";
            this.btnComparar.Size = new System.Drawing.Size(247, 37);
            this.btnComparar.TabIndex = 9;
            this.btnComparar.Text = "Comparar";
            this.btnComparar.UseVisualStyleBackColor = true;
            this.btnComparar.Click += new System.EventHandler(this.btnComparar_Click);
            // 
            // frmHuellas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 376);
            this.Controls.Add(this.btnComparar);
            this.Controls.Add(this.btnTomar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.huellas_2);
            this.Controls.Add(this.huellas_1);
            this.Controls.Add(this.btnDesconectar);
            this.Controls.Add(this.btnConectar);
            this.Controls.Add(this.rdbV10);
            this.Controls.Add(this.rdbV9);
            this.Controls.Add(this.label1);
            this.Name = "frmHuellas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verificar Huellas";
            this.Load += new System.EventHandler(this.frmHuellas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.huellas_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huellas_2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbV9;
        private System.Windows.Forms.RadioButton rdbV10;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Button btnDesconectar;
        private System.Windows.Forms.PictureBox huellas_1;
        private System.Windows.Forms.PictureBox huellas_2;
        private System.Windows.Forms.Button btnTomar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnComparar;
    }
}