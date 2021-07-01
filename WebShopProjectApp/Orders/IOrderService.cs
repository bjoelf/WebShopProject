using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Orders
{
    public interface IOrderService
    {
        Order Add(CreateOrder createOrder);
        List<Order> All();
        Order FindById(int id);
        List<Order> FindByCustomer(string customerId);
        Order Edit(int id, CreateOrder createOrder);
        bool Remove(int id);
    }
}
