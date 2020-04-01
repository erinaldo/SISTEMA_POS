using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturador
{
    public partial class frmControlDatosCliente : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();

        string sSql;
        DataTable dtConsulta;
        bool bRespuesta = false;

        //VARIABLES PARA RETORNAR AL FORMULARIO FACTURADOR
        public int iCodigo;
        public string sIdentificacion;
        public string sNombre;
        public string sApellido;
        public string sDireccion;
        public string sTelefono;
        public string sMail;

        public frmControlDatosCliente()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA COLUMNAS
        private void columnasGrid()
        {            
            dgvDatos.Columns[1].Width = 100;
            dgvDatos.Columns[2].Width = 180;
            dgvDatos.Columns[3].Width = 180;
            dgvDatos.Columns[4].Width = 180;
            dgvDatos.Columns[5].Width = 240;
            dgvDatos.Columns[6].Width = 100;
            dgvDatos.Columns[7].Width = 100;

            dgvDatos.Columns[0].Visible = false;
        }

        //FUNCION PARA CONSULTAR DATOS Y LLENAR EL GRID
        private void consultarRegistros(int op)
        {
            try
            {
                sSql = "";
                sSql = sSql + "select TP.id_persona as CÓDIGO, TP.identificacion as 'Cédula/RUC'," + Environment.NewLine;
                sSql = sSql + "TP.apellidos as 'APELLIDO DEL CLIENTE', TP.nombres as 'NOMBRE DEL CLIENTE', " + Environment.NewLine;
                sSql = sSql + "TP.correo_electronico as 'CORREO ELETRÓNICO'," + Environment.NewLine;
                sSql = sSql + "TD.direccion + ', ' + TD.calle_principal + ' ' + TD.numero_vivienda + ' ' + TD.calle_interseccion as 'DIRECCIÓN'," + Environment.NewLine;
                sSql = sSql + "TT.oficina as 'TELÉFONO', TT.celular as 'CELULAR'" + Environment.NewLine;
                sSql = sSql + "from tp_personas TP LEFT OUTER JOIN" + Environment.NewLine;
                sSql = sSql + "tp_direcciones TD on TP.id_persona = TD.id_persona and TP.estado = 'A' and TD.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                sSql = sSql + "tp_telefonos TT ON TP.id_persona = TT.id_persona and TT.estado = 'A'" + Environment.NewLine;

                if (op == 1)
                {
                    sSql = sSql + "where TP.identificacion like '%' + '" + txtIdentificacion.Text.Trim() + "' + '%'";
                }

                else if (op == 2)
                {
                    sSql = sSql + "where (TP.nombres like '%' + '" + txtNombres.Text.Trim() + 
                                  "' + '%' or TP.apellidos like '%' + '" + txtNombres.Text.Trim() + "' + '%')";
                }

                else if (op == 3)
                {
                    sSql = sSql + "where (TD.direccion like '%' + '" + txtDireccion.Text.Trim() + 
                                  "' + '%' or TD.referencia like '%' + '" + txtDireccion.Text.Trim() + "' + '%')";
                }

                else if (op == 4)
                {
                    sSql = sSql + "where (TT.oficina like '%' + '" + txtTelefono.Text.Trim() + 
                                  "' + '%' or TT.celular like '%' + '" + txtTelefono.Text.Trim() + "' + '%')";
                }

                sSql = sSql + Environment.NewLine + "order by TP.id_persona";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        columnasGrid();
                        dgvDatos.CurrentCell = dgvDatos.Rows[0].Cells[1];
                        dgvDatos.Rows[0].Selected = true;

                        lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";

                        goto fin;
                    }

                    else
                    {
                        //ok.LblMensaje.Text = "No se encontraron registros en la consulta.";
                        dgvDatos.DataSource = dtConsulta;
                        columnasGrid();

                        lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                        //ok.ShowDialog();
                        goto fin;
                    }
                }

                else
                {
                    ok.LblMensaje.Text = "Ocurrió un problema al recuperar los registros.";
                    //lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                    ok.ShowDialog();
                    goto fin;
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                //lblCuentaRegistros.Text = dgvDatos.Rows.Count.ToString() + " Registros Encontrados.";
                catchMensaje.ShowDialog();
                goto fin;
            }
        fin:
            { }
        }

        //FUNCION PARA RECUPERAR LOS DATOS Y REGRESAR AL FORMULARIO PADRE
        private void recuperarDatos()
        {
            dgvDatos.Columns[0].Visible = true;
            iCodigo = Convert.ToInt32(dgvDatos.CurrentRow.Cells[0].Value.ToString());
            sIdentificacion = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            sNombre = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            sApellido = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            sMail = dgvDatos.CurrentRow.Cells[4].Value.ToString();
            sDireccion = dgvDatos.CurrentRow.Cells[5].Value.ToString();

            if (dgvDatos.CurrentRow.Cells[6].Value.ToString() != "")
            {
                sTelefono = dgvDatos.CurrentRow.Cells[6].Value.ToString();
            }

            else if (dgvDatos.CurrentRow.Cells[7].Value.ToString() != "")
            {
                sTelefono = dgvDatos.CurrentRow.Cells[7].Value.ToString();
            }

            else
            {
                sTelefono = "29999999";
            }

            dgvDatos.Columns[0].Visible = false;
            this.DialogResult = DialogResult.OK; 
        }

        #endregion

        private void txtIdentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtNombres.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                consultarRegistros(1);
                txtIdentificacion.Focus();
                txtIdentificacion.SelectionStart = txtIdentificacion.Text.Trim().Length;
            }
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtIdentificacion.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                consultarRegistros(2);
                txtNombres.Focus();
                txtNombres.SelectionStart = txtNombres.Text.Trim().Length;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtIdentificacion.Clear();
                txtNombres.Clear();
                txtDireccion.Clear();
                consultarRegistros(4);
                txtTelefono.Focus();
                txtTelefono.SelectionStart = txtTelefono.Text.Trim().Length;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtIdentificacion.Clear();
                txtTelefono.Clear();
                txtNombres.Clear();
                consultarRegistros(3);
                txtDireccion.Focus();
                txtDireccion.SelectionStart = txtDireccion.Text.Trim().Length;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text.Trim() != "")
            {
                txtNombres.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                consultarRegistros(1);
                txtIdentificacion.Focus();
                txtIdentificacion.SelectionStart = txtIdentificacion.Text.Trim().Length;
            }

            else if (txtNombres.Text.Trim() != "")
            {
                txtIdentificacion.Clear();
                txtTelefono.Clear();
                txtDireccion.Clear();
                consultarRegistros(2);
                txtNombres.Focus();
                txtNombres.SelectionStart = txtNombres.Text.Trim().Length;
            }

            else if (txtTelefono.Text.Trim() != "")
            {
                txtIdentificacion.Clear();
                txtNombres.Clear();
                txtDireccion.Clear();
                consultarRegistros(4);
                txtTelefono.Focus();
                txtTelefono.SelectionStart = txtTelefono.Text.Trim().Length;
            }

            else if (txtDireccion.Text.Trim() != null)
            {
                txtIdentificacion.Clear();
                txtTelefono.Clear();
                txtNombres.Clear();
                consultarRegistros(3);
                txtDireccion.Focus();
                txtDireccion.SelectionStart = txtDireccion.Text.Trim().Length;
            }

            //if (txtIdentificacion.Text == "")
            //{
            //    ok.LblMensaje.Text = "El campo identificación se encuentra vacío. \nEstá opción solo busca por número de identificación.";
            //    ok.ShowInTaskbar = false;
            //    ok.ShowDialog();
            //    txtIdentificacion.Focus();
            //}

            else
            {
                consultarRegistros(1);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            recuperarDatos();
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            recuperarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmNuevoCliente nuevoCliente = new frmNuevoCliente(txtIdentificacion.Text.Trim(), false);
            nuevoCliente.ShowDialog();

            if (nuevoCliente.DialogResult == DialogResult.OK)
            {
                txtIdentificacion.Text = nuevoCliente.sIdentificacion;                
                consultarRegistros(1);                
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count == 0)
            {
                ok.LblMensaje.Text = "No se ha seleccionado ningún registro para editar la información.";
                ok.ShowDialog();
            }
            else
            {
                frmNuevoCliente nuevoCliente = new frmNuevoCliente(dgvDatos.CurrentRow.Cells[1].Value.ToString(), false);
                nuevoCliente.ShowDialog();

                if (nuevoCliente.DialogResult == DialogResult.OK)
                {
                    txtIdentificacion.Text = nuevoCliente.sIdentificacion;
                    consultarRegistros(1);
                }
            }
        }

        private void frmControlDatosCliente_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtIdentificacion;
        }

        private void frmControlDatosCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView grid = ((DataGridView)sender);
            string rowIndex = (e.RowIndex + 1).ToString();
            Font rowFont = new System.Drawing.Font("Tahoma", 8.0F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;

            Rectangle headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIndex, rowFont, SystemBrushes.ControlText, headerBounds, centerFormat);
 
        }
    }
}
