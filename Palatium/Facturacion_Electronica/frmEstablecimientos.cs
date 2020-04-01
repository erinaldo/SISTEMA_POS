using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Palatium.Facturacion_Electronica
{
    public partial class frmEstablecimientos : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        Clases.ClaseValidarCaracteres caracteres = new Clases.ClaseValidarCaracteres();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        DataTable dtConsulta;

        bool bRespuesta;
        bool bActualizar;

        string sSql;

        int iIdLocalidad;

        public frmEstablecimientos()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "";
                sSql += "Select TP.Codigo, TP.valor_texto  +" + Environment.NewLine;
                sSql += "case LOC.emite_comprobante_electronico when 1 then ' electronico' else '' end Nombres," + Environment.NewLine;
                sSql += "LOC.establecimiento 'Est.',LOC.punto_emision 'Pto. Emi.', LOC.Direccion," + Environment.NewLine;
                sSql += "case (LOC.Estado) when 'A' then 'ACTIVO' else 'INACTIVO' end Estado," + Environment.NewLine;
                sSql += "LOC.Id_localidad" + Environment.NewLine;
                sSql += "From tp_localidades LOC, tp_Codigos TP" + Environment.NewLine;
                sSql += "Where LOC.cg_localidad = TP.correlativo and LOC.Estado ='A'" + Environment.NewLine;
                sSql += "order by TP.Codigo";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dtConsulta;
                        dgvDatos.Columns[0].Width = 50;
                        dgvDatos.Columns[1].Width = 150;
                        dgvDatos.Columns[2].Width = 50;
                        dgvDatos.Columns[3].Width = 50;
                        dgvDatos.Columns[4].Width = 200;
                        dgvDatos.Columns[5].Width = 60;
                        dgvDatos.Columns[6].Visible = false;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA ACTUALIZAR EL REGISTRO
        private void actualizarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "Update tp_localidades Set" + Environment.NewLine;
                sSql += "establecimiento= '" + txtEstablecimiento.Text.Trim() + "'," + Environment.NewLine;
                sSql += "punto_emision= '" + txtPuntoEmision.Text.Trim() + "'," + Environment.NewLine;
                sSql += "Direccion= '" + txtDireccion.Text.Trim() + "'" + Environment.NewLine;
                //sSql += "Usuario_Ingreso = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                //sSql += "Terminal_Ingreso = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                //sSql += "Fecha_Ingreso = GetDate()" + Environment.NewLine;
                sSql += "Where Id_localidad = " + iIdLocalidad;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro actualizado éxitosamente.";
                ok.ShowDialog();
                llenarGrid();
                limpiar();
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

        //FUNCION PARA ELIMINAR EL REGISTRO
        private void eliminarRegistro()
        {
            try
            {
                //INICIAMOS UNA NUEVA TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowInTaskbar = false;
                    ok.ShowDialog();
                    goto fin;
                }

                sSql = "";
                sSql += "Update tp_localidades Set" + Environment.NewLine;
                sSql += "estado = 'E'," + Environment.NewLine;
                sSql += "usuario_anula = '" + Program.sDatosMaximo[0] + "'," + Environment.NewLine;
                sSql += "terminal_anula = '" + Program.sDatosMaximo[1] + "'," + Environment.NewLine;
                sSql += "fecha_anula = GetDate()" + Environment.NewLine;
                sSql += "Where id_directorio = " + iIdLocalidad;

                //EJECUTA EL QUERY DE ACTUALIZACION
                if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowInTaskbar = false;
                    catchMensaje.ShowDialog();
                    goto reversa;
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                ok.LblMensaje.Text = "Registro eliminado éxitosamente.";
                ok.ShowDialog();
                llenarGrid();
                limpiar();
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

        //FUNCION PARA LIMPIAR
        private void limpiar()
        {
            txtCodigo.Clear();
            txtNombres.Clear();
            txtEstablecimiento.Clear();
            txtPuntoEmision.Clear();
            txtDireccion.Clear();
            txtEstado.Clear();
            iIdLocalidad = 0;
            bActualizar = false;
            llenarGrid();
        }

        #endregion

        private void frmEstablecimientos_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void txtEstablecimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (iIdLocalidad != 0)
            {
                SiNo.LblMensaje.Text = "¿Está seguro que desea eliminar el registro?";
                SiNo.ShowDialog();

                if (SiNo.DialogResult == DialogResult.OK)
                {
                    eliminarRegistro();
                }
            }

            else
            {
                ok.LblMensaje.Text = "No hay registros para eliminar.";
                ok.ShowDialog();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
             if (txtNombres.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el nombre del consignatario.";
                ok.ShowDialog();
                txtNombres.Focus();
            }

            else if (txtEstablecimiento.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el número de establecimiento.";
                ok.ShowDialog();
                txtEstablecimiento.Focus();
            }

            else if (txtPuntoEmision.Text.Trim() == "")
            {
                ok.LblMensaje.Text = "Favor ingrese el número de punto de emisión.";
                ok.ShowDialog();
                txtPuntoEmision.Focus();
            }

            else
            {
                if (bActualizar == true)
                {
                    actualizarRegistro();
                }
            }
        }

        private void dgvDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCodigo.Text = dgvDatos.CurrentRow.Cells[0].Value.ToString();
            txtNombres.Text = dgvDatos.CurrentRow.Cells[1].Value.ToString();
            txtEstablecimiento.Text = dgvDatos.CurrentRow.Cells[2].Value.ToString();
            txtPuntoEmision.Text = dgvDatos.CurrentRow.Cells[3].Value.ToString();
            txtDireccion.Text = dgvDatos.CurrentRow.Cells[4].Value.ToString();
            txtEstado.Text = dgvDatos.CurrentRow.Cells[5].Value.ToString();

            iIdLocalidad = Convert.ToInt32(dgvDatos.CurrentRow.Cells[6].Value.ToString());
            bActualizar = true;
            txtEstablecimiento.Focus();
        }

        private void txtPuntoEmision_KeyPress(object sender, KeyPressEventArgs e)
        {
            caracteres.soloNumeros(e);
        }

    }
}
