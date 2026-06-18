using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Data;
using WPF_LoginForm.Helpers;

namespace WPF_LoginForm.UserControls
{
    public partial class UC_Reportes : UserControl
    {
        public UC_Reportes()
        {
            InitializeComponent();
            CargarReporte();
        }

        private void CargarReporte()
        {
            D_Reporte datos = new D_Reporte();

            DataTable tabla = datos.ReporteGastosMes(Helpers.SesionUsuario.IdUsuario);

            dgvReporte.ItemsSource = tabla.DefaultView;

            decimal total = 0;

            foreach (DataRow fila in tabla.Rows)
            {
                total += Convert.ToDecimal(fila["monto"]);
            }

            txtTotalGastado.Text = total.ToString("N0");
            txtCantidadGastos.Text = tabla.Rows.Count.ToString();
        }

        private void btnExportarPdf_Click(object sender, RoutedEventArgs e)
        {
            

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Archivo PDF (*.pdf)|*.pdf";
            save.FileName = "Reporte_Gastos_Mes.pdf";

            if (save.ShowDialog() == true)
            {
                PdfDocument documento = new PdfDocument();
                documento.Info.Title = "Reporte de gastos totales del mes";

                PdfPage pagina = documento.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(pagina);

                if (GlobalFontSettings.FontResolver == null)
                {
                    GlobalFontSettings.FontResolver = new PdfFontResolver();
                }

                XFont titulo = new XFont("Arial", 18);
                XFont texto = new XFont("Arial", 10);
                XFont encabezado = new XFont("Arial", 10);

                double y = 40;

                gfx.DrawString("REPORTE DE GASTOS DEL MES", titulo, XBrushes.Black, new XPoint(40, y));
                y += 35;

                gfx.DrawString("Total gastado: " + txtTotalGastado.Text, encabezado, XBrushes.Black, new XPoint(40, y));
                y += 20;

                gfx.DrawString("Cantidad de gastos: " + txtCantidadGastos.Text, encabezado, XBrushes.Black, new XPoint(40, y));
                y += 35;

                gfx.DrawString("Fecha", encabezado, XBrushes.Black, new XPoint(40, y));
                gfx.DrawString("Descripcion", encabezado, XBrushes.Black, new XPoint(120, y));
                gfx.DrawString("Categoria", encabezado, XBrushes.Black, new XPoint(320, y));
                gfx.DrawString("Monto", encabezado, XBrushes.Black, new XPoint(450, y));

                y += 20;

                DataView vista = dgvReporte.ItemsSource as DataView;

                foreach (DataRowView fila in vista)
                {
                    if (y > 780)
                    {
                        pagina = documento.AddPage();
                        gfx = XGraphics.FromPdfPage(pagina);
                        y = 40;
                    }

                    gfx.DrawString(Convert.ToDateTime(fila["fecha"]).ToString("dd/MM/yyyy"), texto, XBrushes.Black, new XPoint(40, y));
                    gfx.DrawString(fila["descripcion"].ToString(), texto, XBrushes.Black, new XPoint(120, y));
                    gfx.DrawString(fila["categoria"].ToString(), texto, XBrushes.Black, new XPoint(320, y));
                    gfx.DrawString(Convert.ToDecimal(fila["monto"]).ToString("N0"), texto, XBrushes.Black, new XPoint(450, y));

                    y += 20;
                }

                documento.Save(save.FileName);

                Helpers.ToastHelper.Mostrar("PDF generado correctamente",Window.GetWindow(this));
            }
        }

    }
}
