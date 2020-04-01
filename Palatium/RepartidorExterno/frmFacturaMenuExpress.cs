using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace Palatium.Informes
{
    public partial class frmFacturaMenuExpress : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sFechaDesde;
        string sFechaHasta;

        string sSql;
        DataTable dtConsulta;
        DataTable dtDatos;
        DataTable dtMenuExpress;
        bool bRespuesta = false;
        Double dSubtotal, dIva, dServicio, dDescuento, dValorItem;
        string sNumeroFactura;
        string sTexto;
        string sPath;
        string sSecuencial;
        int iIdMenuExpress = 98935;

        public frmFacturaMenuExpress()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR EL COMBO DE LAS CATEGORIAS
        private void llenarComboRepartidores()
        {
            try
            {
                dtConsulta = new DataTable();
                sSql = "";
                sSql = sSql + "select id_pos_origen_orden, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_origen_orden" + Environment.NewLine;
                sSql = sSql + "where repartidor_externo = 1" + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                cmbRepartidoresExternos.llenar(dtConsulta, sSql);

                if (cmbRepartidoresExternos.Items.Count > 0)
                    cmbRepartidoresExternos.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }
           
        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            llenarComboRepartidores();
            txtDesde.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            TxtHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            txtDatos.Clear();
        }

        //EXTRAER EL REGISTRO DE MENU EXPRESS
        private void extaerRegistroMenuExpress()
        {
            try
            {
                sSql = "select TP.identificacion, TP.apellidos as nombre, TD.calle_principal + ' ' + " +
                       "TD.numero_vivienda + ' ' + TD.calle_interseccion as direccion, TT.oficina " +
                       "from tp_personas TP, tp_direcciones TD, tp_telefonos TT where TD.id_persona = TP.id_persona " +
                       "and TT.id_persona = TP.id_persona and TP.estado = 'A' and TD.estado = 'A' and TT.estado = 'A' " +
                       "and TP.id_persona = " + iIdMenuExpress;
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dtMenuExpress = dtConsulta;
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro de Menú Express.";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception)
            {
                goto mensaje;
            }

        mensaje:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
                txtDatos.Clear();
            }

        fin:
            { }
        }


        //PROCESO PARA CREAR LA FACTURA
        //private void insertarFactura()
        //{
        //    try
        //    {
        //        string sFechaCompleta = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        //        string sFechaCorta = DateTime.Now.ToString("yyyy/MM/dd");

        //        //INICIAMOS UNA NUEVA TRANSACCION
        //        //=======================================================================================================
        //        //=======================================================================================================
        //        if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
        //        {
        //            MessageBox.Show("Error al abrir transacción");
        //            goto fin;
        //        }
        //        //=======================================================================================================

        //        //INSERTAR EN LA TABLA CV403_FACTURAS
        //        //=========================================================================================================
        //        //=========================================================================================================

        //        sSql = "insert into cv403_facturas (idempresa, id_persona, cg_empresa, idtipocomprobante, " +
        //               "id_localidad, idformulariossri, id_vendedor, id_forma_pago, fecha_factura, fecha_vcto, " +
        //               "cg_moneda, valor, cg_estado_factura, editable, fecha_ingreso, usuario_ingreso, " +
        //               "terminal_ingreso, estado, numero_replica_trigger, numero_control_replica, " +
        //               "Direccion_Factura,Telefono_Factura,Ciudad_Factura, correo_electronico)" +
        //               "values(" + Program.iIdEmpresa + ", " + iIdMenuExpress + ", " + Program.iCgEmpresa +
        //               ",1," + Program.iIdLocalidad + ", " + Program.iIdFormularioSri + ", 1, 14, '" + sFechaCorta +
        //               "', '" + sFechaCorta + "', " + Program.iMoneda + ", " + dTotal + ", 0, 0, '" + sFechaCompleta +
        //               "', '" + Program.sNombreUsuario + "', '" + Environment.MachineName.ToString() + "', 'A', 1, 0, '" +
        //               dtMenuExpress.Rows[0].ItemArray[2].ToString() + "', '" + dtMenuExpress.Rows[0].ItemArray[3].ToString() + "', '" + sCiudad + "', '" + txtMail.Text.Trim() + "')";

        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }
        //        //=========================================================================================================

        //        //EXTRAER ID DEL REGISTRO CV403_FACTURAS
        //        //=========================================================================================================
        //        //=========================================================================================================
        //        sSql = "select max(id_factura) numero from cv403_facturas";
        //        dtConsulta = new DataTable();
        //        dtConsulta.Clear();

        //        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

        //        if (bRespuesta == true)
        //        {
        //            iIdFactura = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
        //        }
        //        else
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }
        //        //=========================================================================================================

        //        //INSERTAR EN LA TABLA CV403_NUMEROS_FACTURAS
        //        //=========================================================================================================
        //        //=========================================================================================================

        //        sSql = "insert into cv403_numeros_facturas (id_factura, idtipocomprobante, numero_factura, " +
        //               "fecha_ingreso, usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, " +
        //               "numero_control_replica) " +
        //               "values (" + iIdFactura + ",1," + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + ", '" + sFechaCompleta + "', '" +
        //                Program.sNombreUsuario + "', '" + Environment.MachineName.ToString() + "', 'A', 1, 0 )";

        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }
        //        //=========================================================================================================

        //        //ACTUALIZAMOS LA TABLA CV403_DCTOS_POR_COBRAR
        //        //=========================================================================================================
        //        //=========================================================================================================

        //        sSql = "update cv403_dctos_por_cobrar set id_factura = " + iIdFactura +
        //               " where id_pedido = " + Convert.ToInt32(sIdOrden) + "";

        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }
        //        //=========================================================================================================

        //        //INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS
        //        //=========================================================================================================
        //        //=========================================================================================================

        //        sSql = "insert into cv403_facturas_pedidos (id_factura, id_pedido, fecha_ingreso, " +
        //               "usuario_ingreso, terminal_ingreso, estado, numero_replica_trigger, numero_control_replica) " +
        //               "values (" + iIdFactura + ", " + Convert.ToInt32(sIdOrden) + ", '" + sFechaCompleta +
        //               "', '" + Program.sNombreUsuario + "', '" + Environment.MachineName.ToString() + "', 'A', 1, 0 )";

        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }

        //        //EXTRAER ID DEL REGISTRO CV403_FACTURAS_PEDIDOS
        //        //=========================================================================================================
        //        //=========================================================================================================
        //        sSql = "select max(id_facturas_pedidos) numero from cv403_facturas_pedidos";
        //        dtConsulta = new DataTable();
        //        dtConsulta.Clear();

        //        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

        //        if (bRespuesta == true)
        //        {
        //            iIdFacturaPedido = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
        //        }
        //        else
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }
        //        //=========================================================================================================

        //        //RECUPERAMOS DATOS NECESARIOS DE LA TABLA CV403_DETALLE_PEDIDOS
        //        //=========================================================================================================
        //        //=========================================================================================================

        //        sSql = "select id_det_pedido, id_producto, cantidad from cv403_det_pedidos where id_pedido = " +
        //               Convert.ToInt32(sIdOrden) + " and estado = 'A'";

        //        dtConsulta = new DataTable();
        //        dtConsulta.Clear();

        //        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

        //        if (bRespuesta == false)
        //        {
        //            //ENVIAR A HACER UN ROLLBACK
        //            goto reversa;
        //        }
        //        //=========================================================================================================

        //        sSql = "update tp_localidades_impresoras set numero_factura = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + " where id_localidad = " + Program.iIdLocalidad;

        //        //sisque no me ejuta el query 
        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //hara el rolBAck
        //            goto reversa;

        //        }

        //        //ACTUALIZAR EL ESTADO A PAGADA EN CV403_CAB_PEDIDOS
        //        sSql = "update cv403_cab_pedidos set estado_orden = 'Pagada' where id_pedido = " + Convert.ToInt32(sIdOrden);

        //        //sisque no me ejuta el query 
        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //hara el rolBAck
        //            goto reversa;

        //        }

        //        //ACTUALIZAR EL ESTADO A PAGADA Y AGREGAMOS LA FECHA DE CIERRE DE ORDENEN CV403_CAB_PEDIDOS
        //        sSql = "update cv403_cab_pedidos set id_persona = " + iIdPersona +
        //               ", fecha_cierre_orden = '" + sFechaCompleta + "'where id_pedido = " + Convert.ToInt32(sIdOrden);

        //        //sisque no me ejuta el query 
        //        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //        {
        //            //hara el rolBAck
        //            goto reversa;

        //        }

        //        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
        //        //crearReporte();
        //        goto fin;

        //    }
        //    catch (Exception)
        //    {
        //        goto reversa;
        //    }

        ////ACCEDER A HACER EL ROLLBACK
        ////=======================================================================================================
        //reversa:
        //    {
        //        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
        //        MessageBox.Show("Ocurrió un problema en la transacción. No se guardarán los cambios");
        //    }

        ////=======================================================================================================
        //fin:
        //    { }
        //}

        //FUNCION PARA LEER LOS VALORES PARA REGISTRAR EN LA FACTURA
        private void datosFactura()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select L.id_localidad, L.establecimiento, L.punto_emision, " + Environment.NewLine;
                sSql = sSql + "P.numero_factura from tp_localidades L, tp_localidades_impresoras P " + Environment.NewLine;
                sSql = sSql + "where L.id_localidad = P.id_localidad" + Environment.NewLine;
                sSql = sSql + "and L.id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No se encuentran registros en la consulta.";
                        ok.ShowDialog();
                    }
                    else
                    {
                        sSecuencial = dtConsulta.Rows[0].ItemArray[3].ToString().PadLeft(9, '0');

                        sNumeroFactura = dtConsulta.Rows[0].ItemArray[1].ToString() + "-" + dtConsulta.Rows[0].ItemArray[2].ToString() + "-" + sSecuencial;
                        listarRepartidor();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message; 
                ok.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID CON LOS DATOS DEL MENU EXPRESS
        private void listarRepartidor()
        {
            try
            {
                dSubtotal = 0;
                dIva = 0;
                dServicio = 0;
                dDescuento = 0;

                sSql = "";
                sSql = sSql + "select CP.id_pedido, CP.fecha_apertura_orden, NCP.numero_pedido," + Environment.NewLine;
                sSql = sSql + "TP.identificacion, ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) cliente," + Environment.NewLine;
                sSql = sSql + "(TD.direccion + ' ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion) direccion," + Environment.NewLine;
                sSql = sSql + "isnull(TT.oficina, '') oficina, isnull(TT.celular, '') celular, isnull(TT.domicilio, '') domicilio" + Environment.NewLine;
                sSql = sSql + "from cv403_cab_pedidos CP, cv403_numero_cab_pedido NCP, tp_personas TP," + Environment.NewLine;
                sSql = sSql + "tp_direcciones TD, tp_telefonos TT" + Environment.NewLine;
                sSql = sSql + "where NCP.id_pedido = CP.id_pedido " + Environment.NewLine;
                sSql = sSql + "and CP.id_persona = TP.id_persona" + Environment.NewLine;
                sSql = sSql + "and TD.id_persona = TP.id_persona" + Environment.NewLine;
                sSql = sSql + "and TT.id_persona = TP.id_persona" + Environment.NewLine;
                sSql = sSql + "and CP.id_pos_origen_orden = " + Convert.ToInt32(cmbRepartidoresExternos.SelectedValue) + Environment.NewLine;
                sSql = sSql + "and CP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and NCP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and TP.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and TD.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and TT.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and CP.fecha_orden between '" + sFechaDesde + "'" + Environment.NewLine;
                sSql = sSql + "and '" + sFechaHasta + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No existen órdenes para " + cmbRepartidoresExternos.Text + " en el rango de fechas seleccionado.";
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

                

                //LLENAR ENCABEZADO PARA NENU EXPRESS
                sTexto = "";
                sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(6, ' ') + "FACTURA N° " + sNumeroFactura + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "FECHA     : " + DateTime.Now.ToString("dd/MM/yyyy") + Environment.NewLine;
                sTexto = sTexto + "CI/RUC    : " + dtConsulta.Rows[0].ItemArray[3].ToString() + Environment.NewLine;
                sTexto = sTexto + "CLIENTE   : " + dtConsulta.Rows[0].ItemArray[4].ToString() + Environment.NewLine;

                //TELEFONO
                if (dtConsulta.Rows[0].ItemArray[8].ToString() != "")
                {
                    sTexto = sTexto + "TELÉFONO  : " + dtConsulta.Rows[0].ItemArray[8].ToString() + Environment.NewLine;
                }

                else if (dtConsulta.Rows[0].ItemArray[7].ToString() != "")
                {
                    sTexto = sTexto + "TELÉFONO  : " + dtConsulta.Rows[0].ItemArray[7].ToString() + Environment.NewLine;
                }

                else if (dtConsulta.Rows[0].ItemArray[6].ToString() != "")
                {
                    sTexto = sTexto + "TELÉFONO  : " + dtConsulta.Rows[0].ItemArray[6].ToString() + Environment.NewLine;
                }
                
                
                //DIRECCION
                if (dtConsulta.Rows[0].ItemArray[8].ToString().Length <= 28)
                {
                    sTexto = sTexto + "DIRECCIÓN : " + dtConsulta.Rows[0].ItemArray[5].ToString() + Environment.NewLine + Environment.NewLine;
                }

                else
                {
                    sTexto = sTexto + "DIRECCIÓN : " + dtConsulta.Rows[0].ItemArray[5].ToString().Substring(0, 28) + Environment.NewLine;
                    sTexto = sTexto + "".PadLeft(12, ' ') + dtConsulta.Rows[0].ItemArray[5].ToString().Substring(28) + Environment.NewLine + Environment.NewLine;
                }
                

                //sTexto = sTexto + "CANT.".PadRight(7, ' ') + "DESCRIPCIÓN".PadRight(18, ' ') + "".PadLeft(2, ' ') + "V. UNIT.".PadRight(10, ' ') + "".PadLeft(2, ' ') + "V. TOT.".PadRight(10, ' ') + Environment.NewLine;
                sTexto = sTexto + "CANT.".PadRight(7, ' ') + "DESCRIPCIÓN".PadRight(16, ' ') + "V. UNIT.".PadRight(10, ' ') + "V. TOT." + Environment.NewLine;

                ////LLENAR EL GRID PARA MOSTRAR LAS ORDENES DE MENU EXPRESS
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sSql = "select sum(cantidad * precio_unitario) subtotal, sum(cantidad * valor_iva) iva, sum(cantidad * valor_otro) servicio, sum(cantidad * valor_dscto) descuento  from cv403_det_pedidos where id_pedido = " + Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                    dtDatos = new DataTable();
                    dtDatos.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtDatos, sSql);

                    if (bRespuesta == true)
                    {
                        dValorItem = Convert.ToDouble(dtDatos.Rows[0].ItemArray[0].ToString());
                        sTexto = sTexto + "  1".PadRight(7, ' ') + ("ORDEN N° " + dtConsulta.Rows[i].ItemArray[2].ToString()).PadRight(16, ' ') + dValorItem.ToString("N2").PadLeft(8, ' ') + dValorItem.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(7, ' ') + dtConsulta.Rows[i].ItemArray[1].ToString().Substring(0, 10) +  Environment.NewLine;
                        dIva = dIva + Convert.ToDouble(dtDatos.Rows[0].ItemArray[1].ToString());
                        dServicio = dServicio + Convert.ToDouble(dtDatos.Rows[0].ItemArray[2].ToString());
                        dDescuento = dDescuento + Convert.ToDouble(dtDatos.Rows[0].ItemArray[3].ToString());

                        dSubtotal = dSubtotal + dValorItem;                        
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                    }
                }

                sTexto = sTexto + Environment.NewLine + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + ("SUBTOTAL " + (Program.iva * 100).ToString() + "%").PadRight(25, ' ') + ":" + dSubtotal.ToString("N2").PadLeft(9, ' ') +  Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + "DESCUENTO:".PadRight(25, ' ') + ":" + dDescuento.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + "SUBTOTAL SIN IMPUESTOS:".PadRight(25, ' ') + ":" + (dSubtotal - dDescuento).ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + ("IVA " + (Program.iva * 100).ToString() + "%").PadRight(25, ' ') + ":" + dIva.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + ((Program.servicio * 100).ToString() + "% SERVICIO").PadRight(25, ' ') + ":" + dServicio.ToString("N2").PadLeft(9, ' ') + Environment.NewLine;
                sTexto = sTexto + "".PadLeft(5, ' ') + "TOTAL A PAGAR".PadRight(25, ' ') + ":" + (dSubtotal - dDescuento + dIva + dServicio).ToString("N2").PadLeft(9, ' ');
                sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                txtDatos.Text = sTexto;
                goto fin;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        fin:
            { }
        }

        #endregion

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtDesde.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtDesde.Text = calendario.txtFecha.Text;
                sFechaDesde = txtDesde.Text.Substring(6, 4) + "/" + txtDesde.Text.Substring(3, 2) + "/" + txtDesde.Text.Substring(0, 2);
            }
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(TxtHasta.Text.Trim());
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                TxtHasta.Text = calendario.txtFecha.Text;
                sFechaHasta = TxtHasta.Text.Substring(6, 4) + "/" + TxtHasta.Text.Substring(3, 2) + "/" + TxtHasta.Text.Substring(0, 2);
            }
        }

        private void frmFacturaMenuExpress_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtDesde.Text.Trim()) > Convert.ToDateTime(TxtHasta.Text.Trim()))
            {
                ok.LblMensaje.Text = "El rango de fechas no está definido correctamente.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
            else
            {
                datosFactura();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {

        }
    }
}
