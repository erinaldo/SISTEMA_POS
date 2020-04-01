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
    public partial class Items : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta = false;

        int iIdProducto;
        int iVersionComanda;

        decimal dCantidad;
        decimal dValorUnitario;
        decimal dValorTotal;
        decimal dIva;

        public Items(int iVersionComanda_P)
        {
            this.iVersionComanda = iVersionComanda_P;
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {   
            try
            {
                Orden ord = Owner as Orden;
                                
                dValorUnitario = 0;
                dIva = Convert.ToDecimal(Program.iva) + 1;

                dCantidad = Convert.ToDecimal(txtCantidad.Text.Trim());
                dValorTotal = Convert.ToDecimal(txtPrecioProducto.Text.Trim());

                if (dCantidad > 1)
                {
                    dValorTotal = dValorTotal / dCantidad;
                }

                if (Program.iManejaServicio == 0)
                {
                    dIva += Convert.ToDecimal(Program.servicio);
                }

                dValorUnitario = dValorTotal / dIva;

                int x = 0;
                x = ord.dgvPedido.Rows.Add();
                ord.dgvPedido.Rows[x].Cells["cod"].Value = 2;
                ord.dgvPedido.Rows[x].Cells["producto"].Value = txtNombreProducto.Text.Trim().ToUpper();
                ord.dgvPedido.Rows[x].Cells["cantidad"].Value = txtCantidad.Text.Trim();
                ord.dgvPedido.Rows[x].Cells["guardada"].Value = 0;


                ord.dgvPedido.Rows[x].Cells["valuni"].Value = dValorUnitario.ToString();
                ord.dgvPedido.Rows[x].Cells["valor"].Value = (dValorUnitario * dCantidad).ToString("N2");
                Program.factorPrecio = 1;

                ord.dgvPedido.Rows[x].Cells["idProducto"].Value = Program.iIdProductoNuevoItem.ToString();
                ord.dgvPedido.Rows[x].Cells["cortesia"].Value = 0;
                ord.dgvPedido.Rows[x].Cells["motivoCortesia"].Value = "";
                ord.dgvPedido.Rows[x].Cells["cancelar"].Value = 0;
                ord.dgvPedido.Rows[x].Cells["motivoCancelacion"].Value = "";
                ord.dgvPedido.Rows[x].Cells["colIdMascara"].Value = "";
                ord.dgvPedido.Rows[x].Cells["colSecuenciaImpresion"].Value = iVersionComanda.ToString();
                ord.dgvPedido.Rows[x].Cells["colOrdenamiento"].Value = "";
                ord.dgvPedido.Rows[x].Cells["colIdOrden"].Value = "";
                ord.dgvPedido.Rows[x].Cells["pagaIva"].Value = "1";

                if (Program.factorPrecio != 1)
                {
                    ord.dgvPedido.Rows[x].Cells["cantidad"].Value = 0.5;
                    //cantidad = 0.5f;
                }

                Program.factorPrecio = 1;

                ord.calcularTotales();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrecioProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void Items_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Items_Load(object sender, EventArgs e)
        {
            
        }

    }
}
