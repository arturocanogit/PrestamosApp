using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Usuario : IUsuario
    {
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public double Saldo { get; set; }
        public double Interes { get; set; }
        public int EstatusId { get; set; }
        public int EstatusPrestamoId { get; set; }
        public TipoUsuario Tipo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
