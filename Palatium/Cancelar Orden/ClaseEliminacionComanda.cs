using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Cancelar_Orden
{
    class ClaseEliminacionComanda
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sMotivo;

        DataTable dtConsulta;
        DataTable dtAyuda;

        bool bRespuesta;

        int iIdPedido;
        int iIdFactura;
        int iIdDocumentoCobrar;
        int iIdPago;
        int iIdDocumentoPago;
        int iIdEventoCobro;
        int iIdPosMovimientoCaja;
        int iIdDespachoPedido;
        int iIdCabDespacho;
        int iIdPosTarCabMovimiento;

        public bool procesoEliminacion(int iIdPedido_P, string sMotivo_P)
        {
            try
            {
                this.iIdPedido = iIdPedido_P;
                this.sMotivo = sMotivo_P;

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    return false;
                }

                if (obtenerRegistros() == false)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                sSql = "";
                sSql += "insert into pos_cancelacion(" + Environment.NewLine;
                sSql += "id_pedido, motivo_cancelacion, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdPedido + ", '" + sMotivo + "', 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                return true;
            }

            catch (Exception)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                return false;
            }
        }

        //FUNCION PARA RECUPERAR LOS VALORES
        private bool obtenerRegistros()
        {
            try
            {
                //BUSCAR EL ID DE LA CABECERA DE LA VENTA DE LA TARJETA DE ALMUERZO

                sSql = "";
                sSql += "select id_pos_tar_cab_movimiento" + Environment.NewLine;
                sSql += "from pos_tar_cab_movimiento" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
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
                    iIdPosTarCabMovimiento = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_tar_cab_movimiento"].ToString());

                    if (eliminarPedidoTarjetaAlmuerzo() == false)
                    {
                        return false;
                    }
                }

                //BUSCAR EL ID_FACTURA
                sSql = "";
                sSql += "select id_factura" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
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
                    iIdFactura = Convert.ToInt32(dtConsulta.Rows[0]["id_factura"].ToString());

                    if (eliminarFactura() == false)
                    {
                        return false;
                    }
                }

                //BUSCAR EL ID_DOCUMENTO_COBRAR
                sSql = "";
                sSql += "select id_documento_cobrar, id_evento_cobro" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                iIdDocumentoCobrar = Convert.ToInt32(dtConsulta.Rows[0]["id_documento_cobrar"].ToString());
                iIdEventoCobro = Convert.ToInt32(dtConsulta.Rows[0]["id_evento_cobro"].ToString());

                //BUSCAMOS LOS PAGOS
                sSql = "";
                sSql += "select id_pago" + Environment.NewLine;
                sSql += "from cv403_documentos_pagados" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
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
                    iIdPago = Convert.ToInt32(dtConsulta.Rows[0]["id_pago"].ToString());

                    if (eliminarPagos() == false)
                    {
                        return false;
                    }
                }

                if (eliminarPedido() == false)
                {
                    return false;
                }


                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA ELIMINAR EL PASO DE FACTURACION
        private bool eliminarFactura()
        {
            try
            {
                sSql = "";
                sSql += "update cv403_numeros_facturas set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_facturas set" + Environment.NewLine;
                sSql += "comentarios = '" + sMotivo + "'," + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_facturas_pedidos set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA ELIMINAR EL PASO DE PAGOS
        private bool eliminarPagos()
        {
            try
            {
                sSql = "";
                sSql += "update cv403_pagos set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_numeros_pagos set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_documentos_pagados set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                //BUSCAMOS LOS DOCUMENTOS PAGOS
                sSql = "";
                sSql += "select id_documento_pago" + Environment.NewLine;
                sSql += "from cv403_documentos_pagos" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdDocumentoPago= Convert.ToInt32(dtAyuda.Rows[i]["id_documento_pago"].ToString());

                    sSql = "";
                    sSql += "select id_pos_movimiento_caja" + Environment.NewLine;
                    sSql += "from pos_movimiento_caja" + Environment.NewLine;
                    sSql += "where id_documento_pago = " + iIdDocumentoPago + Environment.NewLine;
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
                        iIdPosMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_movimiento_caja"].ToString());

                        sSql = "";
                        sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                        //sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "estado = 'N'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_pos_movimiento_caja = " + iIdPosMovimientoCaja + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            return false;
                        }

                        sSql = "";
                        sSql += "update pos_numero_movimiento_caja set" + Environment.NewLine;
                        //sSql += "estado = 'E'," + Environment.NewLine;
                        sSql += "estado = 'N'," + Environment.NewLine;
                        sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                        sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                        sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                        sSql += "where id_pos_movimiento_caja = " + iIdPosMovimientoCaja + Environment.NewLine;
                        sSql += "and estado = 'A'";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            return false;
                        }
                    }
                }

                sSql = "";
                sSql += "update cv403_documentos_pagos set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pago = " + iIdPago + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA ELIMINAR EL PASO DE COMANDAS
        private bool eliminarPedido()
        {
            try
            {
                //SELECCIONAR EL ID MOVIMIENTO BODEGA
                sSql = "";
                sSql += "select id_movimiento_bodega" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    int iIdCabeceraMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update cv402_cabecera_movimientos set" + Environment.NewLine;
                    //sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "estado = 'N'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_movimiento_bodega = " + iIdCabeceraMovimiento + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        return false;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    //sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "estado = 'N'" + Environment.NewLine;
                    sSql += "where id_movimiento_bodega = " + iIdCabeceraMovimiento + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        return false;
                    }
                }

                sSql = "";
                sSql += "update cv403_eventos_cobros set" + Environment.NewLine;
                //sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_evento_cobro = " + iIdEventoCobro + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_dctos_por_cobrar set" + Environment.NewLine;
                //sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_documento_cobrar = " + iIdDocumentoCobrar + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_numero_cab_pedido set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                //BUSCAMOS LAS CANTIDADES DESPACHADAS
                sSql = "";
                sSql += "select id_despacho_pedido, id_despacho" + Environment.NewLine;
                sSql += "from cv403_despachos_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    return false;
                }

                iIdCabDespacho = Convert.ToInt32(dtAyuda.Rows[0]["id_despacho"].ToString());

                for (int i = 0; i < dtAyuda.Rows.Count; i++)
                {
                    iIdDespachoPedido = Convert.ToInt32(dtAyuda.Rows[i]["id_despacho_pedido"].ToString());

                    sSql = "";
                    sSql += "update cv403_cantidades_despachadas set" + Environment.NewLine;
                    //sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "estado = 'N'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_despacho_pedido = " + iIdDespachoPedido + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        return false;
                    }
                }

                sSql = "";
                sSql += "update cv403_despachos_pedidos set" + Environment.NewLine;
                //sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_cab_despachos set" + Environment.NewLine;
                //sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_despacho = " + iIdCabDespacho + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_det_pedidos set" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "estado_orden = 'Cancelada'," + Environment.NewLine;
                sSql += "comentarios = '" + sMotivo + "'," + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA ELIMINAR EL PASO DE LA TARJETA
        private bool eliminarPedidoTarjetaAlmuerzo()
        {
            try
            {
                sSql = "";
                sSql += "update pos_tar_cab_movimiento set" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_tar_cab_movimiento = " + iIdPosTarCabMovimiento + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                sSql = "";
                sSql += "update pos_tar_det_movimiento set" + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_tar_cab_movimiento = " + iIdPosTarCabMovimiento + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    return false;
                }

                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}
