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
    public partial class frmSubCategoria : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        int iNivel = 3;
        int iIdProducto;
        int iIdProductoPadre;
        int iModificable;
        int iPrecioModificable;
        int iPagaIva;
        int iSubcategoria = 1;
        int iModificador;
        int iUltimo;
        int cg_tipoNombre = 5076;
        int nombInterno;
        int iAxuliarCombo;

        int iCambioCompra;
        int iCambioConsumo;

        string sEstado;
        string sCodigoSeparado;
        

        /*
         * VARIACION DE CODIGOS Y VARIABLES
         * AUTOR: ELVIS GUAIGUA
         * FECHA DE MODIFICACIPON: 2018-07-14
         * CONCEPTO: DEFINICIÓN DE VARIABLES PARA AJUSTARSE AL ESTANDAR ESTABLECIDO
        */

        string sSql;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmSubCategoria()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

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

                cmbInsumos.llenar(dtConsulta, sSql);
                cmbInsumos.SelectedValue = 2;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //llenar el comboBox Codigo Padre
        private void LLenarComboCategorias()
        {
            try
            {

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select P.codigo, NP.nombre" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre in (" + Environment.NewLine;
                sSql += "select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + Convert.ToInt32(cmbInsumos.SelectedValue) + "')" + Environment.NewLine;
                sSql += "and P.nivel = 2" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and subcategoria = 1";

                dtConsulta = new DataTable();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbPadre.llenar(dtConsulta, sSql);

                    if (cmbPadre.Items.Count > 0)
                    {
                        if (Convert.ToInt32(cmbPadre.SelectedValue) != 0)
                        {
                            extraerIdProductoPadre();
                        }

                        else
                        {
                            iIdProductoPadre = 0;
                        }
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR U OCULTAR LAS COLUMAS DEL DATAGRID
        private void columnasGrid(bool ok)
        {
            dgvDatos.Columns[0].Visible = ok;
            dgvDatos.Columns[5].Visible = ok;
            dgvDatos.Columns[6].Visible = ok;
            dgvDatos.Columns[7].Visible = ok;

            dgvDatos.Columns[1].Width = 75;
            dgvDatos.Columns[2].Width = 170;
            dgvDatos.Columns[3].Width = 75;
            dgvDatos.Columns[4].Width = 75;
        }

        //FUNCION LIMPIAR NUEVO
        private void limpiarNuevo()
        {
            iIdProducto = 0;
            LLenarComboCompra();
            LLenarComboConsumo();
            txtBuscar.Clear();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtSecuencia.Clear();
            txtSecuencia.Clear();

            chkModificable.Checked = false;
            chkPagaIva.Checked = false;
            chkPrecioModificable.Checked = false;
            cmbPadre.Enabled = true;

            btnAgregar.Text = "Nuevo";
            txtCodigo.Enabled = true;
            txtCodigo.Focus();
        }

        //FUNCION PARA LIMPIAR LAS CAJAS DE TEXTO PARA REGRESAR TODO POR DEFAULT
        private void limpiar()
        {
            iIdProducto = 0;
            LLenarComboCompra();
            LLenarComboConsumo();
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtBuscar.Clear();            
            txtSecuencia.Clear();

            llenarGrid(0);

            grupoDatos.Enabled = false;
            cmbPadre.Enabled = true;

            chkModificable.Checked = false;
            chkPagaIva.Checked = false;
            chkPrecioModificable.Checked = false;

            btnAgregar.Text = "Nuevo";
            txtCodigo.Enabled = true;
        }

        

        //EXTRAER EL ID DEL PRODUCTO PADRE LUEGO DE LLENAR EL COMBOBOX PADRE
        private void extraerIdProductoPadre()
        {
            try
            {
                sSql = "";
                sSql += "select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + cmbPadre.SelectedValue.ToString() + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdProductoPadre = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                    }
                    
                    else
                    {
                        iIdProductoPadre = 0;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
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
                sSql += "and estado='A'";

                cmbCompra.llenar(dtConsulta, sSql);

                if (cmbCompra.Items.Count > 0)
                {
                    cmbCompra.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
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
                sSql += "and estado='A'";

                cmbConsumo.llenar(dtConsulta, sSql);

                if (cmbConsumo.Items.Count > 0)
                {
                    cmbConsumo.SelectedIndex = 0;
                }
            }

            catch (Exception ex)
            {
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
                
                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA MOSTRAR LOS DATOS EN LOS COMOBOBOX, ESTOS NO SE DEBEN EDITAR
        private void seleccionarDatosCombobox()
        {
            try
            {
                sSql = "";
                sSql += "select UP.cg_unidad, TC.valor_texto, UP.unidad_compra" + Environment.NewLine;
                sSql += "from cv401_unidades_productos UP, tp_codigos TC" + Environment.NewLine;
                sSql += "where TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                sSql += "and UP.id_producto = " + iIdProducto + Environment.NewLine;
                sSql += "and UP.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iCambioCompra = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                        iCambioConsumo = Convert.ToInt32(dtConsulta.Rows[1][0].ToString());

                        cmbCompra.SelectedValue = iCambioCompra;
                        cmbConsumo.SelectedValue = iCambioConsumo;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL DATAGRID SEGUN LA CONSULTA 
        private void llenarGrid(int op)
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, P.codigo CODIGO, NP.nombre DESCRIPCION, P.secuencia SECUENCIA," + Environment.NewLine;
                sSql += "case P.estado when 'A' then 'ACTIVO' else 'INACTIVO' end ESTADO," + Environment.NewLine;
                sSql += "P.modificable, P.precio_modificable, P.paga_iva" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and id_producto_padre = " + iIdProductoPadre + Environment.NewLine;
                sSql += "and P.nivel = 3" + Environment.NewLine;
                sSql += "and P.estado ='A'";
                sSql += "and NP.estado ='A'" + Environment.NewLine;
                sSql += "and P.modificador = 0" + Environment.NewLine;
                sSql += "and P.subcategoria = 1" + Environment.NewLine;
                sSql += "and P.ultimo_nivel = 0" + Environment.NewLine;

                if (op == 1)
                {
                    sSql += "and NP.nombre LIKE '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                }

                sSql += "order by P.codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    columnasGrid(false);                    
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    limpiar();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                limpiar();
            }
        }

        //FUNCION PARA DAR DE BAJA UN REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }
                else
                {
                    sSql = "";
                    sSql += "update cv401_productos set" + Environment.NewLine;
                    sSql += "estado = 'E'," + Environment.NewLine;
                    sSql += "codigo = 'codigo." + iIdProducto.ToString() + "'," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql += "where id_producto = " + iIdProducto;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                    ok.ShowDialog();
                    limpiar();
                    btnAgregar.Text = "Nuevo";
                    llenarGrid(1);
                    return;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA INSERTAR UN NUEVO REGISTRO
        private void insertarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select * from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + (cmbPadre.SelectedValue.ToString() + "." + txtCodigo.Text.Trim()) + "'" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ok.lblMensaje.Text = "Ya existe un registro con el código ingresado.";
                        ok.ShowDialog();
                        txtCodigo.Clear();
                        txtCodigo.Focus();
                        return;
                    }
                }

                else
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al realizar la búsqueda del código.";
                    ok.ShowDialog();
                    return;
                }


                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //INSERTAR EN LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_productos (" + Environment.NewLine;
                sSql += "idempresa, codigo, id_producto_padre, estado, nivel, modificable, precio_modificable," + Environment.NewLine;
                sSql += "paga_iva, secuencia, modificador, subcategoria, ultimo_nivel, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Convert.ToInt32(cmbEmpresa.SelectedValue) + ", '" + (cmbPadre.SelectedValue.ToString() + "." + (txtCodigo.Text.Trim())) + "'," + Environment.NewLine;
                sSql += iIdProductoPadre + ", 'A', 3, " + iModificable + ", " + iPrecioModificable + "," + Environment.NewLine;
                sSql += iPagaIva + ", '" + txtSecuencia.Text.Trim() + "', " + iModificador + ", " + iSubcategoria + "," + Environment.NewLine;
                sSql += iUltimo + ", GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                string sTabla = "cv401_productos";
                string sCampo = "id_Producto"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de productos";
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdProducto = Convert.ToInt32(iMaximo);
                }

                //INSERTAR EN LA TABLA CV401_NOMBRES_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_nombre, nombre, nombre_interno, estado,numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdProducto + ", " + cg_tipoNombre + ", '" + txtDescripcion.Text.Trim() + "', " + nombInterno + "," + Environment.NewLine;
                sSql += "'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdProducto + ", " + Program.iCgTipoUnidad + ", " + Convert.ToInt32(cmbCompra.SelectedValue) + ", 1, 'A'," + Environment.NewLine; 
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE(), 1, 1," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += iIdProducto + "," + (Program.iCgTipoUnidad + 1) + ", " + Convert.ToInt32(cmbConsumo.SelectedValue) + ", 0, 'A'," + Environment.NewLine; 
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "', GETDATE(), 1, 1, GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado correctamente";
                ok.ShowDialog();
                limpiarNuevo();
                grupoDatos.Enabled = false;
                llenarGrid(0);
                return;
            }

            catch (Exception ex)
            {
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
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "secuencia = '" + txtSecuencia.Text.Trim() + "'," + Environment.NewLine;
                sSql += "paga_iva = " + iPagaIva + "," + Environment.NewLine;
                sSql += "modificable = " + iModificable + "," + Environment.NewLine;
                sSql += "precio_modificable = " + iPrecioModificable + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto;

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                sSql += "nombre = '" + txtDescripcion.Text.Trim() + "'" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto;

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //UNIDAD DE COMPRA
                if (Convert.ToInt32(cmbCompra.SelectedValue) != iCambioCompra)
                {
                    sSql = "";
                    sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                    sSql += "cg_unidad = " + Convert.ToInt32(cmbCompra.SelectedValue) + Environment.NewLine;
                    sSql += "where id_producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and unidad_compra = 1" + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //UNIDAD DE CONSUMO
                if (Convert.ToInt32(cmbConsumo.SelectedValue) != iCambioConsumo)
                {
                    sSql = "";
                    sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                    sSql += "cg_unidad = " + Convert.ToInt32(cmbConsumo.SelectedValue) + Environment.NewLine;
                    sSql += "where id_producto = " + iIdProducto + Environment.NewLine;
                    sSql += "and unidad_compra = 0" + Environment.NewLine;
                    sSql += "and estado = 'A'";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCION:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();
                limpiarNuevo();
                grupoDatos.Enabled = false;
                llenarGrid(0);
                return;

            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        #endregion

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.Text.Trim().Equals("ACTIVO"))
            {
                sEstado = "A";
            }
            else if (cmbEstado.Text.Trim().Equals("ELIMINADO"))
            {
                sEstado = "E";
            }
        }

        private void frmSubCategoria_Load(object sender, EventArgs e)
        {            
            cmbEstado.Text = "ACTIVO";
            LLenarComboEmpresa();
            LLenarComboPadre();
            LLenarComboCategorias();
            LLenarComboCompra();
            LLenarComboConsumo();            
            llenarGrid(0);
            iAxuliarCombo = 1;            
        }        

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Nuevo")
            {
                limpiarNuevo();
                grupoDatos.Enabled = true;
                btnAgregar.Text = "Guardar";
                BtnEliminar.Enabled = false;
                txtCodigo.Focus();
            }
            else
            {
                if (cmbPadre.SelectedValue.ToString() == "0")
                {
                    ok.lblMensaje.Text = "Favor seleccione el dato padre para crear el registro.";
                    ok.ShowDialog();
                    cmbCompra.Focus();
                }

                else if (txtCodigo.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el código de la sub categoría.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la descripción de la categoría.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (cmbCompra.SelectedValue.ToString() == "0")
                {
                    ok.lblMensaje.Text = "Favor seleccione la unidad de compra del producto.";
                    ok.ShowDialog();
                    cmbCompra.Focus();
                }

                else if (cmbConsumo.SelectedValue.ToString() == "0")
                {
                    ok.lblMensaje.Text = "Favor seleccione la unidad de consumo del producto.";
                    ok.ShowDialog();
                    cmbConsumo.Focus();
                }

                else if (txtSecuencia.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la secuencia del producto.";
                    ok.ShowDialog();
                    txtSecuencia.Focus();
                }

                else
                {
                    if (chkModificable.Checked == true)
                        iModificable = 1;
                    else
                        iModificable = 0;

                    if (chkPagaIva.Checked == true)
                        iPagaIva = 1;
                    else
                        iPagaIva = 0;

                    if (chkPrecioModificable.Checked == true)
                        iPrecioModificable = 1;
                    else
                        iPrecioModificable = 0;

                    if (btnAgregar.Text == "Guardar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Desea guardar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }

                    }

                    else
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Desea actualizar...?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                NuevoSiNo.lblMensaje.Text = "¿Está seguro de eliminar el registro seleccionado?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                btnAgregar.Text = "Nuevo";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
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

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAgregar.Text = "Actualizar";
                BtnEliminar.Enabled = true;
                grupoDatos.Enabled = true;
                cmbPadre.Enabled = false;
                txtCodigo.Enabled = false;

                iIdProducto = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                string valReco = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                List<String> lista = valReco.Split(Convert.ToChar(".")).ToList<String>();

                foreach (String item in lista)
                {
                    sCodigoSeparado = item;
                }

                if (sCodigoSeparado == "2")
                {
                    txtCodigo.Text = "";
                }

                else
                {
                    txtCodigo.Text = sCodigoSeparado;
                }

                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                txtSecuencia.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                cmbEstado.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();

                if (Convert.ToBoolean(dgvDatos.CurrentRow.Cells[5].Value) == true)
                    chkModificable.Checked = true;
                else
                    chkModificable.Checked = false;

                if (Convert.ToBoolean(dgvDatos.CurrentRow.Cells[6].Value) == true)
                    chkPrecioModificable.Checked = true;
                else
                    chkPrecioModificable.Checked = false;

                if (dgvDatos.CurrentRow.Cells[7].Value.ToString() == "1")
                    chkPagaIva.Checked = true;
                else
                    chkPagaIva.Checked = false;

                seleccionarDatosCombobox();
                txtDescripcion.Focus();
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            extraerIdProductoPadre();
            llenarGrid(0);            
        }

        private void cmbInsumos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LLenarComboCategorias();            
        }

    }
}
