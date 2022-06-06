using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public class PrestamoDetalle : Prestamo
    {
        public EstatusPrestamo Estatus { get; set; }
        public IEnumerable<IAbono> Abonos { get; set; }
    }
}
