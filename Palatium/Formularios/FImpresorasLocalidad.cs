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


namespace Palatium.Formularios
{
    public partial class FImpresorasLocalidad : Form
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();
        bool modificar = false;
        string[] G_st_datos = new string[2];
        DataTable dt = new DataTable();
        string T_st_sql = "";
        string T_st_sql2 = "";
        string valReco;
        string valReco2;
        string estado = "A";
        DataTable dtlocalidad;
        int idImpresora;
        int id_localidad_secuencia = 1;
        int numeronotaventa = 1;
        int numeronotaentrega = 1;
        int numero_replica_trigger = 1;
        int numero_control_replica=1;
        bool x = false;

        DataTable dtConsulta;
        string sSql;
        bool bRespuesta;

        public FImpresorasLocalidad()
        {
            InitializeComponent();
        }

        private void FImpresorasLocalidad_Load(object sender, EventArgs e)
        {
            LLenarComboLocali();
        }

        public void Limpiar()
        {
            txtNomEmpresa.Text = "";
            txtPuertoEmpresa.Text = "";
            txtCotizacion.Text = "";
            txtPedido.Text = "";
            txtFactura.Text = "";
            txtCredito.Text = "";
            txtRemision.Text = "";
            txtAnticipo.Text = "";
            txtDebito.Text = "";
            txtPago.Text = "";
            txtSerieB.Text = "";
            //LLenarComboCompra();

        }

        //Llenar combo tipo Empresa
        private void LLenarComboLocali()
        {
            try
            {
                dtlocalidad = new DataTable();
                string sql = "select id_localidad,nombre_localidad from tp_vw_localidades";
                cmbLocalidad.llenar(dtlocalidad, sql);
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al realizar la consulta");
            }
        }


