using System.ComponentModel.DataAnnotations;

namespace WebShopProjectApp.ViewModels
{
    public class CreateProduct
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public CreateProduct(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        public CreateProduct() { }
    }
}
