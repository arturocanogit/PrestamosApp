using PrestamosApp.Models;
using PrestamosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrestamosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoProveedorPage : ContentPage
    {
        public NuevoProveedorPage()
        {
            InitializeComponent();
            BindingContext = new NuevoProveedorViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = TxtNombre.Focus();
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            NuevoProveedorViewModel context = (NuevoProveedorViewModel)BindingContext;
            _ = await context.PostProveedor();
            _ = await Navigation.PopAsync();
        }
    }
}