using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseNotaVentaFacturador
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
        double dbPrecioUnitarioIVA;
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
        double dbSumaSubtotalNeto;
        double dTotal;

        double subtotal;
        double dCantidad, dUnitario, dIva, dDescuento, dPorcentaje;

        int iCuentaLinea;
        int iPagaIva;

        public string llenarNota(DataTable dtConsulta_R, string sIdOrden, string sEstado)
        {
            try
            {
                this.dtConsulta = dtConsulta_R.Copy();

                //if (Program.iManejaNotaVenta == 1)
                //{
                //    if (actualizaIva(Convert.ToInt32(sIdOrden)) == false)
                //    {
                //        return "ERROR";
                //    }

                //    else
                //    {
                //        if (recuperarInformacion(Convert.ToInt32(sIdOrden)) == false)
                //        {
                //            return "ERROR";
                //        }
                //    }
                //}

                //else
                //{
                //    this.dtConsulta = dtConsulta_R.Copy();
                //}

                subtotal = 0;

                sNumeroOrden = dtConsulta.Rows[0][46].ToString();

                //FOR PARA OBTENER EL SUBTOTAL DE LA COMPRA
                for (int j = 0; j < dtConsulta.Rows.Count; j++)
                {
                    if ((dtConsulta.Rows[j][42].ToString() != "1") && (dtConsulta.Rows[j][43].ToString() != "1"))
                    {
                        dCantidad = Convert.ToDouble(dtConsulta.Rows[j][27].ToString());
                        dUnitario = Convert.ToDouble(dtConsulta.Rows[j][28].ToString());
                        dIva = Convert.ToDouble(dtConsulta.Rows[j][33].ToString());
                        dDescuento = Convert.ToDouble(dtConsulta.Rows[j][29].ToString());

                        subtotal = subtotal + (dCantidad * (dUnitario + dIva + dDescuento));
                    }
                }

                //NUMERO DE FACTURA O NOTA DE VENTA
                sSecuencial = dtConsulta.Rows[0][37].ToString().PadLeft(9, '0');

                sOrigen = dtConsulta.Rows[0][56].ToString();

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0][51].ToString()).ToString("dd/MM/yyyy");
                sHoraIngreso = Convert.ToDateTime(dtConsulta.Rows[0][51].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                sHoraSalida = Convert.ToDateTime(dtConsulta.Rows[0][52].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

                sTexto = "";
                sTexto = sTexto + "Num.T. : " + dtConsulta.Rows[0][62].ToString() + " Cja: 01 " + "Msro: " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0][63].ToString() == "1")
                {
                    sTexto = sTexto + "Fact   : " + dtConsulta.Rows[0][53].ToString() + "-" + dtConsulta.Rows[0][54].ToString() + "-" + sSecuencial + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + "N.E.   : " + dtConsulta.Rows[0][53].ToString() + "-" + dtConsulta.Rows[0][54].ToString() + "-" + sSecuencial + Environment.NewLine;
                }


                sTexto = sTexto + "Fecha  : " + sFecha + " Hora:" + sHoraIngreso.Substring(11, 5) + " - " + sHoraSalida.Substring(11, 5) + Environment.NewLine;

                if (sOrigen == "MESAS")
                {
                    sTexto = sTexto + dtConsulta.Rows[0][48].ToString().PadRight(8, ' ') + (" No. Personas: " + dtConsulta.Rows[0][55].ToString()) + Environment.NewLine;
                }


                if ((dtConsulta.Rows[0][17].ToString() + " " + dtConsulta.Rows[0][18].ToString()).Trim().Length <= 30)
                {
                    sTexto = sTexto + "Cliente: " + (dtConsulta.Rows[0][17].ToString() + " " + dtConsulta.Rows[0][18].ToString()).Trim() + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + "Cliente: " + (dtConsulta.Rows[0][17].ToString() + " " + dtConsulta.Rows[0][18].ToString()).Trim().Substring(0, 30) + Environment.NewLine;
                }


                if (dtConsulta.Rows[0][16].ToString() == "9999999999999")
                {
                    sTexto = sTexto + "RUC/CI : " + dtConsulta.Rows[0][16].ToString().PadRight(14, ' ') + Environment.NewLine + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + "RUC/CI : " + dtConsulta.Rows[0][16].ToString().PadRight(14, ' ') + "Tlf.: " + dtConsulta.Rows[0][4].ToString() + Environment.NewLine;
                    if (dtConsulta.Rows[0][3].ToString().Length <= 30)
                    {
                        sTexto = sTexto + "Direcc.: " + dtConsulta.Rows[0][3].ToString() + Environment.NewLine;
                    }
                    else
                    {
                        sTexto = sTexto + "Direcc.: " + dtConsulta.Rows[0][3].ToString().Substring(0, 30).PadRight(30, ' ') + Environment.NewLine;
                    }
                }

                sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;
                sTexto = sTexto + "CANT " + "DESCRIPCION".PadRight(22, ' ') + " V.UNI.  TOT." + Environment.NewLine;
                sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;

                //CALCULO DE VALORES
                //=============================================================
                dPorcentaje = Convert.ToDouble(dtConsulta.Rows[0][59].ToString()) / 100;

                cargarProductos2();

                sTexto = sTexto + "".PadLeft(40, '-');
                //dbTotal = subtotal;
                return sTexto;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al crear el reporte de la nota de venta.";
                ok.ShowDialog();
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
                    dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    //dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString()) - Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbIva = Convert.ToDouble(dtConsulta.Rows[i]["valor_iva"].ToString());
                    dbPrecioUnitarioIVA = dbPrecioUnitario + dbIva;

                    dbServicio = Convert.ToDouble(dtConsulta.Rows[i]["valor_otro"].ToString());

                    iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());

                    if (dbValorDescuento != 0)
                    {
                        dbSumaDescuento += (dbCantidad * dbValorDescuento);
                    }

                    if (iPagaIva == 0)
                    {
                        //dbSumaSubtotalSinIva += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                        //dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitario;
                        dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitarioIVA;
                    }

                    else
                    {
                        //dbSumaSubtotalConIva += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                        //dbSumaSubtotalConIva += dbCantidad * dbPrecioUnitario;
                        dbSumaSubtotalConIva += dbCantidad * dbPrecioUnitarioIVA;
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

                    //dbSumaPrecio = dbCantidad * dbPrecioUnitario;
                    dbSumaPrecio = dbCantidad * dbPrecioUnitarioIVA;


                    if (dbCantidad < 1)
                    {
                        sCantidadProducto = "1/2";
                    }

                    else
                    {
                        sCantidadProducto = dbCantidad.ToString();
                    }

                    if (iConsumoAlimentos == 0)
                    {
                        if (sNombreProducto.Length > 22)
                        {
                            //sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.Substring(0, 20).PadRight(22, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(6, ' ') + dbSumaPrecio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.Substring(0, 20).PadRight(22, ' ') + dbPrecioUnitarioIVA.ToString("N2").PadLeft(6, ' ') + dbSumaPrecio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                            sTexto += "".PadLeft(5, ' ') + sNombreProducto.Substring(20) + Environment.NewLine;
                        }

                        else
                        {
//                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.PadRight(22, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(6, ' ') + dbSumaPrecio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.PadRight(22, ' ') + dbPrecioUnitarioIVA.ToString("N2").PadLeft(6, ' ') + dbSumaPrecio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                        }
                    }
                }

                dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva - dbSumaDescuento;
                //dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva;
                //dbTotal = dbSumaSubtotalNeto + dbSumaIVA + dbSumaServicio;
                dbTotal = dbSumaSubtotalNeto + dbSumaServicio;


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
    }
}
