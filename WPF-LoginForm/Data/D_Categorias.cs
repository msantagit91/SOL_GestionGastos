using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.Helpers;
using WPF_LoginForm.Model;
using WPF_LoginForm.UserControls;


namespace WPF_LoginForm.Data
{
    public class D_Categorias
    {
        public DataTable ListarCategoria() 
        {
            DataTable tabla = new DataTable();

            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_listar_categoria", con);
                comando.CommandType = CommandType.StoredProcedure;


                con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(comando);
                adapter.Fill(tabla);

            }

            catch (Exception ex)
            {
                string respuesta = ex.Message;

                MessageBox.Show(respuesta);
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return tabla;
        }

        public string GuardarCategoria(string descripcion)
        {
            string respuesta = "";

            using (SqlConnection con = Conexion.CrearInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_GUARDAR_CATEGORIA", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;

                con.Open();

                respuesta = cmd.ExecuteNonQuery() >= 1
                    ? "OK"
                    : "No se pudo guardar la categoría";
            }

            return respuesta;
        }

        public DataTable ListarCategoriasMante()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = Conexion.CrearInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_LISTAR_CATEGORIAS_MANTE", con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                tabla.Load(reader);
            }

            return tabla;
        }

        
    }
}
