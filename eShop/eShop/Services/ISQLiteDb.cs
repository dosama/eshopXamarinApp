using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace eShop.Services
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
