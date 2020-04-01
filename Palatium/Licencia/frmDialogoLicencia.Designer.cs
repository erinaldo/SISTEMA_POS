namespace Palatium.Licencia
{
    partial class frmDialogoLicencia
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
            this.btnActivar = new System.Windows.Forms.Button();
            this.lblRestantes = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEjecutarDemo = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPegar = new System.Windows.Forms.Button();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.txtID_1 = new System.Windows.Forms.TextBox();
            this.txtPass_5 = new System.Windows.Forms.TextBox();
            this.txtID_2 = new System.Windows.Forms.TextBox();
            this.txtPass_4 = new System.Windows.Forms.TextBox();
            this.txtID_3 = new System.Windows.Forms.TextBox();
            this.txtPass_3 = new System.Windows.Forms.TextBox();
            this.txtID_4 = new System.Windows.Forms.TextBox();
            this.txtPass_2 = new System.Windows.Forms.TextBox();
            this.txtID_5 = new System.Windows.Forms.TextBox();
            this.txtPass_1 = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnActivar
            // 
            this.btnActivar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnActivar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnActivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActivar.Image = global::Palatium.Properties.Resources.icono_Activacion;
            this.btnActivar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActivar.Location = new System.Drawing.Point(101, 220);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(122, 56);
            this.btnActivar.TabIndex = 18;
            this.btnActivar.Text = "Activar";
            this.btnActivar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttMensaje.SetToolTip(this.btnActivar, "Clic aquí para realizar la activación del producto");
            this.btnActivar.UseVisualStyleBackColor = false;
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            // 
            // lblRestantes
            // 
            this.lblRestantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestantes.ForeColor = System.Drawing.Color.Red;
            this.lblRestantes.Location = new System.Drawing.Point(15, 62);
            this.lblRestantes.Name = "lblRestantes";
            this.lblRestantes.Size = new System.Drawing.Size(200, 27);
            this.lblRestantes.TabIndex = 20;
            this.lblRestantes.Text = "50";
            this.lblRestantes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Transacciones restantes";
            // 
            // btnEjecutarDemo
            // 
            this.btnEjecutarDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEjecutarDemo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnEjecutarDemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEjecutarDemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEjecutarDemo.Image = global::Palatium.Properties.Resources.icono_demo_aplicacion;
            this.btnEjecutarDemo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEjecutarDemo.Location = new System.Drawing.Point(231, 33);
            this.btnEjecutarDemo.Name = "btnEjecutarDemo";
            this.btnEjecutarDemo.Size = new System.Drawing.Size(134, 56);
            this.btnEjecutarDemo.TabIndex = 19;
            this.btnEjecutarDemo.Text = "Ejecutar Demo";
            this.btnEjecutarDemo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ttMensaje.SetToolTip(this.btnEjecutarDemo, "Clic aquí para ejecutar la versión demo del sistema");
            this.btnEjecutarDemo.UseVisualStyleBackColor = false;
            this.btnEjecutarDemo.Click += new System.EventHandler(this.btnEjecutarDemo_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox2.Controls.Add(this.lblRestantes);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnEjecutarDemo);
            this.groupBox2.Location = new System.Drawing.Point(386, 258);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 108);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Versión Demo (Trial)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(19, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 15);
            this.label4.TabIndex = 17;
            this.label4.Text = "SERIAL DE ACTIVACIÓN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(23, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(293, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "ID: Campo requerido para la activación del producto.";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(301, 88);
            this.label1.TabIndex = 14;
            this.label1.Text = "Comuníquese con su proveedor de sistema, para que puede facilitar el serial de la" +
    " aplicación.\r\n\r\nSe solicita otorgar la identificación del sistema y se le otorga" +
    "rá la credencial.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Palatium.Properties.Resources.fondo_nombre_sistema;
            this.pictureBox1.Location = new System.Drawing.Point(386, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(371, 205);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.btnPegar);
            this.groupBox1.Controls.Add(this.btnCopiar);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnActivar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtID_1);
            this.groupBox1.Controls.Add(this.txtPass_5);
            this.groupBox1.Controls.Add(this.txtID_2);
            this.groupBox1.Controls.Add(this.txtPass_4);
            this.groupBox1.Controls.Add(this.txtID_3);
            this.groupBox1.Controls.Add(this.txtPass_3);
            this.groupBox1.Controls.Add(this.txtID_4);
            this.groupBox1.Controls.Add(this.txtPass_2);
            this.groupBox1.Controls.Add(this.txtID_5);
            this.groupBox1.Controls.Add(this.txtPass_1);
            this.groupBox1.Location = new System.Drawing.Point(12, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 284);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información del Registro";
            // 
            // btnPegar
            // 
            this.btnPegar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPegar.FlatAppearance.BorderSize = 0;
            this.btnPegar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPegar.ForeColor = System.Drawing.Color.Transparent;
            this.btnPegar.Image = global::Palatium.Properties.Resources.icono_pegar_id;
            this.btnPegar.Location = new System.Drawing.Point(316, 183);
            this.btnPegar.Name = "btnPegar";
            this.btnPegar.Size = new System.Drawing.Size(25, 22);
            this.btnPegar.TabIndex = 21;
            this.ttMensaje.SetToolTip(this.btnPegar, "Clic aquí para pegar el Serial");
            this.btnPegar.UseVisualStyleBackColor = true;
            this.btnPegar.Click += new System.EventHandler(this.btnPegar_Click);
            // 
            // btnCopiar
            // 
            this.btnCopiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCopiar.FlatAppearance.BorderSize = 0;
            this.btnCopiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCopiar.Image = global::Palatium.Properties.Resources.icono_copiar_id;
            this.btnCopiar.Location = new System.Drawing.Point(316, 138);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(25, 22);
            this.btnCopiar.TabIndex = 20;
            this.ttMensaje.SetToolTip(this.btnCopiar, "Clic aquí para copiar el ID del equipo");
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // txtID_1
            // 
            this.txtID_1.BackColor = System.Drawing.SystemColors.Control;
            this.txtID_1.Enabled = false;
            this.txtID_1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_1.Location = new System.Drawing.Point(18, 140);
            this.txtID_1.MaxLength = 5;
            this.txtID_1.Name = "txtID_1";
            this.txtID_1.Size = new System.Drawing.Size(54, 22);
            this.txtID_1.TabIndex = 0;
            this.txtID_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass_5
            // 
            this.txtPass_5.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPass_5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_5.Location = new System.Drawing.Point(258, 183);
            this.txtPass_5.MaxLength = 5;
            this.txtPass_5.Name = "txtPass_5";
            this.txtPass_5.Size = new System.Drawing.Size(54, 22);
            this.txtPass_5.TabIndex = 11;
            this.txtPass_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtID_2
            // 
            this.txtID_2.BackColor = System.Drawing.SystemColors.Control;
            this.txtID_2.Enabled = false;
            this.txtID_2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_2.Location = new System.Drawing.Point(78, 140);
            this.txtID_2.MaxLength = 5;
            this.txtID_2.Name = "txtID_2";
            this.txtID_2.Size = new System.Drawing.Size(54, 22);
            this.txtID_2.TabIndex = 3;
            this.txtID_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass_4
            // 
            this.txtPass_4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPass_4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_4.Location = new System.Drawing.Point(198, 183);
            this.txtPass_4.MaxLength = 5;
            this.txtPass_4.Name = "txtPass_4";
            this.txtPass_4.Size = new System.Drawing.Size(54, 22);
            this.txtPass_4.TabIndex = 10;
            this.txtPass_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtID_3
            // 
            this.txtID_3.BackColor = System.Drawing.SystemColors.Control;
            this.txtID_3.Enabled = false;
            this.txtID_3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_3.Location = new System.Drawing.Point(138, 140);
            this.txtID_3.MaxLength = 5;
            this.txtID_3.Name = "txtID_3";
            this.txtID_3.Size = new System.Drawing.Size(54, 22);
            this.txtID_3.TabIndex = 4;
            this.txtID_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass_3
            // 
            this.txtPass_3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPass_3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_3.Location = new System.Drawing.Point(138, 183);
            this.txtPass_3.MaxLength = 5;
            this.txtPass_3.Name = "txtPass_3";
            this.txtPass_3.Size = new System.Drawing.Size(54, 22);
            this.txtPass_3.TabIndex = 9;
            this.txtPass_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtID_4
            // 
            this.txtID_4.BackColor = System.Drawing.SystemColors.Control;
            this.txtID_4.Enabled = false;
            this.txtID_4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_4.Location = new System.Drawing.Point(198, 140);
            this.txtID_4.MaxLength = 5;
            this.txtID_4.Name = "txtID_4";
            this.txtID_4.Size = new System.Drawing.Size(54, 22);
            this.txtID_4.TabIndex = 5;
            this.txtID_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass_2
            // 
            this.txtPass_2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPass_2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_2.Location = new System.Drawing.Point(78, 183);
            this.txtPass_2.MaxLength = 5;
            this.txtPass_2.Name = "txtPass_2";
            this.txtPass_2.Size = new System.Drawing.Size(54, 22);
            this.txtPass_2.TabIndex = 8;
            this.txtPass_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtID_5
            // 
            this.txtID_5.BackColor = System.Drawing.SystemColors.Control;
            this.txtID_5.Enabled = false;
            this.txtID_5.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID_5.Location = new System.Drawing.Point(258, 140);
            this.txtID_5.MaxLength = 5;
            this.txtID_5.Name = "txtID_5";
            this.txtID_5.Size = new System.Drawing.Size(54, 22);
            this.txtID_5.TabIndex = 6;
            this.txtID_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPass_1
            // 
            this.txtPass_1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPass_1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass_1.Location = new System.Drawing.Point(18, 183);
            this.txtPass_1.MaxLength = 5;
            this.txtPass_1.Name = "txtPass_1";
            this.txtPass_1.Size = new System.Drawing.Size(54, 22);
            this.txtPass_1.TabIndex = 7;
            this.txtPass_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPass_1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPass_1_KeyDown);
            // 
            // lblComment
            // 
            this.lblComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(12, 9);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(337, 70);
            this.lblComment.TabIndex = 16;
            this.lblComment.Text = "Para usar esta aplicación, debes comprarla.\r\n\r\nAntes de comprar, puede ejecutar l" +
    "a aplicación como prueba. para su conocimiento y adaptación de la misma.";
            // 
            // frmDialogoLicencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(769, 370);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblComment);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDialogoLicencia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuadro de diálogo de Activación del Producto";
            this.Load += new System.EventHandler(this.frmDialogoLicencia_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Label lblRestantes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEjecutarDemo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtID_1;
        private System.Windows.Forms.TextBox txtPass_5;
        private System.Windows.Forms.TextBox txtID_2;
        private System.Windows.Forms.TextBox txtPass_4;
        private System.Windows.Forms.TextBox txtID_3;
        private System.Windows.Forms.TextBox txtPass_3;
        private System.Windows.Forms.TextBox txtID_4;
        private System.Windows.Forms.TextBox txtPass_2;
        private System.Windows.Forms.TextBox txtID_5;
        private System.Windows.Forms.TextBox txtPass_1;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnPegar;
        private System.Windows.Forms.Button btnCopiar;
    }
}