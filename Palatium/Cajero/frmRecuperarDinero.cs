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
    public partial class frmRecuperarDinero : Form
    {
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        string sCantidadGrid;

        int iIdPosMoneda;
        decimal dbValor;
        string sDescripcion;
        int iCantidad;
        decimal dbTotal;
        decimal dbCantidad;

        public frmRecuperarDinero()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //CARGAR GRID CON VALORES
        private void agregarColumnas()
        {
            try
            {                
                for (int i = 0; i < Program.dtMonedasCierre.Rows.Count; i++)
                {
                    iIdPosMoneda = Convert.ToInt32(Program.dtMonedasCierre.Rows[i][0].ToString());
                    dbValor = Convert.ToDecimal(Program.dtMonedasCierre.Rows[i][1].ToString());
                    sDescripcion = Program.dtMonedasCierre.Rows[i][2].ToString();
                    iCantidad = Convert.ToInt32(Program.dtMonedasCierre.Rows[i][3].ToString());
                    dbTotal = Convert.ToDecimal(Program.dtMonedasCierre.Rows[i][4].ToString());

                    dgvBilletes.Rows.Add(iIdPosMoneda.ToString(), dbValor.ToString(), sDescripcion.Trim(),
                                          iCantidad.ToString(), dbTotal.ToString("N2"));
                }

                dgvBilletes.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilletes.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilletes.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //dgvBilletes.CurrentCell = dgvBilletes.Rows[0].Cells[1];
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
            caracter.soloNumeros(e);
        }

        //FUNCION PARA SUMAR LOS VALORES
        private void sumarCeldas()
        {
            try
            {
                dbTotal = 0;

                for (int i = 0; i < dgvBilletes.Rows.Count; i++)
                {
                    dbTotal += Convert.ToDecimal(dgvBilletes.Rows[i].Cells["colTotal"].Value);
                }

                txtTotal.Text = dbTotal.ToString("N2");
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ALMACENAR LAS MONEDAS
        private void guardarCantidades()
        {
            for (int i = 0; i < dgvBilletes.Rows.Count; i++)
            {
                iIdPosMoneda = Convert.ToInt32(dgvBilletes.Rows[i].Cells[0].Value);
                iCantidad = Convert.ToInt32(dgvBilletes.Rows[i].Cells[3].Value);
                dbTotal = Convert.ToDecimal(dgvBilletes.Rows[i].Cells[4].Value);

                for (int j = 0; j < Program.dtMonedasCierre.Rows.Count; j++)
                {
                    if (Convert.ToInt32(Program.dtMonedasCierre.Rows[j][0].ToString()) == iIdPosMoneda)
                    {
                        Program.dtMonedasCierre.Rows[j][3] = iCantidad.ToString();
                        Program.dtMonedasCierre.Rows[j][4] = dbTotal.ToString("N2");
                        break;
                    }
                }
            }
        }

        #endregion

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
                    sCantidadGrid = dgvBilletes.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();

                    if ((sCantidadGrid != null) && (sCantidadGrid != ""))
                    {
                        dbValor = Convert.ToDecimal(dgvBilletes.Rows[e.RowIndex].Cells[1].Value.ToString());
                        dbCantidad = Convert.ToDecimal(dgvBilletes.Rows[e.RowIndex].Cells[3].Value.ToString());
                        dbTotal = dbValor * dbCantidad;
                        dgvBilletes.Rows[e.RowIndex].Cells[4].Value = dbTotal.ToString("N2");
                    }

                    else
                    {
                        dgvBilletes.Rows[e.RowIndex].Cells[3].Value = "0";
                        dgvBilletes.Rows[e.RowIndex].Cells[4].Value = "0.00";
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

        private void frmRecuperarDinero_Load(object sender, EventArgs e)
        {
            agregarColumnas();
        }

        private void frmRecuperarDinero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvBilletes.Rows.Count; i++)
            {
                dgvBilletes.Rows[i].Cells["colCantidad"].Value = "0";
                dgvBilletes.Rows[i].Cells["colTotal"].Value = "0.00";
            }

            txtTotal.Text = "0.00";
        }

        private void frmRecuperarDinero_FormClosing(object sender, FormClosingEventArgs e)
        {
            guardarCantidades();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarCantidades();
            this.Close();
        }
    }
}
