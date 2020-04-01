using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class MotivoCancelación : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sIdOrden;
        string sSql;
        string sFecha;
        DataTable dtConsulta;
        bool bRespuesta = false;
        Double DSumaDetalleOrden;


        public MotivoCancelación(string sIdOrden)
        {
            this.sIdOrden = sIdOrden;
            InitializeComponent();            
        }

        #region FUNCIONES DEL USUARIO

        //EXTRAER EL TOTAL DE LA ORDEN PARA ALMACENAR EN LA BASE DE DATOS
        private void sumarTotalOrden()
        {
            try
            {
                sSql = "";
                sSql += "select sum(DP.cantidad * DP.precio_unitario * (" + Convert.ToDouble(Program.iva + Program.servicio + 1) + ")) total" + Environment.NewLine;
                sSql += "from cv403_det_pedidos as DP, cv403_cab_pedidos as CP" + Environment.NewLine;
                sSql += "where (CP.id_pedido = DP.id_pedido)" + Environment.NewLine;
                sSql += "and CP.id_pedido = " + Convert.ToInt32(sIdOrden) + Environment.NewLine;
                sSql += "and CP.estado = 'A' and DP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    DSumaDetalleOrden = Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al consultar el registro.";
                ok.ShowDialog();
                this.Close();
            }
        }


        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                //INSERTAMOS INFORMACION EN LA TABLA DE CANCELACIONES
                sSql = "";
                sSql += "insert into pos_cancelacion (" + Environment.NewLine;
                sSql += "id_pedido, motivo_cancelacion, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Convert.ToInt32(sIdOrden) + ",'" + txtMotivo.Text + "', 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAMOS EL ESTADO EN LA TABLA CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                sSql += "fecha_cierre_orden = '" + sFecha + "'," + Environment.NewLine;
                sSql += "estado_orden = 'Cancelada'," + Environment.NewLine;
                sSql += "valor_cancelado = " + DSumaDetalleOrden + "," + Environment.NewLine;
                sSql += "estado = 'N'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pedido = " + Convert.ToInt32(sIdOrden);

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONAR EL ID MOVIMIENTO BODEGA
                sSql = "";
                sSql += "select id_movimiento_bodega" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos" + Environment.NewLine;
                sSql += "where id_pedido = " + sIdOrden;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    int iIdCabeceraMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                    sSql = "";
                    sSql += "update cv402_cabecera_movimientos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_movimiento_bodega = " + iIdCabeceraMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    sSql = "";
                    sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                    sSql += "estado = 'E'" + Environment.NewLine;
                    sSql += "where id_movimiento_bodega = " + iIdCabeceraMovimiento;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }


                //SI NO HUBO INCONVENIENTES REALIZA EL COMMIT PARA ALMACENAR LA INFORMACIÓN
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "La orden ha sido cancelada éxitosamente.";
                ok.ShowDialog();
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            catch (Exception)
            {
                goto reversa;
            }

            //ACCEDER A HACER EL ROLLBACK
             //=======================================================================================================
            reversa:
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    ok.LblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios.";
                    ok.ShowDialog();
                }
        }


        #endregion

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text == "")
            {
                ok.LblMensaje.Text = "Debe ingresar un motivo de la cancelación del pedido.";
                ok.ShowDialog();
                txtMotivo.Focus();
            }
            else
            {
                insertarRegistro();
            }
        }

        private void MotivoCancelación_Load(object sender, EventArgs e)
        {
            sumarTotalOrden();
            this.ActiveControl = txtMotivo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MotivoCancelación_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtMotivo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
