using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Formularios
{
    public partial class FInformacionPosSecMes : MaterialForm
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

        int iIdPosSeccionMesa;
        int iCuenta;

        public FInformacionPosSecMes()
        {
            InitializeComponent();
        }

        private void FInformacionPosSecMes_Load(object sender, EventArgs e)
        {
            llenarGrid(0);
            cmbEstado.Text = "ACTIVO";
            cargarColores();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA CARGAR LOS COLORES 
        private void cargarColores()
        {
            try
            {
                cmbPaleta.Items.Clear();
                cmbPaleta.Items.Add(Color.Azure);
                cmbPaleta.Items.Add(Color.LightSteelBlue);
                cmbPaleta.Items.Add(Color.MediumPurple);
                cmbPaleta.Items.Add(Color.LightCoral);
                cmbPaleta.Items.Add(Color.Chocolate);
                cmbPaleta.Items.Add(Color.NavajoWhite);
                cmbPaleta.Items.Add(Color.Gold);
                cmbPaleta.Items.Add(Color.GreenYellow);
                cmbPaleta.Items.Add(Color.Wheat);
                cmbPaleta.Items.Add(Color.DarkTurquoise);

                cmbPaleta.SelectedIndex = 0;
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }
        
        //LIPIAR LAS CAJAS DE TEXTO
        private void limpiarTodo()
        {
            iIdPosSeccionMesa = 0;
            txtBuscar.Clear();
            txtCodigo.Enabled = true;
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtRuta.Clear();
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
                sSql += "select id_pos_seccion_mesa, codigo as CODIGO," + Environment.NewLine;
                sSql += "descripcion as DESCRIPCION, color as COLOR," + Environment.NewLine;
                sSql += "CASE estado when 'A' then 'ACTIVO' else 'INACTIVO' end as ESTADO," + Environment.NewLine;
                sSql += "isnull(fondo_pantalla, '') fondo_pantalla" + Environment.NewLine;
                sSql += "from pos_seccion_mesa" + Environment.NewLine;
                sSql += "where estado in ('A', 'N')" + Environment.NewLine;

                if (iOp == 1)
                {
                    sSql += "and (codigo like '%' + '" + txtBuscar.Text.Trim() + "' + '%'" + Environment.NewLine;
                    sSql += "or descripcion like '%' + '" + txtBuscar.Text.Trim() + "' + '%')" + Environment.NewLine;
                    sSql += "" + Environment.NewLine;
                }

                sSql += "order by id_pos_seccion_mesa";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    dgvDatos.DataSource = dtConsulta;
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[5].Visible = false;
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
                sSql += "insert into pos_seccion_mesa (" + Environment.NewLine;
                sSql += "codigo, descripcion, color, fondo_pantalla, estado," + Environment.NewLine;
                sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                sSql += "values(" + Environment.NewLine;
                sSql += "'" + txtCodigo.Text.Trim() + "', '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "'" + cmbPaleta.Text + "', '" + txtRuta.Text.Trim().ToLower() + "', 'A', GETDATE()," + Environment.NewLine;
                sSql += "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";

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
                sSql += "update pos_seccion_mesa set" + Environment.NewLine;
                sSql += "descripcion = '" + txtDescripcion.Text.Trim() + "'," + Environment.NewLine;
                sSql += "color = '" + cmbPaleta.Text + "'," + Environment.NewLine;
                sSql += "fondo_pantalla = '" + txtRuta.Text.Trim().ToLower() + "'," + Environment.NewLine;
                sSql += "estado = '" + sEstado + "'" + Environment.NewLine;
                sSql += "where id_pos_seccion_mesa = " + iIdPosSeccionMesa + Environment.NewLine;
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

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }
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
                sSql += "update pos_seccion_mesa set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'" + Environment.NewLine;
                sSql += "where id_pos_seccion_mesa = " + iIdPosSeccionMesa + Environment.NewLine;

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
                sSql += "from pos_seccion_mesa" + Environment.NewLine;
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
                sSql += "from pos_mesa" + Environment.NewLine;
                sSql += "where id_pos_seccion_mesa = " + iIdPosSeccionMesa + Environment.NewLine;
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

        #endregion

        private void Btn_CerrarPosSecMes_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_LimpiarPosSecMes_Click(object sender, EventArgs e)
        {
            Grb_Dato.Enabled = false;
            btnNuevo.Text = "Nuevo";
            limpiarTodo();
        }

        private void Btn_BuscarPosSecMes_Click(object sender, EventArgs e)
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

        private void BtnNuevoPosSecMes_Click(object sender, EventArgs e)
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

        private void Btn_AnularPosSecMes_Click(object sender, EventArgs e)
        {
            iCuenta = consultarRegistroEliminar();

            if (iCuenta > 0)
            {
                ok.lblMensaje.Text = "Existen registros de mesas asociadas a la sección a eliminar.";
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
                
        private void cmbPaleta_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb == null) return;
            if (e.Index < 0) return;
            if (!(cmb.Items[e.Index] is Color)) return;
            Color color = (Color)cmb.Items[e.Index];
            // Dibujamos el fondo
            e.DrawBackground();
            // Creamos los objetos GDI+
            Brush brush = new SolidBrush(color);
            Pen forePen = new Pen(e.ForeColor);
            Brush foreBrush = new SolidBrush(e.ForeColor);
            // Dibujamos el borde del rectángulo
            e.Graphics.DrawRectangle(
                forePen,
                new Rectangle(e.Bounds.Left + 2, e.Bounds.Top + 2, 19,
                    e.Bounds.Size.Height - 4));
            // Rellenamos el rectángulo con el Color seleccionado
            // en la combo
            e.Graphics.FillRectangle(brush,
                new Rectangle(e.Bounds.Left + 3, e.Bounds.Top + 3, 18,
                    e.Bounds.Size.Height - 5));
            // Dibujamos el nombre del color
            e.Graphics.DrawString(color.Name, cmb.Font,
                foreBrush, e.Bounds.Left + 25, e.Bounds.Top + 2);
            // Eliminamos objetos GDI+
            brush.Dispose();
            forePen.Dispose();
            foreBrush.Dispose();
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

                iIdPosSeccionMesa = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                txtCodigo.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();

                if ((dgvDatos.CurrentRow.Cells[3].Value.ToString() != "") && (dgvDatos.CurrentRow.Cells[3].Value.ToString() != ""))
                {
                    cmbPaleta.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
                }
                else
                {
                    cargarColores();
                }

                cmbEstado.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
                txtRuta.Text = dgvDatos.CurrentRow.Cells[5].Value.ToString();
            }

            catch (Exception ex)
            {
                catchMensaje.lblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            //abrir.InitialDirectory = "c:\\";
            abrir.Filter = "Archivos imagen (*.jpg; *.png; *.jpeg)|*.jpg;*.png;*.jpeg";
            abrir.Title = "Seleccionar archivo";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = abrir.FileName;
            }
        }
    }
}
