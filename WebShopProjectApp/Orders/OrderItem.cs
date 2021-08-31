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
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
    }
}