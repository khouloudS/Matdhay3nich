using System;
using System.IO;
using Xamarin.Forms;
using Windows.Storage;
using App1.Windows;

[assembly: Dependency(typeof(SQLite_WinApp))]

namespace App1.Windows
{
    class SQLite_WinApp : ISQLite
    {
        public SQLite_WinApp() {}
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "App1SQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);

            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection 
            return conn;
        }
    }
}
