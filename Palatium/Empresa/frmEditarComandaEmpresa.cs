using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Empresa
{
    public partial class frmEditarComandaEmpresa : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sNombreEmpresa;
        string sNombreEmpleado;
        string sEstadoOrden;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdClienteEmpresarial;
        int iIdPersonaCE;
        int iIdPedido;

        int iIdEmpleado_Editar;
        int iIdPersona_Editar;

        DateTime dFechaInicio;

        public frmEditarComandaEmpresa()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR DATOS DE LOS CLIENTES EMPRESARIALES
        private void llenarComboEmpresas()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial, ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) cliente, CE.id_persona" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and CE.estado = 'A'" + Environment.NewLine;
                sSql += "order by TP.apellidos";

                cmbClienteEmpresarial.llenar(dtConsulta, sSql);
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
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select E.id_persona, ltrim(TP.apellidos + ' ' + isnull(TP.nombres, '')) empleado" + Environment.NewLine;
                sSql += "from pos_empleado_cliente E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and E.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "where E.id_pos_cliente_empresarial = " + cmbClienteEmpresarial.SelectedValue + Environment.NewLine;
                sSql += "order by TP.apellidos";

                cmbEmpleados.llenar(dtConsulta, sSql);
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
                sSql += "select id_pedido, empleado, fecha_pedido," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(valor), 0), 10, 2)) valor," + Environment.NewLine;
                sSql += "id_persona, id_empleado_cliente_empresarial, estado_orden" + Environment.NewLine;
                sSql += "from pos_vw_comandas_cliente_empresarial" + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersonaCE + Environment.NewLine;
                sSql += "and fecha_pedido = '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;

                if (Convert.ToInt32(cmbEmpleados.SelectedValue) != 0)
                {
                    sSql += "and id_empleado_cliente_empresarial = " + cmbEmpleados.SelectedValue + Environment.NewLine;
                }

                if (chkIncluyePagadas.Checked == false)
                {
                    sSql += "and estado_orden = 'Cerrada'" + Environment.NewLine;
                }

                sSql += "group by id_pedido, empleado, fecha_pedido," + Environment.NewLine;
                sSql += "id_persona, id_empleado_cliente_empresarial, estado_orden" + Environment.NewLine;
                sSql += "order by fecha_pedido, empleado";

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros seleccionados";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sEstadoOrden = dtConsulta.Rows[i]["estado_orden"].ToString().Trim().ToUpper();

                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pedido"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["empleado"].ToString().Trim().ToUpper(),
                                      Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["valor"].ToString().Trim(),
                                      dtConsulta.Rows[i]["id_persona"].ToString().Trim(),
                                      dtConsulta.Rows[i]["id_empleado_cliente_empresarial"].ToString().Trim(),
                                      sEstadoOrden
                                      );

                    if (sEstadoOrden == "PAGADA")
                    {
                        dgvDatos.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                    }

                    else
                    {
                        dgvDatos.Rows[i].DefaultCellStyle.ForeColor = Color.Purple;
                    }
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

        //FUNCION PARA CONSULTAR EL ID PERSONA DEL CLIENTE EMPRESARIAL
        private int buscarIdPersona()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona from pos_cliente_empresarial" + Environment.NewLine;
                sSql += "where id_pos_cliente_empresarial = " + cmbClienteEmpresarial.SelectedValue + Environment.NewLine;
                sSql += "and estado = 'A'";

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
                {
                    return 0;
                }

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
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
            cmbClienteEmpresarial.SelectedIndexChanged -= new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbClienteEmpresarial.SelectedIndexChanged += new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);

            llenarComboEmpleados();

            cmbEmpleados.Enabled = false;
            chkIncluyePagadas.Checked = false;

            dgvDatos.Rows.Clear();
        }

        #endregion

        private void frmEditarComandaEmpresa_Load(object sender, EventArgs e)
        {
            cmbClienteEmpresarial.SelectedIndexChanged -= new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbClienteEmpresarial.SelectedIndexChanged += new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);

            llenarComboEmpleados();
        }

        private void cmbClienteEmpresarial_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarComboEmpleados();

            if (Convert.ToInt32(cmbClienteEmpresarial.SelectedValue) == 0)
            {
                cmbEmpleados.Enabled = false;
            }

            else
            {
                cmbEmpleados.Enabled = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbClienteEmpresarial.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la empresa.";
                ok.ShowDialog();
                return;
            }

            iIdPersonaCE = buscarIdPersona();

            if (iIdPersonaCE == -1)
            {
                return;
            }

            if (iIdPersonaCE == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No se encuentra parametrizado el identificador del cliente empresarial";
                ok.ShowDialog();
                return;
            }

            sNombreEmpresa = cmbClienteEmpresarial.Text.Trim().ToUpper();
            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvDatos.Columns[e.ColumnIndex].Name == "btnVer")
                {
                    sNombreEmpleado = dgvDatos.Rows[e.RowIndex].Cells["empleado"].Value.ToString().Trim().ToUpper();
                    iIdPedido = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_pedido"].Value);
                    Empresa.frmVerPedidoActual ver = new Empresa.frmVerPedidoActual(sNombreEmpresa, sNombreEmpleado, iIdPedido);
                    ver.ShowDialog();
                }

                else if (dgvDatos.Columns[e.ColumnIndex].Name == "btnModificar")
                {
                    iIdPersona_Editar = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_persona"].Value);
                    iIdEmpleado_Editar = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_persona_empleado"].Value);
                    sNombreEmpleado = dgvDatos.Rows[e.RowIndex].Cells["empleado"].Value.ToString().Trim().ToUpper();
                    iIdPedido = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_pedido"].Value);
                    sEstadoOrden = dgvDatos.Rows[e.RowIndex].Cells["estado"].Value.ToString().Trim().ToUpper();

                    if (sEstadoOrden == "PAGADA")
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "La comanda ya se encuentra pagada.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        Empresa.frmEditarComandaEmpresarial editar = new Empresa.frmEditarComandaEmpresarial(iIdPersona_Editar, sNombreEmpresa, sNombreEmpleado, iIdEmpleado_Editar, iIdPedido);
                        editar.ShowDialog();

                        if (editar.DialogResult == DialogResult.OK)
                        {
                            llenarGrid();
                        }
                    }
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
    }
}
