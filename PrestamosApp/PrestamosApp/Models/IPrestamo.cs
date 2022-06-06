using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public interface IPrestamo : IOperacion
    {
        int PrestamoId { get; set; }
        int ClienteId { get; set; }
        string Nombre { get; set; }
        double Monto { get; set; }
        double Saldo { get; set; }
        double Interes { get; set; }
        double TasaInteres { get; set; }
        int Dia { get; set; }
    }
}
