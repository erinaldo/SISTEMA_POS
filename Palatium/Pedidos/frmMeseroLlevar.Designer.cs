namespace Palatium.Pedidos
{
    partial class frmMeseroLlevar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeseroLlevar));
            this.lblMesero = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMeseros = new System.Windows.Forms.Panel();
            this.btcancelar = new System.Windows.Forms.Button();
            this.btingresar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMesero
            // 
            this.lblMesero.AutoSize = true;
            this.lblMesero.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesero.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblMesero.Location = new System.Drawing.Point(124, 9);
            this.lblMesero.Name = "lblMesero";
            this.lblMesero.Size = new System.Drawing.Size(98, 24);
            this.lblMesero.TabIndex = 37;
            this.lblMesero.Text = "MESERO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(14, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 24);
            this.label2.TabIndex = 36;
            this.label2.Text = "MESERO:";
            // 
            // pnlMeseros
            // 
            this.pnlMeseros.Location = new System.Drawing.Point(15, 41);
            this.pnlMeseros.Name = "pnlMeseros";
            this.pnlMeseros.Size = new System.Drawing.Size(414, 271);
            this.pnlMeseros.TabIndex = 35;
            // 
            // btcancelar
            // 
            this.btcancelar.BackColor = System.Drawing.Color.Navy;
            this.btcancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btcancelar.FlatAppearance.BorderSize = 2;
            this.btcancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btcancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold);
            this.btcancelar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btcancelar.Image = ((System.Drawing.Image)(resources.GetObject("btcancelar.Image")));
            this.btcancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcancelar.Location = new System.Drawing.Point(443, 112);
            this.btcancelar.Margin = new System.Windows.Forms.Padding(2);
            this.btcancelar.Name = "btcancelar";
            this.btcancelar.Size = new System.Drawing.Size(124, 58);
            this.btcancelar.TabIndex = 38;
            this.btcancelar.Text = "Cancelar";
            this.btcancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btcancelar.UseVisualStyleBackColor = false;
            this.btcancelar.Click += new System.EventHandler(this.btcancelar_Click);
            this.btcancelar.MouseEnter += new System.EventHandler(this.btcancelar_MouseEnter);
            this.btcancelar.MouseLeave += new System.EventHandler(this.btcancelar_MouseLeave);
            // 
            // btingresar
            // 
            this.btingresar.BackColor = System.Drawing.Color.Navy;
            this.btingresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btingresar.FlatAppearance.BorderSize = 2;
            this.btingresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btingresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btingresar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btingresar.Image = global::Palatium.Properties.Resources.visto_3;
            this.btingresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btingresar.Location = new System.Drawing.Point(443, 41);
            this.btingresar.Margin = new System.Windows.Forms.Padding(2);
            this.btingresar.Name = "btingresar";
            this.btingresar.Size = new System.Drawing.Size(124, 58);
            this.btingresar.TabIndex = 39;
            this.btingresar.Text = "Ingresar";
            this.btingresar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btingresar.UseVisualStyleBackColor = false;
            this.btingresar.Click += new System.EventHandler(this.btingresar_Click);
            this.btingresar.MouseEnter += new System.EventHandler(this.btingresar_MouseEnter);
            this.btingresar.MouseLeave += new System.EventHandler(this.btingresar_MouseLeave);
            // 
            // frmMeseroLlevar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(578, 325);
            this.Controls.Add(this.btcancelar);
            this.Controls.Add(this.btingresar);
            this.Controls.Add(this.lblMesero);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlMeseros);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMeseroLlevar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione Mesero - Orden para Llevar";
            this.Load += new System.EventHandler(this.frmMeseroLlevar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMeseroLlevar_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMesero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlMeseros;
        private System.Windows.Forms.Button btcancelar;
        private System.Windows.Forms.Button btingresar;
    }
}