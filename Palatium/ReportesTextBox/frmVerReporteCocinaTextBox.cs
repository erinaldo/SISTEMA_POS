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
    public partial class frmVerReporteCocinaTextBox : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Clases.ClaseReporteCocinaTextBox cocina = new Clases.ClaseReporteCocinaTextBox();
        Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();

        //VARIABLES DE CONFIGURACION DE LA IMPRESORA
        string sNombreImpresora;
        int iCantidadImpresiones;

        int iIdImpresionComanda;
        int iIdImpresora;
        int iCortarPapel;
        int iAbrirCajon;
        int iSecuenciaImpresion;

        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;
        string sIdOrden;
        string sRetorno;
        string sDescripcionComanda;

        string sSql;
        DataTable dtConsulta;
        DataTable dtImprimir;
        DataTable dtDestinos;
        DataTable dtSecuencia;
        
        int iRetornoSecuencia;
        int iAcumulador;

        bool bRespuesta = false;

        public frmVerReporteCocinaTextBox(string sIdOrden, int iSecuenciaImpresion)
        {
            this.sIdOrden = sIdOrden;
            this.iSecuenciaImpresion = iSecuenciaImpresion;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LOS DATOS DE LA SECUENCIA DE ENTREGA
        private int consultarSecuenciaEntrega()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_secuencia_entrega, descripcion" + Environment.NewLine;
                sSql += "from pos_secuencia_entrega" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtSecuencia = new DataTable();
                dtSecuencia.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtSecuencia, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }

                else
                {
                    return dtSecuencia.Rows.Count;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA CONSULTAR LO DATOS DE LOS DESTINOS DE IMPRESION
        private void consultarDestinosImpresion()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_impresion_comanda, id_pos_impresora, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_impresion_comanda" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'";

                dtDestinos = new DataTable();
                dtDestinos.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDestinos, sSql);

                if (bRespuesta == true)
                {
                    if (dtDestinos.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No existen registros de destino de impresión para enviar la comanda.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATATABLE
        private void consultarRegistro()
        {
            try
            {
                for (int i = 0; i < dtDestinos.Rows.Count; i++)
                {
                    iIdImpresionComanda = Convert.ToInt32(dtDestinos.Rows[i][0].ToString());
                    iIdImpresora = Convert.ToInt32(dtDestinos.Rows[i][1].ToString());
                    sDescripcionComanda = dtDestinos.Rows[i][2].ToString();

                    sSql = "";
                    sSql = sSql + "select * from pos_vw_det_pedido" + Environment.NewLine;
                    sSql = sSql + "where id_pedido = " + sIdOrden + Environment.NewLine;
                    sSql = sSql + "and id_pos_impresion_comanda = " + iIdImpresionComanda + Environment.NewLine;
                    sSql = sSql + "and secuencia = " + iSecuenciaImpresion + Environment.NewLine;
                    sSql = sSql + "and estado = 'A'" + Environment.NewLine;
                    sSql = sSql + "order by id_pos_secuencia_entrega";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {

                            //VERIFICAR SI SE INGRESO ORDEN DE ENTREGA
                            iAcumulador = 0;
                            for (int k = 0; k < dtConsulta.Rows.Count; k++)
                            {
                                if (dtConsulta.Rows[k][55].ToString() != "0")
                                {
                                    iAcumulador++;
                                }
                            }

                            if (iAcumulador == 0)
                            {
                                sRetorno = "";
                                sRetorno = cocina.llenarPrecuentaCocina(dtConsulta, sIdOrden, sDescripcionComanda);
                            }

                            else
                            {
                                sRetorno = "";
                                sRetorno = cocina.llenarPrecuentaCocinaOrdenEntrega(dtConsulta, sIdOrden, sDescripcionComanda, dtSecuencia);
                            }

                            //SECCION PARA YA IMPRIMIR
                            if (sRetorno == "")
                            {
                                ok.LblMensaje.Text = "No hay datos para imprimir.";
                                ok.ShowDialog();
                            }

                            else
                            {
                                if (Program.iVistaPreviaImpresiones == 1)
                                {
                                    txtReporte.Text = sRetorno;
                                    consultarImpresoraTipoOrden(iIdImpresora); 
                                }

                                else
                                {
                                    txtReporte.Text = txtReporte.Text + Environment.NewLine + Environment.NewLine + sRetorno;
                                }

                                //consultarImpresoraTipoOrden(iIdImpresora); 
                            }                                                       
                        }
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }
                }

                if (Program.iVistaPreviaImpresiones == 1)
                {
                    this.Close();
                }

                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin: { }
        }

        //EXTRAER LOS DATOS LAS IMPRESORAS
        private void consultarImpresoraTipoOrden(int iIdImpresora_P)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select path_url, numero_impresion, puerto_impresora," + Environment.NewLine;
                sSql = sSql + "ip_impresora, descripcion, cortar_papel, abrir_cajon" + Environment.NewLine;
                sSql = sSql + "from pos_impresora" + Environment.NewLine;
                sSql = sSql + "where id_pos_impresora = " + iIdImpresora_P;
                
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
                        //imprimir.escritoEspaciadoCorto(cocina.encabezadoReporteCocina(dtConsulta));


                        string ver;
                        ver = "";

                        //if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                        //{
                        //    imprimir.escritoEspaciadoCorto(cocina.encabezadoReporteCocina(dtConsulta));

                        //    ver = ver + cocina.encabezadoReporteCocina(dtConsulta) + Environment.NewLine;
                        //    ver = ver + dtConsulta.Rows[0][49].ToString();
                        //    //AVANZA ESPACIADO DEL TIPO DE ORDEN
                        //    //imprimir.escritoEspaciadoCorto(dtConsulta.Rows[0][49].ToString());
                        //}

                        //else
                        //{
                        //    imprimir.escritoEspaciadoCorto(cocina.encabezadoReporteCocinaLlevar(dtConsulta));
                        //    //imprimir.escritoFuenteAlta(dtConsulta.Rows[0][40].ToString());
                        //}

                        //imprimir.escritoEspaciadoCorto(cocina.seccionDetalleCocina(dtConsulta));
                        //ver = ver + cocina.seccionDetalleCocina(dtConsulta);

                        Program.iCortar = 0;

                        imprimir.escritoEspaciadoCorto(txtReporte.Text);

                        if (iCortarPapel == 1)
                        {
                            imprimir.cortarPapel();
                        }

                        imprimir.imprimirReporte(sNombreImpresora);

                        sRetorno = "";
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

        #endregion

        private void frmVerReporteCocinaTextBox_Load(object sender, EventArgs e)
        {
            iRetornoSecuencia = consultarSecuenciaEntrega();
            consultarDestinosImpresion();
            consultarRegistro();
            this.ActiveControl = lblRecibir;
        }

        private void menuImprimir_Click(object sender, EventArgs e)
        {

        }

        private void frmVerReporteCocinaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
