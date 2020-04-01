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
    public partial class frmReporteVentasLista : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseReportes reporte = new Clases.ClaseReportes();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        string sSql;
        bool bRespuesta = false;
        DataTable dtConsulta;
        DataTable dtImprimir;

        //string sFecha;
        string sRetorno;
        //int iCerrar;
        int iCortarPapel;
        int iAbrirCajon;
        int iCantidadImpresiones;
        int iIdLocalidad;
        int iIdPosCierreCajero;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        public frmReporteVentasLista(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            this.iIdLocalidad = iIdLocalidad_P;
            this.iIdPosCierreCajero = iIdPosCierreCajero_P;
            InitializeComponent();
        }

        #region FUNCIONES PARA MOSTRAR LA PREUENTA Y FACTURA EN UN TEXTBOX

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
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LA PRECUENTA EN UN TEXTBOX
        private void verMovimientoTextBox()
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
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentra configurado el reporte de cierre.";
                    ok.ShowDialog();
                    return;
                }

                sRetorno += reporte.encabezadoReporte(iIdLocalidad, iIdPosCierreCajero);
                iIdPosCierreCajero = reporte.iIdPosCierreCajero;

                string sAyuda = reporte.listaVentas(iIdLocalidad, iIdPosCierreCajero);

                if (sAyuda != "SN")
                {
                    sRetorno = sRetorno + sAyuda;
                }

                if (sRetorno == "")
                {
                    ok.LblMensaje.Text = "";
                }

                else
                {
                    txtReporte.Text = sRetorno + Environment.NewLine + Environment.NewLine + ".";

                    Program.sValorCierreCaja = sRetorno;

                    //if (Program.iVistaPreviaImpresiones == 1)
                    //{
                    //    consultarImpresoraTipoOrden();
                    //    this.Close();
                    //}

                    sRetorno = "";
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

        private void frmReporteVentasLista_Load(object sender, EventArgs e)
        {
            verMovimientoTextBox();
            this.ActiveControl = lblRecibir;
        }

        private void frmReporteVentasLista_KeyDown(object sender, KeyEventArgs e)
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
