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
    public partial class frmVerPedidoActual : Form
    {
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sNombreEmpresa;
        string sNombreEmpleado;

        bool bRespuesta;

        DataTable dtConsulta;

        int iIdPedido;

        Decimal dbSuma;

        public frmVerPedidoActual(string sNombreEmpresa_P, string sNombreEmpleado_P, int iIdPedido_P)
        {
            this.sNombreEmpresa = sNombreEmpresa_P;
            this.sNombreEmpleado = sNombreEmpleado_P;
            this.iIdPedido = iIdPedido_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_items_comanda_cliente_empresarial" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

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
                    txtTotal.Text = "0.00";
                    return;
                }

                dbSuma = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["cantidad"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["total"].ToString().Trim());

                    dbSuma += Convert.ToDecimal(dtConsulta.Rows[i]["total"].ToString().Trim());
                }

                txtTotal.Text = dbSuma.ToString("N2");
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

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmVerPedidoActual_Load(object sender, EventArgs e)
        {
            lblEmpresa.Text = sNombreEmpresa.ToUpper();
            lblEmpleado.Text = sNombreEmpleado.ToUpper();
            llenarGrid();
        }

        private void frmVerPedidoActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
