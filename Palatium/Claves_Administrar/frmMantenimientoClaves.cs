using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Claves_Administrar
{
    public partial class frmMantenimientoClaves : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        DataTable dtConsulta;

        string sSql;

        bool bRespuesta;

        int iIdRegistro;

        public frmMantenimientoClaves()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL DATAGRIDVIEW
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql = sSql + "select id_pos_administracion_claves, clave_acceso, codigo, descripcion," + Environment.NewLine;
                sSql = sSql + "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO" + Environment.NewLine;
                sSql = sSql + "from pos_administracion_claves" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    sSql = sSql + "and (codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql = sSql + "or descripcion like '%" + txtBuscar.Text.Trim() + "%')";
                }

                sSql = sSql + "order by codigo";

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

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros en el sistema.";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_administracion_claves"].ToString().Trim(),
                                      dtConsulta.Rows[i]["clave_acceso"].ToString().Trim(),
                                      dtConsulta.Rows[i]["codigo"].ToString().Trim(),
                                      dtConsulta.Rows[i]["descripcion"].ToString().Trim().ToUpper(),
                                      dtConsulta.Rows[i]["ESTADO"].ToString().Trim().ToUpper()
                        );
                }

                lblRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados";
                dgvDatos.ClearSelection();
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
            grupoDatos.Enabled = false;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtClave.Clear();
            txtEstado.Clear();
            txtEstado.Text = "ACTIVO";
            chkVerClave.Enabled = false;
            chkVerClave.Checked = false;
            btnActualizar.Enabled = false;
            txtCodigo.Enabled = true;
            grupoDatos.Enabled = false;

            iIdRegistro = 0;

            llenarGrid();
            
            txtBuscar.Focus();
        }

        //FUNCION PARA ACTUALIZAR EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción de actualización.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql = sSql + "update pos_administracion_claves set" + Environment.NewLine;
                sSql = sSql + "clave_acceso = '" + txtClave.Text.Trim() + "'" + Environment.NewLine;
                sSql = sSql + "where id_pos_administracion_claves = " + iIdRegistro + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";


                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }

        }        

        #endregion

        private void frmMantenimientoClaves_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void chkVerClave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVerClave.Checked == true)
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtClave.Text.Trim() == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese una clave para actualizar el registro.";
                ok.ShowDialog();
                return;
            }

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                actualizarRegistro();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_pos_administracion_claves"].Value);
                txtCodigo.Text = dgvDatos.CurrentRow.Cells["codigo"].Value.ToString().Trim().ToUpper();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["descripcion"].Value.ToString().Trim().ToUpper();
                txtClave.Text = dgvDatos.CurrentRow.Cells["clave_acceso"].Value.ToString().Trim().ToUpper();
                txtEstado.Text = dgvDatos.CurrentRow.Cells["estado"].Value.ToString().Trim().ToUpper();

                txtClave.ReadOnly = false;
                chkVerClave.Enabled = true;
                btnActualizar.Enabled = true;
                grupoDatos.Enabled = true;
                txtClave.Focus();
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
