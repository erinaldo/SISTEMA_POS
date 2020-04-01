using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmNuevaAdministracionReceta : Form
    {
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        DataTable dtConsulta;
        DataTable dtUnidades;

        bool bRespuesta;
        bool bNuevoRegistro;

        decimal dbRendimientoTotal;
        decimal dbCostoTotalTotal = 0;
        decimal dbCostoUnitarioTotal = 0;
        decimal dbSumaRendimiento = 0;
        decimal dbPorcentajeParaServicios;
        decimal dbPorcentajeUtilidadGanancias;
        decimal dbNumeroPorciones;
        decimal dbPreciodeVenta;
        decimal dbUtilidadDeGanancias;
        decimal dbUtilidadDeServicios;
        decimal dbPorcentajeDeUtilidad;
        decimal dbPorcentajeDeCostos;
        decimal dbPorcentajeGananciaDeseada;
        decimal dbPorcentajeGananciaServicioDeseado;

        string sSql;
        string sTabla;
        string sCampo;
        string sFecha;
        string sNombre_R;
        string sUnidad_R;
        string sRendimiento_R;
        string sCantidadBruta_R;
        string sCantidadNeta_R;
        string sCostoUnitario_R;
        string sImporte_R;
        string sIdProducto_R;
        string sIdUnidad_R;
        string []sDatos = new string[15];

        long iMaximo;

        int IidPosDetalleReceta;
        int iIdReceta;
        int iCantidadIngredientes;

        //VARIABLES PARA LA EXTRACCION DE DETALLE DE RECETA        
        decimal dbCantidadBruta_R;
        decimal dbCantidadNeta_R;
        decimal dbCostoUnitario_R;
        decimal dbCostoTotal_P;
        decimal dbRendimiento_R;
        decimal dbImporte_R;
        int iIdUnidad_R;
        int iIdPosPorcion_R;
        int iIdProducto_R;
        int iCgUnidad_R;
        decimal dbEquivalencia_R;

        public frmNuevaAdministracionReceta()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION  PARA COMPROBAR SI LA RECETA NO ESTÁ ASOCIADO A UN PRODUCTO
        private int consultarRecetaProducto()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from cv401_productos" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta + Environment.NewLine;
                sSql += "and estado = 'A'";

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

        //FUnción para llenar el combo de temperatura
        private void llenarComboTemperatura()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_temperatura_de_servicio, descripcion" + Environment.NewLine;
                sSql += "from pos_temperatura_de_servicio" + Environment.NewLine;
                sSql += "where estado = 'A' ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbTemperaturaDeServicio.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combo de Empresa
        private void llenarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select C.Correlativo, C.Valor_Texto, C.Valor_Fecha, C.Valor_Numero, C.Tabla," + Environment.NewLine;
                sSql += "C.Valor_Texto Descripcion" + Environment.NewLine;
                sSql += "from tp_codigos C1,tp_codigos C, tp_relaciones R" + Environment.NewLine;
                sSql += "where C.Tabla = R.Tabla_Contenida And C.Codigo = R.Codigo_Contenido" + Environment.NewLine;
                sSql += "and R.CG_Tipo_Relacion = C1.Correlativo" + Environment.NewLine;
                sSql += "and C.Tabla = 'SYS$00017'" + Environment.NewLine;
                sSql += "and R.Tabla_Contenedora = 'SYS$00045'" + Environment.NewLine;
                sSql += "and R.Codigo_Contenedor = 'FJIMENEZ'" + Environment.NewLine;       //FJIMENEZ VERIFICAR
                sSql += "and C1.Codigo = '2'" + Environment.NewLine;
                sSql += "and C1.Estado = 'A'" + Environment.NewLine;
                sSql += "and C.Estado = 'A'" + Environment.NewLine;
                sSql += "and R.Estado = 'A'" + Environment.NewLine;
                sSql += "order By C.Valor_Texto ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbEmpresa.llenar(dtConsulta, sSql);

                if (cmbEmpresa.Items.Count > 0)
                {
                    cmbEmpresa.SelectedValue = Program.iCgEmpresa;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combo de Clasificacion
        private void llenarComboClasificacion()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_clasificacion_receta, descripcion, codigo" + Environment.NewLine;
                sSql += "from pos_clasificacion_receta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbClasificacion.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el combo de Receta
        private void llenarComboReceta()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_tipo_receta, descripcion, codigo" + Environment.NewLine;
                sSql += "principal, complementaria" + Environment.NewLine;
                sSql += "from pos_tipo_receta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbReceta.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar el origen
        private void llenarComboOrigen()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_origen_receta, descripcion, codigo" + Environment.NewLine;
                sSql += "from pos_origen_receta" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbOrigen.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para llenar las sentencias del dbAyuda
        private void llenarSentencias()
        {
            try
            {
                sSql = "";
                sSql += "select codigo, descripcion, id_pos_receta" + Environment.NewLine;
                sSql += "from pos_receta" + Environment.NewLine;
                sSql += "where estado = 'A' ";
                dbAyudaReceta.Ver(sSql, "codigo", 2, 0, 1);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para limpiar los campos
        private void limpiarCampos()
        {
            bNuevoRegistro = true;
            dgvReceta.Rows.Clear();
            txtCostoTotal.Text = "0";
            txtCostoUnitario.Text = "0";
            txtPorcentajeDeCosto.Text = "0";
            txtPorcentajeDeUtilidad.Text = "0";
            txtPrecioDeVenta.Text = "0";
            txtRendimiento.Text = "0";
            txtUtilidadDeGanancias.Text = "0";
            txtUtilidadDeServicios.Text = "0";
            txtNumeroPorciones.Text = "1";
            txtDescripcion.Text = "";
            txtCodigo.Text = "";
            txtPesoGramos.Text = "0";
            txtCostoPorGramo.Text = "0";
            txtCantidadNetaGramos.Text = "0";
            dbAyudaReceta.txtIdentificacion.Text = "";
            dbAyudaReceta.txtDatos.Text = "";
            cmbClasificacion.SelectedIndex = 0;
            cmbOrigen.SelectedIndex = 0;
            cmbReceta.SelectedIndex = 0;
            cmbTemperaturaDeServicio.SelectedIndex = 0;
            txtPorcentajeGananciaDeseada.Text = "100";
            txtPorcentajeServicioDeseado.Text = "10";
            btnA.Enabled = false;
            btnX.Enabled = false;
        }

        //Función para Habilitar los controles
        private void habilitarControles()
        {
            btnA.Enabled = true;
            btnX.Enabled = true;
        }

        //Función para verificar si es un nuevo registro
        private bool nuevoRegistro()
        {
            try
            {
                if (dbAyudaReceta.txtIdentificacion.Text.Trim() == "")
                {
                    bNuevoRegistro = true;
                    return true;
                }
                else
                {
                    bNuevoRegistro = false;
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

        //Función para comprobar si los campos están llenos
        private bool comprobarCampos()
        {
            try
            {
                int iBandera = 0;
                if (Convert.ToInt32(cmbClasificacion.SelectedValue) == 0)
                {
                    mensaje("una clasificación");
                    iBandera = 1;
                }
                else if (Convert.ToInt32(cmbReceta.SelectedValue) == 0)
                {
                    mensaje("un tipo de receta");
                    iBandera = 1;
                }
                else if (txtNumeroPorciones.Text.Trim() == "")
                {
                    mensaje("el número de porciones");
                    txtNumeroPorciones.Focus();
                    iBandera = 1;
                }
                //else if (txtPrecioDeVenta.Text.Trim() == "")
                //{
                //    mensaje("el precio de venta");
                //    txtPrecioDeVenta.Focus();
                //    iBandera = 1;
                //}
                else if (txtDescripcion.Text.Trim() == "")
                {
                    mensaje("el nombre del plato");
                    txtDescripcion.Focus();
                    iBandera = 1;
                }
                else if (txtCodigo.Text.Trim() == "")
                {
                    mensaje("el código del plato");
                    txtCodigo.Focus();
                    iBandera = 1;
                }

                else if (txtPesoGramos.Text.Trim() == "")
                {
                    mensaje("el peso en gramos del plato");
                    txtCodigo.Focus();
                    iBandera = 1;
                }

                if (iBandera == 1) return false; else return true;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return false;
            }
        }

        //Función para mostrar un mensaje
        private void mensaje(string sMensaje)
        {
            ok.lblMensaje.Text = "Advertencia: Debe seleccionar " + sMensaje + ".";
            ok.ShowDialog();
        }

        //Función para recuperar información
        private void recuperarInformacion()
        {
            try
            {
                iIdReceta = dbAyudaReceta.iId;
                habilitarControles();

                sSql = "";
                sSql += "select * from pos_vw_receta" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtDescripcion.Text = dtConsulta.Rows[0]["descripcion"].ToString().ToUpper();
                    txtCodigo.Text = dtConsulta.Rows[0]["codigo"].ToString().ToUpper();
                    txtNumeroPorciones.Text = dtConsulta.Rows[0]["num_porciones"].ToString();
                    cmbClasificacion.SelectedValue = dtConsulta.Rows[0]["id_pos_clasificacion_receta"].ToString();
                    cmbReceta.SelectedValue = dtConsulta.Rows[0]["id_pos_tipo_receta"].ToString();
                    cmbOrigen.SelectedValue = dtConsulta.Rows[0]["id_pos_origen_receta"].ToString();
                    cmbTemperaturaDeServicio.SelectedValue = dtConsulta.Rows[0]["id_pos_temperatura_de_servicio"].ToString();
                    txtPesoGramos.Text = dtConsulta.Rows[0]["peso_en_gramos"].ToString();
                    txtPorcentajeGananciaDeseada.Text = dtConsulta.Rows[0]["porcentaje_utilidad_deseada"].ToString();
                    txtUtilidadDeGanancias.Text = dtConsulta.Rows[0]["utilidad_de_ganancias"].ToString();
                    txtPorcentajeServicioDeseado.Text = dtConsulta.Rows[0]["porcentaje_servicio_deseado"].ToString();
                    txtUtilidadDeServicios.Text = dtConsulta.Rows[0]["utilidad_de_servicios"].ToString();
                    txtRendimiento.Text = dtConsulta.Rows[0]["rendimiento"].ToString();
                    txtPorcentajeDeCosto.Text = dtConsulta.Rows[0]["porcentaje_costo"].ToString();
                    txtPorcentajeDeUtilidad.Text = dtConsulta.Rows[0]["porcentaje_utilidad"].ToString();
                    txtCostoUnitario.Text = dtConsulta.Rows[0]["costo_unitario"].ToString();
                    txtCostoTotal.Text = dtConsulta.Rows[0]["costo_total"].ToString();
                    txtPrecioDeVenta.Text = dtConsulta.Rows[0]["precio_de_venta"].ToString();

                    if (Convert.ToDecimal(txtPesoGramos.Text.Trim()) == 0)
                    {
                        txtCostoPorGramo.Text = "0";
                    }

                    else
                    {
                        txtCostoPorGramo.Text = (Convert.ToDecimal(txtCostoTotal.Text.Trim()) / Convert.ToDecimal(txtPesoGramos.Text.Trim())).ToString("N6");
                    }
                    
                    completarDetalleReceta(iIdReceta);

                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para completar el detalle de la receta
        private void completarDetalleReceta(int iIdReceta_P)
        {
            try
            {
                sSql = "";
                sSql += "select * from pos_vw_detalle_receta_normal" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta_P;

                DataTable dtConsulta_1 = new DataTable();
                dtConsulta_1.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta_1, sSql);

                Decimal dbSumaGramos = 0;

                if (bRespuesta == true)
                {
                    if (dtConsulta_1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta_1.Rows.Count; i++)
                        {
                            sNombre_R = dtConsulta_1.Rows[i]["nombre"].ToString();
                            sCantidadBruta_R = dtConsulta_1.Rows[i]["cantidad_bruta"].ToString();
                            sRendimiento_R = dtConsulta_1.Rows[i]["rendimiento"].ToString();
                            sCantidadNeta_R = dtConsulta_1.Rows[i]["cantidad_neta"].ToString();
                            sUnidad_R = dtConsulta_1.Rows[i]["abreviatura"].ToString();
                            sCostoUnitario_R = dtConsulta_1.Rows[i]["costo_unitario"].ToString();
                            sImporte_R = dtConsulta_1.Rows[i]["importe"].ToString();
                            sIdProducto_R = dtConsulta_1.Rows[i]["id_producto"].ToString();
                            sIdUnidad_R = dtConsulta_1.Rows[i]["id_pos_unidad"].ToString();

                            dbSumaGramos += Convert.ToDecimal(sCantidadNeta_R);
                            
                            dgvReceta.Rows.Add(sNombre_R, sCantidadBruta_R, sRendimiento_R, sCantidadNeta_R, sUnidad_R,
                                               sCostoUnitario_R, sImporte_R, sIdProducto_R, sIdUnidad_R);
                        }

                        txtCantidadNetaGramos.Text = dbSumaGramos.ToString("N2");

                        dgvReceta.ClearSelection();
                    }
                }

                else
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //Función para grabar un Registro
        private void grabarRegistro()
        {
            try
            {
                //sSql = "";
                //sSql += "select id_pos_unidad, cg_unidad" + Environment.NewLine;
                //sSql += "from pos_unidad" + Environment.NewLine;
                //sSql += "where estado in ('A', 'N')";

                //dtUnidades = new DataTable();
                //dtUnidades.Clear();

                //bRespuesta = conexion.GFun_Lo_Busca_Registro(dtUnidades, sSql);

                //if (bRespuesta == false)
                //{
                //    ok.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                //    ok.ShowDialog();
                //    return;
                //}

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                llenarValoresTexto();

                sSql = "";
                sSql += "insert into pos_receta(" + Environment.NewLine;
                sSql += "idempresa, descripcion, codigo, num_porciones, id_pos_clasificacion_receta," + Environment.NewLine;
                sSql += "id_pos_tipo_receta, id_pos_origen_receta, id_pos_temperatura_de_servicio," + Environment.NewLine;
                sSql += "peso_en_gramos, porcentaje_utilidad_deseada, utilidad_de_ganancias, porcentaje_servicio_deseado," + Environment.NewLine;
                sSql += "utilidad_de_servicios, rendimiento, porcentaje_costo, porcentaje_utilidad," + Environment.NewLine;
                sSql += "costo_unitario, costo_total, precio_de_venta, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + ", '" + txtDescripcion.Text.Trim().ToUpper() + "', '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += txtNumeroPorciones.Text.Trim() + ", " + cmbClasificacion.SelectedValue + ", " + cmbReceta.SelectedValue + ", ";
                sSql += cmbOrigen.SelectedValue + ", " + cmbTemperaturaDeServicio.SelectedValue + "," + Environment.NewLine;
                sSql += Convert.ToDecimal(txtPesoGramos.Text.Trim()) + ", " + Convert.ToDecimal(txtPorcentajeGananciaDeseada.Text.Trim()) + ", ";
                sSql += Convert.ToDecimal(txtUtilidadDeGanancias.Text.Trim()) + ", " + Convert.ToDecimal(txtPorcentajeServicioDeseado.Text.Trim()) + "," + Environment.NewLine;
                sSql += Convert.ToDecimal(txtUtilidadDeServicios.Text.Trim()) + ", " + Convert.ToDecimal(txtRendimiento.Text.Trim()) + ", " + Convert.ToDecimal(txtPorcentajeDeCosto.Text.Trim()) + ", ";
                sSql += Convert.ToDecimal(txtPorcentajeDeUtilidad.Text.Trim()) + ", " + Convert.ToDecimal(txtCostoUnitario.Text.Trim()) + "," + Environment.NewLine;
                sSql += Convert.ToDecimal(txtCostoTotal.Text.Trim()) + ", " + Convert.ToDecimal(txtPrecioDeVenta.Text.Trim()) + ", ";
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA POS_RECETA
                dtConsulta = new DataTable();
                dtConsulta.Clear();

                sTabla = "pos_receta";
                sCampo = "id_pos_receta";

                iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                if (iMaximo == -1)
                {
                    ok.lblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                    ok.ShowDialog();
                    goto reversa;
                }

                else
                {
                    iIdReceta = Convert.ToInt32(iMaximo);
                }

                //=================================================================================

                for (int i = 0; i < dgvReceta.Rows.Count; i++)
                {
                    iIdProducto_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[7].Value);
                    dbCantidadBruta_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[1].Value);
                    dbCantidadNeta_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[3].Value);
                    dbRendimiento_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[2].Value);
                    iIdUnidad_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[8].Value);
                    dbCostoUnitario_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[5].Value);
                    dbImporte_R = dbCantidadBruta_R * dbCostoUnitario_R;

                    sSql = "";
                    sSql += "insert into pos_detalle_receta (" + Environment.NewLine;
                    sSql += "id_pos_receta, id_producto, cantidad_bruta, cantidad_neta, id_pos_unidad, costo_unitario," + Environment.NewLine;
                    sSql += "importe, rendimiento, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdReceta + ", " + iIdProducto_R + ", " + dbCantidadBruta_R + "," + Environment.NewLine;
                    sSql += dbCantidadNeta_R + ", " + iIdUnidad_R + ", " + dbCostoUnitario_R + ", " + dbImporte_R + "," + Environment.NewLine;
                    sSql += dbRendimiento_R + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro Guardado Correctamente.";
                ok.ShowDialog();
                limpiarCampos();
                llenarSentencias();
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

        //Función para actualizar un registro
        private void actualizarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_unidad, cg_unidad" + Environment.NewLine;
                sSql += "from pos_unidad" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')";

                dtUnidades = new DataTable();
                dtUnidades.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtUnidades, sSql);

                if (bRespuesta == false)
                {
                    ok.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    ok.ShowDialog();
                    return;
                }

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción.";
                    ok.ShowDialog();
                    return;
                }

                llenarValoresTexto();

                sSql = "";
                sSql += "update pos_receta set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "codigo = '" + txtCodigo.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "num_porciones = " + txtNumeroPorciones.Text.Trim() + "," + Environment.NewLine;
                sSql += "id_pos_clasificacion_receta = " + cmbClasificacion.SelectedValue + "," + Environment.NewLine;
                sSql += "id_pos_tipo_receta = " + cmbReceta.SelectedValue + "," + Environment.NewLine;
                sSql += "id_pos_origen_receta = " + cmbOrigen.SelectedValue + "," + Environment.NewLine;
                sSql += "id_pos_temperatura_de_servicio = " + cmbTemperaturaDeServicio.SelectedValue + "," + Environment.NewLine;
                sSql += "peso_en_gramos = " + Convert.ToDecimal(txtPesoGramos.Text.Trim()) + "," + Environment.NewLine;
                sSql += "porcentaje_utilidad_deseada = " + Convert.ToDecimal(txtPorcentajeGananciaDeseada.Text.Trim()) + "," + Environment.NewLine;
                sSql += "utilidad_de_ganancias = " + Convert.ToDecimal(txtUtilidadDeGanancias.Text.Trim()) + "," + Environment.NewLine;
                sSql += "porcentaje_servicio_deseado = " + Convert.ToDecimal(txtPorcentajeServicioDeseado.Text.Trim()) + "," + Environment.NewLine;
                sSql += "utilidad_de_servicios = " + Convert.ToDecimal(txtUtilidadDeServicios.Text.Trim()) + "," + Environment.NewLine;
                sSql += "rendimiento = " + Convert.ToDecimal(txtRendimiento.Text.Trim()) + "," + Environment.NewLine;
                sSql += "porcentaje_costo = " + Convert.ToDecimal(txtPorcentajeDeCosto.Text.Trim()) + "," + Environment.NewLine;
                sSql += "porcentaje_utilidad = " + Convert.ToDecimal(txtPorcentajeDeUtilidad.Text.Trim()) + "," + Environment.NewLine;
                sSql += "costo_unitario = " + Convert.ToDecimal(txtCostoUnitario.Text.Trim()) + "," + Environment.NewLine;
                sSql += "costo_total = " + Convert.ToDecimal(txtCostoTotal.Text.Trim()) + "," + Environment.NewLine;
                sSql += "precio_de_venta = " + Convert.ToDecimal(txtPrecioDeVenta.Text.Trim()) + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "update pos_detalle_receta set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                for (int i = 0; i < dgvReceta.Rows.Count; i++)
                {
                    iIdProducto_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[7].Value);
                    dbCantidadBruta_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[1].Value);
                    dbCantidadNeta_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[3].Value);
                    dbRendimiento_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[2].Value);
                    iIdUnidad_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[8].Value);
                    dbCostoUnitario_R = Convert.ToDecimal(dgvReceta.Rows[i].Cells[5].Value);
                    dbImporte_R = dbCantidadBruta_R * dbCostoUnitario_R;

                    sSql = "";
                    sSql += "insert into pos_detalle_receta (" + Environment.NewLine;
                    sSql += "id_pos_receta, id_producto, cantidad_bruta, cantidad_neta, id_pos_unidad, costo_unitario," + Environment.NewLine;
                    sSql += "importe, rendimiento, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdReceta + ", " + iIdProducto_R + ", " + dbCantidadBruta_R + "," + Environment.NewLine;
                    sSql += dbCantidadNeta_R + ", " + iIdUnidad_R + ", " + dbCostoUnitario_R + ", " + dbImporte_R + "," + Environment.NewLine;
                    sSql += dbRendimiento_R + ", 'A', GETDATE()," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro Actualizado Éxitosamente.";
                ok.ShowDialog();
                limpiarCampos();
                llenarSentencias();
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

        //Función para anular un registro
        private void anularRegistro(int ibandera)
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_receta set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                sSql = "";
                sSql += "update pos_detalle_receta set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_receta = " + iIdReceta;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA SIGUIENTE INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                if (ibandera != 1)
                {
                    ok.lblMensaje.Text = "Registro Eliminado Correctamente.";
                    ok.ShowDialog();
                    limpiarCampos();
                    llenarSentencias();
                }

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

        //Función para llenar los valores de las cajas de texto
        private void llenarValoresTexto()
        {
            try
            {
                if (dgvReceta.Rows.Count > 0)
                {
                    dbSumaRendimiento = 0;
                    dbRendimientoTotal = 0;
                    dbCostoTotalTotal = 0;
                    dbCostoUnitarioTotal = 0;

                    if ((txtNumeroPorciones.Text.Trim() == "") || (Convert.ToDecimal(txtNumeroPorciones.Text.Trim()) == 0))
                    {
                        txtNumeroPorciones.Text = "1";
                    }

                    dbNumeroPorciones = Convert.ToDecimal(txtNumeroPorciones.Text.Trim());

                    Decimal dbSumaGramos = 0;

                    for (int i = 0; i < dgvReceta.Rows.Count; i++)
                    {
                        dbSumaRendimiento += Convert.ToDecimal(dgvReceta.Rows[i].Cells[2].Value.ToString());
                        dbCostoTotalTotal += Convert.ToDecimal(dgvReceta.Rows[i].Cells[6].Value.ToString());

                        dbSumaGramos += Convert.ToDecimal(dgvReceta.Rows[i].Cells[3].Value.ToString());
                    }

                    txtCantidadNetaGramos.Text = dbSumaGramos.ToString("N2");

                    dbCostoUnitarioTotal = dbCostoTotalTotal / dbNumeroPorciones;

                    txtCostoTotal.Text = dbCostoTotalTotal.ToString("N2");
                    txtCostoUnitario.Text = dbCostoUnitarioTotal.ToString("N2");

                    dbPorcentajeParaServicios = Convert.ToDecimal(txtPorcentajeServicioDeseado.Text.Trim()) / 100;
                    dbPreciodeVenta = (dbCostoUnitarioTotal * dbPorcentajeParaServicios) + dbCostoUnitarioTotal;
                    txtPrecioDeVenta.Text = dbPreciodeVenta.ToString("N2");

                    dbPorcentajeDeCostos = (dbCostoUnitarioTotal * 100) / dbPreciodeVenta;
                    txtPorcentajeDeCosto.Text = dbPorcentajeDeCostos.ToString("N2");

                    dbPorcentajeGananciaDeseada = Convert.ToDecimal(txtPorcentajeGananciaDeseada.Text.Trim());
                    dbPorcentajeUtilidadGanancias = dbPreciodeVenta * (dbPorcentajeGananciaDeseada / 100);
                    dbUtilidadDeGanancias = dbPorcentajeUtilidadGanancias + dbPreciodeVenta;
                    txtUtilidadDeGanancias.Text = dbUtilidadDeGanancias.ToString("N2");

                    dbUtilidadDeServicios = dbCostoTotalTotal * dbPorcentajeParaServicios;
                    txtUtilidadDeServicios.Text = dbUtilidadDeServicios.ToString("N2");

                    dbPorcentajeDeUtilidad = (dbUtilidadDeServicios * 100) / dbPreciodeVenta;
                    txtPorcentajeDeUtilidad.Text = dbPorcentajeDeUtilidad.ToString("N2");

                    iCantidadIngredientes = dgvReceta.Rows.Count;
                    dbSumaRendimiento = dbSumaRendimiento / 100;
                    dbSumaRendimiento = dbSumaRendimiento / iCantidadIngredientes;

                    txtRendimiento.Text = dbSumaRendimiento.ToString("N2");

                    if (Convert.ToDecimal(txtPesoGramos.Text.Trim()) == 0)
                    {
                        txtCostoPorGramo.Text = "0";
                    }

                    else
                    {
                        txtCostoPorGramo.Text = (Convert.ToDecimal(txtCostoTotal.Text.Trim()) / Convert.ToDecimal(txtPesoGramos.Text.Trim())).ToString("N6");
                    }
                }

                else
                {
                    txtCostoTotal.Text = "0";
                    txtCostoUnitario.Text = "0";
                    txtPorcentajeDeCosto.Text = "0";
                    txtPorcentajeDeUtilidad.Text = "0";
                    txtPrecioDeVenta.Text = "0";
                    txtRendimiento.Text = "0";
                    txtUtilidadDeGanancias.Text = "0";
                    txtUtilidadDeServicios.Text = "0";
                    txtCostoPorGramo.Text = "0";
                    txtCantidadNetaGramos.Text = "0";
                    txtPesoGramos.Text = "0";
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //VALIDAR SOLO NUMEROS
        private void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 6);
        }

        #endregion


        private void txtPrecioDeVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtRendimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtNumeroPorciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtCostoUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPorcentajeDeCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtPorcentajeDeUtilidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtUtilidadDeServicios_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void txtUtilidadDeGanancias_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
        }

        private void frmNuevaAdministraciónDeReceta_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();
            llenarComboClasificacion();
            llenarComboOrigen();
            llenarComboReceta();
            llenarComboTemperatura();
            llenarSentencias();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dgvReceta.Rows.Clear();

            if (nuevoRegistro() == true)
            {
                if (comprobarCampos() == true)
                    habilitarControles();
            }
            else
                recuperarInformacion();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "¿Desea limpiar...?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                limpiarCampos();
                llenarSentencias();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (bNuevoRegistro == true)
            {
                ok.lblMensaje.Text = "El registro todavía no ha sido guardado.";
                ok.ShowDialog();
            }

            else
            {
                if (consultarRecetaProducto() > 0)
                {
                    ok.lblMensaje.Text = "No se puede eliminar el registro, ya que la receta se encuenta asociado a un producto.";
                    ok.ShowDialog();
                }

                else if (consultarRecetaProducto() == -1)
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al consultar la asociación de la receta con el producto.";
                    ok.ShowDialog();
                }

                else
                {
                    NuevoSiNo.lblMensaje.Text = "¿Desea eliminar el registro?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        anularRegistro(0);
                    }
                }
            }

        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (dgvReceta.Rows.Count > 0)
            {
                NuevoSiNo.lblMensaje.Text = "¿Desea grabar el registro?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    if (bNuevoRegistro == true)
                        grabarRegistro();
                    else
                        actualizarRegistro();
                }
            }

            else
            {
                ok.lblMensaje.Text = "Por favor, ingrese registros para almacenar la receta.";
                ok.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            Receta.frmModalIngrediente ingrediente = new Receta.frmModalIngrediente();
            ingrediente.ShowDialog();

            if (ingrediente.DialogResult == DialogResult.OK)
            {
                Decimal dbValorUnitario_R = ingrediente.dbValorUnitario;
                Decimal dbPresentacion_R = ingrediente.dbPresentacion;
                Decimal dbRendimiento_R = ingrediente.dbRendimiento;
                Decimal dbPorcentaje_R = (dbRendimiento_R * 100) / dbPresentacion_R;
                int iIdUnidad_P = ingrediente.iIdUnidad;
                Decimal iIdProducto_P = ingrediente.iIdProducto;

                dgvReceta.Rows.Add(ingrediente.sNombreProducto, "0", dbPorcentaje_R.ToString("N2"), "0",
                                   ingrediente.sUnidadConsumo, dbValorUnitario_R.ToString(), "0.00", iIdProducto_P, iIdUnidad_P);

                ingrediente.Close();
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReceta.SelectedRows.Count == 0)
                {
                    ok.lblMensaje.Text = "No se ha seleccionado una línea para remover.";
                    ok.ShowDialog();
                    return;
                }

                NuevoSiNo.lblMensaje.Text = "¿Desea eliminar la línea...?";
                NuevoSiNo.ShowDialog();

                if (NuevoSiNo.DialogResult == DialogResult.OK)
                {
                    dgvReceta.Rows.Remove(dgvReceta.CurrentRow);
                }

                if (dgvReceta.Rows.Count > 0)
                {
                    llenarValoresTexto();
                }

                else
                {
                    txtCantidadNetaGramos.Text = "0";
                    txtCostoTotal.Text = "0";
                    txtCostoPorGramo.Text = "0";
                    txtPesoGramos.Text = "0";
                    txtPrecioDeVenta.Clear();
                    txtCostoUnitario.Clear();
                    txtPorcentajeDeCosto.Clear();
                    txtPorcentajeDeUtilidad.Clear();
                    txtUtilidadDeServicios.Clear();
                    txtUtilidadDeGanancias.Clear();
                    txtRendimiento.Clear();
                }

                dgvReceta.ClearSelection();
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            llenarValoresTexto();
        }
        
        private void txtPorcentajeGananciaDeseada_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtPorcentajeServicioDeseado_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtPesoGramos_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloDecimales(sender, e, 2);
       }

        private void dgvReceta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvReceta.Columns[e.ColumnIndex].Name == "cantidad_neta")
                {
                    if (dgvReceta.Rows[e.RowIndex].Cells[3].Value == null)
                    {
                        dgvReceta.Rows[e.RowIndex].Cells[3].Value = "";
                    }

                    string sCantidadGrid_R = dgvReceta.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();

                    if ((sCantidadGrid_R != null) && (sCantidadGrid_R != ""))
                    {
                        Decimal dbCantidad_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
                        Decimal dbPrecioUnitario_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[5].Value.ToString().Trim());
                        Decimal dbPorcentaje_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[2].Value.ToString().Trim());
                        Decimal dbCantidadOriginal_R = (dbCantidad_R * 100) / dbPorcentaje_R;
                        Decimal dbPrecioTotal_R = dbCantidadOriginal_R * dbPrecioUnitario_R;

                        dgvReceta.Rows[e.RowIndex].Cells[1].Value = dbCantidadOriginal_R.ToString("N2");
                        dgvReceta.Rows[e.RowIndex].Cells[6].Value = dbPrecioTotal_R.ToString("N6");
                    }

                    else
                    {
                        dgvReceta.Rows[e.RowIndex].Cells[1].Value = "0";
                        dgvReceta.Rows[e.RowIndex].Cells[3].Value = "0";
                        dgvReceta.Rows[e.RowIndex].Cells[6].Value = "0.00";
                    }

                    llenarValoresTexto();

                    dgvReceta.ClearSelection();
                }

                else if (dgvReceta.Columns[e.ColumnIndex].Name == "cantidad_bruta")
                {
                    if (dgvReceta.Rows[e.RowIndex].Cells[1].Value == null)
                    {
                        dgvReceta.Rows[e.RowIndex].Cells[1].Value = "";
                    }

                    string sCantidadGrid_R = dgvReceta.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();

                    if ((sCantidadGrid_R != null) && (sCantidadGrid_R != ""))
                    {
                        Decimal dbCantidadOriginal_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[1].Value.ToString().Trim());
                        Decimal dbPrecioUnitario_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[5].Value.ToString().Trim());
                        Decimal dbPorcentaje_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[2].Value.ToString().Trim());
                        Decimal dbCantidad_R = (dbCantidadOriginal_R * dbPorcentaje_R) / 100;
                        Decimal dbPrecioTotal_R = dbCantidadOriginal_R * dbPrecioUnitario_R;

                        dgvReceta.Rows[e.RowIndex].Cells[3].Value = dbCantidadOriginal_R.ToString("N2");
                        dgvReceta.Rows[e.RowIndex].Cells[6].Value = dbPrecioTotal_R.ToString("N6");
                    }

                    else
                    {
                        dgvReceta.Rows[e.RowIndex].Cells[1].Value = "0";
                        dgvReceta.Rows[e.RowIndex].Cells[3].Value = "0";
                        dgvReceta.Rows[e.RowIndex].Cells[6].Value = "0.00";
                    }

                    llenarValoresTexto();

                    dgvReceta.ClearSelection();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void dgvReceta_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox texto = e.Control as TextBox;

            if (texto != null)
            {
                DataGridViewTextBoxEditingControl dTexto = (DataGridViewTextBoxEditingControl)e.Control;
                dTexto.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
                dTexto.KeyPress += new KeyPressEventHandler(dText_KeyPress);
            }
        }

        private void txtPesoGramos_Leave(object sender, EventArgs e)
        {
            if (txtPesoGramos.Text.Trim() == "")
            {
                txtPesoGramos.Text = "0";
            }

            else if (Convert.ToDecimal(txtPesoGramos.Text.Trim()) > Convert.ToDecimal(txtCantidadNetaGramos.Text.Trim()))
            {
                Decimal dbDiferenciaPesos = Convert.ToDecimal(txtPesoGramos.Text.Trim()) - Convert.ToDecimal(txtCantidadNetaGramos.Text.Trim());
                Decimal dbPorcentaje = (dbDiferenciaPesos * 100) / Convert.ToDecimal(txtCantidadNetaGramos.Text.Trim());

                ok = new VentanasMensajes.frmMensajeNuevoOk();
                //ok.lblMensaje.Text = "Información:" + Environment.NewLine + "El peso total en gramos no puede ser superior a la cantidad neta ingresada en la receta.";
                ok.lblMensaje.Text = "El peso total en gramos supera en el " + dbPorcentaje.ToString("N2") + "% a la cantidad neta ingresada en la receta.";
                ok.ShowDialog();

                //txtPesoGramos.Text = txtCantidadNetaGramos.Text;
            }

            if (Convert.ToDecimal(txtPesoGramos.Text.Trim()) == 0)
            {
                txtCostoPorGramo.Text = "0";
            }

            else
            {
                txtCostoPorGramo.Text = (Convert.ToDecimal(txtCostoTotal.Text.Trim()) / Convert.ToDecimal(txtPesoGramos.Text.Trim())).ToString("N6");
            }
        } 
    }
}
