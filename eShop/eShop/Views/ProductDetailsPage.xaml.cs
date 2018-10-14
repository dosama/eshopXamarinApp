using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.CustomControls;
using eShop.Webservice.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductDetailsPage : ToolBarPage
    {
		public ProductDetailsPage(string selectedItem)
		{
			InitializeComponent ();
		    lblWelcome.Text = "Welcome to Product " + selectedItem;

		}

	}
}