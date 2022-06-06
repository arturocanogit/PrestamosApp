using Firebase.Database;
using PrestamosApp.Models;
using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamosApp.ViewModels
{
    public class ClientesViewModel : CustomBaseViewModel
    {
        public IEnumerable<FirebaseObject<UsuarioDetalle>> Clientes { get; set; }
        public double SaldoTotal { get; set; }
        public double InteresTotal { get; set; }
        public Dictionary<int, string> EstatusPrestamoUsuarios { get; set; }
        public Dictionary<int, string> EstatusColorUsuarios { get; set; }

        public ClientesViewModel()
        {

        }
        public override async Task LoadDataAsync()
        {
            Clientes = await DataBase.GetAllAsync<UsuarioDetalle>("Usuarios");
            Clientes = Clientes.Where(x => x.Object.Tipo == TipoUsuario.Cliente);
            SetStatusPrestamoUsuario();
            Clientes = Clientes.OrdenarUsuariosPorEstatus();

            SaldoTotal = Clientes.AsEnumerable().Sum(x => x.Object.Saldo);
            InteresTotal = Clientes.AsEnumerable().Sum(x => x.Object.Interes);

            SetStatusPrestamoUsuario();
        }

        private void SetStatusPrestamoUsuario()
        {
            foreach (FirebaseObject<UsuarioDetalle> usuario in Clientes)
            {
                usuario.Object.EstatusPrestamo = Global.EstatusPrestamos
                    .First(x => x.EstatusId == usuario.Object.EstatusPrestamoId);
            }
        }
    }
}
