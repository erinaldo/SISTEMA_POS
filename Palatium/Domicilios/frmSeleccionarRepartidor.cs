using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Domicilios
{
    public partial class frmSeleccionarRepartidor : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        string sSql;
        string sNombreRepartidor;

        DataTable dtConsulta;

        bool bRespuesta;

        int iPosX;
        int iPosY;
        int iCuentaRepartidor;
        public int iIdRepartidor;

        Button[,] boton = new Button[10, 10];
        Button botonRepartidor;

        public frmSeleccionarRepartidor()
        {
            InitializeComponent();
        }

        #region FUNCIONES DE CONTROL DE BOTONES

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LOS REPARTIDORES
        private void cargarRepartidores()
        {
            try
            {
                pnlPromotores.Controls.Clear();

                sSql = "";
                sSql += "select id_pos_repartidor, descripcion" + Environment.NewLine;
                sSql += "from pos_repartidor" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN SQL:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    iPosX = 0;
                    iPosY = 0;
                    iCuentaRepartidor = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            boton[i, j] = new Button();
                            boton[i, j].Click += boton_clic;
                            boton[i, j].MouseEnter += boton_mouse_enter;
                            boton[i, j].MouseLeave += boton_mouse_leave;

                            boton[i, j].Size = new Size(125, 75);
                            boton[i, j].Location = new Point(iPosX, iPosY);
                            boton[i, j].BackColor = Color.Transparent;
                            boton[i, j].ForeColor = Color.White;
                            boton[i, j].Cursor = Cursors.Hand;
                            boton[i, j].Font = new Font("Arial", 12, FontStyle.Bold);
                            boton[i, j].ForeColor = Color.White;
                            boton[i, j].BackColor = Color.Navy;
                            boton[i, j].Tag = dtConsulta.Rows[iCuentaRepartidor]["id_pos_repartidor"].ToString();
                            boton[i, j].Text = dtConsulta.Rows[iCuentaRepartidor]["descripcion"].ToString();
                            pnlPromotores.Controls.Add(boton[i, j]);
                            iCuentaRepartidor++;

                            if (dtConsulta.Rows.Count == iCuentaRepartidor)
                            {
                                break;
                            }
                        }

                        if (dtConsulta.Rows.Count == iCuentaRepartidor)
                        {
                            break;
                        }
                    }
                }

                else
                {
                    ok.lblMensaje.Text = "No se encuentra promotores en el sistema.";
                    ok.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION CLIC
        public void boton_clic(object sender, EventArgs e)
        {
            botonRepartidor = sender as Button;
            iIdRepartidor = Convert.ToInt32(botonRepartidor.Tag);
            lblRepartidor.Text = botonRepartidor.Text;
            sNombreRepartidor = botonRepartidor.Text;

            this.DialogResult = DialogResult.OK;

        }

        //FUNCION MOUSE_ENTER
        public void boton_mouse_enter(object sender, EventArgs e)
        {
            botonRepartidor = sender as Button;
            ingresaBoton(botonRepartidor);
        }

        //FUNCION MOUSE_LEAVE
        private void boton_mouse_leave(object sender, EventArgs e)
        {
            botonRepartidor = sender as Button;
            salidaBoton(botonRepartidor);
        }

        #endregion

        private void frmSeleccionarRepartidor_Load(object sender, EventArgs e)
        {
            cargarRepartidores();
            iIdRepartidor = Program.iIdPosRepartidor;
            lblRepartidor.Text = Program.sNombreRepartidor.ToUpper();
            sNombreRepartidor = Program.sNombreRepartidor.ToUpper();
        }
    }
}
