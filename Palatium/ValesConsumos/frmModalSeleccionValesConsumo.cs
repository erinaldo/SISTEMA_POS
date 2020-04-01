using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.ValesConsumos
{
    public partial class frmModalSeleccionValesConsumo : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        int iCuenta;
        int iBandera;

        public frmModalSeleccionValesConsumo()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboAreas()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_area_consumo_empleados, descripcion" + Environment.NewLine;
                sSql += "from pos_area_consumo_empleados" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and is_active = 1";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbAreas.DisplayMember = "descripcion";
                    cmbAreas.ValueMember = "id_pos_area_consumo_empleados";
                    cmbAreas.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR LA SENTENCIA DEL DBAYUDA
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select identificacion, ltrim(isnull(nombres, '') + '  ' + apellidos) razon_social, id_persona" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dBAyudaPersonas.Ver(sSql, "identificacion", 2, 0, 1);

                sSql = "";
                sSql += "select TP.identificacion, ltrim(isnull(TP.nombres, '') + '  ' + TP.apellidos) empleado, E.id_persona, E.id_empleado" + Environment.NewLine;
                sSql += "from cv408_empleado E INNER JOIN" + Environment.NewLine;
                sSql += "tp_personas TP ON TP.id_persona = E.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "where E.estado = 'A'";

                dbAyudaNomina.Ver(sSql, "identificacion", 2, 0, 1);
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONTAR LOS REGISTROS EN EL DATAGRID
        private int contarRegistrosGrid(int iIdPersona_P)
        {
            try
            {
                int iCuenta_P = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dgvDatos.Rows[i].Cells[0].Value.ToString()) == iIdPersona_P)
                    {
                        iCuenta_P++;
                        break;
                    }
                }

                return iCuenta_P;
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA CONSULTAR EL REGISTRO EN LA BASE DE DATOS
        private int consultarRegistroBase(int iIdPersona_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_empleado" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_persona = " + iIdPersona_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCICÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
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

        //FUNCION PARA INSERTAR EN LA BASE DE DATOS
        private void insertarRegistros()
        {
            try
            {
                iBandera = 0;

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción para guardar el registro.";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    sSql = "";
                    sSql += "insert into pos_empleado (" + Environment.NewLine;
                    sSql += "id_persona, id_empleado, id_pos_area_consumo_empleados, is_active," + Environment.NewLine;
                    sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += dgvDatos.Rows[i].Cells["id_persona"].Value.ToString() + ", ";
                    sSql += dgvDatos.Rows[i].Cells["id_empleado"].Value.ToString() + ", ";
                    sSql += dgvDatos.Rows[i].Cells["id_pos_area_consumo_empleados"].Value.ToString() + ", ";
                    sSql += "1, 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        iBandera = 1;
                        break;
                    }
                }

                if (iBandera == 1)
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                }

                else
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Registros agregados éxitosamente.";
                    ok.ShowDialog();
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count > 0)
            {
                dgvDatos.Rows.Remove(dgvDatos.CurrentRow);
                dgvDatos.ClearSelection();
            }

            else
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado ningún registro para remover.";
                ok.ShowDialog();
            }
        }

        private void frmModalSeleccionValesConsumo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmModalSeleccionValesConsumo_Load(object sender, EventArgs e)
        {
            llenarSentencias();
            llenarComboAreas();
        }

        private void btnAgregarNormal_Click(object sender, EventArgs e)
        {
            if (dBAyudaPersonas.txtIdentificacion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado ningún registro.";
                ok.ShowDialog();
                return;
            }

            if (Convert.ToInt32(cmbAreas.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el área asignada para el empleado.";
                ok.ShowDialog();
                return;
            }

            int iIdPersona_DB = dBAyudaPersonas.iId;
            iCuenta = contarRegistrosGrid(iIdPersona_DB);
            int iIdArea_DB = Convert.ToInt32(cmbAreas.SelectedValue);

            if (iCuenta == -1)
            {
                return;
            }

            if (iCuenta > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "EL registro ha ingresar ya se encuentra seleccionado.";
                ok.ShowDialog();
                return;
            }

            iCuenta = consultarRegistroBase(iIdPersona_DB);

            if (iCuenta == -1)
            {
                return;
            }

            if (iCuenta > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "EL registro a ingresar ya se encuentra registrado en el sistema.";
                ok.ShowDialog();
                return;
            }

            else
            {
                dgvDatos.Rows.Add(iIdPersona_DB, 0, 0, iIdArea_DB, dBAyudaPersonas.sCodigo, dBAyudaPersonas.txtDatos.Text);
                dBAyudaPersonas.limpiar();
                dgvDatos.ClearSelection();
            }
        }

        private void btnAgregarNomina_Click(object sender, EventArgs e)
        {
            if (dbAyudaNomina.txtIdentificacion.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado ningún registro.";
                ok.ShowDialog();
                return;
            }

            if (Convert.ToInt32(cmbAreas.SelectedValue) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione el área asignada para el empleado.";
                ok.ShowDialog();
                return;
            }

            int iIdPersona_DB = dbAyudaNomina.iId;
            int iIdEmpleado_DB;
            int iIdArea_DB = Convert.ToInt32(cmbAreas.SelectedValue);

            DataRow[] dFila = dbAyudaNomina.dtConsulta.Select("id_persona = " + iIdPersona_DB);

            if (dFila.Length != 0)
            {
                iIdEmpleado_DB = Convert.ToInt32(dFila[0][3].ToString());
            }

            else
            {
                iIdEmpleado_DB = 0;
            }

            iCuenta = contarRegistrosGrid(iIdPersona_DB);

            if (iCuenta == -1)
            {
                return;
            }

            if (iCuenta > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "EL registro ha ingresar ya se encuentra seleccionado.";
                ok.ShowDialog();
                return;
            }

            iCuenta = consultarRegistroBase(iIdPersona_DB);

            if (iCuenta == -1)
            {
                return;
            }

            if (iCuenta > 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "EL registro a ingresar ya se encuentra registrado en el sistema.";
                ok.ShowDialog();
                return;
            }

            else
            {
                dgvDatos.Rows.Add(iIdPersona_DB, 1, iIdEmpleado_DB, iIdArea_DB, dbAyudaNomina.sCodigo, dbAyudaNomina.txtDatos.Text);
                dbAyudaNomina.limpiar();
                dgvDatos.ClearSelection();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No ha seleccionado ningún registro.";
                ok.ShowDialog();
            }

            else
            {
                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Está seguro que desea guardar los registros?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    insertarRegistros();
                }
            }
        }
    }
}
