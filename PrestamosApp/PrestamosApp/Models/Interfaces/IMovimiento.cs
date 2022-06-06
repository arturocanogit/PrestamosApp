using PrestamosApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models.Interfaces
{
    public interface IMovimiento
    {
        TipoMovimiento TipoMovimiento { get; set; }
        double SaldoEnCaja { get; set; }
        double Monto { get; set; }
        string UsuarioKey { get; set; }
        string UsuarioNombre { get; set; }
        DateTime Fecha { get; set; }
    }
}
