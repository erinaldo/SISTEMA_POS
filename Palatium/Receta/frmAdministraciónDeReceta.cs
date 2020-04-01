using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Receta
{
    public partial class frmAdministraciónDeReceta : Form
    {
        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();
                
        DataTable dtConsulta;

        bool bRespuesta;
        bool bNuevoRegistro;
        
        double dbRendimientoTotal = 0;
        double dbCostoTotalTotal = 0;
        double dbCostoUnitarioTotal = 0;
        double dbSumaRendimiento = 0;
        double dbCostUnitarioConsulta;

        string sSql;
        string sCodigoProducto;
        string sNombreProducto;
        string sTabla;
        string sCampo;
        string sFecha;
        string sCodigo_R;
        string sNombre_R;
        string sUnidad_R;
        string sPorcion_R;

        long iMaximo;

        int iIdPosReceta;
        int iIdProducto;
        int iIdReceta;

        //VARIABLES PARA LA EXTRACCION DE DETALLE DE RECETA        
        double dbCantidadBruta_R;
        double dbCantidadNeta_R;
        double dbCostoUnitario_R;
        double dbRendimiento_R;
        double dbImporte_R;
        int iIdUnidad_R;
        int iIdPosPorcion_R;
        int iIdProducto_R;
        int iUnidadControl_R;
        double dbEquivalencia_R;

        //Método constructor
        public frmAdministraciónDeReceta()
        {
            InitializeComponent();
        }

        #region Region para validar el ingreso de números en un textbox

        //Función para validar que en un textbox solo se pueda ingresar números
        private void validarIngresoDeNumeros(Object sender, KeyPressEventArgs e)
        {
            try
            {
                caracter.soloNumeros(e);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void validarIngresoDecimales(object sender, KeyPressEventArgs e)
        {
            try
            {
                caracter.soloDecimales(sender, e, 2);
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

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
                sSql += "select * from pos_temperatura_de_servicio" + Environment.NewLine;
                sSql += "where estado = 'A' ";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        cmbTemperaturaDeServicio.llenar(dtConsulta, sSql);
                    }
                }

                else
                {
                    ok.lblMensaje.Text = "Ocurrió un problema al cargar el combo de temperatura.";
                    ok.ShowDialog();
                }
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

                dtConsulta = new DataTable();
                dtConsulta.Clear();
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
            deshabilitarCajasTexto();
            bNuevoRegistro = true;
            dgvReceta.Rows.Clear();
            txtCostoTotal.Text = "";
            txtCostoUnitario.Text = "";
            txtPorcentajeDeCosto.Text = "";
            txtPorcentajeDeUtilidad.Text = "";
            txtPrecioDeVenta.Text = "";
            txtRendimiento.Text = "";
            txtUtilidadDeGanancias.Text = "";
            txtUtilidadDeServicios.Text = "";
            txtNumeroPorciones.Text = "";
            txtDescripcion.Text = "";
            txtCodigo.Text = "";
            dbAyudaReceta.txtIdentificacion.Text = "";
            dbAyudaReceta.txtDatos.Text = "";
            cmbClasificacion.SelectedIndex = 0;
            cmbOrigen.SelectedIndex = 0;
            cmbReceta.SelectedIndex = 0;
            cmbTemperaturaDeServicio.SelectedIndex = 0;
            btnA.Enabled = false;
            btnX.Enabled = false;
            dgvReceta.ReadOnly = true;
        }

        //Función para Habilitar los controles
        private void habilitarControles()
        {
            btnA.Enabled = true;
            btnX.Enabled = true;
            dgvReceta.ReadOnly = false;
        }

        //Función para habilitar las cajas de texto
        private void habilitarCajasTexto()
        {
            txtRendimiento.Enabled = true;
            txtCostoTotal.Enabled = true;
            txtCostoUnitario.Enabled = true;
            txtPorcentajeDeCosto.Enabled = true;
            txtUtilidadDeGanancias.Enabled = true;
            txtUtilidadDeServicios.Enabled = true;
            txtPorcentajeDeUtilidad.Enabled = true;
        }

        //Función para deshabilitar las cajas de texto
        private void deshabilitarCajasTexto()
        {
            txtRendimiento.Enabled = false;
            txtCostoTotal.Enabled = false;
            txtCostoUnitario.Enabled = false;
            txtPorcentajeDeCosto.Enabled = false;
            txtUtilidadDeGanancias.Enabled = false;
            txtUtilidadDeServicios.Enabled = false;
            txtPorcentajeDeUtilidad.Enabled = false;
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

            catch(Exception ex)
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
                else if (txtPrecioDeVenta.Text.Trim() == "")
                {
                    mensaje("el precio de venta");
                    txtPrecioDeVenta.Focus();
                    iBandera = 1;
                }
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

                if (iBandera == 1) return false; else return true;
            }

            catch(Exception ex)
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
                habilitarCajasTexto();
                iIdReceta = dbAyudaReceta.iId;
                habilitarControles();

                sSql = "";
                sSql += "select * from pos_receta" + Environment.NewLine;
                sSql += "where estado = 'A'" + Environment.NewLine;
                sSql += "and id_pos_receta = " + iIdReceta;

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    txtDescripcion.Text = dbAyudaReceta.txtDatos.Text;
                    txtCodigo.Text = dbAyudaReceta.txtIdentificacion.Text;
                    cmbClasificacion.SelectedValue = dtConsulta.Rows[0][3].ToString();
                    cmbReceta.SelectedValue = dtConsulta.Rows[0][2].ToString();
                    cmbOrigen.SelectedValue = dtConsulta.Rows[0][4].ToString();
                    double dbRendimiento = Convert.ToDouble(dtConsulta.Rows[0][7].ToString());
                    txtRendimiento.Text = dbRendimiento.ToString("N2");
                    txtNumeroPorciones.Text = dtConsulta.Rows[0][8].ToString();
                    cmbTemperaturaDeServicio.SelectedValue = dtConsulta.Rows[0][5].ToString();
                    double dbPrecioVenta = Convert.ToDouble(dtConsulta.Rows[0][9].ToString());
                    txtPrecioDeVenta.Text = dbPrecioVenta.ToString("N2");
                    double dbCostoUnitario = Convert.ToDouble(dtConsulta.Rows[0][10].ToString());
                    txtCostoUnitario.Text = dbCostoUnitario.ToString("N2");
                    double dbPorcentajeCosto = Convert.ToDouble(dtConsulta.Rows[0][11].ToString());
                    txtPorcentajeDeCosto.Text = dbPorcentajeCosto.ToString("N2");
                    double dbProcentajeUtilida = Convert.ToDouble(dtConsulta.Rows[0][12].ToString());
                    txtPorcentajeDeUtilidad.Text = dbProcentajeUtilida.ToString("N2");
                    double dbUtilidadServicio = Convert.ToDouble(dtConsulta.Rows[0][13].ToString());
                    txtUtilidadDeServicios.Text = dbUtilidadServicio.ToString("N2");
                    double dbUtilidadGanancia = Convert.ToDouble(dtConsulta.Rows[0][14].ToString());
                    txtUtilidadDeGanancias.Text = dbUtilidadGanancia.ToString("N2");
                    double dbCostoTotal = Convert.ToDouble(dtConsulta.Rows[0][15].ToString());
                    txtCostoTotal.Text = dbCostoTotal.ToString("N2");

                    completarDetalleReceta(iIdReceta);

                }
                else
                {
                    catchMensaje.lblMensaje.Text = sSql;
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
        private void completarDetalleReceta(int iIdReceta)
        {
            sSql = "";
            sSql += "select PROD.codigo, NP.nombre, DR.cantidad_bruta, DR.cantidad_neta," + Environment.NewLine;
            sSql += "U.descripcion descripcion_unidad, P.descripcion descripcion_porcion," + Environment.NewLine;
            sSql += "DR.costo_unitario, DR.rendimiento, DR.cantidad_bruta * DR.costo_unitario importe," + Environment.NewLine;
            sSql += "DR.id_producto, U.id_pos_unidad, DR.id_pos_porcion, DR.control_unidad, DR.equivalencia" + Environment.NewLine;
            sSql += "from pos_detalle_receta DR INNER JOIN" + Environment.NewLine;
            sSql += "pos_porcion P ON DR.id_pos_porcion = P.id_pos_porcion" + Environment.NewLine;
            sSql += "and P.estado = 'A'" + Environment.NewLine;
            sSql += "and DR.estado = 'A' INNER JOIN" + Environment.NewLine;
            sSql += "cv401_productos PROD ON DR.id_producto = PROD.id_producto" + Environment.NewLine;
            sSql += "and PROD.estado = 'A' INNER JOIN" + Environment.NewLine;
            sSql += "cv401_nombre_productos NP ON NP.id_producto = PROD.id_producto" + Environment.NewLine;
            sSql += "and NP.estado = 'A' INNER JOIN" + Environment.NewLine;
            sSql += "pos_unidad U ON DR.id_pos_equivalencia = U.id_pos_unidad" + Environment.NewLine;
            sSql += "and U.estado = 'A'" + Environment.NewLine;
            sSql += "where DR.id_pos_receta = " + iIdReceta;

            DataTable dtConsulta_1 = new DataTable();
            dtConsulta_1.Clear();

            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta_1, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta_1.Rows.Count > 0)
                {
                    for (int i = 0; i < dtConsulta_1.Rows.Count; i++)
                    {
                        sCodigo_R = dtConsulta_1.Rows[i][0].ToString();
                        sNombre_R = dtConsulta_1.Rows[i][1].ToString();
                        dbCantidadBruta_R = Convert.ToDouble(dtConsulta_1.Rows[i][2].ToString());
                        dbCantidadNeta_R = Convert.ToDouble(dtConsulta_1.Rows[i][3].ToString());
                        sUnidad_R = dtConsulta_1.Rows[i][4].ToString();
                        sPorcion_R = dtConsulta_1.Rows[i][5].ToString();
                        dbCostoUnitario_R = Convert.ToDouble(dtConsulta_1.Rows[i][6].ToString());
                        dbRendimiento_R = Convert.ToDouble(dtConsulta_1.Rows[i][7].ToString());
                        dbImporte_R = Convert.ToDouble(dtConsulta_1.Rows[i][8].ToString());
                        iIdProducto_R = Convert.ToInt32(dtConsulta_1.Rows[i][9].ToString());
                        iIdUnidad_R = Convert.ToInt32(dtConsulta_1.Rows[i][10].ToString());
                        iIdPosPorcion_R = Convert.ToInt32(dtConsulta_1.Rows[i][11].ToString());

                        //string [] sNombre = getNombreProducto(iIdProducto);

                        i = dgvReceta.Rows.Add();

                        //cargarCombos(1, i, iIdUnidad, iIdPosPorcion);
                        dgvReceta.Rows[i].Cells[0].Value = sCodigo_R;
                        dgvReceta.Rows[i].Cells[1].Value = sNombre_R;
                        dgvReceta.Rows[i].Cells[2].Value = dbCantidadBruta_R.ToString("N3"); ;
                        dgvReceta.Rows[i].Cells[3].Value = dbCantidadNeta_R.ToString("N3");
                        dgvReceta.Rows[i].Cells[4].Value = sUnidad_R;
                        dgvReceta.Rows[i].Cells[5].Value = sPorcion_R;
                        dgvReceta.Rows[i].Cells[6].Value = dbCostoUnitario_R.ToString("N5");
                        dgvReceta.Rows[i].Cells[7].Value = dbRendimiento_R.ToString("N2");
                        dgvReceta.Rows[i].Cells[8].Value = dbImporte_R.ToString("N2");
                        dgvReceta.Rows[i].Cells[9].Value = iIdProducto_R.ToString();
                        dgvReceta.Rows[i].Cells[10].Value = iIdUnidad_R.ToString();
                        dgvReceta.Rows[i].Cells[11].Value = iIdPosPorcion_R.ToString();
                        dgvReceta.Rows[i].Cells[12].Value = dtConsulta_1.Rows[i][12].ToString();
                        dgvReceta.Rows[i].Cells[13].Value = dtConsulta_1.Rows[i][13].ToString();
                        dgvReceta.Rows[i].Cells[14].Value = dbCostoUnitario_R.ToString();
                    }

                    dgvReceta.ClearSelection();
                }
            }
        }

        //Función para grabar un Registro
        private void grabarRegistro(int ibandera)
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar transacción.";
                    ok.ShowDialog();
                    return;
                }

                llenarValoresTexto();

                sSql = "";
                sSql += "insert into pos_receta(" + Environment.NewLine;
                sSql += "idempresa, id_pos_tipo_receta, id_pos_clasificacion_receta," + Environment.NewLine;
                sSql += "id_pos_origen_receta, id_pos_temperatura_de_servicio, descripcion," + Environment.NewLine;
                sSql += "rendimiento, num_porciones, precio_de_venta, costo_unitario," + Environment.NewLine;
                sSql += "porcentaje_costo, porcentaje_utilidad, utilidad_de_servicios," + Environment.NewLine;
                sSql += "utilidad_de_ganancias, costo_total, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso, codigo)" + Environment.NewLine;
                sSql += "values (" + Environment.NewLine;
                sSql += Program.iIdEmpresa + "," + cmbReceta.SelectedValue + "," + cmbClasificacion.SelectedValue + "," + Environment.NewLine;
                sSql += cmbOrigen.SelectedValue + "," + cmbTemperaturaDeServicio.SelectedValue + "," + Environment.NewLine;
                sSql += "'" + txtDescripcion.Text + "'," + txtRendimiento.Text + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtNumeroPorciones.Text) + "," + txtPrecioDeVenta.Text + "," + Environment.NewLine;
                sSql += txtCostoUnitario.Text + "," + txtPorcentajeDeCosto.Text + "," + Environment.NewLine;
                sSql += txtPorcentajeDeUtilidad.Text + "," + txtUtilidadDeServicios.Text + "," + Environment.NewLine;
                sSql += txtUtilidadDeGanancias.Text + "," + txtCostoTotal.Text + ", 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "','" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "'" + txtCodigo.Text + "' )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = sSql;
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
                    iIdProducto_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[9].Value);
                    dbCantidadBruta_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[2].Value);
                    dbCantidadNeta_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[3].Value);
                    dbRendimiento_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[7].Value);
                    iIdUnidad_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[10].Value);
                    iIdPosPorcion_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[11].Value);
                    iUnidadControl_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[12].Value);
                    dbEquivalencia_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[13].Value);
                    dbCostoUnitario_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[14].Value);

                    if (dbEquivalencia_R == 0)
                    {
                        dbEquivalencia_R = 1;
                    }
                    
                    sSql = "";
                    sSql += "insert into pos_detalle_receta (" + Environment.NewLine;
                    sSql += "id_pos_receta, id_producto, especificacion, cantidad_bruta," + Environment.NewLine;
                    sSql += "cantidad_neta, id_pos_equivalencia, id_pos_porcion, costo_unitario," + Environment.NewLine;
                    sSql += "rendimiento, control_unidad, equivalencia, estado, fecha_ingreso," + Environment.NewLine;
                    sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values (" + Environment.NewLine;
                    sSql += iIdReceta + ", " + iIdProducto_R + ", '' , " + dbCantidadBruta_R + "," + Environment.NewLine;
                    sSql += dbCantidadNeta_R + ", " + iIdUnidad_R + ", " + iIdPosPorcion_R + "," + Environment.NewLine;
                    sSql += dbCostoUnitario_R + ", " + dbRendimiento_R + ", " + iUnidadControl_R + "," + Environment.NewLine;
                    sSql += dbEquivalencia_R + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro Guardado Correctamente.";
                ok.ShowDialog();
                limpiarCampos();
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
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al iniciar la transacción.";
                    ok.ShowDialog();
                    return;
                }
                else
                {
                    llenarValoresTexto();
                    sFecha = Program.sFechaSistema.ToString("yyyy/MM/dd");

                    sSql = "";
                    sSql += "update pos_receta set" + Environment.NewLine;
                    sSql += "idempresa = " + Program.iIdEmpresa + "," + Environment.NewLine;
                    sSql += "codigo = '" + txtCodigo.Text + "'," + Environment.NewLine;
                    sSql += "descripcion = '" + txtDescripcion.Text + "'," + Environment.NewLine;
                    sSql += "id_pos_tipo_receta = " + cmbReceta.SelectedValue + "," + Environment.NewLine;
                    sSql += "id_pos_clasificacion_receta = " + cmbClasificacion.SelectedValue + "," + Environment.NewLine;
                    sSql += "id_pos_origen_receta = " + cmbOrigen.SelectedValue + "," + Environment.NewLine;
                    sSql += "id_pos_temperatura_de_servicio = " + cmbTemperaturaDeServicio.SelectedValue + "," + Environment.NewLine;
                    sSql += "rendimiento = " + txtRendimiento.Text + "," + Environment.NewLine;
                    sSql += "num_porciones = " + txtNumeroPorciones.Text + "," + Environment.NewLine;
                    sSql += "precio_De_venta = " + txtPrecioDeVenta.Text + "," + Environment.NewLine;
                    sSql += "costo_unitario = " + txtCostoUnitario.Text + "," + Environment.NewLine;
                    sSql += "porcentaje_costo = " + txtPorcentajeDeCosto.Text + "," + Environment.NewLine;
                    sSql += "porcentaje_utilidad = " + txtPorcentajeDeUtilidad.Text + "," + Environment.NewLine;
                    sSql += "utilidad_de_servicios = " + txtUtilidadDeServicios.Text + "," + Environment.NewLine;
                    sSql += "utilidad_de_ganancias = " + txtUtilidadDeGanancias.Text + "," + Environment.NewLine;
                    sSql += "costo_total = " + txtCostoTotal.Text + "," + Environment.NewLine;
                    sSql += "estado = 'A'," + Environment.NewLine;
                    sSql += "fecha_ingreso = '" + sFecha + "'" + Environment.NewLine;
                    sSql += "where id_pos_receta = " + iIdReceta;

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.lblMensaje.Text = sSql;
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
                        catchMensaje.lblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    for (int i = 0; i < dgvReceta.Rows.Count; i++)
                    {
                        iIdProducto_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[9].Value);
                        dbCantidadBruta_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[2].Value);
                        dbCantidadNeta_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[3].Value);
                        dbRendimiento_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[7].Value);
                        iIdUnidad_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[10].Value);
                        iIdPosPorcion_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[11].Value);
                        iUnidadControl_R = Convert.ToInt32(dgvReceta.Rows[i].Cells[12].Value);
                        dbEquivalencia_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[13].Value);
                        dbCostoUnitario_R = Convert.ToDouble(dgvReceta.Rows[i].Cells[14].Value);

                        if (dbEquivalencia_R == 0)
                        {
                            dbEquivalencia_R = 1;
                        }

                        sSql = "";
                        sSql += "insert into pos_detalle_receta (" + Environment.NewLine;
                        sSql += "id_pos_receta, id_producto, especificacion, cantidad_bruta," + Environment.NewLine;
                        sSql += "cantidad_neta, id_pos_equivalencia, id_pos_porcion, costo_unitario," + Environment.NewLine;
                        sSql += "rendimiento, control_unidad, equivalencia, estado, fecha_ingreso," + Environment.NewLine;
                        sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                        sSql += "values (" + Environment.NewLine;
                        sSql += iIdReceta + ", " + iIdProducto_R + ", '' , " + dbCantidadBruta_R + "," + Environment.NewLine;
                        sSql += dbCantidadNeta_R + ", " + iIdUnidad_R + ", " + iIdPosPorcion_R + "," + Environment.NewLine;
                        sSql += dbCostoUnitario_R + ", " + dbRendimiento_R + ", " + iUnidadControl_R + "," + Environment.NewLine;
                        sSql += dbEquivalencia_R + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.lblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.lblMensaje.Text = "Registro Actualizado Éxitosamente.";
                    ok.ShowDialog();
                    limpiarCampos();
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
                    catchMensaje.lblMensaje.Text = sSql;
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
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                if (ibandera != 1)
                {
                    ok.lblMensaje.Text = "Registro Eliminado Correctamente.";
                    ok.ShowDialog();
                    limpiarCampos();
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

                    for (int i = 0; i < dgvReceta.Rows.Count; i++)
                    {
                        dbCostoTotalTotal += Convert.ToDouble(dgvReceta.Rows[i].Cells[8].Value.ToString());
                        dbCostoUnitarioTotal = dbCostoTotalTotal / Convert.ToDouble(txtNumeroPorciones.Text);

                        double dbCantidadBruta = Convert.ToDouble(dgvReceta.Rows[i].Cells[2].Value.ToString());
                        double dbCantidadNeta = Convert.ToDouble(dgvReceta.Rows[i].Cells[3].Value.ToString());
                        double dbRendimiento;

                        if ((dbCantidadBruta == 0) && (dbCantidadNeta == 0))
                        {
                            dbRendimiento = 0;
                        }

                        else if ((dbCantidadBruta == 0) && (dbCantidadNeta != 0))
                        {
                            dbRendimiento = 0;
                        }

                        else
                        {
                            dbRendimiento = (dbCantidadNeta / dbCantidadBruta);
                        }

                        dgvReceta.Rows[i].Cells[7].Value = dbRendimiento.ToString("N2");

                        dbSumaRendimiento += Convert.ToDouble(dgvReceta.Rows[i].Cells[7].Value.ToString());
                    }

                    txtCostoTotal.Text = dbCostoTotalTotal.ToString("N2");
                    txtCostoUnitario.Text = dbCostoUnitarioTotal.ToString("N2");
                    txtUtilidadDeServicios.Text = ((dbCostoTotalTotal * 10) / 100).ToString("N2");
                    txtUtilidadDeGanancias.Text = ((Convert.ToDouble(txtPrecioDeVenta.Text.Trim()) * 2) + Convert.ToDouble(txtPrecioDeVenta.Text.Trim())).ToString("N2");
                    txtPorcentajeDeUtilidad.Text = ((Convert.ToDouble(txtUtilidadDeServicios.Text) * 100) / Convert.ToDouble(txtPrecioDeVenta.Text)).ToString("N2");
                    txtPorcentajeDeCosto.Text = ((dbCostoUnitarioTotal / Convert.ToDouble(txtPrecioDeVenta.Text)) * 100).ToString("N2");
                    txtRendimiento.Text = dbSumaRendimiento.ToString("N2");
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
            validarIngresoDeNumeros(sender, e);
        }

        private void txtRendimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender,e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender, e);
        }

        private void txtCostoUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender, e);
        }

        private void txtPorcentajeDeCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender, e);
        }

        private void txtPorcentajeDeUtilidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender, e);
        }

        private void txtUtilidadDeServicios_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender, e);
        }

        private void txtUtilidadDeGanancias_KeyPress(object sender, KeyPressEventArgs e)
        {
            validarIngresoDeNumeros(sender, e);
        }

        private void frmAdministraciónDeReceta_Load(object sender, EventArgs e)
        {
            llenarComboEmpresa();
            llenarComboClasificacion();
            llenarComboOrigen();
            llenarComboReceta();
            llenarComboTemperatura();
            llenarSentencias();
        }
                
        private void button1_Click(object sender, EventArgs e)
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
                        grabarRegistro(0);
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

                dgvReceta.Rows.Add(ingrediente.sNombreProducto, ingrediente.sUnidadConsumo, dbPorcentaje_R.ToString("N2"),
                                   "0", dbValorUnitario_R.ToString(), "0.00");

                ingrediente.Close();
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            try
            {
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
        
        private void dgvReceta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int f = dgvReceta.CurrentRow.Index;
            int iControlUnidad_P = Convert.ToInt32(dgvReceta.CurrentRow.Cells[12].Value.ToString());
            int iUnidad_P = Convert.ToInt32(dgvReceta.CurrentRow.Cells[10].Value.ToString());
            double dbEquivalencia_P = Convert.ToDouble(dgvReceta.CurrentRow.Cells[13].Value.ToString());
            double dbPrecioUnitario_P = Convert.ToDouble(dgvReceta.CurrentRow.Cells[14].Value.ToString());

            Receta.frmIngresarIngrediente ingrediente = new Receta.frmIngresarIngrediente(1, iControlUnidad_P, iUnidad_P, dbPrecioUnitario_P, dbEquivalencia_P);
            ingrediente.iniciarLimpiar();
            ingrediente.txtCantidadBruta.Text = dgvReceta.CurrentRow.Cells[2].Value.ToString();
            ingrediente.txtCantidadNeta.Text = dgvReceta.CurrentRow.Cells[3].Value.ToString();
            ingrediente.txtCostoUnitario.Text = dgvReceta.CurrentRow.Cells[6].Value.ToString();
            ingrediente.sCodigo_R = dgvReceta.CurrentRow.Cells[0].Value.ToString();
            ingrediente.sDescripcion_R = dgvReceta.CurrentRow.Cells[1].Value.ToString();
            ingrediente.iCorrelativo_R = Convert.ToInt32(dgvReceta.CurrentRow.Cells[9].Value.ToString());
            //ingrediente.iUnidad_R = Convert.ToInt32(dgvReceta.CurrentRow.Cells[10].Value.ToString());
            ingrediente.iPorcion_R = Convert.ToInt32(dgvReceta.CurrentRow.Cells[11].Value.ToString());
            ingrediente.txtRendimiento.Text = dgvReceta.CurrentRow.Cells[7].Value.ToString();
            ingrediente.txtImporteTotal.Text = dgvReceta.CurrentRow.Cells[8].Value.ToString();
            //ingrediente.iUnidadControl = Convert.ToInt32(dgvReceta.CurrentRow.Cells[12].Value.ToString());

            ingrediente.ShowDialog();

            if (ingrediente.DialogResult == DialogResult.OK)
            {
                dgvReceta.Rows[f].Cells[0].Value = ingrediente.sCodigo_R;
                dgvReceta.Rows[f].Cells[1].Value = ingrediente.sDescripcion_R;
                dgvReceta.Rows[f].Cells[2].Value = ingrediente.txtCantidadBruta.Text.Trim();
                dgvReceta.Rows[f].Cells[3].Value = ingrediente.txtCantidadNeta.Text.Trim();
                dgvReceta.Rows[f].Cells[4].Value = ingrediente.sUnidadProductoReceta;
                dgvReceta.Rows[f].Cells[5].Value = ingrediente.cmbPorciones.Text.Trim();
                dgvReceta.Rows[f].Cells[6].Value = ingrediente.txtCostoUnitario.Text.Trim();
                dgvReceta.Rows[f].Cells[7].Value = ingrediente.txtRendimiento.Text.Trim();
                dgvReceta.Rows[f].Cells[8].Value = ingrediente.txtImporteTotal.Text.Trim();
                dgvReceta.Rows[f].Cells[9].Value = ingrediente.iCorrelativo_R.ToString();
                dgvReceta.Rows[f].Cells[10].Value = ingrediente.iUnidadProductoReceta.ToString();
                dgvReceta.Rows[f].Cells[11].Value = ingrediente.cmbPorciones.SelectedValue;
                dgvReceta.Rows[f].Cells[12].Value = ingrediente.iUnidadControl.ToString();
                dgvReceta.Rows[f].Cells[13].Value = ingrediente.dbEquivalencia_DC.ToString();
                dgvReceta.Rows[f].Cells[14].Value = ingrediente.dbPrecioUnitario_P;

                ingrediente.Close();
                llenarValoresTexto();
                dgvReceta.ClearSelection();
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

        private void dgvReceta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvReceta.Columns[e.ColumnIndex].Name == "cantidad")
                {
                    if (dgvReceta.Rows[e.RowIndex].Cells[3].Value == null)
                    {
                        dgvReceta.Rows[e.RowIndex].Cells[3].Value = "";
                    }

                    string sCantidadGrid_R = dgvReceta.Rows[e.RowIndex].Cells[3].Value.ToString().Trim();

                    if ((sCantidadGrid_R != null) && (sCantidadGrid_R != ""))
                    {
                        Decimal dbCantidad_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
                        Decimal dbPrecioUnitario_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[4].Value.ToString().Trim());
                        Decimal dbPorcentaje_R = Convert.ToDecimal(dgvReceta.Rows[e.RowIndex].Cells[2].Value.ToString().Trim());
                        Decimal dbCantidadOriginal_R = (dbCantidad_R * 100) / dbPorcentaje_R;
                        Decimal dbPrecioTotal_R = dbCantidadOriginal_R * dbPrecioUnitario_R;

                        dgvReceta.Rows[e.RowIndex].Cells[5].Value = dbPrecioTotal_R.ToString("N6");
                    }

                    else
                    {
                        dgvReceta.Rows[e.RowIndex].Cells[3].Value = "0";
                        dgvReceta.Rows[e.RowIndex].Cells[5].Value = "0.00";
                    }
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
    }
}
