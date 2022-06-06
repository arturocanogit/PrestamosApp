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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            MainPageViewModel vm = new MainPageViewModel();
            await vm.LoadDataAsync();
            BindingContext = vm;

            if (vm.UtilidadNeta < 0)
            {
                LblUtilidad.TextColor = Color.Red;
            }
            if (vm.SaldoCaja < 0)
            {
                LblSaldoCaja.TextColor = Color.Red;
            }
        }

        private void BtnRuta_Clicked(object sender, EventArgs e)
        {
            switch ((string)((Button)sender).BindingContext)
            {
                case "B1":
                    _ = Navigation.PushAsync(new ProveedoresPage());
                    break;
                case "B2":
                    _ = Navigation.PushAsync(new ClientesPage());
                    break;
                case "B3":
                    _ = Navigation.PushAsync(new MovimientosPage());
                    break;
                default:
                    break;
            }
        }

        private void ListUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new ClienteDetallePage((FirebaseObject<UsuarioDetalle>)e.SelectedItem));
        }
    }
}