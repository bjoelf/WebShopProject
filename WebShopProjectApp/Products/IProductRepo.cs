using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopProjectApp.Products
{
    public interface IProductRepo
    {
        Product Create(Product product);
        Product Read(int id);
        List<Product> Read();
        Product Update(Product product);
        bool Delete(int id);
    }
}
