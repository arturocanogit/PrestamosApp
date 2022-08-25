using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrestamosApp.Models
{
    public class UsuarioDto
    {
        public string Nombre { get; set; }
        public Estatus Estatus { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public IEnumerable<Prestamo> Prestamos { get; set; }
        public double Saldo { get { return Prestamos.Sum(x => x.Saldo); } }
        public double Interes { get { return Prestamos.Sum(x => x.Interes); } }
    }
}
