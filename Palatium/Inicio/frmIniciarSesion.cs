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
using AxZKFPEngXControl;

namespace Palatium.Inicio
{
    public partial class frmIniciarSesion : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeCaja mensajeCaja;

        string sSql;
        string sFechaCorta;
        string sHora;
        string sEstadoCaja;
        string sNombreCajero;
        string sClaveAcceso;

        DataTable dtConsulta;

        bool bRespuesta;
        bool Check;

        int fpcHandle;
        int IBanderaCaja;

        DateTime fechaSistema;
        DateTime fechaCaja;

        private AxZKFPEngX lectorHuellas = new AxZKFPEngX();

        SqlParameter[] parametro;

        public frmIniciarSesion()
        {
            InitializeComponent();
        }

        #region FUNCIONES PARA TRABAJAR CON EL LECTOR DE HUELLAS

        //FUNCION PARA LLENAR EL GRID
        private bool llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select descripcion, claveacceso, huella_dactilar" + Environment.NewLine;
                sSql += "from pos_cajero" + Environment.NewLine;
                sSql += "where is_active = @is_active" + Environment.NewLine;
                sSql += "and is_active_huella = @is_active_huella" + Environment.NewLine;
                sSql += "and huella_dactilar <> @huella_dactilar" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[4];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 1;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@is_active_huella";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 1;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@huella_dactilar";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@estado";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "A";

                #endregion

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

                dgvDatos.DataSource = dtConsulta;
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

