namespace Palatium.Áreas
{
    partial class frmCambioMesa
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalirMesa = new System.Windows.Forms.Button();
            this.pnlSeccionMesa = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelMesas = new System.Windows.Forms.Panel();
            this.lblPisos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Navy;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.btnActualizar);
            this.groupBox1.Controls.Add(this.btnSalirMesa);
            this.groupBox1.Controls.Add(this.pnlSeccionMesa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 638);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.AccessibleDescription = "";
            this.btnActualizar.BackColor = System.Drawing.Color.Transparent;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnActualizar.FlatAppearance.BorderSize = 2;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizar.Image = global::Palatium.Properties.Resources.actualizar_png;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActualizar.Location = new System.Drawing.Point(7, 512);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(125, 109);
            this.btnActualizar.TabIndex = 13;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalirMesa
            // 
            this.btnSalirMesa.BackColor = System.Drawing.Color.Transparent;
            this.btnSalirMesa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalirMesa.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSalirMesa.FlatAppearance.BorderSize = 2;
            this.btnSalirMesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalirMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalirMesa.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalirMesa.Image = global::Palatium.Properties.Resources.salir_2;
            this.btnSalirMesa.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalirMesa.Location = new System.Drawing.Point(130, 512);
            this.btnSalirMesa.Name = "btnSalirMesa";
            this.btnSalirMesa.Size = new System.Drawing.Size(125, 109);
            this.btnSalirMesa.TabIndex = 12;
            this.btnSalirMesa.Text = "Salir";
            this.btnSalirMesa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalirMesa.UseVisualStyleBackColor = false;
            this.btnSalirMesa.Click += new System.EventHandler(this.btnSalirMesa_Click);
            // 
            // pnlSeccionMesa
            // 
            this.pnlSeccionMesa.Location = new System.Drawing.Point(10, 43);
            this.pnlSeccionMesa.Name = "pnlSeccionMesa";
            this.pnlSeccionMesa.Size = new System.Drawing.Size(245, 445);
            this.pnlSeccionMesa.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(71, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "SECCIONES:";
            // 
            // PanelMesas
            // 
            this.PanelMesas.AutoScroll = true;
            this.PanelMesas.BackColor = System.Drawing.Color.LemonChiffon;
            this.PanelMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelMesas.Location = new System.Drawing.Point(328, 60);
            this.PanelMesas.Name = "PanelMesas";
            this.PanelMesas.Size = new System.Drawing.Size(848, 594);
            this.PanelMesas.TabIndex = 7;
            // 
            // lblPisos
            // 
            this.lblPisos.AutoSize = true;
            this.lblPisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPisos.ForeColor = System.Drawing.Color.Blue;
            this.lblPisos.Location = new System.Drawing.Point(321, 15);
            this.lblPisos.Name = "lblPisos";
            this.lblPisos.Size = new System.Drawing.Size(110, 42);
            this.lblPisos.TabIndex = 9;
            this.lblPisos.Text = "PISO";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerBlink
            // 
            this.timerBlink.Enabled = true;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // frmCambioMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1183, 662);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PanelMesas);
            this.Controls.Add(this.lblPisos);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCambioMesa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione la mesa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmCambioMesa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCambioMesa_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalirMesa;
        private System.Windows.Forms.Panel pnlSeccionMesa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PanelMesas;
        private System.Windows.Forms.Label lblPisos;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Timer timerBlink;
    }
}