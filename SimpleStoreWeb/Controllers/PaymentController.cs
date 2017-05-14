using BusinessLayer.Interfaces;
using SimpleStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleStoreWeb.Controllers
{
    [Authorize]
    public class PaymentController : BaseController
    {
        private IUserService userService;
        private IOrderService orderService;

        public PaymentController(IUserService _userService, IOrderService _orderService)
        {
            userService = _userService;
            orderService = _orderService;
        }


        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        // POST: /User/Login
        [HttpPost]
        public ActionResult SubmitPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                int userID = userService.Get(CurrentUserLoginName).ID;

                orderService.Add(new BusinessLayer.Models.OrderModel()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                    Phone = model.Phone,
                    Email = model.Email
                }, userID);

                return RedirectToAction("Index", "Store");
            }
            else
            {
                ModelState.AddModelError("", "Fill required fields.");
            }

            return View("Index", model);
        }
    }
}