namespace Palatium.Facturador
{
    partial class frmBuscarTicket
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.rbdFactura = new System.Windows.Forms.RadioButton();
            this.rbdTicket = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.grupoDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(27, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Ingrese Número:";
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(175, 112);
            this.txtBuscar.MaxLength = 13;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(148, 26);
            this.txtBuscar.TabIndex = 11;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.rbdFactura);
            this.grupoDatos.Controls.Add(this.rbdTicket);
            this.grupoDatos.Controls.Add(this.btnBuscar);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtBuscar);
            this.grupoDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.grupoDatos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.grupoDatos.Location = new System.Drawing.Point(24, 21);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(410, 179);
            this.grupoDatos.TabIndex = 27;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Búsqueda por Número de Ticket";
            // 
            // rbdFactura
            // 
            this.rbdFactura.AutoSize = true;
            this.rbdFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.rbdFactura.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdFactura.Location = new System.Drawing.Point(221, 55);
            this.rbdFactura.Name = "rbdFactura";
            this.rbdFactura.Size = new System.Drawing.Size(121, 24);
            this.rbdFactura.TabIndex = 28;
            this.rbdFactura.Text = "Por Factura";
            this.rbdFactura.UseVisualStyleBackColor = true;
            this.rbdFactura.CheckedChanged += new System.EventHandler(this.rbdFactura_CheckedChanged);
            // 
            // rbdTicket
            // 
            this.rbdTicket.AutoSize = true;
            this.rbdTicket.Checked = true;
            this.rbdTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.rbdTicket.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.rbdTicket.Location = new System.Drawing.Point(31, 55);
            this.rbdTicket.Name = "rbdTicket";
            this.rbdTicket.Size = new System.Drawing.Size(161, 24);
            this.rbdTicket.TabIndex = 27;
            this.rbdTicket.TabStop = true;
            this.rbdTicket.Text = "Por N° de  Orden";
            this.rbdTicket.UseVisualStyleBackColor = true;
            this.rbdTicket.CheckedChanged += new System.EventHandler(this.rbdTicket_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBuscar.Location = new System.Drawing.Point(338, 103);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(49, 44);
            this.btnBuscar.TabIndex = 26;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // frmBuscarTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(462, 222);
            this.Controls.Add(this.grupoDatos);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBuscarTicket";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Búsqueda de Orden";
            this.Load += new System.EventHandler(this.frmBuscarTicket_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBuscarTicket_KeyDown);
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.RadioButton rbdFactura;
        private System.Windows.Forms.RadioButton rbdTicket;
    }
}