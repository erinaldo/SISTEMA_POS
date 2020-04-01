using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Pedidos
{
    public partial class frmReabrirCaja : Form
    {
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        string sSql;
        string sFecha;
        DataTable dtConsulta;
        bool bRespuesta = false;

        public frmReabrirCaja()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LLENAR LABLES
        private void lblLlenarLabels()
        {
            dgvInformacion.Columns[0].Visible = true;
            LblCajero.Text = dgvInformacion.CurrentRow.Cells[5].Value.ToString();
            LblApertura.Text = dgvInformacion.CurrentRow.Cells[1].Value.ToString();
            LblCierre.Text = dgvInformacion.CurrentRow.Cells[2].Value.ToString();
            LblEstado.Text = dgvInformacion.CurrentRow.Cells[4].Value.ToString();
            LblId.Text = dgvInformacion.CurrentRow.Cells[0].Value.ToString();
            dgvInformacion.Columns[0].Visible = false;
        }

        //LIMPIAR LABELS
        private void limpiarLabels()
        {
            LblCajero.Text = "";
            LblApertura.Text = "";
            LblCierre.Text = "";
            LblEstado.Text = "";
            LblId.Text = "";
        }

        //LISTAR EL COMBOBOX DE JORNADA
        private void llenarComboJornada()
        {
            try
            {
                DataTable dtPadre = new DataTable();
                sSql = "select id_pos_jornada, descripcion from pos_jornada where estado = 'A'";

                cmbJornada.llenar(dtPadre, sSql);

                if (cmbJornada.Items.Count == 0)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.\n\nComunìquese con el administrador en caso de presentar el mismo inconveniente.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
                else
                {
                    cmbJornada.SelectedIndex = 1;

                    if (Program.iManejaJornada == 1)
                    {
                        cmbJornada.Enabled = true;
                    }

                    else
                    {
                        cmbJornada.Enabled = false;
                    }
                }

            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        //LISTAR EL COMBOBOX DE LOCALIDAD
        //Función para llenar el Combo de Localidad
        private void llenarComboLocalidad()
        {
            try
            {
                DataTable dtPadre = new DataTable();
                sSql = "select id_localidad, nombre_localidad from tp_vw_localidades";
                cmbLocalidad.llenar(dtPadre, sSql);
                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
                //cmbLocalidad.SelectedIndex = 4;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //LLENAR EL DATAGRID
        private void llenarGrid(string sQuery)
        {
            try
            {                               
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sQuery);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvInformacion.DataSource = dtConsulta;
                        dgvInformacion.Columns[1].Width = 110;
                        dgvInformacion.Columns[2].Width = 110;
                        dgvInformacion.Columns[3].Width = 110;
                        dgvInformacion.Columns[4].Width = 110;
                        dgvInformacion.Columns[5].Width = 90;
                        dgvInformacion.Columns[0].Visible = false;
                    }

                    else
                    {
                        dgvInformacion.DataSource = dtConsulta;
                        ok.LblMensaje.Text = "No existen registros.";
                        ok.ShowDialog();
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un  problema al realizar la consulta.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA REAPERTURAR LA CAJA
        private void reabrirCaja()
        {
            try
            {
                sFecha = Program.sFechaSistema.ToString("yyyy-MM-dd");

                sSql = "";
                sSql = sSql + "select top 1 id_pos_cierre_cajero, fecha_apertura, estado_cierre_cajero" + Environment.NewLine;
                sSql = sSql + "from pos_cierre_cajero" + Environment.NewLine;
                //sSql = sSql + "where fecha_apertura = '" + sFecha + "'" + Environment.NewLine;
                sSql = sSql + "where id_localidad = " + Convert.ToInt32(cmbLocalidad.SelectedValue) + Environment.NewLine;
                sSql = sSql + "and id_jornada = " + Convert.ToInt32(cmbJornada.SelectedValue) + Environment.NewLine;
                sSql = sSql + "order by id_pos_cierre_cajero desc";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        if ((sFecha == Convert.ToDateTime(dtConsulta.Rows[0].ItemArray[1].ToString()).ToString("yyyy-MM-dd")) && (dtConsulta.Rows[0].ItemArray[2].ToString() == "Abierta"))
                        {
                            ok.LblMensaje.Text = "La caja del día en curso se encuentra abierta.";
                            ok.ShowDialog();
                            goto fin;
                        }

                        else if (sFecha == dtConsulta.Rows[0].ItemArray[1].ToString())
                        {
                            ok.LblMensaje.Text = "La caja del día en curso se encuentra abierta.";
                            ok.ShowDialog();
                            goto fin;
                        }

                        else
                        {
                            //PROCESO DE REAPERTURA
                            SiNo.LblMensaje.Text = "¿Está seguro que desea reaperturar la caja seleccionada?";
                            SiNo.ShowDialog();

                            if (SiNo.DialogResult == DialogResult.OK)
                            {
                                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                                {
                                    ok.LblMensaje.Text = "Error al abrir transacción.";
                                    ok.ShowDialog();
                                    goto fin;
                                }

                                sSql = "update pos_cierre_cajero set estado_cierre_cajero = 'Abierta' where id_pos_cierre_cajero = " + Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());

                                //EJECUTAMOS LA INSTRUCCIÒN SQL 
                                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                                {
                                    catchMensaje.LblMensaje.Text = sSql;
                                    catchMensaje.ShowDialog();
                                    goto reversa;
                                }

                                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                                ok.LblMensaje.Text = "La caja on fecha " + sFecha + " se ha reaperturado con éxito.";
                                ok.ShowDialog();
                                limpiarLabels();
                                goto fin;

                            }
                        }
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe una caja en la jornada actual. Recuerde que solo puede reabrir la última caja del mismo día.";
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                //MessageBox.Show("Ocurrió un problema en la transacción. No se guardarán los cambios");
            }

        fin:
            { }
        }

        #endregion

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReabrirCaja_Load(object sender, EventArgs e)
        {
            llenarComboLocalidad();
            llenarComboJornada();

            //sSql = "select id_pos_cierre_cajero, fecha_apertura + ' ' + hora_apertura as 'FECHA/HORA DE APERTURA', fecha_cierre + ' ' + hora_cierre as 'FECHA/HORA DE CIERRE', " +
            //          "terminal_ingreso as 'ESTACIÓN DE TRABAJO', estado_cierre_cajero as 'ESTADO DEL CIERRE', usuario_ingreso as CAJERO from pos_cierre_cajero where id_localidad = " + Program.iIdLocalidad;

            sSql = "";
            sSql = sSql + "select CC.id_pos_cierre_cajero, CC.fecha_apertura + ' ' + CC.hora_apertura as 'FECHA/HORA DE APERTURA'," + Environment.NewLine;
            sSql = sSql + "CC.fecha_cierre + ' ' + CC.hora_cierre as 'FECHA/HORA DE CIERRE', CC.terminal_ingreso as 'ESTACIÓN DE TRABAJO'," + Environment.NewLine;
            sSql = sSql + "CC.estado_cierre_cajero as 'ESTADO DEL CIERRE', C.descripcion as CAJERO" + Environment.NewLine;
            sSql = sSql + "from pos_cierre_cajero CC, pos_cajero C" + Environment.NewLine;
            sSql = sSql + "where CC.id_cajero = C.id_pos_cajero" + Environment.NewLine;
            sSql = sSql + "and CC.id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
            sSql = sSql + "and CC.estado = 'A'" + Environment.NewLine;
            sSql = sSql + "and C.estado = 'A'";

            llenarGrid(sSql);
            //lblLlenarLabels();
        }

        private void BtnExtraer_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToDateTime(Calendario.SelectionRange.Start.ToShortDateString()).ToString("yyyy/MM/dd"));
            try
            {
                string sFecha = Convert.ToDateTime(Calendario.SelectionRange.Start.ToShortDateString()).ToString("yyyy/MM/dd");

                sSql = "select id_pos_cierre_cajero, fecha_apertura + ' ' + hora_apertura as 'FECHA/HORA DE APERTURA', fecha_cierre + ' ' + hora_cierre as 'FECHA/HORA DE CIERRE', " +
                      "terminal_ingreso as 'ESTACIÓN DE TRABAJO', estado_cierre_cajero as 'ESTADO DEL CIERRE', usuario_ingreso as CAJERO from pos_cierre_cajero where fecha_apertura = '" + sFecha + "' " +
                       "and id_localidad = " + Program.iIdLocalidad + " and id_jornada = " + Convert.ToInt32(cmbJornada.SelectedValue);
                llenarGrid(sSql);
                lblLlenarLabels();
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        private void LblAbrirTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sSql = "select id_pos_cierre_cajero, fecha_apertura + ' ' + hora_apertura as 'FECHA/HORA DE APERTURA', fecha_cierre + ' ' + hora_cierre as 'FECHA/HORA DE CIERRE', " +
                      "terminal_ingreso as 'ESTACIÓN DE TRABAJO', estado_cierre_cajero as 'ESTADO DEL CIERRE', usuario_ingreso as CAJERO " +
                      "from pos_cierre_cajero where id_localidad = " + Program.iIdLocalidad;
            llenarGrid(sSql);
            lblLlenarLabels();
        }

        private void dgvInformacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblLlenarLabels();
        }

        private void BtnReabrir_Click(object sender, EventArgs e)
        {
            reabrirCaja();

        //    try
        //    {
        //        if (LblEstado.Text == "Abierta")
        //        {
        //            ok.LblMensaje.Text = "El estado actual de la caja es Abierta.";
        //            ok.ShowInTaskbar = false;
        //            ok.ShowDialog();
        //            goto fin;
        //        }

        //        else
        //        {
        //            SiNo.LblMensaje.Text = "¿Está seguro que desea reaperturar la caja seleccionada?";
        //            SiNo.ShowInTaskbar = false;
        //            SiNo.ShowDialog();

        //            if (SiNo.DialogResult == DialogResult.OK)
        //            {
        //                //INICIAMOS UNA NUEVA TRANSACCION
        //                //=======================================================================================================
        //                //=======================================================================================================
        //                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
        //                {
        //                    ok.LblMensaje.Text = "Error al abrir transacción.";
        //                    ok.ShowInTaskbar = false;
        //                    ok.ShowDialog();
        //                    goto fin;

        //                }
        //                //=======================================================================================================

        //                else
        //                {
        //                    sSql = "update pos_cierre_cajero set estado_cierre_cajero = 'Abierta' where id_pos_cierre_cajero = " + Convert.ToInt32(LblId.Text);

        //                    //EJECUTAMOS LA INSTRUCCIÒN SQL 
        //                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
        //                    {
        //                        catchMensaje.LblMensaje.Text = sSql;
        //                        catchMensaje.ShowInTaskbar = false;
        //                        catchMensaje.ShowDialog();
        //                        goto reversa;

        //                    }

        //                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
        //                    ok.LblMensaje.Text = "La caja se ha reaperturado con éxito.";
        //                    ok.ShowInTaskbar = false;
        //                    ok.ShowDialog();
        //                    frmReabrirCaja_Load(sender, e);
        //                    limpiarLabels();
        //                    goto fin;
        //                }
        //            }
        //            else
        //            {
        //                goto fin;
        //            }
        //        }
        //    }

        //    catch (Exception)
        //    {
        //        goto reversa;
        //    }

        // reversa:
        //    {
        //        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
        //        //MessageBox.Show("Ocurrió un problema en la transacción. No se guardarán los cambios");
        //    }

        //fin:
        //    { }

        }
    }
}
