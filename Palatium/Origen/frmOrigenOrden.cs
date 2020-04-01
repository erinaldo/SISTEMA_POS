using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Origen
{
    public partial class frmOrigenOrden : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();

        DataTable dtConsulta;

        bool bRespuesta;

        string sSql;

        int iServicioConsulta;
        int iDelivery;
        int iRepartidor;
        int iGeneraFactura;
        int iIdOrigenOrden;
        int iIdManejaServicio;

        double dPorcentajeDescuento;

        public frmOrigenOrden()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //COMPROBAR REGISTRO
        private bool comprobarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_origen_orden = " + iIdOrigenOrden;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        return true;
                    }

                    else
                    {
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

        //FUNCION PARA LLENAR LA SENTENDIA DEL DBAYUDA
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
                    ok.LblMensaje.Text = "Ocurrió un problema al cargar datos del dbAyuda";
                    ok.ShowDialog();
                }
            }
            catch (Exception ex)
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
                        dbAyudaPersona.txtDatos.Text = dtConsulta.Rows[0].ItemArray[1].ToString();
                        dbAyudaPersona.txtIdentificacion.Text = dtConsulta.Rows[0].ItemArray[2].ToString();
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

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
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
                        iServicioConsulta = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());

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

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
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
            txtEstado.Text = "ACTIVO";

            iIdOrigenOrden = 0;

            chkManejaServicio.Checked = false;
            chkDelivery.Checked = false;
            chkGeneraFactura.Checked = false;
            chkRepartidorExterno.Checked = false;

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
                sSql += "isnull(porcentaje_descuento_externo, '0') porcentaje_descuento_externo" + Environment.NewLine;
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

        #endregion


        #region FUNCIONES DE PROCESO A BASE DE DATOS

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }
                //=======================================================================================================

                sSql = "";
                sSql += "insert into pos_origen_orden (" + Environment.NewLine;
                sSql += "codigo, descripcion, genera_factura, id_pos_modo_delivery," + Environment.NewLine;
                sSql += "presenta_opcion_delivery, repartidor_externo, imagen," + Environment.NewLine;
                sSql += "id_pos_tipo_forma_cobro, id_persona, maneja_servicio," + Environment.NewLine;
                sSql += "porcentaje_descuento_externo, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
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

                sSql += iIdManejaServicio + "," + dPorcentajeDescuento + ", 'A', GETDATE()," + Environment.NewLine;
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

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }
                //=======================================================================================================

                sSql = "";
                sSql += "update pos_origen_orden set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "genera_factura = " + iGeneraFactura + "," + Environment.NewLine;
                sSql += "id_pos_modo_delivery = " + Convert.ToInt32(cmbModoDelivery.SelectedValue) + "," + Environment.NewLine;
                sSql += "presenta_opcion_delivery = " + iDelivery + "," + Environment.NewLine;
                sSql += "repartidor_externo = " + iRepartidor + "," + Environment.NewLine;
                sSql += "imagen = '" + txtRuta.Text.Trim() + "'," + Environment.NewLine;
                sSql += "maneja_servicio = " + iIdManejaServicio + "," + Environment.NewLine;
                sSql += "porcentaje_descuento_externo = " + dPorcentajeDescuento + "," + Environment.NewLine;

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

                sSql += "where id_pos_origen_orden = " + iIdOrigenOrden;

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

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                //=======================================================================================================
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }
                //=======================================================================================================

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

        #endregion

        private void frmOrigenOrden_Load(object sender, EventArgs e)
        {
            llenarSentencias();
            llenarComboDelivery();
            llenarComboPagos();
            llenarGrid(0);
            consultarServicio();    
        }

        private void txt_Codigo_LostFocus(object sender, EventArgs e)
        {
            dbAyudaPersona.limpiar();

            sSql = "";
            sSql += "select id_pos_origen_orden, descripcion, id_pos_modo_delivery," + Environment.NewLine;
            sSql += "case estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
            sSql += "presenta_opcion_delivery, genera_factura," + Environment.NewLine;
            sSql += "repartidor_externo, isnull(imagen, '') imagen," + Environment.NewLine;
            sSql += "id_pos_tipo_forma_cobro, id_persona" + Environment.NewLine;
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
                    iIdOrigenOrden = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    txtDescripcion.Text = dtConsulta.Rows[0].ItemArray[1].ToString();
                    cmbModoDelivery.SelectedValue = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[2].ToString());
                    txtEstado.Text = dtConsulta.Rows[0].ItemArray[3].ToString();

                    if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[4].ToString()) == 1)
                    {
                        chkDelivery.Checked = true;
                    }

                    else
                    {
                        chkDelivery.Checked = false;
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[5].ToString()) == 1)
                    {
                        chkGeneraFactura.Checked = true;
                        llenarComboPagos();
                        grupoPago.Enabled = false;
                    }

                    else
                    {
                        chkGeneraFactura.Checked = false;
                        grupoPago.Enabled = true;
                        llenarDbAyuda(Convert.ToInt32(dtConsulta.Rows[0].ItemArray[9].ToString()));
                        cmbFormasCobros.SelectedValue = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[8].ToString());
                    }

                    if (Convert.ToInt32(dtConsulta.Rows[0].ItemArray[6].ToString()) == 1)
                    {
                        chkRepartidorExterno.Checked = true;
                    }

                    else
                    {
                        chkRepartidorExterno.Checked = false;
                    }

                    if (dtConsulta.Rows[0].ItemArray[7].ToString() != "")
                    {
                        txtRuta.Text = dtConsulta.Rows[0].ItemArray[7].ToString();
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

                    btnNuevo.Text = "Actualizar";
                    btnEliminar.Enabled = true;
                    txtDescripcion.Focus();
                }

                else
                {
                    txtDescripcion.Clear();
                    llenarComboDelivery();
                    txtEstado.Text = "ACTIVO";
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
                    btnEliminar.Enabled = false;
                    txtDescripcion.Focus();
                }
            }

            else
            {
                catchMensaje.LblMensaje.Text = sSql;
                catchMensaje.ShowDialog();
            }
        }

        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            txtCodigo.LostFocus += new EventHandler(txt_Codigo_LostFocus);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            grupoDatos.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdOrigenOrden == 0)
            {
                ok.LblMensaje.Text = "No ha cargado información para procesar.";
                ok.ShowDialog();
            }

            else if (comprobarRegistro() == false)
            {
                SiNo.LblMensaje.Text = "¿Esta seguro que desea dar de bajar el registro " + txtDescripcion.Text.ToUpper().Trim() + "?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }     
 
            else
            {
                ok.LblMensaje.Text = "No se puede eliminar el registro " + txtDescripcion.Text.ToUpper().Trim() + ", ya que contiene dependencias del mismo.";
                ok.ShowDialog();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                grupoDatos.Enabled = true;
                btnEliminar.Enabled = false;
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

                if (txtPorcentajeDescuento.Text.Trim() == "")
                {
                    txtPorcentajeDescuento.Text = "0";
                    dPorcentajeDescuento = 0;
                }

                else
                {
                    dPorcentajeDescuento = Convert.ToDouble(txtPorcentajeDescuento.Text.Trim());
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

        private void btnBuscar_Click(object sender, EventArgs e)
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

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();

                grupoDatos.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
                llenarGrid(0);
            }
        }

        private void chkGeneraFactura_CheckedChanged(object sender, EventArgs e)
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

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            grupoDatos.Enabled = true;
            btnNuevo.Text = "Actualizar";
            btnEliminar.Enabled = true;
            txtCodigo.Enabled = false;
            dbAyudaPersona.limpiar();

            txtCodigo.Text = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtEstado.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
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
                grupoServicio.Enabled = true;
            }

            else
            {
                chkRepartidorExterno.Checked = false;
                grupoServicio.Enabled = false;
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

            txtPorcentajeDescuento.Text = dgvDatos.CurrentRow.Cells[12].Value.ToString();

            txtDescripcion.Focus();
        }

        private void chkRepartidorExterno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRepartidorExterno.Checked == true)
            {
                grupoServicio.Enabled = true;
            }

            else
            {
                txtPorcentajeDescuento.Text = "0";
                grupoServicio.Enabled = false;
            }
        }
    }
}
