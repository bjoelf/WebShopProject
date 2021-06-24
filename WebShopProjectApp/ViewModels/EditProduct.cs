using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Products;

namespace WebShopProjectApp.ViewModels
{
    public class EditProduct
    {
        public int Id { set; get; }
        public CreateProduct CreateProduct { set; get; }
        public EditProduct(int id, Product product)
        {
            Id = id;
            this.CreateProduct = new CreateProduct() 
            {
                Name = product.Name, 
                Description = product.Description 
            };
        }
    }
}
