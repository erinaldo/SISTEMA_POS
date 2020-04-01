    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmEgresoMateriaPrima : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Bodega.ClaseMovimimentosBodega movimientos = new Bodega.ClaseMovimimentosBodega();

        DataTable dtConsulta = new DataTable();

        string sSql;
        string sNumeroMovimiento;
        string sReferenciaExterna;
        string sObservacion;
        string sIdMovimientoBodega;
        string sFecha;
        string sidMovimientoTesoreria, sCodigoProducto;
        string sCodigoProveedor;
        string sNombreProveedor;
        string sCorrelativoProveedor;
        string sCampo;
        string sTabla;

        bool bRespuesta;
        bool bNuevo = false;
        
        int iIdProducto;        
        int iIdPersona;
        int iIdAuxiliarContable;
        
        long iMaximo;

        public frmEgresoMateriaPrima()
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
                sSql = "Select LOC.id_localidad, BO.descripcion, BO.id_bodega" + Environment.NewLine;
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

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                sSql += "Select C.Correlativo, C.valor_texto" + Environment.NewLine;
                sSql += "from tp_relaciones R , tp_codigos C" + Environment.NewLine;
                sSql += "where R.Tabla_Contenida = 'SYS$00643'" + Environment.NewLine;
                sSql += "and R.Tabla_Contenedora = 'SYS$00648'" + Environment.NewLine;
                sSql += "and R.Codigo_Contenedor = 'EMP'" + Environment.NewLine;
                sSql += "and R.CG_Tipo_Relacion In (53, -1)" + Environment.NewLine;
                sSql += "and R.Estado = 'A'" + Environment.NewLine;
                sSql += "and C.correlativo = R.Correlativo_Contenido" + Environment.NewLine;
                sSql += "and C.estado='A'" + Environment.NewLine;
                sSql += "group by  C.Correlativo, C.valor_texto" + Environment.NewLine;
                sSql += "order by C.valor_texto ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbMotivo.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el dbAyuda de Ingresos
        private void llenarSentenciaEgresos()
        {
            try
            {
                sSql = "";
                sSql += "select numero_movimiento, referencia_externa, observacion," + Environment.NewLine;
                sSql += "id_movimiento_bodega, fecha, id_c_movimiento Id_Mov_Tesoreria , Estado " + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where cg_Empresa = " + Program.iCgEmpresa + Environment.NewLine;
                sSql += "and id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                sSql += "and cg_tipo_movimiento = 7999" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "order by numero_movimiento desc";

                dBAyudaIngresoNumeros.Ver(sSql, "numero_movimiento", 3, 0, 0);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
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
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar los datos
        private void cargarDatos()
        {
            try
            {

                bNuevo = false;
                sSql = "";
                sSql += "Select m.Fecha, m.Cg_Empresa, m.ID_BODEGA," + conexion.GFun_St_esnulo() + "(m.CG_TIPO_MOVIMIENTO, '')cg_tipo_movimiento," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(m.Referencia_Externa, '') referencia_externa, " + conexion.GFun_St_esnulo() + "(m.ORDEN_TRABAJO, '') orden_trabajo," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(m.Orden_Diseno, '') orden_diseno, " + conexion.GFun_St_esnulo() + "(m.Nota_Entrega, '') nota_entrega," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(m.Observacion, '') observacion, " + conexion.GFun_St_esnulo() + "(m.cg_motivo_movimiento_bodega, 6166)cg_motivo_movimiento_bodega," + Environment.NewLine;
                sSql += "Codigo_Proveedor = a.codigo, m.id_pedido, m.id_auxiliar, " + conexion.GFun_St_esnulo() + "(m.id_persona, 64935) id_persona," + Environment.NewLine;
                sSql += "m.id_c_movimiento, m.estado_replica" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos m left outer join" + Environment.NewLine;
                sSql += "cv404_auxiliares_contables a on m.id_auxiliar = a.id_auxiliar" + Environment.NewLine;
                sSql += "Where m.Id_Movimiento_Bodega= " + dBAyudaIngresoNumeros.iId;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        string sFecha = dtConsulta.Rows[0][0].ToString();

                        txtFechaAplicacion.Text = Convert.ToDateTime(sFecha).ToString("dd/MM/yyy");

                        if (dtConsulta.Rows[0][9] != null || dtConsulta.Rows[0][9].ToString() != " ")
                            cmbMotivo.SelectedValue = dtConsulta.Rows[0][9].ToString();

                        if (dtConsulta.Rows[0][4].ToString() != null || dtConsulta.Rows[0][4].ToString() != "")
                            txtComentarios.Text = dtConsulta.Rows[0][4].ToString();

                        if (dtConsulta.Rows[0][5].ToString() != null || dtConsulta.Rows[0][5].ToString() != "")
                            txtOrdenFabricacion.Text = dtConsulta.Rows[0][5].ToString();

                        if (dtConsulta.Rows[0][6].ToString() != null || dtConsulta.Rows[0][6].ToString() != "")
                            txtOrdenDisenio.Text = dtConsulta.Rows[0][6].ToString();

                        if (dtConsulta.Rows[0][7].ToString() != null || dtConsulta.Rows[0][7].ToString() != "")
                            txtNotaEntrega.Text = dtConsulta.Rows[0][7].ToString();

                        if (dtConsulta.Rows[0][12].ToString() != null || dtConsulta.Rows[0][12].ToString() != "")
                            sCorrelativoProveedor = dtConsulta.Rows[0][12].ToString();

                        if (dtConsulta.Rows[0][13].ToString() != null || dtConsulta.Rows[0][13].ToString() != "")
                            iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][13].ToString());

                        if (dtConsulta.Rows[0][8].ToString() != "")
                        {
                            txtComentarios.Text = dtConsulta.Rows[0][8].ToString();
                        }

                        else
                        {
                            txtComentarios.Text = dtConsulta.Rows[0][4].ToString();
                        }
                        
                        string sSql1;
                        sSql1 = "";
                        sSql1 += "select codigo, descripcion, id_auxiliar" + Environment.NewLine;
                        sSql1 += "from cv404_auxiliares_contables" + Environment.NewLine;
                        sSql1 += "where id_auxiliar = " + sCorrelativoProveedor;

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
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.Show();
                            return;
                        }

                        sSql1 = "";
                        sSql1 += "select identificacion, apellidos +' '+ isnull(nombres, '') nombre, id_persona" + Environment.NewLine;
                        sSql1 += "from tp_personas" + Environment.NewLine;
                        sSql1 += "where id_persona = " + iIdPersona;

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
                            catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                            catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                            catchMensaje.Show();
                            return;
                        }

                        cargarGrid();

                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
                    return;
                }


            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para cargar el grid()
        private void cargarGrid()
        {
            try
            {

                dgvDetalleVenta.Rows.Clear();
                sSql = "";
                sSql += "select MB.CORRELATIVO Movimiento_Correlativo, P.codigo codigo_producto," + Environment.NewLine;
                sSql += "N.nombre producto, MB.Id_Producto, U.codigo unidad, MB.cg_unidad_compra cg_unidad_compra," + Environment.NewLine;
                sSql += "MB.CANTIDAD, MB.Precio_Promedio" + Environment.NewLine;
                sSql += "from cv402_movimientos_bodega MB, cv401_productos P," + Environment.NewLine;
                sSql += "tp_codigos U, cv401_nombre_productos N" + Environment.NewLine;
                sSql += "where MB.Id_Producto = P.Id_Producto" + Environment.NewLine;
                sSql += "and P.Id_Producto = N.Id_Producto" + Environment.NewLine;
                sSql += "and N.Nombre_Interno = 1" + Environment.NewLine;
                sSql += "and MB.CG_UNIDAD_COMPRA = U.Correlativo" + Environment.NewLine;
                sSql += "and MB.Id_Movimiento_Bodega = " + dBAyudaIngresoNumeros.iId + Environment.NewLine;
                sSql += "and MB.Estado = 'A'" + Environment.NewLine;
                sSql += "and N.Estado = 'A'" + Environment.NewLine;
                sSql += "order By MB.Movimiento_Correlativo";

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
                            string sUnidad = dtConsulta.Rows[i][4].ToString();
                            double dbcantidad = (Convert.ToDouble(dtConsulta.Rows[i][6].ToString()) * -1);
                            double dbPrecioUnitario;
                            string sAyuda = dtConsulta.Rows[i][7].ToString();
                            if (sAyuda != "")
                            {
                                dbPrecioUnitario = Convert.ToDouble(dtConsulta.Rows[i][7].ToString());
                            }
                            else
                                dbPrecioUnitario = 0;

                            double dbSubtotal = dbcantidad * dbPrecioUnitario;

                            string sFecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");

                            string sSql1;
                            sSql1 = "";
                            sSql1 += "select Sum(M.CANTIDAD) Saldo" + Environment.NewLine;
                            sSql1 += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                            sSql1 += "where C.id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                            sSql1 += "and M.id_producto = " + iIdProducto + Environment.NewLine;
                            sSql1 += "and C.Fecha <= Convert(DateTime,'" + sFecha + "',120)" + Environment.NewLine;
                            sSql1 += "and C.estado = 'A'" + Environment.NewLine;
                            sSql1 += "and M.estado = 'A'" + Environment.NewLine;
                            sSql1 += "and M.correlativo+0 = M.correlativo+0" + Environment.NewLine;
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
                                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                                catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                                catchMensaje.Show();
                            }

                            dgvDetalleVenta.Rows.Add(sCodigo,
                                                     "?",
                                                      sDescripcion,
                                                      sUnidad,
                                                      dbcantidad.ToString("N2"),
                                                      dbPrecioUnitario.ToString("N6"),
                                                      dbSubtotal.ToString("N2"),
                                                      sStock,
                                                      dbcantidad.ToString("N2"),
                                                      iIdProducto
                                                    );
                        }

                        double dbValorTotal = 0;
                        for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                        {

                            dbValorTotal += Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[6].Value.ToString());
                        }
                        txtValorTotal.Text = dbValorTotal.ToString("N2");

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
                sSql += "select Codigo, Descripcion, Unidad_Compra, ID_Unidad_Compra," + Environment.NewLine;
                sSql += "Correlativo, Paga_Iva, correlativo" + Environment.NewLine;
                sSql += "from cv401_vw_productos" + Environment.NewLine;
                sSql += "where Codigo = '" + sCodigoProducto + "'";


                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                if (bRespuesta == true)
                {
                    dgvDetalleVenta.CurrentRow.Cells[0].Value = dtConsulta.Rows[0][0].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[2].Value = dtConsulta.Rows[0][1].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[3].Value = dtConsulta.Rows[0][2].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[9].Value = dtConsulta.Rows[0][6].ToString();
                    dgvDetalleVenta.CurrentRow.Cells[8].Value = 0.000;


                    string sQuery;
                    sQuery = "";
                    sQuery += "select pPRO.valor" + Environment.NewLine;
                    sQuery += "from cv403_precios_productos PPRO, cv403_listas_precios LPREC," + Environment.NewLine;
                    sQuery += "cv401_productos PRO" + Environment.NewLine;
                    sQuery += "where PRO.id_producto = pPRO.id_producto" + Environment.NewLine;
                    sQuery += "and PPRO.id_lista_precio = LPREC.id_lista_precio" + Environment.NewLine;
                    sQuery += "and PRO.codigo = '" + sCodigoProducto + "'" + Environment.NewLine;
                    sQuery += "and PRO.estado = 'A'" + Environment.NewLine;
                    sQuery += "and PPRO.estado = 'A'" + Environment.NewLine;
                    sQuery += "and PPRO.Fecha_Inicio <= Convert(DateTime,'" + sfecha + "', 120)" + Environment.NewLine;
                    sQuery += "and PPRO.FECHA_Final >=  Convert(DateTime,'" + sfecha + "',120)" + Environment.NewLine;
                    sQuery += "and LPREC.lista_base = 1";

                    DataTable dtAyuda = new DataTable();
                    dtAyuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                    if (bRespuesta == true)
                    {
                        double dbPrecioUnitario = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[5].Value = dbPrecioUnitario.ToString("N2");
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        return;
                    }

                    sQuery = "";
                    sQuery += "select isnull(sum(" + conexion.GFun_St_esnulo() + "(M.CANTIDAD, 0)), 0) Saldo" + Environment.NewLine;
                    sQuery += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                    sQuery += "where C.id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                    sQuery += "and M.id_producto = " + iIdProducto + Environment.NewLine;
                    sQuery += "and C.Fecha <= Convert(DateTime,'" + sfecha + "', 120)" + Environment.NewLine;
                    sQuery += "and C.estado = 'A'" + Environment.NewLine;
                    sQuery += "and M.estado = 'A' And M.correlativo+0 = M.correlativo+0" + Environment.NewLine;
                    sQuery += "and C.ID_Movimiento_Bodega = M.ID_Movimiento_Bodega" + Environment.NewLine;
                    sQuery += "and C.cg_empresa = " + Program.iCgEmpresa;

                    dtAyuda = new DataTable();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                    if (bRespuesta == true)
                    {
                        double dbStock = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[7].Value = dbStock.ToString("N2");
                    }

                    else
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.Show();
                        return;
                    }

                    dgvDetalleVenta.CurrentCell = dgvDetalleVenta.CurrentRow.Cells[4];
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
                    //sPagaIva = ayuda.sPagaIva;
                    llenarFilaGrid();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para verificarCampos
        private void verificarCampos()
        {
            bNuevo = true;
            if (Convert.ToDouble(cmbMotivo.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "ADVERTENCIA:" + Environment.NewLine + "Debe seleccionar el motivo del movimiento.";
                ok.ShowDialog();
            }

            else if (txtComentarios.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "ADVERTENCIA: Debe ingresar la Referencia del ingreso.";
                ok.ShowDialog();
                txtComentarios.Focus();
            }

            else if (dBAyudaProveedor.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "ADVERTENCIA:" + Environment.NewLine + "Debe seleccionar el proveedor.";
                ok.ShowDialog();
            }

            else
            {
                btnA.Enabled = true;
                btnX.Enabled = true;
            }
        }

        //Función para limpiar los campos
        private void limpiarCampos()
        {
            dgvDetalleVenta.Rows.Clear();
            dBAyudaIngresoNumeros.limpiar();
            dBAyudaProveedor.limpiar();
            dBAyudaPersona.limpiar();
            cmbMotivo.SelectedIndex = 0;
            txtComentarios.Text = "";
            txtOrdenDisenio.Text = "";
            txtOrdenFabricacion.Text = "";
            txtNotaEntrega.Text = "";
            txtComentarios.Text = "";
            dgvDetalleVenta.Rows.Clear();
            btnA.Enabled = false;
            btnX.Enabled = false;
            bNuevo = true;
        }

        #endregion

        private void frmEgresoBodega_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaAplicacion.Text = sFecha;
            llenarComboBodega();
            llenarComboOficina();
            llenarComboMotivo();
            llenarSentenciaProveedores();
            llenarSentenciaPersonas();
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
                        llenarSentenciaEgresos();
                        dBAyudaIngresoNumeros.Enabled = true;
                    }

                    else
                    {
                        dBAyudaIngresoNumeros.Enabled = false;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.Show();
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

        
        private void btnX_Click(object sender, EventArgs e)
        {
            if (dgvDetalleVenta.SelectedRows.Count > 0)
            {
                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea seleccionada?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    dgvDetalleVenta.Rows.Remove(dgvDetalleVenta.CurrentRow);

                    dgvDetalleVenta.ClearSelection();
                }
            }
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            dgvDetalleVenta.Rows.Add("", "?");
        }

        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == senderGrid.Columns[1].Index && e.RowIndex >= 0 && dgvDetalleVenta.CurrentRow.Cells[0].Value.ToString() =="")
            {
                abrirDbAyuda();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Desea grabar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                if (bNuevo == true)
                {
                    grabarRegistro(1);
                }

                else
                {
                    actualizarRegistro(0);
                }
            }
        }

        //Función para grabar el registro en la base de datos
        private void grabarRegistro(int iBandera)
        {
            try
            {
                //PREGUNTAR SOBRE CG_CLIENTE, ID_AUXILIAR, ID_PERSONA

                if (txtComentarios.Text == "")
                {
                    txtComentarios.Text = "1";
                }

                if (txtComentarios.Text.Trim() == "")
                {
                    txtComentarios.Text = "0";
                }

                string sFechaEnviar = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");
                string sAnio = sFechaEnviar.Substring(0, 4);
                string sMes = sFechaEnviar.Substring(5, 2);

                if (movimientos.realizarEgreso("EG", Convert.ToInt32(cmbBodega.SelectedValue), sAnio, sMes, "MOV",
                                                Convert.ToInt32(cmbMotivo.SelectedValue), dBAyudaProveedor.iId, dBAyudaPersona.iId,
                                                Convert.ToInt32(cmbOficina.SelectedValue), sFechaEnviar, txtComentarios.Text.Trim().ToUpper(),
                                                txtOrdenFabricacion.Text.Trim().ToUpper(), txtOrdenDisenio.Text.Trim().ToUpper(),
                                                txtNotaEntrega.Text.Trim().ToUpper(), iBandera, 0, dBAyudaIngresoNumeros.iId,
                                                dBAyudaIngresoNumeros.txtIdentificacion.Text.Trim(), 0, dgvDetalleVenta, 7999) == true)
                {

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Registro Guardado Correctamente";
                    ok.ShowDialog();
                    llenarSentenciaEgresos();
                    limpiarCampos();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para actualizar el registro
        private void actualizarRegistro(int iBandera)
        {
            try
            {
                string sFechaEnviar = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyyy-MM-dd");
                string sAnio = sFechaEnviar.Substring(0, 4);
                string sMes = sFechaEnviar.Substring(5, 2);
                int iActualizar;

                if (iBandera == 0)
                {
                    iActualizar = 1;
                }

                else
                {
                    iActualizar = 2;
                }

                if (movimientos.realizarEgreso("EG", Convert.ToInt32(cmbBodega.SelectedValue), sAnio, sMes, "MOV",
                                                Convert.ToInt32(cmbMotivo.SelectedValue), dBAyudaProveedor.iId, dBAyudaPersona.iId,
                                                Convert.ToInt32(cmbOficina.SelectedValue), sFechaEnviar, txtComentarios.Text.Trim().ToUpper(),
                                                txtOrdenFabricacion.Text.Trim().ToUpper(), txtOrdenDisenio.Text.Trim().ToUpper(),
                                                txtNotaEntrega.Text.Trim().ToUpper(), 0, iActualizar, dBAyudaIngresoNumeros.iId,
                                                dBAyudaIngresoNumeros.txtIdentificacion.Text.Trim(), 1, dgvDetalleVenta, 7999) == true)
                {
                    if (iBandera == 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Registro actualizado éxitosamente";
                        ok.ShowDialog();
                        return;
                    }

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Registro eliminado éxitosamente";
                    ok.ShowDialog();
                    return;
                }   
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dBAyudaIngresoNumeros.iId == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado ningón movimiento para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                NuevoSiNo.lblMensaje.Text = "¿Desea eliminar...?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    actualizarRegistro(1);
                    limpiarCampos();
                    llenarSentenciaEgresos();
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                limpiarCampos();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Desea imprimir...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                Bodega.frmReporteIngresos ingresos = new frmReporteIngresos(dBAyudaIngresoNumeros.iId, dBAyudaIngresoNumeros.txtIdentificacion.Text);
                ingresos.ShowDialog();
            }            
        }

        private void dgvDetalleVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDetalleVenta.CurrentRow.Cells[4].Value.ToString().Trim() == "")
                {
                    dgvDetalleVenta.CurrentRow.Cells[4].Value = "0";
                }

                double dbCantidad = Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[4].Value);                

                if (dbCantidad > Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[7].Value))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La cantidad ingresada es mayor que el saldo en stock " + dgvDetalleVenta.CurrentRow.Cells[6].Value.ToString();
                    ok.ShowDialog();
                    dgvDetalleVenta.CurrentRow.Cells[4].Value = 0.00;
                    dgvDetalleVenta.CurrentRow.Cells[6].Value = 0.00;
                }

                else
                {
                    double dbSubtotal = Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[5].Value) * dbCantidad;
                    dgvDetalleVenta.CurrentRow.Cells[6].Value = dbSubtotal.ToString("N2");                    
                }

                double dbValorTotal = 0;
                for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                {

                    dbValorTotal += Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[6].Value.ToString());
                }
                txtValorTotal.Text = dbValorTotal.ToString("N2");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
