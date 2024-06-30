using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
    }
}