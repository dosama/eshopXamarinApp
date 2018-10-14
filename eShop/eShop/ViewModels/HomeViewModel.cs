using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using eShop.Views;
using eShop.Webservice.Models;
using eShop.Webservice.Services;
using Xamarin.Forms;

namespace eShop.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ProductsService _productsService;

        public HomeViewModel()
        {
            _productsService = new ProductsService();
            LoadProductsData();
        }

        private async void LoadProductsData()
        {
            var result = await _productsService.GetAllProducts();
            if (result != null)
                Items = new ObservableCollection<Product>(result);
        }

        public HomePage View { get; set; }
        private ObservableCollection<Product> _items;

        public ObservableCollection<Product> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }
    }
}