using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmEditarItems : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        TextBox[] txtNuevaLinea = new TextBox[100];
        Label[] lblNuevaLinea = new Label[100];
        Button[] btnRemoverNuevaLinea = new Button[100];
        int iPosicion = 0;
        int iNuevaPosicion = 0;
        int iBandera = 0;
        int i;
        int j;
        int y = 10;
        int h = 1;
        int iAux = 0;

        public int iIdProducto;

        public frmEditarItems()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIOS

        //FUNCION PARA RECUPERAR DATOS EN CASO DE QUE SI EXISTIERA
        private void recuperarArreglo()
        {
            try
            {
                for (i = 0; i < Program.iContadorDetalle; i++)
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

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al recuperar la línea de observaciones.";
                ok.ShowDialog();
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
                    lblNuevaLinea[j].Top = y + 3;
                    lblNuevaLinea[j].Left = 10;
                    lblNuevaLinea[j].AutoSize = true;
                    lblNuevaLinea[j].Text = "Preferencia " + h + ":";

                    //AGREGAR CAJAS DE TEXTO
                    txtNuevaLinea[j] = new TextBox();
                    txtNuevaLinea[j].Top = y;
                    txtNuevaLinea[j].Left = 128;
                    txtNuevaLinea[j].Width = 225;
                    txtNuevaLinea[j].Tag = h;
                    txtNuevaLinea[j].MaxLength = 150;
                    txtNuevaLinea[j].Name = "txtNuevaLinea" + h;
                    txtNuevaLinea[j].CharacterCasing = CharacterCasing.Upper;
                    txtNuevaLinea[j].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                    txtNuevaLinea[j].Text = Program.sDetallesItems[iPosicion, j + 1];
                                        
                    //AGREGAR BOTON DE REMOVER
                    btnRemoverNuevaLinea[j] = new Button();
                    btnRemoverNuevaLinea[j].Top = y - 3;
                    btnRemoverNuevaLinea[j].Left = 365;
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
                    y += 30;
                    h++;
                }

                iPosicion = i;
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al generar una nueva línea.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //FUNCION PARA AGREGAR LAS CAJAS DE TEXTO
        private void agregarLinea()
        {
            try
            {
                //AGREGAR ETIQUETAS
                lblNuevaLinea[iPosicion] = new Label();
                lblNuevaLinea[iPosicion].Top = y + 3;
                lblNuevaLinea[iPosicion].Left = 10;
                lblNuevaLinea[iPosicion].AutoSize = true;
                lblNuevaLinea[iPosicion].Text = "Preferencia " + h + ":";

                //AGREGAR CAJAS DE TEXTO
                txtNuevaLinea[iPosicion] = new TextBox();
                txtNuevaLinea[iPosicion].Top = y;
                txtNuevaLinea[iPosicion].Left = 128;
                txtNuevaLinea[iPosicion].Width = 225;
                txtNuevaLinea[iPosicion].Tag = h;
                txtNuevaLinea[iPosicion].MaxLength = 150;
                txtNuevaLinea[iPosicion].Name = "txtNuevaLinea" + h;
                txtNuevaLinea[iPosicion].CharacterCasing = CharacterCasing.Upper;
                txtNuevaLinea[iPosicion].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                //AGREGAR BOTON DE REMOVER
                btnRemoverNuevaLinea[iPosicion] = new Button();
                btnRemoverNuevaLinea[iPosicion].Top = y - 3;
                btnRemoverNuevaLinea[iPosicion].Left = 365;
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
                y += 30;
                h++;         
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al generar una nueva línea.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //CREAR EVENTO CLIC PARA ALOS BOTONES REMOVER
        private void botonImprimir_clic(object sender, EventArgs e)
        {
            Button botonsel = sender as Button;
            int iTag = Convert.ToInt32(botonsel.Tag);

            txtNuevaLinea[iTag - 1].ReadOnly = true;
            txtNuevaLinea[iTag - 1].Font = new Font("Microsoft Sans Serif", 10, FontStyle.Strikeout);
        }

        #endregion

        private void btnAgregarLinea_Click(object sender, EventArgs e)
        {
            agregarLinea();
        }

        private void frmEditarItems_Load(object sender, EventArgs e)
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

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrio un problema al guardar los detalles en el arreglo.";
                ok.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
