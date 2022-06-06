using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public interface IOperacion
    {
        int OperacionId { get; set; }
        DateTime FechaRegistro { get; set; }
    }
}
