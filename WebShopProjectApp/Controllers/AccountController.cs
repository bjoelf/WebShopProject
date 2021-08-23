using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using WebShopProjectApp.Users;
using WebShopProjectApp.ViewModels;

namespace WebShopProjectApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUser userReg)
        {
           // userReg.UserName = userReg.Email;

            if (!ModelState.IsValid)
                return BadRequest(userReg);
            else
            {
                User u = await _userService.Add(userReg);
              
                // Koden körs i UserService.cs
                //  IdentityResult res = await _userManager.CreateAsync(u, userReg.Password);

                //if (res.Succeeded)
                //    return RedirectToAction("Index", "Home");

                //foreach (var item in res.Errors)
                //    ModelState.AddModelError(item.Code, item.Description);
            }
            return View(userReg);
        }

        [HttpGet]
        public IActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logon(LogonViewModel logUser)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.PasswordSignInAsync(logUser.UserName, logUser.Password, false, false);

                if (res.Succeeded)
                    return RedirectToAction("Index", "Home");

                if (res.IsLockedOut)
                    ModelState.AddModelError("Locked out!", "Too many attemts");
            }
            return View(logUser);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
