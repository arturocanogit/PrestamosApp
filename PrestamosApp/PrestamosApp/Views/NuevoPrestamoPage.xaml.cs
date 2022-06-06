using Firebase.Database;
using PrestamosApp.Models;
using PrestamosApp.Models.Interfaces;
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
    public partial class NuevoPrestamoPage : ContentPage
    {
        public NuevoPrestamoPage(FirebaseObject<UsuarioDetalle> Usuario)
        {
            InitializeComponent();
            BindingContext = new NuevoPrestamoViewModel(Usuario);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TxtMonto.Text = string.Empty;
            TxtMonto.Focus();
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            NuevoPrestamoViewModel context = (NuevoPrestamoViewModel)BindingContext;
            if (context.Monto <= 0)
            {
                await DisplayAlert("Info", "Favor de ingresar un monto valido.", "Aceptar");
                return;
            }
            _ = await context.PostPrestamo();
            _ = await Navigation.PopAsync();
        }
    }
}