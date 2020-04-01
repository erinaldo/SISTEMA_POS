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
    public partial class FAyudaLisPrec : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string id1;
        string descripcion1;
        string id2;
        string descripcion2;
        string moned1;
        string fechIni1;
        string fechFin1;
        string ListBas1;
        string ListMosdi1;
        string moned2;
        string fechIni2;
        string fechFin2;
        string ListBas2;
        string ListMosdi2;
        string esta1;
        string esta2;

        public FAyudaLisPrec()
        {
            InitializeComponent();
        }

        private void FAyudaLisPrec_Load(object sender, EventArgs e)
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
                    t_st_query = "select  PRE.id_lista_precio, PRE.descripcion as Descripción, COD.valor_texto as Moneda, "+
                        "PRE.fecha_inicio_validez, PRE.fecha_fin_validez, PRE.lista_base, PRE.lista_modificable, "+
                        "PRE.estado from  cv403_listas_precios PRE inner join tp_codigos COD on PRE.cg_moneda = COD.correlativo";
                }

                else
                {
                    t_st_query = "select  PRE.id_lista_precio, PRE.descripcion as Descripción, COD.valor_texto as Moneda, "+
                        "PRE.fecha_inicio_validez, PRE.fecha_fin_validez, PRE.lista_base, PRE.lista_modificable, "+
                        "PRE.estado from  cv403_listas_precios PRE inner join tp_codigos COD on PRE.cg_moneda = "+
                        "COD.correlativo where PRE.id_lista_precio LIKE '%' + '" + t_st_datos[1] + "' OR PRE.descripcion like '%' + "+
                        "'" + t_st_datos[1] + "' OR COD.valor_texto like '%' + '" + t_st_datos[1] + "'"+
                        " OR fecha_inicio_validez like '%' + '" + t_st_datos[1] + "' OR fecha_fin_validez like '%' + "+
                        "'" + t_st_datos[1] + "' OR lista_base like '%' + '" + t_st_datos[1] + "' OR lista_modificable like '%' + "+
                        "'" + t_st_datos[1] + "' OR estado like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "cv403_listas_precios");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvLisPreci.DataSource = conexion.ds.Tables["cv403_listas_precios"];
                    dgvLisPreci.Refresh();

                    dgvLisPreci.Columns[0].Width = 50;
                    dgvLisPreci.Columns[1].Width = 200;
                    dgvLisPreci.Columns[2].Width = 100;
                    dgvLisPreci.Columns[3].Width = 100;
                    dgvLisPreci.Columns[4].Width = 100;
                    dgvLisPreci.Columns[5].Width = 100;
                    dgvLisPreci.Columns[6].Width = 100;
                    dgvLisPreci.Columns[7].Width = 100;

                    //NICOLE
                    dgvLisPreci.Rows[0].Selected = true;
                    dgvLisPreci.CurrentCell = dgvLisPreci.Rows[0].Cells[1];
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaLisPrec.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaLisPrec.Text.Trim();
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

        private void dgvLisPreci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id1 = dgvLisPreci.CurrentRow.Cells[0].Value.ToString();
            descripcion1 = dgvLisPreci.CurrentRow.Cells[1].Value.ToString();
            moned1 = dgvLisPreci.CurrentRow.Cells[2].Value.ToString();
            fechIni1 = dgvLisPreci.CurrentRow.Cells[3].Value.ToString();
            fechFin1 = dgvLisPreci.CurrentRow.Cells[4].Value.ToString();
            ListBas1 = dgvLisPreci.CurrentRow.Cells[5].Value.ToString();
            ListMosdi1 = dgvLisPreci.CurrentRow.Cells[6].Value.ToString();
            esta1 = dgvLisPreci.CurrentRow.Cells[7].Value.ToString();
        }

        public string Id //creamos un metodo 
        {
            get { return id2; }
        }

        public string Descripcion
        {
            get { return descripcion2; }
        }

        public string Moneda
        {
            get { return moned2; }
        }
        
        public string fechaIni
        {
            get { return fechIni2; }
        }
        public string fechaFin //creamos un metodo 
        {
            get { return fechFin2; }
        }

        public string lisBase
        {
            get { return ListBas2; }
        }
        public string lisModi //creamos un metodo 
        {
            get { return ListMosdi2; }
        }
        public string Estad //creamos un metodo 
        {
            get { return esta2; }
        }



        private void btnAceptar_Click(object sender, EventArgs e)
        {
            id2 = id1;
            descripcion2 = descripcion1;
            moned2 = moned1;
            fechIni2 = fechIni1;
            fechFin2 = fechFin1;
            ListBas2 = ListBas1;
            ListMosdi2 = ListMosdi1;
            esta2 = esta1;
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvLisPreci_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvLisPreci_CellClick(sender,e);
            btnAceptar_Click(sender, e);
        }

    }
}
