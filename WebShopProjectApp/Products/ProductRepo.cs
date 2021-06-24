using System.Collections.Generic;
using System.Linq;

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
            _dBContext.Add(product);
            int ret = _dBContext.SaveChanges();
            if (ret == 0)
                return null;

            return product;
        }
        public List<Product> Read()
        {
            if (_dBContext.Products.Count() > 0)
                return _dBContext.Products.ToList();
            return null;
        }
        public Product Read(int id)
        {
            return _dBContext.Products.Find(id);
        }
        public Product Update(Product product)
        {
            Product p = Read(product.Id);

            if (p == null)
                return null;

            _dBContext.Update(p);
            int res = _dBContext.SaveChanges();

            if (res == 0)
                return null;

            return p;
        }
        public bool Delete(int id)
        {
            Product p = Read(id);

            if (p == null)
                return false;
            _dBContext.Products.Remove(p);
            int res = _dBContext.SaveChanges();

            if (res == 0)
                return false;

            return true;
        }
    }
}
