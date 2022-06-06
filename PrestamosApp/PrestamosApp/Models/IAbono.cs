using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public interface IAbono : IOperacion
    {
        int AbonoId { get; set; }
        int PrestamoId { get; set; }
        int ClienteId { get; set; }
        string Nombre { get; set; }
        double Monto { get; set; }
        double Interes { get; set; }
        double Capital { get; set; }
    }
}
