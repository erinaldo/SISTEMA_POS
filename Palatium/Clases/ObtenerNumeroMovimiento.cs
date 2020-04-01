using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ObtenerNumeroMovimiento
    {
        //Variables para la conexion
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        DataTable dtConsulta = new DataTable();
        bool bRespuesta;
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();


        //El método necesita recibir como parámetros el tipo de movimiento, Id de la bodega,
        //el año y el mes
        public string devuelveCorrelativo(string sTipoMovimiento, int iIdBodega, string sAño, string sMes, string sCodigoCorrelativo)
        {

            double dbValorActual =0;
            string sCodigo = "";
            string sAñoCorto = sAño.Substring(2, 2);
            string sMesCorto;
            if (sMes.Substring(0, 1) == "0")
                sMesCorto = sMes.Substring(1, 1);
            else
                sMesCorto = sMes;

            sSql = "select codigo from cv402_bodegas where id_bodega = " + iIdBodega;

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    sCodigo = dtConsulta.Rows[0].ItemArray[0].ToString();
                }
            }
            else
                return "Error";

            string sReferencia;

            sReferencia = sTipoMovimiento + sCodigo + "_" + sAño + "_" + sMesCorto + "_" + Program.iCgEmpresa;
                

            sSql = "select valor_actual from tp_correlativos where referencia = '" + sReferencia + "' and codigo_correlativo = '"+sCodigoCorrelativo+"'";

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    dbValorActual = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());

                    sSql = "update tp_correlativos set valor_actual =  " + (dbValorActual + 1) + " where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        return "Error";
                    }

                        return sTipoMovimiento + sCodigo + sAñoCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');
      
                }
                else
                {
                    int iCorrelativo = 4979;
                    dbValorActual = 1;
                    sSql = "select correlativo from tp_codigos where codigo = 'BD' and tabla = 'SYS$00022'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        }
                    }
                    else
                        return "Error";

                    string sFechaDesde = sAño + "-01-01";
                    string sFechaHasta = sAño + "-12-31";
                    string sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                    string sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

                    sSql = "insert into tp_correlativos (cg_sistema, codigo_correlativo, referencia, valido_desde, valido_hasta, " +
                            "valor_actual, desde, hasta, estado, origen_dato, numero_replica_trigger, " +
                                "estado_replica, numero_control_replica) " +
                            " values(" + iCorrelativo + ",'"+sCodigoCorrelativo+"','" + sReferencia + "','" + sFechaDesde + "','" + sFechaHasta + "'," +
                            "" + (dbValorActual + 1) + ", 0,0,'A',1," + (dbValorActual + 1).ToString("N0") + ",0,0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        return "Error";
                    }

                        return sTipoMovimiento + sCodigo + sAñoCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

                }
            }
            else
                return "Error";

        }





    }




    //Fin de la clase
}
