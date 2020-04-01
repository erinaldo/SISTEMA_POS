using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Areas
{
    public partial class frmVistaPreviaComanda : Form
    {
        Clases.ClasePrecuentaTextBox precuenta = new Clases.ClasePrecuentaTextBox();
        Clases.ClasePrecuentaTextbox2 precuenta2 = new Clases.ClasePrecuentaTextbox2();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        string sIdOrden;
        string sTexto;
        string sSql;
        string sRetorno;
        string sPath;
        int iCerrar;
        int iCortarPapel;
        int iAbrirCajon;

        bool bRespuesta = false;
        DataTable dtConsulta;
        DataTable dtImprimir;
        DataTable dtEmpresa;
        DataTable dtPago;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;
        string sEstado;

        public frmVistaPreviaComanda(string sIdOrden, string sEstado)
        {
            this.sIdOrden = sIdOrden;
            this.sEstado = sEstado;
            InitializeComponent();
        }

        #region FUNCIONES PARA MOSTRAR LA PREUENTA Y FACTURA EN UN TEXTBOX
               
        //FUNCION PARA CONSULTAR LOS DATOS DE LA EMPRESA
        private bool consultarDatosEmpresa()
        {
            try
            {
                sTexto = "";
                sTexto = sTexto + Program.local + Environment.NewLine;
                sTexto = sTexto + Program.direccion + Environment.NewLine;
                sTexto = sTexto + Program.telefono1;

                if (Program.telefono2 != "")
                {
                    sTexto = sTexto + " - " + Program.telefono2 + Environment.NewLine;
                }

                else
                {
                    sTexto = Environment.NewLine;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { return false; }

        }


        //FUNCION PARA CARGAR LA PRECUENTA EN UN TEXTBOX
        private void verPrecuentaTextBox()
        {
            try
            {
                if (consultarDatosEmpresa() == true)
                {
                    if (llenarDataTable() == true)
                    {
                        if (Program.iFormatoPrecuenta == 1)
                        {
                            sRetorno = precuenta.llenarPrecuenta(dtConsulta, sIdOrden, sEstado, dtPago);
                        }

                        else if (Program.iFormatoPrecuenta == 2)
                        {
                            sRetorno = precuenta2.llenarPrecuenta(dtConsulta, sIdOrden, sEstado, dtPago);
                        }

                        if (sRetorno == "")
                        {
                            goto reversa;
                        }
                        else
                        {
                            sTexto = sTexto + sRetorno;
                        }

                        txtReporte.Text = sTexto;
                        sTexto = "";
                        return;
                    }

                    else
                    {
                        goto reversa;
                    }

                }

                else
                {
                    goto reversa;
                }
            }

            catch (Exception)
            {
                goto reversa;
            }

        reversa:
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }
        }


        //FUNCION PARA LLENAR LOS DATATABLES
        private bool llenarDataTable()
        {
            try
            {
                //OPCION CERO   : PARA PRECUENTA
                //OPCION UNO    : PARA FACTURA

                sSql = "";
                sSql = sSql + "select * from pos_vw_det_pedido" + Environment.NewLine;
                sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql = sSql + "and estado in ('A', 'N')" + Environment.NewLine;
                sSql = sSql + "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sSql = "";
                        sSql = sSql + "select descripcion, sum(valor) valor, cambio,  count(*) cuenta," + Environment.NewLine;
                        sSql = sSql + "sum(isnull(valor_recibido, valor)) valor_recibido" + Environment.NewLine;
                        sSql = sSql + "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                        sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                        sSql = sSql + "group by descripcion, valor, cambio, valor_recibido" + Environment.NewLine;
                        sSql = sSql + "having count(*) >= 1";
                        dtPago = new DataTable();
                        dtPago.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPago, sSql);

                        if (bRespuesta == true)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }

            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        private void frmVistaPreviaComanda_Load(object sender, EventArgs e)
        {
            verPrecuentaTextBox();
            this.ActiveControl = lblRecibir;
        }

        private void frmVistaPreviaComanda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
