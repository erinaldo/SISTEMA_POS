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
    public partial class frmReporteCierreCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        Clases.ClaseReportes reporte = new Clases.ClaseReportes();
        Clases.ClaseReportesAdicionales reportes_2 = new Clases.ClaseReportesAdicionales();

        string sSql;
        string sCodigo;

        DataTable dtImprimir;
        DataTable dtConsulta;

        bool bRespuesta;

        public string sRetorno;

        int iIdLocalidad;
        int iCerrar;
        int iCortarPapel;
        int iAbrirCajon;
        int iCantidadImpresiones;
        int iIdPosCierreCajero;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        Decimal dbAhorroEmergencia;
        Decimal dbCajaInicial;
        Decimal dbCajaFinal;
        

        public frmReporteCierreCaja(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            this.iIdLocalidad = iIdLocalidad_P;
            this.iIdPosCierreCajero = iIdPosCierreCajero_P;

            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void consultarImpresoraTipoOrden()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select path_url, numero_impresion, puerto_impresora," + Environment.NewLine;
                sSql = sSql + "ip_impresora, descripcion, cortar_papel, abrir_cajon" + Environment.NewLine;
                sSql = sSql + "from pos_impresora" + Environment.NewLine;
                sSql = sSql + "where id_pos_impresora = " + Program.iIdImpresoraReportes;

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
                        iCortarPapel = Convert.ToInt32(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt32(dtImprimir.Rows[0][6].ToString());

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();
                        imprimir.escritoEspaciadoCorto(txtReporte.Text);

                        if (iCortarPapel == 1)
                        {
                            imprimir.cortarPapel();
                        }

                        imprimir.imprimirReporte(sNombreImpresora);
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

        //FUNCION PARA CREAR EL REPORTE
        private void mostrarReporte()
        {
            try
            {
                sRetorno = "";
                
                sSql = "";
                sSql += "select * from pos_vw_reportes_por_localidad" + Environment.NewLine;
                sSql += "where id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "order by orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok.LblMensaje.Text = "No se encuentra configurado el reporte de cierre.";
                    ok.ShowDialog();
                    return;
                }

                sRetorno += reporte.encabezadoReporte(iIdLocalidad, iIdPosCierreCajero);
                dbAhorroEmergencia = reporte.dbAhorroEmergencia;
                dbCajaInicial = reporte.dbCajaInicial;
                dbCajaFinal = reporte.dbCajaFinal;
                iIdPosCierreCajero = reporte.iIdPosCierreCajero;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sCodigo = dtConsulta.Rows[i]["codigo"].ToString();

                    sRetorno += devolverReporte(sCodigo);
                }

                if (sRetorno == "")
                {
                    ok.LblMensaje.Text = "";
                }

                else
                {
                    txtReporte.Text = sRetorno + Environment.NewLine + Environment.NewLine + ".";

                    Program.sValorCierreCaja = sRetorno;

                    if (Program.iVistaPreviaImpresiones == 1)
                    {
                        consultarImpresoraTipoOrden();
                        this.Close();
                    }

                    sRetorno = "";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS REPORTES
        private string devolverReporte(string sCodigo_P)
        {
            try
            {
                string sTexto = "";

                if (sCodigo_P == "01")
                {
                    string sAyuda = reporte.resumenSistema(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "02")
                {
                    string sAyuda = reporte.pagosPrioritarios(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "03")
                {
                    string sAyuda = reporte.productosDespachados(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "04")
                {
                    string sAyuda = reporte.detalleVentasOrigen(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "05")
                {
                    string sAyuda = reporte.reporteCantidadPagos(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "06")
                {
                    string sAyuda = reporte.arqueoCaja(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "07")
                {
                    string sAyuda = reporte.ventasMesero(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "08")
                {
                    string sAyuda = reporte.llenarMovimientosAgrupados(1, iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "09")
                {
                    string sAyuda = reporte.llenarMovimientosAgrupados(0, iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "10")
                {
                    string sAyuda = reporte.ahorroEmergencia(iIdLocalidad, iIdPosCierreCajero, dbAhorroEmergencia);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "11")
                {
                    string sAyuda = reporte.contarMonedas(iIdPosCierreCajero, iIdLocalidad, dbCajaInicial, dbCajaFinal);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "12")
                {
                    string sAyuda = reportes_2.crearReporte(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "13")
                {
                    string sAyuda = reporte.comprobantesAnulados(iIdPosCierreCajero, 1);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }

                    sAyuda = reporte.comprobantesAnulados(iIdPosCierreCajero, Program.iComprobanteNotaEntrega);

                    if (sAyuda != "SN")
                    {
                        sTexto += sAyuda;
                    }
                }

                else if (sCodigo_P == "14")
                {
                    string sAyuda = reporte.consultaPropinas(iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "15")
                {
                    string sAyuda = reporte.consultaConsumoEmpleados(iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "16")
                {
                    string sAyuda = reporte.consultarConsumoInterno(iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "17")
                {
                    string sAyuda = reporte.comandasAnulados(iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "18")
                {
                    string sAyuda = reporte.listaVentas(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "19")
                {
                    string sEncabezado = "REPORTE DE CORTESIAS GENERADAS".PadLeft(35, ' ');
                    string sAyuda = reporte.listaOrigenesSInFactura(iIdLocalidad, iIdPosCierreCajero, "04", sEncabezado);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "20")
                {
                    string sEncabezado = "REPORTE DE VALES FUNCIONARIOS GENERADOS";
                    string sAyuda = reporte.listaOrigenesSInFactura(iIdLocalidad, iIdPosCierreCajero, "05", sEncabezado);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "21")
                {
                    string sEncabezado = "REPORTE DE CANJES GENERADOS".PadLeft(34, ' ');
                    string sAyuda = reporte.listaOrigenesSInFactura(iIdLocalidad, iIdPosCierreCajero, "08", sEncabezado);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "22")
                {
                    string sAyuda = reporte.listaPropinasPorTarjetas(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "23")
                {
                    string sAyuda = reporte.comandasPorCobrar(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                else if (sCodigo_P == "24")
                {
                    string sAyuda = reporte.comandasPendientesCobradas(iIdLocalidad, iIdPosCierreCajero);

                    if (sAyuda != "SN")
                    {
                        sTexto = sAyuda;
                    }
                }

                return sTexto;
            }

            catch (Exception ex)
            {
                return "ERROR";
            }
        }

        #endregion

        private void frmReporteCierreCaja_Load(object sender, EventArgs e)
        {
            mostrarReporte();
            this.ActiveControl = lblRecibir;
        }

        private void frmReporteCierreCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }
    }
}

