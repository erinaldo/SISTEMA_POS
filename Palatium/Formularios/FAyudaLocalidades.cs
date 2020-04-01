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
    public partial class FAyudaLocalidades : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string nombre_Impresora1;
        string id_localidad1;
        string id_localidad_Impresora1;
        string nombre_Impresora2;
        string id_localidad2;
        string id_localidad_Impresora2;
        DataTable dtConsulta;
        string sSql;
        bool bRespuesta;


        public FAyudaLocalidades()
        {
            InitializeComponent();
        }

        private void FAyudaLocalidades_Load(object sender, EventArgs e)
        {
            string[] t_st_datos = { "1", "adsdasdasd" };
            llenarGrid(0);
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int op)
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "Select IM.id_localidad,IM.nombre_Impresora,IM.id_localidad_Impresora " +
                        " From tp_localidades_impresoras IM,tp_localidades LO Where IM.estado = 'A' And IM.id_localidad = "+
                        "LO.id_localidad And LO.idempresa = " + Program.iIdEmpresa;

                if (op == 0)
                {
                    sSql = sSql + " order by LO.id_localidad";
                }

                else if (op == 1)
                {
                    sSql = sSql + " and IM.nombre_Impresora LIKE '%' + '" + txtBuscaLocalidades.Text.Trim() + "' + '%'"+ 
                                  " OR IM.id_localidad like '%' + '" + txtBuscaLocalidades.Text.Trim() + 
                                  "' + '%' OR IM.id_localidad_Impresora like '%' + "+ "'" + txtBuscaLocalidades.Text.Trim() + "' + '%' order by LO.id_localidad";
                }

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvLocalidades.DataSource = dtConsulta;
                        dgvLocalidades.Columns[0].Width = 60;
                        dgvLocalidades.Columns[1].Width = 300;
                        //dgvLocalidades.Columns[2].Width = 100;

                        //NICOLE
                        dgvLocalidades.Rows[0].Selected = true;
                        dgvLocalidades.CurrentCell = dgvLocalidades.Rows[0].Cells[1];

                        dgvLocalidades.Columns[2].Visible = false;
                    }
                }

                else
                {
                    MessageBox.Show("Ocurrió un problema al realizar la consulta.");
                } 
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al realizar la consulta.");
            }
        }

        private void btnBuscarLocalidades_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaLocalidades.Text == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al realizar la consulta.");
            }
        }

        private void dgvLocalidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            nombre_Impresora1 = dgvLocalidades.CurrentRow.Cells[0].Value.ToString();
            id_localidad1 = dgvLocalidades.CurrentRow.Cells[1].Value.ToString();
            id_localidad_Impresora1 = dgvLocalidades.CurrentRow.Cells[2].Value.ToString();
            
        }

        public string nombre //creamos un metodo 
        {
            get { return nombre_Impresora2; }
        }

        public string localidad
        {
            get { return id_localidad2; }
        }

        public string impresora
        {
            get { return id_localidad_Impresora2; }
        }


        private void btnAceptarLocalidades_Click(object sender, EventArgs e)
        {
            nombre_Impresora2 = nombre_Impresora1;
            id_localidad2 = id_localidad1;
            id_localidad_Impresora2 = id_localidad_Impresora1;

            this.Close();
        }

        private void btnSalirLocalidades_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvLocalidades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            nombre_Impresora1 = dgvLocalidades.CurrentRow.Cells[1].Value.ToString();
            id_localidad1 = dgvLocalidades.CurrentRow.Cells[0].Value.ToString();
            id_localidad_Impresora1 = dgvLocalidades.CurrentRow.Cells[2].Value.ToString();
            nombre_Impresora2 = nombre_Impresora1;
            id_localidad2 = id_localidad1;
            id_localidad_Impresora2 = id_localidad_Impresora1;

            this.Close();
        }



    }
}
