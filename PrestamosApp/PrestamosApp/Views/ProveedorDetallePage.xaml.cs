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
    public partial class ProveedorDetallePage : ContentPage
    {
        public FirebaseObject<UsuarioDetalle> Proveedor { get; }

        public ProveedorDetallePage(FirebaseObject<UsuarioDetalle> proveedor)
        {
            Proveedor = proveedor;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            ProveedorDetalleViewModel vm = new ProveedorDetalleViewModel(Proveedor);
            await vm.LoadDataAsync();
            BindingContext = vm;
        }

        private void ListPrestamos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new PrestamoDetallePage(Proveedor, (FirebaseObject<PrestamoDetalle>)e.SelectedItem));
        }

        private async void BtnNuevoPrestamo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NuevoPrestamoPage(Proveedor));
        }
    }
}