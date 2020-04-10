using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Empresa
{
    public partial class frmPantallaEspereAlmuerzos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;

        string sSql;
        string sNombreProducto;
        string sCodigoClaseProducto;

        DataTable dtConsulta;
        DataTable dtItems;
        DataTable dtDetalleItems;

        bool bRespuesta;

        int iIdOrigenOrden;
        int iIdListaMinorista;
        int iPagaIva;
        int iIdPersonaEmpresa;
        int iIdPersonaEmpleado;

        Decimal dbValor;
        Decimal dbTotal;
        Decimal dbIVA;

        SqlParameter[] parametro;

        public frmPantallaEspereAlmuerzos(int iIdOrigenOrden_P)
        {
            this.iIdOrigenOrden = iIdOrigenOrden_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA RELLENAR EL ARREGLO DE MAXIMOS
        private void llenarArregloMaximo()
        {
            Program.iIDMESA = 0;

            Program.sDatosMaximo[0] = Program.sNombreUsuario;
            Program.sDatosMaximo[1] = Environment.MachineName.ToString();
            Program.sDatosMaximo[2] = "A";
        }

        //FUNCION PARA BUSCAR AL EMPLEADO CON NÚMERO DE CEDULA
        private void consultarEmpleadoIdentificacion()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_busqueda_huellas_empleados_empresa" + Environment.NewLine;
                sSql += "where identificacion = @identificacion";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@identificacion";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtIdentificacion.Text.Trim();

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentra el registro en el sistema.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                iIdPersonaEmpresa = Convert.ToInt32(dtConsulta.Rows[0]["id_persona_empresa"].ToString());
                iIdPersonaEmpleado = Convert.ToInt32(dtConsulta.Rows[0]["id_persona_empleado"].ToString());

                dtDetalleItems = new DataTable();
                dtDetalleItems.Clear();

                if (llenarDataTable() == false)
                    return;

                Clases_Crear_Comandas.ClaseCrearComanda comanda = new Clases_Crear_Comandas.ClaseCrearComanda();

                if (comanda.insertarComanda(0, iIdPersonaEmpresa, iIdPersonaEmpleado, iIdOrigenOrden, dbTotal, "Cerrada",
                                        0, 0, Program.CAJERO_ID, 0, "", Program.iIdMesero, Program.iIdPosTerminal,
                                        0, 0, 0, 0, Program.iIdPosCierreCajero, dtItems, dtDetalleItems, 0, 
                                        Program.iIdLocalidad, conexion) == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = comanda.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                int iIdPedido = comanda.iIdPedido;
                int iNumeroPedidoOrden = comanda.iNumeroPedidoOrden;

                if (Program.iHabilitarDestinosImpresion == 1)
                {
                    ReportesTextBox.frmVerPrecuentaEmpresaTextBox precuenta = new ReportesTextBox.frmVerPrecuentaEmpresaTextBox(iIdPedido.ToString(), 1, 1, 0, 0);
                    precuenta.ShowDialog();
                }

                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Guardado en la orden: " + iNumeroPedidoOrden.ToString() + ".";
                ok.ShowDialog();
                Cursor = Cursors.Default;
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtIdentificacion.Clear();
            txtIdentificacion.Focus();
        }
        
        //FUNCION PARA CONSTRUIR EL DATATABLE DE ITEMS
        private void crearDataTable()
        {
            try
            {
                dtItems = new DataTable();
                dtItems.Columns.Add("id_producto");
                dtItems.Columns.Add("valor_unitario");
                dtItems.Columns.Add("cantidad");
                dtItems.Columns.Add("valor_descuento");
                dtItems.Columns.Add("paga_iva");
                dtItems.Columns.Add("bandera_cortesia");
                dtItems.Columns.Add("bandera_descuento");
                dtItems.Columns.Add("bandera_comentario");
                dtItems.Columns.Add("id_mascara");
                dtItems.Columns.Add("id_ordenamiento");
                dtItems.Columns.Add("secuencia_impresion");
                dtItems.Columns.Add("motivo_cortesia");
                dtItems.Columns.Add("motivo_descuento");
                dtItems.Columns.Add("codigo_producto");                
                dtItems.Columns.Add("nombre_producto");
                dtItems.Columns.Add("porcentaje_descuento");
                dtItems.Columns.Add("paga_servicio");
                dtItems.Clear();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATATABLE
        private bool llenarDataTable()
        {
            try
            {
                crearDataTable();

                sSql = "";
                sSql += "select * from pos_vw_datos_productos_comanda" + Environment.NewLine;
                sSql += "where id_lista_precio = @id_lista_precio" + Environment.NewLine;
                sSql += "and id_producto = @id_producto" ;

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_lista_precio";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdListaMinorista;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Program.iIdProductoAlmuerzoDefault;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se encuentra información del producto. Comuníquese con el administrador.";
                    ok.ShowDialog();
                    return false;
                }

                sNombreProducto = dtConsulta.Rows[0]["nombre_producto"].ToString();
                sCodigoClaseProducto = dtConsulta.Rows[0]["codigo_clase_producto"].ToString();
                dbValor = Convert.ToDecimal(dtConsulta.Rows[0]["valor"].ToString());
                iPagaIva = Convert.ToInt32(dtConsulta.Rows[0]["paga_iva"].ToString());

                if (iPagaIva == 1)
                {
                    dbIVA = dbValor * (Convert.ToDecimal(Program.iva));
                    dbTotal = dbValor + dbIVA;
                }
                else
                {
                    dbTotal = dbValor;
                }

                DataRow row = dtItems.NewRow();
                row["id_producto"] = Program.iIdProductoAlmuerzoDefault;
                row["valor_unitario"] = dbValor;
                row["cantidad"] = "1";
                row["valor_descuento"] = "0";
                row["paga_iva"] = iPagaIva;
                row["bandera_cortesia"] = "0";                
                row["bandera_descuento"] = "0";
                row["bandera_comentario"] = "0";
                row["id_mascara"] = "0";
                row["id_ordenamiento"] = "0";
                row["secuencia_impresion"] = "1";
                row["motivo_cortesia"] = "";
                row["motivo_descuento"] = "";
                row["codigo_producto"] = sCodigoClaseProducto;
                row["nombre_producto"] = sNombreProducto;
                row["porcentaje_descuento"] = "0";
                row["paga_servicio"] = "0";
                dtItems.Rows.Add(row);
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

        //FUNCION PARA CONSULTAR EL ID DE LA LISTA MINORISTA
        private void obtenerIdListaMinorista()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and lista_minorista = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                    iIdListaMinorista = 0;

                iIdListaMinorista = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmPantallaEspereAlmuerzos_Load(object sender, EventArgs e)
        {
            obtenerIdListaMinorista();
            if (Program.iVistaAplicacion == 1)
            {
                this.KeyPreview = true;
                btnConfiguracion.Visible = false;
            }

            else
            {
                this.KeyPreview = false;
                btnConfiguracion.Visible = true;
            }

            this.ActiveControl = txtIdentificacion;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPantallaEspereAlmuerzos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el número identificación.";
                ok.ShowDialog();
                txtIdentificacion.Focus();
                return;
            }

            consultarEmpleadoIdentificacion();
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Favor ingrese el número identificación.";
                    ok.ShowDialog();
                    txtIdentificacion.Focus();
                    return;
                }

                consultarEmpleadoIdentificacion();
            }
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            llenarArregloMaximo();

            Menú.frmCodigoOficina acceso = new Menú.frmCodigoOficina();
            acceso.ShowDialog();

            if (acceso.DialogResult == DialogResult.OK)
            {
                Oficina.frmNuevoMenuConfiguracion menuOficina = new Oficina.frmNuevoMenuConfiguracion();
                menuOficina.ShowInTaskbar = true;
                menuOficina.Show();
            }
        }
    }
}
