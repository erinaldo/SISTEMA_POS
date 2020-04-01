using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseModificadores
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sCodigo { get; set; }
        public string sDescripcion { get; set; }
        public string sIdModificador { get; set; }
        public string sPagaIva { get; set; }

        public ClaseModificadores[] modificadores;
        public Int32 cuenta;
        public string sCodigo_padre;
        string sSqlQuery;

        public ClaseModificadores(string codigoPadre)
        {
            this.sCodigo_padre = codigoPadre;
        }

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            ClaseModificadores objModificadores = new ClaseModificadores(sCodigo_padre);

            sSqlQuery = "";
            sSqlQuery += "select count (*) contador" + Environment.NewLine;
            sSqlQuery += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
            sSqlQuery += "where P.id_Producto = NP.id_Producto" + Environment.NewLine;
            sSqlQuery += "and P.id_Producto_padre = " + Program.iIdProductoModificador + Environment.NewLine;
            sSqlQuery += "and P.nivel = 3" + Environment.NewLine;
            sSqlQuery += "and P.estado ='A'" + Environment.NewLine;
            sSqlQuery += "and NP.estado='A'" + Environment.NewLine;
            sSqlQuery += "and P.subcategoria = 0" + Environment.NewLine;
            sSqlQuery += "and P.ultimo_nivel = 1" + Environment.NewLine;
            sSqlQuery += "and P.modificador = 1" + Environment.NewLine;
            sSqlQuery += "and codigo like '" + sCodigo_padre + "%'";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                cuenta = Convert.ToInt32(dt.Rows[0][0]);
                modificadores = new ClaseModificadores[cuenta];
                if (cuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery += "select P.id_Producto, P.codigo as Código, NP.nombre as Nombre, P.paga_iva" + Environment.NewLine;
                    sSqlQuery += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                    sSqlQuery += "where P.id_Producto = NP.id_Producto" + Environment.NewLine;
                    sSqlQuery += "and P.id_Producto_padre = " + Program.iIdProductoModificador + Environment.NewLine;
                    sSqlQuery += "and P.nivel = 3" + Environment.NewLine;
                    sSqlQuery += "and P.estado ='A'" + Environment.NewLine;
                    sSqlQuery += "and NP.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "and P.subcategoria = 0" + Environment.NewLine;
                    sSqlQuery += "and P.ultimo_nivel = 1" + Environment.NewLine;
                    sSqlQuery += "and P.modificador = 1" + Environment.NewLine;
                    sSqlQuery += "and codigo like '" + sCodigo_padre + "%'" + Environment.NewLine;
                    sSqlQuery += "order by secuencia";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);
                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < cuenta; i++)
                        {
                            objModificadores = new ClaseModificadores(sCodigo_padre);
                            objModificadores.sIdModificador = ayuda.Rows[i][0].ToString();
                            objModificadores.sCodigo = ayuda.Rows[i][1].ToString();
                            objModificadores.sDescripcion = ayuda.Rows[i][2].ToString();
                            objModificadores.sPagaIva = ayuda.Rows[i][3].ToString();
                            modificadores[i] = objModificadores;
                        }
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;

        }

        //Fin de la clase modificadores
    }
}
