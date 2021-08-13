using System.Collections.Generic;
using System.Linq;

using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }
        public Product Add(CreateProduct createProduct)
        {
            Product p = new Product();

            p.Name = createProduct.Name;
            p.Description = createProduct.Description;

            return _productRepo.Create(p);
        }
        public List<Product> All()
        {
            return _productRepo.Read();
        }
        public Product FindById(int id)
        {
            return _productRepo.Read(id);
        }
        public Product Edit(int id, CreateProduct cp)
        {
            Product p = FindById(id);
            if (p == null)
                return null;

            p.Name = cp.Name;
            p.Description = cp.Description;
            p.Price = cp.Price;

            p = _productRepo.Update(p);

            return p;
        }
        public bool Remove(int id)
        {
            return _productRepo.Delete(id);
        }
    }
}
