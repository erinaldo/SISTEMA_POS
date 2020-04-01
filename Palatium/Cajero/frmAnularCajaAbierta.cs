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
    public partial class frmAnularCajaAbierta : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;

        string sSql;
        string sFecha;
        string sMotivo;

        int iIdPosCierreCajero;
        int iIdJornada;
        int iIdLocalidad;

        int iCuentaComandas;
        int iCuentaMovimientos;

        bool bRespuesta;

        DataTable dtConsulta;

        public frmAnularCajaAbierta()
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
                sSql += "select * from pos_vw_listar_cierres_caja_abiertas" + Environment.NewLine;
                sSql += "where fecha_apertura between '" + Convert.ToDateTime(txtDesde.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and '" + Convert.ToDateTime(txtHasta.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;
                sSql += "and estado_cierre_cajero = 'Abierta'" + Environment.NewLine;
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
                                      dtConsulta.Rows[i]["estado_cierre_cajero"].ToString().ToUpper());

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

        //FUNCION PARA CONSULTAR SI HAY COMANADS O MOVIMIENTOS DE CAJA
        private bool contarRegistros(int iIdPosCierreCajero_P)
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajero_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iCuentaComandas = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                sSql = "";
                sSql += "select count(*) cuenta from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_cierre_cajero = " + iIdPosCierreCajero_P;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }

                iCuentaMovimientos = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

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

        //ELIMINAR LA CAJDA
        private void eliminarRegistro(int iIdPosCierreCajero_P)
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cierre_cajero set" + Environment.NewLine;
                sSql += "motivo_anulacion = '" + sMotivo + "'," + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = getdate()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero_P;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();                    
                    return ;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El registro de caja se ha eliminado con éxito. Se recomienda reiniciar la aplicación.";
                ok.ShowDialog();
                llenarComboLocalidades();
                dgvDatos.Rows.Clear();
                txtDesde.Focus();
                return;
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

        private void frmAnularCajaAbierta_Load(object sender, EventArgs e)
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
            txtDesde.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (dgvDatos.Columns[e.ColumnIndex].Name == "eliminar_caja")
            {
                iIdPosCierreCajero = Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells["id_cierre_cajero"].Value);

                if (contarRegistros(iIdPosCierreCajero) == false)
                {
                    this.Cursor = Cursors.Default;
                    dgvDatos.ClearSelection();
                    return;
                }

                if ((iCuentaMovimientos == 0) && (iCuentaComandas == 0))
                {
                    Cancelar_Orden.frmMotivoEliminacionComanda motivo = new Cancelar_Orden.frmMotivoEliminacionComanda();
                    motivo.ShowDialog();

                    if (motivo.DialogResult == DialogResult.OK)
                    {
                        sMotivo = motivo.txtMotivo.Text.Trim().ToUpper();
                        motivo.Close();
                        eliminarRegistro(iIdPosCierreCajero);
                        this.Cursor = Cursors.Default;
                        dgvDatos.ClearSelection();
                    }

                    else
                    {
                        this.Cursor = Cursors.Default;
                    }
                }

                else
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Existen comandas o movimientos de caja asociadas al registro por eliminar. No se procederá a la eliminación del registro.";
                    ok.ShowDialog();
                    this.Cursor = Cursors.Default;
                    dgvDatos.ClearSelection();
                    return;
                }
            }            
        }
    }
}
