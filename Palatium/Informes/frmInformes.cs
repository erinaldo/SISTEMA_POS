using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Palatium.Informes
{
    public partial class frmInformes : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        StreamWriter sw;
        StreamWriter sw1;
        string sSql;
        bool bRespuesta = false;
        double dbTotalDineroEsperado = 0;

        public frmInformes()
        {
            InitializeComponent();
        }

        private void btnInformeMenuExpress_Click(object sender, EventArgs e)
        {
            Informes.informeDeVentas ventas = new informeDeVentas();
            ventas.Show();
            this.Close();
            //crearInformeMenuExpress();
        }

        private void crearInformeMenuExpress()
        {
            try
            {
                string sFechaCierreActual = DateTime.Now.ToString("dd/MM/yyyy");
               // string sPath = @"c:\reportes\MenuExpress.txt";
                string sPath = (@"c:\reportes\MenuExpress.txt");
                double dbTotalMenuExpress = 0;
                string sFecha=DateTime.Now.ToString("yyyy-MM-dd");

                if (File.Exists(sPath))
                {
                    
                    sSql = "select NUM.numero_pedido, CAB.fecha_ingreso, sum(DET.cantidad *(DET.precio_unitario + DET.valor_iva + Det.valor_otro)) Suma " +
                            "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET " +
                            "on CAB.id_pedido = DET.id_pedido inner join cv403_numero_cab_pedido NUM " +
                            "on CAB.id_pedido = NUM.id_pedido " +
                            "where id_pos_origen_orden = 1 "+
                            "and CAB.fecha_orden = '"+sFecha+"' " +
                            "group by NUM.numero_pedido, CAB.fecha_ingreso";

                    string  sSql1 = "select descripcion from pos_origen_orden " +
                                      "where id_pos_origen_orden = 1";
                    

                    DataTable dtNombreOrigen = new DataTable();
                    dtNombreOrigen.Clear();
                    bool bRespuesta1 = conexion.GFun_Lo_Busca_Registro(dtNombreOrigen, sSql1);

                    DataTable dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (bRespuesta == true)
                    {
                        sw = new StreamWriter(sPath);

                        if (dtConsulta.Rows.Count > 0)
                        {


                            sw.WriteLine();
                            sw.WriteLine("*".PadRight(40, '*'));
                            sw.WriteLine("INFORME DE VENTAS ".PadLeft(25, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                            sw.WriteLine();
                            sw.WriteLine("ENTRADA: ".PadRight(10, ' ') + sFechaCierreActual.PadLeft(30, ' '));
                            sw.WriteLine();
                            sw.WriteLine("*".PadRight(40, '*'));
                            sw.WriteLine("NUMERO DE ORDENES: ".PadRight(31, ' ') + SacarTotalOrdenes().PadLeft(9, ' '));
                            sw.WriteLine();
                            sw.WriteLine("=".PadRight(40, '='));
                            sw.WriteLine("ORDENES ".PadLeft(20, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                            sw.WriteLine("=".PadRight(40, '='));
                            sw.WriteLine("# DE ORDEN".PadRight(18, ' ') + "HORA".PadRight(17) + "TOTAL".PadLeft(5, ' '));

                            for (int i = 0; i < dtConsulta.Rows.Count; i++)
                            {
                                int iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                                string sFechaIngreso = dtConsulta.Rows[i].ItemArray[1].ToString();
                                double dbTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                                sw.WriteLine(iNumeroPedido.ToString().PadLeft(8, ' ') + " ".PadRight(8, ' ') +
                                            sFechaIngreso.PadLeft(12, ' ').Substring(10) + dbTotal.ToString("N2").PadLeft(15, ' '));
                                //Variable para sumar el total de ordenes de menú express
                                dbTotalMenuExpress += Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());

                            }

                            sw.WriteLine();
                            sw.WriteLine("---------".PadLeft(40, ' '));
                            sw.WriteLine("TOTAL DE LAS ORDENES: ".PadRight(30, ' ') + "$" + dbTotalMenuExpress.ToString("N2").PadLeft(9, ' '));

                            sw.Close();

                        }
                        else
                        {
                            sw.WriteLine();
                            sw.WriteLine("*".PadRight(40, '*'));
                            sw.WriteLine("INFORME DE VENTAS ".PadLeft(25, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                            sw.WriteLine();
                            sw.WriteLine("ENTRADA: ".PadRight(10, ' ') + sFechaCierreActual.PadLeft(30, ' '));
                            sw.WriteLine();
                            sw.WriteLine("*".PadRight(40, '*'));
                            sw.WriteLine("NUMERO DE ORDENES: ".PadRight(31, ' ') + SacarTotalOrdenes().PadLeft(9, ' '));
                            sw.WriteLine();
                            sw.WriteLine("=".PadRight(40, '='));
                            sw.WriteLine("ORDENES ".PadLeft(20, ' ') + dtNombreOrigen.Rows[0].ItemArray[0].ToString());
                            sw.WriteLine("=".PadRight(40, '='));
                            sw.WriteLine();
                            sw.WriteLine("*".PadRight(40, '*'));
                            sw.WriteLine("NO SE HAN REGISTRADO ÓRDENES".PadLeft(30, ' '));
                            sw.WriteLine("*".PadRight(40, '*'));
                            sw.Close();
                        }


                    }
                    // System.Diagnostics.Process.Start(@"C:\reportes\imprimir.bat");

                   
                    

                }
            }
            catch(Exception)
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al cargar el informe de Menú Express.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowDialog();
            }
            
        }

        //Función para sacar el total de ordenes de menú express
        private string SacarTotalOrdenes()
        {
            try
            {
                string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                sSql = "select COUNT(*) from cv403_cab_pedidos where fecha_orden = '"+fechaActual+"' "+
                        "AND ID_POS_ORIGEN_ORDEN = 1 AND ID_LOCALIDAD = "+Program.iIdLocalidad;
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()) > 0)
                    {
                        return dtConsulta.Rows[0].ItemArray[0].ToString();
                    }
                    else
                        goto reversa;
                }
                else
                    goto reversa;
          
            }
            catch(Exception)
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al cargar el informe de Menú Express.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                return "0";
            }
        }

        //Función para crear el informe de responsabilidad del cajero
        private void crearInformeResponsabilidadCajero()
        {
            try
            {
                string sFechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                string sPath = @"c:\reportes\InformeResponsabilidad.txt";
                
                if (File.Exists(sPath))
                {
                    sSql = "select CAB.id_pedido,NUM.numero_pedido, CAB.fecha_ingreso , " +
                        "sum( DET.cantidad * (DET.precio_unitario +DET.valor_dscto + DET.valor_iva+ DET.valor_otro)) " +
                        "from cv403_cab_pedidos CAB inner join cv403_det_pedidos DET " +
                        "on CAB.id_pedido = DET.id_pedido inner join cv403_numero_cab_pedido NUM " +
                        "on CAB.id_pedido = NUM.id_pedido " +
                        "where CAB.fecha_orden = '"+sFechaActual+"' " +
                        "and CAB.ID_LOCALIDAD = "+Program.iIdLocalidad+" and CAB.estado_orden ='Pagada' " +
                        "and CAB.id_pos_origen_orden in (1,2,3) and DET.estado='A'and CAB.id_pos_jornada = 1 " +
                        "group by CAB.id_pedido, NUM.numero_pedido,CAB.fecha_ingreso ";

                    DataTable dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            sw = new StreamWriter(sPath);
                            sw.WriteLine();
                            sw.WriteLine("INFORME DE RESPONSABILIDAD DEL CAJERO".PadLeft(38,' '));
                            sw.WriteLine();
                            //Función para buscar el nombre del cajero
                            buscarNombreCajero(sFechaActual);
                            //************************************
                            sw.WriteLine();
                            sw.WriteLine("=".PadRight(40,'='));
                            sw.WriteLine("PAGO DE ORDENES".PadLeft(27,' '));
                            sw.WriteLine("=".PadRight(40, '='));
                            //Función para agregar el detalle de las órdenes
                            for (int i = 0; i < dtConsulta.Rows.Count; i++)
                            {
                                int iNumeroOrden = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[1].ToString());
                                string sHora = dtConsulta.Rows[i].ItemArray[2].ToString().Substring(11);
                                double dbTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());
                                int iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                                dbTotalDineroEsperado += dbTotal;

                                sw.WriteLine("# DE ORDEN: ".PadRight(12,' ')+ iNumeroOrden.ToString().PadRight(6, ' ')+sHora.PadLeft(10,' ')+
                                            dbTotal.ToString("N2").PadLeft(13, ' '));
                                
                                agregarDetallePagoOrdenes(iIdPedido);
                            }

                            sw.WriteLine("---------".PadLeft(40,' '));
                            sw.WriteLine("TOTAL DE PAGO DE ORDENES: ".PadRight(30, ' ')+"$"+dbTotalDineroEsperado.ToString("N2").PadLeft(9,' '));
                            sw.WriteLine();
                            //Función para llenar el resumen del dinero esperado
                            llenarResumenDineroEsperado(sFechaActual);
                            sw.WriteLine();
                            //Función para calcular el total del propinas
                            sw.WriteLine("=".PadRight(40, '='));
                            sw.WriteLine("RESUMEN DE LAS PROPINAS".PadLeft(35,' '));
                            sw.WriteLine("=".PadRight(40, '='));
                            sw.WriteLine("TOTAL DE PROPINAS: ".PadRight(30, ' ') + "$" + calcularTotalPropinas().ToString("N2").PadLeft(9,' '));
                            sw.Close();

                        }
                    }
                    else
                    {
                        VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                        mensaje.LblMensaje.Text = "Ocurrió un problema al cargar el informe de Responsabilidad.\nPor Favor, comuníquese con el administrador";
                        mensaje.ShowDialog();
                    }
                }

            }
            catch(Exception)
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al cargar el informe de Responsabilidad.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowDialog();
            }

        }

        //Función para buscar el nombre del Cajero
        private void buscarNombreCajero(string sFechaActual)
        {
            try
            {
                string sSql = "select usuario_ingreso, fecha_ingreso from pos_cierre_cajero " +
                                "where id_localidad = " + Program.iIdLocalidad + " and id_jornada = 1 " +
                                "and fecha_apertura = '" + sFechaActual + "'";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sw.WriteLine("CAJERO: ".PadRight(13,' ')+dtConsulta.Rows[0].ItemArray[0].ToString().PadLeft(27,' '));
                        //La estación está puesta por defecto
                        sw.WriteLine("ESTACIÓN:".PadRight(13, ' ') +"1".PadLeft(27,' '));
                       //************************************************************
                        sw.WriteLine("ENTRADA:".PadRight(13, ' ') + dtConsulta.Rows[0].ItemArray[1].ToString().PadLeft(27, ' '));
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error al buscar el nombre del cajero");
            }
        }

        //Función para agregar el detalle del pago de órdenes
        private void agregarDetallePagoOrdenes(int iIdPedido)
        {
            try 
            {
                string sFecha = DateTime.Now.ToString("yyyy-MM-dd");

                string sSql = "select descripcion, sum(valor) valor, cambio, sum(valor+cambio) from pos_vw_pedido_forma_pago "+
                              "where id_pedido = "+iIdPedido+"  "+
                              "group by descripcion, valor, cambio having count(*) >= 1";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        string sSqlPropinas = "select propina from cv403_pagos PAG , "+
                                            "cv403_documentos_pagados DPAGA,CV403_DCTOS_POR_COBRAR AS XC, cv403_cab_pedidos CP "+
                                                "where DPAGA.id_pago = PAG.id_pago  "+
                                                "and DPAGA.id_documento_cobrar = XC.id_documento_cobrar "+
                                                "and XC.id_pedido = CP.id_pedido "+
                                                "and CP.fecha_pedido ='" + sFecha + "' " +
                                                "and CP.id_pos_jornada = 1 and CP.estado='A' and  CP.id_pedido= "+iIdPedido;

                        DataTable dtPropinas = new DataTable();
                        dtPropinas.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtPropinas, sSqlPropinas);

       
                            for (int i = 0; i < dtConsulta.Rows.Count; i++)
                            {
                                double dbPropinas = 0;
                                string sFormaPago = dtConsulta.Rows[i].ItemArray[0].ToString();
                                Double sTotalPagado = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[3].ToString());

                                if (dtPropinas.Rows.Count <= 0)
                                {
                                    dbPropinas = 0.00;
                                }
                                else
                                { 
                                      dbPropinas = Convert.ToDouble(dtPropinas.Rows[i].ItemArray[0].ToString());
                                }
                                
                                sw.WriteLine("".PadRight(2, ' ') + sFormaPago.PadRight(15, ' ') + "PAGADA"
                                        + ("$" + sTotalPagado.ToString("N2")).ToString().PadLeft(9, ' '));
                                sw.WriteLine("".PadRight(2, ' ') + "(Propinas: " + dbPropinas.ToString("N2").PadLeft(6, ' ') + ")");
                            }
                        
                        
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error al buscar el nombre del cajero");
            }
        }

        private void btnResponsabilidad_Click(object sender, EventArgs e)
        {
            crearInformeResponsabilidadCajero();
        }

        //Función para llenar el resumen del dinero esperado
        private void llenarResumenDineroEsperado(string sFechaActual)
        {
            try
            { 
                //Mandar por parámetro la jornada y la fecha
                double dbTotalCargos = 0;
                string  sSql = "select FP.descripcion,sum(FP.valor) valor  from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP,  "+
                            "pos_vw_pedido_forma_pago FP  "+
                            "where CP.fecha_pedido ='"+sFechaActual+"' and CP.id_pos_jornada = 1  and CP.id_pedido = NP.id_pedido "+
                            "and CP.id_pedido = FP.id_pedido  group by FP.descripcion";
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sw.WriteLine("=".PadRight(40, '='));
                        sw.WriteLine("RESUMEN DEL DINERO ESPERADO".PadLeft(33, ' '));
                        sw.WriteLine("=".PadRight(40, '='));

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreTarjeta = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbTotalTarjeto = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                            dbTotalCargos += dbTotalTarjeto;
                            sw.WriteLine(" ".PadRight(4,' ')+sNombreTarjeta.PadRight(26,' ')+dbTotalTarjeto.ToString("N2").PadLeft(10,' '));
                        }

                        sw.WriteLine("----------".PadLeft(40,' '));
                        sw.WriteLine("TOTAL DE LOS CARGOS".PadRight(29,' ')+"$".PadRight(1,' ')+dbTotalCargos.ToString("N2").PadLeft(10,' '));
                    }
                    else
                    {
                        sw.WriteLine("NO HAY DATOS PARA MOSTRAR");
                    }
                        
                }


            }
            catch(Exception)
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al cargar el resumen del dinero esperado.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowDialog();
            }

        }

        //Función para retornar el total de propinas
        private double calcularTotalPropinas()
        {
            try
            {
                string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                sSql = "select isnull(sum(PAG.propina),0) propina  "+
                    "from cv403_pagos PAG ,  cv403_documentos_pagados DPAGA,CV403_DCTOS_POR_COBRAR AS XC, cv403_cab_pedidos CP "+
                        "where DPAGA.id_pago = PAG.id_pago  "+
                    "and DPAGA.id_documento_cobrar = XC.id_documento_cobrar "+ 
                        "and XC.id_pedido = CP.id_pedido "+
                        "and CP.fecha_pedido ='" + fechaActual + "' " +
                    "and CP.id_pos_jornada = 1 and CP.estado='A'";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta=conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
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
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al cargar el total de propinas.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowDialog();
                return 0;
            }
        }

        private void btnInformeProductos_Click(object sender, EventArgs e)
        {
            Clases.InformePreciosProductos informe = new Clases.InformePreciosProductos(0);
            informe.llenarInforme();
        }

        private void btnImpresoraRollo_Click(object sender, EventArgs e)
        {
            Clases.InformePreciosProductos informe = new Clases.InformePreciosProductos(1);
            informe.llenarInforme();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            Informes.frmInformeProductos productos = new frmInformeProductos();
            productos.Show();
            this.Close();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            Informes.frmInformeVentasCategorias categorias = new frmInformeVentasCategorias();
            categorias.Show();
            this.Close();
        }

        //Fin de la Clase
    }
}
