using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Ventanas_Consumo_Empleados
{
    public partial class frmAyudaConsumoEmpleados : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public string sIdentificacion { get; set; }
        public string sNombre { get; set; }
        public string sIdEmpleado { get; set; }
        string sSql;
        bool bRespuesta = false;
        DataTable dtConsulta;

        public frmAyudaConsumoEmpleados()
        {
            InitializeComponent();
            dgvAyudaConsumoEmpleados.Rows.Clear();
            llenarGrid(0);
        }

        //Función para llenar el grid
        private void llenarGrid(int iBandera)
        {
            try
            {
                dgvAyudaConsumoEmpleados.Rows.Clear();

                sSql = "";
                sSql += "select PER.apellidos + ' ' + " + conexion.GFun_St_esnulo() + "(PER.nombres,'') Apellidos_nombres," + Environment.NewLine;
                sSql += "PER.identificacion,PER.id_persona" + Environment.NewLine;
                sSql += "FROM cv408_cabecera_contrato CC, tp_personas PER" + Environment.NewLine;
                sSql += "where CC.id_persona = PER.id_persona" + Environment.NewLine;
                sSql += "and CC.estado = 'A'" + Environment.NewLine;
                sSql += "and PER.estado='A'" + Environment.NewLine;
                sSql += "and CC.fecha_salida is null" + Environment.NewLine;

                if (iBandera == 1)
                {
                    sSql += "and (PER.identificacion like '%" + TxtBusqueda.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or PER.apellidos like '%" + TxtBusqueda.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or PER.nombres like '%" + TxtBusqueda.Text.Trim() + "%')" + Environment.NewLine;
                }
                
                sSql += "order by PER.apellidos";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgvAyudaConsumoEmpleados.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(), 
                                dtConsulta.Rows[i].ItemArray[1].ToString(), dtConsulta.Rows[i].ItemArray[2].ToString());
                        }
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }
            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }            
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            frmNombreEmpleado empleado = new frmNombreEmpleado();
            sIdentificacion = dgvAyudaConsumoEmpleados.CurrentRow.Cells[1].Value.ToString();
            sNombre = dgvAyudaConsumoEmpleados.CurrentRow.Cells[0].Value.ToString();
            sIdEmpleado = dgvAyudaConsumoEmpleados.CurrentRow.Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dgvAyudaConsumoEmpleados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmNombreEmpleado empleado = new frmNombreEmpleado();
            sIdentificacion = dgvAyudaConsumoEmpleados.CurrentRow.Cells[1].Value.ToString();
            sNombre = dgvAyudaConsumoEmpleados.CurrentRow.Cells[0].Value.ToString();
            sIdEmpleado = dgvAyudaConsumoEmpleados.CurrentRow.Cells[2].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtBusqueda.Text.Trim() == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (TxtBusqueda.Text.Trim() == "")
                    {
                        llenarGrid(0);
                    }

                    else
                    {
                        llenarGrid(1);
                    }
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void frmAyudaConsumoEmpleados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
    }
}
