using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebShopProjectApp.Users
{
    public class JwtLogin
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
