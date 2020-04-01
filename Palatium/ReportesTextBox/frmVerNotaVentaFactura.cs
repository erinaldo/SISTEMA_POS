using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ReportesTextBox
{
    public partial class frmVerNotaVentaFactura : Form
    {
        Clases.ClaseNotaVentaFacturador notaVenta = new Clases.ClaseNotaVentaFacturador();
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

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        public frmVerNotaVentaFactura(string sIdOrden, int iCerrar)
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
                        Program.iCortar = 0;

                        if (iAbrirCajon == 1)
                        {
                            //ABRIR CAJON
                            imprimir.iniciarImpresion();
                            imprimir.AbreCajon();
                            imprimir.imprimirReporte(sNombreImpresora);

                        }

                        for (int i = 0; i < iCantidadImpresiones; i++)
                        {
                            //IMPRIMIR
                            imprimir.iniciarImpresion();
                            //imprimir.escritoEspaciadoCorto(txtReporte.Text);
                            imprimir.escritoEspaciadoCorto(notaVenta.llenarNota(dtConsulta, sIdOrden, "Pagada"));
                            imprimir.escritoFuenteAlta("TOTAL:" + notaVenta.dbTotal.ToString("N2").PadLeft(27, ' ') + Environment.NewLine);
                            imprimir.cortarPapel();
                            imprimir.imprimirReporte(sNombreImpresora);
                        }                            

                        ////IMPRIMIR
                        //imprimir.iniciarImpresion();
                        ////imprimir.escritoEspaciadoCorto(txtReporte.Text);
                        //imprimir.escritoEspaciadoCorto(notaVenta.llenarNota(dtConsulta, sIdOrden, "Pagada"));
                        //imprimir.escritoFuenteAlta("TOTAL:" + notaVenta.dbTotal.ToString("N2").PadLeft(27, ' ') + Environment.NewLine);
                        //imprimir.cortarPapel();
                        //imprimir.imprimirReporte(sNombreImpresora);

                        Program.dValorFacturado = 0;
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
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

        //EXTRAER LOS DATOS LAS IMPRESORAS
        public void consultarImpresoraAbrirCajon()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select I.path_url" + Environment.NewLine;
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
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();
                        imprimir.AbreCajon();
                        imprimir.imprimirReporte(sNombreImpresora);
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

        //FUNCION PARA CARGAR LA FACTURA EN UN TEXTBOX
        private void verNotaTextBox()
        {
            try
            {
                if (llenarDataTable() == true)
                {
                    sRetorno = notaVenta.llenarNota(dtConsulta, sIdOrden, "Pagada");

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
                        consultarImpresoraAbrirCajon();

                        consultarImpresoraTipoOrden();
                        this.DialogResult = DialogResult.OK;
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

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }

        fin: { }
        }


        //FUNCION PARA LLENAR LOS DATATABLES
        private bool llenarDataTable()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select * from pos_vw_factura" + Environment.NewLine;
                sSql = sSql + "where id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql = sSql + "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
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

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void frmVerNotaVentaFactura_Load(object sender, EventArgs e)
        {
            verNotaTextBox();
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVerNotaVentaFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVerNotaVentaFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
