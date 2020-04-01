using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseReporteFactura
    {
        int iAnchoDePrecuenta = 30;
        int iAnchoDeDescripcion = 20;
        int iAnchoDePrecio = 8;
        StreamWriter sw;

        string sSql;
        string sDireccionCliente;

        double dbPorcentajeServicio;

        DataTable dtPagosClase;
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        Clases.ClaseManejoCaracteres caracteres = new ClaseManejoCaracteres();

        //Función para cargar los productos de la orden
        private void cargarProductos(int iIdPosOrden)
        {
            Program.iCuenta = 0;
            bool bRespuesta = false;
            ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

            sSql = "";
            sSql = sSql + "select dbo.cv403_cab_pedidos.id_pedido, dbo.cv401_nombre_productos.nombre," + Environment.NewLine;
            sSql = sSql + "dbo.cv403_det_pedidos.cantidad, dbo.cv403_det_pedidos.precio_unitario, " + Environment.NewLine;
            sSql = sSql + "dbo.cv403_det_pedidos.Valor_dscto, dbo.cv403_det_pedidos.comentario " + Environment.NewLine;
            sSql = sSql + "from dbo.cv403_cab_pedidos inner join " + Environment.NewLine;
            sSql = sSql + "dbo.cv403_det_pedidos on dbo.cv403_cab_pedidos.id_pedido = dbo.cv403_det_pedidos.id_pedido inner join " + Environment.NewLine;
            sSql = sSql + "dbo.cv401_nombre_productos on dbo.cv403_det_pedidos.id_producto = dbo.cv401_nombre_productos.id_producto and dbo.cv401_nombre_productos.estado = 'A'" + Environment.NewLine;
            sSql = sSql + "where dbo.cv403_cab_pedidos.id_pedido = " + iIdPosOrden + Environment.NewLine;
            sSql = sSql + "and dbo.cv403_det_pedidos.estado = 'A' " + Environment.NewLine;
            sSql = sSql + "order by dbo.cv403_det_pedidos.id_det_pedido ";

            DataTable dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

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
                    dbTotal1 += (Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString())
                        * (Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString()) - Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString())));
                    if (i == (Program.iCuenta - 1))
                    {
                        break;
                    }

                    double precio = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                    string Precio1 = precio.ToString("");
                    suma += (Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString())
                        * (Convert.ToDouble(Precio1) - Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString())));
                }

                for (int i = 0; i < Program.iCuenta; i++)
                {
                    if ((dtConsulta.Rows[i].ItemArray[5].ToString() == null) || (dtConsulta.Rows[i].ItemArray[5].ToString() == ""))
                    {
                        Program.sNombreProductos[i] = dtConsulta.Rows[i].ItemArray[1].ToString();
                    }

                    else
                    {
                        Program.sNombreProductos[i] = dtConsulta.Rows[i].ItemArray[5].ToString();
                    }

                    double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                    double dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                    double dbDescuento = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString());
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

                    string sCadena2 = "  ";//se guarda cinco espacios
                    string sCadena3;
                    if (Program.sNombreProductos[i].Length < iAnchoDeDescripcion)
                    {
                        sCadena3 = Program.sNombreProductos[i].PadRight(iAnchoDeDescripcion, ' ');
                    }
                    else
                    {
                        sCadena3 = Program.sNombreProductos[i].Substring(0, iAnchoDeDescripcion);//se guarda el nombre con 13 caractéres
                    }

                    string sCadena4 = "  ";//se guarda tres espacios

                    
                    string sCadena5 = ((dbCantidad * (dbPrecioUnitario - dbDescuento))).ToString("N2");
                   
                    if (i == (Program.iCuenta - 1))
                    {
                        sCadena5 = (Convert.ToDouble(dbTotal1.ToString("N2")) - suma).ToString("N2");
                        if (Convert.ToDouble(sCadena5) < 0)
                        {
                            sCadena5 = "0.00";
                        }
                    }
                    // MessageBox.Show(sCadena5 + "  " + sCadena5.Length + "");
                    sCadena5 = sCadena5.PadLeft(iAnchoDePrecio, ' ');
                    //   MessageBox.Show(sCadena5 + "  " + sCadena5.Length + "");
                    //      MessageBox.Show(sCadena5.Length + "");
                    if (sCadena5.Length < iAnchoDePrecio)
                    {
                        // MessageBox.Show(sCadena5+"  "+ sCadena5.Length+"");
                        int iResta = (iAnchoDePrecio - sCadena5.Length);
                        sCadena5 = sCadena5.PadLeft(iResta, ' ');//Se guarda 6 espacios;
                        //MessageBox.Show(sCadena5 + "  " + sCadena5.Length + "");
                    }
                    else if (sCadena5.Length > iAnchoDePrecio)
                    {
                        //MessageBox.Show(sCadena5 + "  " + sCadena5.Length + "");
                        sCadena5 = sCadena5.Substring(0, iAnchoDePrecio);
                        //MessageBox.Show(sCadena5 + "  " + sCadena5.Length + "");
                    }

                    Program.sCantidadProductos[i] = dtConsulta.Rows[i].ItemArray[2].ToString();
                    Program.dPreciosProductos[i] = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString()) - dbDescuento;

                    // texto = texto + Program.sCantidadProductos[i] + "  " + Program.sNombreProductos[i] + "      $" + ((dbCantidad * (dbPrecioUnitario - dbDescuento))).ToString("N2") +"       \n" + Environment.NewLine;
                    sw.WriteLine(sCadena1.PadLeft(4, ' ') + sCadena2 + sCadena3 + sCadena4 + sCadena5);
                }
            }
            else
            {

            }
        }

        private void completarFactura(int op)
        {
            if (op == 1)
            {
            }

            else
            {
                sw.WriteLine();
                sw.WriteLine();
                Double dValor;

                sw.WriteLine("FORMAS DE PAGO");

                for (int i = 0; i < dtPagosClase.Rows.Count; i++)
                {
                    dValor = Convert.ToDouble(dtPagosClase.Rows[i].ItemArray[1].ToString());
                    sw.WriteLine(dtPagosClase.Rows[i].ItemArray[0].ToString().PadRight(15, ' ') + "  " + dValor.ToString("N2").PadLeft(6, ' '));
                }
                Double dCambio = Convert.ToDouble(dtPagosClase.Rows[0].ItemArray[2].ToString());
                sw.WriteLine("CAMBIO:".PadRight(15, ' ') + dCambio.ToString("N2").PadLeft(8, ' '));


            }
        }


        public void llenarPrecuenta(DataTable dtConsulta, string sIdOrden, string sEstado, DataTable dtPagos)
        {
            try
            {
                this.dtPagosClase = dtPagos;

                string sPath = @"c:\reportes\precuenta.txt";
                if (File.Exists(sPath))
                {
                    Double subtotal = 0;
                    Double iva = 0;
                    Double servicio = 0;

                    
                    string sNumeroOrden = dtConsulta.Rows[0].ItemArray[46].ToString();

                    for (int j = 0; j < dtConsulta.Rows.Count; j++)
                    {
                        if ((dtConsulta.Rows[j].ItemArray[42].ToString() != "1") && (dtConsulta.Rows[j].ItemArray[43].ToString() != "1"))
                        {
                            //subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[5].ToString()) - Convert.ToDouble(dtConsulta.Rows[j].ItemArray[7].ToString())));
                            subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[27].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[28].ToString())));
                            iva = iva + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[27].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[33].ToString()));
                            //servicio = servicio + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[11].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()));

                        }
                    }

                    string sOrigen = dtConsulta.Rows[0].ItemArray[56].ToString();

                    sw = new StreamWriter(sPath);

                    sw.WriteLine(Program.local);
                    sw.WriteLine(Program.direccion);
                    //sw.WriteLine(Program.direccion1);
                    sw.WriteLine(Program.telefono1);
                    sw.WriteLine(Program.telefono2);
                    sw.WriteLine("Mesero:".PadRight(8, ' ') + dtConsulta.Rows[0].ItemArray[49].ToString().PadRight(20, ' ') + "Estacion: 2");
                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("# de Cuenta:".PadRight(16, ' ') + dtConsulta.Rows[0].ItemArray[57].ToString().PadRight(12, ' ') + "Orden: " + dtConsulta.Rows[0].ItemArray[62].ToString());

                    sw.WriteLine("Tipo de Orden: " + sOrigen);

                    if (sOrigen == "MESAS")
                    {
                        sw.WriteLine("# de Personas: " + dtConsulta.Rows[0].ItemArray[55].ToString() + "        MESA " + dtConsulta.Rows[0].ItemArray[48].ToString());
                    }
                    
                    //sw.WriteLine("# de Personas:".PadRight(16, ' ') + dtConsulta.Rows[0].ItemArray[55].ToString().PadRight(12, ' ') + "" + sOrigen + "");
                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("CLIENTE:".PadRight(10, ' ' ) + dtConsulta.Rows[0].ItemArray[18].ToString() + " " + dtConsulta.Rows[0].ItemArray[17].ToString());
                    sw.WriteLine("CI/RUC:".PadRight(10, ' ') + dtConsulta.Rows[0].ItemArray[16].ToString());
                    sw.WriteLine("TELEFONO:".PadRight(10, ' ') + dtConsulta.Rows[0].ItemArray[4].ToString());

                    if (dtConsulta.Rows[0].ItemArray[3].ToString().Length <= 30)
                    {
                        sw.WriteLine("DIRECCION:" + dtConsulta.Rows[0].ItemArray[3].ToString());
                    }
                    else
                    {
                        sw.WriteLine("DIRECCION:" + dtConsulta.Rows[0].ItemArray[3].ToString().Substring(0, 30).PadRight(30, ' ')
                            + Environment.NewLine + dtConsulta.Rows[0].ItemArray[3].ToString().Substring(30).PadLeft((9 + dtConsulta.Rows[0].ItemArray[3].ToString().Substring(30).Length), ' '));
                    }

                    //if (dtConsulta.Rows[0].ItemArray[8].ToString() != "")
                    //{
                    //    if (dtConsulta.Rows[0].ItemArray[8].ToString().Length > 30)
                    //    {

                    //        sw.WriteLine("REFERENCIA: " + dtConsulta.Rows[0].ItemArray[8].ToString().Substring(0, 30).PadRight(30, ' ') + Environment.NewLine +
                    //                                        dtConsulta.Rows[0].ItemArray[8].ToString().Substring(30).PadLeft((12 + dtConsulta.Rows[0].ItemArray[8].ToString().Substring(30).Length), ' '));
                    //    }
                    //    else
                    //    {
                    //        sw.WriteLine("REFERENCIA: " + dtConsulta.Rows[0].ItemArray[8].ToString().PadRight(30, ' '));
                    //    }
                    //}


                    string sSql = "select top 1 referencia from tp_vw_direcciones where id_persona = " + Program.sIDPERSONA + " and estado = 'A'";
                    DataTable dtDomicilio = new DataTable();

                    bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtDomicilio.Rows.Count > 0)
                        {
                            if (dtDomicilio.Rows[0].ItemArray[0].ToString() != "")
                            {
                                int iLongitudCadena = dtDomicilio.Rows[0].ItemArray[0].ToString().Length;
                                int iLongitudRecorte = iLongitudCadena;
                                int iLineas = iLongitudCadena / 40;

                                if (iLongitudCadena < 40)
                                {
                                    //iLineas = 1;
                                    sw.WriteLine(dtDomicilio.Rows[0].ItemArray[0].ToString().PadRight(40, ' '));
                                }

                                else if (iLongitudCadena % 40 != 0)
                                {
                                    iLineas++;
                                    for (int i = 0; i < iLineas; i++)
                                    {
                                        if (iLongitudRecorte >= 40)
                                        {
                                            sw.WriteLine(dtDomicilio.Rows[0].ItemArray[0].ToString().Substring((i * 40), 40));
                                            iLongitudRecorte = iLongitudRecorte - 40;
                                        }

                                        else
                                        {
                                            sw.WriteLine(dtDomicilio.Rows[0].ItemArray[0].ToString().Substring((i * 40), iLongitudRecorte));
                                        }


                                    }

                                }


                            }
                            else
                            {
                                sw.WriteLine("SIN REFERENCIA");
                            }
                        }
                    }
                    

                    //texto = texto + "Nombre de Cliente:" + lista.AccessibleDefaultActionDescription + Environment.NewLine;
                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("          >> ORDEN PAGADA <<");
                    sw.WriteLine("----------------------------------------");
                    cargarProductos(Convert.ToInt32(sIdOrden));

                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("SUBTOTAL:".PadRight(30, ' ') + subtotal.ToString("N2").PadLeft(6, ' '));

                    Double dPorcentaje = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[59].ToString()) / 100;



                    sw.WriteLine(("DESCUENTO " + dtConsulta.Rows[0].ItemArray[59].ToString() + "%:").PadRight(30, ' ') + (subtotal * dPorcentaje).ToString("N2").PadLeft(6, ' '));

                    //dr["PorcentajeDescuento"] = dtConsulta.Rows[i].ItemArray[59].ToString();

                    if (dPorcentaje == 0)
                    {
                        sw.WriteLine("SUBTOTAL:".PadRight(30, ' ') + subtotal.ToString("N2").PadLeft(6, ' '));
                        sw.WriteLine("IVA:".PadRight(30, ' ') + iva.ToString("N2").PadLeft(6, ' '));
                        sw.WriteLine("10% SERVICIO:".PadRight(30, ' ') + (subtotal * Program.servicio).ToString("N2").PadLeft(6, ' '));
                        sw.WriteLine("CANTIDAD DEBIDA:".PadRight(30, ' ') + (subtotal + iva + (subtotal * Program.servicio)).ToString("N2").PadLeft(6, ' '));
                    }

                    else
                    {
                        Double nuevovalor = subtotal - (subtotal * dPorcentaje);

                        sw.WriteLine("SUBTOTAL:".PadRight(30, ' ') + nuevovalor.ToString("N2").PadLeft(6, ' '));
                        sw.WriteLine("IVA:".PadRight(30, ' ') + iva.ToString("N2").PadLeft(6, ' '));
                        sw.WriteLine("10% SERVICIO:".PadRight(30, ' ') + (nuevovalor * Program.servicio).ToString("N2").PadLeft(6, ' '));
                        sw.WriteLine("CANTIDAD DEBIDA:".PadRight(30, ' ') + (nuevovalor + iva + (nuevovalor * Program.servicio)).ToString("N2").PadLeft(7, ' '));
                    }

                    sw.WriteLine();
                    sw.WriteLine("     Creado:   " + dtConsulta.Rows[0].ItemArray[51].ToString());
                    sw.WriteLine("     Pagada:   " + dtConsulta.Rows[0].ItemArray[52].ToString());


                    completarFactura(0);
                    sw.WriteLine(".");
                    sw.WriteLine(".");
                    sw.WriteLine(".");
                    sw.Close();
                    //Process.Start(sPath);
                    System.Diagnostics.Process.Start(@"C:\reportes\imprimir.bat");

                }
            }

            catch (Exception)
            {

            }

        }
    }
}
