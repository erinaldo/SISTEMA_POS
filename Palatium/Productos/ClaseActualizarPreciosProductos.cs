using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Palatium.Productos
{
    class ClaseActualizarPreciosProductos
    {
        ConexionBD.ConexionBD conexion = new ConexionBD.ConexionBD();

        DataTable dtConsulta;
        DataTable dtItems;

        int iParametroConImpuestos;
        int iIdListaMinorista;
        int iPagaIva;
        int iPagaServicio;
        int iIdProducto;

        string sSql;
        string sFechaInicio;
        string sFechaFin;
        string sUsuario;
        string sTerminal;
        public string sMensajeError;

        bool bRespuesta;

        SqlParameter[] parametro;

        Decimal dbValorRecuperado;
        Decimal dbValorInsertar;
        Decimal dbFactorDivision;
        Decimal dbFactorIVA;
        Decimal dbFactorServicio;

        public bool actualizarPrecios(DataTable dtItems_P, int iParametroConImpuestos_P, int iIdListaMinorista_P,
                                      Decimal dbFactorIVA_P, Decimal dbFactorServicio_P, string sFechaInicio_P, 
                                        string sFechaFin_P, string sUsuario_P, string sTerminal_P)
        {
            try
            {
                this.dtItems = dtItems_P;
                this.iParametroConImpuestos = iParametroConImpuestos_P;
                this.iIdListaMinorista = iIdListaMinorista_P;
                this.dbFactorIVA = dbFactorIVA_P;
                this.dbFactorServicio = dbFactorServicio_P;
                this.sFechaInicio = sFechaInicio_P;
                this.sFechaFin = sFechaFin_P;
                this.sUsuario = sUsuario_P;
                this.sTerminal = sTerminal_P;

                if (!conexion.GFun_Lo_Maneja_Transaccion(Program.G_INICIA_TRANSACCION))
                {
                    sMensajeError = "Error al iniciar la transacción de datos.";
                    return false;
                }

                for (int i = 0; i < dtItems.Rows.Count; i++)
                {
                    iIdProducto = Convert.ToInt32(dtItems.Rows[i]["id_producto"].ToString());
                    iPagaIva = Convert.ToInt32(dtItems.Rows[i]["paga_iva"].ToString());
                    iPagaServicio = Convert.ToInt32(dtItems.Rows[i]["paga_servicio"].ToString());
                    dbValorRecuperado = Convert.ToDecimal(dtItems.Rows[i]["valor"].ToString());
                    dbFactorDivision = 1;

                    if (iParametroConImpuestos == 1)
                    {
                        if (iPagaIva == 1)
                            dbFactorDivision += dbFactorIVA;

                        if (iPagaServicio == 1)
                            dbFactorDivision += dbFactorServicio;

                        dbValorInsertar = dbValorRecuperado / dbFactorDivision;
                    }

                    else
                        dbValorInsertar = dbValorRecuperado;

                    //ACTUALIZAR EL PARAMETRO DE SERVICIO E IVA EN LA TABLA CV401_PRODUCTOS
                    sSql = "";
                    sSql += "update cv401_productos set" + Environment.NewLine;
                    sSql += "paga_iva = @paga_iva," + Environment.NewLine;
                    sSql += "paga_servicio = @paga_servicio" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and estado = @estado";

                    parametro = new SqlParameter[4];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@paga_iva";
                    parametro[0].SqlDbType = SqlDbType.Int;
                    parametro[0].Value = iPagaIva;

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@paga_servicio";
                    parametro[1].SqlDbType = SqlDbType.Int;
                    parametro[1].Value = iPagaServicio;

                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@id_producto";
                    parametro[2].SqlDbType = SqlDbType.Int;
                    parametro[2].Value = iIdProducto;

                    parametro[3] = new SqlParameter();
                    parametro[3].ParameterName = "@estado";
                    parametro[3].SqlDbType = SqlDbType.VarChar;
                    parametro[3].Value = "A";

                    //EJECUTAR LA INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        sMensajeError = conexion.sMensajeError;
                        return false;
                    }

                    //CAMBIO DE ESTADO DE 'A' AL ESTADO 'E' EN LA TABLA CV403_PRECIOS_PRODUCTOS
                    sSql = "";
                    sSql += "update cv403_precios_productos set" + Environment.NewLine;
                    sSql += "estado = @estado," + Environment.NewLine;
                    sSql += "fecha_anula = GETDATE()," + Environment.NewLine;
                    sSql += "usuario_anula = @usuario_anula," + Environment.NewLine;
                    sSql += "terminal_anula = @terminal_anula" + Environment.NewLine;
                    sSql += "where id_producto = @id_producto" + Environment.NewLine;
                    sSql += "and id_lista_precio = @id_lista_precio";

                    parametro = new SqlParameter[5];
                    parametro[0] = new SqlParameter();
                    parametro[0].ParameterName = "@estado";
                    parametro[0].SqlDbType = SqlDbType.VarChar;
                    parametro[0].Value = "E";

                    parametro[1] = new SqlParameter();
                    parametro[1].ParameterName = "@usuario_anula";
                    parametro[1].SqlDbType = SqlDbType.VarChar;
                    parametro[1].Value = sUsuario;

                    parametro[2] = new SqlParameter();
                    parametro[2].ParameterName = "@terminal_anula";
                    parametro[2].SqlDbType = SqlDbType.VarChar;
                    parametro[2].Value = sTerminal_P;

                    parametro[3] = new SqlParameter();
                    parametro[3].ParameterName = "@id_producto";
                    parametro[3].SqlDbType = SqlDbType.Int;
                    parametro[3].Value = iIdProducto;

                    parametro[4] = new SqlParameter();
                    parametro[4].ParameterName = "@id_lista_precio";
                    parametro[4].SqlDbType = SqlDbType.Int;
                    parametro[4].Value = iIdListaMinorista;

                    //EJECUTAR LA INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        sMensajeError = conexion.sMensajeError;
                        return false;
                    }

                    //INSERTAR EL NUEVO REGISTRO EN LA TABLA CV403_PRECIOS_PRODUCTOS
                    sSql = "";
                    sSql += "insert into cv403_precios_productos (" + Environment.NewLine;
                    sSql += "id_lista_precio, id_producto, valor_porcentaje, valor, fecha_inicio," + Environment.NewLine;
                    sSql += "fecha_final, estado, numero_replica_trigger, numero_control_replica," + Environment.NewLine;
                    sSql += "fecha_ingreso, usuario_ingreso, terminal_ingreso)" + Environment.NewLine;
                    sSql += "values(" + Environment.NewLine;
                    sSql += "@id_lista_precio, @id_producto, @valor_porcentaje, @valor, @fecha_inicio," + Environment.NewLine;
                    sSql += "@fecha_final, @estado, @numero_replica_trigger, @numero_control_replica," + Environment.NewLine;
                    sSql += "getdate(), @usuario_ingreso, @terminal_ingreso)";

                    int j = 0;
                    parametro = new SqlParameter[11];
                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_lista_precio";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdListaMinorista;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@id_producto";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = iIdProducto;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@valor_porcentaje";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = 0;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@valor";
                    parametro[j].SqlDbType = SqlDbType.Decimal;
                    parametro[j].Value = dbValorInsertar;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@fecha_inicio";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sFechaInicio;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@fecha_final";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sFechaFin;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@estado";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = "A";
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@numero_replica_trigger";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = 0;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@numero_control_replica";
                    parametro[j].SqlDbType = SqlDbType.Int;
                    parametro[j].Value = 0;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@usuario_ingreso";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sUsuario;
                    j++;

                    parametro[j] = new SqlParameter();
                    parametro[j].ParameterName = "@terminal_ingreso";
                    parametro[j].SqlDbType = SqlDbType.VarChar;
                    parametro[j].Value = sTerminal;

                    //EJECUTAR LA INSTRUCCION SQL
                    if (!conexion.GFun_Lo_Ejecutar_SQL_Parametros(sSql, parametro))
                    {
                        conexion.GFun_Lo_Maneja_Transaccion(Program.G_REVERSA_TRANSACCION);
                        sMensajeError = conexion.sMensajeError;
                        return false;
                    }
                }

                conexion.GFun_Lo_Maneja_Transaccion(Program.G_TERMINA_TRANSACCION);
                return true;
            }

            catch (Exception ex)
            {
                sMensajeError = ex.Message;
                return false;
            }
        }
    }
}
