using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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
        int iUnidadCompra = 6142;
        int iUnidadConsumo = 6143;

        string sSql;
        string sNombreOriginal;

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

        string sCodigoSeparado;
        string sTabla;
        string sCampo;

        int iIdUnidadCompra;
        int iIdUnidadConsumo;

        int iIdCategoria;

        SqlParameter[] parametro;

        public frmCategorias()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CONSULTAR LAS UNIDADES
        private bool consultarUnidadesProductos(int iIdRegistro_P)
        {
            try
            {
                sSql = "";
                sSql += "select cg_tipo_unidad, cg_unidad" + Environment.NewLine;
                sSql += "from cv401_unidades_productos" + Environment.NewLine;
                sSql += "where id_producto = @id_producto" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_producto";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdRegistro_P;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                #endregion

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

                if (dtConsulta.Rows.Count > 0)
                {
                    iIdUnidadCompra = Convert.ToInt32(dtConsulta.Rows[0]["cg_unidad"].ToString());
                    iIdUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[1]["cg_unidad"].ToString());
                    cmbCompra.SelectedValue = dtConsulta.Rows[0]["cg_unidad"].ToString();
                    cmbConsumo.SelectedValue = dtConsulta.Rows[1]["cg_unidad"].ToString();
                }

                else
                {
                    iIdUnidadCompra = 0;
                    iIdUnidadConsumo = 0;
                    cmbCompra.SelectedValue = "0";
                    cmbConsumo.SelectedValue = "0";
                }

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

        //FUNCION PARA EXTRAER LA IMAGEN DE LA BASE DE DATOS
        private bool extraerImagenBDD(int iIdRegistro_P)
        {
            try
            {
                sSql = "";
                sSql += "select isnull(imagen_categoria, '') imagen_categoria" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and id_producto = @id_producto";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@id_producto";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = iIdRegistro_P;

                #endregion

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

                if (dtConsulta.Rows.Count == 0)
                {
                    txtBase64.Text = "";
                    imgLogo.Image = null;
                }

                else
                {
                    txtBase64.Text = dtConsulta.Rows[0]["imagen_categoria"].ToString();

                    if (txtBase64.Text.Trim() == "")
                    {
                        imgLogo.Image = null;
                    }

                    else
                    {
                        byte[] imageBytes;
                        Image foto = null;

                        imageBytes = Convert.FromBase64String(txtBase64.Text.Trim());

                        using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                        {
                            foto = Image.FromStream(ms, true);
                        }

                        imgLogo.Image = foto;
                    }
                }

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

        private string ConvertirImagenToBase64(Image file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.Save(memoryStream, file.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        //FUNCION PARA CONSULTAR EL ID DE MODIFICADORES
        private int contarRegistroModificador()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where estado = @estado" + Environment.NewLine;
                sSql += "and modificador = @modificador" + Environment.NewLine;
                sSql += "and nivel = @nivel";

                #region PARAMETROS

                parametro = new SqlParameter[3];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@estado";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = "A";

                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@modificador";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 1;

                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@nivel";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = 2;

                #endregion

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
            sValor = "";

            txtBuscarCategoria.Clear();
            txtCodigoCategoria.Clear();
            txtDescripcion.Clear();
            txtSecuencia.Clear();
            txtRuta.Clear();
            txtBase64.Clear();

            imgLogo.Image = null;

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

            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;

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

            LLenarComboPadre();
            LLenarComboCompra();
            LLenarComboConsumo();
            sValor = "";

            txtCodigoCategoria.Clear();
            txtDescripcion.Clear();
            txtBuscarCategoria.Clear();
            txtSecuencia.Clear();
            txtRuta.Clear();
            txtBase64.Clear();

            imgLogo.Image = null;

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
            chkHabilitado.Checked = true;
            chkHabilitado.Enabled = false;

            btnEliminar.Enabled = false;
            cmbPadre.Enabled = true;

            dtConsulta = new DataTable();
            dgvCategoria.DataSource = dtConsulta;

            grupoDatos.Enabled = false;
            llenarGrid();

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
        private void llenarGrid()
        {
            try
            {
                dgvCategoria.Rows.Clear();

                int a = 5;

                sSql = "";
                sSql += "select P.id_producto, P.modificable, P.precio_modificable, P.paga_iva," + Environment.NewLine;
                sSql += "P.modificador, P.subcategoria, P.menu_pos, isnull(P.is_active, 0) is_active," + Environment.NewLine;
                sSql += "P.otros, P.maneja_almuerzos, P.detalle_por_origen, P.detalle_independiente," + Environment.NewLine;
                sSql += "isnull(P.categoria_delivery, 0) categoria_delivery," + Environment.NewLine;
                sSql += "P.codigo, NP.nombre, P.secuencia, case P.is_active when 1 then 'ACTIVO' else 'INACTIVO' end estado" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and id_producto_padre in (" + Environment.NewLine;
                sSql += "select id_producto from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = @codigo)" + Environment.NewLine;
                sSql += "and P.nivel = @nivel" + Environment.NewLine;
                sSql += "and P.estado in (@estado_1, @estado_2)" + Environment.NewLine;
                sSql += "and NP.estado = @estado_3"+ Environment.NewLine;

                if (txtBuscarCategoria.Text.Trim() != "")
                {
                    a++;
                    sSql += "and NP.nombre LIKE '%@buscar%'";
                }

                sSql += "order by P.id_producto";

                #region PARAMETROS

                parametro = new SqlParameter[a];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@codigo";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = cmbPadre.SelectedValue;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@nivel";
                parametro[1].SqlDbType = SqlDbType.Int;
                parametro[1].Value = 2;

                parametro[2] = new SqlParameter();
                parametro[2].ParameterName = "@estado_1";
                parametro[2].SqlDbType = SqlDbType.VarChar;
                parametro[2].Value = "A";

                parametro[3] = new SqlParameter();
                parametro[3].ParameterName = "@estado_2";
                parametro[3].SqlDbType = SqlDbType.VarChar;
                parametro[3].Value = "N";

                parametro[4] = new SqlParameter();
                parametro[4].ParameterName = "@estado_3";
                parametro[4].SqlDbType = SqlDbType.VarChar;
                parametro[4].Value = "A";

                if (a == 6)
                {
                    parametro[5] = new SqlParameter();
                    parametro[5].ParameterName = "@buscar";
                    parametro[5].SqlDbType = SqlDbType.VarChar;
                    parametro[5].Value = txtBuscarCategoria.Text.Trim();
                }

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

                if (bRespuesta == false)
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    limpiar();
                    return;
                }

                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    dgvCategoria.Rows.Add(dtConsulta.Rows[i]["id_producto"].ToString(),
                                          dtConsulta.Rows[i]["modificable"].ToString(),
                                          dtConsulta.Rows[i]["precio_modificable"].ToString(),
                                          dtConsulta.Rows[i]["paga_iva"].ToString(),
                                          dtConsulta.Rows[i]["modificador"].ToString(),
                                          dtConsulta.Rows[i]["subcategoria"].ToString(),
                                          dtConsulta.Rows[i]["menu_pos"].ToString(),
                                          dtConsulta.Rows[i]["is_active"].ToString(),
                                          dtConsulta.Rows[i]["otros"].ToString(),
                                          dtConsulta.Rows[i]["maneja_almuerzos"].ToString(),
                                          dtConsulta.Rows[i]["detalle_por_origen"].ToString(),
                                          dtConsulta.Rows[i]["detalle_independiente"].ToString(),
                                          dtConsulta.Rows[i]["categoria_delivery"].ToString(),
                                          dtConsulta.Rows[i]["codigo"].ToString(),
                                          dtConsulta.Rows[i]["nombre"].ToString(),
                                          dtConsulta.Rows[i]["secuencia"].ToString(),
                                          dtConsulta.Rows[i]["estado"].ToString()
                        );
                }

                dgvCategoria.ClearSelection();
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
                int a;

                sSql = "";
                sSql += "select * from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = @codigo" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@codigo";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = sValor;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

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
                sSql += "where codigo = @codigp" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@codigo";
                parametro[0].SqlDbType = SqlDbType.VarChar;
                parametro[0].Value = sValor;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                #endregion

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro_Parametros(dtConsulta, sSql, parametro);

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
                sSql += "uso_receta, categoria_delivery, imagen_categoria)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@idempresa, @codigo, @id_producto_padre, @estado, @nivel, @modificable," + Environment.NewLine;
                sSql += "@precio_modificable, @paga_iva, @secuencia, @modificador, @subcategoria," + Environment.NewLine;
                sSql += "@ultimo_nivel, @otros, getdate(), @usuario_ingreso, @terminal_ingreso," + Environment.NewLine;
                sSql += "@menu_pos, @maneja_almuerzos, @detalle_por_origen, @detalle_independiente," + Environment.NewLine;
                sSql += "@uso_receta, @categoria_delivery, @imagen_categoria)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[22];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@idempresa";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(cmbEmpresa.SelectedValue);
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sValor;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto_padre";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = idPadre;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iNivel;
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
                parametro[a].Value = Convert.ToInt32(txtSecuencia.Text.Trim());
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@modificador";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iModificador;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@subcategoria";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iTieneSubCategoria;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@ultimo_nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iUltimo;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@otros";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iOtros;
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
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@menu_pos";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iMenuPos;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@maneja_almuerzos";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iManejaAlmuerzos;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@detalle_por_origen";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iDetallarPorOrigen;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@detalle_independiente";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iDetalleIndependiente;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@uso_receta";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 0;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@categoria_delivery";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iCategoriaDelivery;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@imagen_categoria";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtBase64.Text.Trim();

                #endregion
                                    
                //sisque no me ejuta el query 
                if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                {
                    catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                    catchMensaje.lblMensaje.Text = conexion.sMensajeError;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DEL PRODUCTO REGISTRADO
                sTabla = "cv401_productos";
                sCampo = "id_producto"; ;

                long iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de productos.";
                    ok.ShowDialog();
                    goto reversa;
                }

                iIdCategoria = Convert.ToInt32(iMaximo);
                
                sSql = "";
                sSql += "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql += "id_producto, cg_tipo_nombre, nombre, nombre_interno," + Environment.NewLine;
                sSql += "estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += "@id_producto, @cg_tipo_nombre, @nombre, @nombre_interno," + Environment.NewLine;
                sSql += "@estado, @numero_replica_trigger, @numero_control_replica," + Environment.NewLine;
                sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[9];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdCategoria;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@cg_tipo_nombre";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 5076;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nombre";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtDescripcion.Text.Trim().ToUpper();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@nombre_interno";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = 1;
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
                parametro[a].Value = iIdCategoria;
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
                parametro[a].Value = iIdCategoria;
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

                //si no se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok = new VentanasMensajes.frmMensajeNuevoOk();
                ok.lblMensaje.Text = "Registro ingresado correctamente.";
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
                    ok.lblMensaje.Text = "Error al abrir transacción para actualizar el registro.";
                    ok.ShowDialog();
                    limpiar();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql += "update cv401_productos set" + Environment.NewLine;
                sSql += "codigo = @codigo," + Environment.NewLine;
                sSql += "secuencia = @secuencia," + Environment.NewLine;
                sSql += "subcategoria = @subcategoria," + Environment.NewLine;
                sSql += "modificador = @modificador," + Environment.NewLine;
                sSql += "ultimo_nivel = @ultimo_nivel," + Environment.NewLine;
                sSql += "paga_iva = @paga_iva," + Environment.NewLine;
                sSql += "modificable = @modificable," + Environment.NewLine;
                sSql += "precio_modificable = @precio_modificable," + Environment.NewLine;
                sSql += "otros = @otros," + Environment.NewLine;
                sSql += "menu_pos = @menu_pos," + Environment.NewLine;
                sSql += "maneja_almuerzos = @maneja_almuerzos," + Environment.NewLine;
                sSql += "detalle_por_origen = @detalle_por_origen," + Environment.NewLine;
                sSql += "detalle_independiente = @detalle_independiente," + Environment.NewLine;
                sSql += "categoria_delivery = @categoria_delivery," + Environment.NewLine;
                sSql += "imagen_categoria = @imagen_categoria" + Environment.NewLine;
                sSql += "where id_producto = @id_producto" + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[17];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@codigo";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = sValor;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@secuencia";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = Convert.ToInt32(txtSecuencia.Text.Trim());
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@subcategoria";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iTieneSubCategoria;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@modificador";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iModificador;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@ultimo_nivel";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iUltimo;
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
                parametro[a].ParameterName = "@otros";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iOtros;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@menu_pos";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iMenuPos;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@maneja_almuerzos";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iManejaAlmuerzos;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@detalle_por_origen";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iDetallarPorOrigen;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@detalle_independiente";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iDetalleIndependiente;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@categoria_delivery";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iCategoriaDelivery;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@imagen_categoria";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = txtBase64.Text.Trim();
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@id_producto";
                parametro[a].SqlDbType = SqlDbType.Int;
                parametro[a].Value = iIdCategoria;
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "A";

                #endregion

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
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
                    parametro[a].Value = iIdCategoria;
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
                    parametro[a].Value = iIdCategoria;
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
                    parametro[a].Value = "1";
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

                //CAMBIO EN EL SISTEMA
                //-----------------------------------------------------------------------------------------------
                //ELIMINAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE COMPRA
                sSql = "";
                sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                sSql += "estado = @estado_1," + Environment.NewLine;
                sSql += "fecha_anulacion = getdate()," + Environment.NewLine;
                sSql += "usuario_anulacion = @usuario_anulacion," + Environment.NewLine;
                sSql += "terminal_anulacion = @terminal_anulacion," + Environment.NewLine;
                sSql += "fecha_anula = getdate()," + Environment.NewLine;
                sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                sSql += "where id_Producto = @id_producto" + Environment.NewLine;
                sSql += "and estado = @estado_2" + Environment.NewLine;
                sSql += "and unidad_compra = @unidad_compra";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[8];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_1";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "E";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_anulacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_anulacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
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
                parametro[a].Value = iIdCategoria;
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
                parametro[a].Value = iIdCategoria;
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

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //ELIMINAR EN LA TABLA CV401_UNIDADES_PRODUCTOS - UNIDAD DE COMPRA
                sSql = "";
                sSql += "update cv401_unidades_productos set" + Environment.NewLine;
                sSql += "estado = @estado_1," + Environment.NewLine;
                sSql += "fecha_anulacion = getdate()," + Environment.NewLine;
                sSql += "usuario_anulacion = @usuario_anulacion," + Environment.NewLine;
                sSql += "terminal_anulacion = @terminal_anulacion," + Environment.NewLine;
                sSql += "fecha_anula = getdate()," + Environment.NewLine;
                sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                sSql += "where id_Producto = @id_producto" + Environment.NewLine;
                sSql += "and estado = @estado_2" + Environment.NewLine;
                sSql += "and unidad_compra = @unidad_compra";

                #region PARAMETROS

                a = 0;
                parametro = new SqlParameter[8];
                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@estado_1";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = "E";
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@usuario_anulacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[0];
                a++;

                parametro[a] = new SqlParameter();
                parametro[a].ParameterName = "@terminal_anulacion";
                parametro[a].SqlDbType = SqlDbType.VarChar;
                parametro[a].Value = Program.sDatosMaximo[1];
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
                parametro[a].Value = iIdCategoria;
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
                parametro[a].Value = iIdCategoria;
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

                //-----------------------------------------------------------------------------------------------

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
                int a;

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
                    sSql += "estado = @estado_1," + Environment.NewLine;
                    sSql += "codigo = codigo + '.' + @id_producto_1," + Environment.NewLine;
                    sSql += "fecha_anula = getdate()," + Environment.NewLine;
                    sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                    sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto_2" + Environment.NewLine;
                    sSql += "and estado = @estado_2";

                    #region FUNCIONES DEL USUARIO

                    a = 0;
                    parametro = new SqlParameter[6];
                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@estado_1";
                    parametro[a].SqlDbType = SqlDbType.VarChar;
                    parametro[a].Value = "E";
                    a++;

                    parametro[a] = new SqlParameter();
                    parametro[a].ParameterName = "@id_producto_1";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdCategoria;
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
                    parametro[a].ParameterName = "@id_producto_2";
                    parametro[a].SqlDbType = SqlDbType.Int;
                    parametro[a].Value = iIdCategoria;
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
                    
                    //si se ejecuta bien hara un commit
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                    ok.ShowDialog();
                    limpiarNuevo();
                    btnAgregar.Text = "Nuevo";
                    llenarGrid();
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
                sSql += "where id_producto_padre = @id_producto_padre" + iIdCategoria + Environment.NewLine;
                sSql += "and estado = @estado";

                #region PARAMETROS

                parametro = new SqlParameter[2];
                parametro[0] = new SqlParameter();
                parametro[0].ParameterName = "@id_producto_padre";
                parametro[0].SqlDbType = SqlDbType.Int;
                parametro[0].Value = iIdCategoria;

                parametro[1] = new SqlParameter();
                parametro[1].ParameterName = "@estado";
                parametro[1].SqlDbType = SqlDbType.VarChar;
                parametro[1].Value = "A";

                #endregion

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
            LLenarComboCompra();
            LLenarComboConsumo();            
            llenarGrid();
        }

        private void btnLimpiarCategori_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnNuevoCategori_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbPadre.SelectedValue) == 0)
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

                    lblEtiquetaImagen.Visible = false;
                    imgLogo.Visible = false;
                    btnExaminar.Visible = false;
                    btnClear.Visible = false;
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

                    lblEtiquetaImagen.Visible = true;
                    imgLogo.Visible = true;
                    btnExaminar.Visible = true;
                    btnClear.Visible = true;
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

                if (txtBase64.Text.Trim().Length > 8000)
                {
                    ok = new VentanasMensajes.frmMensajeNuevoOk();
                    ok.lblMensaje.Text = "El ícono para el botón supera el tamaño permitido. Favor seleccione un nuevo ícono.";
                    ok.ShowDialog();
                    imgLogo.Image = null;
                    txtBase64.Clear();
                    txtRuta.Clear();
                    return;
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

                    if (chkTieneModifcador.Checked == true)
                    {
                        int iCantidadModificador = contarRegistroModificador();

                        if (iCantidadModificador == -1)
                            return;

                        if (iCantidadModificador > 0)
                        {
                            ok = new VentanasMensajes.frmMensajeNuevoOk();
                            ok.lblMensaje.Text = "Ya existe un registro para modificadores.";
                            ok.ShowDialog();
                            return;
                        }
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
            try
            {
                iIdCategoria = Convert.ToInt32(dgvCategoria.CurrentRow.Cells["id_producto"].Value.ToString());

                if (extraerImagenBDD(iIdCategoria) == false)
                    return;

                if (consultarUnidadesProductos(iIdCategoria) == false)
                    return;

                List<String> lista = dgvCategoria.CurrentRow.Cells["codigo"].Value.ToString().Split(Convert.ToChar(".")).ToList<String>();

                foreach (String item in lista)
                {
                    sCodigoSeparado = item;
                }

                if (sCodigoSeparado == "2")
                    txtCodigoCategoria.Text = "";
                else
                    txtCodigoCategoria.Text = sCodigoSeparado;

                txtDescripcion.Text = dgvCategoria.CurrentRow.Cells["nombre"].Value.ToString();
                sNombreOriginal = dgvCategoria.CurrentRow.Cells["nombre"].Value.ToString().Trim().ToUpper();

                if (Convert.ToBoolean(dgvCategoria.CurrentRow.Cells["modificable"].Value) == true)
                    chkModificable.Checked = true;
                else 
                    chkModificable.Checked = false;

                if (Convert.ToBoolean(dgvCategoria.CurrentRow.Cells["precio_modificable"].Value) == true)
                    chkPreModificable.Checked = true;
                else 
                    chkPreModificable.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["paga_iva"].Value) == 1)
                    chkPagaIva.Checked = true;
                else 
                    chkPagaIva.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["modificador"].Value) == 1)
                    chkTieneModifcador.Checked = true;
                else
                    chkTieneModifcador.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["subcategoria"].Value) == 1)
                    chkTieneSubCategoria.Checked = true;
                else
                    chkTieneSubCategoria.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["menu_pos"].Value) == 1)
                    chkMenuPos.Checked = true;
                else
                    chkMenuPos.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["is_active"].Value) == 1)
                    chkHabilitado.Checked = true;
                else
                    chkHabilitado.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["otros"].Value) == 1)
                    chkOtros.Checked = true;
                else
                    chkOtros.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["maneja_almuerzos"].Value) == 1)
                    chkAlmuerzos.Checked = true;
                else
                    chkAlmuerzos.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["detalle_por_origen"].Value) == 1)
                    chkDetallarOrigen.Checked = true;
                else
                    chkDetallarOrigen.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["detalle_independiente"].Value) == 1)
                    chkDetalleIndependiente.Checked = true;
                else
                    chkDetalleIndependiente.Checked = false;

                if (Convert.ToInt32(dgvCategoria.CurrentRow.Cells["categoria_delivery"].Value) == 1)
                    chkDelivery.Checked = true;
                else
                    chkDelivery.Checked = false;

                txtSecuencia.Text = dgvCategoria.CurrentRow.Cells["secuencia"].Value.ToString();
                
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

                chkHabilitado.Enabled = true;
                btnAgregar.Text = "Actualizar";
                grupoDatos.Enabled = true;
                cmbPadre.Enabled = true;
                btnEliminar.Enabled = true;
                cmbPadre.Enabled = false;
            }

            catch (Exception ex)
            {
                catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void cmbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtRuta.Clear();
            txtBase64.Clear();
            imgLogo.Image = null;
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = abrir.FileName;
                imgLogo.Image = Image.FromFile(txtRuta.Text.Trim());
                imgLogo.SizeMode = PictureBoxSizeMode.Zoom;
                txtBase64.Text = ConvertirImagenToBase64(imgLogo.Image);
            }

            abrir.Dispose();
        }

    }
}
