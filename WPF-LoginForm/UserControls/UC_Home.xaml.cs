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
using System;
using System.Data.SqlClient;
using System.Windows.Media;


namespace WPF_LoginForm.UserControls
{
    public partial class UC_Home : UserControl
    {

        public SeriesCollection PieSeries { get; set; }
        public UC_Home()
        {
            InitializeComponent();

            PieSeries = new SeriesCollection();

            CargarGraficoGastosPorCategoria();

            DataContext = this;


            CargarResumen();
            CargarUltimosGastos();
        }

        private void CargarGraficoGastosPorCategoria()
        {
            int indiceColor = 0;

            using (SqlConnection con = Conexion.CrearInstancia().CrearConexion())
            {
                SqlCommand cmd = new SqlCommand("SP_GASTOS_POR_CATEGORIA", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = Helpers.SesionUsuario.IdUsuario;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PieSeries.Add(new PieSeries
                    {
                        Title = reader["Categoria"].ToString(),
                        Values = new ChartValues<decimal>
                    {
                        Convert.ToDecimal(reader["Total"])
                    },
                        DataLabels = true,
                        Fill = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString(
                        coloresNeon[indiceColor % coloresNeon.Length]))
                    });

                    indiceColor++;
                }
            }
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

        private readonly string[] coloresNeon =
        {
            "#00E5FF",
            "#FF00FF",
            "#9D4EDD",
            "#00F5D4",
            "#FF4D9D",
            "#7209B7",
            "#3A86FF",
            "#FB5607"
        };


    }
}