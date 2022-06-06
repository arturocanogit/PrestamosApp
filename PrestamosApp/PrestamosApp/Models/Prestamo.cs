using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Prestamo : IPrestamo, IEntityDataBase
    {
        [PrimaryKey]
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public int OperacionId { get; set; }
        public string Nombre { get; set; }
        public double Monto { get; set; }
        public double Saldo { get; set; }
        public double Interes { get; set; }
        public double TasaInteres { get; set; }
        public int EstatusId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public int Dia { get; set; }
    }
}
