using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using eShop.CustomControls;
using eShop.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace eShop.Droid
{
    public class ToastMessage:IToastMessage
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}