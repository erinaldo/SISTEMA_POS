using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Diagnostics;
using System.Threading;

namespace Palatium
{
    public partial class frmGrid : Form
    {
        Clases.ClaseValidarRUC ruc = new Clases.ClaseValidarRUC();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch ok = new VentanasMensajes.frmMensajeCatch();

                
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta;

        Facturacion_Electronica.dsFacturaElectronica ds = new Facturacion_Electronica.dsFacturaElectronica();

        private Process teclado;

        public frmGrid()
        {
            InitializeComponent();
        }

        //FUNCION ACTIVA TECLADO
        private void activaTeclado()
        {
            //this.touchKeyboard1.SetShowTouchKeyboard(this.txtAreaImprimir1, DevComponents.DotNetBar.Keyboard.TouchKeyboardStyle.Floating);
            //this.touchKeyboard1.SetShowTouchKeyboard(this.txtAreaImprimir2, DevComponents.DotNetBar.Keyboard.TouchKeyboardStyle.Floating);
            //this.touchKeyboard1.SetShowTouchKeyboard(this.txtAreaImprimir3, DevComponents.DotNetBar.Keyboard.TouchKeyboardStyle.Floating);
        }

        //AGREGAR FILAS
        private void agregar()
        {
            dgv_DetallePago.Rows.Add("1", "EFECTIVO", "10.00");
            dgv_DetallePago.Rows.Add("2", "VISA", "20.00");
        }

