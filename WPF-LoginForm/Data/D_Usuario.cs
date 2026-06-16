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
    public class D_Usuario
    {
        public string CrearUsuario(Model.M_Usuario Usuario) 
        {
            
            
            string respuesta = "";
            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();
                SqlCommand comando = new SqlCommand("SP_REGISTRAR_USUARIO", con);
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Usuario.nombre;
                comando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = Usuario.usuario;
                comando.Parameters.Add("@contrasena", SqlDbType.VarChar).Value = Usuario.contrasena;
                con.Open();
                comando.ExecuteNonQuery();
                respuesta = "OK";

            }

            catch (Exception ex)
            {
                respuesta = ex.Message;

            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return respuesta;
        }

        public string Login(string usuario, string contrasena)
        {
            string respuesta = "";

            SqlConnection con = new SqlConnection();

            try
            {
                con = Conexion.CrearInstancia().CrearConexion();

                SqlCommand comando = new SqlCommand("SP_LOGIN", con);

                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                comando.Parameters.Add("@contrasena", SqlDbType.VarChar).Value = contrasena;

                con.Open();

                SqlDataReader dr = comando.ExecuteReader();

                if (dr.Read())
                {
                    SesionUsuario.IdUsuario =
                        Convert.ToInt32(dr["id_usuario"]);

                    SesionUsuario.Nombre =
                        dr["nombre"].ToString();

                    SesionUsuario.Usuario =
                        dr["usuario"].ToString();

                    respuesta = "OK";
                }
                else
                {
                    respuesta = "";
                }
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
