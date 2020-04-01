using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmIngresoMateriaPrima : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        Bodega.ClaseMovimimentosBodega movimientos = new Bodega.ClaseMovimimentosBodega();

        string sSql;
        string sReferencia;
        string sCodigo;
        string sTabla;
        string sCampo;
        string sCodigoProducto;
        string sPagaIva;
        string sFecha;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdProducto;
        int iCgTipoMovimiento_MP = 8000;

        public frmIngresoMateriaPrima()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO
        
        //FUNCION PARA LLENAR EL COMBOBOX DE OFICINA LOCAL
        private void llenarComboOficina()
        {
            try
            {
                sSql = "";
                sSql += "Select LOC.id_localidad, BO.descripcion, BO.id_bodega" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.tipo = '1'" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and LOC.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbOficina.llenar(dtConsulta, sSql);
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE BODEGA
        private void llenarComboBodega()
        {
            try
            {
                sSql = "";
                sSql += "select id_bodega, descripcion" + Environment.NewLine;
                sSql += "from cv402_bodegas where categoria = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbBodega.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE MOTIVO
        private void llenarComboMotivo()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00643'" + Environment.NewLine;
                sSql += "and codigo in( '29','01','35','44','45','49','50')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbMotivo.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE MONEDA
        private void llenarComboMoneda()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo,valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla='SYS$00021'" + Environment.NewLine;
                sSql += "and estado='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbMoneda.llenar(dtConsulta, sSql);

                if (cmbMoneda.Items.Count > 0)
                {
                    cmbMoneda.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el dbAyuda de Ingresos
        private void llenarSentenciaIngresos()
        {
            try
            {
                sSql = "";
                sSql += "select numero_movimiento, referencia_externa, observacion," + Environment.NewLine;
                sSql += "id_movimiento_bodega, fecha, id_c_movimiento Id_Mov_Tesoreria , Estado " + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where cg_Empresa = " + Program.iCgEmpresa + Environment.NewLine;
                sSql += "and id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                sSql += "and cg_tipo_movimiento = 8000" + Environment.NewLine;
                sSql += "and id_orden_compra is null" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by fecha desc," + Environment.NewLine;
                sSql += "numero_movimiento desc";

                dBAyudaIngresoNumeros.Ver(sSql, "numero_movimiento", 3, 0, 0);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el dbAyuda de proveedores
        private void llenarSentenciaProveedores()
        {
            try
            {
                sSql = "";
                sSql += "Select Codigo, Nombre, Correlativo" + Environment.NewLine;
                sSql += "From cv405_vw_proveedores_bodega_pt" + Environment.NewLine;
                sSql += "Where Empresa = " + Program.iCgEmpresa + Environment.NewLine;
                sSql += "and ano_fiscal = '" + Program.sFechaSistema.ToString("yyyy") + "'";

                dBAyudaProveedor.Ver(sSql, "codigo", 2, 0, 1);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el dbAyuda de personas
        private void llenarSentenciaPersonas()
        {
            try
            {
                sSql = "";
                sSql += "select identificacion as Identificacion, apellidos + ' ' + isnull(nombres, '') as Persona, id_persona" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dBAyudaPersona.Ver(sSql, "Identificacion", 2, 0, 1);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                sSql += "m.id_c_movimiento, m.estado_replica," + Environment.NewLine;
                sSql += "case m.estado when 'A' then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos m left outer join" + Environment.NewLine;
                sSql += "cv404_auxiliares_contables a on m.id_auxiliar = a.id_auxiliar" + Environment.NewLine;
                sSql += "where m.Id_Movimiento_Bodega = " + dBAyudaIngresoNumeros.iId;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        string sFecha = dtConsulta.Rows[0][0].ToString();
                        txtFechaAplicacion.Text = Convert.ToDateTime(sFecha).ToString("dd/MM/yyy");
                        cmbMotivo.SelectedValue = dtConsulta.Rows[0][11].ToString();
                        txtReferencia.Text = sReferencia;
                        txtFacturaCompra.Text = dtConsulta.Rows[0][8].ToString(); ;
                        txtNotaPedido.Text = dtConsulta.Rows[0][7].ToString();
                        txtNotaEntrega.Text = dtConsulta.Rows[0][9].ToString();
                        txtComentarios.Text = dtConsulta.Rows[0][10].ToString();
                        int iIdAuxiliarContable = Convert.ToInt32(dtConsulta.Rows[0][12].ToString());
                        int iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][13].ToString());
                        txtIva.Text = dtConsulta.Rows[0][14].ToString();
                        txtDescuento.Text = dtConsulta.Rows[0][15].ToString();
                        cmbEstado.Text = dtConsulta.Rows[0][18].ToString();


                        string sSql1;
                        sSql1 = "";
                        sSql1 += "select codigo, descripcion, id_auxiliar" + Environment.NewLine;
                        sSql1 += "from cv404_auxiliares_contables " + Environment.NewLine;
                        sSql1 += "where id_auxiliar = " + iIdAuxiliarContable;

                        DataTable dtAyuda = new DataTable();
                        dtAyuda.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql1);

                        if (bRespuesta == true)
                        {
                            if (dtAyuda.Rows.Count > 0)
                            {
                                dBAyudaProveedor.txtIdentificacion.Text = dtAyuda.Rows[0][0].ToString();
                                dBAyudaProveedor.txtDatos.Text = dtAyuda.Rows[0][1].ToString();
                                dBAyudaProveedor.iId = Convert.ToInt32(dtAyuda.Rows[0][2].ToString());
                            }
                        }

                        else
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return;
                        }

                        sSql1 = "";
                        sSql1 += "select identificacion, apellidos +' '+ nombres, id_persona" + Environment.NewLine;
                        sSql1 += "from tp_personas" + Environment.NewLine;
                        sSql1 += "where id_persona = " + iIdPersona + Environment.NewLine;
                        sSql1 += "and estado = 'A'";

                        dtAyuda = new DataTable();
                        dtAyuda.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql1);

                        if (bRespuesta == true)
                        {
                            if (dtAyuda.Rows.Count > 0)
                            {
                                dBAyudaPersona.txtIdentificacion.Text = dtAyuda.Rows[0][0].ToString();
                                dBAyudaPersona.txtDatos.Text = dtAyuda.Rows[0][1].ToString();
                                dBAyudaPersona.iId = Convert.ToInt32(dtAyuda.Rows[0][2].ToString());
                            }
                        }

                        else
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            return;
                        }

                        cargarGrid();
                        llenarCajasTexto();
                        bloquearControles();

                        btnA.Enabled = true;
                        btnX.Enabled = true;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las cajas de texto
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
                    dbIva = ((dbValorNeto * Convert.ToDouble(txtIva.Text) / 100));
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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                sSql += "and MB.Id_Movimiento_Bodega = " + dBAyudaIngresoNumeros.iId + Environment.NewLine;
                sSql += "and MB.Estado = 'A'" + Environment.NewLine;
                sSql += "order By MB.Correlativo";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            Button btnAyuda = new Button();
                            btnAyuda.Text = "?";
                            btnAyuda.Width = 10;

                            if (dtConsulta.Rows[i][4] == null)
                                dtConsulta.Rows[i][4] = "";

                            int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i][3].ToString());
                            string sCodigo = dtConsulta.Rows[i][1].ToString();
                            string sDescripcion = dtConsulta.Rows[i][2].ToString();
                            string sEspecificacion = dtConsulta.Rows[i][4].ToString();
                            string sUnidad = dtConsulta.Rows[i][5].ToString();
                            double dbcantidad = Convert.ToDouble(dtConsulta.Rows[i][7].ToString());
                            double dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i][8].ToString());
                            double dbPorcentajeDescuento = Convert.ToDouble(dtConsulta.Rows[i][9].ToString());
                            double dbDescuento = Convert.ToDouble(dtConsulta.Rows[i][11].ToString());
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
                            string sStock = "";
                            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql1);

                            if (bRespuesta == true)
                            {
                                if (dtAyuda.Rows.Count > 0)
                                {
                                    sStock = dtAyuda.Rows[0][0].ToString();
                                }
                            }

                            else
                            {
                                catchMensaje.lblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
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
                                                      sStock,
                                                      dbPrecioUnitario.ToString()
                                                    );
                        }
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para bloquear controles
        private void bloquearControles()
        {
            txtValorBruto.ReadOnly = true;
            txtDescuento1.ReadOnly = true;
            txtValorNeto.ReadOnly = true;
            txtIva1.ReadOnly = true;
            txtValorTotal.ReadOnly = true;
            btnGrabar.Enabled = false;
            cmbMotivo.Enabled = false;
            cmbMoneda.Enabled = false;
            txtReferencia.ReadOnly = true;
            txtNotaPedido.ReadOnly = true;
            txtFacturaCompra.ReadOnly = true;
            txtNotaEntrega.ReadOnly = true;
            txtComentarios.ReadOnly = true;
            txtIva.ReadOnly = true;
            txtDescuento.ReadOnly = true;
            cmbOficina.Enabled = true;
            dgvDetalleVenta.ReadOnly = true;
        }

        //Función para verificar Campos
        private void verificarCampos()
        {
            if (cmbMotivo.SelectedIndex == 0)
            {
                ok.lblMensaje.Text = "ADVERTENCIA:" + Environment.NewLine + "Debe seleccionar el motivo del movimiento.";
                ok.ShowDialog();
            }

            else if (txtFacturaCompra.Text == "")
            {
                ok.lblMensaje.Text = "ADVERTENCIA:" + Environment.NewLine + "Debe ingresar el número de factura de compra.";
                ok.ShowDialog();
            }

            else if (dBAyudaProveedor.iId == 0)
            {
                ok.lblMensaje.Text = "ADVERTENCIA:" + Environment.NewLine + "Debe seleccionar el proveedor.";
                ok.ShowDialog();
            }

            else
            {
                btnA.Enabled = true;
                btnX.Enabled = true;
            }
        }

        //Función para grabar el registro en la base de datos
        private void grabarRegistro()
        {
            try
            {

                //buscarCorrelativo();

                string sAnio = txtFechaAplicacion.Text.Substring(6, 4);
                string sMes = txtFechaAplicacion.Text.Substring(3, 2);
                int iIdBodega = Convert.ToInt32(cmbBodega.SelectedValue);
                string sFechaEnviar = Convert.ToDateTime(txtFechaAplicacion.Text.Trim()).ToString("yyyy/MM/dd");

                //Clases.ObtenerNumeroMovimiento movimiento = new Clases.ObtenerNumeroMovimiento();

                //Para obtener el número de movimiento debo usar el método de la clase movimiento
                //y hay que enviar 5 parámetros: 
                //1) tipo de movimiento

                //3) El año del movimiento (2018)
                //4) El mes del movimiento ejemplo (06,12)
                //5) El codigo del movimiento

                if (txtReferencia.Text == "")
                {
                    txtReferencia.Text = "1";
                }

                if (txtDescuento.Text.Trim() == "")
                {
                    txtDescuento.Text = "0";
                }


                if (movimientos.realizarIngreso("IN", Convert.ToInt32(cmbBodega.SelectedValue), sAnio, sMes, "MOV",
                                                Convert.ToInt32(cmbMotivo.SelectedValue), dBAyudaProveedor.iId, dBAyudaPersona.iId,
                                                Convert.ToInt32(cmbOficina.SelectedValue), sFechaEnviar, txtReferencia.Text.Trim().ToUpper(),
                                                txtNotaPedido.Text.Trim().ToUpper(), txtFacturaCompra.Text.Trim().ToUpper(),
                                                txtNotaEntrega.Text.Trim().ToUpper(), txtComentarios.Text.Trim().ToUpper(),
                                                txtIva.Text.Trim(), txtDescuento.Text.Trim(), dgvDetalleVenta, iCgTipoMovimiento_MP) == true)
                {
                    ok.lblMensaje.Text = "Registro Guardado Correctamente";
                    ok.ShowDialog();
                    limpiarCampos();
                    llenarSentenciaIngresos();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();                
            }  
        }

        //Función para anular el registro
        private void anularRegistro()
        {
            try
            {
                if (movimientos.eliminarRegistroIngreso(dBAyudaIngresoNumeros.iId) == true)
                {
                    ok.lblMensaje.Text = "Registro Anulado Correctamente";
                    ok.ShowDialog();
                    limpiarCampos();
                    llenarSentenciaIngresos();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
                
        private void abrirDbAyuda()
        {
            try
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

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDetalleVenta.CurrentRow.Cells[0].Value = dtConsulta.Rows[0][0].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[2].Value = dtConsulta.Rows[0][1].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[4].Value = dtConsulta.Rows[0][2].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[7].Value = "0.00";
                    dgvDetalleVenta.CurrentRow.Cells[8].Value = "0.00";
                    dgvDetalleVenta.CurrentRow.Cells[11].Value = dtConsulta.Rows[0][6].ToString();
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
                    //sQuery += "and PPRO.Fecha_Inicio <= Convert(DateTime,'" + sfecha + "',120)" + Environment.NewLine;
                    //sQuery += "and PPRO.FECHA_Final >=  Convert(DateTime,'" + sfecha + "',120)" + Environment.NewLine;
                    sQuery += "and LPREC.lista_base = 1 ";

                    DataTable dtAyuda = new DataTable();
                    dtAyuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                    if (bRespuesta == true)
                    {
                        double dbPrecioUnitario = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[6].Value = dbPrecioUnitario.ToString("N2");
                        dgvDetalleVenta.CurrentRow.Cells[13].Value = dbPrecioUnitario.ToString();
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
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
                        double dbStock = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[10].Value = dbStock.ToString("N2");
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
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
            dBAyudaIngresoNumeros.limpiar();
            dBAyudaProveedor.limpiar();
            cmbMoneda.Enabled = true;
            txtReferencia.ReadOnly = false;
            txtNotaPedido.ReadOnly = false;
            txtFacturaCompra.ReadOnly = false;
            txtNotaEntrega.ReadOnly = false;
            btnOk.Enabled = true;
            txtComentarios.ReadOnly = false;
            txtIva.ReadOnly = false;
            txtDescuento.ReadOnly = false;
            dgvDetalleVenta.ReadOnly = false;
        }

        //LIMPIAR CAMPOS
        private void limpiarCampos()
        {
            dgvDetalleVenta.Rows.Clear();
            dBAyudaIngresoNumeros.limpiar();
            dBAyudaProveedor.limpiar();
            dBAyudaPersona.limpiar();
            cmbMotivo.SelectedIndex = 0;
            cmbMotivo.Enabled = true;
            cmbMoneda.Enabled = true;
            txtReferencia.Clear();
            txtNotaPedido.Clear();
            txtFacturaCompra.Clear();
            txtNotaEntrega.Clear();
            txtComentarios.Clear();
            txtValorBruto.Clear();
            txtDescuento1.Clear();
            txtValorNeto.Clear();
            txtIva1.Clear();
            txtValorTotal.Clear();
            btnA.Enabled = false;
            btnX.Enabled = false;
        }

        #endregion

        private void frmIngresoBodega_Load(object sender, EventArgs e)
        {
            llenarComboOficina();
            llenarComboBodega();
            llenarComboMotivo();
            llenarComboMoneda();
            llenarSentenciaProveedores();
            llenarSentenciaPersonas();
            txtIva.Text = (Program.iva * 100).ToString("N0");
            txtFechaAplicacion.Text = Program.sFechaSistema.ToString("dd/MM/yyyy");
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
                    if (cmbBodega.Items.Count > 0)
                    {
                        cmbBodega.SelectedValue = dtConsulta.Rows[0][0];
                        llenarSentenciaIngresos();
                        dBAyudaIngresoNumeros.Enabled = true;
                    }

                    else
                    {
                        dBAyudaIngresoNumeros.Enabled = false;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dBAyudaIngresoNumeros.iId != 0)
            {
                cargarDatos();
            }

            else
            {
                verificarCampos();
            }

            dgvDetalleVenta.ClearSelection();
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            dgvDetalleVenta.Rows.Add("", "?");
            dgvDetalleVenta.ClearSelection();
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            if (dgvDetalleVenta.SelectedRows.Count > 0)
            {
                NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea seleccionada?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    dgvDetalleVenta.Rows.Remove(dgvDetalleVenta.CurrentRow);

                    dgvDetalleVenta.ClearSelection();
                }
            }
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
            double dbDescuento = dbSubTotal * dbPorcentajeDescuento;
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

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea imprimir...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                Bodega.frmReporteIngresos ingresos = new frmReporteIngresos(dBAyudaIngresoNumeros.iId, dBAyudaIngresoNumeros.txtIdentificacion.Text);
                ingresos.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            activarControles();
            limpiarCampos();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dBAyudaIngresoNumeros.iId == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado ningón movimiento para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                NuevoSiNo.lblMensaje.Text = "¿Desea eliminar...?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    anularRegistro();
                }
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea grabar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                grabarRegistro();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
