namespace Palatium.Oficina
{
    partial class frmModificarLote
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbLocalidades = new System.Windows.Forms.ComboBox();
            this.pnlModificar = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.txtNuevoLote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLoteActual = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDesde = new MetroFramework.Controls.MetroDateTime();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.id_pos_numero_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_localidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha_apertura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numero_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_lote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.pnlModificar.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.groupBox1.Controls.Add(this.cmbLocalidades);
            this.groupBox1.Controls.Add(this.pnlModificar);
            this.groupBox1.Controls.Add(this.btnLimpiar);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDesde);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(698, 143);
            this.groupBox1.TabIndex = 143;
            this.groupBox1.TabStop = false;
            // 
            // cmbLocalidades
            // 
            this.cmbLocalidades.BackColor = System.Drawing.Color.White;
            this.cmbLocalidades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocalidades.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbLocalidades.FormattingEnabled = true;
            this.cmbLocalidades.Location = new System.Drawing.Point(102, 19);
            this.cmbLocalidades.Name = "cmbLocalidades";
            this.cmbLocalidades.Size = new System.Drawing.Size(252, 28);
            this.cmbLocalidades.TabIndex = 152;
            // 
            // pnlModificar
            // 
            this.pnlModificar.Controls.Add(this.btnActualizar);
            this.pnlModificar.Controls.Add(this.txtNuevoLote);
            this.pnlModificar.Controls.Add(this.label2);
            this.pnlModificar.Controls.Add(this.label3);
            this.pnlModificar.Controls.Add(this.txtLoteActual);
            this.pnlModificar.Location = new System.Drawing.Point(398, 19);
            this.pnlModificar.Name = "pnlModificar";
            this.pnlModificar.Size = new System.Drawing.Size(271, 114);
            this.pnlModificar.TabIndex = 152;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnActualizar.Enabled = false;
            this.btnActualizar.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnActualizar.Location = new System.Drawing.Point(103, 67);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(165, 45);
            this.btnActualizar.TabIndex = 151;
            this.btnActualizar.Text = "ACTUALIZAR";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // txtNuevoLote
            // 
            this.txtNuevoLote.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtNuevoLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtNuevoLote.Location = new System.Drawing.Point(103, 35);
            this.txtNuevoLote.MaxLength = 10;
            this.txtNuevoLote.Name = "txtNuevoLote";
            this.txtNuevoLote.ReadOnly = true;
            this.txtNuevoLote.Size = new System.Drawing.Size(165, 26);
            this.txtNuevoLote.TabIndex = 150;
            this.txtNuevoLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNuevoLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNuevoLote_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 147;
            this.label2.Text = "Lote Actual:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 18);
            this.label3.TabIndex = 149;
            this.label3.Text = "Lote Nuevo:";
            // 
            // txtLoteActual
            // 
            this.txtLoteActual.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLoteActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtLoteActual.Location = new System.Drawing.Point(103, 4);
            this.txtLoteActual.MaxLength = 10;
            this.txtLoteActual.Name = "txtLoteActual";
            this.txtLoteActual.ReadOnly = true;
            this.txtLoteActual.Size = new System.Drawing.Size(165, 26);
            this.txtLoteActual.TabIndex = 148;
            this.txtLoteActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLoteActual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLoteActual_KeyPress);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.OrangeRed;
            this.btnLimpiar.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold);
            this.btnLimpiar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLimpiar.Location = new System.Drawing.Point(247, 88);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(107, 45);
            this.btnLimpiar.TabIndex = 146;
            this.btnLimpiar.Text = "LIMPIAR";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnBuscar.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Location = new System.Drawing.Point(141, 88);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 45);
            this.btnBuscar.TabIndex = 138;
            this.btnBuscar.Text = "BUSCAR";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 17;
            this.label1.Text = "Localidad:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 18);
            this.label9.TabIndex = 13;
            this.label9.Text = "Fecha:";
            // 
            // txtDesde
            // 
            this.txtDesde.Location = new System.Drawing.Point(102, 53);
            this.txtDesde.MinimumSize = new System.Drawing.Size(0, 29);
            this.txtDesde.Name = "txtDesde";
            this.txtDesde.Size = new System.Drawing.Size(252, 29);
            this.txtDesde.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvDatos);
            this.groupBox2.Location = new System.Drawing.Point(12, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(698, 267);
            this.groupBox2.TabIndex = 144;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Información";
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
            this.id_pos_numero_lote,
            this.id_localidad,
            this.fecha_apertura,
            this.descripcion,
            this.numero_lote,
            this.estado_lote});
            this.dgvDatos.Location = new System.Drawing.Point(15, 28);
            this.dgvDatos.MultiSelect = false;
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(670, 220);
            this.dgvDatos.TabIndex = 0;
            this.dgvDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatos_CellDoubleClick);
            // 
            // id_pos_numero_lote
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.id_pos_numero_lote.DefaultCellStyle = dataGridViewCellStyle1;
            this.id_pos_numero_lote.HeaderText = "ID_NUMERO_LOTE";
            this.id_pos_numero_lote.Name = "id_pos_numero_lote";
            this.id_pos_numero_lote.ReadOnly = true;
            this.id_pos_numero_lote.Visible = false;
            this.id_pos_numero_lote.Width = 240;
            // 
            // id_localidad
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.id_localidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.id_localidad.HeaderText = "ID_LOCALIDAD";
            this.id_localidad.Name = "id_localidad";
            this.id_localidad.ReadOnly = true;
            this.id_localidad.Visible = false;
            this.id_localidad.Width = 120;
            // 
            // fecha_apertura
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.fecha_apertura.DefaultCellStyle = dataGridViewCellStyle3;
            this.fecha_apertura.HeaderText = "FECHA APERTURA";
            this.fecha_apertura.Name = "fecha_apertura";
            this.fecha_apertura.ReadOnly = true;
            this.fecha_apertura.Width = 150;
            // 
            // descripcion
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.descripcion.DefaultCellStyle = dataGridViewCellStyle4;
            this.descripcion.HeaderText = "DESCRIPCION";
            this.descripcion.Name = "descripcion";
            this.descripcion.ReadOnly = true;
            this.descripcion.Width = 200;
            // 
            // numero_lote
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.numero_lote.DefaultCellStyle = dataGridViewCellStyle5;
            this.numero_lote.HeaderText = "NÚMERO DE LOTE";
            this.numero_lote.Name = "numero_lote";
            this.numero_lote.ReadOnly = true;
            this.numero_lote.Width = 150;
            // 
            // estado_lote
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado_lote.DefaultCellStyle = dataGridViewCellStyle6;
            this.estado_lote.HeaderText = "ESTADO LOTE";
            this.estado_lote.Name = "estado_lote";
            this.estado_lote.ReadOnly = true;
            this.estado_lote.Width = 130;
            // 
            // frmModificarLote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(720, 434);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModificarLote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo para modificar el número de lote abierto";
            this.Load += new System.EventHandler(this.frmModificarLote_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlModificar.ResumeLayout(false);
            this.pnlModificar.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroDateTime txtDesde;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Panel pnlModificar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.TextBox txtNuevoLote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLoteActual;
        private System.Windows.Forms.ComboBox cmbLocalidades;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_numero_lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_localidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha_apertura;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn numero_lote;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_lote;
    }
}