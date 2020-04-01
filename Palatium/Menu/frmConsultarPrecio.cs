using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Menu
{
    public partial class frmConsultarPrecio : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        int iIdProducto;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        double dSubtotal;
        double dIva;
        double dTotal;

        public frmConsultarPrecio(int iIdProducto)
        {
            this.iIdProducto = iIdProducto;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR DATOS
        private void consultarDatos(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select P.codigo, NP.nombre, PP.valor, " + Environment.NewLine;
                sSql += "(select nombre from cv401_nombre_productos" + Environment.NewLine;
                sSql += "where id_producto = P.id_producto_padre) categoria" + Environment.NewLine;
                sSql += "from  cv401_productos P, cv401_nombre_productos NP," + Environment.NewLine;
                sSql += "cv403_precios_productos PP" + Environment.NewLine;
                sSql += "where NP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and PP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and PP.estado = 'A'" + Environment.NewLine;
                sSql += "and PP.id_lista_precio = 4" + Environment.NewLine;

                if (iOp == 0)
                {
                    sSql += "and P.id_producto = " + iIdProducto;
                }

                else if (iOp == 1)
                {
                    sSql += "and P.codigo = '" + txtCodigo.Text.Trim() + "'";
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtCodigo.Text = dtConsulta.Rows[0][0].ToString();
                        txtDescripcion.Text = dtConsulta.Rows[0][1].ToString();
                        txtCategoria.Text = dtConsulta.Rows[0][3].ToString();

                        dSubtotal = Convert.ToDouble(dtConsulta.Rows[0][2].ToString());
                        dIva = Program.iva * dSubtotal;
                        dTotal = dSubtotal + dIva;

                        txtPrecioSinImpuestos.Text = dSubtotal.ToString("N2");
                        txtTotal.Text = (dSubtotal + dIva).ToString("N2");
                        dIva = Convert.ToDouble(txtTotal.Text.Trim()) - Convert.ToDouble(txtPrecioSinImpuestos.Text.Trim());
                        txtIva.Text = dIva.ToString("N2");
                        
                    }

                    else
                    {
                        txtDescripcion.Clear();
                        txtCategoria.Clear();
                        txtPrecioSinImpuestos.Text = "0.00";
                        txtIva.Text = "0.00";
                        txtTotal.Text = "0.00";
                        //txtCodigo.Focus();
                    }
                    txtCodigo.Focus();
                    txtCodigo.SelectionStart = txtCodigo.Text.Trim().Length;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    this.Close();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                this.Close();
            }
        }

        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConsultarPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmConsultarPrecio_Load(object sender, EventArgs e)
        {
            lblIva.Text = (Program.iva * 100).ToString() + "%:";
            consultarDatos(0);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            consultarDatos(1);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                consultarDatos(1);
            }
        }
    }
}
