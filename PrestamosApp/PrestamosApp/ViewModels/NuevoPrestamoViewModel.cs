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
    public class NuevoPrestamoViewModel : Prestamo
    {
        public NuevoPrestamoViewModel(FirebaseObject<UsuarioDetalle> usuario)
        {
            Usuario = usuario;
            FechaRegistro = DateTime.Now;
            FechaPrestamo = DateTime.Now;
            TasaInteres = 0.05;
            EstatusId = 2;//Nace al corriente
        }
        public FirebaseObject<UsuarioDetalle> Usuario { get; }

        public async Task<FirebaseObject<Prestamo>> PostPrestamo()
        {
            Dia = FechaPrestamo.Day;
            Nombre = GenerarNombre();
            Saldo = Monto;
            Interes = Saldo * TasaInteres;

            Usuario.Object.Saldo += Monto;
            Usuario.Object.Interes += Interes;
            Usuario.Object.NumeroPrestamos += 1;

            //Si es nuevo se cambia a estatus al corriente
            if (Usuario.Object.EstatusPrestamoId == 1)
            {
                Usuario.Object.EstatusPrestamoId = 2;
            }

            IReadOnlyCollection<FirebaseObject<Global>> list = await DataBase.GetAllAsync<Global>("Global");
            FirebaseObject<Global> fbo = list.First();
            Global global = fbo.Object;

            if (Usuario.Object.Tipo == TipoUsuario.Cliente)
            {
                global.SaldoCaja -= Monto;
            }
            if (Usuario.Object.Tipo == TipoUsuario.Proveedor)
            {
                global.SaldoCaja += Monto;
            }

            Movimiento movimiento = new Movimiento
            {
                TipoMovimiento = TipoMovimiento.Prestamo,
                SaldoEnCaja = global.SaldoCaja,
                Monto = Monto,
                UsuarioKey = Usuario.Key,
                Fecha = DateTime.Now
            };

            await Task.WhenAll(
                DataBase.PutAsync($"Global/{fbo.Key}", global),
                DataBase.PutAsync($"Usuarios/{Usuario.Key}", Usuario.Object),
                DataBase.PostAsync($"Movimientos", movimiento));

            return await DataBase.PostAsync($"Prestamos/{Usuario.Key}", (Prestamo)this);
        }

        private string GenerarNombre()
        {
            return $"Prestamo dias { Dia }";
        }
    }
}
