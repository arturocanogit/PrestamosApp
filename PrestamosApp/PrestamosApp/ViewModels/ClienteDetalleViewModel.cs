using Firebase.Database;
using PrestamosApp.Models;
using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PrestamosApp.ViewModels
{
    public class ClienteDetalleViewModel : CustomBaseViewModel, IUsuarioDetalle
    {
        public IReadOnlyCollection<FirebaseObject<PrestamoDetalle>> Prestamos { get; set; }
        public IReadOnlyCollection<FirebaseObject<Abono>> Abonos { get; set; }
        public string Key { get; }
        public int ClienteId { get; set; }
        public double Saldo { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int NumeroPrestamos { get; set; }
        public double Interes { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public Estatus Estatus { get; set; }
        public int IdEstatus { get; set; }
        public TipoUsuario Tipo { get; set; }
        public EstatusPrestamo EstatusPrestamo { get; set; }
        public int EstatusId { get; set; }
        public int EstatusPrestamoId { get; set; }

        public ClienteDetalleViewModel(FirebaseObject<UsuarioDetalle> cliente)
        {
            Key = cliente.Key;
            //ClienteId = cliente.Object.ClienteId;
            Nombre = cliente.Object.Nombre;
            Contacto = cliente.Object.Contacto;
            Saldo = cliente.Object.Saldo;
            Interes = cliente.Object.Interes;
            NumeroPrestamos = cliente.Object.NumeroPrestamos;
        }

        public override async Task LoadDataAsync()
        {
            Prestamos = await DataBase.GetAllAsync<PrestamoDetalle>($"Prestamos/{Key}");
            SetStatusPrestamos();
        }

        private void SetStatusPrestamos()
        {
            foreach (FirebaseObject<PrestamoDetalle> item in Prestamos)
            {
                item.Object.Estatus = Global.EstatusPrestamos
                    .First(x => x.EstatusId == item.Object.EstatusId);
            }
        }

        internal void PostPrestamo()
        {
            throw new NotImplementedException();
        }
    }
}