using Firebase.Database;
using PrestamosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestamosApp.ViewModels
{
    public class ProveedoresViewModel : CustomBaseViewModel
    {
        public IEnumerable<FirebaseObject<UsuarioDetalle>> Proveedores { get; set; }
        public double SaldoTotal { get; set; }
        public double InteresTotal { get; set; }
        public Dictionary<int, string> EstatusUsuarios { get; set; }
        public Dictionary<int, string> EstatusColorUsuarios { get; set; }

        public ProveedoresViewModel()
        {

        }
        public override async Task LoadDataAsync()
        {
            Proveedores = await DataBase.GetAllAsync<UsuarioDetalle>("Usuarios");
            Proveedores = Proveedores.Where(x => x.Object.Tipo == TipoUsuario.Proveedor);
            SetStatusPrestamoUsuario();
            Proveedores = Proveedores.OrdenarUsuariosPorEstatus();

            SaldoTotal = Proveedores.AsEnumerable()
                .Where(x=> !x.Object.EsInversor)
                .Sum(x => x.Object.Saldo);

            InteresTotal = Proveedores.AsEnumerable()
                .Sum(x => x.Object.Interes);
        }

        private void SetStatusPrestamoUsuario()
        {
            foreach (FirebaseObject<UsuarioDetalle> usuario in Proveedores)
            {
                usuario.Object.EstatusPrestamo = Global.EstatusPrestamos
                    .First(x => x.EstatusId == usuario.Object.EstatusPrestamoId);
            }
        }
    }
}
