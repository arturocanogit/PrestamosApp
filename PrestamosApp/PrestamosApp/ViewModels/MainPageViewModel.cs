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
    class MainPageViewModel : CustomBaseViewModel
    {
        public double SaldoProveedores { get; set; }
        public double SaldoClientes { get; set; }
        public double InteresProveedores { get; set; }
        public double InteresClientes { get; set; }
        public double UtilidadNeta { get; set; }
        public double SaldoCaja { get; set; }
        public IReadOnlyCollection<FirebaseObject<UsuarioDetalle>> Usuarios { get; set; }
        public Dictionary<int, string> EstatusPrestamoUsuarios { get; set; }
        public Dictionary<int, string> EstatusColorUsuarios { get; set; }

        public override async Task LoadDataAsync()
        {
            Usuarios = await DataBase.GetAllAsync<UsuarioDetalle>("Usuarios");
            SetStatusPrestamoUsuario();
            Usuarios = Usuarios.OrdenarUsuariosPorEstatus();

            IReadOnlyCollection<FirebaseObject<Global>> list = await DataBase.GetAllAsync<Global>("Global");
            FirebaseObject<Global> fbo = list.First();
            Global global = fbo.Object;
            SaldoCaja = global.SaldoCaja;

            SaldoClientes = Usuarios
                .Where(x => x.Object.Tipo == TipoUsuario.Cliente).Sum(x => x.Object.Saldo);
            InteresClientes = Usuarios
                .Where(x => x.Object.Tipo == TipoUsuario.Cliente).Sum(x => x.Object.Interes);

            SaldoProveedores = Usuarios
                .Where(x => x.Object.Tipo == TipoUsuario.Proveedor).Sum(x => x.Object.Saldo);
            InteresProveedores = Usuarios
                .Where(x => x.Object.Tipo == TipoUsuario.Proveedor && !x.Object.EsInversor).Sum(x => x.Object.Interes);

            UtilidadNeta = InteresClientes - InteresProveedores;
        }

        private void SetStatusPrestamoUsuario()
        {
            foreach (FirebaseObject<UsuarioDetalle> usuario in Usuarios)
            {
                usuario.Object.EstatusPrestamo = Global.EstatusPrestamos
                    .First(x => x.EstatusId == usuario.Object.EstatusPrestamoId);
            }
        }
    }
}
