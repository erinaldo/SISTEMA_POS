namespace Palatium.Areas
{
    partial class frmSeccionMesas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPisos = new System.Windows.Forms.Label();
            this.pnlMesas = new System.Windows.Forms.Panel();
            this.btnCombinar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalirMesa = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.pnlSecciones = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtMesa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.mesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPisos
            // 
            this.lblPisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPisos.ForeColor = System.Drawing.Color.Blue;
            this.lblPisos.Location = new System.Drawing.Point(891, 9);
            this.lblPisos.Name = "lblPisos";
            this.lblPisos.Size = new System.Drawing.Size(247, 58);
            this.lblPisos.TabIndex = 11;
            this.lblPisos.Text = "PISO";
            this.lblPisos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnlMesas
            // 
            this.pnlMesas.AutoScroll = true;
            this.pnlMesas.BackColor = System.Drawing.Color.LemonChiffon;
            this.pnlMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMesas.Location = new System.Drawing.Point(20, 125);
            this.pnlMesas.Name = "pnlMesas";
            this.pnlMesas.Size = new System.Drawing.Size(850, 525);
            this.pnlMesas.TabIndex = 10;
            // 
            // btnCombinar
            // 
            this.btnCombinar.AccessibleDescription = "";
            this.btnCombinar.BackColor = System.Drawing.Color.Blue;
            this.btnCombinar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCombinar.FlatAppearance.BorderSize = 2;
            this.btnCombinar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCombinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCombinar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCombinar.Image = global::Palatium.Properties.Resources.combinar_comanda;
            this.btnCombinar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCombinar.Location = new System.Drawing.Point(881, 467);
            this.btnCombinar.Name = "btnCombinar";
            this.btnCombinar.Size = new System.Drawing.Size(270, 93);
            this.btnCombinar.TabIndex = 17;
            this.btnCombinar.Text = "COMBINAR\r\nCOMANDAS\r\nDE MESAS";
            this.btnCombinar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCombinar.UseVisualStyleBackColor = false;
            this.btnCombinar.Click += new System.EventHandler(this.btnCombinar_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.AccessibleDescription = "";
            this.btnActualizar.BackColor = System.Drawing.Color.Blue;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 2;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnActualizar.Image = global::Palatium.Properties.Resources.actualizar_mesas;
            this.btnActualizar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnActualizar.Location = new System.Drawing.Point(881, 557);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(135, 93);
            this.btnActualizar.TabIndex = 16;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnSalirMesa
            // 
            this.btnSalirMesa.BackColor = System.Drawing.Color.Blue;
            this.btnSalirMesa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalirMesa.FlatAppearance.BorderSize = 2;
            this.btnSalirMesa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalirMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalirMesa.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSalirMesa.Image = global::Palatium.Properties.Resources.salir_mesas;
            this.btnSalirMesa.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalirMesa.Location = new System.Drawing.Point(1016, 557);
            this.btnSalirMesa.Name = "btnSalirMesa";
            this.btnSalirMesa.Size = new System.Drawing.Size(135, 93);
            this.btnSalirMesa.TabIndex = 15;
            this.btnSalirMesa.Text = "Salir";
            this.btnSalirMesa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalirMesa.UseVisualStyleBackColor = false;
            this.btnSalirMesa.Click += new System.EventHandler(this.btnSalirMesa_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.ForeColor = System.Drawing.Color.Transparent;
            this.btnSiguiente.Image = global::Palatium.Properties.Resources.derecha;
            this.btnSiguiente.Location = new System.Drawing.Point(786, 37);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(53, 51);
            this.btnSiguiente.TabIndex = 20;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Visible = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnterior.Enabled = false;
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.ForeColor = System.Drawing.Color.Transparent;
            this.btnAnterior.Image = global::Palatium.Properties.Resources.izquierda;
            this.btnAnterior.Location = new System.Drawing.Point(11, 37);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(53, 51);
            this.btnAnterior.TabIndex = 19;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Visible = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // pnlSecciones
            // 
            this.pnlSecciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pnlSecciones.Location = new System.Drawing.Point(74, 17);
            this.pnlSecciones.Name = "pnlSecciones";
            this.pnlSecciones.Size = new System.Drawing.Size(700, 90);
            this.pnlSecciones.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.pnlSecciones);
            this.panel2.Controls.Add(this.btnAnterior);
            this.panel2.Controls.Add(this.btnSiguiente);
            this.panel2.Location = new System.Drawing.Point(20, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 118);
            this.panel2.TabIndex = 21;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBuscar.Location = new System.Drawing.Point(1098, 80);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(40, 39);
            this.btnBuscar.TabIndex = 24;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtMesa
            // 
            this.txtMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtMesa.Location = new System.Drawing.Point(991, 84);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.Size = new System.Drawing.Size(85, 29);
            this.txtMesa.TabIndex = 23;
            this.txtMesa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMesa.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMesa_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(898, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "MESAS:";
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
            this.mesa,
            this.estado,
            this.tiempo});
            this.dgvDatos.Location = new System.Drawing.Point(881, 125);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(271, 336);
            this.dgvDatos.TabIndex = 25;
            // 
            // mesa
            // 
            this.mesa.HeaderText = "MESA";
            this.mesa.Name = "mesa";
            this.mesa.ReadOnly = true;
            this.mesa.Width = 90;
            // 
            // estado
            // 
            this.estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // tiempo
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tiempo.DefaultCellStyle = dataGridViewCellStyle2;
            this.tiempo.HeaderText = "TIEMPO";
            this.tiempo.Name = "tiempo";
            this.tiempo.ReadOnly = true;
            this.tiempo.Width = 60;
            // 
            // timerBlink
            // 
            this.timerBlink.Enabled = true;
            this.timerBlink.Tick += new System.EventHandler(this.timerBlink_Tick);
            // 
            // frmSeccionMesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1164, 661);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtMesa);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblPisos);
            this.Controls.Add(this.btnCombinar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnSalirMesa);
            this.Controls.Add(this.pnlMesas);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeccionMesas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Área del Restaurante";
            this.Load += new System.EventHandler(this.frmSeccionMesas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSeccionMesas_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPisos;
        private System.Windows.Forms.Panel pnlMesas;
        private System.Windows.Forms.Button btnCombinar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalirMesa;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Panel pnlSecciones;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtMesa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn mesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo;
    }
}