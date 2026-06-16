using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Model;

namespace WPF_LoginForm.Data
{
    public class D_Gasto
    {

        public string Insertar(M_Gasto gasto)
        {
            string respuesta = "";

            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand cmd = new SqlCommand(
                    "SP_INSERTAR_GASTO",
                    con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@descripcion",
                    SqlDbType.VarChar).Value =
                    gasto.Descripcion;

                cmd.Parameters.Add("@monto",
                    SqlDbType.Money).Value =
                    gasto.Monto;

                cmd.Parameters.Add("@id_usuario",
                    SqlDbType.Int).Value =
                    gasto.IdUsuario;

                cmd.Parameters.Add("@id_categoria",
                    SqlDbType.Int).Value =
                    gasto.IdCategoria;

                cmd.Parameters.Add("@fecha",
                    SqlDbType.Date).Value =
                    gasto.Fecha;

                con.Open();

                respuesta =
                    cmd.ExecuteNonQuery() >= 1
                    ? "OK"
                    : "No se pudo insertar";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return respuesta;
        }

        public DataTable Listar()
        {
            DataTable tabla = new DataTable();

            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand cmd =
                    new SqlCommand("SP_LISTAR_GASTOS", con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                da.Fill(tabla);
            }
            catch
            {
                tabla = null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return tabla;
        }

        public string Editar(M_Gasto gasto)
        {
            string respuesta = "";
            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand cmd = new SqlCommand("SP_EDITAR_GASTO", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_gastos", SqlDbType.Int).Value = gasto.IdGastos;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = gasto.Descripcion;
                cmd.Parameters.Add("@monto", SqlDbType.Money).Value = gasto.Monto;
                cmd.Parameters.Add("@id_categoria", SqlDbType.Int).Value = gasto.IdCategoria;
                cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = gasto.Fecha;

                con.Open();

                respuesta = cmd.ExecuteNonQuery() >= 1
                    ? "OK"
                    : "No se pudo editar el gasto";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return respuesta;
        }

        public string Eliminar(int idGasto)
        {
            string respuesta = "";

            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand cmd =
                    new SqlCommand(
                        "SP_ELIMINAR_GASTO",
                        con);

                cmd.CommandType =
                    CommandType.StoredProcedure;

                cmd.Parameters.Add(
                    "@id_gastos",
                    SqlDbType.Int).Value = idGasto;

                con.Open();

                respuesta =
                    cmd.ExecuteNonQuery() >= 1
                    ? "OK"
                    : "No se pudo eliminar";
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return respuesta;
        }
    }
}
