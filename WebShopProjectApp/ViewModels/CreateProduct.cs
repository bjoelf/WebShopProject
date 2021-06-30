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

        public CreateProduct(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public CreateProduct() { }
    }
}
