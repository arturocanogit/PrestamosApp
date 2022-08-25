using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrestamosApp.Models
{
    public class DataBase
    {
        public static string UrlConnection = "https://prestamosappprod-default-rtdb.firebaseio.com/";

        public static Task<IReadOnlyCollection<FirebaseObject<T>>> GetAllAsync<T>(string resourceName)
        {
            var firebase = new FirebaseClient(UrlConnection);
            return firebase.Child(resourceName).OnceAsync<T>();
        }

        public static Task<T> GetAsync<T>(string resourceName)
        {
            return new FirebaseClient(UrlConnection).Child(resourceName).OnceSingleAsync<T>();
        }

        public static Task PutAsync(string child, object objeto)
        {
            var firebase = new FirebaseClient(UrlConnection);
            return firebase.Child(child).PutAsync(objeto);
        }

        public static Task<FirebaseObject<T>> PostAsync<T>(string child, T objeto)
        {
            var firebase = new FirebaseClient(UrlConnection);
            return firebase.Child(child).PostAsync(objeto);
        }

        public static Task DeleteAsync(string child)
        {
            var firebase = new FirebaseClient(UrlConnection);
            return firebase.Child(child).DeleteAsync();
        }
    }
}
