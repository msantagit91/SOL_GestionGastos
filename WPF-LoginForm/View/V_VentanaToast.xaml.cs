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

namespace WPF_LoginForm.View
{
    /// <summary>
    /// Lógica de interacción para V_VentanaToast.xaml
    /// </summary>
    public partial class V_VentanaToast : Window
    {
        public V_VentanaToast(string mensaje, Window owner)
        {
            InitializeComponent();

            txtMensaje.Text = mensaje;
            this.Owner = owner;

            Loaded += ToastNotification_Loaded;
        }

        private async void ToastNotification_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner != null)
            {
                Left = Owner.Left + (Owner.Width - Width) / 2;
                Top = Owner.Top + 20;
            }

            await Task.Delay(3000);

            Close();
        }
    }
}
