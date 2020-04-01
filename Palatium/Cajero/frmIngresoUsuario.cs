using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmIngresoUsuario : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        Clases.ClaseUsuarios usuarios = new Clases.ClaseUsuarios();

        Button botonUsuario;

        DataTable dtConsulta;

        int iIdUsuario;

        bool bRespuesta;

        string sCargo;
        string sNombreUsuario;
        string sSql;
        string sFecha;
        string sHora;
        string sFechaCorta;
        string sNombreJornada;

        string sRespuesta;
        int iIdCajero;
        int iIdMesero;
        string sDescripcionUsuario;
        string sClaveAcceso;
        int iIdPersona;
        string sEstadoUsuario;
        string sCorreoElectronico;


        //HORAS DE APERTURA Y CIERRE DE CADA CAJERO
        //=================================================================
        string sHoraDiurnaAbrir = "08:00:00";
        string sHoraDiurnaCerrar = "17:59:59";

        string sHoraNocturnaAbrir = "18:00:00";
        string sHoraNocturnaCerrar = "23:59:59";

        string sHoraIngreso;

        public frmIngresoUsuario()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el Combo de Jornada
        private void llenarComboJornada()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_jornada, descripcion" + Environment.NewLine;
                sSql += "from pos_jornada where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbJornada.llenar(dtConsulta, sSql);

                if (cmbJornada.Items.Count == 0)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.\n\nComunìquese con el administrador en caso de presentar el mismo inconveniente.";
                    ok.ShowDialog();
                }
                else
                {
                    cmbJornada.SelectedIndex = 1;
                }

            }
            catch (Exception ex)
            {
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //Función para llenar el Combo de Localidad
        private void llenarComboLocalidad()
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
                ok.LblMensaje.Text = ex.Message;
                ok.ShowDialog();
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
        }


        //VERIFICAR LA JORNADA
        private bool jornadaCajero()
        {
            try
            {
                int iHora = Convert.ToInt32(DateTime.Now.Hour);

                sNombreJornada = cmbJornada.Text;

                //BUSCAMOS EL ID DE LA JORNADA
                sSql = "";
                sSql += "select id_pos_jornada" + Environment.NewLine;
                sSql += "from pos_jornada" + Environment.NewLine;
                sSql += "where descripcion = '" + sNombreJornada + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        Program.iJORNADA = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());

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
                ok.LblMensaje.Text = "Ocurrió un problema al concatenar los valores.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //FUNCION PARA CARGAR LOS USUARIOS
        public void mostrarUsuarios()
        {
            Button[,] boton = new Button[10, 10];
            int h = 0;

            //Program.saldo = double.Parse(txt_saldo.Text);
            if (usuarios.llenarDatos() == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].Click += boton_clic_usuario;
                        boton[i, j].Width = 150;
                        boton[i, j].Height = 75;
                        boton[i, j].Top = i * 75;
                        boton[i, j].Left = j * 150;
                        boton[i, j].BackColor = Color.Cyan;

                        if (h == usuarios.iCuenta)
                        {
                            break;
                        }

                        boton[i, j].Font = new Font("Consolas", 11);

                        //VERIFICAR SI ES CAJERO O MESERO
                        if (usuarios.usuarios[h].sIdCajero != "0")
                        {
                            boton[i, j].Image = Properties.Resources.cajero_ico;
                            boton[i, j].ImageAlign = ContentAlignment.MiddleLeft;
                            boton[i, j].Tag = usuarios.usuarios[h].sIdCajero;
                            boton[i, j].Text = usuarios.usuarios[h].sDescripcion + Environment.NewLine + "CAJERO";
                            boton[i, j].TextAlign = ContentAlignment.MiddleRight;
                            boton[i, j].AccessibleName = usuarios.usuarios[h].sDescripcion;
                            boton[i, j].AccessibleDescription = "CAJERO";
                        }

                        else if (usuarios.usuarios[h].sIdMesero != "0")
                        {
                            boton[i, j].Image = Properties.Resources.mesero_ico;
                            boton[i, j].ImageAlign = ContentAlignment.MiddleLeft;
                            boton[i, j].Tag = usuarios.usuarios[h].sIdMesero;
                            boton[i, j].Text = usuarios.usuarios[h].sDescripcion + Environment.NewLine + "MESERO";
                            boton[i, j].TextAlign = ContentAlignment.MiddleRight;
                            boton[i, j].AccessibleName = usuarios.usuarios[h].sDescripcion;
                            boton[i, j].AccessibleDescription = "MESERO";
                        }

                        this.Controls.Add(boton[i, j]);
                        pnlUsuarios.Controls.Add(boton[i, j]);
                        h++;
                    }
                }
            }
            else
            {
                ok.LblMensaje.Text = "No hay ninguna seccion de mesas registrada en el sistema.";
                ok.ShowDialog();
            }
        }

        private void boton_clic_usuario(object sender, EventArgs e)
        {
            botonUsuario = sender as Button;
            iIdUsuario = Convert.ToInt32(botonUsuario.Tag);
            lblUsuario.Text = botonUsuario.AccessibleDescription + " : " + botonUsuario.AccessibleName;
            sCargo = botonUsuario.AccessibleDescription;
            sNombreUsuario = botonUsuario.AccessibleName;
            GrupoDatos.Enabled = true;
            txtCodigo.Focus();
        }


        //FUNCION PARA CONSULTAR EN LA BASE DE DATOS
        private void consultarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select VU.*, isnull(TP.correo_electronico, '') correo_electronico" + Environment.NewLine;
                sSql += "from pos_vw_usuario VU, tp_personas TP" + Environment.NewLine;
                sSql += "where VU.id_persona = TP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;

                if (sCargo == "CAJERO")
                {
                    sSql += "and VU.id_pos_cajero = " + iIdUsuario;
                }

                else if (sCargo == "MESERO")
                {
                    sSql += "and VU.id_pos_mesero = " + iIdUsuario;
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count == 0)
                    {
                        ok.LblMensaje.Text = "No existe un registro configurado. Favor comuníquese con el administrador.";
                        ok.ShowDialog();
                        goto fin;
                    }

                    else
                    {
                        iIdCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        iIdMesero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                        sDescripcionUsuario = dtConsulta.Rows[0].ItemArray[2].ToString();
                        sClaveAcceso = dtConsulta.Rows[0].ItemArray[3].ToString();
                        iIdPersona = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[4].ToString());
                        sEstadoUsuario = dtConsulta.Rows[0].ItemArray[5].ToString();
                        sCorreoElectronico = dtConsulta.Rows[0].ItemArray[6].ToString();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }

                //VERIFICA LA CLAVE DE ACCESO
                //----------------------------------------------------------------------------------------
                if (sClaveAcceso != txtCodigo.Text.Trim())
                {
                    ok.LblMensaje.Text = "Ha ingresado su contraseña incorrecta. Favor verificar.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    goto fin;
                }

                //PERMISOS DE CAJERO
                if (iIdCajero != 0)
                {
                    Program.iPuedeCobrar = 1;

                    Program.CAJERO_ID = iIdCajero;
                    Program.sNombreCajero = sNombreUsuario;
                    Program.sNombreUsuario = sNombreUsuario;
                    Program.iIdPersonaMovimiento = iIdPersona;
                    Program.sEstadoUsuario = sEstadoUsuario;
                    Program.sCorreoElectronico = sCorreoElectronico;

                    Program.sDatosMaximo[0] = sNombreUsuario;
                    Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                    Program.sDatosMaximo[2] = sEstadoUsuario;
                }

                //PERMISOS DE MESERO
                else if(iIdMesero != 0)
                {
                    Program.iPuedeCobrar = 0;

                    Program.iIdMesero = iIdMesero;
                    Program.nombreMesero = sDescripcionUsuario;

                    if (extraerCajero() == false)
                    {
                        ok.LblMensaje.Text = "Ocurrió un problema al consultar los datos del cajero.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                        goto fin;
                    }

                    Program.sNombreUsuario = sNombreUsuario;
                    Program.sEstadoUsuario = sEstadoUsuario;
                    Program.iIdPersonaMovimiento = iIdPersona;
                    Program.sCorreoElectronico = sCorreoElectronico;

                    Program.sDatosMaximo[0] = sNombreUsuario;
                    Program.sDatosMaximo[1] = Environment.MachineName.ToString();
                    Program.sDatosMaximo[2] = sEstadoUsuario;
                }

                if (verCierreCajero() == false)
                {
                    goto fin;
                }


                //SECCION PARA ABRIR EL SISTEMA
                ok.LblMensaje.Text = "Bienvenido (a)\n\n" + Program.sNombreUsuario;
                ok.ShowDialog();
                Program.iVerCaja = 1;
                Program.sFechaOrden = lblFechaOculta.Text.Trim();
                this.Hide();
                //cargarNumeroCuenta();
                this.DialogResult = DialogResult.OK;
                Program.horaEntrada = DateTime.Now.ToLongTimeString();
                goto fin;
                
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

            fin: { }
        }


        //FUNCION PARA VERIFICAR EL CIERRE DE CAJERO
        private bool verCierreCajero()
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where fecha_apertura = '" + lblFechaOculta.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and id_jornada = " + Convert.ToInt32(cmbJornada.SelectedValue);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (dtConsulta.Rows[0].ItemArray[8].ToString() == "Cerrada")
                        {
                            if ((dtConsulta.Rows[0].ItemArray[2].ToString() == cmbJornada.SelectedValue.ToString()) && (dtConsulta.Rows[0].ItemArray[4].ToString().Substring(0, 10) == Convert.ToDateTime(lblFechaOculta.Text.Trim()).ToString("dd/MM/yyyy")))
                            {
                                ok.LblMensaje.Text = "Ya existe un cierre de caja realizado en la jornada seleccionada. Solo puede visualizar solo ciertas opciones.";
                                Program.iVerCaja = 0;
                                this.DialogResult = DialogResult.OK;
                                ok.ShowDialog();
                                txtCodigo.Clear();
                                txtCodigo.Focus();
                                goto falso;
                            }

                            else
                            {
                                //CREAR JORNADA NUEVA
                                if (insertarCierreCajero() == false)
                                {                                 
                                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                                    ok.ShowDialog();
                                    txtCodigo.Clear();
                                    txtCodigo.Focus();
                                    goto falso;
                                }
                            }

                            //YA EXISTE UN CIERRE DE CAJA REALIZADO
                            ok.LblMensaje.Text = "Ya existe un cierre de caja realizado en la jornada seleccionada.";
                            ok.ShowDialog();
                            txtCodigo.Clear();
                            goto falso;
                        }


                        else if (dtConsulta.Rows[0].ItemArray[8].ToString() == "Abierta")
                        {
                            if ((dtConsulta.Rows[0].ItemArray[3].ToString() != Program.CAJERO_ID.ToString()) || (dtConsulta.Rows[0].ItemArray[2].ToString() != cmbJornada.SelectedValue.ToString()))
                            {
                                ok.LblMensaje.Text = "Los datos ingresados no corresponde con el registro vigente para el cierre de cajero.";
                                ok.ShowDialog();
                                txtCodigo.Clear();
                                txtCodigo.Focus();
                                goto falso;
                            }

                            else
                            {
                                //GUARDAR LOS DATOS PARA CIERRE DE CAJERO
                                string sFechaAux;
                                Program.iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                                Program.iJornadaCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[2].ToString());
                                sFechaAux = dtConsulta.Rows[0].ItemArray[4].ToString().Substring(0, 10);
                                Program.sFechaAperturaCajero = Convert.ToDateTime(sFechaAux).ToString("yyyy/MM/dd");
                                Program.sEstadoCajero = dtConsulta.Rows[0].ItemArray[8].ToString();
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                            ok.ShowDialog();
                            txtCodigo.Clear();
                            txtCodigo.Focus();
                            goto falso;
                        }
                    }

                    else
                    {
                        //CREAR JORNADA NUEVA
                        if (insertarCierreCajero() == false)
                        {
                            goto falso;
                        }
                    }

                    goto verdadero;
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    goto falso;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            verdadero: { return true; }
            falso: { return false; }
        }


        //FUNCION PARA EXTRAER LOS DATOS DEL CAJERO
        private bool extraerCajero()
        {
            try
            {
                sFechaCorta = DateTime.Now.ToString("yyyy/MM/dd");

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
                        Program.CAJERO_ID = Convert.ToInt32(dtVer.Rows[0].ItemArray[0].ToString());
                        Program.sNombreCajero = dtVer.Rows[0].ItemArray[1].ToString();
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
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                return false;
            }
        }


        //INSERTAR UN NUEVO REGISTRO
        private bool insertarCierreCajero()
        {
            try
            {
                sFecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                sFechaCorta = DateTime.Now.ToString("yyyy/MM/dd");
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
                                Program.iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                                Program.iJornadaCajero = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[2].ToString());
                                sFechaAux = dtConsulta.Rows[0].ItemArray[4].ToString().Substring(0, 10);
                                Program.sFechaAperturaCajero = Convert.ToDateTime(sFechaAux).ToString("yyyy/MM/dd");
                                Program.sEstadoCajero = dtConsulta.Rows[0].ItemArray[8].ToString();
                                return true;
                            }

                            else
                            {
                                ok.LblMensaje.Text = "Ocurriò un problema al guardar al crear el registro para cierre de cajero.";
                                ok.ShowDialog();
                                return false;                         
                            }
                        }

                        else
                        {
                            ok.LblMensaje.Text = "Ocurriò un problema al guardar al crear el registro para cierre de cajero.";
                            ok.ShowDialog();
                            return false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //Función para cargar el número de Cuenta
        private void cargarNumeroCuenta()
        {
            sSql = "";
            sSql += "select isnull(max(cuenta),0) cuenta" + Environment.NewLine;
            sSql += "from cv403_cab_pedidos" + Environment.NewLine;
            sSql += "where fecha_orden = '" + Program.sFechaOrden + "'";
            
            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                Program.iCuentaDiaria = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                Program.iCuentaDiaria++;
            }
            else
            {
                Program.iCuentaDiaria = 1;
            }
        }

        #endregion

        #region FUNCIONES PARA CERRAR LA CAJA ANTERIOR

        //FUNCION PARA CERRAR LA ULTIMA CAJA ABIERTA
        private void cerrarUltimaCaja()
        {
            try
            {
                sFecha = DateTime.Now.ToString("yyyy/MM/dd");

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
                        if (dtConsulta.Rows[0].ItemArray[2].ToString() == "Abierta")
                        {
                            //ACTUALIZA EL REGISTRO DE CAJA A CERRADA
                            actualizarUltimoRegistro(Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString()));
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
                sSql += "hora_cierre = '" + sFecha.Substring(11, 8) + "'," + Environment.NewLine;
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

        #endregion

        private void frmIngresoUsuario_Load(object sender, EventArgs e)
        {
            cerrarUltimaCaja();
            mostrarUsuarios();
            llenarComboJornada();
            llenarComboLocalidad();
            lblFecha.Text = DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");
            lblFechaOculta.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            concatenarValores(btn0.Text);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //(this.Owner as Form1).desactivarBotones();
            this.Close(); 
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (iIdUsuario == 0)
            {
                ok.LblMensaje.Text = "No ha seleccionado un usuario para ingresar al sistema.";
                ok.ShowDialog();
            }

            else if (txtCodigo.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese la clave del usuario.";
                ok.ShowDialog();
                txtCodigo.Focus();
            }

            else if (jornadaCajero() == false)
            {
                ok.LblMensaje.Text = "Por favor, seleccione una jornada para ingresar al sistema.";
                ok.ShowDialog();
                txtCodigo.Clear();
                txtCodigo.Focus();
            }

            else
            {
                consultarRegistro();
            }
        }

        private void frmIngresoUsuario_KeyDown(object sender, KeyEventArgs e)
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
                btnIngresar_Click(sender, e);
            }
        }
    }
}
