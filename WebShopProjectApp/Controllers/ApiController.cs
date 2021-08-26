using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

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
        private readonly UserManager<User> _userManager;

        public ApiController(IOrderService orderService, IProductService productService, IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _orderService = orderService;
            _productService = productService;
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Users (Customers)

        [HttpPost("/api/RegUser")]
        public async Task<ActionResult<User>> Post([FromBody] RegisterUser userReg)
        {
            if (!ModelState.IsValid)
                return BadRequest(userReg);
            else
            {
                User u = await _userService.Add(userReg);
                Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.PasswordSignInAsync(userReg.UserName, userReg.Password, false, false);

                //TODO: Make sure this is Shop homepage.
                // changed to return Created("", u);
                if (res.Succeeded)
                    return Created("", u);

                if (res.IsLockedOut)
                    //ModelState.AddModelError("Locked out!", "Too many attemts");
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Created("", userReg);
        }

        [HttpPut("/api/EditUser")]
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
        //[Authorize]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            List<Product> pl = _productService.All();
            return Ok(pl);
        }

        #endregion

        #region Orders

        [HttpPost("/api/Order")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Order>> Post(CreateOrder createOrder)
        {
            if (createOrder.Customer == null)
            {
                string id = null;
                foreach (var item in User.Claims)
                {
                    if (item.Type == ClaimTypes.NameIdentifier)
                    {
                        id = item.Value;
                        break;
                    }
                }
                User u = await _userManager.FindByNameAsync(id);
                createOrder.Customer = u;
            }

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
