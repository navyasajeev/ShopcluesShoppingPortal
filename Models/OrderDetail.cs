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
        [Display(Name = "Order ID")]
        public int OrderID { get; set; }

        [Display(Name = "Product name")]
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Order date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Total amount")]
        public int TotalAmount { get; set; }

        [Required(ErrorMessage = "Please enter the address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter the pincode")]
        public int Pincode { get; set; }

        [Required(ErrorMessage = "Please enter the phone number")]
        [Display(Name = "Phone number")]
        public int PhoneNumber{ get;set;}
       

        // Navigation property to ProductDetail
        public ProductDetail Product { get; set; }

        public string Status { get; set; }



    }
}