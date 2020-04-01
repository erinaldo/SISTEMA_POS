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
    public partial class frmDetallePedidoClienteEmpresarial : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        
        DataTable dtConsulta;

        bool bRespuesta;

        int iIdClienteEmpresarial;
        int iIdPersonaCE;

        DateTime dFechaInicio;
        DateTime dFechaFinal;

        public frmDetallePedidoClienteEmpresarial()
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
                sSql += "select empleado, fecha_pedido, nombre, cantidad, valor" + Environment.NewLine;
                sSql += "from pos_vw_detalle_pedidos_cliente_empresarial" + Environment.NewLine;
                sSql += "where id_persona = " + iIdPersonaCE + Environment.NewLine;
                sSql += "and fecha_pedido between '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;

                if (Convert.ToInt32(cmbEmpleados.SelectedValue) != 0)
                {
                    sSql += "and id_empleado_cliente_empresarial = " + cmbEmpleados.SelectedValue + Environment.NewLine;
                }

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
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["empleado"].ToString().Trim().ToUpper(),
                                      Convert.ToDateTime(dtConsulta.Rows[i]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["cantidad"].ToString().Trim(),
                                      dtConsulta.Rows[i]["valor"].ToString().Trim());
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

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            cmbClienteEmpresarial.SelectedIndexChanged -= new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbClienteEmpresarial.SelectedIndexChanged += new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);

            llenarComboEmpleados();

            cmbEmpleados.Enabled = false;

            dgvDatos.Rows.Clear();
        }

        #endregion

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

        private void frmDetallePedidoClienteEmpresarial_Load(object sender, EventArgs e)
        {
            cmbClienteEmpresarial.SelectedIndexChanged -= new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);
            llenarComboEmpresas();
            cmbClienteEmpresarial.SelectedIndexChanged += new EventHandler(cmbClienteEmpresarial_SelectedIndexChanged);

            llenarComboEmpleados();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbClienteEmpresarial.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado un cliente empresarial.";
                ok.ShowDialog();
                return;
            }

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

            iIdClienteEmpresarial = Convert.ToInt32(cmbClienteEmpresarial.SelectedValue);

            DataRow[] dFila = cmbClienteEmpresarial.dt.Select("id_pos_cliente_empresarial = " + iIdClienteEmpresarial);

            if (dFila.Length != 0)
            {
                iIdPersonaCE = Convert.ToInt32(dFila[0][2].ToString());

                llenarGrid();
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Error en la configuración del cliente empresarial. Comuníquese con el administrador.";
                ok.ShowDialog();
                return;
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

        private void frmDetallePedidoClienteEmpresarial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
