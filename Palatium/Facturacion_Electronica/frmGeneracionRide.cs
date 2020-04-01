using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmGeneracionRide : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        Clases.ClaseValidarCaracteres numero = new Clases.ClaseValidarCaracteres();
        Clases_Factura_Electronica.ClaseGenerarRIDE ride = new Clases_Factura_Electronica.ClaseGenerarRIDE();

        DataTable dtConsulta;

        string sSql;
        string sNumeroComprobante;
        string sFechaInicial;
        string sFechaFinal;
        string sDirGenerados;
        string sDirFirmados;
        string sDirAutorizados;
        string sDirNoAutorizados;

        string filename;

        bool bRespuesta;

         public frmGeneracionRide()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LIMPIAR
         private void limpiar()
         {
             cargarCombo();

             txtInicio.Clear();
             txtFin.Clear();

             txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
             txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
         }

         //CARGAR EL DIRECTORIO DONDE SE GUARDARAN LOS XML GENERADOS
         private bool buscarDirectorio()
         {
             try
             {
                 sSql = "";
                 sSql += "select codigo, nombres" + Environment.NewLine;
                 sSql += "from cel_directorio" + Environment.NewLine;
                 sSql += "where id_tipo_comprobante = 1" + Environment.NewLine;
                 sSql += "and estado = 'A'";

                 dtConsulta = new DataTable();
                 dtConsulta.Clear();

                 bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                 if (bRespuesta == true)
                 {
                     if (dtConsulta.Rows.Count > 0)
                     {
                         sDirGenerados = dtConsulta.Rows[0][1].ToString();
                         sDirFirmados = dtConsulta.Rows[1][1].ToString();
                         sDirAutorizados = dtConsulta.Rows[2][1].ToString();
                         sDirNoAutorizados = dtConsulta.Rows[3][1].ToString();
                         return true;
                     }

                     else
                     {
                         ok.LblMensaje.Text = "No existe una configuracion de directorio para guardar los xml genereados.";
                         ok.ShowDialog();
                         return false;
                     }
                 }

                 else
                 {
                     catchMensaje.LblMensaje.Text = sSql;
                     catchMensaje.ShowDialog();
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

        //FUNCION PARA CARGAR COMBO DE TIPOS DE COMPROBANTES
        private void cargarCombo()
        {
            try
            {
                sSql = "";
                sSql += "select id_tipo_comprobante, nombres, codigo" + Environment.NewLine;
                sSql += "from cel_tipo_comprobante" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbTipoComprobante.DisplayMember = "nombres";
                        cmbTipoComprobante.ValueMember = "id_tipo_comprobante";
                        cmbTipoComprobante.Text= "codigo";
                        cmbTipoComprobante.DataSource = dtConsulta;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {

            }
        }

        //CREAR RIDE
        private void crearRide(long iIdFactura_P, string sNumeroDocumento)
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                filename = sDirAutorizados + @"\" + sNumeroDocumento + ".pdf";

                bRespuesta = conexion.GFun_Lo_Genera_Ride(dtConsulta, iIdFactura_P);

                if (bRespuesta == true)
                {
                    bRespuesta = ride.generarRide(dtConsulta, filename, iIdFactura_P);

                    if (bRespuesta == false)
                    {
                        ok.LblMensaje.Text = "Error al crear el reporte RIDE de la factura " + sNumeroDocumento;
                        ok.ShowDialog();
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA EXTRAER EL CODIGO DEL COMPROBANTE
        private void extraerCodigoComprobante()
        {
            try
            {
                sSql = "";
                sSql += "select codigo" + Environment.NewLine;
                sSql += "from cel_tipo_comprobante" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if (dtConsulta.Rows[0][0].ToString() == "01")
                        {
                            sNumeroComprobante = sNumeroComprobante + "F";
                        }

                        else if (dtConsulta.Rows[0][0].ToString() == "04")
                        {
                            sNumeroComprobante = sNumeroComprobante + "NC";
                        }

                        else if (dtConsulta.Rows[0][0].ToString() == "05")
                        {
                            sNumeroComprobante = sNumeroComprobante + "ND";
                        }

                        else if (dtConsulta.Rows[0][0].ToString() == "06")
                        {
                            sNumeroComprobante = sNumeroComprobante + "G";
                        }

                        else if (dtConsulta.Rows[0][0].ToString() == "07")
                        {
                            sNumeroComprobante = sNumeroComprobante + "R";
                        }
                    }

                    else
                    {
                        catchMensaje.LblMensaje.Text = "No existe el código del tipo de comprobante " + cmbTipoComprobante.Text;
                        catchMensaje.ShowDialog();
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();
                sNumeroComprobante = "";

                extraerCodigoComprobante();

                
                sFechaInicial = Convert.ToDateTime(txtFechaInicial.Text.Trim()).ToString("yyyy/MM/dd");
                sFechaFinal = Convert.ToDateTime(txtFechaFinal.Text.Trim()).ToString("yyyy/MM/dd");

                sSql = "";
                sSql += "select F.id_factura, VL.establecimiento, VL.punto_emision, NF.numero_factura," + Environment.NewLine;
                sSql += "(P.nombres + ' ' +P.apellidos ) cliente, P.correo_electronico, F.fecha_factura," + Environment.NewLine;
                sSql += "F.clave_acceso, TA.nombres" + Environment.NewLine;
                sSql += "from cv403_facturas F, cv403_numeros_facturas NF, tp_personas P, " + Environment.NewLine;
                sSql += "tp_vw_localidades VL, cel_tipo_ambiente TA" + Environment.NewLine;
                sSql += "where NF.id_factura = F.id_factura" + Environment.NewLine;
                sSql += "and F.id_localidad = VL.id_localidad" + Environment.NewLine;
                sSql += "and F.id_persona = P.id_persona" + Environment.NewLine;
                sSql += "and F.id_tipo_ambiente = TA.id_tipo_ambiente" + Environment.NewLine;
                sSql += "and VL.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and F.estado = 'A'" + Environment.NewLine;
                sSql += "and NF.estado = 'A'" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and TA.estado = 'A'" + Environment.NewLine;
                sSql += "and F.fecha_factura between '" + sFechaInicial + "'" + Environment.NewLine;
                sSql += "and '" + sFechaFinal + "'" + Environment.NewLine;
                sSql += "and F.idtipocomprobante = " + Convert.ToInt32(cmbTipoComprobante.SelectedValue) + Environment.NewLine;
                sSql += "and autorizacion <> ''";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sNumeroComprobante = "";
                            sNumeroComprobante = sNumeroComprobante + dtConsulta.Rows[i][1].ToString() +
                                                 dtConsulta.Rows[i][2].ToString() +
                                                 dtConsulta.Rows[i][3].ToString().PadLeft(9, '0');
                            
                            dgvDatos.Rows.Add(
                                                false,                                                
                                                dtConsulta.Rows[i][0].ToString(),
                                                cmbTipoComprobante.Text,
                                                sNumeroComprobante,
                                                dtConsulta.Rows[i][4].ToString(),
                                                dtConsulta.Rows[i][5].ToString(),
                                                dtConsulta.Rows[i][6].ToString().Substring(0, 10),
                                                dtConsulta.Rows[i][7].ToString(),
                                                dtConsulta.Rows[i][8].ToString()
                                );
                        }

                        dgvDatos.Columns["colMarca"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvDatos.ClearSelection();
                        lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                    }

                    else
                    {
                        dgvDatos.Rows.Clear();
                        lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }

            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        #endregion

        private void frmGeneracionRide_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmGeneracionRide_Load(object sender, EventArgs e)
        {
            buscarDirectorio();
            limpiar();
        }

        private void btnDesde_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtFechaInicial.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtFechaInicial.Text = calendario.txtFecha.Text;

                if (Convert.ToDateTime(txtFechaInicial.Text) > Convert.ToDateTime(txtFechaFinal.Text))
                {
                    ok.LblMensaje.Text = "La fecha inicial no puede ser superior a la ficha final del rango.";
                    ok.ShowDialog();
                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }

            }
        }

        private void btnHasta_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(txtFechaFinal.Text.Trim());
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                txtFechaFinal.Text = calendario.txtFecha.Text;

                if (Convert.ToDateTime(txtFechaInicial.Text) > Convert.ToDateTime(txtFechaFinal.Text))
                {
                    ok.LblMensaje.Text = "La fecha inicial no puede ser superior a la ficha final del rango.";
                    ok.ShowDialog();
                    txtFechaInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtFechaFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        private void txtInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            numero.soloNumeros(e);
        }

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            numero.soloNumeros(e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count > 0)
            {
                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    crearRide(Convert.ToInt64(dgvDatos.Rows[i].Cells[1].Value), dgvDatos.Rows[i].Cells[3].Value.ToString());
                }
            }

            else
            {
                catchMensaje.LblMensaje.Text = "No hay registros recuperados para generar los RIDE.";
                catchMensaje.ShowDialog();
            }
        }
    }
}
