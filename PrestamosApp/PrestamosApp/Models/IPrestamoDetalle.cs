using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrestamosApp.Models
{
    public interface IPrestamoDetalle : IPrestamo
    {
        IEnumerable<FirebaseObject<Abono>> Abonos { get; set; }
    }
}
