using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using MaterialSkin.Controls;

namespace Palatium.Oficina
{
    public partial class frmBackUp : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        DataTable dtConsulta;

        bool bRespuesta;

        string sBaseDatos;
        string sGuardar = @"D:\datos\backup\bd_";
        string sSql;
        string sArchivoSalida;

        public frmBackUp()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmBackUp_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();
            txtServidor.Text = Program.SQLSERVIDOR;
            txtBaseDatos.Text = Program.SQLBDATOS;

            sBaseDatos = cmbEmpresa.Text.Trim().ToLower();
            sBaseDatos = sBaseDatos.Replace(' ', '_');
            txtRuta.Text = sGuardar + sBaseDatos + "_" + DateTime.Now.ToString("yyyyMMdd");

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
                SiNo.lblMensaje.Text = "¿Desea realizar una copia de seguridad de la base de datos " + Program.SQLBDATOS + "?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (File.Exists(txtRuta.Text.Trim()))
                    {
                        File.Delete(txtRuta.Text.Trim());
                    }

                    bRespuesta = conexion.GFun_BackUp_BD(txtRuta.Text.Trim(), Program.SQLBDATOS);

                    if (bRespuesta == true)
                    {
                        //COMPRIMIR EN ZIP LA BASE DE DATOS
                        //sArchivoSalida = txtRuta.Text.Trim() + ".zip";
                        //sArchivoSalida = @"D:\\datos\\backup\\comprimidos\\" + txtBaseDatos.Text.Trim() + Program.sFechaSistema.ToString("yyyyMMdd") + ".zip";

                        
                        //if (File.Exists(sArchivoSalida))
                        //{
                        //    File.Delete(sArchivoSalida);
                        //}

                        ////FileInfo sourceFile = new FileInfo(txtRuta.Text.Trim());
                        ////FileStream sourceStream = sourceFile.OpenRead();
                        ////FileStream stream = new FileStream(sArchivoSalida, FileMode.Open);
                        ////ZipFile.

                        //ZipFile.CreateFromDirectory(txtRuta.Text.Trim(), sArchivoSalida);

                        ok.lblMensaje.Text = "La copia de la base de datos " + Program.SQLBDATOS + " se ha realizado con éxito.";
                        ok.ShowDialog();
                    }

                    else
                    {
                        ok.lblMensaje.Text = "Ocurrió un problema al realizar la copia de la base de datos " + Program.SQLBDATOS + ".";
                        ok.ShowDialog();
                    }
                }
                this.Cursor = Cursors.Default;
            }

            catch (Exception)
            {
                catchMensaje.lblMensaje.Text = "No se pudo respaldar la base de datos " + Program.SQLBDATOS + ".";
                catchMensaje.ShowDialog();
                this.Cursor = Cursors.Default;
            }
        }
    }
}
