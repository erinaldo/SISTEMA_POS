namespace Palatium.Oficina
{
    partial class frmReportesCierre
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAgregarReportes = new System.Windows.Forms.Button();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.lblEmpresa = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.grupoDatos = new System.Windows.Forms.GroupBox();
            this.btnEliminarLinea = new System.Windows.Forms.Button();
            this.btnNuevaLinea = new System.Windows.Forms.Button();
            this.cmbReportes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrden = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.grupoBotones = new System.Windows.Forms.GroupBox();
            this.btnQuitarSeleccion = new System.Windows.Forms.Button();
            this.id_pos_reportes_cierre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_reportes_cierre_por_localidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_localidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.en_base = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grupoDatos.SuspendLayout();
            this.grupoBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAgregarReportes);
            this.groupBox1.Controls.Add(this.cmbLocalidades);
            this.groupBox1.Controls.Add(this.lblEmpresa);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 57);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // btnAgregarReportes
            // 
            this.btnAgregarReportes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregarReportes.FlatAppearance.BorderSize = 0;
            this.btnAgregarReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarReportes.ForeColor = System.Drawing.Color.Transparent;
            this.btnAgregarReportes.Image = global::Palatium.Properties.Resources.icono_agregar_grid;
            this.btnAgregarReportes.Location = new System.Drawing.Point(330, 9);
            this.btnAgregarReportes.Name = "btnAgregarReportes";
            this.btnAgregarReportes.Size = new System.Drawing.Size(36, 38);
            this.btnAgregarReportes.TabIndex = 51;
            this.ttMensaje.SetToolTip(this.btnAgregarReportes, "Clic auí para agregar reportes de cierre a la localidad seleccionada");
            this.btnAgregarReportes.UseVisualStyleBackColor = true;
            this.btnAgregarReportes.Click += new System.EventHandler(this.btnAgregarReportes_Click);
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(87, 19);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(234, 21);
            this.cmbLocalidades.TabIndex = 11;
            this.cmbLocalidades.SelectedIndexChanged += new System.EventHandler(this.cmbLocalidades_SelectedIndexChanged);
            // 
            // lblEmpresa
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresa.Location = new System.Drawing.Point(13, 20);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(64, 15);
            this.lblEmpresa.TabIndex = 44;
            this.lblEmpresa.Text = "Localidad:";
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pos_reportes_cierre,
            this.id_pos_reportes_cierre_por_localidad,
            this.id_localidad,
            this.descripcion,
            this.orden,
            this.en_base});
            this.dgvDatos.Location = new System.Drawing.Point(410, 12);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(379, 239);
            this.dgvDatos.TabIndex = 11;
            this.dgvDatos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellContentClick);
            // 
            // grupoDatos
            // 
            this.grupoDatos.Controls.Add(this.btnEliminarLinea);
            this.grupoDatos.Controls.Add(this.btnNuevaLinea);
            this.grupoDatos.Controls.Add(this.cmbReportes);
            this.grupoDatos.Controls.Add(this.label2);
            this.grupoDatos.Controls.Add(this.txtOrden);
            this.grupoDatos.Controls.Add(this.label1);
            this.grupoDatos.Enabled = false;
            this.grupoDatos.Location = new System.Drawing.Point(12, 75);
            this.grupoDatos.Name = "grupoDatos";
            this.grupoDatos.Size = new System.Drawing.Size(376, 113);
            this.grupoDatos.TabIndex = 45;
            this.grupoDatos.TabStop = false;
            this.grupoDatos.Text = "Opciones";
            // 
            // btnEliminarLinea
            // 
            this.btnEliminarLinea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEliminarLinea.FlatAppearance.BorderSize = 0;
            this.btnEliminarLinea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarLinea.ForeColor = System.Drawing.Color.Transparent;
            this.btnEliminarLinea.Image = global::Palatium.Properties.Resources.icono_eliminar_grid;
            this.btnEliminarLinea.Location = new System.Drawing.Point(332, 59);
            this.btnEliminarLinea.Name = "btnEliminarLinea";
            this.btnEliminarLinea.Size = new System.Drawing.Size(36, 38);
            this.btnEliminarLinea.TabIndex = 50;
            this.ttMensaje.SetToolTip(this.btnEliminarLinea, "Clic auí para eliminar el registro seleccionado");
            this.btnEliminarLinea.UseVisualStyleBackColor = true;
            this.btnEliminarLinea.Visible = false;
            this.btnEliminarLinea.Click += new System.EventHandler(this.btnEliminarLinea_Click);
            // 
            // btnNuevaLinea
            // 
            this.btnNuevaLinea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNuevaLinea.FlatAppearance.BorderSize = 0;
            this.btnNuevaLinea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaLinea.ForeColor = System.Drawing.Color.Transparent;
            this.btnNuevaLinea.Image = global::Palatium.Properties.Resources.icono_enviar_grid;
            this.btnNuevaLinea.Location = new System.Drawing.Point(334, 22);
            this.btnNuevaLinea.Name = "btnNuevaLinea";
            this.btnNuevaLinea.Size = new System.Drawing.Size(36, 38);
            this.btnNuevaLinea.TabIndex = 49;
            this.ttMensaje.SetToolTip(this.btnNuevaLinea, "Clic auí para añadir a la lista");
            this.btnNuevaLinea.UseVisualStyleBackColor = true;
            this.btnNuevaLinea.Click += new System.EventHandler(this.btnNuevaLinea_Click);
            // 
            // cmbReportes
            // 
            this.cmbReportes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReportes.FormattingEnabled = true;
            this.cmbReportes.Location = new System.Drawing.Point(87, 30);
            this.cmbReportes.Name = "cmbReportes";
            this.cmbReportes.Size = new System.Drawing.Size(234, 21);
            this.cmbReportes.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 30);
            this.label2.TabIndex = 46;
            this.label2.Text = "Seleccione\r\nReporte:";
            // 
            // txtOrden
            // 
            this.txtOrden.Location = new System.Drawing.Point(87, 77);
            this.txtOrden.MaxLength = 2;
            this.txtOrden.Name = "txtOrden";
            this.txtOrden.Size = new System.Drawing.Size(73, 20);
            this.txtOrden.TabIndex = 45;
            this.txtOrden.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOrden_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 44;
            this.label1.Text = "Orden: *";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(164, 12);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(98, 39);
            this.btnLimpiar.TabIndex = 48;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Blue;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(16, 12);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(98, 39);
            this.btnGuardar.TabIndex = 46;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // grupoBotones
            // 
            this.grupoBotones.Controls.Add(this.btnQuitarSeleccion);
            this.grupoBotones.Controls.Add(this.btnLimpiar);
            this.grupoBotones.Controls.Add(this.btnGuardar);
            this.grupoBotones.Enabled = false;
            this.grupoBotones.Location = new System.Drawing.Point(12, 194);
            this.grupoBotones.Name = "grupoBotones";
            this.grupoBotones.Size = new System.Drawing.Size(376, 57);
            this.grupoBotones.TabIndex = 49;
            this.grupoBotones.TabStop = false;
            // 
            // btnQuitarSeleccion
            // 
            this.btnQuitarSeleccion.BackColor = System.Drawing.Color.Red;
            this.btnQuitarSeleccion.ForeColor = System.Drawing.Color.White;
            this.btnQuitarSeleccion.Location = new System.Drawing.Point(268, 12);
            this.btnQuitarSeleccion.Name = "btnQuitarSeleccion";
            this.btnQuitarSeleccion.Size = new System.Drawing.Size(98, 39);
            this.btnQuitarSeleccion.TabIndex = 49;
            this.btnQuitarSeleccion.Text = "Quitar Selección";
            this.btnQuitarSeleccion.UseVisualStyleBackColor = false;
            this.btnQuitarSeleccion.Click += new System.EventHandler(this.btnQuitarSeleccion_Click);
            // 
            // id_pos_reportes_cierre
            // 
            this.id_pos_reportes_cierre.HeaderText = "ID_1";
            this.id_pos_reportes_cierre.Name = "id_pos_reportes_cierre";
            this.id_pos_reportes_cierre.ReadOnly = true;
            this.id_pos_reportes_cierre.Visible = false;
            // 
            // id_pos_reportes_cierre_por_localidad
            // 
            this.id_pos_reportes_cierre_por_localidad.HeaderText = "ID_2";
            this.id_pos_reportes_cierre_por_localidad.Name = "id_pos_reportes_cierre_por_localidad";
            this.id_pos_reportes_cierre_por_localidad.ReadOnly = true;
            this.id_pos_reportes_cierre_por_localidad.Visible = false;
            // 
            // id_localidad
            // 
            this.id_localidad.HeaderText = "ID LOCALIDAD";
            this.id_localidad.Name = "id_localidad";
            this.id_localidad.ReadOnly = true;
            this.id_localidad.Visible = false;
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "NOMBRE DEL REPORTE";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 250;
            // 
            // orden
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.orden.DefaultCellStyle = dataGridViewCellStyle1;
            this.orden.HeaderText = "ORDEN";
            this.orden.Name = "orden";
            this.orden.ReadOnly = true;
            // 
            // en_base
            // 
            this.en_base.HeaderText = "EN BASE";
            this.en_base.Name = "en_base";
            this.en_base.ReadOnly = true;
            this.en_base.Visible = false;
            // 
            // frmReportesCierre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(815, 261);
            this.Controls.Add(this.grupoBotones);
            this.Controls.Add(this.grupoDatos);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmReportesCierre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes Cierre";
            this.Load += new System.EventHandler(this.frmReportesCierre_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grupoDatos.ResumeLayout(false);
            this.grupoDatos.PerformLayout();
            this.grupoBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private System.Windows.Forms.Label lblEmpresa;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox grupoDatos;
        private System.Windows.Forms.TextBox txtOrden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbReportes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnNuevaLinea;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Button btnEliminarLinea;
        private System.Windows.Forms.GroupBox grupoBotones;
        private System.Windows.Forms.Button btnAgregarReportes;
        private System.Windows.Forms.Button btnQuitarSeleccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_reportes_cierre;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_reportes_cierre_por_localidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_localidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn orden;
        private System.Windows.Forms.DataGridViewTextBoxColumn en_base;
    }
}