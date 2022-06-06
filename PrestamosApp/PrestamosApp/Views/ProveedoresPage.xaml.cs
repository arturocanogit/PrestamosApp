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
    public partial class ProveedoresPage : ContentPage
    {
        public ProveedoresPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            ProveedoresViewModel vm = new ProveedoresViewModel();
            await vm.LoadDataAsync();
            BindingContext = vm;
        }

        private void ListProveedores_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ProveedorDetallePage((FirebaseObject<UsuarioDetalle>)e.SelectedItem));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NuevoProveedorPage());
        }
    }
}