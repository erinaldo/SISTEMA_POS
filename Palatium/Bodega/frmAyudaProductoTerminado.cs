using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmAyudaProductoTerminado : Form
    {
        //variables
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoSiNo SiNo;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;

        public string sCodigo { get; set; }

        public string sNombre { get; set; }

        public string sIdProducto { get; set; }

        public string sPagaIva { get; set; }

        public frmAyudaProductoTerminado()
        {
            InitializeComponent();
        }

        private void Btn_Aceptar_Click(object sender, EventArgs e)
        {
            llenarVariables();
        }

        private void Btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAyudaProductos_Load(object sender, EventArgs e)
        {
            llenarGrid(1);
        }

        //Función para llenar el grid
        private void llenarGrid(int iBandera)
        {
            try
            {
                dgv_Datos.Rows.Clear();

                sSql = "";
                sSql += "select PRO.Codigo, NOM.Nombre, PRO.Id_Producto, PRO.paga_iva" + Environment.NewLine;
                sSql += "from cv401_productos PRO, cv401_productos PROPADRE, cv401_nombre_productos NOM" + Environment.NewLine;
                sSql += "where PRO.Estado = 'A'" + Environment.NewLine;
                sSql += "and PRO.Ultimo_Nivel = 1" + Environment.NewLine;
                sSql += "and PRO.Id_Producto = NOM.Id_Producto" + Environment.NewLine;
                sSql += "and PRO.Id_Producto_Padre = PROPADRE.id_producto" + Environment.NewLine;
                sSql += "and NOM.Estado = 'A'" + Environment.NewLine;
                sSql += "and NOM.Nombre_Interno = 1" + Environment.NewLine;
                sSql += "and PRO.valida_stock = 1" + Environment.NewLine;
                sSql += "and PROPADRE.Codigo Like '2%' " + Environment.NewLine;

                if (iBandera != 1)
                {
                    sSql += "and (PRO.Codigo like '%" + TxtBusqueda.Text + "%'" + Environment.NewLine;
                    sSql += "or NOM.nombre like '%" + TxtBusqueda.Text + "%')" + Environment.NewLine;
                }

                sSql += "order By NOM.Nombre";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            dgv_Datos.Rows.Add(dtConsulta.Rows[i].ItemArray[0].ToString(),
                                                dtConsulta.Rows[i].ItemArray[1].ToString(),
                                                dtConsulta.Rows[i].ItemArray[2].ToString(),
                                                dtConsulta.Rows[i].ItemArray[3].ToString()
                                                );
                        }
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }



        private void llenarVariables()
        {
            try
            {
                sCodigo = dgv_Datos.CurrentRow.Cells[0].Value.ToString();
                sNombre = dgv_Datos.CurrentRow.Cells[1].Value.ToString();
                sIdProducto = dgv_Datos.CurrentRow.Cells[2].Value.ToString();
                sPagaIva = dgv_Datos.CurrentRow.Cells[3].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgv_Datos_DoubleClick(object sender, EventArgs e)
        {
            llenarVariables();
        }

        private void TxtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            llenarGrid(0);
        }
    }
}
