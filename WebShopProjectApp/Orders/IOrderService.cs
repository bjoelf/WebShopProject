using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Orders
{
    interface IOrderService
    {
        Order Add(CreateOrder createOrder);
        List<Order> All();
        Order FindById(int id);
        Order Edit(int id, CreateOrder createOrder);
        bool Remove(int id);
    }
}
