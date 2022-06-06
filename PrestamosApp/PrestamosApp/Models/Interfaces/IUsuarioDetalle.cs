using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models.Interfaces
{
    public interface IUsuarioDetalle : IUsuario
    {
        EstatusPrestamo EstatusPrestamo { get; set; }
        int NumeroPrestamos { get; set; }
    }
}
