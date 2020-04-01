using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Palatium.Clases
{
    class ClaseFormasPago
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdFormaPago { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set;}
        public string sImagen { get; set; }

        public ClaseFormasPago[] formasPago;
        public Int32 iCuenta;

        string sSqlQuery;

        bool bRespuesta;

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            ClaseFormasPago objFormasPago = new ClaseFormasPago();

            sSqlQuery = "";
            sSqlQuery += "select count(*) from pos_tipo_forma_cobro where estado = 'A'";

            dt.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0][0]);
                formasPago = new ClaseFormasPago[iCuenta];

                if (iCuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery += "select id_pos_tipo_forma_cobro, codigo, descripcion, isnull(imagen, '') imagen" + Environment.NewLine;
                    sSqlQuery += "from pos_tipo_forma_cobro" + Environment.NewLine;
                    sSqlQuery += "where estado = 'A'";
                    
                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSqlQuery);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objFormasPago = new ClaseFormasPago();
                            objFormasPago.sIdFormaPago = ayuda.Rows[i][0].ToString();
                            objFormasPago.sCodigo = ayuda.Rows[i][1].ToString();
                            objFormasPago.sDescripcion = ayuda.Rows[i][2].ToString();
                            objFormasPago.sImagen = ayuda.Rows[i][3].ToString();
                            formasPago[i] = objFormasPago;
                        }
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;

        }


        //Fin de la clase formasPago
    }
}
