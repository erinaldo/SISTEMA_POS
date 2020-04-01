using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseFormasPagosRecargo
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdFormaPago { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set; }
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
            sSqlQuery += "select count(*) cuenta" + Environment.NewLine;
            sSqlQuery += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
            sSqlQuery += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
            sSqlQuery += "and FC.estado = 'A'" + Environment.NewLine;
            sSqlQuery += "and MP.estado = 'A'" + Environment.NewLine;
            sSqlQuery += "where MP.codigo in ('TC', 'TD')";

            dt.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSqlQuery);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0][0].ToString());
                formasPago = new ClaseFormasPago[iCuenta];

                if (iCuenta != 0)
                {
                    sSqlQuery = "";
                    sSqlQuery += "select FC.id_pos_tipo_forma_cobro, FC.codigo, FC.descripcion, isnull(FC.imagen, '') imagen" + Environment.NewLine;
                    sSqlQuery += "from pos_tipo_forma_cobro FC INNER JOIN" + Environment.NewLine;
                    sSqlQuery += "pos_metodo_pago MP ON MP.id_pos_metodo_pago = FC.id_pos_metodo_pago" + Environment.NewLine;
                    sSqlQuery += "and FC.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "and MP.estado = 'A'" + Environment.NewLine;
                    sSqlQuery += "where MP.codigo in ('TC', 'TD')";

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
    }
}
