using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleStoreWeb.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: /User/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //var user = userService.Get("", "Password");
                if (userService.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View(model);
        }

        //
        // GET: /User/Register
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /User/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var membershipUser = userService.CreateUser(model.UserName, model.Password);

                if (membershipUser)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "User not created");
                }
            }

            return View(model);
        }

        //
        // POST: /User/Logout
        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("/");
            }
        }


    }
}