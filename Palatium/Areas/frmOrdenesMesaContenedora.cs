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
    public partial class frmOrdenesMesaContenedora : Form
    {
        VentanasMensajes.frmMensajeCatch catchMensaje;
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeSiNo SiNo;

        string sNombreMesa;

        DataTable dtConsulta;
        public DataTable dtCombinar;

        int iIdMesa;
        int iSuma;

        Label[] lblOrdenes;
        Button[] btnVisualizar;
        Button[] btnSeleccionar;

        Button btnVer;
        Button btnSeleccion;

        ToolTip ttMensaje = new ToolTip();

        public frmOrdenesMesaContenedora(int iIdMesa_R, DataTable dtConsulta_R, string sNombreMesa_R)
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
            lblOrdenes = new Label[dtConsulta.Rows.Count];
            btnVisualizar = new Button[dtConsulta.Rows.Count];
            btnSeleccionar = new Button[dtConsulta.Rows.Count];

            iSuma = 10;

            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                lblOrdenes[i] = new Label();
                lblOrdenes[i].Text = sNombreMesa + " - ORDEN No." + dtConsulta.Rows[i][1].ToString().Trim();
                lblOrdenes[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblOrdenes[i].Name = dtConsulta.Rows[i][0].ToString();
                lblOrdenes[i].Size = new System.Drawing.Size(296, 24);
                lblOrdenes[i].Location = new System.Drawing.Point(11, iSuma);

                btnVisualizar[i] = new Button();
                btnVisualizar[i].Image = Properties.Resources.ver_precuenta_combinar;
                btnVisualizar[i].Name = dtConsulta.Rows[i][0].ToString();
                btnVisualizar[i].Size = new System.Drawing.Size(25, 24);
                btnVisualizar[i].Location = new System.Drawing.Point(334, iSuma);
                btnVisualizar[i].FlatAppearance.BorderSize = 0;
                btnVisualizar[i].FlatStyle = FlatStyle.Flat;
                btnVisualizar[i].ForeColor = Color.Transparent;
                btnVisualizar[i].AccessibleName = dtConsulta.Rows[i][2].ToString().Trim();
                btnVisualizar[i].Click += boton_clic_visualizar;
                ttMensaje.SetToolTip(btnVisualizar[i], "Clic aquí para visualizar la comanda No. " + dtConsulta.Rows[i][1].ToString().Trim());                

                btnSeleccionar[i] = new Button();
                btnSeleccionar[i].Image = Properties.Resources.seleccionar_comanda_combinar;
                btnSeleccionar[i].Name = dtConsulta.Rows[i][0].ToString();
                btnSeleccionar[i].Size = new System.Drawing.Size(25, 24);
                btnSeleccionar[i].Location = new System.Drawing.Point(360, iSuma);
                btnSeleccionar[i].FlatAppearance.BorderSize = 0;
                btnSeleccionar[i].FlatStyle = FlatStyle.Flat;
                btnSeleccionar[i].ForeColor = Color.Transparent;
                btnSeleccionar[i].Click += boton_clic_seleccionar;
                ttMensaje.SetToolTip(btnSeleccionar[i], "Clic aquí para seleccionar la comanda No. " + dtConsulta.Rows[i][1].ToString().Trim());                

                pnlContenedor.Controls.Add(lblOrdenes[i]);
                pnlContenedor.Controls.Add(btnVisualizar[i]);
                pnlContenedor.Controls.Add(btnSeleccionar[i]);

                iSuma += 40;
            }
        }
        
        //FUNCION CLIC DEL BOTON VISUALIZAR
        private void boton_clic_visualizar(object sender, EventArgs e)
        {
            btnVer = sender as Button;

            Areas.frmVistaPreviaComanda reporte = new frmVistaPreviaComanda(btnVer.Name, btnVer.AccessibleName);
            reporte.ShowDialog();
        }

        //FUNCION CLIC DEL BOTON SELECCIONAR
        private void boton_clic_seleccionar(object sender, EventArgs e)
        {
            btnSeleccion = sender as Button;
            DataRow row = dtCombinar.NewRow();
            row["idMesa"] = iIdMesa.ToString();
            row["idPedido"] = btnSeleccion.Name;
            dtCombinar.Rows.Add(row);

            this.DialogResult = DialogResult.OK;
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

        private void frmOrdenesMesaContenedora_Load(object sender, EventArgs e)
        {
            crearPanelContenedor();
            crearDataTable();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnCancelar);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnCancelar);
        }
    }
}
