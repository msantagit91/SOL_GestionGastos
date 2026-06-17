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
                        DataLabels = true
                    });
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

        

        
    }
}