namespace Palatium.Formularios
{
    partial class FModificarCodigos
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
            this.txtCodigoActual = new System.Windows.Forms.TextBox();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.btnBuscarCodigo = new System.Windows.Forms.Button();
            this.lblBuscarCodigo = new System.Windows.Forms.Label();
            this.txtNuevoCodigo = new System.Windows.Forms.TextBox();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.lblNuevoCodigo = new System.Windows.Forms.Label();
            this.lblCodigoBarras = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCerrarCodigo = new System.Windows.Forms.Button();
            this.btnLimpiarCodigo = new System.Windows.Forms.Button();
            this.btnGuardarCodigo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigoActual
            // 
            this.txtCodigoActual.Location = new System.Drawing.Point(27, 59);
            this.txtCodigoActual.Name = "txtCodigoActual";
            this.txtCodigoActual.ReadOnly = true;
            this.txtCodigoActual.Size = new System.Drawing.Size(100, 20);
            this.txtCodigoActual.TabIndex = 0;
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.Location = new System.Drawing.Point(204, 59);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.ReadOnly = true;
            this.txtNombreProducto.Size = new System.Drawing.Size(318, 20);
            this.txtNombreProducto.TabIndex = 1;
            // 
            // btnBuscarCodigo
            // 
            this.btnBuscarCodigo.Location = new System.Drawing.Point(135, 59);
            this.btnBuscarCodigo.Name = "btnBuscarCodigo";
            this.btnBuscarCodigo.Size = new System.Drawing.Size(63, 23);
            this.btnBuscarCodigo.TabIndex = 2;
            this.btnBuscarCodigo.Text = "?";
            this.btnBuscarCodigo.UseVisualStyleBackColor = true;
            this.btnBuscarCodigo.Click += new System.EventHandler(this.btnBuscarCodigo_Click);
            // 
            // lblBuscarCodigo
            // 
            this.lblBuscarCodigo.AutoSize = true;
            this.lblBuscarCodigo.Location = new System.Drawing.Point(27, 28);
            this.lblBuscarCodigo.Name = "lblBuscarCodigo";
            this.lblBuscarCodigo.Size = new System.Drawing.Size(142, 13);
            this.lblBuscarCodigo.TabIndex = 3;
            this.lblBuscarCodigo.Text = "Ingrese el código a modificar";
            // 
            // txtNuevoCodigo
            // 
            this.txtNuevoCodigo.Location = new System.Drawing.Point(27, 150);
            this.txtNuevoCodigo.Name = "txtNuevoCodigo";
            this.txtNuevoCodigo.Size = new System.Drawing.Size(142, 20);
            this.txtNuevoCodigo.TabIndex = 4;
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(204, 150);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(227, 20);
            this.txtCodigoBarras.TabIndex = 5;
            // 
            // lblNuevoCodigo
            // 
            this.lblNuevoCodigo.AutoSize = true;
            this.lblNuevoCodigo.Location = new System.Drawing.Point(27, 122);
            this.lblNuevoCodigo.Name = "lblNuevoCodigo";
            this.lblNuevoCodigo.Size = new System.Drawing.Size(121, 13);
            this.lblNuevoCodigo.TabIndex = 6;
            this.lblNuevoCodigo.Text = "Ingrese el nuevo código";
            // 
            // lblCodigoBarras
            // 
            this.lblCodigoBarras.AutoSize = true;
            this.lblCodigoBarras.Location = new System.Drawing.Point(201, 122);
            this.lblCodigoBarras.Name = "lblCodigoBarras";
            this.lblCodigoBarras.Size = new System.Drawing.Size(87, 13);
            this.lblCodigoBarras.TabIndex = 7;
            this.lblCodigoBarras.Text = "Código de barras";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnCerrarCodigo);
            this.panel1.Controls.Add(this.btnLimpiarCodigo);
            this.panel1.Controls.Add(this.btnGuardarCodigo);
            this.panel1.Controls.Add(this.txtNombreProducto);
            this.panel1.Controls.Add(this.lblCodigoBarras);
            this.panel1.Controls.Add(this.txtCodigoActual);
            this.panel1.Controls.Add(this.lblNuevoCodigo);
            this.panel1.Controls.Add(this.btnBuscarCodigo);
            this.panel1.Controls.Add(this.txtCodigoBarras);
            this.panel1.Controls.Add(this.lblBuscarCodigo);
            this.panel1.Controls.Add(this.txtNuevoCodigo);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 268);
            this.panel1.TabIndex = 8;
            // 
            // btnCerrarCodigo
            // 
            this.btnCerrarCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCerrarCodigo.ForeColor = System.Drawing.Color.Transparent;
            this.btnCerrarCodigo.Location = new System.Drawing.Point(434, 204);
            this.btnCerrarCodigo.Name = "btnCerrarCodigo";
            this.btnCerrarCodigo.Size = new System.Drawing.Size(88, 39);
            this.btnCerrarCodigo.TabIndex = 10;
            this.btnCerrarCodigo.Text = "Cerrar";
            this.btnCerrarCodigo.UseVisualStyleBackColor = false;
            this.btnCerrarCodigo.Click += new System.EventHandler(this.btnCerrarCodigo_Click);
            // 
            // btnLimpiarCodigo
            // 
            this.btnLimpiarCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnLimpiarCodigo.ForeColor = System.Drawing.Color.Transparent;
            this.btnLimpiarCodigo.Location = new System.Drawing.Point(340, 204);
            this.btnLimpiarCodigo.Name = "btnLimpiarCodigo";
            this.btnLimpiarCodigo.Size = new System.Drawing.Size(88, 39);
            this.btnLimpiarCodigo.TabIndex = 9;
            this.btnLimpiarCodigo.Text = "Limpiar";
            this.btnLimpiarCodigo.UseVisualStyleBackColor = false;
            this.btnLimpiarCodigo.Click += new System.EventHandler(this.btnLimpiarCodigo_Click);
            // 
            // btnGuardarCodigo
            // 
            this.btnGuardarCodigo.BackColor = System.Drawing.Color.Blue;
            this.btnGuardarCodigo.ForeColor = System.Drawing.Color.Transparent;
            this.btnGuardarCodigo.Location = new System.Drawing.Point(246, 204);
            this.btnGuardarCodigo.Name = "btnGuardarCodigo";
            this.btnGuardarCodigo.Size = new System.Drawing.Size(88, 39);
            this.btnGuardarCodigo.TabIndex = 8;
            this.btnGuardarCodigo.Text = "Guardar";
            this.btnGuardarCodigo.UseVisualStyleBackColor = false;
            this.btnGuardarCodigo.Click += new System.EventHandler(this.btnGuardarCodigo_Click);
            // 
            // FModificarCodigos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(547, 268);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FModificarCodigos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificaciones de Códigos de Productos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCodigoActual;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.Button btnBuscarCodigo;
        private System.Windows.Forms.Label lblBuscarCodigo;
        private System.Windows.Forms.TextBox txtNuevoCodigo;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.Label lblNuevoCodigo;
        private System.Windows.Forms.Label lblCodigoBarras;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCerrarCodigo;
        private System.Windows.Forms.Button btnLimpiarCodigo;
        private System.Windows.Forms.Button btnGuardarCodigo;
    }
}