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
using ConexionBD;

namespace Palatium.Formularios
{
    public partial class FLocalidades : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        DataTable dtConsulta;

        string sSql = "";

        bool bRespuesta;

        int idResponsab, id_localidad = 0;

        int comproelectro;

        string sCorrelativoProveedor;

        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoSiNo NuevoSiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        public FLocalidades()
        {
            InitializeComponent();
        }

        //Llenar combo tipo Empresa
        private void llenarComboEmpresa()
        {
            try
            {
                sSql = "";
                sSql += "select idempresa, isnull(nombrecomercial, razonsocial) nombrecomercial" + Environment.NewLine;
                sSql += "from sis_empresa" + Environment.NewLine;
                sSql += "where idempresa = " + Program.iIdEmpresa;

                dtConsulta = new DataTable();
                dtConsulta.Clear();
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

        //LLENAR COMBO bodega
        private void llenarCombBodega()
        {
            try
            {
                sSql = "";
                sSql += "select id_bodega, descripcion" + Environment.NewLine;
                sSql += "from cv402_bodegas" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbBodega.llenar(dtConsulta, sSql);
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO LIsta de precio por defecto
        private void llenarCombLisPrecio()
        {
            try
            {
                sSql = "";
                sSql += "select id_lista_precio, descripcion" + Environment.NewLine;
                sSql += "from cv403_listas_precios" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbListaPrecio.llenar(dtConsulta, sSql);

                //cmbListaPrecio.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void cargarComboMotivo()
        {
            try
            {
                sSql = "";
                sSql += "select C.correlativo, C.valor_texto" + Environment.NewLine;
                sSql += "from tp_relaciones R , tp_codigos C" + Environment.NewLine;
                sSql += "where R.Tabla_Contenida = 'SYS$00643'" + Environment.NewLine;
                sSql += "and R.Tabla_Contenedora = 'SYS$00648'" + Environment.NewLine;
                sSql += "And R.Codigo_Contenedor = 'EMP'" + Environment.NewLine;
                sSql += "And R.CG_Tipo_Relacion In (53, -1)" + Environment.NewLine;
                sSql += "And R.Estado = 'A'" + Environment.NewLine;
                sSql += "And C.correlativo = R.Correlativo_Contenido" + Environment.NewLine;
                sSql += "And C.estado='A'" + Environment.NewLine;
                sSql += "group by C.Correlativo, C.valor_texto" + Environment.NewLine;
                sSql += "order by C.valor_texto";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbMotivos.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //LLENAR COMBO servidor por defecto 
        private void llenarCombServidor()
        {
            try
            {
                sSql = "";
                sSql += "select id_servidor, nombre_servidor" + Environment.NewLine;
                sSql += "from cv480_servidores" + Environment.NewLine;
                sSql += "where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbServidor.llenar(dtConsulta, sSql);
                //cmbServidor.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //LLENAR COMBO localidad
        private void llenarCombLocalidad()
        {
            try
            {
                sSql = "";
                sSql += "select correlativo, valor_texto" + Environment.NewLine;
                sSql += "from tp_codigos" + Environment.NewLine;
                sSql += "where tabla = 'SYS$00019'" + Environment.NewLine;
                sSql += "and estado ='A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbLocalidad.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void FLocalidades_Load(object sender, EventArgs e)
        {            
            llenarComboEmpresa();
            llenarCombBodega();
            llenarCombLisPrecio();
            llenarCombServidor();
            llenarCombLocalidad();
            llenarComboInsumos();
            cargarComboMotivo();
            llenarGrid(0);
        }

        //Función para llenar el combo de insumo
        private void llenarComboInsumos()
        {
            try
            {
                sSql = "";
                sSql += "select LO.id_localidad, CO.valor_texto" + Environment.NewLine;
                sSql += "from tp_localidades LO inner join" + Environment.NewLine;
                sSql += "sis_empresa EM on LO.idempresa = EM.idempresa inner join" + Environment.NewLine;
                sSql += "cv402_bodegas BO on LO.id_bodega = BO.id_bodega inner join" + Environment.NewLine;
                sSql += "tp_codigos CO on LO.cg_localidad = CO.correlativo inner join" + Environment.NewLine;
                sSql += "tp_personas PER on LO.id_responsable = PER.id_persona inner join" + Environment.NewLine;
                sSql += "cv403_listas_precios LIS on LO.id_lista_defecto = LIS.id_lista_precio inner join" + Environment.NewLine;
                sSql += "cv480_servidores SER on LO.id_servidor = SER.id_servidor";

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                cmbInsumos.llenar(dtConsulta, sSql);
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnPregunta_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaRespoLocalida ayuda1 = new Formularios.FAyudaRespoLocalida();
            ayuda1.ShowDialog();

            idResponsab = Convert.ToInt32(ayuda1.IdResponsable);
            txtResponsable.Text = ayuda1.identiResponsable;
            txtEmpresaResponsable.Text = ayuda1.apelliResponsable;
        }

        #region FUNCIONES DEL USUARIO

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            cmbEstado.SelectedIndex = 0;
            cmbBodega.SelectedIndex = 0;
            cmbListaPrecio.SelectedIndex = 0;
            cmbLocalidad.SelectedIndex = 0;
            txtDireccion.Text = "";
            txtEstablecimiento.Text = "";
            txtPuntoEmision.Text = "";
            txtResponsable.Text = "";
            txtEmpresaResponsable.Text = "";
            txtTelefono1.Clear();
            txtTelefono2.Clear();
            //cmbEmpresa.SelectedIndex = 1;
            cmbServidor.SelectedIndex = 1;
            cmbEstado.Text = "ACTIVO";

            llenarGrid(0);
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select LO.id_localidad, EM.nombrecomercial, CO.valor_texto," + Environment.NewLine;
                sSql += "LIS.descripcion, PER.apellidos, BO.descripcion, LIS.descripcion," + Environment.NewLine;
                sSql += "SER.nombre_servidor, LO.establecimiento, LO.punto_emision," + Environment.NewLine;
                sSql += "LO.emite_comprobante_electronico, LO.direccion, LO.estado," + Environment.NewLine;
                sSql += "LO.id_responsable, PER.identificacion, isnull(LO.telefono1, '') telefono1," + Environment.NewLine;
                sSql += "isnull(LO.telefono2, '') telefono2, LO.id_auxiliar, LO.cg_motivo_movimiento_bodega" + Environment.NewLine;
                sSql += "from tp_localidades LO inner join" + Environment.NewLine;
                sSql += "sis_empresa EM on LO.idempresa = EM.idempresa inner join" + Environment.NewLine;
                sSql += "cv402_bodegas BO on LO.id_bodega = BO.id_bodega inner join" + Environment.NewLine;
                sSql += "tp_codigos CO on LO.cg_localidad = CO.correlativo inner join" + Environment.NewLine;
                sSql += "tp_personas PER on LO.id_responsable = PER.id_persona inner join" + Environment.NewLine;
                sSql += "cv403_listas_precios LIS on LO.id_lista_defecto = LIS.id_lista_precio inner join" + Environment.NewLine;
                sSql += "cv480_servidores SER on LO.id_servidor = SER.id_servidor" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "where EM.idempresa like '%" + txtBuscar.Text.Trim().ToUpper() + "%'" + Environment.NewLine;
                    sSql += "OR PER.apellidos like '%" + txtBuscar.Text.Trim().ToUpper() + "%'" + Environment.NewLine;
                    sSql += "OR BO.descripcion like '%" + txtBuscar.Text.Trim().ToUpper() + "%'" + Environment.NewLine;
                    sSql += "OR SER.nombre_servidor like '%" + txtBuscar.Text.Trim().ToUpper() + "%'" + Environment.NewLine;
                    sSql += "OR LO.establecimiento like '%" + txtBuscar.Text.Trim().ToUpper() + "%'" + Environment.NewLine;
                    sSql += "OR LO.punto_emision like '%" + txtBuscar.Text.Trim().ToUpper() + "%'" + Environment.NewLine;
                    sSql += "OR LO.emite_comprobante_electronico like '%" + txtBuscar.Text.Trim().ToUpper() + "%'";
                }

                dtConsulta = new DataTable();
                dtConsulta.Clear();
                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvLocalidades.DataSource = dtConsulta;
                    dgvLocalidades.Columns[0].Visible = false;
                    dgvLocalidades.Columns[13].Visible = false;
                    dgvLocalidades.Columns[14].Visible = false;
                    dgvLocalidades.Columns[15].Visible = false;
                    dgvLocalidades.Columns[17].Visible = false;
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

        //FUNCION PARA INSERTAR UN  REGISTRO
        private void insertarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción para guardar el registro.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }
                
                //INSTRUCCION SQL PARA INSERTAR
                sSql = "";
                sSql += "insert into tp_localidades (" + Environment.NewLine;
                sSql += "idempresa, cg_empresa, cg_localidad, id_responsable, id_bodega," + Environment.NewLine;
                sSql += "id_lista_defecto, id_servidor, establecimiento, punto_emision," + Environment.NewLine;
                sSql += "emite_comprobante_electronico, direccion, telefono1, telefono2," + Environment.NewLine;
                sSql += "estado, fecha_ingreso, usuario_ingreso, terminal_ingreso," + Environment.NewLine;
                sSql += "id_localidad_insumo, id_auxiliar, cg_motivo_movimiento_bodega)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += Convert.ToInt32(cmbEmpresa.SelectedValue) + ", " + Program.iCgEmpresa + ", ";
                sSql += Convert.ToInt32(cmbLocalidad.SelectedValue) + ", " + idResponsab + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbBodega.SelectedValue) + ", " + Convert.ToInt32(cmbListaPrecio.SelectedValue) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbServidor.SelectedValue) + ", '" + txtEstablecimiento.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtPuntoEmision.Text.Trim() + "', '" + comproelectro + "', '" + txtDireccion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + txtTelefono1.Text.Trim() + "', '" + txtTelefono2.Text.Trim() + "', 'A'," + Environment.NewLine;
                sSql += "GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbInsumos.SelectedValue) + ", " + sCorrelativoProveedor + "," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbMotivos.SelectedValue) + " )";

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro insertado correctamente.";
                ok.ShowDialog();

                Grb_DatoLocalidades.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
                return;
            }

            catch(Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); return; }
        }

        //FUNCION PARA ACTUALIZAR UN  REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción para actualizar el registro.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                //INSTRUCCION SQL PARA ACTUALIZAR
                sSql = "";
                sSql += "update tp_localidades set" + Environment.NewLine;
                sSql += "idempresa = " + Convert.ToInt32(cmbEmpresa.SelectedValue) + "," + Environment.NewLine;
                sSql += "cg_localidad = " + Convert.ToInt32(cmbLocalidad.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_responsable = '" + idResponsab + "'," + Environment.NewLine;
                sSql += "id_bodega = " + Convert.ToInt32(cmbBodega.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_lista_defecto = " + Convert.ToInt32(cmbListaPrecio.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_servidor = " + Convert.ToInt32(cmbServidor.SelectedValue) + "," + Environment.NewLine;
                sSql += "establecimiento = '" + txtEstablecimiento.Text.Trim() + "'," + Environment.NewLine;
                sSql += "punto_emision = '" + txtPuntoEmision.Text.Trim() + "'," + Environment.NewLine;
                sSql += "emite_comprobante_electronico = " + comproelectro + "," + Environment.NewLine;
                sSql += "direccion = '" + txtDireccion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "telefono1 = '" + txtTelefono1.Text.Trim() + "'," + Environment.NewLine;
                sSql += "telefono2 = '" + txtTelefono2.Text.Trim() + "'," + Environment.NewLine;
                sSql += "id_localidad_insumo = " + Convert.ToInt32(cmbInsumos.SelectedValue) + "," + Environment.NewLine;
                sSql += "id_auxiliar = " + sCorrelativoProveedor + "," + Environment.NewLine;
                sSql += "cg_motivo_movimiento_bodega = " + Convert.ToInt32(cmbMotivos.SelectedValue) + Environment.NewLine;
                sSql += "where id_localidad = " + id_localidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();

                Grb_DatoLocalidades.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
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

        //FUNCION PARA ELIMINAR UN REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA PROCESO DE ACTUALIZACION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción para eliminar el registro.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                //INSTRUCCION SQL PARA ELIMINAR
                sSql = "";
                sSql += "update tp_localidades set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_localidad = " + id_localidad;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN  LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.lblMensaje.Text = "Registro actualizado correctamente.";
                ok.ShowDialog();

                Grb_DatoLocalidades.Enabled = false;
                btnNuevo.Text = "Nuevo";
                limpiarTodo();
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
        
        #endregion

        private void btnCerrarLocalidades_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiarLocalidades_Click(object sender, EventArgs e)
        {

            Grb_DatoLocalidades.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscarLocalidades_Click(object sender, EventArgs e)
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                //limpiarTodo();
                Grb_DatoLocalidades.Enabled = true;
                btnNuevo.Text = "Guardar";
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR
            else 
            {
                if (chkCmproElectro.Checked == true)
                {
                    comproelectro = 1;
                }

                else
                {
                    comproelectro = 0;
                }
                
                if (txtProveedor.Text.Trim() == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el proveedor.";
                    ok.ShowDialog();
                    txtProveedor.Focus();
                }

                if (btnNuevo.Text == "Guardar")
                {
                    NuevoSiNo.lblMensaje.Text = "¿Desea guardar el registro...?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        insertarRegistro();
                    }
                }

                else if (btnNuevo.Text == "Actualizar")
                {
                    NuevoSiNo.lblMensaje.Text = "¿Desea actualizar el registro...?";
                    NuevoSiNo.ShowDialog();

                    if (NuevoSiNo.DialogResult == DialogResult.OK)
                    {
                        actualizarRegistro();
                    }
                }
            }            
        }

        private void btnAnularLocalidades_Click(object sender, EventArgs e)
        {
            NuevoSiNo.lblMensaje.Text = "Esta seguro que desea dar de bajar el registro?";
            NuevoSiNo.ShowDialog();

            if (NuevoSiNo.DialogResult == DialogResult.OK)
            {
                eliminarRegistro();
            }            
        }

        private void dgvLocalidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnNuevo.Text = "Actualizar";
                Grb_DatoLocalidades.Enabled = true;

                id_localidad = Convert.ToInt32(dgvLocalidades.CurrentRow.Cells[0].Value.ToString());

                cmbInsumos.SelectedValue = buscarIdInsumo(id_localidad);

                if (dgvLocalidades.CurrentRow.Cells[18].Value.ToString() != "")
                    cmbMotivos.SelectedValue = Convert.ToInt32(dgvLocalidades.CurrentRow.Cells[18].Value.ToString());
                else
                    cmbMotivos.SelectedIndex = 0;

                if (dgvLocalidades.CurrentRow.Cells[17].Value.ToString() != "")
                {
                    int iIdAuxiliar = Convert.ToInt32(dgvLocalidades.CurrentRow.Cells[17].Value.ToString());
                    string[] buscarDatos = buscarDatosProveedores(iIdAuxiliar);

                    if (buscarDatos[0] != "0")
                    {
                        txtProveedor.Text = buscarDatos[0];
                        txtNombreProveedor.Text = buscarDatos[1];
                        sCorrelativoProveedor = iIdAuxiliar.ToString();
                    }
                    else
                    {
                        txtProveedor.Text = "";
                        txtNombreProveedor.Text = "";
                    }

                }
                else
                {
                    txtProveedor.Text = "";
                    txtNombreProveedor.Text = "";
                }


                //cmbEmpresa.Text = dgvLocalidades.CurrentRow.Cells[1].Value.ToString();
                //string valReco = dgvLocalidades.CurrentRow.Cells[1].Value.ToString();
                cmbLocalidad.Text = dgvLocalidades.CurrentRow.Cells[2].Value.ToString();
                txtEmpresaResponsable.Text = dgvLocalidades.CurrentRow.Cells[4].Value.ToString();
                cmbBodega.Text = dgvLocalidades.CurrentRow.Cells[5].Value.ToString();
                cmbListaPrecio.Text = dgvLocalidades.CurrentRow.Cells[6].Value.ToString();
                cmbServidor.Text = dgvLocalidades.CurrentRow.Cells[7].Value.ToString();
                txtEstablecimiento.Text = dgvLocalidades.CurrentRow.Cells[8].Value.ToString();
                txtPuntoEmision.Text = dgvLocalidades.CurrentRow.Cells[9].Value.ToString();
                string EmiCompro = dgvLocalidades.CurrentRow.Cells[10].Value.ToString();
                if (EmiCompro == "1")
                    chkCmproElectro.Checked = true;
                else chkCmproElectro.Checked = false;
                txtDireccion.Text = dgvLocalidades.CurrentRow.Cells[11].Value.ToString();
                if (dgvLocalidades.CurrentRow.Cells[12].Value.ToString() == "A")
                {
                    cmbEstado.Text = "ACTIVO";
                }
                else
                {
                    cmbEstado.Text = "ELIMINADO";
                }

                txtResponsable.Text = dgvLocalidades.CurrentRow.Cells[14].Value.ToString();
                idResponsab = Convert.ToInt32(dgvLocalidades.CurrentRow.Cells[13].Value.ToString());

                txtTelefono1.Text = dgvLocalidades.CurrentRow.Cells[15].Value.ToString();
                txtTelefono2.Text = dgvLocalidades.CurrentRow.Cells[16].Value.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Función para buscar los datos de los proveedores
        private string[] buscarDatosProveedores(int iIdAuxiliar)
        {
            string[] sRespuesta = new string[2];
            sRespuesta[0] = "0";
            sRespuesta[1] = "0";
            string sSql = "select  codigo, descripcion from cv404_auxiliares_contables where id_auxiliar = " + iIdAuxiliar;
            DataTable dtAyuda = new DataTable();
            dtAyuda.Clear();
            bool bRespuesta = conexion.GFun_Lo_Busca_Registro(dtAyuda, sSql);
            if (bRespuesta == true)
            {
                if (dtAyuda.Rows.Count > 0)
                {
                    sRespuesta[0] = dtAyuda.Rows[0].ItemArray[0].ToString();
                    sRespuesta[1] = dtAyuda.Rows[0].ItemArray[1].ToString();
                }
            }

            return sRespuesta;
        }

        private void txtEstablecimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            //para permitirme solo ingresar numeros con la ayuda de la clase creada llamada ValidarNum_letra_decimal
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtPuntoEmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtTelefono1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtTelefono2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        //Función para buscar el id del insumo
        private int buscarIdInsumo(int id_localidad)
        {
            string sSql = "select isnull(id_localidad_insumo, 0) from tp_localidades where id_localidad = " + id_localidad;
            DataTable dtConsulta = new DataTable();
            dtConsulta.Clear();
            if (conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql) == true)
            {
                if (dtConsulta.Rows.Count > 0)
                    return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
                else
                    return 0;
            }
            else
                return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bodega.frmAyudaBodegas_Proveedores_ ayuda = new Bodega.frmAyudaBodegas_Proveedores_();
            ayuda.ShowDialog();

            if (ayuda.DialogResult == DialogResult.OK)
            {
                sCorrelativoProveedor = ayuda.sCorrelativo;
                txtProveedor.Text = ayuda.sCodigo;
                txtNombreProveedor.Text = ayuda.sNombre;
            }
        }
    }
}
