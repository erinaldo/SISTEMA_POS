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
    public partial class frmHistorialClientePorOrigen : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sFecha;

        bool bRespuesta;

        DataTable dtConsulta;

        SqlParameter[] parametro;

        int iIdPersona;
        int iHabilitarEscape;

        public frmHistorialClientePorOrigen(int iHabilitarEscape_P)
        {
            this.iHabilitarEscape = iHabilitarEscape_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR DATOS DEL CLIENTE
        private void consultarRegistro(string sIdentificacion_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, identificacion, ltrim(isnull(nombres, '') + ' ' + apellidos) cliente" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where identificacion = @identificacion" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@identificacion";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = sIdentificacion_P;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

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
                    ok.lblMensaje.Text = "Registro no encontrado.";
                    ok.ShowDialog();
                    btnFiltrar.Enabled = false;
                    txtIdentificacion.Focus();
                    return;
                }

                iIdPersona = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                txtApellidos.Text = dtConsulta.Rows[0]["cliente"].ToString().Trim().ToUpper();
                btnFiltrar.Enabled = true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {

                int a = 3;

                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select id_pedido, numero_pedido, fecha_pedido," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_persona = @id_persona" + Environment.NewLine;
                sSql += "and fecha_pedido between @fecha_desde" + Environment.NewLine;
                sSql += "and @fecha_hasta" + Environment.NewLine;

                if (Convert.ToInt32(cmbTipoComanda.SelectedValue) != 0)
                {
                    a++;
                    sSql += "and id_pos_origen_orden = @id_pos_origen_orden" + Environment.NewLine;
                }
                sSql += "group by id_pedido, numero_pedido, fecha_pedido" + Environment.NewLine;
                sSql += "order by numero_pedido";

                parametro = new SqlParameter[a];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_persona";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPersona;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@fecha_desde";
                parametro[1].SqlDbType = SqlDbType.DateTime;
                parametro[1].Value = Convert.ToDateTime(dtFechaDesde.Text);

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@fecha_hasta";
                parametro[2].SqlDbType = SqlDbType.DateTime;
                parametro[2].Value = Convert.ToDateTime(dtFechaHasta.Text);

                if (Convert.ToInt32(cmbTipoComanda.SelectedValue) != 0)
                {
                    parametro[3] = new SqlParameter();
                    parametro[3].ParameterName = "@id_pos_origen_orden";
                    parametro[3].SqlDbType = SqlDbType.Int;
                    parametro[3].Value = Convert.ToInt32(cmbTipoComanda.SelectedValue);
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

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pedido"].ToString(),
                                      dtConsulta.Rows[i]["numero_pedido"].ToString(),
                                      Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["valor"].ToString());
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return;
            }
        }

        //FUNCION PARA OBTENER DATOS DE LA BASE
        private bool consultarPedido(int iIdPedido_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_det_pedido" + Environment.NewLine;
                sSql += "where id_pedido = @id_pedido";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_pedido";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdPedido_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    txtReporte.Clear();
                    lblImpuestos.Text = "0.00";
                    lblSubtotal.Text = "0.00";
                    lblTotal.Text = "0.00";
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra el registro de la comanda seleccionada. Comuníquese con el administrador del sistema.";
                    ok.ShowDialog();
                    return false;
                }

                if (crearReporte() == false)
                    return false;

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

        //FUNCION PARA CREAR EL REPORTE
        private bool crearReporte()
        {
            try
            {
                Decimal dbCantidad;
                Decimal dbPrecioUnitario;
                Decimal dbValorDescuento;
                Decimal dbValorIva;
                Decimal dbValorServicio;
                Decimal dbSumaSubtotal = 0;
                Decimal dbSumaIva = 0;
                Decimal dbSumaServicio = 0;
                Decimal dbSumaDescuento = 0;
                Decimal dbTotalPorLinea;

                string sNombreProducto_P;
                string sEstadoOrden_P = dtConsulta.Rows[0]["estado_orden"].ToString().Trim().ToUpper();

                string sTexto = "";
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "           COMANDA REGISTRADA" + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "COMANDA:      : " + dtConsulta.Rows[0]["descripcion"].ToString().Trim().ToUpper() + Environment.NewLine;
                sTexto += "No. PEDIDO    : " + dtConsulta.Rows[0]["numero_pedido"].ToString().Trim() + Environment.NewLine;
                sTexto += "MESERO        : " + dtConsulta.Rows[0]["nombre_mesero"].ToString().Trim().ToUpper() + Environment.NewLine;
                sTexto += "IDENTIFICACIÓN: " + dtConsulta.Rows[0]["identificacion"].ToString().Trim() + Environment.NewLine;

                string sNombreCliente_P = dtConsulta.Rows[0]["cliente"].ToString().Trim().ToUpper();

                if (sNombreCliente_P.Length > 40)
                    sTexto += sNombreCliente_P.Substring(0, 40) + Environment.NewLine;
                else
                    sTexto += sNombreCliente_P + Environment.NewLine;

                sTexto += "FECHA PEDIDO  : " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy") + Environment.NewLine;
                sTexto += "ESTADO ORDEN  : " + sEstadoOrden_P + Environment.NewLine;
                sTexto += "".PadLeft(40, '=') + Environment.NewLine;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dbCantidad = Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString());
                    dbPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[i]["precio_unitario"].ToString());
                    dbValorDescuento = Convert.ToDecimal(dtConsulta.Rows[i]["valor_dscto"].ToString());
                    dbValorIva = Convert.ToDecimal(dtConsulta.Rows[i]["valor_iva"].ToString());
                    dbValorServicio = Convert.ToDecimal(dtConsulta.Rows[i]["valor_otro"].ToString());

                    dbSumaDescuento += dbCantidad * dbValorDescuento;
                    dbSumaIva += dbCantidad * dbValorIva;
                    dbSumaServicio += dbCantidad * dbValorServicio;
                    dbSumaSubtotal += dbCantidad * (dbPrecioUnitario - dbValorDescuento);
                    dbTotalPorLinea = dbCantidad * (dbPrecioUnitario - dbValorDescuento + dbValorIva + dbValorServicio);
                    sNombreProducto_P = dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper();

                    if (sNombreProducto_P.Length > 26)
                    {
                        sTexto += dbCantidad.ToString().PadLeft(4, ' ') + " " + sNombreProducto_P.Substring(0, 26) + " " + dbTotalPorLinea.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                        sTexto += "".PadLeft(5, ' ') + sNombreProducto_P.Substring(26) + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += dbCantidad.ToString().PadLeft(4, ' ') + " " + sNombreProducto_P.PadRight(26, ' ') + " " + dbTotalPorLinea.ToString("N2").PadLeft(8, ' ') + Environment.NewLine;
                    }
                }

                sTexto += "".PadLeft(40, '=') + Environment.NewLine;
                sTexto += "      CREADO: " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura_orden"].ToString()).ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine;

                if (sEstadoOrden_P == "PAGADA")
                    sTexto += "      PAGADO: " + Convert.ToDateTime(dtConsulta.Rows[0]["fecha_cierre_orden"].ToString()).ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine;
                sTexto += "".PadLeft(40, '=');

                lblSubtotal.Text = dbSumaSubtotal.ToString("N2");
                lblImpuestos.Text = (dbSumaIva + dbSumaServicio).ToString("N2");
                lblTotal.Text = (dbSumaSubtotal + dbSumaIva + dbSumaServicio).ToString("N2");

                txtReporte.Text = sTexto;

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

        //FUNCION PARA LLENAR EL COMBOBOX DE ORIGENES
        private void llenarComboOrigenOrden()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "order by descripcion";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

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
                row["descripcion"] = "Todos...!!!";
                dtConsulta.Rows.InsertAt(row, 0);

                cmbTipoComanda.DisplayMember = "descripcion";
                cmbTipoComanda.ValueMember = "id_pos_origen_orden";
                cmbTipoComanda.DataSource = dtConsulta;
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
            fechaSistema();
            llenarComboOrigenOrden();

            dgvDatos.Rows.Clear();

            lblSubtotal.Text = "0.00";
            lblImpuestos.Text = "0.00";
            lblTotal.Text = "0.00";

            txtReporte.Clear();

            dtFechaDesde.Text = sFecha;
            dtFechaHasta.Text = sFecha;
            txtIdentificacion.Clear();
            txtApellidos.Clear();
            chkPasaporte.Checked = false;
            btnFiltrar.Enabled = false;
            txtIdentificacion.Focus();
        }

        #endregion

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Text = "9999999999999";
            consultarRegistro(txtIdentificacion.Text.Trim());
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "favor ingrese el número de identificación.";
                    ok.ShowDialog();
                    txtIdentificacion.Focus();
                    return;
                }

                //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                //if ((esNumero(txtIdentificacion.Text.Trim()) == true) && (chkPasaporte.Checked == false))
                //{
                //    //INSTRUCCIONES PARA VALIDAR
                //    validarIdentificacion();
                //}
                //else
                //{
                //CONSULTAR EN LA BASE DE DATOS
                consultarRegistro(txtIdentificacion.Text.Trim());
                //}
            }
        }

        private void frmHistorialClientePorOrigen_Load(object sender, EventArgs e)
        {
            limpiar();

            if (iHabilitarEscape == 1)
                this.KeyDown -= new KeyEventHandler(frmHistorialClientePorOrigen_KeyDown);

            this.ActiveControl = txtIdentificacion;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (iIdPersona == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el cliente.";
                ok.ShowDialog();
                txtIdentificacion.Focus();
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

            llenarGrid();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                consultarPedido(Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pedido"].Value));
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Facturador.frmControlDatosCliente controlDatosCliente = new Facturador.frmControlDatosCliente();
            controlDatosCliente.ShowDialog();

            if (controlDatosCliente.DialogResult == DialogResult.OK)
            {
                //iIdPersona = controlDatosCliente.iCodigo;
                txtIdentificacion.Text = controlDatosCliente.sIdentificacion;
                controlDatosCliente.Close();
                consultarRegistro(txtIdentificacion.Text);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHistorialClientePorOrigen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
