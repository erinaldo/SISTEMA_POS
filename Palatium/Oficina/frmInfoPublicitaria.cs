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

namespace Palatium.Oficina
{
    public partial class frmInfoPublicitaria : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        SqlParameter[] parametro;

        public frmInfoPublicitaria()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR EL CAMPO DE LA BASE DE DATOS
        private bool consultarInfoPublicitaria()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(info_publicitaria, '') info_publicitaria" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = @idempresa" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@idempresa";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdEmpresa;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

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

                if (dtConsulta.Rows.Count == 0)
                {
                    txtInfoPublicitaria.Clear();
                    txtInfoPublicitaria.Focus();
                }

                else
                {
                    txtInfoPublicitaria.Text = dtConsulta.Rows[0]["info_publicitaria"].ToString().Trim().ToUpper();
                    txtInfoPublicitaria.SelectionStart = txtInfoPublicitaria.Text.Trim().Length;
                    txtInfoPublicitaria.Focus();
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

        //FUNCION PARA ACTUALIZAR EL REGISTRO   
        private bool actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al iniciar la transacción";
                    ok.ShowDialog();
                    return false;
                }

                sSql = "";
                sSql += "update sis_empresa set" + Environment.NewLine;
                sSql += "info_publicitaria = @info_publicitaria" + Environment.NewLine;
                sSql += "where idempresa = @idempresa" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@info_publicitaria";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = txtInfoPublicitaria.Text.Trim().ToUpper();

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@idempresa";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = Program.iIdEmpresa;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();

                consultarInfoPublicitaria();

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

        #endregion

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtInfoPublicitaria.Clear();
            txtInfoPublicitaria.Focus();
        }

        private void frmInfoPublicitaria_Load(object sender, EventArgs e)
        {
            consultarInfoPublicitaria();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //if (txtInfoPublicitaria.Text.Trim() == "")
            //{
            //    ok = new VentanasMensajes.frmMensajeNuevoOk();
            //    ok.lblMensaje.Text = "Favor ingrese la información publicitaria.";
            //    ok.ShowDialog();
            //    txtInfoPublicitaria.Focus();
            //    return;
            //}

            SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            SiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
            SiNo.ShowDialog();

            if (SiNo.DialogResult == DialogResult.OK)
            {
                actualizarRegistro();
            }
        }
    }
}
