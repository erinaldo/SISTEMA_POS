using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace Palatium.Clases
{
    public class InformePreciosProductos
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sNombre;

        DataTable dtConsulta;
        
        bool bRespuesta = false;
        
        StreamWriter sw;

        int iAnchoPalabraNombre = 48;
        int iAnchoPalabraPrecioUnitario = 15;
        int iAnchoPalabraPrecioTotal = 18;
        int iAnchoPalabraCategoria = 16;        
        int iImpresoraRollo = 0;
        int iAnchoNombreRollo = 31;
        int iAnchoPrecioRollo = 9;

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public InformePreciosProductos(int iImpresora  )
        {
            //Si la impresora es de rollo, recibe como parámetro 1
            this.iImpresoraRollo = iImpresora;
        }

        public void llenarInforme()
        {
            try
            {
                string sPath = @"c:\reportes\Precios.txt";
                //string sPath = (@"c:\reportes\Precios.xls");
                if (File.Exists(sPath))
                {
                    sSql = "";
                    sSql += "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre, subcategoria" + Environment.NewLine;
                    sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                    sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSql += "and P.id_producto_padre in (" + Environment.NewLine;
                    sSql += "select id_producto from cv401_productos" + Environment.NewLine;
                    sSql += "where codigo ='2')" + Environment.NewLine;
                    sSql += "and P.nivel = 2" + Environment.NewLine;
                    sSql += "and P.estado ='A'" + Environment.NewLine;
                    sSql += "and NP.estado='A'" + Environment.NewLine;
                    sSql += "and P.modificador <>1 ";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        sw = new StreamWriter(sPath);
                        sw.WriteLine();
                        if (iImpresoraRollo != 1)
                        {
                            sw.WriteLine("NOMBRE".PadRight(iAnchoPalabraNombre, ' ') + "PRECIO UNITARIO".PadRight(iAnchoPalabraPrecioUnitario, ' ') +
                                     "PRECIO TOTAL".PadLeft(iAnchoPalabraPrecioTotal, ' ') + "CATEGORIA".PadLeft(iAnchoPalabraCategoria, ' '));
                            sw.WriteLine();
                        }

                        if (dtConsulta.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtConsulta.Rows.Count; i++)
                            {     
                                int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                                string sCodigo = dtConsulta.Rows[i].ItemArray[1].ToString();
                                int iSubcategoria = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[3].ToString());
                                sNombre = dtConsulta.Rows[i].ItemArray[2].ToString();

                                
                                cargarNombres(sCodigo,iSubcategoria, iIdProducto);
                                sw.WriteLine();
                                if (iImpresoraRollo != 1)
                                {
                                    sw.WriteLine("*".PadRight(100, '*'));
                                }
                                else
                                {
                                    sw.WriteLine("*".PadRight(40, '*'));
                                }
                                

                            }
                            sw.Close();
                            goto fin;
                        }
                        else
                        {
                            VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                            mensaje.LblMensaje.Text = "NO EXISTEN PRODUCTOS REGISTRADOS";
                            mensaje.ShowInTaskbar = false;
                            mensaje.ShowDialog();
                        }

                    }
                    else
                        goto reversa;
                }


            }
            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }


            #region funciones de ayuda
        reversa:
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precios.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowInTaskbar = false;   
                mensaje.ShowDialog();
            }
            fin: 
            {

            }
            #endregion
        }

        //FUNCIÓN PARA CARGAR LOS PRODUCTOS
        public void cargarNombres(string sCodigo, int iSubcategoria, int iIdProductoPadre)
        {
            try
            {
                string sSql;

                if (iImpresoraRollo == 1)
                {
                    sw.WriteLine();
                    sw.WriteLine(sNombre);
                    sw.WriteLine();
                }

                if (iSubcategoria == 1)
                {
                    sSql = "";
                    sSql += "select P.id_producto,P.codigo as Codigo,NP.nombre as Nombre" + Environment.NewLine;
                    sSql += "from cv401_productos P,cv401_nombre_productos NP " + Environment.NewLine;
                    sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSql += "and id_producto_padre = " + iIdProductoPadre + Environment.NewLine;
                    sSql += "and P.nivel = 3" + Environment.NewLine;
                    sSql += "and P.estado='A'" + Environment.NewLine;
                    sSql += "and NP.estado = 'A'";

                    DataTable dtSubcategoria = new DataTable();
                    dtSubcategoria.Clear();
                    
                    bool bRespuestaSubcategoria = conexion.GFun_Lo_Busca_Registro(dtSubcategoria, sSql);
                    
                    if (bRespuestaSubcategoria == true)
                    {
                        for (int i = 0; i < dtSubcategoria.Rows.Count; i++)
                        {
                            int iIdProductoSubcategoria = Convert.ToInt32(dtSubcategoria.Rows[i].ItemArray[0].ToString());
                            string sCodigoSubcategoria = dtSubcategoria.Rows[i].ItemArray[1].ToString();
                            sNombre = dtSubcategoria.Rows[i].ItemArray[2].ToString();

                            cargarNombresSubcategoria(sCodigoSubcategoria);
                            sw.WriteLine();

                            if (iImpresoraRollo != 1)
                            {
                                sw.WriteLine("*".PadRight(100, '*'));
                            }
                            else
                            {
                                sw.WriteLine("*".PadRight(40, '*'));
                            }
                            
                            
                        }

                        goto fin;
                    }
                    else
                    {
                        goto reversa;
                    }

                }
                else
                {
                    sSql = "";
                    sSql += "select P.id_Producto, NP.nombre as Nombre" + Environment.NewLine;
                    sSql += "from cv401_productos P,cv401_nombre_productos NP " + Environment.NewLine;
                    sSql += "where P.id_Producto = NP.id_Producto" + Environment.NewLine;
                    sSql += "and P.id_Producto_padre in (" + Environment.NewLine;
                    sSql += "select id_Producto from cv401_productos" + Environment.NewLine;
                    sSql += "where codigo ='" + sCodigo + "')" + Environment.NewLine;
                    sSql += "and P.nivel = 3" + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and NP.estado = 'A'" + Environment.NewLine;
                    sSql += "and P.subcategoria = 0" + Environment.NewLine;
                    sSql += "and P.modificador = 0 " + Environment.NewLine;
                    sSql += "and P.ultimo_nivel = 1" + Environment.NewLine;
                    sSql += "order by secuencia";



                    DataTable dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtConsulta.Rows.Count; i++)
                            {
                                int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                                string sNombreProducto = dtConsulta.Rows[i].ItemArray[1].ToString();

                                string sSqlProducto = "select PR.valor from cv403_precios_productos PR " +
                                                    "inner join cv401_productos P on PR.id_producto = P.id_producto " +
                                                    " where id_lista_precio = 4  " +
                                                        "and P.id_producto = " + iIdProducto + " and PR.estado='A' order by secuencia";

                                DataTable dtConsultaProducto = new DataTable();
                                bool bRespuestaProducto = conexion.GFun_Lo_Busca_Registro(dtConsultaProducto, sSqlProducto);

                                if (bRespuestaProducto == true)
                                {
                                    double dbPrecioUnitario = Convert.ToDouble(dtConsultaProducto.Rows[0].ItemArray[0].ToString());
                                    Double dbPrecioTotal = (dbPrecioUnitario + (dbPrecioUnitario * (Program.iva + Program.servicio)));
                                    

                                    if (iImpresoraRollo != 1)
                                    {
                                        if (sNombreProducto.Length > iAnchoPalabraNombre)
                                        {
                                            //sNombreProducto = sNombreProducto.Substring(0, iAnchoPalabraNombre);
                                            sw.WriteLine(sNombreProducto.Substring(0, iAnchoPalabraNombre).PadRight(iAnchoPalabraNombre, ' ') + dbPrecioUnitario.ToString("N6").PadLeft(iAnchoPalabraPrecioUnitario, ' ') +
                                                  dbPrecioTotal.ToString("N2").PadLeft(iAnchoPalabraPrecioTotal, ' ') + " ".PadRight(7, ' ') + sNombre.PadRight(iAnchoPalabraCategoria, ' '));
                                            sw.WriteLine(sNombreProducto.Substring(iAnchoPalabraNombre));
                                            sw.WriteLine("-".PadRight(100, '-'));
                                        }
                                        else
                                        {
                                            sw.WriteLine(sNombreProducto.PadRight(iAnchoPalabraNombre, ' ') + dbPrecioUnitario.ToString("N6").PadLeft(iAnchoPalabraPrecioUnitario, ' ') +
                                                  dbPrecioTotal.ToString("N2").PadLeft(iAnchoPalabraPrecioTotal, ' ') + " ".PadRight(7, ' ') + sNombre.PadRight(iAnchoPalabraCategoria, ' '));
                                            sw.WriteLine("-".PadRight(100, '-'));
                                        }

                                        
                                    }
                                    else
                                    {
                                        if (sNombreProducto.Length > iAnchoNombreRollo)
                                        {

                                            sw.WriteLine(sNombreProducto.Substring(0, iAnchoNombreRollo).PadRight(iAnchoNombreRollo, ' ') + dbPrecioTotal.ToString("N2").PadLeft(iAnchoPrecioRollo, ' '));
                                            sw.WriteLine(sNombreProducto.Substring(iAnchoNombreRollo ));
                                            sw.WriteLine("-".PadRight(40, '-'));
                                            
                                        }
                                        else
                                        {
                                            sw.WriteLine(sNombreProducto.PadRight(iAnchoNombreRollo, ' ') + dbPrecioTotal.ToString("N2").PadLeft(iAnchoPrecioRollo, ' '));
                                            sw.WriteLine("-".PadRight(40, '-'));
                                        }
                                        
                                    }
                                    
                                }
                                else
                                    goto reversa;

                            }

                            goto fin;
                        }
                    }
                    else
                        goto reversa;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            #region Funciones de ayuda
        reversa:
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precios.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowInTaskbar = false;
                mensaje.ShowDialog();
            }
        fin:
            {

            }
            #endregion

        }

        //funcion para cargar los nombres de subcategorías
        private void cargarNombresSubcategoria(string sCodigoSubcategoria)
        {
            try
            {
                string  sSql = "select P.id_Producto, NP.nombre as Nombre from cv401_productos P,cv401_nombre_productos NP " +
                                       "where P.id_Producto = NP.id_Producto and P.id_Producto_padre " +
                                      " in (select id_Producto from cv401_productos where codigo ='" + sCodigoSubcategoria + "') " +
                                       "and P.nivel = 4 and P.estado ='A' and NP.estado='A' and P.subcategoria=0 and P.modificador=0 " +
                                       "and P.ultimo_nivel=1 order by secuencia";
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (iImpresoraRollo == 1)
                {
                    sw.WriteLine();
                    sw.WriteLine(sNombre);
                    sw.WriteLine();
                }


                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                        string sNombreProducto = dtConsulta.Rows[i].ItemArray[1].ToString();

                        string sSqlProducto = "select PR.valor from cv403_precios_productos PR " +
                                            "inner join cv401_productos P on PR.id_producto = P.id_producto " +
                                            " where id_lista_precio = 4  " +
                                                "and P.id_producto = " + iIdProducto + " and PR.estado='A' order by secuencia";

                        DataTable dtConsultaProducto = new DataTable();
                        bool bRespuestaProducto = conexion.GFun_Lo_Busca_Registro(dtConsultaProducto, sSqlProducto);

                        if (bRespuestaProducto == true)
                        {
                            double dbPrecioUnitario = Convert.ToDouble(dtConsultaProducto.Rows[0].ItemArray[0].ToString());
                            Double dbPrecioTotal = (dbPrecioUnitario + (dbPrecioUnitario * (Program.iva + Program.servicio)));
              
                            if (iImpresoraRollo != 1)
                            {

                                if (sNombreProducto.Length > iAnchoPalabraNombre)
                                {
                                    sw.WriteLine(sNombreProducto.Substring(0, iAnchoPalabraNombre).PadRight(iAnchoPalabraNombre, ' ') + dbPrecioUnitario.ToString("N6").PadLeft(iAnchoPalabraPrecioUnitario, ' ') +
                                                  dbPrecioTotal.ToString("N2").PadLeft(iAnchoPalabraPrecioTotal, ' ') + " ".PadRight(7, ' ') + sNombre.PadRight(iAnchoPalabraCategoria, ' '));
                                    sw.WriteLine(sNombreProducto.Substring(iAnchoPalabraNombre ));
                                    sw.WriteLine("-".PadRight(100, '-'));

                                }
                                else
                                {
                                    sw.WriteLine(sNombreProducto.PadRight(iAnchoPalabraNombre, ' ') + dbPrecioUnitario.ToString("N6").PadLeft(iAnchoPalabraPrecioUnitario, ' ') +
                                       dbPrecioTotal.ToString("N2").PadLeft(iAnchoPalabraPrecioTotal, ' ') + " ".PadRight(7, ' ') + sNombre.PadRight(iAnchoPalabraCategoria, ' '));
                                    sw.WriteLine("-".PadRight(100, '-')); 
                                }

                                
                            }
                            else
                            {
                                if (sNombreProducto.Length > iAnchoNombreRollo)
                                {
                                  //  sNombreProducto = sNombreProducto.Substring(0, iAnchoNombreRollo);
                                    sw.WriteLine(sNombreProducto.Substring(0, iAnchoNombreRollo).PadRight(iAnchoNombreRollo, ' ') + dbPrecioTotal.ToString("N2").PadLeft(iAnchoPrecioRollo, ' '));
                                    sw.WriteLine(sNombreProducto.Substring(iAnchoNombreRollo ));
                                    sw.WriteLine("-".PadRight(40, '-'));
                                }
                                else
                                {
                                    sw.WriteLine(sNombreProducto.PadRight(iAnchoNombreRollo, ' ') + dbPrecioTotal.ToString("N2").PadLeft(iAnchoPrecioRollo, ' '));
                                    sw.WriteLine("-".PadRight(40, '-'));
                                }

                            }
                            
                        }
                        else
                            goto reversa;
                    }

                    goto fin;
                }
                else
                    goto reversa;

            }
            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

             #region funciones de ayuda
        reversa:
            {
                VentanasMensajes.frmMensajeOK mensaje = new VentanasMensajes.frmMensajeOK();
                mensaje.LblMensaje.Text = "Ocurrió un problema al crear el reporte de precios.\nPor Favor, comuníquese con el administrador";
                mensaje.ShowInTaskbar = false;
                mensaje.ShowDialog();
            }
        fin:
            {

            }
            #endregion
        }


        //Fin de la clase
    }
}
