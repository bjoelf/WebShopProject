using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebShopProjectApp.Users
{
    public class AppUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Street adress")]
        [StringLength(30, MinimumLength = 2)]
        public string StreetAdress { get; set; }

        [Required]
        [Display(Name = "Street Number")]
        [StringLength(5, MinimumLength = 1)]
        public string StreetNumber { get; set; }

        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(10, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string City { get; set; }
    }
}