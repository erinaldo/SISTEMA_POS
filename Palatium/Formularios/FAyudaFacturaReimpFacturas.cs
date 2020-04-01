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
    public partial class FAyudaFacturaReimpFacturas : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string[] G_st_datos = new string[2];
        bool x = false;
        string sId1;
        string sCliente1;
        string sCliente2;
        int iIdFactura1;
        int iIdFactura2;
        string sNombre1;
        string sId2;
        
        string sApellido2;
        string sNombre2;
        string sIdentificacion1;
        string sIdentificacion2;
        int iIdLocalidades;
        int iNumeroFactura1;
        int iNumeroFactura2;

        public FAyudaFacturaReimpFacturas(int iIdLocalidad)
        {
            InitializeComponent();
            iIdLocalidades = iIdLocalidad;
        }

        private void FAyudaFacturaReimpFacturas_Load(object sender, EventArgs e)
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
                    t_st_query = "Select NF.Numero_Factura,P.identificacion,ltrim(P.apellidos + ' ' + isnull(P.nombres,'')) Cliente,LOCALIDAD.valor_texto + case L.emite_comprobante_electronico when 1 then ' electronico' else '' end localidad,F.id_factura,NF.Id_Numero_Factura, '' autorizacion, isnull(F.id_tci_orden,0) id_tci_orden From cv403_facturas F,cv403_numeros_facturas NF,tp_personas P,tp_localidades L,tp_codigos LOCALIDAD, vta_tipocomprobante TC Where F.idempresa = 1  and F.estado = 'A'   and NF.estado = 'A'  and NF.id_factura = F.id_factura  and F.cg_estado_Factura <> 7476  and F.id_persona = P.id_persona   and F.id_localidad = L.id_localidad and F.id_localidad = " + iIdLocalidades + "  and L.cg_localidad = LOCALIDAD.correlativo  and TC.idtipocomprobante=F.idtipocomprobante  and TC.codigo='Fac'  order by F.id_factura desc";
                }

                else
                {
                    //t_st_query = "select id_persona,identificacion as Identificacion,apellidos as Apellidos," +
                    //    "nombres as Nombres from tp_personas where identificacion LIKE '%' + '" + t_st_datos[1] + "' " +
                    //    "OR apellidos like '%' + '" + t_st_datos[1] + "' OR nombres like '%' + '" + t_st_datos[1] + "' + '%'";
                }

                x = conexion.GFun_Lo_Rellenar_Grid(t_st_query, "tp_personas");
                if (x == false)
                {
                    MessageBox.Show("Error en la consulta");
                }
                else
                {
                    dgvFactura.DataSource = conexion.ds.Tables["tp_personas"];
                    dgvFactura.Refresh();

                    //NICOLE
                    dgvFactura.Rows[0].Selected = true;
                    dgvFactura.CurrentCell = dgvFactura.Rows[0].Cells[1];
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        private void btnBuscarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscaFactura.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    G_st_datos[0] = "1";
                    G_st_datos[1] = "asdsdasd";
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    G_st_datos[0] = "2";
                    G_st_datos[1] = txtBuscaFactura.Text.Trim();
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

        private void dgvFactura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            iNumeroFactura1 = Convert.ToInt32(dgvFactura.CurrentRow.Cells[0].Value.ToString());
            sIdentificacion1 = dgvFactura.CurrentRow.Cells[1].Value.ToString();
            sCliente1 = dgvFactura.CurrentRow.Cells[2].Value.ToString();
            iIdFactura1 = Convert.ToInt32(dgvFactura.CurrentRow.Cells[4].Value.ToString());

        }

        public int NumeroFactura //creamos un metodo 
        {
            get { return iNumeroFactura2; }
        }

        public string Identificacion //creamos un metodo 
        {
            get { return sIdentificacion2; }
        }
        public string Nombrecliente //creamos un metodo 
        {
            get { return sCliente2; }
        }

        public int IdFacturaCliente
        {
            get { return iIdFactura2; }
        }

        private void btnAceptarFactura_Click(object sender, EventArgs e)
        {
            iNumeroFactura2 = iNumeroFactura1;
            sIdentificacion2 = sIdentificacion1;
            sCliente2 = sCliente1;
            iIdFactura2 = iIdFactura1;
            this.Close();
        }

        private void btnSalirFactura_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
