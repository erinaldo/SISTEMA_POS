using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseNotaVenta
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        DataTable dtConsulta;

        bool bRespuesta;

        string sSql;
        string sTexto;
        string sOrigen;
        string sFecha;
        string sHoraIngreso;
        string sHoraSalida;
        string sSecuencial;
        string sNumeroOrden;
        string sNombreProducto;
        string sCantidadProducto;

        double dbCantidad;
        double dbPrecioUnitario;
        double dbPrecioUnitarioVer;
        double dbDescuento;
        double dbIva;
        public Double dbTotal;

        double dbSumaSubtotalConIva;
        double dbSumaSubtotalSinIva;
        double dbSumaDescuento;
        double dbSumaIVA;
        double dbSumaServicio;
        double dbPorcentajeDescuento;
        double dbValorDescuento;
        double dbServicio;
        double dbPorcentajeServicio;
        double dbSumaPrecio;
        double dbSumaPrecioVer;
        double dbSumaSubtotalNeto;
        double dTotal;

        double subtotal;
        double dCantidad, dUnitario, dIva, dDescuento, dPorcentaje;

        int iCuentaLinea;
        int iPagaIva;

        public string llenarNota(int iIdFactura_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "";
                }

                subtotal = 0;
                sNumeroOrden = dtConsulta.Rows[0]["numero_pedido"].ToString();

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (dtConsulta.Rows[i]["cortesia"].ToString() != "1" && dtConsulta.Rows[i]["cancelacion"].ToString() != "1")
                    {
                        dCantidad = Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString());
                        dUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString());
                        dIva = Convert.ToDouble(dtConsulta.Rows[i]["valor_iva"].ToString());
                        dDescuento = Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());
                        subtotal += dCantidad * (dUnitario + dIva + dDescuento);
                    }
                }

                sSecuencial = dtConsulta.Rows[0]["numero_factura"].ToString().PadLeft(9, '0');

                sOrigen = dtConsulta.Rows[0]["descripcion_origen_orden"].ToString();

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("dd/MM/yyyy");
                sHoraIngreso = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                sHoraSalida = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre_orden"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

                sTexto = "";
                sTexto += "Num.T. : " + dtConsulta.Rows[0]["numero_pedido"].ToString() + " Cja: 01 Msro: " + dtConsulta.Rows[0]["mesero"].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0]["idtipocomprobante"].ToString() == "1")
                {
                    sTexto += "Fact   : " + dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["Punto_emision"].ToString() + "-" + sSecuencial + Environment.NewLine;
                }

                else
                {
                    sTexto += "N.E.   : " + dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["Punto_emision"].ToString() + "-" + sSecuencial + Environment.NewLine;
                }

                sTexto += "Fecha  : " + sFecha + " Hora:" + sHoraIngreso.Substring(11, 5) + " - " + sHoraSalida.Substring(11, 5) + Environment.NewLine;

                if (sOrigen == "MESAS")
                {
                    sTexto += dtConsulta.Rows[0]["mesa"].ToString().PadRight(8, ' ') + " No. Personas: " + dtConsulta.Rows[0]["numero_personas"].ToString() + Environment.NewLine;
                }

                if ((dtConsulta.Rows[0]["nombres"].ToString() + " " + dtConsulta.Rows[0]["apellidos"].ToString()).Trim().Length > 30)
                {
                    sTexto += "Cliente: " + (dtConsulta.Rows[0]["nombres"].ToString() + " " + dtConsulta.Rows[0]["apellidos"].ToString()).Trim().Substring(0, 30) + Environment.NewLine;
                }

                else
                {
                    sTexto += "Cliente: " + (dtConsulta.Rows[0]["nombres"].ToString() + " " + dtConsulta.Rows[0]["apellidos"].ToString()).Trim() + Environment.NewLine;
                }
                
                if (dtConsulta.Rows[0]["identificacion"].ToString() == "9999999999999")
                {
                    sTexto += "RUC/CI : " + dtConsulta.Rows[0]["identificacion"].ToString().PadRight(14, ' ') + Environment.NewLine + Environment.NewLine;
                }

                else
                {
                    sTexto += "RUC/CI : " + dtConsulta.Rows[0]["identificacion"].ToString().PadRight(14, ' ') + "Tlf.: " + dtConsulta.Rows[0]["telefono_factura"].ToString() + Environment.NewLine;

                    if (dtConsulta.Rows[0]["direccion_factura"].ToString().Length > 30)
                    {
                        sTexto += "Direcc.: " + dtConsulta.Rows[0]["direccion_factura"].ToString().Substring(0, 30).PadRight(30, ' ') + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += "Direcc.: " + dtConsulta.Rows[0]["direccion_factura"].ToString() + Environment.NewLine;
                    }
                }

                sTexto += "".PadRight(40, '=') + Environment.NewLine;
                sTexto += "CANT " + "DESCRIPCION".PadRight(22, ' ') + " V.UNI.  TOT." + Environment.NewLine;
                sTexto += "".PadRight(40, '=') + Environment.NewLine;

                dPorcentaje = Convert.ToDouble(dtConsulta.Rows[0]["Porcentaje_Dscto"].ToString()) / 100;

                cargarProductos2();

                sTexto += "".PadLeft(40, '-');
                return sTexto;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }
        //FUNCION PARA CARGAR LOS PRODUCTOS
        private void cargarProductos2()
        {
            try
            {
                int iConsumoAlimentos = Convert.ToInt32(dtConsulta.Rows[0]["consumo_alimentos"].ToString());

                dbSumaSubtotalConIva = 0;
                dbSumaSubtotalSinIva = 0;
                dbSumaDescuento = 0;
                dbSumaIVA = 0;
                dbSumaServicio = 0;

                dbPorcentajeDescuento = Convert.ToDouble(dtConsulta.Rows[0]["porcentaje_dscto"].ToString());

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (dtConsulta.Rows[i]["comentario"].ToString() == "")
                    {
                        sNombreProducto = dtConsulta.Rows[i]["Nombre"].ToString();
                    }
                    else
                    {
                        sNombreProducto = dtConsulta.Rows[i]["comentario"].ToString();
                    }

                    dbCantidad = Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString());
                    //dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString());
                    //dbValorDescuento = Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString()) - Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbPrecioUnitarioVer = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString()) - Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString()) + Convert.ToDouble(dtConsulta.Rows[i]["valor_iva"].ToString()) + Convert.ToDouble(dtConsulta.Rows[i]["valor_otro"].ToString());
                    dbIva = Convert.ToDouble(dtConsulta.Rows[i]["valor_iva"].ToString());
                    dbServicio = Convert.ToDouble(dtConsulta.Rows[i]["valor_otro"].ToString());

                    iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());

                    if (dbValorDescuento != 0)
                    {
                        dbSumaDescuento += (dbCantidad * dbValorDescuento);
                    }

                    if (iPagaIva == 0)
                    {
                        //dbSumaSubtotalSinIva += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                        dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitario;
                    }

                    else
                    {
                        //dbSumaSubtotalConIva += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                        dbSumaSubtotalConIva += dbCantidad * dbPrecioUnitario;
                        dbSumaIVA += dbCantidad * dbIva;
                    }

                    if (dbPorcentajeServicio != 0)
                    {
                        if (dbCantidad < 1)
                        {
                            dbSumaServicio += dbServicio;
                        }

                        else
                        {
                            dbSumaServicio += dbCantidad * dbServicio;
                        }
                    }

                    dbSumaPrecio = dbCantidad * dbPrecioUnitario;
                    dbSumaPrecioVer = dbCantidad * dbPrecioUnitarioVer;

                    if (dbCantidad < 1)
                    {
                        sCantidadProducto = "1/2";
                    }

                    else
                    {
                        sCantidadProducto = dbCantidad.ToString("N0");
                    }

                    if (iConsumoAlimentos == 0)
                    {
                        if (sNombreProducto.Length > 22)
                        {
                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.Substring(0, 20).PadRight(22, ' ') + dbPrecioUnitarioVer.ToString("N2").PadLeft(6, ' ') + dbSumaPrecioVer.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                            sTexto += "".PadLeft(5, ' ') + sNombreProducto.Substring(20) + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.PadRight(22, ' ') + dbPrecioUnitarioVer.ToString("N2").PadLeft(6, ' ') + dbSumaPrecioVer.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                        }
                    }
                }

                //dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva - dbSumaDescuento;
                dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva;
                dbTotal = dbSumaSubtotalNeto + dbSumaIVA + dbSumaServicio;


                if (iConsumoAlimentos == 1)
                {
                    //sTexto += "  1  " + "CONSUMO ALIMENTOS".PadRight(21, ' ') + dValores[0].ToString("N2").PadLeft(7, ' ') + dValores[0].ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                    sTexto += "  1  " + "CONSUMO ALIMENTOS".PadRight(21, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(7, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                }

                sTexto += Environment.NewLine + Environment.NewLine;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //ACTUALIZAR EL IVA DEL PEDIDO
        private bool actualizaIva(int iIdPedido_P)
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                sSql += "valor_iva = 0," + Environment.NewLine;
                sSql += "valor_otro = 0" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precuenta.";
                ok.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA RECUPERAR LA COMENDA
        private bool recuperarInformacion(int iIdPedido_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido_P + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precuenta.";
                ok.ShowDialog();
                return false;
            }
        }
    }
}
