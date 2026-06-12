using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.View;

namespace WPF_LoginForm.Helpers
{
    public static class ToastHelper
    {
        public static void Mostrar(string mensaje, Window owner)
        {
            V_VentanaToast toast = new V_VentanaToast(mensaje, owner);
            toast.Owner = owner;
            toast.Show();
        }
        
    }
}
