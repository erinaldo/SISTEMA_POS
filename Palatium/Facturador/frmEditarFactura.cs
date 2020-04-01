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
    public partial class frmEditarFactura : Form
    {
        /* FORMULARIO PARA EDITAR LOS DATOS DE UNA FACTURA EMITIDA DE FORMA ERRONEA
         * AUTOR: ELVIS GUAIGUA
         * FECHA DE CREACION: 2018/07/16
         */
        
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases_Factura_Electronica.ClaseEnviarMail correo = new Clases_Factura_Electronica.ClaseEnviarMail();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        Clases.ClaseValidarRUC validarRuc = new Clases.ClaseValidarRUC();
        ValidarCedula validarCedula = new ValidarCedula();

        int iIdFactura;
        int iIdTipoComprobante;

        string sSql;
        string sNumeroFactura;
        string sNumeroFacturaActual;
        string sMotivoAnulacion;
        string sCiudad = Program.sCiudadDefault;

        bool bRespuesta;
        bool bRespuestaEnvioMail;

        DataTable dtConsulta;
        DataTable dtAuxiliar;

        int iIdPersona;
        int iIdPedido;
        int iIdDespacho;
        int iIdEventoCobro;
        int iIdNumeroFactura;
        int idTipoIdentificacion;
        int idTipoPersona;
        int iTercerDigito;
        int iIdFactura_F;
        int iIdPosMovimientoCaja;
        int iNumeroMovimiento;
        int iNumeroPedido;
        int iIdLocalidadImpresora;
        int iNumeroMovimientoCaja;
        int iIdCaja = 30;
        int iOp;

        string sFacturaRecuperada;

        double dValor;

        string sFecha;
        string sCorreoEmisor;
        string sCorreoCopia1;
        string sCorreoCopia2;
        string sPalabraClave;
        string sSmtp;
        string sPuerto;
        string sManejaSSL;
        string sNombreComercial;
        string sRazonSocial;
        string sMensajeEnviar;
        string sFacturaActual;
        string sAsuntoMail;
        string sMensajeRetorno;

        long iMaximo;

        public frmEditarFactura(int iIdPedido)
        {
            this.iIdPedido= iIdPedido;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR LOS DATOS DEL CORREO DEL EMISOR
        private void consultarDatosMail()
        {
            try
            {
                sSql = "";
                sSql += "select correo_que_envia, correo_con_copia_1," + Environment.NewLine;
                sSql += "correo_con_copia_2, correo_palabra_clave," + Environment.NewLine;
                sSql += "correo_smtp, correo_puerto, maneja_SSL" + Environment.NewLine;
                sSql += "from pos_correo_emisor" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sCorreoEmisor = dtConsulta.Rows[0][0].ToString();
                        sCorreoCopia1 = dtConsulta.Rows[0][1].ToString();
                        sCorreoCopia2 = dtConsulta.Rows[0][2].ToString();
                        sPalabraClave = dtConsulta.Rows[0][3].ToString();
                        sSmtp = dtConsulta.Rows[0][4].ToString();
                        sPuerto = dtConsulta.Rows[0][5].ToString();
                        sManejaSSL = dtConsulta.Rows[0][6].ToString();

                        consultarNombreComercial();
                    }

                    else
                    {
                        iOp = 0;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA EXTRAER EL NOMBRE COMERCIAL
        private void consultarNombreComercial()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(nombrecomercial, '') nombrecomercial, razonsocial" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    sNombreComercial = dtConsulta.Rows[0][0].ToString();
                    sRazonSocial = dtConsulta.Rows[0][1].ToString();
                    iOp = 1;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //FUNCION PARA ENVIAR EL CORREO ELECTRONICO
        private bool enviarMail()
        {
            try
            {
                sAsuntoMail = "IMPORTANTE: ANULACIÓN DE FACTURA";

                sMensajeRetorno = crearMensajeEnvio();

                bRespuestaEnvioMail = correo.enviarCorreo(sSmtp, Convert.ToInt32(sPuerto), sCorreoEmisor,
                                      sPalabraClave, sCorreoEmisor, sCorreoEmisor,
                                      sCorreoCopia1, sCorreoCopia2, sAsuntoMail,
                                      "", sMensajeRetorno, Convert.ToInt32(sManejaSSL));

                if (bRespuestaEnvioMail == true)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }


        //FUNCION PARA CREAR EL CUERPO DEL MENSAJE
        private string crearMensajeEnvio()
        {
            try
            {
                sMensajeEnviar = "";

                sMensajeEnviar = sMensajeEnviar + "Estimado(a) " + sRazonSocial + ":" + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Se informa que se ha procedido a anular la factura No. " + sFacturaActual + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "MOTIVO DE ANULACIÓN:" + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + sMotivoAnulacion.ToUpper() + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Enviado por: " + Program.sDatosMaximo[0].ToUpper() + Environment.NewLine;
                sMensajeEnviar = sMensajeEnviar + "Fecha y Hora: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                return sMensajeEnviar;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return "";
            }
        }


        //=================================================================================================
        //=================================================================================================
        //=================================================================================================


        //FUNCION PARA REMOVER EL REGISTRO DE LA TABLA POS_MOVIMIENTO_CAJA
        private bool editarMovimientoCaja()
        {
            try
            {
                //CONSULTAR EL ID_POS_MOVIMIENTO_CAJA ASOCIADO A LA FACTURA VIGENTE
                sSql = "";
                sSql += "select MC.id_pos_movimiento_caja" + Environment.NewLine;
                sSql += "from" + Environment.NewLine;
                sSql += "cv403_facturas F, cv403_dctos_por_cobrar DC," + Environment.NewLine;
                sSql += "cv403_documentos_pagados DPA, cv403_pagos P," + Environment.NewLine;
                sSql += "cv403_documentos_pagos DPG, pos_movimiento_caja MC" + Environment.NewLine;
                sSql += "where DC.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and DPA.id_documento_cobrar = DC.id_documento_cobrar" + Environment.NewLine;
                sSql += "and P.id_pago = DPA.id_pago" + Environment.NewLine;
                sSql += "and DPG.id_pago = P.id_pago" + Environment.NewLine;
                sSql += "and MC.id_documento_pago = DPG.id_documento_pago" + Environment.NewLine;
                sSql += "and F.id_factura = " + iIdFactura + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and DC.estado = 'A'" + Environment.NewLine;
                sSql += "and DPA.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and DPG.estado = 'A'" + Environment.NewLine;
                sSql += "and MC.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //iIdPosMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            //ACTUALIZAR A ESTADO ELIMINADO 'E' EN LOS REGISTROS DE LA TABLA POS_MOVIMIENTO_CAJA
                            sSql = "";
                            sSql += "update pos_movimiento_caja set" + Environment.NewLine;
                            sSql += "estado = 'E'," + Environment.NewLine;
                            sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                            sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                            sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                            sSql += "where id_pos_movimiento_caja = " + Convert.ToInt32(dtConsulta.Rows[i][0].ToString()) + Environment.NewLine;


                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                catchMensaje.LblMensaje.Text = sSql;
                                catchMensaje.ShowDialog();
                                return false;
                            }
                        }

                    }
                }

                else
                {
                    return false;
                }
                
                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INSTRUCCION SQL PARA EXTRAER EL NUMERO DE MOVIMIENTO A INSERTAR
                sSql = "";
                sSql += "select numeromovimientocaja" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iNumeroMovimiento = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSTRUCCION PARA EXTRAER DATOS PARA INSERTAR EN EL MOVIMIENTO.
                sSql = "";
                sSql += "select id_persona, numero_pedido, establecimiento, punto_emision, numero_factura, id_pedido" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "order by id_det_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                        sFacturaRecuperada = dtConsulta.Rows[0][2].ToString() + "-" + dtConsulta.Rows[0][3].ToString() + "-" + dtConsulta.Rows[0][4].ToString().PadLeft(9, '0');
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSTRUCCIÓN PARA EXTRAER LAS FORMAS DE PAGO
                sSql = "";
                //sSql += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta, " + Environment.NewLine;
                //sSql += "isnull(valor_recibido, valor) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "select descripcion, sum(valor) valor, cambio,  count(*) cuenta, " + Environment.NewLine;
                sSql += "sum(isnull(valor_recibido, valor)) valor_recibido, id_documento_pago" + Environment.NewLine;
                sSql += "from pos_vw_pedido_forma_pago " + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "group by descripcion, valor, cambio, valor_recibido, " + Environment.NewLine;
                sSql += "id_pago, id_documento_pago " + Environment.NewLine;
                sSql += "having count(*) >= 1";

                dtAuxiliar = new DataTable();
                dtAuxiliar.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAuxiliar, sSql);

                if (bRespuesta == true)
                {
                    if (dtAuxiliar.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No existe formas de pagos realizados. Couníquese con el administrador del sistema.";
                        ok.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (iIdTipoComprobante == 1)
                {
                    sFacturaRecuperada = "FACT. No. " + sFacturaRecuperada;
                }

                else if (iIdTipoComprobante == 2)
                {
                    sFacturaRecuperada = "N. VENTA. No. " + sFacturaRecuperada;
                }

                for (int i = 0; i < dtAuxiliar.Rows.Count; i++)
                {
                    //INSTRUCCION INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_movimiento_caja (tipo_movimiento, idempresa, id_localidad, " + Environment.NewLine;
                    sSql += "id_persona, id_cliente, id_caja, id_pos_cargo, fecha, hora, cg_moneda, valor, concepto, " + Environment.NewLine;
                    sSql += "documento_venta, id_documento_pago, id_pos_jornada, id_pos_cierre_cajero," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                    sSql += "values (1, " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + Environment.NewLine;
                    sSql += ", " + Program.iIdPersonaMovimiento + ", " + iIdPersona + ", " + iIdCaja + ", 1 " + Environment.NewLine;
                    sSql += ", '" + sFecha + "', GETDATE(), " + Program.iMoneda + ", " + Environment.NewLine;
                    sSql += Convert.ToDouble(dtAuxiliar.Rows[i][1].ToString()) + ", '" + ("COBRO No. CUENTA " + iNumeroPedido.ToString() + " (" + dtAuxiliar.Rows[i][0].ToString() + ")") + "', '" + Environment.NewLine;
                    sSql += sFacturaRecuperada.Trim() + "', " + Environment.NewLine;
                    sSql += Convert.ToInt32(dtAuxiliar.Rows[i][5].ToString()) + ", " + Program.iJORNADA + ", " + Program.iIdPosCierreCajero + ", 'A'," + Environment.NewLine;
                    sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_MOVIMIENTO_CAJA
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    string sTabla = "pos_movimiento_caja";
                    string sCampo = "id_pos_movimiento_caja";

                    long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowDialog();
                        return false;
                    }

                    else
                    {
                        iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);
                    }

                    //INSTRUCCION INSERTAR EN LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                    sSql = "";
                    sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                    sSql += "id_pos_movimiento_caja, numero_movimiento_caja, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdPosMovimientoCaja + ", " + iNumeroMovimiento + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }

                    iNumeroMovimiento++;
                }

                //INSTRUCCION ACTUALIZAR EL NUMERO DE MOVIMIENTO EN TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeromovimientocaja = " + iNumeroMovimiento + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA BUSCAR LOS DATOS DE LA FACTURA EMITIDA
        private void cargarDatosFactura()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdFactura = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtIdentificacionActual.Text = dtConsulta.Rows[0][16].ToString();
                        txtNombresActual.Text = (dtConsulta.Rows[0][17].ToString() + " " + dtConsulta.Rows[0][18].ToString()).Trim();
                        txtTelefonoActual.Text = dtConsulta.Rows[0][4].ToString();
                        txtDireccionActual.Text = dtConsulta.Rows[0][3].ToString();
                        txtMailActual.Text = dtConsulta.Rows[0][6].ToString();
                        sNumeroFacturaActual = dtConsulta.Rows[0][37].ToString();
                        iIdTipoComprobante = Convert.ToInt32(dtConsulta.Rows[0][63].ToString());
                        sFacturaActual = dtConsulta.Rows[0][53].ToString() + "-" + dtConsulta.Rows[0][54].ToString() + "-" + dtConsulta.Rows[0][37].ToString().PadLeft(9, '0');

                        if (iIdTipoComprobante == 1)
                        {
                            lblTipoComprobante.Text = "FACTURA";
                        }

                        else if (iIdTipoComprobante == 2)
                        {
                            lblTipoComprobante.Text = "NOTA DE VENTA";
                        }

                        txtIdentificacion.Focus();                    
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;

        }

        //CONSULTAR DATOS EN LA BASE
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "SELECT TP.id_persona, TP.identificacion, TP.nombres, TP.apellidos, TP.correo_electronico," + Environment.NewLine;
                sSql += "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion," + Environment.NewLine;
                sSql += "TT.oficina, TT.celular, TD.direccion" + Environment.NewLine;
                sSql += "FROM dbo.tp_personas TP" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_direcciones TD ON TP.id_persona = TD.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and TD.estado = 'A'" + Environment.NewLine;
                sSql += "LEFT OUTER JOIN dbo.tp_telefonos TT ON TP.id_persona = TT.id_persona" + Environment.NewLine;
                sSql += "and TT.estado = 'A'" + Environment.NewLine;
                sSql += "WHERE  TP.identificacion = '" + txtIdentificacion.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        txtNombres.Text = dtConsulta.Rows[0][2].ToString();
                        txtApellidos.Text = dtConsulta.Rows[0][3].ToString();
                        txtMail.Text = dtConsulta.Rows[0][4].ToString();
                        txtDireccion.Text = dtConsulta.Rows[0][5].ToString();
                        sCiudad = dtConsulta.Rows[0][8].ToString();

                        if (dtConsulta.Rows[0][6].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][6].ToString();
                        }

                        else if (dtConsulta.Rows[0][7].ToString() != "")
                        {
                            txtTelefono.Text = dtConsulta.Rows[0][7].ToString();
                        }

                        else
                        {
                            txtTelefono.Text = "299999999";
                        }

                        btnGuardar.Enabled = true;
                        btnGuardar.Focus();
                    }

                    else
                    {
                        //ok.LblMensaje.Text = "No existe ningún registro con la identificación ingresada.";
                        //ok.ShowDialog();

                        frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
                        nuevoCliente.ShowDialog();

                        if (nuevoCliente.DialogResult == DialogResult.OK)
                        {
                            iIdPersona = nuevoCliente.iCodigo;
                            txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                            txtNombres.Text = nuevoCliente.sNombre;
                            txtApellidos.Text = nuevoCliente.sApellido;
                            txtTelefono.Text = nuevoCliente.sTelefono;
                            txtDireccion.Text = nuevoCliente.sDireccion;
                            txtMail.Text = nuevoCliente.sMail;
                            sCiudad = nuevoCliente.sCiudad;
                            nuevoCliente.Close();
                            btnGuardar.Enabled = true;
                            btnGuardar.Focus();
                        }
                    }

                    btnEditar.Visible = true;
                    goto fin;
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        mensaje:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        fin:
            { }
        }

        //FUNCION PARA VALIDAR LA CEDULA O RUC
        private void validarIdentificacion()
        {
            try
            {
                if (txtIdentificacion.Text.Length >= 10)
                {
                    iTercerDigito = Convert.ToInt32(txtIdentificacion.Text.Substring(2, 1));
                }
                else
                {
                    goto mensaje;
                }

                if (txtIdentificacion.Text.Length == 10)
                {
                    if (validarCedula.validarCedulaConsulta(txtIdentificacion.Text.Trim()) == "SI")
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                        goto fin;
                    }

                    else
                    {
                        goto mensaje;
                    }
                }

                else if (txtIdentificacion.Text.Length == 13)
                {
                    if (iTercerDigito == 9)
                    {
                        if (validarRuc.validarRucPrivado(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }

                    }

                    else if (iTercerDigito == 6)
                    {
                        if (validarRuc.validarRucPublico(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }
                    }

                    else if ((iTercerDigito <= 5) || (iTercerDigito >= 0))
                    {
                        if (validarRuc.validarRucNatural(txtIdentificacion.Text.Trim()) == true)
                        {
                            //CONSULTAR EN LA BASE DE DATOS
                            consultarRegistro();
                            goto fin;
                        }

                        else
                        {
                            goto mensaje;
                        }
                    }

                    else
                    {
                        goto mensaje;
                    }
                }

                else
                {
                    goto mensaje;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        mensaje:
            {
                ok.LblMensaje.Text = "El número de identificación ingresado es incorrecto.";
                ok.ShowDialog();
                btnGuardar.Enabled = false;
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        fin:
            { }
        }

        //FUNCION PARA LEER LOS VALORES PARA REGISTRAR EN LA FACTURA
        private void datosFactura()
        {
            try
            {
                sSql = "";
                sSql += "select L.id_localidad, L.establecimiento, L.punto_emision, " + Environment.NewLine;
                sSql += "P.numero_factura, P.numeronotaventa, P.numeromovimientocaja, P.id_localidad_impresora" + Environment.NewLine;
                sSql += "from tp_localidades L, tp_localidades_impresoras P " + Environment.NewLine;
                sSql += "where L.id_localidad = P.id_localidad" + Environment.NewLine;
                sSql += "and L.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and L.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'";

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
                        txtfacturacion.Text = dtConsulta.Rows[0][1].ToString() + "-" + dtConsulta.Rows[0][2].ToString();
                        TxtNumeroFactura.Text = dtConsulta.Rows[0][3].ToString();


                        txtfacturacion.Text = dtConsulta.Rows[0][1].ToString() + "-" + dtConsulta.Rows[0][2].ToString();

                        if (iIdTipoComprobante == 1)
                        {
                            TxtNumeroFactura.Text = dtConsulta.Rows[0][3].ToString();
                        }

                        else if (iIdTipoComprobante == 2)
                        {
                            TxtNumeroFactura.Text = dtConsulta.Rows[0][4].ToString();
                        }

                        iNumeroMovimientoCaja = Convert.ToInt32(dtConsulta.Rows[0][5].ToString());
                        iIdLocalidadImpresora = Convert.ToInt32(dtConsulta.Rows[0][6].ToString());

                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema en la consulta.";
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //Función para grabar la factura
        private void grabarRegistro()
        {
            try
            {
                //PROCESO DE INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "No se pudo iniciar la transacción.";
                    ok.ShowDialog();
                    goto fin;
                }

                //BLOQUE 1 - INICIO
                //Actualizamos el id_persona en las distintas tablas donde aparezca el id_persona
                //============================================================================
                //BLOQUE 1 - FIN
                //PROCESO PARA CAMBIAR EL IDENTIFICADOR DEL CLIENTE EN LA FACTURA EMITIDA                
                sSql = "";
                sSql += "update cv403_facturas set" + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                sSql += "direccion_factura = '" + txtDireccion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "telefono_factura = '" + txtTelefono.Text.Trim() + "'," + Environment.NewLine;
                sSql += "correo_electronico = '" + txtMail.Text.Trim() + "'" + Environment.NewLine;
                sSql += "Where id_factura = " + iIdFactura;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCESO PARA CAMBIAR EL NÚMERO DE FACTURA
                sSql = "";
                sSql += "update cv403_numeros_facturas set" + Environment.NewLine;
                sSql += "numero_factura = " + (Convert.ToInt32(TxtNumeroFactura.Text.Trim())) + Environment.NewLine;
                sSql += "Where id_factura = " + iIdFactura;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONA EL ID PEDIDO DE LA TABLA CV403_FACTURAS_PEDIDOS
                sSql = "";
                sSql += "select id_pedido" + Environment.NewLine;
                sSql += "from cv403_facturas_pedidos" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                        iIdPedido = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    else
                    {
                        goto reversa;
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL ID PERSONA EN LA TABLA CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "update cv403_cab_pedidos set " + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONA EL ID_DESPACHO DE LA TABLA CV403_DESPACHOS_PEDIDOS MEDIANTE EL ID_PEDIDO
                sSql = "";
                sSql += "select id_Despacho" + Environment.NewLine;
                sSql += "from cv403_despachos_pedidos" + Environment.NewLine;
                sSql += "where id_pedido = " + iIdPedido + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdDespacho = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                    else
                    {
                        goto reversa;
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZAR EL ID PERSONA EN LA TABLA CV403_CAB_DESPACHOS
                sSql = "";
                sSql += "update cv403_cab_despachos set " + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "where id_despacho = " + iIdDespacho;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONA EL ID EVENTO DE LA TABLA C403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "select id_evento_cobro" + Environment.NewLine;
                sSql += "from cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "where id_factura = " + iIdFactura;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdEventoCobro = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                    else
                    {
                        goto reversa;
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "update cv403_eventos_cobros set" + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + Environment.NewLine;
                sSql += "where id_evento_cobro = " + iIdEventoCobro;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //============================================================================
                //BLOQUE 1 - FIN  cambio de id_persona


                if (insertarFactura() == false)
                {
                    goto reversa;
                }

                if (editarMovimientoCaja() == false)
                {
                    goto reversa;
                }

                //ACTUALIZAR LA TABLA TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;

                if (iIdTipoComprobante == 1)
                {
                    sSql += "numero_factura = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + "," + Environment.NewLine;
                }

                else if (iIdTipoComprobante == 2)
                {
                    sSql += "numeronotaventa = " + (Convert.ToInt32(TxtNumeroFactura.Text) + 1) + "," + Environment.NewLine;
                }

                sSql += "numeromovimientocaja = " + iNumeroMovimientoCaja + Environment.NewLine;
                sSql += "where id_localidad_impresora = " + iIdLocalidadImpresora;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);


                if (iOp == 0)
                {
                    ok.LblMensaje.Text = "Su factura ha sido modificada con éxito.";
                }

                else
                {
                    if (enviarMail() == true)
                    {
                        ok.LblMensaje.Text = "Su factura ha sido modificada con éxito.";
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Su factura ha sido modificada con éxito." + Environment.NewLine + "No se pudo enviar el informe al administrador.";

                    }
                }

                ok.ShowInTaskbar = false;
                ok.ShowDialog();

                this.Close();

                goto fin;
            }
            catch (Exception exc)
            {
                catchMensaje.LblMensaje.Text = exc.ToString();
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                //MessageBox.Show("Ocurrió un problema al guardar el registro");
            }
        fin:
            { }
        }

        //FUNCION PARA INSERTAR UNA FACTURA PREVIO A LA ANULACIÓN
        private bool insertarFactura()
        {
            try
            {
                //VARIABLES PARA USAR DENTRO DE LA FUNCION
                string sTabla;
                string sCampo;

                int iIdCabPedido_F;
                int iIdCabDespacho_F;                
                int iIdFacturasPedidos_F;
                int iIdDespachoPedido_F;
                int iIdEventoCobro_F;

                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_cab_pedidos (idEmpresa, cg_empresa, Id_Localidad," + Environment.NewLine;
                sSql += "Fecha_Pedido, id_persona, Cg_Tipo_Cliente, Cg_Moneda, Porcentaje_Iva," + Environment.NewLine;
                sSql += "id_vendedor, Fabricante, referencia, Comentarios, cg_estado_Pedido," + Environment.NewLine;
                sSql += "Porcentaje_Dscto, Cg_Facturado, Fecha_Ingreso, Usuario_Ingreso," + Environment.NewLine;
                sSql += "Terminal_Ingreso, Estado, numero_replica_trigger, numero_control_replica, porcentaje_servicio, id_pos_cierre_cajero) " + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iIdPersona + ", 8032, " + Program.iMoneda + "," + Environment.NewLine;
                sSql += (Program.iva * 100) + ", " + Program.iIdVendedor + ", 78, '', '', 6967, 0, 7469," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "'A', 0, 0, " + (Program.servicio * 100) + ", " + Program.iIdPosCierreCajero + ")";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_pedidos";
                sCampo = "Id_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdCabPedido_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CAB_DESPACHOS
                sSql = "";
                sSql += "Insert Into cv403_cab_despachos (idEmpresa, id_persona, cg_empresa, Id_Localidad," + Environment.NewLine;
                sSql += "Fecha_Despacho, Cg_Motivo_Despacho, Id_Destinatario, Punto_Partida, Cg_Ciudad_Entrega," + Environment.NewLine;
                sSql += "Direccion_Entrega, Id_Transportador, Fecha_Inicio_Transporte, Fecha_Fin_Transporte," + Environment.NewLine;
                sSql += "cg_estado_Despacho, Punto_Venta, Comentarios, Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", " + Program.iCgEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', 6972, " + Program.iIdPersona + ", '" + Program.sCiudadDefault + "'," + Environment.NewLine;
                sSql += "0, '" + Program.sCiudadDefault + "', " + Program.iIdPersona + ", '" + sFecha + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', 6970, 1, '', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_CAB_DESPACHOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_cab_despachos";
                sCampo = "Id_Despacho";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdCabDespacho_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_FACTURAS
                sSql = "";
                sSql += "Insert Into cv403_facturas (idEmpresa, id_persona, cg_empresa, idtipocomprobante," + Environment.NewLine;
                sSql += "Id_Localidad, idformulariossri, id_vendedor, Id_Forma_Pago, Fecha_Vcto, Fecha_Factura," + Environment.NewLine;
                sSql += "Cg_Moneda, Valor, cg_estado_Factura, Direccion_Factura, Telefono_Factura, Ciudad_Factura," + Environment.NewLine;
                sSql += "Fabricante, Referencia, Comentarios, Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "Estado, editable, idSriAutorizacion, FacturaElectronica, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iIdPersona + ", " + Program.iCgEmpresa + ", " + iIdTipoComprobante + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 23, " + Program.iIdVendedor + ", 14, '" + sFecha + "'," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + dValor + ", 0, null, null, null, '', '', ''," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "'A', 0, 0, 0, 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_FACTURAS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_facturas";
                sCampo = "Id_factura";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdFactura_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_despachos_pedidos (Id_Despacho, Id_Pedido, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdCabDespacho_F + ", " + iIdCabPedido_F + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_DESPACHOS_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_despachos_pedidos";
                sCampo = "Id_Despacho_Pedido";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdDespachoPedido_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_FACTURAS_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_facturas_pedidos (" + Environment.NewLine;
                sSql += "id_factura, Id_Pedido, Fecha_Ingreso, Usuario_Ingreso," + Environment.NewLine;
                sSql += "Terminal_Ingreso, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdFactura_F + ", " + iIdCabPedido_F + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_FACTURAS_PEDIDOS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_facturas_pedidos";
                sCampo = "id_facturas_pedidos";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdFacturasPedidos_F = Convert.ToInt32(iMaximo);
                }

                ////INSTRUCCION PARA ACTUALIZAR LA TABLA TP_LOCALIDADES_IMPRESORAS
                //sSql = "";
                //sSql += "Update tp_localidades_impresoras Set " + Environment.NewLine;
                //sSql += "Numero_Factura = " + (Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + 1) + Environment.NewLine;
                //sSql += "Where Id_Localidad = " + Program.iIdLocalidad + Environment.NewLine;
                //sSql += "and estado = 'A'" + Environment.NewLine;

                ////EJECUTA INSTRUCCION SQL
                //if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                //{
                //    goto reversa;
                //}

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_NUMEROS_FACTURAS
                sSql = "";
                sSql += "Insert Into cv403_numeros_facturas (" + Environment.NewLine;
                sSql += "idTipoComprobante, id_factura, Numero_Factura, Fecha_Ingreso," + Environment.NewLine;
                sSql += "Usuario_Ingreso, Terminal_Ingreso, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdTipoComprobante + ", " + iIdFactura_F + ", " + Convert.ToInt32(sNumeroFacturaActual) + "," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_DET_PEDIDOS
                sSql = "";
                sSql += "Insert Into cv403_det_pedidos(" +Environment.NewLine;
                sSql += "id_Pedido, id_producto, Cg_Unidad_Medida, precio_unitario," + Environment.NewLine;
                sSql += "Cantidad, Valor_Dscto, Valor_Ice, Valor_Iva, comentario, Id_Definicion_Combo," + Environment.NewLine;
                sSql += "fecha_ingreso, Usuario_Ingreso, Terminal_ingreso, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdCabPedido_F + ", " + Program.iIdProductoAnular + ", 546, " + Program.dValorProductoAnular + "," + Environment.NewLine;
                sSql += "1, 0, 0, " + (Program.iva * 100) + ", '', null, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', 'A', 0,0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CANTIDADES_DESPACHADAS
                sSql = "";
                sSql += "Insert Into cv403_cantidades_despachadas (" + Environment.NewLine;
                sSql += "id_Despacho_Pedido, id_producto," + Environment.NewLine;
                sSql += "Cantidad, Estado, numero_replica_trigger, numero_control_replica) " + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdDespachoPedido_F + ", " + Program.iIdProductoAnular + ", 1, 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_CANTIDADES_FACTURADAS
                sSql = "";
                sSql += "Insert Into cv403_cantidades_facturadas (" + Environment.NewLine;
                sSql += "id_facturas_pedidos, id_producto," + Environment.NewLine;
                sSql += "Cantidad, Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdFacturasPedidos_F + ", " + Program.iIdProductoAnular + ", 1, 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "Insert Into cv403_eventos_cobros (" + Environment.NewLine;
                sSql += "idEmpresa, cg_empresa, id_persona, Id_Localidad," + Environment.NewLine;
                sSql += "Cg_Evento_Cobro, Cg_Moneda, Valor,Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "Estado, numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", " + Program.iCgEmpresa + ", " + Program.iIdPersona + "," + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", 7466, " + Program.iMoneda + ", " + Program.dValorProductoAnular + "," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 'A', 0, 0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }
                
                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV403_EVENTOS_COBROS
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "cv403_eventos_cobros ";
                sCampo = "id_evento_cobro";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdEventoCobro_F = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "Insert Into cv403_dctos_por_cobrar (" + Environment.NewLine;
                sSql += "id_evento_cobro, cg_tipo_documento," + Environment.NewLine;
                sSql += "id_factura, Numero_Documento, Fecha_Vcto, Cg_moneda, Valor, cg_estado_dcto," + Environment.NewLine;
                sSql += "Estado, Fecha_Ingreso, Usuario_Ingreso, Terminal_Ingreso," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                sSql += "Values (" + Environment.NewLine;
                sSql += iIdEventoCobro_F + ", 2725, " + iIdFactura_F + ", " + Convert.ToInt32(TxtNumeroFactura.Text.Trim()) + "," + Environment.NewLine;
                sSql += "'" + sFecha + "', " + Program.iMoneda + ", " + Program.dValorProductoAnular + ", 7460," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0,0)";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                if (anularFactura() == false)
                {
                    goto reversa;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                return false;
            }
        }
                
        //FUNCION PARA ANULAR LA FACTURA
        private bool anularFactura()
        {
            try
            {
                int iCorrelativo_F;
                int iIdFacturaPedido_F;
                int iIdPedido_F;
                int iIdDespacho_F;
                int iIdDespachoPedido_F;
                int iIdDocumentoPorCobrar_F;
                int iIdEventoCobro_F;


                //SELECCIONAR EL CORRELATIVO DE TABLA TP_CODIGOS
                sSql = "";
                sSql += "Select Correlativo Valor" + Environment.NewLine;
                sSql += "From tp_codigos" + Environment.NewLine;
                sSql += "Where tabla = 'SYS$01339'" + Environment.NewLine;
                sSql += "And codigo = '02'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iCorrelativo_F = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        goto reversa;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //SELECCIONAR EL ID DE FACTURAS_PEDIDOS
                sSql = "";
                sSql += "Select id_facturas_pedidos, Id_Pedido" + Environment.NewLine;
                sSql += "From cv403_facturas_pedidos" + Environment.NewLine;
                sSql += "Where id_factura = " + iIdFactura_F + Environment.NewLine; //VERIFICAR DE DONDE SE OBTIENE ESE ID
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdFacturaPedido_F = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iIdPedido_F = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                    }

                    else
                    {
                        goto reversa;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO 'E' EN CV403_CAB_PEDIDOS
                sSql = "";
                sSql += "Update cv403_cab_pedidos Set" + Environment.NewLine;
                sSql += "Comentarios = ''," + Environment.NewLine;
                sSql += "cg_estado_Pedido = 6969," + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine; ;
                sSql += "Where Id_Pedido = " + iIdPedido_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONAR EL ID_DESPACHO_PEDIDO E ID_DESPACHO DE DE CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "Select Id_Despacho_Pedido, Id_Despacho" + Environment.NewLine;
                sSql += "From cv403_despachos_pedidos" + Environment.NewLine;
                sSql += "Where Id_Pedido = " + iIdPedido_F + Environment.NewLine;
                sSql += "And estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdDespachoPedido_F = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iIdDespacho_F = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                    }

                    else
                    {
                        goto reversa;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "Update cv403_despachos_pedidos Set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "Where Id_Pedido = " + iIdPedido_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_CANTIDADES_DESPACHADAS
                sSql = "";
                sSql += "Update cv403_cantidades_despachadas Set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "Where Id_Despacho_Pedido = " + iIdDespachoPedido_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_CAB_DESPACHOS
                sSql = "";
                sSql += "Update cv403_cab_despachos Set" + Environment.NewLine;
                sSql += "Comentarios = ''," + Environment.NewLine;
                sSql += "cg_estado_Despacho = 6971," + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "Where Id_Despacho = " + iIdDespacho_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_FACTURAS_PEDIDOS
                sSql = "";
                sSql += "Update cv403_facturas_pedidos Set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "Where Id_Pedido = " + iIdPedido_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_CANTIDADES_FACTURADAS
                sSql = "";
                sSql += "Update cv403_cantidades_facturadas Set " + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "Where id_facturas_pedidos = " + iIdFacturaPedido_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //Select CM.Id_Movimiento_Bodega From cv402_cabecera_movimientos CM Where CM.Id_Pedido = 345  And CM.estado = 'A'
                
                //ACTUALIZA A ESTADO EN CV403_DET_PEDIDOS
                sSql = "";
                sSql += "Update cv403_det_pedidos Set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "Where Id_Pedido = " + iIdPedido_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_FACTURAS
                sSql = "";
                sSql += "Update cv403_facturas Set" + Environment.NewLine;
                sSql += "Comentarios = ''," + Environment.NewLine;
                sSql += "cg_estado_Factura = 7476," + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "Where id_factura = " + iIdFactura_F; 

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR EL MOTIVO DE ANULACIÓN DE LA FACTURA
                sSql = "";
                sSql += "insert into pos_anulacion_factura (" + Environment.NewLine;
                sSql += "id_factura, motivo_anulacion, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdFactura_F + ", '" + sMotivoAnulacion + "', 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SELECCIONAR EL ID_DOCUMENTO_COBRAR E ID_EVENTO_COBRO DE CV403_DESPACHOS_PEDIDOS
                sSql = "";
                sSql += "Select id_documento_cobrar, id_evento_cobro" + Environment.NewLine;
                sSql += "From cv403_dctos_por_cobrar" + Environment.NewLine;
                sSql += "Where id_factura = " + iIdFactura_F + Environment.NewLine; //VERIFICAR ID DE LA FACTURA
                sSql += "And cg_tipo_documento = 2725" + Environment.NewLine;
                sSql += "And estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdDocumentoPorCobrar_F = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iIdEventoCobro_F = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                    }

                    else
                    {
                        goto reversa;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_DCTOS_POR_COBRAR
                sSql = "";
                sSql += "Update cv403_dctos_por_cobrar Set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "Where id_evento_cobro = " + iIdEventoCobro_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA A ESTADO EN CV403_EVENTOS_COBROS
                sSql = "";
                sSql += "Update cv403_eventos_cobros Set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "Where id_evento_cobro = " + iIdEventoCobro_F;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                return false;
            }
        }

        #endregion

        private void frmEditarFactura_Load(object sender, EventArgs e)
        {            
            consultarDatosMail();
            cargarDatosFactura();
            datosFactura();

            this.ActiveControl = txtIdentificacion;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtMail.Clear();
            txtIdentificacion.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((txtIdentificacion.Text == "") && (txtApellidos.Text == ""))
            {
                ok.LblMensaje.Text = "Favor ingrese los datos del cliente para la factura.";
                ok.ShowDialog();
                return;
            }

            if (Program.iFacturacionElectronica == 1)
            {
                if (txtMail.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Debe ingresar un correo electrónico para enviar el comprobante electrónico.";
                    ok.ShowDialog();
                    btnCorreoElectronicoDefault.Focus();
                    return;
                }
            }

            MotivosCancelacion.frmMotivoAnulacionFactura anulacion = new MotivosCancelacion.frmMotivoAnulacionFactura();
            anulacion.lblEtiqueta.Text = "Favor ingrese el motivo de edición de la factura.";
            anulacion.ShowDialog();

            if (anulacion.DialogResult == DialogResult.OK)
            {
                sMotivoAnulacion = anulacion.sMotivoAnulacion;
                anulacion.Close();
                grabarRegistro();
            }
        }

        private void btnEditarFactura_Click(object sender, EventArgs e)
        {
            if (TxtNumeroFactura.ReadOnly == true)
            {
                sNumeroFactura = TxtNumeroFactura.Text.Trim();
                TxtNumeroFactura.ReadOnly = false;
                TxtNumeroFactura.Focus();
            }

            else
            {
                TxtNumeroFactura.Text = sNumeroFactura;
                TxtNumeroFactura.ReadOnly = true;
                txtIdentificacion.Focus();
            }
        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            Pedidos.frmVerFacturaTextBox factura = new Pedidos.frmVerFacturaTextBox(iIdPedido.ToString(), 0);
            factura.ShowInTaskbar = false;
            factura.ShowDialog();
        }

        private void frmEditarFactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnEditar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), chkPasaporte.Checked);
            nuevoCliente.ShowDialog();

            if (nuevoCliente.DialogResult == DialogResult.OK)
            {
                iIdPersona = nuevoCliente.iCodigo;
                txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                consultarRegistro();
            }
        }

        private void btnConsumidorFinal_Click(object sender, EventArgs e)
        {
            txtIdentificacion.Text = "9999999999999";
            txtApellidos.Text = "CONSUMIDOR FINAL";
            txtNombres.Text = "CONSUMIDOR FINAL";
            txtTelefono.Text = "9999999999";
            txtMail.Text = "dominio@dominio.com";
            txtDireccion.Text = "QUITO";
            iIdPersona = Program.iIdPersona;
            idTipoIdentificacion = 180;
            idTipoPersona = 2447;
            btnGuardar.Enabled = true;
            btnEditar.Visible = false;
            btnGuardar.Focus();
        }

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtIdentificacion.Text != "")
                {
                    //AQUI INSTRUCCIONES PARA CONSULTAR Y VALIDAR LA CEDULA
                    if ((esNumero(txtIdentificacion.Text.Trim()) == true) && (chkPasaporte.Checked == false))
                    {
                        //INSTRUCCIONES PARA VALIDAR
                        validarIdentificacion();
                    }
                    else
                    {
                        //CONSULTAR EN LA BASE DE DATOS
                        consultarRegistro();
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmControlDatosCliente controlClientes = new frmControlDatosCliente();
            controlClientes.ShowDialog();

            if (controlClientes.DialogResult == DialogResult.OK)
            {
                iIdPersona = controlClientes.iCodigo;
                txtIdentificacion.Text = controlClientes.sIdentificacion;
                consultarRegistro();
                btnGuardar.Focus();
                controlClientes.Close();
            }
        }

        private void btnCorreoElectronicoDefault_Click(object sender, EventArgs e)
        {
            txtMail.Text = Program.sCorreoElectronicoDefault;
        }
    }
}