        //FUNCION PARA LLENAR EL GRID
        private void llenarGrid()
        {
            try
            {
                sSql = "select IM.id_localidad_impresora, VL.nombre_localidad, IM.nombre_impresora, " +
                       "IM.Puerto_Impresora, IM.Numero_Cotizacion,IM.Numero_Pedido,IM.Numero_Factura," +
                       "IM.Numero_Nota_Credito, IM.Numero_Pago, IM.Numero_Nota_Debito,IM.Numero_Guia_Remision," +
                       "IM.NumeroAnticipoCliente,IM.numeroPagoSerieB from tp_localidades LO, tp_localidades_impresoras IM, " +
                       "tp_vw_localidades VL where IM.id_localidad = LO.id_localidad and VL.id_localidad = LO.id_localidad " +
                       "and IM.estado = 'A' and LO.estado = 'A' and IM.id_localidad_impresora = " + Convert.ToInt32(txtIdLocalidad.Text.Trim());

                dtConsulta = new DataTable();
                dtConsulta.Clear();

                bRespuesta = conexion.GFun_Lo_Busca_Registro(dtConsulta, sSql);

                if (bRespuesta == true)
                {
                    if (dtConsulta.Rows.Count > 0)
                    {
                        dgvProdCategoria.DataSource = dtConsulta;
                        dgvProdCategoria.Columns[0].Visible = false;
                    }
                }

                else
                {
                    MessageBox.Show("Ocurrió un problema al realizar la consulta.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un problema al realizar la consulta.");
            }
        }


        //FUNCION PARA MODIFICAR REGISTROS EN LA BASE DE DATOS impresoras por localidades
        private void actualizarRegistro(string F_st_query, string F_st_mensaje)
        {
            try
            {

                x = conexion.GFun_Lo_Rellenar_Grid(F_st_query, "tp_localidades_impresoras");

                if (x == true)
                {
                    MessageBox.Show(F_st_mensaje);
                }
                else
                {
                    MessageBox.Show("Error al modificar el registro");
                }

                grpDatolocalidad.Enabled = false;
                btnAgregar.Text = "Nuevo";
                Limpiar();

                llenarGrid();

            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrió un problema al guardar el registro.", "Aviso", MessageBoxButtons.OK);
                grpDatolocalidad.Enabled = false;
                btnAgregar.Text = "Nuevo";
                Limpiar();

                llenarGrid();
            }
        }


        private void btnListCategoria_Click(object sender, EventArgs e)
        {
            Formularios.FAyudaLocalidades ayuda1 = new Formularios.FAyudaLocalidades();
            ayuda1.ShowDialog();
            txtNomLocalidad.Text = Convert.ToString(ayuda1.nombre);
            txtIdLocalidad.Text = ayuda1.impresora;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            llenarGrid();
            Limpiar();
            Grb_listReProCategori.Enabled = true;
        }

        private void dgvProdCategoria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregar.Text = "Actualizar";
            grpDatolocalidad.Enabled = true;
            idImpresora = Convert.ToInt32(dgvProdCategoria.CurrentRow.Cells[0].Value.ToString());
            cmbLocalidad.Text = dgvProdCategoria.CurrentRow.Cells[1].Value.ToString();
            txtNomEmpresa.Text = dgvProdCategoria.CurrentRow.Cells[2].Value.ToString();
            txtPuertoEmpresa.Text = dgvProdCategoria.CurrentRow.Cells[3].Value.ToString(); ;

            txtCotizacion.Text = dgvProdCategoria.CurrentRow.Cells[4].Value.ToString();
            txtPedido.Text = dgvProdCategoria.CurrentRow.Cells[5].Value.ToString();

            txtFactura.Text = dgvProdCategoria.CurrentRow.Cells[6].Value.ToString();
            txtCredito.Text = dgvProdCategoria.CurrentRow.Cells[7].Value.ToString();

            txtRemision.Text = dgvProdCategoria.CurrentRow.Cells[10].Value.ToString();
            txtAnticipo.Text = dgvProdCategoria.CurrentRow.Cells[11].Value.ToString();
            txtDebito.Text = dgvProdCategoria.CurrentRow.Cells[9].Value.ToString();
            txtPago.Text = dgvProdCategoria.CurrentRow.Cells[8].Value.ToString();
            txtSerieB.Text = dgvProdCategoria.CurrentRow.Cells[12].Value.ToString();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string T_st_query = "";
            string T_st_query2 = "";
            string T_st_query3 = "";
            string T_st_query4 = "";
            string T_st_sql3 = "";
            string T_st_sql4 = "";
            string T_st_mensaje = "";
            double precioVenta = 0, iva, porcien10;


            if (btnAgregar.Text == "Nuevo")
            {
                grpDatolocalidad.Enabled = true;
                //Grb_Dato.Enabled = false;
                Grb_listReProCategori.Enabled = true;
                cmbLocalidad.Focus();
                btnAgregar.Text = "Guardar";
            }
            else
            {
                if (btnAgregar.Text == "Guardar")
                {
                    string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " 
                        + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                    //llamo a la funcion que iniciara un begin transAction (se graba en una tabla temporal) y Program.G_INICIA_TRANSACCION devuelve true si abrio bn la transaccion
                    if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                    {
                        MessageBox.Show("Error al abrir transacción");
                        Limpiar();
                    }
                    else
                    {
                        T_st_sql = "insert into tp_localidades_impresoras (Id_localidad,id_localidad_secuencia,Nombre_Impresora,"+
                            "Puerto_Impresora,Numero_Cotizacion,Numero_Pedido,Numero_Factura,Numero_Nota_Credito,Numero_Pago,"+
                            "Numero_Nota_Debito,Numero_Guia_Remision,NumeroAnticipoCliente,numeropagoserieb,numeronotaventa,"+
                            "numeronotaentrega,numero_replica_trigger,numero_control_replica,estado,fecha_ingreso,usuario_ingreso,"+
                            "terminal_ingreso)values('" + cmbLocalidad.SelectedValue.ToString() + "','" + id_localidad_secuencia + "',"+
                            "'" + txtNomEmpresa.Text.ToString().Trim() + "','" + txtPuertoEmpresa.Text.ToString().Trim() + "',"+
                            "'" + txtCotizacion.Text.ToString().Trim() + "', '" + txtPedido.Text.ToString().Trim() + "', "+
                            "'" + txtFactura.Text.ToString().Trim() + "', '" + txtCredito.Text.ToString().Trim() + "',"+
                            " '" + txtPago.Text.ToString().Trim() + "', '" + txtDebito.Text.ToString().Trim() + "',"+
                            " '" + txtRemision.Text.ToString().Trim() + "', '" + txtAnticipo.Text.ToString().Trim() + "',"+
                            " '" + txtSerieB.Text.ToString().Trim() + "', '" + numeronotaventa + "', '" + numeronotaentrega + "', "+
                            "'" + numero_replica_trigger + "', '" + numero_control_replica + "','A', GETDATE(), "+
                            "'" + Program.sDatosMaximo[0] + "', '" + Program.sDatosMaximo[1] + "')";
                        //sisque no me ejuta el query 
                        if (!conexion.GFun_Lo_Ejecuta_SQL(T_st_sql))
                        {
                            //hara el rolBAck
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        }
                        else
                        {
                            //si no se ejecuta bien hara un commit
                            conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                            MessageBox.Show("Registro ingresado correctamente");
                            btnAgregar.Text = "Nuevo";
                            //Grb_Dat.Enabled = false;
                            Limpiar();
                        }
                    }
                }

                //SI EL BOTON ESTA EN OPCION ACTUALIZAR
                else if (btnAgregar.Text == "Actualizar")
                {
                    T_st_query = "update tp_localidades_impresoras set Id_localidad  = '" + cmbLocalidad.SelectedValue.ToString() + "', "+
                        " id_localidad_secuencia = '" + id_localidad_secuencia + "', Nombre_Impresora = "+
                        "'" + txtNomEmpresa.Text.ToString().Trim() + "', Puerto_Impresora = '" + txtPuertoEmpresa.Text.ToString().Trim() + "', "+
                        " Numero_Cotizacion = '" + txtCotizacion.Text.ToString().Trim() + "',"+
                        " Numero_Pedido = '" + txtPedido.Text.ToString().Trim() + "', Numero_Factura = "+
                        "'" + txtFactura.Text.ToString().Trim() + "', Numero_Nota_Credito = '" + txtCredito.Text.ToString().Trim() + "', "+
                        "Numero_Pago = '" + txtPago.Text.ToString().Trim() + "', Numero_Nota_Debito = '" + txtDebito.Text.ToString().Trim() + "',"+
                        " Numero_Guia_Remision = '" + txtRemision.Text.ToString().Trim() + "', NumeroAnticipoCliente = "+
                        "'" + txtAnticipo.Text.ToString().Trim() + "', numeropagoserieb = '" + txtSerieB.Text.ToString().Trim() + "',"+
                        " numeronotaventa = '" + numeronotaventa + "', numeronotaentrega = '" + numeronotaentrega + "',"+
                        " numero_replica_trigger = '" + numero_replica_trigger + "', numero_control_replica = "+
                        " '" + numero_control_replica + "' where id_localidad_impresora = " + idImpresora + "";

                    T_st_mensaje = "Registro Actualizado Ëxitosamente";
                    actualizarRegistro(T_st_query, T_st_mensaje);

                }
            }

            


        }

