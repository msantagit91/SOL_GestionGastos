using System;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Model;

namespace WPF_LoginForm.Data
{
    public class D_Dashboard
    {
        public M_Dashboard ObtenerResumen(int idUsuario)
        {
            M_Dashboard resumen = new M_Dashboard();
            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand cmd = new SqlCommand("SP_DASHBOARD_RESUMEN", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    resumen.TotalGastado = Convert.ToDecimal(dr["total_gastado"]);
                    resumen.GastoMes = Convert.ToDecimal(dr["gasto_mes"]);
                    resumen.CantidadGastos = Convert.ToInt32(dr["cantidad_gastos"]);
                }
            }
            catch
            {
                resumen = null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return resumen;
        }

        public DataTable UltimosGastos(int idUsuario)
        {
            DataTable tabla = new DataTable();
            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand cmd = new SqlCommand("SP_ULTIMOS_GASTOS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
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
    }
}
