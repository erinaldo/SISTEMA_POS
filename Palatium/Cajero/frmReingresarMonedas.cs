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
    public partial class frmReingresarMonedas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        double dValor, dCantidad, dTotal, dSuma;

        string sValorGrid;
        string sCantidadGrid;
        string sSql;

        int iMoneda01, iMoneda05, iMoneda10, iMoneda25, iMoneda50;
        int iBillete1, iBillete2, iBillete5, iBillete10, iBillete20, iBillete50, iBillete100;

        int iIdPosCierreCajero;
        int iIdPosMoneda;
        int iIdLocalidad;

        public Decimal dbCajaInicial;

        DataTable dtConsulta;

        bool bRespuesta;

        public string[,] sContarDinero = { { "1", "0", "0.00" }, { "2", "0", "0.00" }, { "5", "0", "0.00" }, { "10", "0", "0.00" }, { "20", "0", "0.00" }, { "50", "0", "0.00" }, { "100", "0", "0.00" }, { "1 Ctvo.", "0", "0.00" }, { "5 Ctvos.", "0", "0.00" }, { "10 Ctvos.", "0", "0.00" }, { "25 Ctvos.", "0", "0.00" }, { "50 Ctvos.", "0", "0.00" } };


        public frmReingresarMonedas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR GRID CON VALORES
        private void agregarColumnas()
        {
            try
            {
                dgvBilletes.Rows.Clear();

                for (int i = 0; i < sContarDinero.Length / 3; i++)
                {
                    dgvBilletes.Rows.Add(sContarDinero[i, 0], "0", "0.00");
                }

                dgvBilletes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilletes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilletes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvBilletes.CurrentCell = dgvBilletes.Rows[0].Cells[1];
                sumarCeldas();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {

            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        //VALIDAR SOLO NUMEROS
        private void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }

            else if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }
        }

        //FUNCION PARA SUMAR LOS VALORES
        private void sumarCeldas()
        {
            try
            {
                dSuma = 0;

                for (int i = 0; i < dgvBilletes.Rows.Count; i++)
                {
                    dSuma = dSuma + Convert.ToDouble(dgvBilletes.Rows[i].Cells[2].Value);

                    sContarDinero[i, 1] = dgvBilletes.Rows[i].Cells[1].Value.ToString();
                    sContarDinero[i, 2] = dgvBilletes.Rows[i].Cells[2].Value.ToString();
                }

                txtTotal.Text = dSuma.ToString("N2");
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE JORNADAS
        private void llenarComboJornadas()
        {
            try
            {
                sSql = "";
                sSql += "select CC.id_jornada, J.descripcion" + Environment.NewLine;
                sSql += "from pos_cierre_cajero CC INNER JOIN" + Environment.NewLine;
                sSql += "pos_jornada J ON J.id_pos_jornada = CC.id_jornada" + Environment.NewLine;
                sSql += "and CC.estado = 'A'" + Environment.NewLine;
                sSql += "and J.estado = 'A'" + Environment.NewLine;
                sSql += "where CC.fecha_apertura = '" + Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and CC.id_localidad = " + cmbLocalidades.SelectedValue;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    cmbJornada.DisplayMember = "descripcion";
                    cmbJornada.ValueMember = "id_jornada";
                    cmbJornada.DataSource = dtConsulta;

                    btnBuscar.Visible = true;
                    btnGuardar.Visible = true;
                }

                else
                {
                    cmbJornada.DataSource = null;
                    cmbJornada.Items.Clear();

                    btnBuscar.Visible = false;
                    btnGuardar.Visible = false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR SI HAY UN INGRESO DE EFECTIVO
        private void consultarMonedas()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_cierre_cajero" + Environment.NewLine;
                sSql += "from pos_cierre_cajero" + Environment.NewLine;
                sSql += "where id_jornada = " + cmbJornada.SelectedValue + Environment.NewLine;
                sSql += "and id_localidad = " + cmbLocalidades.SelectedValue + Environment.NewLine;
                sSql += "and fecha_apertura = '" + Convert.ToDateTime(txtFecha.Text.Trim()).ToString("yyyy-MM-dd") + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    ok.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    ok.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok.lblMensaje.Text = "No existe información con los parámetros seleccionados.";
                    ok.ShowDialog();
                    return;
                }

                iIdPosCierreCajero = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                sSql = "";
                sSql += "select * from pos_monedas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and tipo_ingreso = 0" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    ok.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    ok.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    ok.lblMensaje.Text = "No existe información con los parámetros seleccionados.";
                    ok.ShowDialog();
                    dgvBilletes.Columns["colCantidad"].ReadOnly = true;
                    return;
                }

                iIdPosMoneda = Convert.ToInt32(dtConsulta.Rows[0]["id_pos_moneda"].ToString());
                dgvBilletes.Columns["colCantidad"].ReadOnly = false;
                //grupoDatos.Enabled = false;

                if (dtConsulta.Rows.Count > 0)
                {
                    dgvBilletes.Rows[0].Cells[1].Value = dtConsulta.Rows[0]["billete1"].ToString();
                    dgvBilletes.Rows[1].Cells[1].Value = dtConsulta.Rows[0]["billete2"].ToString();
                    dgvBilletes.Rows[2].Cells[1].Value = dtConsulta.Rows[0]["billete5"].ToString();
                    dgvBilletes.Rows[3].Cells[1].Value = dtConsulta.Rows[0]["billete10"].ToString();
                    dgvBilletes.Rows[4].Cells[1].Value = dtConsulta.Rows[0]["billete20"].ToString();
                    dgvBilletes.Rows[5].Cells[1].Value = dtConsulta.Rows[0]["billete50"].ToString();
                    dgvBilletes.Rows[6].Cells[1].Value = dtConsulta.Rows[0]["billete100"].ToString();
                    dgvBilletes.Rows[7].Cells[1].Value = dtConsulta.Rows[0]["moneda01"].ToString();
                    dgvBilletes.Rows[8].Cells[1].Value = dtConsulta.Rows[0]["moneda05"].ToString();
                    dgvBilletes.Rows[9].Cells[1].Value = dtConsulta.Rows[0]["moneda10"].ToString();
                    dgvBilletes.Rows[10].Cells[1].Value = dtConsulta.Rows[0]["moneda25"].ToString();
                    dgvBilletes.Rows[11].Cells[1].Value = dtConsulta.Rows[0]["moneda50"].ToString();

                    //btnGuardar.Visible = false;
                    //btnLimpiar.Visible = false;

                    dSuma = 0;

                    for (int i = 0; i < dgvBilletes.Rows.Count; i++)
                    {
                        dCantidad = Convert.ToDouble(dgvBilletes.Rows[i].Cells[1].Value);

                        if (esNumero(dgvBilletes.Rows[i].Cells[0].Value.ToString()) == true)
                        {
                            dValor = Convert.ToDouble(dgvBilletes.Rows[i].Cells[0].Value.ToString());
                        }

                        else
                        {
                            sValorGrid = dgvBilletes.Rows[i].Cells[0].Value.ToString().Substring(0, 2).Trim();
                            dValor = Convert.ToDouble(sValorGrid) / 100;
                        }

                        dTotal = dCantidad * dValor;
                        dSuma += dTotal;

                        dgvBilletes.Rows[i].Cells[2].Value = dTotal.ToString("N2");
                    }

                    txtTotal.Text = dSuma.ToString("N2");

                    //dgvBilletes.Columns[1].ReadOnly = true;
                    dgvBilletes.ClearSelection();
                }

                else
                {
                    btnGuardar.Visible = true;
                    btnLimpiar.Visible = true;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR LAS MONEDAS
        private void actualizarMonedas()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                iMoneda01 = Convert.ToInt32(dgvBilletes.Rows[7].Cells[1].Value);
                iMoneda05 = Convert.ToInt32(dgvBilletes.Rows[8].Cells[1].Value);
                iMoneda10 = Convert.ToInt32(dgvBilletes.Rows[9].Cells[1].Value);
                iMoneda25 = Convert.ToInt32(dgvBilletes.Rows[10].Cells[1].Value);
                iMoneda50 = Convert.ToInt32(dgvBilletes.Rows[11].Cells[1].Value);

                iBillete1 = Convert.ToInt32(dgvBilletes.Rows[0].Cells[1].Value);
                iBillete2 = Convert.ToInt32(dgvBilletes.Rows[1].Cells[1].Value);
                iBillete5 = Convert.ToInt32(dgvBilletes.Rows[2].Cells[1].Value);
                iBillete10 = Convert.ToInt32(dgvBilletes.Rows[3].Cells[1].Value);
                iBillete20 = Convert.ToInt32(dgvBilletes.Rows[4].Cells[1].Value);
                iBillete50 = Convert.ToInt32(dgvBilletes.Rows[5].Cells[1].Value);
                iBillete100 = Convert.ToInt32(dgvBilletes.Rows[6].Cells[1].Value);

                dbCajaInicial = Convert.ToDecimal(txtTotal.Text.Trim());

                sSql = "";
                sSql += "update pos_monedas set" + Environment.NewLine;
                sSql += "moneda01 = " + iMoneda01 + "," + Environment.NewLine;
                sSql += "moneda05 = " + iMoneda05 + "," + Environment.NewLine;
                sSql += "moneda10 = " + iMoneda10 + "," + Environment.NewLine;
                sSql += "moneda25 = " + iMoneda25 + "," + Environment.NewLine;
                sSql += "moneda50 = " + iMoneda50 + "," + Environment.NewLine;
                sSql += "billete1 = " + iBillete1 + "," + Environment.NewLine;
                sSql += "billete2 = " + iBillete2 + "," + Environment.NewLine;
                sSql += "billete5 = " + iBillete5 + "," + Environment.NewLine;
                sSql += "billete10 = " + iBillete10 + "," + Environment.NewLine;
                sSql += "billete20 = " + iBillete20 + "," + Environment.NewLine;
                sSql += "billete50 = " + iBillete50 + "," + Environment.NewLine;
                sSql += "billete100 = " + iBillete100 + Environment.NewLine;
                sSql += "where id_pos_moneda = " + iIdPosMoneda + Environment.NewLine;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cierre_cajero set" + Environment.NewLine;
                sSql += "caja_inicial = " + dbCajaInicial + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro de efectivo ingresado éxitosamente.";
                ok.ShowDialog();

                agregarColumnas();
                consultarMonedas();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmReingresarMonedas_Load(object sender, EventArgs e)
        {
            cmbLocalidades.SelectedIndexChanged -= new EventHandler(cmbLocalidades_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidades.SelectedIndexChanged += new EventHandler(cmbLocalidades_SelectedIndexChanged);

            agregarColumnas();
            llenarComboJornadas();
        }

        private void txtFecha_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                agregarColumnas();
                llenarComboJornadas();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbLocalidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                agregarColumnas();
                llenarComboJornadas();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            agregarColumnas();
            consultarMonedas();
        }

        private void dgvBilletes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox texto = e.Control as TextBox;

            if (texto != null)
            {
                DataGridViewTextBoxEditingControl dTexto = (DataGridViewTextBoxEditingControl)e.Control;
                dTexto.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
                dTexto.KeyPress += new KeyPressEventHandler(dText_KeyPress);
            }
        }

        private void dgvBilletes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvBilletes.Columns[e.ColumnIndex].Name == "colCantidad")
                {
                    if (dgvBilletes.Rows[e.RowIndex].Cells[1].Value == null)
                    {
                        dgvBilletes.Rows[e.RowIndex].Cells[1].Value = "0";
                    }

                    sCantidadGrid = dgvBilletes.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    if ((sCantidadGrid != null) && (sCantidadGrid != ""))
                    {

                        if (esNumero(dgvBilletes.Rows[e.RowIndex].Cells[0].Value.ToString()) == true)
                        {
                            dValor = Convert.ToDouble(dgvBilletes.Rows[e.RowIndex].Cells[0].Value.ToString());
                        }

                        else
                        {
                            sValorGrid = dgvBilletes.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 2).Trim();
                            dValor = Convert.ToDouble(sValorGrid) / 100;
                        }

                        dCantidad = Convert.ToDouble(dgvBilletes.Rows[e.RowIndex].Cells[1].Value.ToString());
                        dTotal = dValor * dCantidad;
                        dgvBilletes.Rows[e.RowIndex].Cells[2].Value = dTotal.ToString("N2");
                    }

                    else
                    {
                        dgvBilletes.Rows[e.RowIndex].Cells[1].Value = "0";
                        dgvBilletes.Rows[e.RowIndex].Cells[2].Value = "0.00";
                    }
                }

                sumarCeldas();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            //grupoDatos.Enabled = true;
            agregarColumnas();
            consultarMonedas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                actualizarMonedas();
            }
        }
    }
}
