﻿namespace Palatium.Cajero
{
    partial class frmRecuperarDinero
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvBilletes = new System.Windows.Forms.DataGridView();
            this.idPosMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valorMoneda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBillete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilletes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBilletes
            // 
            this.dgvBilletes.AllowUserToAddRows = false;
            this.dgvBilletes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvBilletes.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvBilletes.ColumnHeadersHeight = 30;
            this.dgvBilletes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idPosMoneda,
            this.valorMoneda,
            this.colBillete,
            this.colCantidad,
            this.colTotal});
            this.dgvBilletes.Location = new System.Drawing.Point(0, 0);
            this.dgvBilletes.MultiSelect = false;
            this.dgvBilletes.Name = "dgvBilletes";
            this.dgvBilletes.RowHeadersVisible = false;
            this.dgvBilletes.RowTemplate.Height = 70;
            this.dgvBilletes.Size = new System.Drawing.Size(433, 360);
            this.dgvBilletes.TabIndex = 120;
            this.dgvBilletes.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBilletes_CellEndEdit);
            this.dgvBilletes.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvBilletes_EditingControlShowing);
            // 
            // idPosMoneda
            // 
            this.idPosMoneda.HeaderText = "ID MONEDA";
            this.idPosMoneda.Name = "idPosMoneda";
            this.idPosMoneda.Visible = false;
            // 
            // valorMoneda
            // 
            this.valorMoneda.HeaderText = "VALOR MONEDA";
            this.valorMoneda.Name = "valorMoneda";
            this.valorMoneda.Visible = false;
            // 
            // colBillete
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colBillete.DefaultCellStyle = dataGridViewCellStyle1;
            this.colBillete.HeaderText = "Billete";
            this.colBillete.Name = "colBillete";
            this.colBillete.ReadOnly = true;
            this.colBillete.Width = 200;
            // 
            // colCantidad
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCantidad.DefaultCellStyle = dataGridViewCellStyle2;
            this.colCantidad.HeaderText = "Cantidad";
            this.colCantidad.Name = "colCantidad";
            // 
            // colTotal
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::Palatium.Properties.Resources.limpiar_ico;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(122, 371);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(104, 47);
            this.btnLimpiar.TabIndex = 123;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label10.Location = new System.Drawing.Point(249, 381);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 24);
            this.label10.TabIndex = 122;
            this.label10.Text = "Total:";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(310, 378);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(116, 29);
            this.txtTotal.TabIndex = 121;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Image = global::Palatium.Properties.Resources.guardar_ico;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.Location = new System.Drawing.Point(12, 371);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(104, 47);
            this.btnGuardar.TabIndex = 124;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // frmRecuperarDinero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(433, 430);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dgvBilletes);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTotal);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRecuperarDinero";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contar Dinero";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRecuperarDinero_FormClosing);
            this.Load += new System.EventHandler(this.frmRecuperarDinero_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRecuperarDinero_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilletes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBilletes;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn idPosMoneda;
        private System.Windows.Forms.DataGridViewTextBoxColumn valorMoneda;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBillete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.Button btnGuardar;
    }
}