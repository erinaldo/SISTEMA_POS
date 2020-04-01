namespace Palatium.Empresa
{
    partial class frmSeleccionEmpresaEmpleado
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSiguienteEmpleado = new System.Windows.Forms.Button();
            this.btnAnteriorEmpleado = new System.Windows.Forms.Button();
            this.btnSiguienteEmpresa = new System.Windows.Forms.Button();
            this.btnAnteriorEmpresa = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNombreEmpresa = new System.Windows.Forms.Label();
            this.pnlEmpleados = new System.Windows.Forms.Panel();
            this.pnlEmpresa = new System.Windows.Forms.Panel();
            this.txtFiltrarEmpresas = new System.Windows.Forms.TextBox();
            this.txtFiltrarEmpleados = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(7, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 47);
            this.panel2.TabIndex = 116;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Maiandra GD", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Lime;
            this.label3.Location = new System.Drawing.Point(103, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 39);
            this.label3.TabIndex = 22;
            this.label3.Text = "EMPRESAS";
            // 
            // btnSiguienteEmpleado
            // 
            this.btnSiguienteEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSiguienteEmpleado.Image = global::Palatium.Properties.Resources.derecha;
            this.btnSiguienteEmpleado.Location = new System.Drawing.Point(761, 61);
            this.btnSiguienteEmpleado.Name = "btnSiguienteEmpleado";
            this.btnSiguienteEmpleado.Size = new System.Drawing.Size(64, 55);
            this.btnSiguienteEmpleado.TabIndex = 115;
            this.btnSiguienteEmpleado.UseVisualStyleBackColor = false;
            this.btnSiguienteEmpleado.Visible = false;
            this.btnSiguienteEmpleado.Click += new System.EventHandler(this.btnSiguienteEmpleado_Click);
            // 
            // btnAnteriorEmpleado
            // 
            this.btnAnteriorEmpleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAnteriorEmpleado.Enabled = false;
            this.btnAnteriorEmpleado.Image = global::Palatium.Properties.Resources.izquierda;
            this.btnAnteriorEmpleado.Location = new System.Drawing.Point(699, 61);
            this.btnAnteriorEmpleado.Name = "btnAnteriorEmpleado";
            this.btnAnteriorEmpleado.Size = new System.Drawing.Size(64, 55);
            this.btnAnteriorEmpleado.TabIndex = 114;
            this.btnAnteriorEmpleado.UseVisualStyleBackColor = false;
            this.btnAnteriorEmpleado.Visible = false;
            this.btnAnteriorEmpleado.Click += new System.EventHandler(this.btnAnteriorEmpleado_Click);
            // 
            // btnSiguienteEmpresa
            // 
            this.btnSiguienteEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSiguienteEmpresa.Image = global::Palatium.Properties.Resources.derecha;
            this.btnSiguienteEmpresa.Location = new System.Drawing.Point(339, 61);
            this.btnSiguienteEmpresa.Name = "btnSiguienteEmpresa";
            this.btnSiguienteEmpresa.Size = new System.Drawing.Size(64, 55);
            this.btnSiguienteEmpresa.TabIndex = 113;
            this.btnSiguienteEmpresa.UseVisualStyleBackColor = false;
            this.btnSiguienteEmpresa.Visible = false;
            this.btnSiguienteEmpresa.Click += new System.EventHandler(this.btnSiguienteEmpresa_Click);
            // 
            // btnAnteriorEmpresa
            // 
            this.btnAnteriorEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAnteriorEmpresa.Enabled = false;
            this.btnAnteriorEmpresa.Image = global::Palatium.Properties.Resources.izquierda;
            this.btnAnteriorEmpresa.Location = new System.Drawing.Point(277, 61);
            this.btnAnteriorEmpresa.Name = "btnAnteriorEmpresa";
            this.btnAnteriorEmpresa.Size = new System.Drawing.Size(64, 55);
            this.btnAnteriorEmpresa.TabIndex = 112;
            this.btnAnteriorEmpresa.UseVisualStyleBackColor = false;
            this.btnAnteriorEmpresa.Visible = false;
            this.btnAnteriorEmpresa.Click += new System.EventHandler(this.btnAnteriorEmpresa_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.lblNombreEmpresa);
            this.panel1.Location = new System.Drawing.Point(414, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(738, 47);
            this.panel1.TabIndex = 117;
            // 
            // lblNombreEmpresa
            // 
            this.lblNombreEmpresa.Font = new System.Drawing.Font("Maiandra GD", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreEmpresa.ForeColor = System.Drawing.Color.Lime;
            this.lblNombreEmpresa.Location = new System.Drawing.Point(3, 9);
            this.lblNombreEmpresa.Name = "lblNombreEmpresa";
            this.lblNombreEmpresa.Size = new System.Drawing.Size(600, 33);
            this.lblNombreEmpresa.TabIndex = 22;
            this.lblNombreEmpresa.Text = "PERSONAL INGRESADO";
            // 
            // pnlEmpleados
            // 
            this.pnlEmpleados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlEmpleados.Location = new System.Drawing.Point(414, 122);
            this.pnlEmpleados.Name = "pnlEmpleados";
            this.pnlEmpleados.Size = new System.Drawing.Size(738, 530);
            this.pnlEmpleados.TabIndex = 119;
            // 
            // pnlEmpresa
            // 
            this.pnlEmpresa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlEmpresa.Location = new System.Drawing.Point(7, 122);
            this.pnlEmpresa.Name = "pnlEmpresa";
            this.pnlEmpresa.Size = new System.Drawing.Size(396, 530);
            this.pnlEmpresa.TabIndex = 118;
            // 
            // txtFiltrarEmpresas
            // 
            this.txtFiltrarEmpresas.Font = new System.Drawing.Font("Maiandra GD", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltrarEmpresas.Location = new System.Drawing.Point(7, 80);
            this.txtFiltrarEmpresas.Name = "txtFiltrarEmpresas";
            this.txtFiltrarEmpresas.Size = new System.Drawing.Size(246, 36);
            this.txtFiltrarEmpresas.TabIndex = 120;
            this.toolTip1.SetToolTip(this.txtFiltrarEmpresas, "Escriba aquí para buscar por nombre de empresas.\r\nAl finalizar oprima la tecla EN" +
        "TER.");
            this.txtFiltrarEmpresas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltrarEmpresas_KeyPress);
            // 
            // txtFiltrarEmpleados
            // 
            this.txtFiltrarEmpleados.Font = new System.Drawing.Font("Maiandra GD", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltrarEmpleados.Location = new System.Drawing.Point(414, 80);
            this.txtFiltrarEmpleados.Name = "txtFiltrarEmpleados";
            this.txtFiltrarEmpleados.Size = new System.Drawing.Size(246, 36);
            this.txtFiltrarEmpleados.TabIndex = 121;
            this.toolTip1.SetToolTip(this.txtFiltrarEmpleados, "Escriba aquí para buscar por nombre de empleados.\r\nAl finalizar oprima la tecla E" +
        "NTER.");
            this.txtFiltrarEmpleados.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFiltrarEmpleados_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 19);
            this.label1.TabIndex = 122;
            this.label1.Text = "BUSCAR EMPRESAS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(414, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 19);
            this.label2.TabIndex = 123;
            this.label2.Text = "BUSCAR EMPLEADOS";
            // 
            // frmSeleccionEmpresaEmpleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(1164, 661);
            this.Controls.Add(this.txtFiltrarEmpleados);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAnteriorEmpleado);
            this.Controls.Add(this.btnAnteriorEmpresa);
            this.Controls.Add(this.btnSiguienteEmpleado);
            this.Controls.Add(this.btnSiguienteEmpresa);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txtFiltrarEmpresas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlEmpleados);
            this.Controls.Add(this.pnlEmpresa);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSeleccionEmpresaEmpleado";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccione la Empresa y el empleado";
            this.Load += new System.EventHandler(this.frmSeleccionEmpresaEmpleado_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSeleccionEmpresaEmpleado_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSiguienteEmpleado;
        private System.Windows.Forms.Button btnAnteriorEmpleado;
        private System.Windows.Forms.Button btnSiguienteEmpresa;
        private System.Windows.Forms.Button btnAnteriorEmpresa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNombreEmpresa;
        private System.Windows.Forms.Panel pnlEmpleados;
        private System.Windows.Forms.Panel pnlEmpresa;
        private System.Windows.Forms.TextBox txtFiltrarEmpresas;
        private System.Windows.Forms.TextBox txtFiltrarEmpleados;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}