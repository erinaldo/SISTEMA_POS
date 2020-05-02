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
    public partial class frmEstadoCuentaRepExterno : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;
        string sFecha;
        string sFechaDesde;
        string sFechaHasta;

        DataTable dtConsulta;
        DataTable dtLocalidades;

        bool bRespuesta;

        int iBanderaComprobantesElectronicos;

        SqlParameter[] parametro;

        public frmEstadoCuentaRepExterno()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad, emite_comprobante_electronico" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtLocalidades = new DataTable();
                dtLocalidades.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtLocalidades, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtLocalidades.NewRow();
                row["id_localidad"] = "0";
                row["nombre_localidad"] = "Seleccione...!!!";
                dtLocalidades.Rows.InsertAt(row, 0);

                cmbLocalidades.DisplayMember = "nombre_localidad";
                cmbLocalidades.ValueMember = "id_localidad";
                cmbLocalidades.DataSource = dtLocalidades;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE REPARTIDRES EXTERNOS
        private void llenarComboRepartidoresExternos()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where repartidor_externo = @repartidor_externo" + Environment.NewLine;
                sSql += "and estado = @estado" + Environment.NewLine;
                sSql += "order by descripcion";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@repartidor_externo";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 0;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtConsulta.NewRow();
                row["id_pos_origen_orden"] = "0";
                row["descripcion"] = "Seleccione...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbRepartidores.DisplayMember = "descripcion";
                cmbRepartidores.ValueMember = "id_pos_origen_orden";
                cmbRepartidores.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select id_factura, fecha_factura, numero_pedido, establecimiento, punto_emision, numero_factura, identificacion," + Environment.NewLine;
                sSql += "ltrim(isnull(nombres, '') + ' ' + apellidos) cliente, clave_acceso, autorizacion, facturaelectronica," + Environment.NewLine;
                sSql += "isnull(sum(case paga_iva when 0 then cantidad * precio_unitario end), 0) base_iva_cero," + Environment.NewLine;
                sSql += "isnull(sum(case paga_iva when 1 then cantidad * precio_unitario end), 0) base_iva," + Environment.NewLine;
                sSql += "isnull(sum(cantidad * valor_dscto), 0) valor_descuento," + Environment.NewLine;
                sSql += "isnull(sum(cantidad * valor_iva), 0) valor_iva," + Environment.NewLine;
                sSql += "isnull(sum(cantidad * valor_otro), 0) valor_servicio," + Environment.NewLine;
                sSql += "isnull(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 0) total" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and idtipocomprobante = @idtipocomprobante" + Environment.NewLine;
                sSql += "and id_pos_origen_orden = @id_pos_origen_orden" + Environment.NewLine;
                sSql += "and fecha_factura between @fecha_inicio" + Environment.NewLine;
                sSql += "and @fecha_final" + Environment.NewLine;
                sSql += "group by id_factura, id_localidad, fecha_factura, numero_pedido, establecimiento," + Environment.NewLine;
                sSql += "punto_emision, numero_factura, identificacion, nombres, apellidos, clave_acceso, autorizacion, facturaelectronica" + Environment.NewLine;
                sSql += "order by numero_factura";

                #region PARAMETROS

                int a = 0;
                parametro = new SqlParameter[5];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_localidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbLocalidades.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@idtipocomprobante";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_pos_origen_orden";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbRepartidores.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@fecha_inicio";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sFechaDesde;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@fecha_final";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sFechaHasta;

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No existen registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    return;
                }

                string sEstablecimiento_P;
                string sPuntoEmision_P;
                string sNumeroFactura_P;
                string sNumeroAutorizacion_P;
                int iFacturaElectronica_P;
                bool bEstadoFacElectronica_P;
                bool bEstadoAutorizado_P;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    bEstadoFacElectronica_P = false;
                    bEstadoAutorizado_P = false;

                    sEstablecimiento_P = dtConsulta.Rows[i]["establecimiento"].ToString().Trim();
                    sPuntoEmision_P = dtConsulta.Rows[i]["punto_emision"].ToString().Trim();
                    sNumeroFactura_P = dtConsulta.Rows[i]["numero_factura"].ToString().Trim().PadLeft(9, '0');
                    sNumeroAutorizacion_P = dtConsulta.Rows[i]["autorizacion"].ToString().Trim();
                    iFacturaElectronica_P = Convert.ToInt32(dtConsulta.Rows[i]["facturaelectronica"].ToString());

                    if (iFacturaElectronica_P == 1)
                    {
                        bEstadoFacElectronica_P = true;

                        if (sNumeroAutorizacion_P != "")
                            bEstadoAutorizado_P = true;
                    }

                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_factura"].ToString().Trim(),
                                      sEstablecimiento_P,
                                      sPuntoEmision_P,
                                      dtConsulta.Rows[i]["numero_factura"].ToString().Trim(),
                                      dtConsulta.Rows[i]["clave_acceso"].ToString().Trim(),
                                      "Fac",
                                      bEstadoFacElectronica_P,
                                      bEstadoAutorizado_P,
                                      Convert.ToDateTime(dtConsulta.Rows[i]["fecha_factura"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["numero_pedido"].ToString().Trim(),
                                      sEstablecimiento_P + "-" + sPuntoEmision_P + "-" + sNumeroFactura_P,
                                      dtConsulta.Rows[i]["identificacion"].ToString().Trim(),
                                      dtConsulta.Rows[i]["cliente"].ToString().Trim(),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["base_iva_cero"].ToString()).ToString("N2"),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["base_iva"].ToString()).ToString("N2"),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["valor_descuento"].ToString()).ToString("N2"),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["valor_iva"].ToString()).ToString("N2"),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["valor_servicio"].ToString()).ToString("N2"),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["total"].ToString()).ToString("N2"),
                                      "0.00", "0.00", "0.00"
                        );
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION DE LAS COLUMAS DEL DATAGRID
        private void columasGrid(bool ok)
        {
            try
            {
                dgvDatos.Columns["facturaelectronica"].Visible = ok;
                dgvDatos.Columns["autorizado"].Visible = ok;
                dgvDatos.Columns["sri"].Visible = ok;
                dgvDatos.Columns["diferencia"].Visible = ok;
                dgvDatos.Columns["total_orden"].Visible = ok;
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
            fechaSistema();
            llenarComboLocalidades();
            llenarComboRepartidoresExternos();
            columasGrid(false);
            dgvDatos.Rows.Clear();

            cmbLocalidades.Focus();
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

        #endregion

        private void frmEstadoCuentaRepExterno_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbLocalidades.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la localidad.";
                ok.ShowDialog();
                cmbLocalidades.Focus();
                return;
            }

            if (Convert.ToInt32(cmbRepartidores.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el repartidor externo.";
                ok.ShowDialog();
                cmbRepartidores.Focus();
                return;
            }

            if (Convert.ToDateTime(dtFechaDesde.Text) > Convert.ToDateTime(dtFechaHasta.Text))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha final no debe ser superior a la fecha inicial.";
                ok.ShowDialog();
                dtFechaHasta.Text = sFecha;
                return;
            }

            sFechaDesde = Convert.ToDateTime(dtFechaDesde.Text).ToString("yyyy-MM-dd");
            sFechaHasta = Convert.ToDateTime(dtFechaHasta.Text).ToString("yyyy-MM-dd");

            int iIdLocalidad_P = Convert.ToInt32(cmbLocalidades.SelectedValue);
            DataRow[] fila = dtLocalidades.Select("id_localidad = " + iIdLocalidad_P);

            if (fila.Length != 0)
            {
                if (Convert.ToInt32(fila[0][2].ToString()) == 1)
                    iBanderaComprobantesElectronicos = 1;
                else
                    iBanderaComprobantesElectronicos = 0;
            }

            else
                iBanderaComprobantesElectronicos = 0;

            if (iBanderaComprobantesElectronicos == 1)
                columasGrid(true);
            else
                columasGrid(false);

            llenarGrid();
        }

        private void frmEstadoCuentaRepExterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
