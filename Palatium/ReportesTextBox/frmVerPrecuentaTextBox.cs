using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace Palatium.Pedidos
{
    public partial class frmVerPrecuentaTextBox : Form
    {
        Clases.ClasePrecuentaTextBox precuenta = new Clases.ClasePrecuentaTextBox();
        Clases.ClasePrecuentaTextbox2 precuenta2 = new Clases.ClasePrecuentaTextbox2();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

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

        public frmVerPrecuentaTextBox(string sIdOrden, int iCerrar, string sEstado)
        {
            this.sIdOrden = sIdOrden;
            this.iCerrar = iCerrar;
            this.sEstado = sEstado;
            InitializeComponent();
        }

        #region FUNCIONES PARA MOSTRAR LA PREUENTA Y FACTURA EN UN TEXTBOX

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void consultarImpresoraTipoOrden()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                sSql = sSql + "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                sSql = sSql + "from pos_impresora I, pos_formato_precuenta FP" + Environment.NewLine;
                sSql = sSql + "where FP.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql = sSql + "and FP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FP.id_pos_formato_precuenta = " + Program.iFormatoPrecuenta;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt16(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt16(dtImprimir.Rows[0][6].ToString());

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();
                        imprimir.escritoEspaciadoCorto(precuenta2.llenarPrecuentaDatos(dtConsulta, sIdOrden, sEstado, dtPago));
                        imprimir.escritoFuenteAlta("TOTAL:" + precuenta2.dbTotalOrden.ToString("N2").PadLeft(27, ' ') + Environment.NewLine);
                        imprimir.escritoEspaciadoCorto(precuenta2.llenarDetallePrecuenta(dtConsulta, sIdOrden, sEstado, dtPago));
                        imprimir.cortarPapel();
                        imprimir.imprimirReporte(sNombreImpresora);
                        sRetorno = "";
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

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

            catch (Exception)
            {
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
                    if (llenarDataTable(0) == true)
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

                        //if (iCerrar == 1)
                        if (Program.iVistaPreviaImpresiones == 1)
                        {
                            consultarImpresoraTipoOrden();
                            this.Close();
                        }

                        sTexto = "";
                        goto fin;
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
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }

        fin: { }
        }

       
        //FUNCION PARA LLENAR LOS DATATABLES
        private bool llenarDataTable(int op)
        {
            try
            {
                //OPCION CERO   : PARA PRECUENTA
                //OPCION UNO    : PARA FACTURA

                if (op == 0)
                {
                    sSql = "";
                    sSql = sSql + "select * from pos_vw_det_pedido" + Environment.NewLine;
                    sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql = sSql + "and estado in ('A', 'N')" + Environment.NewLine;
                    sSql = sSql + "order by id_det_pedido";
                }

                else
                {
                    sSql = "";
                    sSql = sSql + "select * from pos_vw_factura" + Environment.NewLine;
                    sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql = sSql + "order by id_det_pedido";
                }

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
                        return false;
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

        private void frmVerPrecuentaTextBox_Load(object sender, EventArgs e)
        {
            verPrecuentaTextBox();
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVerPrecuentaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
