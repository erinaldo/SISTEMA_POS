namespace Palatium.Formularios
{
    partial class FInformacionCobro
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
            this.tabCon_PosCobro = new System.Windows.Forms.TabControl();
            this.tabPag_PosCobro = new System.Windows.Forms.TabPage();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAgregarCobro = new System.Windows.Forms.Button();
            this.Grb_listRePosCobro = new System.Windows.Forms.GroupBox();
            this.dgvPosCobro = new System.Windows.Forms.DataGridView();
            this.Grb_opcioPosCobro = new System.Windows.Forms.GroupBox();
            this.btnNuevoPosCobro = new System.Windows.Forms.Button();
            this.btnCerrCobro = new System.Windows.Forms.Button();
            this.btnGuardDetaCobr = new System.Windows.Forms.Button();
            this.Grb_DatoPosCobro = new System.Windows.Forms.GroupBox();
            this.lblTotalOrden = new System.Windows.Forms.Label();
            this.txtTotalOrden = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.txtFechCobro = new System.Windows.Forms.TextBox();
            this.lblPosSecMesa = new System.Windows.Forms.Label();
            this.cmbEstadoCobro = new System.Windows.Forms.ComboBox();
            this.cmbOrdCobro = new System.Windows.Forms.ComboBox();
            this.lblEstaCajero = new System.Windows.Forms.Label();
            this.tabCon_PosCobro.SuspendLayout();
            this.tabPag_PosCobro.SuspendLayout();
            this.Grb_listRePosCobro.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPosCobro)).BeginInit();
            this.Grb_opcioPosCobro.SuspendLayout();
            this.Grb_DatoPosCobro.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_PosCobro
            // 
            this.tabCon_PosCobro.Controls.Add(this.tabPag_PosCobro);
            this.tabCon_PosCobro.Location = new System.Drawing.Point(-4, -1);
            this.tabCon_PosCobro.Name = "tabCon_PosCobro";
            this.tabCon_PosCobro.SelectedIndex = 0;
            this.tabCon_PosCobro.Size = new System.Drawing.Size(843, 570);
            this.tabCon_PosCobro.TabIndex = 5;
            // 
            // tabPag_PosCobro
            // 
            this.tabPag_PosCobro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_PosCobro.Controls.Add(this.btnLimpiar);
            this.tabPag_PosCobro.Controls.Add(this.btnAgregarCobro);
            this.tabPag_PosCobro.Controls.Add(this.Grb_listRePosCobro);
            this.tabPag_PosCobro.Controls.Add(this.Grb_opcioPosCobro);
            this.tabPag_PosCobro.Controls.Add(this.btnCerrCobro);
            this.tabPag_PosCobro.Controls.Add(this.btnGuardDetaCobr);
            this.tabPag_PosCobro.Controls.Add(this.Grb_DatoPosCobro);
            this.tabPag_PosCobro.Location = new System.Drawing.Point(4, 22);
            this.tabPag_PosCobro.Name = "tabPag_PosCobro";
            this.tabPag_PosCobro.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_PosCobro.Size = new System.Drawing.Size(835, 544);
            this.tabPag_PosCobro.TabIndex = 0;
            this.tabPag_PosCobro.Text = "Módulo de Pos_Cobro";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiar.Location = new System.Drawing.Point(613, 446);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(87, 30);
            this.btnLimpiar.TabIndex = 18;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAgregarCobro
            // 
            this.btnAgregarCobro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAgregarCobro.Location = new System.Drawing.Point(412, 447);
            this.btnAgregarCobro.Name = "btnAgregarCobro";
            this.btnAgregarCobro.Size = new System.Drawing.Size(81, 29);
            this.btnAgregarCobro.TabIndex = 17;
            this.btnAgregarCobro.Text = "Agregar";
            this.btnAgregarCobro.UseVisualStyleBackColor = false;
            this.btnAgregarCobro.Click += new System.EventHandler(this.btnAgregarCobro_Click);
            // 
            // Grb_listRePosCobro
            // 
            this.Grb_listRePosCobro.Controls.Add(this.dgvPosCobro);
            this.Grb_listRePosCobro.Enabled = false;
            this.Grb_listRePosCobro.Location = new System.Drawing.Point(17, 221);
            this.Grb_listRePosCobro.Name = "Grb_listRePosCobro";
            this.Grb_listRePosCobro.Size = new System.Drawing.Size(782, 215);
            this.Grb_listRePosCobro.TabIndex = 5;
            this.Grb_listRePosCobro.TabStop = false;
            this.Grb_listRePosCobro.Text = "Lista de Registros";
            // 
            // dgvPosCobro
            // 
            this.dgvPosCobro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPosCobro.Location = new System.Drawing.Point(13, 19);
            this.dgvPosCobro.Name = "dgvPosCobro";
            this.dgvPosCobro.Size = new System.Drawing.Size(750, 181);
            this.dgvPosCobro.TabIndex = 0;
            // 
            // Grb_opcioPosCobro
            // 
            this.Grb_opcioPosCobro.Controls.Add(this.btnNuevoPosCobro);
            this.Grb_opcioPosCobro.Location = new System.Drawing.Point(718, 151);
            this.Grb_opcioPosCobro.Name = "Grb_opcioPosCobro";
            this.Grb_opcioPosCobro.Size = new System.Drawing.Size(81, 64);
            this.Grb_opcioPosCobro.TabIndex = 4;
            this.Grb_opcioPosCobro.TabStop = false;
            this.Grb_opcioPosCobro.Text = "Opciones";
            // 
            // btnNuevoPosCobro
            // 
            this.btnNuevoPosCobro.BackColor = System.Drawing.Color.Blue;
            this.btnNuevoPosCobro.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevoPosCobro.Location = new System.Drawing.Point(6, 19);
            this.btnNuevoPosCobro.Name = "btnNuevoPosCobro";
            this.btnNuevoPosCobro.Size = new System.Drawing.Size(59, 29);
            this.btnNuevoPosCobro.TabIndex = 0;
            this.btnNuevoPosCobro.Text = "OK";
            this.btnNuevoPosCobro.UseVisualStyleBackColor = false;
            this.btnNuevoPosCobro.Click += new System.EventHandler(this.BtnNuevoPosCobro_Click);
            // 
            // btnCerrCobro
            // 
            this.btnCerrCobro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrCobro.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrCobro.Location = new System.Drawing.Point(706, 446);
            this.btnCerrCobro.Name = "btnCerrCobro";
            this.btnCerrCobro.Size = new System.Drawing.Size(74, 29);
            this.btnCerrCobro.TabIndex = 15;
            this.btnCerrCobro.Text = "Cerrar";
            this.btnCerrCobro.UseVisualStyleBackColor = false;
            this.btnCerrCobro.Click += new System.EventHandler(this.btnCerrCobro_Click);
            // 
            // btnGuardDetaCobr
            // 
            this.btnGuardDetaCobr.BackColor = System.Drawing.Color.Blue;
            this.btnGuardDetaCobr.ForeColor = System.Drawing.Color.Transparent;
            this.btnGuardDetaCobr.Location = new System.Drawing.Point(505, 446);
            this.btnGuardDetaCobr.Name = "btnGuardDetaCobr";
            this.btnGuardDetaCobr.Size = new System.Drawing.Size(87, 30);
            this.btnGuardDetaCobr.TabIndex = 16;
            this.btnGuardDetaCobr.Text = "Guardar";
            this.btnGuardDetaCobr.UseVisualStyleBackColor = false;
            this.btnGuardDetaCobr.Click += new System.EventHandler(this.btnGuardDetaCobr_Click);
            // 
            // Grb_DatoPosCobro
            // 
            this.Grb_DatoPosCobro.Controls.Add(this.lblTotalOrden);
            this.Grb_DatoPosCobro.Controls.Add(this.txtTotalOrden);
            this.Grb_DatoPosCobro.Controls.Add(this.lbl1);
            this.Grb_DatoPosCobro.Controls.Add(this.txtFechCobro);
            this.Grb_DatoPosCobro.Controls.Add(this.lblPosSecMesa);
            this.Grb_DatoPosCobro.Controls.Add(this.cmbEstadoCobro);
            this.Grb_DatoPosCobro.Controls.Add(this.cmbOrdCobro);
            this.Grb_DatoPosCobro.Controls.Add(this.lblEstaCajero);
            this.Grb_DatoPosCobro.Location = new System.Drawing.Point(17, 19);
            this.Grb_DatoPosCobro.Name = "Grb_DatoPosCobro";
            this.Grb_DatoPosCobro.Size = new System.Drawing.Size(782, 131);
            this.Grb_DatoPosCobro.TabIndex = 3;
            this.Grb_DatoPosCobro.TabStop = false;
            this.Grb_DatoPosCobro.Text = "Datos del Registro";
            // 
            // lblTotalOrden
            // 
            this.lblTotalOrden.AutoSize = true;
            this.lblTotalOrden.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalOrden.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrden.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotalOrden.Location = new System.Drawing.Point(265, 90);
            this.lblTotalOrden.Name = "lblTotalOrden";
            this.lblTotalOrden.Size = new System.Drawing.Size(104, 15);
            this.lblTotalOrden.TabIndex = 14;
            this.lblTotalOrden.Text = "Total de la Orden:";
            // 
            // txtTotalOrden
            // 
            this.txtTotalOrden.Location = new System.Drawing.Point(375, 89);
            this.txtTotalOrden.MaxLength = 20;
            this.txtTotalOrden.Name = "txtTotalOrden";
            this.txtTotalOrden.Size = new System.Drawing.Size(101, 20);
            this.txtTotalOrden.TabIndex = 13;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.BackColor = System.Drawing.Color.Transparent;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl1.Location = new System.Drawing.Point(272, 33);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(97, 15);
            this.lbl1.TabIndex = 12;
            this.lbl1.Text = "Fecha de Cobro:";
            // 
            // txtFechCobro
            // 
            this.txtFechCobro.Location = new System.Drawing.Point(375, 31);
            this.txtFechCobro.MaxLength = 20;
            this.txtFechCobro.Name = "txtFechCobro";
            this.txtFechCobro.Size = new System.Drawing.Size(101, 20);
            this.txtFechCobro.TabIndex = 11;
            // 
            // lblPosSecMesa
            // 
            this.lblPosSecMesa.AutoSize = true;
            this.lblPosSecMesa.BackColor = System.Drawing.Color.Transparent;
            this.lblPosSecMesa.Location = new System.Drawing.Point(24, 36);
            this.lblPosSecMesa.Name = "lblPosSecMesa";
            this.lblPosSecMesa.Size = new System.Drawing.Size(39, 13);
            this.lblPosSecMesa.TabIndex = 6;
            this.lblPosSecMesa.Text = "Orden:";
            // 
            // cmbEstadoCobro
            // 
            this.cmbEstadoCobro.Enabled = false;
            this.cmbEstadoCobro.FormattingEnabled = true;
            this.cmbEstadoCobro.Items.AddRange(new object[] {
            "ACTIVO",
            "ELIMINADO"});
            this.cmbEstadoCobro.Location = new System.Drawing.Point(619, 26);
            this.cmbEstadoCobro.Name = "cmbEstadoCobro";
            this.cmbEstadoCobro.Size = new System.Drawing.Size(121, 21);
            this.cmbEstadoCobro.TabIndex = 10;
            this.cmbEstadoCobro.SelectedIndexChanged += new System.EventHandler(this.CmbEstadoCobro_SelectedIndexChanged);
            // 
            // cmbOrdCobro
            // 
            this.cmbOrdCobro.FormattingEnabled = true;
            this.cmbOrdCobro.Location = new System.Drawing.Point(80, 31);
            this.cmbOrdCobro.Name = "cmbOrdCobro";
            this.cmbOrdCobro.Size = new System.Drawing.Size(121, 21);
            this.cmbOrdCobro.TabIndex = 5;
            this.cmbOrdCobro.SelectedIndexChanged += new System.EventHandler(this.comboB_OrdCobro_SelectedIndexChanged);
            // 
            // lblEstaCajero
            // 
            this.lblEstaCajero.AutoSize = true;
            this.lblEstaCajero.BackColor = System.Drawing.Color.Transparent;
            this.lblEstaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstaCajero.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblEstaCajero.Location = new System.Drawing.Point(553, 32);
            this.lblEstaCajero.Name = "lblEstaCajero";
            this.lblEstaCajero.Size = new System.Drawing.Size(48, 15);
            this.lblEstaCajero.TabIndex = 7;
            this.lblEstaCajero.Text = "Estado:";
            // 
            // FInformacionCobro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(836, 564);
            this.Controls.Add(this.tabCon_PosCobro);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FInformacionCobro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Configuración de las Formas de Cobro";
            this.Load += new System.EventHandler(this.FInformacionCobro_Load);
            this.tabCon_PosCobro.ResumeLayout(false);
            this.tabPag_PosCobro.ResumeLayout(false);
            this.Grb_listRePosCobro.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPosCobro)).EndInit();
            this.Grb_opcioPosCobro.ResumeLayout(false);
            this.Grb_DatoPosCobro.ResumeLayout(false);
            this.Grb_DatoPosCobro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_PosCobro;
        private System.Windows.Forms.TabPage tabPag_PosCobro;
        private System.Windows.Forms.GroupBox Grb_listRePosCobro;
        private System.Windows.Forms.DataGridView dgvPosCobro;
        private System.Windows.Forms.GroupBox Grb_opcioPosCobro;
        private System.Windows.Forms.Button btnNuevoPosCobro;
        private System.Windows.Forms.GroupBox Grb_DatoPosCobro;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.TextBox txtFechCobro;
        private System.Windows.Forms.Label lblPosSecMesa;
        private System.Windows.Forms.ComboBox cmbEstadoCobro;
        private System.Windows.Forms.ComboBox cmbOrdCobro;
        private System.Windows.Forms.Label lblEstaCajero;
        private System.Windows.Forms.Button btnAgregarCobro;
        private System.Windows.Forms.Button btnGuardDetaCobr;
        private System.Windows.Forms.Button btnCerrCobro;
        private System.Windows.Forms.Label lblTotalOrden;
        private System.Windows.Forms.TextBox txtTotalOrden;
        private System.Windows.Forms.Button btnLimpiar;
    }
}