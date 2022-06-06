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
    public class MovimientosViewModel : CustomBaseViewModel
    {
        public MovimientosViewModel()
        {

        }


        public IReadOnlyCollection<FirebaseObject<Usuario>> Usuarios { get; private set; }
        public IEnumerable<FirebaseObject<Movimiento>> Movimientos { get; private set; }

        public override async Task LoadDataAsync()
        {
            Movimientos = (await DataBase.GetAllAsync<Movimiento>("Movimientos"))
                .OrderByDescending(x => x.Object.Fecha);

            Usuarios = await DataBase.GetAllAsync<Usuario>("Usuarios");

            CargarNombreUsuarios();
        }

        public void CargarNombreUsuarios()
        {
            foreach (Movimiento movimiento in Movimientos.Select(x => x.Object))
            {
                movimiento.UsuarioNombre = Usuarios
                    .First(x => x.Key == movimiento.UsuarioKey).Object.Nombre;
            }
        }
    }
}
