using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Bodega
{
    public partial class frmTransferenciaDeProductos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        string sSql;
        bool bRespuesta;
        DataTable dtConsulta = new DataTable();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        bool bNuevo = true;
        string sCodigoProducto;
        int iIdProducto;
        string sNumeroMovimiento, sReferenciaExterna, sObservacion, sFecha;
        int iIdMovimientoBodega;
       
        string sTabla;
        string sCampo;
        long iMaximo;

        //Método Constructor
        public frmTransferenciaDeProductos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //El método necesita recibir como parámetros el tipo de movimiento, Id de la bodega,
        //el año y el mes
        public string devuelveCorrelativo(string sTipoMovimiento, int iIdBodega, string sAño, string sMes, string sCodigoCorrelativo)
        {

            double dbValorActual = 0;
            string sCodigo = "";
            string sAñoCorto = sAño.Substring(2, 2);
            string sMesCorto;
            if (sMes.Substring(0, 1) == "0")
                sMesCorto = sMes.Substring(1, 1);
            else
                sMesCorto = sMes;

            sSql = "";
            sSql += "select codigo from cv402_bodegas" + Environment.NewLine;
            sSql += "where id_bodega = " + iIdBodega;

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    sCodigo = dtConsulta.Rows[0].ItemArray[0].ToString();
                }
            }
            else
                return "Error";

            string sReferencia;

            sReferencia = sTipoMovimiento + sCodigo + "_" + sAño + "_" + sMesCorto + "_" + Program.iCgEmpresa;

            sSql = "";
            sSql += "select valor_actual from tp_correlativos" + Environment.NewLine;
            sSql += "where referencia = '" + sReferencia + "'"+ Environment.NewLine;
            sSql += "and codigo_correlativo = '" + sCodigoCorrelativo + "'";

            dtConsulta = new DataTable();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    dbValorActual = Convert.ToDouble(dtConsulta.Rows[0].ItemArray[0].ToString());

                    sSql = "";
                    sSql += "update tp_correlativos set" + Environment.NewLine;
                    sSql += "valor_actual =  " + (dbValorActual + 1) + Environment.NewLine;
                    sSql += "where referencia = '" + sReferencia + "'";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        return "Error";
                    }

                    return sTipoMovimiento + sCodigo + sAñoCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');

                }
                else
                {
                    int iCorrelativo = 4979;
                    dbValorActual = 1;

                    sSql = "";
                    sSql += "select correlativo from tp_codigos" + Environment.NewLine;
                    sSql += "where codigo = 'BD' and tabla = 'SYS$00022'";

                    dtConsulta = new DataTable();
                    dtConsulta.Clear();
                    bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);
                    if (bRespuesta == true)
                    {
                        if (dtConsulta.Rows.Count > 0)
                        {
                            iCorrelativo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                        }
                    }
                    else
                        return "Error";

                    string sFechaDesde = sAño + "-01-01";
                    string sFechaHasta = sAño + "-12-31";
                    string sValido_desde = Convert.ToDateTime(sFechaDesde).ToString("yyyy-MM-dd");
                    string sValido_hasta = Convert.ToDateTime(sFechaHasta).ToString("yyyy-MM-dd");

                    sSql = "";
                    sSql += "insert into tp_correlativos (" + Environment.NewLine;
                    sSql += "cg_sistema, codigo_correlativo, referencia, valido_desde, valido_hasta," + Environment.NewLine;
                    sSql += "valor_actual, desde, hasta, estado, origen_dato, numero_replica_trigger," + Environment.NewLine;
                    sSql += "estado_replica, numero_control_replica)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += iCorrelativo + ", '" + sCodigoCorrelativo + "', '" + sReferencia + "', '" + sFechaDesde + "'," + Environment.NewLine;
                    sSql += "'" + sFechaHasta + "', " + (dbValorActual + 1) + ", 0, 0, 'A', 1," + Environment.NewLine;
                    sSql += (dbValorActual + 1).ToString("N0") + ", 0, 0)";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        return "Error";
                    }

                    return sTipoMovimiento + sCodigo + sAñoCorto + sMes + dbValorActual.ToString("N0").PadLeft(4, '0');
                }
            }
            else
            {
                return "Error";
            }
        }

        #endregion

        //Función cuando cargar el formulario
        private void frmTransferenciaDeProductos_Load(object sender, EventArgs e)
        {
            sFecha = Program.sFechaSistema.ToString("dd/MM/yyyy");
            txtFechaAplicacion.Text = sFecha;
            cargarComboOficina();
            cargarComboOficina2();
            cargarComboBodega();
            cargarComboBodega2();
            bloquearControles();
            cargarComboMotivos();
        }

        //Función para cargar el combo oficina 1 
        private void cargarComboOficina()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where codigo in (00004,00005,00006)";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbOficina.llenar(dtConsulta, sSql);
                    cmbOficina.SelectedIndex = -1;
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo oficina  2
        private void cargarComboOficina2()
        {
            try
            {
                sSql = "";
                sSql += "select id_localidad, nombre_localidad" + Environment.NewLine;
                sSql += "from tp_vw_localidades" + Environment.NewLine;
                sSql += "where codigo in (00004,00005,00006)";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbOficina2.llenar(dtConsulta, sSql);
                    cmbOficina2.SelectedIndex = -1;
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo de bodega
        private void cargarComboBodega()
        {
            try
            {
                sSql = "";
                sSql += "select id_bodega, descripcion" + Environment.NewLine;
                sSql += "from cv402_bodegas" + Environment.NewLine;
                sSql += "where categoria = 1";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.llenar(dtConsulta, sSql);
                    cmbBodega.SelectedItem = -1;
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo de bodega 2
        private void cargarComboBodega2()
        {
            try
            {
                sSql = "";
                sSql += "select id_bodega, descripcion" + Environment.NewLine;
                sSql += "from cv402_bodegas" + Environment.NewLine;
                sSql += "where categoria = 1";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega2.llenar(dtConsulta, sSql);
                    cmbBodega2.SelectedItem = -1;
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        //Función para cargar el combo de motivos
        private void cargarComboMotivos()
        {
            try
            {
                sSql = "";
                sSql += "SELECT C.Correlativo, C.Valor_Texto, C.Valor_Fecha, C.Valor_Numero," + Environment.NewLine;
                sSql += "C.Tabla, C.Valor_Texto Descripcion" + Environment.NewLine;
                sSql += "from tp_codigos C1, tp_codigos C, tp_relaciones R" + Environment.NewLine;
                sSql += "where C.Tabla = R.Tabla_Contenida" + Environment.NewLine;
                sSql += "and C.Codigo = R.Codigo_Contenido" + Environment.NewLine;
                sSql += "and R.CG_Tipo_Relacion = C1.Correlativo" + Environment.NewLine;
                sSql += "and C.Tabla = 'SYS$00643'" + Environment.NewLine; 
                sSql += "and R.Tabla_Contenedora = 'SYS$00648'" + Environment.NewLine;
                sSql += "and R.Codigo_Contenedor = 'TEM'" + Environment.NewLine;
                sSql += "and C1.Codigo = '1'" + Environment.NewLine;
                sSql += "and C1.Estado = 'A'" + Environment.NewLine;
                sSql += "and C.Estado = 'A'" + Environment.NewLine;
                sSql += "and R.Estado = 'A'" + Environment.NewLine;
                sSql += "order by C.Valor_Texto ";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbMotivos.llenar(dtConsulta, sSql);
                    cmbMotivos.SelectedIndex = -1;
                }
                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                    ok.ShowDialog();
                }

            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Ocurrió un problema al cargaar el combo de oficina";
                ok.ShowDialog();
            }
        }

        //Función para cuando cambie el index del combo oficina
        private void cmbOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOficina.SelectedIndex > 0)
            {
                sSql = "";
                sSql += "select id_bodega from tp_vw_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Convert.ToInt32(cmbOficina.SelectedValue);

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega.SelectedValue = dtConsulta.Rows[0].ItemArray[0];
                }
            }
        }

        //Función para cuando cambie el index del combo oficina 2
        private void cmbOficina2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbOficina2.SelectedIndex > 0)
            {
                sSql = "";
                sSql += "select id_bodega from tp_vw_localidades" + Environment.NewLine;
                sSql += "where id_localidad = " + Convert.ToInt32(cmbOficina2.SelectedValue);

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    cmbBodega2.SelectedValue = dtConsulta.Rows[0].ItemArray[0];
                }
            }
        }

        //Función para bloquear controles
        private void bloquearControles()
        {
            dgvDetalleVenta.ReadOnly = true;
            btnX.Enabled = false;
            btnA.Enabled = false;
        }

        //Función para activar controles
        private void activarControles()
        {
            dgvDetalleVenta.ReadOnly = false;
            btnX.Enabled = true;
            btnA.Enabled = true;
        }

        //Función para limpiar los campos
        private void limpiarCampos()
        {
            cmbOficina.SelectedIndex = -1;
            cmbOficina2.SelectedIndex = -1;
            cmbBodega.SelectedIndex = -1;
            cmbBodega2.SelectedIndex = -1;
            txtNumeroTraslado.Text = "";
            txtTraslado.Text = "";
            cmbMotivos.SelectedIndex = -1;
            txtReferencia.Text = "";
            txtComentarios.Text = "";
            txtNumeroRecepcion.Text = "";
            bloquearControles();
            dgvDetalleVenta.Rows.Clear();
            bNuevo = true;
        }

        //Función para el botón ok
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (bNuevo == true)
            {
                if (comprobarCampos() == true)
                {
                    activarControles();
                    if (txtReferencia.Text == "")
                        txtReferencia.Text = cmbMotivos.Text;
                }
                else
                    bloquearControles();
            }
            else
            {
                llenarCampos();
                activarControles();
            }
        }

        //Función para calcular los campos
        private bool comprobarCampos()
        { 
            int iBandera=0;
            if (Convert.ToInt32(cmbBodega.SelectedValue) == 0)
            {
                MessageBox.Show("Advertencia: Debe seleccionar la oficina/local","Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iBandera = 1;
            }
            else if (Convert.ToInt32(cmbOficina2.SelectedValue) == 0)
            {
                MessageBox.Show("Advertencia: Debe seleccionar la oficina/local Destino", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iBandera = 1;
            }
            else if (Convert.ToInt32(cmbOficina2.SelectedValue) == Convert.ToInt32(cmbOficina.SelectedValue))
            {
                MessageBox.Show("Advertencia: Debe transferir a una bodega distinta de la actual", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iBandera = 1;
            }
            else if (Convert.ToInt32(cmbMotivos.SelectedValue) == 0)
            {
                MessageBox.Show("Advertencia: Debe seleccionar el Motivo del Movimiento", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                iBandera = 1;
            }
                

            if(iBandera == 1)
                return false;
            else
                return true;

        }

        //Función cuando se aplasta el botón Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea limpiar..?", "Bodega", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                limpiarCampos();
        }

        //Función para cuando se aplasta el botón A
        private void btnA_Click(object sender, EventArgs e)
        {
            dgvDetalleVenta.Rows.Add("", "?");
        }

        //Función para cuando se aplasta el botón X
        private void btnX_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Desea eliminar la línea...?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    dgvDetalleVenta.Rows.Remove(dgvDetalleVenta.CurrentRow);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No hay Productos para eliminar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDetalleVenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (e.ColumnIndex == senderGrid.Columns[1].Index && e.RowIndex >= 0)
            {
                abrirDbAyuda();
            }
        }

        //Función para abrir el dbAyuda cuando se da clic en el botón del datagridview
        private void abrirDbAyuda()
        {
            Bodega.frmAyudaProductos ayuda = new frmAyudaProductos();
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                sCodigoProducto = ayuda.sCodigo;
                if (comprobarCodigoRepetido(sCodigoProducto) == true)
                {
                    iIdProducto = Convert.ToInt32(ayuda.sIdProducto);
                    llenarFilaGrid();
                }
                else
                    MessageBox.Show("El código del producto está repetido","Bodega",MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
        }

        //Función para verificar si el código del producto está repetido
        private bool comprobarCodigoRepetido(string sCodigoProducto)
        { 
            int iBandera=0;
            if (dgvDetalleVenta.Rows.Count > 0)
            {
                for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                {
                    if (dgvDetalleVenta.Rows[i].Cells[0].Value.ToString() == sCodigoProducto)
                    {
                        iBandera = 1;
                        break;
                    }
                }
            }

            if (iBandera == 1)
                return false;
            else
                return true;
        }

        //Función para llenar cada fila del datagridview
        private void llenarFilaGrid()
        {
            try
            {
                string sfecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyy/MM/dd");

                sSql = "";
                sSql += "select Codigo, Descripcion, Unidad_Compra, ID_Unidad_Compra, Correlativo " + Environment.NewLine;
                sSql += "from cv401_vw_productos" + Environment.NewLine;
                sSql += "where Codigo = '" + sCodigoProducto + "'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        int iIdProducto = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[4].ToString());
                        dgvDetalleVenta.CurrentRow.Cells[0].Value = dtConsulta.Rows[0].ItemArray[0].ToString();
                        dgvDetalleVenta.CurrentRow.Cells[2].Value = dtConsulta.Rows[0].ItemArray[1].ToString();
                        dgvDetalleVenta.CurrentRow.Cells[4].Value = dtConsulta.Rows[0].ItemArray[2].ToString();
                        dgvDetalleVenta.CurrentRow.Cells[8].Value = dtConsulta.Rows[0].ItemArray[3].ToString();
                        dgvDetalleVenta.CurrentRow.Cells[7].Value = dtConsulta.Rows[0].ItemArray[4].ToString();

                        sSql = "";
                        sSql += "select Sum(M.CANTIDAD) Saldo " + Environment.NewLine;
                        sSql += "from cv402_movimientos_bodega M, cv402_cabecera_movimientos C" + Environment.NewLine;
                        sSql += "where C.id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + Environment.NewLine;
                        sSql += "and M.id_producto = " + iIdProducto + Environment.NewLine;
                        sSql += "and C.Fecha <= Convert(DateTime, '" + sfecha + "', 120)" + Environment.NewLine;
                        sSql += "and C.estado = 'A'" + Environment.NewLine;
                        sSql += "and M.estado = 'A'" + Environment.NewLine;
                        sSql += "and M.correlativo+0 = M.correlativo+0" + Environment.NewLine;
                        sSql += "and C.ID_Movimiento_Bodega = M.ID_Movimiento_Bodega " + Environment.NewLine;
                        sSql += "and C.cg_empresa = " + Program.iCgEmpresa;

                        dtConsulta = new DataTable();
                        dtConsulta.Clear();
                        bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                        if (bRespuesta == true)
                        { 
                            if(dtConsulta.Rows.Count>0)
                                dgvDetalleVenta.CurrentRow.Cells[6].Value = dtConsulta.Rows[0].ItemArray[0].ToString();  
                        }

                        dgvDetalleVenta.CurrentCell = dgvDetalleVenta.CurrentRow.Cells[5]; 

                    }
                }
                else
                    MessageBox.Show("Ocurrió un problema en la sentencia Sql", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al cargar la fila del datagridview","Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Función cuando se aplasta en el botón Grabar
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (dgvDetalleVenta.Rows.Count > 0)
            {
                if (comprobarCamposGrid() == true)
                {
                    if (MessageBox.Show("Desea Grabar..?", "Bodega", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    { 
                        if (bNuevo == true)
                            grabarRegistro(1);
                        else
                            actualizarRegistro(0);
                    }
                }
                else
                    MessageBox.Show("Debe ingresar la cantidad", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Todos los valores son cero. Por lo menos uno debe ser diferente de Cero", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }

        //Función para ver si el campo de cantidad del grid está lleno
        private bool comprobarCamposGrid()
        {
            int iBandera = 0;

            for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
            {
                if (dgvDetalleVenta.Rows[i].Cells[5].Value == null || dgvDetalleVenta.Rows[i].Cells[5].Value.ToString() == "")
                    iBandera = 1;
            }

                if (iBandera == 1)
                    return false;
                else
                    return true;
        }

        //Función para grabar un registro
        private void grabarRegistro(int iBandera)
        {
            try
            {
                string sfecha = Convert.ToDateTime(txtFechaAplicacion.Text).ToString("yyy/MM/dd");
                int iCgTipoMovimiento = buscarTipoMovimiento();

                if (iCgTipoMovimiento != 0)
                {

                    //INICIAMOS UNA NUEVA TRANSACCION

                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok.LblMensaje.Text = "Error al abrir transacción";
                        ok.ShowDialog();
                        goto fin;
                    }

                    string sNumeroMovimiento;

                    if (iBandera == 1)
                    {
                        string sAño = txtFechaAplicacion.Text.Substring(6, 4);
                        string sMes = txtFechaAplicacion.Text.Substring(3, 2);
                        int iIdBodega = Convert.ToInt32(cmbBodega.SelectedValue);

                        //Clases.ObtenerNumeroMovimiento movimiento = new Clases.ObtenerNumeroMovimiento();
                        sNumeroMovimiento = devuelveCorrelativo("TE", iIdBodega, sAño, sMes, "MOV");
                    }
                    else
                        sNumeroMovimiento = txtNumeroTraslado.Text;

                    sSql = "";
                    sSql += "Insert Into cv402_cabecera_movimientos (" + Environment.NewLine;
                    sSql += "CG_EMPRESA, ID_BODEGA, CG_TIPO_MOVIMIENTO, CG_MOTIVO_MOVIMIENTO_BODEGA," + Environment.NewLine;
                    sSql += "CG_CLIENTE_PROVEEDOR, ID_LOCALIDAD, Fecha, CG_MONEDA_BASE, REFERENCIA_EXTERNA," + Environment.NewLine;
                    sSql += "OBSERVACION, NUMERO_MOVIMIENTO, ID_BODEGA_DESTINO, ID_LOCALIDAD_DESTINO," + Environment.NewLine;
                    sSql += "EXTERNO,usuario_ingreso, terminal_ingreso, ESTADO)" + Environment.NewLine;
                    sSql += "Values (" + Environment.NewLine;
                    sSql += Program.iCgEmpresa + ", " + Convert.ToInt32(cmbBodega.SelectedValue) + ", " + iCgTipoMovimiento + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(cmbMotivos.SelectedValue) + ", 6161," + Convert.ToInt32(cmbOficina.SelectedValue) + "," + Environment.NewLine;
                    sSql += "Convert(DateTime,'" + sfecha + "', 120), " + Program.iMoneda + ", '" + txtReferencia.Text.Trim() + "'," + Environment.NewLine;
                    sSql += "'" + txtComentarios.Text.Trim() + "', '" + sNumeroMovimiento + "', " + Convert.ToInt32(cmbBodega2.SelectedValue) + "," + Environment.NewLine;
                    sSql += Convert.ToInt32(cmbOficina2.SelectedValue) + ", 0,'" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                    sSql += "'" + Program.sDatosMaximo[1] + "', 'A')";

                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        //hara el rolBAck
                        goto reversa;
                    }

                    int NewCodigo = 0;

                    //PROCEDIMINTO PARA EXTRAER EL ID DE LA TABLA CV402_CABECERA_MOVIMIENTOS
                    dtConsulta = new DataTable();
                    dtConsulta.Clear();

                    sTabla = "cv402_cabecera_movimientos";
                    sCampo = "Id_Movimiento_Bodega"; ;

                    iMaximo = conexion.GFun_Ln_Saca_Maximo_ID(sTabla, sCampo, "", Program.sDatosMaximo);

                    if (iMaximo == -1)
                    {
                        ok.LblMensaje.Text = "No se pudo obtener el codigo de la tabla " + sTabla;
                        ok.ShowInTaskbar = false;
                        ok.ShowDialog();
                        goto reversa;
                    }

                    else
                    {
                        NewCodigo = Convert.ToInt32(iMaximo);
                    }

                    
                    for (int i = 0; i < dgvDetalleVenta.Rows.Count; i++)
                    {
                        int iIdProducto = Convert.ToInt32(dgvDetalleVenta.Rows[i].Cells[7].Value.ToString());
                        string sEspecificacion;
                        if (dgvDetalleVenta.Rows[i].Cells[3].Value != null)
                            sEspecificacion = dgvDetalleVenta.Rows[i].Cells[3].Value.ToString();
                        else
                            sEspecificacion = null;
                        double dbCantidad = Convert.ToDouble(dgvDetalleVenta.Rows[i].Cells[5].Value.ToString());
                        int iCorrelativoUnidadCompra = buscarCorrelativoUnidadCompra();

                        if (iCorrelativoUnidadCompra != 0)
                        {
                            sSql = "";
                            sSql += "Insert Into cv402_movimientos_bodega (" + Environment.NewLine;
                            sSql += "ID_PRODUCTO, ESPECIFICACION, ID_MOVIMIENTO_BODEGA," + Environment.NewLine;
                            sSql += "CG_UNIDAD_COMPRA, CANTIDAD,ESTADO)" + Environment.NewLine;
                            sSql += "values (" + Environment.NewLine;
                            sSql += iIdProducto + ", '" + sEspecificacion + "', " + NewCodigo + "," + Environment.NewLine;
                            sSql += iCorrelativoUnidadCompra + ", -" + dbCantidad.ToString() + ", 'A')";

                            if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                            {
                                //hara el rolBAck
                                goto reversa;
                            }

                        }
                        else
                            goto reversa;

                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    MessageBox.Show("Registro Guardado Correctamente");
                    goto fin;

                }
                else
                    goto reversa;

                
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro en la base de datos", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #region Funciones de ayuda
        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                MessageBox.Show("Ocurrió un problema en la transacción. No se guardarán los cambios");
            }
        fin:
            {
                 limpiarCampos();
            }
            #endregion

        }

        //Función que me retorna el Tipo de Movimiento;
        private int buscarTipoMovimiento()
        {
            int iBandera=0;
            sSql = "";
            sSql += "select correlativo from tp_codigos" + Environment.NewLine;
            sSql += "where codigo = 'TEM'" + Environment.NewLine;
            sSql += "and tabla = 'SYS$00648'";
            
            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    iBandera = 1;
                }
            }

            if (iBandera == 1)
                return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
            else
                return 0;

        }

        //Función que me retorna el correlativo de la unidad de compra
        private int buscarCorrelativoUnidadCompra()
        {
            int iBandera = 0;

            sSql = "";
            sSql += "select correlativo from tp_codigos" + Environment.NewLine;
            sSql += "where codigo = 'Und'" + Environment.NewLine;
            sSql += "and tabla = 'SYS$00042'";

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    iBandera = 1;
                }
            }

            if (iBandera == 1)
                return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
            else
                return 0;

        }

        private void btnAyudaNumeroTraslado_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbOficina.SelectedValue) != 0)
            {
                Bodega.frmAyudaTransferenciaBodega ayuda = new frmAyudaTransferenciaBodega(Convert.ToInt32(cmbBodega.SelectedValue));
                ayuda.ShowDialog();

                if (ayuda.DialogResult == DialogResult.OK)
                {
                    sNumeroMovimiento = ayuda.sNumeroMovimiento;
                    sReferenciaExterna = ayuda.sReferencia;
                    sObservacion = ayuda.sObservacion;
                    iIdMovimientoBodega = ayuda.iIdMovimientoBodega;
                    sFecha = ayuda.sFecha;
                    bNuevo = false;
                    txtNumeroTraslado.Text = sNumeroMovimiento;
                    txtTraslado.Text = sObservacion;
                }
            }
            else
                MessageBox.Show("Advertencia: Primero tiene que seleccionar una bodega","Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Information);

            
        }

        //Función para llenar las cajas de texto con la información de la base de datos
        private void llenarCampos()
        {
            try
            {
                sSql = "";
                sSql += "Select m.Fecha, m.Cg_Empresa, m.ID_BODEGA, m.CG_TIPO_MOVIMIENTO," + Environment.NewLine;
                sSql += "m.CG_MOTIVO_MOVIMIENTO_BODEGA, m.Aprobado_Por, ID_BODEGA_DESTINO," + Environment.NewLine;
                sSql += "ID_LOCALIDAD_DESTINO, ID_LOCALIDAD, m.Observacion, m.referencia_externa," + Environment.NewLine;
                sSql += "m.Numero_Transferencia_Cruzado, m.estado_replica" + Environment.NewLine;
                sSql += "from cv402_cabecera_movimientos m" + Environment.NewLine;
                sSql += "where m.Id_Movimiento_Bodega = " + iIdMovimientoBodega;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    { 
                        string sFechaDatos = dtConsulta.Rows[0].ItemArray[0].ToString();
                        int iIdBodega = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[2].ToString());
                        int iIdMotivo = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[4].ToString());
                        int iIdBodegaDestino = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[6].ToString());
                        int iIdLocalidadDestino = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[7].ToString());
                        int iIdLocalidad = Convert.ToInt32(dtConsulta.Rows[0].ItemArray[8].ToString());
                        string sObservacion = dtConsulta.Rows[0].ItemArray[9].ToString();
                        string sReferencia = dtConsulta.Rows[0].ItemArray[10].ToString();
                        txtFechaAplicacion.Text = Convert.ToDateTime(sFechaDatos).ToString("dd-MM-yyy");
                        cmbOficina.SelectedValue = iIdLocalidad;
                        cmbBodega.SelectedValue = iIdBodega;
                        cmbOficina2.SelectedValue = iIdLocalidadDestino;
                        cmbBodega2.SelectedValue = iIdBodegaDestino;
                        cmbMotivos.SelectedValue = iIdMotivo;
                        txtComentarios.Text = sObservacion;
                        txtReferencia.Text = sReferencia;

                        llenarDetalleGrid();
                    }
                }
                else
                    MessageBox.Show("Ocurrió un problema al recuperar los datos ","Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exc)
            {
                MessageBox.Show("Ocurrió un problema al recuperar los datos debido a: \n" + exc.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        //Función para llenar el detalle del grid
        private void llenarDetalleGrid()
        {
            sSql = "";
            sSql += "SELECT MB.CORRELATIVO, P.codigo codigo_producto, N.nombre producto," + Environment.NewLine;
            sSql += "MB.Id_Producto, MB.ESPECIFICACION, U.codigo unidad," + Environment.NewLine;
            sSql += "MB.cg_unidad_compra cg_unidad_compra, MB.CANTIDAD" + Environment.NewLine;
            sSql += "from cv402_movimientos_bodega MB, cv401_productos P," + Environment.NewLine;
            sSql += "tp_codigos U, cv401_nombre_productos N" + Environment.NewLine;
            sSql += "where MB.Id_Producto = P.Id_Producto" + Environment.NewLine;
            sSql += "and P.Id_Producto = N.Id_Producto" + Environment.NewLine;
            sSql += "and N.Nombre_Interno = 1" + Environment.NewLine;
            sSql += "and MB.CG_UNIDAD_COMPRA = U.Correlativo" + Environment.NewLine;
            sSql += "and MB.Id_Movimiento_Bodega = " + iIdMovimientoBodega + Environment.NewLine;
            sSql += "and MB.Estado = 'A'" + Environment.NewLine;
            sSql += "and N.Estado = 'A'" + Environment.NewLine;
            sSql += "order by MB.Correlativo";

            dtConsulta = new DataTable();
            dtConsulta.Clear();
            bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta,sSql);
            if (bRespuesta == true)
            {
                if (dtConsulta.Rows.Count > 0)
                {
                    for (int i = 0; i < dtConsulta.Rows.Count; i++)
                    {
                        string sCodigoProducto = dtConsulta.Rows[i].ItemArray[1].ToString();
                        string sDescripcion = dtConsulta.Rows[i].ItemArray[2].ToString();
                        int iIdProducto = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[3].ToString());
                        string sEspecificacion;
                        if (dtConsulta.Rows[i].ItemArray[4] == null)
                            sEspecificacion = "";
                        else
                            sEspecificacion = dtConsulta.Rows[i].ItemArray[4].ToString();
                        string sUnidad = dtConsulta.Rows[i].ItemArray[5].ToString();
                        string cgUnidadCompra = dtConsulta.Rows[i].ItemArray[6].ToString();
                        double dbCantidad = Math.Abs(Convert.ToDouble(dtConsulta.Rows[i].ItemArray[7].ToString()));

                        dgvDetalleVenta.Rows.Add(sCodigoProducto,
                                                "?",
                                                sDescripcion,
                                                sEspecificacion,
                                                sUnidad,
                                                dbCantidad.ToString("N4"),
                                                "",
                                                iIdProducto,
                                                cgUnidadCompra
                                                );
                    }
                }
                else
                    MessageBox.Show("No se encontraron datos para llenar el detalle", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
                MessageBox.Show("Error el la sentecia planteada","Mensaje",MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        //Función para actualizar el registro
        private void actualizarRegistro(int iBandera)
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "update cv402_cabecera_movimientos set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega=" + iIdMovimientoBodega;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //hara el rolBAck
                    goto reversa;
                }

                sSql = "";
                sSql += "update cv402_movimientos_bodega set" + Environment.NewLine;
                sSql += "estado = 'E'" + Environment.NewLine;
                sSql += "where Id_Movimiento_Bodega=" + iIdMovimientoBodega;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    //hara el rolBAck
                    goto reversa;
                }

                if (iBandera == 0)
                {
                    grabarRegistro(2);
                }

                goto fin;


            }
            catch (Exception)
            {
                ok.LblMensaje.Text = "Error al actualizar el registro";
                ok.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                MessageBox.Show("Ocurrió un problema en la transacción. No se guardarán los cambios");
            }
        fin:
            {
                MessageBox.Show("Operación Exitosa..","Bodega",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Anular..?", "Bodega", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                actualizarRegistro(1);
                limpiarCampos();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Desea Imprimir..?", "Bodega", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Bodega.frmReporteTransferencias reporte = new frmReporteTransferencias(iIdMovimientoBodega, txtNumeroTraslado.Text, Convert.ToInt32(cmbBodega.SelectedValue), Convert.ToInt32(cmbBodega2.SelectedValue));
                    reporte.ShowDialog();

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetalleVenta_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[5].Value.ToString()) > Convert.ToDouble(dgvDetalleVenta.CurrentRow.Cells[6].Value.ToString()))
                {
                    MessageBox.Show("La cantidad ingresada es mayor que el saldo", "Bodega", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDetalleVenta.CurrentRow.Cells[5].Value = "0.00";
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //Fin de la clase
    }
}
