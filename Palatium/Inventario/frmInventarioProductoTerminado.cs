using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Inventario
{
    public partial class frmInventarioProductoTerminado : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Inventario.ClaseReporteInventario reporte = new Inventario.ClaseReporteInventario();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        bool bRespuesta;

        DataTable dtConsulta;
        DataTable dtAyuda;

        int iIdBodega;
        int iIdProducto;
        int iIdTipoMovimiento;
        int iCgTipoMovimiento_PT = 8001;

        public frmInventarioProductoTerminado()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA RECUPERAR LOS DATOS DE LA LOCALIDAD
        private void recuperarDatosLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select cg_tipo_movimiento_PT from tp_localidades" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                iIdTipoMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL COMBOBOX DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select LOC.id_localidad, BO.descripcion + ' ' + loc.establecimiento +" + Environment.NewLine;
                sSql += "case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end descripcion," + Environment.NewLine;
                sSql += "BO.id_bodega" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where LOC.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and BO.tipo = '2'" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.idempresa = BO.idempresa" + Environment.NewLine;
                sSql += "and LOC.idempresa = 1" + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "order by BO.descripcion + ' ' + loc.establecimiento + case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbLocalidades.llenar(dtConsulta, sSql);

                if (cmbLocalidades.Items.Count > 0)
                {
                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR EL COMBO DE BODEGAS
        private void llenarComboBodegas()
        {
            try
            {
                sSql = "";
                sSql += "select BO.id_bodega, BO.descripcion, BO.codigo, BO.categoria" + Environment.NewLine;
                sSql += "from cv402_bodegas BO, tp_localidades LOC" + Environment.NewLine;
                sSql += "where BO.id_bodega = LOC.id_bodega" + Environment.NewLine;
                sSql += "and BO.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.estado = 'A'" + Environment.NewLine;
                sSql += "and LOC.id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.DisplayMember = "descripcion";
                    cmbBodega.ValueMember = "id_bodega";
                    cmbBodega.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            cmbLocalidades.SelectedValueChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedValueChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboBodegas();
            recuperarDatosLocalidad();
            txtDesde.Text = DateTime.Now.ToString("yyyy/MM/dd");
            txtHasta.Text = DateTime.Now.ToString("yyyy/MM/dd");
            dgvDatos.Rows.Clear();
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvDatos.Rows.Clear();

                DataRow[] dFila = cmbLocalidades.dt.Select("id_localidad = " + cmbLocalidades.SelectedValue);

                if (dFila.Length != 0)
                {
                    iIdBodega = Convert.ToInt32(dFila[0][2].ToString());
                }

                sSql = "";
                sSql += "select PRD.correlativo, PRD.codigo, PRD.descripcion" + Environment.NewLine;
                sSql += "from cv402_movimientos_bodega MB, cv402_cabecera_movimientos CM, cv402_bodegas BO, tp_codigos EMP," + Environment.NewLine;
                sSql += "pos_vw_productos_inventario PRD, cv401_productos PRD_PADRE, pos_clase_producto CLASE" + Environment.NewLine;
                sSql += "Where CM.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and CM.id_bodega = " + iIdBodega + Environment.NewLine;
                sSql += "and CM.fecha <= '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and BO.id_bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
                sSql += "and MB.id_producto = PRD.correlativo" + Environment.NewLine;
                sSql += "and PRD.id_producto_padre = PRD_PADRE.id_producto" + Environment.NewLine;
                sSql += "and CM.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and MB.id_producto = PRD.Correlativo" + Environment.NewLine;
                sSql += "and PRD.id_producto_padre = PRD_PADRE.id_producto" + Environment.NewLine;
                sSql += "and CM.id_bodega = BO.id_bodega" + Environment.NewLine;
                sSql += "and CM.cg_empresa = EMP.correlativo" + Environment.NewLine;
                sSql += "and CM.id_movimiento_bodega = MB.id_movimiento_bodega" + Environment.NewLine;
                sSql += "and CLASE.id_pos_clase_producto = PRD.id_pos_clase_producto" + Environment.NewLine;
                sSql += "and PRD_PADRE.Codigo like '2%'" + Environment.NewLine;
                sSql += "and EMP.estado = 'A'" + Environment.NewLine;
                sSql += "and MB.estado =  'A'" + Environment.NewLine;
                sSql += "and CM.estado =  'A'" + Environment.NewLine;
                sSql += "and CLASE.estado = 'A'" + Environment.NewLine;
                sSql += "and CLASE.codigo = '02'" + Environment.NewLine;
                sSql += "group by PRD.codigo, PRD.descripcion, PRD.correlativo," + Environment.NewLine;
                sSql += "CM.idempresa, CM.id_bodega, BO.id_bodega, CLASE.codigo" + Environment.NewLine;
                sSql += "order by PRD.codigo asc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No existen registros con los parámetros ingresados";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    iIdProducto = Convert.ToInt32(dtConsulta.Rows[i]["correlativo"].ToString());

                    dgvDatos.Rows.Add(
                                        iIdProducto.ToString(),
                                        dtConsulta.Rows[i]["codigo"].ToString().Trim().ToUpper(),
                                        dtConsulta.Rows[i]["descripcion"].ToString().Trim(),
                                        "0", "0", "0", "0");

                    if (extraerCantidadActual(iIdProducto, i) == false)
                    {
                        return;
                    }

                    if (ingresoIngresosProductos(iIdProducto, i) == false)
                    {
                        return;
                    }

                    if (extraerEgresosProductos(iIdProducto, i) == false)
                    {
                        return;
                    }

                    for (int j = 0; j < dgvDatos.Rows.Count; j++)
                    {
                        Double dbActual = Convert.ToDouble(dgvDatos.Rows[j].Cells["saldo_actual"].Value);
                        Double dbIngreso = Convert.ToDouble(dgvDatos.Rows[j].Cells["ingresos"].Value);
                        Double dbEgreso = Convert.ToDouble(dgvDatos.Rows[j].Cells["salidas"].Value);
                        Double dbTotal = dbActual + dbIngreso + dbEgreso;

                        dgvDatos.Rows[j].Cells["stock"].Value = dbTotal.ToString();

                        if (dbTotal <= 0)
                        {
                            dgvDatos.Rows[j].Cells["codigo_producto"].Style.BackColor = Color.Yellow;
                        }
                    }
                }

                dgvDatos.ClearSelection();

                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid_2()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvDatos.Rows.Clear();

                DataRow[] dFila = cmbLocalidades.dt.Select("id_localidad = " + cmbLocalidades.SelectedValue);

                if (dFila.Length != 0)
                {
                    iIdBodega = Convert.ToInt32(dFila[0][2].ToString());
                }

                SqlParameter[] Parametros = new SqlParameter[7];
                Parametros[0] = new SqlParameter();
                Parametros[0].ParameterName = "@P_Ln_Empresa";
                Parametros[0].SqlDbType = SqlDbType.Int;
                Parametros[0].Value = Program.iCgEmpresa;

                Parametros[1] = new SqlParameter();
                Parametros[1].ParameterName = "@P_Ln_Moneda";
                Parametros[1].SqlDbType = SqlDbType.Int;
                Parametros[1].Value = Program.iMoneda;

                Parametros[2] = new SqlParameter();
                Parametros[2].ParameterName = "@P_Dt_Fecha_Inicio";
                Parametros[2].SqlDbType = SqlDbType.DateTime;
                Parametros[2].Value = Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd");

                Parametros[3] = new SqlParameter();
                Parametros[3].ParameterName = "@P_Dt_Fecha_Fin";
                Parametros[3].SqlDbType = SqlDbType.DateTime;
                Parametros[3].Value = Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd");

                Parametros[4] = new SqlParameter();
                Parametros[4].ParameterName = "@P_Ln_Articulo";
                Parametros[4].SqlDbType = SqlDbType.Int;
                Parametros[4].Value = 0;

                Parametros[5] = new SqlParameter();
                Parametros[5].ParameterName = "@P_Ln_Bodega";
                Parametros[5].SqlDbType = SqlDbType.Int;
                Parametros[5].Value = iIdBodega;

                Parametros[6] = new SqlParameter();
                Parametros[6].ParameterName = "@P_In_Valorizar";
                Parametros[6].SqlDbType = SqlDbType.Int;
                Parametros[6].Value = 0;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Ejecutar_Consulta_SP(dtConsulta, "Sp_Cv402_Inventario_02", Parametros);

                if (bRespuesta == false)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al consultar información del sistema.";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No existen registros con los parámetros ingresados";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["Id_Producto"].ToString(),
                                      dtConsulta.Rows[i]["Codigo"].ToString(),
                                      dtConsulta.Rows[i]["Descripcion"].ToString(),
                                      dtConsulta.Rows[i]["Saldo_Anterior"].ToString(),
                                      dtConsulta.Rows[i]["Ingresos"].ToString(),
                                      dtConsulta.Rows[i]["Egresos"].ToString(),
                                      dtConsulta.Rows[i]["Saldo_Actual"].ToString());                    
                }

                for (int j = 0; j < dgvDatos.Rows.Count; j++)
                {
                    Double dbActual = Convert.ToDouble(dgvDatos.Rows[j].Cells["stock"].Value);

                    if (dbActual <= 0)
                    {
                        dgvDatos.Rows[j].Cells["codigo_producto"].Style.BackColor = Color.Yellow;
                    }
                }

                dgvDatos.ClearSelection();
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }

        //FUNCION PARA EXTRAER EL SALDO ACTUAL
        private bool extraerCantidadActual(int iIdProducto_P, int iPosicionGrid_P)
        {
            try
            {
                sSql = "";
                sSql += "select Sum(M.cantidad) saldo" + Environment.NewLine;
                sSql += "from cv402_movimientos_bodega M,cv402_cabecera_movimientos C" + Environment.NewLine;
                sSql += "where C.id_bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
                sSql += "and M.id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and C.Fecha <= '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and C.estado = 'A'" + Environment.NewLine;
                sSql += "and M.estado = 'A'" + Environment.NewLine;
                sSql += "and M.correlativo + 0 = M.correlativo + 0" + Environment.NewLine;
                sSql += "and C.ID_Movimiento_Bodega = M.ID_Movimiento_Bodega" + Environment.NewLine;
                sSql += "and C.idempresa = " + Program.iIdEmpresa;

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                Double dbCantidad = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());

                dgvDatos.Rows[iPosicionGrid_P].Cells["saldo_actual"].Value = dbCantidad.ToString();                

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //EXTRAER EL INGRESO DEL PRODUCTO
        private bool ingresoIngresosProductos(int iIdProducto_P, int iPosicionGrid_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(M.cantidad),0) egreso" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos C left outer join" + Environment.NewLine;
                sSql += "cv404_auxiliares_contables AUX on C.Id_Auxiliar = AUX.Id_Auxiliar," + Environment.NewLine;
                sSql += "cv402_movimientos_bodega M, cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                sSql += "where C.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and C.Fecha = '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and C.id_bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
                sSql += "and C.id_movimiento_bodega = M.id_movimiento_bodega" + Environment.NewLine;
                sSql += "and M.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and M.id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and C.cg_tipo_movimiento = T.cg_tipo_movimiento_bodega" + Environment.NewLine;
                sSql += "and C.estado = 'A'" + Environment.NewLine;
                sSql += "and M.Estado = 'A'" + Environment.NewLine;
                sSql += "and C.cg_tipo_movimiento = " + iCgTipoMovimiento_PT;

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                Double dbCantidad = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());

                dgvDatos.Rows[iPosicionGrid_P].Cells["ingresos"].Value = dbCantidad.ToString();

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //EXTRAER EL EGRESO DEL PRODUCTO
        private bool extraerEgresosProductos(int iIdProducto_P, int iPosicionGrid_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(sum(M.cantidad),0) egreso" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos C left outer join" + Environment.NewLine;
                sSql += "cv404_auxiliares_contables AUX on C.Id_Auxiliar = AUX.Id_Auxiliar," + Environment.NewLine;
                sSql += "cv402_movimientos_bodega M, cv401_vw_nombre_producto P, cv402_tipo_movimientos T" + Environment.NewLine;
                sSql += "where C.idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql += "and C.Fecha = '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and C.id_bodega = " + cmbBodega.SelectedValue + Environment.NewLine;
                sSql += "and C.id_movimiento_bodega = M.id_movimiento_bodega" + Environment.NewLine;
                sSql += "and M.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and M.id_producto = " + iIdProducto_P + Environment.NewLine;
                sSql += "and C.cg_tipo_movimiento = T.cg_tipo_movimiento_bodega" + Environment.NewLine;
                sSql += "and C.estado = 'A'" + Environment.NewLine;
                sSql += "and M.Estado = 'A'" + Environment.NewLine;
                sSql += "and C.cg_tipo_movimiento = " + iIdTipoMovimiento;

                dtAyuda = new DataTable();
                dtAyuda.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                Double dbCantidad = Convert.ToDouble(dtAyuda.Rows[0][0].ToString());

                dgvDatos.Rows[iPosicionGrid_P].Cells["salidas"].Value = dbCantidad.ToString();
                
                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void frmInventarioProductoTerminado_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarComboBodegas();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbLocalidades.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la localidad.";
                ok.ShowDialog();
                return;
            }

            string sAux = Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd");
            DateTime dtInicio = Convert.ToDateTime(sAux);
            sAux = Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd");
            DateTime dtFinal = Convert.ToDateTime(sAux);

            if (dtFinal < dtInicio)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha inicial no puede ser superior a la fecha final.";
                ok.ShowDialog();
                return;
            }
            
            llenarGrid_2();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No existen registros para proceder a imprimir.";
                ok.ShowDialog();
                return;
            }

            dtConsulta = new DataTable();
            dtConsulta.Columns.Add("producto");
            dtConsulta.Columns.Add("cantidad_actual");
            dtConsulta.Columns.Add("ingresos");
            dtConsulta.Columns.Add("egresos");
            dtConsulta.Columns.Add("stock");

            this.Cursor = Cursors.WaitCursor;

            for (int i = 0; i < dgvDatos.Rows.Count; i++)
            {
                DataRow row = dtConsulta.NewRow();
                row["producto"] = dgvDatos.Rows[i].Cells["descripcion"].Value.ToString().Trim().ToUpper();
                row["cantidad_actual"] = dgvDatos.Rows[i].Cells["saldo_actual"].Value.ToString().Trim();
                row["ingresos"] = dgvDatos.Rows[i].Cells["ingresos"].Value.ToString().Trim();
                row["egresos"] = dgvDatos.Rows[i].Cells["salidas"].Value.ToString().Trim();
                row["stock"] = dgvDatos.Rows[i].Cells["stock"].Value.ToString().Trim();
                dtConsulta.Rows.Add(row);
            }

            if (reporte.crearReporteInventario(dtConsulta, cmbLocalidades.Text.Trim().ToUpper(), Convert.ToDateTime(txtDesde.Text.Trim()).ToString("dd-MM-yyyy"), Convert.ToDateTime(txtHasta.Text.Trim()).ToString("dd-MM-yyyy")) == false)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un error al generar el reporte de inventario de producto terminado.";
                ok.ShowDialog();
                return;
            }

            this.Cursor = Cursors.Default;
        }
    }
}
