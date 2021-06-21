using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Products;

namespace WebShopProjectApp.Orders
{
    interface IOrderRepo
    {
        Order Create(Product product);
        Order Read(int id);
        List<Order> Read();
        Order Edit(Order order);
        bool Delete(int id);
    }
}
