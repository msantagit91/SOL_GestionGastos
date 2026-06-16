using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_LoginForm.Data;
using WPF_LoginForm.Helpers;
using WPF_LoginForm.Model;

namespace WPF_LoginForm.UserControls
{
    /// <summary>
    /// Lógica de interacción para UC_Gastos.xaml
    /// </summary>
    public partial class UC_Gastos : UserControl
    {
        private int IdGastoSeleccionado = 0;

        public UC_Gastos()
        {
            InitializeComponent();

            CargarCategorias();

            CargarGastos();
        }

        private void CargarCategorias()
        {
            Data.D_Categorias categoriaData = new Data.D_Categorias();

            DataTable dt = categoriaData.ListarCategoria();

            cbCategoria.ItemsSource = dt.DefaultView;
            cbCategoria.DisplayMemberPath = "descripcion";
            cbCategoria.SelectedValuePath = "id_categoria";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                M_Gasto gasto = new M_Gasto();

                gasto.Descripcion =
                    txtDescripcion.Text.Trim();

                gasto.Monto =
                    Convert.ToDecimal(txtMonto.Text);

                gasto.IdCategoria =
                    Convert.ToInt32(
                        cbCategoria.SelectedValue);

                gasto.IdUsuario =
                    SesionUsuario.IdUsuario;

                gasto.Fecha =
                    dpFecha.SelectedDate.Value;

                D_Gasto datos = new D_Gasto();

                string respuesta =
                    datos.Insertar(gasto);

                if (respuesta == "OK")
                {
                    
                    ToastHelper.Mostrar("Gasto registrado correctamente", Window.GetWindow(this));
                    CargarGastos();


                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show(respuesta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LimpiarCampos()
        {
            txtDescripcion.Clear();

            txtMonto.Clear();

            cbCategoria.SelectedIndex = 0;

            dpFecha.SelectedDate = DateTime.Now;

            txtDescripcion.Focus();
        }

        private void CargarGastos()
        {
            D_Gasto datos = new D_Gasto();

            dgvGastos.ItemsSource =
                datos.Listar().DefaultView;
        }

        

        private void dgvGastos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvGastos.SelectedItem == null)
                return;

            DataRowView fila =
                (DataRowView)dgvGastos.SelectedItem;

            IdGastoSeleccionado =
                Convert.ToInt32(fila["id_gastos"]);

            txtDescripcion.Text =
                fila["descripcion"].ToString();

            txtMonto.Text =
                fila["monto"].ToString();

            cbCategoria.SelectedValue =
                Convert.ToInt32(fila["id_categoria"]);

            dpFecha.SelectedDate =
                Convert.ToDateTime(fila["fecha"]);
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (IdGastoSeleccionado == 0)
                {
                    ToastHelper.Mostrar(
                        "Seleccione un gasto para editar",
                        Window.GetWindow(this));
                    return;
                }

                M_Gasto gasto = new M_Gasto();

                gasto.IdGastos = IdGastoSeleccionado;
                gasto.Descripcion = txtDescripcion.Text.Trim();
                gasto.Monto = Convert.ToDecimal(txtMonto.Text);
                gasto.IdCategoria = Convert.ToInt32(cbCategoria.SelectedValue);
                gasto.Fecha = dpFecha.SelectedDate.Value;

                D_Gasto datos = new D_Gasto();

                string respuesta = datos.Editar(gasto);

                if (respuesta == "OK")
                {
                    ToastHelper.Mostrar(
                        "Gasto Editado Correctamente",
                        Window.GetWindow(this));

                    CargarGastos();
                    LimpiarCampos();

                    IdGastoSeleccionado = 0;
                }
                else
                {
                    ToastHelper.Mostrar(
                        respuesta,
                        Window.GetWindow(this));
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Mostrar(
                    ex.Message,
                    Window.GetWindow(this));
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (IdGastoSeleccionado == 0)
                {
                    ToastHelper.Mostrar(
                        "Seleccione un gasto",
                        Window.GetWindow(this));

                    return;
                }

                MessageBoxResult resultado =
                    MessageBox.Show(
                        "¿Desea eliminar el gasto seleccionado?",
                        "Confirmación",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    D_Gasto datos =
                        new D_Gasto();

                    string respuesta =
                        datos.Eliminar(
                            IdGastoSeleccionado);

                    if (respuesta == "OK")
                    {
                        ToastHelper.Mostrar(
                            "Gasto Eliminado Correctamente",
                            Window.GetWindow(this));

                        CargarGastos();

                        LimpiarCampos();
                    }
                    else
                    {
                        ToastHelper.Mostrar(
                            respuesta,
                            Window.GetWindow(this));
                    }
                }
            }
            catch (Exception ex)
            {
                ToastHelper.Mostrar(
                    ex.Message,
                    Window.GetWindow(this));
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
    
}
