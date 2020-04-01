using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmDetalleVentas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        string sSql;
        string sDescripcion;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdRegistro;
        int iIdLocalidad;
        int iIdPosCierreCajeroParametro;

        decimal dbCantidad;
        decimal dbTotal;

        public frmDetalleVentas(int iIdLocalidad_P, int iIdPosCierreCajero_P)
        {
            this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;
            this.iIdLocalidad = iIdLocalidad_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX
        private void llenarComboTipoProducto()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_producto" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbTipoProducto.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
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
                sSql += "select DET.id_producto, sum(DET.cantidad) CANTIDAD, NOM.nombre," + Environment.NewLine;
                sSql += "ltrim(str(isnull(sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))), 0),10, 2)) TOTAL" + Environment.NewLine;
                sSql += "from cv403_det_pedidos DET inner join" + Environment.NewLine;
                sSql += "cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto" + Environment.NewLine;
                sSql += "and NOM.estado = 'A' inner join" + Environment.NewLine;
                sSql += "cv403_cab_pedidos CAB on CAB.id_pedido = DET.id_pedido" + Environment.NewLine;
                sSql += "and CAB.estado = 'A'" + Environment.NewLine;
                sSql += "and DET.estado = 'A' inner join" + Environment.NewLine;
                sSql += "cv401_productos PROD on NOM.id_producto = PROD.id_producto" + Environment.NewLine;
                sSql += "and PROD.estado = 'A'" + Environment.NewLine;
                sSql += "where CAB.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                sSql += "and CAB.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and CAB.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;

                if (Convert.ToInt32(cmbTipoProducto.SelectedValue) != 0)
                {
                    sSql += "and PROD.id_pos_tipo_producto = " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + Environment.NewLine;
                }

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and NOM.nombre like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "group by DET.id_producto, NOM.nombre" + Environment.NewLine;
                sSql += "order by sum(DET.cantidad)";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        iIdRegistro = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                        dbCantidad = Convert.ToDecimal(dtConsulta.Rows[i][1].ToString());
                        sDescripcion = dtConsulta.Rows[i][2].ToString().Trim().ToUpper();
                        dbTotal = Convert.ToDecimal(dtConsulta.Rows[i][3].ToString());

                        dgvDatos.Rows.Add(iIdRegistro.ToString(), (i + 1).ToString(), dbCantidad.ToString(), sDescripcion, dbTotal.ToString("N2"));

                        if (i % 2 == 0)
                        {
                            dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                        }

                        else
                        {
                            dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }

                    dgvDatos.ClearSelection();
                    txtBuscar.Focus();
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
                
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmDetalleVentas_Load(object sender, EventArgs e)
        {
            cmbTipoProducto.SelectedIndexChanged -= new EventHandler(cmbTipoProducto_SelectedIndexChanged);
            llenarComboTipoProducto();
            cmbTipoProducto.SelectedIndexChanged += new EventHandler(cmbTipoProducto_SelectedIndexChanged);
            llenarGrid();
        }

        private void cmbTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                llenarGrid();
            }
        }

        private void frmDetalleVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Cajero.frmDetalleProductoVenta venta = new frmDetalleProductoVenta(Convert.ToInt32(dgvDatos.CurrentRow.Cells["idProducto"].Value), dgvDatos.CurrentRow.Cells["descripcion"].Value.ToString().ToUpper(), iIdPosCierreCajeroParametro, iIdLocalidad, DateTime.Now.ToString("dd-MM-yyyy"));
            venta.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ReportesTextBox.frmReporteVendido vendido = new ReportesTextBox.frmReporteVendido(iIdLocalidad, iIdPosCierreCajeroParametro);
            vendido.ShowDialog();
        }
    }
}
