using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Palatium.Bodega
{
    public partial class frmBodega : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        
        DataTable dtConsulta = new DataTable();

        bool bRespuesta;

        int iIdMovimientoBodega;        
        int iCorrelativoProveedor;
        int iIdPersona;        
        int iIdProducto;

        double dbValorActual = 0;

        string sSql;
        string sCodigo = "";
        string sTabla;
        string sCampo;
        string sCodigoProducto;
        string sPagaIva;
        string sReferencia;
        string sFecha;

        long iMaximo;

        public frmBodega()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        public string devuelveCorrelativo(string sTipoMovimiento, int iIdBodega, string sAño, string sMes, string sCodigoCorrelativo)
        {

            double dbValorActual = 0;
            string sCodigo = "";
            string sAñoCorto = sAño.Substring(2, 2);
            string sMesCorto;
            if (sMes.Substring(0, 1) == "0")
                sMesCorto = sMes.Substring(1, 1);
            else
                sMesCorto = sMes;

            sSql = "";
            sSql += "select codigo from cv402_bodegas" + Environment.NewLine;
            sSql += "where id_bodega = " + iIdBodega;

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    sCodigo = dtConsulta.Rows[0].ItemArray[0].ToString();
                }
            }

            else
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                catchMensaje.Show();
                return "Error";
            }

            string sReferencia;

            sReferencia = sTipoMovimiento + sCodigo + "_" + sAño + "_" + sMesCorto + "_" + Program.iCgEmpresa;

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
                    dbValorActual = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());

                    sSql = "";
                    sSql += "update tp_correlativos set" + Environment.NewLine;
                    sSql += "valor_actual =  " + (dbValorActual + 1) + Environment.NewLine;
                    sSql += "where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        return "Error";
                    }

                    return sTipoMovimiento + sCodigo + sAñoCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

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
                            iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        return "Error";
                    }

                    string sFechaDesde = sAño + "-01-01";
                    string sFechaHasta = sAño + "-12-31";
                    string sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                    string sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

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
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        return "Error";
                    }

                    return sTipoMovimiento + sCodigo + sAñoCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

                }
            }

            else
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                catchMensaje.Show();
                return "Error";
            }
        }

        #endregion

        private void frmBodega_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaAplicacion.Text = sFecha;
            cargarComboOficina();
            cargarComboBodega();
            cargarComboMotivo();
            llenarcomboMoneda();
            txtIva.Text = "12";
            txtDescuento.Text = "0.00";
        }

        //Función para llenar el combo de moneda
        private void llenarcomboMoneda()
        {
            try
            {
                DataTable dtPadre = new DataTable();
                sSql = "";
                sSql += "select correlativo,valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla='SYS$00021'" + Environment.NewLine;
                sSql += "and estado='A'";

                cmbMoneda.llenar(dtPadre, sSql);
                cmbMoneda.SelectedIndex = 1;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }
        
        //Función para cargar el combo de Motivo
        private void cargarComboMotivo()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00643'" + Environment.NewLine;
                sSql += "and codigo in( '29','01','35','44','45','49','50')";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbMotivos.llenar(dtConsulta, sSql);
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            } 
        }

        //Función para cargar el combo de oficina
        private void cargarComboOficina()
        {
            try
            {
                sSql = "";
                sSql = "Select LOC.id_localidad, BO.descripcion, BO.id_bodega" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.tipo = '1'" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = 1" + Environment.NewLine;
                sSql += "and LOC.estado = 'A'";
                
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    cmbOficina.llenar(dtConsulta, sSql);
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        //Función para cargar el combo de bodega
        private void cargarComboBodega()
        {
            try
            {
                sSql = "";
                sSql += "select id_bodega, descripcion" + Environment.NewLine;
                sSql += "from cv402_bodegas where categoria = 1";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.llenar(dtConsulta, sSql);
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }

            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOficina.SelectedIndex > 0)
            {
                sSql = "";
                sSql += "select id_bodega" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + cmbOficina.SelectedValue;
                
                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.SelectedValue = dtConsulta.Rows[0].ItemArray[0];
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }
            }            
        }

        private void btnAyudaIngresoNumeros_Click(object sender, EventArgs e)
        {
            if (cmbBodega.SelectedIndex == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Primero tiene que seleccionar una bodega";
                ok.ShowDialog();
                return;
            }

            Bodega.frmAyudaBodega ayuda = new frmAyudaBodega(Convert.ToInt32(cmbBodega.SelectedValue));
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                iIdMovimientoBodega = Convert.ToInt32(ayuda.sIdMovimientoBodega);
                txtIngresoNumeros.Text = ayuda.sNumeroMovimiento;
                sReferencia = ayuda.sReferenciaExterna;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtIngresoNumeros.Text != "" && iIdMovimientoBodega != 0)
                cargarDatos();
            else
                verificarCampos();
        }
        
        //Función para verificar Campos
        private void verificarCampos()
        {
            if (cmbMotivos.SelectedIndex == 0)
                MessageBox.Show("ADVERTENCIA: Debe seleccionar el motivo del movimiento","Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(txtFacturaCompra.Text =="")
                MessageBox.Show("ADVERTENCIA: Debe ingresar el número de factura de compra", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (txtProveedor.Text == "")
                MessageBox.Show("ADVERTENCIA: Debe seleccionar el proveedor", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                btnA.Enabled = true;
                btnX.Enabled = true;
            }

        }

        //Función para cargar los datos
        private void cargarDatos()
        {
            try
            {
                dgvDetalleVenta.Rows.Clear();

                sSql = "";
                sSql += "select m.Fecha, m.Cg_Empresa, m.ID_BODEGA, m.CG_TIPO_MOVIMIENTO," + Environment.NewLine;
                sSql += "m.ID_LOCALIDAD, m.Cg_Moneda_Base, m.Referencia_Externa, m.Nota_pedido," + Environment.NewLine;
                sSql += "m.FACTURA,m.Nota_entrega,m.Observacion, m.cg_motivo_movimiento_bodega," + Environment.NewLine;
                sSql += "m.Id_Auxiliar, m.Id_Persona, m.Porcentaje_IVA, m.Porcentaje_descuento," + Environment.NewLine;
                sSql += "m.id_c_movimiento, m.estado_replica, m.estado" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos m left outer join" + Environment.NewLine;
                sSql += "cv404_auxiliares_contables a on m.id_auxiliar = a.id_auxiliar" + Environment.NewLine;
                sSql += "where m.Id_Movimiento_Bodega = " + iIdMovimientoBodega;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    { 
                        string sFecha = dtConsulta.Rows[0].ItemArray[0].ToString();
                        txtFechaAplicacion.Text = Convert.ToDateTime(sFecha).ToString("dd/MM/yyy");
                        cmbMotivos.SelectedValue = dtConsulta.Rows[0].ItemArray[11].ToString();
                        txtReferencia.Text = sReferencia;
                        txtFacturaCompra.Text = dtConsulta.Rows[0].ItemArray[8].ToString(); ;
                        txtNotaPedido.Text = dtConsulta.Rows[0].ItemArray[7].ToString();
                        txtNotaEntrega.Text = dtConsulta.Rows[0].ItemArray[9].ToString();
                        txtComentarios.Text = dtConsulta.Rows[0].ItemArray[10].ToString();
                        int iIdAuxiliarContable = Convert.ToInt32( dtConsulta.Rows[0].ItemArray[12].ToString());
                        int iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[13].ToString());
                        txtIva.Text = dtConsulta.Rows[0].ItemArray[14].ToString();
                        txtDescuento.Text = dtConsulta.Rows[0].ItemArray[15].ToString();
                        lblA.Text = dtConsulta.Rows[0].ItemArray[18].ToString(); 
                       

                        string sSql1;
                        sSql1 = "";
                        sSql1 += "select codigo, descripcion, id_auxiliar" + Environment.NewLine;
                        sSql1 += "from cv404_auxiliares_contables " + Environment.NewLine;
                        sSql1 += "where id_auxiliar = "+iIdAuxiliarContable;

                        DataTable dtAyuda = new DataTable();
                        dtAyuda.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql1);

                        if (bRespuesta == true)
                        {
                            if (dtAyuda.Rows.Count > 0)
                            {
                                txtProveedor.Text = dtAyuda.Rows[0].ItemArray[0].ToString();
                                txtNombreProveedor.Text = dtAyuda.Rows[0].ItemArray[1].ToString();
                            }
                        }

                        else
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.Show();
                            return;
                        }

                        sSql1 = "";
                        sSql1 += "select identificacion, apellidos +' '+ nombres" + Environment.NewLine;
                        sSql1 += "from tp_personas" + Environment.NewLine;
                        sSql1 += "where id_persona = " + iIdPersona;

                        dtAyuda = new DataTable();
                        dtAyuda.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql1);

                        if (bRespuesta == true)
                        {
                            if (dtAyuda.Rows.Count > 0)
                            {
                                txtPersona.Text = dtAyuda.Rows[0].ItemArray[0].ToString();
                                txtNombrePersona.Text = dtAyuda.Rows[0].ItemArray[1].ToString();
                            }
                        }

                        else
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.Show();
                        }

                        cargarGrid();
                        llenarCajasTexto();
                     //   MessageBox.Show("ADVERTENCIA: El registro ya fue enviado a contabilidad", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        bloquearControles();

                        btnA.Enabled = true;
                        btnX.Enabled = true;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        //Función para cargar el grid
        private void cargarGrid()
        {
            try
            {
                dgvDetalleVenta.Rows.Clear();

                sSql = "";
                sSql += "SELECT MB.CORRELATIVO, P.codigo codigo_producto, N.nombre producto," + Environment.NewLine;
                sSql += "MB.Id_Producto, MB.especificacion, U.codigo unidad, MB.cg_unidad_compra cg_unidad_compra," + Environment.NewLine;
                sSql += "MB.CANTIDAD, MB.Valor_Unitario, round(100*MB.Valor_Dscto/MB.valor_Unitario,2) Pct_Dscto," + Environment.NewLine;
                sSql += "MB.Valor_Iva, MB.VALOR_DSCTO, Case when P.Paga_Iva = 1 Then 1 Else 0 End Paga_Iva " + Environment.NewLine;
                sSql += "from cv402_movimientos_bodega MB, cv401_productos P, tp_codigos U, cv401_nombre_productos N " + Environment.NewLine;
                sSql += "where MB.Id_Producto = P.Id_Producto" + Environment.NewLine;
                sSql += "and P.Id_Producto = N.Id_Producto" + Environment.NewLine;
                sSql += "and N.Nombre_Interno = 1" + Environment.NewLine;
                sSql += "and N.Estado = 'A'" + Environment.NewLine;
                sSql += "and MB.CG_UNIDAD_COMPRA = U.Correlativo" + Environment.NewLine;
                sSql += "and MB.Id_Movimiento_Bodega = " + iIdMovimientoBodega + Environment.NewLine;
                sSql += "and MB.Estado = 'A'" + Environment.NewLine;
                sSql += "order By MB.Correlativo";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            Button btnAyuda = new Button();
                            btnAyuda.Text = "?";
                            btnAyuda.Width = 10;
                            
                            if(dtConsulta.Rows[i].ItemArray[4] == null)
                                dtConsulta.Rows[i].ItemArray[4] = "";

                            int iIdProducto = Convert.ToInt32( dtConsulta.Rows[i].ItemArray[3].ToString());
                            string sCodigo = dtConsulta.Rows[i].ItemArray[1].ToString();
                            string sDescripcion = dtConsulta.Rows[i].ItemArray[2].ToString();
                            string sEspecificacion = dtConsulta.Rows[i].ItemArray[4].ToString();
                            string sUnidad = dtConsulta.Rows[i].ItemArray[5].ToString();
                            double dbcantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[7].ToString());
                            double dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[8].ToString());
                            double dbPorcentajeDescuento = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[9].ToString());
                            double dbDescuento = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[11].ToString());
                            double dbSubtotal = dbcantidad * dbPrecioUnitario;

                            string sFecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");

                            string sSql1;
                            sSql1 = "";
                            sSql1 += "Select Sum(M.CANTIDAD) Saldo " + Environment.NewLine;
                            sSql1 += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C " + Environment.NewLine;
                            sSql1 += "where C.id_bodega = 14" + Environment.NewLine;        //AQUI HAY UN CODIGO EN DURO
                            sSql1 += "and M.id_producto = " + iIdProducto + Environment.NewLine;
                            sSql1 += "and C.Fecha <= Convert(DateTime,'" + sFecha + "',120)" + Environment.NewLine;
                            sSql1 += "and C.estado = 'A'" + Environment.NewLine;
                            sSql1 += "and M.estado = 'A'" + Environment.NewLine;
                            sSql1 += "and M.correlativo + 0 = M.correlativo + 0" + Environment.NewLine;
                            sSql1 += "and C.ID_Movimiento_Bodega = M.ID_Movimiento_Bodega" + Environment.NewLine;
                            sSql1 += "and C.cg_empresa = " + Program.iCgEmpresa;


                            DataTable dtAyuda = new DataTable();
                            dtAyuda.Clear();
                            string sStock ="";
                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql1);

                            if (bRespuesta == true)
                            {
                                if (dtAyuda.Rows.Count > 0)
                                {
                                    sStock = dtAyuda.Rows[0].ItemArray[0].ToString();
                                }
                            }

                            else
                            {
                                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                                catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.Show();
                                return;
                            }

                            dgvDetalleVenta.Rows.Add(sCodigo,
                                                     "?",
                                                      sDescripcion,
                                                      sEspecificacion,
                                                      sUnidad,
                                                      dbcantidad.ToString("N2"),
                                                      dbPrecioUnitario.ToString("N6"),
                                                      dbPorcentajeDescuento.ToString("N2"),
                                                      dbDescuento.ToString("N4"),
                                                      dbSubtotal.ToString("N2"),
                                                      sStock
                                                    );
                        }
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        //Función para llenar las cajas de text
        private void llenarCajasTexto()
        {
            try
            {
                if (dgvDetalleVenta.Rows.Count > 0)
                {
                    double dbValorBruto = 0;
                    double dbDescuento = 0;
                    double dbValorNeto = 0;
                    double dbIva = 0;
                    double dbValorTotal = 0;

                    for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                    {
                        dbValorBruto += Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[9].Value.ToString());
                        dbDescuento += Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[8].Value.ToString());
                    }

                    dbValorNeto = (dbValorBruto - dbDescuento);
                    dbIva = ((dbValorNeto * Convert.ToDouble(txtIva.Text)/100));
                    dbValorTotal = dbValorNeto + dbIva;

                    txtValorBruto.Text = dbValorBruto.ToString("N2");
                    txtDescuento1.Text = dbDescuento.ToString("N2");
                    txtValorNeto.Text = dbValorNeto.ToString("N2");
                    txtIva1.Text = dbIva.ToString("N2");
                    txtValorTotal.Text = dbValorTotal.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            activarControles();
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            dgvDetalleVenta.Rows.Clear();
            txtIngresoNumeros.Clear();
            txtNumeros.Clear();
            cmbMotivos.SelectedIndex = 0;
            txtReferencia.Clear();
            txtNotaPedido.Clear();
            txtFacturaCompra.Clear();
            txtNotaEntrega.Clear();
            txtProveedor.Clear();
            txtNombreProveedor.Clear();
            txtPersona.Clear();
            txtNombrePersona.Clear();
            txtComentarios.Clear();
            txtValorBruto.Clear();
            txtDescuento1.Clear();
            txtValorNeto.Clear();
            txtIva1.Clear();
            txtValorTotal.Clear();
            btnA.Enabled = false;
            btnX.Enabled = false;
        }

        //Función para bloquear controles
        private void bloquearControles()
        {
            //btnX.Enabled = false;
            //btnA.Enabled = false;
            txtValorBruto.ReadOnly = true;
            txtDescuento1.ReadOnly = true;
            txtValorNeto.ReadOnly = true;
            txtIva1.ReadOnly = true;
            txtValorTotal.ReadOnly = true;
          //  btnAnular.Enabled = false;
            btnGrabar.Enabled = false;
            txtIngresoNumeros.ReadOnly = true;
            cmbMotivos.Enabled = false;
            cmbMoneda.Enabled = false;
            txtReferencia.ReadOnly = true;
            txtNotaPedido.ReadOnly = true;
            txtFacturaCompra.ReadOnly = true;
            txtNotaEntrega.ReadOnly = true;
            //btnOk.Enabled = false;
            txtProveedor.ReadOnly = true;
            btnAyudaProveedor.Enabled = false;
            txtNombreProveedor.ReadOnly = true;
            txtPersona.ReadOnly = true;
            button1.Enabled = false;
            txtComentarios.ReadOnly = true;
            txtIva.ReadOnly = true;
            txtDescuento.ReadOnly = true;
            btnAyudaIngresoNumeros.Enabled = false;
            cmbOficina.Enabled = true;
            dgvDetalleVenta.ReadOnly = true;
        }

        //Función para activar los controles
        private void activarControles()
        {
            btnX.Enabled = true;
            btnA.Enabled = true;
            txtValorBruto.ReadOnly = false;
            txtDescuento1.ReadOnly = false;
            txtValorNeto.ReadOnly = false;
            txtIva1.ReadOnly = false;
            txtValorTotal.ReadOnly = false;
            btnAnular.Enabled = true;
            btnGrabar.Enabled = true;
            txtIngresoNumeros.ReadOnly = false;
            cmbMotivos.Enabled = true;
            cmbMoneda.Enabled = true;
            txtReferencia.ReadOnly = false;
            txtNotaPedido.ReadOnly = false;
            txtFacturaCompra.ReadOnly = false;
            txtNotaEntrega.ReadOnly = false;
            btnOk.Enabled = true;
            txtProveedor.ReadOnly = false;
            btnAyudaProveedor.Enabled = true;
            txtNombreProveedor.ReadOnly = false;
            txtPersona.ReadOnly = false;
            button1.Enabled = true;
            txtComentarios.ReadOnly = false;
            txtIva.ReadOnly = false;
            txtDescuento.ReadOnly = false;
            btnAyudaIngresoNumeros.Enabled = true;
            //cmbOficina.Enabled = false;
            dgvDetalleVenta.ReadOnly = false;
        }

        private void btnAyudaProveedor_Click(object sender, EventArgs e)
        {
            Bodega.frmAyudaProveedores ayuda = new frmAyudaProveedores();
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                txtProveedor.Text = ayuda.sCodigo;
                txtNombreProveedor.Text = ayuda.sNombreProveedor;
                iCorrelativoProveedor = Convert.ToInt32(ayuda.sCorrelativo);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formularios.FAyuda ayuda = new Formularios.FAyuda();
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                txtPersona.Text = ayuda.sIdentificacion;
                txtNombrePersona.Text = ayuda.sNombre;
                iIdPersona = Convert.ToInt32(ayuda.sIdPersona);
            }
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            dgvDetalleVenta.Rows.Add("","?");
            dgvDetalleVenta.ClearSelection();
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDetalleVenta.Rows.RemoveAt((dgvDetalleVenta.Rows.Count - 1));
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == senderGrid.Columns[1].Index && e.RowIndex >= 0)
            {
                abrirDbAyuda();
            }
        }

        private void abrirDbAyuda()
        {
            Bodega.frmAyudaProductos ayuda = new frmAyudaProductos();
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                sCodigoProducto = ayuda.sCodigo;
                iIdProducto = Convert.ToInt32(ayuda.sIdProducto);
                sPagaIva = ayuda.sPagaIva;
                llenarFilaGrid();
            }
        }

        //Función para llenar cada fila del grid
        private void llenarFilaGrid()
        {
            try
            {
                string sfecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyy/MM/dd");

                sSql = "";
                sSql += "Select Codigo, Descripcion, Unidad_Compra, ID_Unidad_Compra," + Environment.NewLine;
                sSql += "Correlativo, Paga_Iva, correlativo" + Environment.NewLine;
                sSql += "from cv401_vw_productos " + Environment.NewLine;
                sSql += "where Codigo = '" + sCodigoProducto + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

                if (bRespuesta == true)
                {
                    dgvDetalleVenta.CurrentRow.Cells[0].Value = dtConsulta.Rows[0].ItemArray[0].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[2].Value = dtConsulta.Rows[0].ItemArray[1].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[4].Value = dtConsulta.Rows[0].ItemArray[2].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[7].Value = "0.00";
                    dgvDetalleVenta.CurrentRow.Cells[8].Value = "0.00";
                    dgvDetalleVenta.CurrentRow.Cells[11].Value = dtConsulta.Rows[0].ItemArray[6].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[12].Value = sPagaIva;


                    string sQuery;
                    sQuery = "";
                    sQuery += "select pPRO.valor" + Environment.NewLine;
                    sQuery += "from cv403_precios_productos PPRO, cv403_listas_precios LPREC," + Environment.NewLine;
                    sQuery += "cv401_productos PRO" + Environment.NewLine;
                    sQuery += "where PRO.id_producto = pPRO.id_producto" + Environment.NewLine;
                    sQuery += "and PPRO.id_lista_precio = LPREC.id_lista_precio" + Environment.NewLine;
                    sQuery += "and PRO.codigo =  '" + sCodigoProducto + "'" + Environment.NewLine;
                    sQuery += "and PRO.estado = 'A'" + Environment.NewLine;
                    sQuery += "and PPRO.estado = 'A'" + Environment.NewLine;
                    sQuery += "and PPRO.Fecha_Inicio <= Convert(DateTime,'" + sfecha + "',120)" + Environment.NewLine;
                    sQuery += "and PPRO.FECHA_Final >=  Convert(DateTime,'" + sfecha + "',120)" + Environment.NewLine;
                    sQuery += "and LPREC.lista_base = 1 ";

                    DataTable dtAyuda = new DataTable();
                    dtAyuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                    if (bRespuesta == true)
                    {
                        double dbPrecioUnitario =Convert.ToDouble(dtAyuda.Rows[0].ItemArray[0].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[6].Value = dbPrecioUnitario.ToString("N2");
                        dgvDetalleVenta.CurrentRow.Cells[13].Value = dbPrecioUnitario.ToString();
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        return;
                    }

                    sQuery = "";
                    sQuery += "select isnull(Sum(M.CANTIDAD),0) Saldo" + Environment.NewLine;
                    sQuery += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                    sQuery += "where C.id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                    sQuery += "and M.id_producto = " + iIdProducto + Environment.NewLine;
                    sQuery += "and C.Fecha <= Convert(DateTime,'" + sfecha + "', 120)" + Environment.NewLine;
                    sQuery += "and C.estado = 'A'" + Environment.NewLine;
                    sQuery += "and M.estado = 'A'" + Environment.NewLine;
                    sQuery += "and M.correlativo + 0 = M.correlativo + 0" + Environment.NewLine;
                    sQuery += "and C.ID_Movimiento_Bodega = M.ID_Movimiento_Bodega" + Environment.NewLine;
                    sQuery += "and C.cg_empresa = " + Program.iCgEmpresa;

                    dtAyuda = new DataTable();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                    if (bRespuesta == true)
                    {
                        double dbStock = Convert.ToDouble(dtAyuda.Rows[0].ItemArray[0].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[10].Value = dbStock.ToString("N2");
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "Desea Grabar...?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                grabarRegistro();
            }
        }

        //Función para grabar el registro en la base de datos
        private void grabarRegistro()
        {
            try
            {
                string sAño = txtFechaAplicacion.Text.Substring(6, 4);
                string sMes = txtFechaAplicacion.Text.Substring(3, 2);
                int iIdBodega = Convert.ToInt32(cmbBodega.SelectedValue);

                Clases.ObtenerNumeroMovimiento movimiento = new Clases.ObtenerNumeroMovimiento();

                //Para obtener el número de movimiento debo usar el método de la clase movimiento
                //y hay que enviar 5 parámetros: 
                //1) tipo de movimiento
                //2) Id de la bodega
                //3) El año del movimiento (2018)
                //4) El mes del movimiento ejemplo (06,12)
                //5) El codigo del movimiento


                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                //string sNumeroMovimiento = movimiento.devuelveCorrelativo("IN",iIdBodega,sAño,sMes,"MOV");
                string sNumeroMovimiento = devuelveCorrelativo("IN", iIdBodega, sAño, sMes, "MOV");

                if (sNumeroMovimiento == "Error")
                {
                    goto reversa;
                }

                //PREGUNTAR SOBRE CG_CLIENTE, ID_AUXILIAR, ID_PERSONA
                string sFecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");
                if (txtReferencia.Text == "")
                    txtReferencia.Text = "1";

                sSql = "";
                sSql += "Insert Into cv402_cabecera_movimientos (" + Environment.NewLine;
                sSql += "CG_EMPRESA, idEmpresa, ID_BODEGA, CG_TIPO_MOVIMIENTO," + Environment.NewLine;
                sSql += "CG_MOTIVO_MOVIMIENTO_BODEGA, CG_CLIENTE_PROVEEDOR," + Environment.NewLine;
                sSql += "ID_AUXILIAR, ID_PERSONA, ID_LOCALIDAD, Fecha, CG_MONEDA_BASE," + Environment.NewLine;
                sSql += "REFERENCIA_EXTERNA, NOTA_PEDIDO, FACTURA, NOTA_ENTREGA, OBSERVACION," + Environment.NewLine;
                sSql += "NUMERO_MOVIMIENTO, usuario_ingreso, terminal_ingreso, ESTADO,PORCENTAJE_IVA," + Environment.NewLine;
                sSql += "PORCENTAJE_DESCUENTO)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iCgEmpresa + ", " + Program.iIdEmpresa + ", " + Convert.ToInt32(cmbBodega.SelectedValue) + "," + Environment.NewLine;
                sSql += "8000, " + Convert.ToInt32(cmbMotivos.SelectedValue) + ", 6162, " + iCorrelativoProveedor + "," + Environment.NewLine;
                sSql += iIdPersona + ", " + Convert.ToInt32(cmbOficina.SelectedValue) + ", Convert(DateTime,'" + sFecha + "',120)," + Environment.NewLine;
                sSql += Program.iMoneda + ", '" + txtReferencia.Text.Trim() + "', '"+txtNotaPedido.Text + "'," + Environment.NewLine;
                sSql += "'" + txtFacturaCompra.Text.Trim() + "', '" + txtNotaEntrega.Text + "', '" + txtComentarios.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + sNumeroMovimiento + "', '" + Program.sDatosMaximo[0] + "', " + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', " + txtIva.Text.Trim() + ", " + txtDescuento.Text.Trim() + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                    goto reversa;
                }

                else
                {
                    int  NewCodigo =0 ;

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
                        NewCodigo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        goto fin; ;
                    }


                    for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                    {
                        int iIdProducto = Convert.ToInt32(dgvDetalleVenta.Rows[i].Cells[11].Value.ToString());
                        double dbCantidad = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[5].Value.ToString());
                        double dbValorUnitario = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[6].Value.ToString());
                        double dbValorDescuento = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[7].Value.ToString());
                        double dbValorIva = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[6].Value.ToString());
                        string sEpecificacion ;

                        if (dgvDetalleVenta.Rows[i].Cells[3].Value != null)
                        {
                            sEpecificacion = dgvDetalleVenta.Rows[i].Cells[3].Value.ToString();
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
                        sSql += iIdProducto + ", '" + sEpecificacion + "', " + NewCodigo + ", 546, " + Environment.NewLine;
                        sSql += dbCantidad + ", " + dbValorUnitario + ", 0, 0,'A')";    //REVISAR VALOR DEL IVA

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.Show();
                            goto reversa;
                        }

                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Registro Guardado Correctamente";
                    ok.ShowDialog();
                    goto fin;
                       
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
                goto reversa;
            }

            #region Funciones de Ayuda
        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                ok.ShowDialog();
                //MessageBox.Show("");
            }
        fin:
            {
                limpiarCampos();
            }
            #endregion

        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "Desea Anular...?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                anularRegistro();
            }
        }

        //Función para anular el registro
        private void anularRegistro()
        {
            try
            {
                string sEstado = "N";
                string sFecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");

                sSql = "";
                sSql += "update cv402_cabecera_movimientos Set" + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate() " + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega = " + iIdMovimientoBodega;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    goto reversa;
                }

                sSql = "";
                sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega= " + iIdMovimientoBodega;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                goto fin;


            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.Show();
                goto reversa;
            }


            #region Funciones de Ayuda
        reversa:
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios.";
                ok.ShowDialog();
            }
        fin:
            {
                limpiarCampos();
            }
            #endregion


        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "Desea imprimir...?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                Bodega.frmReporteIngresos ingresos = new frmReporteIngresos(iIdMovimientoBodega, txtIngresoNumeros.Text);
                ingresos.ShowDialog();
            }
        }

        //Función para buscar correlativo
        private void buscarCorrelativo()
        {
            sCodigo = "";
            string sAño = txtFechaAplicacion.Text.Substring(6, 4);
            string sMes = txtFechaAplicacion.Text.Substring(4, 1);

            sSql = "";
            sSql += "select codigo from cv402_bodegas" + Environment.NewLine;
            sSql += "where id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue);

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    sCodigo = dtConsulta.Rows[0].ItemArray[0].ToString();
                }
            }

            else
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                catchMensaje.Show();
                goto reversa;
            }

            string sReferencia = "IN" + sCodigo + "_" + sAño + "_" + sMes + "_" + Program.iCgEmpresa;

            sSql = "";
            sSql += "select valor_actual from tp_correlativos" + Environment.NewLine;
            sSql += "where referencia = '" + sReferencia + "'";

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    dbValorActual = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());

                    sSql = "";
                    sSql += "update tp_correlativos set" + Environment.NewLine;
                    sSql += "valor_actual =  " + (dbValorActual + 1) + Environment.NewLine;
                    sSql += "where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        goto reversa;
                    }
                    
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
                            iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        }
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        goto reversa;
                    }

                    string sFechaDesde = sAño + "-01-01";
                    string sFechaHasta = sAño + "-12-31";
                    string sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                    string sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

                    sSql = "";
                    sSql += "insert into tp_correlativos (" + Environment.NewLine;
                    sSql += "cg_sistema, codigo_correlativo, referencia, valido_desde," + Environment.NewLine;
                    sSql += "valido_hasta, valor_actual, desde, hasta, estado, origen_dato," + Environment.NewLine;
                    sSql += "numero_replica_trigger, estado_replica, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iCorrelativo + ", 'MOV', '" + sReferencia + "', '" + sFechaDesde + "'," + Environment.NewLine;
                    sSql += "'" + sFechaHasta + "', " + (dbValorActual + 1) + ", 0, 0, 'A', 1," + Environment.NewLine;
                    sSql += (dbValorActual + 1).ToString("N0") + ", 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        goto reversa;
                    }
                }

                goto fin;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        fin: {}
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetalleVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int iIva;
            double dbIva;
            double dbSumaIva;

            double dbCantidad = Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[5].Value);
            double dbPrecioUnitario = Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[13].Value);
            double dbSubTotal = dbCantidad * dbPrecioUnitario;
            double dbPorcentajeDescuento = Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[7].Value) / 100;
            double dbDescuento = dbSubTotal * dbPorcentajeDescuento ;
            double dbSubTotalNeto = dbSubTotal - dbDescuento;
            dgvDetalleVenta.CurrentRow.Cells[9].Value = dbSubTotalNeto.ToString("N2");
            dgvDetalleVenta.CurrentRow.Cells[8].Value = dbDescuento.ToString("N2");

            dbCantidad = 0;
            dbPrecioUnitario = 0;
            dbSubTotal = 0;
            dbPorcentajeDescuento = 0;
            dbDescuento = 0;
            dbSubTotalNeto = 0;
            dbSumaIva = 0;

            dbIva = Convert.ToDouble(txtIva.Text.Trim()) / 100;                

            for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
            {
                dbCantidad = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[5].Value);
                dbPrecioUnitario = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[13].Value);
                dbSubTotal = dbSubTotal + (dbCantidad * dbPrecioUnitario);
                dbPorcentajeDescuento = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[7].Value) / 100;
                dbDescuento = dbDescuento + (dbCantidad * dbPrecioUnitario * dbPorcentajeDescuento);
                //dbSubTotalNeto = dbSubTotalNeto + (dbCantidad * dbPrecioUnitario * dbPorcentajeDescuento);

                iIva = Convert.ToInt32(dgvDetalleVenta.Rows[i].Cells[12].Value);

                if (iIva == 1)
                {
                    dbSumaIva = dbSumaIva + ((dbCantidad * dbPrecioUnitario - (dbCantidad * dbPrecioUnitario * dbPorcentajeDescuento)) * dbIva);
                }
            }

            txtValorBruto.Text = dbSubTotal.ToString("N2");
            txtDescuento1.Text = dbDescuento.ToString("N2");
            txtValorNeto.Text = (dbSubTotal - dbDescuento).ToString("N2");
            txtIva1.Text = dbSumaIva.ToString("N2");
            txtValorTotal.Text = (dbSubTotal - dbDescuento + dbSumaIva).ToString("N2");
        }
    }
}
