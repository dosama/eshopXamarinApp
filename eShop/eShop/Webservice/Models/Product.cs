using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Webservice.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> SlideShowImages { get; set; }
    }
}
