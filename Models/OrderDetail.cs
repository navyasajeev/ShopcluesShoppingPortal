using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }      
        public string EmailAddress { get; set; }
        public DateTime OrderDate { get; set; }

        public int TotalAmount { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the pincode")]
        public int Pincode { get; set; }
        [Required(ErrorMessage = "Please enter the phone number")]
        public int PhoneNumber{ get;set;}
       

        // Navigation property to ProductDetail
        public ProductDetail Product { get; set; }
      
        

    }
}