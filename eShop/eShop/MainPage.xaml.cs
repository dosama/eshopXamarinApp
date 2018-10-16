using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.Constants;
using eShop.Services;
using eShop.ViewModels;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace eShop
{
	public partial class MainPage : ContentPage
	{

	    private readonly LoginViewModel _viewModel;
		public MainPage()
		{
			InitializeComponent();
		    _viewModel = new LoginViewModel();
		    this.BindingContext = _viewModel;
		    _viewModel.View = this;
		


        }


	    protected override void OnAppearing()

	    {
	        base.OnAppearing();

	        AppPersistenceService.SaveValue(AppPropertiesKeys.IS_ONLINE, CrossConnectivity.Current.IsConnected);
            
	    }
    }
}
