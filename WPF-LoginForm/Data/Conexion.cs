using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Data
{
    public class Conexion
    {
            private string Base;
            private string Servidor;
            private string Usuario;
            private string Clave;
            private static Conexion Con = null;

            //Constructor de la clase
            private Conexion()
            {
                this.Servidor = "DESKTOP-Q03BGKU";
                this.Base = "db_GestionGastos";
                this.Usuario = "moi_sa";
                this.Clave = "123456";
            }

            public SqlConnection CrearConexion()
            {
                SqlConnection cadena = new SqlConnection();
                try
                {
                    cadena.ConnectionString = "Server=" + this.Servidor +
                                              "; Database=" + this.Base +
                                              "; User Id=" + this.Usuario +
                                              "; Password=" + this.Clave;
                }
                catch (Exception ex)
                {
                    cadena = null;
                    throw ex;
                }
                return cadena;
            }

            public static Conexion CrearInstancia()
            {
                if (Con == null)
                {
                    Con = new Conexion();
                }
                return Con;
            }
     }
}
