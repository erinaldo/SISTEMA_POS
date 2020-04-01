using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseReporteCocinaTextBox
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sTexto;
        string sCantidad;
        string sDescripcion;
        string sNombreAlterno;
        string sSql;
        bool bRespuesta;
        DataTable dtPreferencia;

        int iOrden;
        int iOrdenPedido;
        int iIdPedido;
        int iBandera;

        //FUNCION QUE RECIBE LOS DATOS PARA CREAR EL REPORTE DE LA COCINA
        public string llenarPrecuentaCocina(DataTable dtConsulta, string sIdOrden, string sDestino)
        {
            try
            {
                sTexto = "";                

                if (sDestino != "")
                {
                    sTexto = sTexto + "." + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                    sTexto = sTexto + sDestino.PadRight(24, ' ') + "ORDEN DE TRABAJO" + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                    sTexto = sTexto + "ORDEN DE TRABAJO" + Environment.NewLine;
                }

                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + (dtConsulta.Rows[0][32].ToString().Substring(0, 10) + " " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss")).PadLeft(40, ' ') + Environment.NewLine;
                sTexto = sTexto + "SECUENCIA DE IMPRESION: " + dtConsulta.Rows[0][54].ToString() + Environment.NewLine;
                sTexto = sTexto + "ORDEN: ".PadRight(8, ' ') + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;
                sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                {
                    sTexto = sTexto + ("Num. Personas: " + dtConsulta.Rows[0][34].ToString()).PadRight(20, ' ') + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine + Environment.NewLine; ;
                }

                else
                {
                    if (dtConsulta.Rows[0][40].ToString() != "PARA LLEVAR")
                    {
                        sTexto = sTexto + Environment.NewLine + dtConsulta.Rows[0][49].ToString() + Environment.NewLine + Environment.NewLine; ;
                    }
                    else
                    {
                        sTexto = sTexto + Environment.NewLine;
                    }
                }

                sTexto = sTexto + ("Ticket: " + dtConsulta.Rows[0][46].ToString()).PadRight(21, ' ') + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine + Environment.NewLine;

                sTexto = sTexto + "Cantidad " + "Descripcion" + Environment.NewLine;
                sTexto = sTexto + "-------- " + "".PadRight(31, '-') + Environment.NewLine;

                //INSTRUCCION PARA LLENAR LOS PRODUCTOS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sCantidad = dtConsulta.Rows[i][4].ToString();
                    sDescripcion = dtConsulta.Rows[i][2].ToString();
                    sNombreAlterno = dtConsulta.Rows[i][27].ToString();

                    if (sNombreAlterno != "")
                    {
                        sDescripcion = sNombreAlterno;
                    }

                    sTexto = sTexto + sCantidad.PadLeft(5, ' ') +  "".PadRight(4, ' ');

                    if (sDescripcion.Length > 31)
                    {
                        sTexto = sTexto + sDescripcion.Substring(0, 31).PadRight(31, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(9, ' ') + sDescripcion.Substring(31).PadRight((sDescripcion.Substring(32).Length + 5), ' ');
                    }

                    else
                    {
                        sTexto = sTexto + sDescripcion;
                    }

                    sTexto = sTexto + Environment.NewLine;

                    //FUNCIONES PARA SABER SI HAY OBSERVACIONES DEL ITEM
                    //sSql = "select count(*) cuenta from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ToString()) + " and estado = 'A'";
                    sSql = "";
                    sSql += "select detalle from pos_det_pedido_detalle" + Environment.NewLine;
                    sSql += "where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;
                    sSql += "and estado = 'A'" + Environment.NewLine;
                    sSql += "order by id_det_pedido";

                    dtPreferencia = new DataTable();
                    dtPreferencia.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPreferencia, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtPreferencia.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtPreferencia.Rows.Count; j++)
                            {
                                sTexto = sTexto + ">>".PadRight(4, ' ') + dtPreferencia.Rows[j][0].ToString().PadRight(16, ' ') + Environment.NewLine;
                            }
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al consultar las observaciones del item '" + dtConsulta.Rows[i][2].ToString() + ".";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                sTexto = sTexto + "".PadRight(40, '=');

                if (sDestino != "")
                {
                    sTexto = sTexto + Environment.NewLine + Environment.NewLine + ".";
                }

                return sTexto;
            }

            catch (Exception)
            {
                return "";
            }        
        }





        //FUNCION QUE RECIBE LOS DATOS PARA CREAR EL REPORTE DE LA COCINA
        public string llenarPrecuentaCocinaOrdenEntrega(DataTable dtConsulta, string sIdOrden, string sDestino, DataTable dtOrdenEntrega)
        {
            try
            {
                sTexto = "";
                string[] sVector = new string[dtOrdenEntrega.Rows.Count];

                if (sDestino != "")
                {
                    sTexto = sTexto + "." + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                    sTexto = sTexto + sDestino.PadRight(24, ' ') + "ORDEN DE TRABAJO" + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                    sTexto = sTexto + "ORDEN DE TRABAJO" + Environment.NewLine;
                }

                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + (dtConsulta.Rows[0][32].ToString().Substring(0, 10) + " " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss")).PadLeft(40, ' ') + Environment.NewLine;
                sTexto = sTexto + "SECUENCIA DE IMPRESION: " + dtConsulta.Rows[0][54].ToString() + Environment.NewLine;
                sTexto = sTexto + "ORDEN: ".PadRight(8, ' ') + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;
                sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                {
                    sTexto = sTexto + ("Num. Personas: " + dtConsulta.Rows[0][34].ToString()).PadRight(20, ' ') + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine + Environment.NewLine; ;
                }

                else
                {
                    if (dtConsulta.Rows[0][40].ToString() != "PARA LLEVAR")
                    {
                        sTexto = sTexto + Environment.NewLine + dtConsulta.Rows[0][49].ToString() + Environment.NewLine + Environment.NewLine; ;
                    }
                    else
                    {
                        sTexto = sTexto + Environment.NewLine;
                    }
                }

                sTexto = sTexto + ("Ticket: " + dtConsulta.Rows[0][46].ToString()).PadRight(21, ' ') + dtConsulta.Rows[0][32].ToString().Substring(0, 19) + Environment.NewLine + Environment.NewLine;

                sTexto = sTexto + "Cantidad " + "Descripcion" + Environment.NewLine;
                sTexto = sTexto + "-------- " + "".PadRight(31, '-') + Environment.NewLine;

                //INSTRUCCION PARA LLENAR LOS PRODUCTOS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    iIdPedido = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                    iOrden = Convert.ToInt32(dtConsulta.Rows[i][55].ToString());
                    sCantidad = dtConsulta.Rows[i][4].ToString();
                    sDescripcion = dtConsulta.Rows[i][2].ToString();

                    for (int j = 0; j < dtOrdenEntrega.Rows.Count; j++)
                    {
                        iOrdenPedido = Convert.ToInt32(dtOrdenEntrega.Rows[j][0].ToString());

                        if (iOrden == 0)
                        {
                            sVector[0] = sVector[0] + sCantidad.PadLeft(5, ' ') + "".PadRight(4, ' ');

                            if (sDescripcion.Length > 31)
                            {
                                sVector[0] = sVector[0] + sDescripcion.Substring(0, 31).PadRight(31, ' ') + Environment.NewLine;
                                sVector[0] = sVector[0] + "".PadLeft(9, ' ') + sDescripcion.Substring(31).PadRight((sDescripcion.Substring(32).Length + 5), ' ');
                            }

                            else
                            {
                                sVector[0] = sVector[0] + sDescripcion;
                            }

                            sVector[0] = sVector[0] + traerDetalleItem(iIdPedido) + Environment.NewLine;
                            break;
                        }

                        else if (iOrdenPedido == iOrden)
                        {
                            sVector[iOrdenPedido] = sVector[iOrdenPedido] + sCantidad.PadLeft(5, ' ') + "".PadRight(4, ' ');

                            if (sDescripcion.Length > 31)
                            {
                                sVector[iOrdenPedido] = sVector[iOrdenPedido] + sDescripcion.Substring(0, 31).PadRight(31, ' ') + Environment.NewLine;
                                sVector[iOrdenPedido] = sVector[iOrdenPedido] + "".PadLeft(9, ' ') + sDescripcion.Substring(31).PadRight((sDescripcion.Substring(32).Length + 5), ' ');
                            }

                            else
                            {
                                sVector[iOrdenPedido] = sVector[iOrdenPedido] + sDescripcion;
                            }

                            sVector[iOrdenPedido] = sVector[iOrdenPedido] + traerDetalleItem(iIdPedido) + Environment.NewLine;
                            break;
                        }                        
                    }

                }

                //RECORREMOS EL ARREGLO PARA IMPRIMIR
                iBandera = 0;

                for (int i = 1; i < sVector.Length; i++)
                {
                    if (sVector[i] != null)
                    {
                        sTexto = sTexto + "SERVIR " + dtOrdenEntrega.Rows[i - 1][1].ToString() + Environment.NewLine;
                        sTexto = sTexto + sVector[i] + Environment.NewLine;
                        iBandera = 1;
                    }
                }

                if (iBandera == 1)
                {
                    sTexto = sTexto + Environment.NewLine + "".PadLeft(40, '-') + Environment.NewLine;;
                }

                if (sVector[0]!= null)
                {
                    sTexto = sTexto + sVector[0] + Environment.NewLine;
                }


                return sTexto;
            }

            catch (Exception)
            {
                return "";
            }
        }



        //FUNCION PARA EXTRAER DETALLES DEL ITEM
        private string traerDetalleItem(int idPedido)
        {
            try
            {
                //FUNCIONES PARA SABER SI HAY OBSERVACIONES DEL ITEM
                string sDetalle;

                sSql = "";
                sSql += "select detalle from pos_det_pedido_detalle" + Environment.NewLine;
                sSql += "where id_det_pedido = " + idPedido + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtPreferencia = new DataTable();
                dtPreferencia.Clear();

                sDetalle = "";

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPreferencia, sSql);

                if (bRespuesta == true)
                {
                    if (dtPreferencia.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtPreferencia.Rows.Count; j++)
                        {
                            sDetalle = sDetalle + " >>".PadRight(4, ' ') + dtPreferencia.Rows[j][0].ToString().PadRight(16, ' ') + Environment.NewLine;
                        }
                    }
                }

                //else
                //{
                //    ok.LblMensaje.Text = "Ocurrió un problema al consultar las observaciones del item '" + dtConsulta.Rows[i][2].ToString() + ".";
                //    ok.ShowInTaskbar = false;
                //    ok.ShowDialog();
                //}

                return sDetalle;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }


        /* DE ESTA SECCION PARA ABAJO SE SEPARAN LAS CLASES PARA USAR DIFERENTES SECUENCIAS ESCAPE
         * AUTOR: ELVIS GUAIGUA
         * FECHA DE EDICIÓN: 2018/07/14
         */

        //public string encabezadoReporteCocina(DataTable dtConsulta)
        //{
        //    try
        //    {
        //        sTexto = "";
        //        sTexto = sTexto + "." + Environment.NewLine + Environment.NewLine;
        //        sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
        //        sTexto = sTexto + "C O C I N A".PadRight(24, ' ') + "ORDEN DE TRABAJO" + Environment.NewLine;
        //        sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
        //        sTexto = sTexto + dtConsulta.Rows[0][32].ToString().Substring(0, 19).PadLeft(40, ' ') + Environment.NewLine;
        //        sTexto = sTexto + "ORDEN: ".PadRight(8, ' ') + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;

        //        if (dtConsulta.Rows[0][40].ToString() == "MESAS")
        //        {
        //            sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString().PadRight(15, ' ') + "Num. Personas: " + dtConsulta.Rows[0][34].ToString() + Environment.NewLine;
        //        }

        //        else
        //        {
        //            sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
        //        }

        //        return sTexto;
        //    }

        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

        //public string seccionDetalleCocina(DataTable dtConsulta)
        //{
        //    try
        //    {
        //        sTexto = "";
        //        sTexto = sTexto + ("Ticket: " + dtConsulta.Rows[0][46].ToString()).PadRight(21, ' ') + dtConsulta.Rows[0][32].ToString().Substring(0, 19) + Environment.NewLine + Environment.NewLine;
        //        sTexto = sTexto + "Cantidad " + "Descripcion" + Environment.NewLine;
        //        sTexto = sTexto + "-------- " + "".PadRight(31, '-') + Environment.NewLine;

        //        //INSTRUCCION PARA LLENAR LOS PRODUCTOS
        //        for (int i = 0; i < dtConsulta.Rows.Count; i++)
        //        {
        //            sCantidad = dtConsulta.Rows[i][4].ToString();
        //            sDescripcion = dtConsulta.Rows[i][2].ToString();

        //            sTexto = sTexto + sCantidad.PadLeft(5, ' ') + "".PadRight(4, ' ');

        //            if (sDescripcion.Length > 31)
        //            {
        //                sTexto = sTexto + sDescripcion.Substring(0, 31).PadRight(31, ' ') + Environment.NewLine;
        //                sTexto = sTexto + "".PadLeft(9, ' ') + sDescripcion.Substring(31).PadRight((sDescripcion.Substring(32).Length + 5), ' ');
        //            }

        //            else
        //            {
        //                sTexto = sTexto + sDescripcion;
        //            }

        //            sTexto = sTexto + Environment.NewLine;

        //            //FUNCIONES PARA SABER SI HAY OBSERVACIONES DEL ITEM
        //            //sSql = "select count(*) cuenta from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ToString()) + " and estado = 'A'";
        //            sSql = "select detalle from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + " and estado = 'A' order by id_det_pedido";
        //            dtPreferencia = new DataTable();
        //            dtPreferencia.Clear();

        //            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPreferencia, sSql);

        //            if (bRespuesta == true)
        //            {
        //                if (dtPreferencia.Rows.Count > 0)
        //                {
        //                    for (int j = 0; j < dtPreferencia.Rows.Count; j++)
        //                    {
        //                        sTexto = sTexto + "  >>".PadRight(4, ' ') + dtPreferencia.Rows[j][0].ToString().PadRight(16, ' ') + Environment.NewLine;
        //                    }
        //                }
        //            }

        //            else
        //            {
        //                ok.LblMensaje.Text = "Ocurrió un problema al consultar las observaciones del item '" + dtConsulta.Rows[i][2].ToString() + ".";
        //                ok.ShowInTaskbar = false;
        //                ok.ShowDialog();
        //            }
        //        }

        //        sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

        //        return sTexto;
        //    }

        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}



        public string encabezadoReporteCocina(DataTable dtConsulta)
        {
            try
            {
                sTexto = "";
                sTexto = sTexto + "." + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "".PadRight(33, '-') + Environment.NewLine;
                sTexto = sTexto + "C O C I N A".PadRight(17, ' ') + "ORDEN DE TRABAJO" + Environment.NewLine;
                sTexto = sTexto + "".PadRight(33, '-') + Environment.NewLine;
                sTexto = sTexto + dtConsulta.Rows[0][32].ToString().Substring(0, 19).PadLeft(33, ' ') + Environment.NewLine;
                sTexto = sTexto + "ORDEN: ".PadRight(8, ' ') + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                {
                    sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                    sTexto = sTexto + "Num. Personas: " + dtConsulta.Rows[0][34].ToString();
                }

                else
                {
                    sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString();
                }

                return sTexto;
            }

            catch (Exception ex)
            {
                return "";
            }
        }


        public string encabezadoReporteCocinaLlevar(DataTable dtConsulta)
        {
            try
            {
                sTexto = "";
                sTexto = sTexto + "." + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "".PadRight(33, '-') + Environment.NewLine;
                sTexto = sTexto + "C O C I N A".PadRight(17, ' ') + "ORDEN DE TRABAJO" + Environment.NewLine;
                sTexto = sTexto + "".PadRight(33, '-') + Environment.NewLine;
                sTexto = sTexto + dtConsulta.Rows[0][32].ToString().Substring(0, 19).PadLeft(33, ' ') + Environment.NewLine;
                sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString();

                return sTexto;
            }

            catch (Exception ex)
            {
                return "";
            }
        }




        public string seccionDetalleCocina(DataTable dtConsulta)
        {
            try
            {
                sTexto = "";
                sTexto = sTexto + ("Ticket: " + dtConsulta.Rows[0][46].ToString()).PadRight(14, ' ') + dtConsulta.Rows[0][32].ToString().Substring(0, 19) + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "Cantidad " + "Descripcion" + Environment.NewLine;
                sTexto = sTexto + "".PadRight(33, '-') + Environment.NewLine;

                //INSTRUCCION PARA LLENAR LOS PRODUCTOS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sCantidad = dtConsulta.Rows[i][4].ToString();
                    sDescripcion = dtConsulta.Rows[i][2].ToString();

                    sTexto = sTexto + sCantidad.PadLeft(5, ' ') + "".PadRight(4, ' ');

                    if (sDescripcion.Length > 24)
                    {
                        sTexto = sTexto + sDescripcion.Substring(0, 24).PadRight(24, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(9, ' ') + sDescripcion.Substring(24).PadRight((sDescripcion.Substring(25).Length + 5), ' ');
                    }

                    else
                    {
                        sTexto = sTexto + sDescripcion;
                    }

                    sTexto = sTexto + Environment.NewLine;

                    //FUNCIONES PARA SABER SI HAY OBSERVACIONES DEL ITEM
                    //sSql = "select count(*) cuenta from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ToString()) + " and estado = 'A'";
                    sSql = "select detalle from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + " and estado = 'A' order by id_det_pedido";
                    dtPreferencia = new DataTable();
                    dtPreferencia.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPreferencia, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtPreferencia.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtPreferencia.Rows.Count; j++)
                            {
                                sTexto = sTexto + "  >>".PadRight(4, ' ') + dtPreferencia.Rows[j][0].ToString().PadRight(16, ' ') + Environment.NewLine;
                            }
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al consultar las observaciones del item '" + dtConsulta.Rows[i][2].ToString() + ".";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                sTexto = sTexto + "".PadRight(33, '=') + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

                return sTexto;
            }

            catch (Exception ex)
            {
                return "";
            }
        }



        //FUNCION QUE RECIBE LOS DATOS PARA CREAR EL REPORTE PARA LOS DIFERENTES DESTINOS
        public string llenarPrecuentaDestinos(DataTable dtConsulta, string sIdOrden)
        {
            try
            {
                sTexto = "";
                sTexto = sTexto + "." + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + dtConsulta.Rows[0][53].ToString().PadRight(24, ' ') + "ORDEN DE TRABAJO" + Environment.NewLine;
                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + (dtConsulta.Rows[0][32].ToString().Substring(0, 10) + " " + Convert.ToDateTime(dtConsulta.Rows[0][32].ToString()).ToString("HH:mm:ss")).PadLeft(40, ' ') + Environment.NewLine;
                sTexto = sTexto + "ORDEN: ".PadRight(8, ' ') + dtConsulta.Rows[0][40].ToString() + Environment.NewLine;
                sTexto = sTexto + "Mesero: " + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0][40].ToString() == "MESAS")
                {
                    sTexto = sTexto + ("Num. Personas: " + dtConsulta.Rows[0][34].ToString()).PadRight(20, ' ') + Environment.NewLine + Environment.NewLine;
                    sTexto = sTexto + dtConsulta.Rows[0][49].ToString() + " - " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine + Environment.NewLine; ;
                }

                else
                {
                    if (dtConsulta.Rows[0][40].ToString() != "PARA LLEVAR")
                    {
                        sTexto = sTexto + Environment.NewLine + dtConsulta.Rows[0][49].ToString() + Environment.NewLine + Environment.NewLine; ;
                    }
                    else
                    {
                        sTexto = sTexto + Environment.NewLine;
                    }
                }

                sTexto = sTexto + ("Ticket: " + dtConsulta.Rows[0][46].ToString()).PadRight(21, ' ') + dtConsulta.Rows[0][32].ToString().Substring(0, 19) + Environment.NewLine + Environment.NewLine;

                sTexto = sTexto + "Cantidad " + "Descripcion" + Environment.NewLine;
                sTexto = sTexto + "-------- " + "".PadRight(31, '-') + Environment.NewLine;


                sSql = "";
                sSql += "" + Environment.NewLine;
                sSql += "" + Environment.NewLine;
                sSql += "" + Environment.NewLine;


                //INSTRUCCION PARA LLENAR LOS PRODUCTOS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sCantidad = dtConsulta.Rows[i][4].ToString();
                    sDescripcion = dtConsulta.Rows[i][2].ToString();

                    sTexto = sTexto + sCantidad.PadLeft(5, ' ') + "".PadRight(4, ' ');

                    if (sDescripcion.Length > 31)
                    {
                        sTexto = sTexto + sDescripcion.Substring(0, 31).PadRight(31, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(9, ' ') + sDescripcion.Substring(31).PadRight((sDescripcion.Substring(32).Length + 5), ' ');
                    }

                    else
                    {
                        sTexto = sTexto + sDescripcion;
                    }

                    sTexto = sTexto + Environment.NewLine;

                    //FUNCIONES PARA SABER SI HAY OBSERVACIONES DEL ITEM
                    //sSql = "select count(*) cuenta from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ToString()) + " and estado = 'A'";
                    sSql = "select detalle from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + " and estado = 'A' order by id_det_pedido";
                    dtPreferencia = new DataTable();
                    dtPreferencia.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPreferencia, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtPreferencia.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtPreferencia.Rows.Count; j++)
                            {
                                sTexto = sTexto + " >>".PadRight(4, ' ') + dtPreferencia.Rows[j][0].ToString().PadRight(16, ' ') + Environment.NewLine;
                            }
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al consultar las observaciones del item '" + dtConsulta.Rows[i][2].ToString() + ".";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

                return sTexto;
            }

            catch (Exception)
            {
                return "";
            }
        }        
    }
}
