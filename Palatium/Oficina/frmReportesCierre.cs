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
    public partial class frmReportesCierre : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        string sSql;
        string sOrden;

        DataTable dtConsulta;

        bool bRespuesta;

        int iIdPosReporteCierre;
        int iIdLocalidad;
        int iBandera;

        public frmReportesCierre()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;

                    cmbLocalidades.SelectedValue = Program.iIdLocalidad;
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

        //FUNCION PARA LLENAR EL COMBO DE REPORTES
        private void llenarComboReportes()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_reportes_cierre, descripcion" + Environment.NewLine;
                sSql += "from pos_reportes_cierre" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbReportes.DisplayMember = "descripcion";
                    cmbReportes.ValueMember = "id_pos_reportes_cierre";
                    cmbReportes.DataSource = dtConsulta;
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

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_reportes_por_localidad" + Environment.NewLine;
                sSql += "where id_localidad = " + cmbLocalidades.SelectedValue;

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

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_reportes_cierre"].ToString(),
                                      dtConsulta.Rows[i]["id_pos_reportes_cierre_por_localidad"].ToString(),
                                      dtConsulta.Rows[i]["id_localidad"].ToString(),
                                      dtConsulta.Rows[i]["descripcion"].ToString(),
                                      dtConsulta.Rows[i]["orden"].ToString(),
                                      dtConsulta.Rows[i]["en_base"].ToString());
                }

                dgvDatos.ClearSelection();

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        //FUNCION PARA GUARDAR LOS CAMBIOS EN LA BASE DE DATOS
        private void guardarRegistros()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_reportes_cierre_por_localidad set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_localidad = " + cmbLocalidades.SelectedValue;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    iIdPosReporteCierre = Convert.ToInt32(dgvDatos.Rows[i].Cells[0].Value);
                    iIdLocalidad = Convert.ToInt32(dgvDatos.Rows[i].Cells[2].Value);
                    sOrden = dgvDatos.Rows[i].Cells[4].Value.ToString().Trim();

                    sSql = "";
                    sSql += "insert into pos_reportes_cierre_por_localidad (" + Environment.NewLine;
                    sSql += "id_localidad, id_pos_reportes_cierre, orden, estado," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdLocalidad + ", " + iIdPosReporteCierre + ", " + sOrden + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //EJECUTAR LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        return;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registros ingresados éxitosamente.";
                ok.ShowDialog();
                limpiar();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            dgvDatos.ClearSelection();
            btnEliminarLinea.Visible = false;
            btnNuevaLinea.Visible = true;
            txtOrden.Clear();
            cmbReportes.SelectedIndex = 0;

            grupoDatos.Enabled = false;
            grupoBotones.Enabled = false;

            cmbLocalidades.Enabled = true;

            llenarGrid();

            cmbLocalidades.Focus();
        }

        #endregion

        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void frmReportesCierre_Load(object sender, EventArgs e)
        {
            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboReportes();
            llenarGrid();
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnAgregarReportes_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = true;
            grupoBotones.Enabled = true;
            cmbReportes.SelectedIndex = 0;
            cmbLocalidades.Enabled = false;
            cmbReportes.Focus();
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdPosReporteCierre = Convert.ToInt32(dgvDatos.CurrentRow.Cells[1].Value);
                txtOrden.Clear();
                btnEliminarLinea.Visible = true;
                btnNuevaLinea.Visible = false;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnEliminarLinea_Click(object sender, EventArgs e)
        {
            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                dgvDatos.Rows.Remove(dgvDatos.CurrentRow);
                dgvDatos.ClearSelection();
                btnEliminarLinea.Visible = false;
                btnNuevaLinea.Visible = true;
                txtOrden.Clear();
                cmbReportes.SelectedIndex = 0;
                cmbReportes.Focus();
            }
        }

        private void btnQuitarSeleccion_Click(object sender, EventArgs e)
        {
            dgvDatos.ClearSelection();
            btnEliminarLinea.Visible = false;
            btnNuevaLinea.Visible = true;
            txtOrden.Clear();
            cmbReportes.SelectedIndex = 0;
            cmbReportes.Focus();
        }

        private void btnNuevaLinea_Click(object sender, EventArgs e)
        {
            if (txtOrden.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el número de orden para imprimir el reporte.";
                ok.ShowDialog();
                txtOrden.Focus();
                return;
            }

            if (Convert.ToInt32(txtOrden.Text.Trim()) == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El número de orden debe ser diferente a cero.";
                ok.ShowDialog();
                txtOrden.Clear();
                txtOrden.Focus();
                return;
            }

            iBandera = 0;

            for (int i = 0; i < dgvDatos.Rows.Count; i++)
            {
                if (Convert.ToInt32(cmbReportes.SelectedValue) == Convert.ToInt32(dgvDatos.Rows[i].Cells[0].Value))
                {
                    iBandera = 1;
                    break;
                }
            }

            if (iBandera == 1)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El reporte seleccionado ya se encuentra ingresado.";
                ok.ShowDialog();
            }

            else
            {
                dgvDatos.Rows.Add(cmbReportes.SelectedValue, "0", cmbLocalidades.SelectedValue,
                                      cmbReportes.Text.ToUpper(), txtOrden.Text.Trim(), "0");
            }

            dgvDatos.ClearSelection();
            cmbReportes.SelectedIndex = 0;
            txtOrden.Clear();
            cmbReportes.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay registros para guardar.";
                ok.ShowDialog();
                return;
            }

            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar los registros?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                guardarRegistros();
            }
        }
    }
}
