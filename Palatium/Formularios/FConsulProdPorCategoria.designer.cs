namespace Palatium.Formularios
{
    partial class FConsulProdPorCategoria
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
            this.tabCon_ConProPorCategoria = new System.Windows.Forms.TabControl();
            this.tabPag_ConProPorCategoria = new System.Windows.Forms.TabPage();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.Grb_listReCajero = new System.Windows.Forms.GroupBox();
            this.dgvConProPorCategoria = new System.Windows.Forms.DataGridView();
            this.clmCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExcelCategoria = new System.Windows.Forms.Button();
            this.Grb_DatoConProPorCategoria = new System.Windows.Forms.GroupBox();
            this.btnOkCategoria = new System.Windows.Forms.Button();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblCodigProducto = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lbEmpresa = new System.Windows.Forms.Label();
            this.tabCon_ConProPorCategoria.SuspendLayout();
            this.tabPag_ConProPorCategoria.SuspendLayout();
            this.Grb_listReCajero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConProPorCategoria)).BeginInit();
            this.Grb_DatoConProPorCategoria.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCon_ConProPorCategoria
            // 
            this.tabCon_ConProPorCategoria.Controls.Add(this.tabPag_ConProPorCategoria);
            this.tabCon_ConProPorCategoria.Location = new System.Drawing.Point(-4, -1);
            this.tabCon_ConProPorCategoria.Name = "tabCon_ConProPorCategoria";
            this.tabCon_ConProPorCategoria.SelectedIndex = 0;
            this.tabCon_ConProPorCategoria.Size = new System.Drawing.Size(801, 552);
            this.tabCon_ConProPorCategoria.TabIndex = 2;
            // 
            // tabPag_ConProPorCategoria
            // 
            this.tabPag_ConProPorCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPag_ConProPorCategoria.Controls.Add(this.btnCerrar);
            this.tabPag_ConProPorCategoria.Controls.Add(this.btnLimpiar);
            this.tabPag_ConProPorCategoria.Controls.Add(this.Grb_listReCajero);
            this.tabPag_ConProPorCategoria.Controls.Add(this.btnExcelCategoria);
            this.tabPag_ConProPorCategoria.Controls.Add(this.Grb_DatoConProPorCategoria);
            this.tabPag_ConProPorCategoria.Location = new System.Drawing.Point(4, 22);
            this.tabPag_ConProPorCategoria.Name = "tabPag_ConProPorCategoria";
            this.tabPag_ConProPorCategoria.Padding = new System.Windows.Forms.Padding(3);
            this.tabPag_ConProPorCategoria.Size = new System.Drawing.Size(793, 526);
            this.tabPag_ConProPorCategoria.TabIndex = 0;
            this.tabPag_ConProPorCategoria.Text = "Consulta de Productos Por Categoría";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(680, 469);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(70, 39);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(604, 469);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 39);
            this.btnLimpiar.TabIndex = 2;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // Grb_listReCajero
            // 
            this.Grb_listReCajero.Controls.Add(this.dgvConProPorCategoria);
            this.Grb_listReCajero.Location = new System.Drawing.Point(17, 144);
            this.Grb_listReCajero.Name = "Grb_listReCajero";
            this.Grb_listReCajero.Size = new System.Drawing.Size(759, 319);
            this.Grb_listReCajero.TabIndex = 5;
            this.Grb_listReCajero.TabStop = false;
            this.Grb_listReCajero.Text = "Lista de Registros";
            // 
            // dgvConProPorCategoria
            // 
            this.dgvConProPorCategoria.AllowUserToAddRows = false;
            this.dgvConProPorCategoria.AllowUserToDeleteRows = false;
            this.dgvConProPorCategoria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConProPorCategoria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCodigo,
            this.clmDescripcion});
            this.dgvConProPorCategoria.Location = new System.Drawing.Point(116, 28);
            this.dgvConProPorCategoria.Name = "dgvConProPorCategoria";
            this.dgvConProPorCategoria.ReadOnly = true;
            this.dgvConProPorCategoria.Size = new System.Drawing.Size(491, 285);
            this.dgvConProPorCategoria.TabIndex = 0;
            // 
            // clmCodigo
            // 
            this.clmCodigo.HeaderText = "Código";
            this.clmCodigo.Name = "clmCodigo";
            this.clmCodigo.ReadOnly = true;
            this.clmCodigo.Width = 150;
            // 
            // clmDescripcion
            // 
            this.clmDescripcion.HeaderText = "Descripción";
            this.clmDescripcion.Name = "clmDescripcion";
            this.clmDescripcion.ReadOnly = true;
            this.clmDescripcion.Width = 300;
            // 
            // btnExcelCategoria
            // 
            this.btnExcelCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExcelCategoria.ForeColor = System.Drawing.Color.White;
            this.btnExcelCategoria.Location = new System.Drawing.Point(35, 469);
            this.btnExcelCategoria.Name = "btnExcelCategoria";
            this.btnExcelCategoria.Size = new System.Drawing.Size(70, 39);
            this.btnExcelCategoria.TabIndex = 0;
            this.btnExcelCategoria.Text = "Enviar a Excel";
            this.btnExcelCategoria.UseVisualStyleBackColor = false;
            // 
            // Grb_DatoConProPorCategoria
            // 
            this.Grb_DatoConProPorCategoria.Controls.Add(this.btnOkCategoria);
            this.Grb_DatoConProPorCategoria.Controls.Add(this.cmbEmpresa);
            this.Grb_DatoConProPorCategoria.Controls.Add(this.lblCodigProducto);
            this.Grb_DatoConProPorCategoria.Controls.Add(this.txtCodigo);
            this.Grb_DatoConProPorCategoria.Controls.Add(this.lbEmpresa);
            this.Grb_DatoConProPorCategoria.Location = new System.Drawing.Point(17, 19);
            this.Grb_DatoConProPorCategoria.Name = "Grb_DatoConProPorCategoria";
            this.Grb_DatoConProPorCategoria.Size = new System.Drawing.Size(759, 94);
            this.Grb_DatoConProPorCategoria.TabIndex = 3;
            this.Grb_DatoConProPorCategoria.TabStop = false;
            this.Grb_DatoConProPorCategoria.Text = "Datos del Registro";
            // 
            // btnOkCategoria
            // 
            this.btnOkCategoria.BackColor = System.Drawing.Color.Blue;
            this.btnOkCategoria.ForeColor = System.Drawing.Color.White;
            this.btnOkCategoria.Location = new System.Drawing.Point(640, 18);
            this.btnOkCategoria.Name = "btnOkCategoria";
            this.btnOkCategoria.Size = new System.Drawing.Size(70, 39);
            this.btnOkCategoria.TabIndex = 6;
            this.btnOkCategoria.Text = "OK";
            this.btnOkCategoria.UseVisualStyleBackColor = false;
            this.btnOkCategoria.Click += new System.EventHandler(this.btnOkCategoria_Click);
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(100, 28);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(177, 21);
            this.cmbEmpresa.TabIndex = 52;
            // 
            // lblCodigProducto
            // 
            this.lblCodigProducto.AutoSize = true;
            this.lblCodigProducto.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigProducto.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCodigProducto.Location = new System.Drawing.Point(326, 30);
            this.lblCodigProducto.Name = "lblCodigProducto";
            this.lblCodigProducto.Size = new System.Drawing.Size(101, 15);
            this.lblCodigProducto.TabIndex = 51;
            this.lblCodigProducto.Text = "Código Producto:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(430, 28);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(116, 20);
            this.txtCodigo.TabIndex = 48;
            // 
            // lbEmpresa
            // 
            this.lbEmpresa.AutoSize = true;
            this.lbEmpresa.BackColor = System.Drawing.Color.Transparent;
            this.lbEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEmpresa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbEmpresa.Location = new System.Drawing.Point(15, 30);
            this.lbEmpresa.Name = "lbEmpresa";
            this.lbEmpresa.Size = new System.Drawing.Size(60, 15);
            this.lbEmpresa.TabIndex = 3;
            this.lbEmpresa.Text = "Empresa:";
            // 
            // FConsulProdPorCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(793, 547);
            this.Controls.Add(this.tabCon_ConProPorCategoria);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FConsulProdPorCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Productos por Categoría";
            this.Load += new System.EventHandler(this.FConsulProdPorCategoria_Load);
            this.tabCon_ConProPorCategoria.ResumeLayout(false);
            this.tabPag_ConProPorCategoria.ResumeLayout(false);
            this.Grb_listReCajero.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConProPorCategoria)).EndInit();
            this.Grb_DatoConProPorCategoria.ResumeLayout(false);
            this.Grb_DatoConProPorCategoria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCon_ConProPorCategoria;
        private System.Windows.Forms.TabPage tabPag_ConProPorCategoria;
        private System.Windows.Forms.GroupBox Grb_listReCajero;
        private System.Windows.Forms.DataGridView dgvConProPorCategoria;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnExcelCategoria;
        private System.Windows.Forms.GroupBox Grb_DatoConProPorCategoria;
        private System.Windows.Forms.Label lblCodigProducto;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lbEmpresa;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Button btnOkCategoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDescripcion;
    }
}