using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ComandaNueva
{
    public partial class frmCrearNuevoItem : Form
    {        
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        public string sNombreProducto;

        public decimal dCantidad;
        public decimal dValorUnitario;
        decimal dValorTotal;
        decimal dIva;

        public frmCrearNuevoItem()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNombreProducto.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese una descripción para el nuevo producto.";
                    ok.ShowDialog();
                    txtNombreProducto.Focus();
                    return;
                }

                if (txtCantidad.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese la cantidad de productos a pedir.";
                    ok.ShowDialog();
                    txtCantidad.Focus();
                    return;
                }

                if (txtPrecioProducto.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese el precio del producto.";
                    ok.ShowDialog();
                    txtPrecioProducto.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtCantidad.Text.Trim()) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese una cantidad superior a cero.";
                    ok.ShowDialog();
                    txtCantidad.Clear();
                    txtCantidad.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtPrecioProducto.Text.Trim()) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "El precio del producto no puede ser un valor cero.";
                    ok.ShowDialog();
                    txtPrecioProducto.Clear();
                    txtPrecioProducto.Focus();
                    return;
                }

                dValorUnitario = 0;
                dIva = Convert.ToDecimal(Program.iva) + 1;

                dCantidad = Convert.ToDecimal(txtCantidad.Text.Trim());
                dValorTotal = Convert.ToDecimal(txtPrecioProducto.Text.Trim());

                dValorUnitario = dValorTotal / dCantidad;

                dValorUnitario = dValorUnitario / dIva;

                //if (Program.iManejaServicio == 0)
                //{
                //    dIva += Convert.ToDecimal(Program.servicio);
                //}

                //dValorUnitario = dValorTotal / dIva;

                sNombreProducto = txtNombreProducto.Text.Trim().ToUpper();
                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtPrecioProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void frmCrearNuevoItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
