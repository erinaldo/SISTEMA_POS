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
    public partial class frmResumenGanancias : Form
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
        int iCantidad;

        DataTable dtConsulta;

        SqlParameter[] parametro;

        public frmResumenGanancias(int iHabilitarEscape_P)
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

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();
                txtTotalValor.Text = "0.00";
                txtCantidadTotal.Text = "0";

                int a = 2;
                int iBanderaLocalidades = 0;
                int iBanderaCobradas = 0;

                sSql = "";
                sSql += "select fecha_pedido, count(*) cuenta," + Environment.NewLine;
                sSql += "ltrim(str(sum(convert(float, valor)), 10, 2)) valor" + Environment.NewLine;
                sSql += "from pos_vw_detallar_items_reporte_consumo" + Environment.NewLine;
                sSql += "where fecha_pedido between @fecha_desde" + Environment.NewLine;
                sSql += "and @fecha_hasta" + Environment.NewLine;

                if (Convert.ToInt32(cmbLocalidades.SelectedValue) != 0)
                {
                    a++;
                    iBanderaLocalidades = 1;
                    sSql += "and id_localidad = @id_localidad" + Environment.NewLine;
                }

                if (rdbCobradas.Checked == true)
                {
                    a+= 2;
                    iBanderaCobradas = 1;
                    sSql += "and idtipocomprobante in (@comprobante_1, @comprobante_2)" + Environment.NewLine;
                }

                sSql += "group by fecha_pedido" + Environment.NewLine;
                sSql += "order by fecha_pedido";

                #region PARAMETROS

                int b = 0;
                parametro = new SqlParameter[a];
                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_desde";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = sFechaDesde;
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_hasta";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = sFechaHasta;

                if (iBanderaLocalidades == 1)
                {
                    b++;
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@id_localidad";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = iIdLocalidad;
                }

                if (iBanderaCobradas == 1)
                {
                    b++;
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@comprobante_1";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = 1;
                    b++;

                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@comprobante_2";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = Program.iComprobanteNotaEntrega;
                }

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
                    dgvDatos.Rows.Add(Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["cuenta"].ToString(),
                                      dtConsulta.Rows[i]["valor"].ToString());

                    dbCantidad_A += Convert.ToDecimal(dtConsulta.Rows[i]["cuenta"].ToString());
                    dbTotal_A += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtCantidadTotal.Text = dbCantidad_A.ToString();
                txtTotalValor.Text = dbTotal_A.ToString();
                llenarGridDetalle();
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
        private void llenarGridDetalle()
        {
            try
            {
                dgvDetalle.Rows.Clear();

                int a = 2;
                int iBanderaLocalidades = 0;
                int iBanderaCobradas = 0;

                sSql = "";
                sSql += "select id_pedido, fecha_pedido, cliente, " + Environment.NewLine;
                sSql += "ltrim(str(sum(cantidad * precio_unitario), 10, 2)) subtotal," + Environment.NewLine;
                sSql += "ltrim(str(sum(cantidad * valor_dscto), 10, 2)) descuento," + Environment.NewLine;
                sSql += "ltrim(str(sum(cantidad * (precio_unitario - valor_dscto)), 10, 2)) subtotal_neto," + Environment.NewLine;
                sSql += "ltrim(str(sum(cantidad * valor_iva), 10, 2)) valor_iva," + Environment.NewLine;
                sSql += "ltrim(str(sum(cantidad * valor_otro), 10, 2)) valor_otro," + Environment.NewLine;
                sSql += "ltrim(str(sum(convert(float, valor)), 10, 2)) valor" + Environment.NewLine;
                sSql += "from pos_vw_reporte_ganancias" + Environment.NewLine;
                sSql += "where fecha_pedido between @fecha_desde" + Environment.NewLine;
                sSql += "and @fecha_hasta" + Environment.NewLine;

                if (Convert.ToInt32(cmbLocalidades.SelectedValue) != 0)
                {
                    a++;
                    iBanderaLocalidades = 1;
                    sSql += "and id_localidad = @id_localidad" + Environment.NewLine;
                }

                if (rdbCobradas.Checked == true)
                {
                    a += 2;
                    iBanderaCobradas = 1;
                    sSql += "and idtipocomprobante in (@comprobante_1, @comprobante_2)" + Environment.NewLine;
                }

                sSql += "group by id_pedido, fecha_pedido, cliente" + Environment.NewLine;
                sSql += "order by fecha_pedido, cliente";

                #region PARAMETROS

                int b = 0;
                parametro = new SqlParameter[a];
                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_desde";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = sFechaDesde;
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@fecha_hasta";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = sFechaHasta;

                if (iBanderaLocalidades == 1)
                {
                    b++;
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@id_localidad";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = iIdLocalidad;
                }

                if (iBanderaCobradas == 1)
                {
                    b++;
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@comprobante_1";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = 1;
                    b++;

                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@comprobante_2";
                    parametro[b].SqlDbType = SqlDbType.Int;
                    parametro[b].Value = Program.iComprobanteNotaEntrega;
                }

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
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDetalle.Rows.Add(dtConsulta.Rows[i]["id_pedido"].ToString(),
                                        Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                        dtConsulta.Rows[i]["cliente"].ToString(),
                                        dtConsulta.Rows[i]["subtotal"].ToString(),
                                        dtConsulta.Rows[i]["descuento"].ToString(),
                                        dtConsulta.Rows[i]["subtotal_neto"].ToString(),
                                        dtConsulta.Rows[i]["valor_iva"].ToString(),
                                        dtConsulta.Rows[i]["valor_otro"].ToString(),
                                        dtConsulta.Rows[i]["valor"].ToString());
                }

                dgvDetalle.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION LIMPIAR
        private void limpiar()
        {
            fechaSistema();
            llenarComboLocalidades();
            dgvDatos.Rows.Clear();
            dgvDetalle.Rows.Clear();
            dtFechaDesde.Text = sFecha;
            dtFechaHasta.Text = sFecha;
            rdbTodos.Checked = true;
            txtCantidadTotal.Text = "0";
            txtTotalValor.Text = "0.00";
            tabControlGanancias.SelectedTab = tabControlGanancias.TabPages["tabResumido"];
            iIdLocalidad = 0;
            sFechaDesde = "";
            sFechaHasta = "";
        }

        #endregion

        private void frmResumenGanancias_Load(object sender, EventArgs e)
        {
            limpiar();
            if (iHabilitarEscape == 1)
                this.KeyDown -= new KeyEventHandler(frmResumenGanancias_KeyDown);

            this.ActiveControl = cmbLocalidades;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmResumenGanancias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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

            iIdLocalidad = Convert.ToInt32(cmbLocalidades.SelectedValue);
            sFechaDesde = Convert.ToDateTime(dtFechaDesde.Text).ToString("yyyy-MM-dd");
            sFechaHasta = Convert.ToDateTime(dtFechaHasta.Text).ToString("yyyy-MM-dd");

            llenarGrid();
        }
    }
}
