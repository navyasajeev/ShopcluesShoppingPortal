using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class CartItem
    {
        
            public ProductDetail Product { get; set; }
            public int Quantity { get; set; }
        
    }
}