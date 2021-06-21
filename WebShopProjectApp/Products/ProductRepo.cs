using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.Database;

namespace WebShopProjectApp.Products
{
    public class ProductRepo : IProductRepo
    {
        private readonly DBContext _dBContext;
        public ProductRepo(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Product Create(Product product)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Product Edit(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Read(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Read()
        {
            throw new NotImplementedException();
        }
    }
}
