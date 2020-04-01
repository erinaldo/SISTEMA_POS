namespace InicioAplicacion.Formularios
{
    partial class FInformacionFacturaOrden
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
            this.tabCon_PosOrd = new System.Windows.Forms.TabControl();
            this.tabPag_PosOrd = new System.Windows.Forms.TabPage();
            this.btnCerrPosOrd = new System.Windows.Forms.Button();
            this.Grb_listRePosMesa = new System.Windows.Forms.GroupBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnGuardDetaOrd = new System.Windows.Forms.Button();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvPosOrd = new System.Windows.Forms.DataGridView();
            this.statusStrip_PosMesa = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupB_PosOrd = new System.Windows.Forms.GroupBox();
            this.btnNuevoPosOrd = new System.Windows.Forms.Button();
            this.btnBuscClient = new System.Windows.Forms.Button();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtCodigoOrde = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txt = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblLocalidad = new System.Windows.Forms.Label();
            this.lblJornad = new System.Windows.Forms.Label();
            this.lblMesa = new System.Windows.Forms.Label();
            this.lblFactura = new System.Windows.Forms.Label();
            this.lblCajero = new System.Windows.Forms.Label();
            this.lblMeser = new System.Windows.Forms.Label();
            this.lblEstaOrden = new System.Windows.Forms.Label();
            this.lblOriOrden = new System.Windows.Forms.Label();
            this.tabCon_PosOrd.SuspendLayout();
            this.tabPag_PosOrd.SuspendLayout();
            this.Grb_listRePosMesa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPosOrd)).BeginInit();
            this.statusStrip_PosMesa.SuspendLayout();
            this.groupB_PosOrd.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_PosOrd
            // 
            this.tabCon_PosOrd.Controls.Add(this.tabPag_PosOrd);
            this.tabCon_PosOrd.Location = new System.Drawing.Point(12, 12);
            this.tabCon_PosOrd.Name = "tabCon_PosOrd";
            this.tabCon_PosOrd.SelectedIndex = 0;
            this.tabCon_PosOrd.Size = new System.Drawing.Size(1000, 665);
            this.tabCon_PosOrd.TabIndex = 6;
            // 
            // tabPag_PosOrd
            // 
            this.tabPag_PosOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_PosOrd.Controls.Add(this.btnCerrPosOrd);
            this.tabPag_PosOrd.Controls.Add(this.Grb_listRePosMesa);
            this.tabPag_PosOrd.Controls.Add(this.groupB_PosOrd);
            this.tabPag_PosOrd.Location = new System.Drawing.Point(4, 22);
            this.tabPag_PosOrd.Name = "tabPag_PosOrd";
            this.tabPag_PosOrd.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_PosOrd.Size = new System.Drawing.Size(992, 639);
            this.tabPag_PosOrd.TabIndex = 0;
            this.tabPag_PosOrd.Text = "Módulo de Pos_Orden";
            // 
            // btnCerrPosOrd
            // 
            this.btnCerrPosOrd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrPosOrd.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrPosOrd.Location = new System.Drawing.Point(740, 597);
            this.btnCerrPosOrd.Name = "btnCerrPosOrd";
            this.btnCerrPosOrd.Size = new System.Drawing.Size(88, 39);
            this.btnCerrPosOrd.TabIndex = 3;
            this.btnCerrPosOrd.Text = "Cerrar";
            this.btnCerrPosOrd.UseVisualStyleBackColor = false;
            // 
            // Grb_listRePosMesa
            // 
            this.Grb_listRePosMesa.Controls.Add(this.btnAgregar);
            this.Grb_listRePosMesa.Controls.Add(this.btnGuardDetaOrd);
            this.Grb_listRePosMesa.Controls.Add(this.txtTotal);
            this.Grb_listRePosMesa.Controls.Add(this.lblTotal);
            this.Grb_listRePosMesa.Controls.Add(this.dgvPosOrd);
            this.Grb_listRePosMesa.Controls.Add(this.statusStrip_PosMesa);
            this.Grb_listRePosMesa.Enabled = false;
            this.Grb_listRePosMesa.Location = new System.Drawing.Point(17, 248);
            this.Grb_listRePosMesa.Name = "Grb_listRePosMesa";
            this.Grb_listRePosMesa.Size = new System.Drawing.Size(940, 315);
            this.Grb_listRePosMesa.TabIndex = 5;
            this.Grb_listRePosMesa.TabStop = false;
            this.Grb_listRePosMesa.Text = "Detalle de la Orden";
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAgregar.Location = new System.Drawing.Point(458, 267);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(81, 29);
            this.btnAgregar.TabIndex = 14;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            // 
            // btnGuardDetaOrd
            // 
            this.btnGuardDetaOrd.BackColor = System.Drawing.Color.Blue;
            this.btnGuardDetaOrd.ForeColor = System.Drawing.Color.Transparent;
            this.btnGuardDetaOrd.Location = new System.Drawing.Point(559, 267);
            this.btnGuardDetaOrd.Name = "btnGuardDetaOrd";
            this.btnGuardDetaOrd.Size = new System.Drawing.Size(87, 30);
            this.btnGuardDetaOrd.TabIndex = 13;
            this.btnGuardDetaOrd.Text = "Guardar";
            this.btnGuardDetaOrd.UseVisualStyleBackColor = false;
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(711, 273);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 12;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(671, 280);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 13);
            this.lblTotal.TabIndex = 11;
            this.lblTotal.Text = "Total:";
            // 
            // dgvPosOrd
            // 
            this.dgvPosOrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPosOrd.Location = new System.Drawing.Point(19, 19);
            this.dgvPosOrd.Name = "dgvPosOrd";
            this.dgvPosOrd.Size = new System.Drawing.Size(884, 237);
            this.dgvPosOrd.TabIndex = 10;
            // 
            // statusStrip_PosMesa
            // 
            this.statusStrip_PosMesa.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip_PosMesa.Location = new System.Drawing.Point(3, 201);
            this.statusStrip_PosMesa.Name = "statusStrip_PosMesa";
            this.statusStrip_PosMesa.Size = new System.Drawing.Size(566, 22);
            this.statusStrip_PosMesa.TabIndex = 9;
            this.statusStrip_PosMesa.Text = "statusStrip1";
            this.statusStrip_PosMesa.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // groupB_PosOrd
            // 
            this.groupB_PosOrd.Controls.Add(this.btnNuevoPosOrd);
            this.groupB_PosOrd.Controls.Add(this.btnBuscClient);
            this.groupB_PosOrd.Controls.Add(this.txtCliente);
            this.groupB_PosOrd.Controls.Add(this.txtCodigoOrde);
            this.groupB_PosOrd.Controls.Add(this.textBox7);
            this.groupB_PosOrd.Controls.Add(this.textBox6);
            this.groupB_PosOrd.Controls.Add(this.textBox5);
            this.groupB_PosOrd.Controls.Add(this.textBox4);
            this.groupB_PosOrd.Controls.Add(this.txt);
            this.groupB_PosOrd.Controls.Add(this.textBox2);
            this.groupB_PosOrd.Controls.Add(this.textBox1);
            this.groupB_PosOrd.Controls.Add(this.txtFecha);
            this.groupB_PosOrd.Controls.Add(this.lblFecha);
            this.groupB_PosOrd.Controls.Add(this.lblLocalidad);
            this.groupB_PosOrd.Controls.Add(this.lblJornad);
            this.groupB_PosOrd.Controls.Add(this.lblMesa);
            this.groupB_PosOrd.Controls.Add(this.lblFactura);
            this.groupB_PosOrd.Controls.Add(this.lblCajero);
            this.groupB_PosOrd.Controls.Add(this.lblMeser);
            this.groupB_PosOrd.Controls.Add(this.lblEstaOrden);
            this.groupB_PosOrd.Controls.Add(this.lblOriOrden);
            this.groupB_PosOrd.Location = new System.Drawing.Point(17, 19);
            this.groupB_PosOrd.Name = "groupB_PosOrd";
            this.groupB_PosOrd.Size = new System.Drawing.Size(940, 212);
            this.groupB_PosOrd.TabIndex = 3;
            this.groupB_PosOrd.TabStop = false;
            this.groupB_PosOrd.Text = "Datos del Registro";
            // 
            // btnNuevoPosOrd
            // 
            this.btnNuevoPosOrd.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoPosOrd.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevoPosOrd.Location = new System.Drawing.Point(447, 23);
            this.btnNuevoPosOrd.Name = "btnNuevoPosOrd";
            this.btnNuevoPosOrd.Size = new System.Drawing.Size(52, 31);
            this.btnNuevoPosOrd.TabIndex = 0;
            this.btnNuevoPosOrd.Text = "OK";
            this.btnNuevoPosOrd.UseVisualStyleBackColor = false;
            this.btnNuevoPosOrd.Click += new System.EventHandler(this.btnNuevoPosOrd_Click);
            // 
            // btnBuscClient
            // 
            this.btnBuscClient.Location = new System.Drawing.Point(181, 27);
            this.btnBuscClient.Name = "btnBuscClient";
            this.btnBuscClient.Size = new System.Drawing.Size(53, 23);
            this.btnBuscClient.TabIndex = 38;
            this.btnBuscClient.Text = "?";
            this.btnBuscClient.UseVisualStyleBackColor = true;
            this.btnBuscClient.Click += new System.EventHandler(this.btnBuscClient_Click);
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(240, 29);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(190, 20);
            this.txtCliente.TabIndex = 37;
            // 
            // txtCodigoOrde
            // 
            this.txtCodigoOrde.Location = new System.Drawing.Point(84, 29);
            this.txtCodigoOrde.Name = "txtCodigoOrde";
            this.txtCodigoOrde.Size = new System.Drawing.Size(91, 20);
            this.txtCodigoOrde.TabIndex = 36;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(743, 73);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(160, 20);
            this.textBox7.TabIndex = 35;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(411, 73);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(160, 20);
            this.textBox6.TabIndex = 34;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(411, 152);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(160, 20);
            this.textBox5.TabIndex = 33;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(411, 113);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(160, 20);
            this.textBox4.TabIndex = 32;
            // 
            // txt
            // 
            this.txt.Location = new System.Drawing.Point(84, 73);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(160, 20);
            this.txt.TabIndex = 31;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(84, 148);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 20);
            this.textBox2.TabIndex = 30;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(84, 112);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 20);
            this.textBox1.TabIndex = 29;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(743, 113);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(160, 20);
            this.txtFecha.TabIndex = 28;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.BackColor = System.Drawing.Color.Transparent;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFecha.Location = new System.Drawing.Point(659, 118);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(44, 15);
            this.lblFecha.TabIndex = 27;
            this.lblFecha.Text = "Fecha:";
            // 
            // lblLocalidad
            // 
            this.lblLocalidad.AutoSize = true;
            this.lblLocalidad.BackColor = System.Drawing.Color.Transparent;
            this.lblLocalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalidad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLocalidad.Location = new System.Drawing.Point(14, 74);
            this.lblLocalidad.Name = "lblLocalidad";
            this.lblLocalidad.Size = new System.Drawing.Size(64, 15);
            this.lblLocalidad.TabIndex = 25;
            this.lblLocalidad.Text = "Localidad:";
            // 
            // lblJornad
            // 
            this.lblJornad.AutoSize = true;
            this.lblJornad.BackColor = System.Drawing.Color.Transparent;
            this.lblJornad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJornad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblJornad.Location = new System.Drawing.Point(327, 118);
            this.lblJornad.Name = "lblJornad";
            this.lblJornad.Size = new System.Drawing.Size(55, 15);
            this.lblJornad.TabIndex = 23;
            this.lblJornad.Text = "Jornada:";
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMesa.Location = new System.Drawing.Point(15, 113);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(41, 15);
            this.lblMesa.TabIndex = 21;
            this.lblMesa.Text = "Mesa:";
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.BackColor = System.Drawing.Color.Transparent;
            this.lblFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactura.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFactura.Location = new System.Drawing.Point(15, 34);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(51, 15);
            this.lblFactura.TabIndex = 19;
            this.lblFactura.Text = "Factura:";
            // 
            // lblCajero
            // 
            this.lblCajero.AutoSize = true;
            this.lblCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCajero.Location = new System.Drawing.Point(15, 154);
            this.lblCajero.Name = "lblCajero";
            this.lblCajero.Size = new System.Drawing.Size(46, 15);
            this.lblCajero.TabIndex = 15;
            this.lblCajero.Text = "Cajero:";
            // 
            // lblMeser
            // 
            this.lblMeser.AutoSize = true;
            this.lblMeser.BackColor = System.Drawing.Color.Transparent;
            this.lblMeser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMeser.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMeser.Location = new System.Drawing.Point(327, 157);
            this.lblMeser.Name = "lblMeser";
            this.lblMeser.Size = new System.Drawing.Size(52, 15);
            this.lblMeser.TabIndex = 17;
            this.lblMeser.Text = "Mesero:";
            // 
            // lblEstaOrden
            // 
            this.lblEstaOrden.AutoSize = true;
            this.lblEstaOrden.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaOrden.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaOrden.Location = new System.Drawing.Point(659, 78);
            this.lblEstaOrden.Name = "lblEstaOrden";
            this.lblEstaOrden.Size = new System.Drawing.Size(48, 15);
            this.lblEstaOrden.TabIndex = 7;
            this.lblEstaOrden.Text = "Estado:";
            // 
            // lblOriOrden
            // 
            this.lblOriOrden.AutoSize = true;
            this.lblOriOrden.BackColor = System.Drawing.Color.Transparent;
            this.lblOriOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOriOrden.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblOriOrden.Location = new System.Drawing.Point(327, 78);
            this.lblOriOrden.Name = "lblOriOrden";
            this.lblOriOrden.Size = new System.Drawing.Size(82, 15);
            this.lblOriOrden.TabIndex = 13;
            this.lblOriOrden.Text = "Origen orden:";
            // 
            // FInformacionFacturaOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1024, 690);
            this.Controls.Add(this.tabCon_PosOrd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionFacturaOrden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Información de Factura por Orden";
            this.tabCon_PosOrd.ResumeLayout(false);
            this.tabPag_PosOrd.ResumeLayout(false);
            this.Grb_listRePosMesa.ResumeLayout(false);
            this.Grb_listRePosMesa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPosOrd)).EndInit();
            this.statusStrip_PosMesa.ResumeLayout(false);
            this.statusStrip_PosMesa.PerformLayout();
            this.groupB_PosOrd.ResumeLayout(false);
            this.groupB_PosOrd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_PosOrd;
        private System.Windows.Forms.TabPage tabPag_PosOrd;
        private System.Windows.Forms.Button btnCerrPosOrd;
        private System.Windows.Forms.GroupBox Grb_listRePosMesa;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnNuevoPosOrd;
        private System.Windows.Forms.Button btnGuardDetaOrd;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgvPosOrd;
        private System.Windows.Forms.StatusStrip statusStrip_PosMesa;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupB_PosOrd;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label lblLocalidad;
        private System.Windows.Forms.Label lblJornad;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.Label lblCajero;
        private System.Windows.Forms.Label lblMeser;
        private System.Windows.Forms.Label lblEstaOrden;
        private System.Windows.Forms.Label lblOriOrden;
        private System.Windows.Forms.Button btnBuscClient;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtCodigoOrde;
    }
}