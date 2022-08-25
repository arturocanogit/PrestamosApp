using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Abono : IEntityDataBase
    {
        public int AbonoId { get; set; }
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public string Nombre { get { return $"Abono {FechaRegistro:dd/MMM/yyyy}"; } }
        public double Monto { get; set; }
        public double Interes { get; set; }
        public double Capital { get; set; }
        public int OperacionId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
