using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Palatium.Informes
{
    public partial class frmDashboard : Form
    {
        //Variables
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        ArrayList Categoria = new ArrayList();
        ArrayList Cantidad = new ArrayList();
        ArrayList Productos = new ArrayList();
        ArrayList CantidadProductos = new ArrayList();


        public frmDashboard()
        {
            InitializeComponent();
        }

        //Función para llenar el primer cuadro estadístico
        private void llenarCategoria()
        {
            try
            {
  
                string sFecha = DateTime.Now.ToString("yyyy-MM-dd");

                sSql = "select sum(DET.cantidad) CANTIDAD, sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) " +
                    "TOTAL, PRO.id_producto_padre " +
                    "from  cv403_det_pedidos DET inner join cv401_nombre_productos NOM on DET.id_producto = NOM.id_producto " +
                    "inner join cv403_cab_pedidos CAB  on CAB.id_pedido = DET.id_pedido and CAB.estado = 'A' and DET.estado = 'A' inner join cv401_productos PRO " +
                    "on NOM.id_producto = PRO.id_producto  and PRO.estado = 'A' and NOM.estado = 'A' " +
                    "where CAB.fecha_pedido = '"+sFecha+"'   " +
                    " and DET.estado = 'A' and CAB.id_localidad = " + Program.iIdLocalidad + " " +
                    "and CAB.estado_orden = 'Pagada' " +
                    "group by  PRO.id_producto_padre " +
                      "order by sum(DET.cantidad)";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[0].ToString());
                            int idProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                            string sNombreCategoria = "";
                            string sQuery = "select nombre from cv401_nombre_productos where id_producto = " + idProducto + " and estado = 'A'";
                            DataTable dtAyuda = new DataTable();
                            dtAyuda.Clear();
                            bool bEmergente = conexion.GFun_Lo_Busca_Registro(dtAyuda, sQuery);

                            if (bEmergente == true)
                                sNombreCategoria = dtAyuda.Rows[0].ItemArray[0].ToString();

                            Categoria.Add(sNombreCategoria);
                            Cantidad.Add(dbCantidad);

                            
                        }

                        chartProductoCategoria.Series[0].Points.DataBindXY(Categoria, Cantidad);
                    }
                    else
                    {
                        ok.LblMensaje.Text = "No hay datos para mostrar en el rango de fechas seleccionado";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }

                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar el grid";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargar el grid";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        }

        //Función para llenar el segundo cuadro estadístico
        private void llenarProductos()
        {
            try
            {
                string sFecha = DateTime.Now.ToString("yyyy-MM-dd");

                sSql = "select NOM.nombre, sum(DET.cantidad) CANTIDAD, sum(DET.cantidad *(((DET.precio_unitario + valor_iva+ valor_otro)-valor_dscto))) TOTAL " +
                        "from  cv403_det_pedidos DET inner join cv401_nombre_productos NOM " +
                        "on DET.id_producto = NOM.id_producto and NOM.estado = 'A' and DET.estado = 'A' inner join cv403_cab_pedidos CAB  " +
                        "on CAB.id_pedido = DET.id_pedido and CAB.estado = 'A'" +
                        "where CAB.fecha_pedido = '"+sFecha+"' " +
                        "and DET.estado = 'A' and CAB.id_localidad = " + Program.iIdLocalidad +
                        "and CAB.estado_orden = 'Pagada' " +
                        "group by NOM.nombre order by sum(DET.cantidad)";


                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {

                        double dbCantidadTotal = 0;
                        double dbTotalFinal = 0;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            string sNombreProducto = dtConsulta.Rows[i].ItemArray[0].ToString();
                            double dbCantidad = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                            double dbTotal = Convert.ToDouble(dtConsulta.Rows[i].ItemArray[2].ToString());
                            dbCantidadTotal += dbCantidad;
                            dbTotalFinal += dbTotal;

                            Productos.Add(sNombreProducto);
                            CantidadProductos.Add(dbCantidad);

                        }

                        chartProductos.Series[0].Points.DataBindXY(Productos, CantidadProductos);
                    }
                    else
                    {

                        ok.LblMensaje.Text = "No hay productos registrados en el rango de fechas";
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                    }

                    goto fin;
                }
                else
                    goto reversa;

            }
            catch (Exception ex)
            {
                goto reversa;
            }

            #region Funciones de Ayuda
        reversa:
            {
                VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Error al cargar el Grid. Por Favor Póngase en contacto con el Administrador";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }
        fin:
            {

            }
            #endregion
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            llenarCategoria();
            llenarProductos();
        }



        //Fin de la clase
    }
}
