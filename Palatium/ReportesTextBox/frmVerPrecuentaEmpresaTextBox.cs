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
    public partial class frmVerPrecuentaEmpresaTextBox : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClasePrecuentaEmpresa precuenta = new Clases.ClasePrecuentaEmpresa();
        Clases.ClasePrecuentaConsumoInterno consumo = new Clases.ClasePrecuentaConsumoInterno();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        string sIdOrden;
        string sTexto;
        string sSql;
        string sRetorno;
        string sPath;
        int iCerrar;
        int iCortarPapel;
        int iAbrirCajon;
        int iBanderaConsumoEmpresa;
        int iBanderaFirma;
        int iEtiquetaConsumo;

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

        public frmVerPrecuentaEmpresaTextBox(string sIdOrden, int iCerrar_P, int iBanderaConsumoEmpresa_P, int iBanderaFirma_P, int iEtiquetaConsumo_P)
        {
            this.sIdOrden = sIdOrden;
            this.iCerrar = iCerrar_P;
            this.iBanderaConsumoEmpresa = iBanderaConsumoEmpresa_P;
            this.iBanderaFirma = iBanderaFirma_P;
            this.iEtiquetaConsumo = iEtiquetaConsumo_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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
                        iCantidadImpresiones = Convert.ToInt32(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt32(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt32(dtImprimir.Rows[0][6].ToString());

                        string sTextoReporte;

                        if (iBanderaConsumoEmpresa == 1)
                        {
                            sTextoReporte = precuenta.llenarPrecuenta(Convert.ToInt32(sIdOrden));

                            for (int i = 0; i < Program.iCantidadImpresionesEmpresa; i++)
                            {
                                imprimir.iniciarImpresion();
                                //imprimir.escritoEspaciadoCorto(precuenta.llenarPrecuenta(Convert.ToInt32(sIdOrden)));
                                imprimir.escritoEspaciadoCorto(sTextoReporte);
                                imprimir.cortarPapel();
                                imprimir.imprimirReporte(sNombreImpresora);
                            }
                        }

                        else
                        {
                            sTextoReporte = consumo.llenarPrecuenta(Convert.ToInt32(sIdOrden), iBanderaFirma, iEtiquetaConsumo);

                            imprimir.iniciarImpresion();
                            imprimir.escritoEspaciadoCorto(sTextoReporte);
                            imprimir.cortarPapel();
                            imprimir.imprimirReporte(sNombreImpresora);
                        }

                        sRetorno = "";
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

        private void verPrecuentaTextBox()
        {
            try
            {
                if (iBanderaConsumoEmpresa == 1)
                {
                    sRetorno = precuenta.llenarPrecuenta(Convert.ToInt32(sIdOrden));
                }

                else
                {
                    sRetorno = consumo.llenarPrecuenta(Convert.ToInt32(sIdOrden), iBanderaFirma, iEtiquetaConsumo);
                }

                if (sRetorno == "")
                {
                    return;
                }

                sTexto += sRetorno;
                txtReporte.Text = sTexto;

                if (Program.iVistaPreviaImpresiones == 1)
                {
                    consultarImpresoraTipoOrden();
                    Close();
                }

                sTexto = "";
            }

            catch (Exception ex)
            {
            }
        }


        #endregion

        private void frmVerPrecuentaEmpresaTextBox_Load(object sender, EventArgs e)
        {
            verPrecuentaTextBox();
            ActiveControl = (Control)this.lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVerPrecuentaEmpresaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmVerPrecuentaEmpresaTextBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
