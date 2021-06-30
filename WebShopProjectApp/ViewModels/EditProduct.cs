using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using WebShopProjectApp.Products;

namespace WebShopProjectApp.ViewModels
{
    public class EditProduct
    {
        public int Id { set; get; }
        public CreateProduct CreateProduct { set; get; }
        public EditProduct(int id, CreateProduct createProduct)
        {
            this.CreateProduct = createProduct;
            Id = id;
        }
    }
}
