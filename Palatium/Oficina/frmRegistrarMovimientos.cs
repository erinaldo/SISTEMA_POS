using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmRegistrarMovimientos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        Clases.libroCaja libro = new Clases.libroCaja();
        Clases.ClaseAbrirCajon abrir = new Clases.ClaseAbrirCajon();
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

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

        public frmRegistrarMovimientos(int iOp_P)
        {
            iOp = iOp_P;
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA VERIFICAR EL DOCUMENTO PAGO
        public int verificarDocumentoPago()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where id_documento_pago is not null" + Environment.NewLine;
                sSql += "and id_pos_movimiento_caja = " + (object)Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value) + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }

                    return 0;
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return 0;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return 0;
            }
        }

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            btnBuscar.Enabled = true;
            txtFechaIngreso.Enabled = true;
            sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");
            //cmbSeleccionarCaja.Text = "Fecha";
            //btnCalendario.Enabled = true;

            numeroMovimiento();
            llenarComboEmpleado();
            llenarComboCaja();
            LLenarComboCargos();
            cmbTipoMovimiento.Text = "Salidas";
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
                    ok.LblMensaje.Text = sSql;
                    ok.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE CAJA
        private void llenarComboCaja()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select CJ.id_caja, CJ.descripcion " + Environment.NewLine;
                sSql += "from cv405_cajas CJ, tp_localidades LOC, tp_vw_tipo_caja TCJ " + Environment.NewLine;
                sSql += "where CJ.id_localidad = LOC.id_localidad and CJ.cg_tipo_caja = TCJ.correlativo " + Environment.NewLine;
                sSql += "and TCJ.codigo = 'GENERAL' and LOC.id_localidad = " + Program.iIdLocalidad;
                cmbCaja.llenar(dtConsulta, sSql);
                cmbCaja.SelectedIndex = 1;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBO DE EMPLEADOS
        private void llenarComboEmpleado()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select id_persona, descripcion" + Environment.NewLine;
                sSql += "from pos_vw_cajero_mesero" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "order by descripcion";

                cmbEmpleados.llenar(dtConsulta, sSql);
                cmbEmpleados.SelectedIndex = 1;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA INSERTAR
        private void insertarRegistro()
        {
            try
            {
                sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    goto fin;
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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_MOVIMIENTO_CAJA
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "pos_movimiento_caja";
                sCampo = "id_pos_movimiento_caja"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
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
                    catchMensaje.LblMensaje.Text = sSql;
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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Movimiento ingresado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(1);
                limpiar();

                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    goto fin;
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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Movimiento actualizado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(1);
                limpiar();

                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }
        }

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    goto fin;
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
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Movimiento eliminado éxitosamente.";
                ok.ShowDialog();
                llenarGrid(1);
                limpiar();

                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }

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
                sSql += "pos_numero_movimiento_caja NM ON MC.id_pos_movimiento_caja = NM.id_pos_movimiento_caja and MC.estado = 'A' LEFT OUTER JOIN " + Environment.NewLine;
                sSql += "tp_personas TP ON (MC.id_cliente = TP.id_persona and TP.estado = 'A')" + Environment.NewLine;



                if (op == 1)
                {
                    sSql += "where MC.fecha = '" + LblFecha.Text + "'" + Environment.NewLine;

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
                        sSql += "where MC.tipo_movimiento = 1 and MC.id_documento_pago is null" + Environment.NewLine;
                    }

                    else if (iOp == 2)
                    {
                        sSql += "where MC.tipo_movimiento = 0 and MC.id_documento_pago is null" + Environment.NewLine;
                    }
                }

                sSql += "and (MC.estado = 'A' and NM.estado = 'A')" + Environment.NewLine;
                sSql += Environment.NewLine + "order by MC.id_pos_movimiento_caja" + Environment.NewLine;

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
                        //dgvDatos.Rows[0].Selected = true;
                        //dgvDatos.CurrentCell = dgvDatos.Rows[0].Cells[1];
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
                            cmbTipoMovimiento.Text = "Ingresos";
                        }

                        else
                        {
                            cmbTipoMovimiento.Text = "Salidas";
                        }

                        cmbEmpleados.SelectedValue = dtConsulta.Rows[0][6].ToString();
                        grupoNuevo.Enabled = true;
                        txtValor.Focus();
                        return true;
                    }

                    else
                    {
                        ok.LblMensaje.Text = "No existe el registro. Favor comuníquese con el admimnistrador para solucionar el problema.";
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

                cmbCargo.llenar(dtConsulta, sSql);

                if (cmbCargo.Items.Count > 0)
                {
                    cmbCargo.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmRegistrarMovimientos_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

            LblFecha.Text = sFecha;

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

            llenarComboCaja();
            llenarComboEmpleado();
            llenarGrid(1);
            LLenarComboCargos();
            limpiar();
        }

        private void TimerFecha_Tick(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((txtValor.Text == "") || (Convert.ToDouble(txtValor.Text) == 0))
            {
                ok.LblMensaje.Text = "Favor ingrese el monto a retirar o ingresar.";
                ok.ShowDialog();
            }

            else if (txtConcepto.Text == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el concepto del retiro o ingreso.";
                ok.ShowDialog();
            }

            else if (cmbEmpleados.SelectedValue.ToString() == "0")
            {
                ok.LblMensaje.Text = "Favor seleccione el empleado que hace uso del ingreso o egreso.";
                ok.ShowDialog();
            }

            else if (cmbCaja.SelectedValue.ToString() == "0")
            {
                ok.LblMensaje.Text = "Favor seleccione el tipo de caja a descontar.";
                ok.ShowDialog();
            }

            else if (cmbCargo.SelectedValue.ToString() == "0")
            {
                ok.LblMensaje.Text = "Favor seleccione el tipo de cargo.";
                ok.ShowDialog();
            }

            else
            {
                if (cmbTipoMovimiento.Text == "Ingresos")
                {
                    iTipoMovimiento = 1;
                }

                else
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No hay ítems para realizar ediciones.";
                ok.ShowDialog();
            }

            else
            {
                if (verificarDocumentoPago() == 1)
                {
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
                ok.LblMensaje.Text = "No hay ítems para eliminar.";
                ok.ShowDialog();
            }

            else
            {
                if (verificarDocumentoPago() == 1)
                {
                    ok.LblMensaje.Text = "No puede realizar ediciones de un ingreso que se ha ingresado con la comanda.";
                    ok.ShowDialog();
                }

                else
                {
                    if (consultarRegistro() == true)
                    {
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
                ok.LblMensaje.Text = "No puede realizar ediciones de un ingreso que se ha ingresado con la comanda.";
                ok.ShowDialog();
            }

            else
            {
                consultarRegistro();
            } 
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grupoNuevo.Enabled = true;
            btnBuscar.Enabled = false;
            txtFechaIngreso.Enabled = false;
            txtConcepto.Focus();
        }

        private void btnImpimir_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
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

        private void frmRegistrarMovimientos_KeyDown(object sender, KeyEventArgs e)
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrid(1);
        }


    }
}
