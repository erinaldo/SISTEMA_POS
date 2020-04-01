using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmMovimientosCaja : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeSiNo SiNo;
        VentanasMensajes.frmMensajeOK ok;
        VentanasMensajes.frmMensajeCatch catchMensaje;

        Clases.libroCaja libro = new Clases.libroCaja();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();

        DataTable dtConsulta;
        bool bRespuesta;
        
        string sSql;
        string sTabla;
        string sCampo;
        string sFecha;

        int iIdEmpleado = 0;
        int iTipoMovimiento = 0;
        int iTipoCargo;
        int iIdPosMovimientoCaja;        
        int iOp;

        public frmMovimientosCaja(int iOp)
        {
            this.iOp = iOp;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VERIFICAR SI TIENE UN DOCUMENTO PAGO
        public int verificarDocumentoPago()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where id_documento_pago is not null" + Environment.NewLine;
                sSql += "and id_pos_movimiento_caja = " + Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value) + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }

                    else
                    {
                        return 0;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            cmbSeleccionarCaja.Text = "Fecha";
            btnCalendario.Enabled = true;

            numeroMovimiento();
            llenarComboEmpleado();
            llenarComboCaja();
            LLenarComboCargos();

            rdbEgresos.Checked = true;

            txtValor.Clear();
            txtConcepto.Clear();
            iIdEmpleado = 0;
            iIdPosMovimientoCaja = 0;
            grupoNuevo.Enabled = false;
        }
        
        //FUNCION PARA EXTRAER EL NUMERO DE MOVIMIENTO DE LA TABLA TP_LOCALIDADES_IMPRESORAS
        private void numeroMovimiento()
        {
            try
            {
                sSql = "";
                sSql += "select numeromovimientocaja" + Environment.NewLine;
                sSql += "from tp_localidades_impresoras" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtNumero.Text = dtConsulta.Rows[0][0].ToString();
                    }
                }
                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE LOCALIDADES
        private void llenarComboLocalidades()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbLocalidad.DisplayMember = "nombre_localidad";
                    cmbLocalidad.ValueMember = "id_localidad";
                    cmbLocalidad.DataSource = dtConsulta;

                    cmbLocalidad.SelectedValue = Program.iIdLocalidad;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE CAJA
        private void llenarComboCaja()
        {
            try
            {
                sSql = "";
                sSql += "select CJ.id_caja, CJ.descripcion " + Environment.NewLine;
                sSql += "from cv405_cajas CJ, tp_localidades LOC, tp_vw_tipo_caja TCJ " + Environment.NewLine;
                sSql += "where CJ.id_localidad = LOC.id_localidad" + Environment.NewLine;
                sSql += "and CJ.cg_tipo_caja = TCJ.correlativo " + Environment.NewLine;
                sSql += "and TCJ.codigo = 'GENERAL'" + Environment.NewLine;
                sSql += "and LOC.id_localidad = " + Program.iIdLocalidad;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbCaja.DisplayMember = "descripcion";
                    cmbCaja.ValueMember = "id_caja";
                    cmbCaja.DataSource = dtConsulta;

                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE EMPLEADOS
        private void llenarComboEmpleado()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, descripcion" + Environment.NewLine;
                sSql += "from pos_vw_cajero_mesero" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by descripcion";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbEmpleados.DisplayMember = "descripcion";
                    cmbEmpleados.ValueMember = "id_persona";
                    cmbEmpleados.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR
        private void insertarRegistro()
        {
            try
            {
                //EXTRAER LA FECHA DEL SISTEMA
                sSql = "";
                sSql += "select getdate() fecha";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }

                sFecha = Convert.ToDateTime(dtConsulta.Rows[0]["fecha"].ToString()).ToString("yyyy-MM-dd");

                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //INSTRUCCION INSERTAR EN LA TABLA POS_MOVIMIENTO_CAJA
                sSql = "";
                sSql += "insert into pos_movimiento_caja (" + Environment.NewLine;
                sSql += "tipo_movimiento, idempresa, id_localidad, id_persona," + Environment.NewLine;
                sSql += "id_caja, id_pos_cargo, fecha, hora, cg_moneda, valor, concepto," + Environment.NewLine;
                sSql += "id_pos_jornada, id_pos_cierre_cajero, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iTipoMovimiento + ", " + Program.iIdEmpresa + ", " + Program.iIdLocalidad + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbEmpleados.SelectedValue) + ", " + Convert.ToInt32(cmbCaja.SelectedValue) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbCargo.SelectedValue) + ", '" + sFecha + "', GETDATE()," + Environment.NewLine;
                sSql += Program.iMoneda + ", " + Convert.ToDouble(txtValor.Text.Trim()) + "," + Environment.NewLine;
                sSql += "'" + txtConcepto.Text.Trim() + "', " + Program.iJORNADA + ", " + Program.iIdPosCierreCajero + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_MOVIMIENTO_CAJA
                sTabla = "pos_movimiento_caja";
                sCampo = "id_pos_movimiento_caja"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No se pudo obtener el codigo del movimiento.";
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdPosMovimientoCaja = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION INSERTAR EN LA TABLA POS_NUMERO_MOVIMIENTO_CAJA
                sSql = "";
                sSql += "insert into pos_numero_movimiento_caja (" + Environment.NewLine;
                sSql += "id_pos_movimiento_caja, numero_movimiento_caja, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdPosMovimientoCaja + ", " + Convert.ToInt32(txtNumero.Text.Trim()) + "," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION ACTUALIZAR EL NUMERO DE MOVIMIENTO EN TP_LOCALIDADES_IMPRESORAS
                sSql = "";
                sSql += "update tp_localidades_impresoras set" + Environment.NewLine;
                sSql += "numeromovimientocaja = numeromovimientocaja + 1" + Environment.NewLine;
                sSql += "where id_localidad = " + Program.iIdLocalidad + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Movimiento ingresado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(1);
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += "update pos_movimiento_caja set " + Environment.NewLine;
                sSql += "tipo_movimiento = " + iTipoMovimiento + "," + Environment.NewLine;
                sSql += "id_persona = " + Convert.ToInt32(cmbEmpleados.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_caja = " + Convert.ToInt32(cmbCaja.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_pos_cargo = " + Convert.ToInt32(cmbCargo.SelectedValue) + "," + Environment.NewLine;
                sSql += "valor = " + Convert.ToDouble(txtValor.Text) + "," + Environment.NewLine;
                sSql += "concepto = '" + txtConcepto.Text + "'" + Environment.NewLine;
                sSql += "where id_pos_movimiento_caja = " + iIdPosMovimientoCaja + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Movimiento actualizado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(1);
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += "update pos_movimiento_caja set " + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_movimiento_caja = " + iIdPosMovimientoCaja + Environment.NewLine;
                sSql += "and estado = 'A'";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Movimiento eliminado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(1);
                limpiar();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
        }

        //FUNCION PARA LLENAR EL DATAGRID
        private void llenarGrid(int op)
        {
            try
            {
                sSql = "";
                sSql += "select MC.id_pos_movimiento_caja, NM.numero_movimiento_caja 'NUM.', MC.fecha FECHA, convert(varchar, hora, 108) HORA, MC.concepto CONCEPTO," + Environment.NewLine;
                sSql += "case (MC.id_cliente) when NULL then '' else TP.apellidos + ' ' + isnull(TP.nombres, '') end CLIENTE, " + Environment.NewLine;
                sSql += "isnull(MC.documento_venta, '') 'DOC. VENTA', " + Environment.NewLine;
                sSql += "case (MC.tipo_movimiento) when 1 then ltrim(str(isnull(MC.valor, 0), 8, 2)) else '' end 'ENTRADA'," + Environment.NewLine;
                sSql += "case (MC.tipo_movimiento) when 0 then ltrim(str(isnull(MC.valor, 0), 8, 2)) else '' end 'SALIDA'," + Environment.NewLine;
                sSql += "case (MC.tipo_movimiento) when 1 then 'INGRESO' else 'EGRESO' end 'MOVIMIENTO' " + Environment.NewLine;
                sSql += "from pos_movimiento_caja MC INNER JOIN " + Environment.NewLine;
                sSql += "pos_numero_movimiento_caja NM ON MC.id_pos_movimiento_caja = NM.id_pos_movimiento_caja" + Environment.NewLine;
                sSql += "and NM.estado = 'A'" + Environment.NewLine;
                sSql += "and MC.estado = 'A' LEFT OUTER JOIN " + Environment.NewLine;
                sSql += "tp_personas TP ON MC.id_cliente = TP.id_persona" + Environment.NewLine;
                sSql += "and TP.estado = 'A'" + Environment.NewLine;
                sSql += "where MC.id_localidad = " + cmbLocalidad.SelectedValue + Environment.NewLine;

                if (op == 1)
                {
                    //sSql += "where MC.fecha = '" + LblFecha.Text + "'" + Environment.NewLine;

                    if (iOp == 1)
                    {
                        sSql += "and MC.tipo_movimiento = 1 and MC.id_documento_pago is not null" + Environment.NewLine;
                    }

                    else if (iOp == 2)
                    {
                        sSql += "and MC.tipo_movimiento = 0 and MC.id_documento_pago is null" + Environment.NewLine;
                    }
                }

                else
                {
                    if (iOp == 1)
                    {
                        sSql += "and MC.tipo_movimiento = 1 and MC.id_documento_pago is null" + Environment.NewLine;
                    }

                    else if (iOp == 2)
                    {
                        sSql += "and MC.tipo_movimiento = 0 and MC.id_documento_pago is null" + Environment.NewLine;
                    }
                }

                sSql += "and MC.id_pos_cierre_cajero = " + Program.iIdPosCierreCajero + Environment.NewLine;
                sSql += "order by MC.id_pos_movimiento_caja" + Environment.NewLine;
                
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        dgvDatos.Columns[1].Width = 55;
                        dgvDatos.Columns[2].Width = 75;
                        dgvDatos.Columns[3].Width = 70;
                        dgvDatos.Columns[4].Width = 150;
                        dgvDatos.Columns[5].Width = 230;
                        dgvDatos.Columns[6].Width = 160;
                        dgvDatos.Columns[7].Width = 70;
                        dgvDatos.Columns[8].Width = 70;

                        dgvDatos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDatos.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        //INSTRUCCION PARA COLOREAR LAS LINEAS DEL GRID
                        for (int i = 0; i < dgvDatos.Rows.Count; i++)
                        {
                            if (dgvDatos.Rows[i].Cells[9].Value.ToString() == "INGRESO")
                            {
                                dgvDatos.Rows[i].DefaultCellStyle.ForeColor = Color.DarkBlue;
                            }

                            else
                            {
                                dgvDatos.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            }
                        }

                        dgvDatos.Columns[0].Visible = false;
                        dgvDatos.Columns[9].Visible = false;
                        //dgvDatos.Columns[10].Visible = false;
                        dgvDatos.Rows[0].Selected = true;
                        dgvDatos.CurrentCell = dgvDatos.Rows[0].Cells[1];
                    }

                    else
                    {
                        dgvDatos.DataSource = dtConsulta;

                        dgvDatos.Columns[1].Width = 65;
                        dgvDatos.Columns[2].Width = 75;
                        dgvDatos.Columns[3].Width = 70;
                        dgvDatos.Columns[4].Width = 150;
                        dgvDatos.Columns[5].Width = 230;
                        dgvDatos.Columns[6].Width = 180;
                        dgvDatos.Columns[7].Width = 70;
                        dgvDatos.Columns[8].Width = 70;

                        dgvDatos.Columns[0].Visible = false;
                        dgvDatos.Columns[9].Visible = false;
                        //dgvDatos.Columns[10].Visible = false;
                    }

                    dgvDatos.ClearSelection();
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA CONSULTAR EL REGISTRO PARA LA ACTUALIZACION
        private bool consultarRegistro()
        {
            try
            {
                dgvDatos.Columns[0].Visible = true;
                iIdPosMovimientoCaja = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                dgvDatos.Columns[0].Visible = false;

                sSql = "";
                sSql += "select NC.numero_movimiento_caja, MC.hora, MC.valor, MC.id_caja, " + Environment.NewLine;
                sSql += "MC.concepto, MC.tipo_movimiento, MC.id_persona " + Environment.NewLine;
                sSql += "from pos_movimiento_caja MC, pos_numero_movimiento_caja NC " + Environment.NewLine;
                sSql += "where MC.id_pos_movimiento_caja = NC.id_pos_movimiento_caja " + Environment.NewLine;
                sSql += "and MC.estado = 'A' " + Environment.NewLine;
                sSql += "and NC.estado = 'A' " + Environment.NewLine;
                sSql += "and MC.id_pos_movimiento_caja = " + iIdPosMovimientoCaja;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        txtNumero.Text = dtConsulta.Rows[0][0].ToString();
                        txtFecha.Text = dtConsulta.Rows[0][1].ToString();
                        txtValor.Text = dtConsulta.Rows[0][2].ToString();
                        cmbCaja.SelectedValue = dtConsulta.Rows[0][3].ToString();
                        txtConcepto.Text = dtConsulta.Rows[0][4].ToString();

                        if (Convert.ToBoolean(dtConsulta.Rows[0][5].ToString()) == true)
                        {
                            rdbIngresos.Checked = true;
                        }

                        else
                        {
                            rdbEgresos.Checked = true;
                        }

                        cmbEmpleados.SelectedValue = dtConsulta.Rows[0][6].ToString();
                        grupoNuevo.Enabled = true;
                        txtValor.Focus();
                        return true;
                    }

                    else
                    {
                        ok = new VentanasMensajes.frmMensajeOK();
                        ok.LblMensaje.Text = "No existe el registro. Favor comuníquese con el admimnistrador para solucionar el problema.";
                        ok.ShowDialog();
                        return false;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //llenar el comboBox de Cargos
        private void LLenarComboCargos()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_pos_cargo, descripcion" + Environment.NewLine;
                sSql += "from pos_cargo" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbCargo.DisplayMember = "descripcion";
                    cmbCargo.ValueMember = "id_pos_cargo";
                    cmbCargo.DataSource = dtConsulta;
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeCatch();
                    catchMensaje.LblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeCatch();
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmMovimientosCaja_Load(object sender, EventArgs e)
        {
            sFecha = DateTime.Now.ToString("dd-MM-yyyy");

            LblFecha.Text = sFecha;

            cmbFiltrar.SelectedIndexChanged -= new EventHandler(cmbFiltrar_SelectedIndexChanged);

            if (iOp == 1)
            {
                cmbFiltrar.Text = "Entradas";
            }

            else if (iOp == 2)
            {
                cmbFiltrar.Text = "Salidas";
            }

            else
            {
                cmbFiltrar.Text = "Todas";
            }

            cmbFiltrar.SelectedIndexChanged += new EventHandler(cmbFiltrar_SelectedIndexChanged);

            cmbSeleccionarCaja.SelectedIndexChanged -= new EventHandler(cmbSeleccionarCaja_SelectedIndexChanged);
            cmbSeleccionarCaja.Text = "Fecha";
            cmbSeleccionarCaja.SelectedIndexChanged += new EventHandler(cmbSeleccionarCaja_SelectedIndexChanged);
            
            btnCalendario.Enabled = true;

            cmbLocalidad.SelectedIndexChanged -= new EventHandler(cmbLocalidad_SelectedIndexChanged);
            llenarComboLocalidades();
            cmbLocalidad.SelectedIndexChanged += new EventHandler(cmbLocalidad_SelectedIndexChanged);
            llenarComboCaja();
            llenarComboEmpleado();
            llenarGrid(1);
            LLenarComboCargos();
            limpiar();
            
        }

        private void TimerFecha_Tick(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txtValor.Text.Length; i++)
            {
                if (txtValor.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 4)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((txtValor.Text == "") || (Convert.ToDouble(txtValor.Text) == 0))
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el monto a retirar o ingresar.";
                ok.ShowDialog();
            }

            else if (txtConcepto.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor ingrese el concepto del retiro o ingreso.";
                ok.ShowDialog();
            }

            else if (cmbEmpleados.SelectedValue.ToString() == "0")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor seleccione el empleado que hace uso del ingreso o egreso.";
                ok.ShowDialog();
            }

            else if (cmbCaja.SelectedValue.ToString() == "0")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor seleccione el tipo de caja a descontar.";
                ok.ShowDialog();
            }

            else if (cmbCargo.SelectedValue.ToString() == "0")
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "Favor seleccione el tipo de cargo.";
                ok.ShowDialog();
            }

            else
            {
                if (rdbIngresos.Checked == true)
                {
                    iTipoMovimiento = 1;
                }

                else if (rdbEgresos.Checked == true)
                {
                    iTipoMovimiento = 0;
                }

                if (iIdPosMovimientoCaja == 0)
                {
                    insertarRegistro();
                }

                else
                {
                    actualizarRegistro();
                }
            }
        }

        private void cmbSeleccionarCaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSeleccionarCaja.Text == "Todas")
            {
                btnCalendario.Enabled = false;
                llenarGrid(0);                
            }

            else
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                dgvDatos.DataSource = dtConsulta;                
                llenarGrid(1);
                btnCalendario.Enabled = true;
            }
        }

        private void btnCalendario_Click(object sender, EventArgs e)
        {
            //sFecha = DateTime.Now.ToString("dd/MM/yyyy");
            //Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(sFecha);
            //calendario.ShowDialog();

            //if (calendario.DialogResult == DialogResult.OK)
            //{
                //sFecha = calendario.txtFecha.Text.Trim();
                //sFecha = Convert.ToDateTime(sFecha).ToString("yyyy/MM/dd");
                //LblFecha.Text = Convert.ToDateTime(sFecha).ToString("yyyy-MM-dd");
                //llenarGrid(1);
            //}
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems para realizar ediciones.";
                ok.ShowDialog();
            }

            else
            {
                if (verificarDocumentoPago() == 1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No puede realizar ediciones de un ingreso que se ha ingresado con la comanda.";
                    ok.ShowDialog();
                }

                else
                {
                    consultarRegistro();
                }                
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                if (verificarDocumentoPago() == 1)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No puede realizar ediciones de un ingreso que se ha ingresado con la comanda.";
                    ok.ShowDialog();
                }

                else
                {
                    if (consultarRegistro() == true)
                    {
                        SiNo = new VentanasMensajes.frmMensajeSiNo();
                        SiNo.LblMensaje.Text = "Está seguro que desea eliminar el ingreso. Tenga en cuenta que puede alterar el arqueo de caja.";
                        SiNo.ShowDialog();

                        if (SiNo.DialogResult == DialogResult.OK)
                        {
                            eliminarRegistro();
                        }

                        else
                        {
                            limpiar();
                        }
                    }
                }    
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (verificarDocumentoPago() == 1)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No puede realizar ediciones de un ingreso que se ha ingresado con la comanda.";
                ok.ShowDialog();
            }

            else
            {
                consultarRegistro();
            }        
        }

        private void cmbFiltrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltrar.Text == "Todas")
            {
                iOp = 0;
                
            }

            else if (cmbFiltrar.Text == "Entradas")
            {
                iOp = 1;
            }

            else
            {
                iOp = 2;
            }


            if (cmbSeleccionarCaja.Text == "Todas")
            {
                llenarGrid(0);
                btnCalendario.Enabled = false;
            }

            else
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();
                dgvDatos.DataSource = dtConsulta;
                llenarGrid(1);
                btnCalendario.Enabled = true;
            }

        }

        private void btnImprimirLibroCaja_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay registros para imprimir.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }

            else
            {
                ReportesTextBox.frmVerLibroDiario libroDiario = new ReportesTextBox.frmVerLibroDiario(1, Program.iIdPosCierreCajero);
                libroDiario.ShowDialog();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grupoNuevo.Enabled = true;
            txtConcepto.Focus();
        }

        private void btnImpimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok = new VentanasMensajes.frmMensajeOK();
                ok.LblMensaje.Text = "No hay ítems para imprimir.";
                ok.ShowDialog();
            }

            else
            {
                Program.iJornadaRecuperada = Program.iJORNADA;
                Pedidos.frmVerMovimientoCaja movimiento = new Pedidos.frmVerMovimientoCaja(Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value), 1);
                movimiento.ShowDialog();
            }
        }

        private void frmMovimientosCaja_KeyDown(object sender, KeyEventArgs e)
        {
            if (Program.iPermitirAbrirCajon == 1)
            {
                if (e.KeyCode == Keys.F7)
                {
                    if (Program.iPuedeCobrar == 1)
                    {
                        abrir.consultarImpresoraAbrirCajon();
                    }
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.Control && e.KeyCode == Keys.N)
            {
                grupoNuevo.Enabled = true;
                txtConcepto.Focus();
            }

            if (e.Control && e.KeyCode == Keys.P)
            {
                if (dgvDatos.Rows.Count == 0)
                {
                    ok = new VentanasMensajes.frmMensajeOK();
                    ok.LblMensaje.Text = "No hay ítems para imprimir.";
                    ok.ShowDialog();
                }

                else
                {
                    Pedidos.frmVerMovimientoCaja movimiento = new Pedidos.frmVerMovimientoCaja(Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value), 1);
                    movimiento.ShowDialog();
                }
            }
        }

        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = ((DataGridView)sender);
            string rowIndex = (e.RowIndex + 1).ToString();
            Font rowFont = new System.Drawing.Font("Tahoma", 8.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;

            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIndex, rowFont, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid(1);
        }
    }
}