        //CREAR RIDE DE PRUEBA
        private void crearRide(DataTable dtDatos)
        {
            try
            {
                DataTable dt = ds.Tables["dtFactura"];
                dt.Clear();

                DataRow dr;
                int iColumna;




                for (int i = 0; i < dtDatos.Rows.Count; i++)
                {
                    dr = dt.NewRow();
                    iColumna = 0;

                    dr["id_Factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["fecha_factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString().Substring(0, 10);
                    iColumna++;
                    dr["fecha_vencimiento"] = dtDatos.Rows[i].ItemArray[iColumna].ToString().Substring(0, 10);
                    iColumna++;
                    dr["plazo"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["direccion_factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["sector"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["telefono_factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["ciudad_factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["fabricante"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["referencia"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["placa"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["kilometraje"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["comentarios"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["usuario_ingreso"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["fecha_ingreso"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["codigo_alterno"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["identificacion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["nombres"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["apellidos"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["nombre_comercial"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["valida_stock"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["valida_stock_descripcion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["id_det_pedido"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["codigo"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["nombre"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["cantidad"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["precio_unitario"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["valor_dscto"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["valor_ice"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["paga_ice"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["porcentaje_iva"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["valor_iva"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["paga_iva"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["estab"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["ptoemi"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["numero_factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString().PadLeft(9, '0');
                    iColumna++;
                    dr["descripcion_pago"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["codigo_vendedor"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["abreviacion_titulo"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["vendedor"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["cargo"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["descripcion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["id_especificacion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["linea"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["numero_linea"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["unidad"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["clave_acceso"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["autorizacion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["fecha_autorizacion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString().Substring(0, 10);
                    
                    iColumna++;

                    if (dtDatos.Rows[i].ItemArray[iColumna].ToString().Length != 0)
                    {
                        dr["hora_autorizacion"] = dtDatos.Rows[i].ItemArray[iColumna].ToString().Substring(0, 8);
                    }

                    else
                    {
                        dr["hora_autorizacion"] = "";
                    }

                    iColumna++;
                    dr["ambiente"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["emision"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["email_factura"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["direccionmatriz"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["direccionsucursal"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["numeroresolucioncontribuyenteespecial"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["obligadollevarcontabilidad"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["tipo_comprobante"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["numeroruc"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["razonsocial"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["nombrecomercial"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["codigo_sri_forma_pago"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["descripcion_sri_forma_pago"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["propina"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["numero_orden"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["numero_cuenta"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["tipo_orden"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["hora"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["cajero"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["forma_pago"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    iColumna++;
                    dr["cambio"] = dtDatos.Rows[i].ItemArray[iColumna].ToString();
                    
                    dt.Rows.Add(dr);
                }

                Facturacion_Electronica.frmReporteFacturaElectronica ver = new Facturacion_Electronica.frmReporteFacturaElectronica(dt);
                ver.ShowDialog();
            }

            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        private void frmGrid_Load(object sender, EventArgs e)
        {
            agregar();
            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int digito = Convert.ToInt32(textBox1.Text.Substring(2, 1));

            if (digito == 9)
            {
                if (ruc.validarRucPrivado(textBox1.Text.Trim()) == true)
                {
                    MessageBox.Show("RUC sociedad validado.");
                }

                else
                {
                    MessageBox.Show("RUC inválido.");
                }
            }

            else if (digito == 6)
            {
                if (ruc.validarRucPublico(textBox1.Text.Trim()) == true)
                {
                    MessageBox.Show("RUC público validado.");
                }

                else
                {
                    MessageBox.Show("RUC inválido.");
                }
            }

            else if ((digito >= 0) && (digito <= 5))
            {
                if (ruc.validarRucNatural(textBox1.Text.Trim()) == true)
                {
                    MessageBox.Show("RUC persona natural validado.");
                }

                else
                {
                    MessageBox.Show("RUC inválido.");
                }
            }

            else
            {
                MessageBox.Show("Número de identificación inválido.");
            }

        }

        private void txtDecimal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtDecimal.Text.Length; i++)
            {
                if (txtDecimal.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }

        private void BtnPunto_Click(object sender, EventArgs e)
        {
            if (txtDecimal.Text.Trim().Contains('.') == true)
            {
                MessageBox.Show("Ya está ingresado un punto.");
            }

            else if (txtDecimal.Text == "")
            {
                txtDecimal.Text = txtDecimal.Text + "0" + BtnPunto.Text;
            }

            else
            {
                txtDecimal.Text = txtDecimal.Text + BtnPunto.Text;
            }
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            if (txtDecimal.Text.Trim().Contains('.') == true)
            {
                int longi = txtDecimal.Text.Trim().Length;
                int band = 0, cont = 0;

                for (int i = 0; i < longi; i++)
                {
                    if (band == 1)
                        cont++;

                    if (txtDecimal.Text.Substring(i, 1) == ".")
                        band = 1;
                }

                if (cont < 2)
                {
                    txtDecimal.Text = txtDecimal.Text + Btn3.Text;
                }
            }

            else
            {
                txtDecimal.Text = txtDecimal.Text + Btn3.Text;
            }            
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show((Convert.ToDouble(Txt1.Text) / Convert.ToDouble(txt2.Text)).ToString());

                sSql = "select * from pos_mesero";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)

                //if (bRespuesta == true)
                {
                    MessageBox.Show("Respuesta correcta.");
                }
                else
                {
                    MessageBox.Show(sSql);
                    //ok.LblMensaje.Text = sSql;
                    //ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }   
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Clases.ClaseCrearImpresion imprimir = new Clases.ClaseCrearImpresion();
            imprimir.iniciarImpresion();
            imprimir.escritoEspaciadoCorto(txtAreaImprimir1.Text.Trim());
            imprimir.escritoFuenteAlta(txtAreaImprimir2.Text.Trim());
            imprimir.escritoEspaciadoCorto(txtAreaImprimir3.Text.Trim());
            imprimir.cortarPapel();
            
            imprimir.imprimirReporte(@"\\server-pc\PRECUENTA");
        }

        private void btnRide_Click(object sender, EventArgs e)
        {
            //sSql = "";
            //sSql = sSql + "declare @P_St_Tabla varchar(30)" + Environment.NewLine;
            //sSql = sSql + "execute Sp_Vta_Factura_e @P_St_Tabla output, " + Convert.ToInt32(Txt1.Text.Trim()) + Environment.NewLine;
            //sSql = sSql + "execute('select * from ' + @P_St_Tabla)";

            //dtConsulta = new DataTable();
            //dtConsulta.Clear();

            //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            //if (bRespuesta == true)
            //{
            //    MessageBox.Show(dtConsulta.Rows.Count.ToString());
            //}

            //else
            //{

            //}

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Genera_Ride(dtConsulta, Convert.ToInt64(Txt1.Text));

            if (bRespuesta == true)
            {
                crearRide(dtConsulta);
            }
        }

        private void btnTeclado_Click(object sender, EventArgs e)
        {
            Process.Start("osk.exe");
        }

        private void btnCerrarTeclado_Click(object sender, EventArgs e)
        {
            try
            {
                Process[] LocalByName = Process.GetProcessesByName("osk.exe");
                
                for (int i = 0; i < LocalByName.Length; i++)
                {
                    if (LocalByName[i].ToString() == "osk.exe")
                    {
                        MessageBox.Show("Abierto");
                    }
                }
            }

            catch(Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        private void keyboardControl1_Click(object sender, EventArgs e)
        {

        }

    }
}
