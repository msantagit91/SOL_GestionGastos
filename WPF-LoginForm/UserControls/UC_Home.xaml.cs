using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Data;
using WPF_LoginForm.Helpers;
using WPF_LoginForm.Model;
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF_LoginForm.UserControls
{
    public partial class UC_Home : UserControl
    {
        
        public SeriesCollection PieSeries { get; set; }
        public UC_Home()
        {
            InitializeComponent();

            PieSeries = new SeriesCollection
{
    new PieSeries
    {
        Title = "Comida",
        Values = new ChartValues<double> { 45000 },
        DataLabels = true
    },
    new PieSeries
    {
        Title = "Transporte",
        Values = new ChartValues<double> { 25000 },
        DataLabels = true
    },
    new PieSeries
    {
        Title = "Servicios",
        Values = new ChartValues<double> { 60000 },
        DataLabels = true
    },
    new PieSeries
    {
        Title = "Otros",
        Values = new ChartValues<double> { 30000 },
        DataLabels = true
    }
};

            DataContext = this;

            DataContext = this;

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