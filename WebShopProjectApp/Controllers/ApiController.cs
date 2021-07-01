using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

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

        public ApiController(IOrderService orderService, IProductService productService, IUserService userService)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
        }

        #region Users (Customers)

        [HttpPost]
        public ActionResult<User> Post([FromBody] UserRegViewModel newCustomer)
        {
            if (!ModelState.IsValid)
                return BadRequest(newCustomer);

            User u = _userService.Add(newCustomer);
            if (u== null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Created("", u);
        }

        [HttpPost]
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
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            List<Product> pl = _productService.All();
            return Ok(pl);
        }

        #endregion

        #region Orders

        [HttpPost("/api/Order")]
        public ActionResult<Order> Post([FromBody] CreateOrder createOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(createOrder);

            Order o = _orderService.Add(createOrder);
            if (o == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Created("", o);
        }

        [HttpGet("/api/Order/{id}")]
        public ActionResult<IEnumerable<Order>> GetOrders(string customerId)
        {
            List<Order> ol = _orderService.FindByCustomer(customerId); 
            return Ok(ol);
        }

        #endregion
    }
}
