using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Utilitarios
{
    public partial class frmDetallePorOrigenSinFactura : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseExcel exportarExcel;

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        DataGridView dgvExportar;

        string sSql;
        string sFiltro;
        string sTexto;
        string sFechaDesde;
        string sFechaHasta;
        string sNombreEmpleado;
        string sNombreArea;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdPersona;
        int iIdPersonaAyuda;
        int iBandera;

        DateTime dFechaInicio;
        DateTime dFechaFinal;

        Decimal dbTotal;

        public frmDetallePorOrigenSinFactura()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR DATOS DE LOS ORIGENES DE ORDEN
        private void llenarComboAreas()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_area_consumo_empleados, descripcion" + Environment.NewLine;
                sSql += "from pos_area_consumo_empleados" + Environment.NewLine;
                //sSql += "where codigo in ('06', '14')" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbArea.DisplayMember = "descripcion";
                    cmbArea.ValueMember = "id_pos_area_consumo_empleados";
                    cmbArea.DataSource = dtConsulta;
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

        //CARGAR DATOS DE LOS EMPELADOS
        private void llenarComboEmpleados()
        {
            try
            {
                sSql = "";
                sSql += "SELECT CE.id_persona, ltrim(TP.apellidos + ' ' + isnull(TP.nombres, '')) empleado" + Environment.NewLine;
                sSql += "from pos_empleado CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and CE.estado = 'A'" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "where CE.id_pos_area_consumo_empleados = " + cmbArea.SelectedValue + Environment.NewLine;
                sSql += "order by TP.apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    DataRow row = dtConsulta.NewRow();
                    row["id_persona"] = "0";
                    row["empleado"] = "Todos...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);

                    cmbListadoEmpleados.DisplayMember = "empleado";
                    cmbListadoEmpleados.ValueMember = "id_persona";
                    cmbListadoEmpleados.DataSource = dtConsulta;
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

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvDatos.Rows.Clear();

                if (Convert.ToInt32(cmbListadoEmpleados.SelectedValue) == 0)
                {
                    int iRespuesta = consultarPersona();

                    if (iRespuesta == -1)
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }

                    if (iRespuesta == 0)
                    {
                        this.Cursor = Cursors.Default;
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "No existen regstros en el sistema.";
                        ok.ShowDialog();
                        return;
                    }
                }

                dbTotal = 0;
                txtTotal.Text = "0.00";

                sSql = "";
                sSql += "select id_persona, id_pedido, persona, fecha_pedido, tipo_comanda, numero_pedido," + Environment.NewLine;
                sSql += "isnull(sum(valor), 0) valor" + Environment.NewLine;
                sSql += "from pos_vw_detalle_pedidos_origen_orden" + Environment.NewLine;
                sSql += "where fecha_pedido between '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and codigo = '14'" + Environment.NewLine;
                sSql += "and estado_orden in ('Cerrada', 'Pagada')" + Environment.NewLine;

                if (Convert.ToInt32(cmbListadoEmpleados.SelectedValue) == 0)
                {
                    sSql += "and id_persona in (" + sFiltro + ")" + Environment.NewLine;
                }

                else
                {
                    sSql += "and id_persona = " + cmbListadoEmpleados.SelectedValue + Environment.NewLine;
                }

                sSql += "group by id_persona, id_pedido, persona, fecha_pedido, tipo_comanda, numero_pedido" + Environment.NewLine;
                sSql += "order by persona, fecha_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    this.Cursor = Cursors.Default;
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros seleccionados";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_persona"].ToString().Trim(),
                                      dtConsulta.Rows[i]["id_pedido"].ToString().Trim(),
                                      dtConsulta.Rows[i]["numero_pedido"].ToString().Trim(),
                                      dtConsulta.Rows[i]["persona"].ToString().Trim().ToUpper(),
                                      Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["tipo_comanda"].ToString().Trim().ToUpper(),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString()).ToString("N2"));
                }

                for (int j = 0; j < dgvDatos.Rows.Count; j++)
                {
                    dbTotal += Convert.ToDecimal(dgvDatos.Rows[j].Cells["total"].Value);
                }

                txtTotal.Text = dbTotal.ToString("N2");
                dgvDatos.ClearSelection();
                this.Cursor = Cursors.Default;
            }

            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL PERSONAL DE CADA AREA
        private int consultarPersona()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona from pos_empleado" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_area_consumo_empleados = " + cmbArea.SelectedValue + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count == 0)
                    return 0;

                sFiltro = "";

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sFiltro += dtConsulta.Rows[i]["id_persona"].ToString().Trim();

                    if (i + 1 < dtConsulta.Rows.Count)
                    {
                        sFiltro += ", ";
                    }
                }

                return 1;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboAreas();
            llenarComboEmpleados();
            sFechaHasta = "";
            sFechaHasta = "";
            txtTotal.Text = "0.00";
            dgvDatos.Rows.Clear();
        }

        //FUNCION PARA CREAR EL REPORTE
        private void crearReporteDetalle()
        {
            try
            {
                sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "REPORTE DE CONSUMO INTERNO".PadLeft(33, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "FECHA DESDE: " + sFechaDesde + Environment.NewLine;
                sTexto += "FECHA HASTA: " + sFechaHasta + Environment.NewLine;
                sTexto += "NOMBRE AREA: " + sNombreArea + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                iIdPersona = Convert.ToInt32(dgvDatos.Rows[0].Cells["id_persona"].Value);
                sNombreEmpleado = dgvDatos.Rows[0].Cells["empleado"].Value.ToString();

                iBandera = 1;
                dbTotal = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (iBandera == 1)
                    {
                        if (sNombreEmpleado.Trim().Length > 30)
                            sTexto += "EMPLEADO: " + sNombreEmpleado.Substring(0, 30) + Environment.NewLine;
                        else
                            sTexto += "EMPLEADO: " + sNombreEmpleado + Environment.NewLine;

                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        iBandera = 0;
                    }

                    dbTotal += Convert.ToDecimal(dgvDatos.Rows[i].Cells["total"].Value);
                    sTexto += ("PEDIDO: " + dgvDatos.Rows[i].Cells["numero_pedido"].Value.ToString().Trim()).PadRight(20, ' ') + dgvDatos.Rows[i].Cells["fecha_pedido"].Value.ToString().Trim().PadRight(14, ' ') + dgvDatos.Rows[i].Cells["total"].Value.ToString().Trim().PadLeft(6, ' ') + Environment.NewLine;

                    if (i + 1 == dgvDatos.Rows.Count)
                    {
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto += "TOTAL REPORTADO: ".PadRight(30, ' ') + dbTotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine;
                    }

                    if (i + 1 < dgvDatos.Rows.Count)
                    {
                        iIdPersonaAyuda = Convert.ToInt32(dgvDatos.Rows[i + 1].Cells["id_persona"].Value);

                        if (iIdPersona != iIdPersonaAyuda)
                        {
                            sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                            sTexto += "TOTAL REPORTADO: ".PadRight(30, ' ') + dbTotal.ToString("N2").PadLeft(10, ' ') + Environment.NewLine;
                            sTexto += "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine;
                            dbTotal = 0;
                            sTexto += Environment.NewLine;
                            sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                            iIdPersona = iIdPersonaAyuda;
                            sNombreEmpleado = dgvDatos.Rows[i + 1].Cells["empleado"].Value.ToString();
                            iBandera = 1;
                        }
                    }
                }

                Utilitarios.frmReporteGenerico reporte = new Utilitarios.frmReporteGenerico(sTexto, 0, 0, 0, 0);
                reporte.ShowDialog();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR EL REPORTE RESUMEN
        private void crearReporteResumen()
        {
            try
            {
                sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "REPORTE DE CONSUMO INTERNO".PadLeft(33, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "FECHA DESDE: " + sFechaDesde + Environment.NewLine;
                sTexto += "FECHA HASTA: " + sFechaHasta + Environment.NewLine;
                sTexto += "NOMBRE AREA: " + sNombreArea + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                iIdPersona = Convert.ToInt32(dgvDatos.Rows[0].Cells["id_persona"].Value);
                sNombreEmpleado = dgvDatos.Rows[0].Cells["empleado"].Value.ToString();

                iBandera = 1;
                dbTotal = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (iBandera == 1)
                    {
                        sTexto += "EMPLEADO: " + sNombreEmpleado + Environment.NewLine;
                        iBandera = 0;
                    }

                    dbTotal += Convert.ToDecimal(dgvDatos.Rows[i].Cells["total"].Value);

                    if (i + 1 == dgvDatos.Rows.Count)
                    {
                        sTexto += "TOTAL REPORTADO: " + dbTotal.ToString("N2") + Environment.NewLine;
                        sTexto += "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine;
                    }

                    if (i + 1 < dgvDatos.Rows.Count)
                    {
                        iIdPersonaAyuda = Convert.ToInt32(dgvDatos.Rows[i + 1].Cells["id_persona"].Value);

                        if (iIdPersona != iIdPersonaAyuda)
                        {
                            sTexto += "TOTAL REPORTADO: " + dbTotal.ToString("N2") + Environment.NewLine;
                            sTexto += "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine;
                            dbTotal = 0;
                            iIdPersona = iIdPersonaAyuda;
                            sNombreEmpleado = dgvDatos.Rows[i + 1].Cells["empleado"].Value.ToString();
                            iBandera = 1;
                        }
                    }
                }

                Utilitarios.frmReporteGenerico reporte = new Utilitarios.frmReporteGenerico(sTexto, 0, 0, 0, 0);
                reporte.ShowDialog();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CREAR EL GRID PARA EXPORTAR
        private bool crearGrid()
        {
            try
            {
                dgvExportar = new DataGridView();
                dgvExportar.AllowUserToAddRows = false;
                dgvExportar.AllowUserToDeleteRows = false;
                dgvExportar.AllowUserToResizeColumns = false;
                dgvExportar.AllowUserToResizeRows = false;
                dgvExportar.MultiSelect = false;

                DataGridViewTextBoxColumn empleado = new DataGridViewTextBoxColumn();
                empleado.HeaderText = "EMPLEADO";
                empleado.Name = "empleado";

                DataGridViewTextBoxColumn fecha_pedido = new DataGridViewTextBoxColumn();
                fecha_pedido.HeaderText = "FECHA PEDIDO";
                fecha_pedido.Name = "fecha_pedido";

                DataGridViewTextBoxColumn tipo_comanda = new DataGridViewTextBoxColumn();
                tipo_comanda.HeaderText = "TIPO COMANDA";
                tipo_comanda.Name = "tipo_comanda";

                DataGridViewTextBoxColumn total = new DataGridViewTextBoxColumn();
                total.HeaderText = "TOTAL";
                total.Name = "total";

                dgvExportar.Columns.Add(empleado);
                dgvExportar.Columns.Add(fecha_pedido);
                dgvExportar.Columns.Add(tipo_comanda);
                dgvExportar.Columns.Add(total);

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    dgvExportar.Rows.Add(
                                            dgvDatos.Rows[i].Cells["empleado"].Value.ToString(),
                                            dgvDatos.Rows[i].Cells["fecha_pedido"].Value.ToString(),
                                            dgvDatos.Rows[i].Cells["tipo_comanda"].Value.ToString(),
                                            dgvDatos.Rows[i].Cells["total"].Value.ToString()
                                        );
                }

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

        private void frmDetalleConsumoEmpleados_Load(object sender, EventArgs e)
        {
            cmbArea.SelectedIndexChanged -= new EventHandler(cmbArea_SelectedIndexChanged);
            llenarComboAreas();
            cmbArea.SelectedIndexChanged += new EventHandler(cmbArea_SelectedIndexChanged);
            llenarComboEmpleados();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //if (Convert.ToInt32(cmbListadoEmpleados.SelectedValue) == 0)
            //{
            //    ok = new VentanasMensajes.frmMensajeNuevoOk();
            //    ok.lblMensaje.Text = "No ha seleccionado un empleado.";
            //    ok.ShowDialog();
            //    return;
            //}

            dFechaInicio = Convert.ToDateTime(txtDesde.Text.Trim());
            dFechaFinal = Convert.ToDateTime(txtHasta.Text.Trim());

            if (dFechaInicio > dFechaFinal)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El rango de fecha no se encuentra definido correctamente.";
                ok.ShowDialog();
                txtDesde.Focus();
                return;
            }

            sFechaDesde = dFechaInicio.ToString("dd-MM-yyyy");
            sFechaHasta = dFechaFinal.ToString("dd-MM-yyyy");
            sNombreArea = cmbArea.Text.Trim().ToUpper();
            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmDetalleConsumoEmpleados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dgvDatos.Columns[e.ColumnIndex].Name == "vista_previa")
            {
                if (e.RowIndex == -1)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }

                int iIdPedido_P = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_pedido"].Value);

                //VALOR 2 - PARA ETIQUETA REPORTE "CONSUMO INTERNO"
                Utilitarios.frmDetalleComandas vista = new Utilitarios.frmDetalleComandas(iIdPedido_P, 1);
                vista.ShowDialog();
            }

            this.Cursor = Cursors.Default;
            dgvDatos.ClearSelection();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No existen registros para imprimir.";
                ok.ShowDialog();
                return;
            }

            crearReporteDetalle();
        }

        private void btnImprimirConsolidad_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No existen registros para imprimir.";
                ok.ShowDialog();
                return;
            }

            crearReporteResumen();
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarComboEmpleados();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No existen registros para realizar la exportación de datos.";
                ok.ShowDialog();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (crearGrid() == false)
            {
                this.Cursor = Cursors.Default;
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un problema al exportar la información. Comuníquese con el administrador del sistema.";
                ok.ShowDialog();
                return;
            }

            exportarExcel = new Clases.ClaseExcel();

            if (exportarExcel.exportarExcel(dgvExportar) == false)
            {
                this.Cursor = Cursors.Default;
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un problema al exportar la información. Comuníquese con el administrador del sistema.";
                ok.ShowDialog();
                return;
            }

            this.Cursor = Cursors.Default;
        }
    }
}
