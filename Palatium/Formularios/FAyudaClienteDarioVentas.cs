using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Formularios
{
    public partial class FAyudaClienteDarioVentas : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string sId1;
        string sIdentificacion1;
        string sApellido1;
        string sNombre1;
        string sId2;
        string sIdentificacion2;
        string sApellido2;
        string sNombre2;
        string sApellido;
        string sNombre;
        string sCliente;
        string sClienteRecuperado1;
        string sClienteRecuperado2;

        public FAyudaClienteDarioVentas()
        {
            InitializeComponent();
        }

        private void FAyudaClienteDarioVentas_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(t_st_datos);
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";
                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select id_persona,identificacion as Identificacion,apellidos as Apellidos,"+
                        "nombres as Nombres,'' as Cliente from tp_personas";
                }

                else
                {
                    t_st_query = "select id_persona,identificacion as Identificacion,apellidos as Apellidos,"+
                        "nombres as Nombres,'' as Cliente from tp_personas where identificacion LIKE '%' + '" + t_st_datos[1] + "' "+
                        " OR apellidos like '%' + '" + t_st_datos[1] + "' OR nombres like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "tp_personas");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvPersona.DataSource = conexion.ds.Tables["tp_personas"];
                    dgvPersona.Refresh();

                    //NICOLE
                    dgvPersona.Rows[0].Selected = true;
                    dgvPersona.CurrentCell = dgvPersona.Rows[0].Cells[1];

                    int iConta2 = 0;
                    foreach (DataGridViewRow row2 in dgvPersona.Rows)
                    {
                        sApellido = row2.Cells["Apellidos"].Value.ToString();
                        sNombre = row2.Cells["Nombres"].Value.ToString();
                        sCliente = sApellido + " " + sNombre;
                        dgvPersona.Rows[iConta2].Cells[4].Value = sCliente;
                           
                        iConta2++;
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }

        private void btnBuscarPersona_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaPersona.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaPersona.Text.Trim();
                }

                llenarGrid(G_st_datos);
            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGrid(t_st_datos);
            }
        }

        private void dgvPersona_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sId1 = dgvPersona.CurrentRow.Cells[0].Value.ToString();
            sIdentificacion1 = dgvPersona.CurrentRow.Cells[1].Value.ToString();
            sClienteRecuperado1 = dgvPersona.CurrentRow.Cells[4].Value.ToString();
            //sNombre1 = dgvPersona.CurrentRow.Cells[3].Value.ToString();
        }

        public string Id //creamos un metodo 
        {
            get { return sId2; }
        }

        public string Identificaci
        {
            get { return sIdentificacion2; }
        }

        public string Cliente
        {
            get { return sClienteRecuperado2; }
        }

        //public string Nombr
        //{
        //    get { return sNombre2; }
        //}

        private void btnAceptarPersona_Click(object sender, EventArgs e)
        {
            sId2 = sId1;
            sIdentificacion2 = sIdentificacion1;
            sClienteRecuperado2 = sClienteRecuperado1;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSalirPersona_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
