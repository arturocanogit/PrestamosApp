using Firebase.Database;
using PrestamosApp.Models;
using AbonosApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PrestamosApp.Models.Interfaces;
using System.Globalization;

namespace AbonosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoAbonoPage : ContentPage
    {
        public NuevoAbonoPage(FirebaseObject<UsuarioDetalle> proveedor, FirebaseObject<PrestamoDetalle> prestamo)
        {
            InitializeComponent();
            BindingContext = new NuevoAbonoViewModel(proveedor, prestamo);
        }

        public double Interes { get; private set; }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            NuevoAbonoViewModel context = (NuevoAbonoViewModel)BindingContext;

            string mensaje = string.Empty;

            context.Monto = context.Interes;
            if (context.Monto < context.MinimoAPagar)
            {
                mensaje = $"El minimo a pagar es de {context.MinimoAPagar}";
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                await DisplayAlert("Info", mensaje, "Aceptar");
                return;
            }

            context.Interes = Math.Round(context.MinimoAPagar, 2);
            context.Capital = Math.Round(context.Monto - context.MinimoAPagar, 2);
            if (await DisplayAlert("Info", $"¿Desea guardar el abono?, está operación no se puede deshacer. \n\n Interes: " +
                $"{context.Interes.ToString("C", CultureInfo.CurrentCulture)} \n Capital: " +
                $"{context.Capital.ToString("C", CultureInfo.CurrentCulture)}", "Aceptar", "Cancelar"))
            {
                _ = await context.PostAbono();
                await Navigation.PopToRootAsync();
            }
        }

        private void ChkSinInteres_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            //CheckBox checkBox = (CheckBox)sender;
            //NuevoAbonoViewModel context = (NuevoAbonoViewModel)BindingContext;
            //if (checkBox.IsChecked)
            //{
            //    Interes = context.Interes;
            //    TxtInteres.Text = "0";
            //}
            //else
            //{
            //    TxtInteres.Text = Interes.ToString();
            //    //context.Interes = Interes;
            //}
        }
    }
}