        private void txtCotizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtRemision_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtAnticipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtDebito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void txtSerieB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarNum_Letra_decimal.soloNumeros(e);
        }

        private void btnLimpiarCajero_Click(object sender, EventArgs e)
        {
            Limpiar();
            btnAgregar.Text = "Nuevo";
            grpDatolocalidad.Enabled = false;
        }

        private void btnNuevCategoria_Click(object sender, EventArgs e)
        {
            grpDatolocalidad.Enabled = false;
            Grb_localidad.Enabled = true;
            Grb_listReProCategori.Enabled = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea dar de baja el registro?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string t_st_fecha = (DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + " " 
                    + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
                string T_st_query = "update tp_localidades_impresoras set estado = 'E', fecha_anula = GETDATE(), "+
                    "usuario_anula = '" + Program.sDatosMaximo[0] + "', terminal_anula = '" + Program.sDatosMaximo[1] + "' " +
                    " where id_localidad_impresora = " + idImpresora + "";
                string T_st_mensaje = "Registro Eliminado Exitosamente";
                actualizarRegistro(T_st_query, T_st_mensaje);
            }
            else
            {
                MessageBox.Show("Se canceló la eliminacion", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Grb_localidad.Enabled = false;
                btnAgregar.Text = "Nuevo";
                Limpiar();

                llenarGrid();
            }
        }
    }
}
