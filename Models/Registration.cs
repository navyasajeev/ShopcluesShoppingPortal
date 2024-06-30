using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopcluesShoppingPortal.Models
{
    public class Registration
    {

        public int UserID { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        [Display(Name = "First name")]
        ///  [StringLength(maximumLength:7,MinimumLength =3,ErrorMessage ="Firstname length must be Maximum 7 & minimum 3")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Select the gender")]
        [Display(Name = "Gender")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Select the date of birth")]
        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Enter phone number")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Enter the state")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required(ErrorMessage = "Enter the city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter emailaddress")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Enter password")]
//[StringLength(maximumLength: 20, MinimumLength = 8, ErrorMessage = "Password length must be Maximum 20 & minimum 8")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter confirm password")]
        [Display(Name = "Confirm password")]
       [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}