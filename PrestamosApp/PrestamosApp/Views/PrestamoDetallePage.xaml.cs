using AbonosApp.Views;
using Firebase.Database;
using PrestamosApp.Models;
using PrestamosApp.Models.Interfaces;
using PrestamosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrestamosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrestamoDetallePage : ContentPage
    {
        internal FirebaseObject<PrestamoDetalle> Prestamo { get; }
        public FirebaseObject<UsuarioDetalle> Usuario { get; }

        public PrestamoDetallePage(FirebaseObject<UsuarioDetalle> usuario, FirebaseObject<PrestamoDetalle> prestamo)
        {
            Prestamo = prestamo;
            Usuario = usuario;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            PrestamoDetalleViewModel vm = new PrestamoDetalleViewModel(Prestamo);
            await vm.LoadDataAsync();
            BindingContext = vm;
        }

        private async void BtnNuevoAbono_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoAbonoPage(Usuario, Prestamo));


            //PrestamoDetalleViewModel context = (PrestamoDetalleViewModel)BindingContext;
            //if (await DisplayAlert("Alert", $"¿Desea registar el interes del mes? " +
            //    $"({context.Interes.ToString("C", CultureInfo.CurrentCulture)})", "Aceptar", "Cancelar"))
            //{
            //    await context.PostInteres();
            //    LoadDataAsync();
            //}
        }
    }
}