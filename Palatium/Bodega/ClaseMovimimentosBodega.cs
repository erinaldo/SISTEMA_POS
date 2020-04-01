using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public class ClaseMovimimentosBodega
    {
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sReferencia;        
        string sCodigo;
        string sAnioCorto;
        string sMesCorto;
        string sFechaDesde;
        string sFechaHasta;
        string sValido_desde;
        string sValido_hasta;
        string sCorrelativo;
        string sTabla;
        string sCampo;

        double dbValorActual;

        DataTable dtConsulta;

        bool bRespuesta;

        long iMaximo;

        //VARIABLES NUMERO DE MOVIMIENTO
        string sTipoMovimiento;        
        string sAnio;
        string sMes;
        string sCodigoCorrelativo;

        int iIdBodega;

        //VARIABLES PARA INSERTAR LA CABECERA Y EL MOVIMIENTO DE INGRESO
        int iIdBodegaInsertar;
        int iMotivo;
        int iIdProveedor;
        int iIdPersona;
        int iIdOficina;
        int iCgTipoMovimiento;

        string sFechaInsertar;
        string sReferenciaInsertar;
        string sNotaPedido;
        string sFacturaCompra;
        string sNotaEntrega;
        string sComentarios;
        string sIva;
        string sDescuento;
        DataGridView dgvDatos;

        //VARIABLES PARA INSERTAR LA CABECERA Y EL MOVIMIENTO DE EGRESO
        int iBandera;
        int iEliminar;
        int iIdNumeroMovimiento;
        int iActualizar;

        string sNumeroMovimiento;
        string sOrdenFabricacion;
        string sOrdenDisenio;


        //FUNCION PARA INICIAR TRANSACCION
        private bool iniciaTransaccion()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {                    
                    return false;
                }

                else
                {
                    return true;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INICIAR UN INGRESO
        public bool realizarIngreso(string sTipoMovimiento_P, int iIdBodega_P, string sAnio_P, string sMes_P, 
                                    string sCodigoCorrelativo_P, int iMotivo_P, int iIdProveedor_P,
                                    int iIdPersona_P, int iIdOficina_P, string sFechaInsertar_P, string sReferenciaInsertar_P,
                                    string sNotaPedido_P, string sFacturaCompra_P, string sNotaEntrega_P, string sComentarios_P,
                                    string sIva_P, string sDescuento_P, DataGridView dgvDatos_P, int iCgTipoMovimiento_P)
        {
            try
            {
                this.sTipoMovimiento = sTipoMovimiento_P;
                this.iIdBodega = iIdBodega_P;
                this.sAnio = sAnio_P;
                this.sMes = sMes_P;
                this.sCodigoCorrelativo = sCodigoCorrelativo_P;

                this.iMotivo = iMotivo_P;
                this.iIdProveedor = iIdProveedor_P;
                this.iIdPersona = iIdPersona_P;
                this.iIdOficina = iIdOficina_P;

                this.sFechaInsertar = sFechaInsertar_P;
                this.sReferenciaInsertar = sReferenciaInsertar_P;
                this.sNotaPedido = sNotaPedido_P;
                this.sFacturaCompra = sFacturaCompra_P;
                this.sNotaEntrega = sNotaEntrega_P;
                this.sComentarios = sComentarios_P;
                this.sIva = sIva_P;
                this.sDescuento = sDescuento_P;
                this.dgvDatos = dgvDatos_P;
                this.iCgTipoMovimiento = iCgTipoMovimiento_P;

                //INICIA TRANSACCIÓN
                if (iniciaTransaccion() == false)
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                }

                sCorrelativo = devuelveCorrelativo();

                if (sCorrelativo == "Error")
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    return false;
                }

                if (insertarRegistroIngreso() == false)
                {
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION  PARA CREAR EL NUMERO DE MOVIMIENTO
        private string devuelveCorrelativo()
        {
            try
            {
                dbValorActual = 0;
                sCodigo = "";
                sAnioCorto = sAnio.Substring(2, 2);

                if (sMes.Substring(0, 1) == "0")
                {
                    sMesCorto = sMes.Substring(1, 1);
                }

                else
                {
                    sMesCorto = sMes;
                }

                sSql = "";
                sSql += "select codigo from cv402_bodegas" + Environment.NewLine;
                sSql += "where id_bodega = " + iIdBodega;

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCodigo = dtConsulta.Rows[0][0].ToString();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return "Error";
                }

                sReferencia = sTipoMovimiento + sCodigo + "_" + sAnio + "_" + sMesCorto + "_" + Program.iCgEmpresa;

                sSql = "";
                sSql += "select valor_actual from tp_correlativos" + Environment.NewLine;
                sSql += "where referencia = '" + sReferencia + "'" + Environment.NewLine;
                sSql += "and codigo_correlativo = '" + sCodigoCorrelativo + "'";

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dbValorActual = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());

                        sSql = "";
                        sSql += "update tp_correlativos set" + Environment.NewLine;
                        sSql += "valor_actual =  " + (dbValorActual + 1) + Environment.NewLine;
                        sSql += "where referencia = '" + sReferencia + "'";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return "Error";
                        }

                        return sTipoMovimiento + sCodigo + sAnioCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');
                    }

                    else
                    {
                        int iCorrelativo = 4979;
                        dbValorActual = 1;

                        sSql = "";
                        sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                        sSql += "where codigo = 'BD'" + Environment.NewLine;
                        sSql += "and tabla = 'SYS$00022'";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                        
                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                            }
                        }

                        else
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return "Error";
                        }

                        sFechaDesde = sAnio + "-01-01";
                        sFechaHasta = sAnio + "-12-31";
                        sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                        sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

                        sSql = "";
                        sSql += "insert into tp_correlativos (" + Environment.NewLine;
                        sSql += "cg_sistema, codigo_correlativo, referencia, valido_desde, valido_hasta," + Environment.NewLine;
                        sSql += "valor_actual, desde, hasta, estado, origen_dato, numero_replica_trigger," + Environment.NewLine;
                        sSql += "estado_replica, numero_control_replica)" + Environment.NewLine;
                        sSql += "values(" + Environment.NewLine;
                        sSql += iCorrelativo + ", '" + sCodigoCorrelativo + "', '" + sReferencia + "'," + Environment.NewLine;
                        sSql += "'" + sFechaDesde + "', '" + sFechaHasta + "', " + (dbValorActual + 1) + "," + Environment.NewLine;
                        sSql += "0, 0, 'A', 1," + (dbValorActual + 1).ToString("N0") + ", 0, 0)";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return "Error";
                        }

                        return sTipoMovimiento + sCodigo + sAnioCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return "Error";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "Error";
            }
        }

        //FUNCION PARA INSERTAR EL INGRESO DE BODEGA
        private bool insertarRegistroIngreso()
        {
            try
            {
                sSql = "";
                sSql += "Insert Into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "CG_EMPRESA, idEmpresa, ID_BODEGA, CG_TIPO_MOVIMIENTO," + Environment.NewLine;
                sSql += "CG_MOTIVO_MOVIMIENTO_BODEGA, CG_CLIENTE_PROVEEDOR," + Environment.NewLine;
                sSql += "ID_AUXILIAR, ID_PERSONA, ID_LOCALIDAD, Fecha, CG_MONEDA_BASE," + Environment.NewLine;
                sSql += "REFERENCIA_EXTERNA, NOTA_PEDIDO, FACTURA, NOTA_ENTREGA, OBSERVACION," + Environment.NewLine;
                sSql += "NUMERO_MOVIMIENTO, usuario_ingreso, terminal_ingreso, ESTADO,PORCENTAJE_IVA," + Environment.NewLine;
                sSql += "PORCENTAJE_DESCUENTO)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iCgEmpresa + ", " + Program.iIdEmpresa + ", " + iIdBodega + "," + Environment.NewLine;
                sSql += iCgTipoMovimiento + ", " + iMotivo + ", 6162, " + iIdProveedor + "," + Environment.NewLine;
                sSql += iIdPersona + ", " + iIdOficina + ", '" + sFechaInsertar + "'," + Environment.NewLine;
                sSql += Program.iMoneda + ", '" + sReferenciaInsertar + "', '" + sNotaPedido + "'," + Environment.NewLine;
                sSql += "'" + sFacturaCompra + "', '" + sNotaEntrega + "', '" + sComentarios + "'," + Environment.NewLine;
                sSql += "'" + sCorrelativo + "', '" + Program.sDatosMaximo[0] + "', " + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', " + sIva + ", " + sDescuento + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV402_CABECERA_MOVIMIENTO
                //dtConsulta = new DataTable();
                //dtConsulta.Clear();

                //sTabla = "cv402_cabecera_movimientos";
                //sCampo = "Id_Movimiento_Bodega";

                //iMaximo = conexion.GFun_Ln_Saca_Maximo_IDXXXXX(sTabla, sCampo, "", Program.sDatosMaximo);

                //if (iMaximo == -1)
                //{
                //    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                //    ok.ShowInTaskbar = false;
                //    ok.ShowDialog();
                //    goto reversa;
                //}

                //else
                //    NewCodigo = Convert.ToInt32(iMaximo);

                sSql = "";
                sSql += "select max(id_movimiento_bodega)" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    iMaximo = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    int iIdProducto = Convert.ToInt32(dgvDatos.Rows[i].Cells[11].Value.ToString());
                    double dbCantidad = Convert.ToDouble(dgvDatos.Rows[i].Cells[5].Value.ToString());
                    double dbValorUnitario = Convert.ToDouble(dgvDatos.Rows[i].Cells[13].Value.ToString());
                    double dbValorDescuento = Convert.ToDouble(dgvDatos.Rows[i].Cells[7].Value.ToString());
                    double dbValorIva = Convert.ToDouble(dgvDatos.Rows[i].Cells[6].Value.ToString());
                    string sEpecificacion;

                    if (dgvDatos.Rows[i].Cells[3].Value != null)
                    {
                        sEpecificacion = dgvDatos.Rows[i].Cells[3].Value.ToString();
                    }
                    else
                    {
                        sEpecificacion = " ";
                    }

                    sSql = "";
                    sSql += "Insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "ID_PRODUCTO, ESPECIFICACION, ID_MOVIMIENTO_BODEGA, CG_UNIDAD_COMPRA," + Environment.NewLine;
                    sSql += "CANTIDAD, Valor_Unitario, VALOR_DSCTO, VALOR_IVA, ESTADO)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto + ", '" + sEpecificacion + "', " + iMaximo + ", 546, " + Environment.NewLine;
                    sSql += dbCantidad + ", " + dbValorUnitario + ", 0, 0,'A')";    //REVISAR VALOR DEL IVA

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                return true;
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }

        //FUNCION  PARA ANULAR EL INGRESO DE BODEGA
        public bool eliminarRegistroIngreso(int iIdMovimiento_P)
        {
            try
            {
                //INICIA TRANSACCIÓN
                if (iniciaTransaccion() == false)
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "update cv402_cabecera_movimientos Set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate() " + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega = " + iIdMovimiento_P;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega= " + iIdMovimiento_P;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }
 
        //FUNCION  PARA INICIAR UN EGRESO
        public bool realizarEgreso(string sTipoMovimiento_P, int iIdBodega_P, string sAnio_P, string sMes_P, 
                                    string sCodigoCorrelativo_P, int iMotivo_P, int iIdProveedor_P,
                                    int iIdPersona_P, int iIdOficina_P, string sFechaInsertar_P, string sComentarios_P,
                                    string sOrdenFabricacion_P, string sOrdenDisenio_P, string sNotaEntrega_P, 
                                    int iBandera_P, int iActualizar_P, int iIdNumeroMovimiento_P, string sNumeroMovimiento_P, 
                                    int iEliminar_P, DataGridView dgvDatos_P, int iCgTipoMovimiento_P)
        {
            try
            {
                this.sTipoMovimiento = sTipoMovimiento_P;
                this.iIdBodega = iIdBodega_P;
                this.sAnio = sAnio_P;
                this.sMes = sMes_P;
                this.sCodigoCorrelativo = sCodigoCorrelativo_P;
                this.iMotivo = iMotivo_P;
                this.iIdProveedor = iIdProveedor_P;
                this.iIdPersona = iIdPersona_P;
                this.iIdOficina = iIdOficina_P;
                this.sFechaInsertar = sFechaInsertar_P;
                this.sComentarios = sComentarios_P;
                this.sOrdenFabricacion = sOrdenFabricacion_P;
                this.sOrdenDisenio = sOrdenDisenio_P;
                this.sNotaEntrega = sNotaEntrega_P;
                this.iBandera = iBandera_P;
                this.iActualizar = iActualizar_P;
                this.sNumeroMovimiento = sNumeroMovimiento_P;
                this.iIdNumeroMovimiento = iIdNumeroMovimiento_P;
                this.iEliminar = iEliminar_P;
                this.dgvDatos = dgvDatos_P;
                this.iCgTipoMovimiento = iCgTipoMovimiento_P;

                //INICIA TRANSACCIÓN
                if (iniciaTransaccion() == false)
                {
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return false;
                }

                /*VERIFICAR SI SE DEBE ELIMINAR
                PARAMETRO IELIMINAR
                0 - NO ELIMINA
                1 - ELIMINAR   */ 

                if (iEliminar == 1)
                {
                    if (eliminarRegistroEgreso(iIdNumeroMovimiento) == false)
                    {
                        return false;
                    }
                }                

                if (iActualizar == 1)
                {
                    sCorrelativo = sNumeroMovimiento;
                    goto continuar;
                }

                else if (iActualizar == 2)
                {
                    goto fin;
                }

                /* VERIFICAR SI SE DEBE OBTENER UN NUMERO DE MOVIMIENTO
                PARAMETRO IBANDETA
                == 1 - CREAR NUMERO DE MOVIMIENTO
                != 1 - RECUPERA EL NUMERO DE MOVIMIENTO
                */

                if (iBandera == 1)
                {
                    sCorrelativo = devuelveCorrelativo();

                    if (sCorrelativo == "Error")
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        return false;
                    }
                }

                else
                {
                    sCorrelativo = sNumeroMovimiento;
                }

                continuar: { }
                //ENVIAR A LA FUNCION DE INGRESO DE UN EGRESO
                if (insertarRegistroEgreso() == false)
                {
                    return false;
                }

                fin: { }
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA INSERTAR EL EGRESO DE BODEGA
        private bool insertarRegistroEgreso()
        {
            try
            {
                sSql = "";
                sSql += "Insert Into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "CG_EMPRESA, idEmpresa, ID_BODEGA, CG_TIPO_MOVIMIENTO," + Environment.NewLine;
                sSql += "CG_MOTIVO_MOVIMIENTO_BODEGA, CG_CLIENTE_PROVEEDOR, ID_AUXILIAR," + Environment.NewLine;
                sSql += "ID_PERSONA, ID_LOCALIDAD, Fecha, CG_MONEDA_BASE, REFERENCIA_EXTERNA," + Environment.NewLine;
                sSql += "NOTA_PEDIDO, FACTURA, NOTA_ENTREGA, OBSERVACION, NUMERO_MOVIMIENTO," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, ESTADO )" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iCgEmpresa + ", " + Program.iIdEmpresa + ", " + iIdBodega + "," + Environment.NewLine;
                sSql += iCgTipoMovimiento + ", " + iMotivo + ", 6162, " + iIdProveedor + "," + Environment.NewLine;
                sSql += iIdPersona + ", " + iIdOficina + ", '" + sFechaInsertar + "'," + Environment.NewLine;
                sSql += Program.iMoneda + ", '" + sComentarios + "', '" + sOrdenFabricacion + "'," + Environment.NewLine;
                sSql += "'" + sOrdenDisenio + "', '" + sNotaEntrega + "', '" + sComentarios + "'," + Environment.NewLine;
                sSql += "'" + sCorrelativo + "', '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A' )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                int NewCodigo = 0;

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv402_cabecera_movimientos";
                sCampo = "Id_Movimiento_Bodega";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    NewCodigo = Convert.ToInt32(iMaximo);
                }

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    int iIdProducto = Convert.ToInt32(dgvDatos.Rows[i].Cells[9].Value.ToString());
                    double dbCantidad = Convert.ToDouble(dgvDatos.Rows[i].Cells[4].Value.ToString());
                    double dbValorUnitario = Convert.ToDouble(dgvDatos.Rows[i].Cells[5].Value.ToString());

                    sSql = "";
                    sSql += "Insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                    sSql += "ID_PRODUCTO, ID_MOVIMIENTO_BODEGA, CG_UNIDAD_COMPRA, CANTIDAD,ESTADO)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdProducto + ", " + NewCodigo + ", 546," + (dbCantidad * -1) + ", 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }

        //FUNCION  PARA ANULAR EL EGRESO DE BODEGA
        private bool eliminarRegistroEgreso(int iIdMovimiento_P)
        {
            try
            {                
                sSql = "";
                sSql += "Update cv402_cabecera_movimientos Set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()" + Environment.NewLine;
                sSql += "Where Id_Movimiento_Bodega=" + iIdMovimiento_P;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "Update cv402_movimientos_bodega Set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega=" + iIdMovimiento_P;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return false; }
        }
    }
}
