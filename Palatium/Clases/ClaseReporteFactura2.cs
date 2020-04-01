using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseReporteFactura2
    {
        bool bRespuesta = false;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        Clases.ClaseManejoCaracteres caracteres = new ClaseManejoCaracteres();

        DataTable dtConsulta;
        DataTable dtPagosClase;
        string sFecha, sHoraIngreso, sHoraSalida;

        int iAnchoDePrecuenta = 30;
        int iAnchoDeDescripcion = 20;
        int iAnchoDePrecio = 8;
        int iPagaIva;

        string sTexto;
        string sSecuencial;
        string sSql;
        string sSeccionMesa;
        string sDireccionCliente;

        int iCuentaLinea;
        Double dPorcentaje;
        Double subtotal = 0;
        Double iva = 0;
        Double servicio = 0;
        Double descuento = 0;
        Double nuevovalor;
        public Double dTotal;

        double dbPorcentajeIva;
        double dbPorcentajeServicio;
        double dbPorcentajeDescuento;
        double dbCantidad;
        double dbPrecioUnitario;
        double dbSumaPrecio;
        double dbPrecioTotal;
        double dbDescuento;
        double dbIva;
        double dbServicio;

        double dbSumaSubtotalConIva;
        double dbSumaSubtotalSinIva;
        double dbSumaDescuento;
        double dbSumaIVA;
        double dbSumaServicio;
        double dbSumaSubtotalNeto;
        double dbValorDescuento;

        //VARIABLES PARA DESPLEGAR INFORMACION
        string sNombreProducto;
        string sCantidadProducto;

        string[] sFactura = { "", 
                              "SubT.  0%:", 
                              "",
                              "Subtotal :",
                               "",
                               ""
                            };

        double[] dValores = new double[6];


        private void completarFactura(int op)
        {
            if (op == 1)
            {
                return;
            }

            else
            {
                Double dValor;
                int iBandera;

                iCuentaLinea = dtPagosClase.Rows.Count;

                if (dtPagosClase.Rows.Count >= sFactura.Length)
                {
                    iBandera = 1;
                }

                else
                {
                    iBandera = 0;
                }


                for (int i = 0; i < sFactura.Length - 1; i++)
                {
                    if (iCuentaLinea >= i + 1)
                    {
                        if (dtPagosClase.Rows[i][0].ToString().Length <= 10)
                        {
                            sTexto += dtPagosClase.Rows[i][0].ToString().PadRight(10, ' ');
                        }

                        else
                        {
                            sTexto += dtPagosClase.Rows[i][0].ToString().Substring(0, 10);
                        }

                        dValor = Convert.ToDouble(dtPagosClase.Rows[i][1].ToString());
                        sTexto += ":" + dValor.ToString("N2").PadLeft(7, ' ') + sFactura[i].PadLeft(14, ' ') + dValores[i].ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += "".PadLeft(22, ' ') + sFactura[i].PadLeft(10, ' ') + dValores[i].ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                    }
                }

                if (iBandera == 1)
                {
                    if (dtPagosClase.Rows[4][0].ToString().Length <= 10)
                    {
                        sTexto += dtPagosClase.Rows[4][0].ToString().PadRight(10, ' ');
                    }

                    else
                    {
                        sTexto += dtPagosClase.Rows[4][0].ToString().Substring(0, 10);
                    }

                    dValor = Convert.ToDouble(dtPagosClase.Rows[5][1].ToString());
                    sTexto += ":" + dValor.ToString("N2").PadLeft(7, ' ') + sFactura[4].PadLeft(14, ' ') + dValores[4].ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                    
                    for (int i = 5; i < dtPagosClase.Rows.Count; i++)
                    {
                        dValor = Convert.ToDouble(dtPagosClase.Rows[i][1].ToString());
                        sTexto += dtPagosClase.Rows[i][0].ToString().PadRight(10, ' ') + ":" + dValor.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                    }
                }

                else
                {
                    if (dbPorcentajeServicio != 0)
                    {
                        sTexto += "".PadLeft(22, ' ') + sFactura[5].PadLeft(6, ' ') + dValores[5].ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                    }
                }
            }
        }

        public string llenarFactura(DataTable dtConsulta_P, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                this.dtConsulta = dtConsulta_P;
                dtPagosClase = new DataTable();
                this.dtPagosClase = dtPagos;

                subtotal = 0;
                iva = 0;
                servicio = 0;
                descuento = 0;

                dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0][70].ToString());
                dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0][70].ToString());

                string sNumeroOrden = dtConsulta.Rows[0][46].ToString();
                string sNombreCliente;
                

                //NUMERO DE FACTURA
                sSecuencial = dtConsulta.Rows[0][37].ToString().PadLeft(9,'0');

                string sOrigen = dtConsulta.Rows[0][56].ToString();

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0][51].ToString()).ToString("dd/MM/yyyy");
                sHoraIngreso = Convert.ToDateTime(dtConsulta.Rows[0][51].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                sHoraSalida = Convert.ToDateTime(dtConsulta.Rows[0][52].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

                sTexto = "";
                sTexto += "Num.T. : " + dtConsulta.Rows[0][62].ToString() + " Cja: 01 " + "Msro: " + dtConsulta.Rows[0][50].ToString() + Environment.NewLine + Environment.NewLine;
                sTexto += "Fact   : " + dtConsulta.Rows[0][53].ToString() + "-" + dtConsulta.Rows[0][54].ToString() + "-" + sSecuencial + Environment.NewLine;
                sTexto += "Fecha  : " + sFecha + " Hora:" + sHoraIngreso.Substring(11,5) + " - " + sHoraSalida.Substring(11, 5) + Environment.NewLine;

                if (sOrigen == "MESAS")
                {
                    sSeccionMesa = dtConsulta.Rows[0][65].ToString();

                    if (sSeccionMesa.Length > 13)
                    {
                        sSeccionMesa = sSeccionMesa.Substring(0, 13);
                    }

                    sTexto += dtConsulta.Rows[0][48].ToString().PadRight(7, ' ') + ": " + sSeccionMesa.PadRight(13, ' ') + ("  No. Personas: " + dtConsulta.Rows[0][55].ToString()) + Environment.NewLine;
                }


                sNombreCliente = (dtConsulta.Rows[0][17].ToString() + " " + dtConsulta.Rows[0][18].ToString()).Trim();

                if (sNombreCliente.Length <= 30)
                {
                    sTexto += "CLIENTE:".PadRight(9, ' ') + sNombreCliente + Environment.NewLine;
                }

                else
                {
                    sTexto += "CLIENTE:".PadRight(9, ' ');
                    sTexto += caracteres.saltoLinea(sNombreCliente, 9);
                }

                if (dtConsulta.Rows[0][16].ToString() == "9999999999999")
                {
                    sTexto += "RUC/CI : " + dtConsulta.Rows[0][16].ToString().PadRight(14, ' ') + Environment.NewLine + Environment.NewLine;
                }

                else
                {
                    sTexto += "RUC/CI : " + dtConsulta.Rows[0][16].ToString().PadRight(14, ' ') + "Tlf.: " + dtConsulta.Rows[0][4].ToString() + Environment.NewLine;

                    sDireccionCliente = dtConsulta.Rows[0][3].ToString();

                    if (sDireccionCliente.Length <= 30)
                    {
                        sTexto += "DIRECCION: " + sDireccionCliente + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += "DIRECCION:".PadRight(10, ' ');
                        sTexto += caracteres.saltoLinea(sDireccionCliente, 10);
                    }
                }

                sTexto += "".PadRight(40, '=') + Environment.NewLine;
                sTexto += "CANT " + "DESCRIPCION".PadRight(22, ' ') + " V.UNI.  TOT." + Environment.NewLine;
                sTexto += "".PadRight(40, '=') + Environment.NewLine;

                //CALCULO DE VALORES
                //=============================================================
                dPorcentaje = Convert.ToDouble(dtConsulta.Rows[0][59].ToString()) / 100;

                if (dPorcentaje == 0)
                {
                    nuevovalor = subtotal;
                    dTotal = subtotal + iva + (subtotal * (dbPorcentajeServicio/100));
                }

                else
                {
                    nuevovalor = subtotal - (subtotal * dPorcentaje);
                    dTotal = nuevovalor + iva + (nuevovalor * (dbPorcentajeServicio/100));
                }

                sFactura[0] = "SubT. " + (Convert.ToDouble(dtConsulta.Rows[0][32].ToString())).ToString("N0").PadLeft(2, ' ') + "%:";
                sFactura[2] = "Descuento:";
                sFactura[4] = "IVA   " + (Convert.ToDouble(dtConsulta.Rows[0][32].ToString())).ToString("N0").PadLeft(2, ' ') + "%:";
                sFactura[5] = "Servicio " + (Convert.ToDouble(dtConsulta.Rows[0][70].ToString())).ToString("N0").PadLeft(2, ' ') + "%:";
                //=============================================================

                //cargarProductos(Convert.ToInt32(sIdOrden), Convert.ToInt32(dtConsulta.Rows[0][66].ToString()));

                if (llenarItemsFactura() == false)
                {
                    return "";
                }

                sTexto += "".PadRight(40, '=') + Environment.NewLine;
                
                completarFactura(0);
                sTexto += "".PadLeft(40, '-');
                return sTexto;
            }

            catch (Exception ex)
            {
                
                return "";
            }
        }


        //FUNCION PARA LLENAR LOS ITEMS
        private bool llenarItemsFactura()
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

                    dbCantidad = Convert.ToDouble(dtConsulta.Rows[i]["cantidad"].ToString());		//4
                    dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i]["precio_unitario"].ToString());	//5
                    dbValorDescuento = Convert.ToDouble(dtConsulta.Rows[i]["valor_dscto"].ToString());	//7
                    dbIva = Convert.ToDouble(dtConsulta.Rows[i]["valor_iva"].ToString());			//10
                    dbServicio = Convert.ToDouble(dtConsulta.Rows[i]["valor_otro"].ToString());		//11

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
                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.Substring(0, 20).PadRight(22, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(6, ' ') + dbSumaPrecio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                            sTexto += "".PadLeft(5, ' ') + sNombreProducto.Substring(20) + Environment.NewLine;
                        }

                        else
                        {
                            sTexto += sCantidadProducto.PadLeft(3, ' ') + "".PadRight(2, ' ') + sNombreProducto.PadRight(22, ' ') + dbPrecioUnitario.ToString("N2").PadLeft(6, ' ') + dbSumaPrecio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                        }
                    }
                }

                dbSumaSubtotalNeto = dbSumaSubtotalConIva + dbSumaSubtotalSinIva - dbSumaDescuento;

                dValores[0] = dbSumaSubtotalConIva;
                dValores[1] = dbSumaSubtotalSinIva;
                dValores[2] = dbSumaDescuento;
                dValores[3] = dbSumaSubtotalNeto;
                dValores[4] = dbSumaIVA;
                dValores[5] = dbSumaServicio;
                dTotal = dbSumaSubtotalNeto + dbSumaIVA + dbSumaServicio;


                if (iConsumoAlimentos == 1)
                {
                    //sTexto += "  1  " + "CONSUMO ALIMENTOS".PadRight(21, ' ') + dValores[0].ToString("N2").PadLeft(7, ' ') + dValores[0].ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                    sTexto += "  1  " + "CONSUMO ALIMENTOS".PadRight(21, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(7, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                }

                sTexto += Environment.NewLine + Environment.NewLine;

                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
