using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterPage : ContentPage
	{
		public FilterPage ()
		{
			InitializeComponent ();
		}

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PopModalAsync();
	    }
	}
}