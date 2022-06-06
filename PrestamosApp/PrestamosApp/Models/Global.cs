using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class Global
    {
        public double SaldoCaja { get; set; }

        public static IEnumerable<EstatusPrestamo> EstatusPrestamos = new List<EstatusPrestamo>
        {
            new EstatusPrestamo { EstatusId = 1, Descripcion = "Nuevo", Color = "Blue", Orden = 2 },
            new EstatusPrestamo { EstatusId = 2, Descripcion = "Al corriente", Color = "Green", Orden = 1 },
            new EstatusPrestamo { EstatusId = 3, Descripcion = "Atrasado", Color = "Red", Orden = 0 },
            new EstatusPrestamo { EstatusId = 4, Descripcion = "Vencido", Color = "Yellow", Orden = 3 },
            new EstatusPrestamo { EstatusId = 5, Descripcion = "Liquidado", Color = "Gray", Orden = 4 }
        };
    }

    public class EstatusPrestamo
    {
        public int EstatusId { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public int Orden { get; set; }
    }
}
