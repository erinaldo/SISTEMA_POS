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

namespace Palatium.Utilitarios
{
    public partial class frmListarProductosVendidos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter;

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        string sSql;
        string sFecha;
        string sFechaInicial;
        string sFechaFinal;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmListarProductosVendidos()
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

        //FUNCION PARA CONSULTAR LA FECHA DEL SISTEMA
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

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("dd-MM-yyyy");
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
            fechaSistema();

            dtFechaDesde.Text = sFecha;
            dtFechaHasta.Text = sFecha;

            llenarComboLocalidades();

            dgvDatos.Rows.Clear();

            cmbCantidad.Text = "5";

            lblCantidad.Text = "0";

            txtTotal.Text = "0.00";
            txtValorMayor.Text = "0.00";
            txtValorMenor.Text = "0.00";
            chkFiltros.Checked = false;
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select top " + cmbCantidad.Text + " nombre, sum(cuenta) cuenta," + Environment.NewLine;
                sSql += "ltrim(str(sum(valor), 10, 2)) valor" + Environment.NewLine;
                sSql += "from pos_vw_producto_mas_menos_vendido" + Environment.NewLine;
                sSql += "where fecha_pedido between @fecha_inicio" + Environment.NewLine;
                sSql += "and @fecha_final" + Environment.NewLine;
                sSql += "and id_localidad = @id_localidad" + Environment.NewLine;
                sSql += "and genera_factura = 1" + Environment.NewLine;
                sSql += "group by nombre" + Environment.NewLine;

                if (chkFiltros.Checked == true)
                {
                    if (rdbMayor.Checked == true)
                        sSql += "having sum(valor) > @valor_inicial" + Environment.NewLine;

                    else if (rdbEntre.Checked == true)
                        sSql += "having sum(valor) between @valor_final and @valor_inicial" + Environment.NewLine;

                    else if (rdbMenor.Checked == true)
                        sSql += "having sum(valor) < @valor_inicial" + Environment.NewLine;
                }

                sSql += "order by sum(valor) desc";

                SqlParameter[] Parametros = new SqlParameter[5];
                Parametros[0] = new SqlParameter();
                Parametros[0].ParameterName = "@fecha_inicio";
                Parametros[0].SqlDbType = SqlDbType.DateTime;
                Parametros[0].Value = Convert.ToDateTime(dtFechaDesde.Text);

                Parametros[1] = new SqlParameter();
                Parametros[1].ParameterName = "@fecha_final";
                Parametros[1].SqlDbType = SqlDbType.DateTime;
                Parametros[1].Value = Convert.ToDateTime(dtFechaHasta.Text);

                Parametros[2] = new SqlParameter();
                Parametros[2].ParameterName = "@id_localidad";
                Parametros[2].SqlDbType = SqlDbType.Int;
                Parametros[2].Value = Convert.ToInt32(cmbLocalidades.SelectedValue);

                Parametros[3] = new SqlParameter();
                Parametros[3].ParameterName = "@valor_inicial";
                Parametros[3].SqlDbType = SqlDbType.Decimal;
                Parametros[3].Value = Convert.ToDecimal(txtValorMayor.Text);

