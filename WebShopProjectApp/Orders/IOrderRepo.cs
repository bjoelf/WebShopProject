using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.Products;
using WebShopProjectApp.Users;


namespace WebShopProjectApp.Orders
{
    public interface IOrderRepo
    {
        Order Create(Order order);
        Order Read(int id);
        List<Order> Read();
        Order Update(Order order);
        bool Delete(int id);
    }
}
