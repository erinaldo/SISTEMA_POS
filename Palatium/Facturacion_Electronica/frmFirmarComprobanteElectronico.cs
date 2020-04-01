using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmFirmarComprobanteElectronico : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases_Factura_Electronica.ClaseFirmarXML firmarXML = new Clases_Factura_Electronica.ClaseFirmarXML();        
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        DataTable dtConsulta;
        string sSql;
        bool bRespuesta = false;
        int iIdFactura = 0;

        int iCol_Correlativo;
        int iCol_Codigo;
        int iCol_Descripcion;

        string sNumeroDocumento;
        string sCodigoDocumento;
        string sRutaArchivo;
        string sAyuda;

        public frmFirmarComprobanteElectronico()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA RELLENAR LAS INSTRUCCIONES SQL
        private void llenarInstruccionesSQL()
        {
            sAyuda = firmarXML.GSub_ActualizaPantalla(sCodigoDocumento, 1);
            iCol_Correlativo = 4;
            iCol_Codigo = 0;
            iCol_Descripcion = 1;
            dB_Ayuda_Facturas.Ver(sAyuda, "", iCol_Correlativo, iCol_Codigo, iCol_Descripcion);
        }

        //FUNCION PARA LIMPIAR EL FORMULARIO
        private void limpiar()
        {
            iIdFactura = 0;
            txtClaveAcceso.Clear();
            txtNumeroDocumento.Clear();
            txtRutaArchivosGenerados.Clear();
            txtRutaArchivosFirmados.Clear();
            txtArchivoFirmar.Clear();

            dB_Ayuda_Facturas.limpiar();

            llenarComboTipoComprobante();
            llenarComboCertificadoDigital();
            llenarComboTipoAmbiente();
            llenarComboTipoEmision();
            cargarConfiguracion();
        }

        //LLENAR EL COMBOBOX DE TIPO DE COMPROBANTE
        private void llenarComboTipoComprobante()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_tipo_comprobante, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_comprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by id_tipo_comprobante";

                cmbTipoComprobante.llenar(dtConsulta, sSql);

                if (cmbTipoComprobante.Items.Count > 0)
                {
                    cmbTipoComprobante.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE CERTIFICADOS DIGITALES
        private void llenarComboCertificadoDigital()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_tipo_certificado_digital, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_certificado_digital" + Environment.NewLine;
                sSql += "Where estado = 'A'" + Environment.NewLine;
                sSql += "Order By codigo";
                
                cmbCertificadoDigital.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE TIPOS DE AMBIENTES
        private void llenarComboTipoAmbiente()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "Select id_tipo_ambiente, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_ambiente" + Environment.NewLine;
                sSql += "Where estado = 'A'" + Environment.NewLine;
                sSql += "Order By codigo";

                cmbTipoAmbiente.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBOBOX DE TIPOS DE EMISION
        private void llenarComboTipoEmision()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "Select id_tipo_emision, nombres" + Environment.NewLine;
                sSql += "from cel_tipo_emision" + Environment.NewLine;
                sSql += "Where estado = 'A'" + Environment.NewLine;
                sSql += "Order By codigo";

                cmbTipoEmision.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PARAMETROS CONFIGURADOS EN EL SISTEMA
        private void cargarConfiguracion()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_emision, id_tipo_ambiente, id_tipo_certificado_digital" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbTipoEmision.SelectedValue = dtConsulta.Rows[0][0].ToString();
                        cmbTipoAmbiente.SelectedValue = dtConsulta.Rows[0][1].ToString();
                        cmbCertificadoDigital.SelectedValue = dtConsulta.Rows[0][2].ToString();
                    }

                    else
                    {
                        cmbTipoEmision.SelectedIndex = 0;
                        cmbTipoAmbiente.SelectedIndex = 0;
                        cmbTipoComprobante.SelectedIndex = 0;

                        ok.lblMensaje.Text = "No se encuentra configurado los parámetros de facturación electrónica.";
                        ok.ShowDialog();
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

        //FUNCION PARA COMPLETAR LA INFORMACION DEL FORMULARIO 
        private void llenarInformacionFactura(int iId)
        {
            try
            {
                sSql = "";
                sSql += sSql + "select * from cel_vw_infofactura" + Environment.NewLine;
                sSql += "where id_factura = " + iId;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCodigoDocumento = dtConsulta.Rows[0].ItemArray[6].ToString();
                        sNumeroDocumento = "";

                        if (sCodigoDocumento == "01")
                        {
                            sNumeroDocumento = sNumeroDocumento + "F";
                        }

                        else if (sCodigoDocumento == "04")
                        {
                            sNumeroDocumento = sNumeroDocumento + "NC";
                        }

                        else if (sCodigoDocumento == "07")
                        {
                            sNumeroDocumento = sNumeroDocumento + "R";
                        }

                        else if (sCodigoDocumento == "06")
                        {
                            sNumeroDocumento = sNumeroDocumento + "G";
                        }

                        else if (sCodigoDocumento == "05")
                        {
                            sNumeroDocumento = sNumeroDocumento + "ND";
                        }
                                                
                        sNumeroDocumento = sNumeroDocumento + dtConsulta.Rows[0].ItemArray[7].ToString() +
                                           dtConsulta.Rows[0].ItemArray[8].ToString() + 
                                           dtConsulta.Rows[0].ItemArray[9].ToString().PadLeft(9, '0');

                        txtNumeroDocumento.Text = sNumeroDocumento;

                        //CONSULTAR SI EXISTE EL ARCHIVO EN LA RUTA CONFIGURADA
                        sRutaArchivo = firmarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 1) + @"\" + sNumeroDocumento + ".xml";

                        if (File.Exists(sRutaArchivo))
                        {
                            txtArchivoFirmar.Text = sNumeroDocumento + ".xml";
                            txtRutaArchivosGenerados.Text = firmarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 1);
                            txtRutaArchivosFirmados.Text = firmarXML.GFun_St_Ruta_Archivo(sCodigoDocumento, 2);
                        }

                        else
                        {
                            ok.lblMensaje.Text = "No existe el archivo en la ruta:" + Environment.NewLine + sRutaArchivo;
                            ok.ShowDialog();
                        }
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //INSTRUCCIONES PARA FIRMAR EL DOCUMENTO XML
        private void firmarArchivoXML()
        {
            try
            {
                string sMensaje;
                string sToken;
                string sArchivoCertificadoDigital;
                string sArchivoFirmar;

                string sJar;
                string sCertificado;
                string sPassCertificado;
                string sXmlIn;
                string sXmlPathOut;
                string sFileOut;
                string sCodigoError = "";
                string sDescripcionError = "";
                string[] sCertificado_digital = new string[5];

                //if (Convert.ToInt32(cmbCertificadoDigital.SelectedValue) == 1)
                //{
                //    sToken = "ETOKEN 7300";
                //}

                //else
                //{
                    firmarXML.Gsub_trae_parametros_certificado(sCertificado_digital);
                    sCertificado = sCertificado_digital[0];
                    sPassCertificado = sCertificado_digital[1];
                //}


                sArchivoFirmar = txtRutaArchivosGenerados.Text + @"\" + txtArchivoFirmar.Text;

                NuevoSiNo.lblMensaje.Text = "¿Está seguro de firmar el archivo " + sArchivoFirmar + "?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult != DialogResult.OK)
                {
                    goto fin;
                }

                //==================================================================
                //EN CASO DE USAR TOKEN - CODIGO EN VISUAL BASIC 6.0

                //If DBC_tipo_certificado_digital.Value = 1 Then 'Token
                //  Contraseña del certificado o de etoken
                //  If DBC_Cel_Token.Value = "01" Then
                //Else
                //End If
                //Else  'Archivo
                //End If

                //==================================================================

                //  FIRMA
                //  liberia_sri.firmarArchivo
                //  java -jar sri.jar C:\firmaelectronica\certificado.p12 HDMjmda19901960 C:\F001009000003840.XML C:\facturasfirmadas F001009000003837x.xml
                //  Shell ("cmd.exe /K java -jar" & " " & jar & " " & certificado & " " & passCertificado & " " & xmlIn & " " & xmlOut & " " & fileOut)

                sJar = @"c:\SRI.jar";
                sXmlIn = txtRutaArchivosGenerados.Text + @"\" + txtArchivoFirmar.Text;
                sXmlPathOut = txtRutaArchivosFirmados.Text + @"\";

                sFileOut = txtArchivoFirmar.Text;

                //while (true)
                //{
                    sCodigoError = firmarXML.GSub_FirmarXML(sJar, sCertificado, sPassCertificado, sXmlIn, sXmlPathOut, sFileOut, sCodigoError, sDescripcionError);
                //    //Thread.Sleep(1000);
                //}                


                if (sCodigoError == "00")
                {
                    ok.lblMensaje.Text = "Archivo firmado con éxito.";
                    ok.ShowDialog();
                }

                else
                {
                    ok.lblMensaje.Text = "Error: en la firma. Codigo: " + sCodigoError;
                    ok.ShowDialog();
                }

                cmbTipoComprobante.SelectedIndexChanged -= new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
                limpiar();
                cmbTipoComprobante.SelectedIndexChanged += new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin: {}
        }

        #endregion

        private void frmFirmarComprobanteElectronico_Load(object sender, EventArgs e)
        {
            cmbTipoComprobante.SelectedIndexChanged -= new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
            limpiar();
            cmbTipoComprobante.SelectedIndexChanged += new EventHandler(cmbTipoComprobante_SelectedIndexChanged);
            sCodigoDocumento = "01";
            lblTipoDocumento.Text = cmbTipoComprobante.Text;
            llenarInstruccionesSQL();
        }

        private void cmbTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "No ha seleccionado el tipo de comprobante para recuperar los directorios.";
                    ok.ShowDialog();
                }

                else
                {
                    if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 1)
                    {
                        sCodigoDocumento = "01";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 2)
                    {
                        sCodigoDocumento = "04";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 3)
                    {
                        sCodigoDocumento = "05";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 4)
                    {
                        sCodigoDocumento = "06";
                    }

                    else if (Convert.ToInt32(cmbTipoComprobante.SelectedValue) == 5)
                    {
                        sCodigoDocumento = "07";
                    }

                    lblTipoDocumento.Text = cmbTipoComprobante.Text;
                    llenarInstruccionesSQL();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                iIdFactura = dB_Ayuda_Facturas.iId;

                if (iIdFactura == 0)
                {
                    ok.lblMensaje.Text = "No ha seleccionado ninguna factura.";
                    ok.ShowDialog();
                }

                else
                {
                    sSql = "";
                    sSql = sSql + "select " + conexion.GFun_St_esnulo() + "(clave_acceso, 'NINGUNA') clave_acceso " + Environment.NewLine;
                    sSql = sSql + "from cv403_facturas " + Environment.NewLine;
                    sSql = sSql + "where id_factura = " + iIdFactura;

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            if (dtConsulta.Rows[0].ItemArray[0].ToString() == "NINGUNA")
                            {
                                ok.lblMensaje.Text = "No ha generado el archivo xml. Favor vuelva a generar.";
                                ok.ShowDialog();
                            }

                            else
                            {
                                txtClaveAcceso.Text = dtConsulta.Rows[0].ItemArray[0].ToString();
                                llenarInformacionFactura(iIdFactura);
                            }
                        }

                        else
                        {
                            ok.lblMensaje.Text = "No existe un registro de factura. Comuníquese con el administrador.";
                            ok.ShowDialog();
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }

                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnFirmar_Click(object sender, EventArgs e)
        {
            firmarArchivoXML();
        }
    }
}
