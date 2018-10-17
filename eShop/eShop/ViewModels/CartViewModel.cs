using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using eShop.Constants;
using eShop.CustomControls;
using eShop.CustomRenders;
using eShop.DBModels;
using eShop.Services;
using eShop.Views;
using eShop.Webservice.Models;
using eShop.Webservice.Services;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace eShop.ViewModels
{
    public class CartViewModel:ViewModelBase
    {

        private readonly PurshasesService _purshaseService;
        public CartViewModel()
        {
            _purshaseService = new PurshasesService();
            SubmitCommand = new Command(ExecuteSubmit, CanExecuteSubmit);
        }

        private bool CanExecuteSubmit()
        {
            return IsSubmitEnabled;
        }

        private void ExecuteSubmit()
        {
           SubmitCartData();
        }

        public async void LoadCartData()
            {
                var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                await connection.CreateTableAsync<CartModel>();
            var cartModels = await connection.Table<CartModel>().ToListAsync();
                if (cartModels != null)
                    Items = new ObservableCollection<CartModel>(cartModels);
            }
        private bool _isViewLoading;
        public bool IsViewLoading
        {
            get => _isViewLoading;
            set { _isViewLoading = value; OnPropertyChanged(); }
        }
        public async void SubmitCartData()
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                IsViewLoading = true;
                var userId = (Guid) AppPersistenceService.GetValue(AppPropertiesKeys.USER_ID);
                var putshases = new List<Purshase>();
                foreach (var item in Items)
                {

                    putshases.Add(new Purshase()
                    {
                        PurshaseId = Guid.NewGuid(),
                        CreatedOn = DateTime.Now,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UserId = userId
                    });
                }
                await _purshaseService.SubmitPurshase(putshases);
                DependencyService.Get<IToolbarItemBadge>().SetBadge(View, View.ToolbarItems.First(), $"{0}", Color.Red, Color.White);
                AppPersistenceService.SaveValue(AppPropertiesKeys.CART_ITEMS_COUNT, 0);
                var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                await connection.DeleteAllAsync<CartModel>();
                IsViewLoading = false;

                await View.Navigation.PushAsync(new HomePage());

                DependencyService.Get<IToastMessage>().Show("Order Was Submitted Successfully ..");
            }
            else
            {
                await View.DisplayAlert("Warning",
                    "Please turn on intenet connectivity in order to submit your order .. ", "OK");
            }
        }

        public async void DeleteCartItem(int id)
        {
            try
            {
                var cartItem = Items.FirstOrDefault(i => i.Id == id);

                if (cartItem != null)
                {
                    var currentItemsCount = (int)AppPersistenceService.GetValue(AppPropertiesKeys.CART_ITEMS_COUNT);
                    DependencyService.Get<IToolbarItemBadge>().SetBadge(View, View.ToolbarItems.First(), $"{currentItemsCount - cartItem.Quantity}", Color.Red, Color.White);
                    AppPersistenceService.SaveValue(AppPropertiesKeys.CART_ITEMS_COUNT, currentItemsCount - cartItem.Quantity);
                    Items.Remove(cartItem);

                    var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
                    await connection.DeleteAsync<CartModel>(id);
                }
             
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              
            }
          
          
        }

        public CartPage View { get; set; }
            public bool IsSubmitEnabled
            {
                get => Items != null && Items.Count > 0;
            }

        public bool IsSubmitDisabled => !IsSubmitEnabled;

        public ICommand SubmitCommand { get; private set; }
        private ObservableCollection<CartModel> _items;
            public ObservableCollection<CartModel> Items
            {
                get => _items;
                set
                {
                    _items = value;
                    OnPropertyChanged();
                OnPropertyChanged(nameof(IsSubmitEnabled));
                OnPropertyChanged(nameof(IsSubmitDisabled));
                }
            }
        }
    }
