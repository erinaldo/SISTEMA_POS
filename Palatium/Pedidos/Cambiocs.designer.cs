namespace Palatium
{
    partial class Cambiocs
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
            this.lblCambio = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pgBarra = new System.Windows.Forms.ProgressBar();
            this.timerBarra = new System.Windows.Forms.Timer(this.components);
            this.lblMensaje = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblVerMensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCambio
            // 
            this.lblCambio.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCambio.Location = new System.Drawing.Point(12, 164);
            this.lblCambio.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(436, 108);
            this.lblCambio.TabIndex = 6;
            this.lblCambio.Text = "p";
            this.lblCambio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 109);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(436, 55);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cambio a dar";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = global::Palatium.Properties.Resources.ok2_png;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(181, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(101, 59);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pgBarra
            // 
            this.pgBarra.Location = new System.Drawing.Point(11, 348);
            this.pgBarra.Name = "pgBarra";
            this.pgBarra.Size = new System.Drawing.Size(436, 21);
            this.pgBarra.TabIndex = 8;
            // 
            // timerBarra
            // 
            this.timerBarra.Enabled = true;
            this.timerBarra.Tick += new System.EventHandler(this.timerBarra_Tick);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(11, 371);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(436, 23);
            this.lblMensaje.TabIndex = 9;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblVerMensaje
            // 
            this.lblVerMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerMensaje.Location = new System.Drawing.Point(11, 9);
            this.lblVerMensaje.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVerMensaje.Name = "lblVerMensaje";
            this.lblVerMensaje.Size = new System.Drawing.Size(436, 90);
            this.lblVerMensaje.TabIndex = 10;
            this.lblVerMensaje.Text = "FACTURA GENERADA ÉXITOSAMENTE";
            this.lblVerMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Cambiocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(475, 405);
            this.Controls.Add(this.lblVerMensaje);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.pgBarra);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblCambio);
            this.Controls.Add(this.label3);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cambiocs";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiocs";
            this.Load += new System.EventHandler(this.Cambiocs_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ProgressBar pgBarra;
        private System.Windows.Forms.Timer timerBarra;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label lblVerMensaje;

    }
}