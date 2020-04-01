using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Cajero
{
    public partial class frmContarDinero : Form
    {
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        double dValor, dCantidad, dTotal, dSuma;
        string sValorGrid;
        string sCantidadGrid;

        public frmContarDinero()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR GRID CON VALORES
        private void agregarColumnas()
        {
            try
            {
                for (int i = 0; i < Program.sContarDinero.Length / 3; i++)
                {
                    dgvBilletes.Rows.Add(Program.sContarDinero[i, 0], Program.sContarDinero[i, 1], Program.sContarDinero[i, 2]);
                }

                dgvBilletes.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilletes.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilletes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvBilletes.CurrentCell = dgvBilletes.Rows[0].Cells[1];
                sumarCeldas();
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
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

                    Program.sContarDinero[i, 1] = dgvBilletes.Rows[i].Cells[1].Value.ToString();
                    Program.sContarDinero[i, 2] = dgvBilletes.Rows[i].Cells[2].Value.ToString();
                }

                txtTotal.Text = dSuma.ToString("N2");
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmContarDinero_Load(object sender, EventArgs e)
        {
            agregarColumnas();
        }

        private void frmContarDinero_KeyDown(object sender, KeyEventArgs e)
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
                catchMensaje.LblMensaje.Text = ex.Message;
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
            Program.sContarDinero[0, 0] = "1";
            Program.sContarDinero[0, 1] = "0";
            Program.sContarDinero[0, 2] = "0.00";

            Program.sContarDinero[1, 0] = "2";
            Program.sContarDinero[1, 1] = "0";
            Program.sContarDinero[1, 2] = "0.00";

            Program.sContarDinero[2, 0] = "5";
            Program.sContarDinero[2, 1] = "0";
            Program.sContarDinero[2, 2] = "0.00";

            Program.sContarDinero[3, 0] = "10";
            Program.sContarDinero[3, 1] = "0";
            Program.sContarDinero[3, 2] = "0.00";

            Program.sContarDinero[4, 0] = "20";
            Program.sContarDinero[4, 1] = "0";
            Program.sContarDinero[4, 2] = "0.00";

            Program.sContarDinero[5, 0] = "50";
            Program.sContarDinero[5, 1] = "0";
            Program.sContarDinero[5, 2] = "0.00";

            Program.sContarDinero[6, 0] = "100";
            Program.sContarDinero[6, 1] = "0";
            Program.sContarDinero[6, 2] = "0.00";

            Program.sContarDinero[7, 0] = "1 Ctvo.";
            Program.sContarDinero[7, 1] = "0";
            Program.sContarDinero[7, 2] = "0.00";

            Program.sContarDinero[8, 0] = "5 Ctvos.";
            Program.sContarDinero[8, 1] = "0";
            Program.sContarDinero[8, 2] = "0.00";

            Program.sContarDinero[9, 0] = "10 Ctvos.";
            Program.sContarDinero[9, 1] = "0";
            Program.sContarDinero[9, 2] = "0.00";

            Program.sContarDinero[10, 0] = "25 Ctvos.";
            Program.sContarDinero[10, 1] = "0";
            Program.sContarDinero[10, 2] = "0.00";

            Program.sContarDinero[11, 0] = "50 Ctvos.";
            Program.sContarDinero[11, 1] = "0";
            Program.sContarDinero[11, 2] = "0.00";

            dgvBilletes.Rows.Clear();
            agregarColumnas();
        }
    }
}
