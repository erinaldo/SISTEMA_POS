using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseSeccionMesa
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        public string sIdSeccionMesa { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set; }
        public string sColor { get; set; }

        public string sFondoPantalla { get; set; }
        public ClaseSeccionMesa[] seccionMesa;
        public Int32 iCuenta;

        DataTable dt;

        string sSql;

        bool bRespuesta;

        public bool llenarDatos()
        {
            
            ClaseSeccionMesa objSeccionMesa = new ClaseSeccionMesa();
            sSql = "";
            sSql += "select count(*) cuenta" + Environment.NewLine;
            sSql += "from pos_seccion_mesa" + Environment.NewLine;
            sSql += "where estado = 'A'";

            dt = new DataTable();
            dt.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSql);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0][0]);
                seccionMesa = new ClaseSeccionMesa[iCuenta];
                if (iCuenta != 0)
                {
                    sSql = "";
                    sSql += "select id_pos_seccion_mesa, codigo, descripcion, color, fondo_pantalla" + Environment.NewLine;
                    sSql += "from pos_seccion_mesa" + Environment.NewLine;
                    sSql += "where estado = 'A'";

                    DataTable dtAyuda = new DataTable();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);

                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objSeccionMesa = new ClaseSeccionMesa();
                            objSeccionMesa.sIdSeccionMesa = dtAyuda.Rows[i][0].ToString();
                            objSeccionMesa.sCodigo = dtAyuda.Rows[i][1].ToString();
                            objSeccionMesa.sDescripcion = dtAyuda.Rows[i][2].ToString();
                            objSeccionMesa.sColor = dtAyuda.Rows[i][3].ToString();
                            objSeccionMesa.sFondoPantalla = dtAyuda.Rows[i][4].ToString();
                            seccionMesa[i] = objSeccionMesa;
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
