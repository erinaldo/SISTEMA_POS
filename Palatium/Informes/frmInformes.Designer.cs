namespace Palatium.Informes
{
    partial class frmInformes
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.informesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeDeResponsabilidadDelCajeroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informesVentasMenúExpressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResponsabilidad = new System.Windows.Forms.Button();
            this.btnInformeMenuExpress = new System.Windows.Forms.Button();
            this.btnInformeProductos = new System.Windows.Forms.Button();
            this.btnImpresoraRollo = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnCategorias = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(644, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // informesToolStripMenuItem
            // 
            this.informesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.informeDeResponsabilidadDelCajeroToolStripMenuItem,
            this.informesVentasMenúExpressToolStripMenuItem});
            this.informesToolStripMenuItem.Name = "informesToolStripMenuItem";
            this.informesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.informesToolStripMenuItem.Text = "Informes";
            // 
            // informeDeResponsabilidadDelCajeroToolStripMenuItem
            // 
            this.informeDeResponsabilidadDelCajeroToolStripMenuItem.Name = "informeDeResponsabilidadDelCajeroToolStripMenuItem";
            this.informeDeResponsabilidadDelCajeroToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.informeDeResponsabilidadDelCajeroToolStripMenuItem.Text = "Informe de Responsabilidad del Cajero";
            // 
            // informesVentasMenúExpressToolStripMenuItem
            // 
            this.informesVentasMenúExpressToolStripMenuItem.Name = "informesVentasMenúExpressToolStripMenuItem";
            this.informesVentasMenúExpressToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.informesVentasMenúExpressToolStripMenuItem.Text = "Informes Ventas Menú Express";
            // 
            // btnResponsabilidad
            // 
            this.btnResponsabilidad.Location = new System.Drawing.Point(131, 66);
            this.btnResponsabilidad.Name = "btnResponsabilidad";
            this.btnResponsabilidad.Size = new System.Drawing.Size(131, 48);
            this.btnResponsabilidad.TabIndex = 1;
            this.btnResponsabilidad.Text = "Imprimir Informe de Responsabilidad";
            this.btnResponsabilidad.UseVisualStyleBackColor = true;
            this.btnResponsabilidad.Click += new System.EventHandler(this.btnResponsabilidad_Click);
            // 
            // btnInformeMenuExpress
            // 
            this.btnInformeMenuExpress.Location = new System.Drawing.Point(131, 126);
            this.btnInformeMenuExpress.Name = "btnInformeMenuExpress";
            this.btnInformeMenuExpress.Size = new System.Drawing.Size(131, 48);
            this.btnInformeMenuExpress.TabIndex = 2;
            this.btnInformeMenuExpress.Text = "Imprimir Informe de Ventas de Menú Express";
            this.btnInformeMenuExpress.UseVisualStyleBackColor = true;
            this.btnInformeMenuExpress.Click += new System.EventHandler(this.btnInformeMenuExpress_Click);
            // 
            // btnInformeProductos
            // 
            this.btnInformeProductos.Location = new System.Drawing.Point(131, 183);
            this.btnInformeProductos.Name = "btnInformeProductos";
            this.btnInformeProductos.Size = new System.Drawing.Size(131, 48);
            this.btnInformeProductos.TabIndex = 3;
            this.btnInformeProductos.Text = "Imprimir Informe Precios Productos";
            this.btnInformeProductos.UseVisualStyleBackColor = true;
            this.btnInformeProductos.Click += new System.EventHandler(this.btnInformeProductos_Click);
            // 
            // btnImpresoraRollo
            // 
            this.btnImpresoraRollo.Location = new System.Drawing.Point(131, 252);
            this.btnImpresoraRollo.Name = "btnImpresoraRollo";
            this.btnImpresoraRollo.Size = new System.Drawing.Size(131, 48);
            this.btnImpresoraRollo.TabIndex = 4;
            this.btnImpresoraRollo.Text = "Imprimir Informe (Impresora Rollo)";
            this.btnImpresoraRollo.UseVisualStyleBackColor = true;
            this.btnImpresoraRollo.Click += new System.EventHandler(this.btnImpresoraRollo_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Location = new System.Drawing.Point(317, 66);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(131, 48);
            this.btnProductos.TabIndex = 5;
            this.btnProductos.Text = "Informe de productos";
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnCategorias
            // 
            this.btnCategorias.Location = new System.Drawing.Point(317, 141);
            this.btnCategorias.Name = "btnCategorias";
            this.btnCategorias.Size = new System.Drawing.Size(131, 48);
            this.btnCategorias.TabIndex = 6;
            this.btnCategorias.Text = "Informe de Categorías";
            this.btnCategorias.UseVisualStyleBackColor = true;
            this.btnCategorias.Click += new System.EventHandler(this.btnCategorias_Click);
            // 
            // frmInformes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 365);
            this.Controls.Add(this.btnCategorias);
            this.Controls.Add(this.btnProductos);
            this.Controls.Add(this.btnImpresoraRollo);
            this.Controls.Add(this.btnInformeProductos);
            this.Controls.Add(this.btnInformeMenuExpress);
            this.Controls.Add(this.btnResponsabilidad);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmInformes";
            this.Text = "Informes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem informesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeDeResponsabilidadDelCajeroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informesVentasMenúExpressToolStripMenuItem;
        private System.Windows.Forms.Button btnResponsabilidad;
        private System.Windows.Forms.Button btnInformeMenuExpress;
        private System.Windows.Forms.Button btnInformeProductos;
        private System.Windows.Forms.Button btnImpresoraRollo;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnCategorias;
    }
}