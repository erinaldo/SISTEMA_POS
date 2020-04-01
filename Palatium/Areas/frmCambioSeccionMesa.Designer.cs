namespace Palatium.Areas
{
    partial class frmCambioSeccionMesa
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSecciones = new System.Windows.Forms.Panel();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalirMesa = new System.Windows.Forms.Button();
            this.pnlMesas = new System.Windows.Forms.Panel();
            this.timerBlink = new System.Windows.Forms.Timer(this.components);
            this.lblPisos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ttMensaje
            // 
            this.ttMensaje.Popup += new System.Windows.Forms.PopupEventHandler(this.ttMensaje_Popup);
            // 
            // tiempo
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tiempo.DefaultCellStyle = dataGridViewCellStyle12;
            this.tiempo.HeaderText = "TIEMPO";
            this.tiempo.Name = "tiempo";
            this.tiempo.ReadOnly = true;
            this.tiempo.Width = 60;
            // 
            // estado
            // 
            this.estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.estado.HeaderText = "ESTADO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // mesa
            // 
            this.mesa.HeaderText = "MESA";
            this.mesa.Name = "mesa";
            this.mesa.ReadOnly = true;
            this.mesa.Width = 90;
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
            this.dgvDatos.Location = new System.Drawing.Point(883, 125);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(271, 426);
            this.dgvDatos.TabIndex = 35;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.pnlSecciones);
            this.panel2.Controls.Add(this.btnAnterior);
            this.panel2.Controls.Add(this.btnSiguiente);
            this.panel2.Location = new System.Drawing.Point(22, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 118);
            this.panel2.TabIndex = 31;
            // 
            // pnlSecciones
            // 
            this.pnlSecciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.pnlSecciones.Location = new System.Drawing.Point(74, 16);
            this.pnlSecciones.Name = "pnlSecciones";
            this.pnlSecciones.Size = new System.Drawing.Size(700, 90);
            this.pnlSecciones.TabIndex = 18;
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
            this.btnAnterior.Location = new System.Drawing.Point(11, 36);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(53, 51);
            this.btnAnterior.TabIndex = 19;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Visible = false;
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
            this.btnSiguiente.Location = new System.Drawing.Point(786, 36);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(53, 51);
            this.btnSiguiente.TabIndex = 20;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Visible = false;
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
            this.btnActualizar.Location = new System.Drawing.Point(883, 557);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(135, 93);
            this.btnActualizar.TabIndex = 29;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnActualizar.UseVisualStyleBackColor = false;
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
            this.btnSalirMesa.Location = new System.Drawing.Point(1018, 557);
            this.btnSalirMesa.Name = "btnSalirMesa";
            this.btnSalirMesa.Size = new System.Drawing.Size(135, 93);
            this.btnSalirMesa.TabIndex = 28;
            this.btnSalirMesa.Text = "Salir";
            this.btnSalirMesa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalirMesa.UseVisualStyleBackColor = false;
            this.btnSalirMesa.Click += new System.EventHandler(this.btnSalirMesa_Click);
            // 
            // pnlMesas
            // 
            this.pnlMesas.AutoScroll = true;
            this.pnlMesas.BackColor = System.Drawing.Color.LemonChiffon;
            this.pnlMesas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlMesas.Location = new System.Drawing.Point(22, 125);
            this.pnlMesas.Name = "pnlMesas";
            this.pnlMesas.Size = new System.Drawing.Size(850, 525);
            this.pnlMesas.TabIndex = 26;
            // 
            // timerBlink
            // 
            this.timerBlink.Enabled = true;
            // 
            // lblPisos
            // 
            this.lblPisos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPisos.ForeColor = System.Drawing.Color.Blue;
            this.lblPisos.Location = new System.Drawing.Point(892, 17);
            this.lblPisos.Name = "lblPisos";
            this.lblPisos.Size = new System.Drawing.Size(247, 58);
            this.lblPisos.TabIndex = 27;
            this.lblPisos.Text = "PISO";
            this.lblPisos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmCambioSeccionMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1164, 661);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnSalirMesa);
            this.Controls.Add(this.pnlMesas);
            this.Controls.Add(this.lblPisos);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCambioSeccionMesa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambio de Sección de Mesa";
            this.Load += new System.EventHandler(this.frmCambioSeccionMesa_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCambioSeccionMesa_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn mesa;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlSecciones;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalirMesa;
        private System.Windows.Forms.Panel pnlMesas;
        private System.Windows.Forms.Timer timerBlink;
        private System.Windows.Forms.Label lblPisos;
    }
}