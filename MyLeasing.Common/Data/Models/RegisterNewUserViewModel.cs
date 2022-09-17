﻿using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Common.Models
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "Document")]
        public string Document { get; set; }



        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }





        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }


        [Required]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }



        [Required]
        [MinLength(6)]
        public string Password { get; set; }


        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
