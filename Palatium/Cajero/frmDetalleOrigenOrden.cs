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
    public partial class frmDetalleOrigenOrden : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;

        string sSql;
        string sCodigo;

        string sCantidad_REC;
        string sValor_REC;

        bool bRespuesta;

        DataTable dtOrigen;
        DataTable dtConsulta;

        int iIdPosCierreCajero;

        public frmDetalleOrigenOrden(int iIdPosCierreCajero_P)
        {
            this.iIdPosCierreCajero = iIdPosCierreCajero_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR LOS VALORES
        private bool consultarValores(string sCodigo_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORI ON ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sCantidad_REC = dtConsulta.Rows[0]["cuenta"].ToString().Trim();

                sSql = "";
                sSql += "select ltrim(str(isnull(sum(DP.cantidad * (DP.precio_unitario - DP.valor_dscto + DP.valor_iva + DP.valor_otro)), 0), 10, 2)) valor" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORI ON ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and ORI.codigo = '" + sCodigo_P + "'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                sValor_REC = dtConsulta.Rows[0]["valor"].ToString().Trim();

                if (sCodigo_P == "01")
                {
                    txtCantidadMesas.Text = sCantidad_REC;
                    txtTotalMesas.Text = sValor_REC;
                }

                if (sCodigo_P == "02")
                {
                    txtCantidadLlevar.Text = sCantidad_REC;
                    txtTotalLlevar.Text = sValor_REC;
                }

                if (sCodigo_P == "03")
                {
                    txtCantidadDomicilio.Text = sCantidad_REC;
                    txtTotalDomicilio.Text = sValor_REC;
                }

                if (sCodigo_P == "04")
                {
                    txtCantidadCortesias.Text = sCantidad_REC;
                    txtTotalCortesias.Text = sValor_REC;
                }

                if (sCodigo_P == "05")
                {
                    txtCantidadVales.Text = sCantidad_REC;
                    txtTotalVales.Text = sValor_REC;
                }

                if (sCodigo_P == "06")
                {
                    txtCantidadConsumoEmpleados.Text = sCantidad_REC;
                    txtTotalConsumoEmpleados.Text = sValor_REC;
                }

                //if (sCodigo_P == "07")
                //{

                //}

                if (sCodigo_P == "08")
                {
                    txtCantidadCanjes.Text = sCantidad_REC;
                    txtTotalCanjes.Text = sValor_REC;
                }

                //if (sCodigo_P == "09")
                //{
                    
                //}

                if (sCodigo_P == "10")
                {
                    txtCantidadVentaExpress.Text = sCantidad_REC;
                    txtTotalVentaExpress.Text = sValor_REC;
                }

                //if (sCodigo_P == "11")
                //{
                    
                //}

                if (sCodigo_P == "12")
                {
                    txtCantidadEmpresas.Text = sCantidad_REC;
                    txtTotalEmpresa.Text = sValor_REC;
                }

                if (sCodigo_P == "13")
                {
                    txtCantidadTarjetaAlmuerzo.Text = sCantidad_REC;
                    txtTotalTarjetaAlmuerzos.Text = sValor_REC;
                }

                if (sCodigo_P == "14")
                {
                    txtCantidadConsumoInterno.Text = sCantidad_REC;
                    txtTotalConsumoInterno.Text = sValor_REC;
                }

                if (sCodigo_P == "15")
                {
                    txtCantidadCompraTarjetas.Text = sCantidad_REC;
                    txtTotalCompraTarjetas.Text = sValor_REC;
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

        //FUNCION PARA CARGAR LOS TIPOS DE COMANDAS
        private void cargarTipoComandas()
        {
            try
            {
                sSql = "";
                sSql += "select codigo from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by codigo";

                dtOrigen = new DataTable();
                dtOrigen.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtOrigen, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

                for (int i = 0; i < dtOrigen.Rows.Count; i++)
                {
                    sCodigo = dtOrigen.Rows[i]["codigo"].ToString().Trim();

                    if (consultarValores(sCodigo) == false)
                    {
                        break;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA OBTENER LA CANTIDAD DE PERSONAS ATENDIDAS
        private void cantidadPersonasAtendidas()
        {
            try
            {
                sSql = "";
                sSql += "select sum(CP.numero_personas) numero_personas" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORI ON ORI.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORI.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and ORI.codigo = '01'" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Pagada', 'Cerrada')";

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

                txtTotalPersonas.Text = dtConsulta.Rows[0]["numero_personas"].ToString().Trim();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmDetalleOrigenOrden_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            cargarTipoComandas();
            cantidadPersonasAtendidas();
            this.ActiveControl = lblControl;
            this.Cursor = Cursors.Default;
        }

        private void frmDetalleOrigenOrden_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
