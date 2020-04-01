using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseMeseros
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        public string sIdMesero { get; set; }
        public string sDescripcion { get; set; }
        public string sCodigo { get; set; }
        public ClaseMeseros[] Meseros;
        public Int32 iCuenta;

        string sSql;
        string sTexto;
        double dSuma;
        bool bRespuesta = false;
        DataTable dtConsulta;

        public bool llenarDatos()
        {
            DataTable dt = new DataTable();
            ClaseMeseros objMesero = new ClaseMeseros();

            sSql = "";
            sSql += "select count(*) from pos_mesero" + Environment.NewLine;
            sSql += "where estado = 'A'" + Environment.NewLine;
            sSql += "and is_active = 1";

            dt.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dt, sSql);

            if (bRespuesta == true)
            {
                iCuenta = Convert.ToInt32(dt.Rows[0].ItemArray[0]);
                Meseros = new ClaseMeseros[iCuenta];
                if (iCuenta != 0)
                {
                    sSql = "";
                    sSql += "select id_pos_mesero, codigo, descripcion" + Environment.NewLine;
                    sSql += "from pos_mesero" + Environment.NewLine;
                    sSql += "where estado = 'A'" + Environment.NewLine;
                    sSql += "and is_active = 1";

                    DataTable ayuda = new DataTable();
                    ayuda.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(ayuda, sSql);
                    if (bRespuesta == true)
                    {
                        for (int i = 0; i < iCuenta; i++)
                        {
                            objMesero = new ClaseMeseros();
                            objMesero.sIdMesero = ayuda.Rows[i].ItemArray[0].ToString();
                            objMesero.sCodigo = ayuda.Rows[i].ItemArray[1].ToString();
                            objMesero.sDescripcion = ayuda.Rows[i].ItemArray[2].ToString();
                            Meseros[i] = objMesero;
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

        public string ventasMesero(int iIdCierreCajero_P)
        {
            try
            {
                dSuma = 0;

                sSql = "";
                sSql += "select mesero, ltrim(str(sum(cantidad * (precio_unitario - valor_dscto + valor_iva + valor_otro)), 10,2)) suma" + Environment.NewLine;
                sSql += "from pos_vw_factura" + Environment.NewLine;
                sSql += "where id_pos_cierre_cajero = " + iIdCierreCajero_P + Environment.NewLine;
                sSql += "group by mesero" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sTexto = "";
                        sTexto = sTexto.PadRight(40, '=') + Environment.NewLine;
                        sTexto = sTexto + Program.local + Environment.NewLine;
                        sTexto = sTexto + Program.telefono1;

                        if (Program.telefono2 != "")
                        {
                            sTexto = sTexto + " - " + Program.telefono2 + Environment.NewLine;
                        }

                        else
                        {
                            sTexto = sTexto + Environment.NewLine;
                        }

                        sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(12, ' ') + "VENTAS POR MESERO" + Environment.NewLine + Environment.NewLine;
                       
                        sTexto = sTexto + "Fecha  : " + DateTime.Now.ToString("dd-MM-yyyy") + Environment.NewLine + Environment.NewLine;

                        sTexto = sTexto + "".PadRight(40, '-') + Environment.NewLine;
                        sTexto = sTexto + "MESERO".PadRight(30, ' ') + "  VALOR" + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(40, '-') + Environment.NewLine;
                        
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sTexto = sTexto + dtConsulta.Rows[i].ItemArray[0].ToString().PadRight(30, ' ') + dtConsulta.Rows[i].ItemArray[1].ToString().PadLeft(10, ' ') + Environment.NewLine;
                            dSuma = dSuma + Convert.ToDouble(dtConsulta.Rows[i].ItemArray[1].ToString());
                        }

                        sTexto = sTexto + "".PadRight(40, '-') + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(23, ' ') + "Total:" + dSuma.ToString("N2").PadLeft(11, ' ');
                        sTexto = sTexto + Environment.NewLine + Environment.NewLine + Environment.NewLine + ".";

                        return sTexto;
                    }

                    else
                    {
                        return "";
                    }
                }

                else
                {
                    return sSql;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                return "";
            }
        }

    }
}
