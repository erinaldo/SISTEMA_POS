using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseFacturaTextBox
    {
        string sTexto;
        string sSecuencial;
        string sSql;

        double dbPorcentajeServicio;
        double dbPorcentajeIva;

        DataTable dtPagosClase;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        //Función para cargar los productos de la orden
        private void cargarProductos(int iIdPosOrden)
        {
            Program.iCuenta = 0;
            bool bRespuesta = false;
            ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

            string sQuery = "";

            sQuery += "select dbo.cv403_cab_pedidos.id_pedido, dbo.cv401_nombre_productos.nombre," + Environment.NewLine;
            sQuery += "dbo.cv403_det_pedidos.cantidad, dbo.cv403_det_pedidos.precio_unitario, " + Environment.NewLine;
            sQuery += "dbo.cv403_det_pedidos.Valor_dscto, dbo.cv403_det_pedidos.comentario " + Environment.NewLine;
            sQuery += "from dbo.cv403_cab_pedidos inner join " + Environment.NewLine;
            sQuery += "dbo.cv403_det_pedidos on dbo.cv403_cab_pedidos.id_pedido = dbo.cv403_det_pedidos.id_pedido inner join " + Environment.NewLine;
            sQuery += "dbo.cv401_nombre_productos on dbo.cv403_det_pedidos.id_producto = dbo.cv401_nombre_productos.id_producto" + Environment.NewLine;
            sQuery += "and dbo.cv401_nombre_productos.estado = 'A' " + Environment.NewLine;
            sQuery += "where dbo.cv403_cab_pedidos.id_pedido = " + iIdPosOrden + Environment.NewLine;
            sQuery += "and dbo.cv403_det_pedidos.estado = 'A' " + Environment.NewLine;
            sQuery += "order by dbo.cv403_det_pedidos.id_det_pedido ";

            DataTable dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sQuery);

            if (bRespuesta == true)
            {
                Program.iCuenta = dtConsulta.Rows.Count;
                Program.sNombreProductos = new string[Program.iCuenta];
                Program.sCantidadProductos = new string[Program.iCuenta];
                Program.dPreciosProductos = new double[Program.iCuenta];

                double dbTotal1 = 0;
                double suma = 0;

                for (int i = 0; i < Program.iCuenta; i++)
                {
                    dbTotal1 += (Convert.ToDouble(dtConsulta.Rows[i][2].ToString())
                        * (Convert.ToDouble(dtConsulta.Rows[i][3].ToString()) - Convert.ToDouble(dtConsulta.Rows[i][4].ToString())));
                    if (i == (Program.iCuenta - 1))
                    {
                        break;
                    }

                    double precio = Convert.ToDouble(dtConsulta.Rows[i][3].ToString());
                    string Precio1 = precio.ToString("");
                    suma += (Convert.ToDouble(dtConsulta.Rows[i][2].ToString())
                        * (Convert.ToDouble(Precio1) - Convert.ToDouble(dtConsulta.Rows[i][4].ToString())));
                }

                for (int i = 0; i < Program.iCuenta; i++)
                {
                    if ((dtConsulta.Rows[i][5].ToString() == null) || (dtConsulta.Rows[i][5].ToString() == ""))
                    {
                        Program.sNombreProductos[i] = dtConsulta.Rows[i][1].ToString();
                    }

                    else
                    {
                        Program.sNombreProductos[i] = dtConsulta.Rows[i][5].ToString();
                    }

                    double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i][2].ToString());
                    double dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i][3].ToString());
                    double dbDescuento = Convert.ToDouble(dtConsulta.Rows[i][4].ToString());
                    string sCadena1;

                    if (dbCantidad < 1)
                    {
                        //sCadena1 = dbCantidad + "";//Se guarda la cantidad en tres espacios
                        sCadena1 = "1/2";//Se guarda la cantidad en tres espacios
                    }
                    else
                    {
                        sCadena1 = "" + dbCantidad;
                    }

                    string sCadena2 = "".PadRight(2, ' ');//se guarda cinco espacios
                    string sCadena3;
                    sCadena3 = Program.sNombreProductos[i].ToString();

                    string sCadena4 = dbPrecioUnitario.ToString("N2").PadLeft(5, ' ');

                    string sCadena5 = ((dbCantidad * dbPrecioUnitario)).ToString("N2");
                    //string sCadena5 = ((dbCantidad * (dbPrecioUnitario - dbDescuento))).ToString("N2");

                    if (i == (Program.iCuenta - 1))
                    {
                        sCadena5 = (Convert.ToDouble(dbTotal1.ToString("N2")) - suma).ToString("N2");
                        if (Convert.ToDouble(sCadena5) < 0)
                        {
                            sCadena5 = "0.00";
                        }
                    }
                    
                    sCadena5 = sCadena5.PadLeft(8, ' ');

                    if (sCadena3.Length > 22)
                    {
                        sTexto = sTexto + sCadena1.PadLeft(3, ' ') + sCadena2.PadRight(2, ' ') + sCadena3.Substring(0, 20).PadRight(22, ' ') + sCadena4.PadRight(5, ' ') + sCadena5.PadLeft(8, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(5, ' ') + sCadena3.Substring(20)+ Environment.NewLine;
                    }

                    else
                    {
                        sTexto = sTexto +  sCadena1.PadLeft(3, ' ') + sCadena2.PadRight(2, ' ') + sCadena3.PadRight(22, ' ') + sCadena4.PadRight(5, ' ') + sCadena5.PadLeft(8, ' ') + Environment.NewLine;
                    }

                    ////FUNCIONES PARA SABER SI HAY OBSERVACIONES DEL ITEM
                    ////sSql = "select count(*) cuenta from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ToString()) + " and estado = 'A'";
                    //sSql = "select detalle from pos_det_pedido_detalle where id_det_pedido = " + Convert.ToInt32(dtConsulta.Rows[i][6].ToString()) + " and estado = 'A' order by id_det_pedido";
                    //dtPreferencia = new DataTable();
                    //dtPreferencia.Clear();

                    //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPreferencia, sSql);

                    //if (bRespuesta == true)
                    //{
                    //    if (dtPreferencia.Rows.Count > 0)
                    //    {
                    //        for (int j = 0; j < dtPreferencia.Rows.Count; j++)
                    //        {
                    //            sTexto = sTexto + " >>".PadRight(4, ' ') + dtPreferencia.Rows[j][0].ToString().PadRight(16, ' ') + Environment.NewLine;
                    //        }
                    //    }
                    //}

                    //else
                    //{
                    //    ok.LblMensaje.Text = "Ocurrió un problema al consultar las observaciones del item '" + dtConsulta.Rows[i][2].ToString() + ".";
                    //    ok.ShowDialog();
                    //}

                    
                }
            }
            else
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
            }
        }

        private void completarFactura(int op)
        {
            if (op == 1)
            {
                return;
            }

            else
            {
                sTexto = sTexto + Environment.NewLine;
                //Double dValor;

                sTexto = sTexto + "FORMAS DE PAGO" + Environment.NewLine + Environment.NewLine;
                //sTexto = sTexto + Environment.NewLine;

                //for (int i = 0; i < dtPagosClase.Rows.Count; i++)
                //{
                //    dValor = Convert.ToDouble(dtPagosClase.Rows[i][1].ToString());
                //    sTexto = sTexto + dtPagosClase.Rows[i][0].ToString().PadRight(15, ' ') + "  " + dValor.ToString("N2").PadLeft(6, ' ') + Environment.NewLine;
                //}

                //Double dCambio = Convert.ToDouble(dtPagosClase.Rows[0][2].ToString());
                //sTexto = sTexto +"CAMBIO:".PadRight(15, ' ') + dCambio.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;

                Double dValor, dValorRecibido, dCambio, dSumaValores = 0, dCantidadDebida = 0;

                for (int i = 0; i < dtPagosClase.Rows.Count; i++)
                {
                    dValor = Convert.ToDouble(dtPagosClase.Rows[i][1].ToString());
                    dValorRecibido = Convert.ToDouble(dtPagosClase.Rows[i][4].ToString());
                    dCantidadDebida = dCantidadDebida + dValor;

                    if (dValor != dValorRecibido)
                    {
                        dValor = dValorRecibido;
                    }

                    dSumaValores = dSumaValores + dValor;

                    sTexto = sTexto + dtPagosClase.Rows[i][0].ToString().PadRight(20, ' ') + dValor.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                }

                dCambio = Convert.ToDouble(dtPagosClase.Rows[0][2].ToString());
                sTexto = sTexto + "".PadRight(27, '-') + Environment.NewLine;
                sTexto = sTexto + "TOTAL RECIBIDO".PadRight(20, ' ') + dSumaValores.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                sTexto = sTexto + "CANTIDAD DEBIDA".PadRight(20, ' ') + dCantidadDebida.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                sTexto = sTexto + "CAMBIO".PadRight(20, ' ') + dCambio.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;

            }
        }

        public string llenarFactura(DataTable dtConsulta, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                dtPagosClase = new DataTable();
                this.dtPagosClase = dtPagos;

                Double subtotal = 0;
                Double iva = 0;
                Double servicio = 0;

                dbPorcentajeServicio = Convert.ToDouble(dtConsulta.Rows[0][70].ToString());
                dbPorcentajeIva = Convert.ToDouble(dtConsulta.Rows[0][32].ToString());
                
                string sNumeroOrden = dtConsulta.Rows[0][46].ToString();

                for (int j = 0; j < dtConsulta.Rows.Count; j++)
                {
                    if ((dtConsulta.Rows[j][42].ToString() != "1") && (dtConsulta.Rows[j][43].ToString() != "1"))
                    {
                        //subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j][4].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j][5].ToString()) - Convert.ToDouble(dtConsulta.Rows[j][7].ToString())));
                        subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j][27].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j][28].ToString())));
                        iva = iva + (Convert.ToDouble(dtConsulta.Rows[j][27].ToString()) * Convert.ToDouble(dtConsulta.Rows[j][33].ToString()));
                        //servicio = servicio + (Convert.ToDouble(dtConsulta.Rows[j][11].ToString()) * Convert.ToDouble(dtConsulta.Rows[j][4].ToString()));

                    }
                }

                //NUMERO DE FACTURA
                sSecuencial = dtConsulta.Rows[0][37].ToString();
                //RELLENAMOS DE NUMEROS CEROS PARA QUE SE CUMPLA LOS 9 CARACTERES DE LA SECUENCIA DE LA FACTURA
                for (int i = 9; i > 0; i--)
                {
                    if (sSecuencial.Length < 9)
                    {
                        sSecuencial = "0" + sSecuencial;
                    }
                }
                //=======================================================================================


                string sOrigen = dtConsulta.Rows[0][56].ToString();

                sTexto = "";

                sTexto = sTexto + "FACTURA N°: ".PadLeft(7, ' ') + dtConsulta.Rows[0][53].ToString() + "-" + dtConsulta.Rows[0][54].ToString() + "-" + sSecuencial + Environment.NewLine + Environment.NewLine;
                //sTexto = sTexto + "Mesero:".PadRight(8, ' ') + dtConsulta.Rows[0][49].ToString().PadRight(20, ' ') + "Estacion: 2" + Environment.NewLine;
                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + "# de Cuenta:".PadRight(15, ' ') + dtConsulta.Rows[0][57].ToString().PadRight(13, ' ') + "Orden: " + dtConsulta.Rows[0][62].ToString() + Environment.NewLine;

                sTexto = sTexto + "Tipo de Orden: " + sOrigen + Environment.NewLine;

                if (sOrigen == "MESAS")
                {
                    sTexto = sTexto + ("# de Personas: " + dtConsulta.Rows[0][55].ToString()).PadRight(28, ' ') + dtConsulta.Rows[0][48].ToString() + Environment.NewLine;
                }

                //sw.WriteLine("# de Personas:".PadRight(16, ' ') + dtConsulta.Rows[0][55].ToString().PadRight(12, ' ') + "" + sOrigen + "");
                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + "CLIENTE:".PadRight(10, ' ') + dtConsulta.Rows[0][17].ToString() + " " + dtConsulta.Rows[0][18].ToString() + Environment.NewLine;
                sTexto = sTexto + "CI/RUC:".PadRight(10, ' ') + dtConsulta.Rows[0][16].ToString() + Environment.NewLine;
                sTexto = sTexto + "TELEFONO:".PadRight(10, ' ') + dtConsulta.Rows[0][4].ToString() + Environment.NewLine;

                if (dtConsulta.Rows[0][3].ToString().Length <= 30)
                {
                    sTexto = sTexto + "DIRECCION:" + dtConsulta.Rows[0][3].ToString() + Environment.NewLine;
                }
                else
                {
                    sTexto = sTexto + "DIRECCION:" + dtConsulta.Rows[0][3].ToString().Substring(0, 30).PadRight(30, ' ')
                        + Environment.NewLine + dtConsulta.Rows[0][3].ToString().Substring(30).PadLeft((9 + dtConsulta.Rows[0][3].ToString().Substring(30).Length), ' ') + Environment.NewLine;
                }

                string sSql = "select top 1 referencia from tp_vw_direcciones where id_persona = " + Program.iIdPersona + " and estado = 'A'";
                DataTable dtDomicilio = new DataTable();

                bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);
                if (bRespuesta == true)
                {
                    if (dtDomicilio.Rows.Count > 0)
                    {
                        if (dtDomicilio.Rows[0][0].ToString() != "")
                        {
                            int iLongitudCadena = dtDomicilio.Rows[0][0].ToString().Length;
                            int iLongitudRecorte = iLongitudCadena;
                            int iLineas = iLongitudCadena / 40;

                            if (iLongitudCadena < 40)
                            {
                                //iLineas = 1;
                                sTexto = sTexto + dtDomicilio.Rows[0][0].ToString().PadRight(40, ' ') + Environment.NewLine;
                            }

                            else if (iLongitudCadena % 40 != 0)
                            {
                                iLineas++;
                                for (int i = 0; i < iLineas; i++)
                                {
                                    if (iLongitudRecorte >= 40)
                                    {
                                        sTexto = sTexto + dtDomicilio.Rows[0][0].ToString().Substring((i * 40), 40) + Environment.NewLine;
                                        iLongitudRecorte = iLongitudRecorte - 40;
                                    }

                                    else
                                    {
                                        sTexto = sTexto + dtDomicilio.Rows[0][0].ToString().Substring((i * 40), iLongitudRecorte) + Environment.NewLine;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //sTexto = sTexto + "SIN REFERENCIA" + Environment.NewLine;
                        }
                    }
                }


                //texto = texto + "Nombre de Cliente:" + lista.AccessibleDefaultActionDescription + Environment.NewLine;
                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                //sTexto = sTexto +  "          >> ORDEN PAGADA <<" + Environment.NewLine;
                sTexto = sTexto + "CANT " + "DESCRIPCION".PadRight(22, ' ') + " V.UNI.  TOT." + Environment.NewLine;
                sTexto = sTexto +  "----------------------------------------" + Environment.NewLine;
                cargarProductos(Convert.ToInt32(sIdOrden));

                sTexto = sTexto + "----------------------------------------" + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(10, ' ') + "SUBTOTAL:".PadRight(20, ' ') + subtotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                Double dPorcentaje = Convert.ToDouble(dtConsulta.Rows[0][59].ToString()) / 100;

                sTexto = sTexto + "".PadLeft(10, ' ') + ("DESCUENTO " + dtConsulta.Rows[0][59].ToString() + "%:").PadRight(20, ' ') + (subtotal * dPorcentaje).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                if (dPorcentaje == 0)
                {
                    sTexto = sTexto + "".PadLeft(10, ' ') + "SUBTOTAL:".PadRight(20, ' ') + subtotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(10, ' ') + ("IVA " + dbPorcentajeIva.ToString("N0") +"%:").PadRight(20, ' ') + iva.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                    if (dbPorcentajeServicio != 0)
                    {
                        sTexto = sTexto + "".PadLeft(10, ' ') + (dbPorcentajeServicio.ToString() + "% SERVICIO:").PadRight(20, ' ') + (subtotal * (dbPorcentajeServicio/100)).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    }

                    sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(10, ' ') + "CANTIDAD DEBIDA:".PadRight(20, ' ') + (subtotal + iva + (subtotal * (dbPorcentajeServicio/100))).ToString("N2").PadLeft(10, ' ') + Environment.NewLine + Environment.NewLine;
                }

                else
                {
                    Double nuevovalor = subtotal - (subtotal * dPorcentaje);

                    sTexto = sTexto + "".PadLeft(10, ' ') + "SUBTOTAL:".PadRight(20, ' ') + nuevovalor.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(10, ' ') + ("IVA " + dbPorcentajeIva.ToString() + "%:").PadRight(20, ' ') + iva.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;

                    if (dbPorcentajeServicio != 0)
                    {
                        sTexto = sTexto + "".PadLeft(10, ' ') + (dbPorcentajeServicio.ToString("N0") + "% SERVICIO:").PadRight(20, ' ') + (nuevovalor * (dbPorcentajeServicio / 100)).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                    }
                    
                    sTexto = sTexto + "".PadLeft(10, ' ') + "CANTIDAD DEBIDA:".PadRight(20, ' ') + (nuevovalor + iva + (nuevovalor * (dbPorcentajeServicio/100))).ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                }

                sTexto = sTexto + Environment.NewLine;
                sTexto = sTexto + "     Creado:   " + dtConsulta.Rows[0][51].ToString() + Environment.NewLine;
                sTexto = sTexto + "     Pagada:   " + dtConsulta.Rows[0][52].ToString() + Environment.NewLine;


                completarFactura(0);
                sTexto = sTexto + Environment.NewLine;
                sTexto = sTexto + Environment.NewLine;
                sTexto = sTexto + Environment.NewLine + ".";

                return sTexto;
            }

            catch (Exception)
            {
                return "";
            }
        }
    }
}
