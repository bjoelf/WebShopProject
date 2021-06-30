using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Orders;
using WebShopProjectApp.Users;

namespace WebShopProjectApp.ViewModels
{
    public class CreateOrder
    {
        public List<OrderItem> Items { get; set; }
        public User Customer { get; set; }
        public CreateOrder(List<OrderItem> items, User customer)
        {
            this.Items = items;
            this.Customer = customer;
        }
        public CreateOrder() { }
    }
}

