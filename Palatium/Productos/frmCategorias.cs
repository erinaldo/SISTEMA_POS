using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmCategorias : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        
        DataTable dt = new DataTable();
        string sValor;
        int cg_tipoNombre = 5076;
        
        int idPadre;
        int iUltimo;

        string sSql;
        DataTable dtConsulta;
        bool bRespuesta = false;
        int iModificable;
        int iPrecioModificable;
        int iPagaIva;
        int iTieneSubCategoria;
        int iModificador;
        int iNivel = 2;
        int iMenuPos;
        int iOtros;
        int iManejaAlmuerzos;
        int iDetallarPorOrigen;
        int iDetalleIndependiente;
        int iCategoriaDelivery;

        string sEstado;
        string sCodigoSeparado;

        int iIdUnidadCompra;
        int iIdUnidadConsumo;

        int iIdCategoria;

        public frmCategorias()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA MOSTRAR U OCULTAR COLUMNAS
        private void columnasGrid(bool ok)
        {
            dgvCategoria.Columns[0].Visible = ok;
            dgvCategoria.Columns[3].Visible = ok;
            dgvCategoria.Columns[4].Visible = ok;
            dgvCategoria.Columns[5].Visible = ok;
            dgvCategoria.Columns[7].Visible = ok;
            dgvCategoria.Columns[8].Visible = ok;
            dgvCategoria.Columns[9].Visible = ok;
            dgvCategoria.Columns[11].Visible = ok;
            dgvCategoria.Columns[12].Visible = ok;
            dgvCategoria.Columns[13].Visible = ok;
            dgvCategoria.Columns[14].Visible = ok;
            dgvCategoria.Columns[15].Visible = ok;

            dgvCategoria.Columns[1].Width = 75;
            dgvCategoria.Columns[2].Width = 170;
            dgvCategoria.Columns[6].Width = 75;
            dgvCategoria.Columns[10].Width = 75;

            dgvCategoria.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCategoria.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCategoria.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        //FUNCION LIMPIAR NUEVO
        private void limpiarNuevo()
        {
            iIdCategoria = 0;
            iModificable = 0;
            iPrecioModificable = 0;
            iPagaIva = 0;
            iTieneSubCategoria = 0;
            iModificador = 0;
            iMenuPos = 0;
            iOtros = 0;
            iManejaAlmuerzos = 0;
            iDetallarPorOrigen = 0;
            iDetalleIndependiente = 0;

            cmbCompra.SelectedIndex = 0;
            cmbConsumo.SelectedIndex = 0;
            cmbEstado.SelectedIndex = 0;
            sValor = "";

            txtBuscarCategoria.Clear();
            txtCodigoCategoria.Clear();
            txtDescripcion.Clear();
            txtSecuencia.Clear();

            chkModificable.Checked = false;
            chkPagaIva.Checked = false;
            chkPreModificable.Checked = false;
            chkTieneModifcador.Checked = false;
            chkTieneSubCategoria.Checked = false;
            chkMenuPos.Checked = false;
            chkAlmuerzos.Checked = false;
            chkDetallarOrigen.Checked = false;
            chkDetalleIndependiente.Checked = false;
            chkOtros.Checked = false;
            chkDelivery.Checked = false;
            btnEliminar.Enabled = false;
            cmbPadre.Enabled = true;

            cmbEstado.Text = "ACTIVO";

            btnAgregar.Text = "Nuevo";
        }

        //FUNCION PARA LIMPIAR LAS CAJAS DE TEXTO PARA REGRESAR TODO POR DEFAULT
        private void limpiar()
        {
            iIdCategoria = 0;
            iModificable = 0;
            iPrecioModificable = 0;
            iPagaIva = 0;
            iTieneSubCategoria = 0;
            iModificador = 0;
            iMenuPos = 0;
            iOtros = 0;
            iManejaAlmuerzos = 0;
            iDetallarPorOrigen = 0;
            iDetalleIndependiente = 0;

            cmbEstado.SelectedIndex = 0;
            LLenarComboPadre();
            LLenarComboCompra();
            LLenarComboConsumo();
            sValor = "";

            txtCodigoCategoria.Clear();
            txtDescripcion.Clear();
            txtBuscarCategoria.Clear();
            txtSecuencia.Clear();

            chkModificable.Checked = false;
            chkPagaIva.Checked = false;
            chkPreModificable.Checked = false;
            chkTieneModifcador.Checked = false;
            chkTieneSubCategoria.Checked = false;
            chkMenuPos.Checked = false;
            chkOtros.Checked = false;
            chkAlmuerzos.Checked = false;
            chkDetallarOrigen.Checked = false;
            chkDetalleIndependiente.Checked = false;
            chkDelivery.Checked = false;

            btnEliminar.Enabled = false;
            cmbPadre.Enabled = true;

            dtConsulta = new DataTable();
            dgvCategoria.DataSource = dtConsulta;
            cmbEstado.Text.Trim().Equals("ACTIVO");

            grupoDatos.Enabled = false;
            llenarGrid(0);

            btnAgregar.Text = "Nuevo";
        }

        //llenar el comboBox Codigo Padre
        private void LLenarComboPadre()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "Select PRD.codigo, '['+PRD.codigo+'] '+ NOM.nombre nombre" + Environment.NewLine;
                sSql += "from cv401_productos PRD, cv401_nombre_productos NOM" + Environment.NewLine;
                sSql += "where PRD.nivel = 1" + Environment.NewLine;
                sSql += "and PRD.ESTADO = 'A'" + Environment.NewLine;
                sSql += "and NOM.ESTADO = 'A'" + Environment.NewLine;
                sSql += "and PRD.id_producto = NOM.id_producto" + Environment.NewLine;
                sSql += "and NOM.nombre_interno = 1" + Environment.NewLine;
                sSql += "order by PRD.codigo ";

                cmbPadre.llenar(dtConsulta, sSql);
                cmbPadre.SelectedValue = 2;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //llenar el comboBox unidad compra 
        private void LLenarComboCompra()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00042'" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and correlativo in (2760, 2754, 540, 2794, 546)" + Environment.NewLine;
                sSql += "order by valor_texto";

                cmbCompra.llenar(dtConsulta, sSql);
                cmbCompra.SelectedValue = 546;
                
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //llenar el comboBox unidad consumo
        private void LLenarComboConsumo()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00042'" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "and correlativo in (2760, 2754, 540, 2794, 546)" + Environment.NewLine;
                sSql += "order by valor_texto";

                cmbConsumo.llenar(dtConsulta, sSql);
                cmbConsumo.SelectedValue = 546;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //llenar comboBox de Empresa
        private void LLenarComboEmpresa()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select idempresa, isnull(nombrecomercial, razonsocial) nombre_comercial, *" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                cmbEmpresa.llenar(dtConsulta, sSql);

                if(cmbEmpresa.Items.Count>=1)
                 cmbEmpresa.SelectedIndex = 1;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int op)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, P.codigo as CÓDIGO, NP.nombre as DESCRIPCIÓN," + Environment.NewLine;
                sSql += "P.modificable as Modificable, P.precio_modificable, P.paga_iva," + Environment.NewLine;
                sSql += "P.secuencia as SECUENCIA, P.modificador as Modificador, P.subcategoria, P.menu_pos," + Environment.NewLine;
                sSql += "case P.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "P.otros, P.maneja_almuerzos, P.detalle_por_origen, P.detalle_independiente," + Environment.NewLine;
                sSql += "isnull(P.categoria_delivery, 0) categoria_delivery" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and id_producto_padre in (" + Environment.NewLine;
                sSql += "select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '"+ cmbPadre.SelectedValue + "')" + Environment.NewLine;
                sSql += "and P.nivel = 2" + Environment.NewLine;
                sSql += "and P.estado in ('A', 'N')" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;

                if (op == 1)
                {
                    sSql += "and NP.nombre LIKE '%" + txtBuscarCategoria.Text.Trim() + "%'";
                }

                sSql += "order by P.id_producto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvCategoria.DataSource = dtConsulta;
                        columnasGrid(false);
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    limpiar();
                }
            }
            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }    

        //FUNCION PARA INSERTAR UN NUEVO REGISTRO
        private void insertarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + sValor + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ok = new VentanasMensajes.frmMensajeNuevoOk();
                        ok.lblMensaje.Text = "Ya existe un registro con el código ingresaado.";
                        ok.ShowDialog();
                        txtCodigoCategoria.Clear();
                        txtCodigoCategoria.Focus();
                        return;
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return;
                }


                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción para guardar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += " select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '"+ cmbPadre.SelectedValue + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        idPadre = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                    }
                }

                else
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv401_productos (" + Environment.NewLine;
                sSql += "idempresa, codigo, id_producto_padre, estado, nivel, modificable," + Environment.NewLine;
                sSql += "precio_modificable, paga_iva, secuencia, modificador, subcategoria," + Environment.NewLine;
                sSql += "ultimo_nivel, otros, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "menu_pos, maneja_almuerzos, detalle_por_origen, detalle_independiente," + Environment.NewLine;
                sSql += "uso_receta, categoria_delivery)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Convert.ToInt32(cmbEmpresa.SelectedValue) + ", '" + sValor + "', " + idPadre + ", 'A'," + Environment.NewLine;
                sSql += iNivel + ", " + iModificable + ", " + iPrecioModificable + ", " + iPagaIva + "," + Environment.NewLine;
                sSql += "'" + txtSecuencia.Text.Trim() + "', " + iModificador + ", " + iTieneSubCategoria + "," + Environment.NewLine;
                sSql += iUltimo + ", " + iOtros + ", GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "', " + iMenuPos + ", " + iManejaAlmuerzos + ", " + Environment.NewLine;
                sSql += iDetallarPorOrigen + ", " + iDetalleIndependiente + ", 0, " + iCategoriaDelivery + ")";
                    
                //sisque no me ejuta el query 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                string sTabla = "cv401_productos";
                string sCampo = "id_Producto"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de productos.";
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdCategoria = Convert.ToInt32(iMaximo);
                }

                sSql = "";
                sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_nombre, nombre, nombre_interno," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCategoria + ", 5076, '" + txtDescripcion.Text.Trim() + "', 1, 'A', 1, 1, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCategoria + ", " + Program.iCgTipoUnidad + ", " + Convert.ToInt32(cmbCompra.SelectedValue) + ", 1, 'A'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE(), 1, 1, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql += "estado, usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdCategoria + ", " + (Program.iCgTipoUnidad + 1) + ", " + Convert.ToInt32(cmbConsumo.SelectedValue) + ", 0, 'A'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE(), 1, 1, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }


                //si no se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado correctamente.";
                ok.ShowDialog();
                limpiarNuevo();
                grupoDatos.Enabled = false;
                llenarGrid(0);
                return;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {                
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción para actualizar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "codigo = '" + sValor + "'," + Environment.NewLine;
                sSql += "secuencia = '" + txtSecuencia.Text.Trim() + "'," + Environment.NewLine;
                sSql += "subcategoria = " + iTieneSubCategoria + "," + Environment.NewLine;
                sSql += "modificador = " + iModificador + "," + Environment.NewLine;
                sSql += "ultimo_nivel = " + iUltimo + "," + Environment.NewLine;
                sSql += "paga_iva = " + iPagaIva + "," + Environment.NewLine;
                sSql += "modificable = " + iModificable + "," + Environment.NewLine;
                sSql += "precio_modificable = " + iPrecioModificable + "," + Environment.NewLine;
                sSql += "otros = " + iOtros + "," + Environment.NewLine;
                sSql += "menu_pos = " + iMenuPos + "," + Environment.NewLine;
                sSql += "maneja_almuerzos = " + iManejaAlmuerzos + "," + Environment.NewLine;
                sSql += "detalle_por_origen = " + iDetallarPorOrigen + "," + Environment.NewLine;
                sSql += "detalle_independiente = " + iDetalleIndependiente + "," + Environment.NewLine;
                sSql += "categoria_delivery = " + iCategoriaDelivery + Environment.NewLine;
                sSql += "where id_producto = " + iIdCategoria;

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                sSql += "nombre = '" + txtDescripcion.Text.Trim() + "'" + Environment.NewLine;
                sSql += "where id_producto = " + iIdCategoria;

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //CAMBIO EN EL SISTEMA
                //-----------------------------------------------------------------------------------------------

                sSql = "";
                sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_Producto = " + iIdCategoria;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR LA UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado, usuario_creacion," + Environment.NewLine;
                sSql += "terminal_creacion, fecha_creacion, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdCategoria + ", " + Program.iUnidadCompraConsumo + ", '" + cmbCompra.SelectedValue.ToString() + "'," + Environment.NewLine;
                sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                sSql += "1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR LA UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado, usuario_creacion," + Environment.NewLine;
                sSql += "terminal_creacion, fecha_creacion, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += iIdCategoria + ", " + (Program.iUnidadCompraConsumo - 1) + ", '" + cmbConsumo.SelectedValue.ToString() + "'," + Environment.NewLine;
                sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                sSql += "1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //-----------------------------------------------------------------------------------------------

                //sSql = "";
                //sSql += "select cg_tipo_unidad, cg_unidad, unidad_compra" + Environment.NewLine;
                //sSql += "from cv401_unidades_productos" + Environment.NewLine;
                //sSql += "where id_producto = " + iIdCategoria + Environment.NewLine;
                //sSql += "and estado = 'A'";

                //dtConsulta = new DataTable();
                //dtConsulta.Clear();

                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                //if (bRespuesta == false)
                //{
                //    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //    catchMensaje.ShowDialog();
                //    goto reversa;
                //}

                //int iBandera = 0;

                ////SI HUBO CAMBIO EM UNIDAD DE COMPRA
                //if ((iIdUnidadCompra != Convert.ToInt32(cmbCompra.SelectedValue)) || (iIdUnidadCompra == 0))
                //{
                //    if (dtConsulta.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                //        {
                //            if (Convert.ToBoolean(dtConsulta.Rows[i].ItemArray[2].ToString()) == false)
                //            {
                //                iBandera = 1;
                //                break;
                //            }
                //        }

                //        if (iBandera == 1)
                //        {
                //            sSql = "";
                //            sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                //            sSql += "estado = 'E'," + Environment.NewLine;
                //            sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                //            sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //            sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                //            sSql += "where id_Producto = " + iIdCategoria + Environment.NewLine;
                //            sSql += "and cg_tipo_unidad = " + Program.iUnidadCompraConsumo;
                            
                //            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                //            {
                //                catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //                catchMensaje.ShowDialog();
                //                goto reversa;
                //            }

                //        }                            
                //    }

                //    //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //    //INSERTAR LA UNIDAD DE COMPRA
                //    sSql = "";
                //    sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                //    sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado, usuario_creacion," + Environment.NewLine;
                //    sSql += "terminal_creacion, fecha_creacion, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                //    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                //    sSql += "values(" + Environment.NewLine;
                //    sSql += iIdCategoria + ", " + Program.iUnidadCompraConsumo + ", '" + cmbCompra.SelectedValue.ToString() + "'," + Environment.NewLine;
                //    sSql += "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                //    sSql += "1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                //    {
                //        catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //        catchMensaje.ShowDialog();
                //        goto reversa;
                //    }
                //}

                //iBandera = 0;
                ////SI HUBO CAMBIO EN UNIDAD DE CONSUMO
                //if ((iIdUnidadConsumo != Convert.ToInt32(cmbConsumo.SelectedValue)) || (iIdUnidadConsumo == 0))
                //{
                //    if (dtConsulta.Rows.Count > 0)
                //    {
                //        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                //        {
                //            if (Convert.ToBoolean(dtConsulta.Rows[i].ItemArray[2].ToString()) == true)
                //            {
                //                iBandera = 1;
                //                break;
                //            }
                //        }

                //        if (iBandera == 0)
                //        {
                //            sSql = "";
                //            sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                //            sSql += "estado = 'E'," + Environment.NewLine;
                //            sSql += "fecha_anulacion = GETDATE()," + Environment.NewLine;
                //            sSql += "usuario_anulacion = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //            sSql += "terminal_anulacion = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                //            sSql += "where id_Producto = " + iIdCategoria + Environment.NewLine;
                //            sSql += "and cg_tipo_unidad = " + (Program.iUnidadCompraConsumo - 1);

                //            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                //            {
                //                catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //                catchMensaje.ShowDialog();
                //                goto reversa;
                //            }

                //        }
                //    }
                    
                //    //INSERTAR LA UNIDAD DE CONSUMO
                //    sSql = "";
                //    sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                //    sSql += "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado, usuario_creacion," + Environment.NewLine;
                //    sSql += "terminal_creacion, fecha_creacion, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                //    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                //    sSql += "values(" + Environment.NewLine;
                //    sSql += iIdCategoria + ", " + (Program.iUnidadCompraConsumo - 1) + ", '" + cmbConsumo.SelectedValue.ToString() + "'," + Environment.NewLine;
                //    sSql += "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE()," + Environment.NewLine;
                //    sSql += "1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                //    {
                //        catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //        catchMensaje.ShowDialog();
                //        goto reversa;
                //    }
                //}

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();
                limpiarNuevo();
                grupoDatos.Enabled = false;
                llenarGrid(0);
                return;

            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return;}        
        }

        //FUNCION PARA DAR DE BAJA UN REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ELIMINAR
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción para eliminar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                else
                {
                    sSql = "";
                    sSql += "update cv401_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "codigo = codigo + '.' + '" + iIdCategoria.ToString() + "'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_producto = " + iIdCategoria;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }


                    //si se ejecuta bien hara un commit
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                    ok.ShowDialog();
                    limpiarNuevo();
                    btnAgregar.Text = "Nuevo";
                    llenarGrid(1);
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA VALIDAR SI HAY ITEMS EN LA CATEGORIA EN ESTADO A

        private int contarRegistrosVigentes()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where id_producto_padre = " + iIdCategoria + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                
                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        #endregion

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            LLenarComboEmpresa();
            cmbPadre.SelectedIndexChanged -= new EventHandler(cmbPadre_SelectedIndexChanged);
            LLenarComboPadre();
            cmbPadre.SelectedIndexChanged += new EventHandler(cmbPadre_SelectedIndexChanged);
            cmbEstado.Text = "ACTIVO";
            LLenarComboCompra();
            LLenarComboConsumo();            
            llenarGrid(0);
        }

        private void btnLimpiarCategori_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnNuevoCategori_Click(object sender, EventArgs e)
        {
            if (cmbPadre.SelectedValue == "0")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione al grupo de productos para guardar el registro.";
                ok.ShowDialog();
                cmbPadre.Focus();
                return;
            }

            if (btnAgregar.Text == "Nuevo")
            {
                limpiarNuevo();
                cmbPadre.Enabled = false;
                grupoDatos.Enabled = true;
                btnAgregar.Text = "Guardar";

                if (cmbPadre.SelectedValue.ToString() == "1")
                {
                    chkMenuPos.Enabled = false;
                    chkTieneModifcador.Enabled = false;
                    chkOtros.Enabled = false;
                    chkAlmuerzos.Enabled = false;
                    chkDetallarOrigen.Enabled = false;
                    chkDetalleIndependiente.Enabled = false;
                    chkDelivery.Enabled = false;
                }

                else
                {
                    chkMenuPos.Enabled = true;
                    chkTieneModifcador.Enabled = true;
                    chkOtros.Enabled = true;
                    chkAlmuerzos.Enabled = true;
                    chkDetallarOrigen.Enabled = true;
                    chkDetalleIndependiente.Enabled = true;
                    chkDelivery.Enabled = true;
                }

                txtCodigoCategoria.Focus();
            }

            else
            {
                if (txtCodigoCategoria.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese el código de la categoría.";
                    ok.ShowDialog();
                    txtCodigoCategoria.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la descripción de la categoría.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (txtSecuencia.Text == "")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor ingrese la secuencia del producto.";
                    ok.ShowDialog();
                    txtSecuencia.Focus();
                }

                else if (cmbCompra.SelectedValue.ToString() == "0")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione la unidad de compra del producto.";
                    ok.ShowDialog();
                    cmbCompra.Focus();
                }

                else if (cmbConsumo.SelectedValue.ToString() == "0")
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione la unidad de consumo del producto.";
                    ok.ShowDialog();
                    cmbConsumo.Focus();
                }

                else
                {
                    if (chkModificable.Checked == true)
                        iModificable = 1;
                    else
                        iModificable = 0;

                    if (chkPreModificable.Checked == true)
                        iPrecioModificable = 1;
                    else
                        iPrecioModificable = 0;

                    if (chkPagaIva.Checked == true)
                        iPagaIva = 1;
                    else
                        iPagaIva = 0;

                    if (chkTieneSubCategoria.Checked == true)
                        iTieneSubCategoria = 1;
                    else
                        iTieneSubCategoria = 0;

                    if (chkTieneModifcador.Checked == true)
                        iModificador = 1;
                    else
                        iModificador = 0;

                    if (chkMenuPos.Checked == true)
                        iMenuPos = 1;
                    else
                        iMenuPos = 0;

                    if (chkOtros.Checked == true)
                        iOtros = 1;
                    else
                        iOtros = 0;

                    if (chkAlmuerzos.Checked == true)
                        iManejaAlmuerzos = 1;
                    else
                        iManejaAlmuerzos = 0;

                    if (chkDetallarOrigen.Checked == true)
                        iDetallarPorOrigen = 1;
                    else
                        iDetallarPorOrigen = 0;

                    if (chkDetalleIndependiente.Checked == true)
                        iDetalleIndependiente = 1;
                    else
                        iDetalleIndependiente = 0;

                    if (chkDelivery.Checked == true)
                        iCategoriaDelivery = 1;
                    else
                        iCategoriaDelivery = 0;

                    if (cmbEstado.Text == "ACTIVO")
                    {
                        sEstado = "A";
                    }

                    else
                    {
                        sEstado = "N";
                    }

                    sValor = cmbPadre.SelectedValue.ToString() + "." + txtCodigoCategoria.Text;

                    if (btnAgregar.Text == "Guardar")
                    {
                        //ENVIAR A FUNCION AGREGAR
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Está seguro de guardar el registro?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }

                    else
                    {
                        //ENVIAR A FUNCION ACTUALIZAR            
                        NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                        NuevoSiNo.lblMensaje.Text = "¿Está seguro de actualizar el registro?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }
        }

        private void btnAnularCategori_Click(object sender, EventArgs e)
        {
            try
            {
                int iContar = contarRegistrosVigentes();

                if (iContar == -1)
                {
                    return;
                }

                if (iContar > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "La categoría no puede ser eliminada, ya que contiene registros que dependen de la misma.";
                    ok.ShowDialog();
                    return;
                }

                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                NuevoSiNo.lblMensaje.Text = "¿Está seguro de eliminar el registro seleccionado?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                btnAgregar.Text = "Nuevo";
            }
        }

        private void dgvCategoria_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregar.Text = "Actualizar";
            grupoDatos.Enabled = true;
            cmbPadre.Enabled = true;
            btnEliminar.Enabled = true;
            cmbPadre.Enabled = false;

            iIdCategoria = Convert.ToInt32(dgvCategoria.CurrentRow.Cells[0].Value.ToString());
            
            List<String> lista = dgvCategoria.CurrentRow.Cells[1].Value.ToString().Split(Convert.ToChar(".")).ToList<String>();

            foreach (String item in lista)
            {
                sCodigoSeparado = item;
            }

            if (sCodigoSeparado == "2")
                txtCodigoCategoria.Text = "";
            else 
                txtCodigoCategoria.Text = sCodigoSeparado;

            txtDescripcion.Text = dgvCategoria.CurrentRow.Cells[2].Value.ToString();

            string chbModifi = dgvCategoria.CurrentRow.Cells[3].Value.ToString();

            if (chbModifi == "True")
                chkModificable.Checked = true;
            else chkModificable.Checked = false;

            string chbPreModifi = dgvCategoria.CurrentRow.Cells[4].Value.ToString();
            if (chbPreModifi == "True")
                chkPreModificable.Checked = true;
            else chkPreModificable.Checked = false;

            string chbPagIva = dgvCategoria.CurrentRow.Cells[5].Value.ToString();
            if (chbPagIva == "1")
                chkPagaIva.Checked = true;
            else chkPagaIva.Checked = false;
            txtSecuencia.Text = dgvCategoria.CurrentRow.Cells[6].Value.ToString();

            string chkTiene = dgvCategoria.CurrentRow.Cells[8].Value.ToString();
            if (chkTiene == "1")
                chkTieneSubCategoria.Checked = true;
            else chkTieneSubCategoria.Checked = false;

            string chkModificador = dgvCategoria.CurrentRow.Cells[7].Value.ToString();
            if (chkModificador == "1")
                chkTieneModifcador.Checked = true;
            else chkTieneModifcador.Checked = false;

            string menuPos = dgvCategoria.CurrentRow.Cells[9].Value.ToString();
            if (menuPos == "1")
                chkMenuPos.Checked = true;
            else chkMenuPos.Checked = false;

            cmbEstado.Text = dgvCategoria.CurrentRow.Cells[10].Value.ToString();

            if (dgvCategoria.CurrentRow.Cells[11].Value.ToString() == "1")
            {
                chkOtros.Checked = true;
            }

            else
            {
                chkOtros.Checked = false;
            }

            if (dgvCategoria.CurrentRow.Cells[12].Value.ToString() == "1")
            {
                chkAlmuerzos.Checked = true;
            }

            else
            {
                chkAlmuerzos.Checked = false;
            }

            if (dgvCategoria.CurrentRow.Cells[13].Value.ToString() == "1")
            {
                chkDetallarOrigen.Checked = true;
            }

            else
            {
                chkDetallarOrigen.Checked = false;
            }

            if (dgvCategoria.CurrentRow.Cells[14].Value.ToString() == "1")
            {
                chkDetalleIndependiente.Checked = true;
            }

            else
            {
                chkDetalleIndependiente.Checked = false;
            }

            if (dgvCategoria.CurrentRow.Cells[15].Value.ToString() == "1")
                chkDelivery.Checked = true;
            else
                chkDelivery.Checked = false;

            if (cmbPadre.SelectedValue.ToString() == "1")
            {
                chkMenuPos.Enabled = false;
                chkTieneModifcador.Enabled = false;
                chkOtros.Enabled = false;
                chkAlmuerzos.Enabled = false;
                chkDetallarOrigen.Enabled = false;
                chkDetalleIndependiente.Enabled = false;
                chkDelivery.Enabled = false;
            }

            else
            {
                chkMenuPos.Enabled = true;
                chkTieneModifcador.Enabled = true;
                chkOtros.Enabled = true;
                chkAlmuerzos.Enabled = true;
                chkDetallarOrigen.Enabled = true;
                chkDetalleIndependiente.Enabled = true;
                chkDelivery.Enabled = true;
            }

            sSql = "";
            sSql += "select cg_tipo_unidad, cg_unidad" + Environment.NewLine;
            sSql += "from cv401_unidades_productos" + Environment.NewLine;
            sSql += "where id_producto = " + iIdCategoria + Environment.NewLine;
            sSql += "and estado = 'A'";

            dtConsulta = new DataTable();
            dtConsulta.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    iIdUnidadCompra = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[1].ToString());
                    iIdUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[1].ItemArray[1].ToString());
                    cmbCompra.SelectedValue = dtConsulta.Rows[0].ItemArray[1].ToString();
                    cmbConsumo.SelectedValue = dtConsulta.Rows[1].ItemArray[1].ToString();
                }

                else
                {
                    iIdUnidadCompra = 0;
                    iIdUnidadConsumo = 0;
                    cmbCompra.SelectedValue = "0";
                    cmbConsumo.SelectedValue = "0";
                }
            }

            else
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                catchMensaje.ShowDialog();
            }

        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            if (txtBuscarCategoria.Text == "")
            {
                llenarGrid(0);
            }
            else
            {
                llenarGrid(1);
            }
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void cmbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

        private void txtCodigoCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void chkDetalleIndependiente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDetallarOrigen.Checked == true)
            {
                chkDetallarOrigen.CheckedChanged -= new EventHandler(chkDetallarOrigen_CheckedChanged);
                chkDetallarOrigen.Checked = false;
                chkDetallarOrigen.CheckedChanged += new EventHandler(chkDetallarOrigen_CheckedChanged);
            }
        }

        private void chkDetallarOrigen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDetalleIndependiente.Checked == true)
            {
                chkDetalleIndependiente.CheckedChanged -= new EventHandler(chkDetalleIndependiente_CheckedChanged);
                chkDetalleIndependiente.Checked = false;
                chkDetalleIndependiente.CheckedChanged += new EventHandler(chkDetalleIndependiente_CheckedChanged);
            }
        }

    }
}
