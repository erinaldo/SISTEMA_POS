using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MaterialSkin.Controls;

namespace Palatium.Oficina
{
    public partial class frmBackUp : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseComprimirArchivos zippear;

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        DataTable dtConsulta;

        bool bRespuesta;

        string sBaseDatos;
        string sGuardar = @"D:\datos\backup\";
        string sRutaComprimir;
        string sSql;
        string sArchivoSalida;
        string sFecha;

        SqlParameter[] parametro;
            
        public frmBackUp()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA EXTRAER LA FECHA DEL SISTEMA
        private void fechaSistema()
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
                    return;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyyMMdd");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL DIRECTORIO DE RESPALDO
        private void extraerDirectorioRespaldo()
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select url_backup_bdd" + Environment.NewLine;
                sSql += "from pos_parametro" + Environment.NewLine;
                sSql += "where estado = @estado";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    sRutaComprimir = "";
                    btnProcesar.Enabled = false;
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentra parametrizado la ruta de respaldo. Favor solicite la parametrización del directorio.";
                    ok.ShowDialog();
                    return;
                }

                btnProcesar.Enabled = true;
                sRutaComprimir = dtConsulta.Rows[0][0].ToString().Trim();

                sBaseDatos = cmbEmpresa.Text.Trim().ToLower();
                sBaseDatos = sBaseDatos.Replace(' ', '_');
                sRutaComprimir = sRutaComprimir + @"\" + sFecha;
                txtRuta.Text = sRutaComprimir + @"\" + sBaseDatos + "_" + sFecha;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE EMPRESA
        private void llenarComboEmpresa()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select idempresa, case when nombrecomercial in ('', null) then" + Environment.NewLine;
                sSql += "razonsocial else nombrecomercial end nombre_comercial, *" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                cmbEmpresa.llenar(dtConsulta, sSql);
                if (cmbEmpresa.Items.Count >= 1)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        

        #endregion

        private void frmBackUp_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();
            fechaSistema();
            extraerDirectorioRespaldo();
            txtServidor.Text = Program.SQLSERVIDOR;
            txtBaseDatos.Text = Program.SQLBDATOS;            

            this.ActiveControl = btnProcesar;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            if (fbRuta.ShowDialog() == DialogResult.OK)
            {
                sBaseDatos = cmbEmpresa.Text.Trim().ToLower();
                sBaseDatos = sBaseDatos.Replace(' ', '_');
                sBaseDatos = fbRuta.SelectedPath + @"\bd_" + sBaseDatos + "_" + DateTime.Now.ToString("yyyyMMdd");
                txtRuta.Text = sBaseDatos;
            }
        }

        private void frmBackUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                SiNo.lblMensaje.Text = "¿Desea realizar una copia de seguridad de la base de datos " + Program.SQLBDATOS + "?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (!Directory.Exists(sRutaComprimir))
                    {
                        DirectoryInfo generado = Directory.CreateDirectory(sRutaComprimir);
                    }

                    if (File.Exists(txtRuta.Text.Trim()))
                    {
                        File.Delete(txtRuta.Text.Trim());
                    }

                    bRespuesta = conexion.GFun_BackUp_BD(txtRuta.Text.Trim(), Program.SQLBDATOS);

                    if (bRespuesta == false)
                    {
                        this.Cursor = Cursors.Default;
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Ocurrió un problema al realizar la copia de la base de datos " + Program.SQLBDATOS + ".";
                        ok.ShowDialog();
                        return;
                    }

                    if (chkComprimir.Checked == true)
                    {
                        zippear = new Clases.ClaseComprimirArchivos();

                        bRespuesta = zippear.comprimirArchivo(sRutaComprimir);

                        if (bRespuesta == false)
                        {
                            this.Cursor = Cursors.Default;
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = zippear.sMensajeError;
                            ok.ShowDialog();
                            return;
                        }
                    }

                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La copia de la base de datos " + Program.SQLBDATOS + " se ha realizado con éxito.";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                }
                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = "No se pudo respaldar la base de datos " + Program.SQLBDATOS + ".";
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }
    }
}
