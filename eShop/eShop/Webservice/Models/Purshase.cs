using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Webservice.Models
{
    public class Purshase
    {
        public Guid PurshaseId { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
