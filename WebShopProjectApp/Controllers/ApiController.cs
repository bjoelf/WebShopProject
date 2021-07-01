using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using WebShopProjectApp.Orders;
using WebShopProjectApp.Products;
using WebShopProjectApp.Users;
using WebShopProjectApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebShopProjectApp.Api
{
    [EnableCors("ReactPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;

        public ApiController(IOrderService orderService, IProductService productService, IUserService userService, SignInManager<User> signInManager
            )
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
            _signInManager = signInManager;
        }

        #region Users (Customers)

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] RegisterUser newCustomer)
        {
            if (!ModelState.IsValid)
                return BadRequest(newCustomer);
            else
            {
                User u = _userService.Add(newCustomer);
                Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.PasswordSignInAsync(newCustomer.UserName, newCustomer.Password, false, false);

                //TODO: Make sure this is Shop homepage.
                if (res.Succeeded)
                    return RedirectToAction("Index", "Home");

                if (res.IsLockedOut)
                    ModelState.AddModelError("Locked out!", "Too many attemts");
            }
            return Created("", newCustomer);
        }

        [HttpPut]
        [Authorize]
        public ActionResult<User> Edit([FromBody] EditUser editUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(editUser);

            User u = _userService.Edit(editUser.Id, editUser.regUser);
            if (u == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Created("", u);
        }

        #endregion

        #region Products

        [HttpGet("/api/Products")]
        [Authorize]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            List<Product> pl = _productService.All();
            return Ok(pl);
        }

        #endregion

        #region Orders

        [HttpPost("/api/Order")]
        [Authorize]
        public ActionResult<Order> Post([FromBody] CreateOrder createOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(createOrder);

            Order o = _orderService.Add(createOrder);
            if (o == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Created("", o);
        }

        [Authorize]
        [HttpGet("/api/Order/{id}")]
        public ActionResult<IEnumerable<Order>> GetOrders(string customerId)
        {
            List<Order> ol = _orderService.FindByCustomer(customerId);
            return Ok(ol);
        }

        #endregion
    }
}
