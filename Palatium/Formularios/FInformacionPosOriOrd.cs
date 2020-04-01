using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FInformacionPosOriOrd : Form
    {
        //VARIABLES, INSTANCIAS
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        DataTable dtConsulta;
        bool bRespuesta;

        int iDelivery;
        int iRepartidor;
        int iGeneraFactura;
        int iIdOrigenOrden;
        int iIdManejaServicio;
        int iServicioConsulta;
        int iCuentaPorCobrar;
        int iPagoAnticipado;

        public FInformacionPosOriOrd()
        {
            InitializeComponent();
        }

        private void FInformacionPosOriOrd_Load(object sender, EventArgs e)
        {
            llenarSentencias();
            llenarComboDelivery();
            llenarComboPagos();
            llenarGrid(0);
            consultarServicio();
            cmbEstado.Text = "ACTIVO";
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR SI ESTÁ ACTIVA LA OPCION  DE SERVICIO
        private void consultarServicio()
        {
            try
            {
                sSql = "";
                sSql += "select isnull(maneja_servicio, 0)" + Environment.NewLine;
                sSql += "from pos_parametro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iServicioConsulta = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        
                        if (iServicioConsulta == 1)
                        {
                            chkManejaServicio.Visible = true;
                        }

                        else
                        {
                            chkManejaServicio.Visible = false;
                        }
                    }

                    else
                    {
                        iServicioConsulta = 0;
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

        //FUNCION PARA LLENAR EL DBAYUDA
        private void llenarDbAyuda(int iIdPersona)
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, ltrim(apellidos + ' ' + isnull(nombres, '')) as apellidos, identificacion" + Environment.NewLine;
                sSql += "from tp_personas" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_persona = " + iIdPersona;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dbAyudaPersona.iId = iIdPersona;
                        dbAyudaPersona.txtDatos.Text = dtConsulta.Rows[0][1].ToString();
                        dbAyudaPersona.txtIdentificacion.Text = dtConsulta.Rows[0][2].ToString();
                    }

                    else
                    {
                        dbAyudaPersona.iId = 0;
                        dbAyudaPersona.txtDatos.Clear();
                        dbAyudaPersona.txtIdentificacion.Clear();
                    }
                }

                else
                {
                    dbAyudaPersona.iId = 0;
                    dbAyudaPersona.txtDatos.Clear();
                    dbAyudaPersona.txtIdentificacion.Clear();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar LA SENTENDIA DEL DBAYUDA
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select id_persona, ltrim(apellidos + ' ' + isnull(nombres,'')) as apellidos, identificacion" + Environment.NewLine;
                sSql += "from tp_personas where estado = 'A'";

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dbAyudaPersona.Ver(sSql, "identificacion", 0, 2, 1);
                }
                else
                {
                    MessageBox.Show("Ocurrió un problema al cargar datos del dbAyuda", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        //LLENAR EL COMBO DELIVERY
        private void llenarComboDelivery()
        {
            try
            {
                dtConsulta = new DataTable();

                sSql = "";
                sSql += "select id_pos_modo_delivery, descripcion" + Environment.NewLine;
                sSql += "from pos_modo_delivery" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbModoDelivery.llenar(dtConsulta, sSql);

                if (cmbModoDelivery.Items.Count > 0)
                    cmbModoDelivery.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR EL COMBO DELIVERY
        private void llenarComboPagos()
        {
            try
            {
                dtConsulta = new DataTable();

                sSql = "";
                sSql += "select id_pos_tipo_forma_cobro, descripcion" + Environment.NewLine;
                sSql += "from pos_tipo_forma_cobro" + Environment.NewLine;
                sSql += "where estado = 'A'";

                cmbFormasCobros.llenar(dtConsulta, sSql);

                if (cmbFormasCobros.Items.Count > 0)
                    cmbFormasCobros.SelectedIndex = 1;

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtRuta.Clear();

            if (imgLogo.Image != null)
            {
                imgLogo.Image.Dispose();
                imgLogo.Image = null;
            }

            dbAyudaPersona.limpiar();
            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            llenarComboDelivery();
            llenarComboPagos();
            cmbEstado.Text = "ACTIVO";

            chkManejaServicio.Checked = false;
            chkDelivery.Checked = false;
            chkGeneraFactura.Checked = false;
            chkRepartidorExterno.Checked = false;
            chkPagoAnticipado.Checked = false;

            llenarGrid(0);
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select codigo as CODIGO, descripcion as DESCRIPCION," + Environment.NewLine;
                sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "id_pos_origen_orden, presenta_opcion_delivery, genera_factura," + Environment.NewLine;
                sSql += "repartidor_externo, isnull(imagen, '') imagen, id_pos_modo_delivery," + Environment.NewLine;
                sSql += "isnull(id_pos_tipo_forma_cobro, 0), isnull(id_persona, 0), maneja_servicio," + Environment.NewLine;
                sSql += "cuenta_por_cobrar, pago_anticipado" + Environment.NewLine;
                sSql += "from pos_origen_orden" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or descripcion like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by id_pos_origen_orden";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[0].Width = 60;
                    dgvDatos.Columns[1].Width = 200;
                    dgvDatos.Columns[2].Width = 100;
                    dgvDatos.Columns[3].Visible = false;
                    dgvDatos.Columns[4].Visible = false;
                    dgvDatos.Columns[5].Visible = false;
                    dgvDatos.Columns[6].Visible = false;
                    dgvDatos.Columns[7].Visible = false;
                    dgvDatos.Columns[8].Visible = false;
                    dgvDatos.Columns[9].Visible = false;
                    dgvDatos.Columns[10].Visible = false;
                    dgvDatos.Columns[11].Visible = false;
                    dgvDatos.Columns[12].Visible = false;
                    dgvDatos.Columns[13].Visible = false;
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

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }
                
                sSql = "";
                sSql += "insert into pos_origen_orden (" + Environment.NewLine;
                sSql += "codigo, descripcion, genera_factura, id_pos_modo_delivery," + Environment.NewLine;
                sSql += "presenta_opcion_delivery, repartidor_externo, imagen, id_pos_tipo_forma_cobro," + Environment.NewLine;
                sSql += "id_persona, maneja_servicio, cuenta_por_cobrar, pago_anticipado," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += iGeneraFactura + ", " + Convert.ToInt32(cmbModoDelivery.SelectedValue) + ", " + Environment.NewLine;
                sSql += iDelivery + ", " + iRepartidor + ", '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;

                if (iGeneraFactura == 1)
                {
                    sSql += "null, null," + Environment.NewLine;
                }

                else
                {
                    if (dbAyudaPersona.iId == 0)
                    {
                        sSql += Convert.ToInt32(cmbFormasCobros.SelectedValue) + ", null," + Environment.NewLine;
                    }

                    else
                    {
                        sSql += Convert.ToInt32(cmbFormasCobros.SelectedValue) + "," + dbAyudaPersona.iId + "," + Environment.NewLine;
                    }
                }

                sSql += iIdManejaServicio + ", " + iCuentaPorCobrar + ", " + iPagoAnticipado + ", 'A', GETDATE()," +  Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro ingresado éxitosamente";
                ok.ShowDialog();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
                return;
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
        }

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_origen_orden set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "genera_factura = " + iGeneraFactura + "," + Environment.NewLine;
                sSql += "id_pos_modo_delivery = " + Convert.ToInt32(cmbModoDelivery.SelectedValue) + "," + Environment.NewLine;
                sSql += "presenta_opcion_delivery = " + iDelivery + "," + Environment.NewLine;
                sSql += "repartidor_externo = " + iRepartidor + "," + Environment.NewLine;
                sSql += "imagen = '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "maneja_servicio = " + iIdManejaServicio + "," + Environment.NewLine;
                sSql += "cuenta_por_cobrar = " + iCuentaPorCobrar + "," + Environment.NewLine;
                sSql += "pago_anticipado = " + iPagoAnticipado + "," + Environment.NewLine;

                if (iGeneraFactura == 1)
                {
                    sSql += "id_pos_tipo_forma_cobro = null," + Environment.NewLine;
                    sSql += "id_persona = null" + Environment.NewLine;
                }

                else
                {
                    sSql += "id_pos_tipo_forma_cobro = " + Convert.ToInt32(cmbFormasCobros.SelectedValue) + "," + Environment.NewLine;

                    if (dbAyudaPersona.iId == 0)
                    {
                        sSql += "id_persona = null" + Environment.NewLine;
                    }

                    else
                    {
                        sSql += "id_persona = " + dbAyudaPersona.iId + Environment.NewLine;
                    }
                }

                sSql += "where id_pos_origen_orden = " +  iIdOrigenOrden;

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente";
                ok.ShowDialog();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
                return;
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
        }

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_origen_orden set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = " + iIdOrigenOrden;

                //EJECUTAR INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente";
                ok.ShowDialog();
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
                return;
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
        }

        #endregion

        private void Txt_Codigo_LostFocus(object sender, EventArgs e)
        {
            dbAyudaPersona.limpiar();

            sSql = "";
            sSql += "select id_pos_origen_orden, descripcion, id_pos_modo_delivery," + Environment.NewLine;
            sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
            sSql += "presenta_opcion_delivery, genera_factura," + Environment.NewLine;
            sSql += "repartidor_externo, isnull(imagen, '') imagen," + Environment.NewLine;
            sSql += "id_pos_tipo_forma_cobro, id_persona, cuenta_por_cobrar" + Environment.NewLine;
            sSql += "from pos_origen_orden" + Environment.NewLine;
            sSql += "where estado = 'A'" + Environment.NewLine;
            sSql += "and codigo = '" + txtCodigo.Text.Trim() + "'";

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    txtDescripcion.Text = dtConsulta.Rows[0][1].ToString();
                    cmbModoDelivery.SelectedValue = Convert.ToInt32(dtConsulta.Rows[0][2].ToString());
                    cmbEstado.Text = dtConsulta.Rows[0][3].ToString();

                    if (Convert.ToInt32(dtConsulta.Rows[0][4].ToString()) == 1)
                    {
                        chkDelivery.Checked = true;
                    }

                    else
                    {
                        chkDelivery.Checked = false;
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0][5].ToString()) == 1)
                    {
                        chkGeneraFactura.Checked = true;
                        llenarComboPagos();
                        grupoPago.Enabled = false;
                    }

                    else
                    {
                        chkGeneraFactura.Checked = false;
                        grupoPago.Enabled = true;
                        llenarDbAyuda(Convert.ToInt32(dtConsulta.Rows[0][9].ToString()));
                        cmbFormasCobros.SelectedValue = Convert.ToInt32(dtConsulta.Rows[0][8].ToString());
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0][6].ToString()) == 1)
                    {
                        chkRepartidorExterno.Checked = true;
                    }

                    else
                    {
                        chkRepartidorExterno.Checked = false;
                    }

                    if (dtConsulta.Rows[0][7].ToString() != "")
                    {
                        txtRuta.Text = dtConsulta.Rows[0][7].ToString();
                        imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                        imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    else
                    {
                        if (imgLogo.Image != null)
                        {
                            imgLogo.Image.Dispose();
                            imgLogo.Image = null;
                        }
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0]["cuenta_por_cobrar"].ToString()) == 1)
                    {
                        chkCuentaPorCobrar.Checked = true;
                    }

                    else
                    {
                        chkCuentaPorCobrar.Checked = false;
                    }

                    btnNuevo.Text = "Actualizar";
                    btnAnular.Enabled = true;
                    txtDescripcion.Focus();
                }

                else
                {
                    txtDescripcion.Clear();
                    llenarComboDelivery();
                    cmbEstado.Text = "ACTIVO";
                    chkGeneraFactura.Checked = false;
                    chkDelivery.Checked = false;
                    chkRepartidorExterno.Checked = false;
                    txtRuta.Clear();

                    if (imgLogo.Image != null)
                    {
                        imgLogo.Image.Dispose();
                        imgLogo.Image = null;
                    }

                    btnNuevo.Text = "Guardar";
                    btnAnular.Enabled = false;
                    txtDescripcion.Focus();
                }
            }

            else
            {
                catchMensaje.LblMensaje.Text = sSql;
                catchMensaje.ShowDialog();
            }
        }

        private void Txt_CodigoPosOriOrd_Leave(object sender, EventArgs e)
        {

        }

        private void Btn_CerrarPosOriOrd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarPosOriOrd_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void Btn_BuscarPosOriOrd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Error al general la consulta.", "Aviso", MessageBoxButtons.OK);
                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();

                llenarGrid(0);
            }
        }

        private void BtnNuevoPosOriOrd_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnAnular.Enabled = false;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
            }

            else
            {
                if (chkGeneraFactura.Checked == true)
                {
                    iGeneraFactura = 1;
                }

                else
                {
                    iGeneraFactura = 0;
                }

                if (chkDelivery.Checked == true)
                {
                    iDelivery = 1;
                }

                else
                {
                    iDelivery = 0;
                }

                if (chkRepartidorExterno.Checked == true)
                {
                    iRepartidor = 1;
                }

                else
                {
                    iRepartidor = 0;
                }

                if (chkManejaServicio.Checked == true)
                {
                    iIdManejaServicio = 1;
                }

                else
                {
                    iIdManejaServicio = 0;
                }

                if (chkCuentaPorCobrar.Checked == true)
                {
                    iCuentaPorCobrar  = 1;
                }

                else
                {
                    iCuentaPorCobrar = 0;
                }

                if (chkPagoAnticipado.Checked == true)
                {
                    iPagoAnticipado = 1;
                }

                else
                {
                    iPagoAnticipado = 0;
                }

                if (btnNuevo.Text == "Guardar")
                {
                    insertarRegistro();
                }

                else if (btnNuevo.Text == "Actualizar")
                {
                    actualizarRegistro();
                }
            }
        }

        private void Btn_AnularPosOriOrd_Click(object sender, EventArgs e)
        {
            if (comprobarRegistro() == false)
            {
                SiNo.LblMensaje.Text = "¿Esta seguro que desea dar de bajar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }                
            }            
        }

        //Comprobar Registro
        private bool comprobarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = "+dgvDatos.CurrentRow.Cells[3].Value.ToString();

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;


            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            //abrir.InitialDirectory = "c:\\";
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = abrir.FileName;
                imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRuta.Clear();
            imgLogo.Image.Dispose();
            imgLogo.Image = null;
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            grupoDatos.Enabled = true;
            btnNuevo.Text = "Actualizar";
            btnAnular.Enabled = true;
            txtCodigo.Enabled = false;
            dbAyudaPersona.limpiar();

            txtCodigo.Text = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            cmbEstado.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            iIdOrigenOrden = Convert.ToInt32(dgvDatos.CurrentRow.Cells[3].Value.ToString());

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[4].Value.ToString()) == 1)
            {
                chkDelivery.Checked = true;
            }

            else
            {
                chkDelivery.Checked = false;
            }

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[5].Value.ToString()) == 1)
            {
                chkGeneraFactura.Checked = true;
                llenarComboPagos();
                grupoPago.Enabled = false;
            }

            else
            {
                chkGeneraFactura.Checked = false;
                grupoPago.Enabled = true;
                llenarDbAyuda(Convert.ToInt32(dgvDatos.CurrentRow.Cells[10].Value.ToString()));
                cmbFormasCobros.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[9].Value.ToString());
            }

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value.ToString()) == 1)
            {
                chkRepartidorExterno.Checked = true;
                
            }

            else
            {
                chkRepartidorExterno.Checked = false;
            }

            if (dgvDatos.CurrentRow.Cells[7].Value.ToString() != "")
            {
                txtRuta.Text = dgvDatos.CurrentRow.Cells[7].Value.ToString();
                imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                imgLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            else
            {
                if (imgLogo.Image != null)
                {
                    imgLogo.Image.Dispose();
                    imgLogo.Image = null;
                }
            }

            cmbModoDelivery.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[8].Value.ToString());

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[11].Value.ToString()) == 1)
            {
                chkManejaServicio.Checked = true;
            }

            else
            {
                chkManejaServicio.Checked = false;
            }

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[12].Value.ToString()) == 1)
            {
                chkCuentaPorCobrar.Checked = true;
            }

            else
            {
                chkCuentaPorCobrar.Checked = false;
            }

            if (Convert.ToInt32(dgvDatos.CurrentRow.Cells[13].Value.ToString()) == 1)
            {
                chkPagoAnticipado.Checked = true;
            }

            else
            {
                chkPagoAnticipado.Checked = false;
            }

            txtDescripcion.Focus();
        }

        private void chkGerneraFactura_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeneraFactura.Checked == true)
            {
                dbAyudaPersona.limpiar();
                grupoPago.Enabled = false;
            }

            else
            {
                dbAyudaPersona.limpiar();
                grupoPago.Enabled = true;
            }
        }

        private void chkCuentaPorCobrar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCuentaPorCobrar.Checked == true)
            {
                chkPagoAnticipado.Checked = false;
            }
        }

        private void chkPagoAnticpado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagoAnticipado.Checked == true)
            {
                chkCuentaPorCobrar.Checked = false;
            }
        }

    }
}
