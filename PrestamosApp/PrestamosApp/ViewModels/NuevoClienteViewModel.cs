using Firebase.Database;
using PrestamosApp.Models;
using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace PrestamosApp.ViewModels
{
    public class NuevoClienteViewModel : UsuarioDetalle
    {
        public int IdEstatus { get; set; }
        public Estatus Estatus { get; set; }

        public NuevoClienteViewModel()
        {
            Tipo = TipoUsuario.Cliente;
            IdEstatus = 1;
            //Al ser nuevo el estatus global de los prestamos es nuevo
            EstatusPrestamoId = 1;
        }

        public Task<FirebaseObject<UsuarioDetalle>> PostCliente()
        {
            FechaRegistro = DateTime.Now;
            Nombre = Nombre.ToTitleCase();
            return DataBase.PostAsync("Usuarios", (UsuarioDetalle)this);
        }
    }
}
