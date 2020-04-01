using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Domicilios
{
    public partial class frmListaClientesDomicilio : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        DataTable dtConsulta = new DataTable();

        string sSql;

        bool bRespuesta;

        public int iIdPersona;
        public int iIdDireccion;
        public int iIdTelefono;

        public string sIdentificacion;
        public string sApellidos;
        public string sNombres;
        public string sCodigoAlterno;
        public string sCorreoElectronico;
        public string sSector;
        public string sCallePrincipal;
        public string sNumeracion;
        public string sCalleSecundaria;
        public string sReferencia;
        public string sTelefonoConvencional;
        public string sTelefonoCelular;

        public frmListaClientesDomicilio(DataTable dtDatos)
        {
            this.dtConsulta = dtDatos;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_persona"].ToString(),
                                  dtConsulta.Rows[i]["identificacion"].ToString(),
                                  dtConsulta.Rows[i]["apellidos"].ToString(),
                                  dtConsulta.Rows[i]["nombres"].ToString(),
                                  dtConsulta.Rows[i]["codigo_alterno"].ToString(),
                                  dtConsulta.Rows[i]["correo_electronico"].ToString(),
                                  dtConsulta.Rows[i]["direccion"].ToString(),
                                  dtConsulta.Rows[i]["calle_principal"].ToString(),
                                  dtConsulta.Rows[i]["numero_vivienda"].ToString(),
                                  dtConsulta.Rows[i]["calle_interseccion"].ToString(),
                                  dtConsulta.Rows[i]["referencia"].ToString(),
                                  dtConsulta.Rows[i]["domicilio"].ToString(),
                                  dtConsulta.Rows[i]["celular"].ToString(),
                                  dtConsulta.Rows[i]["id_direccion"].ToString(),
                                  dtConsulta.Rows[i]["id_telefono"].ToString());
            }

            dgvDatos.ClearSelection();
        }

        //FUNCION PARA RECUPERAR DATOS
        private void recuperarDatos()
        {
            try
            {
                iIdPersona = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
                iIdDireccion = Convert.ToInt32(dgvDatos.CurrentRow.Cells[13].Value);
                iIdTelefono = Convert.ToInt32(dgvDatos.CurrentRow.Cells[14].Value);

                sIdentificacion = dgvDatos.CurrentRow.Cells[1].Value.ToString().Trim().ToUpper();
                sApellidos = dgvDatos.CurrentRow.Cells[2].Value.ToString().Trim().ToUpper();
                sNombres = dgvDatos.CurrentRow.Cells[3].Value.ToString().Trim().ToUpper();
                sCodigoAlterno = dgvDatos.CurrentRow.Cells[4].Value.ToString().Trim().ToUpper();
                sCorreoElectronico = dgvDatos.CurrentRow.Cells[5].Value.ToString().Trim().ToUpper();
                sSector = dgvDatos.CurrentRow.Cells[6].Value.ToString().Trim().ToUpper();
                sCallePrincipal = dgvDatos.CurrentRow.Cells[7].Value.ToString().Trim().ToUpper();
                sNumeracion = dgvDatos.CurrentRow.Cells[8].Value.ToString().Trim().ToUpper();
                sCalleSecundaria = dgvDatos.CurrentRow.Cells[9].Value.ToString().Trim().ToUpper();
                sReferencia = dgvDatos.CurrentRow.Cells[10].Value.ToString().Trim().ToUpper();
                sTelefonoConvencional = dgvDatos.CurrentRow.Cells[11].Value.ToString().Trim().ToUpper();
                sTelefonoCelular = dgvDatos.CurrentRow.Cells[12].Value.ToString().Trim().ToUpper();

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListaClientesDomicilio_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                ok.LblMensaje.Text = "Favor seleccione un registro.";
                ok.ShowDialog();
                return;
            }

            recuperarDatos();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                ok.LblMensaje.Text = "Favor seleccione un registro.";
                ok.ShowDialog();
                return;
            }

            recuperarDatos();
        }

        private void frmListaClientesDomicilio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
