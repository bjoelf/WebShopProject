using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebShopProjectApp.Orders;
using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: OrderController
        public ActionResult Index()
        {
            return View(_orderService.All());
        }

        // GET: OrderController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrder createOrder)
        {
            if (ModelState.IsValid)
            {
                _orderService.Add(createOrder);
                return RedirectToAction(nameof(Index));
            }
            return View(createOrder);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Order order = _orderService.FindById(id);

            if (order == null)
                return RedirectToAction(nameof(Index));

            CreateOrder co = new CreateOrder(order.items,order.Customer);
            EditOrder editOrder = new EditOrder(id, co);

            return View(editOrder);
        }
              
        [HttpPost]
        public IActionResult Edit(int ID, CreateOrder order)
        {
            if (ModelState.IsValid)
            {
                Order o = _orderService.Edit(ID, order);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Edit), new { id = ID });
        }

        // GET: OrderController/Delete/5
        public IActionResult Delete(int id)
        {
            Order order = _orderService.FindById(id);

            if (order == null) 
                return NotFound();

            if (_orderService.Remove(id))
                return Ok("order" + id);

            return BadRequest();
        }
    }
}
