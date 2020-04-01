using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Palatium.Pedidos
{
    public partial class frmVerMovimientoCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Clases.ClaseMovimientoCaja movimiento = new Clases.ClaseMovimientoCaja();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        string sSql;
        string sTexto;
        string sRetorno;

        int iIdMovimiento;
        int iCerrar;
        
        bool bRespuesta = false;
        DataTable dtConsulta;
        DataTable dtImprimir;

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;
        int iCortarPapel;
        int iAbrirCajon;

        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;
        string sEstado;

        public frmVerMovimientoCaja(int iIdMovimiento, int iCerrar)
        {
            this.iIdMovimiento = iIdMovimiento;
            this.iCerrar = iCerrar;
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

        //FUNCION PARA CARGAR LA PRECUENTA EN UN TEXTBOX
        private void verMovimientoTextBox()
        {
            try
            {
                sRetorno = movimiento.llenarMovimiento(iIdMovimiento);

                if (sRetorno == "")
                {
                    ok.LblMensaje.Text = "";
                }

                else
                {                    
                    txtReporte.Text = sRetorno;

                    //if (iCerrar == 1)
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

        #endregion

        private void frmVerMovimimentoCaja_Load(object sender, EventArgs e)
        {
            verMovimientoTextBox();
            this.ActiveControl = lblRecibir;
        }

        private void frmVerMovimimentoCaja_KeyDown(object sender, KeyEventArgs e)
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