                Parametros[4] = new SqlParameter();
                Parametros[4].ParameterName = "@valor_final";
                Parametros[4].SqlDbType = SqlDbType.VarChar;
                Parametros[4].Value = Convert.ToDecimal(txtValorMenor.Text);

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, Parametros);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                Decimal dbSuma_R = 0;

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(
                                        dtConsulta.Rows[i]["nombre"].ToString(),
                                        dtConsulta.Rows[i]["cuenta"].ToString(),
                                        dtConsulta.Rows[i]["valor"].ToString()
                                     );

                    dbSuma_R += Convert.ToDecimal(dtConsulta.Rows[i]["valor"].ToString());
                }

                txtTotal.Text = dbSuma_R.ToString("N2");
                lblCantidad.Text = dtConsulta.Rows.Count.ToString();

                if (dtConsulta.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se encuentran registros con los parámetros ingresados.";
                    ok.ShowDialog();
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

        //FUNCION PARA CREAR EL REPORTE RESUMEN
        private void crearReporteResumen()
        {
            try
            {
                string sTexto;

                sTexto = "";
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "REPORTE DE PRODUCTOS VENDIDOS".PadLeft(34, ' ') + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "FECHA DESDE: " + sFechaInicial + Environment.NewLine;
                sTexto += "FECHA HASTA: " + sFechaFinal + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "DESCRIPCION                 CANT.   TOT." + Environment.NewLine;
                sTexto += "".PadLeft(40, '-') + Environment.NewLine;

                Decimal dbTotal = 0;

                for (int i = 0; i <dgvDatos.Rows.Count; i++)
                {
                    string sNombre = dgvDatos.Rows[i].Cells["nombre_producto"].Value.ToString().Trim().ToUpper();
                    string sCantidad = dgvDatos.Rows[i].Cells["cantidad"].Value.ToString().Trim();
                    string sValor = dgvDatos.Rows[i].Cells["valor"].Value.ToString().Trim();

                    dbTotal += Convert.ToDecimal(sValor);

                    if (sNombre.Length > 28)
                    {
                        sTexto += sNombre.Substring(0, 28) + sCantidad.PadLeft(5, ' ') + sValor.PadLeft(7, ' ') + Environment.NewLine;
                    }

                    else
                    {
                        sTexto += sNombre.PadRight(28, ' ') + sCantidad.PadLeft(5, ' ') + sValor.PadLeft(7, ' ') + Environment.NewLine;
                    }
                }

                sTexto += "".PadLeft(40, '-') + Environment.NewLine;
                sTexto += "TOTAL REPORTADO:".PadRight(28, ' ') + dbTotal.ToString("N2").PadLeft(12, ' ');

                Utilitarios.frmReporteGenerico reporte = new Utilitarios.frmReporteGenerico(sTexto, 0, 0, 0, 0);
                reporte.ShowDialog();

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void txtValorMenor_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter = new Clases.ClaseValidarCaracteres();
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtValorMayor_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter = new Clases.ClaseValidarCaracteres();
            caracter.soloDecimales(sender, e, 2);
        }

        private void chkFiltros_CheckedChanged(object sender, EventArgs e)
        {
            txtValorMayor.Text = "0.00";
            txtValorMenor.Text = "0.00";
            rdbMayor.Checked = true;

            if (chkFiltros.Checked == true)
            {
                grupoFiltro.Enabled = true;                
                txtValorMenor.ReadOnly = true;
                txtValorMayor.Focus();
            }

            else
            {
                grupoFiltro.Enabled = false;
            }
        }

        private void rdbMayor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMayor.Checked == true)
            {
                txtValorMayor.Text = "0.00";
                txtValorMenor.Text = "0.00";
                txtValorMayor.ReadOnly = false;
                txtValorMenor.ReadOnly = true;
                txtValorMayor.Focus();
            }
        }

        private void rdbEntre_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEntre.Checked == true)
            {
                txtValorMayor.Text = "0.00";
                txtValorMenor.Text = "0.00";
                txtValorMayor.ReadOnly = false;
                txtValorMenor.ReadOnly = false;
                txtValorMayor.Focus();
            }
        }

        private void rdbMenor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMenor.Checked == true)
            {
                txtValorMayor.Text = "0.00";
                txtValorMenor.Text = "0.00";
                txtValorMayor.ReadOnly = true;
                txtValorMenor.ReadOnly = false;
                txtValorMenor.Focus();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmListarProductosVendidos_Load(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtFechaDesde.Text) > Convert.ToDateTime(dtFechaHasta.Text))
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "La fecha final no debe ser superior a la fecha inicial.";
                ok.ShowDialog();
                dtFechaHasta.Text = sFecha;
                return;
            }

            if (chkFiltros.Checked == true)
            {
                if (rdbMayor.Checked == true)
                {
                    if (Convert.ToDecimal(txtValorMayor.Text) <= 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "La cantidad a buscar debe ser diferente a cero.";
                        ok.ShowDialog();
                        txtValorMayor.Focus();
                        return;
                    }
                }

                else if (rdbEntre.Checked == true)
                {
                    if (Convert.ToDecimal(txtValorMayor.Text) <= 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "La cantidad inicial debe ser diferente a cero.";
                        ok.ShowDialog();
                        txtValorMayor.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtValorMenor.Text) <= 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "La cantidad final debe ser diferente a cero.";
                        ok.ShowDialog();
                        txtValorMenor.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtValorMayor.Text) < Convert.ToDecimal(txtValorMenor.Text))
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "La cantidad final debe ser superior a la cantidad inicial.";
                        ok.ShowDialog();
                        txtValorMenor.Focus();
                        txtValorMenor.Text = "0.00";
                        txtValorMenor.Focus();
                        return;
                    }
                }

                else if (rdbMenor.Checked == true)
                {
                    if (Convert.ToDecimal(txtValorMenor.Text) <= 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "La cantidad a buscar debe ser diferente a cero.";
                        ok.ShowDialog();
                        txtValorMenor.Focus();
                        return;
                    }
                }
            }

            sFechaInicial = Convert.ToDateTime(dtFechaDesde.Text).ToString("dd-MM-yyyy");
            sFechaFinal = Convert.ToDateTime(dtFechaHasta.Text).ToString("dd-MM-yyyy");

            llenarGrid();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "No hay datos a imprimir.";
                ok.ShowDialog();
                return;
            }

            crearReporteResumen();
        }
    }
}
