using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eShop.Droid;
using eShop.Services;
using SQLite;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(SQLiteDb))]

namespace eShop.Droid
{
    public class SQLiteDb:ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}