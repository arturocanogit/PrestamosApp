using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xamarin.Essentials;

namespace PrestamosApp.Models
{
    public class DataBaseOperations
    {
        private static SQLiteConnection conn;

        private static void GetConnection()
        {
            string baseDirectory = FileSystem.AppDataDirectory;
            string pathDb = $"{baseDirectory}/VentaCuajadasV1_1.db3";

            try
            {
                conn = new SQLiteConnection(pathDb);
            }
            catch (SQLiteException ex)
            {

            }
        }

        private static void CloseDB()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public static void CreateDB()
        {
            if (conn == null)
            {
                GetConnection();
            }
            try
            {
                conn.BeginTransaction();
                #region App

                //_ = conn.CreateTable<Cliente>();
                _ = conn.CreateTable<Prestamo>();
                _ = conn.CreateTable<Abono>();

                #endregion
                conn.Commit();
            }
            catch (SQLiteException e)
            {

                conn.Rollback();
            }
            finally
            {
                CloseDB();
            }
        }

        internal static T Get<T>(object pk) where T : new()
        {
            return conn.Get<T>(pk);
        }

        public static void DeleteAndDropDB()
        {
            GetConnection();
            try
            {
                conn.BeginTransaction();
                DeleteTables();
                DropTables();
                conn.Commit();
                CreateDB();
            }
            catch (SQLiteException ex)
            {
                conn.Rollback();
            }
            finally
            {
                CloseDB();
            }

            void DeleteTables()
            {
                //_ = conn.DeleteAll<Cliente>();
                _ = conn.DeleteAll<Prestamo>();
                _ = conn.DeleteAll<Abono>();
            }
            void DropTables()
            {
                //_ = conn.DropTable<Cliente>();
                _ = conn.DropTable<Prestamo>();
                _ = conn.DropTable<Abono>();
            }
        }

        public static bool InsertItem<T>(T item)
        {
            bool inserted = false;
            int result = -1;
            try
            {
                GetConnection();
                conn.BeginTransaction();
                result = conn.Insert(item);
                conn.Commit();

                if (result >= 0)
                    inserted = true;
            }
            catch (SQLiteException ex)
            {
                conn.Rollback();
                inserted = false;
            }
            finally
            {
                CloseDB();
            }
            return inserted;
        }

        public static bool InsertList<T>(List<T> lstItems) where T : IEntityDataBase
        {
            bool inserted = false;
            int result = -1;
            try
            {
                GetConnection();
                conn.BeginTransaction();
                result = conn.InsertAll(lstItems);
                conn.Commit();

                if (result >= 0)
                    inserted = true;
            }
            catch (SQLiteException ex)
            {
                conn.Rollback();
                inserted = false;
            }
            finally
            {
                CloseDB();
            }
            return inserted;
        }

        public bool Execute(string Query)
        {
            bool response = false;
            try
            {
                GetConnection();
                conn.BeginTransaction();
                int rows = conn.Execute(Query);
                conn.Commit();

                if (rows > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                response = false;
            }
            finally
            {
                CloseDB();
            }
            return response;
        }

        public static bool Update<T>(T item)
        {
            bool response = false;
            try
            {
                GetConnection();
                conn.BeginTransaction();
                int rows = conn.Update(item);
                conn.Commit();

                if (rows > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                response = false;
            }
            finally
            {
                CloseDB();
            }
            return response;
        }

        public bool Delete<T>(T item)
        {
            bool response = false;
            try
            {
                GetConnection();
                conn.BeginTransaction();
                int rows = conn.Delete(item);
                conn.Commit();

                if (rows > 0)
                    response = true;
            }
            catch (Exception ex)
            {
                conn.Rollback();
                response = false;
            }
            finally
            {
                CloseDB();
            }
            return response;
        }

        public static List<T> GetAll<T>() where T : new()
        {
            if (conn == null)
            {
                GetConnection();
            }
            return conn.Table<T>().ToList();
        }

        public static List<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : new()
        {
            if (conn == null)
            {
                GetConnection();
            }
            return conn.Table<T>().ToList();
        }

        //public List<T> GetList<T>(string tableName, string query)
        //{
        //    List<T> result = new List<T>();
        //    try
        //    {
        //        GetConnection();
        //        switch (tableName)
        //        {
        //            case "Clientes":
        //                result = conn.Query<ClientesE>(query).Cast<T>().ToList();
        //                break;
        //            case "Pedidos":
        //                result = conn.Query<PedidosE>(query).Cast<T>().ToList();
        //                break;
        //            case "PedidosDetalles":
        //                result = conn.Query<PedidosDetallesE>(query).Cast<T>().ToList();
        //                break;
        //            case "PedidosMovimientos":
        //                result = conn.Query<PedidosMovimientosE>(query).Cast<T>().ToList();
        //                break;
        //            case "VendedoresInventariosDetalle":
        //                result = conn.Query<VendedoresInventariosDellateE>(query).Cast<T>().ToList();
        //                break;
        //            case "InventariosBodegas":
        //                result = conn.Query<InventarioBodegaE>(query).Cast<T>().ToList();
        //                break;
        //            case "InventariosMovimientos":
        //                result = conn.Query<InventarioMovimientoE>(query).Cast<T>().ToList();
        //                break;
        //            default:
        //                result = null;
        //                break;
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        result = null;
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }
        //    return result;
        //}

        //public T GetDetail<T>(string tableName, string query) where T : new()
        //{
        //    T result = default(T);
        //    try
        //    {
        //        GetConnection();
        //        result = conn.Query<T>(query).Cast<T>().FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        result = default(T);
        //    }
        //    finally
        //    {
        //        CloseDB();
        //    }
        //    return result;
        //}

        public List<int> Query(string query)
        {
            List<int> response = null;
            try
            {
                GetConnection();
                conn.BeginTransaction();
                response = conn.Query<int>(query);
                conn.Commit();
            }
            catch (Exception)
            {
                conn.Rollback();
                response = new List<int>();
            }
            finally
            {
                CloseDB();
            }
            return response;
        }
    }
}
