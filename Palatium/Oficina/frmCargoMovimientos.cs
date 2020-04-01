using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Oficina
{
    public partial class frmCargoMovimientos : MaterialForm
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeNuevoCatch catchMensaje = new VentanasMensajes.frmMensajeNuevoCatch();
        VentanasMensajes.frmMensajeNuevoOk ok = new VentanasMensajes.frmMensajeNuevoOk();
        VentanasMensajes.frmMensajeNuevoSiNo SiNo = new VentanasMensajes.frmMensajeNuevoSiNo();

        bool bRespuesta;

        DataTable dtConsulta;

        string sSql;
        string sEstado;
        string sFecha;

        int iIdRegistro;
        int iCuenta;

        public frmCargoMovimientos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            iIdRegistro = 0;
            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            btnAnular.Enabled = false;
            cmbEstado.Text = "ACTIVO";
            cmbEstado.Enabled = false;
            llenarGrid(0);
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_cargo, codigo as CODIGO, descripcion as DESCRIPCION," + Environment.NewLine;
                sSql += "CASE estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO" + Environment.NewLine;
                sSql += "from pos_cargo" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and (codigo like '%' + '" + txtBuscar.Text.Trim() + "' + '%'" + Environment.NewLine;
                    sSql += "or descripcion like '%' + '" + txtBuscar.Text.Trim() + "' + '%')" + Environment.NewLine;
                    sSql += "" + Environment.NewLine;
                }

                sSql += "order by id_pos_cargo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;                    
                    dgvDatos.Columns[1].Width = 75;
                    dgvDatos.Columns[2].Width = 170;
                    dgvDatos.Columns[3].Width = 75;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.ClearSelection();
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

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                iCuenta = consultarRegistroCrear();

                if (iCuenta > 0)
                {
                    ok.lblMensaje.Text = "Ya existe un registro con el código ingresado.";
                    ok.ShowDialog();
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                    return;
                }

                else if (iCuenta == -1)
                {
                    return;
                }

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_cargo (" + Environment.NewLine;
                sSql += "codigo, descripcion, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim().ToUpper() + "', '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro ingresado correctamente";
                ok.ShowDialog();
                Grb_Dato.Enabled = false;
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

        //FUNCION PARA ACTUALIZAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cargo set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_pos_cargo = " + iIdRegistro + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";


                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro actualizado correctamente";
                ok.ShowDialog();
                Grb_Dato.Enabled = false;
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

        //FUNCION PARA DAR DE BAJA EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {                

                //SE INICIA UNA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.lblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    return;
                }

                sSql = "";
                sSql += "update pos_cargo set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_cargo = " + iIdRegistro + Environment.NewLine;

                //EJECUTAR LA INSTRUCCIÓN SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.lblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.lblMensaje.Text = "Registro eliminado correctamente";
                ok.ShowDialog();
                Grb_Dato.Enabled = false;
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

        //FUNCION PARA CONSULTAR EL REGISTRO SI ESTÁ REGISTRADO
        private int consultarRegistroCrear()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_cargo" + Environment.NewLine;
                sSql += "where codigo = '" + txtCodigo.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and estado in ('A', 'N')";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
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

        //FUNCION PARA CONSULTAR EL REGISTRO SI ESTÁ ELIMINADO
        private int consultarRegistroEliminar()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_movimiento_caja" + Environment.NewLine;
                sSql += "where id_pos_cargo = " + iIdRegistro + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    return Convert.ToInt32(dtConsulta.Rows[0].ItemArray[0].ToString());
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

        #endregion

        private void frmCargoMovimientos_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
            cmbEstado.Text = "ACTIVO";
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Grb_Dato.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text == "")
                {
                    //ENVIAR A LLENAR EL GRID CON TODOS LOS DATOS
                    llenarGrid(0);
                }

                else
                {
                    //ENVIAR A LLENAR EL GRID CON UN SOLO DATO
                    llenarGrid(1);
                }
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                llenarGrid(0);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarTodo();
                Grb_Dato.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
            }

            else
            {
                if ((txtCodigo.Text == "") && (txtDescripcion.Text == ""))
                {
                    ok.lblMensaje.Text = "Debe rellenar todos los campos obligatorios.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtCodigo.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese el código de la sección mesa.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok.lblMensaje.Text = "Favor ingrese la descripción de la sección mesa.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else
                {
                    if (btnNuevo.Text == "Guardar")
                    {
                        insertarRegistro();
                    }

                    else if (btnNuevo.Text == "Actualizar")
                    {
                        if (cmbEstado.Text == "ACTIVO")
                        {
                            sEstado = "A";
                        }

                        else
                        {
                            sEstado = "N";
                        }

                        actualizarRegistro();
                    }
                }
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Grb_Dato.Enabled = true;
                btnNuevo.Text = "Actualizar";
                txtCodigo.Enabled = false;
                btnAnular.Enabled = true;
                cmbEstado.Enabled = true;

                iIdRegistro = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                cmbEstado.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            iCuenta = consultarRegistroEliminar();

            if (iCuenta > 0)
            {
                ok.lblMensaje.Text = "Existen registros asociados a movimientos de caja. No se puede eliminar.";
                ok.ShowDialog();
                return;
            }

            else if (iCuenta == -1)
            {
                return;
            }

            else
            {
                SiNo.lblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }
        }
    }
}
