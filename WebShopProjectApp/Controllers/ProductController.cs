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

        public ActionResult Details(int id)
        {
            return View();
        }

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

            EditProduct editProduct = new EditProduct();
            editProduct.Id = id;
            editProduct.CreateProduct = new CreateProduct()
            {
                Name = p.Name,
                Description = p.Description,
            };
            return View(editProduct);
        }

        [HttpPost]
        public ActionResult Edit(int id, CreateProduct createProduct)
        {
            if (ModelState.IsValid)
            {
                Product p = _productService.Edit(id, createProduct);
                return RedirectToAction(nameof(Index));
            }

            EditProduct editProduct = new EditProduct() {
                Id = id,
                CreateProduct = createProduct,
            };
            return View(editProduct);
        }

        public IActionResult Delete(int id)
        {
            Product p = _productService.FindById(id);

            if (p == null)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
