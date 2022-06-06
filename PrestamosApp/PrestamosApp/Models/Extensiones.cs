using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PrestamosApp.Models
{
    public static class Extensiones
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s);
        }

        public static string ToPascalCase(this string s)
        {
            IEnumerable<string> words = s.Split(new[] { '-', '_' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(word => word.Substring(0, 1).ToUpper() +
                                         word.Substring(1).ToLower());

            string result = string.Concat(words);
            return result;
        }

        public static List<FirebaseObject<UsuarioDetalle>> OrdenarUsuariosPorEstatus(this IEnumerable<FirebaseObject<UsuarioDetalle>> usuarios)
        {
            return usuarios
             .OrderBy(x => x.Object.EstatusPrestamo.Orden)
             .ThenBy(x => x.Object.Nombre)
             .ToList();
        }
    }
}
