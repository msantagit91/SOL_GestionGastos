using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using WPF_LoginForm.Data;
using WPF_LoginForm.Helpers;
using WPF_LoginForm.Model;

namespace WPF_LoginForm.UserControls
{
    public partial class UC_Home : UserControl
    {
        public UC_Home()
        {
            InitializeComponent();
            CargarResumen();
            CargarUltimosGastos();

           
        }

        private void CargarResumen()
        {
            D_Dashboard datos = new D_Dashboard();

            M_Dashboard resumen = datos.ObtenerResumen(SesionUsuario.IdUsuario);

            if (resumen != null)
            {
                txtTotalGastado.Text = "₡" + resumen.TotalGastado.ToString("N0");
                txtGastoMes.Text = "₡" + resumen.GastoMes.ToString("N0");
                txtCantidadGastos.Text = resumen.CantidadGastos.ToString();
            }
        }

        private void CargarUltimosGastos()
        {
            D_Dashboard datos = new D_Dashboard();

            DataTable dt = datos.UltimosGastos(SesionUsuario.IdUsuario);

            if (dt != null)
            {
                dgvUltimosGastos.ItemsSource = dt.DefaultView;
            }
        }

        

        
    }
}