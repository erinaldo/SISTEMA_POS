namespace Palatium.Inicio
{
    partial class frmInicioFacturacionElectronica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInicioFacturacionElectronica));
            this.logo = new System.Windows.Forms.PictureBox();
            this.btnSalidaCajero = new System.Windows.Forms.Button();
            this.btnMovimientoCaja = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnRevisar = new System.Windows.Forms.Button();
            this.btnFacturasSri = new System.Windows.Forms.Button();
            this.btnReenviarFacturas = new System.Windows.Forms.Button();
            this.btnReenviarFacturaIndividual = new System.Windows.Forms.Button();
            this.lblVersionDemo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // logo
            // 
            this.logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo.BackgroundImage")));
            this.logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logo.Location = new System.Drawing.Point(45, 76);
            this.logo.Margin = new System.Windows.Forms.Padding(2);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(457, 501);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 44;
            this.logo.TabStop = false;
            // 
            // btnSalidaCajero
            // 
            this.btnSalidaCajero.BackColor = System.Drawing.Color.Navy;
            this.btnSalidaCajero.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSalidaCajero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalidaCajero.FlatAppearance.BorderSize = 2;
            this.btnSalidaCajero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalidaCajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalidaCajero.ForeColor = System.Drawing.Color.White;
            this.btnSalidaCajero.Image = global::Palatium.Properties.Resources.icono_abrir_caja;
            this.btnSalidaCajero.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSalidaCajero.Location = new System.Drawing.Point(1126, 439);
            this.btnSalidaCajero.Name = "btnSalidaCajero";
            this.btnSalidaCajero.Size = new System.Drawing.Size(192, 138);
            this.btnSalidaCajero.TabIndex = 85;
            this.btnSalidaCajero.Text = "Arqueo de Caja\r\n  ";
            this.btnSalidaCajero.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSalidaCajero.UseVisualStyleBackColor = false;
            this.btnSalidaCajero.Click += new System.EventHandler(this.btnSalidaCajero_Click);
            this.btnSalidaCajero.MouseEnter += new System.EventHandler(this.btnSalidaCajero_MouseEnter);
            this.btnSalidaCajero.MouseLeave += new System.EventHandler(this.btnSalidaCajero_MouseLeave);
            // 
            // btnMovimientoCaja
            // 
            this.btnMovimientoCaja.BackColor = System.Drawing.Color.Navy;
            this.btnMovimientoCaja.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnMovimientoCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMovimientoCaja.FlatAppearance.BorderSize = 2;
            this.btnMovimientoCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMovimientoCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMovimientoCaja.ForeColor = System.Drawing.Color.White;
            this.btnMovimientoCaja.Image = global::Palatium.Properties.Resources.icono_caja_chica;
            this.btnMovimientoCaja.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMovimientoCaja.Location = new System.Drawing.Point(928, 439);
            this.btnMovimientoCaja.Name = "btnMovimientoCaja";
            this.btnMovimientoCaja.Size = new System.Drawing.Size(192, 138);
            this.btnMovimientoCaja.TabIndex = 84;
            this.btnMovimientoCaja.Text = "Movimientos de Caja\r\n ";
            this.btnMovimientoCaja.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMovimientoCaja.UseVisualStyleBackColor = false;
            this.btnMovimientoCaja.Click += new System.EventHandler(this.btnMovimientoCaja_Click);
            this.btnMovimientoCaja.MouseEnter += new System.EventHandler(this.btnMovimientoCaja_MouseEnter);
            this.btnMovimientoCaja.MouseLeave += new System.EventHandler(this.btnMovimientoCaja_MouseLeave);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Navy;
            this.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 2;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = global::Palatium.Properties.Resources.icono_cancelar_orden;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelar.Location = new System.Drawing.Point(730, 439);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(192, 138);
            this.btnCancelar.TabIndex = 83;
            this.btnCancelar.Text = "Cancelar Orden\n  ";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            this.btnCancelar.MouseEnter += new System.EventHandler(this.btnCancelar_MouseEnter);
            this.btnCancelar.MouseLeave += new System.EventHandler(this.btnCancelar_MouseLeave);
            // 
            // btnRevisar
            // 
            this.btnRevisar.BackColor = System.Drawing.Color.Navy;
            this.btnRevisar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRevisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevisar.FlatAppearance.BorderSize = 2;
            this.btnRevisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisar.ForeColor = System.Drawing.Color.White;
            this.btnRevisar.Image = global::Palatium.Properties.Resources.icono_revisar;
            this.btnRevisar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRevisar.Location = new System.Drawing.Point(532, 439);
            this.btnRevisar.Name = "btnRevisar";
            this.btnRevisar.Size = new System.Drawing.Size(192, 138);
            this.btnRevisar.TabIndex = 82;
            this.btnRevisar.Text = "Revisar Órdenes\r\n ";
            this.btnRevisar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRevisar.UseVisualStyleBackColor = false;
            this.btnRevisar.Click += new System.EventHandler(this.btnRevisar_Click);
            this.btnRevisar.MouseEnter += new System.EventHandler(this.btnRevisar_MouseEnter);
            this.btnRevisar.MouseLeave += new System.EventHandler(this.btnRevisar_MouseLeave);
            // 
            // btnFacturasSri
            // 
            this.btnFacturasSri.BackColor = System.Drawing.Color.Navy;
            this.btnFacturasSri.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFacturasSri.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacturasSri.FlatAppearance.BorderSize = 2;
            this.btnFacturasSri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacturasSri.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturasSri.ForeColor = System.Drawing.Color.White;
            this.btnFacturasSri.Image = global::Palatium.Properties.Resources.icono_sri;
            this.btnFacturasSri.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFacturasSri.Location = new System.Drawing.Point(532, 76);
            this.btnFacturasSri.Name = "btnFacturasSri";
            this.btnFacturasSri.Size = new System.Drawing.Size(192, 138);
            this.btnFacturasSri.TabIndex = 86;
            this.btnFacturasSri.Text = "Enviar Facturas al SRI\r\n ";
            this.btnFacturasSri.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFacturasSri.UseVisualStyleBackColor = false;
            this.btnFacturasSri.Click += new System.EventHandler(this.btnFacturasSri_Click);
            this.btnFacturasSri.MouseEnter += new System.EventHandler(this.btnFacturasSri_MouseEnter);
            this.btnFacturasSri.MouseLeave += new System.EventHandler(this.btnFacturasSri_MouseLeave);
            // 
            // btnReenviarFacturas
            // 
            this.btnReenviarFacturas.BackColor = System.Drawing.Color.Navy;
            this.btnReenviarFacturas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReenviarFacturas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReenviarFacturas.FlatAppearance.BorderSize = 2;
            this.btnReenviarFacturas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReenviarFacturas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReenviarFacturas.ForeColor = System.Drawing.Color.White;
            this.btnReenviarFacturas.Image = global::Palatium.Properties.Resources.icono_reenviar_facturas;
            this.btnReenviarFacturas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReenviarFacturas.Location = new System.Drawing.Point(730, 76);
            this.btnReenviarFacturas.Name = "btnReenviarFacturas";
            this.btnReenviarFacturas.Size = new System.Drawing.Size(192, 138);
            this.btnReenviarFacturas.TabIndex = 87;
            this.btnReenviarFacturas.Text = "Reenviar Facturas\r\n ";
            this.btnReenviarFacturas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReenviarFacturas.UseVisualStyleBackColor = false;
            this.btnReenviarFacturas.Click += new System.EventHandler(this.btnReenviarFacturas_Click);
            this.btnReenviarFacturas.MouseEnter += new System.EventHandler(this.btnReenviarFacturas_MouseEnter);
            this.btnReenviarFacturas.MouseLeave += new System.EventHandler(this.btnReenviarFacturas_MouseLeave);
            // 
            // btnReenviarFacturaIndividual
            // 
            this.btnReenviarFacturaIndividual.BackColor = System.Drawing.Color.Navy;
            this.btnReenviarFacturaIndividual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReenviarFacturaIndividual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReenviarFacturaIndividual.FlatAppearance.BorderSize = 2;
            this.btnReenviarFacturaIndividual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReenviarFacturaIndividual.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReenviarFacturaIndividual.ForeColor = System.Drawing.Color.White;
            this.btnReenviarFacturaIndividual.Image = global::Palatium.Properties.Resources.icono_reenviar_factura_individual;
            this.btnReenviarFacturaIndividual.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReenviarFacturaIndividual.Location = new System.Drawing.Point(928, 76);
            this.btnReenviarFacturaIndividual.Name = "btnReenviarFacturaIndividual";
            this.btnReenviarFacturaIndividual.Size = new System.Drawing.Size(192, 138);
            this.btnReenviarFacturaIndividual.TabIndex = 88;
            this.btnReenviarFacturaIndividual.Text = "Reenviar Factura\r\nIndividual";
            this.btnReenviarFacturaIndividual.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReenviarFacturaIndividual.UseVisualStyleBackColor = false;
            this.btnReenviarFacturaIndividual.Click += new System.EventHandler(this.btnReenviarFacturaIndividual_Click);
            // 
            // lblVersionDemo
            // 
            this.lblVersionDemo.AutoSize = true;
            this.lblVersionDemo.Font = new System.Drawing.Font("Maiandra GD", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersionDemo.ForeColor = System.Drawing.Color.Red;
            this.lblVersionDemo.Location = new System.Drawing.Point(39, 30);
            this.lblVersionDemo.Name = "lblVersionDemo";
            this.lblVersionDemo.Size = new System.Drawing.Size(195, 32);
            this.lblVersionDemo.TabIndex = 89;
            this.lblVersionDemo.Text = "Versión Demo";
            // 
            // frmInicioFacturacionElectronica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1360, 694);
            this.Controls.Add(this.lblVersionDemo);
            this.Controls.Add(this.btnReenviarFacturaIndividual);
            this.Controls.Add(this.btnReenviarFacturas);
            this.Controls.Add(this.btnFacturasSri);
            this.Controls.Add(this.btnSalidaCajero);
            this.Controls.Add(this.btnMovimientoCaja);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRevisar);
            this.Controls.Add(this.logo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInicioFacturacionElectronica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sincronización SRI";
            this.Load += new System.EventHandler(this.frmInicioFacturacionElectronica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Button btnSalidaCajero;
        private System.Windows.Forms.Button btnMovimientoCaja;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnRevisar;
        private System.Windows.Forms.Button btnFacturasSri;
        private System.Windows.Forms.Button btnReenviarFacturas;
        private System.Windows.Forms.Button btnReenviarFacturaIndividual;
        private System.Windows.Forms.Label lblVersionDemo;
    }
}