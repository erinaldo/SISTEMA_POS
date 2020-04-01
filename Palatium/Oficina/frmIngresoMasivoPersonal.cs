using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmIngresoMasivoPersonal : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        string sSql;
        string sApellidos;
        string sNombres;
        string sIdentificacion;
        string sCodigo;
        string sTabla;
        string sCampo;

        DataTable dtConsulta;

        bool bRespuesta;

        int iSecuencia;
        int iSecuenciaAuxiliar;
        int iTipoPersona;
        int iTipoIdentificacion;
        int iIdPersona;
        int iIndice = - 1;

        public frmIngresoMasivoPersonal()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBOBOX DE EMPRESAS
        private void llenarCOmboEmpresas()
        {
            try
            {
                sSql = "";
                sSql += "select CE.id_pos_cliente_empresarial," + Environment.NewLine;
                sSql += "ltrim(isnull(TP.nombres, '') + ' ' + TP.apellidos) empresa" + Environment.NewLine;
                sSql += "from pos_cliente_empresarial CE INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = CE.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "and CE.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbEmpresa.DisplayMember = "empresa";
                    cmbEmpresa.ValueMember = "id_pos_cliente_empresarial";
                    cmbEmpresa.DataSource = dtConsulta;

                    cmbEmpresa.SelectedValue = Program.iIdLocalidad;
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        private void recuperarSecuencia()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(secuencia_codigo_empleado_cliente, 1) secuencia_codigo_empleado_cliente" + Environment.NewLine;
                sSql += "from pos_parametro_localidad" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iSecuencia = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iSecuenciaAuxiliar = iSecuencia;
                    }

                    else
                    {
                        iSecuencia = 1;
                        iSecuenciaAuxiliar = 1;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistros()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar los registros.";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    sIdentificacion = dgvDatos.Rows[i].Cells[0].Value.ToString();
                    sApellidos = dgvDatos.Rows[i].Cells[1].Value.ToString();
                    sNombres = dgvDatos.Rows[i].Cells[2].Value.ToString();

                    iTipoIdentificacion = Convert.ToInt32(dgvDatos.Rows[i].Cells[3].Value.ToString());
                    iTipoPersona = Convert.ToInt32(dgvDatos.Rows[i].Cells[4].Value.ToString());

                    //PRIMERO CONSULTAMOS EL REGISTRO EN LA BASE DE DATOS
                    sSql = "";
                    sSql += "select count(*) cuenta" + Environment.NewLine;
                    sSql += "from tp_personas" + Environment.NewLine;
                    sSql += "where identificacion = '" + sIdentificacion + "'" + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == false)
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        break;
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0][0].ToString()) != 0)
                    {
                        goto continuar;
                    }

                    sSql = "";
                    sSql += "insert into tp_personas (" + Environment.NewLine;
                    sSql += "idempresa, cg_tipo_persona, cg_tipo_identificacion, identificacion," + Environment.NewLine;
                    sSql += "nombres, apellidos, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += Program.iIdEmpresa + ", " + iTipoPersona + ", " + iTipoIdentificacion + ", ";
                    sSql += "'" + sIdentificacion + "', '" + sNombres + "', '" + sApellidos + "'," + Environment.NewLine;
                    sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        break;
                    }

                    sTabla = "tp_personas";
                    sCampo = "id_persona";

                    long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        ok.lblMensaje.Text = "No se pudo obtener el codigo del cliente.";
                        ok.ShowDialog();
                        break;
                    }

                    iIdPersona = Convert.ToInt32(iMaximo);

                    //INSTRUCCION PARA INSERTAR EN LA TABLA TP_DIRECCIONES
                    sSql = "";
                    sSql += "Insert Into tp_direcciones (" + Environment.NewLine;
                    sSql += "id_persona, IdTipoEstablecimiento, cg_Localidad, direccion, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso,terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger,numero_control_replica) " + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdPersona + ", 1, " + Program.iCgLocalidad + ", ";
                    sSql += "'QUITO', 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', 0, 0)";

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        break;
                    }

                    //PARA INSERTAR EL TELEFONO EN LA TABLA TP_TELEFONOS
                    sSql = "";
                    sSql += "Insert Into tp_telefonos (" + Environment.NewLine;
                    sSql += "id_persona, idTipoEstablecimiento, codigo_area, domicilio," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                    sSql += "numero_replica_trigger,numero_control_replica ) " + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += iIdPersona + ", 1, '02', '2222222', 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "', 0, 0)";

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        break;
                    }

                    //INSERTAMOS EN LA TABLA POS_EMPLEADOS
                    sSql = "";
                    sSql += "insert into pos_empleado_cliente (" + Environment.NewLine;
                    sSql += "id_pos_cliente_empresarial, id_persona, aplica_almuerzo," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += dgvDatos.Rows[i].Cells[5].Value.ToString() + ", " + iIdPersona + ", ";
                    sSql += "1, 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        break;
                    }

                    continuar: { }
                }

                sSql = "";
                sSql += "update pos_parametro_localidad set" + Environment.NewLine;
                sSql += "secuencia_codigo_empleado_cliente = " + iSecuencia + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registros ingresados éxitosamente";
                ok.ShowDialog();

                limpiar();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            iIndice = -1;
            txtIdentificacion.Clear();
            txtApellidos.Clear();
            txtNombres.Clear();

            dgvDatos.Rows.Clear();

            recuperarSecuencia();

            txtIdentificacion.Focus();
        }

        #endregion

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Agregar")
            {
                if (txtApellidos.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el apellido de la persona.";
                    ok.ShowDialog();
                    txtApellidos.Focus();
                    return;
                }

                if (txtIdentificacion.Text.Trim() == "")
                {
                    txtIdentificacion.Text = "0";
                    iTipoPersona = 2447;
                    iTipoIdentificacion = 7914;
                }

                else
                {
                    if (txtIdentificacion.Text.Trim().Length == 10)
                    {
                        iTipoPersona = 2447;
                        iTipoIdentificacion = 178;
                    }

                    else if (txtIdentificacion.Text.Trim().Length == 13)
                    {
                        int iTercer = Convert.ToInt32(txtIdentificacion.Text.Trim().Substring(2, 1));

                        if (iTercer == 9)
                        {
                            iTipoPersona = 2448;
                            iTipoIdentificacion = 179;
                        }

                        else if (iTercer == 6)
                        {
                            iTipoPersona = 2448;
                            iTipoIdentificacion = 179;
                        }

                        else if (iTercer < 6)
                        {
                            iTipoPersona = 2447;
                            iTipoIdentificacion = 179;
                        }

                        else
                        {
                            iTipoPersona = 2447;
                            iTipoIdentificacion = 7914;
                        }
                    }

                    else
                    {
                        iTipoPersona = 2447;
                        iTipoIdentificacion = 7914;
                    }
                }



                dgvDatos.Rows.Add(txtIdentificacion.Text.Trim(), txtApellidos.Text.Trim().ToUpper(), txtNombres.Text.Trim().ToUpper(), iTipoIdentificacion.ToString(), iTipoPersona.ToString(), cmbEmpresa.SelectedValue.ToString());
                dgvDatos.ClearSelection();

                txtIdentificacion.Clear();
                txtApellidos.Clear();
                txtNombres.Clear();
                txtIdentificacion.Focus();
            }

            else
            {
                if (txtApellidos.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el apellido de la persona.";
                    ok.ShowDialog();
                    txtApellidos.Focus();
                    return;
                }

                if (txtIdentificacion.Text.Trim() == "")
                {
                    txtIdentificacion.Text = "0";
                    iTipoPersona = 2447;
                    iTipoIdentificacion = 7914;
                }

                else
                {
                    if (txtIdentificacion.Text.Trim().Length == 10)
                    {
                        iTipoPersona = 2447;
                        iTipoIdentificacion = 178;
                    }

                    else if (txtIdentificacion.Text.Trim().Length == 13)
                    {
                        int iTercer = Convert.ToInt32(txtIdentificacion.Text.Trim().Substring(2, 1));

                        if (iTercer == 9)
                        {
                            iTipoPersona = 2448;
                            iTipoIdentificacion = 179;
                        }

                        else if (iTercer == 6)
                        {
                            iTipoPersona = 2448;
                            iTipoIdentificacion = 179;
                        }

                        else if (iTercer < 6)
                        {
                            iTipoPersona = 2447;
                            iTipoIdentificacion = 179;
                        }

                        else
                        {
                            iTipoPersona = 2447;
                            iTipoIdentificacion = 7914;
                        }
                    }

                    else
                    {
                        iTipoPersona = 2447;
                        iTipoIdentificacion = 7914;
                    }
                }



                //dgvDatos.Rows.Add(txtIdentificacion.Text.Trim(), txtApellidos.Text.Trim().ToUpper(), txtNombres.Text.Trim().ToUpper(), iTipoIdentificacion.ToString(), iTipoPersona.ToString());

                dgvDatos.Rows[iIndice].Cells[0].Value = txtIdentificacion.Text.Trim();
                dgvDatos.Rows[iIndice].Cells[1].Value = txtApellidos.Text.Trim();
                dgvDatos.Rows[iIndice].Cells[2].Value = txtNombres.Text.Trim();
                dgvDatos.Rows[iIndice].Cells[3].Value = iTipoIdentificacion.ToString();
                dgvDatos.Rows[iIndice].Cells[4].Value = iTipoPersona.ToString();

                dgvDatos.ClearSelection();

                txtIdentificacion.Clear();
                txtApellidos.Clear();
                txtNombres.Clear();
                txtIdentificacion.Focus();
                iIndice = -1;
                btnAgregar.Text = "Agregar";
            }
        }

        private void frmIngresoMasivoPersonal_Load(object sender, EventArgs e)
        {
            llenarCOmboEmpresas();
            recuperarSecuencia();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                dgvDatos.Rows.Remove(dgvDatos.CurrentRow);
                dgvDatos.ClearSelection();
            }

            else
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún registro para remover.";
                ok.ShowDialog();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.lblMensaje.Text = "No hay registros para guardar.";
                ok.ShowDialog();
                return;
            }

            
            //RECORRER EL GRID PARA CAMBIAR LOS CODIGOS
            for (int i = 0; i < dgvDatos.Rows.Count; i++)
            {
                if (dgvDatos.Rows[i].Cells[0].Value.ToString().Trim() == "0")
                {
                    sCodigo = dgvDatos.Rows[i].Cells[1].Value.ToString().Trim().Substring(0, 3) + iSecuencia.ToString().PadLeft(4, '0');
                    dgvDatos.Rows[i].Cells[0].Value = sCodigo.Trim();
                    iSecuencia++;
                }
            }

            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar los registros?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                insertarRegistros();
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iIndice = dgvDatos.CurrentRow.Index;

            txtIdentificacion.Text = dgvDatos.Rows[iIndice].Cells[0].Value.ToString().Trim();
            txtApellidos.Text = dgvDatos.Rows[iIndice].Cells[1].Value.ToString().Trim();
            txtNombres.Text = dgvDatos.Rows[iIndice].Cells[2].Value.ToString().Trim();
            cmbEmpresa.SelectedValue = dgvDatos.Rows[iIndice].Cells[5].Value.ToString().Trim();
            btnAgregar.Text = "Modificar";
        }
    }
}
