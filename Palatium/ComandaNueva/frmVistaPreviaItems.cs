using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ComandaNueva
{
    public partial class frmVistaPreviaItems : Form
    {
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        DataTable dtOrigen;

        Button[,] boton = new Button[4, 3];

        int iPosXItem;
        int iPosYItem;
        int iCuentaItem;
        int iCuentaAyudaItem;
        int iPagaIva;

        Decimal dbCantidad;
        Decimal dbPrecioUnitario;
        Decimal dbValorDescuento;
        Decimal dbValorIva;
        Decimal dbValorTotal;
        Decimal dbSubtotal;

        public frmVistaPreviaItems(DataTable dtOrigen_P)
        {
            dtOrigen = dtOrigen_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA MOSTRAR LOS ITEMS EN BOTONES
        private void crearPanelItems()
        {
            try
            {
                pnlItems.Controls.Clear();
                iPosXItem = 0;
                iPosYItem = 0;
                iCuentaAyudaItem = 0;

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].Cursor = Cursors.Hand;
                        boton[i, j].Size = new Size(300, 100);
                        boton[i, j].Location = new Point(iPosXItem, iPosYItem);
                        boton[i, j].Font = new Font("Maiandra GD", 12F, FontStyle.Regular);

                        iPagaIva = Convert.ToInt32(dtOrigen.Rows[iCuentaItem]["paga_iva"].ToString());

                        dbCantidad = Convert.ToDecimal(dtOrigen.Rows[iCuentaItem]["cantidad"].ToString());
                        dbPrecioUnitario = Convert.ToDecimal(dtOrigen.Rows[iCuentaItem]["valor_unitario"].ToString());
                        dbValorDescuento = Convert.ToDecimal(dtOrigen.Rows[iCuentaItem]["valor_descuento"].ToString());

                        dbSubtotal = dbPrecioUnitario - dbValorDescuento;

                        if (iPagaIva == 1)
                        {                            
                            dbValorIva = dbSubtotal * Convert.ToDecimal(Program.iva);
                            dbValorTotal = dbCantidad * (dbSubtotal + dbValorIva);
                        }

                        else
                        {
                            dbValorTotal = dbCantidad * dbSubtotal;
                        }

                        boton[i, j].Text = dtOrigen.Rows[iCuentaItem]["nombre_producto"].ToString().ToUpper() + Environment.NewLine +
                                           "CANTIDAD: " + dtOrigen.Rows[iCuentaItem]["CANTIDAD"].ToString() + Environment.NewLine +
                                           "VALOR: $ " + dbValorTotal.ToString("N2");

                        if (i + j % 2 == 0)
                        {
                            boton[i, j].BackColor = Color.Lime;
                        }

                        else
                        {
                            boton[i, j].BackColor = Color.Yellow;
                        }

                        pnlItems.Controls.Add(boton[i, j]);
                        iCuentaItem++;
                        iCuentaAyudaItem++;

                        if (j + 1 == 3)
                        {
                            iPosXItem = 0;
                            iPosYItem += 100;
                        }

                        else
                        {
                            iPosXItem += 300;
                        }

                        if (dtOrigen.Rows.Count == iCuentaItem)
                        {
                            btnSiguiente.Enabled = false;
                            break;
                        }
                    }

                    if (dtOrigen.Rows.Count == iCuentaItem)
                    {
                        btnSiguiente.Enabled = false;
                        break;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        #endregion

        private void frmVistaPreviaItems_Load(object sender, EventArgs e)
        {
            iCuentaItem = 0;
            crearPanelItems();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            iCuentaItem -= iCuentaAyudaItem;

            if (iCuentaItem <= 12)
            {
                btnAnterior.Enabled = false;
            }

            btnSiguiente.Enabled = true;
            iCuentaItem -= 12;

            crearPanelItems();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            btnAnterior.Enabled = true;
            crearPanelItems();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVistaPreviaItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
