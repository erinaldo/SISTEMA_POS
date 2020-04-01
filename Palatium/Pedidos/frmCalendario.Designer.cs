namespace Palatium.Pedidos
{
    partial class frmCalendario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCalendario));
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnBajarAnio = new System.Windows.Forms.Button();
            this.btnBajarMes = new System.Windows.Forms.Button();
            this.btnBajarDia = new System.Windows.Forms.Button();
            this.btnSubirAnio = new System.Windows.Forms.Button();
            this.btnSubirMes = new System.Windows.Forms.Button();
            this.btnSubirDia = new System.Windows.Forms.Button();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.cmbJornada = new MisControles.ComboDatos();
            this.lblJornada = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtFecha
            // 
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.Location = new System.Drawing.Point(12, 13);
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.ReadOnly = true;
            this.txtFecha.Size = new System.Drawing.Size(248, 38);
            this.txtFecha.TabIndex = 1;
            this.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelar.Image = global::Palatium.Properties.Resources.cancelar2_png;
            this.btnCancelar.Location = new System.Drawing.Point(266, 121);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(116, 99);
            this.btnCancelar.TabIndex = 40;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnBajarAnio
            // 
            this.btnBajarAnio.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBajarAnio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajarAnio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBajarAnio.Image = ((System.Drawing.Image)(resources.GetObject("btnBajarAnio.Image")));
            this.btnBajarAnio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBajarAnio.Location = new System.Drawing.Point(180, 140);
            this.btnBajarAnio.Margin = new System.Windows.Forms.Padding(2);
            this.btnBajarAnio.Name = "btnBajarAnio";
            this.btnBajarAnio.Size = new System.Drawing.Size(80, 80);
            this.btnBajarAnio.TabIndex = 39;
            this.btnBajarAnio.Text = "Año";
            this.btnBajarAnio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBajarAnio.UseVisualStyleBackColor = false;
            this.btnBajarAnio.Click += new System.EventHandler(this.btnBajarAnio_Click);
            // 
            // btnBajarMes
            // 
            this.btnBajarMes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBajarMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajarMes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBajarMes.Image = ((System.Drawing.Image)(resources.GetObject("btnBajarMes.Image")));
            this.btnBajarMes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBajarMes.Location = new System.Drawing.Point(96, 140);
            this.btnBajarMes.Margin = new System.Windows.Forms.Padding(2);
            this.btnBajarMes.Name = "btnBajarMes";
            this.btnBajarMes.Size = new System.Drawing.Size(80, 80);
            this.btnBajarMes.TabIndex = 38;
            this.btnBajarMes.Text = "Mes";
            this.btnBajarMes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBajarMes.UseVisualStyleBackColor = false;
            this.btnBajarMes.Click += new System.EventHandler(this.btnBajarMes_Click);
            // 
            // btnBajarDia
            // 
            this.btnBajarDia.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBajarDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBajarDia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBajarDia.Image = ((System.Drawing.Image)(resources.GetObject("btnBajarDia.Image")));
            this.btnBajarDia.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnBajarDia.Location = new System.Drawing.Point(12, 140);
            this.btnBajarDia.Margin = new System.Windows.Forms.Padding(2);
            this.btnBajarDia.Name = "btnBajarDia";
            this.btnBajarDia.Size = new System.Drawing.Size(80, 80);
            this.btnBajarDia.TabIndex = 37;
            this.btnBajarDia.Text = "Día";
            this.btnBajarDia.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBajarDia.UseVisualStyleBackColor = false;
            this.btnBajarDia.Click += new System.EventHandler(this.btnBajarDia_Click);
            // 
            // btnSubirAnio
            // 
            this.btnSubirAnio.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSubirAnio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirAnio.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSubirAnio.Image = global::Palatium.Properties.Resources.arriba;
            this.btnSubirAnio.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSubirAnio.Location = new System.Drawing.Point(180, 56);
            this.btnSubirAnio.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubirAnio.Name = "btnSubirAnio";
            this.btnSubirAnio.Size = new System.Drawing.Size(80, 80);
            this.btnSubirAnio.TabIndex = 36;
            this.btnSubirAnio.Text = "Año";
            this.btnSubirAnio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSubirAnio.UseVisualStyleBackColor = false;
            this.btnSubirAnio.Click += new System.EventHandler(this.btnSubirAnio_Click);
            // 
            // btnSubirMes
            // 
            this.btnSubirMes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSubirMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirMes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSubirMes.Image = global::Palatium.Properties.Resources.arriba;
            this.btnSubirMes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSubirMes.Location = new System.Drawing.Point(96, 56);
            this.btnSubirMes.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubirMes.Name = "btnSubirMes";
            this.btnSubirMes.Size = new System.Drawing.Size(80, 80);
            this.btnSubirMes.TabIndex = 35;
            this.btnSubirMes.Text = "Mes";
            this.btnSubirMes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSubirMes.UseVisualStyleBackColor = false;
            this.btnSubirMes.Click += new System.EventHandler(this.btnSubirMes_Click);
            // 
            // btnSubirDia
            // 
            this.btnSubirDia.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSubirDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubirDia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSubirDia.Image = global::Palatium.Properties.Resources.arriba;
            this.btnSubirDia.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSubirDia.Location = new System.Drawing.Point(12, 56);
            this.btnSubirDia.Margin = new System.Windows.Forms.Padding(2);
            this.btnSubirDia.Name = "btnSubirDia";
            this.btnSubirDia.Size = new System.Drawing.Size(80, 80);
            this.btnSubirDia.TabIndex = 34;
            this.btnSubirDia.Text = "Día";
            this.btnSubirDia.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSubirDia.UseVisualStyleBackColor = false;
            this.btnSubirDia.Click += new System.EventHandler(this.btnSubirDia_Click);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSeleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeleccionar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSeleccionar.Image = global::Palatium.Properties.Resources.ok2_png;
            this.btnSeleccionar.Location = new System.Drawing.Point(266, 13);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(115, 99);
            this.btnSeleccionar.TabIndex = 0;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSeleccionar.UseVisualStyleBackColor = false;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // cmbJornada
            // 
            this.cmbJornada.FormattingEnabled = true;
            this.cmbJornada.Location = new System.Drawing.Point(154, 245);
            this.cmbJornada.Name = "cmbJornada";
            this.cmbJornada.Size = new System.Drawing.Size(185, 21);
            this.cmbJornada.TabIndex = 43;
            // 
            // lblJornada
            // 
            this.lblJornada.AutoSize = true;
            this.lblJornada.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJornada.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblJornada.Location = new System.Drawing.Point(48, 239);
            this.lblJornada.Name = "lblJornada";
            this.lblJornada.Size = new System.Drawing.Size(100, 29);
            this.lblJornada.TabIndex = 42;
            this.lblJornada.Text = "Jornada:";
            // 
            // frmCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(395, 277);
            this.Controls.Add(this.cmbJornada);
            this.Controls.Add(this.lblJornada);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnBajarAnio);
            this.Controls.Add(this.btnBajarMes);
            this.Controls.Add(this.btnBajarDia);
            this.Controls.Add(this.btnSubirAnio);
            this.Controls.Add(this.btnSubirMes);
            this.Controls.Add(this.btnSubirDia);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.btnSeleccionar);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCalendario";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione la fecha del informe";
            this.Load += new System.EventHandler(this.frmCalendario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalendario_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Button btnSubirDia;
        private System.Windows.Forms.Button btnSubirMes;
        private System.Windows.Forms.Button btnSubirAnio;
        private System.Windows.Forms.Button btnBajarAnio;
        private System.Windows.Forms.Button btnBajarMes;
        private System.Windows.Forms.Button btnBajarDia;
        private System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.TextBox txtFecha;
        private MisControles.ComboDatos cmbJornada;
        private System.Windows.Forms.Label lblJornada;
    }
}