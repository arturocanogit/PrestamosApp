using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Prestamo : IEntityDataBase
    {
        [PrimaryKey]
        public int PrestamoId { get; set; }
        public int ClienteId { get; set; }
        public int OperacionId { get; set; }
        public string Nombre { get { return $"Prestamo dias {Dia}" ; } }
        public double Monto { get; set; }
        public double Saldo { get; set; }
        public double Interes { get { return Math.Round(Saldo * TasaInteres, 2); }  }
        public double TasaInteres { get; set; }
        public int EstatusId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public int Dia { get; set; }
    }
}
