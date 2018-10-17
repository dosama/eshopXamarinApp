using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace eShop.DBModels
{
    [Table("Cart")]
    public class CartModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
