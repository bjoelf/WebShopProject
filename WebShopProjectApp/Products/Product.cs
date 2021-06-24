using System.ComponentModel.DataAnnotations;

namespace WebShopProjectApp.Products
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

    }
}
