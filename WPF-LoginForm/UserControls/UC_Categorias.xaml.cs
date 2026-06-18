using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WPF_LoginForm.Data;
using WPF_LoginForm.Helpers;
using WPF_LoginForm.Model;
using PdfSharp.Fonts;

namespace WPF_LoginForm.UserControls
{
    public partial class UC_Categorias : UserControl
    {
        public UC_Categorias()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            D_Categorias datos = new D_Categorias();

            cbCategorias.ItemsSource = datos.ListarCategoriasMante().DefaultView;
            cbCategorias.DisplayMemberPath = "descripcion";
            cbCategorias.SelectedValuePath = "id_categoria";
        }

        private void btnGuardarCategoria_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcionCategoria.Text))
            {
                ToastHelper.Mostrar("Digite el nombre de la categoría.", Window.GetWindow(this));
                txtDescripcionCategoria.Focus();
                return;
            }

            D_Categorias datos = new D_Categorias();

            string respuesta = datos.GuardarCategoria(txtDescripcionCategoria.Text.Trim());

            if (respuesta == "OK")
            {
                ToastHelper.Mostrar("Categoría guardada correctamente",Window.GetWindow(this));

                txtDescripcionCategoria.Clear();
                txtDescripcionCategoria.Focus();

                CargarCategorias();
            }
            else
            {
                MessageBox.Show(respuesta, "Sistema", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
