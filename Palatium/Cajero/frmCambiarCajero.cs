using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmCambiarCajero : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        string sFecha;
        bool bRespuesta;
        DataTable dtConsulta;
        int iIdCierreCajero;
        int iIdCajero;

        public frmCambiarCajero()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR DATOS
        private void consultarDatos()
        {
            try
            {
                sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql = sSql + "select CC.id_pos_cierre_cajero, CC.id_cajero, C.descripcion," + Environment.NewLine;
                sSql = sSql + "CC.fecha_apertura, CC.hora_apertura, CC.estado_cierre_cajero" + Environment.NewLine;
                sSql = sSql + "from pos_cierre_cajero CC, pos_cajero C" + Environment.NewLine;
                sSql = sSql + "where CC.id_cajero = C.id_pos_cajero" + Environment.NewLine;
                sSql = sSql + "and CC.fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql = sSql + "and CC.estado_cierre_cajero = 'Abierta'" + Environment.NewLine;
                sSql = sSql + "and CC.id_cajero = " + Program.CAJERO_ID + Environment.NewLine;
                sSql = sSql + "and CC.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and C.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdCierreCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        iIdCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        lblCajero.Text = dtConsulta.Rows[0].ItemArray[2].ToString().ToUpper();
                        lblFecha.Text = dtConsulta.Rows[0].ItemArray[3].ToString().Substring(0, 10);
                        lblHora.Text = dtConsulta.Rows[0].ItemArray[4].ToString();
                        lblEstado.Text = dtConsulta.Rows[0].ItemArray[5].ToString();
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe un registro en el sistema. Comuníquese con el administrador del sistema.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }


            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmCambiarCajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmCambiarCajero_Load(object sender, EventArgs e)
        {
            consultarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cajero.frmCodigoCambioCajero cambio = new frmCodigoCambioCajero(iIdCierreCajero, iIdCajero);
            cambio.ShowInTaskbar = false;
            cambio.ShowDialog();

            if (cambio.DialogResult == DialogResult.OK)
            {
                //consultarDatos();
                this.DialogResult = DialogResult.OK;
                cambio.Close();                
            }
        }
    }
}
