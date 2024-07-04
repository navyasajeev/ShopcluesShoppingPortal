using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class ProductDetail
    {
    
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage="Please enter the product name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter the category name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter the descriptionof the product")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the stock")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Enter the Created date")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Enter the price")]
        public int Price { get; set; }

        [Required(ErrorMessage ="Upload product image")]
        public string ProductImage { get; set; }

      //  [NotMapped]
     //  public HttpPostedFileBase ProductImageFile { get; set; }

    }
}