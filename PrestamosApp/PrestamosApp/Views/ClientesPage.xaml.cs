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
    public partial class ClientesPage : ContentPage
    {
        public ClientesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            ClientesViewModel vm = new ClientesViewModel();
            await vm.LoadDataAsync();
            BindingContext = vm;
        }

        private void ListClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ClienteDetallePage((FirebaseObject<UsuarioDetalle>)e.SelectedItem));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NuevoClientePage());
        }
    }
}