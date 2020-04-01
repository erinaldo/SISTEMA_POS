using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseReporteVentasLista
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        DataTable dtConsulta;
        DataTable dtPagosDesglose;
        DataTable dtTipoProducto;
        
        bool bRespuesta = false;

        string sSql;        
        double dSuma;
        string sTexto;
        string sFecha;
        string sFechaApertura;
        string sHoraApertura;
        string sFechaCierre;
        string sHoraCierre;

        string sNombreProducto;
        double dCantidad;
        double dTotal;

        double dbTotalOrdenes;
        double dbTotalTarjetas;
        double dbTotalEfectivo;
        double dbTotalCheques;
        double dbAuxiliar;
        double dTotalDescuentos;
        double dbPorcentajeIva;
        double dbPorcentajeServicio;

        //------------------------------------------------------------------
        //FUNCION PARAR REORGANIZAR LOS ITEMS PRODUCTOS
        private int consultarTipoProductos()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtTipoProducto = new DataTable();
                dtTipoProducto.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtTipoProducto, sSql);

                if (bRespuesta == true)
                {
                    if (dtTipoProducto.Rows.Count > 0)
                    {
                        return 1;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch(Exception ex)
            {
                //catchMensaje.LblMensaje.Text = ex.Message;
                //catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA CONSULTAR FECHA Y HORA
        private bool consultarFechaHora()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select fecha_apertura, hora_apertura, isnull(fecha_cierre, fecha_apertura) fecha_apertura," + Environment.NewLine;
                sSql = sSql + "isnull(hora_cierre, '') hora_cierre, porcentaje_iva, porcentaje_servicio" + Environment.NewLine;
                sSql = sSql + "from pos_cierre_cajero" + Environment.NewLine;
                sSql = sSql + "where fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql = sSql + "and id_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sFechaApertura = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[0].ToString()).ToString("dd/MM/yyyy");
                        sHoraApertura = dtConsulta.Rows[0].ItemArray[1].ToString();
                        sFechaCierre = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[2].ToString()).ToString("dd/MM/yyyy");

                        if (dtConsulta.Rows[0].ItemArray[3].ToString() == "")
                        {
                            sHoraCierre = DateTime.Now.ToString("HH:mm:dd");
                        }

                        else
                        {
                            sHoraCierre = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[3].ToString()).ToString("HH:mm:ss");
                        }

                        dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0]["porcentaje_iva"]);
                        dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0]["porcentaje_servicio"]);

                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        public string llenarReporteVentas(string sFecha)
        {
            try
            {
                this.sFecha = sFecha;

                if (consultarFechaHora() == false)
                {
                    sTexto = "";
                }

                else
                {
                    dSuma = 0;

                    sTexto = "";
                    sTexto = sTexto + "*".PadRight(40, '*') + Environment.NewLine;
                    sTexto = sTexto + Program.local.PadLeft(30, ' ') + Environment.NewLine;
                    sTexto = sTexto + "*".PadRight(40, '*') + Environment.NewLine;
                    sTexto = sTexto + "REPORTE DE LO VENDIDO".PadLeft(30, ' ') + Environment.NewLine;
                    sTexto = sTexto + "FECHA: " + sFecha + Environment.NewLine;
                    sTexto = sTexto + "DESDE: " + sHoraApertura + Environment.NewLine;
                    sTexto = sTexto + "HASTA: " + sHoraCierre + Environment.NewLine;

                    if (consultarTipoProductos() == 1)
                    {
                        sTexto = sTexto + llenarPorDetalle();
                    }

                    else
                    {
                        sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(5, ' ') + "DESCRIPCION".PadRight(20, ' ') + "CANT.".PadLeft(5, ' ') + "TOTAL".PadLeft(10, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;

                        sSql = "";
                        sSql = sSql + "select NOM.nombre, sum(DET.cantidad) CANTIDAD," + Environment.NewLine;
                        sSql = sSql + "sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) TOTAL" + Environment.NewLine;
                        sSql = sSql + "from cv403_det_pedidos DET inner join" + Environment.NewLine;
                        sSql = sSql + "cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto" + Environment.NewLine;
                        sSql = sSql + "and DET.estado = 'A'" + Environment.NewLine;
                        sSql = sSql + "and NOM.estado = 'A' inner join" + Environment.NewLine;
                        sSql = sSql + "cv403_cab_pedidos CAB on CAB.id_pedido = DET.id_pedido" + Environment.NewLine;
                        sSql = sSql + "and CAB.estado = 'A'" + Environment.NewLine;
                        sSql = sSql + "where CAB.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                        sSql = sSql + "and CAB.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                        sSql = sSql + "and CAB.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                        sSql = sSql + "and CAB.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                        sSql = sSql + "group by NOM.nombre" + Environment.NewLine;
                        sSql = sSql + "order by sum(DET.cantidad)";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                                {
                                    sNombreProducto = dtConsulta.Rows[i].ItemArray[0].ToString();
                                    dCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                                    dTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                                    dSuma = dSuma + dTotal;

                                    if (sNombreProducto.Length > 25)
                                    {
                                        sNombreProducto = sNombreProducto.Substring(0, 25);
                                    }

                                    sTexto = sTexto + sNombreProducto.PadRight(25, ' ') + dCantidad.ToString("N0").PadLeft(5, ' ') + dTotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                                }

                                sTexto = sTexto + "*".PadRight(40, '*') + Environment.NewLine + Environment.NewLine;
                                sTexto = sTexto + "TOTALES".PadRight(30, ' ') + dSuma.ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                                sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                                sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";


                            }

                            else
                            {
                                sTexto = "";
                            }
                        }

                        else
                        {
                            sTexto = "";
                        }
                    }
                }

                return sTexto;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }


        //FUNCION PARA CLASIFICAR LOS ITEMS
        private string llenarPorDetalle()
        {
            string sContinuar = "";

            for (int i = 0; i < dtTipoProducto.Rows.Count; i++)
            {
                sSql = "";
                sSql += "select NOM.nombre, sum(DET.cantidad) CANTIDAD," + Environment.NewLine;
                sSql += "sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) TOTAL" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DET inner join" + Environment.NewLine;
                sSql += "cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto" + Environment.NewLine;
                sSql += "and NOM.estado = 'A' inner join" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CAB on CAB.id_pedido = DET.id_pedido" + Environment.NewLine;
                sSql += "and CAB.estado = 'A'" + Environment.NewLine;
                sSql += "and DET.estado = 'A' inner join" + Environment.NewLine;
                sSql += "cv401_productos PROD on NOM.id_producto = PROD.id_producto" + Environment.NewLine;
                sSql += "and PROD.estado = 'A'" + Environment.NewLine;
                sSql += "where CAB.fecha_pedido = '" + sFecha + "'" + Environment.NewLine;
                sSql += "and CAB.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and CAB.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CAB.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql += "and PROD.id_pos_tipo_producto = " + Convert.ToInt32(dtTipoProducto.Rows[i].ItemArray[0].ToString()) + Environment.NewLine;
                sSql += "group by NOM.nombre" + Environment.NewLine;
                sSql += "order by sum(DET.cantidad)";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sContinuar += "".PadRight(40, '=') + Environment.NewLine;
                        sContinuar += "TIPO DE PRODUCTO: " + dtTipoProducto.Rows[i].ItemArray[1].ToString().ToUpper().Trim() + Environment.NewLine;
                        sContinuar += "".PadRight(40, '=') + Environment.NewLine;
                        sTexto = sTexto + " ".PadRight(5, ' ') + "DESCRIPCION".PadRight(30, ' ') + "CANT.".PadLeft(5, ' ') + Environment.NewLine;
                        sTexto = sTexto + "*".PadRight(40, '=') + Environment.NewLine;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j].ItemArray[0].ToString();
                            dCantidad = Convert.ToDouble(dtConsulta.Rows[j].ItemArray[1].ToString());
                            dTotal = Convert.ToDouble(dtConsulta.Rows[j].ItemArray[2].ToString());
                            dSuma = dSuma + dTotal;

                            if (sNombreProducto.Length > 25)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 25);
                            }

                            sContinuar = sContinuar + sNombreProducto.PadRight(35, '_') + dCantidad.ToString("N0").PadLeft(5, ' ') + Environment.NewLine;
                        }
                    }
                }

                sContinuar += Environment.NewLine;
            }

            sContinuar = sContinuar + "*".PadRight(40, '*') + Environment.NewLine + Environment.NewLine;
            sContinuar = sContinuar + "NOTA: EL REPORTE INCLUYE LOS PRODUCTOS" + Environment.NewLine;
            sContinuar = sContinuar + "DE CUENTAS POR COBRAR Y CORTESÍAS." + Environment.NewLine;
            sContinuar = sContinuar + Environment.NewLine + Environment.NewLine;

            sContinuar = sContinuar + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

            return sContinuar;

        }
    }
}
