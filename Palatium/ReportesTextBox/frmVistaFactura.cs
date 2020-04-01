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
    public partial class frmVistaFactura : Form
    {
        Clases.ClaseNotaVenta notaVenta = new Clases.ClaseNotaVenta();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        Clases.ClaseImprimirFacturaNormal factura = new Clases.ClaseImprimirFacturaNormal();
        Clases_Factura_Electronica.ClaseImprimirFacturaElectronica facturaElectronica = new Clases_Factura_Electronica.ClaseImprimirFacturaElectronica();

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
        int iIdFactura;
        int iIdTipoFactura;
        int iVarios;

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

        public frmVistaFactura(int iIdFactura_P, int iCerrar, int iVarios_P)
        {
            this.iIdFactura = iIdFactura_P;
            this.iCerrar = iCerrar;
            this.iVarios = iVarios_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        private void consultarImpresoraTipoOrden()
        {
            try
            {
                sSql = "";
                sSql += "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                sSql += "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                sSql += "from pos_impresora I, pos_formato_factura FF" + Environment.NewLine;
                sSql += "where FF.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql += "and FF.estado = 'A'" + Environment.NewLine;
                sSql += "and I.estado = 'A'" + Environment.NewLine;
                sSql += "and FF.id_pos_formato_factura = " + Program.iFormatoFactura;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();
                        iCantidadImpresiones = Convert.ToInt32(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt32(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt32(dtImprimir.Rows[0][6].ToString());

                        if (iIdTipoFactura == 1)
                        {
                            if (iVarios == 0)
                            {
                                imprimir.iniciarImpresion();
                                imprimir.escritoEspaciadoCorto(facturaElectronica.sCrearFactura(iIdFactura));

                                if (iCortarPapel == 1)
                                {
                                    if (iCortarPapel == 1)
                                    {
                                        Program.iCortar = 0;
                                    }

                                    else
                                    {
                                        Program.iCortar = 1;
                                    }

                                    imprimir.cortarPapel();
                                }
                                
                                imprimir.imprimirReporte(sNombreImpresora);
                            }

                            else
                            {
                                //ABRIR CAJON DE DINERO

                                imprimir.iniciarImpresion();
                                imprimir.AbreCajon();
                                imprimir.imprimirReporte(sNombreImpresora);

                                for (int i = 0; i < iCantidadImpresiones; i++)
                                {
                                    imprimir.iniciarImpresion();
                                    imprimir.escritoEspaciadoCorto(facturaElectronica.sCrearFactura(iIdFactura));

                                    if (iCortarPapel == 1)
                                    {
                                        if (iCortarPapel == 1)
                                        {
                                            Program.iCortar = 0;
                                        }

                                        else
                                        {
                                            Program.iCortar = 1;
                                        }

                                        imprimir.cortarPapel();
                                    }

                                    imprimir.imprimirReporte(sNombreImpresora);
                                }
                            }
                        }

                        else
                        {
                            //Program.iCortar = 1;

                            if (iCortarPapel == 1)
                            {
                                Program.iCortar = 0;
                            }

                            else
                            {
                                Program.iCortar = 1;
                            }

                            imprimir.iniciarImpresion();
                            imprimir.AbreCajon();
                            imprimir.imprimirReporte(sNombreImpresora);
                            imprimir.iniciarImpresion();
                            imprimir.escritoEspaciadoCorto(factura.sCrearFactura(iIdFactura));
                            imprimir.escritoFuenteAlta("".PadLeft(12, ' ') + "TOTAL:" + factura.dTotal.ToString("N2").PadLeft(15, ' '));
                            imprimir.cortarPapel();
                            imprimir.imprimirReporte(sNombreImpresora);
                        }

                        sRetorno = "";
                        Program.dValorFacturado = 0.0;
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

        private void consultarTipoFactura()
        {
            try
            {
                sSql = "";
                sSql += "select facturaelectronica" + Environment.NewLine;
                sSql += "from cv403_facturas" + Environment.NewLine;
                sSql += "where id_Factura = " + iIdFactura;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    iIdTipoFactura = -1;
                }

                iIdTipoFactura = Convert.ToInt32(dtConsulta.Rows[0]["facturaelectronica"].ToString());
            }

            catch (Exception)
            {
                iIdTipoFactura = -1;
            }
        }

        private void verFacturaTextBox()
        {
            try
            {
                consultarTipoFactura();

                if (iIdTipoFactura == -1)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al visualizar la factura";
                    ok.ShowDialog();
                }

                else
                {
                    if (iIdTipoFactura == 0)
                    {
                        sRetorno = factura.sCrearFactura(iIdFactura);
                        sRetorno += "TOTAL:".PadLeft(28, ' ') + factura.dTotal.ToString("N2").PadLeft(12, ' ') + Environment.NewLine + Environment.NewLine;
                        sRetorno += "PROPINA: " + factura.dbPropina.ToString("N2");
                    }

                    else if (iIdTipoFactura == 1)
                    {
                        sRetorno = facturaElectronica.sCrearFactura(iIdFactura);
                    }

                    if (sRetorno == "")
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al generar la vista previa de la factura.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        sTexto += Environment.NewLine;
                        sTexto += sRetorno;
                    }

                    txtReporte.Text = sTexto;

                    if (Program.iVistaPreviaImpresiones == 1)
                    {
                        if (Program.iFacturacionElectronica == 1)
                        {
                            consultarImpresoraTipoOrden();
                            DialogResult = DialogResult.OK;
                            Close();
                        }

                        else
                        {
                            consultarImpresoraTipoOrden();
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }

                    sTexto = "";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmVistaFactura_Load(object sender, EventArgs e)
        {
            verFacturaTextBox();
            this.ActiveControl = this.lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVistaFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVistaFactura_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
