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
    public partial class frmIngresarDineroCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        double dValor, dCantidad, dTotal, dSuma;

        string sValorGrid;
        string sCantidadGrid;
        string sSql;

        int iMoneda01, iMoneda05, iMoneda10, iMoneda25, iMoneda50;
        int iBillete1, iBillete2, iBillete5, iBillete10, iBillete20, iBillete50, iBillete100;

        int iIdPosCierreCajero;
        int iIdLocalidad;

        public Decimal dbCajaInicial;

        DataTable dtConsulta;

        bool bRespuesta;

        public string[,] sContarDinero = { { "1", "0", "0.00" }, { "2", "0", "0.00" }, { "5", "0", "0.00" }, { "10", "0", "0.00" }, { "20", "0", "0.00" }, { "50", "0", "0.00" }, { "100", "0", "0.00" }, { "1 Ctvo.", "0", "0.00" }, { "5 Ctvos.", "0", "0.00" }, { "10 Ctvos.", "0", "0.00" }, { "25 Ctvos.", "0", "0.00" }, { "50 Ctvos.", "0", "0.00" } };

        public frmIngresarDineroCaja(int iIdPosCierreCajero_P, int iIdLocalidad_P)
        {
            this.iIdPosCierreCajero = iIdPosCierreCajero_P;
            this.iIdLocalidad = iIdLocalidad_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR GRID CON VALORES
        private void agregarColumnas()
        {
            try
            {
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

        //FUNCION PARA INGRESAR EL EFECTIVO
        private void guardarEfectivo()
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
                sSql += "insert into pos_monedas (" + Environment.NewLine;
                sSql += "id_pos_cierre_cajero, id_localidad, moneda01, moneda05, moneda10," + Environment.NewLine;
                sSql += "moneda25, moneda50, billete1, billete2, billete5, billete10," + Environment.NewLine;
                sSql += "billete20, billete50, billete100, estado, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql += "terminal_ingreso, tipo_ingreso, id_pos_jornada)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdPosCierreCajero + ", " + Program.iIdLocalidad + ", " + iMoneda01 + ", ";
                sSql += iMoneda05 + ", " + iMoneda10 + ", " + iMoneda25 + ", " + iMoneda50 + "," + Environment.NewLine;
                sSql += iBillete1 + ", " + iBillete2 + ", " + iBillete5 + ", " + iBillete10 + ", " + iBillete20 + ",";
                sSql += iBillete50 + ", " + iBillete100 + "," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', 0, " + Program.iJORNADA + ")";

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
                sSql += "where id_pos_cierre_cajero = " + Program.iIdPosCierreCajero;

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
                this.DialogResult = DialogResult.OK;
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
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
                sSql += "select * from pos_monedas" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdPosCierreCajero + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
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

                    btnGuardar.Visible = false;
                    btnLimpiar.Visible = false;

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

                    dgvBilletes.Columns[1].ReadOnly = true;
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

        #endregion

        private void frmIngresarDineroCaja_Load(object sender, EventArgs e)
        {
            agregarColumnas();
            consultarMonedas();
        }

        private void frmIngresarDineroCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            sContarDinero[0, 0] = "1";
            sContarDinero[0, 1] = "0";
            sContarDinero[0, 2] = "0.00";

            sContarDinero[1, 0] = "2";
            sContarDinero[1, 1] = "0";
            sContarDinero[1, 2] = "0.00";

            sContarDinero[2, 0] = "5";
            sContarDinero[2, 1] = "0";
            sContarDinero[2, 2] = "0.00";

            sContarDinero[3, 0] = "10";
            sContarDinero[3, 1] = "0";
            sContarDinero[3, 2] = "0.00";

            sContarDinero[4, 0] = "20";
            sContarDinero[4, 1] = "0";
            sContarDinero[4, 2] = "0.00";

            sContarDinero[5, 0] = "50";
            sContarDinero[5, 1] = "0";
            sContarDinero[5, 2] = "0.00";

            sContarDinero[6, 0] = "100";
            sContarDinero[6, 1] = "0";
            sContarDinero[6, 2] = "0.00";

            sContarDinero[7, 0] = "1 Ctvo.";
            sContarDinero[7, 1] = "0";
            sContarDinero[7, 2] = "0.00";

            sContarDinero[8, 0] = "5 Ctvos.";
            sContarDinero[8, 1] = "0";
            sContarDinero[8, 2] = "0.00";

            sContarDinero[9, 0] = "10 Ctvos.";
            sContarDinero[9, 1] = "0";
            sContarDinero[9, 2] = "0.00";

            sContarDinero[10, 0] = "25 Ctvos.";
            sContarDinero[10, 1] = "0";
            sContarDinero[10, 2] = "0.00";

            sContarDinero[11, 0] = "50 Ctvos.";
            sContarDinero[11, 1] = "0";
            sContarDinero[11, 2] = "0.00";

            dgvBilletes.Rows.Clear();
            agregarColumnas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el efectivo ingresado?" + Environment.NewLine + "No se podrá editar los registros.";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                guardarEfectivo();
            }
        }
    }
}
