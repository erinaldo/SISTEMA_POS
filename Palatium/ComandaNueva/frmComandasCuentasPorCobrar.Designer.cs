namespace Palatium.ComandaNueva
{
    partial class frmComandasCuentasPorCobrar
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
            this.pnlOrdenes = new System.Windows.Forms.Panel();
            this.dtFecha = new System.Windows.Forms.DateTimePicker();
            this.btnSubirComandas = new System.Windows.Forms.Button();
            this.btnBajarComandas = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOrdenes
            // 
            this.pnlOrdenes.AutoScroll = true;
            this.pnlOrdenes.BackColor = System.Drawing.Color.LemonChiffon;
            this.pnlOrdenes.Location = new System.Drawing.Point(11, 140);
            this.pnlOrdenes.Margin = new System.Windows.Forms.Padding(2);
            this.pnlOrdenes.Name = "pnlOrdenes";
            this.pnlOrdenes.Size = new System.Drawing.Size(651, 421);
            this.pnlOrdenes.TabIndex = 3;
            // 
            // dtFecha
            // 
            this.dtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFecha.Location = new System.Drawing.Point(243, 16);
            this.dtFecha.Name = "dtFecha";
            this.dtFecha.Size = new System.Drawing.Size(119, 24);
            this.dtFecha.TabIndex = 2;
            // 
            // btnSubirComandas
            // 
            this.btnSubirComandas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSubirComandas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSubirComandas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSubirComandas.Enabled = false;
            this.btnSubirComandas.FlatAppearance.BorderSize = 0;
            this.btnSubirComandas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubirComandas.ForeColor = System.Drawing.Color.Transparent;
            this.btnSubirComandas.Image = global::Palatium.Properties.Resources.arriba_2;
            this.btnSubirComandas.Location = new System.Drawing.Point(575, 8);
            this.btnSubirComandas.Name = "btnSubirComandas";
            this.btnSubirComandas.Size = new System.Drawing.Size(53, 51);
            this.btnSubirComandas.TabIndex = 4;
            this.btnSubirComandas.UseVisualStyleBackColor = false;
            this.btnSubirComandas.Visible = false;
            // 
            // btnBajarComandas
            // 
            this.btnBajarComandas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnBajarComandas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBajarComandas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBajarComandas.FlatAppearance.BorderSize = 0;
            this.btnBajarComandas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBajarComandas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBajarComandas.ForeColor = System.Drawing.Color.Transparent;
            this.btnBajarComandas.Image = global::Palatium.Properties.Resources.abajo_2;
            this.btnBajarComandas.Location = new System.Drawing.Point(575, 60);
            this.btnBajarComandas.Name = "btnBajarComandas";
            this.btnBajarComandas.Size = new System.Drawing.Size(53, 51);
            this.btnBajarComandas.TabIndex = 5;
            this.btnBajarComandas.UseVisualStyleBackColor = false;
            this.btnBajarComandas.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 25);
            this.label1.TabIndex = 23;
            this.label1.Text = "Buscar por Fecha:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.AccessibleDescription = "";
            this.btnBuscar.BackColor = System.Drawing.Color.Blue;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 2;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBuscar.Image = global::Palatium.Properties.Resources.buscar_botnon;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(406, 22);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(135, 77);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBusqueda);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBuscar);
            this.groupBox1.Controls.Add(this.btnBajarComandas);
            this.groupBox1.Controls.Add(this.btnSubirComandas);
            this.groupBox1.Controls.Add(this.dtFecha);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 118);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Búsqueda:";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtBusqueda.Location = new System.Drawing.Point(21, 76);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(341, 24);
            this.txtBusqueda.TabIndex = 25;
            // 
            // frmComandasCuentasPorCobrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(674, 571);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlOrdenes);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComandasCuentasPorCobrar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Módulo de Comandas Pendientes de Cobro";
            this.Load += new System.EventHandler(this.frmComandasCuentasPorCobrar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOrdenes;
        private System.Windows.Forms.DateTimePicker dtFecha;
        private System.Windows.Forms.Button btnSubirComandas;
        private System.Windows.Forms.Button btnBajarComandas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label2;
    }
}