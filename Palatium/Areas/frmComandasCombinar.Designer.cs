namespace Palatium.Areas
{
    partial class frmComandasCombinar
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
            this.pnlComandas = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.id_pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remover = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnCombinar = new System.Windows.Forms.Button();
            this.btnSalirMesa = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlComandas
            // 
            this.pnlComandas.AutoScroll = true;
            this.pnlComandas.Location = new System.Drawing.Point(12, 12);
            this.pnlComandas.Name = "pnlComandas";
            this.pnlComandas.Size = new System.Drawing.Size(782, 637);
            this.pnlComandas.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(831, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 48);
            this.label2.TabIndex = 23;
            this.label2.Text = "COMANDAS SELECCIONADAS\r\nA COMBINAR";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.id_pedido,
            this.mesa,
            this.estado,
            this.remover});
            this.dgvDatos.Location = new System.Drawing.Point(810, 81);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.RowHeadersVisible = false;
            this.dgvDatos.RowTemplate.Height = 30;
            this.dgvDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatos.Size = new System.Drawing.Size(340, 350);
            this.dgvDatos.TabIndex = 26;
            // 
            // id_pedido
            // 
            this.id_pedido.HeaderText = "ID_PEDIDO";
            this.id_pedido.Name = "id_pedido";
            this.id_pedido.ReadOnly = true;
            this.id_pedido.Visible = false;
            // 
            // mesa
            // 
            this.mesa.HeaderText = "MESA";
            this.mesa.Name = "mesa";
            this.mesa.ReadOnly = true;
            this.mesa.Width = 150;
            // 
            // estado
            // 
            this.estado.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.estado.DefaultCellStyle = dataGridViewCellStyle1;
            this.estado.HeaderText = "PEDIDO";
            this.estado.Name = "estado";
            this.estado.ReadOnly = true;
            // 
            // remover
            // 
            this.remover.HeaderText = "REMOVER";
            this.remover.Image = global::Palatium.Properties.Resources.icono_grid_editar_comanda;
            this.remover.Name = "remover";
            this.remover.ReadOnly = true;
            // 
            // btnCombinar
            // 
            this.btnCombinar.AccessibleDescription = "";
            this.btnCombinar.BackColor = System.Drawing.Color.Blue;
            this.btnCombinar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCombinar.FlatAppearance.BorderSize = 2;
            this.btnCombinar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCombinar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCombinar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCombinar.Image = global::Palatium.Properties.Resources.actualizar_mesas;
            this.btnCombinar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCombinar.Location = new System.Drawing.Point(810, 556);
            this.btnCombinar.Name = "btnCombinar";
            this.btnCombinar.Size = new System.Drawing.Size(170, 93);
            this.btnCombinar.TabIndex = 28;
            this.btnCombinar.Text = "Combinar";
            this.btnCombinar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCombinar.UseVisualStyleBackColor = false;
            this.btnCombinar.Click += new System.EventHandler(this.btnCombinar_Click);
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
            this.btnSalirMesa.Location = new System.Drawing.Point(980, 556);
            this.btnSalirMesa.Name = "btnSalirMesa";
            this.btnSalirMesa.Size = new System.Drawing.Size(170, 93);
            this.btnSalirMesa.TabIndex = 27;
            this.btnSalirMesa.Text = "Salir";
            this.btnSalirMesa.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalirMesa.UseVisualStyleBackColor = false;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiguiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.Image = global::Palatium.Properties.Resources.derecha;
            this.btnSiguiente.Location = new System.Drawing.Point(980, 455);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(170, 71);
            this.btnSiguiente.TabIndex = 30;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Image = global::Palatium.Properties.Resources.izquierda;
            this.btnAnterior.Location = new System.Drawing.Point(810, 455);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(170, 71);
            this.btnAnterior.TabIndex = 29;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Visible = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // frmComandasCombinar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1164, 661);
            this.Controls.Add(this.btnSiguiente);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnCombinar);
            this.Controls.Add(this.btnSalirMesa);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlComandas);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComandasCombinar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo par combinar comandas de mesa";
            this.Load += new System.EventHandler(this.frmComandasCombinar_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmComandasCombinar_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlComandas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnCombinar;
        private System.Windows.Forms.Button btnSalirMesa;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn mesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewImageColumn remover;
    }
}