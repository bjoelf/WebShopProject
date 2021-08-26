using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Products;
using System.ComponentModel.DataAnnotations;

namespace WebShopProjectApp.Orders
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        //Product Product { get; set; }
        decimal Quantity { get; set; }
        decimal Price { get; set; }
        bool IsPaid { get; set; }
        decimal deliveredQuantity { get; set; }
    }
}
