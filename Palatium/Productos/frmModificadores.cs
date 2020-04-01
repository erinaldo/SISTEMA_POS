using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Productos
{
    public partial class frmModificadores : Form
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        int iModificable;
        int iPrecioModificable;
        int iExpira;
        int iPagaIva;
        int iIdListaBase;
        int iIdListaMinorista;
        int iIdProducto;
        int iIdPadre;
        int iTipoUnidadCompra;
        int iTipoUnidadConsumo;
        int iUnidadCompra;
        int iUnidadConsumo;
        int iCuenta;

        double dSubtotal;

        string sSql;
        string sTabla;
        string sCampo;
        string sEstado;
        string sFechaListaBase;
        string sFechaListaMinorista;
        string sNombreProducto;
        string sPrecioBase;
        string sPrecioMinorista;
        string sCodigoValidar;

        long iMaximo;

        DataTable dtConsulta;

        bool bRespuesta;

        public frmModificadores()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA OBTENER LOS DATOS DE LA LISTA BASE Y MINORISTA
        private void datosListas()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio, fecha_fin_validez" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_base = 1" + Environment.NewLine;
                sSql += "and estado = 'A'" + Environment.NewLine;
                sSql += "union" + Environment.NewLine;
                sSql += "select id_lista_precio, fecha_fin_validez" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where lista_minorista = 1" + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdListaBase = Convert.ToInt32(dtConsulta.Rows[0]["id_lista_precio"]);
                        sFechaListaBase = Convert.ToDateTime(dtConsulta.Rows[0]["fecha_fin_validez"].ToString()).ToString("yyyy/MM/dd");
                        iIdListaMinorista = Convert.ToInt32(dtConsulta.Rows[1]["id_lista_precio"]);
                        sFechaListaMinorista = Convert.ToDateTime(dtConsulta.Rows[1]["fecha_fin_validez"].ToString()).ToString("yyyy/MM/dd");
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE EMPRESA
        private void llenarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select idempresa, isnull(nombrecomercial, razonsocial) nombre" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedIndex = 1;
                }

                cmbEmpresa.Enabled = false;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL COMBOBOX DE PRODUCTOS PADRE
        private void llenarComboPadre()
        {
            try
            {
                sSql = "";
                sSql = sSql + "Select PRD.id_producto, '['+PRD.codigo+'] '+ NOM.nombre nombre" + Environment.NewLine;
                sSql = sSql + "from cv401_productos PRD, cv401_nombre_productos NOM" + Environment.NewLine;
                sSql = sSql + "where PRD.nivel = 1" + Environment.NewLine;
                sSql = sSql + "and PRD.ESTADO = 'A'" + Environment.NewLine;
                sSql = sSql + "and NOM.ESTADO = 'A'" + Environment.NewLine;
                sSql = sSql + "and PRD.id_producto = NOM.id_producto" + Environment.NewLine;
                sSql = sSql + "and NOM.nombre_interno = 1" + Environment.NewLine;
                sSql = sSql + "order by PRD.codigo ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbPadre.llenar(dtConsulta, sSql);

                if (cmbPadre.Items.Count > 0)
                {
                    cmbPadre.SelectedIndex = 2;
                }

                cmbPadre.Enabled = false;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para extraer el identificador del modificador
        private bool identificadorModificador()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, UP.cg_tipo_unidad, UP.unidad_compra, UP.cg_unidad" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_unidades_productos UP" + Environment.NewLine;
                sSql += "where UP.id_producto = P.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and UP.estado = 'A'" + Environment.NewLine;
                //sSql += "and P.codigo = '" + Program.sCodigoModificador + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iIdPadre = Convert.ToInt32(dtConsulta.Rows[0][0].ToString());

                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            if (dtConsulta.Rows[i][2].ToString() == "1")
                            {
                                iTipoUnidadCompra = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                                iUnidadCompra = Convert.ToInt32(dtConsulta.Rows[i][3].ToString());
                            }

                            else
                            {
                                iUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[i][1].ToString());
                                iTipoUnidadConsumo = Convert.ToInt32(dtConsulta.Rows[i][3].ToString());
                            }
                        }

                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    return false;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //FUNCION PARA LLENAR EL DBAYUDA
        private void llenarDbAyuda()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto as Id_producto, P.codigo as Código,NP.nombre as Nombre" + Environment.NewLine;
                sSql += "from cv401_productos P,cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.id_producto_padre = " + Convert.ToInt32(cmbPadre.SelectedValue) + Environment.NewLine;
                sSql += "and P.nivel = 2" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado= 'A'" + Environment.NewLine;
                sSql += "and P.otros = 1" + Environment.NewLine;
                sSql += "order by P.id_producto";

                dBAyudaModificadores.Ver(sSql, "NP.nombre", 0, 1, 2);
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO DE LOCALIDADES
        private void llenarDestinoImpresion()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_impresion_comanda, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_impresion_comanda" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'" + Environment.NewLine;
                sSql = sSql + "order by id_pos_impresion_comanda";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbDestinoImpresion.llenar(dtConsulta, sSql);

                if (cmbDestinoImpresion.Items.Count > 0)
                {
                    cmbDestinoImpresion.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combo de clase de producto
        private void llenarComboClaseProducto()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_clase_producto, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_clase_producto" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbClaseProducto.llenar(dtConsulta, sSql);

                if (cmbClaseProducto.Items.Count > 0)
                {
                    cmbClaseProducto.SelectedIndex = 1;
                }
            }
            catch (Exception exc)
            {
                catchMensaje.lblMensaje.Text = exc.ToString();
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combo de tipo de producto
        private void llenarComboTipoProducto()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select id_pos_tipo_producto, descripcion" + Environment.NewLine;
                sSql = sSql + "from pos_tipo_producto" + Environment.NewLine;
                sSql = sSql + "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbTipoProducto.llenar(dtConsulta, sSql);

                if (cmbTipoProducto.Items.Count > 0)
                {
                    cmbTipoProducto.SelectedIndex = 1;
                }
            }

            catch (Exception exc)
            {
                catchMensaje.lblMensaje.Text = exc.ToString();
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "select P.id_producto, P.codigo as CODIGO, NP.nombre as MODIFICADOR, P.modificable as Modificable," + Environment.NewLine;
                sSql += "P.precio_modificable as Prec_Modificable, P.paga_iva as Paga_Iva, P.secuencia as SECUENCIA," + Environment.NewLine;
                sSql += "P.id_pos_clase_producto, P.id_pos_tipo_producto," + Environment.NewLine;
                sSql += "case P.estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO," + Environment.NewLine;
                sSql += "0.00 as precio_compra, 0.00 as precio_minorista," + Environment.NewLine;
                sSql += conexion.GFun_St_esnulo() + "(P.id_pos_impresion_comanda, 0) id_pos_impresion_comanda" + Environment.NewLine;
                sSql += "from cv401_productos P, cv401_nombre_productos NP" + Environment.NewLine;
                sSql += "where P.id_producto = NP.id_producto" + Environment.NewLine;
                sSql += "and P.estado = 'A'" + Environment.NewLine;
                sSql += "and NP.estado = 'A'" + Environment.NewLine;
                sSql += "and id_producto_padre = " + dBAyudaModificadores.iId + Environment.NewLine;
                sSql += "and P.nivel = 3" + Environment.NewLine;
                sSql += "and modificador = 1" + Environment.NewLine;
                sSql += "and P.subcategoria = 0" + Environment.NewLine;
                sSql += "and P.ultimo_nivel = 1" + Environment.NewLine;                                
                sSql += "order by P.codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        completarGrid();
                    }

                    else
                    {
                        dtConsulta = new DataTable();
                        dtConsulta.Clear();
                        dgvDatos.DataSource = dtConsulta;
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA RELLENAR EL DATAGRID
        private void completarGrid()
        {
            try
            {
                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    iIdProducto = Convert.ToInt32(dgvDatos.Rows[i].Cells[0].Value);

                    //INSTRUCCION PARA REEMPLAZAR EL VALOR DE LA COLUMNA LISTA BASE
                    sSql = "";
                    sSql = sSql + "select PR.valor" + Environment.NewLine;
                    sSql = sSql + "from cv403_precios_productos PR inner join" + Environment.NewLine;
                    sSql = sSql + "cv401_productos P on PR.id_producto = P.id_producto" + Environment.NewLine;
                    sSql = sSql + "where id_lista_precio in (" + iIdListaBase + ", " + iIdListaMinorista + ")" + Environment.NewLine;
                    sSql = sSql + "and P.id_producto = " + iIdProducto + Environment.NewLine;
                    sSql = sSql + "and PR.estado = 'A'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            dgvDatos.Rows[i].Cells[10].Value = (Convert.ToDouble(dtConsulta.Rows[0][0].ToString()) * (1 + Program.iva + Program.servicio)).ToString("N2");
                            dgvDatos.Rows[i].Cells[11].Value = (Convert.ToDouble(dtConsulta.Rows[1][0].ToString()) * (1 + Program.iva + Program.servicio)).ToString("N2");
                        }

                        else
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto fin;
                        }
                    }

                    else
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto fin;
                    }
                }

                //OCULTAR COLUMAS Y PONER TAMAÑOS AL DATAGRID VIEW
                dgvDatos.Columns[1].Width = 65;
                dgvDatos.Columns[2].Width = 150;
                dgvDatos.Columns[6].Width = 75;

                dgvDatos.Columns[0].Visible = false;
                dgvDatos.Columns[3].Visible = false;
                dgvDatos.Columns[4].Visible = false;
                dgvDatos.Columns[5].Visible = false;
                dgvDatos.Columns[7].Visible = false;
                dgvDatos.Columns[8].Visible = false;
                dgvDatos.Columns[10].Visible = false;
                dgvDatos.Columns[11].Visible = false;
                dgvDatos.Columns[12].Visible = false;

                dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvDatos.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto fin;
            }

        fin:
            { }
        }

        //FUNCION PARA CONTAR REGISTROS
        private double contarPedidos()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv403_det_pedidos" + Environment.NewLine;
                sSql += "where id_producto = " + iIdProducto + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToDouble(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }


        //FUNCION PARA CONTAR REGISTROS
        private int contarRegistros()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where codigo = '" + txtCodigo.Text.Trim().ToUpper() + "'" + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }


        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                iCuenta = contarRegistros();

                if (iCuenta == -1)
                {
                    return;
                }

                else if (iCuenta > 0)
                {
                    ok.lblMensaje.Text = "El código ingresado ya se encuentra registrado en el sistema.";
                    ok.ShowDialog();
                    return;
                }

                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                iIdPadre = dBAyudaModificadores.iId;

                //INSTRUCCION SQL PARA INSERTAR EN LA TABLA CV401_PRODUCTOS
                sSql = "";
                sSql = sSql + "insert into cv401_productos (" + Environment.NewLine;
                sSql = sSql + "idempresa, codigo, id_Producto_padre, estado, Nivel," + Environment.NewLine;
                sSql = sSql + "modificable, precio_modificable, paga_iva, secuencia," + Environment.NewLine;
                sSql = sSql + "modificador, subcategoria, ultimo_nivel," + Environment.NewLine;
                sSql = sSql + "stock_min, stock_max, Expira, fecha_ingreso, usuario_ingreso," + Environment.NewLine;
                sSql = sSql + "terminal_ingreso, id_pos_tipo_producto, id_pos_clase_producto," + Environment.NewLine;
                sSql = sSql + "id_pos_impresion_comanda)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(cmbEmpresa.SelectedValue) + ", '" + txtCodigo.Text.Trim().ToUpper() + "', " + iIdPadre + "," + Environment.NewLine;
                sSql = sSql + "'A', 3, " + iModificable + ", " + iPrecioModificable + ", " + iPagaIva + "," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(txtSecuencia.Text.ToString().Trim()) + ", 1, 0, 1, 0, 0, " + iExpira +  ", " + Environment.NewLine;
                sSql = sSql + "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(cmbTipoProducto.SelectedValue) + ", " + Convert.ToInt32(cmbClaseProducto.SelectedValue) + "," + Environment.NewLine;
                sSql = sSql + Convert.ToInt32(cmbDestinoImpresion.SelectedValue) + " )";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
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
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de productos.";
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdProducto = Convert.ToInt32(iMaximo);
                }

                //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_NOMBRE_PRODUCTOS
                sSql = "";
                sSql = sSql + "insert into cv401_nombre_productos (" + Environment.NewLine;
                sSql = sSql + "id_Producto, cg_tipo_nombre, nombre, nombre_interno," + Environment.NewLine;
                sSql = sSql + "estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql = sSql + "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + iIdProducto + ", 5076, '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql = sSql + "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + (Program.iva + Program.servicio));

                sSql = "";
                sSql = sSql + "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql = sSql + "id_Lista_Precio, id_Producto, valor_Porcentaje," + Environment.NewLine;
                sSql = sSql + "valor, fecha_inicio, fecha_final, estado," + Environment.NewLine;
                sSql = sSql + "numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql = sSql + "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + iIdListaBase + ", " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                sSql = sSql + "'" + sFechaListaBase + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql = sSql + "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + (Program.iva + Program.servicio));

                sSql = "";
                sSql = sSql + "insert into cv403_precios_productos (" + Environment.NewLine;
                sSql = sSql + "id_Lista_Precio, id_Producto, valor_Porcentaje," + Environment.NewLine;
                sSql = sSql + "valor, fecha_inicio, fecha_final, estado," + Environment.NewLine;
                sSql = sSql + "numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                sSql = sSql + "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + iIdListaMinorista + ", " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                sSql = sSql + "'" + sFechaListaMinorista + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql = sSql + "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSTRUCCIONES PARA INSERTAR EN  LA TABLA CV401_UNIDADES_PRODUCTOS
                //INSERTAR LA UNIDAD DE COMPRA
                sSql = "";
                sSql = sSql + "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql = sSql + "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql = sSql + "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql = sSql + "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                sSql = sSql + " usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + iIdProducto + ", " + iTipoUnidadCompra + ", " + iUnidadCompra + "," + Environment.NewLine;
                sSql = sSql + "1, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql = sSql + "GETDATE(), 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //INSERTAR LA UNIDAD DE CONSUMO
                sSql = "";
                sSql = sSql + "insert into cv401_unidades_productos (" + Environment.NewLine;
                sSql = sSql + "id_Producto, cg_tipo_unidad, cg_unidad, unidad_compra," + Environment.NewLine;
                sSql = sSql + "estado, usuario_creacion, terminal_creacion, fecha_creacion," + Environment.NewLine;
                sSql = sSql + "numero_replica_trigger, numero_control_replica, fecha_ingreso," + Environment.NewLine;
                sSql = sSql + "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql = sSql + "values(" + Environment.NewLine;
                sSql = sSql + iIdProducto + ", " + iTipoUnidadConsumo + ", " + iUnidadConsumo + "," + Environment.NewLine;
                sSql = sSql + "0, 'A', '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql = sSql + "GETDATE(), 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI SE EJECUTA TODO REALIZA EL COMMIT
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro ingresado correctamente.";
                ok.ShowDialog();
                limpiarTodo();
                llenarGrid();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = sSql;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }

        }

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                //ACTUALIZA LA TABLA CV401_PRODUCTOS CON LOS DATOS NUEVOS DEL FORMULARIO
                sSql = "";
                sSql = sSql + "update cv401_productos set" + Environment.NewLine;
                sSql = sSql + "estado = '" + sEstado + "'," + Environment.NewLine;
                sSql = sSql + "secuencia = '" + txtSecuencia.Text.ToString().Trim() + "'," + Environment.NewLine;
                sSql = sSql + "paga_iva = " + iPagaIva + "," + Environment.NewLine;
                sSql = sSql + "precio_modificable = " + iPrecioModificable + "," + Environment.NewLine;
                sSql = sSql + "modificable = " + iModificable + "," + Environment.NewLine;
                sSql = sSql + "expira = " + iExpira + "," + Environment.NewLine;
                sSql = sSql + "id_pos_tipo_producto = " + Convert.ToInt32(cmbTipoProducto.SelectedValue) + "," + Environment.NewLine;
                sSql = sSql + "id_pos_clase_producto = " + Convert.ToInt32(cmbClaseProducto.SelectedValue) + "," + Environment.NewLine;
                sSql = sSql + "id_pos_impresion_comanda = " + Convert.ToInt32(cmbDestinoImpresion.SelectedValue) + Environment.NewLine;
                sSql = sSql + "where id_Producto = '" + iIdProducto + "'";

                //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //SI HUBO ALGUN CAMBIO EN EL NOMBRE DEL PRODUCTO, SE REALIZA LA ACTUALIZACION
                if (txtDescripcion.Text.Trim() != sNombreProducto)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql = sSql + "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql = sSql + "estado = 'E'," + Environment.NewLine;
                    sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql = sSql + "where id_Producto = '" + iIdProducto + "'";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA INSERTAR EN LA TABLA CV401_NOMBRE_PRODUCTOS
                    sSql = "";
                    sSql = sSql + "insert into cv401_nombre_productos (" + Environment.NewLine;
                    sSql = sSql + "id_Producto, cg_tipo_nombre, nombre, nombre_interno," + Environment.NewLine;
                    sSql = sSql + "estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                    sSql = sSql + "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql = sSql + "values(" + Environment.NewLine;
                    sSql = sSql + iIdProducto + ", 5076, '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                    sSql = sSql + "1, 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //SI HUBO ALGUN CAMBIO EN EL PRECIO BASE, SE REALIZA LA ACTUALIZACION
                if (txtPrecioCompra.Text.Trim() != sPrecioBase)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql = sSql + "update cv403_precios_productos set" + Environment.NewLine;
                    sSql = sSql + "estado = 'E'," + Environment.NewLine;
                    sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql = sSql + "where id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql = sSql + "and id_Lista_Precio = " + iIdListaBase;

                    //SI NO SE EJECUTA LA INSTRUCCION SALTA A REVERSA 
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA BASE
                    dSubtotal = Convert.ToDouble(txtPrecioCompra.Text) / (1 + (Program.iva + Program.servicio));

                    sSql = "";
                    sSql = sSql + "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql = sSql + "id_Lista_Precio, id_Producto, valor_Porcentaje, valor," + Environment.NewLine;
                    sSql = sSql + "fecha_inicio, fecha_final, estado, numero_replica_trigger," + Environment.NewLine;
                    sSql = sSql + "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql = sSql + "values(" + Environment.NewLine;
                    sSql = sSql + iIdListaBase + ", " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                    sSql = sSql + "'" + sFechaListaBase + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                //SI HUBO ALGUN CAMBIO EN EL PRECIO MINORISTA, SE REALIZA LA ACTUALIZACION
                if (txtPrecioMinorista.Text.Trim() != sPrecioMinorista)
                {
                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E'
                    sSql = "";
                    sSql = sSql + "update cv403_precios_productos set" + Environment.NewLine;
                    sSql = sSql + "estado = 'E'," + Environment.NewLine;
                    sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql = sSql + "where id_Producto = " + iIdProducto + Environment.NewLine;
                    sSql = sSql + "and id_Lista_Precio = " + iIdListaMinorista;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //INSTRUCCION PARA NSERTAR EN LA TABLA CV403_PRECIOS_PRODUCTOS CON LISTA MINORISTA
                    dSubtotal = Convert.ToDouble(txtPrecioMinorista.Text) / (1 + (Program.iva + Program.servicio));

                    sSql = "";
                    sSql = sSql + "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql = sSql + "id_Lista_Precio, id_Producto, valor_Porcentaje, valor," + Environment.NewLine;
                    sSql = sSql + "fecha_inicio, fecha_final, estado,numero_replica_trigger," + Environment.NewLine;
                    sSql = sSql + "numero_control_replica, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql = sSql + "values(" + Environment.NewLine;
                    sSql = sSql + iIdListaMinorista + ", " + iIdProducto + ", 0, " + dSubtotal + ", GETDATE()," + Environment.NewLine;
                    sSql = sSql + "'" + sFechaListaMinorista + "', 'A', 1, 1, GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "'" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                sSql = "";
                sSql = sSql + "select cg_tipo_unidad, cg_unidad, unidad_compra" + Environment.NewLine;
                sSql = sSql + "from cv401_unidades_productos" + Environment.NewLine;
                sSql = sSql + "where id_producto = " + iIdProducto + Environment.NewLine;
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == false)
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();
                limpiarTodo();
                llenarGrid();
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

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }
                else
                {
                    //ELIMINACION DEL PRODUCTO EN CV401_PRODUCTOS
                    sSql = "";
                    sSql = sSql + "update cv401_productos set" + Environment.NewLine;
                    sSql = sSql + "codigo = '" + txtCodigo.Text.Trim() + "(" + iIdProducto + ")'," + Environment.NewLine;
                    sSql = sSql + "estado = 'E'," + Environment.NewLine;
                    sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql = sSql + "where id_producto = " + iIdProducto;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //ELIMINACION DEL PRODUCTO EN CV401_NOMBRE_PRODUCTOS
                    sSql = "";
                    sSql = sSql + "update cv401_nombre_productos set" + Environment.NewLine;
                    sSql = sSql + "estado = 'E'," + Environment.NewLine;
                    sSql = sSql + "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql = sSql + "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql = sSql + "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                    sSql = sSql + "where id_Producto = " + iIdProducto;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    //si se ejecuta bien hara un commit
                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "El registro se ha eliminado con éxito.";
                    ok.ShowDialog();
                    limpiarTodo();
                    llenarGrid();
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

        //FUNCION PARA LIMPIAR
        private void limpiarTodo()
        {
            llenarComboEmpresa();
            llenarComboPadre();
            llenarComboClaseProducto();
            llenarComboTipoProducto();
            llenarDestinoImpresion();
            datosListas();
            llenarDbAyuda();
            cmbEstado.Text = "ACTIVO";

            txtCodigo.Enabled = true;
            grupoDatos.Enabled = false;
            grupoOpciones.Enabled = false;
            grupoPrecio.Enabled = false;
            grupoImpresion.Enabled = false;
            btnAnular.Enabled = false;

            chkPagaIva.Checked = false;
            chkPrecioModificable.Checked = false;
            chkExpira.Checked = false;

            iIdProducto = 0;

            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecioCompra.Clear();
            txtPrecioMinorista.Clear();
            txtSecuencia.Clear();

            btnNuevo.Text = "Nuevo";
        }

        #endregion

        private void frmModificadores_Load(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dBAyudaModificadores.iId == 0)
            {
                ok.lblMensaje.Text = "Favor seleccione el ítem modificador.";
                ok.ShowDialog();
            }

            else
            {
                llenarGrid();
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                iIdProducto = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString().Trim();
                sNombreProducto = dgvDatos.CurrentRow.Cells[2].Value.ToString();

                iPagaIva = Convert.ToInt32(dgvDatos.CurrentRow.Cells[5].Value);
                if (iPagaIva == 1)
                {
                    chkPagaIva.Checked = true;
                }

                else
                {
                    chkPagaIva.Checked = false;
                }

                txtSecuencia.Text = dgvDatos.CurrentRow.Cells[6].Value.ToString();
                cmbClaseProducto.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[7].Value);
                cmbTipoProducto.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[8].Value);
                cmbEstado.Text = dgvDatos.CurrentRow.Cells[9].Value.ToString();
                txtPrecioCompra.Text = dgvDatos.CurrentRow.Cells[10].Value.ToString();
                sPrecioBase = dgvDatos.CurrentRow.Cells[10].Value.ToString();
                txtPrecioMinorista.Text = dgvDatos.CurrentRow.Cells[11].Value.ToString();
                sPrecioMinorista = dgvDatos.CurrentRow.Cells[11].Value.ToString();
                cmbDestinoImpresion.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[12].Value);

                txtCodigo.Enabled = false;
                grupoDatos.Enabled = true;
                grupoOpciones.Enabled = true;
                grupoPrecio.Enabled = true;
                grupoImpresion.Enabled = true;
                btnAnular.Enabled = true;
                btnNuevo.Text = "Actualizar";

                txtDescripcion.Focus();
                txtDescripcion.SelectionStart = txtDescripcion.Text.Trim().Length;
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloDecimales(sender, e, 2);
        }

        private void txtPrecioMinorista_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloDecimales(sender, e, 2);
        }

        private void txtSecuencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (btnNuevo.Text == "Nuevo")
            {
                iIdProducto = 0;
                grupoDatos.Enabled = true;
                grupoPrecio.Enabled = true;
                grupoImpresion.Enabled = true;
                grupoOpciones.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
            }

            else
            {
                if (Convert.ToInt32(cmbEmpresa.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor selecciones la empresa.";
                    ok.ShowDialog();
                    cmbEmpresa.Focus();
                }

                else if (Convert.ToInt32(cmbPadre.SelectedValue) == 0)
                {
                    ok.lblMensaje.Text = "Favor selecciones el identificador del modificador.";
                    ok.ShowDialog();
                    cmbPadre.Focus();
                }

                else if (txtCodigo.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el código del modificador.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el nombre del modificador.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (txtSecuencia.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el número de secuencia del modificador.";
                    ok.ShowDialog();
                    txtSecuencia.Focus();
                }

                else
                {
                    if (chkPrecioModificable.Checked == true)
                    {
                        iPrecioModificable = 1;
                    }

                    else
                    {
                        iPrecioModificable = 0;
                    }

                    if (chkExpira.Checked == true)
                    {
                        iExpira = 1;
                    }

                    else
                    {
                        iExpira = 0;
                    }

                    if (chkPagaIva.Checked == true)
                    {
                        iPagaIva = 1;
                    }

                    else
                    {
                        iPagaIva = 0;
                    }

                    if (cmbEstado.Text == "ACTIVO")
                    {
                        sEstado = "A";
                    }

                    else
                    {
                        sEstado = "N";
                    }

                    //COMPLETAR EL CAMPO DE CODIGO EN CASO DE QUE SOLO SE INGRESE UN NUMERO
                    if (txtCodigo.Text.Trim().Length == 1)
                    {
                        txtCodigo.Text = "0" + txtCodigo.Text.Trim();
                    }

                    //ENVIO A GUARDAR O ACTUALIZAR
                    if (btnNuevo.Text == "Guardar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }

                    else if (btnNuevo.Text == "Actualizar")
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Está seguro que desea actualizar el registro?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            actualizarRegistro();
                        }
                    }
                }
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            try
            {
                if (iIdProducto == 0)
                {
                    ok.lblMensaje.Text = "No hay un registro para eliminar.";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                }

                else
                {
                    if (contarPedidos() > 0)
                    {
                        ok.lblMensaje.Text = "No puede eliminar el registro, ya que se encuentra en uso dentro del sistema.";
                        ok.ShowDialog();
                    }

                    else if (contarPedidos() == 0)
                    {
                        NuevoSiNo.lblMensaje.Text = "¿Está seguro de eliminar el registro seleccionado?";
                        NuevoSiNo.ShowDialog();

                        if (NuevoSiNo.DialogResult == DialogResult.OK)
                        {
                            eliminarRegistro();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                limpiarTodo();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dBAyudaModificadores.limpiar();
            limpiarTodo();
            grupoBotones.Enabled = false;
        }
    }
}
