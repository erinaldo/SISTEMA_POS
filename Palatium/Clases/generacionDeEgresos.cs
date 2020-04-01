using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class generacionDeEgresos
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();

        public bool generarIngreso(string sFecha, int iIdBodega, int iIdLocalidad , int iCgClienteProveedor, int iCgTipoMovimiento,
                                    int cgMonedaBase, string sRefenciaExterna, int iExterno, int iIdPosreceta, int iCantidad, int iIdPedido)
        {
            int iBandera = 0;

            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    iBandera = 1;
                    goto fin;
                }
                else
                {
                    string sFecha1 = Convert.ToDateTime(sFecha).ToString("yyyy-MM-dd");
                    string sFechaActual = Program.sFechaSistema.ToString("yyyy/MM/dd");
                    if (sRefenciaExterna == "")
                        sRefenciaExterna = "1";
                    
                    string sNumeroMovimiento;

                    if (iBandera == 1)
                    {
                        string sAño = sFecha.Substring(6, 4);
                        string sMes = sFecha.Substring(3, 2);
                        Clases.ObtenerNumeroMovimiento movimiento = new Clases.ObtenerNumeroMovimiento();
                        sNumeroMovimiento = movimiento.devuelveCorrelativo("EG", iIdBodega, sAño, sMes, "MOV");
                    }
                    else
                       sNumeroMovimiento = "1";

                    if (sNumeroMovimiento == "Error")
                    {
                        goto reversa;
                    }

                    if (sNumeroMovimiento == "Error")
                        goto reversa;

                    sSql = "insert into cv402_cabecera_movimientos (idempresa,cg_empresa, id_localidad, id_bodega, " +
                            "cg_cliente_proveedor, cg_tipo_movimiento,numero_movimiento, fecha, cg_moneda_base, " +
                           "referencia_externa, externo, estado, terminal_creacion, fecha_creacion, fecha_ingreso, " +
                            "usuario_ingreso, numero_replica_trigger, numero_control_replica, id_pedido) " +
                            "values (" + Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + ", " + iIdBodega + "," +
                             "" + iCgClienteProveedor + ", " + iCgTipoMovimiento + ", '" + sNumeroMovimiento + "',  " +
                            "Convert(DateTime,'" + sFecha1 + "',120), 51, '" + sRefenciaExterna + "', 1, 'A', '" + Program.sDatosMaximo[1] + "', GETDATE()," +
                            "GETDATE(), '" + Program.sDatosMaximo[0] + "',0,0, " + iIdPedido + ")";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        //hara el rolBAck
                        goto reversa;

                    int NewCodigo = 0;

                    sSql = "Select max(Id_Movimiento_Bodega) New_Codigo FROM cv402_cabecera_movimientos where estado = 'A' ";
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                            NewCodigo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        else
                            goto reversa;
                    }
                    else
                        goto reversa;


                    sSql = "select  id_producto, cantidad_bruta, costo_unitario from pos_detalle_receta " +
                            "where id_pos_receta = " + iIdPosreceta + " and estado = 'A'";
                    DataTable dtAyuda = new DataTable();
                    dtAyuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtAyuda.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtAyuda.Rows.Count; i++)
                            {
                                int iIdProducto = Convert.ToInt32(dtAyuda.Rows[i].ItemArray[0].ToString());
                                int iCantidadBruta = Convert.ToInt32(dtAyuda.Rows[i].ItemArray[1].ToString());
                                double dbCostoUnitario = Convert.ToDouble(dtAyuda.Rows[i].ItemArray[2].ToString());

                                sSql = "Insert Into cv402_movimientos_bodega (ID_PRODUCTO,ID_MOVIMIENTO_BODEGA,CG_UNIDAD_COMPRA, " +
                                "CANTIDAD,ESTADO, ) " +
                                 "Values (" + iIdProducto + "," + NewCodigo + ",546," + ((iCantidadBruta * iCantidad) * -1) + ",'A')";

                                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                    //hara el rolBAck
                                    goto reversa;
                            }

                        }
                    }
                    else
                        goto reversa;

                   // conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    goto fin;

                }

    
            }
            catch (Exception)
            {
                goto reversa;
            }

            #region Funciones de ayuda

            reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                iBandera = 1;
            }
            fin: 
            {
                if (iBandera == 1)
                    return false;
                else
                    return true;
            }

            #endregion

        }


        public bool manejaReceta(int iIdProducto)
        {
            int iBandera = 0;
            sSql = "select id_pos_receta, id_producto from cv401_productos  "+
                    "where  id_producto = "+iIdProducto+" and id_pos_receta >0 and estado = 'A'";
            dtConsulta = new DataTable();
            dtConsulta.Clear();
            if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
            {
                if (dtConsulta.Rows.Count > 0)
                    iBandera = 1;
            }

            if (iBandera == 1) return true; else return false;

        }


        //Fin de la clase
    }
}
