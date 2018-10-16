using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using eShop.iOS;
using eShop.Services;
using Foundation;
using SQLite;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace eShop.iOS
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}