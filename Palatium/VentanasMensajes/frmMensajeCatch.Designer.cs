namespace Palatium.VentanasMensajes
{
    partial class frmMensajeCatch
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
            this.btnContinuar = new System.Windows.Forms.Button();
            this.LblMensaje = new System.Windows.Forms.Label();
            this.btnCopiar = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnContinuar
            // 
            this.btnContinuar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinuar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.btnContinuar.Image = global::Palatium.Properties.Resources.ok_png;
            this.btnContinuar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnContinuar.Location = new System.Drawing.Point(252, 288);
            this.btnContinuar.Margin = new System.Windows.Forms.Padding(2);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(159, 104);
            this.btnContinuar.TabIndex = 14;
            this.btnContinuar.Text = "OK";
            this.btnContinuar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // LblMensaje
            // 
            this.LblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LblMensaje.Location = new System.Drawing.Point(36, 23);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(596, 253);
            this.LblMensaje.TabIndex = 13;
            this.LblMensaje.Text = "label1";
            this.LblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCopiar
            // 
            this.btnCopiar.AutoSize = true;
            this.btnCopiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopiar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCopiar.Location = new System.Drawing.Point(470, 368);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(162, 24);
            this.btnCopiar.TabIndex = 15;
            this.btnCopiar.TabStop = true;
            this.btnCopiar.Text = "Copiar el mensaje";
            this.btnCopiar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCopiar_LinkClicked);
            // 
            // frmMensajeCatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(666, 410);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.LblMensaje);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMensajeCatch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMensajeCatch_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinuar;
        public System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.LinkLabel btnCopiar;
    }
}