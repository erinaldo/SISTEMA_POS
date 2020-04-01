using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturador
{
    public partial class frmBuscarTicket : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        DataTable dtConsulta;
        bool bRespuesta;
        string sSql;
        int iIdOrden;
        int iRetorno;

        public frmBuscarTicket()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VERIFICAR SI YA ESTÁ EMITIDA UNA FACTURA EN UNA ORDEN
        private int validarPedido()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select top 1 NCP.id_pedido" + Environment.NewLine;
                sSql = sSql + "from cv403_numero_cab_pedido NCP, cv403_facturas_pedidos FP" + Environment.NewLine;
                sSql = sSql + "where FP.id_pedido = NCP.id_pedido" + Environment.NewLine;
                sSql = sSql + "and FP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and NCP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and NCP.numero_pedido = " + Convert.ToInt32(txtBuscar.Text.Trim()) + Environment.NewLine;
                sSql = sSql + "order by NCP.id_numero_cab_pedido desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        return 1;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA VALIDAR LA FACTURA
        private int validarFactura()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select FP.id_pedido" + Environment.NewLine;
                sSql = sSql + "from cv403_numeros_facturas NF, cv403_facturas_pedidos FP" + Environment.NewLine;
                sSql = sSql + "where NF.id_factura = FP.id_factura" + Environment.NewLine;
                sSql = sSql + "and NF.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and NF.numero_factura = " + Convert.ToInt32(txtBuscar.Text.Trim());


                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        return 1;
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    return 0;
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }
        #endregion

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() == "")
            {
                if (rbdTicket.Checked == true)
                {
                    ok.LblMensaje.Text = "Favor ingrese el número de la orden.";
                }

                else
                {
                    ok.LblMensaje.Text = "Favor ingrese el número de la factura.";
                }

                ok.ShowDialog();
            }

            else
            {
                if (rbdTicket.Checked == true)
                {
                    iRetorno = validarPedido();
                }

                else
                {
                    iRetorno = validarFactura();
                }

                if (iRetorno == 1)
                {
                    //ABRE FORMULARIO
                    Facturador.frmEditarFactura editar = new frmEditarFactura(iIdOrden);
                    editar.ShowDialog();
                    this.DialogResult = DialogResult.OK;
                }

                else
                {
                    ok.LblMensaje.Text = "No existen registros con los datos proporcionados";
                    ok.ShowDialog();
                    txtBuscar.Clear();
                    txtBuscar.Focus();
                }
            }
        }

        private void rbdTicket_CheckedChanged(object sender, EventArgs e)
        {
            if (rbdTicket.Checked == true)
            {
                grupoDatos.Text = "Búsqueda por Número de Ticket";
                txtBuscar.Focus();
            }
        }

        private void rbdFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbdFactura.Checked == true)
            {
                grupoDatos.Text = "Búsqueda por Número de Factura";
                txtBuscar.Focus();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void frmBuscarTicket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmBuscarTicket_Load(object sender, EventArgs e)
        {
            
        }
    }
}
