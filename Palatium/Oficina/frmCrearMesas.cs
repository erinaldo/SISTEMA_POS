using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmCrearMesas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        Button[,] botonMesas;
        Button botonSelecionado;

        DataTable dtConsulta;

        int iIdSeccionMesa;

        string sSql;
        public string sPosicion_X;
        public string sPosicion_Y;

        bool bRespuesta;

        public frmCrearMesas(int iIdSeccion)
        {
            this.iIdSeccionMesa = iIdSeccion;
            InitializeComponent();
        }

        #region FUNCIONES DE USUARIO

        //FUNCION PARA LLENAR EL DATATABLE
        private void consultarDatos()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_mesa, numero_mesa, posicion_x, posicion_y" + Environment.NewLine;
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where id_pos_seccion_mesa = " + iIdSeccionMesa + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR DE BOTONES EL PANEL CON UN ARREGLO DE BOTONES
        private void llenarBotones()
        {
            try
            {
                consultarDatos();

                botonMesas = new Button[7, 9];

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        botonMesas[i, j] = new Button();
                        botonMesas[i, j].Cursor = Cursors.Hand;
                        botonMesas[i, j].Click += boton_clic_Mesa;
                        botonMesas[i, j].Width = 90;
                        botonMesas[i, j].Height = 80;
                        botonMesas[i, j].Top = i * 82;
                        botonMesas[i, j].Left = j * 92;
                        botonMesas[i, j].AccessibleDescription = i.ToString();
                        botonMesas[i, j].AccessibleName = j.ToString();

                        DataRow[] dFila = dtConsulta.Select("posicion_x = " + i + " and posicion_y = " + j);

                        if (dFila.Length != 0)
                        {
                            botonMesas[i, j].Font = new Font("Arial", 25, FontStyle.Bold);
                            botonMesas[i, j].ForeColor = Color.Black;
                            botonMesas[i, j].Tag = Convert.ToInt32(dFila[0][0].ToString());
                            botonMesas[i, j].Text = dFila[0][1].ToString();
                            botonMesas[i, j].BackColor = Color.Lime;
                        }

                        else
                        {
                            botonMesas[i, j].Tag = 0;
                            botonMesas[i, j].BackColor = Color.White;
                        }

                        this.Controls.Add(botonMesas[i, j]);
                        pnlBotones.Controls.Add(botonMesas[i, j]);
                    }
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //FUNCION CLIC PARA LOS BOTONES DEL ARREGLO
        private void boton_clic_Mesa(Object sender, EventArgs e)
        {
            botonSelecionado = sender as Button;

            if (Convert.ToInt32(botonSelecionado.Tag) != 0)
            {
                MessageBox.Show("La mesa no se encuentra disponible.");
            }

            else
            {
                //MessageBox.Show("Mesa disponible.");
                sPosicion_X = botonSelecionado.AccessibleDescription;
                sPosicion_Y = botonSelecionado.AccessibleName;
                this.DialogResult = DialogResult.OK;
            }

            //MessageBox.Show("Botón en la posición " + botonSelecionado.AccessibleDescription + ", " + botonSelecionado.AccessibleName);
        }

        #endregion

        private void frmCrearMesas_Load(object sender, EventArgs e)
        {
            llenarBotones();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCrearMesas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
