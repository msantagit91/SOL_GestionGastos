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
using WPF_LoginForm.Helpers;
using WPF_LoginForm.UserControls;

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            txtBienvenido.Text = $"Bienvenido, {SesionUsuario.Nombre}";

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_Gastos(Helpers.SesionUsuario.IdUsuario);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_Home();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_Categorias();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new UC_Reportes();
        }
    }
}
