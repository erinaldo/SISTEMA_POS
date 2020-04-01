using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Personal
{
    public partial class frmPromotores : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdRegistro;
        int iIdPersona;
        int iCuenta;
        int iHabilitado;

        string sEstado;
        string sSql;

        public frmPromotores()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            dbAyudaPersonal.limpiar();
            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtClave.Clear();
            chkMostrar.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            iIdRegistro = 0;
            iHabilitado = 0;
            btnEliminar.Enabled = false;
            llenarGrid();
        }

        //Función para comprobar si hay un código repetido
        private int comprobarCodigo()
        {
            try
            {
                int iBandera = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (txtCodigo.Text == dgvDatos.Rows[i].Cells[1].Value.ToString())
                    {
                        iBandera = 1;
                        break;
                    }
                }

                return iBandera;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }        

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencia()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, identificacion, apellidos + ' ' + isnull(nombres, '') nombres " + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                if (Program.iManejaNomina == 1)
                {
                    sSql += "and estaenroldepagos = 1" + Environment.NewLine;
                }

                dbAyudaPersonal.Ver(sSql, "identificacion", 0, 1, 2);
            }

            catch (Exception ex)
            {
                ok.lblMensaje.Text = ex.Message;
                ok.ShowDialog();
            }
        }

        //FUNCION COLUMNAS GRID
        private void columnasGrid(bool ok)
        {
            dgvDatos.Columns[0].Visible = ok;
            dgvDatos.Columns[4].Visible = ok;
            dgvDatos.Columns[5].Visible = ok;
            dgvDatos.Columns[6].Visible = ok;
            dgvDatos.Columns[7].Visible = ok;
            dgvDatos.Columns[8].Visible = ok;

            dgvDatos.Columns[1].Width = 75;
            dgvDatos.Columns[2].Width = 200;
            dgvDatos.Columns[3].Width = 80;
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select PR.id_pos_promotor, PR.codigo as CODIGO, PR.descripcion as DESCRIPCION, " + Environment.NewLine;
                //sSql += "case PR.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "case PR.is_active when 1 then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "isnull(PR.claveacceso, 0) claveacceso, P.id_persona," + Environment.NewLine;
                sSql += "P.identificacion, P.apellidos + ' ' + isnull(P.nombres, '') persona," + Environment.NewLine;
                sSql += "isnull(PR.is_active, 0) is_active" + Environment.NewLine;
                sSql += "from pos_promotor PR INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas P on PR.id_persona = P.id_persona" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and PR.estado in ('A', 'N')" + Environment.NewLine;


                if (txtBuscar.Text.Trim() != "")
                {
                    sSql += "and PR.descripcion like '%" + txtBuscar.Text.Trim() + "%'";
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    columnasGrid(false);
                    dgvDatos.ClearSelection();
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR EL REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_promotor (" + Environment.NewLine;
                sSql += "id_persona, codigo, descripcion, claveacceso, is_active, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdPersona + ", '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'" + txtDescripcion.Text.Trim().ToUpper() + "', '" + txtClave.Text.Trim() + "'," + Environment.NewLine;
                sSql += "1, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro ingresado éxitosamente";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }


        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para actualizar el registro.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_promotor set" + Environment.NewLine;
                sSql += "id_persona = " + iIdPersona + "," + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "claveacceso = '" + txtClave.Text + "'," + Environment.NewLine;
                sSql += "is_active = " + iHabilitado + Environment.NewLine;
                sSql += "where id_pos_promotor = " + iIdRegistro;

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //INICIA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción para eliminar el registro.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_promotor set" + Environment.NewLine;
                sSql += "is_active = 0" + Environment.NewLine;
                //sSql += "estado = 'E'," + Environment.NewLine;
                //sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                //sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_promotor = " + iIdRegistro;

                //EJECUTA LA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Enabled = true;
                txtCodigo.Focus();
            }

            else
            {
                if ((txtCodigo.Text == "") && (txtDescripcion.Text == ""))
                {
                    ok.lblMensaje.Text = "Debe rellenar todos los campos obligatorios.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtCodigo.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el código del cajero.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la descripción del cajero.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else
                {
                    iIdPersona = dbAyudaPersonal.iId;

                    if (chkHabilitado.Checked == true)
                        iHabilitado = 1;
                    else
                        iHabilitado = 0;

                    if (btnNuevo.Text == "Guardar")
                    {
                        iCuenta = comprobarCodigo();

                        if (iCuenta == 0)
                        {
                            NuevoSiNo.lblMensaje.Text = "¿Desea guardar...?";
                            NuevoSiNo.ShowDialog();

                            if (NuevoSiNo.DialogResult == DialogResult.OK)
                            {
                                insertarRegistro();
                            }
                        }

                        else if (iCuenta == 1)
                        {
                            ok.lblMensaje.Text = "Ya existe un registro con el código ingresado.";
                            ok.ShowDialog();
                            txtCodigo.Clear();
                            txtCodigo.Focus();
                        }
                    }

                    else if (btnNuevo.Text == "Actualizar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Desea actualizar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdRegistro == 0)
            {
                ok.lblMensaje.Text = "No ha seleccionado un registro para verificar la eliminación.";
                ok.ShowDialog();
            }

            else
            {
                NuevoSiNo.lblMensaje.Text = "Esta seguro que desea dar de baja el registro?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }
        }

        private void txtClaveAcceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
            else
            {
                caracteres.soloNumeros(e);
            }
        }

        private void frmPromotores_Load(object sender, EventArgs e)
        {
            llenarGrid();
            llenarSentencia();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grupoDatos.Enabled = true;
                txtCodigo.Enabled = false;
                btnNuevo.Text = "Actualizar";
                btnEliminar.Enabled = true;

                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                txtClave.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
                iIdPersona = Convert.ToInt32(dgvDatos.CurrentRow.Cells[5].Value.ToString());
                
                if (iIdPersona == 0)
                {
                    dbAyudaPersonal.limpiar();
                }

                else
                {
                    dbAyudaPersonal.iId = iIdPersona;
                    dbAyudaPersonal.txtIdentificacion.Text = dgvDatos.CurrentRow.Cells[6].Value.ToString();
                    dbAyudaPersonal.txtDatos.Text = dgvDatos.CurrentRow.Cells[7].Value.ToString();
                }

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[8].Value.ToString()) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                chkHabilitado.Enabled = true;
                txtDescripcion.Focus();
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrar.Checked == true)
            {
                txtClave.PasswordChar = '\0';
                txtClave.Focus();
            }
            else
            {
                txtClave.PasswordChar = '*';
                txtClave.Focus();
            }

            txtClave.SelectionStart = txtClave.Text.Trim().Length;
        }
    }
}
