using System.Data;
using System.Data.SqlClient;


namespace WPF_LoginForm.Data
{
    public class D_Reporte
    {
        public DataTable ReporteGastosMes(int idUsuario)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = Conexion.CrearInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_REPORTE_GASTOS_MES", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }

            return tabla;
        }
    }
}