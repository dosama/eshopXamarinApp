using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.CustomControls;
using eShop.ViewModels;
using eShop.Webservice.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductDetailsPage : ToolBarPage
	{

	    private readonly ProductDetailsViewModel _viewModel;
		public ProductDetailsPage(Product selectedItem)
		{
			InitializeComponent ();
		    _viewModel = new ProductDetailsViewModel();
		    BindingContext = _viewModel;
		    _viewModel.View = this;
		    _viewModel.CurrentProduct = selectedItem;

		}

	}
}