using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eShop.CustomControls;
using eShop.iOS;
using Foundation;
using UIKit;


[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace eShop.iOS
{
    class ToastMessage:IToastMessage
    {

        const double LONG_DELAY = 3.5;

        UIAlertController alert;

        public void Show(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }


        void ShowAlert(string message, double seconds)
        {
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);

            NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                if (alert != null)
                {
                    UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, () =>
                    {
                        alert.DismissViewController(true, null);
                        alert.Dispose();

                        obj?.Dispose();
                    });


                }
            });
            
        }

    }
}