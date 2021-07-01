﻿using Microsoft.AspNetCore.Http;
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

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            if (ModelState.IsValid)
            {
                User newUsr = new User()
                {
                    UserName = userReg.UserName,
                    FirstName = userReg.FirstName,
                    LastName = userReg.LastName,
                    StreetAdress = userReg.StreetAdress,
                    StreetNumber = userReg.StreetNumber,
                    PostalCode = userReg.PostalCode,
                    City = userReg.City,
                    Email = userReg.Email,
                    PhoneNumber = userReg.Phone
                };
                IdentityResult res = await _userManager.CreateAsync(newUsr, userReg.Password);

                if (res.Succeeded)
                    return RedirectToAction("Index", "Home");

                foreach (var item in res.Errors)
                    ModelState.AddModelError(item.Code, item.Description);
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
