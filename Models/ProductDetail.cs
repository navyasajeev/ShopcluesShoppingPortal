using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopcluesShoppingPortal.Models
{
    public class ProductDetail
    {
    
        [Key]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage="Please enter the product name")]
        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter the category name")]
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter the description of the product")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the stock")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a positive number.")]

        public int Stock { get; set; }

        
        [Required(ErrorMessage = "Enter the Created date")]
        [DataType(DataType.Date)]
        [Display(Name = "Created date")]
        public DateTime CreatedDate { get; set; }

       [Required(ErrorMessage = "Enter the price")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number.")]

        public int Price { get; set; }

        [Required(ErrorMessage ="Upload product image")]
        [Display(Name = "Product image")]
        public string ProductImage { get; set; }

    

    }
}