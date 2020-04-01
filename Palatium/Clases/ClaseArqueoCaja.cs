using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseArqueoCaja
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sFecha;
        string sTexto;
        double dbTotalEntregado = 0;
        double dbTotalPendiente = 0;

        //Función para llenar el informa de arqueo de caja
        public string llenarInforme(string sFecha)
        {
            try
            {
                this.sFecha = sFecha;
                sTexto = "";
                sTexto += Program.local.PadLeft(30, ' ') + Environment.NewLine;
                sTexto += "=".PadRight(40, '=') + Environment.NewLine;
                sTexto += "ARQUEO DE CAJA".PadLeft(27, ' ') + Environment.NewLine;
                sTexto += "=".PadRight(40, '=') + Environment.NewLine;
                sTexto += "FECHA:".PadRight(8, ' ') + DateTime.Now.ToString("dd/MM/yyyy");
                sTexto += "DESDE" + Environment.NewLine;
                sTexto += "CAJERO".PadRight(8, ' ') + Program.sNombreCajero + Environment.NewLine + Environment.NewLine;
                sTexto += "TOTAL VENDIDO:".PadRight(30, ' ') + (calcularTotalPago(1) + calcularTotalPagoTarjetas()).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "COBRADO EFECTIVO:".PadRight(30, ' ') + calcularTotalPago(1).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "COBRADO TARJETAS:".PadRight(30, ' ') + calcularTotalPagoTarjetas().ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                
                llenarDesgloseTarjetas();
                
                sTexto += "COBRADO CHEQUES:".PadRight(30, ' ') + calcularTotalPago(2).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "TOTAL COBRADO".PadRight(30, ' ') + (calcularTotalPago(1) + calcularTotalPagoTarjetas()).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "ENTRADAS MANUAL:".PadRight(30, ' ') + sumarEntradasManuales().ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += "SALIDAS MANUAL:".PadRight(30, ' ') + sumarSalidasManuales().ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += " ".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                sTexto += "TOTAL EFECTIVO:".PadRight(30, ' ') + ((calcularTotalPago(1) + sumarEntradasManuales()) - sumarSalidasManuales()).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += " ".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                
                double dbTotalCobrado = (calcularTotalPago(1) + calcularTotalPagoTarjetas());
                
                sTexto += "SALDO EN CAJA:".PadRight(30, ' ') + ((dbTotalCobrado + sumarEntradasManuales()) - sumarSalidasManuales()).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += " ".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                sTexto += "TOTAL ENTREGADO:".PadRight(30, ' ') + dbTotalEntregado.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += " ".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine;
                sTexto += "DIFERENCIA:".PadRight(30, ' ') + (dbTotalEntregado - ((dbTotalCobrado + sumarEntradasManuales()) - sumarSalidasManuales())).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sTexto += " ".PadRight(25, ' ') + "-".PadRight(15, '-') + Environment.NewLine + Environment.NewLine;
                sTexto += "TOTAL PENDIENTE:".PadRight(30, ' ') + dbTotalPendiente.ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;

                #region Llenar Cortesías
                sSql = "SELECT  dbo.cv401_nombre_productos.nombre, dbo.pos_cortesia.motivo_cortesia, dbo.cv403_det_pedidos.precio_unitario, " +
                    " dbo.cv403_det_pedidos.cantidad, dbo.pos_origen_orden.descripcion " +
                    "FROM dbo.cv403_det_pedidos INNER JOIN  dbo.cv403_cab_pedidos ON dbo.cv403_det_pedidos.id_pedido = dbo.cv403_cab_pedidos.id_pedido INNER JOIN " +
                    " dbo.pos_origen_orden ON dbo.cv403_cab_pedidos.id_pos_origen_orden = dbo.pos_origen_orden.id_pos_origen_orden INNER JOIN " +
                    " dbo.cv401_nombre_productos ON dbo.cv403_det_pedidos.id_producto = dbo.cv401_nombre_productos.id_producto and dbo.cv401_nombre_productos.estado = 'A' INNER JOIN " +
                    " dbo.pos_cortesia ON (dbo.cv403_det_pedidos.id_det_pedido = dbo.pos_cortesia.id_det_pedido and  dbo.pos_cortesia.estado='A') " +
                    "where dbo.cv403_cab_pedidos.fecha_pedido = '" + sFecha + "' and dbo.cv403_det_pedidos.estado = 'A' and dbo.cv403_cab_pedidos.id_pos_jornada =  " + Program.iJORNADA;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                double total = 0;
                double dbTotal = 0;
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            total = total + Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                        }
                        sTexto += "TOTAL CORTESIA: ".PadRight(30, ' ') + (total + (total * (Program.iva + Program.servicio))).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                    }
                    else
                    {
                        sTexto += "TOTAL CORTESIA: ".PadRight(30, ' ') + "0.00".PadLeft(10, ' ') + Environment.NewLine;
                    }
                }

                #endregion

                sTexto += Environment.NewLine;
                sTexto += "TOTAL I.V.A:".PadRight(30, ' ') + (dbTotalCobrado * Program.iva).ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;

                sTexto += "PERSONAS ATENDIDAS:".PadRight(30, ' ') + calcularTotalPersonas(1).ToString().PadLeft(10, ' ') + Environment.NewLine;

                return sTexto;
            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargar el reporte";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                return "";
            }
        }

        //Función para calcular el total
        private double calcularTotalPago(int iFormaPago)
        {
            try
            {
                sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "select FP.descripcion,sum(FP.valor) valor  " +
                                "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP " +
                                 "where CP.fecha_pedido ='" + sFecha + "' " +
                                "and CP.id_pos_jornada = " + Program.iJORNADA + " " +
                                " and CP.id_pedido = NP.id_pedido " +
                                "and CP.id_pedido = FP.id_pedido " +
                                "and FP.id_pos_tipo_forma_cobro = " + iFormaPago +
                                "group by FP.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0].ItemArray[1].ToString());
                    }
                    else
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Función para calcular el total de tarjetas
        private double calcularTotalPagoTarjetas()
        {
            try
            {
                double sumaTarjetas = 0;
                sFecha = DateTime.Now.ToString("yyyy/MM/dd");

                sSql = "select FP.descripcion,sum(FP.valor) valor  from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, " +
                        "pos_vw_pedido_forma_pago FP " +
                        "where CP.fecha_pedido ='" + sFecha + "' and CP.id_pos_jornada = " + Program.iJORNADA + " and CP.id_pedido = NP.id_pedido " +
                         "and CP.id_pedido = FP.id_pedido  " +
                        "and FP.codigo in('TC', 'TD')  " +
                        "group by FP.descripcion";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sumaTarjetas += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                        }
                        //MessageBox.Show(sumaTarjetas.ToString());
                        return sumaTarjetas;
                    }
                    else
                        return 0.00;
                }
                else
                    return 0.00;

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al calcular el total de tarjetas";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                return 0.00;
            }
        }

        //Función para mostrar las tarjetas de crédito
        private void llenarDesgloseTarjetas()
        {
            try
            {
                sSql = "select FP.descripcion,sum(FP.valor) valor, count (CP.id_pedido) Numero  " +
                        "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP,  pos_vw_pedido_forma_pago FP  " +
                        " where CP.fecha_pedido = '2018-06-14' " +
                        "and CP.id_pedido = NP.id_pedido " +
                        "and CP.id_pedido = FP.id_pedido and CP.id_pos_origen_orden in (1,2,3) " +
                        "and CP.estado_orden= 'Pagada' " +
                        "and FP.codigo in ('TC','TD')  " +
                        "and CP.id_pos_jornada = 1 " +
                        "group by FP.descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreTarjeta = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbValorTarjeta = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                            
                            sTexto += " ".PadRight(6, ' ') + sNombreTarjeta.PadRight(21, ' ') + dbValorTarjeta.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                        }
                    }
                    else
                    {
                        sTexto += "No hay datos para ser mostrados";
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "Error al mostrar las tarjetas de crédito";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }


            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al mostrar las tarjetas de crédito";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //Función para calcular el total de personas que ocupan las mesas
        private double calcularTotalPersonas(int iIdPosOrigenOrden)
        {
            try
            {

                sSql = "select isnull(sum(CP.numero_personas),0) numero " +
                            "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_origen_orden ORI " +
                            "where CP.fecha_pedido = '" + sFecha + "' and CP.id_pos_jornada = " + Program.iJORNADA +
                            " and CP.id_pedido = NP.id_pedido " +
                            "and ORI.id_pos_origen_orden = CP.id_pos_origen_orden " +
                            "and ORI.id_pos_origen_orden = " + iIdPosOrigenOrden;

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                    else
                        return 0;
                }
                else
                    return 0;

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al calcular el numero de personas";
                ok.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA OBTENER EL VALOR DE LA SALIDAS MANUALES
        private double sumarSalidasManuales()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select isnull(sum(valor), 0) suma from pos_movimiento_caja  " + Environment.NewLine;
                sSql = sSql + "where estado = 'A' " + Environment.NewLine;
                sSql = sSql + "and tipo_movimiento = 0 " + Environment.NewLine;
                sSql = sSql + "and id_documento_pago is null" + Environment.NewLine;
                sSql = sSql + "and fecha = '" + sFecha + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA OBTENER EL VALOR DE LA SALIDAS ENTRADAS
        private double sumarEntradasManuales()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select isnull(sum(valor), 0) suma from pos_movimiento_caja  " + Environment.NewLine;
                sSql = sSql + "where estado = 'A' " + Environment.NewLine;
                sSql = sSql + "and tipo_movimiento = 1 " + Environment.NewLine;
                sSql = sSql + "and id_documento_pago is null" + Environment.NewLine;
                sSql = sSql + "and fecha = '" + sFecha + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }
    }
}
