using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class OrderDetail
    {
    
        public int OrderID { get; set; }
        public int ProductID { get; set; }
       
        public int Quantity { get; set; }
        public string EmailAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string Address { get; set; }
        public int Pincode { get; set; }
        public int PhoneNumber{ get;set;}

        // Navigation property to ProductDetail
        public ProductDetail Product { get; set; }
        public string ProductName { get; set; }
        

    }
}