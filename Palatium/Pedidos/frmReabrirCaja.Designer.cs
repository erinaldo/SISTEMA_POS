namespace Palatium.Pedidos
{
    partial class frmReabrirCaja
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
            this.dgvInformacion = new System.Windows.Forms.DataGridView();
            this.cmbJornada = new MisControles.ComboDatos();
            this.cmbLocalidad = new MisControles.ComboDatos();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnExtraer = new System.Windows.Forms.Button();
            this.Calendario = new System.Windows.Forms.MonthCalendar();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.LblAbrirTodos = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LblEstado = new System.Windows.Forms.Label();
            this.LblCierre = new System.Windows.Forms.Label();
            this.LblApertura = new System.Windows.Forms.Label();
            this.LblCajero = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnReabrir = new System.Windows.Forms.Button();
            this.LblId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInformacion)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvInformacion
            // 
            this.dgvInformacion.AllowUserToAddRows = false;
            this.dgvInformacion.AllowUserToDeleteRows = false;
            this.dgvInformacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInformacion.Location = new System.Drawing.Point(24, 259);
            this.dgvInformacion.Name = "dgvInformacion";
            this.dgvInformacion.ReadOnly = true;
            this.dgvInformacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInformacion.Size = new System.Drawing.Size(455, 177);
            this.dgvInformacion.TabIndex = 0;
            this.dgvInformacion.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInformacion_CellClick);
            // 
            // cmbJornada
            // 
            this.cmbJornada.FormattingEnabled = true;
            this.cmbJornada.Location = new System.Drawing.Point(24, 110);
            this.cmbJornada.Name = "cmbJornada";
            this.cmbJornada.Size = new System.Drawing.Size(167, 21);
            this.cmbJornada.TabIndex = 1;
            // 
            // cmbLocalidad
            // 
            this.cmbLocalidad.FormattingEnabled = true;
            this.cmbLocalidad.Location = new System.Drawing.Point(24, 42);
            this.cmbLocalidad.Name = "cmbLocalidad";
            this.cmbLocalidad.Size = new System.Drawing.Size(167, 21);
            this.cmbLocalidad.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Localidad";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(20, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Jornada";
            // 
            // BtnExtraer
            // 
            this.BtnExtraer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnExtraer.Location = new System.Drawing.Point(213, 192);
            this.BtnExtraer.Name = "BtnExtraer";
            this.BtnExtraer.Size = new System.Drawing.Size(116, 46);
            this.BtnExtraer.TabIndex = 5;
            this.BtnExtraer.Text = "Extraer\r\nInformación";
            this.BtnExtraer.UseVisualStyleBackColor = true;
            this.BtnExtraer.Click += new System.EventHandler(this.BtnExtraer_Click);
            // 
            // Calendario
            // 
            this.Calendario.Location = new System.Drawing.Point(203, 18);
            this.Calendario.Name = "Calendario";
            this.Calendario.TabIndex = 7;
            // 
            // BtnSalir
            // 
            this.BtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalir.Location = new System.Drawing.Point(335, 192);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(116, 46);
            this.BtnSalir.TabIndex = 8;
            this.BtnSalir.Text = "Salir";
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // LblAbrirTodos
            // 
            this.LblAbrirTodos.AutoSize = true;
            this.LblAbrirTodos.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblAbrirTodos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblAbrirTodos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LblAbrirTodos.Location = new System.Drawing.Point(23, 160);
            this.LblAbrirTodos.Name = "LblAbrirTodos";
            this.LblAbrirTodos.Size = new System.Drawing.Size(168, 20);
            this.LblAbrirTodos.TabIndex = 9;
            this.LblAbrirTodos.TabStop = true;
            this.LblAbrirTodos.Text = "Todos los Registros";
            this.LblAbrirTodos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblAbrirTodos_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LblEstado);
            this.groupBox1.Controls.Add(this.LblCierre);
            this.groupBox1.Controls.Add(this.LblApertura);
            this.groupBox1.Controls.Add(this.LblCajero);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(507, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(210, 331);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos recuperados";
            // 
            // LblEstado
            // 
            this.LblEstado.AutoSize = true;
            this.LblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEstado.Location = new System.Drawing.Point(16, 285);
            this.LblEstado.Name = "LblEstado";
            this.LblEstado.Size = new System.Drawing.Size(0, 16);
            this.LblEstado.TabIndex = 15;
            // 
            // LblCierre
            // 
            this.LblCierre.AutoSize = true;
            this.LblCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCierre.Location = new System.Drawing.Point(16, 205);
            this.LblCierre.Name = "LblCierre";
            this.LblCierre.Size = new System.Drawing.Size(0, 16);
            this.LblCierre.TabIndex = 14;
            // 
            // LblApertura
            // 
            this.LblApertura.AutoSize = true;
            this.LblApertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblApertura.Location = new System.Drawing.Point(16, 128);
            this.LblApertura.Name = "LblApertura";
            this.LblApertura.Size = new System.Drawing.Size(0, 16);
            this.LblApertura.TabIndex = 13;
            // 
            // LblCajero
            // 
            this.LblCajero.AutoSize = true;
            this.LblCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCajero.Location = new System.Drawing.Point(16, 58);
            this.LblCajero.Name = "LblCajero";
            this.LblCajero.Size = new System.Drawing.Size(0, 16);
            this.LblCajero.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Estado del Cierre";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Fecha y Hora de Cierre";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "Fecha y Hora de Apertura";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cajero";
            // 
            // BtnReabrir
            // 
            this.BtnReabrir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnReabrir.Location = new System.Drawing.Point(542, 371);
            this.BtnReabrir.Name = "BtnReabrir";
            this.BtnReabrir.Size = new System.Drawing.Size(150, 48);
            this.BtnReabrir.TabIndex = 16;
            this.BtnReabrir.Text = "Reabrir Última Caja";
            this.BtnReabrir.UseVisualStyleBackColor = true;
            this.BtnReabrir.Click += new System.EventHandler(this.BtnReabrir_Click);
            // 
            // LblId
            // 
            this.LblId.AutoSize = true;
            this.LblId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblId.Location = new System.Drawing.Point(24, 208);
            this.LblId.Name = "LblId";
            this.LblId.Size = new System.Drawing.Size(57, 16);
            this.LblId.TabIndex = 17;
            this.LblId.Text = "1111111";
            this.LblId.Visible = false;
            // 
            // frmReabrirCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(733, 462);
            this.Controls.Add(this.BtnReabrir);
            this.Controls.Add(this.LblId);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LblAbrirTodos);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.Calendario);
            this.Controls.Add(this.BtnExtraer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLocalidad);
            this.Controls.Add(this.cmbJornada);
            this.Controls.Add(this.dgvInformacion);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReabrirCaja";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cajas Registradas";
            this.Load += new System.EventHandler(this.frmReabrirCaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInformacion)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInformacion;
        private MisControles.ComboDatos cmbJornada;
        private MisControles.ComboDatos cmbLocalidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnExtraer;
        private System.Windows.Forms.MonthCalendar Calendario;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.LinkLabel LblAbrirTodos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnReabrir;
        private System.Windows.Forms.Label LblEstado;
        private System.Windows.Forms.Label LblCierre;
        private System.Windows.Forms.Label LblApertura;
        private System.Windows.Forms.Label LblCajero;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LblId;
    }
}