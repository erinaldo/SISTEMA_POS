using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace InicioAplicacion.Formularios
{
    public partial class FAyudaProductos : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string id3;
        string descripcion3;
        string id4;
        string descripcion4;

        public FAyudaProductos()
        {
            InitializeComponent();
        }

        private void FAyudaProductos_Load(object sender, EventArgs e)
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
                    t_st_query = "select codigo,descripcion from cv401_vw_productos";
                }

                else
                {
                    t_st_query = "select codigo,descripcion from cv401_vw_productos where codigo LIKE '%' + '" + t_st_datos[1] + "'"+
                        " OR descripcion like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_vw_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProduc.DataSource = conexion.ds.Tables["cv401_vw_productos"];
                    dgvProduc.Refresh();

                    dgvProduc.Columns[0].Width = 100;
                    dgvProduc.Columns[1].Width = 200;
                    dgvProduc.Columns[2].Width = 100;
                    dgvProduc.Columns[3].Width = 100;
                    dgvProduc.Columns[4].Width = 100;
                    dgvProduc.Columns[5].Width = 100;
                    dgvProduc.Columns[6].Width = 100;

                    //NICOLE
                    dgvProduc.Rows[0].Selected = true;
                    dgvProduc.CurrentCell = dgvProduc.Rows[0].Cells[1];
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscarProduc_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaProduc.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaProduc.Text.Trim();
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

        private void dgvProduc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id3 = dgvProduc.CurrentRow.Cells[0].Value.ToString();
            descripcion3 = dgvProduc.CurrentRow.Cells[1].Value.ToString();
        }

        public string IdPro //creamos un metodo 
        {
            get { return id4; }
        }

        public string DescripcionPro
        {
            get { return descripcion4; }
        }

        private void btnAceptarProduc_Click(object sender, EventArgs e)
        {
            id4 = id3;
            descripcion4 = descripcion3;
            this.Close();
        }

        private void btnSalirProduc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProduc_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            id3 = dgvProduc.CurrentRow.Cells[0].Value.ToString();
            descripcion3 = dgvProduc.CurrentRow.Cells[1].Value.ToString();
            id4 = id3;
            descripcion4 = descripcion3;
            this.Close();
        }



    }
}
