using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using eShop.Constants;
using eShop.Services;
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
       
        }

        public async void LoadProductsData()
        {
            var result = await _productsService.GetAllProducts();
            if (result != null)
            {
                var minmumPrice = AppPersistenceService.GetValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE) != null ? (double)AppPersistenceService.GetValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE) : 0;
                var maximumPrice = AppPersistenceService.GetValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE) != null ? (double)AppPersistenceService.GetValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE) : 100;
                var filteredValue = result.Where(p => p.Price >= Convert.ToDecimal(minmumPrice) &&  p.Price <= Convert.ToDecimal(maximumPrice));

                Items = new ObservableCollection<Product>(filteredValue);

            }
           
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