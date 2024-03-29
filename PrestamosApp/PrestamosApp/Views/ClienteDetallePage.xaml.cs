﻿using Firebase.Database;
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
    public partial class ClienteDetallePage : ContentPage
    {
        public FirebaseObject<UsuarioDetalle> Cliente { get; }

        public ClienteDetallePage(FirebaseObject<UsuarioDetalle> cliente)
        {
            Cliente = cliente;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            ClienteDetalleViewModel vm = new ClienteDetalleViewModel(Cliente);
            await vm.LoadDataAsync();
            BindingContext = vm;
        }

        private void ListPrestamos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new PrestamoDetallePage(Cliente, (FirebaseObject<PrestamoDetalle>)e.SelectedItem));
        }

        private async void BtnNuevoPrestamo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoPrestamoPage(Cliente));
        }

        private void BtnEditar_Clicked(object sender, EventArgs e)
        {

        }

        private async void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Eliminar", "¿Desea eliminar el usuario?", "Aceptar", "Cancelar"))
            {
                await Task.WhenAll(
           DataBase.DeleteAsync($"Usuarios/{Cliente.Key}"),
           DataBase.DeleteAsync($"Prestamos/{Cliente.Key}"));

                await Navigation.PopAsync();
            }
        }

        public async void ActualizarDatosCliente(string key)
        {
            Usuario usaurio = await DataBase.GetAsync<Usuario>($"Usuarios/{key}");
            var prestamos = await DataBase.GetAllAsync<Prestamo>($"Usuarios/Prestamos/{key}");
        }
    }
}