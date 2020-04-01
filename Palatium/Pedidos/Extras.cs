using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class Extras : Form
    {
        Button[,] boton = new Button[13, 3];
        Button[,] boton1 = new Button[13, 4];
        Button botonSel;
        Button botonSel6;
        Clases.ClasePreciosItems precios;
        Clases.ClaseModificadores modificadores;
        Clases.ClaseExtras extras = new Clases.ClaseExtras();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        int iSecuencia;

        string sPagaIva_P;
        string sNombreProducto_P;

        public Extras(int iSecuencia)
        {
            this.iSecuencia = iSecuencia;
            InitializeComponent();
            mostrarBotones();
        }

        public void mostrarBotones()
        {
            int contador = 0;
            panel2.Controls.Clear();

            if (extras.llenarDatos() == true)
            {
                for (int i = 0; i < 13; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].BackColor = Color.Wheat;
                        boton[i, j].Width = 110;
                        boton[i, j].Height = 70;
                        boton[i, j].Top = i * 80;
                        boton[i, j].Left = j * 110;
                        if (contador == extras.iCuenta)
                        {
                            break;
                        }

                        boton[i, j].Click += boton_clicExtras;
                        boton[i, j].Tag = contador;
                        boton[i, j].Text = extras.extras[contador].sDescripcion;
                        boton[i, j].Name = boton[i, j].Text;
                        boton[i, j].Font = new Font("Microsoft Sans Serif", 15, FontStyle.Regular);

                        this.Controls.Add(boton[i, j]);
                        panel2.Controls.Add(boton[i, j]);
                        contador++;
                    }

                }
            }
        }

        public void crearBotones(Clases.ClaseModificadores modificadores)
        {

            int h = 0;

            if (modificadores.llenarDatos() == true)
            {
                for (int i = 0; i < 13; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {
                        boton1[i, j] = new Button();
                        boton1[i, j].BackColor = Color.Chocolate;
                        boton1[i, j].Click += boton_productosExtras;
                        boton1[i, j].Width = 120;
                        boton1[i, j].Height = 70;
                        boton1[i, j].Top = i * 70;
                        boton1[i, j].Left = j * 120;
                        if (h == modificadores.cuenta)
                        {
                            break;
                        }

                        boton1[i, j].Tag = h;
                        boton1[i, j].BackColor = Color.GhostWhite;
                        boton1[i, j].Text = modificadores.modificadores[h].sDescripcion;
                        boton1[i, j].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                        boton1[i, j].AccessibleName = modificadores.modificadores[h].sIdModificador;
                        boton1[i, j].AccessibleDescription = modificadores.modificadores[h].sPagaIva;

                        this.Controls.Add(boton1[i, j]);

                        panel1.Controls.Add(boton1[i, j]);
                        h++;
                    }

                }
            }
            else
            {
                ok.LblMensaje.Text = "No hay ningún extra registrado.";
                ok.ShowDialog();
            }            
        }

        private void boton_clicExtras(object sender, EventArgs e)
        {

            botonSel = sender as Button;
            //botonSel.BackColor = Color.Blue;
            lblLetra.Text = botonSel.Text;
            panel1.Controls.Clear();
            modificadores = new Clases.ClaseModificadores(botonSel.Text);
            crearBotones(modificadores);            
        }

         private void boton_productosExtras(object sender, EventArgs e)
         {
             Orden ord = Owner as Orden;

            int subindice;
            botonSel6 = sender as Button;
            //botonSel6.BackColor = Color.Yellow;

            precios = new Clases.ClasePreciosItems(Convert.ToInt32(botonSel6.AccessibleName));

            int existe = 0;
            float cantidad = 0;
            float valoru = 0;

            try
            {
                if (precios.llenarDatos() == true)
                {
                    subindice = int.Parse(botonSel6.Tag.ToString());

                    for (int i = 0; i < ord.dgvPedido.Rows.Count; i++)
                    {
                        if (ord.dgvPedido.Rows[i].Cells["producto"].Value.Equals(botonSel6.Text.ToString().Trim()))
                        {
                            cantidad = float.Parse(ord.dgvPedido.Rows[i].Cells["cantidad"].Value.ToString().Trim());
                            cantidad = cantidad + 1;

                            ord.dgvPedido.Rows[i].Cells["cantidad"].Value = cantidad;
                            valoru = float.Parse(ord.dgvPedido.Rows[i].Cells["valuni"].Value.ToString().Trim());
                            ord.dgvPedido.Rows[i].Cells["valor"].Value = cantidad * valoru * Program.factorPrecio;
                            Program.factorPrecio = 1;

                            existe = 1;
                        }
                    }


                    if (existe == 0)
                    {
                        int x = 0;
                        x = ord.dgvPedido.Rows.Add();
                        ord.dgvPedido.Rows[x].Cells["cod"].Value = 2;
                        ord.dgvPedido.Rows[x].Cells["producto"].Value = botonSel6.Text.ToString().Trim();
                        sNombreProducto_P = botonSel6.Text.ToString().Trim();
                        ord.dgvPedido.Rows[x].Cells["cantidad"].Value = 1;
                        ord.dgvPedido.Rows[x].Cells["guardada"].Value = 0;
                        ord.dgvPedido.Rows[x].Cells["idProducto"].Value = modificadores.modificadores[subindice].sIdModificador;
                        ord.dgvPedido.Rows[x].Cells["cortesia"].Value = 0;
                        ord.dgvPedido.Rows[x].Cells["motivoCortesia"].Value = "";
                        ord.dgvPedido.Rows[x].Cells["cancelar"].Value = 0;
                        ord.dgvPedido.Rows[x].Cells["motivoCancelacion"].Value = "";
                        ord.dgvPedido.Rows[x].Cells["colIdMascara"].Value = "";
                        ord.dgvPedido.Rows[x].Cells["colSecuenciaImpresion"].Value = iSecuencia.ToString();
                        ord.dgvPedido.Rows[x].Cells["colOrdenamiento"].Value = "";
                        ord.dgvPedido.Rows[x].Cells["colIdOrden"].Value = "";                        
                        sPagaIva_P = botonSel6.AccessibleDescription.ToString().Trim();
                        ord.dgvPedido.Rows[x].Cells["pagaIva"].Value = sPagaIva_P;

                        if (sPagaIva_P == "")
                        {
                            ord.dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Blue;
                            ord.dgvPedido.Rows[x].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                            ord.dgvPedido.Rows[x].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                            ord.dgvPedido.Rows[x].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " PAGA IVA";
                        }

                        else
                        {
                            ord.dgvPedido.Rows[x].DefaultCellStyle.ForeColor = Color.Purple;
                            ord.dgvPedido.Rows[x].Cells["cantidad"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                            ord.dgvPedido.Rows[x].Cells["producto"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                            ord.dgvPedido.Rows[x].Cells["valor"].ToolTipText = sNombreProducto_P.Trim().ToUpper() + " NO PAGA IVA";
                        }

                        cantidad = float.Parse("1");

                        ord.dgvPedido.Rows[x].Cells["valuni"].Value = precios.precios[0].sPreciosItems;
                        valoru = float.Parse(precios.precios[0].sPreciosItems);
                        ord.dgvPedido.Rows[x].Cells["valor"].Value = Math.Round((cantidad * valoru * Program.factorPrecio), 2);
                        Program.factorPrecio = 1;

                        if (Program.factorPrecio != 1)
                        {
                            ord.dgvPedido.Rows[x].Cells["cantidad"].Value = 0.5;
                            cantidad = 0.5f;
                        }


                        Program.factorPrecio = 1;

                    }
                }

                ord.calcularTotales();

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "No hay precio en este producto ";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

         private void btnCancelar_Click(object sender, EventArgs e)
         {
             this.Close();
         }

         private void btnListo_Click(object sender, EventArgs e)
         {
             this.DialogResult = DialogResult.OK;
             this.Close();
         }
    }
}
