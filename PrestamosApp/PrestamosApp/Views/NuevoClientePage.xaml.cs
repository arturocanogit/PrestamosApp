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
    public partial class NuevoClientePage : ContentPage
    {
        public NuevoClientePage()
        {
            InitializeComponent();
            BindingContext = new NuevoClienteViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = TxtNombre.Focus();
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            NuevoClienteViewModel context = (NuevoClienteViewModel)BindingContext;
            _ = await context.PostCliente();
            _ = await Navigation.PopAsync();
        }
    }
}