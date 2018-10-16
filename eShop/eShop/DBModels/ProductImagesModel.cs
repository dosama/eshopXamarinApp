using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace eShop.DBModels
{
    [Table("ProductImages")]
    public class ProductImagesModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}
