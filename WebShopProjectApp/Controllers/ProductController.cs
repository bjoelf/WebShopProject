using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopProjectApp.Products;
using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_productService.All());
        }

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateProduct createProduct)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(createProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(createProduct);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product p = _productService.FindById(id);

            if (p == null)
                return RedirectToAction(nameof(Index));

            CreateProduct cp = new CreateProduct(p.Name,p.Description);
            EditProduct ep = new EditProduct(id, cp);

            return View(ep);
        }

        [HttpPost]
        public IActionResult Edit(int ID, CreateProduct createProduct)
        {
            if (ModelState.IsValid)
            {
                Product p = _productService.Edit(ID, createProduct);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Edit), new { id = ID });
        }

        public IActionResult Delete(int id)
        {
            Product p = _productService.FindById(id);

            if (p == null)
                return NotFound();

            if (_productService.Remove(id))
                return Ok("product" + id);

            return BadRequest();
        }
    }
}
