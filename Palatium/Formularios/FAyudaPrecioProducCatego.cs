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


namespace Palatium.Formularios
{
    public partial class FAyudaPrecioProducCatego : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string idCategoria1;
        string iCodigoCategoria1;
        string iNombreCategoria1;
        string idCategoria2;
        string iCodigoCategoria2;
        string iNombreCategoria2; 

        public FAyudaPrecioProducCatego()
        {
            InitializeComponent();
        }

        private void FAyudaPrecioProducCatego_Load(object sender, EventArgs e)
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
                    t_st_query = "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre "+
                        "from cv401_productos P,cv401_nombre_productos NP where P.id_producto = NP.id_producto "+
                        "and P.id_producto_padre in (select id_producto from cv401_productos where codigo ='2') "+
                        "and P.nivel = 2 and P.estado ='A' and NP.estado='A'";
                }

                else
                {
                    t_st_query = "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre "+
                        "from cv401_productos P,cv401_nombre_productos NP where P.id_producto = NP.id_producto "+
                        "and P.id_producto_padre in (select id_producto from cv401_productos where codigo ='2') "+
                        "and P.nivel = 2 and P.estado ='A' and NP.estado='A' and NP.nombre LIKE '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    //dgvCategoria.Columns[0].Visible = true;
                    dgvCategoria.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvCategoria.Refresh();
                    dgvCategoria.Columns[0].Width = 100;
                    dgvCategoria.Columns[1].Width = 200;
                    dgvCategoria.Columns[0].Visible = false;

                    //NICOLE
                    dgvCategoria.Rows[0].Selected = true;
                    dgvCategoria.CurrentCell = dgvCategoria.Rows[0].Cells[1];
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaCategoria.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaCategoria.Text.Trim();
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

        private void dgvCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idCategoria1 = dgvCategoria.CurrentRow.Cells[0].Value.ToString();
            iCodigoCategoria1 = dgvCategoria.CurrentRow.Cells[1].Value.ToString();
            iNombreCategoria1 = dgvCategoria.CurrentRow.Cells[2].Value.ToString();
        }

        public string IdCategoria
        {
            get { return idCategoria2; }
        }

        public string CodiCategoria //creamos un metodo 
        {
            get { return iCodigoCategoria2; }
        }

        public string NombrCategoria
        {
            get { return iNombreCategoria2; }
        }

        private void btnAceptarCategoria_Click(object sender, EventArgs e)
        {
            dgvCategoria.Columns[0].Visible = true;
            idCategoria2 = idCategoria1;
            iCodigoCategoria2 = iCodigoCategoria1;
            iNombreCategoria2 = iNombreCategoria1;
            this.Close();
        }

        private void btnSalirLocalidadesCategoria_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCategoria_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            idCategoria1 = dgvCategoria.CurrentRow.Cells[0].Value.ToString();
            iCodigoCategoria1 = dgvCategoria.CurrentRow.Cells[1].Value.ToString();
            iNombreCategoria1 = dgvCategoria.CurrentRow.Cells[2].Value.ToString();
            idCategoria2 = idCategoria1;
            iCodigoCategoria2 = iCodigoCategoria1;
            iNombreCategoria2 = iNombreCategoria1;
            this.Close();
        }


    }
}
