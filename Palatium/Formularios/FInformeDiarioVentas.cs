using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using ConexionBD;
using System.Diagnostics;
using MaterialSkin.Controls;
//using Microsoft.Office.Interop.Excel;  //libreria para excel 

namespace Palatium.Formularios
{
    public partial class FInformeDiarioVentas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();

        DataTable dtConsulta;
        
        bool bRespuesta;
        
        string sSql;
        string sFechaInicio;
        string sFechaFinal;

        string sIdFactura;
        string sNombreLocalidad;
        string sFechaFactura;
        string sNumeroFactura;
        string sCliente;
        string sBaseIVA;
        string sBaseCero;
        string sDescuento;
        string sValorNeto;
        string sValorIVA;
        string sValorServicio;
        string sValorTotal;
        string sIdComprobante;

        decimal dbValorBruto;
        decimal dbBaseIVA;
        decimal dbBaseCero;
        decimal dbDescuento;
        decimal dbValorNeto;
        decimal dbValorIVA;
        decimal dbValorServicio;
        decimal dbValorTotal;
                
        public FInformeDiarioVentas()
        {
            InitializeComponent();
        }

        private void FInformeDiarioVentas_Load(object sender, EventArgs e)
        {
            txtFechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
            llenarComboEmpresa();
            llenarCombLocalidad();
            llenarVendedor();
            llenarComboComprobante();
            llenarComboMoneda();
            llenarSentencia();
        }

        #region FUNCIONES DEL USUARIO

