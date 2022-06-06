using Firebase.Database;
using PrestamosApp.Models;
using PrestamosApp.Models.Enums;
using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbonosApp.ViewModels
{
    public class NuevoAbonoViewModel : Abono
    {
        public NuevoAbonoViewModel(FirebaseObject<UsuarioDetalle> usuario, FirebaseObject<PrestamoDetalle> prestamo)
        {
            Usuario = usuario;
            Prestamo = prestamo;
            FechaRegistro = DateTime.Now;
            MinimoAPagar = prestamo.Object.Interes;
            Interes = MinimoAPagar;
        }

        public FirebaseObject<UsuarioDetalle> Usuario { get; }
        public FirebaseObject<PrestamoDetalle> Prestamo { get; }
        public double MinimoAPagar { get; }

        public async Task<FirebaseObject<Abono>> PostAbono()
        {
            Monto = Capital + Interes;
            Nombre = GenerarNombre();

            //Si se abona a capital se resta de la deuda y se recalcula el interes
            if (Capital > 0)
            {
                double interesEliminado = Capital * Prestamo.Object.TasaInteres;

                Prestamo.Object.Saldo -= Capital;
                Prestamo.Object.Interes -= interesEliminado;

                Usuario.Object.Saldo -= Capital;
                Usuario.Object.Interes -= interesEliminado;
            }

            IReadOnlyCollection<FirebaseObject<Global>> list = await DataBase.GetAllAsync<Global>("Global");
            FirebaseObject<Global> fbo = list.First();
            Global global = fbo.Object;

            if (Usuario.Object.Tipo == TipoUsuario.Cliente)
            {
                global.SaldoCaja += Monto;
            }
            if (Usuario.Object.Tipo == TipoUsuario.Proveedor)
            {
                global.SaldoCaja -= Monto;
            }

            Movimiento movimiento = new Movimiento
            {
                TipoMovimiento = TipoMovimiento.Abono,
                SaldoEnCaja = global.SaldoCaja,
                Monto = Monto,
                UsuarioKey = Usuario.Key,
                Fecha = DateTime.Now
            };

            await Task.WhenAll(
            DataBase.PutAsync($"Global/{fbo.Key}", global),
            DataBase.PutAsync($"Usuarios/{Usuario.Key}", Usuario.Object),
            DataBase.PutAsync($"Prestamos/{Usuario.Key}/{Prestamo.Key}", Prestamo.Object),
            DataBase.PostAsync($"Movimientos", movimiento));

            return await DataBase.PostAsync($"Abonos/{Prestamo.Key}", (Abono)this);
        }

        private string GenerarNombre()
        {
            return $"Abono { FechaRegistro:dd/MMM/yyyy}";
        }
    }
}
