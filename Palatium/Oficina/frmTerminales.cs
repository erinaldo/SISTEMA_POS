using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using MaterialSkin.Controls;

namespace Palatium.Oficina
{
    public partial class frmTerminales : MaterialForm
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        DataTable dtConsulta;
        string sSql;
        string sFecha;
        string sEstado;
        bool bRespuesta;
        int iIdTerminal;

        public frmTerminales()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //Función para llenar el Combo de Localidad
        private void llenarComboLocalidad()
        {
            try
            {
                dtConsulta = new DataTable();
                sSql = "select id_localidad, nombre_localidad from tp_vw_localidades";
                cmbLocalidad.llenar(dtConsulta, sSql);
                cmbLocalidad.SelectedValue = Program.iIdLocalidad;
            }
            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        //LIMPIAR TODO EL FORMULARIO
        private void limpiarTodo()
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtNombreEquipo.Clear();
            TxtIPAsignada.Clear();
            cmbEstado.Text = "ACTIVO";
            btnNuevo.Text = "Nuevo";
            btnEliminar.Enabled = false;

            llenarComboLocalidad();

            grupoDatos.Enabled = false;
            llenarGrid(0);
        }

        //LIMPIAR SOLO LAS CAJAS DE TEXTO
        private void limpiarCajasTexto()
        {
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtNombreEquipo.Clear();
            TxtIPAsignada.Clear();
            cmbEstado.Text = "ACTIVO";
            btnNuevo.Text = "Nuevo";
            btnEliminar.Enabled = false;
            llenarComboLocalidad();
        }

