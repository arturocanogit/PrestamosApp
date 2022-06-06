using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public enum TipoUsuario
    {
        Cliente = 1,
        Proveedor = 2
    }
    public enum EstatusUsuarioOperacion
    {
        Corriente,
        Atrasado,
        Vencido
    }
}
