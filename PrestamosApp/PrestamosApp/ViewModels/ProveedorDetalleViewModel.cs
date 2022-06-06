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
    public class ProveedorDetalleViewModel : CustomBaseViewModel, IUsuarioDetalle
    {
        public IReadOnlyCollection<FirebaseObject<PrestamoDetalle>> Prestamos { get; set; }
        public IReadOnlyCollection<FirebaseObject<Abono>> Abonos { get; set; }
        public string Key { get; }
        public int ProveedorId { get; set; }
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

        public ProveedorDetalleViewModel(FirebaseObject<UsuarioDetalle> usuario)
        {
            Key = usuario.Key;
            Nombre = usuario.Object.Nombre;
            Contacto = usuario.Object.Contacto;
            Saldo = usuario.Object.Saldo;
            Interes = usuario.Object.Interes;
        }

        public override async Task LoadDataAsync()
        {
            Prestamos = await DataBase.GetAllAsync<PrestamoDetalle>($"Prestamos/{Key}");
        }

        internal void PostPrestamo()
        {
            throw new NotImplementedException();
        }
    }
}