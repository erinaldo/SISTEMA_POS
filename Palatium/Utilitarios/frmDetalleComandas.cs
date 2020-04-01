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
    public partial class frmDetalleComandas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sFecha;

        int iIdPedido;
        int iEtiquetaConsumo;

        bool bRespuesta;

        DataTable dtConsulta;

        Decimal dbTotal;

        public frmDetalleComandas(int iIdPedido_P, int iEtiquetaConsumo_P)
        {
            this.iIdPedido = iIdPedido_P;
            this.iEtiquetaConsumo = iEtiquetaConsumo_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();
                txtTotal.Text = "0.00";

                sSql = "";
                sSql += "select identificacion, persona, numero_pedido, tipo_comanda, fecha_pedido," + Environment.NewLine;
                sSql += "cantidad, nombre, isnull(valor, 0) valor" + Environment.NewLine;
                sSql += "from pos_vw_detalle_pedidos_origen_orden" + Environment.NewLine;
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

                dbTotal = 0;

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No existen registros. Comuníquese con el administrador.";
                    ok.ShowDialog();
                    txtTotal.Text = dbTotal.ToString("N2");
                    return;
                }

                txtRazonSocial.Text = dtConsulta.Rows[0]["persona"].ToString().Trim().ToUpper();
                txtIdentificacion.Text = dtConsulta.Rows[0]["identificacion"].ToString().Trim().ToUpper();
                txtPedido.Text = dtConsulta.Rows[0]["numero_pedido"].ToString().Trim();
                txtTipoComanda.Text = dtConsulta.Rows[0]["tipo_comanda"].ToString().Trim().ToUpper();
                txtFecha.Text = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_pedido"].ToString()).ToString("dd-MM-yyyy");

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["cantidad"].ToString(),
                                      dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper(),
                                      Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString()).ToString("N2")
                                      );

                    dbTotal += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtTotal.Text = dbTotal.ToString("N2");
                dgvDatos.ClearSelection();
                btnSalir.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmDetalleComandas_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetalleComandas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmVerPrecuentaEmpresaTextBox precuenta = new ReportesTextBox.frmVerPrecuentaEmpresaTextBox(iIdPedido.ToString(), 1, 2, 1, iEtiquetaConsumo);
            precuenta.ShowDialog();
        }
    }
}
