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
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            Order order = _orderService.FindById(id);

            if (order == null)
                return RedirectToAction(nameof(Index));

            CreateOrder co = new CreateOrder();
            co.Items = order.items;

            EditOrder editOrder = new EditOrder(id, co);

            return View(editOrder);
        }

      
        [HttpPost]
        public IActionResult Edit(int id, CreateOrder order)
        {
            if (ModelState.IsValid)
            {
                Order o = _orderService.Edit(id, order);
                return RedirectToAction(nameof(Index));
            }

            //TODO: Ändra här
            return RedirectToAction(nameof(Index));
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
