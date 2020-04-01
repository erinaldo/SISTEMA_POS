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
    public partial class frmVerReportePropietario : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseReportePropietario reportw = new Clases.ClaseReportePropietario();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sTexto;
        string sFecha;
        string sSql;
        DataTable dtConsulta;
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

        int iIdLocalidad;

        public frmVerReportePropietario(string sFecha_P, int iIdLocalidad_P)
        {
            this.sFecha = sFecha_P;
            this.iIdLocalidad = iIdLocalidad_P;
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

        //CONSULTAR EL INFORME PARA EL PROPIETARIO
        private void consultarInformePropietario()
        {
            try
            {
                sTexto = "";
                sTexto = reportw.crearReporte(sFecha, iIdLocalidad);

                if (sTexto == "VACIO")
                {
                    ok.LblMensaje.Text = "No hay valores a visualizar con los parámetros seleccionados.";
                    ok.ShowDialog();
                    this.Close();
                }

                else if (sTexto == "ERROR")
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al generar el reporte. Comuníquese con el administrador.";
                    ok.ShowDialog();
                    this.Close();
                }

                else
                {
                    txtReporte.Text = sTexto;

                    if (Program.iVistaPreviaImpresiones == 1)
                    {
                        consultarImpresoraTipoOrden();
                        this.Close();
                    }
                }

                sTexto = "";
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmVerReportePropietario_Load(object sender, EventArgs e)
        {
            consultarInformePropietario();
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {
            consultarImpresoraTipoOrden();
        }

        private void frmVerReportePropietario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
