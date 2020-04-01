using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmReordenarItems : Form
    {
        Clases.ClaseReorden reorden = new Clases.ClaseReorden();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Button botonOrden;

        public frmReordenarItems()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LOS MESEROS
        public void mostrarReorden()
        {
            Button[,] boton = new Button[10, 10];
            int h = 0;

            //Program.saldo = double.Parse(txt_saldo.Text);
            if (reorden.llenarDatos() == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        boton[i, j] = new Button();
                        boton[i, j].Click += boton_clic_reorden;
                        //boton[i, j].Enter += boton_enter;
                        //boton[i, j].Leave += boton_leave;
                        boton[i, j].Width = 125;
                        boton[i, j].Height = 75;
                        boton[i, j].Top = i * 75;
                        boton[i, j].Left = j * 125;
                        boton[i, j].BackColor = Color.Cyan;

                        if (h == reorden.iCuenta)
                        {
                            break;
                        }

                        boton[i, j].Font = new Font("Consolas", 11);
                        //En el tag se guarda el código de la seccion de la mesa
                        boton[i, j].Tag = reorden.reOrden[h].sIdOrden;
                        //En el text muestra la descripción
                        boton[i, j].Text = reorden.reOrden[h].sDescripcion;

                        this.Controls.Add(boton[i, j]);
                        pnlReorden.Controls.Add(boton[i, j]);
                        h++;
                    }
                }
            }
            else
            {
                ok.LblMensaje.Text = "No hay ninguna seccion de mesas registrada en el sistema.";
                ok.ShowDialog();
            }
        }

        //PARA EVENTO CLIC DE BOTONES DEL PANEL REORDEN
        public void boton_clic_reorden(object sender, EventArgs e)
        {
            botonOrden = sender as Button;

            dgvPedido.CurrentRow.Cells["colOrdenamiento"].Value = botonOrden.Text;
            dgvPedido.CurrentRow.Cells["colIdOrden"].Value = botonOrden.Tag.ToString(); ;
        }

        #endregion

        private void frmReordenarItems_Load(object sender, EventArgs e)
        {
            mostrarReorden();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Orden ord = Owner as Orden;

                for (int i = 0; i < dgvPedido.Rows.Count; i++)
                {
                    if ((dgvPedido.Rows[i].Cells["colIdOrden"].Value != null) && (dgvPedido.Rows[i].Cells["colIdOrden"].Value != ""))
                    {
                        ord.dgvPedido.Rows[i].Cells["colOrdenamiento"].Value = dgvPedido.Rows[i].Cells["colOrdenamiento"].Value.ToString();
                        ord.dgvPedido.Rows[i].Cells["colIdOrden"].Value = dgvPedido.Rows[i].Cells["colIdOrden"].Value.ToString();
                    }
                }

                this.Close();
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
