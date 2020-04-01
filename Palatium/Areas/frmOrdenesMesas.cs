using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Areas
{
    public partial class frmOrdenesMesas : Form
    {
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeSiNo SiNo;

        string sNombreMesa;

        DataTable dtConsulta;
        public DataTable dtCombinar;

        int iIdMesa;
        int iSuma;

        CheckBox[] chkOrdenes;
        CheckBox chkSeleccionado;

        public frmOrdenesMesas(int iIdMesa_R, DataTable dtConsulta_R, string sNombreMesa_R)
        {
            this.iIdMesa = iIdMesa_R;
            this.dtConsulta = dtConsulta_R;
            this.sNombreMesa = sNombreMesa_R;
            InitializeComponent();        
        }

        #region FUNCIONES DE CONTROL DE BOTONES

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        #endregion

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CREAR EL PANEL CONTENEDOR
        private void crearPanelContenedor()
        {
            chkOrdenes = new CheckBox[dtConsulta.Rows.Count];
            iSuma = 10;

            for (int i = 0; i< dtConsulta.Rows.Count; i++)
            {
                chkOrdenes[i] = new CheckBox();
                chkOrdenes[i].Checked = false;
                chkOrdenes[i].Text = sNombreMesa + " - ORDEN No." + dtConsulta.Rows[i][2].ToString().Trim();
                chkOrdenes[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                chkOrdenes[i].Name = dtConsulta.Rows[i][0].ToString();
                chkOrdenes[i].Size = new System.Drawing.Size(345, 28);
                chkOrdenes[i].Location = new System.Drawing.Point(11, iSuma);
                chkOrdenes[i].CheckedChanged += check_Seleccion;
                this.Controls.Add(chkOrdenes[i]);
                pnlContenedor.Controls.Add(chkOrdenes[i]);
                iSuma += 40;
            }
        }

        //FUNCION PARA EL CAMBIO DE ESTADO DE CHECKBOX
        private void check_Seleccion(object sender, EventArgs e)
        {
            chkSeleccionado = sender as CheckBox;

            if (chkSeleccionado.Checked == true)
            {
                DataRow row = dtCombinar.NewRow();
                row["idMesa"] = iIdMesa.ToString();
                row["idPedido"] = chkSeleccionado.Name;
                dtCombinar.Rows.Add(row);
            }

            else
            {
                if (dtCombinar.Rows.Count == 1)
                {
                    crearDataTable();
                }

                else
                {
                    //ELIMINAR LA MESA A A COMBINAR
                    for (int i = dtCombinar.Rows.Count - 1; i >= 0; i--)
                    {
                        if (Convert.ToInt32(dtCombinar.Rows[i][1].ToString()) == Convert.ToInt32(chkSeleccionado.Name))
                        {
                            dtCombinar.Rows.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void crearDataTable()
        {
            try
            {
                dtCombinar = new DataTable();
                dtCombinar.Clear();
                dtCombinar.Columns.Add("idMesa");
                dtCombinar.Columns.Add("idPedido");
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmOrdenesMesas_Load(object sender, EventArgs e)
        {
            crearPanelContenedor();
            crearDataTable();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnIngresar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnIngresar);
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnIngresar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnIngresar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }
    }
}
