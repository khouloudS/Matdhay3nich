using System;
using System.IO;
using Xamarin.Forms;
using Windows.Storage;
using App1.UWP;

[assembly: Dependency(typeof(SQLite_UWP))]

namespace App1.UWP
{
    class SQLite_UWP : ISQLite
    {
        public SQLite_UWP() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "App1SQLite.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }

    }
}
