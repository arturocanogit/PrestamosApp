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
    public partial class PrestamoEditPage : ContentPage
    {
        public FirebaseObject<PrestamoDetalle> Prestamo { get; }
        string UsuarioKey { get; }

        public PrestamoEditPage(string usuarioKey, FirebaseObject<PrestamoDetalle> prestamo)
        {
            InitializeComponent();
            Prestamo = prestamo;
            UsuarioKey = usuarioKey;
            BindingContext = Prestamo.Object;
        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Editar", "¿Deseas editar el préstamo?", "Aceptar", "Cancelar"))
            {
                await DataBase.PutAsync($"Prestamos/{UsuarioKey}/{Prestamo.Key}", BindingContext);
                await Navigation.PopAsync(true);
            }
        }

        private void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}