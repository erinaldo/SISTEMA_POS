using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmSubCategoria : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracter;

        VentanasMensajes.frmMensajeNuevoOk ok;
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje;
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo;

        SqlParameter[] parametro;

        int iHabilitado;
        int iIdProducto;
        int iUnidadCompra = 6142;
        int iUnidadConsumo = 6143;
        int iModificable;
        int iPrecioModificable;
        int iPagaIva;
        int iSubcategoria = 1;
        int iUltimo;
        int cg_tipoNombre = 5076;
        int nombInterno;
        int iCambioCompra;
        int iCambioConsumo;

        string sCodigoSeparado;
        string sNombreOriginal;
        string sSql;
        string sCodigoCategoria;
        string sTabla;
        string sCampo;

        DataTable dtConsulta;
        DataTable dtCategorias;

        bool bRespuesta;

        public frmSubCategoria()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //llenar comboBox de Empresa
        private void LLenarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select idempresa, isnull(nombrecomercial, razonsocial) nombre_comercial" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = @idempresa";

                parametro = new SqlParameter[1];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@idempresa";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    DataRow row = dtConsulta.NewRow();
                    row["idempresa"] = "0";
                    row["nombre_comercial"] = "Seleccione empresa...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);
                }

                cmbEmpresa.DisplayMember = "nombre_comercial";
                cmbEmpresa.ValueMember = "idempresa";
                cmbEmpresa.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //llenar el comboBox Codigo Padre
        private void LLenarComboPadre()
        {
            try
            {
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sSql = "";
                sSql += "select P.id_producto, '['+P.codigo+'] '+ NP.nombre nombre" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.ESTADO = @estado_1" + Environment.NewLine;
                sSql += "and NP.ESTADO = @estado_2" + Environment.NewLine;
                sSql += "where P.nivel = @nivel" + Environment.NewLine;
                sSql += "and NP.nombre_interno = @nombre_interno" + Environment.NewLine;
                sSql += "and P.codigo = @codigo" + Environment.NewLine;
                sSql += "order by NP.nombre";

                parametro = new SqlParameter[5];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@nivel";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = 1;

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@nombre_interno";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = 1;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@codigo";
                parametro[4].SqlDbType = SqlDbType.Int;
                parametro[4].Value = 2;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                if (dtConsulta.Rows.Count == 0)
                {
                    DataRow row = dtConsulta.NewRow();
                    row["id_producto"] = "0";
                    row["nombre"] = "Seleccione...!!!";
                    dtConsulta.Rows.InsertAt(row, 0);
                }

                cmbCodigoOrigen.DisplayMember = "nombre";
                cmbCodigoOrigen.ValueMember = "id_producto";
                cmbCodigoOrigen.DataSource = dtConsulta;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //llenar el comboBox Codigo Padre
        private void LLenarComboCategorias()
        {
            try
            {

                sSql = "";
                sSql += "select P.id_producto, NP.nombre, P.codigo" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.ESTADO = @estado_1" + Environment.NewLine;
                sSql += "and NP.ESTADO = @estado_2" + Environment.NewLine;
                sSql += "where P.id_producto_padre = @id_producto_padre" + Environment.NewLine;
                sSql += "and P.nivel = @nivel" + Environment.NewLine;
                sSql += "and P.subcategoria = @subcategoria" + Environment.NewLine;
                sSql += "order by NP.nombre";

                parametro = new SqlParameter[5];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado_1";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado_2";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@id_producto_padre";
                parametro[2].SqlDbType = SqlDbType.Int;
                parametro[2].Value = Convert.ToInt32(cmbCodigoOrigen.SelectedValue);

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@nivel";
                parametro[3].SqlDbType = SqlDbType.Int;
                parametro[3].Value = 2;

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@subcategoria";
                parametro[4].SqlDbType = SqlDbType.Int;
                parametro[4].Value = 1;

                dtCategorias = new DataTable();
                dtCategorias.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtCategorias, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                DataRow row = dtCategorias.NewRow();
                row["id_producto"] = "0";
                row["nombre"] = "Seleccione...!!!";
                row["codigo"] = "0";
                dtCategorias.Rows.InsertAt(row, 0);

                cmbCategorias.DisplayMember = "nombre";
                cmbCategorias.ValueMember = "id_producto";
                cmbCategorias.DataSource = dtCategorias;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
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
            cmbCategorias.Enabled = true;
            cmbCodigoOrigen.Enabled = true;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            BtnEliminar.Enabled = false;

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

            llenarGrid();

            grupoDatos.Enabled = false;
            cmbCodigoOrigen.Enabled = true;
            cmbCategorias.Enabled = true;

            chkModificable.Checked = false;
            chkPagaIva.Checked = false;
            chkPrecioModificable.Checked = false;
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;
            BtnEliminar.Enabled = false;

            btnAgregar.Text = "Nuevo";
            txtCodigo.Enabled = true;
            txtBuscar.Focus();
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
        
        //FUNCION PARA MOSTRAR LOS DATOS EN LOS COMOBOBOX, ESTOS NO SE DEBEN EDITAR
        private bool seleccionarDatosCombobox()
        {
            try
            {
                sSql = "";
                sSql += "select UP.cg_unidad, TC.valor_texto, UP.unidad_compra" + Environment.NewLine;
                sSql += "from cv401_unidades_productos UP, tp_codigos TC" + Environment.NewLine;
                sSql += "where TC.correlativo = UP.cg_unidad" + Environment.NewLine;
                sSql += "and UP.id_producto = @id_producto" + Environment.NewLine;
                sSql += "and UP.estado = @estado" + Environment.NewLine;
                sSql += "order by UP.unidad_compra desc";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_producto";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdProducto;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return false;
                }

                if (dtCategorias.Rows.Count == 0)
                {
                    iCambioCompra = 0;
                    iCambioConsumo = 0;
                }

                if (dtCategorias.Rows.Count == 1)
                {
                    if (Convert.ToInt32(dtConsulta.Rows[0]["unidad_compra"].ToString()) == 0)
                    {
                        iCambioCompra = 0;
                        iCambioConsumo = Convert.ToInt32(dtConsulta.Rows[1]["cg_unidad"].ToString());
                    }

                    else
                    {
                        iCambioCompra = Convert.ToInt32(dtConsulta.Rows[0]["cg_unidad"].ToString());
                        iCambioConsumo = 0;
                    }
                }

                if (dtConsulta.Rows.Count > 0)
                {
                    iCambioCompra = Convert.ToInt32(dtConsulta.Rows[0]["cg_unidad"].ToString());
                    iCambioConsumo = Convert.ToInt32(dtConsulta.Rows[1]["cg_unidad"].ToString());                    
                }

                cmbCompra.SelectedValue = iCambioCompra;
                cmbConsumo.SelectedValue = iCambioConsumo;

                return true;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA LLENAR EL DATAGRID SEGUN LA CONSULTA 
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                int a = 6;

                sSql = "";
                sSql += "select P.id_producto, P.modificable, P.precio_modificable, P.paga_iva," + Environment.NewLine;
                sSql += "P.is_active, P.codigo, NP.nombre, P.secuencia," + Environment.NewLine;
                sSql += "case P.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from cv401_productos P INNER JOIN" + Environment.NewLine;
                sSql += "cv401_nombre_productos NP ON P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = @estado_1" + Environment.NewLine;
                sSql += "and NP.estado = @estado_2" + Environment.NewLine;
                sSql += "where P.id_producto_padre = @id_producto_padre" + Environment.NewLine;
                sSql += "and P.nivel = @nivel" + Environment.NewLine;
                sSql += "and P.modificador = @modificador" + Environment.NewLine;
                sSql += "and P.subcategoria = @subcategoria" + Environment.NewLine;
                //sSql += "and P.ultimo_nivel = @ultimo_nivel" + Environment.NewLine;

                if (txtBuscar.Text.Trim() != "")
                {
                    a = 7;
                    sSql += "and NP.nombre like '%@buscar%'" + Environment.NewLine;
                }

                sSql += "order by NP.nombre";

                int b = 0;
                parametro = new SqlParameter[a];
                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@estado_1";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = "A";
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@estado_2";
                parametro[b].SqlDbType = SqlDbType.VarChar;
                parametro[b].Value = "A";
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@id_producto_padre";
                parametro[b].SqlDbType = SqlDbType.Int;
                parametro[b].Value = cmbCategorias.SelectedValue;
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@nivel";
                parametro[b].SqlDbType = SqlDbType.Int;
                parametro[b].Value = 3;
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@modificador";
                parametro[b].SqlDbType = SqlDbType.Int;
                parametro[b].Value = 0;
                b++;

                parametro[b] = new SqlParameter();
                parametro[b].ParameterName = "@subcategoria";
                parametro[b].SqlDbType = SqlDbType.Int;
                parametro[b].Value = 1;
                b++;

                //parametro[b] = new SqlParameter();
                //parametro[b].ParameterName = "@ultimo_nivel";
                //parametro[b].SqlDbType = SqlDbType.Int;
                //parametro[b].Value = 1;
                
                if (a == 7)
                {
                    b++;
                    parametro[b] = new SqlParameter();
                    parametro[b].ParameterName = "@buscar";
                    parametro[b].SqlDbType = SqlDbType.VarChar;
                    parametro[b].Value = txtBuscar.Text.Trim();
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvDatos.Rows.Add(dtConsulta.Rows[i]["id_producto"].ToString(),
                                      dtConsulta.Rows[i]["modificable"].ToString(),
                                      dtConsulta.Rows[i]["precio_modificable"].ToString(),
                                      dtConsulta.Rows[i]["paga_iva"].ToString(),
                                      dtConsulta.Rows[i]["is_active"].ToString(),
                                      dtConsulta.Rows[i]["codigo"].ToString(),
                                      dtConsulta.Rows[i]["nombre"].ToString(),
                                      dtConsulta.Rows[i]["secuencia"].ToString(),
                                      dtConsulta.Rows[i]["estado"].ToString()
                        
                        );
                }

                dgvDatos.ClearSelection();
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                limpiar();
            }
        }
        
        //FUNCION PARA CONSULTAR EL CODIGO A INGRESAR
        private int iContarCodigos()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and codigo = @codigo";

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@codigo";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = sCodigoCategoria.Trim() + "." + txtCodigo.Text.Trim();

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return Convert.ToInt32(dtConsulta.Rows[0]["cuenta"].ToString());
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR UN NUEVO REGISTRO
        private void insertarRegistro()
        {
            try
            {
                int a;

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
                sSql += "idempresa, codigo, id_producto_padre, estado, nivel, modificable," + Environment.NewLine;
                sSql += "precio_modificable, paga_iva, secuencia, modificador, subcategoria," + Environment.NewLine;
                sSql += "ultimo_nivel, is_active, fecha_ingreso, usuario_ingreso,terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "@idempresa, @codigo, @id_producto_padre, @estado, @nivel, @modificable," + Environment.NewLine;
                sSql += "@precio_modificable, @paga_iva, @secuencia, @modificador, @subcategoria," + Environment.NewLine;
                sSql += "@ultimo_nivel, @is_active, getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[15];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@idempresa";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbEmpresa.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sCodigoCategoria + "." + txtCodigo.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto_padre";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbCategorias.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 3;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@modificable";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iModificable;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@precio_modificable";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPrecioModificable;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@paga_iva";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPagaIva;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@secuencia";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(txtSecuencia.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@modificador";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@subcategoria";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iSubcategoria;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@ultimo_nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iUltimo;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];

                #endregion                

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                sTabla = "cv401_productos";
                sCampo = "id_Producto"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de productos";
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdProducto = Convert.ToInt32(iMaximo);

                //INSERTAR EN LA TABLA CV401_NOMBRES_PRODUCTOS
                sSql = "";
                sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_nombre, nombre, nombre_interno, estado," + Environment.NewLine;
                sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_nombre, @nombre, @nombre_interno, @estado," + Environment.NewLine;
                sSql += "@numero_replica_trigger, @numero_control_replica, getdate()," + Environment.NewLine;
                sSql += "@usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[9];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_nombre";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = cg_tipoNombre;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nombre";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nombre_interno";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = nombInterno;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];


                #endregion                

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE COMPRA
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_unidad, @cg_unidad, @unidad_compra, @estado," + Environment.NewLine;
                sSql += "@usuario_creacion, @terminal_creacion, getdate(), @numero_replica_trigger," + Environment.NewLine;
                sSql += "@numero_control_replica, getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iUnidadCompra;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbCompra.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@unidad_compra";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];


                #endregion                
                
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE CONSUMO
                sSql = "";
                sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_unidad, @cg_unidad, @unidad_compra, @estado," + Environment.NewLine;
                sSql += "@usuario_creacion, @terminal_creacion, getdate(), @numero_replica_trigger," + Environment.NewLine;
                sSql += "@numero_control_replica, getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[11];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iUnidadConsumo;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_unidad";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbConsumo.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@unidad_compra";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_creacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_replica_trigger";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@numero_control_replica";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_ingreso";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];


                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado correctamente";
                ok.ShowDialog();
                limpiarNuevo();
                grupoDatos.Enabled = false;
                llenarGrid();
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
                int a;

                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "secuencia = @secuencia," + Environment.NewLine;
                sSql += "paga_iva = @paga_iva," + Environment.NewLine;
                sSql += "modificable = @modificable," + Environment.NewLine;
                sSql += "precio_modificable = @precio_modificable," + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_producto = @id_producto";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[6];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@secuencia";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(txtSecuencia.Text);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@paga_iva";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPagaIva;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@modificable";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iModificable;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@precio_modificable";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iPrecioModificable;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@is_active";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iHabilitado;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdProducto;
                a++;

                #endregion

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //ACTUALIZA LA TABLA CV401_NOMBRE_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                string sNombreVerificar = txtDescripcion.Text.Trim().ToUpper();

                if (sNombreVerificar != sNombreOriginal)
                {
                    sSql = "";
                    sSql += "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql += "estado = @estado_1," + Environment.NewLine;
                    sSql += "fecha_anula = getdate()," + Environment.NewLine;
                    sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                    sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and estado = @estado_2";

                    #region PARAMETROS

                    a = 0;
                    parametro = new SqlParameter[5];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_1";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "E";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_anula";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_anula";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdProducto;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_2";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "A";

                    #endregion

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAR EN LA TABLA CV401_NOMBRES_PRODUCTOS
                    sSql = "";
                    sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                    sSql += "id_producto, cg_tipo_nombre, nombre, nombre_interno, estado," + Environment.NewLine;
                    sSql += "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "@id_producto, @cg_tipo_nombre, @nombre, @nombre_interno, @estado," + Environment.NewLine;
                    sSql += "@numero_replica_trigger, @numero_control_replica, getdate()," + Environment.NewLine;
                    sSql += "@usuario_ingreso, @terminal_ingreso)";

                    #region PARAMETROS

                    a = 0;
                    parametro = new SqlParameter[9];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdProducto;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@cg_tipo_nombre";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = cg_tipoNombre;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@nombre";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@nombre_interno";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = nombInterno;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "A";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@numero_replica_trigger";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@numero_control_replica";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_ingreso";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_ingreso";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];


                    #endregion

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //UNIDAD DE COMPRA
                if (Convert.ToInt32(cmbCompra.SelectedValue) != iCambioCompra)
                {
                    sSql = "";
                    sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                    sSql += "estado = @estado_1," + Environment.NewLine;
                    sSql += "fecha_anula = getdate()," + Environment.NewLine;
                    sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                    sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and estado = @estado_2" + Environment.NewLine;
                    sSql += "and unidad_compra = @unidad_compra";

                    #region PARAMETROS

                    a = 0;
                    parametro = new SqlParameter[6];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_1";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "E";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_anula";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_anula";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdProducto;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_2";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "A";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@unidad_compra";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 1;

                    #endregion

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE COMPRA
                    sSql = "";
                    sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                    sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                    sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "@id_producto, @cg_tipo_unidad, @cg_unidad, @unidad_compra, @estado," + Environment.NewLine;
                    sSql += "@usuario_creacion, @terminal_creacion, getdate(), @numero_replica_trigger," + Environment.NewLine;
                    sSql += "@numero_control_replica, getdate(), @usuario_ingreso, @terminal_ingreso)";

                    #region PARAMETROS

                    a = 0;
                    parametro = new SqlParameter[11];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdProducto;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@cg_tipo_unidad";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iUnidadCompra;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@cg_unidad";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = Convert.ToInt32(cmbCompra.SelectedValue);
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@unidad_compra";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 1;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "A";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_creacion";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_creacion";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@numero_replica_trigger";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@numero_control_replica";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_ingreso";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_ingreso";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];


                    #endregion

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //UNIDAD DE CONSUMO
                if (Convert.ToInt32(cmbConsumo.SelectedValue) != iCambioConsumo)
                {
                    sSql = "";
                    sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                    sSql += "estado = @estado_1," + Environment.NewLine;
                    sSql += "fecha_anula = getdate()," + Environment.NewLine;
                    sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                    sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and estado = @estado_2" + Environment.NewLine;
                    sSql += "and unidad_compra = @unidad_compra";

                    #region PARAMETROS

                    a = 0;
                    parametro = new SqlParameter[6];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_1";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "E";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_anula";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_anula";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdProducto;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_2";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "A";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@unidad_compra";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;

                    #endregion

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSERTAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE CONSUMO
                    sSql = "";
                    sSql += "insert into cv401_unidades_productos (" + Environment.NewLine;
                    sSql += "id_producto, cg_tipo_unidad, cg_unidad, unidad_compra, estado," + Environment.NewLine;
                    sSql += "usuario_creacion, terminal_creacion, fecha_creacion, numero_replica_trigger," + Environment.NewLine;
                    sSql += "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += "@id_producto, @cg_tipo_unidad, @cg_unidad, @unidad_compra, @estado," + Environment.NewLine;
                    sSql += "@usuario_creacion, @terminal_creacion, getdate(), @numero_replica_trigger," + Environment.NewLine;
                    sSql += "@numero_control_replica, getdate(), @usuario_ingreso, @terminal_ingreso)";

                    #region PARAMETROS

                    a = 0;
                    parametro = new SqlParameter[11];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdProducto;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@cg_tipo_unidad";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iUnidadConsumo;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@cg_unidad";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = Convert.ToInt32(cmbConsumo.SelectedValue);
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@unidad_compra";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "A";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_creacion";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_creacion";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@numero_replica_trigger";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@numero_control_replica";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = 0;
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@usuario_ingreso";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[0];
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@terminal_ingreso";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = Program.sDatosMaximo[1];


                    #endregion

                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                        catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();
                limpiarNuevo();
                grupoDatos.Enabled = false;
                llenarGrid();
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

        //FUNCION PARA DAR DE BAJA UN REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "is_active = @is_active" + Environment.NewLine;
                sSql += "where id_producto = @id_producto" + Environment.NewLine;
                sSql += "and estado = @estado";

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@is_active";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 0;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdProducto;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    return;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "El registro se ha inhabilitado con éxito.";
                ok.ShowDialog();
                limpiar();
                btnAgregar.Text = "Nuevo";
                llenarGrid();
                return;
            }

            catch (Exception ex)
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        #endregion

        private void frmSubCategoria_Load(object sender, EventArgs e)
        {            
            LLenarComboEmpresa();
            LLenarComboPadre();

            cmbCategorias.SelectedValueChanged -= new EventHandler(cmbCategorias_SelectedIndexChanged);
            LLenarComboCategorias();
            cmbCategorias.SelectedValueChanged += new EventHandler(cmbCategorias_SelectedIndexChanged);

            LLenarComboCompra();
            LLenarComboConsumo();
        }        

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Nuevo")
            {
                if (Convert.ToInt32(cmbCategorias.SelectedValue) == 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "Favor seleccione el dato padre para crear el registro.";
                    ok.ShowDialog();
                    cmbCompra.Focus();
                    return;
                }

                limpiarNuevo();
                cmbCodigoOrigen.Enabled = false;
                cmbCategorias.Enabled = false;
                grupoDatos.Enabled = true;
                btnAgregar.Text = "Guardar";
                BtnEliminar.Enabled = false;
                txtCodigo.Focus();
                return;
            }

            if (txtCodigo.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese el código de la sub categoría.";
                ok.ShowDialog();
                txtCodigo.Focus();
                return;
            }

            if (txtDescripcion.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la descripción de la categoría.";
                ok.ShowDialog();
                txtDescripcion.Focus();
                return;
            }

            if (cmbCompra.SelectedValue.ToString() == "0")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la unidad de compra del producto.";
                ok.ShowDialog();
                cmbCompra.Focus();
                return;
            }

            if (cmbConsumo.SelectedValue.ToString() == "0")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor seleccione la unidad de consumo del producto.";
                ok.ShowDialog();
                cmbConsumo.Focus();
                return;
            }

            if (txtSecuencia.Text == "")
            {
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Favor ingrese la secuencia del producto.";
                ok.ShowDialog();
                txtSecuencia.Focus();
                return;
            }            

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

            if (chkHabilitado.Checked == true)
                iHabilitado = 1;
            else
                iHabilitado = 0;

            NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

            if (btnAgregar.Text == "Guardar")
            {
                int iCuenta_P = iContarCodigos();

                if (iCuenta_P == -1)
                    return;

                if (iCuenta_P > 0)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El código ingresado ya existe en el sistema. Favor ingrese un nuevo código.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                    insertarRegistro();
            }

            else
            {
                NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                    actualizarRegistro();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                NuevoSiNo.lblMensaje.Text = "¿Está seguro de inhabilitar el registro seleccionado?";
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
                llenarGrid();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAgregar.Text = "Actualizar";
                BtnEliminar.Enabled = true;
                grupoDatos.Enabled = true;
                cmbCategorias.Enabled = false;
                cmbCodigoOrigen.Enabled = false;
                txtCodigo.Enabled = false;

                iIdProducto = Convert.ToInt32(dgvDatos.CurrentRow.Cells["id_producto"].Value);

                if (seleccionarDatosCombobox() == false)
                    return;
                
                string sCodigo_P = dgvDatos.CurrentRow.Cells["codigo"].Value.ToString();
                string[] item = sCodigo_P.Split('.');

                sCodigoSeparado = item[2];
                txtCodigo.Text = sCodigoSeparado;

                txtDescripcion.Text = dgvDatos.CurrentRow.Cells["nombre"].Value.ToString();
                sNombreOriginal = dgvDatos.CurrentRow.Cells["nombre"].Value.ToString().Trim().ToUpper();
                txtSecuencia.Text = dgvDatos.CurrentRow.Cells["secuencia"].Value.ToString();

                if (Convert.ToBoolean(dgvDatos.CurrentRow.Cells["modificable"].Value) == true)
                    chkModificable.Checked = true;
                else
                    chkModificable.Checked = false;

                if (Convert.ToBoolean(dgvDatos.CurrentRow.Cells["precio_modificable"].Value) == true)
                    chkPrecioModificable.Checked = true;
                else
                    chkPrecioModificable.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["paga_iva"].Value) == 1)
                    chkPagaIva.Checked = true;
                else
                    chkPagaIva.Checked = false;

                if (Convert.ToInt32(dgvDatos.CurrentRow.Cells["is_active"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                chkHabilitado.Enabled = true;
                txtDescripcion.Focus();
            }

            catch(Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cmbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbCategorias.SelectedValue) == 0)
            {
                dgvDatos.Rows.Clear();
                cmbCategorias.Focus();
                sCodigoCategoria = "0";
                return;
            }

            DataRow[] dFila = dtCategorias.Select("id_producto = " + Convert.ToInt32(cmbCategorias.SelectedValue));

            if (dFila.Length != 0)
            {
                sCodigoCategoria = dFila[0][2].ToString().Trim();
                llenarGrid();
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter = new Clases.ClaseValidarCaracteres();
            caracter.soloNumeros(e);
        }
    }
}
