using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.CustomControls;
using eShop.DBModels;
using eShop.Webservice.Models;
using eShop.Webservice.Services;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace eShop.Services
{
    public class SyncDataService
    {
        private static SyncDataService instance = null;
        private readonly ProductsService _productsService;
        private SyncDataService()
        {
            _productsService = new ProductsService();
        }

        public static SyncDataService Instance => instance ?? (instance = new SyncDataService());

        public async Task<List<Product>> SyncProductsData()
        {
            List<Product> result = new List<Product>();
            DependencyService.Get<IToastMessage>().Show("App Data Sync is starting ..");
            try
            {
                var connection = DependencyService.Get<ISQLiteDb>().GetConnection();
               
                if (CrossConnectivity.Current.IsConnected)
                {
                    result = await _productsService.GetAllProducts();

                    if (result != null)
                    {

                        await connection.CreateTableAsync<ProductModel>();
                        await connection.CreateTableAsync<ProductImagesModel>();
                        List<ProductModel> products = new List<ProductModel>();
                        List<ProductImagesModel> productImages = new List<ProductImagesModel>();
                        foreach (var product in result)
                        {
                            products.Add(new ProductModel() { ProductId = product.ProductId, Description = product.Description, MainImageUrl = product.MainImageUrl, Price = product.Price, Title = product.Title });
                            foreach (var image in product.SlideShowImages)
                            {
                                productImages.Add(new ProductImagesModel() { ImageUrl = image, ProductId = product.ProductId });
                            }
                        }

                        await connection.DeleteAllAsync<ProductModel>();
                        await connection.InsertAllAsync(products);
                        await connection.DeleteAllAsync<ProductImagesModel>();
                        await connection.InsertAllAsync(productImages);
                    }

                }
                else
                {
                    var products = await connection.Table<ProductModel>().ToListAsync();
                    var images = await connection.Table<ProductImagesModel>().ToListAsync();

                    foreach (var item in products)
                    {
                        result.Add(new Product()
                        {
                            Description = item.Description,
                            MainImageUrl = item.MainImageUrl,
                            Price = item.Price,
                            ProductId = item.ProductId,
                            SlideShowImages = images.Where(i => i.ProductId == item.ProductId).Select(x => x.ImageUrl).ToList(),
                            Title = item.Title

                        });
                    }



                }

                DependencyService.Get<IToastMessage>().Show("App Data Sync Done ..");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                DependencyService.Get<IToastMessage>().Show("Error while App Data Sync ..");
            }

            return result;
        }
    
       
    }
}
