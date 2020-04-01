namespace Palatium.Bodega
{
    partial class frmBodegaDefinicionDetallada
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvBodega = new System.Windows.Forms.DataGridView();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.dbAyudaCategoria = new Controles.Auxiliares.DB_Ayuda();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbProductoTerminado = new System.Windows.Forms.RadioButton();
            this.rdbMateriaPrima = new System.Windows.Forms.RadioButton();
            this.dbAyudaResponsable = new Controles.Auxiliares.DB_Ayuda();
            this.lblFamilia = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEmpresa = new MisControles.ComboDatos();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idPersona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEmpresa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idBodega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBodega)).BeginInit();
            this.grupoDatos.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvBodega);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 322);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle";
            // 
            // dgvBodega
            // 
            this.dgvBodega.AllowUserToAddRows = false;
            this.dgvBodega.AllowUserToResizeColumns = false;
            this.dgvBodega.AllowUserToResizeRows = false;
            this.dgvBodega.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvBodega.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBodega.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.descripcion,
            this.estado,
            this.categoria,
            this.estado1,
            this.idPersona,
            this.idEmpresa,
            this.idBodega,
            this.tipo});
            this.dgvBodega.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.dgvBodega.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvBodega.Location = new System.Drawing.Point(6, 19);
            this.dgvBodega.Name = "dgvBodega";
            this.dgvBodega.ReadOnly = true;
            this.dgvBodega.RowHeadersWidth = 25;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvBodega.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBodega.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBodega.Size = new System.Drawing.Size(537, 285);
            this.dgvBodega.TabIndex = 1;
            this.dgvBodega.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBodega_CellDoubleClick);
            // 
            // grupoDatos
            // 
            this.grupoDatos.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grupoDatos.Controls.Add(this.cmbEstado);
            this.grupoDatos.Controls.Add(this.dbAyudaCategoria);
            this.grupoDatos.Controls.Add(this.label4);
            this.grupoDatos.Controls.Add(this.groupBox3);
            this.grupoDatos.Controls.Add(this.dbAyudaResponsable);
            this.grupoDatos.Controls.Add(this.lblFamilia);
            this.grupoDatos.Controls.Add(this.txtDescripcion);
            this.grupoDatos.Controls.Add(this.label3);
            this.grupoDatos.Controls.Add(this.txtCodigo);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Controls.Add(this.cmbEmpresa);
            this.grupoDatos.Controls.Add(this.lblEmpresa);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(591, 13);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(375, 322);
            this.grupoDatos.TabIndex = 1;
            this.grupoDatos.TabStop = false;
            // 
            // dbAyudaCategoria
            // 
            this.dbAyudaCategoria.iId = 0;
            this.dbAyudaCategoria.Location = new System.Drawing.Point(6, 260);
            this.dbAyudaCategoria.Name = "dbAyudaCategoria";
            this.dbAyudaCategoria.sCodigo = null;
            this.dbAyudaCategoria.Size = new System.Drawing.Size(346, 21);
            this.dbAyudaCategoria.sNombre = null;
            this.dbAyudaCategoria.TabIndex = 151;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 13);
            this.label4.TabIndex = 152;
            this.label4.Text = "Categoría del Catálogo General  de Productos";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbProductoTerminado);
            this.groupBox3.Controls.Add(this.rdbMateriaPrima);
            this.groupBox3.Location = new System.Drawing.Point(6, 180);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 49);
            this.groupBox3.TabIndex = 150;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipo de Bodega";
            // 
            // rdbProductoTerminado
            // 
            this.rdbProductoTerminado.AutoSize = true;
            this.rdbProductoTerminado.Location = new System.Drawing.Point(201, 19);
            this.rdbProductoTerminado.Name = "rdbProductoTerminado";
            this.rdbProductoTerminado.Size = new System.Drawing.Size(121, 17);
            this.rdbProductoTerminado.TabIndex = 1;
            this.rdbProductoTerminado.Text = "Producto Terminado";
            this.rdbProductoTerminado.UseVisualStyleBackColor = true;
            // 
            // rdbMateriaPrima
            // 
            this.rdbMateriaPrima.AutoSize = true;
            this.rdbMateriaPrima.Checked = true;
            this.rdbMateriaPrima.Location = new System.Drawing.Point(15, 19);
            this.rdbMateriaPrima.Name = "rdbMateriaPrima";
            this.rdbMateriaPrima.Size = new System.Drawing.Size(175, 17);
            this.rdbMateriaPrima.TabIndex = 0;
            this.rdbMateriaPrima.TabStop = true;
            this.rdbMateriaPrima.Text = "Materia Prima, Respuestos, etc.";
            this.rdbMateriaPrima.UseVisualStyleBackColor = true;
            // 
            // dbAyudaResponsable
            // 
            this.dbAyudaResponsable.iId = 0;
            this.dbAyudaResponsable.Location = new System.Drawing.Point(6, 141);
            this.dbAyudaResponsable.Name = "dbAyudaResponsable";
            this.dbAyudaResponsable.sCodigo = null;
            this.dbAyudaResponsable.Size = new System.Drawing.Size(346, 21);
            this.dbAyudaResponsable.sNombre = null;
            this.dbAyudaResponsable.TabIndex = 148;
            // 
            // lblFamilia
            // 
            this.lblFamilia.AutoSize = true;
            this.lblFamilia.Location = new System.Drawing.Point(3, 126);
            this.lblFamilia.Name = "lblFamilia";
            this.lblFamilia.Size = new System.Drawing.Size(124, 13);
            this.lblFamilia.TabIndex = 149;
            this.lblFamilia.Text = "Responsable de Bodega";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDescripcion.Location = new System.Drawing.Point(71, 93);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(281, 20);
            this.txtDescripcion.TabIndex = 147;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 146;
            this.label3.Text = "Descripción";
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.Window;
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigo.Location = new System.Drawing.Point(6, 93);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(51, 20);
            this.txtCodigo.TabIndex = 145;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 144;
            this.label2.Text = "Código";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 142;
            this.label1.Text = "Estado";
            // 
            // cmbEmpresa
            // 
            this.cmbEmpresa.BackColor = System.Drawing.SystemColors.Window;
            this.cmbEmpresa.Enabled = false;
            this.cmbEmpresa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbEmpresa.FormattingEnabled = true;
            this.cmbEmpresa.Location = new System.Drawing.Point(6, 33);
            this.cmbEmpresa.Name = "cmbEmpresa";
            this.cmbEmpresa.Size = new System.Drawing.Size(129, 21);
            this.cmbEmpresa.TabIndex = 114;
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(3, 19);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(48, 13);
            this.lblEmpresa.TabIndex = 115;
            this.lblEmpresa.Text = "Empresa";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.escoba;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(840, 359);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(60, 23);
            this.btnLimpiar.TabIndex = 156;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.Image = global::Palatium.Properties.Resources.borrar;
            this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnular.Location = new System.Drawing.Point(777, 359);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(60, 23);
            this.btnAnular.TabIndex = 155;
            this.btnAnular.Text = "Anular";
            this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Image = global::Palatium.Properties.Resources.disco_flexible;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(708, 359);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(66, 23);
            this.btnGrabar.TabIndex = 154;
            this.btnGrabar.Text = "Nuevo";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Image = global::Palatium.Properties.Resources.salida;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(906, 359);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(60, 23);
            this.btnSalir.TabIndex = 153;
            this.btnSalir.Text = "Cerrar";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "ACTIVO",
            "INACTIVO"});
            this.cmbEstado.Location = new System.Drawing.Point(231, 33);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(121, 21);
            this.cmbEstado.TabIndex = 153;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Empresa";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 80;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "Código";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 60;
            // 
            // estado
            // 
            this.estado.HeaderText = "Descripción";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.estado.Width = 140;
            // 
            // categoria
            // 
            this.categoria.HeaderText = "Categoría";
            this.categoria.Name = "categoria";
            this.categoria.ReadOnly = true;
            // 
            // estado1
            // 
            this.estado1.HeaderText = "Estado";
            this.estado1.Name = "estado1";
            this.estado1.ReadOnly = true;
            // 
            // idPersona
            // 
            this.idPersona.HeaderText = "IdPersona";
            this.idPersona.Name = "idPersona";
            this.idPersona.ReadOnly = true;
            this.idPersona.Visible = false;
            // 
            // idEmpresa
            // 
            this.idEmpresa.HeaderText = "Id Empresa";
            this.idEmpresa.Name = "idEmpresa";
            this.idEmpresa.ReadOnly = true;
            this.idEmpresa.Visible = false;
            // 
            // idBodega
            // 
            this.idBodega.HeaderText = "Id Bodega";
            this.idBodega.Name = "idBodega";
            this.idBodega.ReadOnly = true;
            this.idBodega.Visible = false;
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            this.tipo.Visible = false;
            // 
            // frmBodegaDefinicionDetallada
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 394);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnSalir);
            this.MaximizeBox = false;
            this.Name = "frmBodegaDefinicionDetallada";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bodegas";
            this.Load += new System.EventHandler(this.frmBodegaDefinicionDetallada_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBodega)).EndInit();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.DataGridView dgvBodega;
        private MisControles.ComboDatos cmbEmpresa;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Controles.Auxiliares.DB_Ayuda dbAyudaResponsable;
        private System.Windows.Forms.Label lblFamilia;
        private Controles.Auxiliares.DB_Ayuda dbAyudaCategoria;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbProductoTerminado;
        private System.Windows.Forms.RadioButton rdbMateriaPrima;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPersona;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEmpresa;
        private System.Windows.Forms.DataGridViewTextBoxColumn idBodega;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
    }
}