namespace Palatium.Receta
{
    partial class frmListarRecetas
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
            this.Grb_listRejornada = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnBuscarjornada = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.idJornada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClasificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReceta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnVer = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Grb_listRejornada.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // Grb_listRejornada
            // 
            this.Grb_listRejornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Grb_listRejornada.Controls.Add(this.btnCerrar);
            this.Grb_listRejornada.Controls.Add(this.btnBuscarjornada);
            this.Grb_listRejornada.Controls.Add(this.txtBuscar);
            this.Grb_listRejornada.Controls.Add(this.btnExcel);
            this.Grb_listRejornada.Controls.Add(this.dgvDatos);
            this.Grb_listRejornada.Location = new System.Drawing.Point(12, 12);
            this.Grb_listRejornada.Name = "Grb_listRejornada";
            this.Grb_listRejornada.Size = new System.Drawing.Size(1083, 457);
            this.Grb_listRejornada.TabIndex = 13;
            this.Grb_listRejornada.TabStop = false;
            this.Grb_listRejornada.Text = "Lista de Registros";
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrar.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Location = new System.Drawing.Point(1009, 12);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(64, 39);
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // btnBuscarjornada
            // 
            this.btnBuscarjornada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBuscarjornada.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscarjornada.Location = new System.Drawing.Point(235, 25);
            this.btnBuscarjornada.Name = "btnBuscarjornada";
            this.btnBuscarjornada.Size = new System.Drawing.Size(88, 26);
            this.btnBuscarjornada.TabIndex = 4;
            this.btnBuscarjornada.Text = "Buscar";
            this.btnBuscarjornada.UseVisualStyleBackColor = false;
            this.btnBuscarjornada.Click += new System.EventHandler(this.btnBuscarjornada_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(13, 29);
            this.txtBuscar.MaxLength = 20;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(216, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnExcel.ForeColor = System.Drawing.Color.Transparent;
            this.btnExcel.Location = new System.Drawing.Point(939, 12);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(64, 39);
            this.btnExcel.TabIndex = 5;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = false;
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idJornada,
            this.codigo,
            this.descripcion,
            this.colClasificacion,
            this.colReceta,
            this.colOrigen,
            this.estado1,
            this.btnVer});
            this.dgvDatos.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvDatos.Location = new System.Drawing.Point(13, 61);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersWidth = 35;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(1060, 378);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellContentClick);
            // 
            // idJornada
            // 
            this.idJornada.HeaderText = "IDRECETA";
            this.idJornada.Name = "idJornada";
            this.idJornada.ReadOnly = true;
            this.idJornada.Visible = false;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "CODIGO";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Width = 80;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "NOMBRE DE LA RECETA";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 300;
            // 
            // colClasificacion
            // 
            this.colClasificacion.HeaderText = "CLASIFICACIÓN";
            this.colClasificacion.Name = "colClasificacion";
            this.colClasificacion.ReadOnly = true;
            this.colClasificacion.Width = 150;
            // 
            // colReceta
            // 
            this.colReceta.HeaderText = "RECETA";
            this.colReceta.Name = "colReceta";
            this.colReceta.ReadOnly = true;
            this.colReceta.Width = 150;
            // 
            // colOrigen
            // 
            this.colOrigen.HeaderText = "ORIGEN";
            this.colOrigen.Name = "colOrigen";
            this.colOrigen.ReadOnly = true;
            this.colOrigen.Width = 150;
            // 
            // estado1
            // 
            this.estado1.HeaderText = "ESTADO";
            this.estado1.Name = "estado1";
            this.estado1.ReadOnly = true;
            // 
            // btnVer
            // 
            this.btnVer.HeaderText = "VER";
            this.btnVer.Name = "btnVer";
            this.btnVer.ReadOnly = true;
            this.btnVer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnVer.Text = "Ver";
            this.btnVer.ToolTipText = "Clic aquí para ver el detalle de la receta";
            this.btnVer.Width = 40;
            // 
            // frmListarRecetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1107, 481);
            this.Controls.Add(this.Grb_listRejornada);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListarRecetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listar Recetas";
            this.Load += new System.EventHandler(this.frmListarRecetas_Load);
            this.Grb_listRejornada.ResumeLayout(false);
            this.Grb_listRejornada.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Grb_listRejornada;
        private System.Windows.Forms.Button btnBuscarjornada;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn idJornada;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClasificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReceta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado1;
        private System.Windows.Forms.DataGridViewButtonColumn btnVer;
    }
}