using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using eShop.Constants;
using eShop.CustomControls;
using eShop.CustomRenders;
using eShop.DBModels;
using eShop.Services;
using eShop.Views;
using eShop.Webservice.Models;
using eShop.Webservice.Services;
using Xamarin.Forms;

namespace eShop.ViewModels
{
    public class ProductDetailsViewModel : ViewModelBase
    {
     
        public ProductDetailsViewModel()
        {

            AddToCartCommand = new Command(ExecuteAddToCart);
        }

        private async void ExecuteAddToCart()
        {

           
            var cartItemsCount = AppPersistenceService.GetValue(AppPropertiesKeys.CART_ITEMS_COUNT);

            if (cartItemsCount != null)
            {
                
                DependencyService.Get<IToolbarItemBadge>().SetBadge(View, View.ToolbarItems.First(), $"{(int)cartItemsCount + Quantity}", Color.Red, Color.White);
                
                AppPersistenceService.SaveValue(AppPropertiesKeys.CART_ITEMS_COUNT, (int) cartItemsCount + Quantity);
            }
            else
            {
                DependencyService.Get<IToolbarItemBadge>().SetBadge(View, View.ToolbarItems.First(), $"{Quantity}", Color.Red, Color.White);
                AppPersistenceService.SaveValue(AppPropertiesKeys.CART_ITEMS_COUNT, Quantity);
            }

            var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            await connection.CreateTableAsync<CartModel>();

            var cartItem = await connection.Table<CartModel>()
                .FirstOrDefaultAsync(x => x.ProductId == CurrentProduct.ProductId);
            if (cartItem == null)
                await connection.InsertAsync(new CartModel() { ProductId = CurrentProduct.ProductId , Price = CurrentProduct.Price,ProductImageUrl = CurrentProduct.MainImageUrl,ProductTitle = CurrentProduct.Title,Quantity = Quantity });
            else
            {
                cartItem.Quantity += Quantity;
                await connection.UpdateAsync(cartItem);
            }

            await View.Navigation.PushAsync(new HomePage());

            DependencyService.Get<IToastMessage>().Show("Cart Updated Successfully ..");

       

        }


        public ProductDetailsPage View { get; set; }

        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }
        public ICommand AddToCartCommand { get; private set; }
        private Product _currentProduct;
        public Product CurrentProduct
        {
            get => _currentProduct;
            set { _currentProduct = value; OnPropertyChanged();  }
        }
    }
}
