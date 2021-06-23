using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopProjectApp.ViewModels
{
    public class UserRegViewModel
    {
        [Required]
        [StringLength(30, MinimumLength =5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
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
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(60, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
