using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Utilitarios
{
    public partial class frmReporteGenerico : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        string sRetorno;
        string sSql;

        DataTable dtImprimir;

        bool bRespuesta;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        int iCortarPapel;
        int iAbrirCajon;
        int iImprimir;
        int iImpresioMultiple;
        int iCantidadImprimir;
        int iAbriCajonDinero;

        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;

        public frmReporteGenerico(string sReporte_P, int iImprimir_P, int iAbriCajonDinero_P, int iImpresioMultiple_P, int iCantidadImprimir_P)
        {
            this.sRetorno = sReporte_P;
            this.iImprimir = iImprimir_P;
            this.iAbriCajonDinero = iAbriCajonDinero_P;
            this.iImpresioMultiple = iImpresioMultiple_P;
            this.iCantidadImprimir = iCantidadImprimir_P;
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
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt32(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt32(dtImprimir.Rows[0][6].ToString());

                        //ENVIAR A IMPRIMIR
                        imprimir.iniciarImpresion();
                        
                        if (iAbriCajonDinero == 1)
                        {
                            imprimir.AbreCajon();
                        }

                        imprimir.escritoEspaciadoCorto(txtReporte.Text);
                        
                        if (iCortarPapel == 1)
                        {
                            imprimir.cortarPapel();
                        }

                        if (iImpresioMultiple == 0)
                        {
                            imprimir.imprimirReporte(sNombreImpresora);
                        }

                        else
                        {
                            for (int i = 0; i < iCantidadImprimir; i++)
                            {
                                imprimir.imprimirReporte(sNombreImpresora);
                            }
                        }
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

        private void frmReporteGenerico_Load(object sender, EventArgs e)
        {
            txtReporte.Text = sRetorno + Environment.NewLine + Environment.NewLine + "." + Environment.NewLine;
            this.ActiveControl = this.lblRecibir;

            if (iImprimir == 1)
            {
                if (Program.iVistaPreviaImpresiones == 1)
                {
                    consultarImpresoraTipoOrden();
                    this.Close();
                }
            }
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmReporteGenerico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmReporteGenerico_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
