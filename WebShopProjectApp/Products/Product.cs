using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopProjectApp.Products
{
    public class Product
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
