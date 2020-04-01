using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace Palatium.Clases
{
    public class ClaseReportePrecuenta
    {
        int iAnchoDePrecuenta = 30;
        int iAnchoDeDescripcion = 20;
        int iAnchoDePrecio = 8;
        StreamWriter sw;
        DataTable dtPagosClase;

        string sSql = "";
        DataTable dtDomicilio;
        bool bRespuesta = false;
        string sOrigen;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        //Función para cargar los productos de la orden
        private void cargarProductos(int iIdPosOrden)
        {
            Program.iCuenta = 0;
            bool bRespuesta = false;
            ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

            string sQuery = "select dbo.cv403_cab_pedidos.id_pedido, dbo.cv401_nombre_productos.nombre, dbo.cv403_det_pedidos.cantidad, dbo.cv403_det_pedidos.precio_unitario, "+
                            "dbo.cv403_det_pedidos.Valor_dscto, dbo.cv403_det_pedidos.comentario " +
                            "from dbo.cv403_cab_pedidos inner join "+
	                        "dbo.cv403_det_pedidos on dbo.cv403_cab_pedidos.id_pedido = dbo.cv403_det_pedidos.id_pedido inner join "+
                            "dbo.cv401_nombre_productos on dbo.cv403_det_pedidos.id_producto = dbo.cv401_nombre_productos.id_producto and dbo.cv401_nombre_productos.estado = 'A'" +
                            "where dbo.cv403_cab_pedidos.id_pedido = " + iIdPosOrden + " and dbo.cv403_det_pedidos.estado = 'A' "+
                            "  ORDER BY dbo.cv403_det_pedidos.id_det_pedido ";

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
                        Program.sNombreProductos[i] = dtConsulta.Rows[i].ItemArray[1].ToString() + "(" + dtConsulta.Rows[i].ItemArray[5].ToString() + ")";
                    }

                    double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString()); 
                    double dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                    double dbDescuento = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[4].ToString());
                    string sCadena1;
                    if (dbCantidad < 1)
                    {
                         //sCadena1 = dbCantidad +"" ;//Se guarda la cantidad en tres espacios
                         sCadena1 = "1/2";//Se guarda la cantidad en tres espacios
                    }
                    else
                    {
                        sCadena1 = dbCantidad.ToString();
                    }

                    string sCadena2 = " ";//se guarda cinco espacios
                    string sCadena3;

                    sCadena3 = Program.sNombreProductos[i].ToString();
                    //if (Program.sNombreProductos[i].Length < iAnchoDeDescripcion)
                    //{
                    //    sCadena3 =Program.sNombreProductos[i].PadRight(iAnchoDeDescripcion,' ');
                    //}
                    //else
                    //{
                    //    sCadena3 = Program.sNombreProductos[i].Substring(0, iAnchoDeDescripcion);//se guarda el nombre con 13 caractéres
                    //}
                    
                    string sCadena4 = " ";//se guarda tres espacios


                    string sCadena5 = ((dbCantidad * (dbPrecioUnitario - dbDescuento))).ToString("N2");

                    if (i == (Program.iCuenta - 1))
                    {
                        sCadena5 = (Convert.ToDouble(dbTotal1.ToString("N2")) - suma).ToString("N2");
                        if (Convert.ToDouble(sCadena5) < 0)
                        {
                            sCadena5 = "0.00";
                        }
                    }

                    Program.sCantidadProductos[i] = dtConsulta.Rows[i].ItemArray[2].ToString();
                    Program.dPreciosProductos[i] = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString()) - dbDescuento;

                    if (sCadena3.Length > 25)
                    {
                        sw.WriteLine(sCadena1.PadLeft(4, ' ' ) + sCadena2.PadRight(1, ' ') + sCadena3.Substring(0, 25).PadRight(25, ' ' ) + sCadena4.PadRight(1, ' ' ) + sCadena5.PadLeft(9, ' '));
                        sw.WriteLine(sCadena3.Substring(25).PadLeft((sCadena3.Substring(26).Length + 5), ' '));
                    }

                    else
                    {
                        sw.WriteLine(sCadena1.PadLeft(4, ' ' ) + sCadena2.PadRight(1, ' ') + sCadena3.PadRight(25, ' ' ) + sCadena4.PadRight(1, ' ' ) + sCadena5.PadLeft(9, ' '));
                    }

                   // texto = texto + Program.sCantidadProductos[i] + "  " + Program.sNombreProductos[i] + "      $" + ((dbCantidad * (dbPrecioUnitario - dbDescuento))).ToString("N2") +"       \n" + Environment.NewLine;
                    //sw.WriteLine(sCadena1.PadLeft(4, ' ' ) + sCadena2 + sCadena3 + sCadena4 + sCadena5);
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
                sw.WriteLine("         Gracias por su visita");
                sw.WriteLine("         DATOS PARA LA FACTURA");
                sw.WriteLine();
                sw.WriteLine("NOMBRES:");
                sw.WriteLine(".................................... ");
                sw.WriteLine("APELLIDOS:");
                sw.WriteLine(".................................... ");
                sw.WriteLine("CEDULA O RUC:");
                sw.WriteLine(".................................... ");
                sw.WriteLine("CORREO E.:");
                sw.WriteLine(".................................... ");
                sw.WriteLine("DIRECCION:");
                sw.WriteLine(".................................... ");
                sw.WriteLine("TELEFONO:");
                sw.WriteLine(".................................... ");
                sw.WriteLine("SU OPINION ES IMPORTANTE:");
                sw.WriteLine(".................................... ");
                sw.WriteLine(".................................... ");
                sw.WriteLine(".................................... ");
                sw.WriteLine(".................................... ");
            }

            else if (op == 2)
            {
                sw.WriteLine();
                sw.WriteLine();
                Double dValor;

                for (int i = 0; i < dtPagosClase.Rows.Count; i++)
                {                    
                    dValor = Convert.ToDouble(dtPagosClase.Rows[i].ItemArray[1].ToString());
                    sw.WriteLine(dtPagosClase.Rows[i].ItemArray[0].ToString().PadRight(15,' ')+ "  "+ dValor.ToString("N2"));
                }

                sw.WriteLine("CAMBIO:".PadRight(15,' ') + dtPagosClase.Rows[0].ItemArray[2].ToString());
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
                            subtotal = subtotal + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()) * (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[5].ToString())));
                            iva = iva + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[10].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()));
                            servicio = servicio + (Convert.ToDouble(dtConsulta.Rows[j].ItemArray[11].ToString()) * Convert.ToDouble(dtConsulta.Rows[j].ItemArray[4].ToString()));

                        }
                    }

                    sOrigen = dtConsulta.Rows[0].ItemArray[40].ToString();

                    sw = new StreamWriter(sPath);

                    sw.WriteLine(Program.local);
                    sw.WriteLine(Program.direccion);
                    //sw.WriteLine(Program.direccion1);
                    sw.WriteLine(Program.telefono1);
                    sw.WriteLine(Program.telefono2);
                    sw.WriteLine("Mesero: " + Program.nombreMesero + "         Estacion: 2");
                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("# de Cuenta: " + dtConsulta.Rows[0].ItemArray[38].ToString() + "          Orden:  " + dtConsulta.Rows[0].ItemArray[46].ToString());
                    sw.WriteLine("Tipo de Orden: " + sOrigen);

                    //AQUI CLASE PARA VALES FUNCIONARIOS, CONSUMO EMPLEADOS, MENU EXPRESS, CANJES

                    if ((Program.sIDPERSONA == null) || (Program.sIDPERSONA == "") || (Program.sIDPERSONA == "64930"))
                    {  }

                    else
                    {
                        
                    //}

                    //if (sOrigen == "MESAS")
                    //{
                    //    sw.WriteLine("# de Personas: " + dtConsulta.Rows[0].ItemArray[34].ToString() + "        MESA " + dtConsulta.Rows[0].ItemArray[29].ToString());
                    //}

                    //if ((Program.sCodigoAsignadoOrigenOrden == "03") || (Program.iDomicilioEspeciales == 1))
                    //{
                        //AQUI CREAMOS NUESTRA INSTRUCCION DE BUSQUEDA EN SQL
                        sSql = "select identificacion, apellidos, nombres, correo_electronico, codigo_alterno, id_persona from tp_personas where id_persona = '" + Program.sIDPERSONA + "'";
                        dtDomicilio = new DataTable();
                        dtDomicilio.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtDomicilio.Rows.Count > 0)
                            {
                                sw.WriteLine("----------------------------------------");
                                sw.WriteLine("CLIENTE:".PadRight(10, ' ') + dtDomicilio.Rows[0].ItemArray[1].ToString() + " " + dtDomicilio.Rows[0].ItemArray[2].ToString());
                                sw.WriteLine("CI/RUC:".PadRight(10, ' ') + dtDomicilio.Rows[0].ItemArray[0].ToString());
                                sw.WriteLine("TELEFONO:".PadRight(10, ' ') + dtDomicilio.Rows[0].ItemArray[4].ToString());

                                //iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[5].ToString());

                                sSql = "SELECT direccion, calle_principal, calle_interseccion, numero_vivienda, referencia FROM TP_DIRECCIONES where id_persona = " + Program.sIDPERSONA + " and estado = 'A'";
                                dtDomicilio = new DataTable();
                                dtDomicilio.Clear();
                                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

                                if (bRespuesta == true)
                                {
                                    if (dtDomicilio.Rows.Count > 0)
                                    {
                                        string sDireccionPrecuenta = dtDomicilio.Rows[0].ItemArray[1].ToString() + " " + dtDomicilio.Rows[0].ItemArray[3].ToString() + " " + dtDomicilio.Rows[0].ItemArray[2].ToString();
                                        if (sDireccionPrecuenta.Length <= 30)
                                        {
                                            sw.WriteLine("DIRECCION:" + sDireccionPrecuenta.ToString());
                                        }
                                        else
                                        {
                                            sw.WriteLine("DIRECCION:" + sDireccionPrecuenta.Substring(0, 30).PadRight(30, ' ')
                                                + Environment.NewLine + sDireccionPrecuenta.ToString().Substring(30).PadLeft((9 + sDireccionPrecuenta.ToString().Substring(30).Length), ' '));
                                        }

                                        //sw.WriteLine();

                                        sSql = "select top 1 referencia from tp_vw_direcciones where id_persona = " + Program.sIDPERSONA + " and estado = 'A'";
                                        dtDomicilio = new DataTable();

                                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDomicilio, sSql);

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
                                        else
                                        {
                                            sw.WriteLine("----------------------------------------");
                                            sw.WriteLine("Error".PadLeft(15,' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error en la consulta de direcion");
                                            sw.WriteLine("Informe al administrador");
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("Error".PadLeft(15, ' '));
                                            sw.WriteLine("----------------------------------------");
                                        }


                                    }
                                    
                                }
                            }
                        }
                    }
                    


                    //AQUI INCRUSTAR DATOS DEL CLIENTE

                    
                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("          >> ORDEN " + sEstado.ToUpper() + " <<");
                    sw.WriteLine("----------------------------------------");
                    
                    cargarProductos(Convert.ToInt32(sIdOrden));

                    sw.WriteLine("----------------------------------------");
                    sw.WriteLine("SUBTOTAL:".PadRight(31, ' ' ) +subtotal.ToString("N2").PadLeft(9, ' '));
                    sw.WriteLine("IVA " + (Program.iva * 100).ToString() + "%:".PadRight(31, ' ') + iva.ToString("N2").PadLeft(9, ' '));
                    sw.WriteLine("TOTAL DE LA ORDEN:".PadRight(31, ' ') + (subtotal + iva).ToString("N2").PadLeft(9, ' '));
                    sw.WriteLine(((Program.servicio * 100).ToString() + "% SERVICIO").PadRight(31, ' ') + servicio.ToString("N2").PadLeft(9, ' '));
                    sw.WriteLine("CANTIDAD DEBIDA:".PadRight(31, ' ') + (subtotal + iva + servicio).ToString("N2").PadLeft(9, ' '));
                    
                    sw.WriteLine();
                    sw.WriteLine("     Creado:   " + dtConsulta.Rows[0].ItemArray[32].ToString());


                    if ((sEstado == "Abierta") || (sEstado == "Pre-Cuenta") && (sOrigen != "DOMICILIOS"))
                    {
                        completarFactura(1);
                    }

                    else if (sEstado == "Pagada")
                    {
                        sw.WriteLine("     Pagada:   " + dtConsulta.Rows[0].ItemArray[32].ToString());
                        completarFactura(2);

                    }

                    else if (sEstado == "Cancelada")
                    {
                        completarFactura(0);
                    }

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
