using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.SplashScreen
{
    public partial class frmSplashScreen : MetroFramework.Forms.MetroForm
    {
        ConexionBD.ConexionBD conectar = new ConexionBD.ConexionBD();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        public frmSplashScreen()
        {
            InitializeComponent();            
        }


        //FUNCION PARA EXTRAER LA PÁGINA WEB DEL FABRICANTE
        private void extraerContactos()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(contacto_fabricante, '0995610690') contacto_fabricante," + Environment.NewLine;
                sSql += "isnull(sitio_web_fabricante, 'www.aplicsis.net') sitio_web_fabricante" + Environment.NewLine;
                sSql += "from pos_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conectar.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        lblContacto.Text = "CONTACTO: " + dtConsulta.Rows[0]["contacto_fabricante"].ToString();
                        lblSitioWeb.Text = dtConsulta.Rows[0]["sitio_web_fabricante"].ToString();
                    }

                    else
                    {
                        lblContacto.Text = "CONTACTO: 0995610690";
                        lblSitioWeb.Text = "www.aplicsis.net";
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void frmSplashScreen_Load(object sender, EventArgs e)
        {
            extraerContactos();
        }
    }
}
