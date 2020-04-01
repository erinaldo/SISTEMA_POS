using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseLlenarMonedas
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;

        bool bRespuesta;

        public void llenarMonedas()
        {
            sSql = "";
            sSql += "select id_pos_moneda, valor, descripcion" + Environment.NewLine;
            sSql += "from pos_monedas" + Environment.NewLine;
            sSql += "where estado = 'A'" + Environment.NewLine;
            sSql += "order by secuencia";

            Program.dtMonedasCierre = new DataTable();
            Program.dtMonedasCierre.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(Program.dtMonedasCierre, sSql);

            if (bRespuesta == true)
            {
                Program.dtMonedasCierre.Columns.Add("cantidad", typeof(string));
                Program.dtMonedasCierre.Columns.Add("total", typeof(string));

                for (int i = 0; i < Program.dtMonedasCierre.Rows.Count; i++)
                {
                    Program.dtMonedasCierre.Rows[i]["cantidad"] = "0";
                    Program.dtMonedasCierre.Rows[i]["total"] = "0.00";
                }
            }

            else
            {
                catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                catchMensaje.ShowDialog();
            }
        }
    }
}
