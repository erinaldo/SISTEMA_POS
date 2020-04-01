using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmModalIngrediente : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();

        string sSql;        

        bool bRespuesta;

        DataTable dtConsulta;

        //VARIABLES PUBLICAS
        public int iIdProducto;
        public int iIdUnidad;
        public string sNombreProducto;
        public string sUnidadConsumo;
        public Decimal dbPresentacion;
        public Decimal dbRendimiento;
        public Decimal dbValorUnitario;

        public frmModalIngrediente()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                //sSql = "";
                //sSql += "select PRO.Id_Producto, NOM.Nombre, PRO.Presentacion," + Environment.NewLine;
                //sSql += "PRO.Rendimiento, UP.id_pos_unidad, TP.valor_texto unidad_consumo," + Environment.NewLine;
                //sSql += "ltrim(str(isnull(PP.valor, 0), 10, 6)) valor, UP.abreviatura" + Environment.NewLine;
                //sSql += "from cv401_productos PRO INNER JOIN" + Environment.NewLine;
                //sSql += "cv401_productos PROPADRE ON" + Environment.NewLine;
                //sSql += "PRO.Id_Producto_Padre = PROPADRE.id_producto" + Environment.NewLine;
                //sSql += "and PROPADRE.Estado = 'A'" + Environment.NewLine;
                //sSql += "and PRO.Estado = 'A' INNER JOIN" + Environment.NewLine;
                //sSql += "cv401_nombre_productos NOM ON PRO.Id_Producto = NOM.Id_Producto" + Environment.NewLine;
                //sSql += "and NOM.estado = 'A' INNER JOIN" + Environment.NewLine;
                //sSql += "cv401_vw_unidades_productos UP ON PRO.id_producto = UP.id_producto" + Environment.NewLine;
                //sSql += "and UP.estado = 'A' INNER JOIN" + Environment.NewLine;
                //sSql += "tp_codigos TP ON TP.correlativo = UP.cg_unidad INNER JOIN" + Environment.NewLine;
                //sSql += "cv403_precios_productos PP ON PRO.id_producto = PP.id_producto" + Environment.NewLine;
                //sSql += "and PP.estado = 'A'" + Environment.NewLine;
                //sSql += "where PROPADRE.Codigo Like '1%'" + Environment.NewLine;
                //sSql += "and PRO.Ultimo_Nivel = 1" + Environment.NewLine;
                //sSql += "and NOM.Nombre_Interno = 1" + Environment.NewLine;
                //sSql += "and PRO.valida_stock = 1" + Environment.NewLine;
                //sSql += "and UP.unidad_compra = 0" + Environment.NewLine;
                //sSql += "and PP.id_lista_precio = 1" + Environment.NewLine;

                //if (iOp == 1)
                //{
                //    sSql += "and (PRO.codigo like '%" + txtBusqueda.Text.Trim() + "%'" + Environment.NewLine;
                //    sSql += "or NOM.Nombre like '%" + txtBusqueda.Text.Trim() + "%')" + Environment.NewLine;
                //}

                //sSql += "order By NOM.Nombre";

                sSql = "";
                sSql += "select * from pos_vw_insumo_receta" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "where (codigo like '%" + txtBusqueda.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or Nombre like '%" + txtBusqueda.Text.Trim() + "%')" + Environment.NewLine;
                }

                sSql += "order by nombre" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[4].Visible = false;
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ENVIAR LOS DATOS
        private void enviarDatos()
        {
            try
            {
                iIdProducto = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
                sNombreProducto = dgvDatos.CurrentRow.Cells[1].Value.ToString().Trim().ToUpper();
                dbPresentacion = Convert.ToDecimal(dgvDatos.CurrentRow.Cells[2].Value);
                dbRendimiento = Convert.ToDecimal(dgvDatos.CurrentRow.Cells[3].Value);
                iIdUnidad = Convert.ToInt32(dgvDatos.CurrentRow.Cells[4].Value);
                sUnidadConsumo = dgvDatos.CurrentRow.Cells[7].Value.ToString().Trim().ToUpper();
                dbValorUnitario = Convert.ToDecimal(dgvDatos.CurrentRow.Cells[6].Value);

                this.DialogResult = DialogResult.OK;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmModalIngrediente_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        private void btnCerrarInformeVentas_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                this.Close();
                return;
            }

            if (dgvDatos.SelectedRows.Count > 0)
            {
                enviarDatos();
            }

            else
            {
                ok.lblMensaje.Text = "No ha seleccionado ningún registro";
                ok.ShowDialog();
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                this.Close();
                return;
            }

            else
            {
                enviarDatos();
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtBusqueda.Text.Trim() == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }
        }

        private void frmModalIngrediente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
