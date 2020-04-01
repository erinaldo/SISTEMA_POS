using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    public class ClaseVectores
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdMesa { get; set; }
        public string sDescripcion { get; set; }
        public ClaseVectores[] vector;
        public Int32 cuenta;

        public bool llenarDatos(string sSqlCuenta, string sSqlCadena)
        {
            ////AQUI CREAMOS NUESTRA INSTRUCCION DE BUSQUEDA EN SQL
            DataTable dt = new DataTable();

            ClaseVectores objVector = new ClaseVectores();            

            //string sSqlQuery = "select count(*) cuenta from pos_mesa where estado = 'A'";
            string sSqlQuery = sSqlCuenta;
            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                cuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                vector = new ClaseVectores[cuenta];
                if (cuenta != 0)
                {
                    //EXTRAEMOS INFORMACION DE LA BASE DE DATOS
                    //sSqlCadena = "select id_pos_mesa, descripcion from pos_mesa";
                    sSqlQuery = sSqlCadena;
                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < cuenta; i++)
                        {
                            objVector = new ClaseVectores();
                            objVector.sIdMesa = ayuda.Rows[i].ItemArray[0].ToString();
                            objVector.sDescripcion = ayuda.Rows[i].ItemArray[1].ToString();
                            vector[i] = objVector;
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
