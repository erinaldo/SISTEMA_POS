namespace Palatium.ValesConsumos
{
    partial class frmModalSeleccionValesConsumo
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
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.btnAgregarNormal = new System.Windows.Forms.Button();
            this.dBAyudaPersonas = new Controles.Auxiliares.DB_Ayuda();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAgregarNomina = new System.Windows.Forms.Button();
            this.dbAyudaNomina = new Controles.Auxiliares.DB_Ayuda();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAreas = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.id_persona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.en_nomina = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pos_area_consumo_empleados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.identificacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razón_social = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemover
            // 
            this.btnRemover.BackColor = System.Drawing.Color.Red;
            this.btnRemover.ForeColor = System.Drawing.Color.White;
            this.btnRemover.Location = new System.Drawing.Point(384, 378);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(93, 39);
            this.btnRemover.TabIndex = 39;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = false;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(478, 378);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(93, 39);
            this.btnSalir.TabIndex = 38;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Blue;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(291, 378);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(93, 39);
            this.btnGuardar.TabIndex = 37;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeColumns = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_persona,
            this.en_nomina,
            this.id_empleado,
            this.id_pos_area_consumo_empleados,
            this.identificacion,
            this.razón_social});
            this.dgvDatos.Location = new System.Drawing.Point(12, 130);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(559, 242);
            this.dgvDatos.TabIndex = 36;
            // 
            // btnAgregarNormal
            // 
            this.btnAgregarNormal.BackColor = System.Drawing.Color.Blue;
            this.btnAgregarNormal.ForeColor = System.Drawing.Color.White;
            this.btnAgregarNormal.Location = new System.Drawing.Point(470, 10);
            this.btnAgregarNormal.Name = "btnAgregarNormal";
            this.btnAgregarNormal.Size = new System.Drawing.Size(71, 31);
            this.btnAgregarNormal.TabIndex = 2;
            this.btnAgregarNormal.Text = "Agregar";
            this.btnAgregarNormal.UseVisualStyleBackColor = false;
            this.btnAgregarNormal.Click += new System.EventHandler(this.btnAgregarNormal_Click);
            // 
            // dBAyudaPersonas
            // 
            this.dBAyudaPersonas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dBAyudaPersonas.iId = 21;
            this.dBAyudaPersonas.Location = new System.Drawing.Point(8, 15);
            this.dBAyudaPersonas.Margin = new System.Windows.Forms.Padding(4);
            this.dBAyudaPersonas.Name = "dBAyudaPersonas";
            this.dBAyudaPersonas.sCodigo = null;
            this.dBAyudaPersonas.Size = new System.Drawing.Size(471, 22);
            this.dBAyudaPersonas.sNombre = null;
            this.dBAyudaPersonas.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(551, 70);
            this.tabControl1.TabIndex = 40;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.tabPage1.Controls.Add(this.btnAgregarNormal);
            this.tabPage1.Controls.Add(this.dBAyudaPersonas);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(543, 44);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Buscar en Registros Generales";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.tabPage2.Controls.Add(this.btnAgregarNomina);
            this.tabPage2.Controls.Add(this.dbAyudaNomina);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(543, 44);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Buscar en Nómina";
            // 
            // btnAgregarNomina
            // 
            this.btnAgregarNomina.BackColor = System.Drawing.Color.Blue;
            this.btnAgregarNomina.ForeColor = System.Drawing.Color.White;
            this.btnAgregarNomina.Location = new System.Drawing.Point(470, 10);
            this.btnAgregarNomina.Name = "btnAgregarNomina";
            this.btnAgregarNomina.Size = new System.Drawing.Size(71, 31);
            this.btnAgregarNomina.TabIndex = 4;
            this.btnAgregarNomina.Text = "Agregar";
            this.btnAgregarNomina.UseVisualStyleBackColor = false;
            this.btnAgregarNomina.Click += new System.EventHandler(this.btnAgregarNomina_Click);
            // 
            // dbAyudaNomina
            // 
            this.dbAyudaNomina.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dbAyudaNomina.iId = 21;
            this.dbAyudaNomina.Location = new System.Drawing.Point(8, 15);
            this.dbAyudaNomina.Margin = new System.Windows.Forms.Padding(4);
            this.dbAyudaNomina.Name = "dbAyudaNomina";
            this.dbAyudaNomina.sCodigo = null;
            this.dbAyudaNomina.Size = new System.Drawing.Size(471, 22);
            this.dbAyudaNomina.sNombre = null;
            this.dbAyudaNomina.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 42;
            this.label1.Text = "Área:";
            // 
            // cmbAreas
            // 
            this.cmbAreas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAreas.FormattingEnabled = true;
            this.cmbAreas.Location = new System.Drawing.Point(176, 79);
            this.cmbAreas.Name = "cmbAreas";
            this.cmbAreas.Size = new System.Drawing.Size(248, 21);
            this.cmbAreas.TabIndex = 43;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.panel1.Controls.Add(this.cmbAreas);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(557, 112);
            this.panel1.TabIndex = 44;
            // 
            // id_persona
            // 
            this.id_persona.HeaderText = "ID PERSONA";
            this.id_persona.Name = "id_persona";
            this.id_persona.ReadOnly = true;
            this.id_persona.Visible = false;
            // 
            // en_nomina
            // 
            this.en_nomina.HeaderText = "NOMINA";
            this.en_nomina.Name = "en_nomina";
            this.en_nomina.ReadOnly = true;
            this.en_nomina.Visible = false;
            // 
            // id_empleado
            // 
            this.id_empleado.HeaderText = "ID_EMPLEADO";
            this.id_empleado.Name = "id_empleado";
            this.id_empleado.ReadOnly = true;
            this.id_empleado.Visible = false;
            // 
            // id_pos_area_consumo_empleados
            // 
            this.id_pos_area_consumo_empleados.HeaderText = "ID_AREA";
            this.id_pos_area_consumo_empleados.Name = "id_pos_area_consumo_empleados";
            this.id_pos_area_consumo_empleados.ReadOnly = true;
            this.id_pos_area_consumo_empleados.Visible = false;
            // 
            // identificacion
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.identificacion.DefaultCellStyle = dataGridViewCellStyle2;
            this.identificacion.HeaderText = "IDENTIFICACIÓN";
            this.identificacion.Name = "identificacion";
            this.identificacion.ReadOnly = true;
            this.identificacion.Width = 170;
            // 
            // razón_social
            // 
            this.razón_social.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.razón_social.HeaderText = "RAZÓN SOCIAL";
            this.razón_social.Name = "razón_social";
            this.razón_social.ReadOnly = true;
            // 
            // frmModalSeleccionValesConsumo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(584, 428);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvDatos);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModalSeleccionValesConsumo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selección de Datos";
            this.Load += new System.EventHandler(this.frmModalSeleccionValesConsumo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModalSeleccionValesConsumo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRemover;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnAgregarNormal;
        private Controles.Auxiliares.DB_Ayuda dBAyudaPersonas;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnAgregarNomina;
        private Controles.Auxiliares.DB_Ayuda dbAyudaNomina;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAreas;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_persona;
        private System.Windows.Forms.DataGridViewTextBoxColumn en_nomina;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pos_area_consumo_empleados;
        private System.Windows.Forms.DataGridViewTextBoxColumn identificacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn razón_social;
    }
}