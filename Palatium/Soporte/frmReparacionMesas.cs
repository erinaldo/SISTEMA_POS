using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Palatium.Soporte
{
    public partial class frmReparacionMesas : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        VentanasMensajes.frmMensajeCatch catchMensaje = new VentanasMensajes.frmMensajeCatch();
        VentanasMensajes.frmMensajeOK ok = new VentanasMensajes.frmMensajeOK();
        VentanasMensajes.frmMensajeSiNo SiNo = new VentanasMensajes.frmMensajeSiNo();

        DataTable dtConsulta;

        bool bRespuesta;
        bool bCheck;

        string sFechaActual;
        string sSql;
        string sTipoOrden;
        string sFechaPedido;

        int iIdPedido;
        int iNumeroPedido;
        int iNumeroMesa;
        int iContador;

        public frmReparacionMesas()
        {
            InitializeComponent();
        }

        #region FUNCIONES DEL USUARIO

        //INGRESAR EL CURSOR AL BOTON
        private void ingresaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.Black;
            btnProceso.BackColor = Color.LawnGreen;
        }

        //SALIR EL CURSOR DEL BOTON
        private void salidaBoton(Button btnProceso)
        {
            btnProceso.ForeColor = Color.White;
            btnProceso.BackColor = Color.Navy;
        }

        //FUNCION PARA LLENAR EL COMBOBOX DEL DATAGRIDVIEW
        private void llenarCombo()
        {
            try
            {
                DataGridViewComboBoxColumn comboboxMesa = dgvDatos.Columns["colIdMesa"] as DataGridViewComboBoxColumn;

                sSql = "";
                sSql += "select M.id_pos_mesa, SM.descripcion + ' - ' + M.descripcion as descripcion" + Environment.NewLine;
                sSql += "from pos_seccion_mesa SM INNER JOIN" + Environment.NewLine;
                sSql += "pos_mesa M ON M.id_pos_seccion_mesa = SM.id_pos_seccion_mesa" + Environment.NewLine;
                sSql += "and M.estado = 'A'" + Environment.NewLine;
                sSql += "and SM.estado = 'A'";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        DataRow fila = dtConsulta.NewRow();
                        fila[dtConsulta.Columns[0].ToString()] = "0";
                        fila[dtConsulta.Columns[1].ToString()] = "Seleccionar item";
                        dtConsulta.Rows.InsertAt(fila, 0);

                        comboboxMesa.ValueMember = dtConsulta.Columns[0].ToString();
                        comboboxMesa.DisplayMember = dtConsulta.Columns[1].ToString();
                        comboboxMesa.DataSource = dtConsulta;
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //FUNCION PARA LLENAR EL  DATAGRID
        private void llenarGrid()
        {
            try
            {
                dgvDatos.Rows.Clear();

                sSql = "";
                sSql += "select CP.id_pedido, NCP.numero_pedido, isnull(CP.id_pos_mesa, '0') id_pos_mesa," + Environment.NewLine;
                sSql += "OO.descripcion, CP.fecha_pedido" + Environment.NewLine;
                sSql += "from cv403_cab_pedidos CP INNER JOIN" + Environment.NewLine;
                sSql += "cv403_numero_cab_pedido NCP ON NCP.id_pedido = CP.id_pedido" + Environment.NewLine;
                sSql += "and CP.estado = 'A'" + Environment.NewLine;
                sSql += "and NCP.estado = 'A' LEFT OUTER JOIN" + Environment.NewLine;
                sSql += "pos_mesa M ON M.id_pos_mesa = CP.id_pos_mesa" + Environment.NewLine;
                sSql += "and M.estado = 'A' INNER JOIN" + Environment.NewLine;
                sSql += "pos_origen_orden OO ON OO.id_pos_origen_orden = CP.id_pos_origen_orden" + Environment.NewLine;
                sSql += "and OO.estado = 'A'" + Environment.NewLine;
                sSql += "where CP.id_pos_origen_orden = 1" + Environment.NewLine;
                sSql += "and CP.fecha_pedido = '" + sFechaActual + "'" + Environment.NewLine;
                sSql += "and CP.id_pos_mesa is null" + Environment.NewLine;
                sSql += "order by CP.id_pedido";

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtConsulta.Rows.Count; i++)
                        {
                            iIdPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[0].ToString());
                            iNumeroPedido = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[1].ToString());
                            iNumeroMesa = Convert.ToInt32(dtConsulta.Rows[i].ItemArray[2].ToString());
                            sTipoOrden = dtConsulta.Rows[i].ItemArray[3].ToString();
                            sFechaPedido = dtConsulta.Rows[i].ItemArray[4].ToString();

                            dgvDatos.Rows.Add(iIdPedido.ToString(), iNumeroPedido.ToString(), iNumeroMesa.ToString(),
                                              sTipoOrden, sFechaPedido, false, false);

                        }
                    }
                }

                else
                {
                    catchMensaje.LblMensaje.Text = sSql;
                    catchMensaje.ShowDialog();
                }
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }
        }

        //VALIDAR SOLO NUMEROS
        private void dText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }

            else if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = false;
            }

            else
            {
                e.Handled = true;
            }
        }


        //VERIFICAR SI LA CADENA ES UN NUMERO O UN STRING
        public bool esNumero(object Expression)
        {
            bool isNum;

            double retNum;

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        //FUNCION PARA ACTUALIZAR LOS REGISTROS
        private void actualizarRegistros()
        {
            try
            {
                //INICIO DE TRANSACCION
                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    ok.LblMensaje.Text = "Error al abrir transacción";
                    ok.ShowDialog();
                    goto fin;
                }

                iContador = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    bCheck = Convert.ToBoolean(dgvDatos.Rows[i].Cells[5].Value);

                    if (bCheck == true)
                    {
                        iIdPedido = Convert.ToInt32(dgvDatos.Rows[i].Cells[0].Value);
                        iNumeroMesa = Convert.ToInt32(dgvDatos.Rows[i].Cells[1].Value);

                        sSql = "";
                        sSql += "update cv403_cab_pedidos set" + Environment.NewLine;
                        sSql += "id_pos_mesa = " + iNumeroMesa + Environment.NewLine;
                        sSql += "where id_pedido = " + iIdPedido;

                        //EJECUCIÓN DE LA INSTRUCCIÓN SQL
                        if (!conexion.GFun_Lo_Ejecuta_SQL(sSql))
                        {
                            catchMensaje.LblMensaje.Text = sSql;
                            catchMensaje.ShowDialog();
                            goto reversa;
                        }

                        dgvDatos.Rows[i].Cells[6].Value = true;
                        iContador++;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);

                ok.LblMensaje.Text = "Se han actualizado " + iContador.ToString() + " registros éxitosamente.";
                ok.ShowDialog();

                llenarGrid();
                goto fin;
            }

            catch(Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowDialog();
            }

            reversa: { conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION); }

            fin: { }
        }

        #endregion

        private void btnSubirFecha_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(btnFecha.Text);
            fecha = fecha.AddDays(1);
            sFechaActual = fecha.ToString("yyyy/MM/dd");
            btnFecha.Text = Convert.ToDateTime(sFechaActual).ToString("dd/MM/yyyy");
        }

        private void btnBajarFecha_Click(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(btnFecha.Text);
            fecha = fecha.AddDays(-1);
            sFechaActual = fecha.ToString("yyyy/MM/dd");
            btnFecha.Text = Convert.ToDateTime(sFechaActual).ToString("dd/MM/yyyy");
        }

        private void frmReparacionMesas_Load(object sender, EventArgs e)
        {
            sFechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            btnFecha.Text = sFechaActual;
        }

        private void btnFecha_Click(object sender, EventArgs e)
        {
            Pedidos.frmCalendario calendario = new Pedidos.frmCalendario(btnFecha.Text);
            calendario.ShowInTaskbar = false;
            calendario.ShowDialog();

            if (calendario.DialogResult == DialogResult.OK)
            {
                btnFecha.Text = calendario.txtFecha.Text;
            }
        }

        private void btnSubirFecha_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnSubirFecha);
        }

        private void btnSubirFecha_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnSubirFecha);
        }

        private void btnFecha_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnFecha);
        }

        private void btnFecha_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnFecha);
        }

        private void btnBajarFecha_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnBajarFecha);
        }

        private void btnBajarFecha_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnBajarFecha);
        }

        private void btnBuscar_MouseEnter(object sender, EventArgs e)
        {
            ingresaBoton(btnBuscar);
        }

        private void btnBuscar_MouseLeave(object sender, EventArgs e)
        {
            salidaBoton(btnBuscar);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //llenarCombo();
            llenarGrid();
        }

        private void dgvDatos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (esNumero(dgvDatos.Rows[e.RowIndex].Cells[2].Value.ToString()) == true)
                {
                    if (Convert.ToInt32(dgvDatos.Rows[e.RowIndex].Cells[2].Value.ToString()) == 0)
                    {
                        dgvDatos.Rows[e.RowIndex].Cells[5].Value = false;
                    }

                    else
                    {
                        dgvDatos.Rows[e.RowIndex].Cells[5].Value = true;
                    }                    
                }
            }

            catch (Exception ex)
            {
                catchMensaje.LblMensaje.Text = ex.Message;
                catchMensaje.ShowInTaskbar = false;
                catchMensaje.ShowDialog();
            }
        }

        private void dgvDatos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox texto = e.Control as TextBox;

            if (texto != null)
            {
                DataGridViewTextBoxEditingControl dTexto = (DataGridViewTextBoxEditingControl)e.Control;
                dTexto.KeyPress -= new KeyPressEventHandler(dText_KeyPress);
                dTexto.KeyPress += new KeyPressEventHandler(dText_KeyPress);
            }
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.Rows.Count > 0)
            {
                iContador = 0;

                for (int i = 0; i < dgvDatos.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvDatos.Rows[i].Cells[5].Value) == true)
                    {
                        iContador++;
                    }
                }

                if (iContador == 0)
                {
                    ok.LblMensaje.Text = "No hay registros editados para procesar la información";
                    ok.ShowDialog();
                }

                else
                {
                    SiNo.LblMensaje.Text = "¿Está seguro que desea procesar la información";
                    SiNo.ShowDialog();

                    if (SiNo.DialogResult == DialogResult.OK)
                    {
                        actualizarRegistros();
                    }
                }
            }

            else
            {
                ok.LblMensaje.Text = "No hay registros para procesar";
                ok.ShowDialog();
            }
        }
    }
}
