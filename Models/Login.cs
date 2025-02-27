﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class Login
    {
        
        [Required(ErrorMessage = "Enter emailaddress")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [Display(Name = "Password")]
          [DataType(DataType.Password)]
        public string Password { get; set; }
    
    }
}