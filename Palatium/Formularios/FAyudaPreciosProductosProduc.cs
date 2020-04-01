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
    public partial class FAyudaPreciosProductosProduc : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string iCodigoCategoria;
        string T_st_sql7 = "";
        string T_st_sql8 = "";
        string iCodig1;
        string iCodig2;
        string iNombre1;
        string iNombre2;
        DataTable dt;
        int iIdProducto;
        int iId1;
        int iId2;
        double dbValor;
        double iPrecioCompra1;
        double iPrecioCompra2;
        double iPrecioMinorista1;
        double iPrecioMinorista2;
        int iIdProductoPdre;
        int iIdProducto1;
        string iCodigProducto1;
        string iNombreProducto1;
        int iIdProducto2;
        string iCodigProducto2;
        string iNombreProducto2;

        public FAyudaPreciosProductosProduc(int iIdProductoPadre, string iCodigoCate)
        {
            InitializeComponent();
            iIdProductoPdre = iIdProductoPadre;
            iCodigoCategoria = iCodigoCate;
        }

        private void FAyudaPreciosProductosProduc_Load(object sender, EventArgs e)
        {
            if (iIdProductoPdre == 0 && iCodigoCategoria == "")
            {
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGridSinCategoria(t_st_datos);
            }
            else
            {
                string[] t_st_datos = { "1", "adsdasdasd" };
                llenarGridConCategoria(t_st_datos);
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGridSinCategoria(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";
                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select P.id_Producto, P.codigo as Código,NP.nombre as Nombre from cv401_productos P "+
                        "inner join cv401_nombre_productos NP  on P.id_producto = NP.id_producto where P.estado ='A' "+
                        "and NP.estado='A' and P.subcategoria=0 and P.ultimo_nivel=1 or P.modificador = 1";
                }

                else
                {
                    t_st_query = "select P.id_Producto, P.codigo as Código,NP.nombre as Nombre from cv401_productos P "+
                        "inner join cv401_nombre_productos NP  on P.id_producto = NP.id_producto where P.estado ='A' "+
                        "and NP.estado='A' and P.subcategoria=0 and P.ultimo_nivel=1 or P.modificador = 1 "+
                        "and P.codigo LIKE '%' + '" + t_st_datos[1] + "' OR NP.nombre like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProductos.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvProductos.Refresh();

                    //NICOLE
                    dgvProductos.Rows[0].Selected = true;
                    dgvProductos.CurrentCell = dgvProductos.Rows[0].Cells[1];
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGridConCategoria(string[] t_st_datos)
        {
            try
            {
                string t_st_query = "";
                if (t_st_datos[0] == "1")
                {
                    t_st_query = "select PRO.id_producto,PRO.codigo, NOM.nombre from cv401_productos PRO "+
                        "inner join cv401_nombre_productos NOM on PRO.id_producto = NOM.id_producto and NOM.estado = 'A' and PRO.estado = 'A' "+
                        " where codigo like '" + iCodigoCategoria + ".%' union select PRO.id_producto,PRO.codigo, "+
                        "NOM.nombre from cv401_productos PRO inner join cv401_nombre_productos NOM "+
                        "on PRO.id_producto = NOM.id_producto and NOM.estado = 'A' and PRO.estado = 'A' where id_producto_padre = " + iIdProductoPdre + "";
                }

                else
                {
                    t_st_query = "select PRO.id_producto,PRO.codigo, NOM.nombre from cv401_productos PRO "+
                        "inner join cv401_nombre_productos NOM on PRO.id_producto = NOM.id_producto and NOM.estado = 'A' and PRO.estado = 'A' "+
                        "where codigo like '" + iCodigoCategoria + ".%' union select PRO.id_producto,PRO.codigo,"+
                        " NOM.nombre from cv401_productos PRO inner join cv401_nombre_productos NOM "+
                        "on PRO.id_producto = NOM.id_producto and NOM.estado = 'A' and PRO.estado = 'A' where id_producto_padre = " + iIdProductoPdre + "  "+
                        "and P.codigo LIKE '%' + '" + t_st_datos[1] + "' OR NP.nombre like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv401_productos");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvProductos.DataSource = conexion.ds.Tables["cv401_productos"];
                    dgvProductos.Refresh();
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtBuscaProductos.Text == "")
            //    {
            //        //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
            //        G_st_datos[0] = "1";
            //        G_st_datos[1] = "asdsdasd";
            //    }

            //    else
            //    {
            //        //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
            //        G_st_datos[0] = "2";
            //        G_st_datos[1] = txtBuscaProductos.Text.Trim();
            //    }

            //    llenarGrid(G_st_datos);
            //}

            //catch (Exception)
            //{
            //    MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
            //    string[] t_st_datos = { "1", "adsdasdasd" };
            //    llenarGrid(t_st_datos);
            //}
        }

        private void dgvLisPreci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iIdProducto1 = Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value.ToString());

            iCodigProducto1 = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            iNombreProducto1 = dgvProductos.CurrentRow.Cells[2].Value.ToString();
        }

        public int IdProducto //creamos un metodo 
        {
            get { return iIdProducto2; }
        }

        public string CodigoProducto
        {
            get { return iCodigProducto2; }
        }

        public string NombreProducto
        {
            get { return iNombreProducto2; }
        }

        //public double PrecioCompra
        //{
        //    get { return iPrecioCompra2; }
        //}
        //public double PrecioMinorista //creamos un metodo 
        //{
        //    get { return iPrecioMinorista2; }
        //}

        //public string lisBase
        //{
        //    get { return ListBas2; }
        //}
        //public string lisModi //creamos un metodo 
        //{
        //    get { return ListMosdi2; }
        //}
        //public string Estad //creamos un metodo 
        //{
        //    get { return esta2; }
        //}

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            iIdProducto2 = iIdProducto1;
            iCodigProducto2 = iCodigProducto1;
            iNombreProducto2 = iNombreProducto1;
            
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvLisPreci_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iIdProducto1 = Convert.ToInt32(dgvProductos.CurrentRow.Cells[0].Value.ToString());
            iCodigProducto1 = dgvProductos.CurrentRow.Cells[1].Value.ToString();
            iNombreProducto1 = dgvProductos.CurrentRow.Cells[2].Value.ToString();
            iIdProducto2 = iIdProducto1;
            iCodigProducto2 = iCodigProducto1;
            iNombreProducto2 = iNombreProducto1;
            this.Close();
        }


    }
}
