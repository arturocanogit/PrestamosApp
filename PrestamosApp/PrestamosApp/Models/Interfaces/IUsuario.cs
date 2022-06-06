using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models.Interfaces
{
    public interface IUsuario
    {
        string Nombre { get; set; }
        string Contacto { get; set; }
        double Saldo { get; set; }
        double Interes { get; set; }
        int EstatusId { get; set; }
        int EstatusPrestamoId { get; set; }
        TipoUsuario Tipo { get; set; }
        DateTime FechaRegistro { get; set; }
    }
}
