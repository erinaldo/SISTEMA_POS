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
    public partial class frmDetalleProductoVenta : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        string sSql;
        string sFecha;
        string sNombreProducto;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdProducto;
        int iIdLocalidad;
        int iIdPosCierreCajeroParametro;

        //VARIABLES DEL GRID
        string sOrigen;
        string sMesa;
        int iNumeroPersonas;
        int iNumeroPedido;
        int iNumeroCuenta;
        string sHoraPedido;
        decimal dbCantidad;
        string sDescripcion;
        decimal dbTotal;
        decimal dbSumaTotal;

        public frmDetalleProductoVenta(int iIdProducto_P, string sNombreProducto_P, int iIdPosCierreCajero_P, int iIdLocalidad_P, string sFecha_P)
        {
            this.iIdProducto = iIdProducto_P;
            this.sNombreProducto = sNombreProducto_P;
            this.iIdLocalidad = iIdLocalidad_P;
            this.iIdPosCierreCajeroParametro = iIdPosCierreCajero_P;
            this.sFecha = sFecha_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO
        
        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();
                dbSumaTotal = 0;

                sSql = "";
                sSql += "select ORIGEN.descripcion descripcion_origen, isnull(MESA.descripcion, 'NINGUNA') descripcion_mesa,    " + Environment.NewLine;
                sSql += "CP.numero_personas, NCP.numero_pedido, CP.cuenta, CP.fecha_apertura_orden, DP.cantidad, NP.nombre," + Environment.NewLine;
                sSql += "DP.cantidad * (DP.precio_unitario - DP.valor_dscto + valor_iva + valor_otro) total_cobrado" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv403_numero_cab_pedido NCP ON CP.id_pedido = NCP.id_pedido" + Environment.NewLine;
                sSql += "and NCP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORIGEN ON ORIGEN.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORIGEN.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "pos_mesa MESA ON MESA.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                sSql += "and MESA.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajeroParametro + Environment.NewLine;
                //sSql += "and CP.estado_orden = 'Pagada'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')" + Environment.NewLine;
                sSql += "and CP.id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "and DP.id_producto = " + iIdProducto;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        sOrigen = dtConsulta.Rows[i][0].ToString().ToUpper();
                        sMesa = dtConsulta.Rows[i][1].ToString().ToUpper();
                        iNumeroPersonas = Convert.ToInt32(dtConsulta.Rows[i][2].ToString());
                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i][3].ToString());
                        iNumeroCuenta = Convert.ToInt32(dtConsulta.Rows[i][4].ToString());
                        sHoraPedido = Convert.ToDateTime(dtConsulta.Rows[i][5].ToString()).ToString("HH:mm");
                        dbCantidad = Convert.ToDecimal(dtConsulta.Rows[i][6].ToString());
                        sDescripcion = dtConsulta.Rows[i][7].ToString();
                        dbTotal = Convert.ToDecimal(dtConsulta.Rows[i][8].ToString());

                        dgvDatos.Rows.Add(sOrigen, sMesa, iNumeroPersonas.ToString(), iNumeroPedido.ToString(),
                                          iNumeroCuenta.ToString(), sHoraPedido, dbCantidad.ToString(), 
                                          sDescripcion, dbTotal.ToString("N2"));

                        dbSumaTotal += dbTotal;

                        if (i % 2 == 0)
                        {
                            dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                        }

                        else
                        {
                            dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }   
                    }

                    txtBuscar.Text = dbSumaTotal.ToString("N2");
                    dgvDatos.ClearSelection();
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

        private void frmDetalleProductoVenta_Load(object sender, EventArgs e)
        {
            lblProducto.Text = sNombreProducto;
            lblFecha.Text = sFecha;
            llenarGrid();
        }

        private void frmDetalleProductoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
