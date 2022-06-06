using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Operacion : IOperacion
    {
        public int OperacionId { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
