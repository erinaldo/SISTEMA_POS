using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmResumenCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeSiNo SiNo;
        Clases_Factura_Electronica.ClaseEnviarMail correo = new Clases_Factura_Electronica.ClaseEnviarMail();
        
        Clases.ClaseCierreCajero arqueo = new Clases.ClaseCierreCajero();
        Clases.ClaseArqueoCaja2 arqueo2 = new Clases.ClaseArqueoCaja2();
        Clases.ClaseReporteVendido reporte = new Clases.ClaseReporteVendido();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();
                
        bool bRespuesta;
        bool bRespuestaEnvioMail;
        
        DataTable dtConsulta;
        string sFecha;
        double dTotal;
        double dSumaTarjetas = 0, dSumaCheques = 0, dSumaEfectivo = 0, dSumaTransferencias = 0;

        string sDestinatarios;
        string sSql;
        string sFechaCierre;
        string sHoraCierre;
        string sCorreoEmisor;
        string sCorreoCopia1;
        string sCorreoCopia2;
        string sCorreoCopia3;
        string sPalabraClave;
        string sSmtp;
        string sPuerto;
        string sManejaSSL;
        string sNombreComercial;
        string sRazonSocial;
        string sMensajeEnviar;
        string sFacturaActual;
        string sAsuntoMail;
        string sMensajeRetorno;
        string sTextoDesglose;

        int iPuedeGuardar;
        int iOp;
        int iJornada;
        int iIdCierreCajero;
        int iIdPosCierreCajeroParametro;
        int iCuentaRegistroCorreo;

        int iMoneda01, iMoneda05, iMoneda10, iMoneda25, iMoneda50;
        int iBillete1, iBillete2, iBillete5, iBillete10, iBillete20, iBillete50, iBillete100;

        double dTotalPagadoCortesiaP;
        double dTotalProductosCortesiaP;

        Decimal dbCajaInicial;
        Decimal dbCajaFinal;

        public frmResumenCaja(int iPuedeGuardar)
        {
            this.iPuedeGuardar = iPuedeGuardar;
            InitializeComponent();            
        }

        #region ANIMACION DE BOTONES

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.BackColor = Color.LightSkyBlue;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.BackColor = Color.White;
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR LOS DATOS DEL CORREO DEL EMISOR
        private int consultarDatosMail()
        {
            try
            {
                sSql = "";
                sSql += "select cuenta, password, smtp, puerto, maneja_SSL," + Environment.NewLine;
                sSql += "correo_1, correo_2, correo_3" + Environment.NewLine;
                sSql += "from pos_correo_cierre" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    sCorreoEmisor = dtConsulta.Rows[0]["cuenta"].ToString();
                    sPalabraClave = dtConsulta.Rows[0]["password"].ToString();
                    sSmtp = dtConsulta.Rows[0]["smtp"].ToString();
                    sPuerto = dtConsulta.Rows[0]["puerto"].ToString();
                    sManejaSSL = dtConsulta.Rows[0]["maneja_SSL"].ToString();
                    sCorreoCopia1 = dtConsulta.Rows[0]["correo_1"].ToString();
                    sCorreoCopia2 = dtConsulta.Rows[0]["correo_2"].ToString();
                    sCorreoCopia3 = dtConsulta.Rows[0]["correo_3"].ToString();

                    return 1;
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }
        

        
        //FUNCION PARA ENVIAR EL CORREO ELECTRONICO
        private bool enviarMail(string sCaja_P)
        {
            try
            {
                string SCuerpoMensaje;

                //CREANDO EL REPORTE
                LocalReport reporteLocal = new LocalReport();
                reporteLocal.ReportEmbeddedResource = "Palatium.Cajero.rptCierreCajero.rdlc";
                ReportParameter[] parametros = new ReportParameter[1];
                parametros[0] = new ReportParameter("P_Datos", sCaja_P);
                reporteLocal.SetParameters(parametros);                               

                sDestinatarios += sCorreoCopia1;

                if (sCorreoCopia2.Trim() != "")
                    sDestinatarios += ", " + sCorreoCopia2;

                if (sCorreoCopia3.Trim() != "")
                    sDestinatarios += ", " + sCorreoCopia3;

                byte[] bytes = reporteLocal.Render("PDF");

                //File.WriteAllBytes("C:\\palatium\\cierre.pdf", bytes);

                SCuerpoMensaje = "";
                SCuerpoMensaje += "CIERRE DE CAJA:</br></br>";
                SCuerpoMensaje += "FECHA:" + DateTime.Now.ToString("dd-MM-yyyy") + "</br></br>";
                SCuerpoMensaje += "Saludos cordiales.";

                MailMessage mail = new MailMessage();
                mail.To.Add(sDestinatarios);
                mail.From = new MailAddress(sCorreoEmisor);
                mail.Subject = "ENVIO DE CIERRE DE CAJA";
                mail.Body = SCuerpoMensaje;
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;

                string sNombreArchivo = "CIERRE_CAJA_" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";

                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), sNombreArchivo));

                SmtpClient smtp = new SmtpClient();
                smtp.Host = sSmtp;
                smtp.Port = Convert.ToInt32(sPuerto);
                smtp.Credentials = new System.Net.NetworkCredential(sCorreoEmisor, sPalabraClave);

                if (sManejaSSL == "1")
                {
                    smtp.EnableSsl = true;
                }
                else
                {
                    smtp.EnableSsl = false;
                }

                smtp.Send(mail);

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CONSULTAR LAS PROPINAS RECAUDADAS
        private void consultarPropinas()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(sum(isnull(propina, 0)), 10, 2)) propinas" + Environment.NewLine;
                sSql += "from pos_vw_propinas_recaudadas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta =conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                txtPropinas.Text = dtConsulta.Rows[0][0].ToString();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL ESTADO DE LA CAJA A ABRIR
        private void consultarEstadoCaja()
        {
            try
            {
                string sFechaActual_P = Program.sFechaSistema.ToString("yyyy-MM-dd");

                if (Convert.ToDateTime(sFechaActual_P) != Convert.ToDateTime(sFecha))
                {
                    txtAhorroManual.ReadOnly = true;
                }

                else
                {
                    txtAhorroManual.ReadOnly = false;
                }

                sSql = "";
                sSql += "select estado_cierre_cajero, isnull(ahorro_emergencia, '0.00') ahorro_emergencia," + Environment.NewLine;
                sSql += "isnull(caja_inicial, 0) caja_inicial, id_pos_cierre_cajero" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtAhorroManual.Text = Convert.ToDecimal(dtConsulta.Rows[0]["ahorro_emergencia"].ToString()).ToString("N2");
                        txtCajaInicial.Text = Convert.ToDecimal(dtConsulta.Rows[0]["caja_inicial"].ToString()).ToString("N2");
                        dbCajaInicial = Convert.ToDecimal(dtConsulta.Rows[0]["caja_inicial"].ToString());
                        iIdCierreCajero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cierre_cajero"].ToString());

                        if (iIdCierreCajero == Program.iIdPosCierreCajero)
                        {
                            BtnGuardar.Visible = true;
                            btnVerReporte.Visible = false;
                        }

                        else
                        {
                            BtnGuardar.Visible = false;
                            btnVerReporte.Visible = true;
                        }
                    }

                    else
                    {
                        txtAhorroManual.Text = "0.00";
                        txtCajaInicial.Text = "0.00";
                        iIdCierreCajero = 0;
                        dbCajaInicial = 0;

                        BtnGuardar.Visible = true;
                        btnVerReporte.Visible = false;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void extraerOtrosValoresCortesia(string sCodigo)
        {
            try
            {
                sTextoDesglose = "";
                dTotalPagadoCortesiaP = 0;

                sSql = "";
                sSql += "select isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_otro + DP.valor_iva - DP.valor_dscto)), 0) total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP," + Environment.NewLine;
                sSql += "pos_origen_orden OO" + Environment.NewLine;
                sSql += "where OO.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and OO.codigo = '" + sCodigo + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and OO.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotalPagadoCortesiaP = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        dTotalPagadoCortesiaP = 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar el grid de cheques y tarjetas
        private void cargarGridChequesTarjetas()
        {
            try
            {
                //sFecha = DateTime.Now.ToString("yyyy/MM/dd");
                dSumaCheques = 0;
                dSumaEfectivo = 0;
                dSumaTarjetas = 0;
                dSumaTransferencias = 0;

                dgvCheques.Rows.Clear();
                dgvCheques.Refresh();

                dgvTarjetas.Rows.Clear();
                dgvTarjetas.Refresh();

                sSql = "";
                sSql += "select NP.numero_pedido, FP.descripcion, FP.valor, FP.codigo " + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP " + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + Convert.ToInt32(cmbLocalidades.SelectedValue) + Environment.NewLine;
                sSql += "order by FP.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {                        
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i]["codigo"].ToString() == "CH")
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaCheques = dSumaCheques + dTotal;
                                //dgvCheques.Rows.Add(false, dtConsulta.Rows[i][0].ToString(), dtConsulta.Rows[i][1].ToString(), iTotal.ToString("N2"));
                                dgvCheques.Rows.Add(false, dtConsulta.Rows[i]["numero_pedido"].ToString() + " - CHEQUE", dTotal.ToString("N2"));
                            }

                            else if (dtConsulta.Rows[i]["codigo"].ToString() == "EF")
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaEfectivo = dSumaEfectivo + dTotal;
                                //dgvCheques.Rows.Add(false, dtConsulta.Rows[i][0].ToString(), dtConsulta.Rows[i][1].ToString(), iTotal.ToString("N2"));
                                //dgvCheques.Rows.Add(false, dtConsulta.Rows[i][1].ToString(), dTotal.ToString("N2"));
                            }

                            else if ((dtConsulta.Rows[i]["codigo"].ToString() == "TC") || (dtConsulta.Rows[i]["codigo"].ToString() == "TD"))
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaTarjetas = dSumaTarjetas + dTotal;
                                //dgvTarjetas.Rows.Add(false, dtConsulta.Rows[i][0].ToString(), dtConsulta.Rows[i][1].ToString(), iTotal.ToString("N2"));
                                dgvTarjetas.Rows.Add(false, dtConsulta.Rows[i]["descripcion"].ToString(), dTotal.ToString("N2"));
                            }

                            else if (dtConsulta.Rows[i]["codigo"].ToString() == "TR")
                            {
                                dTotal = Convert.ToDouble(dtConsulta.Rows[i]["valor"].ToString());
                                dSumaTransferencias = dSumaTransferencias + dTotal;
                            }
                        }

                        dgvCheques.ClearSelection();
                        dgvTarjetas.ClearSelection();

                        txtCobradoEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtTotalEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtCobradoTarjetas.Text = dSumaTarjetas.ToString("N2");
                        txtCobradoCheques.Text = dSumaCheques.ToString("N2");
                        txtCobradoTransferencia.Text = dSumaTransferencias.ToString("N2");
                    }

                    else
                    {
                        dgvCheques.Rows.Clear();
                        dgvCheques.Refresh();

                        dgvTarjetas.Rows.Clear();
                        dgvTarjetas.Refresh();

                        txtCobradoEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtTotalEfectivo.Text = dSumaEfectivo.ToString("N2");
                        txtCobradoTarjetas.Text = dSumaTarjetas.ToString("N2");
                        txtCobradoCheques.Text = dSumaCheques.ToString("N2");
                        txtCobradoTransferencia.Text = dSumaTransferencias.ToString("N2");
                    }
                               
                    txtTotalVendido.Text = (dSumaCheques + dSumaTarjetas + dSumaEfectivo + dSumaTransferencias).ToString("N2");
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR LAS ENTRADAS Y SALIDAS 
        private void sumaEntradasSalidas(int op)
        {
            try
            {
                //sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select ltrim(str(isnull(sum(valor),0), 10, 2)) suma" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where tipo_movimiento = " + op + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_documento_pago is null" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotal = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());

                        if (op == 1)
                        {
                            txtEntradasManuales.Text = dTotal.ToString("N2");
                        }

                        else
                        {
                            txtSalidasManuales.Text = dTotal.ToString("N2");
                        }
                    }

                    else
                    {
                        if (op == 1)
                        {
                            txtEntradasManuales.Text = "0.00";
                        }

                        else
                        {
                            txtSalidasManuales.Text = "0.00";
                        }
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER EL VALOR TOTAL DE CORTESIAS
        private void sumarProductosCortesia()
        {
            try
            {
                sSql = "";
                sSql += "select NP.nombre, PC.motivo_cortesia," + Environment.NewLine;
                sSql += "ltrim(str(DP.precio_unitario, 10, 2)) precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad, O.descripcion" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON DP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON CP.id_pos_origen_orden = O.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON DP.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN " + Environment.NewLine;
                sSql += "pos_cortesia PC ON DP.id_det_pedido = PC.id_det_pedido" + Environment.NewLine;
                sSql += "and PC.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_Cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    double suma = 0;
                    if (dtConsulta.Rows.Count != 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            //suma = suma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString()) * (1 + Program.iva + Program.recargo);
                            suma = suma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString());
                        }

                        dTotalProductosCortesiaP = suma;
                    }

                    else
                    {
                        dTotalProductosCortesiaP = 0;
                    }


                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el total de personas que ocupan las mesas
        private void calcularTotalPersonas(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(CP.numero_personas),0) numero " + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_origen_orden ORI " + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido " + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden " + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtTotalPersonas.Text = dtConsulta.Rows[0][0].ToString();
                    }
                    else
                    {
                        txtTotalPersonas.Text = "0";
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el número de órdenes (Mesa, Llevar, Domicilio, Consumo, etc)
        //private int calcularNumeroOrdenes(int iIdPosOrigenOrden)
        private int calcularNumeroOrdenes(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and O.codigo = '" + sCodigo_P + "'" + Environment.NewLine;            
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }
        
        //Función para calcular el total de descuentos
        private void calcularDescuentos()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(DP.cantidad,0), 10, 2)) cantidad, ltrim(str(isnull(DP.valor_dscto,0), 10, 2)) valor_dscto" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_det_pedidos DP" + Environment.NewLine;
                sSql += "where CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and DP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and DP.valor_dscto <> 0" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dTotal = 0;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dTotal = dTotal + (Convert.ToDouble(dtConsulta.Rows[i][0].ToString()) * Convert.ToDouble(dtConsulta.Rows[i][1].ToString()));
                        }

                        txtTotalDescuentos.Text = dTotal.ToString("N2");
                    }
                    else
                    {
                        txtTotalDescuentos.Text = "0.00";
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para calcular el total de orden (Mesa, Llevar, Domicilio, Consumo, etc)
        private double calcularTotalOrigenOrden(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select ORI.descripcion, ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP," + Environment.NewLine;
                sSql += "pos_origen_orden ORI, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;
                sSql += "group by ORI.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0][1].ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA EXTRAER EL IVA COBRADO
        private void extraerIva()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(ltrim(str(sum(DP.cantidad * DP.valor_iva), 10, 2)), 0), 10, 2)) suma" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_facturas_pedidos FP ON CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_facturas F ON F.id_factura = FP.id_factura" + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "where O.genera_factura = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;
                sSql += "and F.idtipocomprobante = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtImpuestoIVA.Text = dtConsulta.Rows[0][0].ToString();
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeCatch();
                        catchMensaje.LblMensaje.Text = "No se pudo extraer el total del IVA cobrado." + Environment.NewLine + "Comuníquese con el administrador del sistema.";
                        catchMensaje.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER LAA CUENTAS POR COBRAR
        private void cargarCuentasPorCobrar()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(ltrim(str(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_otro - DP.valor_dscto)), 10, 2)), 0.00) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_dctos_por_cobrar XC ON CP.id_pedido = XC.id_pedido" + Environment.NewLine;
                sSql += "and XC.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where XC.cg_estado_dcto = 7460" + Environment.NewLine;
                sSql += "and O.cuenta_por_cobrar = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                
                if (bRespuesta == true)
                {
                    txtCuentasPorCobrar.Text = dtConsulta.Rows[0][0].ToString();
                }
                
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LAS TARJETAS DE ALMUERZOS
        private void cargarCuentasTarjetaAlmuerzos()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva - DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where O.cuenta_por_cobrar = 0" + Environment.NewLine;
                sSql += "and O.pago_anticipado = 1" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Cerrada'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtTarjetasAlmuerzos.Text = dtConsulta.Rows[0][0].ToString().Trim();
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL VALOR COBRADO EN PRODUCTOS DE EMERGENCIA
        private void consultaProductosAhorro()
        { 
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto)), 0), 10, 2)) suma_ahorro" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and P.ahorro_emergencia = 1" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + Convert.ToInt32(cmbLocalidades.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtAhorroProductos.Text = dtConsulta.Rows[0][0].ToString();
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECALCULO
        private void recalcularValores()
        {
            try
            {
                Decimal dbEfectivoCobrado_P;
                Decimal dbAhorroProducto_P;
                Decimal dbAhorroManual_P;
                Decimal dbEntradasManuales_P;
                Decimal dbSalidasManuales_P;
                //Decimal dbIvaCobrado_P;
                Decimal dbTotal_P;

                dbEfectivoCobrado_P = Convert.ToDecimal(txtTotalEfectivo.Text.Trim());
                dbAhorroProducto_P = Convert.ToDecimal(txtAhorroProductos.Text.Trim());
                dbAhorroManual_P = Convert.ToDecimal(txtAhorroManual.Text.Trim());
                dbEntradasManuales_P = Convert.ToDecimal(txtEntradasManuales.Text.Trim());
                dbSalidasManuales_P = Convert.ToDecimal(txtSalidasManuales.Text.Trim());
                //dbIvaCobrado_P = Convert.ToDecimal(txtImpuestoIVA.Text.Trim());

                //dbTotal_P = dbEfectivoCobrado_P - dbAhorroProducto_P - dbAhorroManual_P + dbEntradasManuales_P - dbSalidasManuales_P - dbIvaCobrado_P;
                dbTotal_P = dbEfectivoCobrado_P - dbAhorroProducto_P - dbAhorroManual_P + dbEntradasManuales_P - dbSalidasManuales_P + dbCajaInicial;

                txtTotalCaja.Text = dbTotal_P.ToString("N2");
                txtTotalCaja.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS PARAMETROS
        private void cargarValores()
        {       
            dSumaTarjetas = 0;
            dSumaCheques = 0; 
            dSumaEfectivo = 0;
            dSumaTransferencias = 0;
            cargarGridChequesTarjetas();
            sumaEntradasSalidas(1);
            sumaEntradasSalidas(0);
            sumarProductosCortesia();
            extraerOtrosValoresCortesia("04");
            calcularTotalPersonas("01");
            calcularDescuentos();
            txtParaMesa.Text = calcularNumeroOrdenes("01").ToString();
            txtParaLlevar.Text = calcularNumeroOrdenes("02").ToString();
            txtParaDomicilio.Text = calcularNumeroOrdenes("03").ToString();
            txtVentaExpress.Text = calcularNumeroOrdenes("10").ToString();

            txtTotalParaMesa.Text = calcularTotalOrigenOrden("01").ToString("N2");
            txtTotalParaLlevar.Text = calcularTotalOrigenOrden("02").ToString("N2");
            txtTotalParaDomicilio.Text = calcularTotalOrigenOrden("03").ToString("N2");
            txtTotalVentaExpress.Text = calcularTotalOrigenOrden("10").ToString("N2");

            txtTotalCortesias.Text = dTotalPagadoCortesiaP.ToString("N2");
            consultaProductosAhorro();
            extraerIva();
            cargarCuentasPorCobrar();
            cargarCuentasTarjetaAlmuerzos();
            recalcularValores();
            consultarPropinas();

            //txtTotalCaja.Text = (Convert.ToDouble(txtTotalVendido.Text) + Convert.ToDouble(txtEntradasManuales.Text) - Convert.ToDouble(txtSalidasManuales.Text)).ToString("N2");
            //txtCuentasPorCobrar.Text = "-" + txtTotalCaja.Text;
            //txtTotalEfectivo.Text = (Convert.ToDouble(txtCobradoEfectivo.Text) - Convert.ToDouble(txtSalidasManuales.Text) + Convert.ToDouble(txtEntradasManuales.Text)).ToString("N2");
        }

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where cg_localidad = " + Program.iCgLocalidadRecuperado;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;

                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmArqueoCaja_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            iIdPosCierreCajeroParametro = Program.iIdPosCierreCajero;
            iJornada = Program.iJORNADA;
            Program.iJornadaRecuperada = iJornada;
            sFecha = DateTime.Now.ToString("yyyy/MM/dd");
            lblFechaCaja.Text = sFecha;

            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);

            consultarEstadoCaja();
            cargarValores();
            iCuentaRegistroCorreo = consultarDatosMail();

            if (Program.iVerCaja == 0)
            {
                BtnGuardar.Visible = false;                
            }

            this.Cursor = Cursors.Default;
        }

        private void btnReporteVendido_Click(object sender, EventArgs e)
        {
            //ReportesTextBox.frmReporteVendido vendido = new ReportesTextBox.frmReporteVendido(sFecha, 1, Convert.ToInt32(cmbLocalidades.SelectedValue), Convert.ToDecimal(txtAhorroManual.Text.Trim()));
            //vendido.ShowDialog();
        }

        private void TimerHora_Tick(object sender, EventArgs e)
        {
            LblFecha.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (iPuedeGuardar == 1)
                {
                    VentanasMensajes.frmMensajeAceptar mensaje = new VentanasMensajes.frmMensajeAceptar();
                    mensaje.LblMensaje.Text = "¿Está seguro que desea realizar el cierre de caja?\nUna vez guardado no podrá realizar modificaciones.";
                    mensaje.ShowDialog();

                    if (mensaje.DialogResult == DialogResult.OK)
                    {
                        this.Cursor = Cursors.WaitCursor;

                        //EXTRAER LA FECHA DEL SISTEMA
                        sSql = "";
                        sSql += "select getdate() fecha";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == false)
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
                            catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            return;
                        }

                        //sFechaCierre = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                        sHoraCierre = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("HH:mm:ss");
                        sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                        //ACTUALIZAR EL REGISTRO PARA CERRAR EL CAJERO
                        //INICIAMOS UNA NUEVA TRANSACCION
                        //=======================================================================================================
                        if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                        {
                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "Error al abrir transacción.";
                            ok.ShowDialog();
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        //=======================================================================================================

                        iMoneda01 = Convert.ToInt32(Program.sContarDinero[7, 1]);
                        iMoneda05 = Convert.ToInt32(Program.sContarDinero[8, 1]);
                        iMoneda10 = Convert.ToInt32(Program.sContarDinero[9, 1]);
                        iMoneda25 = Convert.ToInt32(Program.sContarDinero[10, 1]);
                        iMoneda50 = Convert.ToInt32(Program.sContarDinero[11, 1]);

                        iBillete1 = Convert.ToInt32(Program.sContarDinero[0, 1]);
                        iBillete2 = Convert.ToInt32(Program.sContarDinero[1, 1]);
                        iBillete5 = Convert.ToInt32(Program.sContarDinero[2, 1]);
                        iBillete10 = Convert.ToInt32(Program.sContarDinero[3, 1]);
                        iBillete20 = Convert.ToInt32(Program.sContarDinero[4, 1]);
                        iBillete50 = Convert.ToInt32(Program.sContarDinero[5, 1]);
                        iBillete100 = Convert.ToInt32(Program.sContarDinero[6, 1]);

                        dbCajaFinal = 0;

                        for (int i = 0; i < Program.sContarDinero.Length / 3; i++)
                        {
                            dbCajaFinal += Convert.ToDecimal(Program.sContarDinero[i, 2]);
                        }

                        //INSERTAR LOS VALORES DE LAS MONEDAS
                        sSql = "";
                        sSql += "insert into pos_monedas (" + Environment.NewLine;
                        sSql += "id_pos_cierre_cajero, id_localidad, moneda01, moneda05, moneda10," + Environment.NewLine;
                        sSql += "moneda25, moneda50, billete1, billete2, billete5, billete10," + Environment.NewLine;
                        sSql += "billete20, billete50, billete100, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                        sSql += "terminal_ingreso, tipo_ingreso, id_pos_jornada)" + Environment.NewLine;
                        sSql += "values (" + Environment.NewLine;
                        sSql += Program.iIdPosCierreCajero + ", " + Program.iIdLocalidad + ", " + iMoneda01 + ", ";
                        sSql += iMoneda05 + ", " + iMoneda10 + ", " + iMoneda25 + ", " + iMoneda50 + "," + Environment.NewLine;
                        sSql += iBillete1 + ", " + iBillete2 + ", " + iBillete5 + ", " + iBillete10 + ", " + iBillete20 + ",";
                        sSql += iBillete50 + ", " + iBillete100 + "," + Environment.NewLine;
                        sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, " + Program.iJORNADA + ")";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
                            catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                        //--------------------------------------------------------------------------------------------------------------

                        sSql = "";
                        sSql += "update pos_cierre_cajero set" + Environment.NewLine;
                        sSql += "fecha_cierre = '" + sFecha + "'," + Environment.NewLine;
                        sSql += "hora_cierre = '" + sHoraCierre + "'," + Environment.NewLine;
                        sSql += "ahorro_emergencia = " + Convert.ToDecimal(txtAhorroManual.Text.Trim()) + "," + Environment.NewLine;
                        sSql += "caja_final = " + dbCajaFinal + "," + Environment.NewLine;
                        sSql += "estado_cierre_cajero = 'Cerrada'" + Environment.NewLine;
                        sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero;

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
                            catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                        sSql = "";
                        sSql += "update pos_numero_lote set" + Environment.NewLine;
                        sSql += "fecha_cierre = '" + sFecha + "'," + Environment.NewLine;
                        sSql += "estado_lote = 'Cerrada'" + Environment.NewLine;
                        sSql += "where estado = 'A'" + Environment.NewLine;
                        sSql += "and id_pos_cierre_cajero = " + iIdCierreCajero;

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeCatch();
                            catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                        
                        ReportesTextBox.frmReporteCierreCaja cierre = new ReportesTextBox.frmReporteCierreCaja(Convert.ToInt32(cmbLocalidades.SelectedValue), iIdPosCierreCajeroParametro);
                        cierre.ShowDialog();

                        string sReporte = Program.sValorCierreCaja;

                        if (iCuentaRegistroCorreo == 1)
                        {
                            enviarMail(sReporte);
                        }

                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "El cierre de caja se ha registrado con éxito.";
                        ok.ShowDialog();

                        if (ok.DialogResult == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                        }

                        this.Cursor = Cursors.Default;
                        this.Close();

                        Application.Exit();

                        return;
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No puede cerrar la caja, ya que aún existen órdenes abiertas. Favor cobrar las cuentas abiertas.";
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: {  
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        this.Cursor = Cursors.Default;
                    }
        }

        private void btnVentasMesero_Click(object sender, EventArgs e)
        {
            Oficina.frmVentasPorMesero mesero = new Oficina.frmVentasPorMesero(iIdCierreCajero);
            mesero.ShowDialog();
        }

        private void btnReimpresionTickets_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmVerResumenCaja vendido = new ReportesTextBox.frmVerResumenCaja(1, sFecha, Convert.ToInt32(cmbLocalidades.SelectedValue), Convert.ToDecimal(txtAhorroManual.Text.Trim()), iIdCierreCajero);
            vendido.ShowDialog();
        }

        private void BtnVistaArqueo_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmVerCierreCajero arqueo = new ReportesTextBox.frmVerCierreCajero(1, sFecha, Convert.ToInt32(cmbLocalidades.SelectedValue));
            arqueo.ShowDialog();
        }

        private void frmResumenCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void btnContarDinero_Click(object sender, EventArgs e)
        {
            Cajero.frmContarDinero contar = new frmContarDinero();
            //Cajero.frmRecuperarDinero contar = new frmRecuperarDinero();
            contar.ShowDialog();
        }

        private void btnEnviarInforme_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmVerReportePropietario reporte = new ReportesTextBox.frmVerReportePropietario(sFecha, Convert.ToInt32(cmbLocalidades.SelectedValue));
            reporte.ShowDialog();
        }

        private void btnListarVentas_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmReporteVentasLista vendido = new ReportesTextBox.frmReporteVentasLista(Convert.ToInt32(cmbLocalidades.SelectedValue), iIdPosCierreCajeroParametro);
            vendido.ShowDialog();
        }

        private void btnListarMateriaPrima_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmVerReporteMateriaPrima vendido = new ReportesTextBox.frmVerReporteMateriaPrima(sFecha, 1, Convert.ToInt32(cmbLocalidades.SelectedValue));
            vendido.ShowDialog();
        }

        private void btnDetallarVentas_Click(object sender, EventArgs e)
        {
            Cajero.frmDetalleVentas detalle = new frmDetalleVentas(Convert.ToInt32(cmbLocalidades.SelectedValue), iIdPosCierreCajeroParametro);
            detalle.ShowDialog();
        }

        private void btnEntradas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Oficina.frmEntradasSalidasManuales movimiento = new Oficina.frmEntradasSalidasManuales(1, sFecha, iIdPosCierreCajeroParametro);
            movimiento.ShowDialog();
        }

        private void btnSalidas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Oficina.frmEntradasSalidasManuales movimiento = new Oficina.frmEntradasSalidasManuales(0, sFecha, iIdPosCierreCajeroParametro);
            movimiento.ShowDialog();
        }

        private void txtAhorroManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloDecimales(sender, e, 2);
        }

        private void txtAhorroManual_Leave(object sender, EventArgs e)
        {
            if (txtAhorroManual.Text.Trim() == "")
            {
                txtAhorroManual.Text = "0.00";
            }

            recalcularValores();
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            iJornada = Program.iJORNADA;
            consultarEstadoCaja();
            cargarValores();            
        }

        private void btnAbrirCaja_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRevisarCaja);
        }

        private void btnAbrirCaja_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRevisarCaja);
        }

        private void BtnGuardar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(BtnGuardar);
        }

        private void BtnGuardar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(BtnGuardar);
        }

        private void btnVentasMesero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnVentasMesero);
        }

        private void btnVentasMesero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnVentasMesero);
        }

        private void btnReimpresionTickets_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReimpresionTickets);
        }

        private void btnReimpresionTickets_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReimpresionTickets);
        }

        private void btnReporteVendido_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnReporteVendido);
        }

        private void btnReporteVendido_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnReporteVendido);
        }

        private void BtnVistaArqueo_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(BtnVistaArqueo);
        }

        private void BtnVistaArqueo_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(BtnVistaArqueo);
        }

        private void btnListarVentas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnListarVentas);
        }

        private void btnListarVentas_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnListarVentas);
        }

        private void btnDetallarVentas_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnDetallarVentas);
        }

        private void btnDetallarVentas_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnDetallarVentas);
        }

        private void btnEnviarInforme_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnEnviarInforme);
        }

        private void btnEnviarInforme_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnEnviarInforme);
        }

        private void btnListarMateriaPrima_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnListarMateriaPrima);
        }

        private void btnListarMateriaPrima_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnListarMateriaPrima);
        }

        private void btnContarDinero_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnContarDinero);
        }

        private void btnContarDinero_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnContarDinero);
        }

        private void btnRevisarCaja_Click(object sender, EventArgs e)
        {
            if (Program.iManejaJornada == 1)
            {
                Program.iMostrarJornada = 1;
            }

            Menú.frmCodigoAdministrador codigo = new Menú.frmCodigoAdministrador();
            codigo.ShowDialog();

            if (codigo.DialogResult == DialogResult.OK)
            {
                codigo.Close();

                Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(DateTime.Now.ToString("dd/MM/yyyy"));
                calendario.ShowDialog();

                if (calendario.DialogResult == DialogResult.OK)
                {
                    sFecha = Convert.ToDateTime(calendario.txtFecha.Text).ToString("yyyy/MM/dd");
                    lblFechaCaja.Text = sFecha;
                    iJornada = Program.iJornadaRecuperada;                    
                    consultarEstadoCaja();
                    cargarValores();
                }
            }     
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            Cajero.frmIngresarDineroCaja ingreso = new frmIngresarDineroCaja(Program.iIdPosCierreCajero, Convert.ToInt32(cmbLocalidades.SelectedValue));
            ingreso.ShowDialog();

            if (ingreso.DialogResult == DialogResult.OK)
            {
                txtCajaInicial.Text = ingreso.dbCajaInicial.ToString("N2");
                ingreso.Close();
            }
        }

        private void btnVerReporte_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmReporteCierreCaja cierre = new ReportesTextBox.frmReporteCierreCaja(Convert.ToInt32(cmbLocalidades.SelectedValue), iIdPosCierreCajeroParametro);
            cierre.ShowDialog();
        }

        private void btnVistaPreviaCierre_Click(object sender, EventArgs e)
        {
            Menú.frmCodigoAdministrador codigo = new Menú.frmCodigoAdministrador();
            codigo.ShowDialog();

            if (codigo.DialogResult == DialogResult.OK)
            {
                codigo.Close();
                Cajero.frmVistaPreviaCierre vista = new frmVistaPreviaCierre(Convert.ToInt32(cmbLocalidades.SelectedValue), iIdPosCierreCajeroParametro);
                vista.ShowDialog();
            }
        }

        private void btnDetallarComandas_Click(object sender, EventArgs e)
        {
            Cajero.frmDetalleOrigenOrden orden = new frmDetalleOrigenOrden(iIdPosCierreCajeroParametro);
            orden.ShowDialog();
        }
    }
}
