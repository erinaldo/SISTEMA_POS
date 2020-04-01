using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Palatium
{
    public partial class CodigoCajero : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        

        string sNombreJornada;

        DataTable dtConsulta;
        string sSql = "";
        string sFecha;
        string sHora;
        string sFechaCorta;
        bool bRespuesta = false;

        int iHora;
        int iMinuto;

        //public int iVerCaja = 0;

        //HORAS DE APERTURA Y CIERRE DE CADA CAJERO
        //=================================================================
        string sHoraDiurnaAbrir = "08:00:00";
        string sHoraDiurnaCerrar = "17:59:59";

        string sHoraNocturnaAbrir = "18:00:00";
        string sHoraNocturnaCerrar = "23:59:59";

        string sHoraIngreso;

        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        //=================================================================

        //VARIABLES PARA RECUPERAR INFORMACION DE BASE DE DATOS PARA COMPARACION CON EL INGRESO
        int iIdPosCierreOrdenBDD;
        int iJornadaBDD;
        int iCajeroBDD;
        int iLocalidadBDD;
        string sFechaBDD;
        string sEstadoBDD;

        public CodigoCajero()
        {
            InitializeComponent();            
        }

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

        //FUNCION PARA CONCATENAR
        private void concatenarValores(string sValor)
        {
            try
            {
                textBoxcodigo.Text = textBoxcodigo.Text + sValor;
                textBoxcodigo.Focus();
                textBoxcodigo.SelectionStart = textBoxcodigo.Text.Trim().Length;
            }

            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }


        //ACTUALIZA EL REGISTRO A CERRADA
        private void actualizarUltimoRegistro(int iId)
        {
            try
            {
                sFecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "update pos_cierre_cajero set" + Environment.NewLine;
                sSql += "fecha_cierre = '" + sFecha.Substring(0, 10) + "'," + Environment.NewLine;
                sSql += "hora_cierre = '" + sFecha.Substring(11, 8) +"'," + Environment.NewLine;
                sSql += "estado_cierre_cajero = 'Cerrada' " + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iId;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin:
            { }
        }

        //FUNCION PARA CERRAR LA ULTIMA CAJA ABIERTA
        private void cerrarUltimaCaja()
        {
            try
            {
                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                sSql = "";
                sSql = sSql + "select top 1 id_pos_cierre_cajero, fecha_apertura, estado_cierre_cajero" + Environment.NewLine;
                sSql = sSql + "from pos_cierre_cajero" + Environment.NewLine;
                sSql = sSql + "where fecha_apertura <> '" + sFecha + "'" + Environment.NewLine;
                sSql = sSql + "and id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql = sSql + "and id_jornada = " + Program.iJORNADA + Environment.NewLine;
                sSql = sSql + "order by id_pos_cierre_cajero desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (dtConsulta.Rows[0][2].ToString() == "Abierta")
                        {
                            //ACTUALIZA EL REGISTRO DE CAJA A CERRADA
                            actualizarUltimoRegistro(Convert.ToInt32(dtConsulta.Rows[0][0].ToString()));
                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        fin:
            { }
        }

        //FUNCION PARA EXTRAER LOS DATOS DEL CAJERO
        private bool extraerCajero()
        {
            try
            {
                sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");

                sSql = "";
                sSql = sSql + "select CC.id_cajero, C.descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_cajero C, pos_cierre_cajero CC" + Environment.NewLine;
                sSql = sSql + "where CC.id_cajero = C.id_pos_cajero" + Environment.NewLine;
                sSql = sSql + "and C.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and CC.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and CC.estado_cierre_cajero = 'Abierta'" + Environment.NewLine;
                sSql = sSql + "and CC.fecha_apertura = '" + sFechaCorta + "'" + Environment.NewLine;

                DataTable dtVer = new DataTable();
                dtVer.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtVer, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.CAJERO_ID = Convert.ToInt32(dtVer.Rows[0][0].ToString());
                        Program.sNombreCajero = dtVer.Rows[0][1].ToString();
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA CONSULTAR LOS DATOS
        private void consultarRegistro()
        {
            try
            {
                if (textBoxcodigo.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el código de usuario.";
                    ok.ShowDialog();
                    textBoxcodigo.Clear();
                    textBoxcodigo.Focus();
                    goto fin;
                }

                else if (jornadaCajero() == false)
                {
                    ok.LblMensaje.Text = "Por favor, seleccione una jornada para ingresar al sistema.";
                    ok.ShowDialog();
                    textBoxcodigo.Clear();
                    textBoxcodigo.Focus();
                    goto fin;
                }

                else
                {
                    string sFechaAuxiliar = Program.sFechaSistema.ToString("yyyy/MM/dd");

                    //AQUI CONSULTAMOS LOS DATOS DEL CAJERO EN LA BASE DE DATOS

                    //sSql = "select id_pos_cajero, descripcion, estado, id_persona from pos_cajero where claveacceso = " +
                    //       Convert.ToInt32(textBoxcodigo.Text) + " and estado = 'A'";

                    //sSql = "";
                    //sSql = sSql + "select * from pos_vw_usuario" + Environment.NewLine;
                    //sSql = sSql + "where claveacceso = '" + textBoxcodigo.Text.Trim() + "'" + Environment.NewLine;
                    //sSql = sSql + "and estado = 'A'";

                    sSql = "";
                    sSql += "select VU.*, isnull(TP.correo_electronico, '') correo_electronico" + Environment.NewLine;
                    sSql += "from pos_vw_usuario VU, tp_personas TP" + Environment.NewLine;
                    sSql += "where VU.id_persona = TP.id_persona" + Environment.NewLine;
                    sSql += "and TP.estado = 'A'" + Environment.NewLine;
                    sSql += "and VU.claveacceso = '" + textBoxcodigo.Text.Trim() + "'";


                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            //AQUI NUEVO CAMBIO
                            if (dtConsulta.Rows[0][0].ToString() != "0")
                            {
                                Program.iPuedeCobrar = 1;

                                Program.CAJERO_ID = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                                Program.sNombreCajero = dtConsulta.Rows[0][2].ToString();
                                Program.sNombreUsuario = dtConsulta.Rows[0][2].ToString();
                                Program.iIdPersonaMovimiento = Convert.ToInt32(dtConsulta.Rows[0][4].ToString());
                                Program.sEstadoUsuario = dtConsulta.Rows[0][5].ToString();
                                Program.sCorreoElectronico = dtConsulta.Rows[0][6].ToString();
                                

                                Program.sDatosMaximo[0] = dtConsulta.Rows[0][2].ToString();
                                Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                                Program.sDatosMaximo[2] = dtConsulta.Rows[0][5].ToString();
                            }

                            else if (dtConsulta.Rows[0][1].ToString() != "0")
                            {
                                Program.iPuedeCobrar = 0;

                                Program.iIdMesero = Convert.ToInt32(dtConsulta.Rows[0][1].ToString());
                                Program.nombreMesero = dtConsulta.Rows[0][2].ToString();

                                //Program.CAJERO_ID = Program.iIdCajeroDefault;
                                //Program.sNombreCajero = Program.sNombreCajeroDefault;

                                if (extraerCajero() == false)
                                {
                                    ok.LblMensaje.Text = "Ocurrió un problema al consultar los datos del cajero.";
                                    ok.ShowDialog();
                                    textBoxcodigo.Clear();
                                    textBoxcodigo.Focus();
                                    goto fin;
                                }

                                Program.sNombreUsuario = dtConsulta.Rows[0][2].ToString();
                                Program.sEstadoUsuario = dtConsulta.Rows[0][5].ToString();
                                Program.iIdPersonaMovimiento = Convert.ToInt32(dtConsulta.Rows[0][4].ToString());
                                Program.sCorreoElectronico = dtConsulta.Rows[0][6].ToString();

                                Program.sDatosMaximo[0] = dtConsulta.Rows[0][2].ToString();
                                Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                                Program.sDatosMaximo[2] = dtConsulta.Rows[0][5].ToString();
                            }

                        }

                        else
                        {
                            ok.LblMensaje.Text = "No existe un cajero registrado con el código ingresado. Favor revise su información.";
                            ok.ShowDialog();
                            textBoxcodigo.Clear();
                            textBoxcodigo.Focus();
                            goto fin;
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                        ok.ShowDialog();
                        textBoxcodigo.Clear();
                        textBoxcodigo.Focus();
                        goto fin;
                    }

                    //EN ESTA SECCION SE PROCEDE A VERIFICAR LOS DATOS CON EL CIERRE DE CAJERO, YA SEA VIGENTE O CERRADO
                    //==================================================================================================
                    //sSql = "select top 1 * from pos_cierre_cajero order by id_pos_cierre_cajero desc";
                    sSql = "";
                    sSql += "select * from pos_cierre_cajero" + Environment.NewLine;
                    sSql += "where fecha_apertura = '" + txtFecha.Text.Trim() + "'" + Environment.NewLine;
                    sSql += "and id_jornada = " + Convert.ToInt32(cmbJornada.SelectedValue);
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            if (dtConsulta.Rows[0][8].ToString() == "Cerrada")
                            {
                                if ((dtConsulta.Rows[0][2].ToString() == cmbJornada.SelectedValue.ToString()) && (dtConsulta.Rows[0][4].ToString().Substring(0, 10) == Convert.ToDateTime(txtFecha.Text).ToString("dd/MM/yyyy")))
                                {
                                    ok.LblMensaje.Text = "Ya existe un cierre de caja realizado en la jornada seleccionada. Solo puede visualizar solo ciertas opciones.";
                                    Program.iVerCaja = 0;
                                    this.DialogResult = DialogResult.OK;
                                    ok.ShowDialog();
                                    textBoxcodigo.Clear();
                                    textBoxcodigo.Focus();
                                    goto fin;
                                }

                                else
                                {
                                    //CREAR JORNADA NUEVA
                                    if (insertarCierreCajero() == true)
                                    {
                                        goto mensaje;
                                    }

                                    else
                                    {
                                        ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                                        ok.ShowDialog();
                                        textBoxcodigo.Clear();
                                        textBoxcodigo.Focus();
                                        goto fin;
                                    }
                                }

                                //YA EXISTE UN CIERRE DE CAJA REALIZADO
                                ok.LblMensaje.Text = "Ya existe un cierre de caja realizado en la jornada seleccionada.";
                                ok.ShowDialog();
                                textBoxcodigo.Clear();
                                goto fin;
                            }


                            else if (dtConsulta.Rows[0][8].ToString() == "Abierta")
                            {
                                //MessageBox.Show(dtConsulta.Rows[0][2].ToString() + " ----- " + cmbJornada.SelectedValue.ToString());

                                if ((dtConsulta.Rows[0][3].ToString() != Program.CAJERO_ID.ToString()) || (dtConsulta.Rows[0][2].ToString() != cmbJornada.SelectedValue.ToString()))
                                //if ((dtConsulta.Rows[0][3].ToString() != Program.CAJERO_ID.ToString()) && (dtConsulta.Rows[0][4].ToString().Substring(0, 10) != Convert.ToDateTime(txtFecha.Text).ToString("dd/MM/yyyy")))
                                {
                                    ok.LblMensaje.Text = "Los datos ingresados no corresponde con el registro vigente para el cierre de cajero.";
                                    ok.ShowDialog();
                                    textBoxcodigo.Clear();
                                    textBoxcodigo.Focus();
                                    goto fin;
                                }

                                else
                                {
                                    //GUARDAR LOS DATOS PARA CIERRE DE CAJERO
                                    //Program.iIdPosCierreCajero = iIdPosCierreOrdenBDD;

                                    string sFechaAux;
                                    Program.iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                                    Program.iJornadaCajero = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                                    sFechaAux = dtConsulta.Rows[0][4].ToString();
                                    Program.sFechaAperturaCajero = Convert.ToDateTime(sFechaAux).ToString("yyyy/MM/dd");
                                    Program.sEstadoCajero = dtConsulta.Rows[0][8].ToString();

                                    goto mensaje;
                                    //enviar la continuacion de la consulta
                                }
                            }

                            else
                            {
                                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                                ok.ShowDialog();
                                textBoxcodigo.Clear();
                                textBoxcodigo.Focus();
                                goto fin;
                            }
                        }

                        else
                        {
                            //CREAR JORNADA NUEVA
                            insertarCierreCajero();
                            goto mensaje;
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                        ok.ShowDialog();
                        textBoxcodigo.Clear();
                        textBoxcodigo.Focus();
                        goto fin;
                    }
                }


            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                textBoxcodigo.Clear();
                textBoxcodigo.Focus();
                goto fin;
            }

        mensaje:
            {
                ok.LblMensaje.Text = "Bienvenido (a)\n\n" + Program.sNombreUsuario;
                ok.ShowDialog();
                Program.iVerCaja = 1;
                Program.sFechaOrden = txtFecha.Text;
                this.Close();
                cargarNumeroCuenta();
                this.DialogResult = DialogResult.OK;
                Program.horaEntrada = DateTime.Now.ToLongTimeString();
                goto fin;
            }

        fin:
            { }
        }

        //INSERTAR UN NUEVO REGISTRO
        private bool insertarCierreCajero()
        {
            try
            {
                sFecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sFechaCorta = Program.sFechaSistema.ToString("yyyy/MM/dd");
                sHora = sFecha.Substring(11, 8);

                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    return false;
                }
                //=======================================================================================================

                else
                {
                    sSql = "";
                    sSql += "insert into pos_cierre_cajero (" + Environment.NewLine;
                    sSql += "id_localidad, id_jornada, id_cajero, fecha_apertura," + Environment.NewLine;
                    sSql += "hora_apertura, estado_cierre_cajero, porcentaje_iva, porcentaje_servicio," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += Program.iIdLocalidad + ", " + Convert.ToInt32(cmbJornada.SelectedValue) + "," + Environment.NewLine;
                    sSql += Program.CAJERO_ID + ", '" + sFechaCorta + "', '" + sHora + "', 'Abierta'," + Environment.NewLine;
                    sSql += (Program.iva * 100) + ", " + (Program.servicio * 100) + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                        
                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        return false;
                    }
                    else
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                        sSql = "";
                        sSql += "select top 1 * from pos_cierre_cajero" + Environment.NewLine;
                        sSql += "order by id_pos_cierre_cajero desc";

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();

                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                        if (bRespuesta == true)
                        {
                            if (dtConsulta.Rows.Count > 0)
                            {
                                string sFechaAux;
                                Program.iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                                Program.iJornadaCajero = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                                sFechaAux = dtConsulta.Rows[0][4].ToString().Substring(0, 10);
                                Program.sFechaAperturaCajero = Convert.ToDateTime(sFechaAux).ToString("yyyy/MM/dd");
                                Program.sEstadoCajero = dtConsulta.Rows[0][8].ToString();
                                return true;
                            }

                            else
                            {
                                ok.LblMensaje.Text = "Ocurrió un problema al guardar al crear el registro para cierre de cajero.";
                                ok.ShowDialog();
                                return false;                         
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Ocurrió un problema al guardar al crear el registro para cierre de cajero.";
                            ok.ShowDialog();
                            return false;
                        }
                    }
                }
            }

            catch (Exception)
            {
                return false;
            }
        }
        
        //FUNCION PARA SABER LA JORNADA DEL CAJERO

        private bool LocalidadCajero()
        {
            try
            {
                int iHora = Convert.ToInt32(DateTime.Now.Hour);

                string sNombreLocalidad = cmbLocalidad.Text;

                //BUSCAMOS EL ID DE LA JORNADA
                sSql = "";
                sSql += "select id_localidad from tp_vw_localidades" + Environment.NewLine;
                sSql += "where nombre_localidad = '" + sNombreLocalidad + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool jornadaCajero()
        {
            try
            {
                int iHora = Convert.ToInt32(DateTime.Now.Hour);

                sNombreJornada = cmbJornada.Text;
                
                //BUSCAMOS EL ID DE LA JORNADA
                sSql = "";
                sSql += "select id_pos_jornada from pos_jornada" + Environment.NewLine;
                sSql += "where descripcion = '" + sNombreJornada + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        //Program.iJORNADA = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        Program.iJORNADA = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //FUNCION PARA SABER EN QUE JORNADA DEBE CARGARSE EL COMBO DE JORNADA
        private void verJornada()
        {
            if (Program.iManejaJornada == 1)
            {
                sHoraIngreso = DateTime.Now.ToString("HH:mm:ss");

                if ((Convert.ToDateTime(sHoraIngreso) >= Convert.ToDateTime(sHoraDiurnaAbrir)) && (Convert.ToDateTime(sHoraIngreso) <= Convert.ToDateTime(sHoraDiurnaCerrar)))
                {
                    cmbJornada.SelectedValue = "1";
                }

                else
                {
                    cmbJornada.SelectedValue = "2";
                }
                cmbJornada.Enabled = true;
            }

            else
            {
                cmbJornada.SelectedValue = "1";
                cmbJornada.Enabled = false;
            }

            //MessageBox.Show(iHora.ToString() +  ":" + iMinuto.ToString() + "      " + sHora);
        }

        //Función para llenar el Combo de Jornada
        private void llenarComboJornada()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_jornada, descripcion" + Environment.NewLine;
                sSql += "from pos_jornada" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbJornada.DataSource = dtConsulta;
                        cmbJornada.DisplayMember = "descripcion";
                        cmbJornada.ValueMember = "id_pos_jornada";
                        cmbJornada.SelectedIndex = 0;
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el Combo de Localidad
        private void llenarComboLocalidad()
        {
            try
            {
                DataTable dtPadre = new DataTable();

                sSql = "select id_localidad, nombre_localidad from tp_vw_localidades";
                
                cmbLocalidad.llenar(dtPadre, sSql);
                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
                //cmbLocalidad.SelectedIndex = 4;
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar la fecha
        private void llenarFecha()
        {
            string sFechaActual = Program.sFechaSistema.ToString("yyyy/MM/dd");
            txtFecha.Text = sFechaActual;
        }

        //FUNCION PARA OBTENER EL DI DEL PRODUCTO ITEM
        private bool sacarIdProductoItems()
        {
            sSql = "select id_producto from cv401_productos where codigo = 'ITEM'";
            
            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    Program.iIdProduto = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    return true;
                }
                else
                {
                    Program.iIdProduto = 0;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Función para cargar el número de Cuenta
        private void cargarNumeroCuenta()
        {
            sSql = "";
            sSql = "select isnull(max(cuenta),0) cuenta" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos" + Environment.NewLine;
            sSql += "where fecha_orden = '" + Program.sFechaOrden + "'";

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                Program.iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                Program.iCuentaDiaria++;
            }
            else
            {
                Program.iCuentaDiaria = 1;
            }
        }

        #endregion

        private void CodigoCajero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void CodigoCajero_Load(object sender, EventArgs e)
        {
            if (sacarIdProductoItems() == false)
            {
                //ok.LblMensaje.Text = "No se ha encontrado el producto item";
                //ok.ShowDialog();
            }

            this.ActiveControl = textBoxcodigo;

            cerrarUltimaCaja();
            llenarComboJornada();
            llenarComboLocalidad();
            llenarFecha();
            verJornada();
        }

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

            if (textBoxcodigo.Text.Length > 0)
            {

                str = textBoxcodigo.Text.Substring(textBoxcodigo.Text.Length - 1);
                loc = textBoxcodigo.Text.Length;
                textBoxcodigo.Text = textBoxcodigo.Text.Remove(loc - 1, 1);
            }

            textBoxcodigo.Focus();
            textBoxcodigo.SelectionStart = textBoxcodigo.Text.Trim().Length;
        }

        private void textBoxcodigo_KeyPress(object sender, KeyPressEventArgs e)
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
                if (textBoxcodigo.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la clave para proceder con la consulta.";
                    ok.ShowDialog();
                }

                else
                {
                    consultarRegistro();
                }
            }
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
    }
}