        //Llenar el combo Comprobante
        private void llenarComboComprobante()
        {
            try
            {
                sSql = "";
                sSql += "select idtipocomprobante, descripcion, codigo" + Environment.NewLine;
                sSql += "from vta_tipocomprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and codigo in ('Fac', 'NotV')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbComprobante.llenar(dtConsulta, sSql);

                if (cmbComprobante.Items.Count > 0)
                {
                    cmbComprobante.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Llenar combo tipo Empresa
        private void llenarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select idempresa, isnull(nombrecomercial, razonsocial) razon_social" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO localidad
        private void llenarCombLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbLocalidad.llenar(dtConsulta, sSql);

                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO Vendedor
        private void llenarVendedor()
        {
            try
            {
                sSql = "";
                sSql += "select id_vendedor, codigo" + Environment.NewLine;
                sSql += "from cv403_vendedores";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbVendedor.llenar(dtConsulta, sSql);

                if (cmbVendedor.Items.Count > 0)
                {
                    cmbVendedor.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO moneda
        private void llenarComboMoneda()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla='SYS$00021'" + Environment.NewLine;
                sSql += "and estado='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbMoneda.llenar(dtConsulta, sSql);

                if (cmbMoneda.Items.Count > 0)
                {
                    cmbMoneda.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR LA SENTENCIA SQL DEL DB AYUDA
        private void llenarSentencia()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, identificacion as Identificacion," + Environment.NewLine;
                sSql += "ltrim(isnull(nombres, '') + ' ' + apellidos) as Cliente" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dbAyudaPersonas.Ver(sSql, "identificacion", 0, 1, 2);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LIMPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtFechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
            dbAyudaPersonas.limpiar();
            chkAnuladas.Checked = false;
            chkCedulaRuc.Checked = false;
            chkCodigoVendedor.Checked = false;

            dgvInformeVentas.Rows.Clear();

            txtValorBruto.Text = "0.00";
            txtDescuento.Text = "0.00";
            txtValorNeto.Text = "0.00";
            txtValorIva.Text = "0.00";
            txtServicio.Text = "0.00";
            txtValorTotal.Text = "0.00";
        }

        //FUNCION PARA LLENAR EL GRID cuando no se haya seleccionado ningun cliente 
        private void llenarGrid()
        {
            try
            {
                dgvInformeVentas.Rows.Clear();
                dbBaseIVA = 0;
                dbBaseCero = 0;
                dbDescuento = 0;
                dbValorIVA = 0;
                dbValorServicio = 0;

                sFechaInicio = Convert.ToDateTime(txtFechaInicio.Text.Trim()).ToString("yyyy-MM-dd");
                sFechaFinal = Convert.ToDateTime(txtFechaFinal.Text.Trim()).ToString("yyyy-MM-dd");

                sSql = "";
                sSql += "Select V.id_factura, V.nombre_Localidad, V.fecha_factura, V.Numero_Factura," + Environment.NewLine;
                sSql += "case sum(V.valor_Bruto) When 0 then ' *** ANULADA ***' else isnull(V.Cliente,'') end Cliente," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.valor_Neto-V.base_cero), 10, 2)) Base_doce," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.Base_Cero), 10, 2)) Base_Cero," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.valor_Descuento), 10, 2)) Valor_Descuento," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.valor_Neto), 10, 2)) Valor_Neto," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.valor_Iva), 10, 2)) Valor_Iva," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.valor_servicio), 10, 2)) Valor_Servicio," + Environment.NewLine;
                sSql += "ltrim(str(sum(V.valor_Total), 10, 2)) Valor_Total," + Environment.NewLine;
                sSql += "V.idtipocomprobante" + Environment.NewLine;
                sSql += "from cv403_vw_facturas_det_pedidos V" + Environment.NewLine;
                sSql += "where V.id_localidad = " + Convert.ToInt32(cmbLocalidad.SelectedValue) + Environment.NewLine;
                sSql += "and V.cg_moneda = " + Convert.ToInt32(cmbMoneda.SelectedValue) + Environment.NewLine;
                sSql += "and V.fecha_factura between '" + sFechaInicio + "'" + Environment.NewLine;
                sSql += "and '" + sFechaFinal + "'" + Environment.NewLine;
                sSql += "and V.id_vendedor = " + Convert.ToInt32(cmbVendedor.SelectedValue) + Environment.NewLine;
                sSql += "and V.idtipocomprobante = " + Convert.ToInt32(cmbComprobante.SelectedValue) + Environment.NewLine;

                if (chkAnuladas.Checked == true)
                {
                    sSql += "and ((V.estado='A' and V.estado_det_pedido='A')" + Environment.NewLine;
                    sSql += "or (V.estado<>'A' and  V.estado_det_pedido <> 'A'))" + Environment.NewLine;
                }

                else
                {
                    sSql += "and V.estado_det_pedido = 'A'" + Environment.NewLine;
                }

                if (dbAyudaPersonas.iId != 0)
                {
                    sSql += "and V.id_persona = " + dbAyudaPersonas.iId + Environment.NewLine;
                }

                sSql += "Group By V.id_factura, V.nombre_Localidad, V.fecha_factura," + Environment.NewLine;
                sSql += "V.Numero_Factura, V.Cliente, V.estado , V.idtipocomprobante" + Environment.NewLine;
                sSql += "Order By V.nombre_Localidad,V.fecha_factura,V.Numero_Factura" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sIdFactura = dtConsulta.Rows[i][0].ToString();
                            sNombreLocalidad = dtConsulta.Rows[i][1].ToString().ToUpper();
                            sFechaFactura = Convert.ToDateTime(dtConsulta.Rows[i][2].ToString()).ToString("dd-MM-yyyy");
                            sNumeroFactura = dtConsulta.Rows[i][3].ToString().Trim().PadLeft(9, '0');
                            sCliente = dtConsulta.Rows[i][4].ToString().ToUpper();
                            sBaseIVA = dtConsulta.Rows[i][5].ToString();
                            sBaseCero = dtConsulta.Rows[i][6].ToString();
                            sDescuento = dtConsulta.Rows[i][7].ToString();
                            sValorNeto = dtConsulta.Rows[i][8].ToString();
                            sValorIVA = dtConsulta.Rows[i][9].ToString();
                            sValorServicio = dtConsulta.Rows[i][10].ToString();
                            sValorTotal = dtConsulta.Rows[i][11].ToString();
                            sIdComprobante = dtConsulta.Rows[i][12].ToString();

                            dbBaseIVA += Convert.ToDecimal(sBaseIVA);
                            dbBaseCero += Convert.ToDecimal(sBaseCero);
                            dbDescuento += Convert.ToDecimal(sDescuento);
                            dbValorIVA += Convert.ToDecimal(sValorIVA);
                            dbValorServicio += Convert.ToDecimal(sValorServicio);

                            dgvInformeVentas.Rows.Add(sIdFactura, sNombreLocalidad, sFechaFactura, sNumeroFactura,
                                                      sCliente, sBaseIVA, sBaseCero, sDescuento, sValorNeto,
                                                      sValorIVA, sValorServicio, sValorTotal, sIdComprobante);
                        }


                        txtValorBruto.Text = (dbBaseIVA + dbBaseCero).ToString("N2");
                        txtDescuento.Text = dbDescuento.ToString("N2");
                        txtValorNeto.Text = (dbBaseIVA + dbBaseCero - dbDescuento).ToString("N2");
                        txtValorIva.Text = dbValorIVA.ToString("N2");
                        txtServicio.Text = dbValorServicio.ToString("N2");
                        txtValorTotal.Text = (dbBaseIVA + dbBaseCero - dbDescuento + dbValorIVA + dbValorServicio).ToString("N2");

                        dgvInformeVentas.ClearSelection();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "No se ha encontrado datos para los criterios seleccionados";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion


        private void btnCerrarInformeVentas_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarInformeVentas_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        private void btnOKInformeVentas_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbEmpresa.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione una empresa.";
                    ok.ShowDialog();
                    cmbEmpresa.Focus();
                }

                else if (Convert.ToInt32(cmbLocalidad.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione una localidad.";
                    ok.ShowDialog();
                    cmbLocalidad.Focus();
                }

                else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione el tipo de moneda.";
                    ok.ShowDialog();
                    cmbMoneda.Focus();
                }

                else if (Convert.ToInt32(cmbVendedor.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione el vendedor.";
                    ok.ShowDialog();
                    cmbVendedor.Focus();
                }

                else if (Convert.ToInt32(cmbComprobante.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor seleccione el tipo de comprobante.";
                    ok.ShowDialog();
                    cmbComprobante.Focus();
                }

                else if (txtFechaInicio.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la fecha de inicio para buscar la información.";
                    ok.ShowDialog();
                    txtFechaInicio.Focus();
                }

                else if (txtFechaFinal.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la fecha final para buscar la información.";
                    ok.ShowDialog();
                    txtFechaFinal.Focus();
                }

                else if (Convert.ToDateTime(txtFechaInicio.Text.Trim()) > Convert.ToDateTime(txtFechaFinal.Text.Trim()))
                {
                    ok.lblMensaje.Text = "La fecha inicial no puede ser superior a la fecha final, para el rango de búsqueda.";
                    ok.ShowDialog();
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }

                else
                {
                    llenarGrid();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        public void exportarExcel(DataGridView tabla)
        {
            ok.lblMensaje.Text = "Módulo en desarrollo.";
        }
        
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvInformeVentas.Rows.Count > 0)
                exportarExcel(dgvInformeVentas);
            else
                MessageBox.Show("No hay datos para ser exportados");
        }
    }
}
