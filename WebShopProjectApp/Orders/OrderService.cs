using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.ViewModels;

using WebShopProjectApp.Database;

namespace WebShopProjectApp.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Order Add(CreateOrder createOrder)
        {
            Order o = new Order();

            o.Customer = createOrder.Customer;
            o.items = createOrder.Items;

            return _orderRepo.Create(o);
        }
        public List<Order> All()
        {
            return _orderRepo.Read();
        }
        public Order FindById(int id)
        {
            return _orderRepo.Read(id);
        }
        public Order Edit(int id, CreateOrder createOrder)
        {
            Order o = FindById(id);
            if (o == null)
                return null;

            o.Customer = createOrder.Customer;
            o.items = createOrder.Items;
            o = _orderRepo.Update(o);

            return o;
        }
        public bool Remove(int id)
        {
            return _orderRepo.Delete(id);
        }
    }
}
