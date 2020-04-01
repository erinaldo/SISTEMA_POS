using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Palatium.ReportesTextBox
{
    public partial class frmReporteVendido : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Clases.ClaseReportes reporte = new Clases.ClaseReportes();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
        Clases.ClaseReportesAdicionales reportes_2 = new Clases.ClaseReportesAdicionales();

        string sSql;
        bool bRespuesta = false;
        DataTable dtConsulta;
        DataTable dtImprimir;


        string sFecha;
        string sRetorno;

        int iIdLocalidad;
        int iIdPosCierreCajero;
        int iCerrar;
        int iCortarPapel;
        int iAbrirCajon;
        int iCantidadImpresiones;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;        
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        public frmReporteVendido(int iIdLocalidad_P, int iIdPosCierreCajero_P)
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
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
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
                iIdPosCierreCajero = reporte.iIdPosCierreCajero;

                sRetorno += devolverReporte("03");

                if (sRetorno == "")
                {
                    ok.LblMensaje.Text = "";
                }

                else
                {
                    txtReporte.Text = sRetorno + Environment.NewLine + Environment.NewLine + ".";

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

                string sAyuda = reporte.productosDespachados(iIdLocalidad, iIdPosCierreCajero);

                if (sAyuda != "SN")
                {
                    sTexto = sAyuda;
                }

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        #endregion

        private void frmReporteVendido_Load(object sender, EventArgs e)
        {
            mostrarReporte();
            this.ActiveControl = lblRecibir;
        }

        private void frmReporteVendido_KeyDown(object sender, KeyEventArgs e)
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
