using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace eShop.DBModels
{
    [Table("Products")]
    public class ProductModel
    {
        [PrimaryKey]
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string MainImageUrl { get; set; }
    }
}
