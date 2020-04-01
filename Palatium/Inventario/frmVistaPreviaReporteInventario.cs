using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Inventario
{
    public partial class frmVistaPreviaReporteInventario : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sTexto;

        DataTable dtImprimir;

        bool bRespuesta;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        int iCortarPapel;
        int iAbrirCajon;

        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        public frmVistaPreviaReporteInventario(string sTexto_P)
        {
            this.sTexto = sTexto_P;
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
                        sNombreImpresora = dtImprimir.Rows[0]["path_url"].ToString();
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0]["numero_impresion"].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0]["puerto_impresora"].ToString();
                        sIpImpresora = dtImprimir.Rows[0]["ip_impresora"].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0]["descripcion"].ToString();
                        iCortarPapel = Convert.ToInt32(dtImprimir.Rows[0]["cortar_papel"].ToString());
                        iAbrirCajon = Convert.ToInt32(dtImprimir.Rows[0]["abrir_cajon"].ToString());

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
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmVistaPreviaReporteInventario_Load(object sender, EventArgs e)
        {
            txtReporte.Text = sTexto;

            if (Program.iVistaPreviaImpresiones == 1)
            {
                consultarImpresoraTipoOrden();
                this.Close();
            }

            sTexto = "";
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVistaPreviaReporteInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
