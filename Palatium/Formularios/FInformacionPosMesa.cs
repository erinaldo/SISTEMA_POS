using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.IO;
using System.Net;
using System.Security.Util;
using ConexionBD;
using MaterialSkin.Controls;

namespace Palatium.Formularios
{
    public partial class FInformacionPosMesa : MaterialForm
    {
        //VARIABLES, INSTANCIAS
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        Clases.ClaseValidarCaracteres caracter = new Clases.ClaseValidarCaracteres();

        DataTable dtConsulta;

        int iIdMesa;
        int iCuenta;
        
        string sSql;
        string sCodigo;
        string sDescripcion;
        string sNumeroMesa;
        string sDescripcionSeccion;
        string sEstado;
        string sPosicionX;
        string sPosicionY;        
        string sIdMesa;
        string sCapacidad;
        string sIdSeccion;

        bool bRespuesta;

        public FInformacionPosMesa()
        {
            InitializeComponent();
        }                

        #region FUNCIONES DEL USUARIO

        //llenar el comboBox
        private void LLenarComboSecciones()
        {
            try
            {
                sSql = "";
                sSql += "select id_pos_seccion_mesa, descripcion" + Environment.NewLine;
                sSql += "from pos_seccion_mesa" + Environment.NewLine;
                sSql += " where estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                cmbSeccionMesa.llenar(dtConsulta, sSql);

                if (cmbSeccionMesa.Items.Count > 0)
                {
                    cmbSeccionMesa.SelectedIndex = 1;
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }


        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtNumeroMesa.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtCapacidad.Clear();
            txtNumeroMesa.Clear();
            txtX.Clear();
            txtY.Clear();
            cmbEstado.Text = "ACTIVO";
            btnNuevo.Text = "Nuevo";
            //cmbSeccionMesa.SelectedIndex = 0;
            iIdMesa = 0;
            chkEditar.Visible = false;
            chkEditar.Checked = false;

            btnAnular.Enabled = false;
            grupoDatos.Enabled = false;

            cmbSeccionMesa.Enabled = true;

            llenarGrid(0);

            txtBuscar.Focus();
        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int iOp)
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select a.id_pos_mesa, a.id_pos_seccion_mesa, a.codigo as CODIGO," + Environment.NewLine;
                sSql += "a.descripcion AS DESCR_MESA, a.numero_mesa," + Environment.NewLine;
                sSql += "b.descripcion AS DESCR_SECCIONMESA, a.capacidad as CAPACIDAD," + Environment.NewLine;
                sSql += "case a.estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO," + Environment.NewLine;
                sSql += "a.posicion_x, a.posicion_y" + Environment.NewLine;
                sSql += "from pos_mesa a inner join" + Environment.NewLine;
                sSql += "pos_seccion_mesa b on b.id_pos_seccion_mesa = a.id_pos_seccion_mesa" + Environment.NewLine;
                sSql += "and a.estado = 'A'" + Environment.NewLine;
                sSql += "and b.estado = 'A'" + Environment.NewLine;
                sSql += "and a.id_pos_seccion_mesa = " + Convert.ToInt32(cmbSeccionMesa.SelectedValue) + Environment.NewLine;
                
                if (iOp == 1)
                {
                    sSql += "where (a.codigo like '%" + txtBuscar.Text.Trim() + "%'" + Environment.NewLine;
                    sSql += "or a.descripcion like '%" + txtBuscar.Text.Trim() + "%')" + Environment.NewLine;
                }

                sSql += "order by a.id_pos_mesa";

                DataTable dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            sIdMesa = dtConsulta.Rows[i][0].ToString();
                            sIdSeccion = dtConsulta.Rows[i][1].ToString();
                            sCodigo = dtConsulta.Rows[i][2].ToString();
                            sDescripcion = dtConsulta.Rows[i][3].ToString();
                            sNumeroMesa= dtConsulta.Rows[i][4].ToString();
                            sDescripcionSeccion = dtConsulta.Rows[i][5].ToString();
                            sCapacidad = dtConsulta.Rows[i][6].ToString();
                            sEstado = dtConsulta.Rows[i][7].ToString();
                            sPosicionX = dtConsulta.Rows[i][8].ToString();
                            sPosicionY = dtConsulta.Rows[i][9].ToString();

                            dgvDatos.Rows.Add(sIdMesa, sIdSeccion, sCodigo, sDescripcion, 
                                              sNumeroMesa, sDescripcionSeccion, sCapacidad, 
                                              sEstado, sPosicionX, sPosicionY);

                        }
                    }
                }
                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                }
 
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //COMPROBAR SI EL NUMERO DE MESA YA ESTÁ REGISTRADO
        private int consultarMesa()
        {
            try
            {
                iCuenta = 0;

                //VERIFICAR EL CODIGO
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where numero_mesa = " + txtNumeroMesa.Text.Trim() + Environment.NewLine;
                sSql += "and id_pos_seccion_mesa = " + Convert.ToInt32(cmbSeccionMesa.SelectedValue) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = iCuenta + Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

                return iCuenta;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //COMPROBAR SI EL NUMERO DE MESA YA ESTÁ REGISTRADO
        private int consultarCodigoMesa()
        {
            try
            {
                iCuenta = 0;

                //VERIFICAR EL CODIGO
                sSql = "";
                sSql += "select count(*) cuenta" + Environment.NewLine;
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where codigo = '" + txtCodigo.Text.Trim() + "'" + Environment.NewLine;
                sSql += "and id_pos_seccion_mesa = " + Convert.ToInt32(cmbSeccionMesa.SelectedValue) + Environment.NewLine;
                sSql += "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    iCuenta = iCuenta + Convert.ToInt32(dtConsulta.Rows[0][0].ToString());
                }

                else
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }
                
                return iCuenta;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        //FUNCION PARA INSERTAR REGISTROS EN LA BASE DE DATOS
        private void insertarRegistro()
        {
            try
            {
                //VERIFICAR LA EXISTENCIA DEL CODIGO
                if (consultarCodigoMesa() > 0)
                {
                    ok.LblMensaje.Text = "Ya existe un registro con el con el código de mesa ingresado.";
                    ok.ShowDialog();
                    return;
                }

                else if (consultarCodigoMesa() == -1)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al consultar la existencia del código de mesa ingresado.";
                    ok.ShowDialog();
                    return;
                }

                //VERIFICAR LA EXISTENCIA DE LA MESA
                if (consultarMesa() > 0)
                {
                    ok.LblMensaje.Text = "Ya existe un registro con el número de mesa ingresado.";
                    ok.ShowDialog();
                    return;
                }

                else if (consultarMesa() == -1)
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al consultar la existencia del número de mesa ingresado.";
                    ok.ShowDialog();
                    return;
                }

                //AQUI INICIA LA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                sSql = "";
                sSql += "insert into pos_mesa (" + Environment.NewLine;
                sSql += "codigo, descripcion, id_pos_seccion_mesa, capacidad, numero_mesa," + Environment.NewLine;
                sSql += "posicion_x, posicion_y, estado, fecha_ingreso," + Environment.NewLine;
                sSql += "usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim().ToUpper() + "', '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += Convert.ToInt32(cmbSeccionMesa.SelectedValue) + ", " + Convert.ToInt32(txtCapacidad.Text.Trim()) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtNumeroMesa.Text) + ", " + Convert.ToInt32(txtX.Text.Trim()) + "," + Environment.NewLine;
                sSql += Convert.ToInt32(txtY.Text.Trim()) + ", 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[1] + "')";

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro insertado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

            reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS
        private void actualizarRegistro()
        {
            try
            {
                //AQUI INICIA LA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                sSql = "";
                sSql += "update pos_mesa set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim().ToUpper() + "'," + Environment.NewLine;
                sSql += "id_pos_seccion_mesa = " + Convert.ToInt32(cmbSeccionMesa.SelectedValue) + "," + Environment.NewLine;
                sSql += "capacidad = " + Convert.ToInt32(txtCapacidad.Text.Trim()) + "," + Environment.NewLine;
                sSql += "numero_mesa = " + Convert.ToInt32(txtNumeroMesa.Text.Trim()) + "," + Environment.NewLine;
                sSql += "posicion_x = " + Convert.ToInt32(txtX.Text.Trim()) + "," + Environment.NewLine;
                sSql += "posicion_y = " + Convert.ToInt32(txtY.Text.Trim()) + Environment.NewLine;
                sSql += "where id_pos_mesa = " + iIdMesa + Environment.NewLine;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        //FUNCION PARA ELIMINAR REGISTROS EN LA BASE DE DATOS
        private void eliminarRegistro()
        {
            try
            {
                //AQUI INICIA LA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    limpiarTodo();
                    return;
                }

                sSql = "";
                sSql += "update pos_mesa set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_mesa = " + iIdMesa + Environment.NewLine;

                //EJECUTA INSTRUCCION SQL
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                limpiarTodo();
                return;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }
        }

        //Función para ver si un registro ya está siendo utilizado
        private int comprobarRegistro()
        {
            try
            {
                sSql = "";
                sSql += "select count(*) cuenta from cv403_cab_pedidos" + Environment.NewLine;
                sSql += "where id_pos_mesa = " + iIdMesa + Environment.NewLine;
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
                    catchMensaje.LblMensaje.Text = "ERROR EN LA INSTRUCCIÓN:" + Environment.NewLine + sSql;
                    catchMensaje.ShowDialog();
                    return -1;
                }

            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
                return -1;
            }
        }

        #endregion

        private void FInformacionPosMesa_Load(object sender, EventArgs e)
        {
            cmbEstado.Text = "ACTIVO";

            cmbSeccionMesa.SelectedIndexChanged -= new EventHandler(cmbSeccionMesa_SelectedIndexChanged);            
            LLenarComboSecciones();
            cmbSeccionMesa.SelectedIndexChanged += new EventHandler(cmbSeccionMesa_SelectedIndexChanged);

            llenarGrid(0);
        }

        private void BtnNuevoPosMesa_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(cmbSeccionMesa.SelectedValue) == 0)
            {
                ok.LblMensaje.Text = "Favor seleccione la sección para crear una mesa.";
                ok.ShowDialog();
                cmbSeccionMesa.Focus();
            }

            //SI EL BOTON ESTA EN OPCION NUEVO
            else if (btnNuevo.Text == "Nuevo")
            {
                grupoDatos.Enabled = true;
                btnNuevo.Text = "Guardar";
                cmbSeccionMesa.Enabled = false;
                txtCodigo.Focus();
            }

            //SI EL BOTON ESTA EN OPCION GUARDAR O ACTUALIZAR
            else 
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el código para la mesa.";
                    ok.ShowDialog();
                    txtCodigo.Focus();
                }

                else if (txtDescripcion.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese una descripción para la mesa.";
                    ok.ShowDialog();
                    txtDescripcion.Focus();
                }

                else if (txtNumeroMesa.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el número de mesa.";
                    ok.ShowDialog();
                    txtNumeroMesa.Focus();
                }

                else if (txtCapacidad.Text.Trim() == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la capacidad de personas para la mesa.";
                    ok.ShowDialog();
                    txtCapacidad.Focus();
                }

                else if ((txtX.Text.Trim() == "") || (txtY.Text.Trim() == ""))
                {
                    ok.LblMensaje.Text = "Favor seleccione la ubicación de la mesa.";
                    ok.ShowDialog();
                    btnExaminar.Focus();
                }

                else
                {
                    if (iIdMesa == 0)
                    {
                        SiNo.LblMensaje.Text = "¿Está seguro que desea guardar el registro?";
                        SiNo.ShowDialog();

                        if (SiNo.DialogResult == DialogResult.OK)
                        {
                            insertarRegistro();
                        }
                    }

                    else
                    {
                        actualizarRegistro();
                    }
                }
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSeccionMesa.SelectedValue) == 0)
            {
                ok.LblMensaje.Text = "Favor seleccione la sección para ubicar la mesa.";
                ok.ShowDialog();
            }

            else
            {
                Oficina.frmCrearMesas crear = new Oficina.frmCrearMesas(Convert.ToInt32(cmbSeccionMesa.SelectedValue));
                crear.lblSeccion.Text = cmbSeccionMesa.Text;
                crear.lblMesa.Text = "MESA " + txtNumeroMesa.Text.Trim();
                crear.ShowDialog();

                if (crear.DialogResult == DialogResult.OK)
                {
                    txtX.Text = crear.sPosicion_X;
                    txtY.Text = crear.sPosicion_Y;
                    crear.Close();
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            iCuenta = comprobarRegistro();

            if (iCuenta == 0)
            {
                eliminarRegistro();
            }

            else if (iCuenta > 0)
            {
                ok.LblMensaje.Text = "No se puede eliminar la mesa, ya que se encuentra en uso en el sistema.";
                ok.ShowDialog();
            }     
        }

        private void txtNumeroMesa_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void txtCapacidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracter.soloNumeros(e);
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                grupoDatos.Enabled = true;
                btnNuevo.Text = "Actualizar";
                btnAnular.Enabled = true;
                txtCodigo.Enabled = false;
                cmbSeccionMesa.Enabled = false;
                txtNumeroMesa.Enabled = true;

                iIdMesa = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
                cmbSeccionMesa.SelectedValue = Convert.ToInt32(dgvDatos.CurrentRow.Cells[1].Value);
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                txtNumeroMesa.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
                txtCapacidad.Text = dgvDatos.CurrentRow.Cells[6].Value.ToString();
                cmbEstado.Text = dgvDatos.CurrentRow.Cells[7].Value.ToString();
                txtX.Text = dgvDatos.CurrentRow.Cells[8].Value.ToString();
                txtY.Text = dgvDatos.CurrentRow.Cells[9].Value.ToString();

                chkEditar.Visible = true;
                chkEditar.Checked = false;
                //txtNumeroMesa.Enabled = false;

                txtDescripcion.Focus();
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void chkEditar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditar.Checked == true)
            {
                txtNumeroMesa.Enabled = true;
                txtNumeroMesa.Focus();
            }

            else
            {
                txtNumeroMesa.Enabled = false;
            }
        }

        private void cmbSeccionMesa_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenarGrid(0);
        }

    }
}
