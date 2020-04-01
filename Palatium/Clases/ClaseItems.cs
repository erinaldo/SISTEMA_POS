using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Palatium.Clases
{
    public class ClaseItems
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        public string iCodigo { get; set; }
        public string iDescripcion { get; set; }
        public string iPagaIva { get; set; }

        public string iCodigoTipoProducto { get; set; }

        public ClaseItems[] items;
        public Int32 cuenta;
        public string sCodigo_padre;
        public int iNivel;

        //Método Constructor
        public ClaseItems(string sCodigo_padre, int nivel)
        {
            this.sCodigo_padre = sCodigo_padre;
            this.iNivel = nivel;
        }

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();

            ClaseItems objItems = new ClaseItems(sCodigo_padre, iNivel);
            string sSqlQuery;

            if (iNivel == 3)
            {
                sSqlQuery = "";
                sSqlQuery += "select count(*) cuenta" + Environment.NewLine;
                sSqlQuery += "from cv401_productos P,cv401_nombre_productos NP, pos_clase_producto CP" + Environment.NewLine;
                sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSqlQuery += "and CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                sSqlQuery += "and P.id_producto_padre in" + Environment.NewLine;
                sSqlQuery += "(select id_producto from cv401_productos where codigo ='" + sCodigo_padre + "')" + Environment.NewLine;
                sSqlQuery += "and P.nivel = " + iNivel + Environment.NewLine;
                sSqlQuery += "and P.estado ='A'" + Environment.NewLine;
                sSqlQuery += "and NP.estado='A'" + Environment.NewLine;
                sSqlQuery += "and CP.estado = 'A'" + Environment.NewLine;
                sSqlQuery += "and P.subcategoria = 0" + Environment.NewLine;
                sSqlQuery += "and P.modificador = 0" + Environment.NewLine;
                sSqlQuery += "and P.is_active = 1" + Environment.NewLine;
                sSqlQuery += "and P.ultimo_nivel = 1";
            }

            else 
            {
                sSqlQuery = "";
                sSqlQuery += "select count(*) cuenta" + Environment.NewLine;
                sSqlQuery += "from cv401_productos P,cv401_nombre_productos NP, pos_clase_producto CP" + Environment.NewLine;
                sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSqlQuery += "and CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSqlQuery += "and P.id_producto_padre in" + Environment.NewLine;
                sSqlQuery += "(select id_producto from cv401_productos where codigo ='" + sCodigo_padre + "')" + Environment.NewLine;
                sSqlQuery += "and P.nivel = " + iNivel + Environment.NewLine;
                sSqlQuery += "and P.estado ='A'" + Environment.NewLine;
                sSqlQuery += "and NP.estado='A'" + Environment.NewLine;
                sSqlQuery += "and CP.estado = 'A'" + Environment.NewLine;
                sSqlQuery += "and P.subcategoria = 0" + Environment.NewLine;
                sSqlQuery += "and P.is_active = 1" + Environment.NewLine;
                sSqlQuery += "and P.modificador = 0";
            }

            
            //string sSqlQuery = "select count(*) cuenta from cv401_productos P,cv401_nombre_productos NP where P.id_producto = NP.id_producto  and P.id_producto_padre  = '" + sCodigo_padre + "' and P.nivel = 3 and P.estado ='A' and NP.estado='A'";                   
            

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                cuenta = Convert.ToInt32(dt.Rows[0][0]);
                items = new ClaseItems[cuenta];
                if (cuenta != 0)
                {
                    if (iNivel == 3)
                    {
                        sSqlQuery = "";
                        sSqlQuery += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva, CP.codigo" + Environment.NewLine;
                        sSqlQuery += "from cv401_productos P,cv401_nombre_productos NP, pos_clase_producto CP" + Environment.NewLine;
                        sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                        sSqlQuery += "and CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                        sSqlQuery += "and P.id_Producto_padre in " + Environment.NewLine;
                        sSqlQuery += "(select id_Producto from cv401_productos where codigo ='" + sCodigo_padre + "') " + Environment.NewLine;
                        sSqlQuery += "and P.nivel = " + iNivel + Environment.NewLine;
                        sSqlQuery += "and P.estado ='A'" + Environment.NewLine;
                        sSqlQuery += "and NP.estado='A'" + Environment.NewLine;
                        sSqlQuery += "and CP.estado = 'A'" + Environment.NewLine;
                        sSqlQuery += "and P.subcategoria = 0" + Environment.NewLine;
                        sSqlQuery += "and P.modificador = 0" + Environment.NewLine;
                        sSqlQuery += "and P.ultimo_nivel = 1" + Environment.NewLine;
                        sSqlQuery += "and P.is_active = 1" + Environment.NewLine;
                        sSqlQuery += "order by secuencia";
                    }
                    else
                    {
                        sSqlQuery = "";
                        sSqlQuery += "select P.id_Producto, NP.nombre as Nombre, P.paga_iva, CP.codigo" + Environment.NewLine;
                        sSqlQuery += "from cv401_productos P,cv401_nombre_productos NP, pos_clase_producto CP" + Environment.NewLine;
                        sSqlQuery += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                        sSqlQuery += "and CP.id_pos_clase_producto = P.id_pos_clase_producto" + Environment.NewLine;
                        sSqlQuery += "and P.id_Producto_padre in " + Environment.NewLine;
                        sSqlQuery += "(select id_Producto from cv401_productos where codigo ='" + sCodigo_padre + "') " + Environment.NewLine;
                        sSqlQuery += "and P.nivel = " + iNivel + Environment.NewLine;
                        sSqlQuery += "and P.estado ='A'" + Environment.NewLine;
                        sSqlQuery += "and NP.estado='A'" + Environment.NewLine;
                        sSqlQuery += "and CP.estado = 'A'" + Environment.NewLine;
                        sSqlQuery += "and P.subcategoria = 0" + Environment.NewLine;
                        sSqlQuery += "and P.modificador = 0" + Environment.NewLine;
                        sSqlQuery += "and P.is_active = 1" + Environment.NewLine;
                        sSqlQuery += "order by secuencia";
                    }                    

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
 

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);
                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < cuenta; i++)
                        {
                            objItems = new ClaseItems(sCodigo_padre,iNivel);
                            objItems.iCodigo = ayuda.Rows[i][0].ToString();
                            objItems.iDescripcion = ayuda.Rows[i][1].ToString();
                            objItems.iPagaIva = ayuda.Rows[i][2].ToString();
                            objItems.iCodigoTipoProducto = ayuda.Rows[i][3].ToString();
                            items[i] = objItems;
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    ok.LblMensaje.Text = "No hay productos en la familia";
                    ok.ShowDialog();
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
