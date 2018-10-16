using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using eShop.Constants;
using eShop.CustomControls;
using eShop.DBModels;
using eShop.Services;
using eShop.Views;
using eShop.Webservice.Models;
using eShop.Webservice.Services;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace eShop.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public async void LoadProductsData()
        {
            IsViewLoading = true;
            

            var result = await SyncDataService.Instance.SyncProductsData();
            var minmumPrice = AppPersistenceService.GetValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE) != null ? (double)AppPersistenceService.GetValue(AppPropertiesKeys.MINIMUM_FILTER_VALUE) : 0;
            var maximumPrice = AppPersistenceService.GetValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE) != null ? (double)AppPersistenceService.GetValue(AppPropertiesKeys.MAXIMUM_FILTER_VALUE) : 100;
            var filteredValue = result?.Where(p => p.Price >= Convert.ToDecimal(minmumPrice) && p.Price <= Convert.ToDecimal(maximumPrice));

            if (filteredValue!= null)
            Items = new ObservableCollection<Product>(filteredValue);

            IsViewLoading = false;

        }


        private bool _isViewLoading;
        public bool IsViewLoading
        {
            get => _isViewLoading;
            set { _isViewLoading = value; OnPropertyChanged(); }
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