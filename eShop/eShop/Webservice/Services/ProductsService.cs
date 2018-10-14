using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using eShop.Webservice.Models;

namespace eShop.Webservice.Services
{
    public class ProductsService:RestService
    {
        public ProductsService()
        {
            ServiceName = "Products";
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await GetDataAsync<Product>();
        }
    }
}
