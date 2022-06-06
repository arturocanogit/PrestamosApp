using PrestamosApp.Models.Enums;
using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Movimiento : IMovimiento
    {
        public TipoMovimiento TipoMovimiento { get; set; }
        public double SaldoEnCaja { get; set; }
        public double Monto { get; set; }
        public string UsuarioKey { get; set; }
        public string UsuarioNombre { get; set; }
        public DateTime Fecha { get; set; }
    }
}
