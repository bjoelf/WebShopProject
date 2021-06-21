using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Products;

namespace WebShopProjectApp.Orders
{
    public class OrderItem
    {
        Product Product { get; set; }
        decimal Quantity { get; set; }
        decimal Price { get; set; }
        bool IsPaid { get; set; }
        decimal deliveredQuantity { get; set; }
    }
}
