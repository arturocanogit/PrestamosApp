using PrestamosApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class UsuarioDetalle : IUsuarioDetalle
    {
        public EstatusPrestamo EstatusPrestamo { get; set; }
        public int NumeroPrestamos { get; set; }
        public string Nombre { get; set; }
        public string Contacto { get; set; }
        public double Saldo { get; set; }
        public double Interes { get; set; }
        public int EstatusId { get; set; }
        public int EstatusPrestamoId { get; set; }
        public TipoUsuario Tipo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EsInversor { get; set; }
    }
}
