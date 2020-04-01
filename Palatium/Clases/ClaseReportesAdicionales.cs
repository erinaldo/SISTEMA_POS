using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseReportesAdicionales
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sTexto;
        string sNombreCategoria;
        string sNombreProducto;
        string sPrecioProducto;
        string sRespuestaTexto;

        int iIdLocalidad;
        int iIdProducto;
        int iCantidadProductos;
        int iIdTipoProducto;
        int iIdPosCierreCajeroParametro;

        DataTable dtConsulta;
        DataTable dtAyuda;

        bool bRespuesta;

        Decimal dbTotalCategoria;
        Decimal dbPrecioProducto;
        Decimal dbTotalClienteEmpresarial;
        Decimal dbTotalTarjetaAlmuerzo;
        Decimal dbCajaInicial;
        Decimal dbCajaFinal;
        Decimal dbSumaMonedas_P;
        Decimal dbDiferencia;
        Decimal dbCobradoEfectivo;
        Decimal dbEntradasManuales;
        Decimal dbSalidasManuales;
        Decimal dbTotalVentas;
        Decimal dbTotalGastos;

        SqlParameter[] parametro;

        public string crearReporte(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            try
            {
                this.iIdLocalidad = iIdLocalidad_P;
                this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;

                dbTotalClienteEmpresarial = 0;
                dbTotalTarjetaAlmuerzo = 0;

                sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "    DETALLE DE PRODUCTOS DESPACHADOS" + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                sRespuestaTexto = itemAlmuerzos();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }

                sRespuestaTexto = itemAlmuerzosCodigo("12");

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }


                sRespuestaTexto = itemAlmuerzosCodigo("13");

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }

                sRespuestaTexto = itemDesayunos();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }

                sRespuestaTexto = itemsPorTipoProducto();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }

                //AQUI INTEGRAMOS LAS CORTESIAS
                //-------------------------------------------------------------------------------------------
                sRespuestaTexto = productosCortesia();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }
                //-------------------------------------------------------------------------------------------

                extraerCajas();

                sTexto += Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "RESUMEN DE VALORES".PadLeft(29, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "CAJA INICIAL: " + dbCajaInicial.ToString("N2") + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                sRespuestaTexto = resumenCaja();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto;
                }

                sTexto += "CLIENTE EMPRESARIAL:".PadRight(27, ' ') + dbTotalClienteEmpresarial.ToString("N2").PadLeft(13, ' ') + Environment.NewLine;
                sTexto += "TARJETA DE ALMUERZO:".PadRight(27, ' ') + dbTotalTarjetaAlmuerzo.ToString("N2").PadLeft(13, ' ') + Environment.NewLine;

                dbEntradasManuales = sumarEntradasSalidasManuales(1);
                dbSalidasManuales = sumarEntradasSalidasManuales(0);

                sTexto += "INGRESOS MANUALES:".PadRight(27, ' ') + dbEntradasManuales.ToString("N2").PadLeft(13, ' ') + Environment.NewLine;
                sTexto += "SALIDAS MANUALES:".PadRight(27, ' ') + dbSalidasManuales.ToString("N2").PadLeft(13, ' ') + Environment.NewLine;

                sRespuestaTexto = contarMonedas();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "TOTAL EN VENTAS: " + dbTotalVentas.ToString("N2") + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += sRespuestaTexto;

                    dbCobradoEfectivo = totalEfectivo();

                    dbDiferencia = dbCobradoEfectivo + dbEntradasManuales - dbSalidasManuales;
                    dbDiferencia = dbSumaMonedas_P - dbDiferencia; 

                    sTexto += "DIFERENCIA EN CAJA: " + dbDiferencia.ToString("N2") + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "NOTA: PARA LA CAJA FINAL NO SE TOMA EN" +Environment.NewLine;
                    sTexto += "CUENTA EL VALOR DE CAJA INICIAL" + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                }

                else 
                {
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                    sTexto += "TOTAL EN VENTAS: " + dbTotalVentas.ToString("N2") + Environment.NewLine;
                    sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                }

                //AQUI INTEGRAMOS LOS GASTOS
                //-------------------------------------------------------------------------------------------
                sRespuestaTexto = mostrarGastos();

                if (sRespuestaTexto == "ERROR")
                {
                    return "ERROR";
                }

                if (sRespuestaTexto != "SN")
                {
                    sTexto += sRespuestaTexto + Environment.NewLine;
                }
                //-------------------------------------------------------------------------------------------

                return sTexto;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA RECUPERAR EL ITEM ALMUERZOS
        private string itemAlmuerzos()
        {
            try
            {
                string sRetorno = "";

                sSql = "";
                sSql += "select P.id_producto, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;
                sSql += "and P.detalle_por_origen = 1" + Environment.NewLine;
                sSql += "and P.detalle_independiente = 0";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdProducto = Convert.ToInt32(dtAyuda.Rows[i]["id_producto"].ToString());
                    sNombreCategoria = dtAyuda.Rows[i]["nombre"].ToString().Trim().ToUpper();

                    sRetorno += "CATEGORÍA: " + sNombreCategoria + Environment.NewLine;
                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                    sRetorno += "PRODUCTOS COBRADOS" + Environment.NewLine;
                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                    sSql = "";
                    sSql += "select NP.nombre, sum(DP.cantidad) cantidad," + Environment.NewLine;
                    sSql += "ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                    sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_det_pedidos DP ON P.id_producto = DP.id_producto" + Environment.NewLine;
                    sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                    sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                    sSql += "and O.estado = 'A'" + Environment.NewLine;
                    sSql += "where CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and O.genera_factura = 1" + Environment.NewLine;
                    sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and P.id_producto_padre = " + iIdProducto + Environment.NewLine;
                    sSql += "group by NP.nombre";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    if (dtConsulta.Rows.Count == 0)
                    {
                        sRetorno += "SIN INFORMACION";
                    }

                    else
                    {
                        dbTotalCategoria = 0;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j]["nombre"].ToString().Trim().ToUpper();
                            iCantidadProductos = Convert.ToInt32(dtConsulta.Rows[j]["cantidad"].ToString());
                            sPrecioProducto = dtConsulta.Rows[j]["total"].ToString();
                            dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[j]["total"].ToString());

                            if (sNombreProducto.Length > 22)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 22);
                            }

                            sRetorno += sNombreProducto.PadRight(22, ' ') + iCantidadProductos.ToString("N0").PadLeft(5, ' ') + sPrecioProducto.PadLeft(13, ' ') + Environment.NewLine;

                            dbTotalCategoria += dbPrecioProducto;
                        }

                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                        sRetorno += "TOTAL REPORTADO:".PadRight(27, ' ') + dbTotalCategoria.ToString("N2").PadLeft(13, ' ') + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }

                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PAR ARECUPERAR EL ITEM ALMUERZO DEL CLIENTE EMPRESARIAL
        private string itemAlmuerzosCodigo(string sCodigo_P)
        {
            try
            {
                string sRetorno = "";

                sSql = "";
                sSql += "select P.id_producto, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;
                sSql += "and P.detalle_por_origen = 1" + Environment.NewLine;
                sSql += "and P.detalle_independiente = 0";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdProducto = Convert.ToInt32(dtAyuda.Rows[i]["id_producto"].ToString());
                    sNombreCategoria = dtAyuda.Rows[i]["nombre"].ToString().Trim().ToUpper();

                    //sRetorno += "CATEGORÍA: " + sNombreCategoria + Environment.NewLine;
                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                    if (sCodigo_P == "12")
                    {
                        sRetorno += "CLIENTE EMPRESARIAL" + Environment.NewLine;
                    }

                    else
                    {
                        sRetorno += "TARJETA DE ALMUERZO" + Environment.NewLine;
                    }
                    
                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                    sSql = "";
                    sSql += "select NP.nombre, sum(DP.cantidad) cantidad," + Environment.NewLine;
                    sSql += "ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                    sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_det_pedidos DP ON P.id_producto = DP.id_producto" + Environment.NewLine;
                    sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                    sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                    sSql += "and O.estado = 'A'" + Environment.NewLine;
                    sSql += "where CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and O.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                    sSql += "and P.id_producto_padre = " + iIdProducto + Environment.NewLine;
                    sSql += "group by NP.nombre";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    if (dtConsulta.Rows.Count == 0)
                    {
                        sRetorno += "SIN INFORMACION" + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }

                    else
                    {
                        dbTotalCategoria = 0;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j]["nombre"].ToString().Trim().ToUpper();
                            iCantidadProductos = Convert.ToInt32(dtConsulta.Rows[j]["cantidad"].ToString());
                            sPrecioProducto = dtConsulta.Rows[j]["total"].ToString();
                            dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[j]["total"].ToString());

                            if (sNombreProducto.Length > 22)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 22);
                            }

                            sRetorno += sNombreProducto.PadRight(22, ' ') + iCantidadProductos.ToString("N0").PadLeft(5, ' ') + sPrecioProducto.PadLeft(13, ' ') + Environment.NewLine;

                            dbTotalCategoria += dbPrecioProducto;
                        }

                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                        sRetorno += "TOTAL REPORTADO:".PadRight(27, ' ') + dbTotalCategoria.ToString("N2").PadLeft(13, ' ') + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                        if (sCodigo_P == "12")
                        {
                            dbTotalClienteEmpresarial = dbTotalCategoria;
                        }

                        else if (sCodigo_P == "13")
                        {
                            dbTotalTarjetaAlmuerzo = dbTotalCategoria;
                        }
                    }
                }

                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA RECUPERAR EL ITEM DESAYUNOS
        private string itemDesayunos()
        {
            try
            {
                string sRetorno = "";

                sSql = "";
                sSql += "select P.id_producto, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "where P.nivel = 2" + Environment.NewLine;
                sSql += "and P.menu_pos = 1" + Environment.NewLine;
                sSql += "and P.detalle_por_origen = 0" + Environment.NewLine;
                sSql += "and P.detalle_independiente = 1";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdProducto = Convert.ToInt32(dtAyuda.Rows[i]["id_producto"].ToString());
                    sNombreCategoria = dtAyuda.Rows[i]["nombre"].ToString().Trim().ToUpper();

                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                    sRetorno += "CATEGORÍA: " + sNombreCategoria + Environment.NewLine;
                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                    sRetorno += "PRODUCTOS COBRADOS" + Environment.NewLine;
                    sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                    sSql = "";
                    sSql += "select NP.nombre, sum(DP.cantidad) cantidad," + Environment.NewLine;
                    sSql += "ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                    sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_det_pedidos DP ON P.id_producto = DP.id_producto" + Environment.NewLine;
                    sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                    sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                    sSql += "and O.estado = 'A'" + Environment.NewLine;
                    sSql += "where CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and O.genera_factura = 1" + Environment.NewLine;
                    sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and P.id_producto_padre = " + iIdProducto + Environment.NewLine;
                    sSql += "group by NP.nombre";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    if (dtConsulta.Rows.Count == 0)
                    {
                        sRetorno += "SIN INFORMACION";
                    }

                    else
                    {
                        dbTotalCategoria = 0;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j]["nombre"].ToString().Trim().ToUpper();
                            iCantidadProductos = Convert.ToInt32(dtConsulta.Rows[j]["cantidad"].ToString());
                            sPrecioProducto = dtConsulta.Rows[j]["total"].ToString();
                            dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[j]["total"].ToString());

                            if (sNombreProducto.Length > 22)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 22);
                            }

                            sRetorno += sNombreProducto.PadRight(22, ' ') + iCantidadProductos.ToString("N0").PadLeft(5, ' ') + sPrecioProducto.PadLeft(13, ' ') + Environment.NewLine;

                            dbTotalCategoria += dbPrecioProducto;
                        }

                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                        sRetorno += "TOTAL REPORTADO:".PadRight(27, ' ') + dbTotalCategoria.ToString("N2").PadLeft(13, ' ') + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                    }
                }

                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA SEPARAR LOS ITEMS ADICIONALES
        private string itemsPorTipoProducto()
        {
            try
            {
                string sRetorno = "";

                sSql = "";
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtAyuda.Rows.Count == 0)
                {
                    return "SN";
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    sNombreCategoria = dtAyuda.Rows[i]["descripcion"].ToString().Trim().ToUpper();
                    iIdTipoProducto = Convert.ToInt32(dtAyuda.Rows[i]["id_pos_tipo_producto"].ToString());

                    sSql = "";
                    sSql += "select NP.nombre, sum(DP.cantidad) cantidad," + Environment.NewLine;
                    sSql += "ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                    sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                    sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                    sSql += "and P.estado = 'A'" + Environment.NewLine;
                    sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_det_pedidos DP ON P.id_producto = DP.id_producto" + Environment.NewLine;
                    sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                    sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                    sSql += "cv401_productos PADRE ON PADRE.id_producto = P.id_producto_padre" + Environment.NewLine;
                    sSql += "and PADRE.estado = 'A'" + Environment.NewLine;
                    sSql += "where CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                    sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                    sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                    sSql += "and PADRE.detalle_por_origen = 0" + Environment.NewLine;
                    sSql += "and PADRE.detalle_independiente = 0" + Environment.NewLine;
                    sSql += "and P.id_pos_tipo_producto = " + iIdTipoProducto + Environment.NewLine;
                    sSql += "group by NP.nombre" + Environment.NewLine;
                    sSql += "order by DP.cantidad, NP.nombre";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        return "ERROR";
                    }

                    if (dtConsulta.Rows.Count > 0)
                    {
                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                        sRetorno += "TIPO PRODUCTO: " + sNombreCategoria + Environment.NewLine;
                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                        dbTotalCategoria = 0;

                        for (int j = 0; j < dtConsulta.Rows.Count; j++)
                        {
                            sNombreProducto = dtConsulta.Rows[j]["nombre"].ToString().Trim().ToUpper();
                            iCantidadProductos = Convert.ToInt32(dtConsulta.Rows[j]["cantidad"].ToString());
                            sPrecioProducto = dtConsulta.Rows[j]["total"].ToString();
                            dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[j]["total"].ToString());

                            if (sNombreProducto.Length > 22)
                            {
                                sNombreProducto = sNombreProducto.Substring(0, 22);
                            }

                            sRetorno += sNombreProducto.PadRight(22, ' ') + iCantidadProductos.ToString("N0").PadLeft(5, ' ') + sPrecioProducto.PadLeft(13, ' ') + Environment.NewLine;

                            dbTotalCategoria += dbPrecioProducto;
                        }

                        sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                        sRetorno += "TOTAL REPORTADO:".PadRight(27, ' ') + dbTotalCategoria.ToString("N2").PadLeft(13, ' ') + Environment.NewLine + Environment.NewLine;
                    }
                }

                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        public string resumenCaja()
        {
            try
            {
                string sRetorno = "";
                dbTotalVentas = 0;

                sSql = "";
                sSql += "select ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.codigo in ('EF', 'TR', 'CH')" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    dbTotalVentas += Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString().Trim());
                    sRetorno += "EFECTIVO:".PadRight(27, ' ') + dtConsulta.Rows[0]["valor"].ToString().Trim().PadLeft(13, ' ') + Environment.NewLine;
                }

                sSql = "";
                sSql += "select ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.codigo in ('TC', 'TD')" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    dbTotalVentas += Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString().Trim());
                    sRetorno += "TARJETAS:".PadRight(27, ' ') + dtConsulta.Rows[0]["valor"].ToString().Trim().PadLeft(13, ' ') + Environment.NewLine;
                }


                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA OBTENER EL VALOR DE LA ENTRADAS Y SALIDAS MANUALES
        private Decimal sumarEntradasSalidasManuales(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(valor), 0) suma" + Environment.NewLine;
                sSql += "from pos_movimiento_caja  " + Environment.NewLine;
                sSql += "where estado = 'A' " + Environment.NewLine;
                sSql += "and tipo_movimiento = " + iOp + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and id_documento_pago is null" + Environment.NewLine;
                }

                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToDecimal(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        //FUNCION PARA EXTRAER LA CAJA INICIAL Y FINAL
        private bool extraerCajas()
        {
            try
            {
                dbCajaInicial = 0;
                dbCajaFinal = 0;

                sSql = "";
                sSql += "select ltrim(str(isnull(caja_inicial, 0), 10, 2)) caja_inicial," + Environment.NewLine;
                sSql += "ltrim(str(isnull(caja_final, 0), 10, 2)) caja_final" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    dbCajaInicial = Convert.ToDecimal(dtConsulta.Rows[0]["caja_inicial"].ToString());
                    dbCajaFinal = Convert.ToDecimal(dtConsulta.Rows[0]["caja_final"].ToString());
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA CONTAR LAS MONEDAS
        public string contarMonedas()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_monedas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and tipo_ingreso = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                Decimal iMonedas;
                Decimal dbValorCalculo;
                dbSumaMonedas_P = 0;

                string sMoneda = "" + Environment.NewLine;
                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;
                sMoneda += "RESUMEN DE MONEDAS Y BILLETES".PadLeft(35, ' ') + Environment.NewLine;
                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda01"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.01");
                sMoneda += "1   CENTAVO".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda05"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.05");
                sMoneda += "5   CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda10"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.10");
                sMoneda += "10  CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda25"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.25");
                sMoneda += "25  CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["moneda50"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("0.50");
                sMoneda += "50  CENTAVOS".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete1"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("1");
                sMoneda += "1   DOLAR".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete2"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("2");
                sMoneda += "2   DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete5"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("5");
                sMoneda += "5   DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete10"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("10");
                sMoneda += "10  DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete20"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("20");
                sMoneda += "20  DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete50"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("50");
                sMoneda += "50  DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                iMonedas = Convert.ToDecimal(dtConsulta.Rows[0]["billete100"].ToString());
                dbValorCalculo = iMonedas * Convert.ToDecimal("100");
                sMoneda += "100 DOLARES".PadRight(20, ' ') + iMonedas.ToString().PadLeft(10, ' ') + dbValorCalculo.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                dbSumaMonedas_P += dbValorCalculo;

                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;
                sMoneda += "TOTAL EN EFECTIVO DE CAJA:".PadRight(30, ' ') + dbSumaMonedas_P.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                sMoneda += "".PadLeft(40, '-') + Environment.NewLine;

                return sMoneda;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA EXTRAER EL VALOR COBRADO EN EFECTIVO
        public Decimal totalEfectivo()
        {
            try
            {
                sSql = "";
                sSql += "select ltrim(str(isnull(sum(FP.valor), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NP, pos_vw_pedido_forma_pago FP" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CP.id_pedido = NP.id_pedido" + Environment.NewLine;
                sSql += "and CP.id_pedido = FP.id_pedido" + Environment.NewLine;
                sSql += "and FP.codigo in ('EF')" + Environment.NewLine;
                sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return 0;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    return Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString().Trim());
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception)
            {
                return 0;
            }
        }

        //FUNCION PARA INTEGRAR LAS CORTESIAS
        private string productosCortesia()
        {
            try
            {
                string sRetorno = "";

                sSql = "";
                sSql += "select NP.nombre, sum(DP.cantidad) cantidad," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario + DP.valor_iva + DP.valor_otro - DP.valor_dscto)), 0), 10, 2)) total" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden O ON O.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and O.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and O.codigo = '04'" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "group by NP.nombre";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                sRetorno += "PRODUCTOS CORTESIA".PadLeft(29, ' ') + Environment.NewLine;
                sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                dbTotalCategoria = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNombreProducto = dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper();
                    iCantidadProductos = Convert.ToInt32(dtConsulta.Rows[i]["cantidad"].ToString());
                    sPrecioProducto = dtConsulta.Rows[i]["total"].ToString();
                    dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[i]["total"].ToString());

                    if (sNombreProducto.Length > 22)
                    {
                        sNombreProducto = sNombreProducto.Substring(0, 22);
                    }

                    sRetorno += sNombreProducto.PadRight(22, ' ') + iCantidadProductos.ToString("N0").PadLeft(5, ' ') + sPrecioProducto.PadLeft(13, ' ') + Environment.NewLine;

                    dbTotalCategoria += dbPrecioProducto;
                }

                sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                sRetorno += "TOTAL REPORTADO CORTESIAS:".PadRight(27, ' ') + dbTotalCategoria.ToString("N2").PadLeft(13, ' ') + Environment.NewLine + Environment.NewLine + Environment.NewLine;


                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

        //FUNCION PARA INTEGRAR LOS GASTOS
        private string mostrarGastos()
        {
            try
            {
                string sRetorno = "";

                sSql = "";
                sSql += "select concepto, ltrim(str(isnull(valor, 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where estado = 'A' " + Environment.NewLine;
                sSql += "and tipo_movimiento = 0" + Environment.NewLine;
                sSql += "and id_documento_pago is null" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    return "SN";
                }

                sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                sRetorno += "GASTOS REALIZADOS".PadLeft(29, ' ') + Environment.NewLine;
                sRetorno += "".PadLeft(40, '-') + Environment.NewLine;

                dbTotalGastos = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNombreProducto = dtConsulta.Rows[i]["concepto"].ToString().Trim().ToUpper();
                    sPrecioProducto = dtConsulta.Rows[i]["valor"].ToString();
                    dbPrecioProducto = Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());

                    if (sNombreProducto.Length > 30)
                    {
                        sNombreProducto = sNombreProducto.Substring(0, 30);
                    }

                    sRetorno += sNombreProducto.PadRight(30, ' ') + sPrecioProducto.PadLeft(10, ' ') + Environment.NewLine;

                    dbTotalGastos += dbPrecioProducto;
                }

                sRetorno += "".PadLeft(40, '-') + Environment.NewLine;
                sRetorno += "TOTAL GASTOS REALIZADOS:".PadRight(27, ' ') + dbTotalGastos.ToString("N2").PadLeft(13, ' ') + Environment.NewLine + Environment.NewLine + Environment.NewLine;


                return sRetorno;
            }

            catch (Exception)
            {
                return "ERROR";
            }
        }

    }
}
