using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace eShop.Services
{
    public static class AppPersistenceService
    {
        public static void SaveValue(string key, object newValue)
        {
           
            if (Application.Current.Properties.ContainsKey(key))
                Application.Current.Properties[key] = newValue;
            else
                Application.Current.Properties.Add(key, newValue);
        }

        public static object GetValue(string key)
        {
            try
            {
                if (Application.Current.Properties.ContainsKey(key))
                {
                    return Application.Current.Properties[key];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
         
            return null;
        }
    }
}
