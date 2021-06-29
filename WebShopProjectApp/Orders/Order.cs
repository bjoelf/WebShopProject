using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Users;
using System.ComponentModel.DataAnnotations;


namespace WebShopProjectApp.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public List<OrderItem> items { get; set; }
        public User Customer { get; set; }
    }
}
