using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.Constants;
using eShop.CustomRenders;
using eShop.DBModels;
using eShop.Services;
using eShop.Views;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ToolBarPage : ContentPage
	{
        
		public ToolBarPage ()
		{
			InitializeComponent ();
		   
        }

	    protected async override void OnAppearing()
	    {
	        base.OnAppearing();

	        Title = AppPersistenceService.GetValue(AppPropertiesKeys.USER_NAME) + "";
	        var cartItemsCount = AppPersistenceService.GetValue(AppPropertiesKeys.CART_ITEMS_COUNT);

	        if (cartItemsCount != null)
	        {

	            DependencyService.Get<IToolbarItemBadge>().SetBadge(this, ToolbarItems.First(), $"{(int)cartItemsCount}", Color.Red, Color.White);

	        }
	        else
	        {
	            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
	            var cartItems = await connection.Table<CartModel>().ToListAsync();
	            AppPersistenceService.SaveValue(AppPropertiesKeys.CART_ITEMS_COUNT, cartItems.Count);
	            DependencyService.Get<IToolbarItemBadge>().SetBadge(this, ToolbarItems.First(), $"{cartItems.Count}", Color.Red, Color.White);

	        }

	        CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
        }

	    private async void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
	    {
	        AppPersistenceService.SaveValue(AppPropertiesKeys.IS_ONLINE, e.IsConnected);
	        if (e.IsConnected)
	        {
	            await StartSyncProcess();
	        }
	    }

	

        private async void Cart_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CartPage());

        }

	    private async void Home_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new HomePage());
        }

	    private async void Filter_OnClicked(object sender, EventArgs e)
	    {
	        if (Navigation.NavigationStack.Last().GetType() == typeof(HomePage))
	        {
	            await Navigation.PushModalAsync(new FilterPage());
            }
	   
        }

	    private async void Sync_OnClicked(object sender, EventArgs e)
	    {

	       await StartSyncProcess();
	    }

	    public async Task StartSyncProcess()
	    {
	        DependencyService.Get<IToastMessage>().Show("App Data Sync is starting ..");
            await SyncDataService.Instance.SyncProductsData();
	        DependencyService.Get<IToastMessage>().Show("App Data Sync Done ..");
        }


        private async void Logout_OnClicked(object sender, EventArgs e)
	    {
	        AppPersistenceService.SaveValue(AppPropertiesKeys.USER_NAME, string.Empty);
            await Navigation.PushAsync(new MainPage());
        }
	    protected override bool OnBackButtonPressed()
	    {
	        return true;
	    }
    }
}