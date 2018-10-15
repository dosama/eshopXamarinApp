using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.Constants;
using eShop.Services;
using eShop.Views;
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
		    Title = AppPersistenceService.GetValue(AppPropertiesKeys.USER_NAME) +"";
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

	    private void Sync_OnClicked(object sender, EventArgs e)
	    {
	    
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