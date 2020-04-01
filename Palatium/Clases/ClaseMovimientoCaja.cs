using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseMovimientoCaja
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        string sTexto;
        double dSuma;
        int iLongi;

        bool bRespuesta = false;
        DataTable dtConsulta;

        public string llenarMovimiento(int iIdMovimiento)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select" + Environment.NewLine;
                sSql = sSql + "CASE MC.tipo_movimiento when 1 then 'Entrada a caja' else 'Salida de caja' end 'MOVIMIENTO DE CAJA'," + Environment.NewLine;
                sSql = sSql + "NMC.numero_movimiento_caja, MC.hora, MC.concepto,  ltrim(str(MC.valor, 8, 2)) valor" + Environment.NewLine;
                sSql = sSql + "from pos_movimiento_caja MC, pos_numero_movimiento_caja NMC" + Environment.NewLine;
                sSql = sSql + "where NMC.id_pos_movimiento_caja = MC.id_pos_movimiento_caja" + Environment.NewLine;
                sSql = sSql + "and MC.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and NMC.estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and MC.id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql = sSql + "and MC.id_pos_movimiento_caja = " + iIdMovimiento;

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
                        sTexto = sTexto + "".PadLeft(17, ' ') + "RECIBO" + Environment.NewLine + Environment.NewLine;
                        sTexto = sTexto + "** ".PadLeft(11, ' ') + dtConsulta.Rows[0][0].ToString() + " **" + Environment.NewLine + Environment.NewLine;
                        sTexto = sTexto + "Número : " + dtConsulta.Rows[0][1].ToString() + Environment.NewLine;
                        sTexto = sTexto + "Fecha  : " + dtConsulta.Rows[0][2].ToString() + Environment.NewLine;
                        sTexto = sTexto + "Cargo  : " + Environment.NewLine + Environment.NewLine;
                        sTexto = sTexto + "Concepto:" + Environment.NewLine;
                        sTexto = sTexto + dtConsulta.Rows[0][3].ToString() + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine;
                        sTexto = sTexto + "Valor  :" + dtConsulta.Rows[0][4].ToString().PadLeft(11, ' ') + Environment.NewLine; ;
                        sTexto = sTexto + "".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                        sTexto = sTexto + "".PadLeft(7, ' ') + "Recibió" + "Entrego".PadLeft(20, ' ');
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
                catchMensaje.ShowDialog();
                return "";
            }
        }


        public string llenarMovimientosAgrupados(int iTipoMovimiento, string sFecha)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select convert(varchar, hora, 108) hora, concepto, ltrim(str(valor, 8, 2)) valor" + Environment.NewLine;
                sSql = sSql + "from pos_movimiento_caja" + Environment.NewLine;
                sSql = sSql + "where tipo_movimiento = " + iTipoMovimiento + Environment.NewLine;
                sSql = sSql + "and fecha = '" + sFecha + "'" + Environment.NewLine;
                sSql = sSql + "and id_documento_pago is null" + Environment.NewLine;
                sSql = sSql + "and estado = 'A'" + Environment.NewLine;
                sSql = sSql + "and id_pos_jornada = " + Program.iJornadaRecuperada + Environment.NewLine;
                sSql = sSql + "order by id_pos_movimiento_caja";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dSuma = 0;

                        sTexto = "";
                        sTexto = sTexto + Program.local + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;

                        if (iTipoMovimiento == 1)
                        {
                            sTexto = sTexto + "".PadRight(8, ' ') + "MANUALES MANUALES (CAJA)" + Environment.NewLine;
                        }

                        else
                        {
                            sTexto = sTexto + "".PadRight(8, ' ') + "SALIDAS MANUALES (CAJA)" + Environment.NewLine;
                        }

                        sTexto = sTexto + "".PadRight(40, '=') + Environment.NewLine;
                        sTexto = sTexto + "Fecha  : " + sFecha + " - " + sFecha + Environment.NewLine;
                        sTexto = sTexto + "Desde  : " + dtConsulta.Rows[0][0].ToString() + " Horas" + Environment.NewLine;
                        sTexto = sTexto + "Hasta  : " + dtConsulta.Rows[dtConsulta.Rows.Count - 1][0].ToString() + " Horas" + Environment.NewLine;
                        sTexto = sTexto + "Terminal: <Todas>" + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(40, '-') + Environment.NewLine;
                        sTexto = sTexto + "HORA  " + "CONCEPTO".PadRight(27, ' ') + "VALOR" + Environment.NewLine;
                        sTexto = sTexto + "".PadRight(40, '-') + Environment.NewLine;

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i][1].ToString().Length <= 25)
                            {
                                sTexto = sTexto + dtConsulta.Rows[i][0].ToString().Substring(0, 5).PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().PadRight(27, ' ') + dtConsulta.Rows[i][2].ToString().PadLeft(7, ' ') + Environment.NewLine;
                            }

                            else
                            {
                                sTexto = sTexto + dtConsulta.Rows[i][0].ToString().Substring(0, 5).PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().Substring(0, 25).PadRight(27, ' ') + dtConsulta.Rows[i][2].ToString().PadLeft(7, ' ') + Environment.NewLine;

                                for (int j = 25; j < dtConsulta.Rows[i][1].ToString().Length; j++)
                                {
                                    iLongi = dtConsulta.Rows[i][1].ToString().Substring(j).Length;

                                    if (iLongi > 25)
                                    {
                                        sTexto = sTexto + "".PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().Substring(j, 25) + Environment.NewLine;
                                        j = j + 25;
                                    }

                                    else
                                    {
                                        sTexto = sTexto + "".PadRight(6, ' ') + dtConsulta.Rows[i][1].ToString().Substring(j, iLongi) + Environment.NewLine;
                                        break;
                                    }
                                }
                            }

                            dSuma = dSuma + Convert.ToDouble(dtConsulta.Rows[i][2].ToString());
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
                catchMensaje.ShowDialog();
                return "";
            }
        }
    }
}
