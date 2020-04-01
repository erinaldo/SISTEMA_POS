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
    public partial class frmRevisarCierres : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sFecha;

        int iIdPosCierreCajero;
        int iIdJornada;
        int iIdLocalidad;

        bool bRespuesta;

        DataTable dtConsulta;

        public frmRevisarCierres()
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
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where cg_localidad = " + Program.iCgLocalidadRecuperado;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidades.DisplayMember = "nombre_localidad";
                    cmbLocalidades.ValueMember = "id_localidad";
                    cmbLocalidades.DataSource = dtConsulta;
                    //cmbLocalidades.Items.Insert(0, "Todos");

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

        //FUNCION PAR LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select * from pos_vw_listar_cierres_caja" + Environment.NewLine;
                sSql += "where fecha_apertura between '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;

                if (chkCajasAbiertas.Checked == false)
                    sSql += "and estado_cierre_cajero = 'Cerrada'" + Environment.NewLine;

                sSql += "order by fecha_apertura desc";

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
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_pos_cierre_cajero"].ToString(),
                                      Convert.ToDateTime(dtConsulta.Rows[i]["fecha_apertura"].ToString()).ToString("dd-MM-yyyy"),
                                      dtConsulta.Rows[i]["hora_apertura"].ToString(),
                                      dtConsulta.Rows[i]["cajero"].ToString(),
                                      dtConsulta.Rows[i]["jornada"].ToString(),
                                      dtConsulta.Rows[i]["caja_inicial"].ToString(),
                                      dtConsulta.Rows[i]["caja_final"].ToString(),
                                      dtConsulta.Rows[i]["id_jornada"].ToString(),
                                      dtConsulta.Rows[i]["id_localidad"].ToString());

                    if (i % 2 == 0)
                    {
                        dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 255);
                    }

                    else
                    {
                        dgvDatos.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
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

        #endregion

        private void frmRevisarCierres_Load(object sender, EventArgs e)
        {
            llenarComboLocalidades();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime dtInicio = Convert.ToDateTime(txtDesde.Text.Trim());
            DateTime dtFinal = Convert.ToDateTime(txtHasta.Text.Trim());

            if (dtFinal < dtInicio)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha final no puede ser superior a la fecha inicial.";
                ok.ShowDialog();
                return;
            }

            llenarGrid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            llenarComboLocalidades();
            dgvDatos.Rows.Clear();
            chkCajasAbiertas.Checked = false;
            txtDesde.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dgvDatos.Columns[e.ColumnIndex].Name == "visualizar")
            {
                sFecha = Convert.ToDateTime(dgvDatos.Rows[e.RowIndex].Cells["fecha_apertura"].Value).ToString("yyyy-MM-dd");
                iIdPosCierreCajero = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_cierre_cajero"].Value);
                iIdJornada = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_jornada"].Value);
                iIdLocalidad = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_localidad"].Value);

                Cajero.frmVistaPreviaCierre vista = new frmVistaPreviaCierre(Convert.ToInt32(cmbLocalidades.SelectedValue), iIdPosCierreCajero);
                vista.ShowDialog();
            }

            else if (dgvDatos.Columns[e.ColumnIndex].Name == "ventas")
            {
                iIdPosCierreCajero = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_cierre_cajero"].Value);
                iIdLocalidad = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_localidad"].Value);

                Cajero.frmDetalleVentas detalle = new frmDetalleVentas(iIdLocalidad, iIdPosCierreCajero);
                detalle.ShowDialog();
            }

            else if (dgvDatos.Columns[e.ColumnIndex].Name == "caja_modal")
            {
                iIdPosCierreCajero = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_cierre_cajero"].Value);

                Cajero.frmVerArqueoCaja caja = new frmVerArqueoCaja(iIdPosCierreCajero);
                caja.ShowDialog();
            }

            this.Cursor = Cursors.Default;
            dgvDatos.ClearSelection();
        }
    }
}
