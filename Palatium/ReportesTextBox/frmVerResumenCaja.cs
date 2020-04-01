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
    public partial class frmVerResumenCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseCierreCajero arqueo = new Clases.ClaseCierreCajero();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        int iOp;
        string sTexto;
        string sFecha;
        string sSql;
        DataTable dtConsulta;
        DataTable dtImprimir;
        bool bRespuesta = false;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        int iCortarPapel;
        int iAbrirCajon;
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        int iIdLocalidad;
        int iIdCierreCajero;

        Decimal dbAhorroEmergencia;

        public frmVerResumenCaja(int iOp, string sFecha_P, int iIdLocalidad_P, Decimal dbAhorroEmergencia_P, int iIdCierreCajero_P)
        {
            this.iOp = iOp;
            this.sFecha = sFecha_P;
            this.iIdLocalidad = iIdLocalidad_P;
            this.dbAhorroEmergencia = dbAhorroEmergencia_P;
            this.iIdCierreCajero = iIdCierreCajero_P;
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
        
        //CONSULTAR EL CIERRE DE CAJERO
        private void consultarCierreCajero()
        {
            try
            {
                sTexto = "";
                sTexto = arqueo.llenarCierreCajero(sFecha, iIdLocalidad, dbAhorroEmergencia, iIdCierreCajero);
                Program.sCierreCajero = "";
                Program.sCierreCajero = sTexto;
                //sTexto = arqueo.llenarInforme(sFecha);

                if (sTexto == "")
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de cierre de cajero.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

                else
                {
                    //sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                    //sTexto = sTexto + vendido.llenarReporteVentas(sFecha);

                    txtReporte.Text = sTexto;

                    //if (iOp == 1)
                    if (Program.iVistaPreviaImpresiones == 1)
                    {
                        consultarImpresoraTipoOrden();
                        this.Close();
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

        private void frmVerResumenCaja_Load(object sender, EventArgs e)
        {
            consultarCierreCajero();
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVerResumenCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
