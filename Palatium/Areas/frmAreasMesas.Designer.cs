namespace Palatium.Áreas
{
    partial class frmAreasMesas
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
            this.lblPisos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.PanelMesas = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCombinar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalirMesa = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtMesa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSeccionMesa = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPisos
            // 
            this.lblPisos.AutoSize = true;
            this.lblPisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPisos.ForeColor = System.Drawing.Color.Blue;
            this.lblPisos.Location = new System.Drawing.Point(321, 15);
            this.lblPisos.Name = "lblPisos";
            this.lblPisos.Size = new System.Drawing.Size(110, 42);
            this.lblPisos.TabIndex = 6;
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
            // PanelMesas
            // 
            this.PanelMesas.AutoScroll = true;
            this.PanelMesas.BackColor = System.Drawing.Color.LemonChiffon;
            this.PanelMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PanelMesas.Location = new System.Drawing.Point(328, 60);
            this.PanelMesas.Name = "PanelMesas";
            this.PanelMesas.Size = new System.Drawing.Size(871, 594);
            this.PanelMesas.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Navy;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.btnCombinar);
            this.groupBox1.Controls.Add(this.btnActualizar);
            this.groupBox1.Controls.Add(this.btnSalirMesa);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.txtMesa);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pnlSeccionMesa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 638);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnCombinar
            // 
            this.btnCombinar.AccessibleDescription = "";
            this.btnCombinar.BackColor = System.Drawing.Color.Transparent;
            this.btnCombinar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCombinar.FlatAppearance.BorderSize = 2;
            this.btnCombinar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCombinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCombinar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCombinar.Image = global::Palatium.Properties.Resources.combinar_comanda;
            this.btnCombinar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCombinar.Location = new System.Drawing.Point(13, 437);
            this.btnCombinar.Name = "btnCombinar";
            this.btnCombinar.Size = new System.Drawing.Size(232, 93);
            this.btnCombinar.TabIndex = 14;
            this.btnCombinar.Text = "COMBINAR\r\nMESAS";
            this.btnCombinar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCombinar.UseVisualStyleBackColor = false;
            this.btnCombinar.Click += new System.EventHandler(this.btnCombinar_Click);
            this.btnCombinar.MouseEnter += new System.EventHandler(this.btnCombinar_MouseEnter);
            this.btnCombinar.MouseLeave += new System.EventHandler(this.btnCombinar_MouseLeave);
            // 
            // btnActualizar
            // 
            this.btnActualizar.AccessibleDescription = "";
            this.btnActualizar.BackColor = System.Drawing.Color.Transparent;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 2;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizar.Image = global::Palatium.Properties.Resources.actualizar_mesas;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActualizar.Location = new System.Drawing.Point(13, 528);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(117, 93);
            this.btnActualizar.TabIndex = 13;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            this.btnActualizar.MouseEnter += new System.EventHandler(this.btnActualizar_MouseEnter);
            this.btnActualizar.MouseLeave += new System.EventHandler(this.btnActualizar_MouseLeave);
            // 
            // btnSalirMesa
            // 
            this.btnSalirMesa.BackColor = System.Drawing.Color.Transparent;
            this.btnSalirMesa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalirMesa.FlatAppearance.BorderSize = 2;
            this.btnSalirMesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalirMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalirMesa.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalirMesa.Image = global::Palatium.Properties.Resources.salir_mesas;
            this.btnSalirMesa.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalirMesa.Location = new System.Drawing.Point(128, 528);
            this.btnSalirMesa.Name = "btnSalirMesa";
            this.btnSalirMesa.Size = new System.Drawing.Size(117, 93);
            this.btnSalirMesa.TabIndex = 12;
            this.btnSalirMesa.Text = "Salir";
            this.btnSalirMesa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalirMesa.UseVisualStyleBackColor = false;
            this.btnSalirMesa.Click += new System.EventHandler(this.btnSalirMesa_Click);
            this.btnSalirMesa.MouseEnter += new System.EventHandler(this.btnSalirMesa_MouseEnter);
            this.btnSalirMesa.MouseLeave += new System.EventHandler(this.btnSalirMesa_MouseLeave);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBuscar.Location = new System.Drawing.Point(197, 382);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 39);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtMesa
            // 
            this.txtMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtMesa.Location = new System.Drawing.Point(106, 384);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.Size = new System.Drawing.Size(85, 29);
            this.txtMesa.TabIndex = 10;
            this.txtMesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMesa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesa_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(15, 387);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "MESAS:";
            // 
            // pnlSeccionMesa
            // 
            this.pnlSeccionMesa.Location = new System.Drawing.Point(10, 43);
            this.pnlSeccionMesa.Name = "pnlSeccionMesa";
            this.pnlSeccionMesa.Size = new System.Drawing.Size(245, 319);
            this.pnlSeccionMesa.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Navy;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(71, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "SECCIONES:";
            // 
            // frmAreasMesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1227, 662);
            this.Controls.Add(this.lblPisos);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PanelMesas);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAreasMesas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione una Mesa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAreasMesas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAreasMesas_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelMesas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSeccionMesa;
        private System.Windows.Forms.Label lblPisos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtMesa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalirMesa;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Button btnCombinar;
    }
}