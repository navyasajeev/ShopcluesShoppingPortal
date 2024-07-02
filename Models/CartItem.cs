using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class CartItem
    {
        [Key]
            public int CartItemId { get; set; }

   
            public string EmailAddress { get; set; }
            public int ProductID { get; set; }

           public int Quantity { get; set; }
        
    }
}