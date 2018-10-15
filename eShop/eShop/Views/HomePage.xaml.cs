using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using eShop.CustomControls;
using eShop.ViewModels;
using eShop.Webservice.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eShop.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ToolBarPage
    {
        private readonly HomeViewModel _viewModel;


    public HomePage()
        {
            InitializeComponent();
            _viewModel = new HomeViewModel();
            BindingContext = _viewModel;
            _viewModel.View = this;
        }

         
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new ProductDetailsPage((Product)e.Item));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadProductsData();
        }
    }
}
