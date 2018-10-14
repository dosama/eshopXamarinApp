using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Views
{

    public class ProductsMenuItem
    {
        public ProductsMenuItem()
        {
            TargetType = typeof(ProductsDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}