        //EXTRAER LA IP DEL EQUIPO
        private void recuperarIP()
        {
            IPHostEntry host;

            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    TxtIPAsignada.Text = ip.ToString();
                }
            }
        }

        //INSERTAR UN REGISTRO
        private void insertarRegistro()
        {
            try
            {
                sSql = "";
                sSql = sSql + "select * from pos_terminal where (codigo = '" + txtCodigo.Text.Trim() + "'" + Environment.NewLine;
                sSql = sSql + "or nombre_maquina = '" + txtNombreEquipo.Text.Trim() + "'";
                sSql = sSql + "or ip_maquina = '" + TxtIPAsignada.Text.Trim() + "')";
                sSql = sSql + "and estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        ok.LblMensaje.Text = "Ya existe un registro con el codigo o datos ingresados para el equipo " + dtConsulta.Rows[0].ItemArray[3].ToString();
                        ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto fin;
                }

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql = sSql + "insert into pos_terminal (codigo, descripcion, nombre_maquina, ip_maquina, estado, fecha_ingreso, usuario_ingreso, terminal_ingreso) " + Environment.NewLine;
                sSql = sSql + "values ('" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "', '" + txtNombreEquipo.Text.Trim() + "', '" + TxtIPAsignada.Text.Trim() + "', 'A', GETDATE(), '" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //si no se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro ingresado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarCajasTexto();
                limpiarTodo();
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }

        }

        //ACTUALIZAR UN REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción.";
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql = sSql + "update pos_terminal set" + Environment.NewLine;
                sSql = sSql + "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "nombre_maquina = '" + txtNombreEquipo.Text.Trim() + "'," + Environment.NewLine;
                sSql = sSql + "ip_maquina = '" + TxtIPAsignada.Text.Trim() + "'" + Environment.NewLine;
                sSql = sSql + "where id_pos_terminal = " + iIdTerminal;

                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                //si no se ejecuta bien hara un commit
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                btnNuevo.Text = "Nuevo";
                grupoDatos.Enabled = false;
                limpiarCajasTexto();
                limpiarTodo();
                goto fin;
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
            }

        fin: { }

        }

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid(int op)
        {
            try
            {
                sSql = "";

                sSql = sSql + "select id_pos_terminal, codigo CODIGO, descripcion DESCRIPCION, " + Environment.NewLine;
                sSql = sSql + "nombre_maquina 'NOMBRE DEL EQUIPO', ip_maquina 'IP ASIGNADA', " + Environment.NewLine;
                sSql = sSql + "case (estado) when 'A' then 'ACTIVO' else 'ELIMINADO' end ESTADO" + Environment.NewLine;
                sSql = sSql + "from pos_terminal" + Environment.NewLine;
                sSql = sSql + "where ";

                if (op == 1)
                {
                    sSql = sSql + "codigo like '%' + '" + txtBusqueda.Text.Trim() + "' + '%' " + Environment.NewLine;
                    sSql = sSql + "or descripcion like '%' + '" + txtBusqueda.Text.Trim() + "' + '%'" + Environment.NewLine;
                    sSql = sSql + "or nombre_maquina like '%' + '" + txtBusqueda.Text.Trim() + "' + '%'" + Environment.NewLine;
                    sSql = sSql + "and ";
                }

                sSql = sSql + "estado = 'A' order by id_pos_terminal";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        dgvDatos.Columns[1].Width = 70;
                        dgvDatos.Columns[2].Width = 150;
                        dgvDatos.Columns[3].Width = 150;
                        dgvDatos.Columns[4].Width = 100;
                        dgvDatos.Columns[5].Width = 80;
                        dgvDatos.Columns[0].Visible = false;
                        goto fin;
                    }
                    else
                    {
                        goto fin;
                    }
                }

                else
                {
                    goto reversa;
                }

            }

            catch (Exception)
            {
                goto reversa;
            }

        reversa:
            {
                ok.LblMensaje.Text = "Ocurrió un problema al realizar la consulta.";
                ok.ShowInTaskbar = false;
                ok.ShowDialog();
            }

        fin: { }

        }

        //FUNCION PARA ELIMINAR
        private void eliminarRegistro(int iId)
        {
            try
            {
                SiNo.LblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    //INICIAMOS UNA NUEVA TRANSACCION
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        ok.LblMensaje.Text = "Error al abrir transacción.";
                        ok.ShowDialog();
                        goto fin;
                    }

                    sSql = "update pos_terminal set estado = 'E' where id_pos_terminal = " + iId;

                    //SE EJECUTA LA INSTRUCCIÓN SQL
                    if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                    {
                        catchMensaje.LblMensaje.Text = sSql;
                        catchMensaje.ShowDialog();
                        goto reversa;
                    }

                    conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                    ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                    ok.ShowDialog();
                    limpiarTodo();
                    goto fin;
                }

                else
                {
                    goto fin;
                }
            }

            catch (Exception)
            {
                goto reversa;
            }

        reversa:
            {
                conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                //ok.LblMensaje.Text = "Ocurrió un problema en la transacción. No se guardarán los cambios.";
                //ok.ShowDialog();
            }

        fin: { }
        }

        #endregion

        private void btnExtraerNombreEquipo_Click(object sender, EventArgs e)
        {
            txtNombreEquipo.Text = Environment.MachineName.ToString();
        }

        private void btnNuevoCanImpre_Click(object sender, EventArgs e)
        {
            //SI EL BOTON ESTA EN OPCION NUEVO
            if (btnNuevo.Text == "Nuevo")
            {
                limpiarCajasTexto();
                grupoDatos.Enabled = true;
                txtCodigo.Enabled = true;
                btnNuevo.Text = "Guardar";
                txtCodigo.Focus();
            }

            else
            {
                if (txtCodigo.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el código para el terminal.";
                    ok.ShowDialog();
                }

                else if (txtDescripcion.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la descripción para el terminal.";
                    ok.ShowDialog();
                }

                else if (txtNombreEquipo.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese el nombre del equipo.";
                    ok.ShowDialog();
                }

                else if (TxtIPAsignada.Text == "")
                {
                    ok.LblMensaje.Text = "Favor ingrese la IP asignada del equipo.";
                    ok.ShowDialog();
                }

                else if (btnNuevo.Text == "Guardar")
                {
                    insertarRegistro();
                }

                else if (btnNuevo.Text == "Actualizar")
                {
                    actualizarRegistro();
                }
            }
        }

        private void btnExtraerIpAsignada_Click(object sender, EventArgs e)
        {
            recuperarIP();
        }

        private void frmTerminales_Load(object sender, EventArgs e)
        {
            cmbEstado.Text = "ACTIVO";
            llenarComboLocalidad();
            llenarGrid(0);
        }

        private void btnCerrarCanImpre_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == "")
            {
                llenarGrid(0);
            }

            else
            {
                llenarGrid(1);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTodo();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDatos.Columns[0].Visible = true;
            iIdTerminal = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtNombreEquipo.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            TxtIPAsignada.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
            cmbEstado.Text = dgvDatos.CurrentRow.Cells[5].Value.ToString();

            dgvDatos.Columns[0].Visible = false;
            txtCodigo.ReadOnly = true;
            btnEliminar.Enabled = true;
            btnNuevo.Text = "Actualizar";
            grupoDatos.Enabled = true;
            txtDescripcion.Focus();
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEliminar.Enabled = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (iIdTerminal != 0)
            {
                eliminarRegistro(iIdTerminal);
            }

            else
            {
                dgvDatos.Columns[0].Visible = true;
                iIdTerminal = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value);
                dgvDatos.Columns[0].Visible = false;
                eliminarRegistro(iIdTerminal);
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.Text == "ACTIVO")
            {
                sEstado = "A";
            }

            else
            {
                sEstado = "E";
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtBusqueda.Text == "")
                {
                    llenarGrid(0);
                }

                else
                {
                    llenarGrid(1);
                }
            }
        }
    }
}
