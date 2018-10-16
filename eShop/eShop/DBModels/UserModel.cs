using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace eShop.DBModels
{
    [Table("Users")]
    public class UserModel
    {
        [PrimaryKey]
        public Guid UserId { get; set; }
        [MaxLength(255)]
        public string UserName { get; set; }
    }
}
