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
    public partial class frmModificarItemsPedido : Form
    {
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        TextBox[] txtNuevaLinea = new TextBox[100];
        Label[] lblNuevaLinea = new Label[100];
        Button[] btnRemoverNuevaLinea = new Button[100];

        int iPosicion = 0;
        int iNuevaPosicion = 0;
        int iBandera = 0;
        int i;
        int j;
        int h = 1;
        int iAux = 0;

        int iPosicionYLabel = 13;
        int iPosicionYTextBox = 7;
        int iPosicionYBoton = 6;

        int iMaximoCrear = 10;
        int iCuentaCrear = 0;

        public int iIdProducto;

        public frmModificarItemsPedido()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIOS

        //FUNCION PARA RECUPERAR DATOS EN CASO DE QUE SI EXISTIERA
        private void recuperarArreglo()
        {
            try
            {
                //for (i = 0; i < Program.iContadorDetalle; i++)
                for (i = 0; i < 10; i++)
                {
                    if (Program.sDetallesItems[i, 0] == iIdProducto.ToString())
                    {
                        iPosicion = i;
                        iNuevaPosicion = i;
                        iBandera = 1;
                        break;
                    }
                }

                if (iBandera == 1)
                {
                    agregarLineaProducto();
                }

                else
                {
                    agregarLinea();
                }

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA AGREGAR LAS CAJAS DE TEXTO
        private void agregarLineaProducto()
        {
            try
            {
                for (i = 0; i < Program.iContadorDetalleMximoY; i++)
                {
                    if (Program.sDetallesItems[iPosicion, i + 1] == null)
                    {
                        break;
                    }
                }

                for (j = 0; j < i; j++)
                {
                    //AGREGAR ETIQUETAS
                    lblNuevaLinea[j] = new Label();
                    lblNuevaLinea[j].Location = new Point(13, iPosicionYLabel);
                    lblNuevaLinea[j].AutoSize = true;
                    lblNuevaLinea[j].Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular);
                    lblNuevaLinea[j].Text = "Observación " + h + ":";

                    //AGREGAR CAJAS DE TEXTO
                    txtNuevaLinea[j] = new TextBox();
                    txtNuevaLinea[j].Location = new Point(133, iPosicionYTextBox);
                    txtNuevaLinea[j].Width = 225;
                    txtNuevaLinea[j].Tag = h;
                    txtNuevaLinea[j].MaxLength = 150;
                    txtNuevaLinea[j].Name = "txtNuevaLinea" + h;
                    txtNuevaLinea[j].CharacterCasing = CharacterCasing.Upper;
                    txtNuevaLinea[j].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                    txtNuevaLinea[j].Text = Program.sDetallesItems[iPosicion, j + 1];

                    //AGREGAR BOTON DE REMOVER
                    btnRemoverNuevaLinea[j] = new Button();
                    btnRemoverNuevaLinea[j].Location = new Point(387, iPosicionYBoton);
                    btnRemoverNuevaLinea[j].Height = 30;
                    btnRemoverNuevaLinea[j].Width = 30;
                    btnRemoverNuevaLinea[j].Tag = h;
                    btnRemoverNuevaLinea[j].BackColor = Color.FromArgb(255, 255, 128);
                    btnRemoverNuevaLinea[j].ForeColor = Color.White;
                    btnRemoverNuevaLinea[j].Image = Palatium.Properties.Resources.menos_png;
                    btnRemoverNuevaLinea[j].ImageAlign = ContentAlignment.TopCenter;
                    btnRemoverNuevaLinea[j].Click += botonImprimir_clic;
                    toolTip1.SetToolTip(btnRemoverNuevaLinea[j], "Remover Preferencia " + (h + 1).ToString());

                    pnlControles.Controls.Add(lblNuevaLinea[j]);
                    pnlControles.Controls.Add(txtNuevaLinea[j]);
                    pnlControles.Controls.Add(btnRemoverNuevaLinea[j]);
                    txtNuevaLinea[j].Focus();
                    iPosicionYLabel += 27;
                    iPosicionYTextBox += 27;
                    iPosicionYBoton += 27;
                    h++;
                    iCuentaCrear++;
                }

                iPosicion = i;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA AGREGAR LAS CAJAS DE TEXTO
        private void agregarLinea()
        {
            try
            {
                if (iCuentaCrear == iMaximoCrear)
                {
                    return;
                }

                //AGREGAR ETIQUETAS
                lblNuevaLinea[iPosicion] = new Label();
                lblNuevaLinea[iPosicion].Location = new Point(13, iPosicionYLabel);
                lblNuevaLinea[iPosicion].AutoSize = true;
                lblNuevaLinea[iPosicion].Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular);
                lblNuevaLinea[iPosicion].Text = "Observación " + h + ":";

                //AGREGAR CAJAS DE TEXTO
                txtNuevaLinea[iPosicion] = new TextBox();
                txtNuevaLinea[iPosicion].Location = new Point(133, iPosicionYTextBox);
                txtNuevaLinea[iPosicion].Width = 225;
                txtNuevaLinea[iPosicion].Tag = h;
                txtNuevaLinea[iPosicion].MaxLength = 150;
                txtNuevaLinea[iPosicion].Name = "txtNuevaLinea" + h;
                txtNuevaLinea[iPosicion].CharacterCasing = CharacterCasing.Upper;
                txtNuevaLinea[iPosicion].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //AGREGAR BOTON DE REMOVER
                btnRemoverNuevaLinea[iPosicion] = new Button();
                btnRemoverNuevaLinea[iPosicion].Location = new Point(387, iPosicionYBoton);
                btnRemoverNuevaLinea[iPosicion].Height = 30;
                btnRemoverNuevaLinea[iPosicion].Width = 30;
                btnRemoverNuevaLinea[iPosicion].Tag = h;
                btnRemoverNuevaLinea[iPosicion].BackColor = Color.FromArgb(255, 255, 128);
                btnRemoverNuevaLinea[iPosicion].ForeColor = Color.White;
                btnRemoverNuevaLinea[iPosicion].Image = Palatium.Properties.Resources.menos_png;
                btnRemoverNuevaLinea[iPosicion].ImageAlign = ContentAlignment.TopCenter;
                btnRemoverNuevaLinea[iPosicion].Click += botonImprimir_clic;
                toolTip1.SetToolTip(btnRemoverNuevaLinea[iPosicion], "Remover Preferencia " + (h + 1).ToString());

                pnlControles.Controls.Add(lblNuevaLinea[iPosicion]);
                pnlControles.Controls.Add(txtNuevaLinea[iPosicion]);
                pnlControles.Controls.Add(btnRemoverNuevaLinea[iPosicion]);
                txtNuevaLinea[iPosicion].Focus();
                iPosicion++;
                iPosicionYLabel += 27;
                iPosicionYTextBox += 27;
                iPosicionYBoton += 27;
                h++;
                iCuentaCrear++;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CREAR EVENTO CLIC PARA ALOS BOTONES REMOVER
        private void botonImprimir_clic(object sender, EventArgs e)
        {
            try
            {
                Button botonsel = sender as Button;
                int iTag = Convert.ToInt32(botonsel.Tag);

                txtNuevaLinea[iTag - 1].ReadOnly = true;
                txtNuevaLinea[iTag - 1].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Strikeout);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            agregarLinea();
        }

        private void frmModificarItemsPedido_Load(object sender, EventArgs e)
        {
            recuperarArreglo();
        }

        private void btnListo_Click(object sender, EventArgs e)
        {
            try
            {
                if (iBandera == 1)
                {
                    //VACIAR TODO LA FILA DEL ARREGLO
                    for (i = 0; i < 100; i++)
                    {
                        Program.sDetallesItems[iNuevaPosicion, i] = null;
                    }

                    Program.sDetallesItems[iNuevaPosicion, 0] = iIdProducto.ToString();

                    for (i = 0; i < iPosicion; i++)
                    {
                        if (txtNuevaLinea[i].Text != "")
                        {
                            if (txtNuevaLinea[i].ReadOnly == false)
                            {
                                Program.sDetallesItems[iNuevaPosicion, iAux + 1] = txtNuevaLinea[i].Text.Trim();
                                iAux++;
                            }
                        }
                    }
                }

                else
                {
                    Program.sDetallesItems[Program.iContadorDetalle, 0] = iIdProducto.ToString();

                    for (i = 0; i < iPosicion; i++)
                    {
                        if (txtNuevaLinea[i].Text != "")
                        {
                            if (txtNuevaLinea[i].ReadOnly == false)
                            {
                                Program.sDetallesItems[Program.iContadorDetalle, iAux + 1] = txtNuevaLinea[i].Text.Trim();
                                iAux++;
                            }
                        }
                    }

                    Program.iContadorDetalle++;
                }

                this.Close();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmModificarItemsPedido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
