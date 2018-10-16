using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eShop.Constants;
using eShop.CustomControls;
using eShop.Services;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace eShop
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new eShop.MainPage())
			{
			    BarBackgroundColor = Color.LawnGreen,
                BarTextColor = Color.Black
            };;

		    CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

        }

	    private async void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
	    {
	        AppPersistenceService.SaveValue(AppPropertiesKeys.IS_ONLINE, e.IsConnected);
	        if (!e.IsConnected)
	        {
	            DependencyService.Get<IToastMessage>().Show("You Are Offline Now .. Please turn on internet Connectivity");
            }


	    }
        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
