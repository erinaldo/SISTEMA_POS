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

namespace Palatium.Reportes_Formas
{
    public partial class frmListadoResumidoConsumo : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sFecha;
        string sFechaDesde;
        string sFechaHasta;

        bool bRespuesta;

        int iHabilitarEscape;
        int iIdLocalidad;

        DataTable dtConsulta;

        SqlParameter[] parametro;

        public frmListadoResumidoConsumo(int iHabilitarEscape_P)
        {
            this.iHabilitarEscape = iHabilitarEscape_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR EL COMBOBOX DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtConsulta.NewRow();
                row["id_localidad"] = "0";
                row["nombre_localidad"] = "Todos...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbLocalidades.DisplayMember = "nombre_localidad";
                cmbLocalidades.ValueMember = "id_localidad";
                cmbLocalidades.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                int a = 2;

                sSql = "";
                sSql += "select id_producto, codigo, sum(cantidad) cantidad, nombre," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 0), 10, 2)) total" + Environment.NewLine;
                sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where fecha_pedido between @fecha_desde" + Environment.NewLine;
                sSql += "and @fecha_hasta" + Environment.NewLine;

                if (Convert.ToInt32(cmbLocalidades.SelectedValue) != 0)
                {
                    a++;
                    sSql += "and id_localidad = @id_localidad" + Environment.NewLine;
                }

                sSql += "group by id_producto, codigo, nombre" + Environment.NewLine;
                sSql += "order by sum(cantidad) desc";

                parametro = new SqlParameter[a];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@fecha_desde";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = sFechaDesde;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@fecha_hasta";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = sFechaHasta;

                if (a == 3)
                {
                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@id_localidad";
                    parametro[2].SqlDbType = SqlDbType.Int;
                    parametro[2].Value = iIdLocalidad;
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    btnDetallarTodos.Visible = false;
                    txtTotalValor.Text = "0.00";
                    txtCantidadTotal.Text = "0";
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    btnDetallarTodos.Visible = false;
                    txtTotalValor.Text = "0.00";
                    txtCantidadTotal.Text = "0";
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    return;
                }

                Decimal dbCantidad_A = 0;
                Decimal dbTotal_A = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_producto"].ToString(),
                                      dtConsulta.Rows[i]["codigo"].ToString(),
                                      dtConsulta.Rows[i]["cantidad"].ToString(),
                                      dtConsulta.Rows[i]["nombre"].ToString(),
                                      dtConsulta.Rows[i]["total"].ToString());

                    dbCantidad_A += Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString());
                    dbTotal_A += Convert.ToDecimal(dtConsulta.Rows[i]["total"].ToString());
                }

                txtCantidadTotal.Text = dbCantidad_A.ToString();
                txtTotalValor.Text = dbTotal_A.ToString();
                btnDetallarTodos.Visible = true;
                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGridDetallado(int iIdLocalidad_P, int iIdProducto_P, string sFechaDesde_P, string sFechaHasta_P)
        {
            try
            {
                dgvDetalle.Rows.Clear();

                int a = 2;

                sSql = "";
                sSql += "select * from pos_vw_detallar_items_reporte_consumo" + Environment.NewLine;
                sSql += "where fecha_pedido between '2020-04-01'" + Environment.NewLine;
                sSql += "and '2020-04-18'" + Environment.NewLine;

                if (iIdLocalidad != 0)
                {
                    a++;
                    sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                }

                if (iIdProducto_P != 0)
                {
                    a++;
                    sSql += "and id_producto = @id_producto" + Environment.NewLine;
                }

                sSql += "order by numero_pedido";

                int b = 0;
                parametro = new SqlParameter[a];
                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_desde";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = sFechaDesde_P;
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_hasta";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = sFechaHasta_P;
                b++;

                if (iIdLocalidad != 0)
                {
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@id_localidad";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = iIdLocalidad_P;
                    b++;
                }

                if (iIdProducto_P != 0)
                {
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@id_producto";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = iIdProducto_P;
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    btnDetallarTodos.Visible = false;
                    txtTotalValor.Text = "0.00";
                    txtCantidadTotal.Text = "0";
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    return;
                }

                tabControlProductos.TabPages.Add(tabDetallado);

                Decimal dbTotal_A = 0;
                int iNumeroFactura_A;

                string sEstablecimiento_A;
                string sPuntoEmision_A;
                string sNumeroFactura_A;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    iNumeroFactura_A = Convert.ToInt32(dtConsulta.Rows[i]["numero_factura"].ToString());
                    sEstablecimiento_A = dtConsulta.Rows[i]["establecimiento"].ToString();
                    sPuntoEmision_A = dtConsulta.Rows[i]["punto_emision"].ToString();

                    if (iNumeroFactura_A == 0)
                        sNumeroFactura_A = "";
                    else
                        sNumeroFactura_A = sEstablecimiento_A + "-" + sPuntoEmision_A + "-" + iNumeroFactura_A.ToString().PadLeft(9, '0');

                    dgvDetalle.Rows.Add(Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["cantidad"].ToString(),
                                      dtConsulta.Rows[i]["nombre"].ToString(),
                                      dtConsulta.Rows[i]["valor"].ToString(),
                                      dtConsulta.Rows[i]["cliente"].ToString(),
                                      dtConsulta.Rows[i]["descripcion"].ToString(),
                                      dtConsulta.Rows[i]["numero_pedido"].ToString(),
                                      sNumeroFactura_A
                                      );

                    dbTotal_A += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtTotalDetallado.Text = dbTotal_A.ToString();
                tabControlProductos.SelectedTab = tabControlProductos.TabPages["tabDetallado"];
                dgvDetalle.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR LA FECHA DEL SISTEMA
        private void fechaSistema()
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("dd-MM-yyyy");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void limpiar()
        {
            fechaSistema();
            llenarComboLocalidades();
            dgvDatos.Rows.Clear();
            dtFechaDesde.Text = sFecha;
            dtFechaHasta.Text = sFecha;
            tabControlProductos.TabPages.Remove(tabDetallado);
            btnDetallarTodos.Visible = false;
            txtCantidadTotal.Text = "0";
            txtTotalDetallado.Text = "0.00";
            txtTotalValor.Text = "0.00";
            iIdLocalidad = 0;
            sFechaDesde = "";
            sFechaHasta = "";
        }

        #endregion

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaDesde.Text) > Convert.ToDateTime(dtFechaHasta.Text))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha final no debe ser superior a la fecha inicial.";
                ok.ShowDialog();
                dtFechaHasta.Text = sFecha;
                return;
            }

            iIdLocalidad = Convert.ToInt32(cmbLocalidades.SelectedValue);
            sFechaDesde = Convert.ToDateTime(dtFechaDesde.Text).ToString("yyyy-MM-dd");
            sFechaHasta = Convert.ToDateTime(dtFechaHasta.Text).ToString("yyyy-MM-dd");

            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmListadoResumidoConsumo_Load(object sender, EventArgs e)
        {
            limpiar();
            if (iHabilitarEscape == 1)
                this.KeyDown -= new KeyEventHandler(frmListadoResumidoConsumo_KeyDown);

            this.ActiveControl = cmbLocalidades;
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int iIdProducto_P = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_producto"].Value);
                tabControlProductos.TabPages.Remove(tabDetallado);
                llenarGridDetallado(iIdLocalidad, iIdProducto_P, sFechaDesde, sFechaHasta);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnDetallarTodos_Click(object sender, EventArgs e)
        {
            tabControlProductos.TabPages.Remove(tabDetallado);
            llenarGridDetallado(iIdLocalidad, 0, sFechaDesde, sFechaHasta);
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            tabControlProductos.SelectedTab = tabControlProductos.TabPages["tabResumido"];
        }

        private void frmListadoResumidoConsumo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
