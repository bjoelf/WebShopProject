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
    }
}

