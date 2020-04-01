using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseImprimirFacturaNormal
    {
         ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
         ClaseManejoCaracteres caracteres = new ClaseManejoCaracteres();

         string[] sFactura = new string[6]
        {
          "",
          "SubT.  0%:",
          "",
          "Subtotal :",
          "",
          ""
        };

         Decimal subtotal = 0;
         Decimal iva = 0;
         Decimal servicio = 0;
         Decimal descuento = 0;
         public Decimal dbPropina = 0;
         Decimal[] dValores = new Decimal[6];

         string sSql;
         string sTexto;
         string sNombreComercial;
         string sNumeroRucEmpresa;
         string sTelefonoEmpresa;
         string sDireccionEmpresa;
         string sSecuencial;
         string sOrigen;
         string sNombreCliente;
         string sFecha;
         string sHoraIngreso;
         string sHoraSalida;
         string sSeccionMesa;
         string sDireccionCliente;
         string sCantidadProducto;
         string sNombreProducto;         

         bool bRespuesta;

         DataTable dtConsulta;
         DataTable dtPagosClase;

         Decimal dPorcentaje;
         Decimal nuevovalor;
         Decimal dbPorcentajeServicio;
         Decimal dbPorcentajeIva;         
         Decimal dbSumaSubtotalConIva;
         Decimal dbSumaSubtotalSinIva;
         Decimal dbSumaDescuento;
         Decimal dbSumaIVA;
         Decimal dbSumaServicio;
         Decimal dbPorcentajeDescuento;
         Decimal dbCantidad;
         Decimal dbPrecioUnitario;
         Decimal dbValorDescuento;
         Decimal dbIva;
         Decimal dbServicio;
         Decimal dbSumaPrecio;
         Decimal dbSumaSubtotalNeto;
         Decimal dValor;
         public Decimal dTotal;

        int iPagaIva;
        int iCuentaLinea;
        int iIdPedido;
        int iBandera;
        
        public string sCrearFactura(int iIdFactura_P)
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
                iva = 0;
                servicio = 0;
                descuento = 0;

                dbPorcentajeServicio = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_servicio"].ToString());
                dbPorcentajeIva = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_iva"].ToString());
                sSecuencial = dtConsulta.Rows[0]["numero_factura"].ToString().PadLeft(9, '0');
                sOrigen = dtConsulta.Rows[0]["descripcion_origen_orden"].ToString();
                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("dd/MM/yyyy");
                sHoraIngreso = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
                sHoraSalida = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre_orden"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");

                sTexto = "";
                sTexto += "Num.T. : " + dtConsulta.Rows[0]["numero_pedido"].ToString() + " Cja: 01 Msro: " + dtConsulta.Rows[0]["mesero"].ToString() + Environment.NewLine + Environment.NewLine;
                sTexto += "Fact   : " + dtConsulta.Rows[0]["establecimiento"].ToString() + "-" + dtConsulta.Rows[0]["punto_emision"].ToString() + "-" + sSecuencial + Environment.NewLine;
                sTexto += "Fecha  : " + sFecha + " Hora:" + sHoraIngreso.Substring(11, 5) + " - " + sHoraSalida.Substring(11, 5) + Environment.NewLine;

                if (sOrigen == "MESAS")
                {
                    sSeccionMesa = dtConsulta.Rows[0]["seccion_mesa"].ToString();

                    if (sSeccionMesa.Length > 13)
                    {
                        sSeccionMesa = sSeccionMesa.Substring(0, 13);
                    }

                    sTexto += dtConsulta.Rows[0]["mesa"].ToString().PadRight(7, ' ') + ": " + sSeccionMesa.PadRight(13, ' ') + "  No. Personas: " + dtConsulta.Rows[0]["numero_personas"].ToString() + Environment.NewLine;
                }

                if (dtConsulta.Rows[0]["tipo_persona"].ToString() == "J")
                {
                    sNombreCliente = (dtConsulta.Rows[0]["apellidos"].ToString() + " " + dtConsulta.Rows[0]["nombres"].ToString()).Trim();
                }

                else
                {
                    sNombreCliente = (dtConsulta.Rows[0]["nombres"].ToString() + " " + dtConsulta.Rows[0]["apellidos"].ToString()).Trim();
                }

                if (sNombreCliente.Length <= 31)
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
                    sTexto += "RUC/CI : " + dtConsulta.Rows[0]["identificacion"].ToString().PadRight(14, ' ') + Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    sTexto += "RUC/CI : " + dtConsulta.Rows[0]["identificacion"].ToString().PadRight(14, ' ') + "Tlf.: " + dtConsulta.Rows[0]["telefono_factura"].ToString() + Environment.NewLine;
                    sDireccionCliente = dtConsulta.Rows[0]["direccion_factura"].ToString();
                    
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

                dPorcentaje = Convert.ToDecimal(dtConsulta.Rows[0][59].ToString()) / 100;

                if (dPorcentaje == new Decimal(0))
                {
                    nuevovalor = subtotal;
                    dTotal = subtotal + iva + subtotal * (dbPorcentajeServicio / 100);
                }

                else
                {
                    nuevovalor = subtotal - subtotal * dPorcentaje;
                    dTotal = nuevovalor + iva + nuevovalor * (dbPorcentajeServicio / 100);
                }

                sFactura[0] = "SubT. " + Convert.ToDouble(dtConsulta.Rows[0][32].ToString()).ToString("N0").PadLeft(2, ' ') + "%:";
                sFactura[2] = "Descuento:";
                sFactura[4] = "IVA   " + Convert.ToDouble(dtConsulta.Rows[0][32].ToString()).ToString("N0").PadLeft(2, ' ') + "%:";
                sFactura[5] = "Servicio " + Convert.ToDouble(dtConsulta.Rows[0][70].ToString()).ToString("N0").PadLeft(2, ' ') + "%:";

                if (llenarItemsFactura() == false)
                {
                    return "";
                }

                sTexto += "".PadRight(40, '=') + Environment.NewLine;

                iIdPedido = Convert.ToInt32(dtConsulta.Rows[0]["id_pedido"].ToString());

                sSql = "";
                sSql += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta," + Environment.NewLine;
                sSql += "sum(isnull(valor_recibido, valor)) valor_recibido, propina" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, propina" + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtPagosClase = new DataTable();
                dtPagosClase.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPagosClase, sSql);

                if (bRespuesta == false)
                {
                    return "";
                }

                completarFactura(0);
                //sTexto += "PROPINA: " + dbPropina.ToString("N2") + Environment.NewLine;
                sTexto += "".PadLeft(40, '-');
                return sTexto;

            }

            catch (Exception ex)
            {
                return "";
            }
        }

        //LLENAR LOS ITEMS DE LA FACTURA
        private bool llenarItemsFactura()
        {
            try
            {
                int iConcumoAlimentos_P = Convert.ToInt32(dtConsulta.Rows[0]["consumo_alimentos"].ToString());

                dbSumaSubtotalConIva = 0;
                dbSumaSubtotalSinIva = 0;
                dbSumaDescuento = 0;
                dbSumaIVA = 0;
                dbSumaServicio = 0;

                dbPorcentajeDescuento = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_dscto"].ToString());

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

                    dbCantidad = Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString());
                    dbPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[i]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDecimal(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbIva = Convert.ToDecimal(dtConsulta.Rows[i]["valor_iva"].ToString());
                    dbServicio = Convert.ToDecimal(dtConsulta.Rows[i]["valor_otro"].ToString());
                    iPagaIva = Convert.ToInt32(dtConsulta.Rows[i]["paga_iva"].ToString());

                    if (dbValorDescuento != 0)
                    {
                        dbSumaDescuento += dbCantidad * dbValorDescuento;
                    }

                    if (iPagaIva == 0)
                    {
                        dbSumaSubtotalSinIva += dbCantidad * dbPrecioUnitario;
                    }
                    else
                    {
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

                    if (iConcumoAlimentos_P == 0)
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

                if (iConcumoAlimentos_P == 1)
                {
                    sTexto += "  1  " + "CONSUMO ALIMENTOS".PadRight(21, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(7, ' ') + dbSumaSubtotalNeto.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                }

                sTexto += Environment.NewLine + Environment.NewLine;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //FUNCION PARA COMPLETAR LOS DATOS DE LA FACTURA
        private void completarFactura(int op)
        {
            if (op == 1)
            {
                return;
            }

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

                   dValor = Convert.ToDecimal(dtPagosClase.Rows[i][1].ToString());
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

               dValor = Convert.ToDecimal(dtPagosClase.Rows[5][1].ToString());

               sTexto += ":" + dValor.ToString("N2").PadLeft(7, ' ') + sFactura[4].PadLeft(14, ' ') + dValores[4].ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
               
               for (int i = 5; i < dtPagosClase.Rows.Count; i++)
               {
                   dValor = Convert.ToDecimal(dtPagosClase.Rows[i][1].ToString());
                   sTexto += dtPagosClase.Rows[i][0].ToString().PadRight(10, ' ') + ":" + dValor.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
               }
           }

           else if (dbPorcentajeServicio != 0)
           {
               sTexto += "".PadLeft(22, ' ') + sFactura[5].PadLeft(6, ' ') + dValores[5].ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
           }

            dbPropina = Convert.ToDecimal(dtPagosClase.Rows[0]["propina"].ToString());
       }
    }
}
