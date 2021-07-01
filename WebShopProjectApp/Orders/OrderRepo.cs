using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.Products;
using WebShopProjectApp.Users;
using WebShopProjectApp.Database;

namespace WebShopProjectApp.Orders
{
    public class OrderRepo : IOrderRepo
    {
        private readonly DBContext _dBContext;
        public OrderRepo(DBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Order Create(Order order)
        {
            _dBContext.Add(order);
            int ret = _dBContext.SaveChanges();
            if (ret == 0)
                return null;

            return order;
        }
        public List<Order> Read()
        {
            if (_dBContext.Orders.Count() > 0)
                return _dBContext.Orders.ToList();
            return null;
        }
        public Order Read(int id)
        {
            return _dBContext.Orders.Find(id);
        }
        public List<Order> ReadCustomer(string customerId)
        {
            return _dBContext.Orders.Where(ci => ci.Customer.Id.Equals(customerId)).ToList();
        }
        public Order Update(Order order)
        {
            Order o = Read(order.Id);

            if (o == null)
                return null;

            _dBContext.Update(o);
            int res = _dBContext.SaveChanges();

            if (res == 0)
                return null;

            return o;
        }
        public bool Delete(int id)
        {
            Order o = Read(id);

            if (o == null)
                return false;
            _dBContext.Orders.Remove(o);
            int res = _dBContext.SaveChanges();

            if (res == 0)
                return false;

            return true;
        }
    }
}
