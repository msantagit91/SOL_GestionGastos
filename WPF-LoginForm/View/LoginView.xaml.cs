using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using WPF_LoginForm.Model;
using WPF_LoginForm.Data;
using WPF_LoginForm.Helpers;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            RegisterTransform.X = 0;
        }

        #region "Metodos"
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUser.Text.ToString();
            string contrasena = txtPass.Password.ToString();

            D_Usuario d_usuario = new D_Usuario();
            string respuesta = d_usuario.Login(user,contrasena);

            if (respuesta == "OK")
            {
                
                MainView dashboard = new MainView();
                dashboard.Show();

                this.Close();
                               
            
            }
            else
            {
                ToastHelper.Mostrar("Credenciales Incorrectas", this);
                
                txtUser.Clear();
                txtPass.Clear();
            }

                      
        }

        private void MostrarRegistro()
        {
            DoubleAnimation regAnim = new DoubleAnimation();
            regAnim.From = 0;
            regAnim.To = -350;
            regAnim.Duration = TimeSpan.FromMilliseconds(500);

            RegisterTransform.BeginAnimation(
                TranslateTransform.XProperty,
                regAnim);

            DoubleAnimation loginAnim = new DoubleAnimation();
            loginAnim.From = 0;
            loginAnim.To = -120;
            loginAnim.Duration = TimeSpan.FromMilliseconds(500);

            LoginTransform.BeginAnimation(
                TranslateTransform.XProperty,
                loginAnim);
        }

        private void OcultarRegistro()
        {
            DoubleAnimation regAnim = new DoubleAnimation();
            regAnim.From = -350;
            regAnim.To = 0;
            regAnim.Duration = TimeSpan.FromMilliseconds(500);

            RegisterTransform.BeginAnimation(
                TranslateTransform.XProperty,
                regAnim);

            DoubleAnimation loginAnim = new DoubleAnimation();
            loginAnim.From = -120;
            loginAnim.To = 0;
            loginAnim.Duration = TimeSpan.FromMilliseconds(500);

            LoginTransform.BeginAnimation(
                TranslateTransform.XProperty,
                loginAnim);
        }


        #endregion

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            MostrarRegistro();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            OcultarRegistro();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            
            M_Usuario usuario = new M_Usuario();

            usuario.nombre = txNombre.Text;
            usuario.usuario = txUsername.Text.Trim();
            usuario.contrasena = txtPassword.Password.Trim();

            D_Usuario user = new D_Usuario();
            string respuesta = user.CrearUsuario(usuario);

            if (respuesta == "OK")
            {
                
                ToastHelper.Mostrar("Usuario registrado correctamente", this);
                
                
                txNombre.Clear();
                txUsername.Clear();
                txtPassword.Clear();

                OcultarRegistro();

            }
            else
            {
                ToastHelper.Mostrar(respuesta,this);
            }

            
        }
    }
}
