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
    public partial class MovimientosPage : ContentPage
    {
        public MovimientosPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            MovimientosViewModel vm = new MovimientosViewModel();
            await vm.LoadDataAsync();
            BindingContext = vm;
        }

        private void ListMovimientos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           //Navigation.PushAsync(new MovimientoDetallePage((FirebaseObject<UsuarioDetalle>)e.SelectedItem));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new NuevoMovimientoPage());
        }
    }
}