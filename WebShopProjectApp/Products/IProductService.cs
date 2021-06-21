using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Products
{
    public interface IProductService
    {
        Product Add(CreateProduct createProduct);
        List<Product> All();
        Product FindById(int id);
        Product Edit(int id, CreateProduct createProduct);
        bool Remove(int id);
    }
}