        //FUNCION PARA INICIALIZAR EL DISPOSITIVO
        private void iniciarDispositivoReconocer()
        {
            try
            {
                Controls.Add(lectorHuellas);

                if (lectorHuellas.InitEngine() == 0)
                {
                    lectorHuellas.FPEngineVersion = "9";
                    lectorHuellas.EnrollCount = 3;
                    lblMensajeRespuesta.Text = "Dispositivo: " + lectorHuellas.SensorSN;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void lectorHuellas_OnCapture(object sender, IZKFPEngXEvents_OnCaptureEvent e)
        {
            string template = lectorHuellas.EncodeTemplate1(e.aTemplate);
            string regTemplateString = "";
            int iBandera = 0;

            foreach (DataGridViewRow row in dgvDatos.Rows)
            {
                sNombreCajero = row.Cells["descripcion"].Value.ToString().Trim();
                sClaveAcceso = row.Cells["claveacceso"].Value.ToString().Trim();
                regTemplateString = row.Cells["huella_dactilar"].Value.ToString();

                if (lectorHuellas.VerFingerFromStr(ref template, regTemplateString, false, ref Check))
                {
                    iBandera = 1;
                    break;
                }
            }

            if (iBandera == 1)
            {
                lblMensajeRespuesta.Text = sNombreCajero;
                txtCodigo.Text = sClaveAcceso;
                consultarRegistro();
            }

            else
            {
                lblMensajeRespuesta.Text = "Registro no encontrado";
            }
        }

        #endregion

        #region FUNCIONES DE CONTROL DE BOTONES

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        #region FUNCIONES NECESARIAS PARA EL USUARIO

        //Función para llenar el Combo de Localidad
        private void llenarComboLocalidad()
        {
            try
            {
                dtConsulta = new DataTable();

                sSql = "select id_localidad, nombre_localidad from tp_vw_localidades";

                cmbLocalidad.llenar(dtConsulta, sSql);
                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                txtCodigo.Text = txtCodigo.Text + sValor;
                txtCodigo.Focus();
                txtCodigo.SelectionStart = txtCodigo.Text.Trim().Length;
            }

            catch (Exception)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowDialog();
            }
        }

        #endregion

        #region FUNCIONES DE CONSULTA A LA BASE DE DATOS

        //FUNCION PARA CONSULTAR LOS DATOS
        private void consultarRegistro()
        {
            try
            {
                if (txtCodigo.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el código de usuario.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFechaCorta = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");
                sHora = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("HH:mm:ss");

                //AQUI CONSULTAMOS LOS DATOS DEL CAJERO EN LA BASE DE DATOS
                sSql = "";
                sSql += "select VU.*, isnull(TP.correo_electronico, '') correo_electronico" + Environment.NewLine;
                sSql += "from pos_vw_usuario VU, tp_personas TP" + Environment.NewLine;
                sSql += "where VU.id_persona = TP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and VU.claveacceso = '" + txtCodigo.Text.Trim() + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[0]["is_active"].ToString()) == 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "El usuario no se encuentra activo.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                        return;
                    }

                    if (dtConsulta.Rows[0]["id_pos_cajero"].ToString() != "0")
                    {
                        Program.iPuedeCobrar = 1;

                        Program.CAJERO_ID = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cajero"].ToString());
                        Program.sNombreCajero = dtConsulta.Rows[0]["descripcion"].ToString();
                        Program.sNombreUsuario = dtConsulta.Rows[0]["descripcion"].ToString();
                        Program.iIdPersonaMovimiento = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                        Program.sEstadoUsuario = dtConsulta.Rows[0]["estado"].ToString();
                        Program.sCorreoElectronico = dtConsulta.Rows[0]["correo_electronico"].ToString();

                        Program.sDatosMaximo[0] = dtConsulta.Rows[0]["descripcion"].ToString();
                        Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                        Program.sDatosMaximo[2] = dtConsulta.Rows[0]["estado"].ToString();

                        IBanderaCaja = 1;
                    }

                    else if (dtConsulta.Rows[0]["id_pos_mesero"].ToString() != "0")
                    {
                        Program.iPuedeCobrar = 0;

                        Program.CAJERO_ID = Program.iIdCajeroDefault;
                        Program.sNombreCajero = Program.sNombreCajeroDefault;

                        Program.iIdMesero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_mesero"].ToString());
                        Program.nombreMesero = dtConsulta.Rows[0]["descripcion"].ToString();
                        Program.sNombreUsuario = dtConsulta.Rows[0]["descripcion"].ToString();
                        Program.sEstadoUsuario = dtConsulta.Rows[0]["estado"].ToString();
                        Program.iIdPersonaMovimiento = Convert.ToInt32(dtConsulta.Rows[0]["id_persona"].ToString());
                        Program.sCorreoElectronico = dtConsulta.Rows[0]["correo_electronico"].ToString();

                        Program.sDatosMaximo[0] = dtConsulta.Rows[0]["descripcion"].ToString();
                        Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                        Program.sDatosMaximo[2] = dtConsulta.Rows[0]["estado"].ToString();

                        IBanderaCaja = 0;
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No existe información con los datos ingresados.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                //AQUI SE INTEGRA NUEVA FUNCIONALIDAD
                //  FECHA: 2019-10-23
                //  AUTOR: ELVIS GUAIGUA
                //---------------------------------------------------------------------------------------

                sSql = "";
                sSql += "select top 1 CC.id_jornada, CC.id_cajero, CC.estado_cierre_cajero, J.orden, CC.fecha_apertura, CC.id_pos_cierre_cajero" + Environment.NewLine;
                sSql += "from pos_cierre_cajero CC INNER JOIN" + Environment.NewLine;
                sSql += "pos_jornada J ON  J.id_pos_jornada = CC.id_jornada" + Environment.NewLine;
                sSql += "and J.estado = 'A'" + Environment.NewLine;
                sSql += "and CC.estado = 'A'" + Environment.NewLine;
                sSql += "where CC.id_localidad = " + cmbLocalidad.SelectedValue + Environment.NewLine;
                sSql += "order by CC.id_pos_cierre_cajero desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                //SI EXISTE UN REGISTRO EN LA CONSULTA
                if (dtConsulta.Rows.Count > 0)
                {
                    sEstadoCaja = dtConsulta.Rows[0]["estado_cierre_cajero"].ToString().Trim().ToUpper();
                    fechaCaja = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString());
                    fechaSistema = Convert.ToDateTime(sFechaCorta);

                    int iIdJornadaConsulta = Convert.ToInt32(dtConsulta.Rows[0]["id_jornada"].ToString());
                    int iIdCierreCajeroConsulta = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cierre_cajero"].ToString());

                    if (sEstadoCaja == "CERRADA")
                    {
                        int iConsultaJornada;

                        if (fechaCaja == fechaSistema)
                        {
                            iConsultaJornada = recuperarJornada(Convert.ToInt32(dtConsulta.Rows[0]["orden"].ToString()) + 1);
                        }

                        else
                        {
                            iConsultaJornada = recuperarJornada(1);
                        }

                        //int iConsultaJornada = recuperarJornada(Convert.ToInt32(dtConsulta.Rows[0]["orden"].ToString()) + 1);
                        //int iConsultaJornada = recuperarJornada(1);

                        if (iConsultaJornada > 0)
                        {
                            if (insertarCierreCajero(iConsultaJornada) == false)
                            {
                                return;
                            }

                            fechaCaja = fechaSistema;

                            if (recuperarCierre() == false)
                            {
                                return;
                            }
                        }

                        else
                        {
                            return;
                        }
                    }

                    else
                    {
                        if (fechaCaja == fechaSistema)
                        {
                            if (recuperarCierre() == false)
                            {
                                return;
                            }
                        }

                        else
                        {
                            //this.Hide();

                            mensajeCaja = new VentanasMensajes.frmMensajeCaja();
                            mensajeCaja.lblMensaje.Text = "La caja se encuentra aperturada con fecha:" +
                                                   Environment.NewLine + fechaCaja.ToString("dd-MMMM-yyyy") + Environment.NewLine +
                                                   "¿Desea continuar trabajando con la misma caja?";

                            mensajeCaja.ShowDialog();

                            if (mensajeCaja.DialogResult == DialogResult.OK)
                            {
                                if (recuperarCierre() == false)
                                {
                                    return;
                                }
                            }

                            else if (mensajeCaja.DialogResult == DialogResult.No)
                            {
                                if (cerrarCajaVigente(iIdCierreCajeroConsulta, iIdJornadaConsulta) == false)
                                {
                                    return;
                                }

                                int iConsultaJornada = recuperarJornada(1);

                                if (iConsultaJornada > 0)
                                {
                                    if (insertarCierreCajero(iConsultaJornada) == false)
                                    {
                                        return;
                                    }

                                    fechaCaja = fechaSistema;

                                    if (recuperarCierre() == false)
                                    {
                                        return;
                                    }
                                }

                                else
                                {
                                    return;
                                }
                            }

                            else if (mensajeCaja.DialogResult == DialogResult.Cancel)
                            {
                                this.Close();
                                return;
                            }

                            //SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                            //SiNo.lblMensaje.Text = "La caja se encuentra aperturada con fecha:" +
                            //                       Environment.NewLine + fechaCaja.ToString("dd-MMMM-yyyy") + Environment.NewLine +
                            //                       "¿Desea continuar trabajando con la misma caja?";
                            //SiNo.ShowDialog();

                            //if (SiNo.DialogResult == DialogResult.OK)
                            //{
                            //    if (recuperarCierre() == false)
                            //    {
                            //        return;
                            //    }
                            //}

                            //else if (SiNo.DialogResult == DialogResult.No)
                            //{
                            //    if (cerrarCajaVigente(iIdCierreCajeroConsulta, iIdJornadaConsulta) == false)
                            //    {
                            //        return;
                            //    }

                            //    int iConsultaJornada = recuperarJornada(1);

                            //    if (iConsultaJornada > 0)
                            //    {
                            //        if (insertarCierreCajero(iConsultaJornada) == false)
                            //        {
                            //            return;
                            //        }

                            //        if (recuperarCierre() == false)
                            //        {
                            //            return;
                            //        }
                            //    }

                            //    else
                            //    {
                            //        return;
                            //    }
                            //}
                        }
                    }
                }

                //EN CASO DE NO EXISTIR UN REGISTRO, SE PROCEDE A INSERTAR EN LA BASE DE DATOS
                else
                {
                    if (IBanderaCaja == 1)
                    {
                        int iConsultaJornada = recuperarJornada(1);

                        if (iConsultaJornada > 0)
                        {
                            if (insertarCierreCajero(iConsultaJornada) == false)
                            {
                                return;
                            }

                            if (recuperarCierre() == false)
                            {
                                return;
                            }
                        }

                        else
                        {
                            return;
                        }
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Favor solicite que se haga la apertura de caja.";
                        ok.ShowDialog();
                        return;
                    }
                }

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Bienvenido (a)\n\n" + Program.sNombreUsuario;
                ok.ShowDialog();
                Program.iVerCaja = 1;
                Program.sFechaOrden = txtFecha.Text;
                this.DialogResult = DialogResult.OK;
                Program.horaEntrada = DateTime.Now.ToLongTimeString();
                Program.iBanderaGrupoCierreCaja = 1;
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RECUPERAR LA PRIMERA JORNADA
        private int recuperarJornada(int iOrden_P)
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_jornada" + Environment.NewLine;
                sSql += "from pos_jornada" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and orden = " + iOrden_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran configuradas las jornadas. Favor comuníquese con el administrador.";
                    ok.ShowDialog();
                    return 0;
                }

                else
                { 
                    return Convert.ToInt32(dtConsulta.Rows[0]["id_pos_jornada"].ToString());
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR EL REGISTRO DE CIERRE DE CAJA
        private bool insertarCierreCajero(int iJornada_P)
        {
            try
            {              

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "insert into pos_cierre_cajero (" + Environment.NewLine;
                sSql += "id_localidad, id_jornada, id_cajero, fecha_apertura," + Environment.NewLine;
                sSql += "hora_apertura, estado_cierre_cajero, porcentaje_iva, porcentaje_servicio," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdLocalidad + ", " + iJornada_P + "," + Environment.NewLine;
                sSql += Program.CAJERO_ID + ", '" + sFechaCorta + "', '" + sHora + "', 'Abierta'," + Environment.NewLine;
                sSql += (Program.iva * 100) + ", " + (Program.servicio * 100) + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA RECUPERAR LA INFORMACION DEL CIERRE
        private bool recuperarCierre()
        {
            try
            {
                sSql = "";
                sSql += "select top 1 id_pos_cierre_cajero, id_jornada, id_cajero, fecha_apertura, estado_cierre_cajero" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where fecha_apertura = '" + Convert.ToDateTime(fechaCaja).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidad.SelectedValue + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and estado_cierre_cajero = 'Abierta'" + Environment.NewLine;
                sSql += "order by id_pos_cierre_cajero desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    Program.iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_cierre_cajero"].ToString());
                    Program.iJornadaCajero = Convert.ToInt32(dtConsulta.Rows[0]["id_jornada"].ToString());
                    Program.sFechaAperturaCajero = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_apertura"].ToString()).ToString("yyyy/MM/dd");
                    Program.sEstadoCajero = dtConsulta.Rows[0]["estado_cierre_cajero"].ToString();
                    Program.iJORNADA = Convert.ToInt32(dtConsulta.Rows[0]["id_jornada"].ToString());
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al recuperar los datos de apertura de caja. Comuníquese con el administrador del sistema.";
                    ok.ShowDialog();
                    return false;
                }

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

        //FUNCION PARA CERRAR LA CAJA ANTERIOR
        private bool cerrarCajaVigente(int iIdPosCierreCajero_R, int iIdPosJornada_R)
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                string sFecha_R = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");
                string sHora_R = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("HH:mm:ss");

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "update pos_cierre_cajero set" + Environment.NewLine;
                sSql += "fecha_cierre = '" + sFecha_R + "'," + Environment.NewLine;
                sSql += "hora_cierre = '" + sHora_R + "'," + Environment.NewLine;
                sSql += "ahorro_emergencia = 0," + Environment.NewLine;
                sSql += "caja_final = 0," + Environment.NewLine;
                sSql += "estado_cierre_cajero = 'Cerrada'" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero_R;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //INSERTAR LOS VALORES DE LAS MONEDAS
                sSql = "";
                sSql += "insert into pos_monedas (" + Environment.NewLine;
                sSql += "id_pos_cierre_cajero, id_localidad, moneda01, moneda05, moneda10," + Environment.NewLine;
                sSql += "moneda25, moneda50, billete1, billete2, billete5, billete10," + Environment.NewLine;
                sSql += "billete20, billete50, billete100, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, tipo_ingreso, id_pos_jornada)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdPosCierreCajero_R + ", " + Program.iIdLocalidad + ", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 1, " + iIdPosJornada_R + ")";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                //CERRAR LOS LOTES DE LAS TARJETAS
                sSql = "";
                sSql += "update pos_numero_lote set" + Environment.NewLine;
                sSql += "fecha_cierre = '" + sFecha_R + "'," + Environment.NewLine;
                sSql += "estado_lote = 'Cerrada'" + Environment.NewLine;
                sSql += "where estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        #endregion

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            consultarRegistro();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //(this.Owner as Form1).desactivarBotones();
            this.Close();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            concatenarValores(btn1.Text);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            concatenarValores(btn2.Text);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            concatenarValores(btn3.Text);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            concatenarValores(btn4.Text);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            concatenarValores(btn5.Text);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            concatenarValores(btn6.Text);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            concatenarValores(btn7.Text);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            concatenarValores(btn8.Text);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            concatenarValores(btn9.Text);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            string str;
            int loc;

            if (txtCodigo.Text.Length > 0)
            {

                str = txtCodigo.Text.Substring(txtCodigo.Text.Length - 1);
                loc = txtCodigo.Text.Length;
                txtCodigo.Text = txtCodigo.Text.Remove(loc - 1, 1);
            }

            txtCodigo.Focus();
            txtCodigo.SelectionStart = txtCodigo.Text.Trim().Length;
        }

        private void btn0_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn0);
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn1);
        }

        private void btn2_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn2);
        }

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn3);
        }

        private void btn4_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn4);
        }

        private void btn5_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn5);
        }

        private void btn6_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn6);
        }

        private void btn7_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn7);
        }

        private void btn8_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn8);
        }

        private void btn9_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btn9);
        }

        private void btnRetroceder_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnRetroceder);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnIngresar);
        }

        private void btn0_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn0);
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn1);
        }

        private void btn2_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn2);
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn3);
        }

        private void btn4_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn4);
        }

        private void btn5_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn5);
        }

        private void btn6_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn6);
        }

        private void btn7_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn7);
        }

        private void btn8_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn8);
        }

        private void btn9_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btn9);
        }

        private void btnRetroceder_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnRetroceder);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnIngresar);
        }

        private void frmIniciarSesion_Load(object sender, EventArgs e)
        {
            txtFecha.Text = Program.sFechaSistema.ToString("dd-MM-yyyy");
            llenarComboLocalidad();

            if (Program.iUsarHuellasCajeros == 1)
            {                
                if (llenarGrid() == false)
                    return;

                this.Size = new Size(333, 536);
                lblMensajeRespuesta.Visible = true;

                iniciarDispositivoReconocer();
                fpcHandle = lectorHuellas.CreateFPCacheDB();

                string regTemplateString = "";
                int FpId = 0;

                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    try
                    {
                        regTemplateString = row.Cells["huella_dactilar"].Value.ToString();

                        lectorHuellas.AddRegTemplateStrToFPCacheDB(fpcHandle, FpId, regTemplateString);

                        FpId = FpId + 1;
                    }
                    catch { }
                }

                lectorHuellas.OnCapture += lectorHuellas_OnCapture;
                lectorHuellas.BeginCapture();
            }

            else
            {
                this.Size = new Size(333, 515);
                lblMensajeRespuesta.Visible = false;
            }
        }

        private void frmIniciarSesion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
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
                if (txtCodigo.Text.Trim() == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la clave para proceder con la consulta.";
                    ok.ShowDialog();
                }

                else
                {
                    consultarRegistro();
                }
            }
        }

        private void frmIniciarSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (Program.iUsarHuellasCajeros == 1)
                {
                    lectorHuellas.OnCapture -= lectorHuellas_OnCapture;
                    lectorHuellas.CancelEnroll();
                    lectorHuellas.EndEngine();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
