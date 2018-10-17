using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using eShop.CustomControls;
using eShop.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ToolBarPage
    {
        private readonly CartViewModel _viewModel;  

        public CartPage()
        {
            InitializeComponent();
            _viewModel = new CartViewModel();
            BindingContext = _viewModel;
            _viewModel.View = this;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadCartData();
        }

        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var itemId = (int) menuItem?.CommandParameter;
            _viewModel.DeleteCartItem(itemId);
        }
    }
}
