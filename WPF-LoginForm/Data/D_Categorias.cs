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
    }
}
