﻿using Firebase.Database;
using PrestamosApp.Models;
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
            //Si se abona a capital se resta de la deuda y se recalcula el interes
            if (Capital > 0)
            {
                Prestamo.Object.Saldo = Math.Round(Prestamo.Object.Saldo - Capital, 2);
                //Prestamo.Object.Interes = Math.Round(Prestamo.Object.Saldo * Prestamo.Object.TasaInteres, 2);
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
            DataBase.PutAsync($"Prestamos/{Usuario.Key}/{Prestamo.Key}", Prestamo.Object),
            DataBase.PostAsync($"Movimientos", movimiento));


            IReadOnlyCollection<FirebaseObject<PrestamoDetalle>> Prestamos =
                   await DataBase.GetAllAsync<PrestamoDetalle>($"Prestamos/{Usuario.Key}");

            Usuario.Object.Saldo = Prestamos.Sum(x => x.Object.Saldo);
            Usuario.Object.Interes = Prestamos.Sum(x => x.Object.Interes);

            await DataBase.PutAsync($"Usuarios/{Usuario.Key}", Usuario.Object);
            return await DataBase.PostAsync($"Abonos/{Prestamo.Key}", (Abono)this);
        }
    }
}
