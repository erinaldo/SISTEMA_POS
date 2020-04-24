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
    public partial class frmReporteVentasPorItems : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sFecha;

        DataTable dtConsulta;

        int iHabilitarEscape;

        bool bRespuesta;

        SqlParameter[] parametro;

        public frmReporteVentasPorItems(int iHabilitarEscape_P)
        {
            this.iHabilitarEscape = iHabilitarEscape_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE LOCALIDADES
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

        //FUNCIONPARA LLENAR EL COMBOBOX DE TIPO DE COMPROBANTES
        private void llenarComboTipoComprobante()
        {
            try
            {
                sSql = "";
                sSql += "select idtipocomprobante, descripcion" + Environment.NewLine;
                sSql += "from vta_tipocomprobante" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and codigo in (@comprobante_1, @comprobante_2)";

                #region PARAMETROS

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@comprobante_1";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "Fac";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@comprobante_2";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "Nen";

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
                row["idtipocomprobante"] = "0";
                row["descripcion"] = "Todos...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbTipoComprobantes.DisplayMember = "descripcion";
                cmbTipoComprobantes.ValueMember = "idtipocomprobante";
                cmbTipoComprobantes.DataSource = dtConsulta;
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
                btnExcel.Visible = false;
                txtSubtotal.Text = "0.00";
                txtDescuento.Text = "0.00";
                txtNeto.Text = "0.00";
                txtIva.Text = "0.00";
                txtServicio.Text = "0.00";
                txtTotal.Text = "0.00";

                int a = 2;

                sSql = "";
                sSql += "select * from pos_vw_detallar_items_reporte_consumo" + Environment.NewLine;
                sSql += "where fecha_pedido between @fecha_desde" + Environment.NewLine;
                sSql += "and @fecha_hasta" + Environment.NewLine;

                if (Convert.ToInt32(cmbLocalidades.SelectedValue) != 0)
                {
                    a++;
                    sSql += "and id_localidad = @id_localidad" + Environment.NewLine;
                }

                if (Convert.ToInt32(cmbTipoComprobantes.SelectedValue) != 0)
                {
                    a++;
                    sSql += "and idtipocomprobante = @idtipocomprobante" + Environment.NewLine;
                }

                sSql += "order by numero_pedido";

                int b = 0;
                parametro = new SqlParameter[a];
                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_desde";
                parametro[b].SqlDbType = SqlDbType.DateTime;
                parametro[b].Value = Convert.ToDateTime(dtFechaDesde.Text);
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_hasta";
                parametro[b].SqlDbType = SqlDbType.DateTime;
                parametro[b].Value = Convert.ToDateTime(dtFechaHasta.Text);
                b++;

                if (Convert.ToInt32(cmbLocalidades.SelectedValue) != 0)
                {
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@id_localidad";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = Convert.ToInt32(cmbLocalidades.SelectedValue);
                    b++;
                }

                if (Convert.ToInt32(cmbTipoComprobantes.SelectedValue) != 0)
                {
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@idtipocomprobante";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = Convert.ToInt32(cmbTipoComprobantes.SelectedValue);
                }

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
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    return;
                }

                Decimal dbSumaSubtotal_A = 0;
                Decimal dbSumaDescuento_A = 0;
                Decimal dbSumaSubtotalNeto_A = 0;
                Decimal dbSumaIva_A = 0;
                Decimal dbSumaServicio_A = 0;
                Decimal dbSumaTotal_A = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {


                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["tipo_comprobante"].ToString(),
                                        dtConsulta.Rows[i]["numero_factura"].ToString(),
                                        Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                        dtConsulta.Rows[i]["cliente"].ToString(),
                                        dtConsulta.Rows[i]["codigo"].ToString(),
                                        dtConsulta.Rows[i]["nombre"].ToString(),                                        
                                        dtConsulta.Rows[i]["cantidad"].ToString(),
                                        dtConsulta.Rows[i]["precio_unitario"].ToString(),
                                        dtConsulta.Rows[i]["valor_dscto"].ToString(),
                                        dtConsulta.Rows[i]["subtotal_neto"].ToString(),
                                        dtConsulta.Rows[i]["valor_iva"].ToString(),
                                        dtConsulta.Rows[i]["valor_otro"].ToString(),
                                        dtConsulta.Rows[i]["valor"].ToString()
                                     );

                    dbSumaSubtotal_A += Convert.ToDecimal(dtConsulta.Rows[i]["subtotal"].ToString());
                    dbSumaDescuento_A += Convert.ToDecimal(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbSumaSubtotalNeto_A += Convert.ToDecimal(dtConsulta.Rows[i]["subtotal_neto"].ToString());
                    dbSumaIva_A += Convert.ToDecimal(dtConsulta.Rows[i]["valor_iva"].ToString());
                    dbSumaServicio_A += Convert.ToDecimal(dtConsulta.Rows[i]["valor_otro"].ToString());
                    dbSumaTotal_A += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtSubtotal.Text = dbSumaSubtotal_A.ToString("N2");
                txtDescuento.Text = dbSumaDescuento_A.ToString("N2");
                txtNeto.Text = dbSumaSubtotalNeto_A.ToString("N2");
                txtIva.Text = dbSumaIva_A.ToString("N2");
                txtServicio.Text = dbSumaServicio_A.ToString("N2");
                txtTotal.Text = dbSumaTotal_A.ToString("N2");

                dgvDatos.ClearSelection();
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

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            dgvDatos.Rows.Clear();

            fechaSistema();
            llenarComboLocalidades();
            llenarComboTipoComprobante();
            dtFechaDesde.Text = sFecha;
            dtFechaHasta.Text = sFecha;
            btnExcel.Visible = false;

            txtSubtotal.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtNeto.Text = "0.00";
            txtIva.Text = "0.00";
            txtServicio.Text = "0.00";
            txtTotal.Text = "0.00";
        }

        #endregion

        private void frmReporteVentasPorItems_Load(object sender, EventArgs e)
        {
            limpiar();

            if (iHabilitarEscape == 1)
                this.KeyDown -= new KeyEventHandler(frmReporteVentasPorItems_KeyDown);

            this.ActiveControl = cmbLocalidades;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

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

            llenarGrid();
        }

        private void frmReporteVentasPorItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
