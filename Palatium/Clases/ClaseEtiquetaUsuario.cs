using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Palatium.Clases
{
    class ClaseEtiquetaUsuario
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sEtiqueta;
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta = false;

        ///     TIPO O MARCA DE BASE DE DATOS
        ///     MODULO
        ///     VERSION
        ///     -
        ///     USUARIO CONECTADO O VALIDADO
        ///     @
        ///     NOMBRE DE LA BASE DE DATOS
        ///     .
        ///     NOMBRE SERVIDOR
        ///     -
        ///     EMPRESA
        ///     

        public void crearEtiquetaUsuario()
        {
            try
            {
                
                sEtiqueta = "";
                sEtiqueta = sEtiqueta + "[" + Program.SQLCONEXION + "(Módulo de Punto de Venta)][" + Program.sVersionProducto + "] - ";
                sEtiqueta = sEtiqueta + Program.sDatosMaximo[0].ToLower() + "@";
                sEtiqueta = sEtiqueta + Program.SQLBDATOS.ToLower() + "." + Program.SQLSERVIDOR.ToLower();
                sEtiqueta = sEtiqueta + " - Empresa: ";

                sSql = "";
                sSql = sSql + "select isnull(razonsocial, 'NO REGISTRADA') razonsocial " + Environment.NewLine;
                sSql = sSql + "from sis_empresa " + Environment.NewLine;
                sSql = sSql + "where idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sEtiqueta = sEtiqueta + dtConsulta.Rows[0].ItemArray[0].ToString();
                    }

                    else
                    {
                        sEtiqueta = sEtiqueta + "NO REGISTRADA";
                    }

                    Program.sEtiqueta = sEtiqueta;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        public void crearEtiquetaAdministracion()
        {
            try
            {

                sEtiqueta = "";
                sEtiqueta = sEtiqueta + "[" + Program.SQLCONEXION + "(Módulo de Administración de Punto de Venta)][" + Program.sVersionProducto + "] - ";
                sEtiqueta = sEtiqueta + Program.sDatosMaximo[0].ToLower() + "@";
                sEtiqueta = sEtiqueta + Program.SQLBDATOS.ToLower() + "." + Program.SQLSERVIDOR.ToLower();
                sEtiqueta = sEtiqueta + " - Empresa: ";

                sSql = "";
                sSql = sSql + "select isnull(razonsocial, 'NO REGISTRADA') razonsocial " + Environment.NewLine;
                sSql = sSql + "from sis_empresa " + Environment.NewLine;
                sSql = sSql + "where idempresa = " + Program.iIdEmpresa + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        sEtiqueta = sEtiqueta + dtConsulta.Rows[0].ItemArray[0].ToString();
                    }

                    else
                    {
                        sEtiqueta = sEtiqueta + "NO REGISTRADA";
                    }

                    Program.sEtiquetaAdministrador = sEtiqueta;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        public void crearEtiquetaVacia()
        {
            Program.sEtiqueta = " ";
        }
    }
}
