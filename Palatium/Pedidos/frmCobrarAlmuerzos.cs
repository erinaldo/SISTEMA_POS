using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmCobrarAlmuerzos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        Pedidos.ClaseIngresarCobros cobrarLinea = new ClaseIngresarCobros();
        Clases.ClaseCrearImpresion _imprimir = new Clases.ClaseCrearImpresion();
        Clases.ClaseCrearImpresion _imprimir2 = new Clases.ClaseCrearImpresion();

        string sSql;
        string sEstadoPago;
        string sMesa;
        string sTipoOrden;
        string sDescripcion;
        string sTexto;

        DataTable dtConsulta;
        DataTable dtImprimir;

        bool bRespuesta;

        int iIdPedido;
        int iIdDetPedido;
        int iIdPosMesa;
        int iNumeroMesa;
        int iCantidad;

        decimal dbPrecioUnitario;
        decimal dbTotal;

        double dbTotalEnviar;

        string sNombreImpresora;
        int iCantidadImpresiones; 
        string sPuertoImpresora;
        string sIpImpresora;
        string sDescripcionImpresora;
        int iCortarPapel;
        int iAbrirCajon;

        string _sCantidad;
        string _sDescripcion;
        double _dbUnitario;
        double _dbTotal;
        string _sMesa;
        string sNombreImpresoraAbrirCajon;


        public frmCobrarAlmuerzos()
        {
            InitializeComponent();
        }

        #region FUNCIONES PARA MOSTRAR LA PREUENTA Y FACTURA EN UN TEXTBOX

        //FUNCION PARA CONSULTAR LAS IMPRESORAS
        private void consultarImpresora()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select I.path_url, I.numero_impresion, I.puerto_impresora," + Environment.NewLine;
                sSql = sSql + "I.ip_impresora, I.descripcion, I.cortar_papel, I.abrir_cajon" + Environment.NewLine;
                sSql = sSql + "from pos_impresora I, pos_formato_precuenta FP" + Environment.NewLine;
                sSql = sSql + "where FP.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql = sSql + "and FP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FP.id_pos_formato_precuenta = " + Program.iFormatoPrecuenta;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresora = dtImprimir.Rows[0][0].ToString();
                        iCantidadImpresiones = Convert.ToInt16(dtImprimir.Rows[0][1].ToString());
                        sPuertoImpresora = dtImprimir.Rows[0][2].ToString();
                        sIpImpresora = dtImprimir.Rows[0][3].ToString();
                        sDescripcionImpresora = dtImprimir.Rows[0][4].ToString();
                        iCortarPapel = Convert.ToInt16(dtImprimir.Rows[0][5].ToString());
                        iAbrirCajon = Convert.ToInt16(dtImprimir.Rows[0][6].ToString());
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //EXTRAER LOS DATOS LAS IMPRESORAS
        public void consultarImpresoraAbrirCajon()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select I.path_url" + Environment.NewLine;
                sSql = sSql + "from pos_impresora I, pos_formato_factura FF" + Environment.NewLine;
                sSql = sSql + "where FF.id_pos_impresora = I.id_pos_impresora" + Environment.NewLine;
                sSql = sSql + "and FF.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and I.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and FF.id_pos_formato_factura = " + Program.iFormatoFactura;

                dtImprimir = new DataTable();
                dtImprimir.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtImprimir, sSql);

                if (bRespuesta == true)
                {
                    if (dtImprimir.Rows.Count > 0)
                    {
                        sNombreImpresoraAbrirCajon = dtImprimir.Rows[0][0].ToString();
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No existe el registro de configuración de impresora. Comuníquese con el administrador.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //CREAR REPORTE
        private void crearReporte(string sCantidad_P, string sDescripcion_P, double dbUnitario_P, double dbTotal_P, string sMesa_P)
        {
            try
            {
                sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "RECIBO DE PAGO".PadLeft(27, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "CLIENTE: CONSUMIDOR FINAL" + Environment.NewLine;
                sTexto += "RUC/CI : 9999999999999" + Environment.NewLine;
                sTexto += "FECHA  : " + DateTime.Now.ToString("dd-MM-yyyy") + Environment.NewLine;
                sTexto += "MESA   : " + sMesa_P.ToUpper() + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "CANT DESCRIPCION             V.UNI. TOT." + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                if (sDescripcion_P.Trim().Length > 21)
                {
                    sDescripcion_P = sDescripcion_P.Substring(0, 21);
                }

                sTexto += sCantidad_P.PadLeft(3, ' ') + "".PadLeft(2, ' ') + sDescripcion_P.ToUpper().PadRight(21, ' ') + dbUnitario_P.ToString("N2").PadLeft(7, ' ') + dbTotal_P.ToString("N2").PadLeft(7, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine + ".";

                //ENVIAR A IMPRIMIR
                _imprimir.iniciarImpresion();
                _imprimir.escritoEspaciadoCorto(sTexto);
                _imprimir.imprimirReporte(sNombreImpresora);

                //ENVIAR A IMPRIMIR
                _imprimir2.iniciarImpresion();
                _imprimir2.AbreCajon();
                _imprimir2.imprimirReporte(sNombreImpresoraAbrirCajon);
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select CP.id_pedido, DP.id_det_pedido, CP.id_pos_mesa, isnull(MESA.numero_mesa, 0) numero_mesa," + Environment.NewLine;
                sSql += "isnull(DP.estado_pago, 'PENDIENTE') estado_pago," + Environment.NewLine;
                sSql += "isnull(MESA.descripcion, 'NINGUNA') mesa, ORDEN.descripcion tipo_orden," + Environment.NewLine;
                sSql += "DP.cantidad, NP.nombre, DP.precio_unitario," + Environment.NewLine;
                sSql += "DP.cantidad * DP.precio_unitario total" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos PADRE ON PADRE.id_producto = P.id_producto_padre" + Environment.NewLine;
                sSql += "and PADRE.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORDEN ON CP.id_pos_origen_orden = ORDEN.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORDEN.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "pos_mesa MESA ON MESA.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                sSql += "and MESA.estado = 'A'" + Environment.NewLine;
                sSql += "where PADRE.maneja_almuerzos = 1" + Environment.NewLine;
                //sSql += "and CP.fecha_pedido = '" + sFechaActual + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and DP.estado_pago is null" + Environment.NewLine;
                sSql += "and CP.estado_orden in ('Abierta', 'Pre-Cuenta')" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and MESA.numero_mesa = " + Convert.ToInt32(txtMesa.Text.Trim()) + Environment.NewLine;
                }

                sSql += "and CP.id_pedido NOT IN (" + Environment.NewLine;
                sSql += "select CP.id_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_det_pedidos DP ON CP.id_pedido = DP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and DP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos P ON P.id_producto = DP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "cv401_productos PADRE ON PADRE.id_producto = P.id_producto_padre" + Environment.NewLine;
                sSql += "and PADRE.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden ORDEN ON CP.id_pos_origen_orden = ORDEN.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and ORDEN.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "pos_mesa MESA ON MESA.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                sSql += "and MESA.estado = 'A'" + Environment.NewLine;
                sSql += "where PADRE.maneja_almuerzos = 0" + Environment.NewLine;
                //sSql += "and CP.fecha_pedido = '" + sFechaActual + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "and DP.estado_pago is null" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and MESA.numero_mesa = " + Convert.ToInt32(txtMesa.Text.Trim()) + Environment.NewLine;
                }

                sSql += "and CP.estado_orden in ('Abierta', 'Pre-Cuenta'))" + Environment.NewLine;
                sSql += "order by CP.id_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            iIdPedido = Convert.ToInt32(dtConsulta.Rows[i][0].ToString());
                            iIdDetPedido = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                            iIdPosMesa = Convert.ToInt32(dtConsulta.Rows[i][2].ToString());
                            iNumeroMesa = Convert.ToInt32(dtConsulta.Rows[i][3].ToString());
                            sEstadoPago = dtConsulta.Rows[i][4].ToString().Trim().ToUpper();
                            sMesa = dtConsulta.Rows[i][5].ToString().Trim().ToUpper();
                            sTipoOrden = dtConsulta.Rows[i][6].ToString().Trim().ToUpper();
                            iCantidad = Convert.ToInt32(dtConsulta.Rows[i][7].ToString());
                            sDescripcion = dtConsulta.Rows[i][8].ToString().Trim().ToUpper();
                            dbPrecioUnitario = Convert.ToDecimal(dtConsulta.Rows[i][9].ToString());
                            dbTotal = Convert.ToDecimal(dtConsulta.Rows[i][10].ToString());

                            dgvDatos.Rows.Add((i+1).ToString(), iIdPedido.ToString(), iIdDetPedido.ToString(), iIdPosMesa.ToString(), 
                                              iNumeroMesa.ToString(), sEstadoPago, sMesa, sTipoOrden, 
                                              iCantidad.ToString(), sDescripcion, dbPrecioUnitario.ToString("N2"),
                                              dbTotal.ToString("N2")
                                );

                            if (i %2 == 0)
                            {
                                dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                            }

                            else
                            {
                                dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 128);
                            }
                        }

                        //dgvDatos.Columns["numeracion"].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                        dgvDatos.ClearSelection();
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }


            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        #endregion

        private void frmCobrarAlmuerzos_Load(object sender, EventArgs e)
        {
            consultarImpresora();
            consultarImpresoraAbrirCajon();
            llenarGrid(0);
            this.ActiveControl = txtMesa;
        }

        private void frmCobrarAlmuerzos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }
        }

        private void dgvDatos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvDatos.Columns[e.ColumnIndex].Name == "imprimir" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvDatos.Rows[e.RowIndex].Cells["imprimir"] as DataGridViewButtonCell;
                Icon icoAtomico = Properties.Resources.icono_pagar_almuerzo;
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                this.dgvDatos.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                this.dgvDatos.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                e.Handled = true;
            }
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                
   
                if (dgvDatos.Columns[e.ColumnIndex].Name == "imprimir")
                {
                    if (dgvDatos.CurrentRow.Cells["estado"].Value.ToString() == "PAGADA")
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "La línea actual ya fue cobrada. Para actualizar los cambios, favor dar clic en Actualizar.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        iIdPedido = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pedido"].Value);
                        iIdDetPedido = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_det_pedido"].Value);
                        dbTotalEnviar = Convert.ToDouble(dgvDatos.CurrentRow.Cells["valor_total"].Value);

                        _sCantidad = dgvDatos.CurrentRow.Cells["cantidad"].Value.ToString().Trim();
                        _sDescripcion = dgvDatos.CurrentRow.Cells["descripcion"].Value.ToString().Trim().ToUpper();
                        _dbUnitario = Convert.ToDouble(dgvDatos.CurrentRow.Cells["precio_unitario"].Value);
                        _dbTotal = Convert.ToDouble(dgvDatos.CurrentRow.Cells["valor_total"].Value);
                        _sMesa = dgvDatos.CurrentRow.Cells["mesa"].Value.ToString().Trim().ToUpper();

                        if (cobrarLinea.recibirParametros(iIdPedido, iIdDetPedido, dbTotalEnviar) == true)
                        {
                            
                            //AQUI MANDAR A IMPRIMIR
                            crearReporte(_sCantidad, sDescripcion, _dbUnitario, _dbTotal, _sMesa);

                            ok = new VentanasMensajes.frmMensajeOK();
                            ok.LblMensaje.Text = "COBRO ÉXITOSO.";
                            ok.ShowDialog();

                            if (txtMesa.Text.Trim() == "")
                            {
                                llenarGrid(0);
                            }

                            else
                            {
                                llenarGrid(1);

                                if (dgvDatos.Rows.Count == 0)
                                {
                                    llenarGrid(0);
                                    txtMesa.Clear();
                                }
                            }

                            dgvDatos.ClearSelection();
                            txtMesa.Focus();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtMesa_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtMesa.Text.Trim() == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }

            else
            {
                caracter.soloNumeros(e);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtMesa.Clear();
            llenarGrid(0);
            txtMesa.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtMesa.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }

            txtMesa.Focus();
        }
    }
}
