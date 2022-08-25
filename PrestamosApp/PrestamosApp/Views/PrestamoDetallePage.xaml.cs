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
        }

        private void BtnEditar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrestamoEditPage(Usuario.Key, Prestamo));
        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Eliminar", "¿Deseas eliminar el préstamo?, se eliminaran tambien sus abonos.", "Aceptar", "Cancelar"))
            {
                await Task.WhenAll(
                    DataBase.DeleteAsync($"Prestamos/{Usuario.Key}/{Prestamo.Key}"),
                    DataBase.DeleteAsync($"Abonos/{Prestamo.Key}"));

                await Navigation.PopAsync(true);
            }
        }
    }
}