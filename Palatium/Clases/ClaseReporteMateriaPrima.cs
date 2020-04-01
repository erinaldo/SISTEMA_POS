using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Clases
{
    class ClaseReporteMateriaPrima
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        Clases.ClaseManejoCaracteres caracteres = new Clases.ClaseManejoCaracteres();

        DataTable dtConsulta;

        bool bRespuesta = false;

        int iIdLocalidad;

        string sSql;
        string sFecha;
        string sTexto;
        string sFechaApertura;
        string sHoraApertura;
        string sFechaCierre;
        string sHoraCierre;
        string sNombreItem;
        string sUnidadItem;

        decimal dbPorcentajeIva;
        decimal dbPorcentajeServicio;
        decimal dbCantidadItem;

        //FUNCION PARA CONSULTAR FECHA Y HORA
        private bool consultarFechaHora()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select fecha_apertura, hora_apertura, isnull(fecha_cierre, fecha_apertura) fecha_apertura," + Environment.NewLine;
                sSql = sSql + "isnull(hora_cierre, '') hora_cierre, porcentaje_iva, porcentaje_servicio" + Environment.NewLine;
                sSql = sSql + "from pos_cierre_cajero" + Environment.NewLine;
                sSql = sSql + "where fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql = sSql + "and id_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sFechaApertura = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[0].ToString()).ToString("dd/MM/yyyy");
                        sHoraApertura = dtConsulta.Rows[0].ItemArray[1].ToString();
                        sFechaCierre = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[2].ToString()).ToString("dd/MM/yyyy");

                        if (dtConsulta.Rows[0].ItemArray[3].ToString() == "")
                        {
                            sHoraCierre = DateTime.Now.ToString("HH:mm:dd");
                        }

                        else
                        {
                            sHoraCierre = Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[3].ToString()).ToString("HH:mm:ss");
                        }

                        dbPorcentajeIva = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_iva"]);
                        dbPorcentajeServicio = Convert.ToDecimal(dtConsulta.Rows[0]["porcentaje_servicio"]);

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

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        public string llenarReporte(string sFecha_P, int iIdLocalidad_P)
        {
            try
            {
                this.sFecha = sFecha_P;
                this.iIdLocalidad = iIdLocalidad_P;

                if (consultarFechaHora() == false)
                {
                    return "ERROR";
                }

                sTexto = "";
                sTexto += "".PadRight(40, '-') + Environment.NewLine;
                sTexto += Program.local.PadLeft(30, ' ') + Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine;
                sTexto += "REPORTE DE MATERIA PRIMA UTILIZADA".PadLeft(37, ' ') + Environment.NewLine;
                sTexto += "FECHA: " + sFecha + Environment.NewLine;
                sTexto += "DESDE: " + sHoraApertura + Environment.NewLine;
                sTexto += "HASTA: " + sHoraCierre + Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine;

                sSql = "";
                sSql += "select * from pos_vw_reporte_materia_prima" + Environment.NewLine;
                sSql += "where fecha_pedido = '" + sFecha_P + "'" + Environment.NewLine;
                sSql += "and id_localidad = " + iIdLocalidad + Environment.NewLine;
                sSql += "order by cantidad";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    return "ERROR";
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    sNombreItem = dtConsulta.Rows[i]["nombre"].ToString().Trim().ToUpper();
                    sUnidadItem = dtConsulta.Rows[i]["unidad"].ToString().Trim().ToUpper();
                    dbCantidadItem = Convert.ToDecimal(dtConsulta.Rows[i]["cantidad"].ToString());

                    if (sNombreItem.Trim().Length > 28)
                    {
                        sNombreItem = sNombreItem.Trim().Substring(0, 28);
                    }

                    sTexto += sNombreItem.PadRight(30, '_') + dbCantidadItem.ToString("N4").PadLeft(10, ' ') + Environment.NewLine;
                }

                sTexto += Environment.NewLine;
                sTexto += "".PadRight(40, '-') + Environment.NewLine + Environment.NewLine;
                sTexto += "NOTA: EL REPORTE INCLUYE LOS PRODUCTOS" + Environment.NewLine;
                sTexto += "DE CUENTAS POR COBRAR Y CORTESÍAS. LAS" + Environment.NewLine;
                sTexto += "CANTIDADES SE REPORTAN CON 4 DECIMALES" + Environment.NewLine;
                sTexto += Environment.NewLine + Environment.NewLine;

                sTexto += Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

                return sTexto;
            }

            catch(Exception ex)
            {
                return "ERROR";
            }
        }
    }
}
