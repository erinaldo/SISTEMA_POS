using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium
{
    public partial class Cortesias : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sIdPedido;
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta = false;

        public Cortesias(string sIdOrden)
        {
            this.sIdPedido = sIdOrden;
            InitializeComponent();
            Program.iBanderaCortesia = 0;
        }

        private void btnConsumoAlimentos_Click(object sender, EventArgs e)
        {           
            MotivoCancelacionCortesias motivoCortesia = new MotivoCancelacionCortesias();
            AddOwnedForm(motivoCortesia);
            motivoCortesia.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            calcularValores();      
        }

        //Función para calcular los nuevos valores
        private void calcularValores()
        {
            Orden ord = Owner as Orden;
            Orden o = Owner as Orden;

            o.dgvPedido.Rows.Clear();
            int bandera = 0;

            for (int i = 0; i < dgvPedido.Rows.Count; i++)
            {
                Double cantidad = 0;
                bandera = 0;
                double dValor_1;

                for (int j = 0; j < o.dgvPedido.Rows.Count; j++)
                {
                    if ((dgvPedido.Rows[i].Cells["idProducto"].Value.ToString() == o.dgvPedido.Rows[j].Cells["idProducto"].Value.ToString()))
                    {
                        if ((Convert.ToDouble(dgvPedido.Rows[i].Cells["valuni"].Value) == Convert.ToDouble(o.dgvPedido.Rows[j].Cells["valuni"].Value)) && (Convert.ToDouble(dgvPedido.Rows[i].Cells["cortesia"].Value) == Convert.ToDouble(o.dgvPedido.Rows[j].Cells["cortesia"].Value)) && (Convert.ToDouble(dgvPedido.Rows[i].Cells["cancelar"].Value) == Convert.ToDouble(o.dgvPedido.Rows[j].Cells["cancelar"].Value)) && ((Convert.ToDouble(dgvPedido.Rows[i].Cells["cancelar"].Value) != 1)))
                        {

                            bandera = 1;

                            cantidad = Convert.ToInt32(o.dgvPedido.Rows[j].Cells["cantidad"].Value.ToString().Trim());
                            cantidad = cantidad + 1;

                            o.dgvPedido.Rows[j].Cells["cantidad"].Value = cantidad;
                            o.dgvPedido.Rows[j].Cells["valor"].Value = (cantidad * Convert.ToDouble(o.dgvPedido.Rows[j].Cells["valuni"].Value)).ToString("N2");
                        }

                    }
                }

                if (bandera == 0)
                {
                    if ((Convert.ToDouble(dgvPedido[4, i].Value) == 0 && (Convert.ToDouble(dgvPedido[8, i].Value) > 0)))
                    {
                        if ((Convert.ToDouble(dgvPedido[4, i].Value) == 0) && (Convert.ToDouble(dgvPedido[8, i].Value) != 1))
                        {
                            dValor_1 = Convert.ToDouble(dgvPedido[4, i].Value);

                            o.dgvPedido.Rows.Add(new string[] {
                            Convert.ToString(dgvPedido[0, i].Value),
                            Convert.ToString(dgvPedido[1, i].Value),
                            Convert.ToString(dgvPedido[2, i].Value),
                            Convert.ToString(dgvPedido[0, i].Value),
                            dValor_1.ToString("N2"),
                            Convert.ToString(dgvPedido[5, i].Value),
                            Convert.ToString(dgvPedido[6, i].Value),
                            Convert.ToString(dgvPedido[7, i].Value),
                             "1",
                             Convert.ToString(dgvPedido[9, i].Value),
                             Convert.ToString(dgvPedido[10, i].Value),
                             Convert.ToString(dgvPedido[11, i].Value),
                             Convert.ToString(dgvPedido[12, i].Value),
                             Convert.ToString(dgvPedido[13, i].Value),
                             Convert.ToString(dgvPedido[14, i].Value),
                             Convert.ToString(dgvPedido[15, i].Value),
                             Convert.ToString(dgvPedido[16, i].Value)
                             });
                        }
                        else if (Convert.ToDouble(dgvPedido[8, i].Value) > 0)
                        {
                            dValor_1 = Convert.ToDouble(dgvPedido[4, i].Value);

                            o.dgvPedido.Rows.Add(new string[] {
                            Convert.ToString(dgvPedido[0, i].Value),
                            Convert.ToString(dgvPedido[1, i].Value),
                            Convert.ToString(dgvPedido[2, i].Value),
                            Convert.ToString(dgvPedido[3, i].Value),
                            dValor_1.ToString("N2"),
                            Convert.ToString(dgvPedido[5, i].Value),
                            Convert.ToString(dgvPedido[6, i].Value),
                            Convert.ToString(dgvPedido[7, i].Value),
                             "1",
                             Convert.ToString(dgvPedido[9, i].Value),
                             Convert.ToString(dgvPedido[10, i].Value),
                             Convert.ToString(dgvPedido[11, i].Value),
                             Convert.ToString(dgvPedido[12, i].Value),
                             Convert.ToString(dgvPedido[13, i].Value),
                             Convert.ToString(dgvPedido[14, i].Value),
                             Convert.ToString(dgvPedido[15, i].Value),
                             Convert.ToString(dgvPedido[16, i].Value)
                             });
                        }
                        else
                        {
                            o.dgvPedido.Rows.Add(new string[] {
                            Convert.ToString(dgvPedido[0, i].Value),
                            Convert.ToString(dgvPedido[1, i].Value),
                            Convert.ToString(dgvPedido[2, i].Value),
                            Convert.ToString(dgvPedido[3, i].Value),
                            Convert.ToString(dgvPedido[3, i].Value),
                            Convert.ToString(dgvPedido[5, i].Value),
                            Convert.ToString(dgvPedido[6, i].Value),
                            Convert.ToString(dgvPedido[7, i].Value),
                             Convert.ToString(dgvPedido[8, i].Value),
                             Convert.ToString(dgvPedido[9, i].Value),
                             Convert.ToString(dgvPedido[10, i].Value),
                             Convert.ToString(dgvPedido[11, i].Value),
                             Convert.ToString(dgvPedido[12, i].Value),
                             Convert.ToString(dgvPedido[13, i].Value),
                             Convert.ToString(dgvPedido[14, i].Value),
                             Convert.ToString(dgvPedido[15, i].Value),
                             Convert.ToString(dgvPedido[16, i].Value)
                             });
                        }
                        
                    }
                    else
                    {
                        dValor_1 = Convert.ToDouble(dgvPedido[4, i].Value);

                        o.dgvPedido.Rows.Add(new string[] {
                            Convert.ToString(dgvPedido[0, i].Value),
                            Convert.ToString(dgvPedido[1, i].Value),
                            Convert.ToString(dgvPedido[2, i].Value),
                            Convert.ToString(dgvPedido[3, i].Value),
                            dValor_1.ToString("N2"),
                            Convert.ToString(dgvPedido[5, i].Value),
                            Convert.ToString(dgvPedido[6, i].Value),
                            Convert.ToString(dgvPedido[7, i].Value),
                             "0",
                             "",
                             Convert.ToString(dgvPedido[10, i].Value),
                             Convert.ToString(dgvPedido[11, i].Value),
                             Convert.ToString(dgvPedido[12, i].Value),
                             Convert.ToString(dgvPedido[13, i].Value),
                             Convert.ToString(dgvPedido[14, i].Value),
                             Convert.ToString(dgvPedido[15, i].Value),
                             Convert.ToString(dgvPedido[16, i].Value)
                             });
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Cortesias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
