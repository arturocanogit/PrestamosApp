using Firebase.Database;
using PrestamosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrestamosApp.ViewModels
{
    public class PrestamoDetalleViewModel : CustomBaseViewModel, IPrestamoDetalle
    {
        public IEnumerable<FirebaseObject<Abono>> Abonos { get; set; }
        public FirebaseObject<PrestamoDetalle> Prestamo { get; }
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public double Monto { get; set; }
        public double Saldo { get; set; }
        public double TasaInteres { get; set; }
        public double Interes { get; set; }
        public int OperacionId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Dia { get; set; }

        public PrestamoDetalleViewModel(FirebaseObject<PrestamoDetalle> prestamo)
        { 
            Prestamo = prestamo;
            PrestamoId = prestamo.Object.PrestamoId;
            Nombre = prestamo.Object.Nombre;
            Monto = prestamo.Object.Monto;
            Saldo = prestamo.Object.Saldo;
            TasaInteres = prestamo.Object.TasaInteres;
            Interes = prestamo.Object.Interes;
            FechaRegistro = prestamo.Object.FechaRegistro;
            Dia = prestamo.Object.Dia;
        }

        public async Task PostInteres()
        {
            Abono abono = new Abono
            {
                Monto = Interes,
                Interes = Interes,
                FechaRegistro = DateTime.Now,
                Nombre = GenerarNombre()
            };

            IReadOnlyCollection<FirebaseObject<Global>> list = await DataBase.GetAllAsync<Global>("Global");
            FirebaseObject<Global> fbo = list.First();
            Global global = fbo.Object;
            global.SaldoCaja += Interes;

            await DataBase.PutAsync($"Global/{fbo.Key}", global);
            _ = await DataBase.PostAsync($"PrestamosAbonos/{Prestamo.Key}/", abono);
        }

        private string GenerarNombre()
        {
            return $"Abono { DateTime.Now.Date.ToString().Split(' ')[0] }";
        }

        public void PostAbono()
        {
            Abono abono = new Abono
            {
                PrestamoId = PrestamoId,
                Monto = Interes,
                Interes = Interes
            };
            IPrestamo prestamo = this;
            prestamo.Saldo -= 300;

            //_ = Task.WhenAll(
            //    DataBase.PostAsync("Abonos", abono),
            //    DataBase.PutAsync("Prestamos", Prestamo.Key, Prestamo.Object));
        }

        public override async Task LoadDataAsync()
        {
            Abonos = (await DataBase.GetAllAsync<Abono>($"Abonos/{Prestamo.Key}"))
                .OrderByDescending(x => x.Object.FechaRegistro);
        }
    }
}