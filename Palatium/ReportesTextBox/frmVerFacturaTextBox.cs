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
    public partial class frmVerFacturaTextBox : Form
    {
        Clases.ClaseFacturaTextBox factura = new Clases.ClaseFacturaTextBox();
        Clases.ClaseReporteFactura2 factura2 = new Clases.ClaseReporteFactura2();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        Clases_Factura_Electronica.ClaseReporteFacturaElectronica facturaElectronica = new Clases_Factura_Electronica.ClaseReporteFacturaElectronica();

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

        public frmVerFacturaTextBox(string sIdOrden, int iCerrar)
        {
            this.sIdOrden = sIdOrden;
            this.iCerrar = iCerrar;
            InitializeComponent();
        }

        #region FUNCIONES PARA MOSTRAR LA FACTURA EN UN TEXTBOX

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void consultarImpresoraTipoOrden()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                sSql = sSql + "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                sSql = sSql + "from pos_impresora I, pos_formato_factura FF" + Environment.NewLine;
                sSql = sSql + "where FF.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql = sSql + "and FF.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FF.id_pos_formato_factura = " + Program.iFormatoFactura;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0].ItemArray[0].ToString();
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0].ItemArray[1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0].ItemArray[2].ToString();
                        sIpImpresora = dtImprimir.Rows[0].ItemArray[3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0].ItemArray[4].ToString();
                        iCortarPapel = Convert.ToInt16(dtImprimir.Rows[0].ItemArray[5].ToString());
                        iAbrirCajon = Convert.ToInt16(dtImprimir.Rows[0].ItemArray[6].ToString());

                        //ENVIAR A IMPRIMIR
                        if (Program.iFacturacionElectronica == 0)
                        {
                            Program.iCortar = 1;

                            //ABRIR CAJON
                            imprimir.iniciarImpresion();
                            imprimir.AbreCajon();
                            imprimir.imprimirReporte(sNombreImpresora);

                            //IMPRIMIR
                            //ENVIAR A IMPRIMIR
                            imprimir.iniciarImpresion();


                            if (Program.iFormatoFactura == 1)
                            {
                                imprimir.escritoEspaciadoCorto(factura.llenarFactura(dtConsulta, sIdOrden, "Pagada", dtPago));
                                imprimir.cortarPapel();
                                imprimir.imprimirReporte(sNombreImpresora);
                            }

                            else
                            {
                                imprimir.escritoEspaciadoCorto(factura2.llenarFactura(dtConsulta, sIdOrden, "Pagada", dtPago));
                                imprimir.escritoFuenteAlta("".PadLeft(12, ' ') + "TOTAL:" + factura2.dTotal.ToString("N2").PadLeft(15, ' '));
                                imprimir.cortarPapel();
                                imprimir.imprimirReporte(sNombreImpresora);
                            }

                            sRetorno = "";                            
                        }

                        else
                        {
                            Program.iCortar = 0;
                            //ABRIR CAJON
                            imprimir.iniciarImpresion();
                            imprimir.AbreCajon();
                            imprimir.imprimirReporte(sNombreImpresora);

                            imprimir.iniciarImpresion();
                            imprimir.escritoEspaciadoCorto(facturaElectronica.llenarFactura(dtConsulta, dtPago));
                            imprimir.cortarPapel();
                            imprimir.imprimirReporte(sNombreImpresora);
                            sRetorno = "";
                        }

                        Program.dValorFacturado = 0;
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        
        //FUNCION PARA CARGAR LA FACTURA EN UN TEXTBOX
        private void verFacturaTextBox()
        {
            try
            {
                if (llenarDataTable(1) == true)
                {
                    if (Program.iFacturacionElectronica == 1)
                    {
                        sRetorno = facturaElectronica.llenarFactura(dtConsulta, dtPago);
                    }

                    else
                    {
                        if (Program.iFormatoFactura == 1)
                        {
                            sRetorno = factura.llenarFactura(dtConsulta, sIdOrden, "Pagada", dtPago);
                        }

                        else if (Program.iFormatoFactura == 2)
                        {
                            sRetorno = factura2.llenarFactura(dtConsulta, sIdOrden, "Pagada", dtPago);
                        }
                    }

                    if (sRetorno == "")
                    {
                        goto reversa;
                    }
                    else
                    {
                        sTexto = sTexto + Environment.NewLine;
                        sTexto = sTexto + sRetorno;
                    }

                    txtReporte.Text = sTexto;

                    //if (iCerrar == 1)
                    if (Program.iVistaPreviaImpresiones == 1)
                    {
                        if (Program.iFacturacionElectronica == 1)
                        {
                            consultarImpresoraTipoOrden();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }

                        else
                        {
                            consultarImpresoraTipoOrden();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }

                    sTexto = "";
                    goto fin;
                }

                else
                {
                    goto reversa;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowInTaskbar = false;
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
                    sSql = sSql = "select * from pos_vw_det_pedido" + Environment.NewLine;
                    sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                    sSql = sSql + "and estado = 'A'" + Environment.NewLine;
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
                        //sSql = sSql + "isnull(valor_recibido, valor) valor_recibido" + Environment.NewLine;
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

        private void frmVerFacturaTextBox_Load(object sender, EventArgs e)
        {
            verFacturaTextBox();
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVerFacturaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVerFacturaTextBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
