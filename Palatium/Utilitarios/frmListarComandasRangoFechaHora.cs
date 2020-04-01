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

namespace Palatium.Utilitarios
{
    public partial class frmListarComandasRangoFechaHora : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;
        string sFecha;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmListarComandasRangoFechaHora()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where cg_localidad = " + Program.iCgLocalidadRecuperado;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;

                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
            fechaSistema();

            dtFechaDesde.Text = sFecha;
            dtFechaHasta.Text = sFecha;
            dtHoraDesde.Text = "00:00";
            dtHoraHasta.Text = "23:59";

            llenarComboLocalidades();

            dgvDatos.Rows.Clear();
            lblCantidad.Text = "0";
            txtTotal.Text = "0.00";
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_comandas_por_rango_fecha_hora" + Environment.NewLine;
                sSql += "where fecha_pedido between @fecha_inicio and @fecha_final" + Environment.NewLine;
                sSql += "and (convert(varchar(10), fecha_apertura_orden, 108) >= @hora_inicio" + Environment.NewLine;
                sSql += "and convert(varchar(10), fecha_apertura_orden, 108) <= @hora_final)" + Environment.NewLine;
                sSql += "and id_localidad = @id_localidad";

                SqlParameter[] Parametros = new SqlParameter[5];
                Parametros[0] = new SqlParameter();
                Parametros[0].ParameterName = "@fecha_inicio";
                Parametros[0].SqlDbType = SqlDbType.DateTime;
                Parametros[0].Value = Convert.ToDateTime(dtFechaDesde.Text);

                Parametros[1] = new SqlParameter();
                Parametros[1].ParameterName = "@fecha_final";
                Parametros[1].SqlDbType = SqlDbType.DateTime;
                Parametros[1].Value = Convert.ToDateTime(dtFechaHasta.Text);

                Parametros[2] = new SqlParameter();
                Parametros[2].ParameterName = "@hora_inicio";
                Parametros[2].SqlDbType = SqlDbType.VarChar;
                Parametros[2].Value = dtHoraDesde.Text + ":00";

                Parametros[3] = new SqlParameter();
                Parametros[3].ParameterName = "@hora_final";
                Parametros[3].SqlDbType = SqlDbType.VarChar;
                Parametros[3].Value = dtHoraHasta.Text + ":59";

                Parametros[4] = new SqlParameter();
                Parametros[4].ParameterName = "@id_localidad";
                Parametros[4].SqlDbType = SqlDbType.Int;
                Parametros[4].Value = Convert.ToInt32(cmbLocalidades.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, Parametros);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                Decimal dbSuma_R = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                        dtConsulta.Rows[i]["id_pedido"].ToString(),
                                        dtConsulta.Rows[i]["numero_pedido"].ToString(),
                                        dtConsulta.Rows[i]["tipo_comanda"].ToString(),
                                        dtConsulta.Rows[i]["cliente"].ToString(),
                                        Convert.ToDateTime(dtConsulta.Rows[i]["fecha_apertura_orden"].ToString()).ToString("dd-MM-yyyy HH:mm:ss"),
                                        dtConsulta.Rows[i]["valor"].ToString(),
                                        dtConsulta.Rows[i]["estado_orden"].ToString()
                                     );

                    dbSuma_R += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtTotal.Text = dbSuma_R.ToString("N2");
                lblCantidad.Text = dtConsulta.Rows.Count.ToString();

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
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

        #endregion

        private void frmListarComandasRangoFechaHora_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaDesde.Text) > Convert.ToDateTime(dtFechaHasta.Text))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha final no debe ser superior a la fecha inicial.";
                ok.ShowDialog();
                dtFechaHasta.Text = sFecha;
                return;
            }

            if (Convert.ToDateTime(dtHoraDesde.Text) > Convert.ToDateTime(dtHoraHasta.Text))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La hora final no debe ser superior a la fecha inicial.";
                ok.ShowDialog();
                dtHoraHasta.Text = "23:59";
                return;
            }

            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmListarComandasRangoFechaHora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dgvDatos.Columns[e.ColumnIndex].Name == "visualizar")
            {
                if (e.RowIndex == -1)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                int iIdPedido_P = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_pedido"].Value);
                string sEstado_P = dgvDatos.Rows[e.RowIndex].Cells["estado_orden"].Value.ToString().Trim();

                //Areas.frmVistaPreviaComanda ver = new Areas.frmVistaPreviaComanda(iIdPedido_P.ToString(), "");
                //ver.ShowDialog();

                Pedidos.frmVerPrecuentaTextBox precuenta = new Pedidos.frmVerPrecuentaTextBox(iIdPedido_P.ToString(), 0, sEstado_P);
                precuenta.ShowDialog(); 
            }

            this.Cursor = Cursors.Default;
            dgvDatos.ClearSelection();
        }        
    }
}
