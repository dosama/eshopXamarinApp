using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CartPage : ToolBarPage
    {
		public CartPage ()
		{
			InitializeComponent ();
		}
	}
}